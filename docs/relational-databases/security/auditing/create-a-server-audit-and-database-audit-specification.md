---
title: "Create a server audit & database audit specification"
description: Learn to create a SQL Server audit and a database audit specification by using SQL Server Management Studio or Transact-SQL (T-SQL).
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: randolphwest
ms.date: 09/06/2024
ms.service: sql
ms.subservice: security
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.sqlaudit.dbaudit.general.f1"
helpviewer_keywords:
  - "audits [SQL Server], creating database specification"
  - "database audit [SQL Server]"
---
# Create a server audit and database audit specification

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article describes how to create a server audit and a database audit specification in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../../includes/tsql-md.md)].

Auditing an instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] or a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] database involves tracking and logging events that occur on the system. The *SQL Server Audit* object collects a single instance of server-level or database-level actions and groups of actions to monitor. The audit is at the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance level. You can have multiple audits per [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance. The *Database-Level Audit Specification* object belongs to an audit. You can create one database audit specification per SQL Server database per audit. For more information, see [SQL Server Audit (Database Engine)](sql-server-audit-database-engine.md).

## Limitations

Database audit specifications are non-securable objects that reside in a given database. When a database audit specification is created, it's in a disabled state.

When you're creating or modifying a database audit specification in a user database, don't include audit actions on server-scope objects, like the system views. If you include server-scoped objects, the audit is created. But the server-scoped objects aren't included, and no error is returned. To audit server-scoped objects, use a database audit specification in the `master` database.

Database audit specifications reside in the database where they're created, except for the `tempdb` system database.

## Permissions

- Users who have the `ALTER ANY DATABASE AUDIT` permission can create database audit specifications and bind them to any audit.

- After a database audit specification is created, principals who have `CONTROL SERVER` or `ALTER ANY DATABASE AUDIT` permissions can view it. The `sysadmin` account can also view it.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

#### Create a server audit

1. In Object Explorer, expand the **Security** folder.

1. Right-click the **Audits** folder and select **New Audit**. For more information, see [Create a Server Audit and Server Audit Specification](create-a-server-audit-and-server-audit-specification.md).

1. When you finish selecting options, select **OK**.

#### Create a database-level audit specification

1. In Object Explorer, expand the database where you want to create the audit specification.

1. Expand the **Security** folder.

1. Right-click the **Database Audit Specifications** folder and select **New Database Audit Specification**.

   These options are available in the **Create Database Audit Specification** dialog box:

   **Name**

   The name of the database audit specification. A name is generated automatically when you create a server audit specification. The name is editable.

   **Audit**

   The name of an existing server audit object. Either type in the name of the audit or select it from the list.

   **Audit Action Type**

   Specifies the database-level audit action groups and audit actions to capture. For a list of database-level audit action groups and audit actions and descriptions of the events they contain, see [SQL Server Audit Action Groups and Actions](sql-server-audit-action-groups-and-actions.md).

   **Object Schema**

   Displays the schema for the specified **Object Name**.

   **Object Name**

   The name of the object to audit. This option is available only for audit actions. It doesn't apply to audit groups.

   **Ellipsis (...)**

   Opens the **Select Objects** dialog box so you can browse for and select an available object, based on the specified **Audit Action Type**.

   **Principal Name**

   The account to filter the audit by for the object being audited.

   **Ellipsis (...)**

   Opens the **Select Objects** dialog box so you can browse for and select an available object, based on the specified **Object Name**.

1. When you finish selecting options, select **OK**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

#### Create a server audit

1. In Object Explorer, connect to an instance of [!INCLUDE [ssDE](../../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Paste the following example into the query window and then select **Execute**.

   ```sql
   USE master;
   GO
   
   -- Create the server audit.
   CREATE SERVER AUDIT Payroll_Security_Audit TO FILE (FILEPATH = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA');
   GO
   
   -- Enable the server audit.
   ALTER SERVER AUDIT Payroll_Security_Audit
   WITH (STATE = ON);
   ```

#### Create a database-level audit specification

1. In Object Explorer, connect to an instance of [!INCLUDE [ssDE](../../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Paste the following example into the query window and then select **Execute**. This example creates a database audit specification called `Audit_Pay_Tables`. It audits SELECT and INSERT statements by the `dbo` user for the `HumanResources.EmployeePayHistory` table, based on the server audit defined in the previous section.

   ```sql
   USE AdventureWorks2022;
   GO
   
   -- Create the database audit specification.
   CREATE DATABASE AUDIT SPECIFICATION Audit_Pay_Tables
   FOR SERVER AUDIT Payroll_Security_Audit ADD (
       SELECT, INSERT ON HumanResources.EmployeePayHistory BY dbo
   )
   WITH (STATE = ON);
   GO
   ```

## Related content

- [CREATE SERVER AUDIT (Transact-SQL)](../../../t-sql/statements/create-server-audit-transact-sql.md)
- [CREATE DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../../t-sql/statements/create-database-audit-specification-transact-sql.md)
