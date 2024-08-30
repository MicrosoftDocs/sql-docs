---
title: "sp_unprepare (Transact-SQL)"
description: Discards the execution plan created by the `sp_prepare` stored procedure.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cursor_unprepare_TSQL"
  - "sp_cursor_unprepare"
helpviewer_keywords:
  - "sp_unprepare"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_unprepare (Transact-SQL)

[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

Discards the execution plan created by the `sp_prepare` stored procedure. `sp_unprepare` is invoked by specifying `ID = 15` in a tabular data stream (TDS) packet.

## Syntax

```syntaxsql
sp_unprepare handle
[ ; ]
```

## Arguments

#### *handle*

The *handle* value returned by `sp_prepare`. *handle* is **int**.

## Examples

The following example prepares, executes, and unprepares a basic statement.

```SQL
DECLARE @P1 INT;

EXEC sp_prepare @P1 OUTPUT,
    N'@P1 NVARCHAR(128), @P2 NVARCHAR(100)',
    N'SELECT database_id, name FROM sys.databases WHERE name = @P1 AND state_desc = @P2';

EXEC sp_execute @P1, N'tempdb', N'ONLINE';

EXEC sp_unprepare @P1;
```

## Related content

- [sp_prepare (Transact SQL)](sp-prepare-transact-sql.md)
