---
title: "MSSQL_ENG003165 | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG003165 error"
ms.assetid: 707d33dd-644e-4cc9-ac51-dddd49031530
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# MSSQL_ENG003165
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|3165|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Database '%ls' was restored; however, an error was encountered while replication was being restored/removed. The database has been left offline. See the topic MSSQL_ENG003165 in SQL Server Books Online.|  
  
## Explanation  
 This error is raised if a problem occurs restoring a backup of a replicated database:  
  
-   If the backup is being restored to the same database and server on which it was taken, the error indicates that replication settings could not be restored properly.  
  
-   If the backup is being restored to a different database or server, the error indicates that replication settings could not be removed properly (by default, replication settings are removed if the database or server is different).  
  
 The error is probably the result of a mismatch between the state of the restored database and one or more system databases that contain replication metadata: **msdb**, **master**, or the distribution database.  
  
## User Action  
 To resolve this issue:  
  
1.  Execute ALTER DATABASE to bring the database online; for example: `ALTER DATABASE AdventureWorks SET ONLINE`. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md). If you want to preserve replication settings, go to step 2. If not, go to step 3.  
  
2.  Execute [sp_restoredbreplication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-restoredbreplication-transact-sql.md). If this stored procedure executes successfully, the restore is complete. If it does not execute successfully, go to step 3.  
  
3.  Execute [sp_removedbreplication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md) to remove all replication settings.  
  
     Reconfigure replication if necessary. If you have scripted the replication topology as recommended, use scripts to reconfigure the topology.  
  
## See Also  
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [Back Up and Restore Replicated Databases](../../relational-databases/replication/administration/back-up-and-restore-replicated-databases.md)   
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
