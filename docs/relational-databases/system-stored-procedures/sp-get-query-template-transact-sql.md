---
title: "sp_get_query_template (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_get_query_template"
  - "sp_get_query_template_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_get_query_template"
ms.assetid: 85e9bef7-2417-41a8-befa-fe75507d9bf2
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_get_query_template (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the parameterized form of a query. The results returned mimic the parameterized form of a query that results from using forced parameterization. sp_get_query_template is used primarily when you create TEMPLATE plan guides.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_get_query_template  
   [ @querytext = ] N'query_text'  
   , @templatetext OUTPUT   
   , @parameters OUTPUT   
```  
  
## Arguments  
 '*query_text*'  
 Is the query for which the parameterized version is to be generated. '*query_text*' must be enclosed in single quotation marks and be preceded by the N Unicode specifier. N'*query_text*' is the value assigned to the @querytext parameter. This is of type **nvarchar(max)**.  
  
 @templatetext  
 Is an output parameter of type **nvarchar(max)**, provided as indicated, to receive the parameterized form of *query_text* as a string literal.  
  
 @parameters  
 Is an output parameter of type **nvarchar(max)**, provided as indicated, to receive a string literal of the parameter names and data types that have been parameterized in @templatetext.  
  
## Remarks  
 sp_get_query_template returns an error when the following occur:  
  
-   It does not parameterize any constant literal values in *query_text*.  
  
-   *query_text* is NULL, not a Unicode string, syntactically not valid, or cannot be compiled.  
  
 If sp_get_query_template returns an error, it does not modify the values of the @templatetext and @parameters output parameters.  
  
## Permissions  
 Requires membership in the public database role.  
  
## Examples  
 The following example returns the parameterized form of a query that contains two constant literal values.  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @my_templatetext nvarchar(max)  
DECLARE @my_parameters nvarchar(max)  
EXEC sp_get_query_template   
    N'SELECT pi.ProductID, SUM(pi.Quantity) AS Total  
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
  
 Here are the parameterized results of the `@my_templatetext``OUTPUT` parameter:  
  
 `select pi . ProductID , SUM ( pi . Quantity ) as Total`  
  
 `from Production . ProductModel pm`  
  
 `inner join Production . ProductInventory pi`  
  
 `on pm . ProductModelID = pi . ProductID`  
  
 `where pi . ProductID = @0`  
  
 `group by pi . ProductID , pi . Quantity`  
  
 `having SUM ( pi . Quantity ) > 400`  
  
 Note that the first constant literal, `2`, is converted to a parameter. The second literal, `400`, is not converted because it is inside a `HAVING` clause. The results returned by sp_get_query_template mimic the parameterized form of a query when the PARAMETERIZATION option of ALTER DATABASE is set to FORCED.  
  
 Here are the parameterized results of the `@my_parameters OUTPUT` parameter:  
  
```  
@0 int  
```  
  
> [!NOTE]  
>  The order and naming of parameters in the output of sp_get_query_template can change between quick-fix engineering, service pack, and version upgrades of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Upgrades can also cause a different set of constant literals to be parameterized for the same query, and different spacing to be applied in the results of both output parameters.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [Specify Query Parameterization Behavior by Using Plan Guides](../../relational-databases/performance/specify-query-parameterization-behavior-by-using-plan-guides.md)  
  
  
