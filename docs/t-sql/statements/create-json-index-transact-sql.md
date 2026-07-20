---
title: "CREATE JSON INDEX (Transact-SQL)"
description: Creates a JSON index on a specified table and column in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].
author: uc-msft
ms.author: umajay
ms.reviewer: randolphwest
ms.date: 10/27/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: language-reference
ms.custom:
  - build-2025
monikerRange: ">=sql-server-2017"
---
# CREATE JSON INDEX (Transact-SQL)

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

Creates a JSON index on a specified table and column in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

JSON indexes:

- Can be created before there's data in the table.
- Can be created on tables in another database by specifying a qualified database name.
- Require the table to have a clustered primary key.
- Can't be specified on indexed views.

> [!NOTE]  
> Creating JSON indexes is currently in preview and only available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CREATE JSON INDEX name ON table_name (json_column_name)
  [ FOR ( sql_json_path [ , ...n ] ) ]
  [ WITH ( <json_index_option> [ , ...n ] ) ]
  [ ON { filegroup_name | "default" } ]
[ ; ]

<object> ::=
    { database_name.schema_name.table_name | schema_name.table_name | table_name }

<sql_json_path> ::=
    { character_string_literal }

<json_index_option> ::=
{
    OPTIMIZE_FOR_ARRAY_SEARCH = { ON | OFF }
  | FILLFACTOR = fillfactor
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

The name of the index. Index names must be unique within a table but don't have to be unique within a database. Index names must follow the rules of [identifiers](../../relational-databases/databases/database-identifiers.md).

- **ON \<object> ( *json_column_name* )**

  Specifies the object (database, schema, or table) on which the index is to be created, and the name of the **json** column.

- ***json_column_name***

  The name of the column of **json** data type in `table_name` that contains zero or more of the specified SQL/JSON paths.

- ***sql_json_path***

  The SQL/JSON path that needs to be extracted and indexed from `json_column_name`. The default for `sql_json_path` is `$`.

  - Recursively indexes all keys/values from the specified path onward.
  - Supports up to 128 levels in the JSON document path.
  - Doesn't allow overlapping.

  For example, `$.a` and `$.a.b` raise an error, since the path `$.a` recursively includes all the paths and the user intent is unclear.

#### ON *filegroup_name*

Creates the specified index on the specified filegroup. If no location is specified and the table isn't partitioned, the index uses the same filegroup as the underlying table. The filegroup must already exist.

#### ON "default"

Creates the specified index on the default filegroup.

The term default, in this context, isn't a keyword. It's an identifier for the default filegroup and must be delimited, as in `ON "default"` or `ON [default]`. If `"default"` is specified, the `QUOTED_IDENTIFIER` option must be `ON` for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER](set-quoted-identifier-transact-sql.md).

#### \<object>:: =

The fully qualified or non-fully qualified object to be indexed.

- ***database_name***

  The name of the database.

- ***schema_name***

  The name of the schema to which the table belongs.

- ***table_name***

  The name of the table to be indexed.

#### OPTIMIZE_FOR_ARRAY_SEARCH = { ON | OFF }

Specifies if array searches are optimized in the JSON index. The default is `OFF`.

#### FILLFACTOR = *fillfactor*

Specifies a percentage that indicates how full the [!INCLUDE [ssDE](../../includes/ssde-md.md)] should make the leaf level of each index page during index creation or rebuild. *fillfactor* must be an integer value from `1` to `100`. The default is `0`. If *fillfactor* is `100` or `0`, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] creates indexes with leaf pages filled to capacity.

> [!NOTE]  
> Fill factor values `0` and `100` are the same in all respects.

The `FILLFACTOR` setting applies only when the index is created or rebuilt. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] doesn't dynamically keep the specified percentage of empty space in the pages. To view the fill factor setting, use the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.

Creating a clustered index with a `FILLFACTOR` less than `100` affects the amount of storage space the data occupies, because the [!INCLUDE [ssDE](../../includes/ssde-md.md)] redistributes the data when it creates the clustered index.

