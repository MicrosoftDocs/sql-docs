---
title: "Database Mirroring and Full-Text Catalogs (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "database mirroring [SQL Server], interoperability"
  - "full-text catalogs [SQL Server], database mirroring"
  - "catalogs [SQL Server], database mirroring"
ms.assetid: e34072ae-fe8a-462d-bb03-02fa0987f793
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Database Mirroring and Full-Text Catalogs (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  To mirror a database that has a full-text catalog, use backup as usual to create a full database backup of the principal database, and then restore the backup to copy the database to the mirror server. For more information, see [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md).  
  
## Full-Text Catalog and Indexes Before Failover  
 In a newly created mirror database, the full-text catalog is the same as when the database was backed up. After database mirroring starts, any catalog-level changes that were made by DDL statements (CREATE FULLTEXT CATALOG, ALTER FULLTEXT CATALOG, DROP FULLTEXT CATALOG) are logged and sent to the mirror server to be replayed on the mirror database. However, index-level changes are not reproduced on the mirror database because it is not logged on to the principal server. Therefore, as the contents of the full-text catalog change on the principal database, the contents of the full-text catalog on the mirror database are unsynchronized.  
  
## Full-Text Indexes After Failover  
 After a failover, a full crawl of a full-text index on the new principal server might be required or useful in the following situations:  
  
-   If change-tracking is turned OFF on a full text index, you must start a full crawl on that index by using the following statement:  
  
     ALTER FULLTEXT INDEX ON *table_name* START FULL POPULATION  
  
-   If a full-text index is configured for automatic change tracking, the full-text index is automatically synchronized. However, synchronization slows full-text performance somewhat. If performance is too slow, you can cause a full crawl by setting change tracking off and then resetting it to automatic:  
  
    -   To set change tracking off:  
  
         ALTER FULLTEXT INDEX ON *table_name* SET CHANGE_TRACKING OFF  
  
    -   To set on automatic change tracking to automatic:  
  
         ALTER FULLTEXT INDEX ON *table_name* SET CHANGE_TRACKING AUTO  
  
    > [!NOTE]  
    >  To see whether auto change tracking is on, you can use the [OBJECTPROPERTYEX](../../t-sql/functions/objectpropertyex-transact-sql.md) function to query the **TableFullTextBackgroundUpdateIndexOn** property of the table.  
  
 For more information, see [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md).  
  
> [!NOTE]  
>  Starting a crawl after failover works the same as starting a crawl after a restore.  
  
## After Forcing Service  
 After service is forced to the mirror server (with possible data loss), start a full crawl. The method to use for starting a full crawl depends on whether the full-text index is change tracked. For more information, see "Full-Text Indexes After Failover," earlier in this topic.  
  
## See Also  
 [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md)   
 [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md)   
 [DROP FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-fulltext-index-transact-sql.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)   
 [Back Up and Restore Full-Text Catalogs and Indexes](../../relational-databases/search/back-up-and-restore-full-text-catalogs-and-indexes.md)  
  
  
