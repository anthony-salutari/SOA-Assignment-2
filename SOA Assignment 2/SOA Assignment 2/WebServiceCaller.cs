using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services.Description;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOA_Assignment_2
{
    class WebServiceCaller
    {
        //DONE
        //This method generates proxy code from a webservice's WSDL file, compiles and assembles it. It returns the compiled on success, 
        //null on failure.
        private CompilerResults GenerateProxyWebService(string webServiceURL)
        {
            string[] assemblyReferences = {"System.dll", 
                                           "System.Web.Services.dll", 
                                           "System.Web.dll", 
                                           "System.Xml.dll", 
                                           "System.Data.dll"};
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CodeNamespace webNameSpace = new CodeNamespace();
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            ServiceDescriptionImporter webServiceImporter = new ServiceDescriptionImporter();
            WebClient webServiceClient = new WebClient();

            try
            {
                //Connect to new web service through the passed URL...                    
                Stream serviceStream = webServiceClient.OpenRead(webServiceURL + "?wsdl");
                //This gets the WSDL file...
                ServiceDescription webServiceDescription = ServiceDescription.Read(serviceStream);
                //Initalize the importer...
                webServiceImporter.ProtocolName = "Soap12";
                webServiceImporter.AddServiceDescription(webServiceDescription, null, null);

                //Generate the proxy code
                webServiceImporter.Style = ServiceDescriptionImportStyle.Client;
                webServiceImporter.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;
                //Initialize the Code-DOM tree
                compileUnit.Namespaces.Add(webNameSpace);
                //Import the service
                ServiceDescriptionImportWarnings importWarning = webServiceImporter.Import(webNameSpace, compileUnit);

                //If no warnings...
                if (importWarning == 0)
                {
                    //Generate code...
                    CompilerParameters compilerParameters = new CompilerParameters(assemblyReferences);
                    compilerParameters.GenerateInMemory = true;
                    CompilerResults compilerResults = codeProvider.CompileAssemblyFromDom(compilerParameters, compileUnit);

                    if (compilerResults.Errors.Count > 0)
                    {
                        throw new System.Exception("Compiler error occured when calling webservice...");
                    }

                    return compilerResults;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //DONE
        //This method generates a list of operations from a web service description. Returns a list of the 
        //webservice's operations on success, and null on failure.
        public string[] GetListOfOperations(string webServiceURL)
        {
            List<string> ListOfOperations = new List<string>();
            WebClient webServiceClient = new WebClient();
            Binding binding = new Binding();

            try
            {
                Stream serviceStream = webServiceClient.OpenRead(webServiceURL + "?wsdl");
                //This gets the WSDL file...
                ServiceDescription webServiceDescription = ServiceDescription.Read(serviceStream);

                //Get a list of operations from the web service description...
                binding = webServiceDescription.Bindings[0];
                OperationBindingCollection operationCollection = binding.Operations;

                foreach (OperationBinding operation in operationCollection)
                {
                    ListOfOperations.Add(operation.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ListOfOperations.ToArray();
        }

        //DONE
        //This method returns the parameters of web method from a webservice on success, null on failure.
        public ParameterInfo[] GetParameterInfoFromOperation(string webServiceURL,
                                                            string webServiceName,
                                                            string webMethodName)
        {
            ParameterInfo[] parameterInfo = null;

            CompilerResults compilerResults = GenerateProxyWebService(webServiceURL);
            Object webServiceClass = compilerResults.CompiledAssembly.CreateInstance(webServiceName);
            MethodInfo[] webMethodInfo = webServiceClass.GetType().GetMethods();

            foreach (MethodInfo methodInfo in webMethodInfo)
            {
                if (String.Equals(methodInfo.Name, webMethodName))
                {
                    parameterInfo = methodInfo.GetParameters();
                }
            }

            return parameterInfo;
        }

        //This method returns the returning parameter type of web method from a webservice on success, null on failure.
        public ParameterInfo ReturnParameterInfoFromOperation(string webServiceURL,
                                                            string webServiceName,
                                                            string webMethodName)
        {
            ParameterInfo parameterInfo = null;

            CompilerResults compilerResults = GenerateProxyWebService(webServiceURL);
            Object webServiceClass = compilerResults.CompiledAssembly.CreateInstance(webServiceName);
            MethodInfo[] webMethodInfo = webServiceClass.GetType().GetMethods();

            foreach (MethodInfo methodInfo in webMethodInfo)
            {
                if (String.Equals(methodInfo.Name, webMethodName))
                {
                    parameterInfo = methodInfo.ReturnParameter;
                }
            }

            return parameterInfo;
        }

        //DONE
        //This method invokes a webservices method and returns the resulting object on success, and null on failure.
        public object CallWebMethod(string webServiceURL,
                                    string webServiceName,
                                    string webMethodName,
                                    object[] arguments)
        {
            try
            {
                //Invoke the webservices method and return the result as an object
                CompilerResults compilerResults = GenerateProxyWebService(webServiceURL);
                Object webServiceClass = compilerResults.CompiledAssembly.CreateInstance(webServiceName);
                MethodInfo webMethodInfo = webServiceClass.GetType().GetMethod(webMethodName);

                object result = webMethodInfo.Invoke(webServiceClass, arguments);
                    
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
