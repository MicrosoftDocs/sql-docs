---
title: "OBJECT_SCHEMA_NAME (Transact-SQL)"
description: "OBJECT_SCHEMA_NAME (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "06/25/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "OBJECT_SCHEMA_NAME"
  - "OBJECT_SCHEMA_NAME_TSQL"
helpviewer_keywords:
  - "objects [SQL Server], names"
  - "schemas [SQL Server], names"
  - "displaying schema names"
  - "database objects [SQL Server], names"
  - "OBJECT_SCHEMA_NAME function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqledge-current || = azure-sqldw-latest"
---
# OBJECT_SCHEMA_NAME (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  Returns the database schema name for schema-scoped objects. For a list of schema-scoped objects, see [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
OBJECT_SCHEMA_NAME ( object_id [, database_id ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *object_id*  
 Is the ID of the object to be used. *object_id* is **int** and is assumed to be a schema-scoped object in the specified database, or in the current database context.  
  
 *database_id*  
 Is the ID of the database where the object is to be looked up. *database_id* is **int**.  
  
## Return Types  
 **sysname**  
  
## Exceptions  
 Returns NULL on error or if a caller does not have permission to view the object. If the target database has the AUTO_CLOSE option set to ON, the function will open the database.  
  
 A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as OBJECT_SCHEMA_NAME may return NULL if the user does not have any permission on the object. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Permissions  
 Requires ANY permission on the object. To specify a database ID, CONNECT permission to the database is also required, or the guest account must be enabled.  
  
## Remarks  
 System functions can be used in the select list, in the WHERE clause, and anywhere an expression is allowed. For more information, see [Expressions](../../t-sql/language-elements/expressions-transact-sql.md) and [WHERE](../../t-sql/queries/where-transact-sql.md).  
  
 The result set returned by this system function uses the collation of the current database.  
  
 If *database_id* is not specified, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] assumes that *object_id* is in the context of the current database. A query that references an *object_id* in another database returns NULL or incorrect results. For example, in the following query the context of the current database is [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)]. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] tries to return an object schema name for the specified object ID in that database instead of the database specified in the FROM clause of the query. Therefore, incorrect information is returned.  
  
```sql
SELECT DISTINCT OBJECT_SCHEMA_NAME(object_id)  
FROM master.sys.objects;  
```  
  
 The following example specifies the database ID for the `master` database in the `OBJECT_SCHEMA_NAME` function and returns the correct results.  
  
```sql
SELECT DISTINCT OBJECT_SCHEMA_NAME(object_id, 1) AS schema_name  
FROM master.sys.objects;   
```  
  
## Examples  
  
### A. Returning the object schema name and object name  
 The following example returns the object schema name, object name, and SQL text for all cached query plans that are not ad hoc or prepared statements.  
  
```sql
SELECT DB_NAME(st.dbid) AS database_name,   
    OBJECT_SCHEMA_NAME(st.objectid, st.dbid) AS schema_name,  
    OBJECT_NAME(st.objectid, st.dbid) AS object_name,   
    st.text AS query_statement  
FROM sys.dm_exec_query_stats AS qs  
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS st  
WHERE st.objectid IS NOT NULL;  
GO  
```  
  
### B. Returning three-part object names  
 The following example returns the database, schema, and object name along with all other columns in the `sys.dm_db_index_operational_stats` dynamic management view for all objects in all databases.  
  
```sql
SELECT QUOTENAME(DB_NAME(database_id))   
    + N'.'   
    + QUOTENAME(OBJECT_SCHEMA_NAME(object_id, database_id))   
    + N'.'   
    + QUOTENAME(OBJECT_NAME(object_id, database_id))  
    , *   
FROM sys.dm_db_index_operational_stats(null, null, null, null);  
GO  
```  
  
## See Also  
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [OBJECT_DEFINITION &#40;Transact-SQL&#41;](../../t-sql/functions/object-definition-transact-sql.md)   
 [OBJECT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/object-id-transact-sql.md)   
 [OBJECT_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/object-name-transact-sql.md)   
 [Securables](../../relational-databases/security/securables.md)
