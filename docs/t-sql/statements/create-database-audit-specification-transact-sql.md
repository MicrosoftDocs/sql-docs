---
title: "CREATE DATABASE AUDIT SPECIFICATION"
titleSuffix: SQL Server (Transact-SQL)
description: Create a database audit specification object using the SQL Server audit feature.
author: sravanisaluru
ms.author: srsaluru
ms.date: "03/23/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: seo-lt-2019
f1_keywords:
  - "CREATE DATABASE AUDIT"
  - "DATABASE_AUDIT_SPECIFICATION_TSQL"
  - "DATABASE AUDIT SPECIFICATION"
  - "CREATE_DATABASE_AUDIT_SPECIFICATION_TSQL"
  - "CREATE_DATABASE_AUDIT_TSQL"
  - "CREATE DATABASE AUDIT SPECIFICATION"
helpviewer_keywords:
  - "database audit specification"
  - "CREATE DATABASE AUDIT SPECIFICATION statement"
dev_langs:
  - "TSQL"
---
# CREATE DATABASE AUDIT SPECIFICATION (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates a database audit specification object using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] audit feature. For more information, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE DATABASE AUDIT SPECIFICATION audit_specification_name  
{  
    FOR SERVER AUDIT audit_name   
        [ { ADD ( { <audit_action_specification> | audit_action_group_name } )   
      } [, ...n] ]  
    [ WITH ( STATE = { ON | OFF } ) ]  
}  
[ ; ]  
<audit_action_specification>::=  
{  
      action [ ,...n ]ON [ class :: ] securable BY principal [ ,...n ]  
}  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *audit_specification_name*  
 Is the name of the audit specification.  
  
 *audit_name*  
 Is the name of the audit to which this specification is applied.  
  
 *audit_action_specification*  
 Is the specification of actions on securables by principals that should be recorded in the audit.  
  
 *action*  
 Is the name of one or more database-level auditable actions. For a list of audit actions, see [SQL Server Audit Action Groups and Actions](../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md).  
  
 *audit_action_group_name*  
 Is the name of one or more groups of database-level auditable actions. For a list of audit action groups, see [SQL Server Audit Action Groups and Actions](../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md).  
  
 *class*  
 Is the class name (if applicable) on the securable.  
  
 *securable*  
 Is the table, view, or other securable object in the database on which to apply the audit action or audit action group. For more information, see [Securables](../../relational-databases/security/securables.md).  
  
 *principal*  
 Is the name of database principal on which to apply the audit action or audit action group. To audit all database principals use the database principal `public`. For more information, see [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md).  
  
 WITH ( STATE = { ON | OFF } )  
 Enables or disables the audit from collecting records for this audit specification.  
  
## Remarks  
 Database audit specifications are non-securable objects that reside in a given database. When a database audit specification is created, it is in a disabled state.  
  
## Permissions  
 Users with the `ALTER ANY DATABASE AUDIT` permission can create database audit specifications and bind them to any audit.  
  
 After a database audit specification is created, it can be viewed by users with the `CONTROL SERVER` permission, or the `sysadmin` account.  
  
## Examples

### A. Audit SELECT and INSERT on a table for any database principal 
 The following example creates a server audit called `Payrole_Security_Audit` and then a database audit specification called `Payrole_Security_Audit` that audits `SELECT` and `INSERT` statements by any member of the `public` database role, for the `HumanResources.EmployeePayHistory` table in the `AdventureWorks2012` database. This has the effect that every user is audited as every user is always member of the `public` role.
  
```sql  
USE master ;  
GO  
-- Create the server audit.  
CREATE SERVER AUDIT Payrole_Security_Audit  
    TO FILE ( FILEPATH =   
'D:\SQLAudit\' ) ;  -- make sure this path exists
GO  
-- Enable the server audit.  
ALTER SERVER AUDIT Payrole_Security_Audit   
WITH (STATE = ON) ;  
GO  
-- Move to the target database.  
USE AdventureWorks2012 ;  
GO  
-- Create the database audit specification.  
CREATE DATABASE AUDIT SPECIFICATION Audit_Pay_Tables  
FOR SERVER AUDIT Payrole_Security_Audit  
ADD (SELECT , INSERT  
     ON HumanResources.EmployeePayHistory BY public )  
WITH (STATE = ON) ;  
GO  
``` 

### B. Audit any DML (INSERT, UPDATE or DELETE) on _all_ objects in the _sales_ schema for a specific database role  
 The following example creates a server audit called `DataModification_Security_Audit` and then a database audit specification called `Audit_Data_Modification_On_All_Sales_Tables` that audits `INSERT`, `UPDATE` and `DELETE` statements by users in a new database role `SalesUK`, for all objects in the `Sales` schema in the `AdventureWorks2012` database.  
  
```sql  
USE master ;  
GO  
-- Create the server audit.
-- Change the path to a path that the SQLServer Service has access to. 
CREATE SERVER AUDIT DataModification_Security_Audit  
    TO FILE ( FILEPATH = 
'D:\SQLAudit\' ) ;  -- make sure this path exists
GO  
-- Enable the server audit.  
ALTER SERVER AUDIT DataModification_Security_Audit   
WITH (STATE = ON) ;  
GO  
-- Move to the target database.  
USE AdventureWorks2012 ;  
GO  
CREATE ROLE SalesUK
GO
-- Create the database audit specification.  
CREATE DATABASE AUDIT SPECIFICATION Audit_Data_Modification_On_All_Sales_Tables  
FOR SERVER AUDIT DataModification_Security_Audit  
ADD ( INSERT, UPDATE, DELETE  
     ON Schema::Sales BY SalesUK )  
WITH (STATE = ON) ;    
GO  
```  


## See Also  
 [CREATE SERVER AUDIT &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-transact-sql.md)   
 [ALTER SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-transact-sql.md)   
 [DROP SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-transact-sql.md)   
 [CREATE SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-specification-transact-sql.md)   
 [ALTER SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)   
 [DROP SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)   
 [CREATE DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/create-database-audit-specification-transact-sql.md)   
 [ALTER DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-audit-specification-transact-sql.md)   
 [DROP DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-audit-specification-transact-sql.md)   
 [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md)   
 [sys.fn_get_audit_file &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)   
 [sys.server_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)   
 [sys.server_file_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)   
 [sys.server_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)   
 [sys.server_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)   
 [sys.database_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)   
 [sys.database_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)   
 [sys.dm_server_audit_status &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)   
 [sys.dm_audit_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)   
 [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)  
  
  
