---
title: "Supported Version and Edition Upgrades | Microsoft Docs"
ms.custom: ""
ms.date: "10/26/2015"
ms.prod: "sql-server-2014"
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
manager: craigg
---
# Supported Version and Edition Upgrades
  You can upgrade from [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], and [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], and [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. This topic lists the supported upgrade paths from these [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions, and the supported edition upgrades for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
## Pre upgrade Checklist  
  
-   Before upgrading from one edition of [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] to another, verify that the functionality you are currently using is supported in the edition to which you are moving.  
  
-   Before upgrading [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], enable Windows Authentication for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent and verify the default configuration: that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account is a member of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sysadmin group.  
  
-   To upgrade to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], you must be running a supported operating system. For more information, see [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
-   Upgrade will be blocked if there is a pending restart.  
  
-   Upgrade will be blocked if the Windows Installer service is not running.  
  
## Unsupported Scenarios  
  
-   Cross-version instances of [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] are not supported. Version numbers of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components must be the same in an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
-   Cross-platform upgrade is not supported. You cannot upgrade a 32-bit instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to native 64-bit using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. However, you can back up or detach databases from a 32-bit instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then restore or attach them to a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (64-bit) if the databases are not published in replication. You must re-create any logins and other user objects in master, msdb, and model system databases.  
  
-   You cannot add new features during the upgrade of your existing instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. After you upgrade an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you can add features by using the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Setup. For more information, see [Add Features to an Instance of SQL Server 2014 &#40;Setup&#41;](add-features-to-an-instance-of-sql-server-setup.md).  
  
-   Failover Clusters are not supported in WOW mode.  
  
-   Upgrade from an Evaluation edition of a previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version is not supported.  
  
## Upgrades from Earlier Versions to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
  
> [!NOTE]  
>  Support for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] is described in more detail in the next section, '[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] support for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]'.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 32-bit editions can be upgraded to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] on the 32-bit subsystem (WOW64) of a 64-bit server.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 64-bit versions can be upgraded to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] 64-bit server only.  
  
> [!NOTE]  
>  When you upgrade to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] from a prior version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise edition, choose between Enterprise Edition: Core-based Licensing and Enterprise Edition. These Enterprise editions differ only with respect to the licensing modes and the maximum number of cores supported. For more information, see [Compute Capacity Limits by Edition of SQL Server](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).  
  
 [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] supports upgrade from the following versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP4 or later  
  
