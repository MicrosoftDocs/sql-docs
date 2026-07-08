---
title: "sys.dm_exec_describe_first_result_set (Transact-SQL)"
description: sys.dm_exec_describe_first_result_set describes the metadata of the first result set for a statement as a parameter.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/12/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.dm_exec_describe_first_result_set"
  - "sys.dm_exec_describe_first_result_set_TSQL"
helpviewer_keywords:
  - "sys.dm_exec_describe_first_result_set catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.dm_exec_describe_first_result_set (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

This dynamic management function takes a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement as a parameter and returns the metadata for the first result set of the statement.

`sys.dm_exec_describe_first_result_set` returns the same result set definition as [sys.dm_exec_describe_first_result_set_for_object](sys-dm-exec-describe-first-result-set-for-object-transact-sql.md) and is similar to [sp_describe_first_result_set](../system-stored-procedures/sp-describe-first-result-set-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.dm_exec_describe_first_result_set(@tsql , @params , @include_browse_information)
```

## Arguments

#### *@tsql*

One or more [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. The *@tsql* batch can be **nvarchar(*n*)** or **nvarchar(max)**.

#### *@params*

*@params* provides a declaration string for parameters for the [!INCLUDE [tsql](../../includes/tsql-md.md)] batch, similar to `sp_executesql`. Parameters can be **nvarchar(*n*)** or **nvarchar(max)**.

A single string that contains the definitions of all parameters that are embedded in the *@tsql* batch. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates additional parameter definitions. Every parameter specified in stmt must be defined in *@params*. If the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch in the statement doesn't contain parameters, *@params* isn't required. `NULL` is the default value for this parameter.

#### *@include_browse_information*

If set to 1, each query is analyzed as if it has a `FOR BROWSE` option on the query. The result includes extra key columns and source table information.

## Table returned

The function returns this common metadata as a result set. Each row corresponds to a column in the results metadata and describes the type and nullability of the column in the format shown in the following table. If the first statement doesn't exist for every control path, the function returns a result set with zero rows.

| Column name | Data type | Description |
| --- | --- | --- |
| `is_hidden` | **bit** | Specifies that the column is an extra column, added for browsing and informational purposes, that doesn't actually appear in the result set. |
| `column_ordinal` | **int** | Contains the ordinal position of the column in the result set. Position of the first column is specified as `1`. |
| `name` | **sysname** | Contains the name of the column if a name can be determined. If not, it's `NULL`. |
| `is_nullable` | **bit** | Contains the following values:<br /><br />Returns `1` if column allows `NULL` values.<br /><br />Returns `0` if the column doesn't allow `NULL` values.<br /><br />Returns `1` if it can't be determined that the column allows `NULL` values. |
| `system_type_id` | **int** | Contains the `system_type_id` of the column data type as specified in `sys.types`. For CLR types, even though the `system_type_name` column returns `NULL`, this column returns `240`. |
| `system_type_name` | **nvarchar(256)** | Contains the name and arguments (such as length, precision, scale), specified for the data type of the column.<br /><br />If data type is a user-defined alias type, the underlying system type is specified here.<br /><br />If data type is a CLR user-defined type, `NULL` is returned in this column. |
| `max_length` | **smallint** | Maximum length (in bytes) of the column.<br /><br />`-1` = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br />For **text** columns, the `max_length` value is `16`, or the value set by `sp_tableoption 'text in row'`. |
| `precision` | **tinyint** | Precision of the column if numeric-based. Otherwise returns `0`. |
| `scale` | **tinyint** | Scale of column if numeric-based. Otherwise returns `0`. |
| `collation_name` | **sysname** | Name of the collation of the column if character-based. Otherwise returns `NULL`. |
| `user_type_id` | **int** | For CLR and alias types, contains the `user_type_id` of the data type of the column as specified in `sys.types`. Otherwise is `NULL`. |
| `user_type_database` | **sysname** | For CLR and alias types, contains the name of the database in which the type is defined. Otherwise is `NULL`. |
| `user_type_schema` | **sysname** | For CLR and alias types, contains the name of the schema in which the type is defined. Otherwise is `NULL`. |
| `user_type_name` | **sysname** | For CLR and alias types, contains the name of the type. Otherwise is `NULL`. |
| `assembly_qualified_type_name` | **nvarchar(4000)** | For CLR types, returns the name of the assembly and class defining the type. Otherwise is `NULL`. |
| `xml_collection_id` | **int** | Contains the `xml_collection_id` of the data type of the column as specified in `sys.columns`. This column returns `NULL` if the type returned isn't associated with an XML schema collection. |
| `xml_collection_database` | **sysname** | Contains the database in which the XML schema collection associated with this type is defined. This column returns `NULL` if the type returned isn't associated with an XML schema collection. |
| `xml_collection_schema` | **sysname** | Contains the schema in which the XML schema collection associated with this type is defined. This column returns `NULL` if the type returned isn't associated with an XML schema collection. |
| `xml_collection_name` | **sysname** | Contains the name of the XML schema collection associated with this type. This column returns `NULL` if the type returned isn't associated with an XML schema collection. |
| `is_xml_document` | **bit** | Returns `1` if the returned data type is XML and that type is guaranteed to be a complete XML document (including a root node, as opposed to an XML fragment). Otherwise returns `0`. |
| `is_case_sensitive` | **bit** | Returns `1` if the column is of a case-sensitive string type. Returns `0` if it isn't. |
| `is_fixed_length_clr_type` | **bit** | Returns `1` if the column is of a fixed-length CLR type. Returns `0` if it isn't. |
| `source_server` | **sysname** | Name of the originating server (if it originates from a remote server). The name is given as it appears in `sys.servers`. Returns `NULL` if the column originates on the local server or if it can't be determined which server it originates on. Is only populated if browsing information is requested. |
| `source_database` | **sysname** | Name of the originating database returned by the column in this result. Returns `NULL` if the database can't be determined. Is only populated if browsing information is requested. |
| `source_schema` | **sysname** | Name of the originating schema returned by the column in this result. Returns `NULL` if the schema can't be determined. Is only populated if browsing information is requested. |
| `source_table` | **sysname** | Name of the originating table returned by the column in this result. Returns `NULL` if the table can't be determined. Is only populated if browsing information is requested. |
| `source_column` | **sysname** | Name of the originating column returned by the result column. Returns `NULL` if the column can't be determined. Is only populated if browsing information is requested. |
| `is_identity_column` | **bit** | Returns `1` if the column is an identity column and 0 if not. Returns `NULL` if it can't be determined that the column is an identity column. |
| `is_part_of_unique_key` | **bit** | Returns `1` if the column is part of a unique index (including unique and primary constraints) and 0 if it isn't. Returns `NULL` if it can't be determined that the column is part of a unique index. Is only populated if browsing information is requested. |
| `is_updateable` | **bit** | Returns `1` if the column is updateable and 0 if not. Returns `NULL` if it can't be determined that the column is updateable. |
| `is_computed_column` | **bit** | Returns `1` if the column is a computed column and 0 if not. Returns `NULL` if it can't be determined if the column is a computed column. |
| `is_sparse_column_set` | **bit** | Returns `1` if the column is a sparse column and 0 if not. Returns `NULL` if it can't be determined that the column is a part of a sparse column set. |
| `ordinal_in_order_by_list` | **smallint** | The position of this column is in `ORDER BY` list. Returns `NULL` if the column doesn't appear in the `ORDER BY` list, or if the `ORDER BY` list can't be uniquely determined. |
| `order_by_list_length` | **smallint** | The length of the `ORDER BY` list. `NULL` is returned if there's no `ORDER BY` list or if the `ORDER BY` list can't be uniquely determined. This value is the same for all rows returned by `sp_describe_first_result_set`. |
| `order_by_is_descending` | **smallint** | If the `ordinal_in_order_by_list` isn't `NULL`, the `order_by_is_descending` column reports the direction of the `ORDER BY` clause for this column. Otherwise it reports `NULL`. |
| `error_number` | **int** | Contains the error number returned by the function. If no error occurred, the column contains `NULL`. |
| `error_severity` | **int** | Contains the severity returned by the function. If no error occurred, the column contains `NULL`. |
| `error_state` | **int** | Contains the state message returned by the function. If no error occurred, the column contains `NULL`. |
| `error_message` | **nvarchar(4096)** | Contains the message returned by the function. If no error occurred, the column contains `NULL`. |
| `error_type` | **int** | Contains an integer representing the error being returned. Maps to `error_type_desc`. See the list under remarks. |
| `error_type_desc` | **nvarchar(60)** | Contains a short uppercase string representing the error being returned. Maps to `error_type`. See the list under remarks. |

## Remarks

This function uses the same algorithm as `sp_describe_first_result_set`. For more information, see [sp_describe_first_result_set](../system-stored-procedures/sp-describe-first-result-set-transact-sql.md).

The following table lists the error types and their descriptions.

| `error_type` | `error_type` | Description |
| --- | --- | --- |
| `1` | `MISC` | All errors that aren't otherwise described. |
| `2` | `SYNTAX` | A syntax error occurred in the batch. |
| `3` | `CONFLICTING_RESULTS` | The result couldn't be determined because of a conflict between two possible first statements. |
| `4` | `DYNAMIC_SQL` | The result couldn't be determined because of dynamic SQL that could potentially return the first result. |
| `5` | `CLR_PROCEDURE` | The result couldn't be determined because a CLR stored procedure could potentially return the first result. |
| `6` | `CLR_TRIGGER` | The result couldn't be determined because a CLR trigger could potentially return the first result. |
| `7` | `EXTENDED_PROCEDURE` | The result couldn't be determined because an extended stored procedure could potentially return the first result. |
| `8` | `UNDECLARED_PARAMETER` | The result couldn't be determined because the data type of one or more of the result set's columns potentially depends on an undeclared parameter. |
| `9` | `RECURSION` | The result couldn't be determined because the batch contains a recursive statement. |
| `10` | `TEMPORARY_TABLE` | The result couldn't be determined because the batch contains a temporary table and isn't supported by `sp_describe_first_result_set` . |
| `11` | `UNSUPPORTED_STATEMENT` | The result couldn't be determined because the batch contains a statement that isn't supported by `sp_describe_first_result_set` (for example, `FETCH`, `REVERT`, etc.). |
| `12` | `OBJECT_TYPE_NOT_SUPPORTED` | The `@object_id` passed to the function isn't supported (that is, it isn't a stored procedure). |
| `13` | `OBJECT_DOES_NOT_EXIST` | The `@object_id` passed to the function isn't found in the system catalog. |

## Permissions

Requires permission to execute the *@tsql* argument.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

You can adapt the examples in the [sp_describe_first_result_set](../system-stored-procedures/sp-describe-first-result-set-transact-sql.md) article to use `sys.dm_exec_describe_first_result_set`.

### A. Return information about a single Transact-SQL statement

The following code returns information about the results of a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement.

```sql
USE @AdventureWorks2025;

SELECT *
FROM sys.dm_exec_describe_first_result_set (
    N'SELECT object_id, name, type_desc FROM sys.indexes', null, 0
);
```

### B. Return information about a procedure

The following example creates a stored procedure named `pr_TestProc` that returns two result sets. Then the example demonstrates that `sys.dm_exec_describe_first_result_set` returns information about the first result set in the procedure.

```sql
USE @AdventureWorks2025;
GO

CREATE PROC Production.TestProc
AS
    SELECT Name, ProductID, Color
    FROM Production.Product;

    SELECT Name, SafetyStockLevel, SellStartDate
    FROM Production.Product;
GO

SELECT *
FROM sys.dm_exec_describe_first_result_set('Production.TestProc', NULL, 0);
```

### C. Return metadata from a batch that contains multiple statements

The following example evaluates a batch that contains two [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. The result set describes the first result set returned.

```sql
USE AdventureWorks2025;
GO

SELECT *
FROM sys.dm_exec_describe_first_result_set(
    N'SELECT CustomerID, TerritoryID, AccountNumber FROM Sales.Customer WHERE CustomerID = @CustomerID;SELECT * FROM Sales.SalesOrderHeader;',
    N'@CustomerID int',
    0
) AS a;
```

## Related content

- [sp_describe_first_result_set](../system-stored-procedures/sp-describe-first-result-set-transact-sql.md)
- [sp_describe_undeclared_parameters](../system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md)
- [sys.dm_exec_describe_first_result_set_for_object](sys-dm-exec-describe-first-result-set-for-object-transact-sql.md)
