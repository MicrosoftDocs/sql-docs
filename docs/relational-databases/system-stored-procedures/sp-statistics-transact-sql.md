---
description: "The sp_statistics system stored procedure returns a list of all indexes and statistics on a specified table or indexed view."
title: "sp_statistics (Transact-SQL)"
ms.custom: ""
ms.date: "03/11/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_statistics_TSQL"
  - "sp_statistics"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_statistics"
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_statistics (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a list of all indexes and statistics on a specified table or indexed view.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
-- Syntax for SQL Server, Azure SQL Database, Azure Synapse Analytics, Parallel Data Warehouse  

sp_statistics [ @table_name = ] 'table_name'    
     [ , [ @table_owner = ] 'owner' ]   
     [ , [ @table_qualifier = ] 'qualifier' ]   
     [ , [ @index_name = ] 'index_name' ]   
     [ , [ @is_unique = ] 'is_unique' ]  
     [ , [ @accuracy = ] 'accuracy' ]  
```  

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments  

#### `[ @table_name = ] 'table_name'`
 Specifies the table used to return catalog information. `table_name` is **sysname**, with no default. Wildcard pattern matching is not supported.  
  
#### `[ @table_owner = ] 'owner'`
 Is the name of the table owner of the table used to return catalog information. `table_owner` is **sysname**, with a default of NULL. Wildcard pattern matching is not supported. If `owner` is not specified, the default table visibility rules of the underlying DBMS apply.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if the current user owns a table with the specified name, the indexes of that table are returned. If `owner` is not specified and the current user does not own a table with the specified `name`, this procedure looks for a table with the specified `name` owned by the database owner. If one exists, the indexes of that table are returned.  
  
#### `[ @table_qualifier = ] 'qualifier'`
 Is the name of the table qualifier. `qualifier` is **sysname**, with a default of NULL. Various DBMS products support three-part naming for tables (`_qualifier_**.**_owner_**.**_name_`). In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this parameter represents the database name. In some products, it represents the server name of the table's database environment.  
  
#### `[ @index_name = ] 'index_name'`
 Is the index name. `index_name` is **sysname**, with a default of %. Wildcard pattern matching is supported.  
  
#### `[ @is_unique = ] 'is_unique'`
 Is whether only unique indexes (if **Y**) are to be returned. `is_unique` is **char(1)**, with a default of **N**.  
  
#### `[ @accuracy = ] 'accuracy'`
 Is the level of cardinality and page accuracy for statistics. `accuracy` is **char(1)**, with a default of **Q**. Specify **E** to make sure that statistics are updated so that cardinality and pages are accurate.  
  
 The value **E** (SQL_ENSURE) asks the driver to unconditionally retrieve the statistics.  
  
 The value **Q** (SQL_QUICK) asks the driver to retrieve the cardinality and pages only if they are readily available from the server. In this case, the driver does not ensure that the values are current. Applications that are written to the Open Group standard will always get SQL_QUICK behavior from ODBC 3.x-compliant drivers.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**TABLE_QUALIFIER**|**sysname**|Table qualifier name. This column can be NULL.|  
|**TABLE_OWNER**|**sysname**|Table owner name. This column always returns a value.|  
|**TABLE_NAME**|**sysname**|Table name. This column always returns a value.|  
|**NON_UNIQUE**|**smallint**|NOT NULL.<br /><br /> 0 = Unique<br /><br /> 1 = Not unique|  
|**INDEX_QUALIFIER**|**sysname**|Index owner name. Some DBMS products allow for users other than the table owner to create indexes. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column is always the same as **TABLE_NAME**.|  
|**INDEX_NAME**|**sysname**|Is the name of the index. This column always returns a value.|  
|**TYPE**|**smallint**|This column always returns a value:<br /><br /> 0 = Statistics for a table<br /><br /> 1 = Clustered<br /><br /> 2 = Hashed<br /><br /> 3 = Nonclustered|  
|**SEQ_IN_INDEX**|**smallint**|Position of the column within the index.|  
|**COLUMN_NAME**|**sysname**|Column name for each column of the **TABLE_NAME** returned. This column always returns a value.|  
|**COLLATION**|**char(1)**|Order used in collation. Can be:<br /><br /> A = Ascending<br /><br /> D = Descending<br /><br /> NULL = Not applicable|  
|**CARDINALITY**|**int**|Number of rows in the table or unique values in the index.|  
|**PAGES**|**int**|Number of pages to store the index or table.|  
|**FILTER_CONDITION**|**varchar(128)**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not return a value.|  
  
## Return Code Values  
 None  
  
## Remarks  
 The indexes in the result set appear in ascending order by the columns `NON_UNIQUE`, `TYPE`, `INDEX_NAME`, and `SEQ_IN_INDEX`.  
  
 The index type clustered refers to an index in which table data is stored in the order of the index. This corresponds to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clustered indexes.  
  
 The index type Hashed accepts exact match or range searches, but pattern matching searches do not use the index.  
  
 The `sp_statistics` system stored procedure is equivalent to **SQLStatistics** in ODBC. The results returned are ordered by `NON_UNIQUE`, `TYPE`, `INDEX_QUALIFIER`, `INDEX_NAME`, and `SEQ_IN_INDEX`. For more information, see the [ODBC API Reference](../../odbc/reference/syntax/odbc-reference.md).
  
## Permissions  
 Requires SELECT permission on the schema.  
  
## Example: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example returns information about the `DimEmployee` table from the `AdventureWorks` sample database.  
  
```sql  
EXEC sp_statistics DimEmployee;  
```  
  
## See also  

- [Catalog Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/catalog-stored-procedures-transact-sql.md)   
- [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
- [Statistics for Memory-Optimized Tables](../in-memory-oltp/statistics-for-memory-optimized-tables.md)

## Next steps

- [Statistics](../statistics/statistics.md)
- [Update Statistics](../statistics/update-statistics.md)
