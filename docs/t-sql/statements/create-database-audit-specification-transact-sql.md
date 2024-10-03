---
title: "CREATE DATABASE AUDIT SPECIFICATION"
titleSuffix: SQL Server (Transact-SQL)
description: Learn how to create a database audit specification object using the SQL Server audit feature.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: randolphwest
ms.date: 09/06/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a database audit specification object using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] audit feature. For more information, see [SQL Server Audit (Database Engine)](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CREATE DATABASE AUDIT SPECIFICATION audit_specification_name
{
    FOR SERVER AUDIT audit_name
        [ ADD (
            { <audit_action_specification> | audit_action_group_name }
            [ , ...n ] )
        ]
        [ WITH ( STATE = { ON | OFF } ) ]
}
[ ; ]
<audit_action_specification>::=
{
    action [ , ...n ] ON [class::]securable BY principal [ , ...n ]
}
```

## Arguments

#### *audit_specification_name*

The name of the audit specification.

#### *audit_name*

The name of the audit to which this specification is applied.

#### *audit_action_specification*

The specification of actions on securables by principals that should be recorded in the audit.

#### *action*

The name of one or more database-level auditable actions. For a list of audit actions, see [SQL Server Audit Action Groups and Actions](../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md).

#### *audit_action_group_name*

The name of one or more groups of database-level auditable actions. For a list of audit action groups, see [SQL Server Audit Action Groups and Actions](../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md).

#### *class*

The class name (if applicable) on the securable.

#### *securable*

The table, view, or other securable object in the database on which to apply the audit action or audit action group. For more information, see [Securables](../../relational-databases/security/securables.md).

#### *principal*

The name of database principal on which to apply the audit action or audit action group. To audit all database principals, use the **public** database principal. For more information, see [Principals (Database Engine)](../../relational-databases/security/authentication-access/principals-database-engine.md).

#### WITH ( STATE = { ON | OFF } )

Enables or disables the audit from collecting records for this audit specification.

## Remarks

Database audit specifications are non-securable objects that reside in a given database. When a database audit specification is created, it's in a disabled state.

## Permissions

Users with the `ALTER ANY DATABASE AUDIT` permission can create database audit specifications and bind them to any audit.

After a database audit specification is created, users with the `CONTROL SERVER` permission, or the `sysadmin` account, can view it.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Audit SELECT and INSERT on a table for any database principal

The following example creates a server audit called `Payroll_Security_Audit` and then a database audit specification called `Payroll_Security_Audit` that audits `SELECT` and `INSERT` statements by any member of the **public** database role, for the `HumanResources.EmployeePayHistory` table. Every user is audited, because every user is always member of the **public** role.

```sql
USE master;
GO

-- Create the server audit.
CREATE SERVER AUDIT Payroll_Security_Audit
TO FILE (FILEPATH = 'D:\SQLAudit\'); -- make sure this path exists
GO

-- Enable the server audit.
ALTER SERVER AUDIT Payroll_Security_Audit
WITH (STATE = ON);
GO

-- Move to the target database.
USE AdventureWorks2022;
GO

-- Create the database audit specification.
CREATE DATABASE AUDIT SPECIFICATION Audit_Pay_Tables
FOR SERVER AUDIT Payroll_Security_Audit ADD (
    SELECT, INSERT ON HumanResources.EmployeePayHistory BY PUBLIC
)
WITH (STATE = ON);
GO
```

### B. Audit any data modification on all objects in a schema for a specific database role

The following example creates a server audit called `DataModification_Security_Audit` and then a database audit specification called `Audit_Data_Modification_On_All_Sales_Tables` that audits `INSERT`, `UPDATE`, and `DELETE` statements by users in a new database role `SalesUK`, for all objects in the `Sales` schema.

```sql
USE master;
GO

-- Create the server audit.
-- Change the path to a path that the SQLServer Service has access to.
CREATE SERVER AUDIT DataModification_Security_Audit
TO FILE (FILEPATH = 'D:\SQLAudit\'); -- make sure this path exists
GO

-- Enable the server audit.
ALTER SERVER AUDIT DataModification_Security_Audit
WITH (STATE = ON);
GO

-- Move to the target database.
USE AdventureWorks2022;
GO

CREATE ROLE SalesUK
GO

-- Create the database audit specification.
CREATE DATABASE AUDIT SPECIFICATION Audit_Data_Modification_On_All_Sales_Tables
FOR SERVER AUDIT DataModification_Security_Audit ADD (
    INSERT, UPDATE, DELETE ON SCHEMA::Sales BY SalesUK
)
WITH (STATE = ON);
GO
```

## Related tasks

Server audit specifications:

- [CREATE SERVER AUDIT SPECIFICATION (Transact-SQL)](create-server-audit-specification-transact-sql.md)
- [ALTER SERVER AUDIT SPECIFICATION (Transact-SQL)](alter-server-audit-specification-transact-sql.md)
- [DROP SERVER AUDIT SPECIFICATION (Transact-SQL)](drop-server-audit-specification-transact-sql.md)

Database audit specifications:

- [CREATE DATABASE AUDIT SPECIFICATION (Transact-SQL)](create-database-audit-specification-transact-sql.md)
- [ALTER DATABASE AUDIT SPECIFICATION (Transact-SQL)](alter-database-audit-specification-transact-sql.md)
- [DROP DATABASE AUDIT SPECIFICATION (Transact-SQL)](drop-database-audit-specification-transact-sql.md)

Catalog views and DMVs:

- [sys.server_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)
- [sys.server_file_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)
- [sys.server_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)
- [sys.server_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)
- [sys.database_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)
- [sys.database_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)

## Related content

- [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)
- [CREATE SERVER AUDIT (Transact-SQL)](create-server-audit-transact-sql.md)
- [ALTER SERVER AUDIT (Transact-SQL)](alter-server-audit-transact-sql.md)
- [DROP SERVER AUDIT (Transact-SQL)](drop-server-audit-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](alter-authorization-transact-sql.md)
- [sys.fn_get_audit_file (Transact-SQL)](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)
- [sys.dm_server_audit_status (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)
- [sys.dm_audit_actions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)
