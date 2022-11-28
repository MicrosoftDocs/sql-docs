---
description: "Integration Services (SSIS) Logging"
title: "Integration Services (SSIS) Logging | Microsoft Docs"
ms.custom: supportability
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.configuredtslogs.containers.f1"
  - "sql13.dts.designer.configuredtslogs.loggingdetails.f1"
  - "sql13.dts.designer.configuredtslogs.providersandlogs.f1"
  - "SQL13.SSIS.SSMS.ISMANAGECUSTOMIZEDLOGGINGLEVEL.F1"
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
author: chugugrace
ms.author: chugu
---
# Integration Services (SSIS) Logging

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes log providers that you can use to implement logging in packages, containers, and tasks. With logging, you can capture run-time information about a package, helping you audit and troubleshoot a package every time it is run. For example, a log can capture the name of the operator who ran the package and the time the package began and finished.  
  
 You can configure the scope of logging that occurs during a package execution on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. For more information, see [Enable Logging for Package Execution on the SSIS Server](#server_logging).  
  
 You can also include logging when you run a package using the **dtexec** command prompt utility. For more information about the command prompt arguments that support logging, see [dtexec Utility](../../integration-services/packages/dtexec-utility.md).  
  
## Configure Logging in SQL Server Data Tools  
 Logs are associated with packages and are configured at the package level. Each task or container in a package can log information to any package log. The tasks and containers in a package can be enabled for logging even if the package itself is not. For example, you can enable logging on an Execute SQL task without enabling logging on the parent package. A package, container, or task can write to multiple logs. You can enable logging on the package only, or you can choose to enable logging on any individual task or container that the package includes.  
  
 When you add the log to a package, you choose the log provider and the location of the log. The log provider specifies the format for the log data: for example, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database or text file.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes the following log providers:  
  
-   The Text File log provider, which writes log entries to ASCII text files in a comma-separated value (CSV) format. The default file name extension for this provider is .log.  
  
-   The [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] log provider, which writes traces that you can view using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Profiler. The default file name extension for this provider is .trc.  
  
    > [!NOTE]  
    >  You cannot use the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] log provider in a package that is running in 64-bit mode.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log provider, which writes log entries to the **sysssislog** table in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
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
  
 You can also create custom log providers. For more information, see [Creating a Custom Log Provider](../../integration-services/extending-packages-custom-objects/log-provider/creating-a-custom-log-provider.md).  
  
 The log providers in a package are members of the log providers collection of the package. When you create a package and implement logging by using [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, you can see a list of the collection members in the **Log Provider** folders on the **Package Explorer** tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 You configure a log provider by providing a name and description for the log provider and specifying the connection manager that the log provider uses. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log provider uses an OLE DB connection manager. The Text File, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], and XML File log providers all use File connection managers. The Windows Event log provider does not use a connection manager, because it writes directly to the Windows Event log. For more information, see [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md) and [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md).  
  
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
  
