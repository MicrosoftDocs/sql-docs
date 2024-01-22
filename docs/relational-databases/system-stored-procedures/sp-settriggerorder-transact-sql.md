---
title: "sp_settriggerorder (Transact-SQL)"
description: sp_settriggerorder specifies the AFTER triggers that are fired first or last.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_settriggerorder"
  - "sp_settriggerorder_TSQL"
helpviewer_keywords:
  - "sp_settriggerorder"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_settriggerorder (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Specifies the `AFTER` triggers that are fired first or last. The `AFTER` triggers that are fired between the first and last triggers are executed in undefined order.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_settriggerorder
    [ @triggername = ] N'triggername'
    , [ @order = ] 'order'
    , [ @stmttype = ] 'stmttype'
    [ , [ @namespace = ] 'DATABASE' | 'SERVER' | NULL ]
[ ; ]
```

## Arguments

#### [ @triggername = ] N'*triggername*'

The name of the trigger and the schema to which it belongs, if applicable, whose order is to be set or changed. *@triggername* is **nvarchar(517)**, with no default, and is in the format **[ trigger_schema . ] trigger_name**. If the name doesn't correspond to a trigger or if the name corresponds to an `INSTEAD OF` trigger, the procedure returns an error. A schema can't be specified for DDL or logon triggers.

#### [ @order = ] '*order*'

The setting for the new order of the trigger. *@order* is **varchar(10)**, and can be any one of the following values.

| Value | Description |
| --- | --- |
| `First` | Trigger is fired first. |
| `Last` | Trigger is fired last. |
| `None` | Trigger is fired in undefined order. |

> [!IMPORTANT]  
> The `First` and `Last` triggers must be two different triggers.

#### [ @stmttype = ] '*stmttype*'

Specifies the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement that fires the trigger. *@stmttype* is **varchar(50)**, and can be `INSERT`, `UPDATE`, `DELETE`, `LOGON`, or any T-SQL statement event listed in [DDL Events](../triggers/ddl-events.md). Event groups can't be specified.

A trigger can be designated as the `First` or `Last` trigger for a statement type only after that trigger was defined as a trigger for that statement type. For example, trigger `TR1` can be designated `First` for `INSERT` on table `T1` if `TR1` is defined as an `INSERT` trigger. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] returns an error if `TR1`, which was defined only as an `INSERT` trigger, is set as a `First` or `Last` trigger for an `UPDATE` statement. For more information, see the [Remarks](#remarks) section.

#### @namespace = { 'DATABASE' | 'SERVER' | NULL }

When *@triggername* is a DDL trigger, *@namespace* specifies whether *@triggername* was created with database scope or server scope. If *@triggername* is a logon trigger, `SERVER` must be specified. For more information about DDL trigger scope, see [DDL Triggers](../triggers/ddl-triggers.md). If not specified, or if `NULL` is specified, *@triggername* is a DML trigger.

## Return code values

`0` (success) and `1` (failure).

## Remarks

This section discusses considerations for data manipulation language (DML) and data definition language (DDL) triggers.

### DML triggers

There can be only one `First` and one `Last` trigger for each statement on a single table.

If a `First` trigger is already defined on the table, database, or server, you can't designate a new trigger as `First` for the same table, database, or server for the same *@stmttype*. This restriction also applies `Last` triggers.

Replication automatically generates a first trigger for any table that is included in an immediate updating or queued updating subscription. Replication requires that its trigger is the first trigger. Replication raises an error when you try to include a table with a first trigger in an immediate updating or queued updating subscription. If you try to make a trigger a first trigger after a table is included in a subscription, `sp_settriggerorder` returns an error. If you use `ALTER TRIGGER` on the replication trigger, or use `sp_settriggerorder` to change the replication trigger to a `Last` or `None` trigger, the subscription doesn't function correctly.

### DDL triggers

If a DDL trigger with database scope and a DDL trigger with server scope exist on the same event, you can specify that both triggers be a `First` trigger or a `Last` trigger. However, server-scoped triggers always fire first. In general, the order of execution of DDL triggers that exist on the same event is as follows:

1. The server-level trigger marked `First`
1. Other server-level triggers
1. The server-level trigger marked `Last`
1. The database-level trigger marked `First`
1. Other database-level triggers
1. The database-level trigger marked `Last`

## General trigger considerations

If an `ALTER TRIGGER` statement changes a first or last trigger, the `First` or `Last` attribute originally set on the trigger is dropped, and the value is replaced by `None`. The order value must be reset by using `sp_settriggerorder`.

If the same trigger must be designated as the first or last order for more than one statement type, `sp_settriggerorder` must be executed for each statement type. Also, the trigger must be first defined for a statement type before it can be designated as the `First` or `Last` trigger to fire for that statement type.

## Permissions

Setting the order of a DDL trigger with server scope (created `ON ALL SERVER`) or a logon trigger requires `CONTROL SERVER` permission.

Setting the order of a DDL trigger with database scope (created `ON DATABASE`) requires `ALTER ANY DATABASE DDL TRIGGER` permission.

Setting the order of a DML trigger requires `ALTER` permission on the table or view on which the trigger is defined.

## Examples

### A. Set the firing order for a DML trigger

The following example specifies that trigger `uSalesOrderHeader` is the first trigger to fire after an `UPDATE` operation occurs on the `Sales.SalesOrderHeader` table.

```sql
USE AdventureWorks2022;
GO

EXEC sp_settriggerorder @triggername = 'Sales.uSalesOrderHeader',
    @order = 'First',
    @stmttype = 'UPDATE';
```

### B. Set the firing order for a DDL trigger

The following example specifies that trigger `ddlDatabaseTriggerLog` is the first trigger to fire after an `ALTER_TABLE` event occurs in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO

EXEC sp_settriggerorder @triggername = 'ddlDatabaseTriggerLog',
    @order = 'First',
    @stmttype = 'ALTER_TABLE',
    @namespace = 'DATABASE';
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [ALTER TRIGGER (Transact-SQL)](../../t-sql/statements/alter-trigger-transact-sql.md)
