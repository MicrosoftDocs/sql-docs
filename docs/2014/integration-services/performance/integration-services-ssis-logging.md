---
title: "Integration Services (SSIS) Logging | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "containers [Integration Services], logs"
  - "Windows Event log provider [Integration Services]"
  - "XML File log provider [Integration Services]"
  - "SQL Server Profiler, log provider"
  - "SQL Server Integration Services packages, logs"
  - "SSIS packages, logs"
  - "tasks [Integration Services], logs"
  - "logs [Integration Services], log providers"
  - "Integration Services packages, logs"
  - "log providers [Integration Services]"
  - "packages [Integration Services], logs"
  - "Text File log provider"
  - "SQL Server log provider"
ms.assetid: 65e17889-371f-4951-9a7e-9932b2d0dcde
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Integration Services (SSIS) Logging
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes log providers that you can use to implement logging in packages, containers, and tasks. With logging, you can capture run-time information about a package, helping you audit and troubleshoot a package every time it is run. For example, a log can capture the name of the operator who ran the package and the time the package began and finished.  
  
 You can configure the scope of logging that occurs during a package execution on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. For more information, see [Enable Logging for Package Execution on the SSIS Server](../enable-logging-for-package-execution-on-the-ssis-server.md).  
  
 You can also include logging when you run a package using the **dtexec** command prompt utility. For more information about the command prompt arguments that support logging, see [dtexec Utility](../packages/dtexec-utility.md).  
  
## Configure Logging in SQL Server Data Tools  
 Logs are associated with packages and are configured at the package level. Each task or container in a package can log information to any package log. The tasks and containers in a package can be enabled for logging even if the package itself is not. For example, you can enable logging on an Execute SQL task without enabling logging on the parent package. A package, container, or task can write to multiple logs. You can enable logging on the package only, or you can choose to enable logging on any individual task or container that the package includes.  
  
 When you add the log to a package, you choose the log provider and the location of the log. The log provider specifies the format for the log data: for example, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database or text file.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes the following log providers:  
  
-   The Text File log provider, which writes log entries to ASCII text files in a comma-separated value (CSV) format. The default file name extension for this provider is .log.  
  
-   The [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] log provider, which writes traces that you can view using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Profiler. The default file name extension for this provider is .trc.  
  
    > [!NOTE]  
    >  You cannot use the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] log provider in a package that is running in 64-bit mode.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log provider, which writes log entries to the `sysssislog` table in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
-   The Windows Event log provider, which writes entries to the Application log in the Windows Event log on the local computer.  
  
