---
title: "sp_describe_cursor_tables (Transact-SQL)"
description: sp_describe_cursor_tables reports the objects or base tables referenced by a server cursor.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_describe_cursor_tables_TSQL"
  - "sp_describe_cursor_tables"
helpviewer_keywords:
  - "sp_describe_cursor_tables"
dev_langs:
  - "TSQL"
---
# sp_describe_cursor_tables (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports the objects or base tables referenced by a server cursor.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_describe_cursor_tables
    [ @cursor_return = ] cursor_return OUTPUT
    , [ @cursor_source = ] { N'local' | N'global' | N'variable' }
    , [ @cursor_identity = ] N'cursor_identity'
[ ; ]
```

## Arguments

#### [ @cursor_return = ] *cursor_return* OUTPUT

The name of a declared cursor variable to receive the cursor output. *@cursor_return* is an OUTPUT **cursor**, with no default, and must not be associated with any cursors at the time `sp_describe_cursor_tables` is called. The cursor returned is a scrollable, dynamic, read-only cursor.

#### [ @cursor_source = ] { N'local' | N'global' | N'variable' }

Specifies whether the cursor being reported on is specified by using the name of a local cursor, a global cursor, or a cursor variable. *@cursor_source* is **nvarchar(30)**, with no default.

#### [ @cursor_identity = ] N'*cursor_identity*'

When *@cursor_source* is `local`, *@cursor_identity* is the name of a cursor created by a `DECLARE CURSOR` statement either having the `LOCAL` keyword, or that defaulted to `LOCAL`.

When *@cursor_source* is `global`, *@cursor_identity* is the name of a cursor created by a `DECLARE CURSOR` statement either having the `GLOBAL` keyword, or that defaulted to `GLOBAL`. *@cursor_identity* can also be the name of an API server cursor opened by an ODBC application that then named the cursor by calling `SQLSetCursorName`.

When *@cursor_source* is `variable`, *@cursor_identity* is the name of a cursor variable associated with an open cursor.

*@cursor_identity* is **nvarchar(128)**, with no default.

## Return code values

None.

## Cursors returned

`sp_describe_cursor_tables` encapsulates its report as a [!INCLUDE [tsql](../../includes/tsql-md.md)] **cursor** output parameter. This enables [!INCLUDE [tsql](../../includes/tsql-md.md)] batches, stored procedures, and triggers to work with the output one row at a time. This also means that the procedure can't be called directly from API functions. The **cursor** output parameter must be bound to a program variable, but the APIs don't support bind **cursor** parameters or variables.

The following table shows the format of the cursor that is returned by `sp_describe_cursor_tables`.

| Column name | Data type | Description |
| --- | --- | --- |
| `table_owner` | **sysname** | User ID of the table owner. |
| `table_name` | **sysname** | Name of the object or base table. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], server cursors always return the user-specified object, not the base tables. |
| `optimizer_hint` | **smallint** | Bitmap that is made up of one or more of the following options:<br /><br />1 = Row-level locking (`ROWLOCK`)<br />4 = Page-level locking (`PAGELOCK`)<br />8 = Table lock (`TABLOCK`)<br />16 = Exclusive table lock (`TABLOCKX`)<br />32 = Update lock (`UPDLOCK`)<br />64 = No lock (`NOLOCK`)<br />128 = Fast first-row option (`FASTFIRST`)<br />4096 = Read repeatable semantic when used with `DECLARE CURSOR` (`HOLDLOCK`)<br /><br />When multiple options are supplied, the system uses the most restrictive. However, `sp_describe_cursor_tables` shows the flags that are specified in the query. |
| `lock_type` | **smallint** | Scroll-lock type requested either explicitly or implicitly for each base table that underlies this cursor. The value can be one of the following options:<br /><br />0 = None<br />1 = Shared<br />3 = Update |
| `server_name` | **sysname, nullable** | Name of the linked server that the table resides on. `NULL` when `OPENQUERY` or `OPENROWSET` are used. |
| `objectid` | **int** | Object ID of the table. 0 when `OPENQUERY` or `OPENROWSET` are used. |
| `dbid` | **int** | ID of the database that the table resides in. 0 when `OPENQUERY` or `OPENROWSET` are used. |
| `dbname` | **sysname**, **nullable** | Name of the database that the table resides in. `NULL` when `OPENQUERY` or `OPENROWSET` are used. |

## Remarks

`sp_describe_cursor_tables` describes the base tables referenced by a server cursor. For a description of the attributes of the result set returned by the cursor, use `sp_describe_cursor_columns`. For a description of the global characteristics of the cursor, such as its scrollability and updatability, use `sp_describe_cursor`. To obtain a report of the [!INCLUDE [tsql](../../includes/tsql-md.md)] server cursors that are visible on the connection, use `sp_cursor_list`.

## Permissions

Requires membership in the **public** role.

## Examples

The following example opens a global cursor and uses `sp_describe_cursor_tables` to report on the tables referenced by the cursor.

```sql
USE AdventureWorks2022;
GO
-- Declare and open a global cursor.
DECLARE abc CURSOR KEYSET FOR
SELECT LastName
FROM Person.Person
WHERE LastName LIKE 'S%';

OPEN abc;
GO
-- Declare a cursor variable to hold the cursor output variable
-- from sp_describe_cursor_tables.
DECLARE @Report CURSOR;

-- Execute sp_describe_cursor_tables into the cursor variable.
EXEC master.dbo.sp_describe_cursor_tables
    @cursor_return = @Report OUTPUT,
    @cursor_source = N'global',
    @cursor_identity = N'abc';

-- Fetch all the rows from the sp_describe_cursor_tables output cursor.
FETCH NEXT from @Report;
WHILE (@@FETCH_STATUS <> -1)
BEGIN
   FETCH NEXT from @Report;
END

-- Close and deallocate the cursor from sp_describe_cursor_tables.
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
- [sp_cursor_list (Transact-SQL)](sp-cursor-list-transact-sql.md)
- [sp_describe_cursor (Transact-SQL)](sp-describe-cursor-transact-sql.md)
- [sp_describe_cursor_columns (Transact-SQL)](sp-describe-cursor-columns-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
