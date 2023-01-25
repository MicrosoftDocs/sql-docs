---
title: "Upgrade Database Engine"
description: The article provides links to resources that help you upgrade the SQL Server Database Engine from a prior release of SQL Server to SQL Server 2019.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/04/2019
ms.service: sql
ms.subservice: install
ms.topic: conceptual
helpviewer_keywords:
  - "compatibility [SQL Server], databases"
  - "compatibility levels [SQL Server], after upgrade"
  - "Database Engine [SQL Server], upgrading"
monikerRange: ">=sql-server-2016"
---
# Upgrade Database Engine

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
  The articles in this section will help you upgrade the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine from a prior release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)].  
  
1.  [Choose a Database Engine Upgrade Method](../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md). Before beginning an upgrade, you need to understand the various upgrade methods. This article discusses the upgrade methods and the steps involved with each upgrade method.  
  
2.  [Plan and Test the Database Engine Upgrade Plan](../../database-engine/install-windows/plan-and-test-the-database-engine-upgrade-plan.md). After reviewing the upgrade methods, you are ready to develop the appropriate upgrade method for your environment and then test the upgrade method before upgrading the existing environment. This article discusses developing an upgrade plan and testing it.  
  
3.  [Complete the Database Engine Upgrade](../../database-engine/install-windows/complete-the-database-engine-upgrade.md). After your database engine has been upgraded and databases are online, there are additional steps you need to take, including taking a new backup, upgrading the databases functionality to enable new features, and repopulating full-text catalogs. This article discusses these steps.  
  
4.  Upgrade the [Database Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#compatibility-levels-and-database-engine-upgrades) (**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]). One of the steps to take after your databases are online in the new version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], may be to upgrade the databases functionality mode to enable new features, by changing the database compatibility level. This can be done manually or through the Query Tuning Assistant. 

    - [Change the Database Compatibility Mode and Use the Query Store](../../database-engine/install-windows/change-the-database-compatibility-mode-and-use-the-query-store.md). After manually changing the database compatibility level, use the Query Store to monitor performance and identify possible regressions. This article discusses the recommended process and provides a recommended workflow.  

    - [Change the Database Compatibility Mode with Query Tuning Assistant](../../relational-databases/performance/upgrade-dbcompat-using-qta.md). Alternatively to a manual change, use the **Query Tuning Assistant (QTA)** to be guided through the recommended process of changing the database compatibility level. This article discusses this process and provides instructions on the QTA workflow.  

    For more information about new features and improved behaviors available after changing a database compatibility level, see [Differences between Compatibility Levels](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#compatibility-levels-and-stored-procedures).

5.  [Take Advantage of New SQL Server Features](https://www.microsoft.com/sql-server/sql-server-2019). Finally, after you have completed the previous steps, you are ready to explore taking advantage of specific new database engine enhancements. This article suggests a few of these enhancements and provides links for more information.  
  
  
