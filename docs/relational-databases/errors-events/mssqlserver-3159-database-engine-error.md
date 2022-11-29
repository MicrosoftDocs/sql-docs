---
description: "MSSQLSERVER_3159"
title: "MSSQLSERVER_3159 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3159 (Database Engine error)"
ms.assetid: c93c1003-0e3a-40aa-9873-44a0f5b8b57e
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_3159
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|3159|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LDDB_LOGNOTBACKEDUP|  
|Message Text|The tail of the log for the database "%ls" has not been backed up. Use BACKUP LOG WITH NORECOVERY to back up the log if it contains work that you do not want to lose. Use the WITH REPLACE or WITH STOPAT clause of the RESTORE statement to just overwrite the contents of the log.|  
  
## Explanation  
In most cases, under the full or bulk-logged recovery models, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires that you back up the tail of the log to capture the log records that have not yet been backed up. A log backup taken of the tail of the log just before a restore operation is called a tail-log backup.  
  
When you are recovering a database to the point of a failure, the tail-log backup is the last backup of interest in the recovery plan. If you cannot back up the tail of the log, you can recover a database only to the end of the last backup that was created before the failure.  
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] usually requires that you take a tail-log backup before you start to restore a database. The tail-log backup prevents work loss and keeps the log chain intact. However, not all restore scenarios require a tail-log backup. You do not have to have a tail-log backup if the recovery point is included in an earlier log backup, or if you are moving or replacing (overwriting) the database and do not need to restore it to a point of time after the most recent backup. Also, if the log files are damaged and a tail-log backup cannot be created, you must restore the database without using a tail-log backup. Any transactions committed after the latest log backup are lost. For more information, see "Restoring Without Using a Tail-Log Backup," later in this topic.  
  
> [!CAUTION]  
> REPLACE should be used rarely, and only after careful consideration.  
  
## User Action  
Take a tail-log backup, and retry the restore operation.  
  
If you cannot back up the tail of the log, use WITH STOPAT or WITH REPLACE in your RESTORE statements.  
  
## See Also  
[Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](~/relational-databases/backup-restore/restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)  
[Back Up the Transaction Log When the Database Is Damaged &#40;SQL Server&#41;](~/relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md)  
[Back Up a Transaction Log &#40;SQL Server&#41;](~/relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  
[Tail-Log Backups &#40;SQL Server&#41;](~/relational-databases/backup-restore/tail-log-backups-sql-server.md)  
  
