---
description: "sp_create_removable (Transact-SQL)"
title: "sp_create_removable (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_create_removable"
  - "sp_create_removable_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_create_removable"
ms.assetid: 06e36ae5-f70d-4a26-9a7f-ee4b9360b355
author: markingmyname
ms.author: maghan
---
# sp_create_removable (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates a removable media database. Creates three or more files (one for the system catalog tables, one for the transaction log, and one or more for the data tables) and places the database on those files.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_create_removable   
   [ @dbname = ] 'dbname',   
   [ @syslogical= ] 'syslogical',   
   [ @sysphysical = ] 'sysphysical',   
   [ @syssize = ] syssize,   
   [ @loglogical = ] 'loglogical',   
   [ @logphysical = ] 'logphysical',   
   [ @logsize = ] logsize,   
   [ @datalogical1 = ] 'datalogical1',   
   [ @dataphysical1 = ] 'dataphysical1',   
   [ @datasize1 = ] datasize1 ,   
   [ @datalogical16 = ] 'datalogical16',   
   [ @dataphysical16 = ] 'dataphysical16',   
   [ @datasize16 = ] datasize16 ]  
```  
  
## Arguments  
`[ @dbname = ] 'dbname'`
 Is the name of the database to create for use on removable media. *dbname* is **sysname**.  
  
`[ @syslogical = ] 'syslogical'`
 Is the logical name of the file that contains the system catalog tables. *syslogical* is **sysname**.  
  
`[ @sysphysical = ] 'sysphysical'`
 Is the physical name. This includes a fully qualified path, of the file that holds the system catalog tables. *sysphysical* is **nvarchar(260)**.  
  
`[ @syssize = ] syssize`
 Is the size, in megabytes, of the file that holds the system catalog tables. *syssize* is **int**. The minimum *syssize* is 1.  
  
`[ @loglogical = ] 'loglogical'`
 Is the logical name of the file that contains the transaction log. *loglogical* is **sysname**.  
  
`[ @logphysical = ] 'logphysical'`
 Is the physical name. This includes a fully qualified path, of the file that contains the transaction log. *logphysical* is **nvarchar(260)**.  
  
`[ @logsize = ] logsize`
 Is the size, in megabytes, of the file that contains the transaction log. *logsize* is **int**. The minimum *logsize* is 1.  
  
`[ @datalogical1 = ] 'datalogical'`
 Is the logical name of a file that contains the data tables. *datalogical* is **sysname**.  
  
 There must be from 1 through 16 data files. Typically, more than one data file is created when the database is expected to be large and must be distributed on multiple disks.  
  
`[ @dataphysical1 = ] 'dataphysical'`
 Is the physical name. This includes a fully qualified path, of a file that contains data tables. *dataphysical* is **nvarchar(260)**.  
  
`[ @datasize1 = ] 'datasize'`
 Is the size, in megabytes, of a file that contains data tables. *datasize* is **int**. The minimum *datasize* is 1.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 If you want to make a copy of your database on removable media, such as a compact disc, and distribute the database to other users, use this stored procedure.  
  
## Permissions  
 Requires CREATE DATABASE, CREATE ANY DATABASE, or ALTER ANY DATABASE permission.  
  
 To maintain control over disk use on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], permission to create databases is typically limited to a few login accounts.  
  
### Permissions on Data and Log Files  
 Whenever certain operations are performed on a database, corresponding permissions are set on its data and log files. The permissions prevent the files from being accidentally tampered with if they reside in a directory that has open permissions.  
  
|Operation on Database|Permissions Set on Files|  
|---------------------------|------------------------------|  
|Modified to add a new file|Created|  
|Backed up|Attached|  
|Restored|Detached|  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not set data and log file permissions.  
  
## Examples  
 The following example creates the database `inventory` as a removable database.  
  
```  
EXEC sp_create_removable 'inventory',   
   'invsys',  
   'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\invsys.mdf'  
, 2,   
   'invlog',  
   'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\invlog.ldf', 4,  
   'invdata',  
   'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\invdata.ndf',   
10;  
```  
  
## See Also  
 [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)   
 [sp_certify_removable &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-certify-removable-transact-sql.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [sp_dbremove &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbremove-transact-sql.md)   
 [sp_detach_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-detach-db-transact-sql.md)   
 [sp_helpfile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpfile-transact-sql.md)   
 [sp_helpfilegroup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpfilegroup-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
