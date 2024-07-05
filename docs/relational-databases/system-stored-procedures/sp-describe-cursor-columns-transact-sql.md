---
title: "sp_describe_cursor_columns (Transact-SQL)"
description: sp_describe_cursor_columns reports the attributes of the columns in the result set of a server cursor.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_describe_cursor_columns"
  - "sp_describe_cursor_columns_TSQL"
helpviewer_keywords:
  - "sp_describe_cursor_columns"
dev_langs:
  - "TSQL"
---
# sp_describe_cursor_columns (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports the attributes of the columns in the result set of a server cursor.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_describe_cursor_columns
    [ @cursor_return = ] cursor_return OUTPUT
    , [ @cursor_source = ] { N'local' | N'global' | N'*cursor_source*' }
    , [ @cursor_identity = ] N'cursor_identity'
[ ; ]
```

## Arguments

#### [ @cursor_return = ] *cursor_return* OUTPUT

The name of a declared cursor variable to receive the cursor output. *@cursor_return* is an OUTPUT parameter of type **int**, with no default, and must not be associated with any cursors at the time `sp_describe_cursor_columns` is called. The cursor returned is a scrollable, dynamic, read-only cursor.

#### [ @cursor_source = ] { N'local' | N'global' | N'*cursor_source*' }

Specifies whether the cursor being reported on is specified, by using the name of a *local* cursor, a *global* cursor, or a cursor variable. *@cursor_source* is **nvarchar(30)**, with no default.

#### [ @cursor_identity = ] N'*cursor_identity*'

The name of a cursor created by a `DECLARE CURSOR` statement. *@cursor_identity* is **nvarchar(128)**, with no default.

- If cursor has the `LOCAL` keyword, or is defaulted to `LOCAL`, *@cursor_identity* is `local`.

- If cursor has the `GLOBAL` keyword, or is defaulted to `GLOBAL`, *@cursor_identity* is `global`. *@cursor_identity* can also be the name of an API server cursor opened by an ODBC application, and then named by calling `SQLSetCursorName`.

- Otherwise, *@cursor_identity* is the name of a cursor variable associated with an open cursor.

## Return code values

None.

## Cursors returned

`sp_describe_cursor_columns` encapsulates its report as a [!INCLUDE [tsql](../../includes/tsql-md.md)] `cursor` output parameter. This enables [!INCLUDE [tsql](../../includes/tsql-md.md)] batches, stored procedures, and triggers to work with the output one row at a time. This also means that the procedure can't be called directly from database API functions. The `cursor` output parameter must be bound to a program variable, but the database APIs don't support binding `cursor` parameters or variables.

The following table shows the format of the cursor returned by using `sp_describe_cursor_columns`.

| Column name | Data type | Description |
| --- | --- | --- |
| `column_name` | **sysname** | Name assigned to the result set column. The column is `NULL` if the column was specified without an accompanying `AS` clause.<br /><br />Nullable. |
| `ordinal_position` | **int** | Relative position of the column from the leftmost column in the result set. The first column is in position `0`. |
| `column_characteristics_flags` | **int** | A bitmask that indicates the information stored in `DBCOLUMNFLAGS` in OLE DB. Can be one or a combination of the following values:<br /><br />`1` = Bookmark<br />`2` = Fixed length<br />`4` = Nullable<br />`8` = Row versioning<br />`16` = Updatable column (set for projected columns of a cursor that's no `FOR UPDATE` clause and, if there's such a column, can be only one per cursor).<br /><br />When bit values are combined, the characteristics of the combined bit values apply. For example, if the bit value is `6`, the column is a fixed-length (`2`), nullable (`4`) column. |
| `column_size` | **int** | Maximum possible size for a value in this column. |
| `data_type_sql` | **smallint** | Number that indicates the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type of the column. |
| `column_precision` | **tinyint** | Maximum precision of the column as per the `bPrecision` value in OLE DB. |
| `column_scale` | **tinyint** | Number of digits to the right of the decimal point for the **numeric** or **decimal** data types as per the `bScale` value in OLE DB. |
| `order_position` | **int** | If the column participates in the ordering of the result set, the position of the column in the order key relative to the leftmost column. |
| `order_direction` | **varchar(1)** | `A` = The column is in the order key and the ordering is ascending.<br />`D` = The column is in the order key and the ordering is descending.<br /><br />`NULL` = The column doesn't participate in ordering.<br /><br />Nullable. |
| `hidden_column` | **smallint** | `0` = this column appears in the select list.<br /><br />`1` = Reserved for future use. |
| `columnid` | **int** | Column ID of the base column. If the result set column was built from an expression, `columnid` is `-1`. |
| `objectid` | **int** | Object ID of the object or base table that is supplying the column. If the result set column was built from an expression, `objectid` is `-1`. |
| `dbid` | **int** | ID of the database that contains the base table that is supplying the column. If the result set column was built from an expression, `dbid` is `-1`. |
| `dbname` | **sysname** | Name of the database that contains the base table that is supplying the column. If the result set column was built from an expression, dbname is `NULL`.<br /><br />Nullable. |

## Remarks

`sp_describe_cursor_columns` describes the attributes of the columns in the result set of a server cursor, such as the name and data type of each cursor. Use `sp_describe_cursor` for a description of the global attributes of the server cursor. Use `sp_describe_cursor_tables` for a report of the base tables referenced by the cursor. To obtain a report of the [!INCLUDE [tsql](../../includes/tsql-md.md)] server cursors visible on the connection, use `sp_cursor_list`.

## Permissions

Requires membership in the **public** role.

## Examples

The following example opens a global cursor and uses `sp_describe_cursor_columns` to report on the columns used in the cursor.

```sql
USE AdventureWorks2022;
GO
-- Declare and open a global cursor.
DECLARE abc CURSOR KEYSET FOR
    SELECT LastName
    FROM Person.Person;
GO
OPEN abc;

-- Declare a cursor variable to hold the cursor output variable
-- from sp_describe_cursor_columns.
DECLARE @Report CURSOR;

-- Execute sp_describe_cursor_columns into the cursor variable.
EXEC master.dbo.sp_describe_cursor_columns
    @cursor_return = @Report OUTPUT,
    @cursor_source = N'global',
    @cursor_identity = N'abc';

-- Fetch all the rows from the sp_describe_cursor_columns output cursor.
FETCH NEXT from @Report;
WHILE (@@FETCH_STATUS <> -1)
BEGIN
    FETCH NEXT from @Report;
END

-- Close and deallocate the cursor from sp_describe_cursor_columns.
CLOSE @Report;
DEALLOCATE @Report;
GO
-- Close and deallocate the original cursor.
CLOSE abc;
DEALLOCATE abc;
GO
```

## Related content

- [Cursors (SQL Server)](../cursors.md)
- [CURSOR_STATUS (Transact-SQL)](../../t-sql/functions/cursor-status-transact-sql.md)
- [DECLARE CURSOR (Transact-SQL)](../../t-sql/language-elements/declare-cursor-transact-sql.md)
- [sp_describe_cursor (Transact-SQL)](sp-describe-cursor-transact-sql.md)
- [sp_cursor_list (Transact-SQL)](sp-cursor-list-transact-sql.md)
- [sp_describe_cursor_tables (Transact-SQL)](sp-describe-cursor-tables-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
