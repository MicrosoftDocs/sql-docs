---
title: "Enable Logging for Package Execution on the SSIS Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
ms.assetid: 8930c63c-bc6f-46c2-b428-b3c29ee89a7d
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Enable Logging for Package Execution on the SSIS Server
  This procedure describes how to set or change the logging level for a package when you execute a package that you have deployed to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server. The logging level you set when you execute the package overrides the package logging you configure using [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. See [Enable Package Logging in SQL Server Data Tools](../../2014/integration-services/enable-package-logging-in-sql-server-data-tools.md) for more information.  
  
 You can specify the logging level by using one of the following methods. This topic covers the first method.  
  
-   Configuring an instance of a package execution by using the Execute Package dialog box  
  
-   Setting parameters for an instance of an execution by using the [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database)  
  
-   Configuring a SQL Server Agent job for a package execution by using the New Job Step dialog box.  
  
### To set the logging level for a package by using the Execute Package dialog box  
  
1.  In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], navigate to the package in Object Explorer.  
  
2.  Right-click the package and select **Execute**.  
  
3.  Select the **Advanced** tab in the **Execute Package** dialog box.  
  
4.  Under **Logging level**, select the logging level. See the table below for a description of available values.  
  
5.  Complete any other package configurations, then click **OK** to run the package.  
  
 The following logging levels are available.  
  
|Logging Level|Description|  
|-------------------|-----------------|  
|None|Logging is turned off. Only the package execution status is logged.|  
|Basic|All events are logged, except custom and diagnostic events. This is the default value.|  
|Performance|Only performance statistics, and OnError and OnWarning events, are logged.<br /><br /> The **Execution Performance** report displays Active Time and Total Time for package data flow components. This information is available when the logging level of the last package execution was set to **Performance** or **Verbose**. For more information, see [Reports for the Integration Services Server](../../2014/integration-services/reports-for-the-integration-services-server.md).<br /><br /> The [catalog.execution_component_phases](/sql/integration-services/system-views/catalog-execution-component-phases) view displays the start and end times for the data flow components, for each phase of an execution. This view displays this information for these components only when the logging level of the package execution is set to **Performance** or **Verbose**.|  
|Verbose|All events are logged, including custom and diagnostic events.<br /><br /> An example of a diagnostic event, is the DiagnosticEx event. Whenever an Execute Package task executes a child package, it logs this event. The event message consists of the parameter values passed to child packages<br /><br /> The value of the message column for DiagnosticEx is XML text. . To view the message text for a package execution, query the [catalog.operation_messages &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-operation-messages-ssisdb-database) view.<br /><br /> Note: Custom events include those events that are logged by [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] tasks. For more information, see [Custom Messages for Logging](../../2014/integration-services/custom-messages-for-logging.md).<br /><br /> The [catalog.execution_data_statistics](../relational-databases/statistics/statistics.md) view displays a row each time a data flow component sends data to a downstream component, for a package execution. The logging level must be set to **Verbose** to capture this information in the view.|  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Logging](performance/integration-services-ssis-logging.md)   
 [Enable Package Logging in SQL Server Data Tools](../../2014/integration-services/enable-package-logging-in-sql-server-data-tools.md)  
  
  