#### Log Entries  
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
|**Diagnostic**<br /><br /> **DiagnosticEx**|Writes a log entry that provides diagnostic information.<br /><br /> For example, you can log a message before and after every call to an external data provider. For more information, see [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md).<br /><br /> Log the **DiagnosticEx** event when you want to find the column names for  columns in the data flow that have errors. This event writes a data flow lineage map to the log. You can then look up the column name in this lineage map by using the column identifier captured by an error output. For more info, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).<br /><br /> Note that the **DiagnosticEx** event does not preserve whitespace in its XML output to reduce the size of the log. To improve readability, copy the log into an XML editor - in Visual Studio, for example - that supports XML formatting and syntax highlighting.<br /><br /> Note: If you log the **DiagnosticEx** event with the SQL Server log provider, the output may be truncated. The **message** field of the SQL Server log provider is of type nvarchar(2048). To avoid truncation, use a different log provider when you log the **DiagnosticEx** event.|  
  
 The package and many tasks have custom log entries that can be enabled for logging. For example, the Send Mail task provides the **SendMailTaskBegin** custom log entry, which logs information when the Send Mail task starts to run, but before the task sends an e-mail message. For more information, see [Custom Messages for Logging](#custom_messages).  
  
### Differentiating Package Copies  
 Log data includes the name and the GUID of the package to which the log entries belong. If you create a new package by copying an existing package, the name and the GUID of the existing package are also copied. As a result, you may have two packages that have the same GUID and name, making it difficult to differentiate between the packages in the log data.  
  
 To eliminate this ambiguity, you should update the name and the GUID of the new packages. In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you can regenerate the GUID in the **ID** property and update the value of the **Name** property in the Properties window. You can also change the GUID and the name programmatically, or by using the **dtutil** command prompt. For more information, see [Set Package Properties](../../integration-services/set-package-properties.md) and [dtutil Utility](../../integration-services/dtutil-utility.md).  
  
### Parent Logging Options  
 Frequently, the logging options of tasks and For Loop, Foreach Loop, and Sequence containers match those of the package or a parent container. In that case, you can configure them to inherit their logging options from their parent container. For example, in a For Loop container that includes an Execute SQL task, the Execute SQL task can use the logging options that are set on the For Loop container. To use the parent logging options, you set the LoggingMode property of the container to **UseParentSetting**. You can set this property in the **Properties** window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or through the **Configure SSIS Logs** dialog box in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
### Logging Templates  
 In the **Configure SSIS Logs** dialog box, you can also create and save frequently used logging configurations as templates, and then use the templates in multiple packages. This makes it easy to apply a consistent logging strategy across multiple packages and to modify log settings on packages by updating and then applying the templates. The templates are stored in XML files.  
  
 **To configure logging using the Configure SSIS Logs dialog box**  
  
1.  Enable the package and its tasks for logging. Logging can occur at the package, the container, and the task level. You can specify different logs for packages, containers, and tasks.  
  
2.  Select a log provider and add a log for the package. Logs can be created only at the package level, and a task or container must use one of the logs created for the package. Each log is associated with one of the following log providers: Text file, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Windows Event Log, or XML file. For more information, see [Enable Package Logging in SQL Server Data Tools](#ssdt).  
  
3.  Select the events and the log schema information about each event you want to capture in the log. For more information, see [Configure Logging by Using a Saved Configuration File](#saved_config).  
  
### Configuration of Log Provider  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 A log provider is created and configured as a step in implementing logging in a package.  
  
 After you create a log provider, you can view and modify its properties in the Properties window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 For information about programmatically setting these properties, see the documentation for the <xref:Microsoft.SqlServer.Dts.Runtime.LogProvider> class.  
  
### Logging for Data Flow Tasks  
 The Data Flow task provides many custom log entries that can be used to monitor and adjust performance. For example, you can monitor components that might cause memory leaks, or keep track of how long it takes to run a particular component. For a list of these custom log entries and sample logging output, see [Data Flow Task](../../integration-services/control-flow/data-flow-task.md).  
  
#### Capture the names of columns in which errors occur  
 When you configure an error output in the data flow, by default the error output provides only the numeric identifier of the column in which the error occurred. For more info, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).  
  
 You can find column names by enabling logging and selecting the **DiagnosticEx** event. This event writes a data flow lineage map to the log. You can then look up the column name from its identifier in this lineage map. Note that the **DiagnosticEx** event does not preserve whitespace in its XML output to reduce the size of the log. To improve readability, copy the log into an XML editor - in Visual Studio, for example - that supports XML formatting and syntax highlighting.  
  
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

## <a name="ssdt"></a> Enable Package Logging in SQL Server Data Tools
  This procedure describes how to add logs to a package, configure package-level logging, and save the logging configuration to an XML file. You can add logs only at the package level, but the package does not have to perform logging to enable logging in the containers that the package includes.  
  
> [!IMPORTANT]  
>  If you deploy the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, the logging level that you set for the package execution overrides the package logging that you configure using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 By default, the containers in the package use the same logging configuration as their parent container. For information about setting logging options for individual containers, see [Configure Logging by Using a Saved Configuration File](#saved_config).  
  
### To enable logging in a package  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  On the **SSIS** menu, click **Logging**.  
  
3.  Select a log provider in the **Provider type** list, and then click **Add**.  
  
4.  In the **Configuration** column, select a connection manager or click **\<New connection>** to create a new connection manager of the appropriate type for the log provider. Depending on the selected provider, use one of the following connection managers:  
  
    -   For Text files, use a File connection manager. For more information, see [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md)  
  
    -   For [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], use a File connection manager.  
  
    -   For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use an OLE DB connection manager. For more information, see [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
    -   For Windows Event Log, do nothing. [!INCLUDE[ssIS](../../includes/ssis-md.md)] automatically creates the log.  
  
    -   For XML files, use a File connection manager.  
  
5.  Repeat steps 3 and 4 for each log to use in the package.  
  
    > [!NOTE]  
    >  A package can use more than one log of each type.  
  
6.  Optionally, select the package-level check box, select the logs to use for package-level logging, and then click the **Details** tab.  
  
7.  On the **Details** tab, select **Events** to log all log entries, or clear **Events** to select individual events.  
  
8.  Optionally, click **Advanced** to specify which information to log.  
  
    > [!NOTE]  
    >  By default, all information is logged.  
  
9. On the **Details** tab, click **Save.** The **Save As** dialog box appears. Locate the folder in which to save the logging configuration, type a file name for the new log configuration, and then click **Save**.  
  
10. Click **OK**.  
  
11. To save the updated package, click **Save Selected Items** on the **File** menu.  

## <a name="configure_logs"></a> Configure SSIS Logs Dialog Box
  Use the **Configure SSIS Logs** dialog box to define logging options for a package.  
  
 **What do you want to do?**  
  
1.  [Open the Configure SSIS Logs Dialog Box](#open_dialog)  
  
2.  [Configure the Options in the Containers Pane](#container)  
  
3.  [Configure the Options on the Providers and Logs Tab](#provider)  
  
4.  [Configure the Options on the Details Tab](#detail)  
  
###  <a name="open_dialog"></a> Open the Configure SSIS Logs Dialog Box  
 **To open the Configure SSIS Logs dialog box**  
  
-   In the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click **Logging** on the **SSIS** menu.  
  
###  <a name="container"></a> Configure the Options in the Containers Pane  
 Use the **Containers** pane of the **Configure SSIS Logs** dialog box to enable the package and its containers for logging.  
  
#### Options  
 **Containers**  
 Select the check boxes in the hierarchical view to enable the package and its containers for logging:  
  
-   If cleared, the container is not enabled for logging. Select to enable logging.  
  
-   If dimmed, the container uses the logging options of its parent. This option is not available for the package.  
  
-   If checked, the container defines its own logging options.  
  
 If a container is dimmed and you want to set logging options on the container, click its check box twice. The first click clears the check box, and the second click selects it, enabling you to choose the log providers to use and select the information to log.  
  
###  <a name="provider"></a> Configure the Options on the Providers and Logs Tab  
 Use the **Providers and Logs** tab of the **Configure SSIS Logs** dialog box to create and configure logs for capturing run-time events.  
  
#### Options  
 **Provider type**  
 Select a type of log provider from the list.  
  
 **Add**  
 Add a log of the specified type to the collection of log providers of the package.  
  
 **Name**  
 Enable or disable logs for containers or tasks selected in the **Containers** pane of the **Configure SSIS Logs** dialog box, by using the check boxes. The name field is editable. Use the default name for the provider, or type a unique descriptive name.  
  
 **Description**  
 The description field is editable. Click and then modify the default description of the log.  
  
 **Configuration**  
 Select an existing connection manager in the list, or click \<**New connection...**> to create a new connection manager. Depending on the type of log provider, you can configure an OLE DB connection manager or a File connection manager. The log provider for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Event Log requires no connection.  
  
 Related Topics: [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md) manager, [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md)  
  
 **Delete**  
 Select a log provider and then click **Delete**.  
  
###  <a name="detail"></a> Configure the Options on the Details Tab  
 Use the **Details** tab of the **Configure SSIS Logs** dialog box to specify the events to enable for logging and the information details to log. The information that you select applies to all the log providers in the package. For example, you cannot write some information to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and different information to a text file.  
  
#### Options  
 **Events**  
 Enable or disable events for logging.  
  
 **Description**  
 View a description of the event.  
  
 **Advanced**  
 Select or clear events to log, and select or clear information to log for each event. Click **Basic** to hide all logging details, except the list of events. The following information is available for logging:  
  
|Value|Description|  
|-----------|-----------------|  
|**Computer**|The name of the computer on which the logged event occurred.|  
|**Operator**|The user name of the person who started the package.|  
|**SourceName**|The name of the package, container, or task in which the logged event occurred.|  
|**SourceID**|The global unique identifier (GUID) of the package, container, or task in which the logged event occurred.|  
|**ExecutionID**|The global unique identifier of the package execution instance.|  
|**MessageText**|A message associated with the log entry.|  
|**DataBytes**|Reserved for future use.|  
  
 **Basic**  
 Select or clear events to log. This option hides logging details except the list of events. If you select an event, all logging details are selected for the event by default. Click **Advanced** to show all logging details.  
  
 **Load**  
 Specify an existing XML file to use as a template for setting logging options.  
  
 **Save**  
 Save configuration details as a template to an XML file.  

## <a name="saved_config"></a> Configure Logging by Using a Saved Configuration File
  This procedure describes how to configure logging for new containers in a package by loading a previously saved logging configuration file.  
  
 By default, all containers in a package use the same logging configuration as their parent container. For example, the tasks in a Foreach Loop use the same logging configuration as the Foreach Loop.  
  
### To configure logging for a container  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  On the **SSIS** menu, click **Logging**.  
  
3.  Expand the package tree view and select the container to configure.  
  
4.  On the **Providers and Logs** tab, select the logs to use for the container.  
  
    > [!NOTE]  
    >  You can create logs only at the package level. For more information, see [Enable Package Logging in SQL Server Data Tools](#ssdt).  
  
5.  Click the **Details** tab and click **Load**.  
  
6.  Locate the logging configuration file you want to use and click **Open**.  
  
7.  Optionally, select a different log entry to log by selecting its check box in the **Events** column. Click **Advanced** to select the type of information to log for this entry.  
  
    > [!NOTE]  
    >  The new container may include additional log entries that are not available for the container originally used to create the logging configuration. These additional log entries must be selected manually if you want them to be logged.  
  
8.  To save the updated version of the logging configuration, click **Save**.  
  
9. To save the updated package, click **Save Selected Items** on the **File** menu.  

## <a name="server_logging"></a> Enable Logging for Package Execution on the SSIS Server

This topic describes how to set or change the logging level for a package when you run a package that you have deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. The logging level you set when you run the package overrides the package logging you configure at design time in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. See [Enable Package Logging in SQL Server Data Tools](#ssdt) for more information.  

### To review and adjust a server's default logging level property

1. In the SQL Server instance, go to the package in Object Explorer.

2. Select **Integration Services Catalog**.

3. Right-click **SSISDB** and select **Properties**.

4. In **Catalog Properties**, look for the **Operations Log** group box and the **Service-Wide Default Logging Level** entry.

You can pick from one of the built-in logging levels described in this topic, or you can pick an existing customized logging level. The selected logging level applies by default to all packages deployed to the SSIS Catalog. It also applies by default to a SQL Agent job step that runs an SSIS package.  
 
You can also specify the logging level for an individual package by using one of the following methods. This topic covers the first method.  
  
-   Configuring an instance of a package execution by using the Execute Package dialog box  
  
-   Setting parameters for an instance of an execution by using the [catalog.set_execution_parameter_value \(SSISDB Database\)](../../integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database.md)  
  
-   Configuring a SQL Server Agent job for a package execution by using the New Job Step dialog box.  
  
### Set the logging level for a package by using the Execute Package dialog box  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], navigate to the package in Object Explorer.  
  
2.  Right-click the package and select **Execute**.  
  
3.  Select the **Advanced** tab in the **Execute Package** dialog box.  
  
4.  Under **Logging level**, select the logging level. This topic contains a description of available values.  
  
5.  Complete any other package configurations, then click **OK** to run the package.  
  
### Select a logging level  
 The following built-in logging levels are available. You can also select an existing customized logging level. This topic contains a description of customized logging levels.  
  
|Logging Level|Description|  
|-------------------|-----------------|  
|None|Logging is turned off. Only the package execution status is logged.|  
|Basic|All events are logged, except custom and diagnostic events. This is the default value.|  
|RuntimeLineage|Collects the data required to track lineage information in the data flow. You can parse this lineage information to map the lineage relationship between tasks. ISVs and developers  can build custom lineage mapping tools with this information.|  
|Performance|Only performance statistics, and OnError and OnWarning events, are logged.<br /><br /> The **Execution Performance** report displays Active Time and Total Time for package data flow components. This information is available when the logging level of the last package execution was set to **Performance** or **Verbose**. For more information, see [Reports for the Integration Services Server](../../integration-services/performance/monitor-running-packages-and-other-operations.md#reports).<br /><br /> The [catalog.execution_component_phases](../../integration-services/system-views/catalog-execution-component-phases.md) view displays the start and end times for the data flow components, for each phase of an execution. This view displays this information for these components only when the logging level of the package execution is set to **Performance** or **Verbose**.|  
|Verbose|All events are logged, including custom and diagnostic events.<br /><br /> Custom events include those events that are logged by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] tasks. For more information about custom events, see [Custom Messages for Logging](#custom_messages).<br /><br /> An example of a diagnostic event is the **DiagnosticEx** event. Whenever an Execute Package task executes a child package, this event captures the parameter values passed to child packages.<br /><br /> The **DiagnosticEx** event also helps you to get the names of columns in which row-level errors occur. This event writes a data flow lineage map to the log. You can then look up the column name in this lineage map by using the column identifier captured by an error output.  For more info, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).<br /><br /> The value of the message column for **DiagnosticEx** is XML text. To view the message text for a package execution, query the [catalog.operation_messages \(SSISDB Database\)](../../integration-services/system-views/catalog-operation-messages-ssisdb-database.md) view. Note that the **DiagnosticEx** event does not preserve whitespace in its XML output to reduce the size of the log. To improve readability, copy the log into an XML editor - in Visual Studio, for example - that supports XML formatting and syntax highlighting.<br /><br /> The [catalog.execution_data_statistics](../../integration-services/system-views/catalog-execution-data-statistics.md) view displays a row each time a data flow component sends data to a downstream component, for a package execution. The logging level must be set to **Verbose** to capture this information in the view.|  
  
### Create and manage customized logging levels by using the Customized Logging Level Management dialog box  
 You can create customized logging levels that collect only the statistics and events that you want. Optionally you can also capture the context of events, which includes variable values, connection strings, and component properties. When you run a package, you can select a customized logging level wherever you can select a built-in logging level.  
  
> [!TIP]  
>  To capture the values of package variables, the **IncludeInDebugDump** property of the variables must be set to **True**.  
  
1.  To create and manage customized logging levels, in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], right-click on the SSISDB database and select **Customized Logging Level** to open the **Customized Logging Level Management** dialog box. The **Customized Logging Levels** list contains all the existing customized logging levels.  
  
2.  To **create** a new customized logging level, click **Create**, and then provide a name and description. On the **Statistics** and **Events** tabs, select the statistics and events that you want to collect. On the **Events** tab, optionally select **Include Context** for individual events. Then click **Save**.  
  
3.  To **update** an existing customized logging level, select it in the list, reconfigure it, and then click **Save**.  
  
4.  To **delete** an existing customized logging level, select it in the list, and then click **Delete**.  
  
 **Permissions for customized logging levels.**  
  
-   All users of the SSISDB database can see customized logging levels and select a customized logging level when they run packages.  
  
-   Only users in the ssis_admin or sysadmin role can create, update, or delete customized logging levels.  

## <a name="custom_messages"></a> Custom Messages for Logging
SQL Server Integration Services provides a rich set of custom events for writing log entries for packages and many tasks. You can use these entries to save detailed information about execution progress, results, and problems by recording predefined events or user-defined messages for later analysis. For example, you can record when a bulk insert begins and ends to identify performance issues when the package runs.  
  
 The custom log entries are a different set of entries than the set of standard logging events that are available for packages and all containers and tasks. The custom log entries are tailored to capture useful information about a specific task in a package. For example, one of the custom log entries for the Execute SQL task records the SQL statement that the task executes in the log.  
  
 All log entries include date and time information, including the log entries that are automatically written when a package begins and finishes. Many of the log events write multiple entries to the log. This typically occurs when the event has different phases. For example, the **ExecuteSQLExecutingQuery** log event writes three entries: one entry after the task acquires a connection to the database, another after the task starts to prepare the SQL statement, and one more after the execution of the SQL statement is completed.  
  
 The following [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] objects have custom log entries:  
  
 [Package](#Package)  
  
 [Bulk Insert Task](#BulkInsert)  
  
 [Data Flow Task](#DataFlow)  
  
 [Execute DTS 2000 Task](#ExecuteDTS200)  
  
 [Execute Process Task](#ExecuteProcess)  
  
 [Execute SQL Task](#ExecuteSQL)  
  
 [File System Task](#FileSystem)  
  
 [FTP Task](#FTP)  
  
 [Message Queue Task](#MessageQueue)  
  
 [Script Task](#Script)  
  
 [Send Mail Task](#SendMail)  
  
 [Transfer Database Task](#TransferDatabase)  
  
 [Transfer Error Messages Task](#TransferErrorMessages)  
  
 [Transfer Jobs Task](#TransferJobs)  
  
 [Transfer Logins Task](#TransferLogins)  
  
 [Transfer Master Stored Procedures Task](#TransferMasterStoredProcedures)  
  
 [Transfer SQL Server Objects Task](#TransferSQLServerObjects)  
  
 [Web Services Task](#WebServices)  
  
 [WMI Data Reader Task](#WMIDataReader)  
  
 [WMI Event Watcher Task](#WMIEventWatcher)  
  
 [XML Task](#XML)  
  
### Log Entries  
  
####  <a name="Package"></a> Package  
 The following table lists the custom log entries for packages.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**PackageStart**|Indicates that the package began to run. This log entry is automatically written to the log. You cannot exclude it.|  
|**PackageEnd**|Indicates that the package completed. This log entry is automatically written to the log. You cannot exclude it.|  
|**Diagnostic**|Provides information about the system configuration that affects package execution such as the number executables that can be run concurrently.<br /><br /> The **Diagnostic** log entry also includes before and after entries for calls to external data providers.|  
  
####  <a name="BulkInsert"></a> Bulk Insert Task  
 The following table lists the custom log entries for the Bulk Insert task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**DTSBulkInsertTaskBegin**|Indicates that the bulk insert began.|  
|**DTSBulkInsertTaskEnd**|Indicates that the bulk insert finished.|  
|**DTSBulkInsertTaskInfos**|Provides descriptive information about the task.|  
  
####  <a name="DataFlow"></a> Data Flow Task  
 The following table lists the custom log entries for the Data Flow task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**BufferSizeTuning**|Indicates that the Data Flow task changed the size of the buffer. The log entry describes the reasons for the size change and lists the temporary new buffer size.|  
|**OnPipelinePostEndOfRowset**|Denotes that a component has been given its end-of-rowset signal, which is set by the last call of the **ProcessInput** method. An entry is written for each component in the data flow that processes input. The entry includes the name of the component.|  
|**OnPipelinePostPrimeOutput**|Indicates that the component has completed its last call to the **PrimeOutput** method. Depending on the data flow, multiple log entries may be written. If the component is a source, this means that the component has finished processing rows.|  
|**OnPipelinePreEndOfRowset**|Indicates that a component is about to receive its end-of-rowset signal, which is set by the last call of the **ProcessInput** method. An entry is written for each component in the data flow that processes input. The entry includes the name of the component.|  
|**OnPipelinePrePrimeOutput**|Indicates that the component is about to receive its call from the **PrimeOutput** method. Depending on the data flow, multiple log entries may be written.|  
|**OnPipelineRowsSent**|Reports the number of rows provided to a component input by a call to the **ProcessInput** method. The log entry includes the component name.|  
|**PipelineBufferLeak**|Provides information about any component that kept buffers alive after the buffer manager goes away. This means that buffers resources were not released and may cause memory leaks. The log entry provides the name of the component and the ID of the buffer.|  
|**PipelineExecutionPlan**|Reports the execution plan of the data flow. It provides information about how buffers will be sent to components. This information, in combination with the PipelineExecutionTrees entry, describes what is occurring in the task.|  
|**PipelineExecutionTrees**|Reports the execution trees of the layout in the data flow. The scheduler of the data flow engine use the trees to build the execution plan of the data flow.|  
|**PipelineInitialization**|Provides initialization information about the task. This information includes the directories to use for temporary storage of BLOB data, the default buffer size, and the number of rows in a buffer. Depending on the configuration of the Data Flow task, multiple log entries may be written.|  
  
####  <a name="ExecuteDTS200"></a> Execute DTS 2000 Task  
 The following table lists the custom log entries for the Execute DTS 2000 task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**ExecuteDTS80PackageTaskBegin**|Indicates that the task began to run a DTS 2000 package.|  
|**ExecuteDTS80PackageTaskEnd**|Indicates that the task finished.<br /><br /> Note: The DTS 2000 package may continue to run after the task ends.|  
|**ExecuteDTS80PackageTaskTaskInfo**|Provides descriptive information about the task.|  
|**ExecuteDTS80PackageTaskTaskResult**|Reports the execution result of the DTS 2000 package that the task ran.|  
  
####  <a name="ExecuteProcess"></a> Execute Process Task  
 The following table lists the custom log entries for the Execute Process task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**ExecuteProcessExecutingProcess**|Provides information about the process of running the executable that the task is configured to run.<br /><br /> Two log entries are written. One contains information about the name and location of the executable that the task runs, and the other records the exit from the executable.|  
|**ExecuteProcessVariableRouting**|Provides information about which variables are routed to the input and outputs of the executable. Log entries are written for stdin (the input), stdout (the output), and stderr (the error output).|  
  
####  <a name="ExecuteSQL"></a> Execute SQL Task  
 The following table describes the custom log entry for the Execute SQL task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**ExecuteSQLExecutingQuery**|Provides information about the execution phases of the SQL statement. Log entries are written when the task acquires connection to the database, when the task starts to prepare the SQL statement, and after the execution of the SQL statement is completed. The log entry for the prepare phase includes the SQL statement that the task uses.|  
  
####  <a name="FileSystem"></a> File System Task  
 The following table describes the custom log entry for the File System task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**FileSystemOperation**|Reports the operation that the task performs. The log entry is written when the file system operation starts and includes information about the source and destination.|  
  
####  <a name="FTP"></a> FTP Task  
 The following table lists the custom log entries for the FTP task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**FTPConnectingToServer**|Indicates that the task initiated a connection to the FTP server.|  
|**FTPOperation**|Reports the beginning of and the type of FTP operation that the task performs.|  
  
####  <a name="MessageQueue"></a> Message Queue Task  
 The following table lists the custom log entries for the Message Queue task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**MSMQAfterOpen**|Indicates that the task finished opening the message queue.|  
|**MSMQBeforeOpen**|Indicates that the task began to open the message queue.|  
|**MSMQBeginReceive**|Indicates that the task began to receive a message.|  
|**MSMQBeginSend**|Indicates that the task began to send a message.|  
|**MSMQEndReceive**|Indicates that the task finished receiving a message.|  
|**MSMQEndSend**|Indicates that the task finished sending a message|  
|**MSMQTaskInfo**|Provides descriptive information about the task.|  
|**MSMQTaskTimeOut**|Indicates that the task timed out.|  
  
####  <a name="Script"></a> Script Task  
 The following table describes the custom log entry for the Script task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**ScriptTaskLogEntry**|Reports the results of implementing logging in the script. A log entry is written for each call to the **Log** method of the **Dts** object. The entry is written when the code is run. For more information, see [Logging in the Script Task](../../integration-services/extending-packages-scripting/task/logging-in-the-script-task.md).|  
  
####  <a name="SendMail"></a> Send Mail Task  
 The following table lists the custom log entries for the Send Mail task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**SendMailTaskBegin**|Indicates that the task began to send an e-mail message.|  
|**SendMailTaskEnd**|Indicates that the task finished sending an e-mail message.|  
|**SendMailTaskInfo**|Provides descriptive information about the task.|  
  
####  <a name="TransferDatabase"></a> Transfer Database Task  
 The following table lists the custom log entries for the Transfer Database task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**SourceDB**|Specifies the database that the task copied.|  
|**SourceSQLServer**|Specifies the computer from which the database was copied.|  
  
####  <a name="TransferErrorMessages"></a> Transfer Error Messages Task  
 The following table lists the custom log entries for the Transfer Error Messages task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**TransferErrorMessagesTaskFinishedTransferringObjects**|Indicates that the task finished transferring error messages.|  
|**TransferErrorMessagesTaskStartTransferringObjects**|Indicates that the task started to transfer error messages.|  
  
####  <a name="TransferJobs"></a> Transfer Jobs Task  
 The following table lists the custom log entries for the Transfer Jobs task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**TransferJobsTaskFinishedTransferringObjects**|Indicates that the task finished transferring [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs.|  
|**TransferJobsTaskStartTransferringObjects**|Indicates that the task started to transfer [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs.|  
  
####  <a name="TransferLogins"></a> Transfer Logins Task  
 The following table lists the custom log entries for the Transfer Logins task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**TransferLoginsTaskFinishedTransferringObjects**|Indicates that the task finished transferring logins.|  
|**TransferLoginsTaskStartTransferringObjects**|Indicates that the task started to transfer logins.|  
  
####  <a name="TransferMasterStoredProcedures"></a> Transfer Master Stored Procedures Task  
 The following table lists the custom log entries for the Transfer Master Stored Procedures task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**TransferStoredProceduresTaskFinishedTransferringObjects**|Indicates that the task finished transferring user-defined stored procedures that are stored in the **master** database.|  
|**TransferStoredProceduresTaskStartTransferringObjects**|Indicates that the task started to transfer user-defined stored procedures that are stored in the **master** database.|  
  
####  <a name="TransferSQLServerObjects"></a> Transfer SQL Server Objects Task  
 The following table lists the custom log entries for the Transfer [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Objects task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**TransferSqlServerObjectsTaskFinishedTransferringObjects**|Indicates that the task finished transferring [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database objects.|  
|**TransferSqlServerObjectsTaskStartTransferringObjects**|Indicates that the task started to transfer [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database objects.|  
  
####  <a name="WebServices"></a> Web Services Task  
 The following table lists the custom log entries that you can enable for the Web Services task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**WSTaskBegin**|The task began to access a Web service.|  
|**WSTaskEnd**|The task completed a Web service method.|  
|**WSTaskInfo**|Descriptive information about the task.|  
  
####  <a name="WMIDataReader"></a> WMI Data Reader Task  
 The following table lists the custom log entries for the WMI Data Reader task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**WMIDataReaderGettingWMIData**|Indicates that the task began to read WMI data.|  
|**WMIDataReaderOperation**|Reports the WQL query that the task ran.|  
  
####  <a name="WMIEventWatcher"></a> WMI Event Watcher Task  
 The following table lists the custom log entries for the WMI Event Watcher task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**WMIEventWatcherEventOccurred**|Denotes that the event occurred that the task was monitoring.|  
|**WMIEventWatcherTimedout**|Indicates that the task timed out.|  
|**WMIEventWatcherWatchingForWMIEvents**|Indicates that the task began to execute the WQL query. The entry includes the query.|  
  
####  <a name="XML"></a> XML Task  
 The following table describes the custom log entry for the XML task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|**XMLOperation**|Provides information about the operation that the task performs|  

## Related Tasks  
 The following list contains links to topics that show how to perform tasks related to the logging feature.  
  
-   [Events Logged by an Integration Services Package](../../integration-services/performance/events-logged-by-an-integration-services-package.md)  