-   The XML File log provider, which writes log files to an XML file. The default file name extension for this provider is .xml.  
  
 If you add a log provider to a package or configure logging programmatically, you can use either a ProgID or ClassID to identify the log provider, instead of using the names that [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer displays in the **Configure SSIS Logs** dialog box.  
  
 The following table lists the ProgID and ClassID for the log providers that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes, and the location of the logs to which log providers write.  
  
|Log provider|ProgID|ClassID|Location|  
|------------------|------------|-------------|--------------|  
|Text file|DTS.LogProviderTextFile|{0A039101-ACC1-4E06-943F-279948323883}|The File connection manager that the log provider uses specifies the path of the text file.|  
|SQL Server Profiler|DTS.LogProviderSQLProfiler|{E93F6300-AE0C-4916-A7BF-A8D0CE12C77A}|The File connection manager that the log provider uses specifies the path of the file used by [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|  
|SQL Server|DTS.LogProviderSQLServer|{94150B25-6AEB-4C0D-996D-D37D1C4FDEDA}|The OLE DB connection manager that the log provider uses specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the sysssislog table with the log entries.|  
|Windows Event Log|DTS.LogProviderEventLog|{071CC8EB-C343-4CFF-8D58-564B92FCA3CF}|The Application log in Windows Event Viewer contains the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] log information.|  
|XML File|DTS.LogProviderXMLFile|{440945A4-2A22-4F19-B577-EAF5FDDC5F7A}|The File connection manager that the log provider uses specifies the path of the XML file.|  
  
 You can also create custom log providers. For more information, see [Creating a Custom Log Provider](../extending-packages-custom-objects/log-provider/creating-a-custom-log-provider.md).  
  
 The log providers in a package are members of the log providers collection of the package. When you create a package and implement logging by using [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, you can see a list of the collection members in the **Log Provider** folders on the **Package Explorer** tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 You configure a log provider by providing a name and description for the log provider and specifying the connection manager that the log provider uses. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log provider uses an OLE DB connection manager. The Text File, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], and XML File log providers all use File connection managers. The Windows Event log provider does not use a connection manager, because it writes directly to the Windows Event log. For more information, see [OLE DB Connection Manager](../connection-manager/ole-db-connection-manager.md) and [File Connection Manager](../connection-manager/file-connection-manager.md).  
  
### Logging Customization  
 To customize the logging of an event or custom message, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides a schema of commonly logged information to include in log entries. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] log schema defines the information that you can log. You can select elements from the log schema for each log entry.  
  
 A package and its containers and tasks do not have to log the same information, and tasks within the same package or container can log different information. For example, a package can log operator information when the package starts, one task can log the source of the task's failure, and another task can log information when errors occur. If a package and its containers and tasks use multiple logs, the same information is written to all the logs.  
  
 You can select a level of logging that suits your needs by specifying the events to log and the information to log for each event. You may find that some events provide more useful information than others. For example, you might want to log only the computer and operator names for the **PreExecute** event but all available information for the **Error** event.  
  
 To prevent log files from using large amounts of disk space, or to avoid excessive logging, which could degrade performance, you can limit logging by selecting specific events and information items to log. For example, you can configure a log to capture only the date and the computer name for each error.  
  
 In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, you define the logging options by using the **Configure SSIS Logs** dialog box.  
  
#### Log Schema  
 The following table describes the elements in the log schema.  
  
