---
title: "Install SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "09/09/2016"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "installing SQL Server, preparing to install"
  - "installation [SQL Server]"
ms.assetid: 0300e777-d56b-4d10-9c33-c9ebd2489ee5
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Install SQL Server 2014
## [Download SQL Server 2014 Express](http://www.hanselman.com/blog/DownloadSQLServerExpress.aspx)
  **Thank you to [Scott Hanselman](http://www.hanselman.com/) for collecting all of the installer package links in one place!**
  
 This topic provides an overview of different installation options we have for installing [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information about the various [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components that can be installed, and the installation process, see [Installation for SQL Server 2014](installation-for-sql-server.md).  
> **NOTE:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is available in 32-bit and 64-bit editions. The 64-bit and 32-bit editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are installed either through the Installation Wizard, or at a command prompt. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components, see [Editions and Components of SQL Server 2014](../../sql-server/editions-and-components-of-sql-server-2016.md) and [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 By default, sample databases and sample code are not installed as part of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. To install sample databases and sample code for non-Express editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see the [CodePlex Web site](https://go.microsoft.com/fwlink/?LinkId=87843). To read about support for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sample databases and sample code for [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], see [Databases and Samples Overview](https://go.microsoft.com/fwlink/?LinkId=110391).  
  
 Before you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], review installation requirements, system configuration checks, and security considerations. For more information, see [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md). Review the topics in the next section for information about various installation scenarios for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
  
## Install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Components  
  
|Topic|Description|  
|-----------|-----------------|  
|[About the SQL Server Database Engine](../sql-server-database-engine-overview.md)|Describes how to install and configure the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].|  
|[Install SQL Server Replication](install-sql-server-replication.md)|Describes how to install and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication.|  
|[Install Distributed Replay](../../tools/distributed-replay/install-distributed-replay-overview.md)|Lists down the topics to install the Distributed Replay feature.|  
|[Install SQL Server Management Tools](../../sql-server/install/install-sql-server-management-tools.md)|Describes how to install and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] management tools.|  
|[Install SQL Server PowerShell](install-sql-server-powershell.md)|Describes the considerations for installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell components.|  
  
## How to Install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
  
|Title|Description|  
|-----------|-----------------|  
|[Installation How-to Topics](../../sql-server/install/installation-how-to-topics.md)|Provides links to procedural topics for installing [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] from the installation wizard, from the command prompt, by using configuration files, and by using SysPrep.|  
|[Install SQL Server 2014 on Server Core](install-sql-server-on-server-core.md)|Review this topic to install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] on Windows Server Core.|  
|[Validate a SQL Server Installation](validate-a-sql-server-installation.md)|Review the use of the SQL Discovery report to verify the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features installed on the computer.|  
|[Check Parameters for the System Configuration Checker](check-parameters-for-the-system-configuration-checker.md)|Discusses the function of the System Configuration Checker (SCC).|  
  
## Configuration  
  
|Topic|Description|  
|-----------|-----------------|  
|[Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)|This topic provides an overview of firewall configuration and how to configure the Windows firewall.|  
|[Configure a Multi-Homed Computer for SQL Server Access](../../sql-server/install/configure-a-multi-homed-computer-for-sql-server-access.md)|This topic describes how to configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Firewall with Advanced Security to provide for network connections to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a multi-homed environment.|  
|[Configure the Windows Firewall to Allow Analysis Services Access](../../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md)|You can follow the steps provided in this topic to configure both port and firewall settings to allow access to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or PowerPivot for SharePoint.|  
  
## Related Sections  
 [Install SQL Server 2014 BI Features](../../sql-server/install/install-sql-server-business-intelligence-features.md)  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that are part of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] BI platform include [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)], and several client applications used for creating or working with analytical data. This section of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup documentation explains how to install [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)], [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)], and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 [SQL Server Failover Cluster Installation](../../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md)  
 This section of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup documentation explains how to install, and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster.  
  
## See Also  
 [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md)   
 [Upgrade to SQL Server 2014](upgrade-sql-server.md)   
 [Uninstall SQL Server 2014](../../sql-server/install/uninstall-sql-server.md)   
 [High Availability Solutions &#40;SQL Server&#41;](../../sql-server/failover-clusters/high-availability-solutions-sql-server.md)  
  
  
