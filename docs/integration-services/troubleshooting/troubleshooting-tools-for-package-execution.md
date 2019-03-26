---
title: "Troubleshooting Tools for Package Execution | Microsoft Docs"
ms.custom: ""
ms.date: "08/26/2016"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Integration Services packages, troubleshooting"
  - "SSIS packages, troubleshooting"
  - "Integration Services, troubleshooting"
  - "errors [Integration Services], troubleshooting"
  - "packages [Integration Services], troubleshooting"
ms.assetid: f18d6ff6-e881-444c-a399-730b52130e7c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Troubleshooting Tools for Package Execution
  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes features and tools that you can use to troubleshoot packages when you execute them after they have been completed and deployed.  
  
 At design time, [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] provides breakpoints to pause package execution, the Progress window, and data viewers to watch your data as it passes through the data flow. However, these features are not available when you are running packages that have been deployed. The main techniques for troubleshooting deployed packages are as follows:  
  
-   Catch and handle package errors by using event handlers.  
  
-   Capture bad data by using error outputs.  
  
-   Track the steps of package execution by using logging.  
  
 You can also use the following tips and techniques to avoid problems with running packages  
  
-   **Help to ensure data integrity by using transactions**. For more information, see [Integration Services Transactions](../../integration-services/integration-services-transactions.md).  
  
-   **Restart packages from the point of failure by using checkpoints**. For more information, see [Restart Packages by Using Checkpoints](../../integration-services/packages/restart-packages-by-using-checkpoints.md).  
  
## Catch and Handle Package Errors by Using Event Handlers  
 You can respond to the many events that are raised by the package and the objects in the package by using event handlers.  
  
-   **Create an event handler for the OnError event**. In the event handler, you can use a Send Mail task to notify an administrator of the failure, use a Script task and custom logic to obtain system information for troubleshooting, or clean up temporary resources or incomplete output. For more information, see [Integration Services &#40;SSIS&#41; Event Handlers](../../integration-services/integration-services-ssis-event-handlers.md).  
  
## Troubleshoot Bad Data by Using Error Outputs  
 You can use the error output available on many data flow components to direct rows that contain errors to a separate destination for later analysis. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).  
  
-   **Capture bad data by using error outputs**. Send rows that contain errors to a separate destination such as an error table or a text file. The error output automatically adds two numeric columns that contain the number of the error that caused the row to be rejected, and the ID of the column in which the error occurred.  
  
-   **Add friendly information to the error outputs**. You can make it easier to analyze the error output by adding the error message and the column name in addition to the two numeric identifiers that are supplied by the error output. For an example of how to add these two additional columns by using scripting, see [Enhancing an Error Output with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-examples/enhancing-an-error-output-with-the-script-component.md).  
  
-   **Or, get the column names by logging the DiagnosticEx event**. This event writes a data flow lineage map to the log. You can then look up the column name in this lineage map by using the column identifier captured by an error output.  For more info, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).  
  
     The value of the message column for **DiagnosticEx** is XML text. To view the message text for a package execution, query the [catalog.operation_messages &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operation-messages-ssisdb-database.md) view. Note that the **DiagnosticEx** event does not preserve whitespace in its XML output to reduce the size of the log. To improve readability, copy the log into an XML editor - in Visual Studio, for example - that supports XML formatting and syntax highlighting.  
  