|Element|Description|  
|-------------|-----------------|  
|Computer|The name of the computer on which the log event occurred.|  
|Operator|The identity of the user who launched the package.|  
|SourceName|The name of the container or task in which the log event occurred.|  
|SourceID|The unique identifier of the package; the For Loop, Foreach Loop, or Sequence container; or the task in which the log event occurred.|  
|ExecutionID|The GUID of the package execution instance.<br /><br /> Note: Running a single package might create log entries with different values for the ExecutionID element. For example, when you run a package in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], the validation phase might create log entries with an ExecutionID element that corresponds to [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. However, the execution phase might create log entries with an ExecutionID element that corresponds to dtshost.exe. For another example, when you run a package that contains Execute Package tasks, each of these tasks runs a child package. These child packages might create log entries that have a different ExecutionID element than the log entries that the parent package creates.|  
|MessageText|A message associated with the log entry.|  
|DataBytes|A byte array specific to the log entry. The meaning of this field varies by log entry.|  
  
 The following table describes three additional elements in the log schema that are not available on the **Details** tab of the **Configure SSIS Logs** dialog box.  
  
|Element|Description|  
|-------------|-----------------|  
|StartTime|The time at which the container or task starts to run.|  
|EndTime|The time at which the container or task stops running.|  
|DataCode|An optional integer value that typically contains a value from the <xref:Microsoft.SqlServer.Dts.Runtime.DTSExecResult> enumeration that indicates the result of running the container or task:<br /><br /> 0 - Success<br /><br /> 1 - Failure<br /><br /> 2 - Completed<br /><br /> 3 - Canceled|  
  
##### Log Entries  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] supports log entries on predefined events and provides custom log entries for many [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] objects. The **Configure SSIS Logs** dialog box in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer lists these events and custom log entries.  
  
 The following table describes the predefined events that can be enabled to write log entries when run-time events occur. These log entries apply to executables, the package, and the tasks and containers that the package includes. The name of the log entry is the same as the name of the run-time event that was raised and caused the log entry to be written.  
  
|Events|Description|  
|------------|-----------------|  
|**OnError**|Writes a log entry when an error occurs.|  
|**OnExecStatusChanged**|Writes a log entry when a task (not a container) is suspended or resumed during debugging.|  
|**OnInformation**|Writes a log entry during the validation and execution of an executable to report information.|  
|**OnPostExecute**|Writes a log entry immediately after the executable has finished running.|  
|**OnPostValidate**|Writes a log entry when the validation of the executable finishes.|  
|**OnPreExecute**|Writes a log entry immediately before the executable runs.|  
|**OnPreValidate**|Writes a log entry when the validation of the executable starts.|  
|**OnProgress**|Writes a log entry when measurable progress is made by the executable.|  
|**OnQueryCancel**|Writes a log entry at any juncture in the task processing where it is feasible to cancel execution.|  
|**OnTaskFailed**|Writes a log entry when a task fails.|  
|**OnVariableValueChanged**|Writes a log entry when the value of a variable changes.|  
|**OnWarning**|Writes a log entry when a warning occurs.|  
|**PipelineComponentTime**|For each data flow component, writes a log entry for each phase of validation and execution. The log entry specifies the processing time for each phase.|  
|**Diagnostic**|Writes a log entry that provides diagnostic information.<br /><br /> For example, you can log a message before and after every call to an external data provider. For more information, see [Troubleshooting Tools for Package Execution](../troubleshooting/troubleshooting-tools-for-package-execution.md).|  
  
 The package and many tasks have custom log entries that can be enabled for logging. For example, the Send Mail task provides the **SendMailTaskBegin** custom log entry, which logs information when the Send Mail task starts to run, but before the task sends an e-mail message. For more information, see [Custom Messages for Logging](../custom-messages-for-logging.md).  
  
### Differentiating Package Copies  
 Log data includes the name and the GUID of the package to which the log entries belong. If you create a new package by copying an existing package, the name and the GUID of the existing package are also copied. As a result, you may have two packages that have the same GUID and name, making it difficult to differentiate between the packages in the log data.  
  
 To eliminate this ambiguity, you should update the name and the GUID of the new packages. In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you can regenerate the GUID in the `ID` property and update the value of the `Name` property in the Properties window. You can also change the GUID and the name programmatically, or by using the **dtutil** command prompt. For more information, see [Set Package Properties](../set-package-properties.md) and [dtutil Utility](../dtutil-utility.md).  
  
### Parent Logging Options  
 Frequently, the logging options of tasks and For Loop, Foreach Loop, and Sequence containers match those of the package or a parent container. In that case, you can configure them to inherit their logging options from their parent container. For example, in a For Loop container that includes an Execute SQL task, the Execute SQL task can use the logging options that are set on the For Loop container. To use the parent logging options, you set the LoggingMode property of the container to **UseParentSetting**. You can set this property in the **Properties** window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or through the **Configure SSIS Logs** dialog box in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
### Logging Templates  
 In the **Configure SSIS Logs** dialog box, you can also create and save frequently used logging configurations as templates, and then use the templates in multiple packages. This makes it easy to apply a consistent logging strategy across multiple packages and to modify log settings on packages by updating and then applying the templates. The templates are stored in XML files.  
  
 **To configure logging using the Configure SSIS Logs dialog box**  
  
1.  Enable the package and its tasks for logging. Logging can occur at the package, the container, and the task level. You can specify different logs for packages, containers, and tasks.  
  
2.  Select a log provider and add a log for the package. Logs can be created only at the package level, and a task or container must use one of the logs created for the package. Each log is associated with one of the following log providers: Text file, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Windows Event Log, or XML file. For more information, see [Enable Package Logging in SQL Server Data Tools](../enable-package-logging-in-sql-server-data-tools.md).  
  
3.  Select the events and the log schema information about each event you want to capture in the log. For more information, see [Configure Logging by Using a Saved Configuration File](../configure-logging-by-using-a-saved-configuration-file.md).  
  
### Configuration of Log Provider  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 A log provider is created and configured as a step in implementing logging in a package. For more information, see [Integration Services Logging](integration-services-ssis-logging.md).  
  
 After you create a log provider, you can view and modify its properties in the Properties window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 For information about programmatically setting these properties, see the documentation for the <xref:Microsoft.SqlServer.Dts.Runtime.LogProvider> class.  
  
### Logging for Data Flow Tasks  
 The Data Flow task provides many custom log entries that can be used to monitor and adjust performance. For example, you can monitor components that might cause memory leaks, or keep track of how long it takes to run a particular component. For a list of these custom log entries and sample logging output, see [Data Flow Task](../control-flow/data-flow-task.md).  
  
#### Use the PipelineComponentTime Event  
 Perhaps the most useful custom log entry is the PipelineComponentTime event. This log entry reports the number of milliseconds that each component in the data flow spends on each of the five major processing steps. The following table describes these processing steps. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] developers will recognize these steps as the principal methods of a <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent>.  
  
