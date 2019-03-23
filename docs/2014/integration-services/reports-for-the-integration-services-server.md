---
title: "Reports for the Integration Services Server | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.SWB.SUMMARY.RENDER.CUSTOM.REPORT.F1"
ms.assetid: e976e7c0-a805-4370-bf73-356c8e3becfb
author: janinezhang
ms.author: janinez
manager: craigg
---
# Reports for the Integration Services Server
  In the current release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], standard reports are available in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to help you monitor [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] projects that have been deployed to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server. These reports help you to view package status and history, and, if necessary, identify the cause of package execution failures.  
  
 At the top of each report page, the back icon takes you to the previous page you viewed, the refresh icon refreshes the information displayed on the page, and the print icon allows you to print the current page.  
  
 For information on how to deploy packages to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server, see [Deploy Projects to Integration Services Server](../../2014/integration-services/deploy-projects-to-integration-services-server.md).  
  
## Integration Services Dashboard  
 The **Integration Services Dashboard** report provides an overview of all the package executions on the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance. For each package that has run on the server, the dashboard allows you to "zoom in" to find specific details on package execution errors that may have occurred.  
  
 The report displays the following sections of information.  
  
|Section|Description|  
|-------------|-----------------|  
|**Execution Information**|Shows the number of executions that are in different states (failed, running, succeeded, others) in the past 24 hours.|  
|**Package Information**|Shows the total number of packages that have been executed in the past 24 hours.|  
|**Connection Information**|Shows the connections that have been used in failed executions in the past 24 hours.|  
|**Package Detailed Information**|Shows the details of the completed executions that have occurred in the past 24 hours. For example, this section shows the number of failed executions versus the total number of executions, the duration of an executions (in seconds), and the average duration of executions for over the past three months.<br /><br /> You can view additional information for a package by clicking **Overview**, **All Messages**, and **Execution Performance**.<br /><br /> The **Execution Performance** report shows the duration of the last execution instance, as well as the start and end times, and the environment that was applied.<br /><br /> The chart and associated table included in the **Execution Performance** report shows the duration of the past 10 successful executions of the package. The table also shows the average execution duration over a three-month period. Different environments and different literal values may have been applied at runtime for these 10 successful executions of the package.<br /><br /> Finally, the **Execution Performance** report shows the Active Time and Total Time for the package data flow components. The Active Time refers to the total amount of time that component has spent executing in all phases, and the Total Time refers to the total time elapsed for a component. The report only displays this information for package components when the logging level of the last package execution was set to Performance or Verbose.<br /><br /> The **Overview** report shows the state of package tasks. The **Messages** report shows the event messages and error messages for the package and tasks, such as reporting the start and end times, and the number of rows written.<br /><br /> You can also click **View Messages** in the **Overview** report to navigate to the **Messages** report. You can also click **View Overview** in the **Messages** report to navigate to the **Overview** report.|  
  
 You can filter the table displayed on any page by clicking **Filter** and then selecting criteria in the **Filter Settings** dialog. The filter criteria that are available depend on the data being displayed. You can change the sort order of the report by clicking the sort icon in the **Filter Settings** dialog.  
  
## All Executions Report  
 The **All Executions Report** displays a summary of all [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] executions that have been performed on the server. There can be multiple executions of the sample package. Unlike the **Integration Services Dashboard** report, you can configure the **All Executions** report to show executions that have started during a range of dates. The dates can span multiple days, months, or years.  
  
 The report displays the following sections of information.  
  
|Section|Description|  
|-------------|-----------------|  
|Filter|Shows the current filter applied to the report, such as the Start time range.|  
|Execution Information|Shows the start time, end time, and duration for each package execution.You can view a list of the parameter values that were used with a package execution, such as values that were passed to a child package using the Execute Package task. To view the parameter list, click Overview.|  
  
 For more information about using the Execute Package task to make values available to a child package, see [Execute Package Task](control-flow/execute-package-task.md).  
  
 For more information about parameters, see [Integration Services &#40;SSIS&#41; Parameters](integration-services-ssis-package-and-project-parameters.md).  
  
## All Connections  
 The **All Connections** report provides the following information for connections that have failed, for executions that have occurred on the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance.  
  
 The report displays the following sections of information.  
  
|Section|Description|  
|-------------|-----------------|  
|Filter|Shows the current filter applied to the report, such as connections with a specified string and the **Last failed time** range.<br /><br /> You set the **Last failed time** range to display only connection failures that occurred during a range of dates. The range can span multiple days, months, or years.|  
|Details|Shows the connection string, number of executions during which a connection failed, and the date when the connection last failed.|  
  
## All Operations Report  
 The **All Operations Report** displays a summary of all [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] operations that have been performed on the server, including package deployment, validation, and execution, as well as other administrative operations. As with the Integration Services Dashboard, you can apply a filter to the table to narrow down the information displayed.  
  
## All Validations Report  
 The **All Validations Report** displays a summary of all [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] validations that have been performed on the server. The summary displays information for each validation such as status, start time, and end time. Each summary entry includes a link to messages generated during validation. As with the Integration Services Dashboard, you can apply a filter to the table to narrow down the information displayed.  
  
## Custom Reports  
 You can add a custom report (.rdl file) to the **SSISDB** catalog node under the **Integration Services Catalogs** node in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. Before adding the report, confirm that you are using a three-part naming convention to fully qualify the objects you reference such as a source table. Otherwise, [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] will display an error. The naming convention is \<database>.\<owner>.\<object>. An example would be SSISDB.internal.executions.  
  
> [!NOTE]  
>  When you add custom reports to the **SSISDB** node under the **Databases** node, the SSISDB prefix is not necessary.  
  
 For instructions on how to create and add a custom report, see [Add a Custom Report to Management Studio](../ssms/object/add-a-custom-report-to-management-studio.md).  
  
## Related Tasks  
 [View Reports for the Integration Services Server](../../2014/integration-services/view-reports-for-the-integration-services-server.md)  
  
## Related Content  
 [Monitoring for Package Executions and Other Operations](performance/monitor-running-packages-and-other-operations.md)  
  
  
