---
title: "Transaction Log Backups (SQL Server) | Microsoft Docs"
description: Independent of the database backups, you can back up the SQL Server transaction log frequently. The sequence of transaction log backups is a log chain.
ms.custom: ""
ms.date: "01/05/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "backing up [SQL Server], transaction logs"
  - "transaction log backups [SQL Server], creating"
  - "log backups [SQL Server]"
  - "transaction log backups [SQL Server], sequencing"
ms.assetid: f4a44a35-0f44-4a42-91d5-d73ac658a3b0
author: MashaMSFT
ms.author: mathoma
---
# Transaction Log Backups (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic is relevant only for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases that are using the full or bulk-logged recovery models. This topic discusses backing up the transaction log of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
 Minimally, you must have created at least one full backup before you can create any log backups. After that, the transaction log can be backed up at any time unless the log is already being backed up. 
 
We recommend you take log backups frequently, both to minimize work loss exposure and to truncate the transaction log. 
 
A database administrator typically creates a full database backup occasionally, such as weekly, and, optionally, creates a series of differential database backup at a shorter interval, such as daily. Independent of the database backups, the database administrator backs up the transaction log at frequent intervals. For a given type of backup, the optimal interval depends on factors such as the importance of the data, the size of the database, and the workload of the server. For more information about implementing a good strategy, see [Recommendations](#Recommendations) in this topic. 
   
##  <a name="LogBackupSequence"></a> How a sequence of log backups works  
 The sequence of transaction log backups *log chain* is independent of data backups. For example, assume the following sequence of events.  
  
|Time|Event|  
|----------|-----------|  
|8:00 AM|Back up database.|  
|Noon|Back up transaction log.|  
|4:00 PM|Back up transaction log.|  
|6:00 PM|Back up database.|  
|8:00 PM|Back up transaction log.|  
  
 The transaction log backup created at 8:00 PM contains transaction log records from 4:00 PM through 8:00 PM, spanning the time when the full database backup was created at 6:00 PM The sequence of transaction log backups is continuous from the initial full database backup created at 8:00 AM to the last transaction log backup created at 8:00 PM. For information about how to apply these log backups, see the example in [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md).  
  
##  <a name="Recommendations"></a> Recommendations  
  
-   If a transaction log is damaged, work that is performed since the most recent valid backup is lost. Therefore we strongly recommend that you put your log files on fault-tolerant storage.  
  
-   If a database is damaged or you are about to restore the database, we recommend that you create a [tail-log backup](../../relational-databases/backup-restore/tail-log-backups-sql-server.md) to enable you to restore the database to the current point in time.  
  
-   By default, every successful backup operation adds an entry in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and in the system event log. If back up the log very frequently, these success messages accumulate quickly, resulting in huge error logs that can make finding other messages difficult. In such cases you can suppress these log entries by using trace flag 3226 if none of your scripts depend on those entries. For more information, see [Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).  

-   Take frequent enough log backups to support your business requirements, specifically your tolerance for work loss such as might be caused by a damaged log storage. 
   -   The appropriate frequency for taking log backups depends on your tolerance for work-loss exposure balanced by how many log backups you can store, manage, and, potentially, restore. Think about the required [RTO](https://wikipedia.org/wiki/Recovery_time_objective) and [RPO](https://wikipedia.org/wiki/Recovery_point_objective) when implementing your recovery strategy, and specifically the log backup cadence.
   -   Taking a log backup every 15 to 30 minutes might be enough. If your business requires that you minimize work-loss exposure, consider taking log backups more frequently. More frequent log backups have the added advantage of increasing the frequency of log truncation, resulting in smaller log files.  
  
> [!IMPORTANT]
> To limit the number of log backups that you need to restore, it is essential to routinely back up your data. For example, you might schedule a weekly full database backup and daily differential database backups.  
> Again, think about the required [RTO](https://wikipedia.org/wiki/Recovery_time_objective) and [RPO](https://wikipedia.org/wiki/Recovery_point_objective) when implementing your recovery strategy, and specifically the full and differential database backup cadence.
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To create a transaction log backup**  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Backup.SqlBackup%2A> (SMO)  
  
 To schedule backup jobs, see [Use the Maintenance Plan Wizard](../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md).  
  

## See Also  
 [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)   
 [Transaction Log Backups in the SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Backups)     
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [Tail-Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/tail-log-backups-sql-server.md)   
 [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)  
  
  
