---
title: "Upgrade Database Engine | Microsoft Docs"
ms.custom: ""
ms.date: "10/26/2015"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility [SQL Server], databases"
  - "compatibility levels [SQL Server], after upgrade"
  - "Database Engine [SQL Server], upgrading"
ms.assetid: 3c036813-36cf-4415-a0c9-248d0a433859
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Upgrade Database Engine
  This topic provides the information that you will need to prepare for and understand the upgrade process; it covers:  
  
-   Known upgrade issues.  
  
-   Pre-upgrade tasks and considerations.  
  
-   Links to procedural topics for upgrading the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   Links to procedural topics for migrating databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Considerations for failover clusters.  
  
-   Post-upgrade tasks and considerations.  
  
## Known Upgrade issues  
 Before upgrading the [!INCLUDE[ssDE](../../includes/ssde-md.md)], review [SQL Server Database Engine Backward Compatibility](../sql-server-database-engine-backward-compatibility.md). For information about supported upgrade scenarios and upgrade known issues, see [Supported Version and Edition Upgrades](supported-version-and-edition-upgrades.md). For backward compatibility content for other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components, see [Backward Compatibility](../../getting-started/backward-compatibility.md).  
  
> [!IMPORTANT]  
>  Before you upgrade from one edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to another, verify that the functionality that you are currently using is supported in the edition to which you are upgrading.  
  
> [!NOTE]  
>  When you upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] from a prior version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise edition, choose between Enterprise Edition: Core-based Licensing and Enterprise Edition. These Enterprise editions differ only with respect to the licensing modes. For more information, see [Compute Capacity Limits by Edition of SQL Server](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).  
  
