---
title: "sp_cursorprepexec (Transact-SQL)"
description: Compiles a plan for the submitted cursor statement or batch, then creates and populates the cursor.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/02/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cursorprepexec_TSQL"
  - "sp_cursorprepexec"
helpviewer_keywords:
  - "sp_cursorprepexec"
dev_langs:
  - "TSQL"
---
# sp_cursorprepexec (Transact-SQL)

[!INCLUDE[sql-asdb-asa-pdw](../../includes/applies-to-version/sql-asdb-asa-pdw.md)]

Compiles a plan for the submitted cursor statement or batch, then creates and populates the cursor. `sp_cursorprepexec` combines the functions of `sp_cursorprepare` and `sp_cursorexecute`. This procedure is invoked by specifying `ID = 5` in a tabular data stream (TDS) packet.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_cursorprepexec prepared handle OUTPUT , cursor OUTPUT , params , statement , options
    [ , scrollopt [ , ccopt [ , rowcount ] ] ]
    [ , '@parameter_name [ , ...n ] ' ]
```

## Arguments

#### *prepared handle*

A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generated *prepared handle* identifier. *prepared handle* is required and returns **int**.

#### *cursor*

The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generated *cursor* identifier. *cursor* is a required parameter that must be supplied on all subsequent procedures that act upon this cursor, for example, `sp_cursorfetch`.

#### *params*

Identifies parameterized statements. The *params* definition of variables is substituted for parameter markers in the statement. *params* is a required parameter that calls for an **ntext**, **nchar**, or **nvarchar** input value.

> [!NOTE]  
> Use an **ntext** string as the input value when *statement* is parameterized and the *scrollopt* PARAMETERIZED_STMT value is ON.

#### *statement*

Defines the cursor result set. The *statement* parameter is required and calls for an **ntext**, **nchar**, or **nvarchar** input value.

> [!NOTE]  
> The rules for specifying the *statement* value are the same as those for `sp_cursoropen`, with the exception that the *statement* string data type must be **ntext**.

#### *options*

An optional parameter that returns a description of the cursor result set columns. *options* require the following **int** input value.

| Value | Description |
| --- | --- |
| 0x0001 | RETURN_METADATA |

#### *scrollopt*

Scroll option. *scrollopt* is an optional parameter that requires one of the following **int** input values.

| Value | Description |
| --- | --- |
| 0x0001 | KEYSET |
| 0x0002 | DYNAMIC |
| 0x0004 | FORWARD_ONLY |
| 0x0008 | STATIC |
| 0x10 | FAST_FORWARD |
| 0x1000 | PARAMETERIZED_STMT |
| 0x2000 | AUTO_FETCH |
| 0x4000 | AUTO_CLOSE |
| 0x8000 | CHECK_ACCEPTED_TYPES |
| 0x10000 | KEYSET_ACCEPTABLE |
| 0x20000 | DYNAMIC_ACCEPTABLE |
| 0x40000 | FORWARD_ONLY_ACCEPTABLE |
| 0x80000 | STATIC_ACCEPTABLE |
| 0x100000 | FAST_FORWARD_ACCEPTABLE |

Because of the possibility that the requested option isn't appropriate for the cursor defined by *statement*, this parameter serves as both input and output. In such cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] assigns an appropriate type and modifies this value.

#### *ccopt*

Concurrency control option. *ccopt* is an optional parameter that requires one of the following **int** input values.

| Value | Description |
| --- | --- |
| 0x0001 | READ_ONLY |
| 0x0002 | SCROLL_LOCKS (previously known as LOCKCC) |
| 0x0004 | OPTIMISTIC (previously known as OPTCC) |
| 0x0008 | OPTIMISTIC (previously known as OPTCCVAL) |
| 0x2000 | ALLOW_DIRECT |
| 0x4000 | UPDT_IN_PLACE |
| 0x8000 | CHECK_ACCEPTED_OPTS |
| 0x10000 | READ_ONLY_ACCEPTABLE |
| 0x20000 | SCROLL_LOCKS_ACCEPTABLE |
| 0x40000 | OPTIMISTIC_ACCEPTABLE |
| 0x80000 | OPTIMISITC_ACCEPTABLE |

As with *scrollopt*, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can assign a different value than the one requested.

#### *rowcount*

An optional parameter that signifies the number of fetch buffer rows to use with AUTO_FETCH. The default is 20 rows. *rowcount* behaves differently when assigned as an input value versus a return value.

| As input value | As return value |
| --- | --- |
| When AUTO_FETCH is specified with FAST_FORWARD cursors *rowcount* represents the number of rows to place into the fetch buffer. | Represents the number of rows in the result set. When the *scrollopt* AUTO_FETCH value is specified, *rowcount* returns the number of rows that were fetched into the fetch buffer. |

#### *parameter_name*

Designates one or more parameter names as defined in the *params* argument. There must be a parameter supplied for every parameter included in *params*. This argument isn't required when the Transact-SQL statement or batch in *params* has no parameters defined.

## Return code values

If *params* returns a NULL value, then the statement isn't parameterized.

## Examples

This example demonstrates the use of `sp_cursorprepexec`. It runs a query against the `Person` table in the `AdventureWorks2019` database returning all records where first name is "Katherine".

```sql
USE AdventureWorks2019;

DECLARE @prep_handle INT,
        @cursor      INT,
        @scrollopt   INT = 4104,
        @ccopt       INT = 8193,
        @rowcnt      INT

EXEC sp_cursorprepexec
  @prep_handle output,
  @cursor output,
  N'@fName nvarchar(100)',
  N'SELECT FirstName, LastName FROM Person.Person WHERE FirstName = @fName',
  @scrollopt,
  @ccopt,
  @rowcnt output,
  'Katherine';

EXEC Sp_cursorfetch  @cursor;
```

## See also

- [sp_cursoropen (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-cursoropen-transact-sql.md)
- [sp_cursorexecute (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-cursorexecute-transact-sql.md)
- [sp_cursorprepare (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-cursorprepare-transact-sql.md)
- [sp_cursorfetch (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-cursorfetch-transact-sql.md)
- [System Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)