For more information, see [Specify Fill Factor for an Index](../../relational-databases/indexes/specify-fill-factor-for-an-index.md).

#### DROP_EXISTING = { ON | OFF }

Specifies that the named, preexisting JSON index is dropped and rebuilt. The default is `OFF`.

- **ON**

  The existing index is dropped and rebuilt. The index name specified must be the same as a currently existing index; however, the index definition can be modified. For example, you can specify different columns, sort order, partition scheme, or index options.

- **OFF**

  An error is displayed if the specified index name already exists.

The index type can't be changed by using `DROP_EXISTING`.

#### ONLINE = OFF

Specifies that underlying tables and associated indexes aren't available for queries and data modification during the index operation. In this version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], online index builds aren't supported for JSON indexes. If this option is set to `ON` for a JSON index, an error is raised. Either omit the `ONLINE` option or set `ONLINE` to `OFF`.

An offline index operation that creates, rebuilds, or drops a JSON index, acquires a Schema modification (Sch-M) lock on the table. This prevents all user access to the underlying table for the duration of the operation.

Online index operations aren't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

[!INCLUDE [editions-supported-features-windows](../../includes/editions-supported-features-windows.md)]

#### ALLOW_ROW_LOCKS = { ON | OFF }

Specifies whether row locks are allowed. The default is `ON`.

- **ON**

  Row locks are allowed when accessing the index. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] determines when row locks are used.

- **OFF**

  Row locks aren't used.

#### ALLOW_PAGE_LOCKS = { ON | OFF }

Specifies whether page locks are allowed. The default is `ON`.

- **ON**

  Page locks are allowed when accessing the index. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] determines when page locks are used.

- **OFF**

  Page locks aren't used.

#### MAXDOP = *max_degree_of_parallelism*

Overrides the `max degree of parallelism` configuration option for the duration of the index operation. Use `MAXDOP` to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.

> [!IMPORTANT]  
> Although the `MAXDOP` option is syntactically supported, `CREATE JSON INDEX` currently always uses only a single processor.

*max_degree_of_parallelism* can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | Suppresses parallel plan generation. |
| `>1` | Restricts the maximum number of processors used in a parallel index operation to the specified number or fewer based on the current system workload. |
| `0` (default) | Uses the actual number of processors or fewer based on the current system workload. |

For more information, see [Configure parallel index operations](../../relational-databases/indexes/configure-parallel-index-operations.md).

Parallel index operations aren't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

[!INCLUDE [editions-supported-features-windows](../../includes/editions-supported-features-windows.md)]

#### DATA_COMPRESSION = { NONE | ROW | PAGE }

Determines the level of data compression used by the index.

- **NONE**

  No compression used on data by the index

- **ROW**

  Row compression used on data by the index

- **PAGE**

  Page compression used on data by the index

## Remarks

Every option can be specified only once per `CREATE JSON INDEX` statement. Specifying a duplicate of any option raises an error.

#### [ ON { *filegroup_name* | "default" } ]

If you specify a filegroup for a JSON index, the index is placed on that filegroup, regardless of the partitioning scheme of the table.

