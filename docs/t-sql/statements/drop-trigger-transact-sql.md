---
title: "DROP TRIGGER (Transact-SQL)"
description: DROP TRIGGER (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/12/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP TRIGGER"
  - "DROP_TRIGGER_TSQL"
helpviewer_keywords:
  - "renaming triggers"
  - "triggers [SQL Server], removing"
  - "DDL triggers, removing"
  - "DROP TRIGGER statement"
  - "deleting triggers"
  - "dropping triggers"
  - "removing triggers"
  - "DML triggers, removing"
dev_langs:
  - "TSQL"
---
# DROP TRIGGER (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Removes one or more DML or DDL triggers from the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Trigger on an INSERT, UPDATE, or DELETE statement to a table or view (DML Trigger)  
  
DROP TRIGGER [ IF EXISTS ] [schema_name.]trigger_name [ ,...n ] [ ; ]  
  
-- Trigger on a CREATE, ALTER, DROP, GRANT, DENY, REVOKE or UPDATE statement (DDL Trigger)  
  
DROP TRIGGER [ IF EXISTS ] trigger_name [ ,...n ]   
ON { DATABASE | ALL SERVER }   
[ ; ]  
  
-- Trigger on a LOGON event (Logon Trigger)  
  
DROP TRIGGER [ IF EXISTS ] trigger_name [ ,...n ]   
ON ALL SERVER  
```  

  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level), [!INCLUDE[sssds](../../includes/sssds-md.md)]).  
  
 Conditionally drops the trigger only if it already exists.  
  
 *schema_name*  
 Is the name of the schema to which a DML trigger belongs. DML triggers are scoped to the schema of the table or view on which they are created. *schema_name* cannot be specified for DDL or logon triggers.  
  
 *trigger_name*  
 Is the name of the trigger to remove. To see a list of currently created triggers, use [sys.server_assembly_modules](../../relational-databases/system-catalog-views/sys-triggers-transact-sql.md) or [sys.server_triggers](../../relational-databases/system-catalog-views/sys-server-triggers-transact-sql.md).  
  
 DATABASE  
 Indicates the scope of the DDL trigger applies to the current database. DATABASE must be specified if it was also specified when the trigger was created or modified.  
  
 ALL SERVER  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.  
  
 Indicates the scope of the DDL trigger applies to the current server. ALL SERVER must be specified if it was also specified when the trigger was created or modified. ALL SERVER also applies to logon triggers.  
  
> [!NOTE]  
>  This option is not available in a contained database.  
  
## Remarks  
 You can remove a DML trigger by dropping it or by dropping the trigger table. When a table is dropped, all associated triggers are also dropped.  
  
 When a trigger is dropped, information about the trigger is removed from the **sys.objects**, **sys.triggers** and **sys.sql_modules** catalog views.  
  
 Multiple DDL triggers can be dropped per DROP TRIGGER statement only if all triggers were created using identical ON clauses.  
  
 To rename a trigger, use DROP TRIGGER and CREATE TRIGGER. To change the definition of a trigger, use ALTER TRIGGER.  
  
 For more information about determining dependencies for a specific trigger, see [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md), [sys.dm_sql_referenced_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql.md), and [sys.dm_sql_referencing_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md).  
  
 For more information about viewing the text of the trigger, see [sp_helptext &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md) and [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md).  
  
 For more information about viewing a list of existing triggers, see [sys.triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-triggers-transact-sql.md) and [sys.server_triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-triggers-transact-sql.md).  
  
## Permissions  
 To drop a DML trigger requires ALTER permission on the table or view on which the trigger is defined.  
  
 To drop a DDL trigger defined with server scope (ON ALL SERVER) or a logon trigger requires CONTROL SERVER permission in the server. To drop a DDL trigger defined with database scope (ON DATABASE) requires ALTER ANY DATABASE DDL TRIGGER permission in the current database.  
  
## Examples  
  
### A. Dropping a DML trigger  
 The following example drops the `employee_insupd` trigger in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. (Beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] you can use the DROP TRIGGER IF EXISTS syntax.)  
  
```sql  
IF OBJECT_ID ('employee_insupd', 'TR') IS NOT NULL  
   DROP TRIGGER employee_insupd;  
```  
  
### B. Dropping a DDL trigger  
 The following example drops DDL trigger `safety`.  
  
> [!IMPORTANT]  
>  Because DDL triggers are not schema-scoped and, therefore do not appear in the **sys.objects** catalog view, the OBJECT_ID function cannot be used to query whether they exist in the database. Objects that are not schema-scoped must be queried by using the appropriate catalog view. For DDL triggers, use **sys.triggers**.  
  
```sql  
DROP TRIGGER safety  
ON DATABASE;  
```  
  
## See Also  
 [ALTER TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-trigger-transact-sql.md)   
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 [ENABLE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/enable-trigger-transact-sql.md)   
 [DISABLE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/disable-trigger-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Get Information About DML Triggers](../../relational-databases/triggers/get-information-about-dml-triggers.md)   
 [sp_help &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)   
 [sp_helptrigger &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptrigger-transact-sql.md)   
 [sys.triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-triggers-transact-sql.md)   
 [sys.trigger_events &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-trigger-events-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [sys.assembly_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-assembly-modules-transact-sql.md)   
 [sys.server_triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-triggers-transact-sql.md)   
 [sys.server_trigger_events &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-trigger-events-transact-sql.md)   
 [sys.server_sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-sql-modules-transact-sql.md)   
 [sys.server_assembly_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-assembly-modules-transact-sql.md)  
