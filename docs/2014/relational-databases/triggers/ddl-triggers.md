---
title: "DDL Triggers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "DDL triggers, about DDL triggers"
ms.assetid: 1a4a6564-9820-4a14-9305-2c0e9ea37454
author: rothja
ms.author: jroth
manager: craigg
---
# DDL Triggers
  DDL triggers fire in response to a variety of Data Definition Language (DDL) events. These events primarily correspond to [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that start with the keywords CREATE, ALTER, DROP, GRANT, DENY, REVOKE or UPDATE STATISTICS. Certain system stored procedures that perform DDL-like operations can also fire DDL triggers.  
  
 Use DDL triggers when you want to do the following:  
  
-   Prevent certain changes to your database schema.  
  
-   Have something occur in the database in response to a change in your database schema.  
  
-   Record changes or events in the database schema.  
  
> [!IMPORTANT]  
>  Test your DDL triggers to determine their responses to system stored procedures that are run. For example, the CREATE TYPE statement and the **sp_addtype** stored procedure will both fire a DDL trigger that is created on a CREATE_TYPE event.  
  
## Types of DDL Triggers  
 Transact-SQL DDL Trigger  
 A special type of [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure that executes one or more [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in response to a server-scoped or database-scoped event. For example, a DDL Trigger may fire if a statement such as ALTER SERVER CONFIGURATION is executed or if a table is deleted by using DROP TABLE.  
  
 CLR DDL Trigger  
 Instead of executing a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure, a CLR trigger executes one or more methods written in managed code that are members of an assembly created in the .NET Framework and uploaded in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 DDL triggers fire only after the DDL statements that trigger them are run. DDL triggers cannot be used as INSTEAD OF triggers. DDL triggers do not fire in response to events that affect local or global temporary tables and stored procedures.  
  
 DDL triggers do not create the special `inserted` and `deleted` tables.  
  
 The information about an event that fires a DDL trigger, and the subsequent changes caused by the trigger, is captured by using the EVENTDATA function.  
  
 Multiple triggers to be created for each DDL event.  
  
 Unlike DML triggers, DDL triggers are not scoped to schemas. Therefore, functions such as OBJECT_ID, OBJECT_NAME, OBJECTPROPERTY, and OBJECTPROPERTYEX cannot be used for querying metadata about DDL triggers. Use the catalog views instead.  
  
 Server-scoped DDL triggers appear in the SQL Server Management Studio Object Explorer in the **Triggers** folder. This folder is located under the **Server Objects** folder. Database-scoped DDL triggers appear in the **Database Triggers** folder. This folder is located under the **Programmability** folder of the corresponding database.  
  
> [!IMPORTANT]  
>  Malicious code inside triggers can run under escalated privileges. For more information about how to help reduce this threat, see [Manage Trigger Security](manage-trigger-security.md).  
  
## DDL Trigger Scope  
 DDL triggers can fire in response to a [!INCLUDE[tsql](../../includes/tsql-md.md)] event processed in the current database, or on the current server. The scope of the trigger depends on the event. For example, a DDL trigger created to fire in response to a CREATE_TABLE event can do so whenever a CREATE_TABLE event occurs in the database, or on the server instance. A DDL trigger created to fire in response to a CREATE_LOGIN event can do so only when a CREATE_LOGIN event occurs in the server instance.  
  
 In the following example, DDL trigger `safety` will fire whenever a `DROP_TABLE` or `ALTER_TABLE` event occurs in the database.  
  
```  
CREATE TRIGGER safety   
ON DATABASE   
FOR DROP_TABLE, ALTER_TABLE   
AS   
   PRINT 'You must disable Trigger "safety" to drop or alter tables!'   
   ROLLBACK;  
```  
  
 In the following example, a DDL trigger prints a message if any `CREATE_DATABASE` event occurs on the current server instance. The example uses the `EVENTDATA` function to retrieve the text of the corresponding [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. For more information about how to use EVENTDATA with DDL triggers, see [Use the EVENTDATA Function](use-the-eventdata-function.md).  
  
```  
IF EXISTS (SELECT * FROM sys.server_triggers  
    WHERE name = 'ddl_trig_database')  
DROP TRIGGER ddl_trig_database  
ON ALL SERVER;  
GO  
CREATE TRIGGER ddl_trig_database   
ON ALL SERVER   
FOR CREATE_DATABASE   
AS   
    PRINT 'Database Created.'  
    SELECT EVENTDATA().value('(/EVENT_INSTANCE/TSQLCommand/CommandText)[1]','nvarchar(max)')  
GO  
DROP TRIGGER ddl_trig_database  
ON ALL SERVER;  
GO  
  
```  
  
 The lists that map the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to the scopes that can be specified for them are available through the links provided in the section "Selecting a Particular DDL Statement to Fire a DDL Trigger," later in this topic.  
  
 Database-scoped DDL triggers are stored as objects in the database in which they are created. DDL triggers can be created in the **master** database and behave just like those created in user-designed databases. You can obtain information about DDL triggers by querying the **sys.triggers** catalog view. You can query **sys.triggers** within the database context in which the triggers are created or by specifying the database name as an identifier, such as **master.sys.triggers**.  
  
 Server-scoped DDL triggers are stored as objects in the **master** database. However, you can obtain information about server-scoped DDL triggers by querying the **sys.server_triggers** catalog view in any database context.  
  
## Specifying a Transact-SQL Statement or Group of Statements  
  
### Selecting a Particular DDL Statement to Fire a DDL Trigger  
 DDL triggers can be designed to fire after one or more particular [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are run. In the previous example, trigger `safety` fires after any `DROP_TABLE` or `ALTER_TABLE` event. For lists of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that can be specified to fire a DDL trigger, and the scope at which the trigger can fire, see [DDL Events](ddl-events.md).  
  
### Selecting a Predefined Group of DDL Statements to Fire a DDL Trigger  
 A DDL trigger can fire after execution of any [!INCLUDE[tsql](../../includes/tsql-md.md)] event that belongs to a predefined grouping of similar events. For example, if you want a DDL trigger to fire after any CREATE TABLE, ALTER TABLE, or DROP TABLE statement is run, you can specify FOR DDL_TABLE_EVENTS in the CREATE TRIGGER statement. After CREATE TRIGGER is run, the events that are covered by an event group are added to the **sys.trigger_events** catalog view.  
  
 In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], if a trigger is created on an event group, **sys.trigger_events** does not include information about the event group, **sys.trigger_events** includes information only about the individual events covered by that group. In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and higher, **sys.trigger_events** persists metadata about the event group on which the triggers is created, and also about the individual events that the event group covers. Therefore, changes to the events that are covered by event groups in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and higher do not apply to DDL triggers that are created on those event groups in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
 For a list of the predefined groups of DDL statements that are available for DDL triggers, the particular statements the event groups cover, and the scopes at which these event groups can be programmed, see [DDL Event Groups](ddl-event-groups.md).  
  
## Related Tasks  
  
|Task|Topic|  
|----------|-----------|  
|Describes how to create, modify, delete or disable DDL triggers.|[Implement DDL Triggers](implement-ddl-triggers.md)|  
|Describes how to create a CLR DDL trigger.|[Create CLR Triggers](create-clr-triggers.md)|  
|Describes how to return information about DDL triggers.|[Get Information About DDL Triggers](get-information-about-ddl-triggers.md)|  
|Describes how to return information about an event that fires a DDL trigger by using the EVENTDATA function.|[Use the EVENTDATA Function](use-the-eventdata-function.md)|  
|Describes how to manage trigger security.|[Manage Trigger Security](manage-trigger-security.md)|  
  
## See Also  
 [DML Triggers](dml-triggers.md)   
 [Logon Triggers](logon-triggers.md)   
 [CREATE TRIGGER &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-trigger-transact-sql)  
  
  
