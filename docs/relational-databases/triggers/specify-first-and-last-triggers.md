---
title: "Specify First and Last Triggers"
description: "Specify First and Last Triggers"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/04/2017"
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "first triggers [SQL Server]"
  - "last triggers"
  - "DML triggers, first or last triggers"
  - "INSTEAD OF triggers"
  - "AFTER triggers"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Specify First and Last Triggers
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]
  You can specify that one of the AFTER triggers associated with a table be either the first AFTER trigger or the last AFTER trigger that is fired for each INSERT, DELETE, and UPDATE triggering actions. The AFTER triggers that are fired between the first and last triggers are executed in undefined order.  
  
 To specify the order for an AFTER trigger, use the **sp_settriggerorder** stored procedure. **sp_settriggerorder** has the following options.  
  
|Option|Description|  
|------------|-----------------|  
|**First**|Specifies that the DML trigger is the first AFTER trigger fired for a triggering action.|  
|**Last**|Specifies that the DML trigger is the last AFTER trigger fired for a triggering action.|  
|**None**|Specifies that there is no specific order in which the DML trigger should be fired. Used mainly to reset a trigger from being either first or last.|  
  
 The following example shows using **sp_settriggerorder**:  
  
```  
sp_settriggerorder @triggername = 'MyTrigger', @order = 'first', @stmttype = 'UPDATE'  
```  
  
> [!IMPORTANT]  
>  The first and last triggers must be two different DML triggers.  
  
 A table can have INSERT, UPDATE, and DELETE triggers defined on it at the same time. Each statement type can have its own first and last triggers, but they cannot be the same triggers.  
  
 If the first or last trigger defined for a table does not cover a triggering action, such as not covering FOR UPDATE, FOR DELETE, or FOR INSERT, there is no first or last trigger for the missing actions.  
  
 INSTEAD OF triggers cannot be specified as first or last triggers. INSTEAD OF triggers are fired before updates are made to the underlying tables. If updates are made by an INSTEAD OF trigger to underlying tables, the updates occur before any AFTER triggers defined on the table are fired. For example, if an INSTEAD OF INSERT trigger on a view inserts data into a base table and the base table itself contains an INSTEAD OF INSERT trigger and three AFTER INSERT triggers, the INSTEAD OF INSERT trigger on the base table is fired instead of the inserting action, and the AFTER triggers on the base table are fired after any inserting action on the base table. For more information, see [DML Triggers](../../relational-databases/triggers/dml-triggers.md).  
  
 If an ALTER TRIGGER statement changes a first or last trigger, the **First** or **Last** attribute is dropped and the order value is set to **None**. The order must be reset by using **sp_settriggerorder**.  
  
 The OBJECTPROPERTY function reports whether a trigger is a first or last trigger by using the following properties: **ExecIsFirstInsertTrigger**, **ExecIsFirstUpdateTrigger**, **ExecIsFirstDeleteTrigger**, **ExecIsLastInsertTrigger**, **ExecIsLastUpdateTrigger**, and **ExecIsLastDeleteTrigger**.  
  
 Replication automatically generates a first trigger for any table that is included in an immediate updating or queued updating subscription. Replication requires that its trigger be the first trigger. Replication raises an error when you try to include a table with a first trigger in an immediate updating or queued updating subscription. If you try to make a trigger a first trigger after a table has been included in a subscription, **sp_settriggerorder** returns an error. If you use ALTER on the replication trigger or use **sp_settriggerorder** to change the replication trigger to a last or none trigger, the subscription will not function correctly.  
  
## Related content

- [OBJECTPROPERTY (Transact-SQL)](../../t-sql/functions/objectproperty-transact-sql.md)
- [sys.sp_settriggerorder (Transact-SQL)](../system-stored-procedures/sp-settriggerorder-transact-sql.md)
