---
title: "Install SQL Server 2016 | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "12/16/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "setup-install"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
helpviewer_keywords: 
  - "AdventureWorks sample database"
  - "installing SQL Server, preparing to install"
  - "installation [SQL Server]"
ms.assetid: 0300e777-d56b-4d10-9c33-c9ebd2489ee5
caps.latest.revision: 59
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Install SQL Server

 > For content related to previous versions of SQL Server, see [Install SQL Server 2014](https://msdn.microsoft.com/en-US/library/bb500395(SQL.120).aspx).

  SQL Server 2016 is a 64-bit application. Here are important details about how to get SQL Server and how to install it.

## Installation details
  
*  **Options**: Install through the Installation Wizard, a command prompt, or through sysprep
 
*  **Requirements**: Before you install, take some time to review installation requirements, system configuration checks, and security considerations in [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md) 

* **Process**: See [Installation for SQL Server](../../database-engine/install-windows/installation-for-sql-server-2016.md) for complete instructions on the installation process

* **Sample databases and sample code**: 
    * They are not installed as part of SQL Server setup by default 
    * To install them for non-Express editions of SQL Server, see the [CodePlex Web site](http://go.microsoft.com/fwlink/?LinkId=87843)
    * To read about support for SQL Server sample databases and sample code for SQL Server Express, see [Databases and Samples Overview](http://go.microsoft.com/fwlink/?LinkId=110391)
    

## Get the installation media

The download location for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] depends on the edition:

- **SQL Server Enterprise, Standard, and Express Editions** are licensed for production use. For Enterprise and Standard Editions, contact your software vendor for the installation media. You can find purchasing information and a directory of Microsoft partners on the [Microsoft purchasing website](https://www.microsoft.com/en-us/server-cloud/products/sql-server/overview.aspx). 

- **Free editions** are available at these links:

| Edition | Description
|---------|--------
|[Developer Edition](http://myprodscussu1.app.vssubscriptions.visualstudio.com/Downloads?q=SQL%20Server%20Developer) | Free, full-featured set of SQL Server 2016 Enterprise edition software that allows developers to build, test, and demonstrate applications in a non-production environment. 
|[Express Edition](https://www.microsoft.com/sql-server/sql-server-editions-express)|  Entry-level, free database that is ideal for deploying small databases in production environments. Build desktop, and small server, data-driven applications up to 10 GB of disk size. 
| [Evaluation Edition](http://technet.microsoft.com/evalcenter/mt130694) | Full feature set of SQL Server Enterprise edition software that provides a 180 day evaluation period.
   
 
  

## How to install SQL Server
 
|Title|Description|  
|-----------|-----------------|  
|[Install SQL Server 2016 on Server Core](../../database-engine/install-windows/install-sql-server-on-server-core.md)|Review this topic to install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] on Windows Server Core.|  
|[Check Parameters for the System Configuration Checker](../../database-engine/install-windows/check-parameters-for-the-system-configuration-checker.md)|Discusses the function of the System Configuration Checker (SCC).|  
|[Install SQL Server 2016 from the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md)|Procedural topic for a typical [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation by using the Installation Wizard.|  
|[Install SQL Server 2016 from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md)|Procedural topic that provides sample syntax and installation parameters for running unattended Setup.|  
|[Install SQL Server 2016 Using a Configuration File](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md)|Procedural topic that provides sample syntax and installation parameters for running Setup through a configuration file.|  
|[Install SQL Server 2016 Using SysPrep](../../database-engine/install-windows/install-sql-server-using-sysprep.md)|Procedural topic that provides sample syntax and installation parameters for running Setup through SysPrep.|  
|[Add Features to an Instance of SQL Server 2016 &#40;Setup&#41;](../../database-engine/install-windows/add-features-to-an-instance-of-sql-server-2016-setup.md)|Procedural topic for updating components of an existing instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|[Repair a Failed SQL Server 2016 Installation](../../database-engine/install-windows/repair-a-failed-sql-server-installation.md)|Procedural topic for repairing a corrupt [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation.|  
|[Rename a Computer that Hosts a Stand-Alone Instance of SQL Server](../../database-engine/install-windows/rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md)|Procedural topic for updating system metadata that is stored in sys.servers.|  
|[Install SQL Server 2016 Servicing Updates](../../database-engine/install-windows/install-sql-server-servicing-updates.md)|Procedural topic for installing updates for SQL Server 2016.|  
|[View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)|Procedural topic for checking errors in setup log files.|  
|[Validate a SQL Server Installation](../../database-engine/install-windows/validate-a-sql-server-installation.md)|Review the use of the SQL Discovery report to verify the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features installed on the computer.|  
  
  
## How to install individual components  
  
|Topic|Description|  
|-----------|-----------------|  
|[Install SQL Server Database Engine](../../database-engine/install-windows/install-sql-server-database-engine.md)|Describes how to install and configure the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].|  
|[Install SQL Server Replication](../../database-engine/install-windows/install-sql-server-replication.md)|Describes how to install and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication.|  
|[Install Distributed Replay - Overview](../../tools/distributed-replay/install-distributed-replay-overview.md)|Lists down the topics to install the Distributed Replay feature.|  
|[Install SQL Server Management Tools with SSMS](http://msdn.microsoft.com/library/af68d59a-a04d-4f23-9967-ad4ee2e63381)|Describes how to install and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] management tools.|  
|[Install SQL Server PowerShell](../../database-engine/install-windows/install-sql-server-powershell.md)|Describes the considerations for installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell components.|  
  

## How to configure SQL Server  
  
|Topic|Description|  
|-----------|-----------------|  
|[Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)|This topic provides an overview of firewall configuration and how to configure the Windows firewall.|  
|[Configure a Multi-Homed Computer for SQL Server Access](../../sql-server/install/configure-a-multi-homed-computer-for-sql-server-access.md)|This topic describes how to configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Firewall with Advanced Security to provide for network connections to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a multi-homed environment.|  
|[Configure the Windows Firewall to Allow Analysis Services Access](../../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md)|You can follow the steps provided in this topic to configure both port and firewall settings to allow access to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint.|  
  
## Related sections  
[Editions and Supported Features for SQL Server 2016](../../sql-server/editions-and-supported-features-for-sql-server-2016.md)  
[Install SQL Server 2016 Business Intelligence Features](../../sql-server/install/install-sql-server-business-intelligence-features.md)  
  [SQL Server Failover Cluster Installation](../../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md)  
 
  
## See also  

[Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md)   
 [Upgrade to SQL Server 2016](../../database-engine/install-windows/upgrade-sql-server.md)   
 [Uninstall SQL Server 2016](../../sql-server/install/uninstall-sql-server.md)   
 [High Availability Solutions &#40;SQL Server&#41;](../../sql-server/failover-clusters/high-availability-solutions-sql-server.md)  
  
  
