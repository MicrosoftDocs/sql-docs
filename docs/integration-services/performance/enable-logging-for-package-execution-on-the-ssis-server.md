---
title: "Enable Logging for Package Execution on the SSIS Server | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "SQL13.SSIS.SSMS.ISMANAGECUSTOMIZEDLOGGINGLEVEL.F1"
ms.assetid: 8930c63c-bc6f-46c2-b428-b3c29ee89a7d
caps.latest.revision: 19
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Enable Logging for Package Execution on the SSIS Server
  This topic describes how to set or change the logging level for a package when you run a package that you have deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. The logging level you set when you run the package overrides the package logging you configure at design time in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. See [Enable Package Logging in SQL Server Data Tools](../../integration-services/performance/enable-package-logging-in-sql-server-data-tools.md) for more information.  
  
 In SQL Server **Server Properties**, under the **Server logging level** property, you can select a default server-wide logging level. You can pick from one of the built-in logging levels described in this topic, or you can pick an existing customized logging level. The selected logging level applies by default to all packages deployed to the SSIS Catalog. It also applies by default to a SQL Agent job step that runs an SSIS package.  
  
 You can also specify the logging level for an individual package by using one of the following methods. This topic covers the first method.  
  
-   Configuring an instance of a package execution by using the Execute Package dialog box  
  
-   Setting parameters for an instance of an execution by using the [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database.md)  
  
-   Configuring a SQL Server Agent job for a package execution by using the New Job Step dialog box.  
  
## Set the logging level for a package by using the Execute Package dialog box  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], navigate to the package in Object Explorer.  
  
2.  Right-click the package and select **Execute**.  
  
3.  Select the **Advanced** tab in the **Execute Package** dialog box.  
  
4.  Under **Logging level**, select the logging level. This topic contains a description of available values.  
  
5.  Complete any other package configurations, then click **OK** to run the package.  
  
## Select a logging level  
 The following built-in logging levels are available. You can also select an existing customized logging level. This topic contains a description of customized logging levels.  
  
|Logging Level|Description|  
|-------------------|-----------------|  
|None|Logging is turned off. Only the package execution status is logged.|  
|Basic|All events are logged, except custom and diagnostic events. This is the default value.|  
|RuntimeLineage|Collects the data required to track lineage information in the data flow. You can parse this lineage information to map the lineage relationship between tasks. ISVs and developers  can build custom lineage mapping tools with this information.|  
|Performance|Only performance statistics, and OnError and OnWarning events, are logged.<br /><br /> The **Execution Performance** report displays Active Time and Total Time for package data flow components. This information is available when the logging level of the last package execution was set to **Performance** or **Verbose**. For more information, see [Reports for the Integration Services Server](../../integration-services/performance/reports-for-the-integration-services-server.md).<br /><br /> The [catalog.execution_component_phases](../../integration-services/system-views/catalog-execution-component-phases.md) view displays the start and end times for the data flow components, for each phase of an execution. This view displays this information for these components only when the logging level of the package execution is set to **Performance** or **Verbose**.|  
|Verbose|All events are logged, including custom and diagnostic events.<br /><br /> Custom events include those events that are logged by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] tasks. For more information about custom events, see [Custom Messages for Logging](../../integration-services/performance/custom-messages-for-logging.md).<br /><br /> An example of a diagnostic event is the **DiagnosticEx** event. Whenever an Execute Package task executes a child package, this event captures the parameter values passed to child packages.<br /><br /> The **DiagnosticEx** event also helps you to get the names of columns in which row-level errors occur. This event writes a data flow lineage map to the log. You can then look up the column name in this lineage map by using the column identifier captured by an error output.  For more info, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).<br /><br /> The value of the message column for **DiagnosticEx** is XML text. To view the message text for a package execution, query the [catalog.operation_messages &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operation-messages-ssisdb-database.md) view. Note that the **DiagnosticEx** event does not preserve whitespace in its XML output to reduce the size of the log. To improve readability, copy the log into an XML editor - in Visual Studio, for example - that supports XML formatting and syntax highlighting.<br /><br /> The [catalog.execution_data_statistics](../../integration-services/system-views/catalog-execution-data-statistics.md) view displays a row each time a data flow component sends data to a downstream component, for a package execution. The logging level must be set to **Verbose** to capture this information in the view.|  
  
## Create and manage customized logging levels by using the Customized Logging Level Management dialog box  
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
  
## See Also  
 [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md)   
 [Enable Package Logging in SQL Server Data Tools](../../integration-services/performance/enable-package-logging-in-sql-server-data-tools.md)  
  
  