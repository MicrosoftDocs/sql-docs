---
title: "Complete the Database Engine Upgrade"
description: This article describes some extra steps you might have to take after you finish upgrading the Database Engine of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: install
ms.topic: upgrade-and-migration-article
monikerRange: ">=sql-server-2017"
---
# Complete the Database Engine upgrade

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

After upgrading the [!INCLUDE [ssDE](../../includes/ssde-md.md)], complete the following tasks:

- **Back up your databases:**

  Perform a full backup of each database.

- **Enable new features:**

  In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, some changes are only enabled once the `DATABASE_COMPATIBILITY` level for a database is changed to 130 or greater. For more information and for the recommended workflow, see [Change the database compatibility level and use the Query Store](change-the-database-compatibility-mode-and-use-the-query-store.md). If your database has memory-optimized tables created in [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], review [Statistics for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/statistics-for-memory-optimized-tables.md).

- **Integration Services:**

  Migrate Integration Services packages to the latest format. For more information, see [Upgrade Integration Services Packages](../../integration-services/install-windows/upgrade-integration-services-packages.md).

- **Reporting Services:**

  For a new installation upgrade, restore the Reporting Services encryption Keys. For more information, see [Back up and restore SQL Server Reporting Services (SSRS) encryption keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md).

- **Master Data Services:**

  Upgrade the Master Data Services (MDS) database schema and create the [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] web application. For more information, see [Upgrade Master Data Services](upgrade-master-data-services.md).

  > [!NOTE]  
  > MDS is discontinued in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

- **Data Quality Services:**

  Upgrade the Data Quality Services (DQS) databases schema and verify the DQS databases schema upgrade. For more information, see [Upgrade Data Quality Services](upgrade-data-quality-services.md).

  > [!NOTE]  
  > DQS is discontinued in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

- **Full Text Search:**

  Repopulate full-text catalogs to ensure semantic consistency in query results. For more information, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md).
