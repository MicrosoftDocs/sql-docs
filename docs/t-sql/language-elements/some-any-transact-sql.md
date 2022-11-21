---
title: "SOME | ANY (Transact-SQL)"
description: "SOME | ANY (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SOME"
  - "SOME_TSQL"
helpviewer_keywords:
  - "scalar values"
  - "comparing scalar with single-column set"
  - "ANY operator"
  - "SOME | ANY keyword"
  - "single-column set of values [SQL Server]"
dev_langs:
  - "TSQL"
---
# SOME | ANY (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Compares a scalar value with a single-column set of values. SOME and ANY are equivalent.  

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax
  
```syntaxsql
scalar_expression { = | < > | ! = | > | > = | ! > | < | < = | ! < }   
     { SOME | ANY } ( subquery )   
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*scalar_expression*  
Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md).

{ = \| <> \| != \| > \| >= \| !> \| < \| <= \| !< }  
Is any valid comparison operator.

#### SOME | ANY

Specifies that a comparison should be made.

#### *subquery*

Is a subquery that has a result set of one column. The data type of the column returned must be the same data type as *scalar_expression*.

## Result types

**Boolean**

## Result value

SOME or ANY returns **TRUE** when the comparison specified is TRUE for any pair (*scalar_expression*, *x*) where *x* is a value in the single-column set; otherwise, returns **FALSE**.

## Remarks

SOME requires the *scalar_expression* to compare positively to at least one value returned by the subquery. For statements that require the *scalar_expression* to compare positively to every value that is returned by the subquery, see [ALL &#40;Transact-SQL&#41;](../../t-sql/language-elements/all-transact-sql.md). For instance, if the subquery returns values of 2 and 3, *scalar_expression* = SOME (subquery) would evaluate as TRUE for a *scalar_express* of 2. If the subquery returns values of 2 and 3, *scalar_expression* = ALL (subquery) would evaluate as FALSE, because some of the values of the subquery (the value of 3) wouldn't meet the criteria of the expression.

## Examples

### A. Running a simple example

The following statements create a simple table and add the values of `1`, `2`, `3`, and `4` to the `ID` column.

```sql  
CREATE TABLE T1  
(ID INT) ;  
GO  
INSERT T1 VALUES (1) ;  
INSERT T1 VALUES (2) ;  
INSERT T1 VALUES (3) ;  
INSERT T1 VALUES (4) ;  
```

The following query returns `TRUE` because `3` is less than some of the values in the table.

```sql  
IF 3 < SOME (SELECT ID FROM T1)  
PRINT 'TRUE'   
ELSE  
PRINT 'FALSE' ;  
```

The following query returns `FALSE` because `3` isn't less than all of the values in the table.

```sql  
IF 3 < ALL (SELECT ID FROM T1)  
PRINT 'TRUE'   
ELSE  
PRINT 'FALSE' ;  
```

### B. Running a practical example

The following example creates a stored procedure that determines whether all the components of a specified `SalesOrderID` in the `AdventureWorks2012` database can be manufactured in the specified number of days. The example uses a subquery to create a list of the number of `DaysToManufacture` value for all the components of the specific `SalesOrderID`, and then tests whether any of the values that are returned by the subquery are greater than the number of days specified. If every value of `DaysToManufacture` that is returned is less than the number provided, the condition is TRUE and the first message is printed.

```sql  
-- Uses AdventureWorks

CREATE PROCEDURE ManyDaysToComplete @OrderID INT, @NumberOfDays INT  
AS  
IF   
@NumberOfDays < SOME  
   (  
    SELECT DaysToManufacture  
    FROM Sales.SalesOrderDetail  
    JOIN Production.Product   
    ON Sales.SalesOrderDetail.ProductID = Production.Product.ProductID   
    WHERE SalesOrderID = @OrderID  
   )  
PRINT 'At least one item for this order can''t be manufactured in specified number of days.'
ELSE   
PRINT 'All items for this order can be manufactured in the specified number of days or less.' ;

```

To test the procedure, execute the procedure by using the `SalesOrderID``49080`, which has one component that requires `2` days and two components that require 0 days. The first statement meets the criteria. The second query doesn't.

```sql  
EXECUTE ManyDaysToComplete 49080, 2 ;  
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

`All items for this order can be manufactured in the specified number of days or less.`

```sql
EXECUTE ManyDaysToComplete 49080, 1 ;  
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

`At least one item for this order can't be manufactured in specified number of days.`

## See also

- [ALL &#40;Transact-SQL&#41;](../../t-sql/language-elements/all-transact-sql.md)
- [CASE &#40;Transact-SQL&#41;](../../t-sql/language-elements/case-transact-sql.md)
- [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)
- [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)
- [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)
- [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)
- [IN &#40;Transact-SQL&#41;](../../t-sql/language-elements/in-transact-sql.md)
- [IS [NOT] DISTINCT FROM (Transact-SQL)](../../t-sql/queries/is-distinct-from-transact-sql.md)
