---
title: "Complete the Database Engine Upgrade"
description: This article describes some additional steps you might have to take after you finish upgrading the Database Engine of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: "10/23/2017"
ms.service: sql
ms.subservice: install
ms.topic: conceptual
monikerRange: ">=sql-server-2016"
---
# Complete the Database Engine Upgrade

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

After the upgrading SQL Server complete, there are a number of additional steps that you may need to take. These include the following:  
  
After upgrading the [!INCLUDE[ssDE](../../includes/ssde-md.md)], complete the following tasks:  
  
- **Backup your databases:** Perform a full backup of each database.  

- **Enable new features:** In SQL Server 2016, 2017 and 2019 some changes are only enabled once the DATABASE_COMPATIBILITY level for a database has been changed to 130 or greater.  For more information and for the recommended workflow, see [Change the Database Compatibility Mode and Use the Query Store](../../database-engine/install-windows/change-the-database-compatibility-mode-and-use-the-query-store.md). If your database has memory-optimized tables created in SQL Server 2014, review [Statistics for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/statistics-for-memory-optimized-tables.md).
  
- **Integration Services:**  
  
     Migrate Integration Services packages to the latest format. For more information, see [Upgrade Integration Services Packages](../../integration-services/install-windows/upgrade-integration-services-packages.md).  
  
- **Reporting Services:** For a new installation upgrade, restore the Reporting Services  encryption Keys. For more information, see [Back Up and Restore Reporting Services Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md).  
  
- **Master Data Services:**  Upgrade the MDS database schema and create the SQL Server 2019 web application. For more information, see [Upgrade Master Data Services](../../database-engine/install-windows/upgrade-master-data-services.md).  
  
- **Data Quality Services:** Upgrade the DQS databases schema and verify the DQS databases schema upgrade. For more information, see [Upgrade Data Quality Services](../../database-engine/install-windows/upgrade-data-quality-services.md).  
  
- **Full Text Search:** Re-populate full-text catalogs to ensure semantic consistency in query results. For more information, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md).  
  
