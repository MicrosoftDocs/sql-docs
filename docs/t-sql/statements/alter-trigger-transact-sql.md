---
title: "ALTER TRIGGER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/08/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER TRIGGER"
  - "ALTER_TRIGGER_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DDL triggers, modifying"
  - "triggers [SQL Server], modifying"
  - "modifying triggers"
  - "ALTER TRIGGER statement"
  - "DML triggers, modifying"
ms.assetid: 2a99c7c1-ac2f-4637-aa7c-3d1bf514e500
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER TRIGGER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Modifies the definition of a DML, DDL, or logon trigger that was previously created by the CREATE TRIGGER statement. Triggers are created by using CREATE TRIGGER. They can be created directly from [!INCLUDE[tsql](../../includes/tsql-md.md)] statements or from methods of assemblies that are created in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime (CLR) and uploaded to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information about the parameters that are used in the ALTER TRIGGER statement, see [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- SQL Server Syntax  
-- Trigger on an INSERT, UPDATE, or DELETE statement to a table or view (DML Trigger)  

ALTER TRIGGER schema_name.trigger_name   
ON  ( table | view )   
[ WITH <dml_trigger_option> [ ,...n ] ]  
 ( FOR | AFTER | INSTEAD OF )   
{ [ DELETE ] [ , ] [ INSERT ] [ , ] [ UPDATE ] }   
[ NOT FOR REPLICATION ]   
AS { sql_statement [ ; ] [ ...n ] | EXTERNAL NAME <method specifier>   
[ ; ] }   
  
<dml_trigger_option> ::=  
    [ ENCRYPTION ]  
    [ <EXECUTE AS Clause> ]  
  
<method_specifier> ::=  
    assembly_name.class_name.method_name  
  
-- Trigger on an INSERT, UPDATE, or DELETE statement to a table 
-- (DML Trigger on memory-optimized tables)  

ALTER TRIGGER schema_name.trigger_name   
ON  ( table  )   
[ WITH <dml_trigger_option> [ ,...n ] ]  
 ( FOR | AFTER )   
{ [ DELETE ] [ , ] [ INSERT ] [ , ] [ UPDATE ] }   
AS { sql_statement [ ; ] [ ...n ] }   
  
<dml_trigger_option> ::=  
    [ NATIVE_COMPILATION ]  
    [ SCHEMABINDING ]  
    [ <EXECUTE AS Clause> ]  
  
-- Trigger on a CREATE, ALTER, DROP, GRANT, DENY, REVOKE, 
-- or UPDATE statement (DDL Trigger)  
  
ALTER TRIGGER trigger_name   
ON { DATABASE | ALL SERVER }   
[ WITH <ddl_trigger_option> [ ,...n ] ]  
{ FOR | AFTER } { event_type [ ,...n ] | event_group }   
AS { sql_statement [ ; ] | EXTERNAL NAME <method specifier>   
[ ; ] }  
}   
  
<ddl_trigger_option> ::=  
    [ ENCRYPTION ]  
    [ <EXECUTE AS Clause> ]  
  
<method_specifier> ::=  
    assembly_name.class_name.method_name  
  
-- Trigger on a LOGON event (Logon Trigger)  

ALTER TRIGGER trigger_name   
ON ALL SERVER   
[ WITH <logon_trigger_option> [ ,...n ] ]  
{ FOR| AFTER } LOGON   
AS { sql_statement  [ ; ] [ ,...n ] | EXTERNAL NAME < method specifier >  
  [ ; ] }  
  
<logon_trigger_option> ::=  
    [ ENCRYPTION ]  
    [ EXECUTE AS Clause ]  
  
<method_specifier> ::=  
    assembly_name.class_name.method_name  
```  
  
```  
-- Windows Azure SQL Database Syntax   
-- Trigger on an INSERT, UPDATE, or DELETE statement to a table or view (DML Trigger)   
  
ALTER TRIGGER schema_name. trigger_name   
ON (table | view )   
 [ WITH <dml_trigger_option> [ ,...n ] ]   
 ( FOR | AFTER | INSTEAD OF )   
{ [ DELETE ] [ , ] [ INSERT ] [ , ] [ UPDATE ] }   
AS { sql_statement [ ; ] [...n ] }   
  
<dml_trigger_option> ::=   
    [ <EXECUTE AS Clause> ]   
  
-- Trigger on a CREATE, ALTER, DROP, GRANT, DENY, REVOKE, or UPDATE statement (DDL Trigger)   
  
ALTER TRIGGER trigger_name   
ON { DATABASE }   
 [ WITH <ddl_trigger_option> [ ,...n ] ]   
{ FOR | AFTER } { event_type [ ,...n ] | event_group }   
AS { sql_statement   
[ ; ] }  
}   
  
