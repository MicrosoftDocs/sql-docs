---
title: "Install SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: quickstart
helpviewer_keywords: 
  - "AdventureWorks sample database"
  - "installing SQL Server, preparing to install"
  - "installation [SQL Server]"
ms.assetid: 0300e777-d56b-4d10-9c33-c9ebd2489ee5
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Install SQL Server

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
 
 Beginning with [!INCLUDE[sssql15](../../includes/sssql15-md.md)], [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] is only available as a 64-bit application. Here are important details about how to get SQL Server and how to install it.

## Installation details
  
*  **Options**: Install through the Installation Wizard, a command prompt, or through sysprep
 
*  **Requirements**: Before you install, take some time to review installation requirements, system configuration checks, and security considerations in [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md) 

* **Process**: See [Installation for SQL Server](../../database-engine/install-windows/installation-for-sql-server-2016.md) for complete instructions on the installation process

* **Sample databases and sample code**: 
    * They are not installed as part of SQL Server setup by default 
    * To install them for non-Express editions of SQL Server, see the [GitHub](https://github.com/Microsoft/sql-server-samples)
    

## Get the installation media

[!INCLUDE[GetInstallationMedia](../../includes/getssmedia.md)]

## How to install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]
 
|Title|Description|  
|-----------|-----------------|  
|[Install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] on Server Core](../../database-engine/install-windows/install-sql-server-on-server-core.md)|Review this article to install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] on Windows Server Core.|  
|[Check Parameters for the System Configuration Checker](../../database-engine/install-windows/check-parameters-for-the-system-configuration-checker.md)|Discusses the function of the System Configuration Checker (SCC).|  
|[Install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] from the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md)|Procedural article for a typical [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation by using the Installation Wizard.|  
|[Install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md)|Procedural article that provides sample syntax and installation parameters for running unattended Setup.|  
|[Install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Using a Configuration File](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md)|Procedural article that provides sample syntax and installation parameters for running Setup through a configuration file.|  
|[Install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Using SysPrep](../../database-engine/install-windows/install-sql-server-using-sysprep.md)|Procedural article that provides sample syntax and installation parameters for running Setup through SysPrep.|  
|[Add Features to an Instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] &#40;Setup&#41;](../../database-engine/install-windows/add-features-to-an-instance-of-sql-server-2016-setup.md)|Procedural article for updating components of an existing instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].|  
|[Repair a Failed [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Installation](../../database-engine/install-windows/repair-a-failed-sql-server-installation.md)|Procedural article for repairing a corrupt [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] installation.|  
|[Rename a Computer that Hosts a Stand-Alone Instance of SQL Server](../../database-engine/install-windows/rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md)|Procedural article for updating system metadata that is stored in sys.servers.|  
|[Install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Servicing Updates](../../database-engine/install-windows/install-sql-server-servicing-updates.md)|Procedural article for installing updates for [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].|  
|[View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)|Procedural article for checking errors in setup log files.|  
|[Validate a SQL Server Installation](../../database-engine/install-windows/validate-a-sql-server-installation.md)|Review the use of the SQL Discovery report to verify the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features installed on the computer.|  
  
  
## How to install individual components  
  
|Topic|Description|  
|-----------|-----------------|  
|[Install SQL Server Database Engine](../../database-engine/install-windows/install-sql-server-database-engine.md)|Describes how to install and configure the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].|  
|[Install SQL Server Replication](../../database-engine/install-windows/install-sql-server-replication.md)|Describes how to install and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication.|  
|[Install Distributed Replay - Overview](../../tools/distributed-replay/install-distributed-replay-overview.md)|Lists down the articles to install the Distributed Replay feature.|  
|[Install SQL Server Management Tools with SSMS](https://msdn.microsoft.com/library/af68d59a-a04d-4f23-9967-ad4ee2e63381)|Describes how to install and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] management tools.|  
|[Install SQL Server PowerShell](../../database-engine/install-windows/install-sql-server-powershell.md)|Describes the considerations for installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell components.|  
  

## How to configure SQL Server  
  
|Topic|Description|  
|-----------|-----------------|  
|[Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)|This article provides an overview of firewall configuration and how to configure the Windows firewall.|  
|[Configure a Multi-Homed Computer for SQL Server Access](../../sql-server/install/configure-a-multi-homed-computer-for-sql-server-access.md)|This article describes how to configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Firewall with Advanced Security to provide for network connections to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a multi-homed environment.|  
|[Configure the Windows Firewall to Allow Analysis Services Access](../../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md)|You can follow the steps provided in this article to configure both port and firewall settings to allow access to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint.|  
  
## Related sections  
[Editions and Supported Features for [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]](../../sql-server/editions-and-supported-features-for-sql-server-2016.md)  
[Install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Business Intelligence Features](../../sql-server/install/install-sql-server-business-intelligence-features.md)  
  [SQL Server Failover Cluster Installation](../../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md)  
 
  
## See also  

[Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md)   
 [Upgrade to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]](../../database-engine/install-windows/upgrade-sql-server.md)   
 [Uninstall [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]](../../sql-server/install/uninstall-sql-server.md)   
 [High Availability Solutions &#40;SQL Server&#41;](../../sql-server/failover-clusters/high-availability-solutions-sql-server.md)  
  
  
