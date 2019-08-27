---
title: "Compatibility Certification | Microsoft Docs"
ms.custom: ""
ms.date: "08/26/2019"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility [SQL Server], databases"
  - "compatibility levels [SQL Server], after upgrade"
  - "Database Engine [SQL Server], upgrading"
  - "Databases [SQL Server], upgrading"
  - "Database Engine [SQL Server], upgrading"
  - "compatibility [SQL Server], certification"
  - "compatibility level [SQL Server], upgrades"
ms.assetid: 3c036813-36cf-4415-a0c9-248d0a433856
author: pmasl
ms.author: pelopes
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
---

# Compatibility Certification

[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Compatibility certification allows businesses to upgrade and modernize a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database on-premises, in the cloud, and on the edge, eliminating risks of application compatibility. 

The same [!INCLUDE[ssde_md](../../includes/ssde_md.md)] powers both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] (including Managed Instance). This shared [!INCLUDE[ssde_md](../../includes/ssde_md.md)] means that a user database can be moved seamlessly between on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], while the application code that executes in the database as [!INCLUDE[tsql](../../includes/tsql-md.md)] continues to work as it would in its source system.

For each new release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default compatibility level is set to the version of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. But the compatibility level of previous versions is preserved for continued compatibility of existing applications. This compatibility matrix can be seen [here](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#arguments).
Therefore, an application that was certified to work with a given [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version was in fact certified to work on that version's default compatibility level.

For example, database compatibility level 130 was the default in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Because compatibility levels force specific [!INCLUDE[tsql](../../includes/tsql-md.md)] functional and query optimization behaviors, a database certified to work on [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] was implicitely certified on database compatibility level 130. This database can work as-is on a more recent version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (such as [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]) and [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], as long as the database compatibility level is kept as 130. 

This is a fundamental principle for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] continuous integration operation model. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] is continuously improved and upgraded in Azure, but because existing databases keep their current compatibility level, they continue to work as designed even after upgrades to the underlying [!INCLUDE[ssde_md](../../includes/ssde_md.md)]. 

## Managing upgrade risk with Compatibility Certification
Using Compatibility Certification is a valuable approach to database modernization. By certifying based on compatibility level, developers set the technical requirements for an application to be supported on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], but allow the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to be upgraded as needed by lifecycle policies, as well as leveraging new scalablity and performance enhancements that are not code dependant.

Applications lifecycle is decoupled from platform lifecycle, and connecting applications **maintain their functional status** through upgrades. Besides maintaining [!INCLUDE[tsql](../../includes/tsql-md.md)] functionality, performance is also a risk factor for any upgrade. With changes in the Query Optimizer being introduced in every version, it could be expected to encounter query plan differences between [!INCLUDE[ssde_md](../../includes/ssde_md.md)] versions, which translate to risk when there is potential that some changes may be detrimental for a given query. However, Compatibility Certification includes **query plan shape protection**. Knowing that a given query that is used by an application will have the same query plan shape, for the same database, after it is moved to a new version represents peace of mind in terms of managing upgrade risks. Certifying based on compatibility level, and maintaining a database compatibility level as-is immediately after a [!INCLUDE[ssde_md](../../includes/ssde_md.md)] upgrade means that the query optimization model used to create query plans in the new version is the same as it was before the upgrade. For more information about query plan shape protection, see the [Using compatibility levels for backward compatibility](#using-compatibility-level-for-backward-compatibility) section in this article.

As long as the application does not need to leverage enhancements that are only available in a higher database compatibility levels, it is a valid approach to upgrade the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and maintain the previous database compatibility level, with no need to recertify an application. 

For new development work, or when an existing application requires use of new features such as [Intelligent Query Processing](../../relational-databases/performance/intelligent-query-processing.md), as well as some new [!INCLUDE[tsql](../../includes/tsql-md.md)], plan to upgrade the database compatibility level to the latest available in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and certify your application to work with that compatibility level. For more details on upgrading the database compatibility level, see [Best Practices for upgrading Database Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#best-practices-for-upgrading-database-compatibility-level).

## Compatibility Certification benefits
There are several immediate benefits to database certification as a compatibility-based approach rather than a named-version approach:

-  **Decouple application certification from the platform**. Because of its shared [!INCLUDE[ssde_md](../../includes/ssde_md.md)], for applications that just need to execute [!INCLUDE[tsql](../../includes/tsql-md.md)] queries, there is no need to maintain separate certification processes for Azure and on-premises.
-  **Reduce upgrade risks** because during database platform modernization, application and database platform layer upgrade cycles can be separated for less disruption, and improved change management.
-  **Upgrade with no code changes**. Upgrading to a new version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] can be done with no code changes by keeping the same compatibility level as the source system, and no immediate need to recertify.
- **Improve manageability and scalability** without any requirement for application changes, using enhancements that are not gated by database compatibility level. These include for example: 
  - Rich monitoring and troubleshooting improvements, with new [System Dynamic Management Views](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md) and [Extended Events](../../relational-databases/extended-events/extended-events.md). 
  - Improved scalability with [Automatic Soft-NUMA](../../database-engine/configure-windows/soft-numa-sql-server.md#automatic-soft-numa).

New Databases are still set to the default compatibility level of the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] version. But when a database is moved from any earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to a new version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], the database retains its existing compatibility level. 

> [!IMPORTANT]
> Before moving a database to a new version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], verify if the database compatibility level is still supported. The database compatibility level support matrix can be seen [here](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#arguments). 
>
> Upgrading a database with a compatibility level lower than the allowed level (for example, 90 which was the default in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]), sets the database to the lowest compatibility level allowed (100).
>
> To determine the current compatibility level, query the **compatibility_level** column of [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md).

