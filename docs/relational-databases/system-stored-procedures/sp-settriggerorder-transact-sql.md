---
description: "sp_settriggerorder (Transact-SQL)"
title: "sp_settriggerorder (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_settriggerorder"
  - "sp_settriggerorder_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_settriggerorder"
ms.assetid: 8b75c906-7315-486c-bc59-293ef12078e8
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_settriggerorder (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Specifies the AFTER triggers that are fired first or last. The AFTER triggers that are fired between the first and last triggers are executed in undefined order.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_settriggerorder [ @triggername = ] '[ triggerschema. ] triggername'   
    , [ @order = ] 'value'   
    , [ @stmttype = ] 'statement_type'   
    [ , [ @namespace = ] { 'DATABASE' | 'SERVER' | NULL } ]  
```  
  
## Arguments  
`[ @triggername = ] '[ _triggerschema.] _triggername'`
 Is the name of the trigger and the schema to which it belongs, if applicable, whose order is to be set or changed. [_triggerschema_**.**]*triggername* is **sysname**. If the name does not correspond to a trigger or if the name corresponds to an INSTEAD OF trigger, the procedure returns an error. *triggerschema* cannot be specified for DDL or logon triggers.  
  
`[ @order = ] 'value'`
 Is the setting for the new order of the trigger. *value* is **varchar(10)** and it can be any one of the following values.  
  
> [!IMPORTANT]  
>  The **First** and **Last** triggers must be two different triggers.  
  
|Value|Description|  
|-----------|-----------------|  
|**First**|Trigger is fired first.|  
|**Last**|Trigger is fired last.|  
|**None**|Trigger is fired in undefined order.|  
  
`[ @stmttype = ] 'statement_type'`
 Specifies the SQL statement that fires the trigger. *statement_type* is **varchar(50)** and can be INSERT, UPDATE, DELETE, LOGON, or any [!INCLUDE[tsql](../../includes/tsql-md.md)] statement event listed in [DDL Events](../../relational-databases/triggers/ddl-events.md). Event groups cannot be specified.  
  
 A trigger can be designated as the **First** or **Last** trigger for a statement type only after that trigger has been defined as a trigger for that statement type. For example, trigger **TR1** can be designated **First** for INSERT on table **T1** if **TR1** is defined as an INSERT trigger. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] returns an error if **TR1**, which has been defined only as an INSERT trigger, is set as a **First**, or **Last**, trigger for an UPDATE statement. For more information, see the Remarks section.  
  
 **\@namespace=** { **'DATABASE'** | **'SERVER'** | NULL }  
 When *triggername* is a DDL trigger, **\@namespace** specifies whether *triggername* was created with database scope or server scope. If *triggername* is a logon trigger, SERVER must be specified. For more information about DDL trigger scope, see [DDL Triggers](../../relational-databases/triggers/ddl-triggers.md). If not specified, or if NULL is specified, *triggername* is a DML trigger.  
  
* SERVER applies to: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.
  
## Return Code Values  
 0 (success) and 1 (failure)  
  
## Remarks  
  
## DML Triggers  
 There can be only one **First** and one **Last** trigger for each statement on a single table.  
  
 If a **First** trigger is already defined on the table, database, or server, you cannot designate a new trigger as **First** for the same table, database, or server for the same *statement_type*. This restriction also applies **Last** triggers.  
  
 Replication automatically generates a first trigger for any table that is included in an immediate updating or queued updating subscription. Replication requires that its trigger be the first trigger. Replication raises an error when you try to include a table with a first trigger in an immediate updating or queued updating subscription. If you try to make a trigger a first trigger after a table has been included in a subscription, **sp_settriggerorder** returns an error. If you use ALTER TRIGGER on the replication trigger, or use **sp_settriggerorder** to change the replication trigger to a **Last** or **None** trigger, the subscription does not function correctly.  
  
## DDL Triggers  
 If a DDL trigger with database scope and a DDL trigger with server scope exist on the same event, you can specify that both triggers be a **First** trigger or a **Last** trigger. However, server-scoped triggers always fire first. In general, the order of execution of DDL triggers that exist on the same event is as follows:  
  
1.  The server-level trigger marked **First**.  
  
2.  Other server-level triggers.  
  
3.  The server-level trigger marked **Last**.  
  
4.  The database-level trigger marked **First**.  
  
5.  Other database-level triggers.  
  
6.  The database-level trigger marked **Last**.  
  
## General Trigger Considerations  
 If an ALTER TRIGGER statement changes a first or last trigger, the **First** or **Last** attribute originally set on the trigger is dropped, and the value is replaced by **None**. The order value must be reset by using **sp_settriggerorder**.  
  
 If the same trigger must be designated as the first or last order for more than one statement type, **sp_settriggerorder** must be executed for each statement type. Also, the trigger must be first defined for a statement type before it can be designated as the **First** or **Last** trigger to fire for that statement type.  
  
## Permissions  
 To set the order of a DDL trigger with server scope (created ON ALL SERVER) or a logon trigger requires CONTROL SERVER permission.  
  
 To set the order of a DDL trigger with database scope (created ON DATABASE) requires ALTER ANY DATABASE DDL TRIGGER permission.  
  
 To set the order of a DML trigger requires ALTER permission on the table or view on which the trigger is defined.  
  
## Examples  
  
### A. Setting the firing order for a DML trigger  
 The following example specifies that trigger `uSalesOrderHeader` be the first trigger to fire after an `UPDATE` operation occurs on the `Sales.SalesOrderHeader` table.  
  
```  
USE AdventureWorks2012;  
GO  
sp_settriggerorder @triggername= 'Sales.uSalesOrderHeader', @order='First', @stmttype = 'UPDATE';  
```  
  
### B. Setting the firing order for a DDL trigger  
 The following example specifies that trigger `ddlDatabaseTriggerLog` be the first trigger to fire after an `ALTER_TABLE` event occurs in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
USE AdventureWorks2012;  
GO  
sp_settriggerorder @triggername= 'ddlDatabaseTriggerLog', @order='First', @stmttype = 'ALTER_TABLE', @namespace = 'DATABASE';  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [ALTER TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-trigger-transact-sql.md)  
  
  
