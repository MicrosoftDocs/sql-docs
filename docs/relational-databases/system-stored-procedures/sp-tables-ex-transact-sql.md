---
description: "sp_tables_ex (Transact-SQL)"
title: "sp_tables_ex (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_tables_ex"
  - "sp_tables_ex_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_tables_ex"
ms.assetid: 33755c33-7e1e-4ef7-af14-a9cebb1e2ed4
author: markingmyname
ms.author: maghan
---
# sp_tables_ex (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns table information about the tables from the specified linked server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_tables_ex [ @table_server = ] 'table_server'   
     [ , [ @table_name = ] 'table_name' ]   
     [ , [ @table_schema = ] 'table_schema' ]  
     [ , [ @table_catalog = ] 'table_catalog' ]   
     [ , [ @table_type = ] 'table_type' ]   
     [ , [@fUsePattern = ] 'fUsePattern' ]  
```  
  
## Arguments  
`[ @table_server = ] 'table_server'`
 Is the name of the linked server for which to return table information. *table_server* is **sysname**, with no default.  
  
``[ , [ @table_name = ] 'table_name']``
 Is the name of the table for which to return data type information. *table_name*is **sysname**, with a default of NULL.  
  
`[ @table_schema = ] 'table_schema']`
 Is the table schema. *table_schema*is **sysname**, with a default of NULL.  
  
`[ @table_catalog = ] 'table_catalog'`
 Is the name of the database in which the specified *table_name* resides. *table_catalog* is **sysname**, with a default of NULL.  
  
`[ @table_type = ] 'table_type'`
 Is the type of the table to return. *table_type* is **sysname**, with a default of NULL, and can have one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**ALIAS**|Name of an alias.|  
|**GLOBAL TEMPORARY**|Name of a temporary table available system wide.|  
|**LOCAL TEMPORARY**|Name of a temporary table available only to the current job.|  
|**SYNONYM**|Name of a synonym.|  
|**SYSTEM TABLE**|Name of a system table.|  
|**SYSTEM VIEW**|Name of a system view.|  
|**TABLE**|Name of a user table.|  
|**VIEW**|Name of a view.|  
  
`[ @fUsePattern = ] 'fUsePattern'`
 Determines whether the characters **_**, **%**, **[**, and **]** are interpreted as wildcard characters. Valid values are 0 (pattern matching is off) and 1 (pattern matching is on). *fUsePattern* is **bit**, with a default of 1.  
  
## Return Code Values  
 None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**TABLE_CAT**|**sysname**|Table qualifier name. Various DBMS products support three-part naming for tables (_qualifier_**.**_owner_**.**_name_). In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some other products, it represents the server name of the database environment of the table. This field can be NULL.|  
|**TABLE_SCHEM**|**sysname**|Table owner name. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the name of the database user who created the table. This field always returns a value.|  
|**TABLE_NAME**|**sysname**|Table name. This field always returns a value.|  
|**TABLE_TYPE**|**varchar(32)**|Table, system table, or view.|  
|**REMARKS**|**varchar(254)**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not return a value for this column.|  
  
## Remarks  
 **sp_tables_ex** is executed by querying the TABLES rowset of the **IDBSchemaRowset** interface of the OLE DB provider corresponding to *table_server*. The *table_name*, *table_schema*, *table_catalog*, and *column* parameters are passed to this interface to restrict the rows returned.  
  
 **sp_tables_ex** returns an empty result set if the OLE DB provider of the specified linked server does not support the TABLES rowset of the **IDBSchemaRowset** interface.  
  
## Permissions  
 Requires SELECT permission on the schema.  
  
## Examples  
 The following example returns information about the tables that are contained in the `HumanResources` schema in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database on the `LONDON2` linked server.  
  
```  
EXEC sp_tables_ex @table_server = 'LONDON2',   
@table_catalog = 'AdventureWorks2012',   
@table_schema = 'HumanResources',   
@table_type = 'TABLE';  
```  
  
## See Also  
 [Distributed Queries Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/distributed-queries-stored-procedures-transact-sql.md)   
 [sp_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-catalogs-transact-sql.md)   
 [sp_columns_ex &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-columns-ex-transact-sql.md)   
 [sp_column_privileges &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-column-privileges-transact-sql.md)   
 [sp_foreignkeys &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-foreignkeys-transact-sql.md)   
 [sp_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-indexes-transact-sql.md)   
 [sp_linkedservers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-linkedservers-transact-sql.md)   
 [sp_table_privileges &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-table-privileges-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
