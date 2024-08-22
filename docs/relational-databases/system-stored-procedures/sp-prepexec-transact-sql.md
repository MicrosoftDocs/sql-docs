---
title: "sp_prepexec (Transact-SQL)"
description: sp_prepexec prepares and executes a parameterized Transact-SQL statement.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cursor_prepexec"
  - "sp_cursor_prepexec_TSQL"
helpviewer_keywords:
  - "sp_prepexec"
dev_langs:
  - "TSQL"
---
# sp_prepexec (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Prepares and executes a parameterized [!INCLUDE [tsql](../../includes/tsql-md.md)] statement. `sp_prepexec` combines the functions of `sp_prepare` and `sp_execute.` This action is invoked by `ID = 13` in a tabular data stream (TDS) packet.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_prepexec handle OUTPUT , params , stmt
    [ , bound param ] [ , ...n ]
[ ; ]
```

## Arguments

#### *handle*

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]-generated *handle* identifier. *handle* is a required parameter with an **int** return value.

#### *params*

Identifies parameterized statements. The *params* definition of variables is substituted for parameter markers in the statement. *params* is a required parameter that calls for an **ntext**, **nchar**, or **nvarchar** input value. Input a `NULL` value if the statement isn't parameterized.

#### *stmt*

Defines the cursor result set. The *stmt* parameter is required and calls for an **ntext**, **nchar**, or **nvarchar** input value.

#### *bound_param*

Signifies the optional use of extra parameters. *bound_param* calls for an input value of any data type to designate the extra parameters in use.

## Examples

The following example prepares and executes a simple statement:

```sql
Declare @Out int;
EXEC sp_prepexec @Out output,
    N'@P1 nvarchar(128), @P2 nvarchar(100)',
    N'SELECT database_id, name
      FROM sys.databases
      WHERE name=@P1 AND state_desc = @P2',
          @P1 = 'tempdb', @P2 = 'ONLINE';
EXEC sp_unprepare @Out;
```

## Related content

- [sp_prepare (Transact SQL)](sp-prepare-transact-sql.md)
- [sp_execute (Transact-SQL)](sp-execute-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
