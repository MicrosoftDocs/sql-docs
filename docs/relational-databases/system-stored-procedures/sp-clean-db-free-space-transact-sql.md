---
title: "sp_clean_db_free_space (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_clean_db_free_space_TSQL"
  - "sp_clean_db_free_space"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_clean_db_free_space"
  - "ghost records"
ms.assetid: faa96f7e-be92-47b1-8bc5-4dbba5331655
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_clean_db_free_space (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes residual information left on database pages because of data modification routines in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. sp_clean_db_free_space cleans all pages in all files of the database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_clean_db_free_space   
[ @dbname ] = 'database_name'   
[ , [ @cleaning_delay = ] 'delay_in_seconds' ] [;]  
```  
  
## Arguments  
 [ @dbname= ] '*database_name*'  
 Is the name of the database to clean. *dbname* is **sysname** and cannot be NULL.  
  
 [ @cleaning_delay= ] '*delay_in_seconds*'  
 Specifies an interval to delay between the cleaning of pages. This helps reduce the effect on the I/O system. *delay_in_seconds* is **int** with a default of 0.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 Delete operations from a table or update operations that cause a row to move can immediately free up space on a page by removing references to the row. However, under certain circumstances, the row can physically remain on the data page as a ghost record. Ghost records are periodically removed by a background process. This residual data is not returned by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] in response to queries. However, in environments in which the physical security of the data or backup files is at risk, you can use sp_clean_db_free_space to clean these ghost records.  
  
 The length of time required to run sp_clean_db_free_space depends on the size of the file, the available free space, and the capacity of the disk. Because running sp_clean_db_free_space can significantly affect I/O activity, we recommend that you run this procedure outside usual operation hours.  
  
 Before you run sp_clean_db_free_space, we recommend that you create a full database backup.  
  
 The related [sp_clean_db_file_free_space](../../relational-databases/system-stored-procedures/sp-clean-db-file-free-space-transact-sql.md) stored procedure can clean a single file.  
  
## Permissions  
 Requires membership in the db_owner database role.  
  
## Examples  
 The following example cleans all residual information from the `AdventureWorks2012` database.  
  
```  
USE master;  
GO  
EXEC sp_clean_db_free_space   
@dbname = N'AdventureWorks2012' ;  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)
 <br>[Ghost Cleanup Process Guide](../ghost-record-cleanup-process-guide.md) 
  
  