## Pre-Upgrade Checklist  
 Upgrading [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from an earlier version is supported by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup program. You can also migrate databases from previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions. Migration can be from one [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to another on the same computer, or from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on another computer. Migration options include use of the Copy Database Wizard, Backup and restore functionality, use of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Import and Export Wizard, and bulk export/bulk import methods.  
  
 Before upgrading the [!INCLUDE[ssDE](../../includes/ssde-md.md)], review the following:  
  
-   Review [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
-   Review [Check Parameters for the System Configuration Checker](check-parameters-for-the-system-configuration-checker.md).  
  
-   Review [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md).  
  
-   Review [Supported Version and Edition Upgrades](supported-version-and-edition-upgrades.md).  
  
-   Review [Use Upgrade Advisor to Prepare for Upgrades](../../sql-server/install/use-upgrade-advisor-to-prepare-for-upgrades.md).  
  
-   Review [Use the Distributed Replay Utility to Prepare for Upgrades](../../sql-server/install/use-the-distributed-replay-utility-to-prepare-for-upgrades.md).  
  
-   Review [SQL Server Database Engine Backward Compatibility](../sql-server-database-engine-backward-compatibility.md).  
  
-   Review [Migrate Query Plans](change-the-database-compatibility-mode-and-use-the-query-store.md).  
  
 Review the following issues and make changes before you upgrade [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   When upgrading instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is enlisted in MSX/TSX relationships, upgrade target servers before you upgrade master servers. If you upgrade master servers before target servers, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent will not be able to connect to master instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   When upgrading from a 64-bit edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to a 64-bit edition of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you must upgrade [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] before you upgrade the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   Back up all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database files from the instance to be upgraded, so that you can restore them, if it is required.  
  
-   Run the appropriate Database Console Commands (DBCC) on databases to be upgraded to ensure that they are in a consistent state.  
  
-   Estimate the disk space that is required to upgrade [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components, in addition to user databases. For disk space that is required by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components, see [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
-   Ensure that existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system databases - master, model, msdb, and tempdb - are configured to autogrow, and ensure that they have sufficient hard disk space.  
  
-   Ensure that all database servers have logon information in the master database. This is important for restoring a database, as system logon information resides in master.  
  
-   Disable all startup stored procedures, as the upgrade process will stop and start services on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance being upgraded. Stored procedures processed at startup time might block the upgrade process.  
  
-   Make sure that Replication is current and then stop Replication.  
  
-   Quit all applications, including all services that have [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] dependencies. Upgrade might fail if local applications are connected to the instance being upgraded.  
  
-   If you use Database Mirroring, see [Minimize Downtime for Mirrored Databases When Upgrading Server Instances](../database-mirroring/upgrading-mirrored-instances.md).  
  
## Upgrading the Database Engine  
 You can overwrite an installation of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later with a version upgrade. If an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is detected when you run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup, all previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] program files are upgraded, and all data stored in the previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is preserved. In addition, earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online will remain intact on the computer.  
  
> [!WARNING]  
>  When running the SQL Server 2014 setup program, the SQL Server instance is stopped and restarted as part of running the pre-upgrade checks.  
  
> [!CAUTION]  
>  When you upgrade [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance will be overwritten and will no longer exist on your computer. Before upgrading, back up [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases and other objects associated with the previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 You can upgrade the [!INCLUDE[ssDE](../../includes/ssde-md.md)] by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard.  
  
### Database Compatibility Level After Upgrade  
 The compatibility levels of the `tempdb`, `model`, `msdb` and **Resource** databases are set to 120 after upgrade. The `master` system database retains the compatibility level it had before upgrade.  
  
 If the compatibility level of a user database was 100 or higher before the upgrade, it remains the same after upgrade. If the compatibility level was 90 before upgrade, in the upgraded database, the compatibility level is set to 100, which is the lowest supported compatibility level in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
> [!NOTE]  
>  New user databases will inherit the compatibility level of the `model` database.  
  
## Migrating Databases  
 You can move user databases to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using backup and restore or detach and attach functionalities in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Copy Databases with Backup and Restore](../../relational-databases/databases/copy-databases-with-backup-and-restore.md) or [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md).  
  
> [!IMPORTANT]  
>  A database that has the identical name on both source and destination servers cannot be moved or copied. In this case, it will be noted as "Already exists."  
  
 For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## After Upgrading the Database Engine  
 After upgrading the [!INCLUDE[ssDE](../../includes/ssde-md.md)], complete the following tasks:  
  
-   Re-register your servers. For more information about registering servers, see [Register Servers](../../ssms/register-servers/register-servers.md).  
  
-   Re-populate full-text catalogs to ensure semantic consistency in query results.  
  
     [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installs new word breakers for use by Full-Text and Semantic Search. The word breakers are used both at indexing time and at query time. If you do not rebuild the full-text catalogs, your search results may be inconsistent. If you issue a full-text query that looks for a phrase that is broken differently by the word breaker in a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the current word breaker, a document or row containing the phrase might not be retrieved. This is because the indexed phrases were broken using different logic than the query is using. The solution is to repopulate (rebuild) the full-text catalogs with the new word breakers so that index time and query time behavior are identical.  
  
     For more information, see [sp_fulltext_catalog &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-fulltext-catalog-transact-sql).  
  
-   Configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation. To reduce the attackable surface area of a system, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] selectively installs and enables key services and features.  
  
-   Validate or remove USE PLAN hints that are generated by [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and applied to queries on partitioned tables and indexes.  
  
     [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] changes the way queries on partitioned tables and indexes are processed. Queries on partitioned objects that use the USE PLAN hint for a plan that is generated by [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] might contain a plan that is not usable in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. We recommend the following procedures after you upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
     **When the USE PLAN hint is specified directly in a query:**  
  
    1.  Remove the USE PLAN hint from the query.  
  
    2.  Test the query.  
  
    3.  If the optimizer does not select an appropriate plan, tune the query, and then consider specifying the USE PLAN hint with the desired query plan.  
  
     **When the USE PLAN hint is specified in a plan guide:**  
  
    1.  Use the sys.fn_validate_plan_guide function to check the validity of the plan guide. Alternatively, you can check for invalid plans by using the Plan Guide Unsuccessful event in [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
    2.  If the plan guide is not valid, drop the plan guide. If the optimizer does not select an appropriate plan, tune the query, and then consider specifying the USE PLAN hint with the query plan that you want.  
  
     A plan that is not valid will not cause the query to fail when the USE PLAN hint is specified in a plan guide. Instead, the query is compiled without using the USE PLAN hint.  
  
 Any databases that were marked full-text enabled or disabled before the upgrade will maintain that status after upgrade. After the upgrade, the full-text catalogs will be rebuilt and populated automatically for all full-text enabled databases. This is a time-consuming and resource-consuming operation. You can pause the full-text indexing operation temporarily by running the following statement:  
  
```  
EXEC sp_fulltext_service 'pause_indexing', 1;  
```  
  
 To resume full-text index population, run the following statement:  
  
```  
EXEC sp_fulltext_service 'pause_indexing', 0;  
```  
  
## See Also  
 [Supported Version and Edition Upgrades](supported-version-and-edition-upgrades.md)   
 [Work with Multiple Versions and Instances of SQL Server](../../../2014/sql-server/install/work-with-multiple-versions-and-instances-of-sql-server.md)   
 [Backward Compatibility](../../getting-started/backward-compatibility.md)   
 [Upgrade Replicated Databases](upgrade-replicated-databases.md)  
  
  
