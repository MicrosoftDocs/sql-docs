---
title: "sp_describe_first_result_set (Transact-SQL)"
description: Returns the metadata for the first possible result set of the Transact-SQL batch.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_describe_first_result_set"
  - "sp_describe_first_result_set_TSQL"
helpviewer_keywords:
  - "sp_describe_first_result_set"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sp_describe_first_result_set (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns the metadata for the first possible result set of the [!INCLUDE [tsql](../../includes/tsql-md.md)] batch. Returns an empty result set if the batch returns no results. Raises an error if the [!INCLUDE [ssDE](../../includes/ssde-md.md)] can't determine the metadata for the first query that will be executed by performing a static analysis. The dynamic management view [sys.dm_exec_describe_first_result_set](../system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql.md) returns the same information.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_describe_first_result_set [ @tsql = ] N'tsql'
    [ , [ @params = ] N'params [ , ...n ]' ]
    [ , [ @browse_information_mode = ] <tinyint> ]
[ ; ]
```

## Arguments

#### [ @tsql = ] '*tsql*'

One or more [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. *@tsql* might be **nvarchar(*n*)** or **nvarchar(max)**.

#### [ @params = ] N'*params*'

*@params* provides a declaration string for parameters for the [!INCLUDE [tsql](../../includes/tsql-md.md)] batch, which is similar to `sp_executesql`. Parameters might be **nvarchar(*n*)** or **nvarchar(max)**.

A string that contains the definitions of all parameters that are embedded in *@tsql*. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates additional parameter definitions. Every parameter specified in the statement must be defined in *@params*. If the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch in the statement doesn't contain parameters, *@params* isn't required. `NULL` is the default value for this parameter.

#### [ @browse_information_mode = ] *tinyint*

Specifies if extra key columns and source table information are returned. If set to `1`, each query is analyzed as if it includes a `FOR BROWSE` option on the query.

- If set to `0`, no information is returned.

- If set to `1`, each query is analyzed as if it includes a `FOR BROWSE` option on the query. This returns base table names as the source column information.

- If set to `2`, each query is analyzed as if it would be used in preparing or executing a cursor. This returns view names as source column information.

## Return code values

`sp_describe_first_result_set` always returns a status of zero on success. If the procedure throws an error and the procedure is called as an RPC, return status is populated by the type of error described in the error_type column of `sys.dm_exec_describe_first_result_set`. If the procedure is called from [!INCLUDE [tsql](../../includes/tsql-md.md)], the return value is always zero, even when there's an error.

## Result set

This common metadata is returned as a result set with one row for each column in the results metadata. Each row describes the type and nullability of the column in the format described in the following section. If the first statement doesn't exist for every control path, a result set with zero rows is returned.

| Column name | Data type | Description |
| --- | --- | --- |
| `is_hidden` | **bit** | Indicates that the column is an extra column added for browsing information purposes and that it doesn't actually appear in the result set. Not nullable. |
| `column_ordinal` | **int** | Contains the ordinal position of the column in the result set. The first column's position is specified as `1`. Not nullable. |
| `name` | **sysname** | Contains the name of the column if a name can be determined. Otherwise, it contains `NULL`. Nullable. |
| `is_nullable` | **bit** | Contains the value `1` if the column allows `NULL`, `0` if the column doesn't allow `NULL`, and `1` if it can't be determined if the column allows `NULL`. Not nullable. |
| `system_type_id` | **int** | Contains the `system_type_id` of the data type of the column as specified in `sys.types`. For CLR types, even though the `system_type_name` column returns `NULL`, this column returns the value `240`. Not nullable. |
| `system_type_name` | **nvarchar(256)** | Contains the name and arguments (such as length, precision, scale), specified for the data type of the column. If the data type is a user-defined alias type, the underlying system type is specified here. If it's a CLR user-defined type, `NULL` is returned in this column. Nullable. |
| `max_length` | **smallint** | Maximum length (in bytes) of the column.<br /><br />`-1` = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br />For **text** columns, the `max_length` value is `16` or the value set by `sp_tableoption 'text in row'`. Not nullable. |
| `precision` | **tinyint** | Precision of the column if numeric-based. Otherwise returns `0`. Not nullable. |
| `scale` | **tinyint** | Scale of column if numeric-based. Otherwise returns `0`. Not nullable. |
| `collation_name` | **sysname** | Name of the collation of the column if character-based. Otherwise returns `NULL`. Nullable. |
| `user_type_id` | **int** | For CLR and alias types, contains the `user_type_id` of the data type of the column as specified in `sys.types`. Otherwise is `NULL`. Nullable. |
| `user_type_database` | **sysname** | For CLR and alias types, contains the name of the database in which the type is defined. Otherwise is `NULL`. Nullable. |
| `user_type_schema` | **sysname** | For CLR and alias types, contains the name of the schema in which the type is defined. Otherwise is `NULL`. Nullable. |
| `user_type_name` | **sysname** | For CLR and alias types, contains the name of the type. Otherwise is `NULL`. Nullable. |
| `assembly_qualified_type_name` | **nvarchar(4000)** | For CLR types, returns the name of the assembly and class defining the type. Otherwise is `NULL`. Nullable. |
| `xml_collection_id` | **int** | Contains the `xml_collection_id` of the data type of the column as specified in `sys.columns`. This column returns `NULL` if the type returned isn't associated with an XML schema collection. Nullable. |
| `xml_collection_database` | **sysname** | Contains the database in which the XML schema collection associated with this type is defined. This column returns `NULL` if the type returned isn't associated with an XML schema collection. Nullable. |
| `xml_collection_schema` | **sysname** | Contains the schema in which the XML schema collection associated with this type is defined. This column returns `NULL` if the type returned isn't associated with an XML schema collection. Nullable. |
| `xml_collection_name` | **sysname** | Contains the name of the XML schema collection associated with this type. This column returns `NULL` if the type returned isn't associated with an XML schema collection. Nullable. |
| `is_xml_document` | **bit** | Returns `1` if the returned data type is XML and that type is guaranteed to be a complete XML document (including a root node), as opposed to an XML fragment. Otherwise returns `0`. Not nullable. |
| `is_case_sensitive` | **bit** | Returns `1` if the column is a case-sensitive string type and `0` if it's not. Not nullable. |
| `is_fixed_length_clr_type` | **bit** | Returns `1` if the column is a fixed-length CLR type and `0` if it's not. Not nullable. |
| `source_server` | **sysname** | Name of the originating server returned by the column in this result (if it originates from a remote server). The name is given as it appears in `sys.servers`. Returns `NULL` if the column originates on the local server or if it can't be determined which server it originates on. Is only populated if browsing information is requested. Nullable. |
| `source_database` | **sysname** | Name of the originating database returned by the column in this result. Returns `NULL` if the database can't be determined. Is only populated if browsing information is requested. Nullable. |
| `source_schema` | **sysname** | Name of the originating schema returned by the column in this result. Returns `NULL` if the schema can't be determined. Is only populated if browsing information is requested. Nullable. |
| `source_table` | **sysname** | Name of the originating table returned by the column in this result. Returns `NULL` if the table can't be determined. Is only populated if browsing information is requested. Nullable. |
| `source_column` | **sysname** | Name of the originating column returned by the result column. Returns `NULL` if the column can't be determined. Is only populated if browsing information is requested. Nullable. |
| `is_identity_column` | **bit** | Returns `1` if the column is an identity column and `0` if not. Returns `NULL` if it can't be determined that the column is an identity column. Nullable. |
| `is_part_of_unique_key` | **bit** | Returns `1` if the column is part of a unique index (including unique and primary constraint) and `0` if not. Returns `NULL` if it can't be determined that the column is part of a unique index. Only populated if browsing information is requested. Nullable. |
| `is_updateable` | **bit** | Returns `1` if the column is updateable and `0` if not. Returns `NULL` if it can't be determined that the column is updateable. Nullable. |
| `is_computed_column` | **bit** | Returns `1` if the column is a computed column and `0` if not. Returns `NULL` if it can't be determined that the column is a computed column. Nullable. |
| `is_sparse_column_set` | **bit** | Returns `1` if the column is a sparse column and `0` if not. Returns `NULL` if it can't be determined that the column is part of a sparse column set. Nullable. |
| `ordinal_in_order_by_list` | **smallint** | Position of this column in `ORDER BY` list. Returns `NULL` if the column doesn't appear in the `ORDER BY` list or if the `ORDER BY` list can't be uniquely determined. Nullable. |
| `order_by_list_length` | **smallint** | Length of the `ORDER BY` list. Returns `NULL` if there's no `ORDER BY` list or if the `ORDER BY` list can't be uniquely determined. This value is the same for all rows returned by `sp_describe_first_result_set`. Nullable. |
| `order_by_is_descending` | **smallint** | If the `ordinal_in_order_by_list` isn't `NULL`, the `order_by_is_descending` column reports the direction of the `ORDER BY` clause for this column. Otherwise it reports `NULL`. Nullable. |
| `tds_type_id` | **int** | For internal use. Not nullable. |
| `tds_length` | **int** | For internal use. Not nullable. |
| `tds_collation_id` | **int** | For internal use. Nullable. |
| `tds_collation_sort_id` | **tinyint** | For internal use. Nullable. |

## Remarks

`sp_describe_first_result_set` guarantees that if the procedure returns the first result-set metadata for (a hypothetical) batch A and if that batch (A) is then executed, the batch either:

- raises an optimization-time error
- raises a run-time error
- returns no result set
- returns a first result set with the same metadata described by `sp_describe_first_result_set`

The name, nullability, and data type can differ. If `sp_describe_first_result_set` returns an empty result set, the guarantee is that the batch execution returns no-result sets.

This guarantee presumes there are no relevant schema changes on the server. Relevant schema changes on the server don't include creating a temporary tables or table variables in the batch A between the time that `sp_describe_first_result_set` is called and the time that the result set is returned during execution, including schema changes made by batch B.

`sp_describe_first_result_set` returns an error in any of the following cases:

- The input *@tsql* isn't a valid [!INCLUDE [tsql](../../includes/tsql-md.md)] batch. Validity is determined by parsing and analyzing the [!INCLUDE [tsql](../../includes/tsql-md.md)] batch. Any errors caused by the batch during query optimization or during execution aren't considered when determining whether the [!INCLUDE [tsql](../../includes/tsql-md.md)] batch is valid.

- *@params* isn't `NULL` and contains a string that isn't a syntactically valid declaration string for parameters, or if it contains a string that declares any parameter more than one time.

- The input [!INCLUDE [tsql](../../includes/tsql-md.md)] batch declares a local variable of the same name as a parameter declared in *@params*.

- The statement uses a temporary table.

- The query includes the creation of a permanent table that is then queried.

If all other checks succeed, all possible control flow paths inside the input batch are considered. This takes into account all control flow statements (`GOTO`, `IF`/`ELSE`, `WHILE`, and [!INCLUDE [tsql](../../includes/tsql-md.md)] `TRY`/`CATCH` blocks) as well as any procedures, dynamic [!INCLUDE [tsql](../../includes/tsql-md.md)] batches, or triggers invoked from the input batch by an `EXEC` statement, a DDL statement that causes DDL triggers to be fired, or a DML statement that causes triggers to be fired on a target table or on a table that is modified because of cascading action on a foreign key constraint. At some point, as with many possible control paths, an algorithm stops.

For each control flow path, the first statement (if any) that returns a result set is determined by `sp_describe_first_result_set`.

When multiple possible first statements are found in a batch, their results can differ in number of columns, column name, nullability, and data type. How these differences are handled is described in more detail here:

- If the number of columns differs, an error is thrown and no result is returned.

- If the column name differs, the column name returned is set to `NULL`.

- If the nullability differs, the nullability returned allows `NULL`.

- If the data type differs, an error is thrown and no result is returned except for the following cases:

  - **varchar(a)** to **varchar(a')** where a' > a.
  - **varchar(a)** to **varchar(max)**
  - **nvarchar(a)** to **nvarchar(a')** where a' > a.
  - **nvarchar(a)** to **nvarchar(max)**
  - **varbinary(a)** to **varbinary(a')** where a' > a.
  - **varbinary(a)** to **varbinary(max)**

`sp_describe_first_result_set` doesn't support indirect recursion.

## Permissions

Requires permission to execute the *@tsql* argument.

## Examples

### Typical examples

#### A. Basic example

The following example describes the result set returned from a single query.

```sql
EXEC sp_describe_first_result_set @tsql = N'SELECT object_id, name, type_desc FROM sys.indexes';
```

The following example shows the result set returned from a single query that contains a parameter.

```sql
EXEC sp_describe_first_result_set @tsql = N'
SELECT object_id, name, type_desc
FROM sys.indexes
WHERE object_id = @id1',
@params = N'@id1 int';
```

#### B. Browse mode examples

The following three examples illustrate the key difference between the different browse information modes. Only the relevant columns are included in the query results.

Example using `0`, indicating no information is returned.

```sql
CREATE TABLE dbo.t (
    a INT PRIMARY KEY,
    b1 INT
);
GO

CREATE VIEW dbo.v AS
SELECT b1 AS b2
FROM dbo.t;
GO

EXEC sp_describe_first_result_set N'SELECT b2 AS b3 FROM dbo.v', NULL, 0;
```

Here's a partial result set.

| is_hidden | column_ordinal | name | source_schema | source_table | source_column | is_part_of_unique_key |
| --- | --- | --- | --- | --- | --- | --- |
| `0` | 1 | b3 | `NULL` | `NULL` | `NULL` | `NULL` |

Example using 1 indicating it returns information as if it includes a FOR BROWSE option on the query.

```sql
EXEC sp_describe_first_result_set N'SELECT b2 AS b3 FROM v', NULL, 1;
```

Here's a partial result set.

| is_hidden | column_ordinal | name | source_schema | source_table | source_column | is_part_of_unique_key |
| --- | --- | --- | --- | --- | --- | --- |
| `0` | 1 | b3 | dbo | t | B1 | 0 |
| `1` | 2 | a | dbo | t | a | 1 |

Example using 2 indicating analyzed as if you're preparing a cursor.

```sql
EXEC sp_describe_first_result_set N'SELECT b2 AS b3 FROM v', NULL, 2;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

| is_hidden | column_ordinal | name | source_schema | source_table | source_column | is_part_of_unique_key |
| --- | --- | --- | --- | --- | --- | --- |
| `0` | 1 | B3 | dbo | v | B2 | 0 |
| `1` | 2 | ROWSTAT | `NULL` | `NULL` | `NULL` | 0 |

### C. Store results in a table

In some scenarios, you need to put the results of the `sp_describe_first_result_set` procedure in a table so your can further process the schema.

First you need to create a table that matches the output of the `sp_describe_first_result_set` procedure:

```sql
CREATE TABLE #frs (
    is_hidden BIT NOT NULL,
    column_ordinal INT NOT NULL,
    name SYSNAME NULL,
    is_nullable BIT NOT NULL,
    system_type_id INT NOT NULL,
    system_type_name NVARCHAR(256) NULL,
    max_length SMALLINT NOT NULL,
    precision TINYINT NOT NULL,
    scale TINYINT NOT NULL,
    collation_name SYSNAME NULL,
    user_type_id INT NULL,
    user_type_database SYSNAME NULL,
    user_type_schema SYSNAME NULL,
    user_type_name SYSNAME NULL,
    assembly_qualified_type_name NVARCHAR(4000),
    xml_collection_id INT NULL,
    xml_collection_database SYSNAME NULL,
    xml_collection_schema SYSNAME NULL,
    xml_collection_name SYSNAME NULL,
    is_xml_document BIT NOT NULL,
    is_case_sensitive BIT NOT NULL,
    is_fixed_length_clr_type BIT NOT NULL,
    source_server SYSNAME NULL,
    source_database SYSNAME NULL,
    source_schema SYSNAME NULL,
    source_table SYSNAME NULL,
    source_column SYSNAME NULL,
    is_identity_column BIT NULL,
    is_part_of_unique_key BIT NULL,
    is_updateable BIT NULL,
    is_computed_column BIT NULL,
    is_sparse_column_set BIT NULL,
    ordinal_in_order_by_list SMALLINT NULL,
    order_by_list_length SMALLINT NULL,
    order_by_is_descending SMALLINT NULL,
    tds_type_id INT NOT NULL,
    tds_length INT NOT NULL,
    tds_collation_id INT NULL,
    tds_collation_sort_id TINYINT NULL
);
```

When you create a table, you can store the schema of some query in that table.

```sql
DECLARE @tsql NVARCHAR(MAX) = 'select top 0 * from sys.credentials';

INSERT INTO #frs
EXEC sys.sp_describe_first_result_set @tsql;

SELECT * FROM #frs;
```

### Examples of problems

The following examples use two tables for all examples. Execute the following statements to create the example tables.

```sql
CREATE TABLE dbo.t1 (
    a INT NULL,
    b VARCHAR(10) NULL,
    c NVARCHAR(10) NULL
);

CREATE TABLE dbo.t2 (
    a SMALLINT NOT NULL,
    d VARCHAR(20) NOT NULL,
    e INT NOT NULL
);
```

#### Error because the number of columns differ

Number of columns in possible first result sets differ in this example.

```sql
EXEC sp_describe_first_result_set @tsql = N'
IF (1 = 1)
    SELECT a FROM t1;
ELSE
    SELECT a, b FROM t1;

SELECT * FROM t; -- Ignored, not a possible first result set.';
```

#### Error because the data types differ

Columns types differ in different possible first result sets.

```sql
EXEC sp_describe_first_result_set @tsql = N'
IF (1 = 1)
    SELECT a FROM t1;
ELSE
    SELECT a FROM t2;';
```

This results in an error of mismatching types (**int** vs. **smallint**).

#### Column name can't be determined

Columns in possible first result sets differ by length for same variable length type, nullability, and column names:

```sql
EXEC sp_describe_first_result_set @tsql = N'
IF (1 = 1)
    SELECT b FROM t1;
ELSE
    SELECT d FROM t2;';
```

Here's a partial result set.

| Column | Value |
| --- | --- |
| `name` | Unknown column name |
| `system_type_name` | `varchar` |
| `max_length` | `20` |
| `is_nullable` | `1` |

#### Column name forced to be identical through aliasing

Same as previous, but columns have the same name through column aliasing.

```sql
EXEC sp_describe_first_result_set @tsql = N'
IF (1 = 1)
    SELECT b FROM t1;
ELSE
    SELECT d AS b FROM t2;';
```

Here's a partial result set.

| Column | Value |
| --- | --- |
| `name` | `b` |
| `system_type_name` | `varchar` |
| `max_length` | `20` |
| `is_nullable` | `1` |

#### Error because column types can't be matched

The columns types differ in different possible first result sets.

```sql
EXEC sp_describe_first_result_set @tsql = N'
IF (1 = 1)
    SELECT b FROM t1;
ELSE
    SELECT c FROM t1;';
```

This results in an error of mismatching types (**varchar(10)** vs. **nvarchar(10)**).

#### Result set can return an error

First result set is either error or result set.

```sql
EXEC sp_describe_first_result_set @tsql = N'
IF (1 = 1)
    RAISERROR(''Some Error'', 16 , 1);
ELSE
    SELECT a FROM t1;
SELECT e FROM t2; -- Ignored, not a possible first result set.';
```

Here's a partial result set.

| Column | Value |
| --- | --- |
| `name` | `a` |
| `system_type_name` | `int` |
| `is_nullable` | `1` |

#### Some code paths return no results

First result set is either null or a result set.

```sql
EXEC sp_describe_first_result_set @tsql = N'
IF (1 = 1)
    RETURN;
SELECT a FROM t1;';
```

Here's a partial result set.

| Column | Value |
| --- | --- |
| `name` | `a` |
| `system_type_name` | `int` |
| `is_nullable` | `1` |

#### Result from dynamic SQL

First result set is dynamic SQL that is discoverable because it's a literal string.

```sql
EXEC sp_describe_first_result_set @tsql = N'
EXEC(N''SELECT a FROM t1'');';
```

Here's a partial result set.

| Column | Value |
| --- | --- |
| `name` | `a` |
| `system_type_name` | `int` |
| `is_nullable` | `1` |

#### Result failure from dynamic SQL

First result set is undefined because of dynamic SQL.

```sql
EXEC sp_describe_first_result_set @tsql = N'
DECLARE @SQL NVARCHAR(max);
SET @SQL = N''SELECT a FROM t1 WHERE 1 = 1'';
IF (1 = 1)
    SET @SQL += N'' AND e > 10'';
EXEC(@SQL);';
```

This results in an error. The result isn't discoverable because of the dynamic SQL.

#### Result set specified by user

First result set is specified manually by user.

```sql
EXEC sp_describe_first_result_set @tsql =
N'
DECLARE @SQL NVARCHAR(max);
SET @SQL = N''SELECT a FROM t1 WHERE 1 = 1'';
IF (1 = 1)
    SET @SQL += N'' AND e > 10'';
EXEC(@SQL)
    WITH RESULT SETS (
        (Column1 BIGINT NOT NULL)
    );';
```

Here's a partial result set.

| Column | Value |
| --- | --- |
| `name` | `Column1` |
| `system_type_name` | `bigint` |
| `is_nullable` | `0` |

#### Error caused by an ambiguous result set

This example assumes that another user named `user1` has a table named `t1` in the default schema `s1` with columns (`a int NOT NULL`).

```sql
EXEC sp_describe_first_result_set @tsql = N'
    IF (@p > 0)
    EXECUTE AS USER = ''user1'';
    SELECT * FROM t1;',
@params = N'@p int';
```

This code results in an `Invalid object name` error. `t1` can be either `dbo.t1` or `s1.t1`, each with a different number of columns.

#### Result even with ambiguous result set

Use the same assumptions as the previous example.

```sql
EXEC sp_describe_first_result_set @tsql =
N'
    IF (@p > 0)
    EXECUTE AS USER = ''user1'';
    SELECT a FROM t1;';
```

Here's a partial result set.

| Column | Value |
| --- | --- |
| `name` | `a` |
| `system_type_name` | `int` |
| `is_nullable` | `1` |

Both `dbo.t1.a` and `s1.t1.a` have type **int**, and different nullability.

## Related content

- [sp_describe_undeclared_parameters (Transact-SQL)](sp-describe-undeclared-parameters-transact-sql.md)
- [sys.dm_exec_describe_first_result_set (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql.md)
- [sys.dm_exec_describe_first_result_set_for_object (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-describe-first-result-set-for-object-transact-sql.md)