|Step|Description|  
|----------|-----------------|  
|Validate|The component checks for valid property values and configuration settings.|  
|PreExecute|The component performs one-time processing before it starts to process rows of data.|  
|PostExecute|The component performs one-time processing after it has processed all rows of data.|  
|ProcessInput|The transformation or destination component processes the incoming rows of data that an upstream source or transformation has passed to it.|  
|PrimeOutput|The source or transformation component fills the buffers of data to be passed to a downstream transformation or destination component.|  
  
 When you enable the PipelineComponentTime event, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] logs one message for each processing step performed by each component. The following log entries show a subset of the messages that the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] CalculatedColumns package sample logs:  
  
 `The component "Calculate LineItemTotalCost" (3522) spent 356 milliseconds in ProcessInput.`  
  
 `The component "Sum Quantity and LineItemTotalCost" (3619) spent 79 milliseconds in ProcessInput.`  
  
 `The component "Calculate Average Cost" (3662) spent 16 milliseconds in ProcessInput.`  
  
 `The component "Sort by ProductID" (3717) spent 125 milliseconds in ProcessInput.`  
  
 `The component "Load Data" (3773) spent 0 milliseconds in ProcessInput.`  
  
 `The component "Extract Data" (3869) spent 688 milliseconds in PrimeOutput filling buffers on output "OLE DB Source Output" (3879).`  
  
 `The component "Sum Quantity and LineItemTotalCost" (3619) spent 141 milliseconds in PrimeOutput filling buffers on output "Aggregate Output 1" (3621).`  
  
 `The component "Sort by ProductID" (3717) spent 16 milliseconds in PrimeOutput filling buffers on output "Sort Output" (3719).`  
  
 These log entries show that the data flow task spent the most time on the following steps, shown here in descending order:  
  
-   The OLE DB source that is named "Extract Data" spent 688 ms. loading data.  
  
-   The Derived Column transformation that is named "Calculate LineItemTotalCost" spent 356 ms. performing calculations on incoming rows.  
  
-   The Aggregate transformation that is named "Sum Quantity and LineItemTotalCost" spent a combined 220 ms-141 in PrimeOutput and 79 in ProcessInput-performing calculations and passing the data to the next transformation.  
  
## Related Tasks  
 The following list contains links to topics that show how to perform tasks related to the logging feature.  
  
-   [Configure SSIS Logs Dialog Box](../configure-ssis-logs-dialog-box.md)  
  
-   [Enable Package Logging in SQL Server Data Tools](../enable-package-logging-in-sql-server-data-tools.md)  
  
-   [Enable Logging for Package Execution on the SSIS Server](../enable-logging-for-package-execution-on-the-ssis-server.md)  
  
-   [View Log Entries in the Log Events Window](../view-log-entries-in-the-log-events-window.md)  
  
## Related Content  
 [DTLoggedExec Tool for Full and Detail Logging (CodePlex Project)](https://go.microsoft.com/fwlink/?LinkId=150579)  
  
## See Also  
 [View Log Entries in the Log Events Window](../view-log-entries-in-the-log-events-window.md)  
  
  
