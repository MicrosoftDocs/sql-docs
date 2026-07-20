---
title: "Change the Database Compatibility Level and Use the Query Store"
description: This article explains the place of using the Query Store to establish a baseline and changing the database compatibility level in a SQL Server upgrade.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: install
ms.topic: concept-article
helpviewer_keywords:
  - "query plans [SQL Server], migrating"
  - "upgrading SQL Server, migrating query plans"
  - "plan guides [SQL Server], migrating query plans"
monikerRange: ">=sql-server-2017"
---
# Change the database compatibility level and use the Query Store

[!INCLUDE [SQL Server-Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later, some changes are only enabled once the [database compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) has been changed. This was done for several reasons:

- Since upgrade is a one-way operation (it isn't possible to downgrade the file format), there's value in separating the enablement of new features to a separate operation within the database. It's possible to revert a setting to a prior database compatibility level. The new model reduces the number of things that must happen during an outage window.

- Changes to the query processor can have complex effects. Even though a "good" change to the system might be great for most workloads, it could cause an unacceptable regression on an important query for others. Separating this logic from the upgrade process allows for features, such as the Query Store, to mitigate plan choice regressions quickly or even avoid them completely in production servers.

The following behaviors are expected for [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] when a database is attached or restored, and after an in-place upgrade:

- If the compatibility level of a user database was 100 or higher before the upgrade, it remains the same after upgrade.
- If the compatibility level of a user database was 90 before upgrade, in the upgraded database, the compatibility level is set to 100, which is the lowest supported compatibility level in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)].
- The compatibility levels of the `tempdb`, `model`, `msdb`, and `Resource` databases are set to the current compatibility level after upgrade.
- The `master` system database retains the compatibility level it had before upgrade.

The upgrade process to enable new query processor functionality is related to the post-release servicing model of the product. Some of those fixes are released under [trace flag 4199](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4199). Customers needing fixes can opt in to those fixes without causing unexpected regressions for other customers. The post-release servicing model for query processor hotfixes is documented in [KB974006](https://support.microsoft.com/help/974006). Beginning with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], moving to a new compatibility level implies that trace flag 4199 is no longer needed, because those fixes are now enabled by default in the latest compatibility level. Therefore, as part of the upgrade process, it's important to validate that 4199 isn't enabled once the upgrade process completes.

> [!NOTE]  
> Trace flag 4199 is still needed to enable any new query processor fixes released after RTM, if applicable.

For information about the recommended workflow to upgrade the query processor to the latest version of the code, see [Keep performance stability during the upgrade to newer SQL Server section of Query Store Usage Scenarios](../../relational-databases/performance/query-store-usage-scenarios.md#CEUpgrade).

:::image type="content" source="media/change-the-database-compatibility-mode-and-use-the-query-store/query-store-usage.png" alt-text="Diagram showing the recommended workflow for upgrading the query processor to the latest version of the code." lightbox="media/change-the-database-compatibility-mode-and-use-the-query-store/query-store-usage.png":::

Starting with [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] 18, users can be guided through the recommended workflow using the Query Tuning Assistant. For more information, see [Upgrade databases using the Query Tuning Assistant](../../relational-databases/performance/upgrade-dbcompat-using-qta.md).

## Related content

- [View or change the compatibility level of a database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md)
- [Query Store Usage Scenarios](../../relational-databases/performance/query-store-usage-scenarios.md)
- [ALTER DATABASE (Transact-SQL) compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
- [Upgrade databases using the Query Tuning Assistant](../../relational-databases/performance/upgrade-dbcompat-using-qta.md)
