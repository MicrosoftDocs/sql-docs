---
title: CREATE JSON INDEX (Transact-SQL)
description: Creates a JSON index on a specified table and column in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].
author: uc-msft
ms.author: umajay
ms.date: 05/19/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: language-reference
monikerRange: " >=sql-server-2016"
---
# CREATE JSON INDEX (Transact-SQL)

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

Creates a JSON index on a specified table and column in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

JSON indexes:
- Can be created before there is data in the table
- Can be created on tables in another database by specifying a qualified database name.
- Require the table to have a clustered primary key.
- Cannot be specified on indexed views.

> [!NOTE]
> Creating JSON indexes is currently in preview and only available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. 

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE JSON INDEX name ON table_name (json_column_name)
  [ FOR ( sql_json_path [,…n] ) ]
  [ WITH ( <json_index_option> [,…n] ) ]
  [ ON { filegroup_name | "default" } ]  
[;]
  
<object> ::=  
    { database_name.schema_name.table_name | schema_name.table_name | table_name }  
  
<sql_json_path> ::=
    { character_string_literal }

  
<json_index_option> ::=  
{  
    FILLFACTOR = fillfactor  
  | DROP_EXISTING = { ON | OFF }  
  | ONLINE = OFF  
  | ALLOW_ROW_LOCKS = { ON | OFF }  
  | ALLOW_PAGE_LOCKS = { ON | OFF }  
  | MAXDOP = max_degree_of_parallelism  
  | DATA_COMPRESSION = { NONE | ROW | PAGE }  
}  
``` 
  
## Arguments

### index_name   
The name of the index. Index names must be unique within a table but do not have to be unique within a database. Index names must follow the rules of [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
ON \<object> ( *json_column_name* )     
Specifies the object (database, schema, or table) on which the index is to be created and the name of json column.  
  
#### *json_column_name*

The name of the column of json data type in `table_name` that contains zero or more of the specified SQL/JSON paths.
  
#### *sql_json_path*

The SQL/JSON path that needs to be extracted and indexed from `json_column_name`. The default for `sql_json_path` is `$`.

- Recursively indexes all keys/values from the specified path onward.
- Supports up to 128 levels in JSON document path.
- Doesn't allow overlapping.

For example, `$.a` and `$.a.b` raise error since the path `$.a` recursively includes all the paths and the user intent is unclear.

#### ON *filegroup_name*      
  
Creates the specified index on the specified filegroup. If no location is specified and the table is not partitioned, the index uses the same filegroup as the underlying table. The filegroup must already exist.  
  
#### ON "default"     
  
Creates the specified index on the default filegroup.  
  
The term default, in this context, is not a keyword. It is an identifier for the default filegroup and must be delimited, as in ON "default" or ON [default]. If "default" is specified, the QUOTED_IDENTIFIER option must be ON for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
  
 **\<object>::=**    
 Is the fully qualified or non-fully qualified object to be indexed.  
  
 *database_name*      
 Is the name of the database.  
  
 *schema_name*     
 Is the name of the schema to which the table belongs.  
  
 *table_name*      
 Is the name of the table to be indexed.  

  
#### FILLFACTOR =*fillfactor*     
  
Specifies a percentage that indicates how full the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should make the leaf level of each index page during index creation or rebuild. *fillfactor* must be an integer value from 1 to 100. The default is 0. If *fillfactor* is 100 or 0, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] creates indexes with leaf pages filled to capacity.  
  
> [!NOTE]  
> Fill factor values 0 and 100 are the same in all respects.
  
 The FILLFACTOR setting applies only when the index is created or rebuilt. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not dynamically keep the specified percentage of empty space in the pages. To view the fill factor setting, use the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.  
  
> [!IMPORTANT]  
> Creating a clustered index with a FILLFACTOR less than 100 affects the amount of storage space the data occupies because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] redistributes the data when it creates the clustered index.  
  
 For more information, see [Specify Fill Factor for an Index](../../relational-databases/indexes/specify-fill-factor-for-an-index.md).  
  
#### DROP_EXISTING = { ON | **OFF** }     
  
 Specifies that the named, preexisting spatial index is dropped and rebuilt. The default is OFF.  
  
 ON     
 The existing index is dropped and rebuilt. The index name specified must be the same as a currently existing index; however, the index definition can be modified. For example, you can specify different columns, sort order, partition scheme, or index options.  
  
 OFF     
 An error is displayed if the specified index name already exists.  
  
 The index type cannot be changed by using DROP_EXISTING.  
  
#### ONLINE =**OFF**      
Specifies that underlying tables and associated indexes are not available for queries and data modification during the index operation. In this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], online index builds are not supported for json indexes. If this option is set to ON for a json index, an error is raised. Either omit the ONLINE option or set ONLINE to OFF.  
  
 An offline index operation that creates, rebuilds, or drops a json index, acquires a Schema modification (Sch-M) lock on the table. This prevents all user access to the underlying table for the duration of the operation.  
  
> [!NOTE]  
> Online index operations are not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
#### ALLOW_ROW_LOCKS = { **ON** | OFF }     
  
 Specifies whether row locks are allowed. The default is ON.  
  
 ON     
 Row locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when row locks are used.  
  
 OFF     
 Row locks are not used.  
  
#### ALLOW_PAGE_LOCKS = { **ON** | OFF }     
  
 Specifies whether page locks are allowed. The default is ON.  
  
 ON    
 Page locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when page locks are used.  
  
 OFF     
 Page locks are not used.  
  
#### MAXDOP =*max_degree_of_parallelism*      
  
 Overrides the `max degree of parallelism` configuration option for the duration of the index operation. Use MAXDOP to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.  
  
> [!IMPORTANT]  
> Although the MAXDOP option is syntactically supported, CREATE SPATIAL INDEX currently always uses only a single processor.  
  
 *max_degree_of_parallelism* can be:  
  
 1     
 Suppresses parallel plan generation.  
  
 \>1    
 Restricts the maximum number of processors used in a parallel index operation to the specified number or fewer based on the current system workload.  
  
 0 (default)    
 Uses the actual number of processors or fewer based on the current system workload.  
  
 For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).  
  
> [!NOTE]
> Parallel index operations are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
DATA_COMPRESSION = {NONE | ROW | PAGE}     
  
 Determines the level of data compression used by the index.  
  
 NONE     
 No compression used on data by the index  
  
 ROW    
 Row compression used on data by the index  
  
 PAGE    
 Page compression used on data by the index  
  
## Remarks
Every option can be specified only once per CREATE JSON INDEX statement. Specifying a duplicate of any option raises an error.  
  
[ ON { *filegroup_name* | "default" } ]     
If you specify a filegroup for a json index, the index is placed on that filegroup, regardless of the partitioning scheme of the table.  
  
For more information about creating indexes, see the "Remarks" section in [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md).  

## Predicates supported with a JSON index

Searching operations on JSON documents contained in a **json** column in a table can be optimized if a JSON index exists on the **json** column. The JSON index is used in queries with various JSON function-based expressions.

The following examples use the Sales.SalesOrderHeader table in AdventureWorks database with a **json** column called Info. The Info column is created as a **json** type. A JSON index is also created on the Info column with default settings. The following code sample shows the `CREATE JSON INDEX` statement:

```sql
CREATE JSON INDEX sales_info_idx ON Sales.SalesOrderHeader(Info);
```

For the sample search expressions, use the following JSON documents as data:

| SalesOrderNumber | Info |
| --- | --- |
| `437` | `{"Customer":{"Name":"Kelsey Raje","ID":16517,"Type":"IN"},"Order":{"ID":43710,"Number":"SO43710","CreationDate":"2011-06-02T00:00:00","TotalDue":3953.9884}}` |
| `643` | `{"Customer":{"Name":"Aaron Campbell","ID":16167,"Type":"IN"},"Order":{"ID":64304,"Number":"SO64304","CreationDate":"2014-01-16T00:00:00","TotalDue":36.0230, "IsProcessed": true}}` |

### JSON_PATH_EXISTS function

Use the [JSON_PATH_EXISTS](../functions/json-path-exists-transact-sql.md) function to test if a specified SQL/JSON path exists in a JSON document.

This query demonstrates `JSON_PATH_EXISTS` on a **json** column that can be optimized using a JSON index:

```sql
SELECT COUNT(*)
FROM Sales.SalesOrderHeader
WHERE JSON_PATH_EXISTS(Info, '$.Order.IsProcessed') = 1;
```
JSON index is supported with `JSON_PATH_EXISTS` predicate and the following operators:

- Comparison operators (`=`)
- `IS [NOT] NULL` predicate (Not currently supported)

### JSON_VALUE function

Use the [JSON_VALUE](../functions/json-value-transact-sql.md) to extract the JSON text / scalar value in a specified SQL/JSON path in a JSON document. The following queries show how a `JSON_VALUE` expression on a **json** column can be optimized using a JSON index:

- Equality search for a JSON string in an object property

  ```sql
  SELECT COUNT(*)
  FROM Sales.SalesOrderHeader
  WHERE JSON_VALUE(Info, '$.Customer.Type') = 'IN';
  ```

- Equality search for a JSON number in an object property after converting the value to an **int** data type

  ```sql
  SELECT *
  FROM Sales.SalesOrderHeader
  WHERE JSON_VALUE(Info, '$.Customer.ID' RETURNING INT) = 16167;
  ```

- Range search for a JSON number in an object property after converting the value to an **int** data type

  ```sql
  SELECT *
  FROM Sales.SalesOrderHeader
  WHERE JSON_VALUE(Info, '$.Customer.ID' RETURNING INT) IN (16167, 16517);
  ```

- Range search for a JSON number in an object property after converting the value to a **decimal** data type

  ```sql
  SELECT *
  FROM Sales.SalesOrderHeader
  WHERE JSON_VALUE(Info, '$.Order.TotalDue RETURNING decimal(20, 4)) BETWEEN 1000 and 2000;
  ```

The JSON index is supported with a `JSON_VALUE` predicate, and the following operators:

- Comparison operators (`=`)
- `LIKE` predicate (not currently supported)
- `IS [NOT] NULL` predicate (not currently supported)

### JSON_CONTAINS function

The [JSON_CONTAINS](../functions/json-contains-transact-sql.md) function supports easy searching of JSON values in a JSON document that can use a JSON index if present on a **json** column. This function can be used to test if a JSON scalar value, object, or array is contained in the specified SQL/JSON path in a JSON document. The search values specified as SQL scalar types are converted per existing SQL/JSON type conversions. These rules are defined in the behavior section.

## Requirement
A clustering key is required on the table that contains the JSON column. An error is raised if the clustering key is absent. The clustering key is limited to 31 columns and the maximum size of the index key should be less than 128 bytes.
  
## Permissions
The user must have `ALTER` permission on the table, or be a member of the sysadmin fixed server role or the `db_ddladmin` and `db_owner` fixed database roles.  

## Restrictions

The following limitations exist for the JSON index statement:

- Only one JSON index can be created on a **json** column in a table.
- You can create up to 249 JSON indexes in a table. Creating more than one JSON index on specific JSON column is not supported.  
- A JSON index can't be created on computed **json** columns.
- A JSON index can't be created on **json** columns in a view, table-valued variable, or memory optimized table.
- A JSON index can be created or altered only in an offline manner.
- JSON paths can't overlap in the index definition. For example, `$a` and `$a.b` overlap, and aren't allowed in the `CREATE JSON INDEX` statement.
- Modification of paths requires recreating the JSON index.
- JSON indexes aren't supported in index hints.
- Data compression option isn't supported.
  
## Examples  
  
### A. Create a JSON index on a json column
The following example creates a table named `docs` that contains a **json** type column, `content`. The example then creates a json index, `json_content_index`, on the `content` column. The example creates the **json** index on the entire JSON document or all SQL/JSON paths in the JSON document.  
  
```sql  
DROP TABLE IF EXISTS docs;
CREATE TABLE docs ( content json, id int primary key );
CREATE JSON INDEX json_content_index on docs( content );
```  
  
### A. Create a JSON index on a json column with specific paths
The following example creates a table named `docs` that contains a **json** type column, `content`. The example then creates a json index, `json_content_index`, on the `content` column. The example creates the **json** index on specific SQL/JSON paths in the JSON document.  
 The example also sets the index FILLFACTOR to `80`.  
  
```sql  
DROP TABLE IF EXISTS docs;
CREATE TABLE docs ( content json, id int primary key );
CREATE JSON INDEX json_content_index on docs( content )
FOR ('$.a', '$.b')
WITH (FILLFACTOR = 80); 
```  
  
 
## Related content


- [DROP INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-index-transact-sql.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
- [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)
- [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)
- [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)