<ddl_trigger_option> ::=   
    [ <EXECUTE AS Clause> ]  
```  
  
## Arguments  
 *schema_name*  
 Is the name of the schema to which a DML trigger belongs. DML triggers are scoped to the schema of the table or view on which they are created. *schema**_name* is optional only if the DML trigger and its corresponding table or view belong to the default schema. *schema_name* cannot be specified for DDL or logon triggers.  
  
 *trigger_name*  
 Is the existing trigger to modify.  
  
 *table* | *view*  
 Is the table or view on which the DML trigger is executed. Specifying the fully-qualified name of the table or view is optional.  
  
 DATABASE  
 Applies the scope of a DDL trigger to the current database. If specified, the trigger fires whenever *event_type* or *event_group* occurs in the current database.  
  
 ALL SERVER  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Applies the scope of a DDL or logon trigger to the current server. If specified, the trigger fires whenever *event_type* or *event_group* occurs anywhere in the current server.  
  
 WITH ENCRYPTION  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Encrypts the sys.syscommentssys.sql_modules entries that contain the text of the ALTER TRIGGER statement. Using WITH ENCRYPTION prevents the trigger from being published as part of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication. WITH ENCRYPTION cannot be specified for CLR triggers.  
  
> [!NOTE]  
>  If a trigger is created by using WITH ENCRYPTION, it must be specified again in the ALTER TRIGGER statement for this option to remain enabled.  
  
 EXECUTE AS  
 Specifies the security context under which the trigger is executed. Enables you to control the user account the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses to validate permissions on any database objects that are referenced by the trigger.  
  
 For more information, see [EXECUTE AS Clause &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-clause-transact-sql.md).  
  
 NATIVE_COMPILATION  
 Indicates that the trigger is natively compiled.  
  
 This option is required for triggers on memory-optimized tables.  
  
 SCHEMABINDING  
 Ensures that tables that are referenced by a trigger cannot be dropped or altered.  
  
 This option is required for triggers on memory-optimized tables and is not supported for triggers on traditional tables.  
  
 AFTER  
 Specifies that the trigger is fired only after the triggering SQL statement is executed successfully. All referential cascade actions and constraint checks also must have been successful before this trigger fires.  
  
 AFTER is the default, if only the FOR keyword is specified.  
  
 DML AFTER triggers may be defined only on tables.  
  
 INSTEAD OF  
 Specifies that the DML trigger is executed instead of the triggering SQL statement, therefore, overriding the actions of the triggering statements. INSTEAD OF cannot be specified for DDL or logon triggers.  
  
 At most, one INSTEAD OF trigger per INSERT, UPDATE, or DELETE statement can be defined on a table or view. However, you can define views on views where each view has its own INSTEAD OF trigger.  
  
 INSTEAD OF triggers are not allowed on views created by using WITH CHECK OPTION. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] raises an error when an INSTEAD OF trigger is added to a view for which WITH CHECK OPTION was specified. The user must remove that option using ALTER VIEW before defining the INSTEAD OF trigger.  
  
 { [ DELETE ] [ , ] [ INSERT ] [ , ] [ UPDATE ] } | { [INSERT ] [ , ] [ UPDATE ] }  
 Specifies the data modification statements, when tried against this table or view, activate the DML trigger. At least one option must be specified. Any combination of these in any order is allowed in the trigger definition. If more than one option is specified, separate the options with commas.  
  
 For INSTEAD OF triggers, the DELETE option is not allowed on tables that have a referential relationship specifying a cascade action ON DELETE. Similarly, the UPDATE option is not allowed on tables that have a referential relationship specifying a cascade action ON UPDATE. For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).  
  
 *event_type*  
 Is the name of a [!INCLUDE[tsql](../../includes/tsql-md.md)] language event that, after execution, causes a DDL trigger to fire. Valid events for DDL triggers are listed in [DDL Events](../../relational-databases/triggers/ddl-events.md).  
  
 *event_group*  
 Is the name of a predefined grouping of [!INCLUDE[tsql](../../includes/tsql-md.md)] language events. The DDL trigger fires after execution of any [!INCLUDE[tsql](../../includes/tsql-md.md)] language event that belongs to *event_group*. Valid event groups for DDL triggers are listed in [DDL Event Groups](../../relational-databases/triggers/ddl-event-groups.md). After ALTER TRIGGER has finished running, *event_group* also acts as a macro by adding the event types it covers to the sys.trigger_events catalog view.  
  
 NOT FOR REPLICATION  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Indicates that the trigger should not be executed when a replication agent modifies the table involved in the trigger.  
  
 *sql_statement*  
 Is the trigger conditions and actions.  
  
 For triggers on memory-optimized tables, the only *sql_statement* allowed at the top level is an ATOMIC block. The T-SQL allowed inside the ATOMIC block is limited by the T-SQL allowed inside native procs.  
  
 EXTERNAL NAME \<method_specifier>  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the method of an assembly to bind with the trigger. The method must take no arguments and return void. *class_name* must be a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier and must exist as a class in the assembly with assembly visibility. The class cannot be a nested class.  
  
## Remarks  
 For more information about ALTER TRIGGER, see Remarks in [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md).  
  
> [!NOTE]  
>  The EXTERNAL_NAME and ON_ALL_SERVER options are not available in a contained database.  
  
## DML Triggers  
 ALTER TRIGGER supports manually updatable views through INSTEAD OF triggers on tables and views. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] applies ALTER TRIGGER the same way for all kinds of triggers (AFTER, INSTEAD-OF).  
  
 The first and last AFTER triggers to be executed on a table can be specified by using sp_settriggerorder. Only one first and one last AFTER trigger can be specified on a table. If there are other AFTER triggers on the same table, they are randomly executed.  
  
 If an ALTER TRIGGER statement changes a first or last trigger, the first or last attribute set on the modified trigger is dropped, and the order value must be reset by using sp_settriggerorder.  
  
 An AFTER trigger is executed only after the triggering SQL statement has executed successfully. This successful execution includes all referential cascade actions and constraint checks associated with the object updated or deleted. The AFTER trigger operation checks for the effects of the triggering statement and also all referential cascade UPDATE and DELETE actions that are caused by the triggering statement.  
  
 When a DELETE action to a child or referencing table is the result of a CASCADE on a DELETE from the parent table, and an INSTEAD OF trigger on DELETE is defined on that child table, the trigger is ignored and the DELETE action is executed.  
  
## DDL Triggers  
 Unlike DML triggers, DDL triggers are not scoped to schemas. Therefore, the OBJECT_ID, OBJECT_NAME, OBJECTPROPERTY, and OBJECTPROPERTY(EX) cannot be used when querying metadata about DDL triggers. Use the catalog views instead. For more information, see [Get Information About DDL Triggers](../../relational-databases/triggers/get-information-about-ddl-triggers.md).  
  
## Logon Triggers  
 [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] does not support triggers on logon events.  
  
## Permissions  
 To alter a DML trigger requires ALTER permission on the table or view on which the trigger is defined.  
  
 To alter a DDL trigger defined with server scope (ON ALL SERVER) or a logon trigger requires CONTROL SERVER permission on the server. To alter a DDL trigger defined with database scope (ON DATABASE) requires ALTER ANY DATABASE DDL TRIGGER permission in the current database.  
  
## Examples  
 The following example creates a DML trigger in the AdventureWorks 2012 database, that prints a user-defined message to the client when a user tries to add or change data in the `SalesPersonQuotaHistory` table. The trigger is then modified by using `ALTER TRIGGER` to apply the trigger only on `INSERT` activities. This trigger is helpful because it reminds the user that updates or inserts rows into this table to also notify the `Compensation` department.  
  
```  
CREATE TRIGGER Sales.bonus_reminder  
ON Sales.SalesPersonQuotaHistory  
WITH ENCRYPTION  
AFTER INSERT, UPDATE   
AS RAISERROR ('Notify Compensation', 16, 10);  
GO  