-   [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP3 or later  
  
-   [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP2 or later  
  
-   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 or later  
  
 The table below lists the supported upgrade scenarios from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
|Upgrade from|Supported upgrade path|  
|------------------|----------------------------|  
|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP4 Enterprise|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence|  
|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP4 Developer|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Developer|  
|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP4 Standard|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|  
|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP4 Workgroup|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|  
|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP4 Express,<br /><br /> [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP4 Express with Tools, and<br /><br /> [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP4 Express with Advanced Services|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Express|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP3 Enterprise|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP3 Developer|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Developer|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP3 Standard|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP3 Small Business|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP3 Web|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP3 Workgroup|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP3 Express,<br /><br /> [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP3 Express with Tools, and<br /><br /> [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP3 Express with Advanced Services|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Express|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP2 Datacenter|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP2 Enterprise|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP2 Developer|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Developer|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP2 Small Business|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP2 Standard|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP2 Web|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP2 Workgroup|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP2 Express,<br /><br /> [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP2 Express with Tools, and<br /><br /> [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP2 Express with Advanced Services|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Express|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 Enterprise|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 Developer|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Developer|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 Standard|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 Web|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 Express,<br /><br /> [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 Express with Tools, and<br /><br /> [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 Express Management Studio, and<br /><br /> [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 Express with Advanced Services|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Express|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 Business Intelligence|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence|  
  
### [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Support for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]  
 This section discusses [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] support for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. In [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], you will be able to do the following:  
  
-   Upgrade a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] instance of database engine to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] by running [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] setup using the installation wizard or from the command prompt.  
  
-   Attach a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database (mdf/ldf files) to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] instance of database engine.  
  
-   Restore a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] instance of database engine from a backup.  
  
-   Upgrade a [!INCLUDE[ssISversion2005](../../includes/ssisversion2005-md.md)] package to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. Execute packages with automatic-in-place upgrade.  
  
-   Upgrade a [!INCLUDE[ssASversion2005](../../includes/ssasversion2005-md.md)] to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] by running [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] setup.  
  
-   Back up a [!INCLUDE[ssASversion2005](../../includes/ssasversion2005-md.md)] cube and restoring on [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
-   Upgrade [!INCLUDE[ssRSversion2005](../../includes/ssrsversion2005-md.md)] to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] by running [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] setup.  
  
-   Connect to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] 2014.  
  
 When a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database is upgraded to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], the database compatibility level will be changed from 90 to 100. (In [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], valid values for the database compatibility level are 100, 110 and 120.) [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level) discusses how the compatibility level change could affect [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] applications.  
  
 Any scenarios not specified in the list above are not supported, including but not limited to the following:  
  
-   Installing [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] on same computer (side by side).  
  
-   Using a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] instance as a member of the replication topology that involves a [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] instance.  
  
-   Configuring database mirroring between [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] instances.  
  
-   Backing up the transaction log with log shipping between [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] instances.  
  
-   Configuring linked servers between [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] instances.  
  
-   Managing a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] instance from a [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Management Studio.  
  
-   Attaching a [!INCLUDE[ssASversion2005](../../includes/ssasversion2005-md.md)] cube in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Management Studio.  
  
-   Connecting to [!INCLUDE[ssISversion2005](../../includes/ssisversion2005-md.md)] from [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Management Studio.  
  
-   Managing a [!INCLUDE[ssISversion2005](../../includes/ssisversion2005-md.md)] service from [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Management Studio.  
  
-   Support for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] third party custom Integration Services components, such as execute and upgrade.  
  
## [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Edition Upgrade  
 The following table lists the supported edition upgrade scenarios in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
 For step-by-step instructions on how to perform an edition upgrade, see [Upgrade to a Different Edition of SQL Server 2014 &#40;Setup&#41;](upgrade-to-a-different-edition-of-sql-server-setup.md).  
  
|Upgrade From|Upgrade To|  
|------------------|----------------|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Server+CAL and Core) <sup>2</sup>|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Server+CAL or Core License)|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Evaluation Enterprise <sup>2</sup>|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Server+CAL or Core License)<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Developer<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web<br /><br /> Upgrading from Evaluation Enterprise (a free edition) to any of the paid editions is supported for stand-alone installations, but is not supported for clustered installations.|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard <sup>2</sup>|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Server+CAL or Core License)|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Developer <sup>2</sup>|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Server+CAL or Core License)<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Server+CAL or Core License)<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Express <sup>1</sup>|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Server+CAL or Core License)<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Developer<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Standard<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Web|  
  
 Additionally you can also perform an edition upgrade between [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Server+CAL license) and [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Core License):  
  
|Edition Upgrade From|Edition Upgrade To|  
|--------------------------|------------------------|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Server+CAL License) <sup>2</sup>|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Core License)|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Core License)|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (Server+CAL License)|  
  
 <sup>1</sup> Also applies to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Express with Tools and [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Express with Advanced Services.  
  
 <sup>2</sup> Changing the edition of a [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] failover cluster is limited. The following scenarios are not supported for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] failover clusters:  
  
-   SQL Server 2014 Enterprise to SQL Server 2014 Developer, Standard, or Enterprise Evaluation.  
  
-   SQL Server 2014 Developer to SQL Server 2014 Standard or Enterprise Evaluation.  
  
-   SQL Server 2014 Standard to SQL Server 2014 Enterprise Evaluation.  
  
-   SQL Server 2014 Enterprise Evaluation to SQL Server 2014 Standard.  
  
## See Also  
 [Features Supported by the Editions of SQL Server 2014](../../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md)   
 [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)   
 [Upgrade to SQL Server 2014](upgrade-sql-server.md)   
 [Use Upgrade Advisor to Prepare for Upgrades](../../../2014/sql-server/install/use-upgrade-advisor-to-prepare-for-upgrades.md)  
  
  
