---
title: "Prerequisites for Minimal Logging in Bulk Import | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "minimal logging [SQL Server]"
  - "logged bulk copy [SQL Server]"
  - "logs [SQL Server], minimal logging"
  - "minimally logged operations [SQL Server]"
  - "bulk importing [SQL Server], minimal logging"
ms.assetid: bd1dac6b-6ef8-4735-ad4e-67bb42dc4f66
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Prerequisites for Minimal Logging in Bulk Import
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
  
    -   If the table is empty, index pages are minimally logged.  
  
    -   If table is non-empty, index pages are fully logged.  
  
        > [!NOTE]  
        >  If you start with an empty table and bulk import the data in multiple batches, both index and data pages are minimally logged for the first batch, but beginning with the second batch, only data pages are minimally logged.  
  
-   If the table has a clustered index and is empty, both data and index pages are minimally logged. In contrast, if a table has a clustered index and is non-empty, data pages and index pages are both fully logged regardless of the recovery model.  
  
    > [!NOTE]  
    >  If you start with an empty table and bulk import the data in batches, both index and data pages are minimally logged for the first batch, but from the second batch onwards, only data pages are bulk logged.  
  
> [!NOTE]  
>  When transactional replication is enabled, BULK INSERT operations are fully logged even under the Bulk Logged recovery model.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [View or Change the Recovery Model of a Database &#40;SQL Server&#41;](../backup-restore/view-or-change-the-recovery-model-of-a-database-sql-server.md)  
  

  
## See Also  
 [Recovery Models &#40;SQL Server&#41;](../backup-restore/recovery-models-sql-server.md)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql)   
 [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql)   
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)   
 [Table Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-table)   
 [INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/insert-transact-sql)  
  
  
