---
title: "Upgrade the Database Engine"
description: The article provides links to resources that help you upgrade the SQL Server Database Engine from a prior release of SQL Server to the latest version.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/26/2023
ms.service: sql
ms.subservice: install
ms.topic: conceptual
helpviewer_keywords:
  - "compatibility [SQL Server], databases"
  - "compatibility levels [SQL Server], after upgrade"
  - "Database Engine [SQL Server], upgrading"
monikerRange: ">=sql-server-2016"
---
# Upgrade the Database Engine

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The articles in this section help you upgrade the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] from a prior release of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

1. [Choose a Database Engine upgrade method](choose-a-database-engine-upgrade-method.md). Before beginning an upgrade, you need to understand the various upgrade methods. This article discusses the upgrade methods and the steps involved with each upgrade method.

1. [Plan and test the Database Engine upgrade plan](plan-and-test-the-database-engine-upgrade-plan.md). After reviewing the upgrade methods, you're ready to develop the appropriate upgrade method for your environment and then test the upgrade method before upgrading the existing environment. This article discusses developing an upgrade plan and testing it.

1. [Complete the Database Engine Upgrade](complete-the-database-engine-upgrade.md). After your database engine has been upgraded and databases are online, there are additional steps you need to take, including taking a new backup, upgrading the databases functionality to enable new features, and repopulating full-text catalogs. This article discusses these steps.

1. Upgrade the [Database Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#compatibility-levels-and-database-engine-upgrades).

   **Applies to:** [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]

   One of the steps to take after your databases are online in the new version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], may be to upgrade the databases functionality mode to enable new features, by changing the database compatibility level. This can be done manually or through the Query Tuning Assistant.

   - [Change the Database Compatibility Mode and Use the Query Store](change-the-database-compatibility-mode-and-use-the-query-store.md). After manually changing the database compatibility level, use the Query Store to monitor performance and identify possible regressions. This article discusses the recommended process and provides a recommended workflow.

   - [Change the Database Compatibility Mode with Query Tuning Assistant](../../relational-databases/performance/upgrade-dbcompat-using-qta.md). Alternatively to a manual change, use the **Query Tuning Assistant (QTA)** to be guided through the recommended process of changing the database compatibility level. This article discusses this process and provides instructions on the QTA workflow.

   For more information about new features and improved behaviors available after changing a database compatibility level, see [Differences between Compatibility Levels](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#compatibility-levels-and-stored-procedures).

1. [Take Advantage of New SQL Server Features](https://www.microsoft.com/sql-server/sql-server-2022). Finally, after you have completed the previous steps, you're ready to explore taking advantage of specific new database engine enhancements. This article suggests a few of these enhancements and provides links for more information.
