---
title: "DROP PROCEDURE (Transact-SQL)"
description: Removes one or more stored procedures or procedure groups from the current database in the SQL Server Database Engine.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 06/13/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP PROCEDURE"
  - "DROP_PROCEDURE_TSQL"
helpviewer_keywords:
  - "removing stored procedures"
  - "dropping procedure groups"
  - "deleting stored procedures"
  - "deleting procedure groups"
  - "DROP PROCEDURE statement"
  - "dropping stored procedures"
  - "stored procedures [SQL Server], removing"
  - "removing procedure groups"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# DROP PROCEDURE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Removes one or more stored procedures or procedure groups from the current database in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Managed Instance, and Azure SQL Database:

```syntaxsql
DROP { PROC | PROCEDURE } [ IF EXISTS ] { [ schema_name. ] procedure } [ , ...n ]
```

Syntax for Azure Synapse Analytics, Analytics Platform System (PDW), and Microsoft Fabric:

```syntaxsql
DROP { PROC | PROCEDURE } { [ schema_name. ] procedure_name }
```

## Arguments

#### IF EXISTS

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]

Conditionally drops the procedure only if it already exists.

#### *schema_name*

The name of the schema to which the procedure belongs. A server name or database name can't be specified.

#### *procedure*

The name of the stored procedure or stored procedure group to be removed. Individual procedures within a numbered procedure group can't be dropped; the whole procedure group is dropped.

## Best practices

Before removing any stored procedure, check for dependent objects and modify these objects accordingly. Dropping a stored procedure can cause dependent objects and scripts to fail when these objects aren't updated. For more information, see [View the Dependencies of a Stored Procedure](../../relational-databases/stored-procedures/view-the-dependencies-of-a-stored-procedure.md)

## Metadata

To display a list of existing procedures, query the `sys.objects` catalog view. To display the procedure definition, query the `sys.sql_modules` catalog view.

## Permissions

Requires `CONTROL` permission on the procedure, or `ALTER` permission on the schema to which the procedure belongs, or membership in the **db_ddladmin** fixed server role.

## Examples

The following example removes the `dbo.uspMyProc` stored procedure in the current database.

```sql
DROP PROCEDURE dbo.uspMyProc;
GO
```

The following example removes several stored procedures in the current database.

```sql
DROP PROCEDURE
    dbo.uspGetSalesbyMonth,
    dbo.uspUpdateSalesQuotes,
    dbo.uspGetSalesByYear;
```

The following example removes the `dbo.uspMyProc` stored procedure if it exists but doesn't cause an error if the procedure doesn't exist. This syntax was introduced in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)].

```sql
DROP PROCEDURE IF EXISTS dbo.uspMyProc;
GO
```

## Related content

- [ALTER PROCEDURE (Transact-SQL)](alter-procedure-transact-sql.md)
- [CREATE PROCEDURE (Transact-SQL)](create-procedure-transact-sql.md)
- [sys.objects (Transact-SQL)](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)
- [sys.sql_modules (Transact-SQL)](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)
- [Delete a Stored Procedure](../../relational-databases/stored-procedures/delete-a-stored-procedure.md)
