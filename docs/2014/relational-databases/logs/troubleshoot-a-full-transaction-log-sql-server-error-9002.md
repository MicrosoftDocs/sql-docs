---
title: "Troubleshoot a Full Transaction Log (SQL Server Error 9002) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
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
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Troubleshoot a Full Transaction Log (SQL Server Error 9002)
  This topic discusses possible responses to a full transaction log and suggests how to avoid it in the future. When the transaction log becomes full, [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] issues a 9002 error. The log can fill when the database is online or in recovery. If the log fills while the database is online, the database remains online but can only be read, not updated. If the log fills during recovery, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] marks the database as RESOURCE PENDING. In either case, user action is required to make log space available.  
  
## Responding to a Full Transaction Log  
 The appropriate response to a full transaction log depends partly on what condition or conditions caused the log to fill. To discover what is preventing log truncation in a given case, use the **log_reuse_wait** and **log_reuse_wait_desc** columns of the **sys.database** catalog view. For more information, see [sys.databases &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql). For descriptions of factors that can delay log truncation, see [The Transaction Log &#40;SQL Server&#41;](the-transaction-log-sql-server.md).  
  
> [!IMPORTANT]  
>  If the database was in recovery when the 9002 error occurred, after resolving the problem, recover the database by using ALTER DATABASE *database_name* SET ONLINE.  
  
 Alternatives for responding to a full transaction log include:  
  
-   Backing up the log.  
  
-   Freeing disk space so that the log can automatically grow.  
  
-   Moving the log file to a disk drive with sufficient space.  
  
-   Increasing the size of a log file.  
  
-   Adding a log file on a different disk.  
  
-   Completing or killing a long-running transaction.  
  
 These alternatives are discussed in the following sections. Choose a response that fits your situation best.  
  
### Backing up the Log  
 Under the full recovery model or bulk-logged recovery model, if the transaction log has not been backed up recently, backup might be what is preventing log truncation. If the log has never been backed up, you must create two log backups to permit the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to truncate the log to the point of the last backup. Truncating the log frees space for new log records. To keep the log from filling up again, take log backups frequently.  
  
 **To create a transaction log backup**  
  
> [!IMPORTANT]  
>  If the database is damaged, see [Tail-Log Backups &#40;SQL Server&#41;](../backup-restore/tail-log-backups-sql-server.md).  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](../backup-restore/back-up-a-transaction-log-sql-server.md)  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Backup.SqlBackup%2A> (SMO)  
  
### Freeing Disk Space  
 You might be able to free disk space on the disk drive that contains the transaction log file for the database by deleting or moving other files. The freed disk space allows the recovery system to enlarge the log file automatically.  
  
### Moving the Log File to a Different Disk  
 If you cannot free enough disk space on the drive that currently contains the log file, consider moving the file to another drive with sufficient space.  
  
> [!IMPORTANT]  
>  Log files should never be placed on compressed file systems.  
  
 **To move a log file**  
  
-   [Move Database Files](../databases/move-database-files.md)  
  
### Increasing the Size of a Log File  
 If space is available on the log disk, you can increase the size of the log file. The maximum size for log files is two terabytes (TB) per log file.  
  
 **To increase the file size**  
  
 If autogrow is disabled, the database is online, and sufficient space is available on the disk, either:  
  
-   Manually increase the file size to produce a single growth increment.  
  
-   Turn on autogrow by using the ALTER DATABASE statement to set a non-zero growth increment for the FILEGROWTH option.  
  
> [!NOTE]  
>  In either case, if the current size limit has been reached, increase the MAXSIZE value.  
  
### Adding a Log File on a Different Disk  
 Add a new log file to the database on a different disk that has sufficient space by using ALTER DATABASE <database_name> ADD LOG FILE.  
  
 **To add a log file**  
  
-   [Add Data or Log Files to a Database](../databases/add-data-or-log-files-to-a-database.md)  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)   
 [Manage the Size of the Transaction Log File](manage-the-size-of-the-transaction-log-file.md)   
 [Transaction Log Backups &#40;SQL Server&#41;](../backup-restore/transaction-log-backups-sql-server.md)   
 [sp_add_log_file_recover_suspect_db &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-add-log-file-recover-suspect-db-transact-sql)  
  
  
