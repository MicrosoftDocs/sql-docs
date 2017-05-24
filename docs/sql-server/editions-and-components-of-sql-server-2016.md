---
title: "Editions and Components of SQL Server | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "05/24/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "server-general"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Enterprise Edition [SQL Server]"
  - "Developer Edition [SQL Server]"
  - "32-bit vs. 64-bit editions [SQL Server]"
  - "default components"
  - "Workgroup Edition [SQL Server]"
  - "Internet servers [SQL Server]"
  - "installing SQL Server, components"
  - "Setup [SQL Server], components"
  - "SQL Server, editions"
  - "SQL Server, components"
  - "client/server applications [SQL Server]"
  - "editions [SQL Server]"
  - "versions [SQL Server]"
  - "Setup [SQL Server], editions"
  - "SQL Server Installation Wizard"
  - "components [SQL Server]"
  - "Standard Edition [SQL Server]"
  - "64-bit edition [SQL Server]"
  - "IIS [SQL Server]"
  - "installing SQL Server, editions"
  - "editions [SQL Server], about edition options"
  - "Setup [SQL Server]"
ms.assetid: e5186f02-dd91-47d0-8fa4-de3f41c76903
caps.latest.revision: 121
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Editions and Components of SQL Server
> For details of features supported by the different editions of SQL Server, see [Editions and Supported Features for SQL Server](../sql-server/editions-and-supported-features-for-sql-server-2016.md).

  Installation requirements vary based on your application needs. The different editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] accommodate the unique performance, runtime, and price requirements of organizations and individuals. The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components that you install also depend on your specific requirements. The following sections help you understand how to make the best choice among the editions and components available in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
## [!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Editions  
 The following table describes the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. 
  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition|Definition|  
|---------------------------------------|----------------|  
|Enterprise|The premium offering, [!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Enterprise edition delivers comprehensive high-end datacenter capabilities with blazing-fast performance, unlimited virtualization, and end-to-end business intelligence — enabling high service levels for mission-critical workloads and end user access to data insights.|  
|Standard|[!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Standard edition delivers basic data management and business intelligence database for departments and small organizations to run their applications and supports common development tools for on-premise and cloud — enabling effective database management with minimal IT resources.|  
|Web|[!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Web edition is a low total-cost-of-ownership option for Web hosters and Web VAPs to provide scalability, affordability, and manageability capabilities for small to large scale Web properties.|  
|Developer|[!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Developer edition lets developers build any kind of application on top of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. It includes all the functionality of Enterprise edition, but is licensed for use as a development and test system, not as a production server. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Developer is an ideal choice for people who build<br />                [!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] and test applications.|  
|Express editions|Express edition is the entry-level, free database and is ideal for learning and building desktop and small server data-driven applications. It is the best choice for independent software vendors, developers, and hobbyists building client applications. If you need more advanced database features, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Express can be seamlessly upgraded to other higher end versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Express LocalDB, a lightweight version of Express that has all of its programmability features, yet runs in user mode and has a fast, zero-configuration installation and a short list of prerequisites.|  
  
## Using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with an Internet Server  
 On an Internet server, such as a server that is running Internet Information Services (IIS), you will typically install the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] client tools. Client tools include the client connectivity components used by an application connecting to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
> **NOTE:**  Although you can install an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on a computer that is running IIS, this is typically done only for small Web sites that have a single server computer. Most Web sites have their middle-tier IIS systems on one server or a cluster of servers, and their databases on a separate server or federation of servers.  
  
## Using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with Client/Server Applications  
 You can install just the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] client components on a computer that is running client/server applications that connect directly to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. A client components installation is also a good option if you administer an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on a database server, or if you plan to develop [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] applications.  
  
 The client tools option installs the following [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] features: backward compatibility components, [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], connectivity components, management tools, software development kit, and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online components. For more information, see  [Install SQL Server](../database-engine/install-windows/install-sql-server.md).  
  
## Deciding Among [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Components  
 Use the Feature Selection page of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Installation Wizard to select the components to include in an installation of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. By default, none of the features in the tree are selected.  
  
 Use the information in the following tables to determine the set of features that best fits your needs.  
  
|Server components|Description|  
|-----------------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|[!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] includes the [!INCLUDE[ssDE](../includes/ssde-md.md)], the core service for storing, processing, and securing data, replication, full-text search, tools for managing relational and XML data,   in database analytics integration, and Polybase integration for access to Hadoop and other heterogeneous data sources, and the [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) server.|  
|[!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]|[!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] includes the tools for creating and managing online analytical processing (OLAP) and data mining applications.|  
|[!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]|[!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] includes server and client components for creating, managing, and deploying tabular, matrix, graphical, and free-form reports. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] is also an extensible platform that you can use to develop report applications.|  
|[!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]|[!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] is a set of graphical tools and programmable objects for moving, copying, and transforming data. It also includes the [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) component for [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)].|  
|[!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]|[!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] (MDS) is the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] solution for master data management. MDS can be configured to manage any domain (products, customers, accounts) and includes hierarchies, granular security, transactions, data versioning, and business rules, as well as an [!INCLUDE[ssMDSXLS](../includes/ssmdsxls-md.md)] that can be used to manage data.|  
|[!INCLUDE[rsql_productname](../includes/rsql-productname-md.md)]|[!INCLUDE[rsql_productname](../includes/rsql-productname-md.md)] supports distributed, scalable R solutions on multiple platforms and using multiple enterprise data sources, including Linux, Hadoop, and Teradata.|  
  
|Management tools|Description|  
|----------------------|-----------------|  
|[!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]|[!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] is an integrated environment to access, configure, manage, administer, and develop components of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] lets developers and administrators of all skill levels use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].<br /><br /> Download and install <br />                [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] from  [Download SQL Server Management Studio](http://msdn.microsoft.com/library/mt238290.aspx)|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager provides basic configuration management for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] services, server protocols, client protocols, and client aliases.|  
|[!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)]|[!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] provides a graphical user interface to monitor an instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)] or [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].|  
|[!INCLUDE[ssDE](../includes/ssde-md.md)] Tuning Advisor|[!INCLUDE[ssDE](../includes/ssde-md.md)] Tuning Advisor helps create optimal sets of indexes, indexed views, and partitions.|  
|Data Quality Client|Provides a highly simple and intuitive graphical user interface to connect to the DQS server, and perform data cleansing operations. It also allows you to centrally monitor various activities performed during the data cleansing operation.|  
|[!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)]|[!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] provides an IDE for building solutions for the Business Intelligence components: [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], and [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)].<br /><br /> (Formerly called Business Intelligence Development Studio).<br /><br /> [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] also includes "Database Projects", which provides an integrated environment for database developers to carry out all their database design work for any [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] platform (both on and off premise) within Visual Studio. Database developers can use the enhanced Server Explorer in Visual Studio to easily create or edit database objects and data, or execute queries.|  
|Connectivity Components|Installs components for communication between clients and servers, and network libraries for DB-Library, ODBC, and OLE DB.|  
  
|Documentation|Description|  
|-------------------|-----------------|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online|Core documentation for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].|  
  
  
