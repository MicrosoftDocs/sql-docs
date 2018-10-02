---
title: "Prepare to Bulk Import Data (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "bulk importing [SQL Server], about bulk importing"
  - "BULK INSERT statement, guidelines"
  - "BULK INSERT statement, restrictions"
  - "bcp utility [SQL Server], guidelines"
  - "bcp utility [SQL Server], restrictions"
  - "hidden characters"
  - "OPENROWSET function, BCP guidelines"
ms.assetid: a82ef43c-d006-4c71-bfca-f001a3ba1ba0
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Prepare to Bulk Import Data (SQL Server)
  You can use the **bcp** command, BULK INSERT statement, or OPENROWSET(BULK) function to bulk import data from a data file only.  
  
> [!NOTE]  
>  It is possible to write a custom application that bulk imports data from objects other than a text file. To bulk import data from memory buffers, use either the bcp extensions to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client (ODBC) application programming interface (API) or the OLE DB **IRowsetFastLoad** interface.  To bulk import data from a C# data table, use the ADO.NET bulk-copy API, **SqlBulkCopy**.  
  
> [!NOTE]  
>  Bulk importing data into a remote table is not supported.  
  
 Use the following guidelines when you bulk import data from a data file to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   Obtain required permissions for your user account.  
  
     The user account in which you use the **bcp** utility, the BULK INSERT statement, or the INSERT ... SELECT * FROM OPENROWSET(BULK...) statement must have the required permissions on the table, which are assigned by the table owner. For more information about permissions that are required by each method, see [bcp Utility](../../tools/bcp-utility.md), [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql), and [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql).  
  
-   Use the bulk-logged recovery model.  
  
     This guideline is for a database that uses the full recovery model. The bulk-logged recovery model is useful when performing bulk operations into an unindexed table (a *heap*). Using bulk-logged recovery helps prevent the transaction log from running out of space because bulk-logged recovery does not perform log row inserts. For more information about the bulk-logged recovery model, see [Recovery Models &#40;SQL Server&#41;](../backup-restore/recovery-models-sql-server.md).  
  
     We recommend that you change the database to use the bulk-logged recovery model immediately before the bulk import operation. Immediately afterwards, you should reset the database to the full recovery model. For more information, see [View or Change the Recovery Model of a Database &#40;SQL Server&#41;](../backup-restore/view-or-change-the-recovery-model-of-a-database-sql-server.md).  
  
    > [!NOTE]  
    >  more information about how to minimize logging during bulk import operations, see [Prerequisites for Minimal Logging in Bulk Import](prerequisites-for-minimal-logging-in-bulk-import.md).  
  
-   Back up after bulk importing data.  
  
     For a database that uses the simple recovery model, we recommend that you take a full or differential backup after the bulk-import operation finishes. For more information, see [Create a Full Database Backup &#40;SQL Server&#41;](../backup-restore/create-a-full-database-backup-sql-server.md) or [Create a Differential Database Backup &#40;SQL Server&#41;](../backup-restore/create-a-differential-database-backup-sql-server.md).  
  
     For the bulk-logged recovery model or full recovery model, a log backup is enough. For more information, see [Transaction Log Backups &#40;SQL Server&#41;](../backup-restore/transaction-log-backups-sql-server.md).  
  
-   Drop table indexes to improve performance for large bulk imports.  
  
     This guideline is for when you are importing a large amount of data compared to the amount of data that is already in the table. In this case, dropping the indexes from the table before you perform the bulk-import operation can significantly increase performance.  
  
    > [!NOTE]  
    >  If you are loading a small amount of data compared to the amount of data already in the table, dropping the indexes is counterproductive. The time required to rebuild the indexes might be longer than the time saved during the bulk-import operation.  
  
-   Find and remove hidden characters in the data file.  
  
     Many utilities and text editors display hidden characters, which are usually at the end of the data file. During a bulk-import operation, hidden characters in an ASCII data file can cause problems that cause an error of "unexpected null found". Finding and removing all the hidden characters should help prevent this problem.  
  
## See Also  
 [Import and Export Bulk Data by Using the bcp Utility &#40;SQL Server&#41;](import-and-export-bulk-data-by-using-the-bcp-utility-sql-server.md)   
 [Import Bulk Data by Using BULK INSERT or OPENROWSET&#40;BULK...&#41; &#40;SQL Server&#41;](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql)   
 [Data Formats for Bulk Import or Bulk Export &#40;SQL Server&#41;](data-formats-for-bulk-import-or-bulk-export-sql-server.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql)  
  
  
