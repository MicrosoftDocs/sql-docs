---
description: "sp_indexes (Transact-SQL)"
title: "sp_indexes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_indexes_TSQL"
  - "sp_indexes"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_indexes"
ms.assetid: 25469e72-9d95-463f-912a-193471c8f5e2
author: markingmyname
ms.author: maghan
---
# sp_indexes (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns index information for the specified remote table.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_indexes [ @table_server = ] 'table_server'   
     [ , [ @table_name = ] 'table_name' ]   
     [ , [ @table_schema = ] 'table_schema' ]   
     [ , [ @table_catalog = ] 'table_db' ]   
     [ , [ @index_name = ] 'index_name' ]   
     [ , [ @is_unique = ] 'is_unique' ]  
```  
  
## Arguments  
 [ @table_server= ] '*table_server*'  
 Is the name of a linked server running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for which table information is being requested. *table_server* is **sysname**, with no default.  
  
 [ @table_name= ] '*table_name*'  
 Is the name of the remote table for which to provide index information. *table_name* is **sysname**, with a default of NULL. If NULL, all tables in the specified database are returned.  
  
 [ @table_schema= ] '*table_schema*'  
 Specifies the table schema. In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environment, this corresponds to the table owner. *table_schema* is **sysname**, with a default of NULL.  
  
 [ @table_catalog= ] '*table_db*'  
 Is the name of the database in which *table_name* resides. *table_db* is **sysname**, with a default of NULL. If NULL, *table_db* defaults to **master**.  
  
 [ @index_name= ] '*index_name*'  
 Is the name of the index for which information is being requested. *index* is **sysname**, with a default of NULL.  
  
 [ @is_unique= ] '*is_unique*'  
 Is the type of index for which to return information. *is_unique* is **bit**, with a default of NULL, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|1|Returns information about unique indexes.|  
|0|Returns information about indexes that are not unique.|  
|NULL|Returns information about all indexes.|  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|TABLE_CAT|**sysname**|Name of the database in which the specified table resides.|  
|TABLE_SCHEM|**sysname**|Schema for the table.|  
|TABLE_NAME|**sysname**|Name of the remote table.|  
|NON_UNIQUE|**smallint**|Whether the index is unique or not unique:<br /><br /> 0 = Unique<br /><br /> 1 = Not unique|  
|INDEX_QUALIFER|**sysname**|Name of the index owner. Some DBMS products allow for users other than the table owner to create indexes. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column is always the same as **TABLE_NAME**.|  
|INDEX_NAME|**sysname**|Name of the index.|  
|TYPE|**smallint**|Type of index:<br /><br /> 0 = Statistics for a table<br /><br /> 1 = Clustered<br /><br /> 2 = Hashed<br /><br /> 3 = Other|  
|ORDINAL_POSITION|**int**|Ordinal position of the column in the index. The first column in the index is 1. This column always returns a value.|  
|COLUMN_NAME|**sysname**|Is the corresponding name of the column for each column of the TABLE_NAME returned.|  
|ASC_OR_DESC|**varchar**|Is the order used in collation:<br /><br /> A = Ascending<br /><br /> D = Descending<br /><br /> NULL = Not applicable<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] always returns A.|  
|CARDINALITY|**int**|Is the number of rows in the table or unique values in the index.|  
|PAGES|**int**|Is the number of pages to store the index or table.|  
|FILTER_CONDITION|**nvarchar(**4000**)**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not return a value.|  
  
## Permissions  
 Requires SELECT permission on the schema.  
  
## Examples  
 The following example returns all index information from the `Employees` table of the `AdventureWorks2012` database on the `Seattle1` linked server.  
  
```  
EXEC sp_indexes @table_server = 'Seattle1',   
   @table_name = 'Employee',   
   @table_schema = 'HumanResources',  
   @table_catalog = 'AdventureWorks2012';  
```  
  
## See Also  
 [Distributed Queries Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/distributed-queries-stored-procedures-transact-sql.md)   
 [sp_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-catalogs-transact-sql.md)   
 [sp_column_privileges &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-column-privileges-transact-sql.md)   
 [sp_foreignkeys &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-foreignkeys-transact-sql.md)   
 [sp_linkedservers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-linkedservers-transact-sql.md)   
 [sp_tables_ex &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tables-ex-transact-sql.md)   
 [sp_table_privileges &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-table-privileges-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
