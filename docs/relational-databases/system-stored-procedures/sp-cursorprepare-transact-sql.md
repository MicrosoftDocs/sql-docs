---
title: sp_cursorprepare (Transact-SQL)
description: sp_cursorprepare compiles the cursor statement or batch into an execution plan, but doesn't create the cursor.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cursor_prepare_TSQL"
  - "sp_cursor_prepare"
helpviewer_keywords:
  - "sp_cursor_prepare"
dev_langs:
  - "TSQL"
---
# sp_cursorprepare (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Compiles the cursor statement or batch into an execution plan, but doesn't create the cursor. The compiled statement can later be used by `sp_cursorexecute`. This procedure, coupled with `sp_cursorexecute`, has the same function as `sp_cursoropen`, but is split into two phases. `sp_cursorprepare` is invoked by specifying `ID = 3` in a tabular data stream (TDS) packet.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_cursorprepare prepared_handle OUTPUT , params , stmt , options
    [ , scrollopt [ , ccopt ] ]
[ ; ]
```

## Arguments

#### *prepared_handle*

A SQL Server-generated prepared `handle` identifier that returns an **int** value.

*prepared_handle* is then supplied to a `sp_cursorexecute` procedure in order to open a cursor. Once a handle is created, it exists until you sign out, or until you explicitly remove it through a `sp_cursorunprepare` procedure.

#### *params*

Identifies parameterized statements. The *params* definition of variables is substituted for parameter markers in the statement. *params* is a required parameter that calls for an **ntext**, **nchar**, or **nvarchar** input value. Input a `NULL` value if the statement isn't parameterized.

Use an **ntext** string as the input value when *stmt* is parameterized and the *scrollopt* PARAMETERIZED_STMT value is ON.

#### *stmt*

Defines the cursor result set. The *stmt* parameter is required and calls for an **ntext**, **nchar, or **nvarchar** input value.

The rules for specifying the *stmt* value are the same as `sp_cursoropen`, with the exception that the *stmt* string data type must be **ntext**.

#### *options*

Returns a description of the cursor result set columns. The *options* parameter is **int**, with a default of `NULL`. When set to `0x0001`, it means `RETURN_METADATA`.

#### *scrollopt*

Scroll option. The *scrollopt* parameter is an optional parameter that requires one of the following **int** input values.

| Value | Description |
| --- | --- |
| `0x0001` | `KEYSET` |
| `0x0002` | `DYNAMIC` |
| `0x0004` | `FORWARD_ONLY` |
| `0x0008` | `STATIC` |
| `0x10` | `FAST_FORWARD` |
| `0x1000` | `PARAMETERIZED_STMT` |
| `0x2000` | `AUTO_FETCH` |
| `0x4000` | `AUTO_CLOSE` |
| `0x8000` | `CHECK_ACCEPTED_TYPES` |
| `0x10000` | `KEYSET_ACCEPTABLE` |
| `0x20000` | `DYNAMIC_ACCEPTABLE` |
| `0x40000` | `FORWARD_ONLY_ACCEPTABLE` |
| `0x80000` | `STATIC_ACCEPTABLE` |
| `0x100000` | `FAST_FORWARD_ACCEPTABLE` |

Because the requested value might not be appropriate for the cursor defined by *stmt*, this parameter serves as both input and output. In such cases, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] assigns an appropriate value.

#### *ccopt*

Concurrency control option. *ccopt* is an optional parameter that requires one of the following **int** input values.

| Value | Description |
| --- | --- |
| `0x0001` | `READ_ONLY` |
| `0x0002` | `SCROLL_LOCKS` (previously known as `LOCKCC`) |
| `0x0004` | `OPTIMISTIC` (previously known as `OPTCC`) |
| `0x0008` | `OPTIMISTIC` (previously known as `OPTCCVAL`) |
| `0x2000` | `ALLOW_DIRECT` |
| `0x4000` | `UPDT_IN_PLACE` |
| `0x8000` | `CHECK_ACCEPTED_OPTS` |
| `0x10000` | `READ_ONLY_ACCEPTABLE` |
| `0x20000` | `SCROLL_LOCKS_ACCEPTABLE` |
| `0x40000` | `OPTIMISTIC_ACCEPTABLE` |
| `0x80000` | `OPTIMISTIC_ACCEPTABLE` |

As with *scrollpt*, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can assign a different value from the one requested.

## Remarks

The RPC status parameter is one of the following values:

| Value | Description |
| --- | --- |
| `0` | Success |
| `0x0001` | Failure |
| `1FF6` | Couldn't return metadata.<br /><br />**Note:** The reason for this is that the statement doesn't produce a result set; for example, it's an `INSERT` or DDL statement. |

## Examples

The following code is an example of using `sp_cursorprepare` and `sp_cursorexecute`:

```sql
DECLARE @handle INT, @p5 INT, @p6 INT;

EXEC sp_cursorprepare @handle OUTPUT,
    N'@dbid int',
    N'select * from sys.databases where database_id < @dbid',
    1,
    @p5 OUTPUT,
    @p6 OUTPUT;

DECLARE @p1 INT
SET @P1 = @handle;

DECLARE @p2 INT;
DECLARE @p3 INT;
DECLARE @p4 INT;

SET @P6 = 4;

EXEC sp_cursorexecute @p1,
    @p2 OUTPUT,
    @p3 OUTPUT,
    @p4 OUTPUT,
    @p5 OUTPUT,
    @p6;

EXEC sp_cursorfetch @P2;
EXEC sp_cursorunprepare @handle;
EXEC sp_cursorclose @p2;
```

When *stmt* is parameterized and the *scrollopt* `PARAMETERIZED_STMT` value is `ON`, the format of the string is in the following form:

```syntaxsql
<parameter_name> <data_type> [ ,... n ]
```

## Related content

- [sp_cursorexecute (Transact-SQL)](sp-cursorexecute-transact-sql.md)
- [sp_cursoropen (Transact-SQL)](sp-cursoropen-transact-sql.md)
- [sp_cursorunprepare (Transact-SQL)](sp-cursorunprepare-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
