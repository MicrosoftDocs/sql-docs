---
title: "Compatibility Certification"
description: Compatibility certification eliminates risks of application compatibility, which allows you to upgrade a SQL Server database on-premises and in the cloud.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/24/2022
ms.service: sql
ms.subservice: install
ms.topic: conceptual
helpviewer_keywords:
  - "compatibility [SQL Server], databases"
  - "compatibility levels [SQL Server], after upgrade"
  - "Database Engine [SQL Server], upgrading"
  - "Databases [SQL Server], upgrading"
  - "compatibility [SQL Server], certification"
  - "compatibility level [SQL Server], upgrades"
monikerRange: ">=sql-server-2016"
---

# Compatibility certification

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Compatibility certification allows businesses to upgrade and modernize a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database on-premises, in the cloud, and on the edge, eliminating risks of application compatibility.

The same [!INCLUDE[ssde_md](../../includes/ssde_md.md)] powers both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] (including Azure SQL Managed Instance). This shared [!INCLUDE[ssde_md](../../includes/ssde_md.md)] means that a user database can be moved seamlessly between on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], while the application code that executes in the database as [!INCLUDE[tsql](../../includes/tsql-md.md)] continues to work as it would in its source system.

For each new release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default compatibility level is set to the version of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. But the compatibility level of previous versions is preserved for continued compatibility of existing applications. This compatibility matrix can be seen [here](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#supported-dbcompats).
Therefore, an application that was certified to work with a given [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version **was in fact certified to work on that version's default compatibility level**.

For example, database compatibility level 130 was the default in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]. Because compatibility levels force specific [!INCLUDE[tsql](../../includes/tsql-md.md)] functional and query optimization behaviors, **a database certified to work on [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] was implicitly certified on database compatibility level 130**. This database can work as-is on a more recent version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (such as [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]) and [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], as long as the database compatibility level is kept as 130.

This is a fundamental principle for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] continuous integration operation model. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] is continuously improved and upgraded in Azure, but because existing databases keep their current compatibility level, they continue to work as designed even after upgrades to the underlying [!INCLUDE[ssde_md](../../includes/ssde_md.md)].

This is also how SharePoint Server 2016 and SharePoint Server 2019 certify on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)], allowing you to deploy any [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that can use the supported database compatibility levels for those SharePoint Server versions. For more information, see [Hardware and software requirements for SharePoint Server 2016](/sharepoint/install/hardware-and-software-requirements#minimum-requirements-for-a-database-server-in-a-farm) and [Hardware and software requirements for SharePoint Server 2019](/sharepoint/install/hardware-and-software-requirements-2019#minimum-requirements-for-a-database-server-in-a-farm).

## Manage upgrade risk with compatibility certification

Using Compatibility Certification is a valuable approach to database modernization. By certifying based on compatibility level, developers set the technical requirements for an application to be supported on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], but decouple the application lifecycle from the database platform lifecycle. This allows companies to keep the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] upgraded as needed by lifecycle policies, using new scalability and performance enhancements that aren't code dependent, and connecting applications **maintain their functional status** through upgrades.

The possibilities of adversely affecting functionality and performance are the main risk factors for any upgrade. Compatibility Certification represents peace of mind in terms of managing these upgrade risks:

- In what relates to [!INCLUDE[tsql](../../includes/tsql-md.md)] behavior, any change means that an application needs to be recertified for correctness. However, the [database compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) setting provides backward compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] only for the specified database, not for the entire server. **Keeping the database compatibility level as-is ensures that existing application queries continue to display the same behavior before and after a [!INCLUDE[ssde_md](../../includes/ssde_md.md)] upgrade**. For more information about [!INCLUDE[tsql](../../includes/tsql-md.md)] behavior and compatibility levels, see [Using compatibility levels for backward compatibility](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#backwardCompat).

- In what relates to performance, because improvements in the Query Optimizer are introduced with every version, it could be expected to encounter query plan differences between different [!INCLUDE[ssde_md](../../includes/ssde_md.md)] versions. Query plan differences in the scope of an upgrade usually translate to risk, when there is potential that some changes may be detrimental for a given query or workload. In turn, this risk is what usually drives the need for application recertification, which can delay upgrades and pose lifecycle and support challenges.

  Mitigating upgrade risks is why Query Optimizer improvements are gated to the default compatibility level of a new release (in other words, the highest compatibility level available for any new version). Compatibility Certification includes **query plan shape protection**: the notion that maintaining a database compatibility level as-is immediately after a [!INCLUDE[ssde_md](../../includes/ssde_md.md)] upgrade translates into using the same query optimization model in the new version, as it was before the upgrade, and the query plan shape shouldn't change.

  For more information, see the [Why query plan shape?](#queryplan_shape) section in this article.

For more information about compatibility levels, see [Using compatibility levels for backward compatibility](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#backwardCompat).

> [!IMPORTANT]  
> For an existing application that was already certified for a given compatibility level, upgrade the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and **maintain** the previous database compatibility level. There is no need to re-certify an application in this scenario. For more information, see [Compatibility levels and Database Engine upgrades](#compatibility-levels-and-database-engine-upgrades) later in this article.
>
> For new development work, or when an existing application requires use of new features such as [Intelligent Query Processing](../../relational-databases/performance/intelligent-query-processing.md), as well as some new [!INCLUDE[tsql](../../includes/tsql-md.md)], plan to upgrade the database compatibility level to the latest available in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and re-certify your application to work with that compatibility level. For more information on upgrading the database compatibility level, see [Best Practices for upgrading Database Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#best-practices-for-upgrading-database-compatibility-level).

### <a id="queryplan_shape"></a> Why query plan shape?

Query plan shape refers to the visual representation of the various operators that make up a query plan. This includes operators like seeks, scans, joins, and sorts, as well as the connections between them that indicate the flow of data and the order of the operations that must be executed to produce the intended result set. The query plan shape is determined by the Query Optimizer.

To keep query performance predictable during an upgrade, one of the fundamental goals is to ensure the same query plan shape is used. This can be achieved by not changing the database compatibility level immediately after an upgrade, even though the underlying [!INCLUDE[ssde_md](../../includes/ssde_md.md)] has different versions. If nothing else changed in the query execution ecosystem, such as significant changes in available resources, or data distribution in the underlying data, a query's performance should remain unchanged.

However, keeping a query plan's shape isn't the only factor that may have performance implications after an upgrade. If you move the database to a newer [!INCLUDE[ssde_md](../../includes/ssde_md.md)] and also make environmental changes, you may be introducing factors that will have immediate impact on a query's performance, even if the query plan retains the same shape across versions. These environmental changes may include the new [!INCLUDE[ssde_md](../../includes/ssde_md.md)] having more or less memory and CPU resources available, changes to server or database configuration options, or changes to data distribution that affect how a query plan is created. This is why it's important to understand that maintaining the database compatibility level protects against changes in the query plan **shape**, but offers no protection from other environmental aspects that influence query performance, some of which are user-initiated changes.

For more information, see the [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#optimizing-select-statements).

## Compatibility certification benefits

There are several immediate benefits to database certification as a compatibility-based approach rather than a named-version approach:

- **Decouple application certification from the platform**. Because of its shared [!INCLUDE[ssde_md](../../includes/ssde_md.md)], for applications that just need to execute [!INCLUDE[tsql](../../includes/tsql-md.md)] queries, there's no need to maintain separate certification processes for Azure and on-premises.
- **Reduce upgrade risks** because during database platform modernization, application and database platform layer upgrade cycles can be separated for less disruption, and improved change management.
- **Upgrade with no code changes**. Upgrading to a new version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] can be done with no code changes by keeping the same compatibility level as the source system, and no immediate need to recertify until such time when the application needs to use enhancements that are only available in a higher database compatibility level.
- **Improve manageability and scalability** without requiring application changes, using enhancements that aren't gated by database compatibility level. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] these include for example:
  - Rich monitoring and troubleshooting improvements, with new [System Dynamic Management Views](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md), [Extended Events](../../relational-databases/extended-events/extended-events.md), and [Automatic Tuning](../../relational-databases/automatic-tuning/automatic-tuning.md).
  - Improved scalability, for example with [Automatic Soft-NUMA](../../database-engine/configure-windows/soft-numa-sql-server.md#automatic-soft-numa), [Accelerated Database Recovery](../../relational-databases/accelerated-database-recovery-concepts.md), or [Memory-optimized tempdb metadata](../../relational-databases/in-memory-database.md#memory-optimized-tempdb-metadata).

New databases are still set to the default compatibility level of the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] version. But when a database is restored or attached from any earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to a new version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], the database retains its existing compatibility level.

> [!IMPORTANT]  
> Before moving a database to a new version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], verify if the database compatibility level is still supported. The database compatibility level support matrix can be seen [here](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#arguments).
>
> Upgrading a database with a compatibility level lower than the allowed level (for example, 90 which was the default in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]), sets the database to the lowest compatibility level allowed (100).
>
> To determine the current compatibility level, query the **compatibility_level** column of [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md).

## Compatibility levels and database engine upgrades

To upgrade the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] to the latest version, while maintaining the database compatibility level that existed before the upgrade and its supportability status, it is recommended to perform **static functional surface area validation** of the application code **in the database** (programmability objects such as stored procedures, functions, triggers, and others) and **in the application** (using a workload trace that captures the dynamic code sent by the application).

This can be easily done by using the [Microsoft Data Migration Assistant](https://www.microsoft.com/download/details.aspx?id=53595) tool (DMA). The absence of errors in the DMA tool output, about missing or incompatible functionality, protects application from any functional regressions on the new target version. If changes are required to ensure your database will work in the new version, then DMA will allow you to pinpoint where changes are needed, and what workarounds are available. For more information, see [Overview of Data Migration Assistant](../../dma/dma-overview.md).

> [!TIP]
> This functional validation is especially important when moving a database from a legacy version (such as [!INCLUDE[ssKilimanjaro](../../includes/ssKilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) into a new version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], because your application code may be using discontinued [!INCLUDE[tsql](../../includes/tsql-md.md)] that is not protected by database compatibility level. But when moving from a more recent version (such as [!INCLUDE[ssSQL15](../../includes/sssql16-md.md)]) to [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] or [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], there is no discontinued [!INCLUDE[tsql](../../includes/tsql-md.md)] to worry about. For more information about discontinued [!INCLUDE[tsql](../../includes/tsql-md.md)], see [Using compatibility level for backward compatibility](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#backwardCompat).

> [!NOTE]  
> DMA supports database compatibility level 100 and above. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] as source version is excluded.

> [!IMPORTANT]  
> [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that some minimal testing is done to validate the success of an upgrade, while maintaining the previous database compatibility level. You should determine what minimal testing means for your own application and scenario.

> [!IMPORTANT]  
> [!INCLUDE[msCoName](../../includes/msconame-md.md)] provides query plan shape protection when:
>
> - The new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version (target) runs on hardware that is comparable to the hardware where the previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version (source) was running.
> - The same [supported database compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#supported-dbcompats) is used both at the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and source [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
> - The **same** database and workload is used both at the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the source [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
>
> Any query plan shape regression (as compared to the source [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]) that occurs in the above conditions will be addressed. Please contact Microsoft Customer Support if this is the case.

## See also

- [ALTER DATABASE COMPATIBILITY LEVEL](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
- [View or Change the Compatibility Level of a Database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md)
- [Best Practices for upgrading Database Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#best-practices-for-upgrading-database-compatibility-level)
