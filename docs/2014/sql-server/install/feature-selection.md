---
title: "Feature Selection | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "feature selection, Setup"
helpviewer_keywords: 
  - "SQL Server Installation Wizard, Components to Install page"
  - "Components to Install page [SQL Server Installation Wizard]"
ms.assetid: 73182088-153b-4634-a060-d14d1fd23b70
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Feature Selection
  Use the check boxes on the **Feature Selection** page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation wizard to select components for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation.  
  
## Installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Features  
 On the **Feature Selection** page, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features are separated into two main sections: **Instance Features** and **Shared Features**.  
  
 **Instance Features** refers to the components that are installed once for each instance so that you have multiple copies of them (one for each instance). Each instance can be a separate version that has a different patch level. When you install a patch, whether it is a service pack, a hotfix, or a cumulative update, it updates only the files for one instance on a given machine, plus the shared features if they have not already been updated.  
  
 **Shared Features** refers to features that are common across all instances on a given machine. Each of these shared features is designed to be backward compatible with supported [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions (service packs, cumulative updates, and hotfixes) that can install side by side.  
  
> [!NOTE]  
>  **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Failover Clusters:** The [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components are the only [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] features that support failover clustering. You can install other components, like [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] or [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], on a failover cluster node, but they are not cluster-aware, and their services do not fail over if the failover cluster node goes offline. For more information, see [AlwaysOn Failover Cluster Instances &#40;SQL Server&#41;](../failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md).  
  
### Instance Features  
 A description for each component group appears in the **Description** pane when you select it. You can select any combination of check boxes. To continue, you must make a selection.  
  
|Features|Description|  
|--------------|-----------------|  
|[!INCLUDE[ssDE](../../includes/ssde-md.md)] Services|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] includes the [!INCLUDE[ssDE](../../includes/ssde-md.md)], the core service for storing, processing, and securing data, replication, full-text search, tools for managing relational and XML data, and the [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)] (DQS) server. The following are features of the database engine:<br /><br /> The [!INCLUDE[ssDE](../../includes/ssde-md.md)] is the core service for storing, processing, and securing data.<br /><br /> Replication: Optional: Replication is a set of technologies for copying and distributing data and database objects from one database to another and then synchronizing between databases to maintain consistency.<br /><br /> Full-Text Search: Optional: Full-Text Search provides functionality to issue full-text queries against plain character-based data in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables.<br /><br /> [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)]: Optional: [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)] (DQS) is a data-cleansing solution that enables you to discover inconsistent and incorrect data in your data source, and provides computer-assisted and interactive ways to cleanse your data. Select this check box to install DQS server. After the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation completes, you have to run the DQSInstaller.exe file to *complete* the DQS server installation. If you installed the default instance of SQL server, this file is available at C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.MSSQLSERVER\MSSQL\Binn.<br /><br /> <br /><br /> **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Failover Clusters:** Replication and Full-Text Search components are required, and automatically selected by Setup for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover clustering installations when you select Database Engine Services.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] includes the tools for creating and managing online analytical processing (OLAP) and data mining applications.|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] -Native|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode includes server and client components for creating, managing, and deploying tabular, matrix, graphical, and free-form reports. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is also an extensible platform that you can use to develop report applications.|  
  
