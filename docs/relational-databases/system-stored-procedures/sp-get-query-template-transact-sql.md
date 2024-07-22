---
title: "sp_get_query_template (Transact-SQL)"
description: sp_get_query_template returns the parameterized form of a query.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_get_query_template"
  - "sp_get_query_template_TSQL"
helpviewer_keywords:
  - "sp_get_query_template"
dev_langs:
  - "TSQL"
---
# sp_get_query_template (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the parameterized form of a query. The results returned mimic the parameterized form of a query that results from using forced parameterization. `sp_get_query_template` is used primarily when you create `TEMPLATE` plan guides.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_get_query_template
   [ @querytext = ] N'querytext'
   , @templatetext OUTPUT
   , @parameters OUTPUT
[ ; ]
```

## Arguments

#### [ @querytext = ] N'querytext'

The query for which the parameterized version is to be generated. *@querytext* is **nvarchar(max)**, and must be enclosed in single quotation marks and be preceded by the `N` Unicode specifier.

#### @templatetext

An OUTPUT parameter of type **nvarchar(max)**, provided as indicated, to receive the parameterized form of *@querytext* as a string literal.

#### @parameters

An output parameter of type **nvarchar(max)**, provided as indicated, to receive a string literal of the parameter names and data types that are parameterized in *@templatetext*.

## Remarks

`sp_get_query_template` returns an error when the following occur:

- It doesn't parameterize any constant literal values in *@querytext*.
- *@querytext* is `NULL`, not a Unicode string, syntactically not valid, or can't be compiled.

If `sp_get_query_template` returns an error, it doesn't modify the values of the *@templatetext* and @parameters output parameters.

## Permissions

Requires membership in the **public** database role.

## Examples

The following example returns the parameterized form of a query that contains two constant literal values.

```sql
USE AdventureWorks2022;
GO

DECLARE @my_templatetext NVARCHAR(MAX);
DECLARE @my_parameters NVARCHAR(MAX);

EXEC sp_get_query_template N'SELECT pi.ProductID, SUM(pi.Quantity) AS Total
        FROM Production.ProductModel pm
        INNER JOIN Production.ProductInventory pi
        ON pm.ProductModelID = pi.ProductID
        WHERE pi.ProductID = 2
        GROUP BY pi.ProductID, pi.Quantity
        HAVING SUM(pi.Quantity) > 400',
    @my_templatetext OUTPUT,
    @my_parameters OUTPUT;

SELECT @my_templatetext;
SELECT @my_parameters;
```

Here are the parameterized results of the `@my_templatetext OUTPUT` parameter:

```output
select pi . ProductID , SUM ( pi . Quantity ) as Total
from Production . ProductModel pm
inner join Production . ProductInventory pi
on pm . ProductModelID = pi . ProductID
where pi . ProductID = @0
group by pi . ProductID , pi . Quantity
having SUM ( pi . Quantity ) > 400
```

The first constant literal, `2`, is converted to a parameter. The second literal, `400`, isn't converted because it's inside a `HAVING` clause. The results returned by `sp_get_query_template` mimic the parameterized form of a query when the `PARAMETERIZATION` option of `ALTER DATABASE` is set to `FORCED`.

Here are the parameterized results of the `@my_parameters OUTPUT` parameter:

```output
@0 int
```

The order and naming of parameters in the output of `sp_get_query_template` can change between quick-fix engineering, service pack, and version upgrades of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Upgrades can also cause a different set of constant literals to be parameterized for the same query, and different spacing to be applied in the results of both output parameters.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [Specify Query Parameterization Behavior by Using Plan Guides](../performance/specify-query-parameterization-behavior-by-using-plan-guides.md)
