---
title: "sp_execute (Transact-SQL)"
description: sp_execute executes a prepared Transact-SQL statement using a specified handle and optional parameter value.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cursor_execute"
  - "sp_cursor_execute_TSQL"
helpviewer_keywords:
  - "sp_execute"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_execute (Transact-SQL)

[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

Executes a prepared [!INCLUDE [tsql](../../includes/tsql-md.md)] statement using a specified handle and optional parameter value. `sp_execute` is invoked by specifying `ID = 12` in a tabular data stream (TDS) packet.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_execute handle OUTPUT
    [ , bound_param ] [ , ...n ]
[ ; ]
```

## Arguments

#### *handle*

The *handle* value returned by `sp_prepare`. The required *handle* parameter is **int**, and can't be `NULL`.

#### *bound_param*

Signifies the use of extra parameters. The *bound_param* parameter is any data type, to signify more parameters for the procedure, and can't be `NULL`.

> [!NOTE]  
> *bound_param* must match the declarations made by the `sp_prepare` *@params* value, and can be in the form `@name = <value>` or `<value>`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sp_prepare (Transact SQL)](sp-prepare-transact-sql.md)
- [sp_executesql (Transact-SQL)](sp-executesql-transact-sql.md)
