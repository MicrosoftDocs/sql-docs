Drops an existing user-defined Resource Governor workload group.

:::image type="icon" source="../database-engine/configure-windows/media/topic-link.gif"::: [Transact-SQL Syntax Conventions](../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax

```syntaxsql
DROP WORKLOAD GROUP group_name
[;]
```

## Arguments

*group_name*
Is the name of an existing user-defined workload group.

## Remarks

The DROP WORKLOAD GROUP statement is not allowed on the Resource Governor internal or default groups.

When you are executing DDL statements, we recommend that you be familiar with Resource Governor states. For more information, see [Resource Governor](../relational-databases/resource-governor/resource-governor.md).

If a workload group contains active sessions, dropping or moving the workload group to a different resource pool will fail when the ALTER RESOURCE GOVERNOR RECONFIGURE statement is called to apply the change. To avoid this problem, you can take one of the following actions:

- Wait until all the sessions from the affected group have disconnected, and then rerun the ALTER RESOURCE GOVERNOR RECONFIGURE statement.

- Explicitly stop sessions in the affected group by using the KILL command, and then rerun the ALTER RESOURCE GOVERNOR RECONFIGURE statement.

- Restart the server. After the restart process is completed, the deleted group will not be created, and a moved group will use the new resource pool assignment.

- In a scenario in which you have issued the DROP WORKLOAD GROUP statement but decide that you do not want to explicitly stop sessions to apply the change, you can re-create the group by using the same name that it had before you issued the DROP statement, and then move the group to the original resource pool. To apply the changes, run the ALTER RESOURCE GOVERNOR RECONFIGURE statement.

## Permissions

Requires CONTROL SERVER permission.

## Examples

The following example drops the workload group named `adhoc`.

```
DROP WORKLOAD GROUP adhoc;
GO
ALTER RESOURCE GOVERNOR RECONFIGURE;
GO
```

## See Also

- [Resource Governor](../relational-databases/resource-governor/resource-governor.md)
- [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../t-sql/statements/create-workload-group-transact-sql.md)  
- [ALTER WORKLOAD GROUP &#40;Transact-SQL&#41;](../t-sql/statements/alter-workload-group-transact-sql.md)
- [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](../t-sql/statements/create-resource-pool-transact-sql.md)
- [ALTER RESOURCE POOL &#40;Transact-SQL&#41;](../t-sql/statements/alter-resource-pool-transact-sql.md)
- [DROP RESOURCE POOL &#40;Transact-SQL&#41;](../t-sql/statements/drop-resource-pool-transact-sql.md)
- [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../t-sql/statements/alter-resource-governor-transact-sql.md)  