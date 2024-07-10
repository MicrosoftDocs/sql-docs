---
title: "sp_cursor_list (Transact-SQL)"
description: sp_cursor_list reports the attributes of server cursors currently open for the connection.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cursor_list"
  - "sp_cursor_list_TSQL"
helpviewer_keywords:
  - "sp_cursor_list"
dev_langs:
  - "TSQL"
---
# sp_cursor_list (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports the attributes of server cursors currently open for the connection.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_cursor_list
    [ @cursor_return = ] cursor_return OUTPUT
    , [ @cursor_scope = ] cursor_scope
[ ; ]
```

## Arguments

#### [ @cursor_return = ] *cursor_return* OUTPUT

The name of a declared cursor variable. *@cursor_return* is an OUTPUT parameter of type **int**. The cursor is a scrollable, dynamic, read-only cursor.

#### [ @cursor_scope = ] *cursor_scope*

Specifies the level of cursors to report. *@cursor_scope* is **int**, with no default, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Report all local cursors. |
| `2` | Report all global cursors. |
| `3` | Report both local and global cursors. |

## Return code values

None.

## Cursors returned

`sp_cursor_list` returns its report as a [!INCLUDE [tsql](../../includes/tsql-md.md)] cursor output parameter, not as a result set. This allows [!INCLUDE [tsql](../../includes/tsql-md.md)] batches, stored procedures, and triggers to work with the output one row at a time. It also means the procedure can't be called directly from database API functions. The cursor output parameter must be bound to a program variable, but the database APIs don't support binding cursor parameters or variables.

This is the format of the cursor returned by `sp_cursor_list`. The format of the cursor is the same as the format returned by `sp_describe_cursor`.

| Column name | Data type | Description |
| --- | --- | --- |
| `reference_name` | **sysname** | The name used to refer to the cursor. If the reference to the cursor was through the name given on a `DECLARE CURSOR` statement, the reference name is the same as cursor name. If the reference to the cursor was through a variable, the reference name is the name of the cursor variable. |
| `cursor_name` | **sysname** | The name of the cursor from a `DECLARE CURSOR` statement. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], if the cursor was created by setting a cursor variable to a cursor, `cursor_name` returns the name of the cursor variable. In previous releases, this output column returns a system-generated name. |
| `cursor_scope` | **smallint** | `1` = `LOCAL`<br />`2` = `GLOBAL` |
| `status` | **smallint** | The same values as reported by the `CURSOR_STATUS` system function:<br /><br />`1` = The cursor referenced by the cursor name or variable is open. If the cursor is insensitive, static, or keyset, it's at least one row. If the cursor is dynamic, the result set has zero or more rows.<br /><br />`0` = The cursor referenced by the cursor name or variable is open but has no rows. Dynamic cursors never return this value.<br /><br />`-1` = The cursor referenced by the cursor name or variable is closed.<br /><br />`-2` = Applies only to cursor variables. There's no cursor assigned to the variable. Possibly, an `OUTPUT` parameter assigned a cursor to the variable, but the stored procedure closed the cursor before returning.<br /><br />`-3` = A cursor or cursor variable with the specified name doesn't exist, or the cursor variable doesn't have a cursor allocated to it. |
| `model` | **smallint** | `1` = Insensitive (or static)<br />`2` = Keyset<br />`3` = Dynamic<br />`4` = Fast Forward |
| `concurrency` | **smallint** | `1` = Read-only<br />`2` = Scroll locks<br />`3` = Optimistic |
| `scrollable` | **smallint** | `0` = Forward-only<br />`1` = Scrollable |
| `open_status` | **smallint** | `0` = Closed<br />`1` = Open |
| `cursor_rows` | **int** | The number of qualifying rows in the result set. For more information, see [@@CURSOR_ROWS](../../t-sql/functions/cursor-rows-transact-sql.md). |
| `fetch_status` | **smallint** | The status of the last fetch on this cursor. For more information, see [&#x40;&#x40;FETCH_STATUS](../../t-sql/functions/fetch-status-transact-sql.md):<br /><br />`0` = Fetch successful.<br />`-1` = Fetch failed or is beyond the bounds of the cursor.<br />`-2` = The requested row is missing.<br />`-9` = There's been no fetch on the cursor. |
| `column_count` | **smallint** | The number of columns in the cursor result set. |
| `row_count` | **smallint** | The number of rows affected by the last operation on the cursor. For more information, see [@@ROWCOUNT](../../t-sql/functions/rowcount-transact-sql.md). |
| `last_operation` | **smallint** | The last operation performed on the cursor:<br /><br />`0` = No operations were performed on the cursor.<br />`1` = `OPEN`<br />`2` = `FETCH`<br />`3` = `INSERT`<br />`4` = `UPDATE`<br />`5` = `DELETE`<br />`6` = `CLOSE`<br />`7` = `DEALLOCATE` |
| `cursor_handle` | **int** | A unique value that identifies the cursor within the scope of the server. |

## Remarks

`sp_cursor_list` produces a list of the current server cursors opened by the connection and describes the attributes global to each cursor, such as the scrollability and updatability of the cursor. The cursors listed by `sp_cursor_list` include:

- [!INCLUDE [tsql](../../includes/tsql-md.md)] server cursors.

- API server cursors opened by an ODBC application that is then called `SQLSetCursorName` to name the cursor.

Use `sp_describe_cursor_columns` for a description of the attributes of the result set returned by the cursor. Use `sp_describe_cursor_tables` for a report of the base tables referenced by the cursor. `sp_describe_cursor` reports the same information as `sp_cursor_list`, but only for a specified cursor.

## Permissions

Execute permissions default to the **public** role.

## Examples

The following example opens a global cursor and uses `sp_cursor_list` to report on the attributes of the cursor.

```sql
USE AdventureWorks2022;
GO

-- Declare and open a keyset-driven cursor.
DECLARE abc CURSOR KEYSET
FOR
SELECT LastName
FROM Person.Person
WHERE LastName LIKE 'S%';

OPEN abc;

-- Declare a cursor variable to hold the cursor output variable
-- from sp_cursor_list.
DECLARE @Report CURSOR;

-- Execute sp_cursor_list into the cursor variable.
EXEC master.dbo.sp_cursor_list
    @cursor_return = @Report OUTPUT,
    @cursor_scope = 2;

-- Fetch all the rows from the sp_cursor_list output cursor.
FETCH NEXT from @Report;
WHILE (@@FETCH_STATUS <> -1)
BEGIN
    FETCH NEXT from @Report;
END

-- Close and deallocate the cursor from sp_cursor_list.
CLOSE @Report;
DEALLOCATE @Report;
GO

-- Close and deallocate the original cursor.
CLOSE abc;
DEALLOCATE abc;
GO
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
