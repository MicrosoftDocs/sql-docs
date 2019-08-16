---
title: "Troubleshooting Reports for Package Execution | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 8fc476ac-bd69-434e-9636-70776e0b3b6c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Troubleshooting Reports for Package Execution
  In the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], standard reports are available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to help you monitor and troubleshoot [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages that have been deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog. Two of these package reports in particular help you to view package execution status and identify the cause of execution failures.  
  
-   **Integration Services Dashboard** - This report provides an overview of all the package executions on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance in the past 24 hours. The report displays information such as Status, Operation Type, Package Name, etc., for each package.  
  
     The Start Time, End Time, and Duration can be interpreted as follows:  
  
    -   If the package is still running, then Duration = current time - Start Time  
  
    -   If the package has completed, then Duration = End Time - Start Time  
  
     For each package that has run on the server, the dashboard allows you to "zoom in" to find specific details on package execution errors that may have occurred. For example, click **Overview** to display a high-level overview of the status of the tasks in the execution, or **All Messages** to display the detailed messages that were captured as part of the package execution.  
  
     You can filter the table displayed on any page by clicking **Filter** and then selecting criteria in the **Filter Settings** dialog. The filter criteria available depends on the data being displayed. You can change the sort order of the report by clicking the sort icon in the **Filter Settings** dialog.  
  
-   **Activity - All Executions Report** - This report displays a summary of all [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] executions that have been performed on the server. The summary displays information for each execution such as status, start time, and end time. Each summary entry includes links to more information about the execution including messages generated during execution and performance data. As with the Integration Services Dashboard, you can apply a filter to the table to narrow down the information displayed.  
  
## Related Tasks  
 [View Reports for the Integration Services Server](../view-reports-for-the-integration-services-server.md)  
  
## Related Content  
 [Reports for the Integration Services Server](../reports-for-the-integration-services-server.md)  
  
 [Troubleshooting Tools for Package Execution](troubleshooting-tools-for-package-execution.md)  
  
  
