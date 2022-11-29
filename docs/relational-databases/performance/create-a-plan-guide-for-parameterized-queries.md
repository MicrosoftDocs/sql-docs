---
title: "Create a Plan Guide for Parameterized Queries | Microsoft Docs"
description: Learn how to create a plan guide that matches any query that parameterizes to a specified form and directs SQL Server to force parameterization of the query.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "parameterized queries, plan guides for"
  - "plan guides [SQL Server], parameterized queries"
ms.assetid: b532ae16-66e7-4641-9bc8-b0d805853477
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Create a Plan Guide for Parameterized Queries
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  A TEMPLATE plan guide matches stand-alone queries that parameterize to a specified form.  
  
 The following example creates a plan guide that matches any query that parameterizes to a specified form, and directs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to force parameterization of the query. The following two queries are syntactically equivalent, but differ only in their constant literal values.  
  
```  
SELECT * FROM AdventureWorks2012.Sales.SalesOrderHeader AS h  
INNER JOIN AdventureWorks2012.Sales.SalesOrderDetail AS d   
    ON h.SalesOrderID = d.SalesOrderID  
WHERE h.SalesOrderID = 45639;  
  
SELECT * FROM AdventureWorks2012.Sales.SalesOrderHeader AS h  
INNER JOIN AdventureWorks2012.Sales.SalesOrderDetail AS d   
    ON h.SalesOrderID = d.SalesOrderID  
WHERE h.SalesOrderID = 45640;  
```  
  
 Here is the plan guide on the parameterized form of the query:  
  
```  
EXEC sp_create_plan_guide   
    @name = N'TemplateGuide1',  
    @stmt = N'SELECT * FROM AdventureWorks2012.Sales.SalesOrderHeader AS h  
              INNER JOIN AdventureWorks2012.Sales.SalesOrderDetail AS d   
                  ON h.SalesOrderID = d.SalesOrderID  
              WHERE h.SalesOrderID = @0',  
    @type = N'TEMPLATE',  
    @module_or_batch = NULL,  
    @params = N'@0 int',  
    @hints = N'OPTION(PARAMETERIZATION FORCED)';  
```  
  
 In the previous example, the value for the `@stmt` parameter is the parameterized form of the query. The only reliable way to obtain this value for use in sp_create_plan_guide is to use the [sp_get_query_template](../../relational-databases/system-stored-procedures/sp-get-query-template-transact-sql.md) system stored procedure. The following script can be used both to obtain the parameterized query and then create a plan guide on it.  
  
```  
DECLARE @stmt nvarchar(max);  
DECLARE @params nvarchar(max);  
EXEC sp_get_query_template   
    N'SELECT * FROM AdventureWorks2012.Sales.SalesOrderHeader AS h  
      INNER JOIN AdventureWorks2012.Sales.SalesOrderDetail AS d   
          ON h.SalesOrderID = d.SalesOrderID  
      WHERE h.SalesOrderID = 45639;',  
    @stmt OUTPUT,   
    @params OUTPUT  
EXEC sp_create_plan_guide N'TemplateGuide1',   
    @stmt,   
    N'TEMPLATE',   
    NULL,   
    @params,   
    N'OPTION(PARAMETERIZATION FORCED)';  
```  
  
> [!IMPORTANT]  
>  The value of the constant literals in the `@stmt` parameter passed to `sp_get_query_template` might affect the data type that is chosen for the parameter that replaces the literal. This will affect plan guide matching. You may have to create more than one plan guide to handle different parameter value ranges.  
  
 You can also use TEMPLATE plan guides together with SQL plan guides. For example, you can create a TEMPLATE plan guide to make sure that a class of queries is parameterized. You can then create an SQL plan guide on the parameterized form of that query.  
  
  
