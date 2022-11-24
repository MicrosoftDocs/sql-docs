---
title: "Prerequisites for Minimal Logging in Bulk Import"
description: In a simple recovery or bulk-logged recovery model, minimal logging of bulk-import operations reduces the possibility that an operation fills the log space.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
helpviewer_keywords:
  - "minimal logging [SQL Server]"
  - "logged bulk copy [SQL Server]"
  - "logs [SQL Server], minimal logging"
  - "minimally logged operations [SQL Server]"
  - "bulk importing [SQL Server], minimal logging"
---
# Prerequisites for Minimal Logging in Bulk Import
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  For a database under the full recovery model, all row-insert operations that are performed by bulk import are fully logged in the transaction log. Large data imports can cause the transaction log to fill rapidly if the full recovery model is used. In contrast, under the simple recovery model or bulk-logged recovery model, minimal logging of bulk-import operations reduces the possibility that a bulk-import operation will fill the log space. Minimal logging is also more efficient than full logging.  
  
> [!NOTE]  
>  The bulk-logged recovery model is designed to temporarily replace the full recovery model during large bulk operations.  
  
## Table Requirements for Minimally Logging Bulk-Import Operations  
 Minimal logging requires that the target table meets the following conditions:  
  
-   The table is not being replicated.  
  
-   Table locking is specified (using TABLOCK). 
  
    > [!NOTE]  
    >  Although data insertions are not logged in the transaction log during a minimally logged bulk-import operation, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] still logs extent allocations each time a new extent is allocated to the table.  
  
-   Table is not a memory-optimized table.  
  
 Whether minimal logging can occur for a table also depends on whether the table is indexed and, if so, whether the table is empty:  
  
-   If the table has no indexes, data pages are minimally logged.  
  
-   If the table has no clustered index but has one or more nonclustered indexes, data pages are always minimally logged. How index pages are logged, however, depends on whether the table is empty:  
  
    -   If the table is empty, index pages are minimally logged.  If you start with an empty table and bulk import the data in multiple batches, both index and data pages are minimally logged for the first batch, but beginning with the second batch, only data pages are minimally logged. 
  
    -   If table is non-empty, index pages are fully logged.    

-   If the table has a clustered index and is empty, both data and index pages are minimally logged. In contrast, if a table has a B-tree based clustered index and is non-empty, data pages and index pages are both fully logged regardless of the recovery model. If you start with an empty table  rowstore table and bulk import the data in batches, both index and data pages are minimally logged for the first batch, but from the second batch onwards, only data pages are bulk logged.

   [!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

- For information about logging for a clustered columnstore index (CCI), see [Columnstore index data loading guidance](../indexes/columnstore-indexes-data-loading-guidance.md#plan-bulk-load-sizes-to-minimize-delta-rowgroups).
  

  
> [!NOTE]  
>  When transactional replication is enabled, BULK INSERT operations are fully logged even under the Bulk Logged recovery model.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [View or Change the Recovery Model of a Database &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-or-change-the-recovery-model-of-a-database-sql-server.md)  
  
  
## See Also  
 [Recovery Models &#40;SQL Server&#41;](../../relational-databases/backup-restore/recovery-models-sql-server.md)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)  
  
  
