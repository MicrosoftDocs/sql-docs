---
title: "Discontinued SQL Server Features in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 0678bfbc-5d3f-44f4-89c0-13e8e52404da
author: mightypen
ms.author: genemi
manager: craigg
---
# Discontinued SQL Server Features in SQL Server 2014
  This topic describes features that are no longer available after you upgrade to [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
## Discontinued Features in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]  
 No discontinued features in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
  
## Discontinued Features in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]  
  
### Discontinued Active Directory Helper Service  
 The Active Directory Helper service and the related components has been removed. The following table lists the associated components that are removed are a result:  
  
|Category|Discontinued Feature|Replacement|  
|--------------|--------------------------|-----------------|  
|System Stored Procedures|sp_ActiveDirectory_Obj<br /><br /> sp_ActiveDirectory_SCP<br /><br /> sp_ActiveDirectory_Start|No replacement available|  
  
## Discontinued Features in SQL Server 2008 R2  
  
### 64-bit Platform Support in Reporting Services  
 Starting in [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)], the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] component no longer supports Itanium-based servers running Windows Server 2003 or Windows Server 2003 R2. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] continues to support other 64-bit operating systems, including Windows Server°2008 for Itanium-Based Systems and Windows Server°2008°R2 for Itanium-Based Systems. To upgrade to [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] from a [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] installation with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] on an Itanium-based system edition of Windows Server 2003 or Windows Server 2003 R2, you must first upgrade the operating system.  
  
## Discontinued Features in SQL Server 2008  
  
### Discontinued SQL-DMO from SQL Server Express Installation  
 SQL-DMO for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has been removed from [!INCLUDE[ssExpressEd10](../includes/ssexpressed10-md.md)]. We recommend that you modify applications that currently use this feature as soon as possible. If you must support SQL-DMO for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Express, install the Backward Compatibility Components from the [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] feature pack from the [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=51230). Use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Management Objects (SMO) for new development work.  
  
### Discontinued Option for Web Assistant  
 The `sp_configure` option to enable Web Assistant has been removed from [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. We recommend that you use [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] instead.  
  
### Surface Area Configuration Tool  
 The Surface Area Configuration tool is discontinued for [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. The following table shows what you can use to configure settings, options, and component features in this release.  
  
|Replacement settings and component features|How to configure|  
|-------------------------------------------------|----------------------|  
|Protocols, connection, and startup options|Use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager.|  
|[!INCLUDE[ssDE](../includes/ssde-md.md)] features|Use Policy-Based Management, the property settings in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], or sp_Configure.|  
|[!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] features|Use the property settings in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].|  
|[!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] - EnableIntegrated Security property|Use the property settings in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].|  
|[!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] - "Schedule events and report delivery" and "Web service and HTTP access"|Edit the RSReportServer.config configuration file.|  
|Command line options|No support in this release.|  
|SOAP and [!INCLUDE[ssSB](../includes/sssb-md.md)] endpoints|Use [CREATE ENDPOINT](/sql/t-sql/statements/create-endpoint-transact-sql)and [ALTER ENDPOINT](/sql/t-sql/statements/alter-endpoint-transact-sql).|  
  
### Discontinued Command Prompt Parameters for SQL Server Setup  
 The following table shows Setup command prompt parameters from earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that are not supported in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)].  
  
|Discontinued parameter|Replacement parameter|  
|----------------------------|---------------------------|  
|ADDLOCAL|/ACTION=Uninstall and /FEATURES|  
|DISABLENETWORKPROTOCOLS|/TCPENABLED for TCP/IP<sup>1</sup>|  
|DISABLENETWORKPROTOCOLS|/NPENABLED for Named Pipes<sup>1</sup>|  
|INSTALLSQLDATADIR|/SQLUSERDBDIR<br /><br /> /SQLUSERDBLOGDIR<br /><br /> /SQLBACKUPDIR<br /><br /> /SQLTEMPDBDIR<br /><br /> /SQLTEMPDBLOGDIR|  
|REINSTALL|No equivalent in this release.|  
|REINSTALLMODE|No equivalent in this release.|  
|REMOVE|/ACTION=Uninstall and /FEATURES|  
|SAMPLEDATABASE|No equivalent in this release.|  
|SAVESYSDB|No equivalent in this release.|  
|SKUUPGRADE<sup>2</sup>|No equivalent in this release.|  
|UPGRADE|/ACTION=Upgrade and /FEATURES|  
|USESYSDB|No equivalent in this release.|  
  
 <sup>1</sup>These parameters are valid only for installation.  
  
 <sup>2</sup>Starting [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], specify /Action=EditionUpgrade, to upgrade an existing edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to a different edition any time without using the original installation media. For more information about the supported version and edition upgrades, see [Supported Version and Edition Upgrades](../database-engine/install-windows/supported-version-and-edition-upgrades.md).  
  
 For more information, see [Install SQL Server 2014 from the Command Prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).  
  
  
