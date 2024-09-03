---
title: "@@CURSOR_ROWS (Transact-SQL)"
description: "@@CURSOR_ROWS returns the number of qualifying rows currently in the last cursor opened on the connection."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/06/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@CURSOR_ROWS"
  - "@@CURSOR_ROWS_TSQL"
helpviewer_keywords:
  - "@@CURSOR_ROWS function"
  - "cursors [SQL Server], last-opened"
  - "last-opened cursor"
  - "asynchronous cursors [SQL Server]"
dev_langs:
  - "TSQL"
---
# @@CURSOR_ROWS (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This function returns the number of qualifying rows currently in the last cursor opened on the connection. To improve performance, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can populate large keyset and static cursors asynchronously. `@@CURSOR_ROWS` can be called to determine that the number of the rows that qualify for a cursor are retrieved at the time of the `@@CURSOR_ROWS` call.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
@@CURSOR_ROWS
```

## Return types

**int**

## Return value

| Return value | Description |
| --- | --- |
| `-m` | The cursor populates asynchronously. The value returned (-*m*) is the number of rows currently in the keyset. |
| `-1` | The cursor is dynamic. Because dynamic cursors reflect all changes, the number of rows that qualify for the cursor constantly changes. The cursor doesn't necessarily retrieve all qualified rows. |
| `0` | No cursors are open, no rows qualified for the last opened cursor, or the last-opened cursor is closed or deallocated. |
| `n` | The cursor is fully populated. The value returned (*n*) is the total number of rows in the cursor. |

## Remarks

`@@CURSOR_ROWS` returns a negative number if the last cursor opened asynchronously. Keyset-driver or static cursors open asynchronously if the value for `sp_configure` cursor threshold exceeds `0`, and the number of rows in the cursor result set exceeds the cursor threshold.

## Examples

This example first declares a cursor, and then uses `SELECT` to display the value of `@@CURSOR_ROWS`. The setting has a value of `0` before the cursor opens and then has a value of `-1`, to indicate that the cursor keyset populates asynchronously.

```sql
USE AdventureWorks2022;
GO

SELECT @@CURSOR_ROWS;

DECLARE Name_Cursor CURSOR
FOR
SELECT LastName, @@CURSOR_ROWS
FROM Person.Person;
OPEN Name_Cursor;
FETCH NEXT FROM Name_Cursor;
SELECT @@CURSOR_ROWS;
CLOSE Name_Cursor;
DEALLOCATE Name_Cursor;
GO
```

Here are the result sets.

```output
-----------
0

LastName   (No column name)   ROWSTAT
---------  -----------------  ---------
Sánchez    -1                 NULL

-----------
-1
```

## Related content

- [Cursor Functions (Transact-SQL)](cursor-functions-transact-sql.md)
- [OPEN (Transact-SQL)](../language-elements/open-transact-sql.md)
