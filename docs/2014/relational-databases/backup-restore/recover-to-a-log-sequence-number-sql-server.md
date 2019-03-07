---
title: "Recover to a Log Sequence Number (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "log sequence numbers [SQL Server]"
  - "STOPBEFOREMARK option [RESTORE statement]"
  - "STOPATMARK option [RESTORE statement]"
  - "point in time recovery [SQL Server]"
  - "restoring databases [SQL Server], point in time"
  - "recovery [SQL Server], databases"
  - "restoring [SQL Server], point in time"
  - "LSNs"
  - "database recovery [SQL Server]"
  - "database restores [SQL Server], point in time"
ms.assetid: f7b3de5b-198d-448d-8c71-1cdd9239676c
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Recover to a Log Sequence Number (SQL Server)
  This topic is relevant only for databases that are using the full or bulk-logged recovery models.  
  
 You can use a log sequence number (LSN) to define the recovery point for a restore operation. However, this is a specialized feature that is intended for tools vendors and is unlikely to be generally useful.  
  
##  <a name="LSNs"></a> Overview of Log Sequence Numbers  
 LSNs are used internally during a RESTORE sequence to track the point in time to which data has been restored. When a backup is restored, the data is restored to the LSN corresponding to the point in time at which the backup was taken. Differential and log backups advance the restored database to a later time, which corresponds to a higher LSN.  
  
 Every record in the transaction log is uniquely identified by a log sequence number (LSN). LSNs are ordered such that if LSN2 is greater than LSN1, the change described by the log record referred to by LSN2 occurred after the change described by the log record LSN.  
  
 The LSN of a log record at which a significant event occurred can be useful for constructing correct restore sequences. Because LSNs are ordered, they can be compared for equality and inequality (that is, **\<**, **>**, **=**, **\<=**, **>=**). Such comparisons are useful when constructing restore sequences.  
  
> [!NOTE]  
>  LSNs are values of data type `numeric`(25,0). Arithmetic operations (for example, addition or subtraction) are not meaningful and must not be used with LSNs.  
  

  
## Viewing LSNs Used by Backup and Restore  
 The LSN of a log record at which a given backup and restore event occurred is viewable using one or more of the following:  
  
-   [backupset](/sql/relational-databases/system-tables/backupset-transact-sql)  
  
-   [backupfile](/sql/relational-databases/system-tables/backupfile-transact-sql)  
  
-   [sys.database_files](/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql); [sys.master_files](/sql/relational-databases/system-catalog-views/sys-master-files-transact-sql)  
  
-   [RESTORE HEADERONLY](/sql/t-sql/statements/restore-statements-headeronly-transact-sql)  
  
-   [RESTORE FILELISTONLY](/sql/t-sql/statements/restore-statements-filelistonly-transact-sql)  
  
> [!NOTE]  
>  LSNs also appear in some message texts.  
  
## Transact-SQL Syntax for Restoring to an LSN  
 By using a [RESTORE](/sql/t-sql/statements/restore-statements-transact-sql) statement, you can stop at or immediately before the LSN, as follows:  
  
-   Use the WITH STOPATMARK **='**lsn:_<lsn_number>_**'** clause, where lsn:*\<lsnNumber>* is a string that specifies that the log record that contains the specified LSN is the recovery point.  
  
     STOPATMARK roll forwards to the LSN and includes that log record in the roll forward.  
  
-   Use the WITH STOPBEFOREMARK **='**lsn:_<lsn_number>_**'** clause, where lsn:*\<lsnNumber>* is a string that specifies that the log record immediately before the log record that contains the specified LSN number is the recovery point.  
  
     STOPBEFOREMARK rolls forward to the LSN and excludes that log record from the roll forward.  
  
 Typically, a specific transaction is selected to be included or excluded. Although not required, in practice, the specified log record is a transaction-commit record.  
  
## Examples  
 The following example assumes that the `AdventureWorks` database has been changed to use the full recovery model.  
  
```  
RESTORE LOG AdventureWorks FROM DISK = 'c:\adventureworks_log.bak'   
WITH STOPATMARK = 'lsn:15000000040000037'  
GO  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Restore a Database Backup &#40;SQL Server Management Studio&#41;](restore-a-database-backup-using-ssms.md)  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md)  
  
-   [Restore a Transaction Log Backup &#40;SQL Server&#41;](restore-a-transaction-log-backup-sql-server.md)  
  
-   [Restore a Database to the Point of Failure Under the Full Recovery Model &#40;Transact-SQL&#41;](restore-database-to-point-of-failure-full-recovery.md)  
  
-   [Restore a Database to a Marked Transaction &#40;SQL Server Management Studio&#41;](restore-a-database-to-a-marked-transaction-sql-server-management-studio.md)  
  
-   [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)  
  
## See Also  
 [Apply Transaction Log Backups &#40;SQL Server&#41;](transaction-log-backups-sql-server.md)   
 [The Transaction Log &#40;SQL Server&#41;](../logs/the-transaction-log-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)  
  
  