> [!IMPORTANT]
>  1.  Setup does not configure load-balancing and single-URL addressing for multiple nodes in a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] scale-out deployment. To complete a scale-out deployment, you must use Windows Server, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Application Center, or third-party cluster management software. For more information about setting up Web farm deployment, see [Configuring Reporting Services for Scale-Out Deployment](https://go.microsoft.com/fwlink/?LinkId=199448) (https://go.microsoft.com/fwlink/?LinkId=199448).  
> 2.  For browser requirements of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components, see [Planning for Reporting Services and Power View Browser Support &#40;Reporting Services 2014&#41;](../../../2014/reporting-services/browser-support-for-reporting-services-and-power-view.md).  
> 3.  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is not supported in side-by-side configurations on the 64-bit platform and on the 32-bit subsystem (WOW64) of a 64-bit server at the same time.  
  
### Shared Features  
 Features shared by all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a single computer are installed to a single directory. They include the following:  
  
|Feature|Description|  
|-------------|-----------------|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] - SharePoint|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode is a server based application for creating, managing, and delivering reports to email, multiple file formats, and interactive Web-based formations. SharePoint mode integrates the report viewing and report management experience with SharePoint products. For more information, see [Reporting Services Report Server &#40;SharePoint Mode&#41;](../../../2014/reporting-services/reporting-services-report-server-sharepoint-mode.md).|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint Products|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint products includes management and user interface components to integrate a SharePoint product with an [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server in SharePoint mode. For more information, see [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md).|  
|Data Quality Client|Data Quality Client is a standalone application that connects to the DQS server, and provides an intuitive graphical user interface to perform data-cleansing and data-matching operations, and to perform administrative tasks in DQS.|  
|Client Tools Connectivity|Client Tools includes components for communication between clients and servers, including network libraries for DB-Library, OLEDB for OLAP, ODBC, ADODB, and ADOMD+.|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is a set of graphical tools and programmable objects for moving, copying, and transforming data.|  
|Client Tools Backward Compatibility|Client Tools Backward Compatibility includes the following components:<br /><br /> SQL Distributed Management Objects (SQL-DMO). For more information, see [Discontinued SQL Server Features in SQL Server 2014](../../../2014/getting-started/discontinued-sql-server-features-in-sql-server-2014.md).<br /><br /> Decision Support Objects (DSO). For more information, see [Breaking Changes to Analysis Services Features in SQL Server 2014](../../../2014/analysis-services/breaking-changes-to-analysis-services-features-in-sql-server-2014.md).|  
|Client Tools SDK|Includes the software development kit containing resources for programmers.|  
|Documentation Components|Documentation components include the components to view and manage help content.|  
|Management Tools - Basic|**Management Tools - Basic :** This includes the following:<br /><br /> [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] support for the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], **sqlcmd** utility, and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell provider<br /><br /> **Management Tools - Complete :** This includes the following components in addition to the components in the basic version:<br /><br /> [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] support for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]<br /><br /> [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]<br /><br /> Database Engine Tuning Advisor<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility management|  
|Distributed Replay Controller|The Distributed Replay controller orchestrates the actions of the distributed replay clients. There can only be one controller instance in each Distributed Replay environment. For more information, see [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md).|  
|Distributed Replay Client|The Distributed Replay clients work together to simulate workloads against an instance of SQL Server. There can be one or more clients in each Distributed Replay environment. For more information, see [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md).|  
|SQL Client Connectivity SDK|Includes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client (ODBC/OLE DB) SDK for database application development.|  
|[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]|[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] is a platform for integrating data from disparate systems across an organization into a single source of master data for accuracy and auditing purposes. The [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] option installs [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)], assemblies, a Windows PowerShell snap-in, and folders and files for Web applications and services.|  
  
 By default, shared components are installed to %Program Files%Microsoft SQL Server\\. To change the installation path, click the **Browse** button. If you change the installation path for one shared component, you also change it for other shared components. Subsequent installations will install shared components to the same location as the original installation.  
  
 **Instance root directory** - By default, the instance root directory is C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\. To specify a non-default root directory, click the **Browse** button, or provide a path name.  
  
 On x64-based operating systems, you can specify where 64-bit components will be installed, and where on the WOW64 subsystem 32-bit components will be installed.  
  
## Disk Space Requirements  
 Use the Disk Cost Summary page to examine disk space requirements for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that you selected for your installation.  
  
### Options  
 If required disk space is too high, consider the following options:  
  
-   Change installation directories to a drive with more disk space.  
  
-   Change the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features for your installation.  
  
-   Create more free space on the specified drive by moving other files or applications.  
  
## Installing AdventureWorks Sample Databases  
 By default, sample databases and sample code are not installed as part of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. For more information about installing sample databases and sample code, see the [Microsoft SQL Server Community Projects & Samples](https://go.microsoft.com/fwlink/?LinkId=87843) (https://go.microsoft.com/fwlink/?LinkId=87843).  
  
 Additional information about samples is available after [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] has been installed. From the **Start** menu, click **All Programs**, click **[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]**, click **Documentation and Tutorials**, and then click **[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Samples Overview**.  
  
## See Also  
 [Editions and Components of SQL Server 2014](../editions-and-components-of-sql-server-2016.md)  
  
  
