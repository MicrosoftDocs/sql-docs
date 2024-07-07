---
title: "sp_cursorunprepare (Transact-SQL)"
description: sp_cursorunprepare discards the execution plan developed in the sp_cursorprepare stored procedure.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cursorunprepare_TSQL"
  - "sp_cursorunprepare"
helpviewer_keywords:
  - "sp_cursorunprepare"
dev_langs:
  - "TSQL"
---
# sp_cursorunprepare (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Discards the execution plan developed in the `sp_cursorprepare` stored procedure. `sp_cursorunprepare` is invoked by specifying `ID = 6` in a tabular data stream (TDS) packet.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_cursorunprepare handle
[ ; ]
```

## Arguments

#### *handle*

The *handle* value returned by `sp_cursorprepare` when the statement is prepared.

## Related content

- [sp_cursorprepare (Transact-SQL)](sp-cursorprepare-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
