---
title: "sp_control_plan_guide (Transact-SQL)"
description: The sp_control_plan_guide system stored procedure drops, enables, or disables a plan guide.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_control_plan_guide"
  - "sp_control_plan_guide_TSQL"
helpviewer_keywords:
  - "sp_control_plan_guide"
dev_langs:
  - "TSQL"
---
# sp_control_plan_guide (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `sp_control_plan_guide` system stored procedure is used to drop, enable, or disable a plan guide.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_control_plan_guide
    [ @operation = ] { N'DROP [ ALL ]' | N'DISABLE [ ALL ]' | N'ENABLE [ ALL ]' }
    [ , [ @name = ] N'name' ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

Specifies the plan guide that is being dropped, enabled, or disabled. *@name* is **sysname**, with a default of `NULL`. *@name* is resolved to the current database. If not specified, *@name* defaults to `NULL`.

#### [ @operation = ] { N'DROP [ ALL ]' | N'DISABLE [ ALL ]' | N'ENABLE [ ALL ]' }

The operation to perform on the plan guide specified in *@name*. *@operation* is **nvarchar(60)**, with no default.

- `DROP`

  Drops the plan guide specified by *@name*. After a plan guide is dropped, future executions of a query formerly matched by the plan guide aren't influenced by the plan guide.

- `DROP ALL`

  Drops all plan guides in the current database. *@name* can't be specified when `DROP ALL` is specified.

- `DISABLE`

  Disables the plan guide specified by *@name*. After a plan guide is disabled, future executions of a query formerly matched by the plan guide aren't influenced by the plan guide.

- `DISABLE ALL`

  Disables all plan guides in the current database. *@name* can't be specified when `DISABLE ALL` is specified.

- `ENABLE`

  Enables the plan guide specified by *@name*. A plan guide can be matched with an eligible query after it's enabled. By default, plan guides are enabled at the time they're created.

- `ENABLE ALL`

  Enables all plan guides in the current database. *@name* can't be specified when `ENABLE ALL` is specified.

## Remarks

Trying to drop or modify a function, stored procedure, or DML trigger that is referenced by a plan guide, either enabled or disabled, causes an error.

Disabling a disabled plan guide or enabling an enabled plan guide has no effect and runs without error.

Plans guides aren't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md). However, you can execute `sp_control_plan_guide` with the `DROP` or `DROP ALL` option in any edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Permissions

Executing `sp_control_plan_guide` on a plan guide of type `OBJECT` (created specifying `@type = '<object>'`) requires `ALTER` permission on the object that is referenced by the plan guide. All other plan guides require `ALTER DATABASE` permission.

## Examples

### A. Enable, disable, and drop a plan guide

The following example creates a plan guide, disables it, enables it, and drops it.

```sql
--Create a procedure on which to define the plan guide.
IF OBJECT_ID(N'Sales.GetSalesOrderByCountry', N'P') IS NOT NULL
    DROP PROCEDURE Sales.GetSalesOrderByCountry;
GO

CREATE PROCEDURE Sales.GetSalesOrderByCountry (@Country NVARCHAR(60))
AS
BEGIN
    SELECT *
    FROM Sales.SalesOrderHeader AS h
    INNER JOIN Sales.Customer AS c
        ON h.CustomerID = c.CustomerID
    INNER JOIN Sales.SalesTerritory AS t
        ON c.TerritoryID = t.TerritoryID
    WHERE t.CountryRegionCode = @Country;
END
GO

--Create the plan guide.
EXEC sp_create_plan_guide N'Guide3',
    N'SELECT *
    FROM Sales.SalesOrderHeader AS h
    INNER JOIN Sales.Customer AS c
        ON h.CustomerID = c.CustomerID
    INNER JOIN Sales.SalesTerritory AS t
        ON c.TerritoryID = t.TerritoryID
    WHERE t.CountryRegionCode = @Country',
    N'OBJECT',
    N'Sales.GetSalesOrderByCountry',
    NULL,
    N'OPTION (OPTIMIZE FOR (@Country = N''US''))';
GO

--Disable the plan guide.
EXEC sp_control_plan_guide N'DISABLE',
    N'Guide3';
GO

--Enable the plan guide.
EXEC sp_control_plan_guide N'ENABLE',
    N'Guide3';
GO

--Drop the plan guide.
EXEC sp_control_plan_guide N'DROP',
    N'Guide3';
GO
```

### B. Disable all plan guides in the current database

The following example disables all plan guides in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO
EXEC sp_control_plan_guide N'DISABLE ALL';
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sp_create_plan_guide (Transact-SQL)](sp-create-plan-guide-transact-sql.md)
- [sys.plan_guides (Transact-SQL)](../system-catalog-views/sys-plan-guides-transact-sql.md)
- [Plan Guides](../performance/plan-guides.md)
