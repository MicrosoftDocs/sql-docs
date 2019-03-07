---
title: "sp_primarykeys (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_primarykeys_TSQL"
  - "sp_primarykeys"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_primarykeys"
ms.assetid: 0f76dd31-5b7b-4209-9e2e-b9ed5cac164d
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_primarykeys (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the primary key columns, one row per key column, for the specified remote table.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_primarykeys [ @table_server = ] 'table_server'   
     [ , [ @table_name = ] 'table_name' ]   
     [ , [ @table_schema = ] 'table_schema' ]   
     [ , [ @table_catalog = ] 'table_catalog' ]  
```  
  
## Arguments  
 [ **@table_server =** ] **'**_table_server'_  
 Is the name of the linked server from which to return primary key information. *table_server* is **sysname**, with no default.  
  
 [ **@table_name =** ] **'**_table_name_**'**  
 Is the name of the table for which to provide primary key information. *table_name*is **sysname**, with a default of NULL.  
  
 [ **@table_schema =** ] **'**_table_schema_**'**  
 Is the table schema. *table_schema* is **sysname**, with a default of NULL. In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environment, this corresponds to the table owner.  
  
 [ **@table_catalog =** ] **'**_table_catalog_**'**  
 Is the name of the catalog in which the specified *table_name* resides. In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environment, this corresponds to the database name. *table_catalog* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**TABLE_CAT**|**sysname**|Table catalog.|  
|**TABLE_SCHEM**|**sysname**|Table schema.|  
|**TABLE_NAME**|**sysname**|Name of the table.|  
|**COLUMN_NAME**|**sysname**|Name of the column.|  
|**KEY_SEQ**|**int**|Sequence number of the column in a multicolumn primary key.|  
|**PK_NAME**|**sysname**|Primary key identifier. Returns NULL if not applicable to the data source.|  
  
## Remarks  
 **sp_primarykeys** is executed by querying the PRIMARY_KEYS rowset of the **IDBSchemaRowset** interface of the OLE DB provider corresponding to *table_server*. The *table_name*, *table_schema*, *table_catalog*, and *column* parameters are passed to this interface to restrict the rows returned.  
  
 **sp_primarykeys** returns an empty result set if the OLE DB provider of the specified linked server does not support the PRIMARY_KEYS rowset of the **IDBSchemaRowset** interface.  
  
## Permissions  
 Requires SELECT permission on the schema.  
  
## Examples  
 The following example returns primary key columns from the `LONDON1` server for the `HumanResources.JobCandidate` table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
EXEC sp_primarykeys @table_server = N'LONDON1',   
   @table_name = N'JobCandidate',  
   @table_catalog = N'AdventureWorks2012',   
   @table_schema = N'HumanResources';  
```  
  
## See Also  
 [Distributed Queries Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/distributed-queries-stored-procedures-transact-sql.md)   
 [sp_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-catalogs-transact-sql.md)   
 [sp_column_privileges &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-column-privileges-transact-sql.md)   
 [sp_foreignkeys &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-foreignkeys-transact-sql.md)   
 [sp_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-indexes-transact-sql.md)   
 [sp_linkedservers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-linkedservers-transact-sql.md)   
 [sp_tables_ex &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tables-ex-transact-sql.md)   
 [sp_table_privileges &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-table-privileges-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
