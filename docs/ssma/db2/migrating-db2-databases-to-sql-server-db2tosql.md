---
title: "Migrate Db2 Databases to SQL Server (Db2ToSQL)"
description: Use this recommended process to migrate Db2 databases to SQL Server or Azure SQL Database using SQL Server Migration Assistant (SSMA).
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: niball, subasak
ms.date: 02/03/2026
ms.service: sql
ms.subservice: ssma
ms.topic: upgrade-and-migration-article
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
f1_keywords:
  - "ssma.db2.migratedata.f1"
---
# Migrate Db2 databases to SQL Server (Db2ToSQL)

SQL Server Migration Assistant (SSMA) for Db2 is a comprehensive environment that helps you quickly migrate Db2 databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database. By using SSMA for Db2, you can review database objects and data, assess databases for migration, migrate database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, and then migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database. You can't migrate SYS and SYSTEM Db2 schemas.

## Recommended migration process

To successfully migrate objects and data from Db2 databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, use the following process:

1. [New Project](new-project-db2tosql.md).

   After you create the project, you can set project conversion, migration, and type mapping options. For information about project settings, see [Project Settings (Conversion)](project-settings-conversion-db2tosql.md) and related sections. For information about how to customize data type mappings, see [Map Db2 and SQL Server data types](mapping-db2-and-sql-server-data-types-db2tosql.md).

1. [Connect to Db2 database](connecting-to-db2-database-db2tosql.md).

1. [Connect to SQL Server](connecting-to-sql-server-db2tosql.md). If you don't have a SQL Server configured already, you can optionally use the [SKU sizing recommendation (preview)](sku-recommendation-for-db2-workloads-db2tosql.md), which takes input from you about your Db2 database and generates a report recommending the right SQL SKUs.

1. [Map Db2 schemas to SQL Server schemas](mapping-db2-schemas-to-sql-server-schemas-db2tosql.md).

1. Optionally, [Assessment Report](assessment-report-db2tosql.md) to assess database objects for conversion and estimate the conversion time.

1. [Convert Db2 schemas](converting-db2-schemas-db2tosql.md).

1. [Load converted database objects into SQL Server](loading-converted-database-objects-into-sql-server-db2tosql.md).

   You can do this in one of the following ways:

   - Save a script and run it in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   - Synchronize the database objects.

1. [Migrate Db2 Data into SQL Server](migrating-db2-data-into-sql-server-db2tosql.md).

1. If necessary, update database applications.

## Related content

- [Install SSMA for Db2 client](installing-ssma-for-db2-client-db2tosql.md)
- [Get started with SSMA for Db2](getting-started-with-ssma-for-db2-db2tosql.md)
