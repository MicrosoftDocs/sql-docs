---
title: "ENABLE TRIGGER (Transact-SQL)"
description: ENABLE TRIGGER (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/12/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ENABLE TRIGGER"
  - "ENABLE_TSQL"
  - "ENABLE_TRIGGER_TSQL"
  - "ENABLE"
helpviewer_keywords:
  - "DDL triggers, enabling"
  - "triggers [SQL Server], enabling"
  - "DML triggers, enabling"
  - "ENABLE TRIGGER statement"
dev_langs:
  - "TSQL"
---
# ENABLE TRIGGER (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Enables a DML, DDL, or logon trigger.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ENABLE TRIGGER { [ schema_name . ] trigger_name [ ,...n ] | ALL }  
ON { object_name | DATABASE | ALL SERVER } [ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*schema_name*  
Is the name of the schema to which the trigger belongs. *schema_name* can't be specified for DDL or logon triggers.  
  
*trigger_name*  
Is the name of the trigger to be enabled.  
  
ALL  
Indicates that all triggers defined at the scope of the ON clause are enabled.  
  
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
Enabling a trigger doesn't re-create it. A disabled trigger still exists as an object in the current database, but doesn't fire. To enable a trigger, causes it to fire when any [!INCLUDE[tsql](../../includes/tsql-md.md)] statements on which it was originally programmed are run. Triggers are disabled by using [DISABLE TRIGGER](../../t-sql/statements/disable-trigger-transact-sql.md). DML triggers defined on tables can also be disabled or enabled by using [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).  
  
## Permissions  
To enable a DML trigger, at a minimum, a user needs ALTER permission on the table or view on which the trigger was created.  
  
To enable a DDL trigger with server scope (ON ALL SERVER) or a logon trigger, a user needs CONTROL SERVER permission on the server. To enable a DDL trigger with database scope (ON DATABASE), at a minimum, a user needs ALTER ANY DATABASE DDL TRIGGER permission in the current database.  
  
## Examples  
  
### A. Enabling a DML trigger on a table  
The following example disables trigger `uAddress` that was created on table `Address` in the AdventureWorks database, and then enables it.  
  
```sql  
DISABLE TRIGGER Person.uAddress ON Person.Address;  
GO  
ENABLE Trigger Person.uAddress ON Person.Address;  
GO  
```  
  
### B. Enabling a DDL trigger  
The following example creates a DDL trigger `safety` with database scope, and then disables and enables it.  
  
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
ENABLE TRIGGER safety ON DATABASE;  
GO  
```  
  
### C. Enabling all triggers that were defined with the same scope  
The following example enables all DDL triggers that were created at the server scope.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.  
  
```sql  
ENABLE Trigger ALL ON ALL SERVER;  
GO  
```  
  
## See Also  
 [DISABLE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/disable-trigger-transact-sql.md)   
 [ALTER TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-trigger-transact-sql.md)   
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 [DROP TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-trigger-transact-sql.md)   
 [sys.triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-triggers-transact-sql.md)  
  
  
