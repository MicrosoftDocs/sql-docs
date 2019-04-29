---
title: "sp_helpfile (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpfile"
  - "sp_helpfile_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helpfile"
ms.assetid: 1546e0ae-5a99-4e01-9eb9-d147fa65884c
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpfile (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the physical names and attributes of files associated with the current database. Use this stored procedure to determine the names of files to attach to or detach from the server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpfile [ [ @filename= ] 'name' ]  
```  
  
## Arguments  
`[ @filename = ] 'name'`
 Is the logical name of any file in the current database. *name* is **sysname**, with a default of NULL. If *name* is not specified, the attributes of all files in the current database are returned.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Logical file name.|  
|**fileid**|**smallint**|Numeric identifier of the file. Is not returned if *name* is specified*.*|  
|**filename**|**nchar(260)**|Physical file name.|  
|**filegroup**|**sysname**|Filegroup in which the file belongs.<br /><br /> NULL = File is a log file. This is never a part of a filegroup.|  
|**size**|**nvarchar(15)**|File size in kilobytes.|  
|**maxsize**|**nvarchar(15)**|Maximum size to which the file can grow. A value of UNLIMITED in this field indicates that the file grows until the disk is full.|  
|**growth**|**nvarchar(15)**|Growth increment of the file. This indicates the amount of space added to the file every time that new space is required.<br /><br /> 0 = File is a fixed size and will not grow.|  
|**usage**|**varchar(9)**|For data file, the value is **'data only'** and for the log file the value is **'log only'**.|  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example returns information about the files in [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)].  
  
```sql  
USE AdventureWorks2012;  
GO  
EXEC sp_helpfile;  
GO  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [sp_helpfilegroup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpfilegroup-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)  
  
  