-- Now, change the trigger.  
ALTER TRIGGER Sales.bonus_reminder  
ON Sales.SalesPersonQuotaHistory  
AFTER INSERT  
AS RAISERROR ('Notify Compensation', 16, 10);  
GO  
```  
  
## See Also  
 [DROP TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-trigger-transact-sql.md)   
 [ENABLE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/enable-trigger-transact-sql.md)   
 [DISABLE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/disable-trigger-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sp_helptrigger &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptrigger-transact-sql.md)   
 [Create a Stored Procedure](../../relational-databases/stored-procedures/create-a-stored-procedure.md)   
 [sp_addmessage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmessage-transact-sql.md)   
 [Transactions](../../relational-databases/native-client-ole-db-transactions/transactions.md)   
 [Get Information About DML Triggers](../../relational-databases/triggers/get-information-about-dml-triggers.md)   
 [Get Information About DDL Triggers](../../relational-databases/triggers/get-information-about-ddl-triggers.md)   
 [sys.triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-triggers-transact-sql.md)   
 [sys.trigger_events &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-trigger-events-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [sys.assembly_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-assembly-modules-transact-sql.md)   
 [sys.server_triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-triggers-transact-sql.md)   
 [sys.server_trigger_events &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-trigger-events-transact-sql.md)   
 [sys.server_sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-sql-modules-transact-sql.md)   
 [sys.server_assembly_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-assembly-modules-transact-sql.md)   
 [Make Schema Changes on Publication Databases](../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md)  
  
  
