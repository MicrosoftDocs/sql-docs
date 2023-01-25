---
title: "Using PIVOT and UNPIVOT | Microsoft Docs"
description: "Transact-SQL reference for PIVOT and UNPIVOT relational operators. Use these operators on SELECT statements to change a table-valued expression into another table."
ms.date: "10/14/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "PIVOT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "FROM clause, UNPIVOT operator"
  - "unpivoting tables"
  - "table pivoting [SQL Server]"
  - "UNPIVOT operator"
  - "crosstab query"
  - "PIVOT operator"
  - "rotating table-valued expressions"
  - "pivoting tables"
  - "FROM clause, PIVOT operator"
  - "rotating columns"
ms.assetid: 24ba54fc-98f7-4d35-8881-b5158aac1d66
author: VanMSFT
ms.author: vanto
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# FROM - Using PIVOT and UNPIVOT

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

You can use the `PIVOT` and `UNPIVOT` relational operators to change a table-valued expression into another table. `PIVOT` rotates a table-valued expression by turning the unique values from one column in the expression into multiple columns in the output. And `PIVOT` runs aggregations where they're required on any remaining column values that are wanted in the final output. `UNPIVOT` carries out the opposite operation to PIVOT by rotating columns of a table-valued expression into column values.  
  
The syntax for `PIVOT` provides is simpler and more readable than the syntax that may otherwise be specified in a complex series of `SELECT...CASE` statements. For a complete description of the syntax for `PIVOT`, see [FROM (Transact-SQL)](../../t-sql/queries/from-transact-sql.md).  
  
## Syntax  
The following syntax summarizes how to use the `PIVOT` operator.  
  
```syntaxsql  
SELECT <non-pivoted column>,  
    [first pivoted column] AS <column name>,  
    [second pivoted column] AS <column name>,  
    ...  
    [last pivoted column] AS <column name>  
FROM  
    (<SELECT query that produces the data>)   
    AS <alias for the source query>  
PIVOT  
(  
    <aggregation function>(<column being aggregated>)  
FOR   
[<column that contains the values that will become column headers>]   
    IN ( [first pivoted column], [second pivoted column],  
    ... [last pivoted column])  
) AS <alias for the pivot table>  
<optional ORDER BY clause>;  
```  

## Remarks  
The column identifiers in the `UNPIVOT` clause follow the catalog collation. For [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], the collation is always `SQL_Latin1_General_CP1_CI_AS`. For [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] partially contained databases, the collation is always `Latin1_General_100_CI_AS_KS_WS_SC`. If the column is combined with other columns, then a collate clause (`COLLATE DATABASE_DEFAULT`) is required to avoid conflicts.  

  
## Basic PIVOT Example  
The following code example produces a two-column table that has four rows.  
  
```sql
USE AdventureWorks2014 ;  
GO  
SELECT DaysToManufacture, AVG(StandardCost) AS AverageCost   
FROM Production.Product  
GROUP BY DaysToManufacture;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
DaysToManufacture AverageCost
----------------- -----------
0                 5.0885
1                 223.88
2                 359.1082
4                 949.4105
```
  
No products are defined with three `DaysToManufacture`.  
  
The following code displays the same result, pivoted so that the `DaysToManufacture` values become the column headings. A column is provided for three `[3]` days, even though the results are `NULL`.  
  
```sql
-- Pivot table with one row and five columns  
SELECT 'AverageCost' AS Cost_Sorted_By_Production_Days,   
  [0], [1], [2], [3], [4]  
FROM  
(
  SELECT DaysToManufacture, StandardCost   
  FROM Production.Product
) AS SourceTable  
PIVOT  
(  
  AVG(StandardCost)  
  FOR DaysToManufacture IN ([0], [1], [2], [3], [4])  
) AS PivotTable;  
  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
Cost_Sorted_By_Production_Days 0           1           2           3           4         
------------------------------ ----------- ----------- ----------- ----------- -----------
AverageCost                    5.0885      223.88      359.1082    NULL        949.4105
```
  
## Complex PIVOT Example  
A common scenario where `PIVOT` can be useful is when you want to generate cross-tabulation reports to give a summary of the data. For example, suppose you want to query the `PurchaseOrderHeader` table in the `AdventureWorks2014` sample database to determine the number of purchase orders placed by certain employees. The following query provides this report, ordered by vendor.  
  
