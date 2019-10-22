---
title: "Supported Version and Edition Upgrades - SQL Server 2019| Microsoft Docs"
ms.custom: ""
ms.date: "11/01/2019"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "components [SQL Server], adding to existing installations"
  - "versions [SQL Server], upgrading"
  - "upgrading SQL Server, upgrades supported"
  - "cross-language support"
ms.assetid: 702359c4-6ca9-42a8-860c-a95a802898a1
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions"
---
# Supported Version and Edition Upgrades for SQL Server 2019

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
  
  You can upgrade from [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)], and [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]. This article lists the supported upgrade paths from these [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions, and the supported edition upgrades for [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)].  
  
## Pre upgrade Checklist  
  
-   Before upgrading from one edition of [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] to another, verify that the functionality you are currently using is supported in the edition to which you are moving.  
  
-   Before upgrading [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], enable Windows Authentication for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent and verify the default configuration: that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account is a member of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sysadmin group.  
  
-   To upgrade to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)], you must be running a supported operating system. For more information, see [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
-   Upgrade will be blocked if there is a pending restart.  
  
-   Upgrade will be blocked if the Windows Installer service is not running.  
  
## Unsupported Scenarios  
  
-   Cross-version instances of [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] are not supported. Version numbers of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] components must be the same in an instance of [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)].  
  
-   [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] is only available for 64-bit platforms. Cross-platform upgrade is not supported. You cannot upgrade a 32-bit instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to native 64-bit using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. However, you can back up or detach databases from a 32-bit instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then restore or attach them to a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (64-bit) if the databases are not published in replication. You must re-create any logins and other user objects in master, msdb, and model system databases.  
  
-   You cannot add new features during the upgrade of your existing instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. After you upgrade an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)], you can add features by using the [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Setup. For more information, see [Add Features to an Instance of SQL Server &#40;Setup&#41;](../../database-engine/install-windows/add-features-to-an-instance-of-sql-server-2016-setup.md).  
 
## Upgrades from Earlier Versions to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)]  
 
[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] supports upgrade from the following versions of SQL Server:

- SQL Server 2008 SP4 or later
- SQL Server 2008 R2 SP3 or later
- SQL Server 2012 SP2 or later
- SQL Server 2014 or later
- SQL Server 2016 or later
- SQL Server 2017

> [!NOTE]  
>  To upgrade databases on [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] see [Support for 2005](#SupportFor2005).  
  
 The table below lists the supported upgrade scenarios from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)].  
  
|Upgrade from|Supported upgrade path|  
|:------|:------|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP4 Enterprise|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP4 Developer|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP4 Standard|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP4 Small Business|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP4 Web|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP4 Workgroup|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP4 Express |[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Express|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP3 Datacenter|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP3 Enterprise|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP3 Developer|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP3 Small Business|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP3 Standard|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP3 Web|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP3 Workgroup|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP3 Express |[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Express|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP2 Enterprise|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP2 Developer|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP2 Standard|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 Web|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP2 Express |[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Express <br/> <br/> |  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP2 Business Intelligence|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP2 Evaluation|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Evaluation <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Developer|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Express |[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Express <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Evaluation|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Evaluation <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer|
|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Enterprise|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Developer|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Standard|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard|  
|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Web|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web|  
|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Express |[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Express <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer|  
|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Business Intelligence|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/> |  
|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Evaluation|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Evaluation <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer|
|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] release candidate* |[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise |  
|[!INCLUDE[sssqlv14_md](../../includes/sssqlv14-md.md)] Developer |[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise | 

 \* Microsoft support to upgrade from release candidate software is specifically for customers who participated in the Technology Adoption Program (TAP). 

   
###  <a name="SupportFor2005"></a> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Support for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]  
 This section discusses [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] support for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. In [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)], you will be able to do the following:  
  
-   Attach a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database (mdf/ldf files) to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] instance of database engine.  
  
-   Restore a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] instance of database engine from a backup.  
  
-   Back up a [!INCLUDE[ssASversion2005](../../includes/ssasversion2005-md.md)] cube and restore it on [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)].  
  
When a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database is upgraded to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)], the database compatibility level will be changed from 90 to 100. (In [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)], valid values for the database compatibility level are 100, 110, 120, 130, and 140.) [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) discusses how the compatibility level change could affect [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] applications.  
  
Any scenarios not specified in the list above are not supported, including but not limited to the following:  
  
- Installing [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] on same computer (side by side).  
  
- Using a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] instance as a member of the replication topology that involves a [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] instance.  
  
- Configuring database mirroring between [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] and [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] instances.  
  
- Backing up the transaction log with log shipping between [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] and [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] instances.  
  
- Configuring linked servers between [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] and [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] instances.  
  
- Managing a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] instance from a [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Management Studio.  
  
- Attaching a [!INCLUDE[ssASversion2005](../../includes/ssasversion2005-md.md)] cube in [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Management Studio.  
  
- Connecting to [!INCLUDE[ssISversion2005](../../includes/ssisversion2005-md.md)] from [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Management Studio.  
  
- Managing a [!INCLUDE[ssISversion2005](../../includes/ssisversion2005-md.md)] service from [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Management Studio.  
  
- Support for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] third party custom Integration Services components, such as execute and upgrade.  
  
## [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Edition Upgrade  
The following table lists the supported edition upgrade scenarios in [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)].  
  
For step-by-step instructions on how to perform an edition upgrade, see [Upgrade to a Different Edition of SQL Server &#40;Setup&#41;](../../database-engine/install-windows/upgrade-to-a-different-edition-of-sql-server-setup.md).  
  
|Upgrade From|Upgrade To|  
|------------------|----------------|  
|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Server+CAL and Core)**|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise |  
|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Evaluation Enterprise**|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Server+CAL or Core License) <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> Upgrading from Evaluation (a free edition) to any of the paid editions is supported for stand-alone installations, but is not supported for clustered installations.|  
|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard**|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Server+CAL or Core License)|  
|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer**|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Server+CAL or Core License) <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard|  
|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Server+CAL or Core License) <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard|  
|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Express*|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Server+CAL or Core License) <br/><br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard <br/> <br/> [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Web|  
  
 Additionally you can also perform an edition upgrade between [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Server+CAL license) and [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Core License):  
  
|Edition Upgrade From|Edition Upgrade To|  
|--------------------------|------------------------|  
|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Server+CAL License)**|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Core License)|  
|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Core License)|[!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise (Server+CAL License)|  
  
 \* Also applies to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Express with Tools and [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Express with Advanced Services.  
  
 ** Changing the edition of a clustered instance of [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] limited. The following scenarios are not supported for [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] failover clusters:  
  
-   [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Enterprise to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer, Standard, or Evaluation.  
  
-   [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Developer to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard or Evaluation.  
  
-   [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Evaluation.  
  
-   [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Evaluation to [!INCLUDE[sssqlv15-md](../../includes/sssqlv15-md.md)] Standard.  
  
## See Also  

 [Editions and Supported Features for SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md)
 
 [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)   
 
 [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md)  
  
  
