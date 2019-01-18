---
title: "Complete the Database Engine Upgrade | Microsoft Docs"
ms.custom: ""
ms.date: "10/23/2017"
ms.prod: sql
ms.technology: install
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 3f08087e-e532-416c-8caa-e0ec88c57596
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Complete the Database Engine Upgrade

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

After the upgrading SQL Server complete, there are a number of additional steps that you may need to take. These include the following:  
  
After upgrading the [!INCLUDE[ssDE](../../includes/ssde-md.md)], complete the following tasks:  
  
- **Backup your databases:** Perform a full backup of each database.  

- **Enable new features:** In SQL Server 2016 and SQL Server 2017, some changes are only enabled once the DATABASE_COMPATIBILITY level for a database has been changed to 130 or greater.  For more information and for the recommended workflow, see [Change the Database Compatibility Mode and Use the Query Store](../../database-engine/install-windows/change-the-database-compatibility-mode-and-use-the-query-store.md). If your database has memory-optimized tables created in SQL Server 2014, review [Statistics for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/statistics-for-memory-optimized-tables.md).
  
- **Integration Services:**  
  
     Migrate Integration Services packages to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] format. For more information, see [Upgrade Integration Services Packages](../../integration-services/install-windows/upgrade-integration-services-packages.md).  
  
- **Reporting Services:** For a new installation upgrade, restore the Reporting Services  encryption Keys. For more information, see [Back Up and Restore Reporting Services Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md).  
  
- **Master Data Services:**  Upgrade the MDS database schema and create the SQL Server 2017 web application. For more information, see [Upgrade Master Data Services](../../database-engine/install-windows/upgrade-master-data-services.md).  
  
- **Data Quality Services:** Upgrade the DQS databases schema and verify the DQS databases schema upgrade. For more information, see [Upgrade Data Quality Services](../../database-engine/install-windows/upgrade-data-quality-services.md).  
  
- **Full Text Search:** Re-populate full-text catalogs to ensure semantic consistency in query results. For more information, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md).  
  