```sql
USE AdventureWorks2014;  
GO  
SELECT VendorID, [250] AS Emp1, [251] AS Emp2, [256] AS Emp3, [257] AS Emp4, [260] AS Emp5  
FROM   
(SELECT PurchaseOrderID, EmployeeID, VendorID  
FROM Purchasing.PurchaseOrderHeader) p  
PIVOT  
(  
COUNT (PurchaseOrderID)  
FOR EmployeeID IN  
( [250], [251], [256], [257], [260] )  
) AS pvt  
ORDER BY pvt.VendorID;  
```  
  
Here is a partial result set.  
  
```
VendorID    Emp1        Emp2        Emp3        Emp4        Emp5  
----------- ----------- ----------- ----------- ----------- -----------
1492        2           5           4           4           4
1494        2           5           4           5           4
1496        2           4           4           5           5
1498        2           5           4           4           4
1500        3           4           4           5           4
```
  
The results returned by this subselect statement are pivoted on the `EmployeeID` column.  
  
```sql
SELECT PurchaseOrderID, EmployeeID, VendorID  
FROM PurchaseOrderHeader;  
```  
  
The unique values returned by the `EmployeeID` column become fields in the final result set. As such, there's a column for each `EmployeeID` number specified in the pivot clause: in this case employees `250`, `251`, `256`, `257`, and `260`. The `PurchaseOrderID` column serves as the value column, against which the columns returned in the final output, which are called the grouping columns, are grouped. In this case, the grouping columns are aggregated by the `COUNT` function. Notice that a warning message appears that indicates that any null values appearing in the `PurchaseOrderID` column weren't considered when computing the `COUNT` for each employee.  
  
> [!IMPORTANT]  
>  When aggregate functions are used with `PIVOT`, the presence of any null values in the value column are not considered when computing an aggregation.  

## UNPIVOT Example
  
`UNPIVOT` carries out almost the reverse operation of `PIVOT`, by rotating columns into rows. Suppose the table produced in the previous example is stored in the database as `pvt`, and you want to rotate the column identifiers `Emp1`, `Emp2`, `Emp3`, `Emp4`, and `Emp5` into row values that correspond to a particular vendor. As such, you must identify two additional columns. The column that will contain the column values that you're rotating (`Emp1`, `Emp2`,...) will be called `Employee`, and the column that will hold the values that currently exist under the columns being rotated will be called `Orders`. These columns correspond to the *pivot_column* and *value_column*, respectively, in the [!INCLUDE[tsql](../../includes/tsql-md.md)] definition. Here is the query.  
  
```sql
-- Create the table and insert values as portrayed in the previous example.  
CREATE TABLE pvt (VendorID INT, Emp1 INT, Emp2 INT,  
    Emp3 INT, Emp4 INT, Emp5 INT);  
GO  
INSERT INTO pvt VALUES (1,4,3,5,4,4);  
INSERT INTO pvt VALUES (2,4,1,5,5,5);  
INSERT INTO pvt VALUES (3,4,3,5,4,4);  
INSERT INTO pvt VALUES (4,4,2,5,5,4);  
INSERT INTO pvt VALUES (5,5,1,5,5,5);  
GO  
-- Unpivot the table.  
SELECT VendorID, Employee, Orders  
FROM   
   (SELECT VendorID, Emp1, Emp2, Emp3, Emp4, Emp5  
   FROM pvt) p  
UNPIVOT  
   (Orders FOR Employee IN   
      (Emp1, Emp2, Emp3, Emp4, Emp5)  
)AS unpvt;  
GO  
```  
  
Here is a partial result set.  
  
```
VendorID    Employee    Orders
----------- ----------- ------
1            Emp1       4
1            Emp2       3 
1            Emp3       5
1            Emp4       4
1            Emp5       4
2            Emp1       4
2            Emp2       1
2            Emp3       5
2            Emp4       5
2            Emp5       5
...
```
  
Notice that `UNPIVOT` isn't the exact reverse of `PIVOT`. `PIVOT` carries out an aggregation and merges possible multiple rows into a single row in the output. `UNPIVOT` doesn't reproduce the original table-valued expression result because rows have been merged. Also, null values in the input of `UNPIVOT` disappear in the output. When the values disappear, it shows that there may have been original null values in the input before the `PIVOT` operation.  
  
The `Sales.vSalesPersonSalesByFiscalYears` view in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database uses `PIVOT` to return the total sales for each salesperson, for each fiscal year. To script the view in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in **Object Explorer**, locate the view under the **Views** folder for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. Right-click the view name, and then select **Script View as**.  
  
## See Also  
[FROM (Transact-SQL)](../../t-sql/queries/from-transact-sql.md)   
[CASE (Transact-SQL)](../../t-sql/language-elements/case-transact-sql.md)  
  
