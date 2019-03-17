---
title: "Specify First and Last Triggers | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "first triggers [SQL Server]"
  - "last triggers"
  - "DML triggers, first or last triggers"
  - "INSTEAD OF triggers"
  - "AFTER triggers"
ms.assetid: 9e6c7684-3dd3-46bb-b7be-523b33fae4d5
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specify First and Last Triggers
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]
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
  
 The OBJECTPROPERTY function reports whether a trigger is a first or last trigger by using the followwing properties: **ExecIsFirstInsertTrigger**, **ExecIsFirstUpdateTrigger**, **ExecIsFirstDeleteTrigger**, **ExecIsLastInsertTrigger**, **ExecIsLastUpdateTrigger**, and **ExecIsLastDeleteTrigger**.  
  
 Replication automatically generates a first trigger for any table that is included in an immediate updating or queued updating subscription. Replication requires that its trigger be the first trigger. Replication raises an error when you try to include a table with a first trigger in an immediate updating or queued updating subscription. If you try to make a trigger a first trigger after a table has been included in a subscription, **sp_settriggerorder** returns an error. If you use ALTER on the replication trigger or use **sp_settriggerorder** to change the replication trigger to a last or none trigger, the subscription will not function correctly.  
  
## See Also  
 [OBJECTPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/objectproperty-transact-sql.md)   
 [sp_settriggerorder &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-settriggerorder-transact-sql.md)  
  
  
