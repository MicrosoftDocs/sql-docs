---
title: "sp_databases (Transact-SQL)"
description: "sp_databases (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_databases_TSQL"
  - "sp_databases"
helpviewer_keywords:
  - "sp_databases"
dev_langs:
  - "TSQL"
---
# sp_databases (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Lists databases that either reside in an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or are accessible through a database gateway.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
  
  
