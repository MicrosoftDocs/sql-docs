---
author: uc-msft
ms.author: umajay
ms.reviewer: dfurman
ms.date: 02/17/2025
ms.service: sql
ms.topic: include
---

Drops an existing user-defined resource governor workload group.

> [!NOTE]
> To modify resource governor configuration in [!INCLUDE[ssazuremi-md.md](../../includes/ssazuremi-md.md)], you must be in the context of the `master` database on the primary replica.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax

```syntaxsql
DROP WORKLOAD GROUP group_name
[;]
```

## Arguments

#### *group_name*

The name of an existing user-defined workload group.

## Remarks

The `DROP WORKLOAD GROUP` statement is not allowed on the resource governor built-in `internal` and `default` groups.

If a workload group contains active sessions, deleting the workload group fails when the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement is executed to apply the change. To avoid this problem, you can take one of the following actions:

- Wait until all sessions in the affected group disconnect, and then execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement.
- Explicitly stop sessions in the affected group by using the [KILL](../../t-sql/language-elements/kill-transact-sql.md) T-SQL command, and then execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement. If you decide that you don't want to explicitly stop sessions, re-create the group by using the original name and settings.
- Restart the server. When the server restarts, the deleted group is deleted permanently.

For more information, see [Resource governor](../../relational-databases/resource-governor/resource-governor.md) and [Resource governor workload group](../../relational-databases/resource-governor/resource-governor-workload-group.md).

## Permissions

Requires the `CONTROL SERVER` permission.

## Examples

The following example drops the workload group named `adhoc`.

```sql
DROP WORKLOAD GROUP adhoc;

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

## Related content

- [Resource governor](../../relational-databases/resource-governor/resource-governor.md)
- [Delete a workload group](../../relational-databases/resource-governor/delete-a-workload-group.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](../statements/create-workload-group-transact-sql.md)
- [ALTER WORKLOAD GROUP (Transact-SQL)](../statements/alter-workload-group-transact-sql.md)
- [CREATE RESOURCE POOL (Transact-SQL)](../statements/create-resource-pool-transact-sql.md)
- [ALTER RESOURCE POOL (Transact-SQL)](../statements/alter-resource-pool-transact-sql.md)
- [DROP RESOURCE POOL (Transact-SQL)](../statements/drop-resource-pool-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../statements/alter-resource-governor-transact-sql.md)
