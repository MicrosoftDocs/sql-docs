---
title: "INDEX_COL (Transact-SQL)"
description: "INDEX_COL (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "INDEX_COL_TSQL"
  - "INDEX_COL"
helpviewer_keywords:
  - "displaying column names"
  - "INDEX_COL function"
  - "viewing column names"
  - "column names [SQL Server]"
  - "names [SQL Server], columns"
dev_langs:
  - "TSQL"
---
# INDEX_COL (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the indexed column name. Returns NULL for XML indexes.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
INDEX_COL ( '[ database_name . [ schema_name ] .| schema_name ]  
    table_or_view_name', index_id , key_id )   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *database_name*  
 Is the name of the database.  
  
 *schema_name*  
 Is the name of the schema to which the index belongs.  
  
 *table_or_view_name*  
 Is the name of the table or indexed view. *table_or_view_name* must be delimited by single quotation marks and can be fully qualified by database name and schema name.  
  
 *index_id*  
 Is the ID of the index. *index_ID* is **int**.  
  
 *key_id*  
 Is the index key column position. *key_ID* is **int**.  
  
## Return Types  
 **nvarchar (128** **)**  
  
## Exceptions  
 Returns NULL on error or if a caller does not have permission to view the object.  
  
 A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as INDEX_COL may return NULL if the user does not have any permission on the object. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
  
### A. Using INDEX_COL to return an index column name  
 The following example returns the column names of the two key columns in the index `PK_SalesOrderDetail_SalesOrderID_LineNumber`.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT   
    INDEX_COL (N'AdventureWorks2012.Sales.SalesOrderDetail', 1,1) AS  
        [Index Column 1],   
    INDEX_COL (N'AdventureWorks2012.Sales.SalesOrderDetail', 1,2) AS  
        [Index Column 2]  
;  
GO  
```  
  
 Here is the result set:  
  
```  
Index Column 1      Index Column 2  
-----------------------------------------------  
SalesOrderID        SalesOrderDetailID  
```  
  
## See Also  
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)  
  
  
