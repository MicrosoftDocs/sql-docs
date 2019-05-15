---
title: "Monitor Running Packages and Other Operations | Microsoft Docs"
ms.custom: supportability
ms.date: 06/04/2018
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.ssis.ssms.isoperations.executions.f1"
  - "sql13.ssis.ssms.isoperations.general.f1"
ms.assetid: cbbcd79f-ab9b-46ec-84cb-4821c1d16b99
author: janinezhang
ms.author: janinez
manager: craigg
---
# Monitor Running Packages and Other Operations

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  You can monitor [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package executions, project validations, and other operations by using one of more of the following tools. Certain tools such as data taps are available only for projects that are deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
-   Logs  
  
     For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
-   Reports  
  
     For more information, see [Reports for the Integration Services Server](#reports).  
  
-   Views  
  
     For more information, see [Views &#40;Integration Services Catalog&#41;](../../integration-services/system-views/views-integration-services-catalog.md).  
  
-   Performance counters  
  
     For more information, see [Performance Counters](../../integration-services/performance/performance-counters.md).  
  
-   Data taps  

> [!NOTE]
> This article describes how to monitor running SSIS packages in general, and how to monitor running packages on premises. You can also run and monitor SSIS packages in Azure SQL Database. For more info, see [Lift and shift SQL Server Integration Services workloads to the cloud](../lift-shift/ssis-azure-lift-shift-ssis-packages-overview.md).
>
> Although you can also run SSIS packages on Linux, no monitoring tools are provided on Linux. For more info, see [Extract, transform, and load data on Linux with SSIS](../../linux/sql-server-linux-migrate-ssis.md).

## Operation Types  
 Several different types of operations are monitored in the **SSISDB** catalog, on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. Each operation can have multiple messages associated with it. Each message can be classified into one of several different types. For example, a message can be of type Information, Warning, or Error. For the full list of message types, see the documentation for the Transact-SQL [catalog.operation_messages &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operation-messages-ssisdb-database.md) view. For a full list of the operations types, see [catalog.operations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operations-ssisdb-database.md).  
  
 Nine different status types are used to indicate the status of an operation. For a full list of the status types, see the [catalog.operations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operations-ssisdb-database.md) view.  

## <a name="active_ops"></a> Active Operations Dialog Box
  Use the **Active Operations** dialog box to view the status of currently running [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] operations on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, such as deployment, validation, and package execution. This data is stored in the SSISDB catalog.  
  
 For more information about related [!INCLUDE[tsql](../../includes/tsql-md.md)] views, see [catalog.operations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operations-ssisdb-database.md), [catalog.validations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-validations-ssisdb-database.md), and [catalog.executions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-executions-ssisdb-database.md)  
  
###  <a name="open_dialog"></a> Open the Active Operations Dialog Box  
  
1.  Open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
2.  Connect Microsoft SQL Server Database Engine  
  
3.  In Object Explorer, expand the **Integration Services** node, right-click **SSISDB**, and then click **Active Operations**.  
  
### Configure the Options  
  
 **Type**  
 Specifies the type of operation. The following are the possible values for the **Type** field and the corresponding values in the operations_type column of the Transact-SQL **catalog.operations** view.  
  
|||  
|-|-|  
|Integration Services initialization|1|  
|Operations cleanup (SQL Agent job)|2|  
|Project versions cleanup (SQL Agent job)|3|  
|Deploy project|101|  
|Restore project|106|  
|Create and start package execution|200|  
|Stop operation (stopping a validation or execution|202|  
|Validate project|300|  
|Validate package|301|  
|Configure catalog|1000|  
  
 **Stop**  
 Click to stop a currently running operation.  

## Viewing and Stopping Packages Running on the Integration Services Server
  The **SSISDB** database stores execution history in internal tables that are not visible to users. However it exposes the information that you need through public views that you can query. It also provides stored procedures that you can call to perform common tasks related to packages.  
  
 Typically you manage [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] objects on the server in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. However you can also query the database views and call the stored procedures directly, or write custom code that calls the managed API. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and the managed API query the views and call the stored procedures to perform many of their tasks. For example, you can view the list of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages that are currently running on the server, and request packages to stop if you have to.  
  
### Viewing the List of Running Packages  
 You can view the list of packages that are currently running on the server in the **Active Operations** dialog box. For more information, see [Active Operations Dialog Box](#active_ops).  
  
 For information about the other methods that you can use to view the list of running packages, see the following topics.  
  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] access  
 To view the list of packages that are running on the server, query the view, [catalog.executions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-executions-ssisdb-database.md) for packages that have a status of 2.  
  
 Programmatic access through the managed API  
 See the <xref:Microsoft.SqlServer.Management.IntegrationServices> namespace and its classes.  
  
### Stopping a Running Package  
 You can request a running package to stop in the **Active Operations** dialog box. For more information, see [Active Operations Dialog Box](#active_ops).  
  
 For information about the other methods that you can use to stop a running package, see the following topics.  
  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] access  
 To stop a package that is running on the server, call the stored procedure, [catalog.stop_operation &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-stop-operation-ssisdb-database.md).  
  
 Programmatic access through the managed API  
 See the <xref:Microsoft.SqlServer.Management.IntegrationServices> namespace and its classes.  
  
### Viewing the History of Packages That Have Run  
 To view the history of packages that have run in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], use the **All Executions** report. For more information on the **All Executions** report and other standard reports, see [Reports for the Integration Services Server](#reports).  
  
 For information about the other methods that you can use to view the history of running packages, see the following topics.  
  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] access  
 To view information about packages that have run, query the view, [catalog.executions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-executions-ssisdb-database.md).  
  
 Programmatic access through the managed API  
 See the <xref:Microsoft.SqlServer.Management.IntegrationServices> namespace and its classes.  

## <a name="reports"></a> Reports for the Integration Services Server
  In the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], standard reports are available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to help you monitor [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects that have been deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. These reports help you to view package status and history, and, if necessary, identify the cause of package execution failures.  
  
 At the top of each report page, the back icon takes you to the previous page you viewed, the refresh icon refreshes the information displayed on the page, and the print icon allows you to print the current page.  
  
 For information on how to deploy packages to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, see [Deploy Integration Services (SSIS) Projects and Packages](../../integration-services/packages/deploy-integration-services-ssis-projects-and-packages.md).  
  
### Integration Services Dashboard  
 The **Integration Services Dashboard** report provides an overview of all the package executions on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. For each package that has run on the server, the dashboard allows you to "zoom in" to find specific details on package execution errors that may have occurred.  
  
 The report displays the following sections of information.  
  
|Section|Description|  
|-------------|-----------------|  
|**Execution Information**|Shows the number of executions that are in different states (failed, running, succeeded, others) in the past 24 hours.|  
|**Package Information**|Shows the total number of packages that have been executed in the past 24 hours.|  
|**Connection Information**|Shows the connections that have been used in failed executions in the past 24 hours.|  
|**Package Detailed Information**|Shows the details of the completed executions that have occurred in the past 24 hours. For example, this section shows the number of failed executions versus the total number of executions, the duration of an executions (in seconds), and the average duration of executions for over the past three months.<br /><br /> You can view additional information for a package by clicking **Overview**, **All Messages**, and **Execution Performance**.<br /><br /> The **Execution Performance** report shows the duration of the last execution instance, as well as the start and end times, and the environment that was applied.<br /><br /> The chart and associated table included in the **Execution Performance** report shows the duration of the past 10 successful executions of the package. The table also shows the average execution duration over a three-month period. Different environments and different literal values may have been applied at runtime for these 10 successful executions of the package.<br /><br /> Finally, the **Execution Performance** report shows the Active Time and Total Time for the package data flow components. The Active Time refers to the total amount of time that component has spent executing in all phases, and the Total Time refers to the total time elapsed for a component. The report only displays this information for package components when the logging level of the last package execution was set to Performance or Verbose.<br /><br /> The **Overview** report shows the state of package tasks. The **Messages** report shows the event messages and error messages for the package and tasks, such as reporting the start and end times, and the number of rows written.<br /><br /> You can also click **View Messages** in the **Overview** report to navigate to the **Messages** report. You can also click **View Overview** in the **Messages** report to navigate to the **Overview** report.|  
  
 You can filter the table displayed on any page by clicking **Filter** and then selecting criteria in the **Filter Settings** dialog. The filter criteria that are available depend on the data being displayed. You can change the sort order of the report by clicking the sort icon in the **Filter Settings** dialog.  
  
### All Executions Report  
 The **All Executions Report** displays a summary of all [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] executions that have been performed on the server. There can be multiple executions of the sample package. Unlike the **Integration Services Dashboard** report, you can configure the **All Executions** report to show executions that have started during a range of dates. The dates can span multiple days, months, or years.  
  
 The report displays the following sections of information.  
  
|Section|Description|  
|-------------|-----------------|  
|Filter|Shows the current filter applied to the report, such as the Start time range.|  
|Execution Information|Shows the start time, end time, and duration for each package execution.You can view a list of the parameter values that were used with a package execution, such as values that were passed to a child package using the Execute Package task. To view the parameter list, click Overview.|  
  
 For more information about using the Execute Package task to make values available to a child package, see [Execute Package Task](../../integration-services/control-flow/execute-package-task.md).  
  
 For more information about parameters, see [Integration Services (SSIS) Package and Project Parameters](../../integration-services/integration-services-ssis-package-and-project-parameters.md).  
  
### All Connections  
 The **All Connections** report provides the following information for connections that have failed, for executions that have occurred on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 The report displays the following sections of information.  
  
|Section|Description|  
|-------------|-----------------|  
|Filter|Shows the current filter applied to the report, such as connections with a specified string and the **Last failed time** range.<br /><br /> You set the **Last failed time** range to display only connection failures that occurred during a range of dates. The range can span multiple days, months, or years.|  
|Details|Shows the connection string, number of executions during which a connection failed, and the date when the connection last failed.|  
  
### All Operations Report  
 The **All Operations Report** displays a summary of all [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] operations that have been performed on the server, including package deployment, validation, and execution, as well as other administrative operations. As with the Integration Services Dashboard, you can apply a filter to the table to narrow down the information displayed.  
  
### All Validations Report  
 The **All Validations Report** displays a summary of all [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] validations that have been performed on the server. The summary displays information for each validation such as status, start time, and end time. Each summary entry includes a link to messages generated during validation. As with the Integration Services Dashboard, you can apply a filter to the table to narrow down the information displayed.  
  
### Custom Reports  
 You can add a custom report (.rdl file) to the **SSISDB** catalog node under the **Integration Services Catalogs** node in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Before adding the report, confirm that you are using a three-part naming convention to fully qualify the objects you reference such as a source table. Otherwise, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] will display an error. The naming convention is \<database>.\<owner>.\<object>. An example would be SSISDB.internal.executions.  
  
> [!NOTE]  
>  When you add custom reports to the **SSISDB** node under the **Databases** node, the SSISDB prefix is not necessary.  
  
 For instructions on how to create and add a custom report, see [Add a Custom Report to Management Studio](../../ssms/object/add-a-custom-report-to-management-studio.md).  

## View Reports for the Integration Services Server
  In the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], standard reports are available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to help you monitor [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects that have been deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  For more information about the reports, see [Reports for the Integration Services Server](#reports).  
  
### To view reports for the Integration Services server  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the **Integration Services Catalogs** node in Object Explorer.  
  
2.  Right-click **SSISDB**, click **Reports**, and then click **Standard Reports**.  
  
3.  Click one more of the following to view a report.  
  
    -   **Integration Services Dashboard**  
  
    -   **All Executions**  
  
    -   **All Validations**  
  
    -   **All Operations**  
  
    -   **All Connections**  

## See Also  
 [Execution of Projects and Packages](../packages/deploy-integration-services-ssis-projects-and-packages.md)   
 [Troubleshooting Reports for Package Execution](../troubleshooting/troubleshooting-reports-for-package-execution.md)  
