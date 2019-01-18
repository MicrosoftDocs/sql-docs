---
title: "Troubleshoot a Full Transaction Log (SQL Server Error 9002) | Microsoft Docs"
ms.custom: ""
ms.date: "08/05/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [SQL Server], full"
  - "troubleshooting [SQL Server], full transaction log"
  - "9002 (Database Engine error)"
  - "transaction logs [SQL Server], truncation"
  - "backing up transaction logs [SQL Server], full logs"
  - "transaction logs [SQL Server], full log"
  - "full transaction logs [SQL Server]"
ms.assetid: 0f23aa84-475d-40df-bed3-c923f8c1b520
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Troubleshoot a Full Transaction Log (SQL Server Error 9002)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic discusses possible responses to a full transaction log and suggests how to avoid it in the future. 
  
  When the transaction log becomes full, [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] issues a **9002 error**. The log can fill when the database is online, or in recovery. If the log fills while the database is online, the database remains online but can only be read, not updated. If the log fills during recovery, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] marks the database as RESOURCE PENDING. In either case, user action is required to make log space available.  
  
## Responding to a full transaction log  
 The appropriate response to a full transaction log depends partly on what condition or conditions caused the log to fill. 
 
 To discover what is preventing log truncation in a given case, use the **log_reuse_wait** and **log_reuse_wait_desc** columns of the **sys.database** catalog view. For more information, see [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md). For descriptions of factors that can delay log truncation, see [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md).  
  
> **IMPORTANT!!**  
>  If the database was in recovery when the 9002 error occurred, after resolving the problem, recover the database by using [ALTER DATABASE *database_name* SET ONLINE.](../../t-sql/statements/alter-database-transact-sql-set-options.md)  
  
 Alternatives for responding to a full transaction log include:  
  
-   Backing up the log.  
  
-   Freeing disk space so that the log can automatically grow.  
  
-   Moving the log file to a disk drive with sufficient space.  
  
-   Increasing the size of a log file.  
  
-   Adding a log file on a different disk.  
  
-   Completing or killing a long-running transaction.  
  
 These alternatives are discussed in the following sections. Choose a response that fits your situation best.  
  
## Back up the log  
 Under the full recovery model or bulk-logged recovery model, if the transaction log has not been backed up recently, backup might be what is preventing log truncation. If the log has never been backed up, you **must create two log backups** to permit the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to truncate the log to the point of the last backup. Truncating the log frees space for new log records. To keep the log from filling up again, take log backups frequently.  
  
 **To create a transaction log backup**  
  
> **IMPORTANT**  
>  If the database is damaged, see [Tail-Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Backup.SqlBackup%2A> (SMO)  
  
### Freeing disk space  
 You might be able to free disk space on the disk drive that contains the transaction log file for the database by deleting or moving other files. The freed disk space allows the recovery system to enlarge the log file automatically.  
  
### Move the log file to a different disk  
 If you cannot free enough disk space on the drive that currently contains the log file, consider moving the file to another drive with sufficient space.  
  
> **IMPORTANT!!** Log files should never be placed on compressed file systems.  
  
 **Move a log file**  
  
-   [Move Database Files](../../relational-databases/databases/move-database-files.md)  
  
### Increase log file size  
 If space is available on the log disk, you can increase the size of the log file. The maximum size for log files is two terabytes (TB) per log file.  
  
 **Increase the file size**  
  
 If autogrow is disabled, the database is online, and sufficient space is available on the disk, either:  
  
-   Manually increase the file size to produce a single growth increment.  
  
-   Turn on autogrow by using the ALTER DATABASE statement to set a non-zero growth increment for the FILEGROWTH option.  
  
> **NOTE** In either case, if the current size limit has been reached, increase the MAXSIZE value.  
  
### Add a log file on a different disk  
 Add a new log file to the database on a different disk that has sufficient space by using ALTER DATABASE <database_name> ADD LOG FILE.  
  
 **Add a log file**  
  
-   [Add Data or Log Files to a Database](../../relational-databases/databases/add-data-or-log-files-to-a-database.md)  
## Complete or kill a long-running transaction
### Discovering long-running transactions
A very long-running transaction can cause the transaction log to fill. To look for long-running transactions, use one of the following:
 - **[sys.dm_tran_database_transactions](../system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md).**
This dynamic management view returns information about transactions at the database level. For a long-running transaction, columns of particular interest include the time of the first log record [(database_transaction_begin_time)](../system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md), the current state of the transaction [(database_transaction_state)](../system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md), and the [log sequence number (LSN)](../backup-restore/recover-to-a-log-sequence-number-sql-server.md) of the begin record in the transaction log [(database_transaction_begin_lsn)](../system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md).

 - **[DBCC OPENTRAN](../../t-sql/database-console-commands/dbcc-opentran-transact-sql.md).**
This statement lets you identify the user ID of the owner of the transaction, so you can potentially track down the source of the transaction for a more orderly termination (committing it rather than rolling it back).

### Kill a transaction
Sometimes you just have to end the process; you may have to use the [KILL](../../t-sql/language-elements/kill-transact-sql.md) statement. Please use this statement very carefully,  especially when critical processes are running that you don't want to kill. For more information, see [KILL (Transact-SQL)](../../t-sql/language-elements/kill-transact-sql.md)

## See also  
[KB support article - A transaction log grows unexpectedly or becomes full in SQL Server](https://support.microsoft.com/kb/317375)
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [Manage the Size of the Transaction Log File](../../relational-databases/logs/manage-the-size-of-the-transaction-log-file.md)   
 [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md)   
 [sp_add_log_file_recover_suspect_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-log-file-recover-suspect-db-transact-sql.md)  
  
  
