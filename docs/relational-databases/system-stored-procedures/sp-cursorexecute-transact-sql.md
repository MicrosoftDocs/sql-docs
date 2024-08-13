---
title: "sp_cursorexecute (Transact-SQL)"
description: sp_cursorexecute creates and populates a cursor based upon the execution plan created by sp_cursorprepare.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cursorexecute"
  - "sp_cursorexecute_TSQL"
helpviewer_keywords:
  - "sp_cursor_execute"
dev_langs:
  - "TSQL"
---
# sp_cursorexecute (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates and populates a cursor based upon the execution plan created by `sp_cursorprepare`. This procedure, coupled with `sp_cursorprepare`, has the same function as `sp_cursoropen`, but is split into two phases. `sp_cursorexecute` is invoked by specifying `ID = 4` in a tabular data stream (TDS) packet.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_cursorexecute prepared_handle , cursor
    [ , scrollopt [ OUTPUT ]
    [ , ccopt [ OUTPUT ]
    [ , rowcount OUTPUT [ , bound param ] [ , ...n ] ] ] ]
[ ; ]
```

## Arguments

#### *prepared_handle*

The prepared statement *handle* value returned by `sp_cursorprepare`. The *prepared_handle* parameter is **int**, and can't be `NULL`.

#### *cursor*

The [!INCLUDE [ssde-md](../../includes/ssde-md.md)]-generated cursor identifier. *cursor* is a required parameter that must be supplied on all subsequent procedures that act upon the cursor, such as `sp_cursorfetch`.

#### *scrollopt*

Scroll option. The *scrollopt* parameter is **int**, with a default of `NULL`. The `sp_cursorexecute` *scrollopt* parameter has the same value options as `sp_cursoropen`.

The `PARAMETERIZED_STMT` value isn't supported.

If a *scrollopt* value isn't specified, the default value is `KEYSET` regardless of *scrollopt* value specified in `sp_cursorprepare`.

#### *ccopt*

Currency control option. *ccopt* is an optional parameter that requires an **int** input value. The `sp_cursorexecute`*ccopt* parameter has the same value options as `sp_cursoropen`.

If a *ccopt* value isn't specified, the default value is `OPTIMISTIC` regardless of *ccopt* value specified in `sp_cursorprepare`.

#### *rowcount*

An optional parameter that signifies the number of fetch buffer rows to use with `AUTO_FETCH`. The default is 20 rows. *rowcount* behaves differently when assigned as an input value versus a return value.

| As input value | As return value |
| --- | --- |
| When `AUTO_FETCH` is specified with `FAST_FORWARD` cursors, *rowcount* represents the number of rows to place into the fetch buffer. | Represents the number of rows in the result set. When the *scrollopt* `AUTO_FETCH` value is specified, *rowcount* returns the number of rows that were fetched into the fetch buffer. |

#### *bound_param*

Signifies the optional use of extra parameters.

Any parameters after the fifth are passed along to the statement plan as input parameters.

## Return code values

*rowcount* returns the following values.

| Value | Description |
| --- | --- |
| `-1` | Number of rows unknown. |
| `-n` | An asynchronous population is in effect. |

## Remarks

## scrollopt and ccopt Parameters

*scrollopt* and *ccopt* are useful when the cached plans are preempted for the server cache, meaning that the prepared handle identifying the statement must be recompiled. The *scrollopt* and *ccopt* parameter values must match the values sent in the original request to `sp_cursorprepare`.

`PARAMETERIZED_STMT` shouldn't be assigned to *scrollopt*.

Failure to provide matching values results in recompilation of the plans, negating the prepare and execute operations.

## RPC and TDS considerations

The RPC `RETURN_METADATA` input flag can be set to `1` to request that cursor select list metadata is returned in the TDS stream.

## Related content

- [sp_cursoropen (Transact-SQL)](sp-cursoropen-transact-sql.md)
- [sp_cursorfetch (Transact-SQL)](sp-cursorfetch-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
