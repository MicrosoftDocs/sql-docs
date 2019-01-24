---
title: "File States | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "restoring file state [SQL Server]"
  - "verifying file states"
  - "current file states"
  - "verifying filegroup states"
  - "file states [SQL Server]"
  - "online file state"
  - "offline file state [SQL Server]"
  - "viewing filegroup states"
  - "viewing file states"
  - "suspect file state"
  - "recovering file state [SQL Server]"
  - "current filegroup state"
  - "recovery pending file state [SQL Server]"
  - "displaying file states"
  - "states [SQL Server], files"
  - "displaying filegroup states"
  - "defunct file state"
ms.assetid: b426474d-8954-4df0-b78b-887becfbe8d6
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# File States
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the state of a database file is maintained independently from the state of the database. A file is always in one specific state, such as ONLINE or OFFLINE. To view the current state of a file, use the [sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md) or [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md) catalog view. If the database is offline, the state of the files can be viewed from the [sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md) catalog view.  
  
 The state of the files in a filegroup determines the availability of the whole filegroup. For a filegroup to be available, all files within the filegroup must be online. To view the current state of a filegroup, use the [sys.filegroups](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md) catalog view. If a filegroup is offline and you try to access the filegroup by a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement, it will fail with an error. When the query optimizer builds query plans for SELECT statements, it avoids nonclustered indexes and indexed views that reside in offline filegroups, letting these statements to succeed. However, if the offline filegroup contains the heap or clustered index of the target table, the SELECT statements fail. Additionally, any INSERT, UPDATE, or DELETE statement that modifies a table with any index in an offline filegroup will fail.  
  
## File State Definitions  
 The following table defines the file states.  
  
|State|Definition|  
|-----------|----------------|  
|ONLINE|The file is available for all operations. Files in the primary filegroup are always online if the database itself is online. If a file in the primary filegroup is not online, the database is not online and the states of the secondary files are undefined.|  
|OFFLINE|The file is not available for access and may not be present on the disk. Files become offline by explicit user action and remain offline until additional user action is taken.<br /><br /> **\*\* Caution \*\*** A file state can be set offline when the file is corrupted, but it can be restored. A file set to offline can only be set online by restoring the file from backup. For more information about restoring a single file, see [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md). <br /><br /> A database file is also set OFFLINE when a file is dropped. The entry in sys.master_files persists until a transaction log is truncated past the drop_lsn value. For more information, see [Transaction Log Truncation](../../relational-databases/logs/the-transaction-log-sql-server.md#-transaction-log-truncation).|  
|RESTORING|The file is being restored. Files enter the restoring state because of a restore command affecting the whole file, not just a page restore, and remain in this state until the restore is completed and the file is recovered.|  
|RECOVERY PENDING|The recovery of the file has been postponed. A file enters this state automatically because of a piecemeal restore process in which the file is not restored and recovered. Additional action by the user is required to resolve the error and allow for the recovery process to be completed. For more information, see [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md).|  
|SUSPECT|Recovery of the file failed during an online restore process. If the file is in the primary filegroup, the database is also marked as suspect. Otherwise, only the file is suspect and the database is still online.<br /><br /> The file will remain in the suspect state until it is made available by one of the following methods:<br /><br /> Restore and recovery<br /><br /> DBCC CHECKDB with REPAIR_ALLOW_DATA_LOSS|  
|DEFUNCT|The file was dropped when it was not online. All files in a filegroup become defunct when an offline filegroup is removed.|  
  
## Related Content  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
  
 [Database States](../../relational-databases/databases/database-states.md)  
  
 [Mirroring States &#40;SQL Server&#41;](../../database-engine/database-mirroring/mirroring-states-sql-server.md)  
  
 [DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)  
  
 [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)  
  
 
