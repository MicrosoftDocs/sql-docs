---
title: sp_check_for_sync_trigger (Transact-SQL)
description: sp_check_for_sync_trigger Determines the calling context of a user-defined trigger or stored procedure.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_check_for_sync_trigger"
helpviewer_keywords:
  - "sp_check_for_sync_trigger"
dev_langs:
  - "TSQL"
---
# sp_check_for_sync_trigger (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Determines whether a user-defined trigger or stored procedure is being called in the context of a replication trigger, which is used for immediate updating subscriptions. This stored procedure is executed at the Publisher on the publication database or at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_check_for_sync_trigger
    [ @tabid = ] tabid
    [ , [ @trigger_op = ] 'trigger_op' OUTPUT ]
    [ , [ @fonpublisher = ] fonpublisher ]
[ ; ]
```

## Arguments

#### [ @tabid = ] *tabid*

The object ID of the table being checked for immediate updating triggers. *@tabid* is **int**, with no default.

#### [ @trigger_op = ] '*trigger_op*' OUTPUT

Specifies if the output parameter is to return the type of trigger it's being called from. *@trigger_op* is an OUTPUT parameter of type **char(10)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `Ins` | `INSERT` trigger |
| `Upd` | `UPDATE` trigger |
| `Del` | `DELETE` trigger |
| `NULL` (default) | |

#### [ @fonpublisher = ] *fonpublisher*

Specifies the location where the stored procedure is executed. *@fonpublisher* is **bit**, with a default of `0`.

- If `0`, the execution is at the Subscriber.
- If `1`, the execution is at the Publisher.

## Return code values

0 indicates that the stored procedure isn't being called within the context of an immediate-updating trigger. 1 indicates that it's being called within the context of an immediate-updating trigger and is the type of trigger being returned in *@trigger_op*.

## Remarks

`sp_check_for_sync_trigger` is used in snapshot replication and transactional replication.

`sp_check_for_sync_trigger` is used to coordinate between replication and user-defined triggers. This stored procedure determines if it's being called within the context of a replication trigger. For example, you can call the procedure `sp_check_for_sync_trigger` in the body of a user-defined trigger. If `sp_check_for_sync_trigger` returns `0`, the user-defined trigger continues processing. If `sp_check_for_sync_trigger` returns `1`, the user-defined trigger exits. This ensures that the user-defined trigger doesn't fire when the replication trigger updates the table.

## Examples

### A. Add code to a trigger on a Subscriber table

The following example shows code that could be used in a trigger on a Subscriber table.

```sql
DECLARE @retcode INT,
    @trigger_op CHAR(10),
    @table_id INT;

SELECT @table_id = object_id('tablename');

EXEC @retcode = sp_check_for_sync_trigger
    @table_id,
    @trigger_op OUTPUT;

IF @retcode = 1
    RETURN;
```

### B. Add code to a trigger on a Publisher table

The code can also be added to a trigger on a table at the Publisher; the code is similar, but the call to `sp_check_for_sync_trigger` includes an extra parameter.

```sql
DECLARE @retcode INT,
    @trigger_op CHAR(10),
    @table_id INT,
    @fonpublisher INT;

SELECT @table_id = object_id('tablename');

SELECT @fonpublisher = 1;

EXEC @retcode = sp_check_for_sync_trigger
    @table_id,
    @trigger_op OUTPUT,
    @fonpublisher;

IF @retcode = 1
    RETURN;
```

## Permissions

Any user with `SELECT` permissions in the [sys.objects](../system-catalog-views/sys-objects-transact-sql.md) system view can execute `sp_check_for_sync_trigger`.

## Related content

- [Updatable Subscriptions - For Transactional Replication](../replication/transactional/updatable-subscriptions-for-transactional-replication.md)