## Troubleshoot Package Execution by Using Operations Reports  
 Standard operations reports are available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to help you monitor [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages that have been deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog. These package reports help you to view package status and history, and, if necessary, identify the cause of failures.  
  
 For more information, see [Troubleshooting Reports for Package Execution](../../integration-services/troubleshooting/troubleshooting-reports-for-package-execution.md).  
  
## Troubleshoot Package Execution by Using SSISDB Views  
 A number of SSISDB database views are available that you can query to monitor package execution and other operations information. For more information, see [Monitor Running Packages and Other Operations](../../integration-services/performance/monitor-running-packages-and-other-operations.md).  
  
## Troubleshoot Package Execution by Using Logging  
 You can track much of what occurs in your running packages by enabling logging. Log providers capture information about the specified events for later analysis, and save that information in a database table, a flat file, an XML file, or another supported output format.  
  
-   **Enable logging**. You can refine the logging output by selecting only the events and only the items of information that you want to capture. For more information, see [Integration Services (SSIS) Logging](../performance/integration-services-ssis-logging.md).  
  
-   **Select the package's Diagnostic event to troubleshoot provider issues.** There are logging messages that help you troubleshoot a package's interaction with external data sources. For more information, see [Troubleshooting Tools Package Connectivity](troubleshooting-tools-for-package-connectivity.md).  
  
-   **Enhance the default logging output**. Logging typically appends rows to the logging destination each time that a package is run. Although each row of logging output identifies the package by its name and unique identifier, and also identifies the execution of the package by a unique ExecutionID, the large quantity of logging output in a single list can become difficult to analyze.  
  
     The following approach is one suggestion for enhancing the default logging output and making it easier to generate reports:  
  
    1.  **Create a parent table that logs each execution of a package**. This parent table has only a single row for each execution of a package, and uses the ExecutionID to link to the child records in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] logging table. You can use an Execute SQL task at the beginning of each package to create this new row and to record the start time. Then you can use another Execute SQL task at the end of the package to update the row with the end time, duration, and status.  
  
    2.  **Add auditing information to the data flow**. You can use the Audit transformation to add information to rows in the data flow about the package execution that created or modified each row. The Audit transformation makes nine pieces of information available, including the PackageName and ExecutionInstanceGUID. For more information, see [Audit Transformation](../../integration-services/data-flow/transformations/audit-transformation.md). If you have custom information that you would also like to include in each row for auditing purposes, you can add this information to rows in the data flow by using a Derived Column transformation. For more information, see [Derived Column Transformation](../../integration-services/data-flow/transformations/derived-column-transformation.md).  
  
    3.  **Consider capturing row count data**. Consider creating a separate table for row count information, where each instance of package execution is identified by its ExecutionID. Use the Row Count transformation to save the row count into a series of variables at critical points in the data flow. After the data flow ends, use an Execute SQL task to insert the series of values into a row in the table for later analysis and reporting.  
  
     For more information about this approach, see the section, "ETL Auditing and Logging," in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] white paper, [Project REAL: Business Intelligence ETL Design Practices](https://go.microsoft.com/fwlink/?LinkId=96602).  
  
## Troubleshoot Package Execution by Using Debug Dump Files  
 In [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you can create debug dump files that provide information about the execution of a package. For more information, see [Generating Dump Files for Package Execution](../../integration-services/troubleshooting/generating-dump-files-for-package-execution.md).  
  
## Troubleshoot Run-time Validation Issues  
 Sometimes you might not be able to connect to your data sources, or portions of your package cannot be validated, until prior tasks in the package have executed. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes the following features to help you avoid the validation errors that would otherwise result from these conditions:  
  
-   **Configure the DelayValidation property on package elements that are not valid when the package is loaded**. You can set **DelayValidation** to **True** on package elements whose configuration is not valid, to prevent validation errors when the package is loaded. For example, you may have a Data Flow task that uses a destination table that does not exist until an Execute SQL task creates the table at run time. The **DelayValidation** property can be enabled at the package level, or at the level of the individual tasks and containers that the package includes.  
  
     The **DelayValidation** property can be set on a Data Flow task, but not on individual data flow components. You can achieve a similar effect by setting the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.ValidateExternalMetadata%2A> property of individual data flow components to **false**. However, when the value of this property is **false**, the component is not aware of changes to the metadata of external data sources. When set to **true**, the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.ValidateExternalMetadata%2A> property can help to avoid blocking issues caused by locking in the database, especially when the package is using transactions.  
  
## Troubleshoot Run-time Permissions Issues  
 If you encounter errors when trying to run deployed packages by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, the accounts used by Agent might not have the required permissions. For information on how to troubleshoot packages that are run from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs, see [An SSIS package does not run when you call the SSIS package from a SQL Server Agent job step](https://support.microsoft.com/kb/918760). For more information on how to run packages from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs, see [SQL Server Agent Jobs for Packages](../../integration-services/packages/sql-server-agent-jobs-for-packages.md).  
  
 To connect to Excel or Access data sources, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent requires an account that has permission to read, write, create, and delete temporary files in the folder that is specified by the TEMP and TMP environment variables.  
  
## Troubleshoot 64-bit Issues  
  
-   **Some data providers are not available on the 64-bit platform**. In particular, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Jet OLE DB Provider that is required to connect to Excel or Access data sources is not available in a 64-bit version.  
  
## Troubleshoot Errors without a Description  
 If you encounter an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] error that does not have an accompanying description, you can locate the description in [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md) by looking up the error by its number. The list does not include troubleshooting information at this time.  
  
## Related Tasks  
 [Debugging Data Flow](../../integration-services/troubleshooting/debugging-data-flow.md)  
  
## Related Content  
 Blog entry, [Adding the error column name to an error output](https://go.microsoft.com/fwlink/?LinkId=261546), on dougbert.com.  