## Compatibility levels and database upgrades
To upgrade the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to the latest version, while maintaining the database compatibility level that existed before the upgrade and its supportability status, it is recommended to perform static functional surface area validation of the application code in the database (programmability objects such as stored procedures, functions, triggers, and others) and in the application (using a workload trace that captures the dynamic code sent by the application), by using the [Microsoft Data Migration Assistant](https://www.microsoft.com/download/details.aspx?id=53595) tool (DMA). The absence of errors in the DMA tool output, about missing or incompatible functionality, protects application from any functional regressions on the new target version. For more information, see [Overview of Data Migration Assistant](../../dma/dma-overview.md).

> [!NOTE]
> DMA supports database compatibility level 100 and above. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] as source version is excluded.   

> [!IMPORTANT]
> [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that some minimal testing is done to validate the success of an upgrade, while maintaining the previous database compatibility level. You should determine what minimal testing means for your own application and scenario.   

> [!NOTE]
> [!INCLUDE[msCoName](../../includes/msconame-md.md)] provides query plan shape protection when:
>
> - The new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version (target) runs on hardware that is comparable to the hardware where the previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version (source) was running.
> - The same [supported database compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#remarks) is used both at the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and source [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
>
> Any query plan shape regression (as compared to the source [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]) that occurs in the above conditions will be addressed. Please contact Microsoft Customer Support if this is the case.

## Using compatibility level for backward compatibility
The [database compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) setting affects behaviors only for the specified database, not for the entire server. Database compatibility level provides backward compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in what relates to [!INCLUDE[tsql](../../includes/tsql-md.md)] and query optimization behaviors. 

Starting with compatibility mode 130, any new query plan affecting features have been intentionally added only to the new compatibility level. This has been done in order to minimize the risk during upgrades that arise from performance degradation due to query plan changes potentially introduced by new query optimization behaviors.      
From an application perspective, the goal should still be to upgrade to the latest compatibility level at some point in time, in order to inherit some of the new features such as [Intelligent Query Processing](../../relational-databases/performance/intelligent-query-processing.md), but to do so in a controlled way. Use the lower compatibility level as a safer migration aid to work around version differences, in the behaviors that are controlled by the relevant compatibility level setting.
For more details, including the recommended workflow for upgrading database compatibility level, see [Best Practices for upgrading Database Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#best-practices-for-upgrading-database-compatibility-level).

> [!IMPORTANT]
> Discontinued functionality introduced in a given [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version is not protected by compatibility level. This refers to functionality that was removed from the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].
> For example, the `FASTFIRSTROW` hint was discontinued in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and replaced with the `OPTION (FAST n )` hint. Setting the database compatibility level to 110 will not restore the discontinued hint.  
>  
> For more information on discontinued functionality, see [Discontinued Database Engine Functionality in SQL Server 2016](../../database-engine/discontinued-database-engine-functionality-in-sql-server-2016.md), [Discontinued Database Engine Functionality in SQL Server 2014](https://docs.microsoft.com/sql/database-engine/discontinued-database-engine-functionality-in-sql-server-2016?view=sql-server-2014), and [Discontinued Database Engine Functionality in SQL Server 2012](https://docs.microsoft.com/sql/database-engine/discontinued-database-engine-functionality-in-sql-server-2016?view=sql-server-2014#Denali).    

> [!IMPORTANT]
> Breaking changes introduced in a given [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version **may** not be protected by compatibility level. This refers to behavior changes between versions of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. [!INCLUDE[tsql](../../includes/tsql-md.md)] behavior is usually protected by compatibility level. However, changed or removed system objects are **not** protected by compatibility level.
>
> An example of a breaking change **protected** by compatibility level is an implicit conversion from datetime to datetime2 data types. Under database compatibility level 130, these show improved accuracy by accounting for the fractional milliseconds, resulting in different converted values. To restore previous conversion behavior, set the database compatibility level to 120 or lower.
>
> Examples of breaking changes **not protected** by compatibility level are:
>
> - Changed column names in system objects. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] the column *single_pages_kb* in sys.dm_os_sys_info was renamed to *pages_kb*. Regardless of the compatibility level, the query `SELECT single_pages_kb FROM sys.dm_os_sys_info` will produce error 207 (Invalid column name).
> - Removed system objects. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] the `sp_dboption` was removed. Regardless of the compatibility level, the statement `EXEC sp_dboption 'AdventureWorks2016', 'autoshrink', 'FALSE';` will produce error 2812 (Could not find stored procedure 'sp_dboption').
>
> For more information on breaking changes, see [Breaking Changes to Database Engine Features in SQL Server 2017](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2017.md), [Breaking Changes to Database Engine Features in SQL Server 2016](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md), [Breaking Changes to Database Engine Features in SQL Server 2014](https://docs.microsoft.com/sql/database-engine/discontinued-database-engine-functionality-in-sql-server-2016?view=sql-server-2014), and [Breaking Changes to Database Engine Features in SQL Server 2012](https://docs.microsoft.com/sql/database-engine/discontinued-database-engine-functionality-in-sql-server-2016?view=sql-server-2014#Denali).
  
## See Also 
[ALTER DATABASE COMPATIBILITY LEVEL](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)       
[View or Change the Compatibility Level of a Database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md)       
[Best Practices for upgrading Database Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#best-practices-for-upgrading-database-compatibility-level)      
