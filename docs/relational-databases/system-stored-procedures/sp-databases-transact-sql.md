---
description: "sp_databases (Transact-SQL)"
title: "sp_databases (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_databases_TSQL"
  - "sp_databases"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_databases"
ms.assetid: 2a83b92a-9ecc-43c4-8ff4-e91e3a940b5a
author: markingmyname
ms.author: maghan
---
# sp_databases (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Lists databases that either reside in an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or are accessible through a database gateway.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_databases  
```  
  
## Return Code Values  
 None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**DATABASE_NAME**|**sysname**|Name of the database. In the [!INCLUDE[ssDE](../../includes/ssde-md.md)], this column represents the database name as stored in the **sys.databases** catalog view.|  
|**DATABASE_SIZE**|**int**|Size of database, in kilobytes.|  
|**REMARKS**|**varchar(254)**|For the [!INCLUDE[ssDE](../../includes/ssde-md.md)], this field always returns NULL.|  
  
## Remarks  
 Database names that are returned can be used as parameters in the USE statement to change the current database context.
 
 DATABASE_SIZE returns a NULL value for databases larger than 2.15 TB.
  
 **sp_databases** has no equivalent in Open Database Connectivity (ODBC).  
  
## Permissions  
 Requires CREATE DATABASE, or ALTER ANY DATABASE, or VIEW ANY DEFINITION permission, and must have access permission to the database. Cannot be denied VIEW ANY DEFINITION permission.  
  
## Examples  
 The following example shows executing `sp_databases`.  
  
```sql  
USE master;  
GO  
EXEC sp_databases;  
```  
  
## See Also  
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [HAS_DBACCESS &#40;Transact-SQL&#41;](../../t-sql/functions/has-dbaccess-transact-sql.md)  
  
  
