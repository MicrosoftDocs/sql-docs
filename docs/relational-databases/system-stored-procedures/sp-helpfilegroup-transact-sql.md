---
title: "sp_helpfilegroup (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpfilegroup_TSQL"
  - "sp_helpfilegroup"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helpfilegroup"
ms.assetid: 619716b5-95dc-4538-82ae-4b90b9da8ebc
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpfilegroup (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the names and attributes of filegroups associated with the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpfilegroup [ [ @filegroupname = ] 'name' ]  
```  
  
## Arguments  
 [ **@filegroupname =** ] **'***name***'**  
 Is the logical name of any filegroup in the current database. *name* is **sysname**, with a default of NULL. If *name* is not specified, all filegroups in the current database are listed and only the first result set shown in the Result Sets section is displayed.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**groupname**|**sysname**|Name of the filegroup.|  
|**groupid**|**smallint**|Numeric filegroup identifier.|  
|**filecount**|**int**|Number of files in the filegroup.|  
  
 If *name* is specified, one row for each file in the filegroup is returned.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**file_in_group**|**sysname**|Logical name of the file in the filegroup.|  
|**fileid**|**smallint**|Numeric file identifier.|  
|**filename**|**nchar(260)**|Physical name of the file including the directory path.|  
|**size**|**nvarchar(15)**|File size in kilobytes.|  
|**maxsize**|**nvarchar(15)**|Maximum size of the file.<br /><br /> This is the maximum size to which the file can grow. A value of UNLIMITED in this field indicates that the file grows until the disk is full.|  
|**growth**|**nvarchar(15)**|Growth increment of the file. This indicates the amount of space added to the file every time new space is required.<br /><br /> 0 = File is a fixed size and will not grow.|  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
  
### A. Returning all filegroups in a database  
 The following example returns information about the filegroups in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.  
  
```sql  
USE AdventureWorks2012;  
GO  
EXEC sp_helpfilegroup;  
GO  
```  
  
### B. Returning all files in a filegroup  
 The following example returns information for all files in the `PRIMARY` filegroup in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.  
  
```sql  
USE AdventureWorks2012;  
GO  
EXEC sp_helpfilegroup 'PRIMARY';  
GO  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [sp_helpfile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpfile-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)  
  
  
