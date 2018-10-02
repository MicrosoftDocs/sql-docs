---
title: "Transaction Log Backups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "backing up [SQL Server], transaction logs"
  - "transaction log backups [SQL Server], creating"
  - "log backups [SQL Server["
  - "transaction log backups [SQL Server], sequencing"
ms.assetid: f4a44a35-0f44-4a42-91d5-d73ac658a3b0
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Transaction Log Backups (SQL Server)
  This topic is relevant only for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases that are using the full or bulk-logged recovery models. This topic discusses backing up the transaction log of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
 Minimally, you must have created at least one full backup before you can create any log backups. After that, the transaction log can be backed up at any time unless the log is already being backed up. We recommend that you take log backups frequently, both to minimize work loss exposure and to truncate the transaction log. Typically, a database administrator creates a full database backup occasionally, such as weekly, and, optionally, creates a series of differential database backup at a shorter interval, such as daily. Independently of the database backups, the database administrator backs up the transaction log at frequent intervals, such as every 10 minutes. For a given type of backup, the optimal interval depends on factors such as the importance of the data, the size of the database, and the workload of the server.  
  
 **In this Topic:**  
  
-   [How a Sequence of Log Backups Works](#LogBackupSequence)  
  
-   [Recommendations](#Recommendations)  
  
-   [Related Tasks](#RelatedTasks)  
  
-   [Related Content](#RelatedContent)  
  
##  <a name="LogBackupSequence"></a> How a Sequence of Log Backups Works  
 The sequence of transaction log backups *log chain* is independent of data backups. For example, assume the following sequence of events.  
  
|Time|Event|  
|----------|-----------|  
|8:00 A.M.|Back up database.|  
|Noon|Back up transaction log.|  
|4:00 P.M.|Back up transaction log.|  
|6:00 P.M.|Back up database.|  
|8:00 P.M.|Back up transaction log.|  
  
 The transaction log backup created at 8:00 P.M. contains transaction log records from 4:00 P.M. through 8:00 P.M., spanning the time when the full database backup was created at 6:00 P.M. The sequence of transaction log backups is continuous from the initial full database backup created at 8:00 A.M. to the last transaction log backup created at 8:00 P.M. For information about how to apply these log backups, see the example in [Apply Transaction Log Backups &#40;SQL Server&#41;](transaction-log-backups-sql-server.md).  
  
##  <a name="Recommendations"></a> Recommendations  
  
-   If a transaction log is damaged, work that is performed since the most recent valid backup is lost. Therefore we strongly recommend that you put your log files on fault-tolerant storage.  
  
-   If a database is damaged or you are about to restore the database, we recommend that you create a [tail-log backup](tail-log-backups-sql-server.md) to enable you to restore the database to the current point in time.  
  
-   By default, every successful backup operation adds an entry in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and in the system event log. If back up the log very frequently, these success messages accumulate quickly, resulting in huge error logs that can make finding other messages difficult. In such cases you can suppress these log entries by using trace flag 3226 if none of your scripts depend on those entries. For more information, see [Trace Flags &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To create a transaction log backup**  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md)  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Backup.SqlBackup%2A> (SMO)  
  
 To schedule backup jobs, see [Use the Maintenance Plan Wizard](../maintenance-plans/use-the-maintenance-plan-wizard.md).  
  
##  <a name="RelatedContent"></a> Related Content  
 None.  
  
## See Also  
 [The Transaction Log &#40;SQL Server&#41;](../logs/the-transaction-log-sql-server.md)   
 [Back Up and Restore of SQL Server Databases](back-up-and-restore-of-sql-server-databases.md)   
 [Tail-Log Backups &#40;SQL Server&#41;](tail-log-backups-sql-server.md)   
 [Apply Transaction Log Backups &#40;SQL Server&#41;](transaction-log-backups-sql-server.md)  
  
  
