---
title: "Recover to a Log Sequence Number (SQL Server) | Microsoft Docs"
description: In SQL Server, you can use the log sequence number (LSN) to recover to a certain point. This feature is intended for tools vendors.
ms.custom: ""
ms.date: "10/23/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
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
author: MashaMSFT
ms.author: mathoma
---
# Recover to a Log Sequence Number (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic is relevant only for databases that are using the full or bulk-logged recovery models.  
  
 You can use a log sequence number (LSN) to define the recovery point for a restore operation. However, this is a specialized feature that is intended for tools vendors and is unlikely to be generally useful.  
  
##  <a name="LSNs"></a> Overview of Log Sequence Numbers  
 LSNs are used internally during a RESTORE sequence to track the point in time to which data has been restored. When a backup is restored, the data is restored to the LSN corresponding to the point in time at which the backup was taken. Differential and log backups advance the restored database to a later time, which corresponds to a higher LSN. For more information about LSNs, see the [SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch).  
  
> [!NOTE]  
> LSNs are values of data type **numeric(25,0)**. Arithmetic operations (for example, addition or subtraction) are not meaningful and must not be used with LSNs.  
 
## Viewing LSNs used by Backup and Restore  
 The LSN of a log record at which a given backup and restore event occurred is viewable using one or more of the following:  
  
-   [backupset](../../relational-databases/system-tables/backupset-transact-sql.md)  
  
-   [backupfile](../../relational-databases/system-tables/backupfile-transact-sql.md)  
  
-   [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md); [sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)  
  
-   [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)  
  
-   [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)  
  
> [!NOTE]  
>  LSNs also appear in some messages in the error log.  
  
## Transact-SQL Syntax for restoring to an LSN  
 By using a [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) statement, you can stop at or immediately before the LSN, as follows:  
  
-   Use the WITH STOPATMARK **='**lsn:_<lsn_number>_**'** clause, where lsn:*\<lsnNumber>* is a string that specifies that the log record that contains the specified LSN is the recovery point.  
  
     STOPATMARK roll forwards to the LSN and includes that log record in the roll forward.  
  
-   Use the WITH STOPBEFOREMARK **='**lsn:_<lsn_number>_**'** clause, where lsn:*\<lsnNumber>* is a string that specifies that the log record immediately before the log record that contains the specified LSN number is the recovery point.  
  
     STOPBEFOREMARK rolls forward to the LSN and excludes that log record from the roll forward.  
  
 Typically, a specific transaction is selected to be included or excluded. Although not required, in practice, the specified log record is a transaction-commit record.  
  
## Examples  
 The following example assumes that the `AdventureWorks` database has been changed to use the full recovery model.  
  
```sql  
RESTORE LOG AdventureWorks FROM DISK = 'c:\adventureworks_log.bak'   
WITH STOPATMARK = 'lsn:15000000040000037'  
GO  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Restore a Database Backup Using SSMS](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md)  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  
  
-   [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md)  
  
-   [Restore a Database to the Point of Failure Under the Full Recovery Model &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/restore-database-to-point-of-failure-full-recovery.md)  
  
-   [Restore a Database to a Marked Transaction &#40;SQL Server Management Studio&#41;](../../relational-databases/backup-restore/restore-a-database-to-a-marked-transaction-sql-server-management-studio.md)  
  
-   [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)  
  
## See Also  
 [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)   
 [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)     
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)     
 [Restore and Recovery Overview (SQL Server)](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md#TlogAndRecovery)       
 [SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md)      
  
