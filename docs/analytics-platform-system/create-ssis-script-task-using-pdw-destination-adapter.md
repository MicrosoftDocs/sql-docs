---
title: Create a script task with SSIS PDW destination adapter | Microsoft Docs
desciption: This Integration Services (SSIS) package script code is a code example for using the SSIS PDW destination adaptor. The Integration Services Script Task allows you to perform virtually any operation that can be accomplished in a .Net application within the context of an SSIS control flow.  
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Create a script task that uses SSIS Parallel Data Warehouse destination adapter
This SSIS package script code is a code example for using the SSIS PDW destination adapter.  The Integration Services Script Task allows you to perform virtually any operation that can be accomplished in a .Net application within the context of an SSIS control flow. 
  
## Sample Code  
  
1.  Create a blank Script Task in SSIS.  
  
2.  Open the task in the **Script Task Editor**, and click **Edit Script**.  
  
3.  Edit the script to use the code provided below. Customized the script to your environment by making the following changes:  
  
    -   You must retain the namespace of the script created by the script task. Note the name of the namespace `namespace ST_<GUID>` and edit the script below, changing *ST_<GUID>* to the original *GUID* of the script task.  
  
    -   Replace four occurrences of the placeholder value *XXXXXXXX* with the values appropriate to your environment and intended action.  
  
    ```c#  
    #region Help:  Introduction to the script task  
    /* The Script Task allows you to perform virtually any operation that can be accomplished in  
     * a .Net application within the context of an Integration Services control flow.   
     *   
     * Expand the other regions which have "Help" prefixes for examples of specific ways to use  
     * Integration Services features within this script task. */  
    #endregion  
  
    #region Namespaces  
    using System;  
    using System.Data;  
    using Microsoft.SqlServer.Dts.Runtime;  
    using System.Windows.Forms;  
    using Microsoft.SqlServer.Management.IntegrationServices;  
    using Microsoft.SqlServer.Dts.Pipeline;  
    using Microsoft.SqlServer.Dts.Pipeline.Wrapper;  
    using System.Runtime.InteropServices;  
    using System.Reflection;  
    #endregion  
  
    namespace ST_<GUID>  
    {  
        /// <summary>  
        /// ScriptMain is the entry point class of the script.  Do not change the name, attributes,  
        /// or parent of this class.  
        /// </summary>  
    [Microsoft.SqlServer.Dts.Tasks.ScriptTask.SSISScriptTaskEntryPointAttribute]  
    public partial class ScriptMain : Microsoft.SqlServer.Dts.Tasks.ScriptTask.VSTARTScriptObjectModelBase  
    {  
            #region Help:  Using Integration Services variables and parameters in a script  
            /* To use a variable in this script, first ensure that the variable has been added to   
             * either the list contained in the ReadOnlyVariables property or the list contained in   
             * the ReadWriteVariables property of this script task, according to whether or not your  
             * code needs to write to the variable.  To add the variable, save this script, close this instance of  
             * Visual Studio, and update the ReadOnlyVariables and   
             * ReadWriteVariables properties in the Script Transformation Editor window.  
             * To use a parameter in this script, follow the same steps. Parameters are always read-only.  
             *   
             * Example of reading from a variable:  
             *  DateTime startTime = (DateTime) Dts.Variables["System::StartTime"].Value;  
             *   
             * Example of writing to a variable:  
             *  Dts.Variables["User::myStringVariable"].Value = "new value";  
             *   
             * Example of reading from a package parameter:  
             *  int batchId = (int) Dts.Variables["$Package::batchId"].Value;  
             *    
             * Example of reading from a project parameter:  
             *  int batchId = (int) Dts.Variables["$Project::batchId"].Value;  
             *   
             * Example of reading from a sensitive project parameter:  
             *  int batchId = (int) Dts.Variables["$Project::batchId"].GetSensitiveValue();  
             * */  
  
            #endregion  
  
            #region Help:  Firing Integration Services events from a script  
            /* This script task can fire events for logging purposes.  
             *   
             * Example of firing an error event:  
             *  Dts.Events.FireError(18, "Process Values", "Bad value", "", 0);  
             *   
             * Example of firing an information event:  
             *  Dts.Events.FireInformation(3, "Process Values", "Processing has started", "", 0, ref fireAgain)  
             *   
             * Example of firing a warning event:  
             *  Dts.Events.FireWarning(14, "Process Values", "No values received for input", "", 0);  
             * */  
            #endregion  
  
            #region Help:  Using Integration Services connection managers in a script  
            /* Some types of connection managers can be used in this script task.  See the topic   
             * "Working with Connection Managers Programatically" for details.  
             *   
             * Example of using an ADO.Net connection manager:  
             *  object rawConnection = Dts.Connections["Sales DB"].AcquireConnection(Dts.Transaction);  
             *  SqlConnection myADONETConnection = (SqlConnection)rawConnection;  
             *  //Use the connection in some code here, then release the connection  
             *  Dts.Connections["Sales DB"].ReleaseConnection(rawConnection);  
             *  
             * Example of using a File connection manager  
             *  object rawConnection = Dts.Connections["Prices.zip"].AcquireConnection(Dts.Transaction);  
             *  string filePath = (string)rawConnection;  
             *  //Use the connection in some code here, then release the connection  
             *  Dts.Connections["Prices.zip"].ReleaseConnection(rawConnection);  
             * */  
            #endregion  
  
            public static string GetSymbolicName(int errorCode)  
            {  
                string symbolicName = string.Empty;  
                HResults hresults = new HResults();  
  
                foreach (FieldInfo fieldInfo in hresults.GetType().GetFields())  
                {  
                    if ((int)fieldInfo.GetValue(hresults) == errorCode)  
                    {  
                        symbolicName = fieldInfo.Name;  
                        break;  
                    }  
                }  
  
                return symbolicName;  
            }  
         /// <summary>  
            /// This method is called when this script task executes in the control flow.  
            /// Before returning from this method, set the value of Dts.TaskResult to indicate success or failure.  
            /// To open Help, press F1.  
            /// </summary>  
            public void Main()  
            {  
                try  
                {  
                    Package package = new Package();  
                    Executable dataFlowtask = package.Executables.Add("STOCK:PipelineTask");  
  
                    TaskHost taskHost = dataFlowtask as TaskHost;  
                    taskHost.Name = "Data Flow Task";  
  
                    MainPipe pipeline = taskHost.InnerObject as MainPipe;  
  
                    //Connection to the Oracle Source created  
                    ConnectionManager connectionSource = package.Connections.Add("OLEDB");  
                    connectionSource.Name = "OracleSource";  
                    connectionSource.ConnectionString = "Data Source=127.0.0.1;User ID=sa;Password=XXXXXXXX;Provider=OraOLEDB.Oracle.1;Persist Security Info=True;";  
  
                    IDTSComponentMetaData100 srcComponent = pipeline.ComponentMetaDataCollection.New();  
                    srcComponent.ComponentClassID = "DTSAdapter.OleDbSource";  
                    srcComponent.ValidateExternalMetadata = true;  
                    IDTSDesigntimeComponent100 srcDesignTimeComponent = srcComponent.Instantiate();  
                    srcDesignTimeComponent.ProvideComponentProperties();  
                    srcComponent.Name = "OleDb Source";  
  
                    srcDesignTimeComponent.SetComponentProperty("AccessMode", 0);  
                    srcDesignTimeComponent.SetComponentProperty("OpenRowset", "XXXXXXXX");  
  
                    // Set the connection manager  
                    srcComponent.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(connectionSource);  
                    srcComponent.RuntimeConnectionCollection[0].ConnectionManagerID = connectionSource.ID;  
  
                    // Retrieve the column metadata  
                    srcDesignTimeComponent.AcquireConnections(null);  
                    srcDesignTimeComponent.ReinitializeMetaData();  
                    srcDesignTimeComponent.ReleaseConnections();  
  
                    ConnectionManager connectionDest = package.Connections.Add("SQLPDW");  
                    connectionDest.Name = "SQL Server Destination";  
                    connectionDest.ConnectionString = @"Data Source=127.0.0.1,17001;Initial Catalog=SomeDB;User ID=sa;Password=XXXXXXXX;";  
  
                    IDTSComponentMetaData100 destComponent = pipeline.ComponentMetaDataCollection.New();  
                    destComponent.ComponentClassID = typeof(Microsoft.SqlServer.Dts.Pipeline.SQLPDWDestinationAdapter).AssemblyQualifiedName;  
                    IDTSDesigntimeComponent100 destDesignTimeComponent = destComponent.Instantiate();  
                    destDesignTimeComponent.ProvideComponentProperties();  
  
                    destComponent.Name = "ADO Destination";  
                    destDesignTimeComponent.SetComponentProperty("Mode", "Append");  
                    destDesignTimeComponent.SetComponentProperty("FinalTable", "XXXXXXXX");  
  
                    // set connection  
                    destComponent.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(connectionDest);  
                    destComponent.RuntimeConnectionCollection[0].ConnectionManagerID = connectionDest.ID;  
  
                    destDesignTimeComponent.AcquireConnections(null);  
  
                    // get metadata  
                    destDesignTimeComponent.ReinitializeMetaData();  
                    destDesignTimeComponent.ReleaseConnections();  
  
                    //Connect Source and Destination  
                    IDTSPath100 path = pipeline.PathCollection.New();  
                    path.AttachPathAndPropagateNotifications(srcComponent.OutputCollection[0], destComponent.InputCollection[0]);  
  
                    // Configure the destination  
                    IDTSInput100 destInput = destComponent.InputCollection[0];  
                    IDTSVirtualInput100 destVirInput = destInput.GetVirtualInput();  
                    IDTSInputColumnCollection100 destInputCols = destInput.InputColumnCollection;  
                    IDTSExternalMetadataColumnCollection100 destExtCols = destInput.ExternalMetadataColumnCollection;  
                    IDTSOutputColumnCollection100 sourceColumns = srcComponent.OutputCollection[0].OutputColumnCollection;  
  
                    // The OLEDB destination requires you to hook up the external columns  
                    foreach (IDTSOutputColumn100 outputCol in sourceColumns)  
                    {  
                        // Get the external column id  
                        IDTSExternalMetadataColumn100 extCol = (IDTSExternalMetadataColumn100)destExtCols[outputCol.Name];  
                        if (extCol != null)  
                        {  
                            // Create an input column from an output col of previous component.  
                            destVirInput.SetUsageType(outputCol.ID, DTSUsageType.UT_READONLY);  
                            IDTSInputColumn100 inputCol = destInputCols.GetInputColumnByLineageID(outputCol.ID);  
                            if (inputCol != null)  
                            {  
                                // map the input column with an external metadata column  
                                destDesignTimeComponent.MapInputColumn(destInput.ID, inputCol.ID, extCol.ID);  
                            }  
                        }  
                    }  
  
                    string pkg = @"D:\Test\GenPackage.dtsx";  
                    Microsoft.SqlServer.Dts.Runtime.Application app = new Microsoft.SqlServer.Dts.Runtime.Application();  
                    app.SaveToXml(pkg, package, null);  
  
                    // Now execute the package after creating it.  
                    package.Execute();  
                }  
                catch (COMException ex)  
                {  
                    string symbolicName = GetSymbolicName(ex.ErrorCode);  
                    Console.WriteLine("Symbolic Name: {0}", symbolicName);  
                    string zz = symbolicName;  
                }  
  
                Dts.TaskResult = (int)ScriptResults.Success;  
            }  
  
            #region ScriptResults declaration  
            /// <summary>  
            /// This enum provides a convenient shorthand within the scope of this class for setting the  
            /// result of the script.  
            ///   
            /// This code was generated automatically.  
            /// </summary>  
            enum ScriptResults  
            {  
                Success = Microsoft.SqlServer.Dts.Runtime.DTSExecResult.Success,  
                Failure = Microsoft.SqlServer.Dts.Runtime.DTSExecResult.Failure  
            };  
            #endregion  
  
         }  
    }  
    ```  
  
## See Also  
[Load Data With Integration Services](load-with-ssis.md)  

<!-- MISSING LINK
[Install Integration Services Destination Adapters](install-integration-services-destination-adapters-sql-server-pdw.md)
-->
  
