---
title: "Hardware and Software Requirements for Reporting Services in SharePoint Mode | Microsoft Docs"
ms.custom: ""
ms.date: 01/09/2019
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: ed91877d-4f74-4266-a932-b824b4810c99
author: markingmyname
ms.author: maghan
manager: craigg
---
# Hardware and Software Requirements for Reporting Services in SharePoint Mode

  This topic describes prerequisites, hardware requirements, and installation considerations for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] running in SharePoint mode. Because [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode requires a SharePoint server, most of the requirements are based on the SharePoint environment. For native mode report servers, your hardware should meet minimum hardware and software requirements for running [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information, see [Hardware and Software Requirements for Installing SQL Server 2014](hardware-and-software-requirements-for-installing-sql-server.md).  
  
-   [Prerequisites](#bkmk_prereq)  
  
-   [Report Server Database Requirements](#bkmk_report_server_database)  
  
-   [Power View Requirements](#bkmk_powerview)  
  
-   [More Information](#bkmk_more_information)  
  
##  <a name="bkmk_prereq"></a> Prerequisites  
  
-   For local installations, the account logged in during installation of SharePoint and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] needs to be a member of the administrators group in the local operating system. The setup account does not need to be a member of the SharePoint farm administrators group.  
  
     For more information, see [Account permissions and security settings in SharePoint 2013](https://technet.microsoft.com/library/cc678863.aspx).  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] running in SharePoint mode requires SharePoint Server. For more information about SharePoint requirements and configurations, see the following:  
  
    -   [Hardware and software requirements (SharePoint 2013)](https://go.microsoft.com/fwlink/p/?LinkId=256365) (https://go.microsoft.com/fwlink/p/?LinkId=256365)  
  
    -   [Capacity management and sizing for SharePoint Server 2013](https://technet.microsoft.com/library/cc261700.aspx)  
  
    -   [Software requirements for business intelligence (SharePoint 2013)](https://go.microsoft.com/fwlink/p/?LinkId=256367)  
  
    -   [Hardware and software requirements (SharePoint Server 2010)](https://technet.microsoft.com/library/cc262485\(v=office.14\))  
  
    -   [Capacity management and sizing for SharePoint Server 2010](https://technet.microsoft.com/library/cc261700.aspx\(v=office.14\))  
  
-   If you want to upgrade or update existing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint installation with [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], see [Upgrade and Migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md).  
  
-   Verify the **SharePoint 2013 Administration** service is started in Windows Server Manager.  
  
###  <a name="bkmk_report_server_database"></a> Report Server Database Requirements  
  
-   Both [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and SharePoint products and technologies use SQL Server relational databases to store application data.  
  
-   [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] requires an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] of a compatible SQL Server edition. For more information on hardware and software requirements, see [Hardware and Software Requirements for Installing SQL Server 2014](hardware-and-software-requirements-for-installing-sql-server.md).  
  
-   SharePoint products can use an existing database instance. If an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] is not installed, the SharePoint Products Setup program installs SQL Server Express Edition for the SharePoint application databases.  
  
-   The report server instance cannot use the SQL Server Express Edition for its database. However, the SQL Server Express Edition instance installed by the SharePoint product can exist side-by-side with other Database Engine editions.  
  
##  <a name="bkmk_powerview"></a> [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] Requirements

 Review the most up-to-date [Power View documentation](http://office.microsoft.com/excel-help/power-view-explore-visualize-and-present-your-data-HA102835634.aspx) on Office.Microsoft.com. [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] is a feature of Microsoft Excel 2013, and is part of the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Reporting Services add-in for Microsoft SharePoint Server 2010 and 2013 Enterprise Editions.  
  
##  <a name="bkmk_more_information"></a> More Information

 For information on SharePoint changes, see [Changes from SharePoint 2010 to SharePoint 2013](https://technet.microsoft.com/library/ff607742\(office.15\).aspx) (https://technet.microsoft.com/library/ff607742(office.15).aspx).  
  
 [SQL Server 2014 Release Notes](https://go.microsoft.com/fwlink/?LinkID=296445).  
  
  
