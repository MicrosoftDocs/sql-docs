---
title: "sp_helpdb (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpdb"
  - "sp_helpdb_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helpdb"
ms.assetid: 4c3e3302-6cf1-4b2b-8682-004049b578c3
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpdb (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reports information about a specified database or all databases.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpdb [ [ @dbname= ] 'name' ]  
```  
  
## Arguments  
`[ @dbname = ] 'name'`
 Is the name of the database for which information is reported. *name* is **sysname**, with no default. If *name* is not specified, **sp_helpdb** reports on all databases in the **sys.databases** catalog view.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Database name.|  
|**db_size**|**nvarchar(13)**|Total size of the database.|  
|**owner**|**sysname**|Database owner, such as **sa**.|  
|**dbid**|**smallint**|Database ID.|  
|**created**|**nvarchar(11)**|Date the database was created.|  
|**status**|**nvarchar(600)**|Comma-separated list of values of database options that are currently set on the database.<br /><br /> Boolean-valued options are listed only if they are enabled. Non-Boolean options are listed with their corresponding values in the form of *option_name*=*value*.<br /><br /> For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).|  
|**compatibility_level**|**tinyint**|Database compatibility level: 60, 65, 70, 80, or 90.|  
  
 If *name* is specified, there is an additional result set that shows the file allocation for the specified database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**nchar(128)**|Logical file name.|  
|**fileid**|**smallint**|File ID.|  
|**filename**|**nchar(260)**|Operating-system file name (physical file name).|  
|**filegroup**|**nvarchar(128)**|Filegroup in which the file belongs.<br /><br /> NULL = file is a log file. This is never a part of a filegroup.|  
|**size**|**nvarchar(18)**|File size in megabytes.|  
|**maxsize**|**nvarchar(18)**|Maximum size to which the file can grow. A value of UNLIMITED in this field indicates that the file grows until the disk is full.|  
|**growth**|**nvarchar(18)**|Growth increment of the file. This indicates the amount of space added to the file each time new space is needed.|  
|**usage**|**varchar(9)**|Usage of the file. For a data file, the value is **'data only'** and for the log file the value is **'log only'**.|  
  
## Remarks  
 The **status** column in the result set reports which options have been set to ON in the database. All database options are not reported by the **status** column. To see a complete list of the current database option settings, use the **sys.databases** catalog view.  
  
## Permissions  
 When a single database is specified, membership in the **public** role in the database is required. When no database is specified, membership in the **public** role in the **master** database is required.  
  
 If a database cannot be accessed, **sp_helpdb** displays error message 15622 and as much information about the database as it can.  
  
## Examples  
  
### A. Returning information about a single database  
 The following example displays information about the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```sql  
EXEC sp_helpdb N'AdventureWorks2012';  
```  
  
### B. Returning information about all databases  
 This following example displays information about all databases on the server running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```sql  
EXEC sp_helpdb;  
GO  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)   
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
