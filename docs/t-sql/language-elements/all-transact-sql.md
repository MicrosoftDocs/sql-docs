---
title: "ALL (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALL_TSQL"
  - "ALL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "single-column set of values [SQL Server]"
  - "ALL (Transact-SQL)"
ms.assetid: 4b0c002e-1ffd-4425-a980-11fdc1f24af7
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# ALL (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Compares a scalar value with a single-column set of values.  
  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
scalar_expression { = | <> | != | > | >= | !> | < | <= | !< } ALL ( subquery )  
```  
  
## Arguments  
 *scalar_expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md).  
  
 { = | <> | != | > | >= | !> | < | <= | !< }  
 Is a comparison operator.  
  
 *subquery*  
 Is a subquery that returns a result set of one column. The data type of the returned column must be the same data type as the data type of *scalar_expression*.  
  
 Is a restricted SELECT statement, in which the ORDER BY clause and the INTO keyword aren't allowed.  
  
## Result Types  
 **Boolean**  
  
## Result Value  
 Returns TRUE when the comparison specified is TRUE for all pairs (_scalar_expression_**,**_x)_, when *x* is a value in the single-column set. Otherwise returns FALSE.  
  
## Remarks  
 ALL requires the *scalar_expression* to compare positively to every value that is returned by the subquery. For instance, if the subquery returns values of 2 and 3, *scalar_expression* <= ALL (subquery) would evaluate as TRUE for a *scalar_expression* of 2. If the subquery returns values of 2 and 3, *scalar_expression* = ALL (subquery) would evaluate as FALSE, because some of the values of the subquery (the value of 3) wouldn't meet the criteria of the expression.  
  
 For statements that require the *scalar_expression* to compare positively to only one value that is returned by the subquery, see [SOME &#124; ANY &#40;Transact-SQL&#41;](../../t-sql/language-elements/some-any-transact-sql.md).  
  
 This article refers to ALL when it is used with a subquery. ALL can also be used with [UNION](../../t-sql/language-elements/set-operators-union-transact-sql.md) and [SELECT](../../t-sql/queries/select-transact-sql.md).  
  
## Examples  
 The following example creates a stored procedure that determines whether all the components of a specified `SalesOrderID` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database can be manufactured in the specified number of days. The example uses a subquery to create a list of the number of `DaysToManufacture` values for all of the components of the specific `SalesOrderID`, and then confirms that all the `DaysToManufacture` are within the number of days specified.  
  
```  
-- Uses AdventureWorks  
  
CREATE PROCEDURE DaysToBuild @OrderID int, @NumberOfDays int  
AS  
IF   
@NumberOfDays >= ALL  
   (  
    SELECT DaysToManufacture  
    FROM Sales.SalesOrderDetail  
    JOIN Production.Product   
    ON Sales.SalesOrderDetail.ProductID = Production.Product.ProductID   
    WHERE SalesOrderID = @OrderID  
   )  
PRINT 'All items for this order can be manufactured in specified number of days or less.'  
ELSE   
PRINT 'Some items for this order cann''t be manufactured in specified number of days or less.' ;  
  
```  
  
 To test the procedure, execute the procedure by using the `SalesOrderID 49080`, which has one component requiring `2` days and two components that require 0 days. The first statement below meets the criteria. The second query doesn't.  
  
```  
EXECUTE DaysToBuild 49080, 2 ;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `All items for this order can be manufactured in specified number of days or less.`  
  
```  
EXECUTE DaysToBuild 49080, 1 ;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `Some items for this order can't be manufactured in specified number of days or less.`  
  
## See Also  
 [CASE &#40;Transact-SQL&#41;](../../t-sql/language-elements/case-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)   
 [IN &#40;Transact-SQL&#41;](../../t-sql/language-elements/in-transact-sql.md)  
  
