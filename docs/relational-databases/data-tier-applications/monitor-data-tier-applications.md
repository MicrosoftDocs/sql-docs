---
description: "Monitor Data-tier Applications"
title: "Monitor Data-tier Applications | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice:
ms.topic: conceptual
helpviewer_keywords: 
  - "monitoring [SQL Server], data-tier applications"
  - "monitoring server performance [SQL Server], DACs"
  - "data-tier application [SQL Server], monitor"
ms.assetid: d2765828-2385-4019-aef2-1de3ab7d1b26
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Monitor Data-tier Applications
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  A data-tier application (DAC) can be monitored from the **Utility Explorer** and **Object Explorer** in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS), along with system views and tables. In addition, all objects in the database contained in the DAC can be monitored using standard database and [!INCLUDE[ssDE](../../includes/ssde-md.md)] monitoring techniques.  
  
## Before You Begin  
 If you deploy a DAC to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], information about the deployed DAC is incorporated into the SQL Server Utility the next time the utility collection set is sent from the instance to the utility control point. You can then view basic health information about the DAC by using the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] **Utility Explorer**.  
  
 The SSMS **Object Explorer** displays basic configuration information about each DAC deployed to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], regardless of whether the instance is managed in the SQL Server Utility. Also, the database associated with a deployed DAC can be monitored using the same procedures for monitoring any database.  
  
## Using the SQL Server Utility  
 The **Deployed Data-tier Applications** detail page in the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] **Utility Explorer** displays a dashboard that reports the resource utilization of all DACs that have been deployed to instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The top pane of the details page lists each deployed DAC with visual indicators showing whether their utilization of CPU and file resources are outside the policies defined for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. If you select any DAC in the list view, further details are displayed in tabs in the bottom pane of the page. For more information about the information presented on the details page, see [Deployed Data-tier Application Details &#40;SQL Server Utility&#41;](/previous-versions/sql/sql-server-2016/ee240857(v=sql.130)).  
  
 After using the **Deployed Data-tier Applications** detail page to quickly identify any DACs that are either under-utilizing or stressing their hardware resource, you can make plans to address any problems. Multiple DACs that are not fully utilizing their current hardware resources could be consolidated to a single server, freeing some of the servers for other uses. If a DAC is stressing the resources on its current server, the DAC can be moved to a larger server, or additional resources can be added to the current server.  
  
 The minimum and maximum limits for resource usage are defined by application monitoring policies defined in the **Utility Administration** details page. Database administrators can tailor the policies to match the limits established by their organizations. For example, one company might set 75% as the maximum CPU utilization for a DAC, while another company might set the maximum at 80%. For more information about setting application monitoring policies, see [Utility Administration &#40;SQL Server Utility&#41;](/previous-versions/sql/sql-server-2016/ee240832(v=sql.130)).  
  
 To view the **Deployed Data-tier Applications** detail page:  
  
1.  Select the **View/Utility Explorer** menu.  
  
2.  Connect the **Utility Explorer** to the utility control point (UCP).  
  
3.  Select the **View/Utility Explorer Details** menu.  
  
4.  Select the **Deployed Data-tier Applications** node in the **Utility Explorer**.  

 The information in the **Deployed Data-tier Applications** detail page comes from the data in the utility management data warehouse, which defaults to collecting the data every 15 minutes. The interval can also be tailored using the **Utility Administration** details page.  
  
## Using Object Explorer  
 The SSMS **Object Explorer** displays basic configuration information about each DAC deployed to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. This includes both instances that have been enrolled in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, and stand-alone instances that cannot be viewed in the **Utility Explorer**.  
  
 To view the details of a DAC deployed to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]:  
  
1.  Select the **View/Object Explorer** menu.  
  
2.  Connect to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] from the Object Explorer pane.  
  
3.  Select the **View/Object Explorer Details** menu.  
  
4.  Select the server node in **Object Explorer** that maps to the instance, and then navigate to the **Management\Data-tier Applications** node.  
  
5.  The list view in the top pane of the details page lists each DAC deployed to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Select a DAC to display the information in the detail pane at the bottom of the page.  
  
 The right-click menu of the **Data-tier Applications** node is also used to deploy a new DAC or delete an existing DAC.  
  
## Using the DAC System Views and Tables  
 The msdb.dbo.sysdac_history_internal system table records the success or failure of all DAC management actions performed on an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The table records the time each action occurred, and which login initiated the action. For more information, see [sysdac_history_internal &#40;Transact-SQL&#41;](../../relational-databases/system-tables/data-tier-application-tables-sysdac-history-internal.md).  
  
 The DAC system views report basic catalog information. For more information, see [Data-tier Application Views &#40;Transact-SQL&#41;](../system-catalog-views/data-tier-application-views-dbo-sysdac-instances.md).  
  
## Monitoring DAC Databases  
 After a DAC has been successfully deployed, the database contained in the DAC operates the same as any other database. Use standard [!INCLUDE[ssDE](../../includes/ssde-md.md)] techniques and tools for monitoring the performance, log, events, and resource utilization of the database.  
  
## See Also  
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)   
 [Deploy a Data-tier Application](../../relational-databases/data-tier-applications/deploy-a-data-tier-application.md)  
  