For more information about creating indexes, see the Remarks section in [CREATE INDEX](create-index-transact-sql.md#remarks).

## Predicates supported with a JSON index

Searching operations on JSON documents contained in a **json** column in a table can be optimized if a JSON index exists on the **json** column. The JSON index is used in queries with various JSON function-based expressions.

The following examples use the `Sales.SalesOrderHeader` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database with a **json** column called `Info`. The `Info` column is created as a **json** type. A JSON index is also created on the `Info` column with default settings. The following code sample shows the `CREATE JSON INDEX` statement:

```sql
CREATE JSON INDEX sales_info_idx
    ON Sales.SalesOrderHeader (Info);
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

Use the [JSON_VALUE](../functions/json-value-transact-sql.md) to extract the JSON text / scalar value in a specified SQL/JSON path in a JSON document. The following queries show how a `JSON_VALUE` expression on a **json** column can be optimized using a JSON index.

- Equality search for a JSON string in an object property:

  ```sql
  SELECT COUNT(*)
  FROM Sales.SalesOrderHeader
  WHERE JSON_VALUE(Info, '$.Customer.Type') = 'IN';
  ```

- Equality search for a JSON number in an object property after converting the value to an **int** data type:

  ```sql
  SELECT *
  FROM Sales.SalesOrderHeader
  WHERE JSON_VALUE(Info, '$.Customer.ID' RETURNING INT) = 16167;
  ```

- Range search for a JSON number in an object property after converting the value to an **int** data type:

  ```sql
  SELECT *
  FROM Sales.SalesOrderHeader
  WHERE JSON_VALUE(Info, '$.Customer.ID' RETURNING INT) IN (16167, 16517);
  ```

- Range search for a JSON number in an object property after converting the value to a **decimal** data type:

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

The user must have `ALTER` permission on the table, or be a member of the **sysadmin** fixed server role, or the **db_ddladmin** and **db_owner** fixed database roles.

## Limitations

The following limitations exist for the JSON index statement:

- Only one JSON index can be created on a **json** column in a table.
- You can create up to 249 JSON indexes in a table. Creating more than one JSON index on a specific JSON column isn't supported.
- A JSON index can't be created on computed **json** columns.
- A JSON index can't be created on **json** columns in a view, table-valued variable, or memory optimized table.
- A JSON index can be created or altered only in an offline manner.
- JSON paths can't overlap in the index definition. For example, `$a` and `$a.b` overlap, and aren't allowed in the `CREATE JSON INDEX` statement.
- Modification of paths requires recreating the JSON index.
- JSON indexes aren't supported in index hints.
- The data compression option isn't supported.

## Examples

### A. Create a JSON index on a JSON column

The following example creates a table named `docs` that contains a **json** type column, `content`. The example then creates a JSON index, `json_content_index`, on the `content` column. The example creates the **json** index on the entire JSON document or all SQL/JSON paths in the JSON document.

```sql
DROP TABLE IF EXISTS docs;

CREATE TABLE docs
(
    content JSON,
    id INT PRIMARY KEY
);

CREATE JSON INDEX json_content_index
    ON docs (content);
```

### A. Create a JSON index on a JSON column with specific paths

The following example creates a table named `docs` that contains a **json** type column, `content`. The example then creates a JSON index, `json_content_index`, on the `content` column. The example creates the **json** index on specific SQL/JSON paths in the JSON document.  
The example also sets the index FILLFACTOR to `80`.

```sql
DROP TABLE IF EXISTS docs;

CREATE TABLE docs
(
    content JSON,
    id INT PRIMARY KEY
);

CREATE JSON INDEX json_content_index
    ON docs (content)
    FOR ('$.a', '$.b') WITH (FILLFACTOR = 80);
```

### B. JSON index with array search optimization

The following example returns JSON indexes for the table `dbo.Customers`. The JSON index is created with the array search optimization option enabled.

```sql
DROP TABLE IF EXISTS dbo.Customers;

CREATE TABLE dbo.Customers
(
    customer_id INT IDENTITY PRIMARY KEY,
    customer_info JSON NOT NULL
);

CREATE JSON INDEX CustomersJsonIndex
    ON dbo.Customers (customer_info) WITH (OPTIMIZE_FOR_ARRAY_SEARCH = ON);

INSERT INTO dbo.Customers (customer_info)
VALUES ('{"name":"customer1", "email": "customer1@example.com", "phone":["123-456-7890", "234-567-8901"]}');

SELECT object_id,
       index_id,
       optimize_for_array_search
FROM sys.json_indexes AS ji
WHERE object_id = OBJECT_ID('dbo.Customers');
```

## Related content

- [DROP INDEX (Transact-SQL)](drop-index-transact-sql.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [sys.index_columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)
- [sys.indexes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)
