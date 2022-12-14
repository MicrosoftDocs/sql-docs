---
title: "DISABLE TRIGGER (Transact-SQL)"
description: DISABLE TRIGGER (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "05/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DISABLE_TSQL"
  - "DISABLE"
  - "DISABLE TRIGGER"
  - "DISABLE_TRIGGER_TSQL"
helpviewer_keywords:
  - "DML triggers, disabling"
  - "DDL triggers, disabling"
  - "DISABLE TRIGGER statement"
  - "triggers [SQL Server], disabling"
  - "disabling triggers"
dev_langs:
  - "TSQL"
---
# DISABLE TRIGGER (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Disables a trigger.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DISABLE TRIGGER { [ schema_name . ] trigger_name [ ,...n ] | ALL }  
ON { object_name | DATABASE | ALL SERVER } [ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *schema_name*  
 Is the name of the schema to which the trigger belongs. *schema_name* cannot be specified for DDL or logon triggers.  
  
 *trigger_name*  
 Is the name of the trigger to be disabled.  
  
 ALL  
 Indicates that all triggers defined at the scope of the ON clause are disabled.  
  
> [!CAUTION]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates triggers in databases that are published for merge replication. Specifying ALL in published databases disables these triggers, which disrupts replication. Verify that the current database is not published for merge replication before specifying ALL.  
  
 *object_name*  
 Is the name of the table or view on which the DML trigger *trigger_name* was created to execute.  
  
 DATABASE  
 For a DDL trigger, indicates that *trigger_name* was created or modified to execute with database scope.  
  
 ALL SERVER  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.  
  
 For a DDL trigger, indicates that *trigger_name* was created or modified to execute with server scope. ALL SERVER also applies to logon triggers.  
  
> [!NOTE]  
>  This option is not available in a contained database.  
  
## Remarks  
 Triggers are enabled by default when they are created. Disabling a trigger does not drop it. The trigger still exists as an object in the current database. However, the trigger does not fire when any [!INCLUDE[tsql](../../includes/tsql-md.md)] statements on which it was programmed are executed. Triggers can be re-enabled by using [ENABLE TRIGGER](../../t-sql/statements/enable-trigger-transact-sql.md). DML triggers defined on tables can be also be disabled or enabled by using [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).  
  
 Changing the trigger by using the **ALTER TRIGGER** statement enables the trigger.  
  
## Permissions  
 To disable a DML trigger, at a minimum, a user must have ALTER permission on the table or view on which the trigger was created.  
  
 To disable a DDL trigger with server scope (ON ALL SERVER) or a logon trigger, a user must have CONTROL SERVER permission on the server. To disable a DDL trigger with database scope (ON DATABASE), at a minimum, a user must have ALTER ANY DATABASE DDL TRIGGER permission in the current database.  
  
## Examples  
The following examples are described in the AdventureWorks2012 database.
  
### A. Disabling a DML trigger on a table  
 The following example disables trigger `uAddress` that was created on table `Person`.  
  
```sql  
DISABLE TRIGGER Person.uAddress ON Person.Address;  
GO  
```  
  
### B. Disabling a DDL trigger  
 The following example creates a DDL trigger `safety` with database scope, and then disables it.  
  
```sql  
CREATE TRIGGER safety   
ON DATABASE   
FOR DROP_TABLE, ALTER_TABLE   
AS   
   PRINT 'You must disable Trigger "safety" to drop or alter tables!'   
   ROLLBACK;  
GO  
DISABLE TRIGGER safety ON DATABASE;  
GO  
```  
  
### C. Disabling all triggers that were defined with the same scope  
 The following example disables all DDL triggers that were created at the server scope.  
  
```sql  
DISABLE Trigger ALL ON ALL SERVER;  
GO  
```  
  
## See Also  
 [ENABLE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/enable-trigger-transact-sql.md)   
 [ALTER TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-trigger-transact-sql.md)   
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 [DROP TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-trigger-transact-sql.md)   
 [sys.triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-triggers-transact-sql.md)  
  
  
