---
description: "sp_lock (Transact-SQL)"
title: "sp_lock (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_lock_TSQL"
  - "sp_lock"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_lock"
ms.assetid: 9eaa0ec2-2ad9-457c-ae48-8da92a03dcb0
author: markingmyname
ms.author: maghan
---
# sp_lock (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Reports information about locks.  
  
> [!IMPORTANT]  
> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] To obtain information about locks in the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], use the [sys.dm_tran_locks](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md) dynamic management view.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_lock [ [ @spid1 = ] 'session ID1' ] [ , [@spid2 = ] 'session ID2' ]  
[ ; ]  
```  
  
## Arguments  
`[ @spid1 = ] 'session ID1'`
 Is a [!INCLUDE[ssDE](../../includes/ssde-md.md)] session ID  number from **sys.dm_exec_sessions** for which the user wants locking information. *session ID1* is **int** with a default value of NULL. Execute **sp_who** to obtain process information about the session. If *session ID1* is not specified, information about all locks is displayed.  
  
`[ @spid2 = ] 'session ID2'`
 Is another [!INCLUDE[ssDE](../../includes/ssde-md.md)] session ID number from **sys.dm_exec_sessions** that might have a lock at the same time as *session ID1* and about which the user also wants information. *session ID2* is **int** with a default value of NULL.  
  
## Return Code Values  
 0 (success)  
  
## Result Sets  
 The **sp_lock** result set contains one row for each lock held by the sessions specified in the **\@spid1** and **\@spid2** parameters. If neither **\@spid1** nor **\@spid2** is specified, the result set reports the locks for all sessions currently active in the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**spid**|**smallint**|The [!INCLUDE[ssDE](../../includes/ssde-md.md)] session ID number for the process requesting the lock.|  
|**dbid**|**smallint**|The identification number of the database in which the lock is held. You can use the DB_NAME() function to identify the database.|  
|**ObjId**|**int**|The identification number of the object on which the lock is held. You can use the OBJECT_NAME() function in the related database to identify the object. A value of 99 is a special case that indicates a lock on one of the system pages used to record the allocation of pages in a database.|  
|**IndId**|**smallint**|The identification number of the index on which the lock is held.|  
|**Type**|**nchar(4)**|The lock type:<br /><br /> RID = Lock on a single row in a table identified by a row identifier (RID).<br /><br /> KEY = Lock within an index that protects a range of keys in serializable transactions.<br /><br /> PAG = Lock on a data or index page.<br /><br /> EXT = Lock on an extent.<br /><br /> TAB = Lock on an entire table, including all data and indexes.<br /><br /> DB = Lock on a database.<br /><br /> FIL = Lock on a database file.<br /><br /> APP = Lock on an application-specified resource.<br /><br /> MD = Locks on metadata, or catalog information.<br /><br /> HBT = Lock on a heap or B-Tree (HoBT). This information is incomplete in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> AU = Lock on an allocation unit. This information is incomplete in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**Resource**|**nchar(32)**|The value identifying the resource that is locked. The format of the value depends on the type of resource identified in the **Type** column:<br /><br /> **Type** Value: **Resource** Value<br /><br /> RID: An identifier in the format fileid:pagenumber:rid, where fileid identifies the file containing the page, pagenumber identifies the page containing the row, and rid identifies the specific row on the page. fileid matches the **file_id** column in the **sys.database_files** catalog view.<br /><br /> KEY: A hexadecimal number used internally by the [!INCLUDE[ssDE](../../includes/ssde-md.md)].<br /><br /> PAG: A number in the format fileid:pagenumber, where fileid identifies the file containing the page, and pagenumber identifies the page.<br /><br /> EXT: A number identifying the first page in the extent. The number is in the format fileid:pagenumber.<br /><br /> TAB: No information provided because the table is already identified in the **ObjId** column.<br /><br /> DB: No information provided because the database is already identified in the **dbid** column.<br /><br /> FIL: The identifier of the file, which matches the **file_id** column in the **sys.database_files** catalog view.<br /><br /> APP: An identifier unique to the application resource being locked. In the format DbPrincipalId:\<first two to 16 characters of the resource string>\<hashed value>.<br /><br /> MD: varies by resource type. For more information, see the description of the **resource_description** column in [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).<br /><br /> HBT: No information provided. Use the **sys.dm_tran_locks** dynamic management view instead.<br /><br /> AU: No information provided. Use the **sys.dm_tran_locks** dynamic management view instead.|  
|**Mode**|**nvarchar(8)**|The lock mode requested. Can be:<br /><br /> NULL = No access is granted to the resource. Serves as a placeholder.<br /><br /> Sch-S = Schema stability. Ensures that a schema element, such as a table or index, is not dropped while any session holds a schema stability lock on the schema element.<br /><br /> Sch-M = Schema modification. Must be held by any session that wants to change the schema of the specified resource. Ensures that no other sessions are referencing the indicated object.<br /><br /> S = Shared. The holding session is granted shared access to the resource.<br /><br /> U = Update. Indicates an update lock acquired on resources that may eventually be updated. It is used to prevent a common form of deadlock that occurs when multiple sessions lock resources for potential update at a later time.<br /><br /> X = Exclusive. The holding session is granted exclusive access to the resource.<br /><br /> IS = Intent Shared. Indicates the intention to place S locks on some subordinate resource in the lock hierarchy.<br /><br /> IU = Intent Update. Indicates the intention to place U locks on some subordinate resource in the lock hierarchy.<br /><br /> IX = Intent Exclusive. Indicates the intention to place X locks on some subordinate resource in the lock hierarchy.<br /><br /> SIU = Shared Intent Update. Indicates shared access to a resource with the intent of acquiring update locks on subordinate resources in the lock hierarchy.<br /><br /> SIX = Shared Intent Exclusive. Indicates shared access to a resource with the intent of acquiring exclusive locks on subordinate resources in the lock hierarchy.<br /><br /> UIX = Update Intent Exclusive. Indicates an update lock hold on a resource with the intent of acquiring exclusive locks on subordinate resources in the lock hierarchy.<br /><br /> BU = Bulk Update. Used by bulk operations.<br /><br /> RangeS_S = Shared Key-Range and Shared Resource lock. Indicates serializable range scan.<br /><br /> RangeS_U = Shared Key-Range and Update Resource lock. Indicates serializable update scan.<br /><br /> RangeI_N = Insert Key-Range and Null Resource lock. Used to test ranges before inserting a new key into an index.<br /><br /> RangeI_S = Key-Range Conversion lock. Created by an overlap of RangeI_N and S locks.<br /><br /> RangeI_U = Key-Range Conversion lock created by an overlap of RangeI_N and U locks.<br /><br /> RangeI_X = Key-Range Conversion lock created by an overlap of RangeI_N and X locks.<br /><br /> RangeX_S = Key-Range Conversion lock created by an overlap of RangeI_N and RangeS_S. locks.<br /><br /> RangeX_U = Key-Range Conversion lock created by an overlap of RangeI_N and RangeS_U locks.<br /><br /> RangeX_X = Exclusive Key-Range and Exclusive Resource lock. This is a conversion lock used when updating a key in a range.|  
|**Status**|**nvarchar(5)**|The lock request status:<br /><br /> CNVRT: The lock is being converted from another mode, but the conversion is blocked by another process holding a lock with a conflicting mode.<br /><br /> GRANT: The lock was obtained.<br /><br /> WAIT: The lock is blocked by another process holding a lock with a conflicting mode.|  
  
## Remarks  
 Users can control the locking of read operations by:  
  
-   Using SET TRANSACTION ISOLATION LEVEL to specify the level of locking for a session. For syntax and restrictions, see [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md).  
  
-   Using locking table hints to specify the level of locking for an individual reference of a table in a FROM clause. For syntax and restrictions, see [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md).  
  
 All distributed transactions not associated with a session are orphaned transactions. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] assigns all orphaned distributed transactions the SPID value of -2, which makes it easier for a user to identify blocking distributed transactions. For more information, see [Use Marked Transactions to Recover Related Databases Consistently &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md).  
  
## Permissions  
 Requires VIEW SERVER STATE permission.  
  
## Examples  
  
### A. Listing all locks  
 The following example displays information about all locks currently held in an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
```sql  
USE master;  
GO  
EXEC sp_lock;  
GO  
```  
  
### B. Listing a lock from a single-server process  
 The following example displays information, including locks, about process ID `53`.  
  
```sql  
USE master;  
GO  
EXEC sp_lock 53;  
GO  
```  
  
## See Also  
 [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md)   
 [DB_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/db-name-transact-sql.md)   
 [KILL &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-transact-sql.md)   
 [OBJECT_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/object-name-transact-sql.md)   
 [sp_who &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sys.dm_os_tasks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md)   
 [sys.dm_os_threads &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-threads-transact-sql.md)  
  
  
