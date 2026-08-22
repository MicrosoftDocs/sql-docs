---
title: Set Query Parameterization Behavior Using Plan Guides
description: Learn about options for parameterization, where parameters are substituted for literal values in a query in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 05/21/2026
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "TEMPLATE plan guide"
  - "PARAMETERIZATION FORCED option"
  - "PARAMETERIZATION option"
  - "PARAMETERIZATION SIMPLE option"
  - "parameterization [SQL Server]"
  - "overriding parameterization behavior"
  - "plan guides [SQL Server], parameterization"
  - "parameterized queries [SQL Server]"
---
# Specify query parameterization behavior by using plan guides

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

When you set the `PARAMETERIZATION` database option to `SIMPLE`, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer might choose to parameterize the queries. This parameterization substitutes any literal values in a query with parameters. This process is referred to as simple parameterization. When `SIMPLE` parameterization is in effect, you can't control which queries are parameterized and which queries aren't. However, you can specify that all queries in a database be parameterized by setting the `PARAMETERIZATION` database option to `FORCED`. This process is referred to as forced parameterization.

You can override the parameterization behavior of a database by using plan guides in the following ways:

| Option | Description |
| --- | --- |
| `SIMPLE` | You can specify that forced parameterization is attempted on a certain class of queries. You do this by creating a TEMPLATE plan guide on the parameterized form of the query, and specifying the `PARAMETERIZATION FORCED` query hint in the [sp_create_plan_guide](../system-stored-procedures/sp-create-plan-guide-transact-sql.md) stored procedure. You can consider this kind of plan guide as a way to enable forced parameterization only on a certain class of queries, instead of all queries. For more information, see [Simple parameterization](../query-processing-architecture-guide.md#simple-parameterization). |
| `FORCED` | You can specify that for a certain class of queries, only simple parameterization is attempted, not forced parameterization. You do this by creating a TEMPLATE plan guide on the force-parameterized form of the query, and specifying the `PARAMETERIZATION SIMPLE` query hint in `sp_create_plan_guide`. For more information, see [Forced parameterization](../query-processing-architecture-guide.md#forced-parameterization). |

Consider the following query on the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database:

```sql
SELECT pi.ProductID,
       SUM(pi.Quantity) AS Total
FROM Production.ProductModel AS pm
     INNER JOIN Production.ProductInventory AS pi
         ON pm.ProductModelID = pi.ProductID
WHERE pi.ProductID = 101
GROUP BY pi.ProductID, pi.Quantity
HAVING SUM(pi.Quantity) > 50;
```

As a database administrator, you determine that you don't want to enable forced parameterization on all queries in the database. However, you want to avoid compilation costs on all queries that are syntactically equivalent to the previous query, but differ only in their constant literal values. In other words, you want the query to be parameterized so that a query plan for this kind of query is reused. In this case, complete the following steps:

1. Retrieve the parameterized form of the query. The only safe way to obtain this value for use in `sp_create_plan_guide` is by using the [sp_get_query_template](../system-stored-procedures/sp-get-query-template-transact-sql.md) system stored procedure.

1. Create the plan guide on the parameterized form of the query, specifying the `PARAMETERIZATION FORCED` query hint.

   > [!IMPORTANT]  
   > As part of parameterizing a query, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] assigns a data type to the parameters that replace the literal values, depending on the value and size of the literal. The same process occurs to the value of the constant literals passed to the `@stmt` output parameter of `sp_get_query_template`. Because the data type specified in the `@params` argument of `sp_create_plan_guide` must match that of the query as it's parameterized by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you might have to create more than one plan guide to cover the complete range of possible parameter values for the query.

The following script can be used both to obtain the parameterized query and then create a plan guide on it:

```sql
DECLARE @stmt AS NVARCHAR (MAX);
DECLARE @params AS NVARCHAR (MAX);

EXECUTE sp_get_query_template
    N'SELECT pi.ProductID, SUM(pi.Quantity) AS Total
      FROM Production.ProductModel AS pm
      INNER JOIN Production.ProductInventory AS pi ON pm.ProductModelID = pi.ProductID
      WHERE pi.ProductID = 101
      GROUP BY pi.ProductID, pi.Quantity
      HAVING SUM(pi.Quantity) > 50',
@stmt OUTPUT, @params OUTPUT;

EXECUTE sp_create_plan_guide N'TemplateGuide1',
@stmt, N'TEMPLATE', NULL,
@params, N'OPTION(PARAMETERIZATION FORCED)';
```

If forced parameterization is already enabled on the database, you can override it for specific queries. To parameterize the sample query and syntactically equivalent queries according to simple parameterization rules, specify `PARAMETERIZATION SIMPLE` instead of `PARAMETERIZATION FORCED` in the `OPTION` clause.

> [!NOTE]  
> TEMPLATE plan guides match statements to queries submitted in batches that consist of a single statement only. Statements inside multistatement batches aren't eligible to be matched by TEMPLATE plan guides.

## Related content

- [Query processing architecture guide](../query-processing-architecture-guide.md)
- [sys.sp_create_plan_guide (Transact-SQL)](../system-stored-procedures/sp-create-plan-guide-transact-sql.md)
