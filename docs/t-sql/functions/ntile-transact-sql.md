---
title: "NTILE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "NTILE_TSQL"
  - "NTILE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "distributing rows"
  - "groups [SQL Server], row distribution"
  - "row distribution [SQL Server]"
  - "NTILE function"
ms.assetid: 1c364511-d72a-4789-8efa-3cf2a1f6b791
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# NTILE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Distributes the rows in an ordered partition into a specified number of groups. The groups are numbered, starting at one. For each row, NTILE returns the number of the group to which the row belongs.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
NTILE (integer_expression) OVER ( [ <partition_by_clause> ] < order_by_clause > )  
```  
  
## Arguments  
 *integer_expression*  
 Is a positive integer expression that specifies the number of groups into which each partition must be divided. *integer_expression* can be of type **int**, or **bigint**.  
  
 \<partition_by_clause>  
 Divides the result set produced by the [FROM](../../t-sql/queries/from-transact-sql.md) clause into partitions to which the function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md).  
  
 \<order_by_clause>  
 Determines the order in which the NTILE values are assigned to the rows in a partition. An integer cannot represent a column when the \<order_by_clause> is used in a ranking function.  
  
## Return Types  
 **bigint**  
  
## Remarks  
 If the number of rows in a partition is not divisible by *integer_expression*, this will cause groups of two sizes that differ by one member. Larger groups come before smaller groups in the order specified by the OVER clause. For example if the total number of rows is 53 and the number of groups is five, the first three groups will have 11 rows and the two remaining groups will have 10 rows each. If on the other hand the total number of rows is divisible by the number of groups, the rows will be evenly distributed among the groups. For example, if the total number of rows is 50, and there are five groups, each bucket will contain 10 rows.  
  
 NTILE is nondeterministic. For more information, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
## Examples  
  
### A. Dividing rows into groups  
 The following example divides rows into four groups of employees based on their year-to-date sales. Because the total number of rows is not divisible by the number of groups, the first two groups have four rows and the remaining groups have three rows each.  
  
```  
USE AdventureWorks2012;   
GO  
SELECT p.FirstName, p.LastName  
    ,NTILE(4) OVER(ORDER BY SalesYTD DESC) AS Quartile  
    ,CONVERT(nvarchar(20),s.SalesYTD,1) AS SalesYTD  
    , a.PostalCode  
FROM Sales.SalesPerson AS s   
INNER JOIN Person.Person AS p   
    ON s.BusinessEntityID = p.BusinessEntityID  
INNER JOIN Person.Address AS a   
    ON a.AddressID = p.BusinessEntityID  
WHERE TerritoryID IS NOT NULL   
    AND SalesYTD <> 0;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
  
FirstName      LastName              Quartile  SalesYTD       PostalCode  
-------------  --------------------- --------- -------------- ----------  
Linda          Mitchell              1         4,251,368.55   98027  
Jae            Pak                   1         4,116,871.23   98055  
Michael        Blythe                1         3,763,178.18   98027  
Jillian        Carson                1         3,189,418.37   98027  
Ranjit         Varkey Chudukatil     2         3,121,616.32   98055  
José           Saraiva               2         2,604,540.72   98055  
Shu            Ito                   2         2,458,535.62   98055  
Tsvi           Reiter                2         2,315,185.61   98027  
Rachel         Valdez                3         1,827,066.71   98055  
Tete           Mensa-Annan           3         1,576,562.20   98055  
David          Campbell              3         1,573,012.94   98055  
Garrett        Vargas                4         1,453,719.47   98027  
Lynn           Tsoflias              4         1,421,810.92   98055  
Pamela         Ansman-Wolfe          4         1,352,577.13   98027  

(14 row(s) affected)  
```  
  
### B. Dividing the result set by using PARTITION BY  
 The following example adds the `PARTITION BY` argument to the code in example A. The rows are first partitioned by `PostalCode` and then divided into four groups within each `PostalCode`. The example also declares a variable `@NTILE_Var` and uses that variable to specify the value for the *integer_expression* parameter.  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @NTILE_Var int = 4;  
  
SELECT p.FirstName, p.LastName  
    ,NTILE(@NTILE_Var) OVER(PARTITION BY PostalCode ORDER BY SalesYTD DESC) AS Quartile  
    ,CONVERT(nvarchar(20),s.SalesYTD,1) AS SalesYTD  
    ,a.PostalCode  
FROM Sales.SalesPerson AS s   
INNER JOIN Person.Person AS p   
    ON s.BusinessEntityID = p.BusinessEntityID  
INNER JOIN Person.Address AS a   
    ON a.AddressID = p.BusinessEntityID  
WHERE TerritoryID IS NOT NULL   
    AND SalesYTD <> 0;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
FirstName    LastName             Quartile SalesYTD      PostalCode  
------------ -------------------- -------- ------------  ----------  
Linda        Mitchell             1        4,251,368.55  98027  
Michael      Blythe               1        3,763,178.18  98027  
Jillian      Carson               2        3,189,418.37  98027  
Tsvi         Reiter               2        2,315,185.61  98027  
Garrett      Vargas               3        1,453,719.47  98027  
Pamela       Ansman-Wolfe         4        1,352,577.13  98027  
Jae          Pak                  1        4,116,871.23  98055  
Ranjit       Varkey Chudukatil    1        3,121,616.32  98055  
José         Saraiva              2        2,604,540.72  98055  
Shu          Ito                  2        2,458,535.62  98055  
Rachel       Valdez               3        1,827,066.71  98055  
Tete         Mensa-Annan          3        1,576,562.20  98055  
David        Campbell             4        1,573,012.94  98055  
Lynn         Tsoflias             4        1,421,810.92  98055  
  
(14 row(s) affected)  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Dividing rows into groups  
 The following example uses the NTILE function to divide a set of salespersons into four groups based on their assigned sales quota for the year 2003. Because the total number of rows is not divisible by the number of groups, the first group has five rows and the remaining groups have four rows each.  
  
```  
-- Uses AdventureWorks  
  
SELECT e.LastName, NTILE(4) OVER(ORDER BY SUM(SalesAmountQuota) DESC) AS Quartile,  
       CONVERT (varchar(13), SUM(SalesAmountQuota), 1) AS SalesQuota  
FROM dbo.DimEmployee AS e   
INNER JOIN dbo.FactSalesQuota AS sq   
    ON e.EmployeeKey = sq.EmployeeKey  
WHERE sq.CalendarYear = 2003  
    AND SalesTerritoryKey IS NOT NULL AND SalesAmountQuota <> 0  
GROUP BY e.LastName  
ORDER BY Quartile, e.LastName;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
LastName          Quartile SalesYTD  
----------------- -------- ------------`  
Blythe            1        4,716,000.00  
Carson            1        4,350,000.00  
Mitchell          1        4,682,000.00  
Pak               1        5,142,000.00  
Varkey Chudukatil 1        2,940,000.00  
Ito               2        2,644,000.00  
Saraiva           2        2,293,000.00  
Vargas            2        1,617,000.00  
Ansman-Wolfe      3        1,183,000.00  
Campbell          3        1,438,000.00  
Mensa-Annan       3        1,481,000.00  
Valdez            3        1,294,000.00  
Abbas             4          172,000.00  
Albert            4          651,000.00  
Jiang             4          544,000.00  
Tsoflias          4          867,000.00
```  
  
### D. Dividing the result set by using PARTITION BY  
 The following example adds the PARTITION BY argument to the code in example A. The rows are first partitioned by `SalesTerritoryCountry` and then divided into two groups within each `SalesTerritoryCountry`. Notice that the ORDER BY in the OVER clause orders the NTILE and the ORDER BY of the SELECT statement orders the result set.  
  
```  
-- Uses AdventureWorks  
  
SELECT e.LastName, NTILE(2) OVER(PARTITION BY e.SalesTerritoryKey ORDER BY SUM(SalesAmountQuota) DESC) AS Quartile,  
       CONVERT (varchar(13), SUM(SalesAmountQuota), 1) AS SalesQuota  
   ,st.SalesTerritoryCountry  
FROM dbo.DimEmployee AS e   
INNER JOIN dbo.FactSalesQuota AS sq   
    ON e.EmployeeKey = sq.EmployeeKey  
INNER JOIN dbo.DimSalesTerritory AS st  
    ON e.SalesTerritoryKey = st.SalesTerritoryKey  
WHERE sq.CalendarYear = 2003  
GROUP BY e.LastName,e.SalesTerritoryKey,st.SalesTerritoryCountry  
ORDER BY st.SalesTerritoryCountry, Quartile;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
LastName          Quartile SalesYTD       SalesTerritoryCountry  
----------------- -------- -------------- ------------------  
Tsoflias          1         867,000.00     Australia  
Saraiva           1        2,293,000.00    Canada  
Varkey Chudukatil 1        2,940,000.00    France  
Valdez            1        1,294,000.00    Germany  
Alberts           1          651,000.00    NA  
Jiang             1          544,000.00    NA  
Pak               1        5,142,000.00    United Kingdom  
Mensa-Annan       1        1,481,000.00    United States  
Campbell          1        1,438,000.00    United States  
Reiter            1        2,768,000.00    United States  
Blythe            1        4,716,000.00    United States  
Carson            1        4,350,000.00     United States  
Mitchell          1        4,682,000.00     United States  
Vargas            2        1,617,000.00     Canada  
Abbas             2          172,000.00     NA  
Ito               2        2,644,000.00     United States  
Ansman-Wolfe      2        1,183,000.00     United States
```  
  
## See Also  
 [RANK &#40;Transact-SQL&#41;](../../t-sql/functions/rank-transact-sql.md)   
 [DENSE_RANK &#40;Transact-SQL&#41;](../../t-sql/functions/dense-rank-transact-sql.md)   
 [ROW_NUMBER &#40;Transact-SQL&#41;](../../t-sql/functions/row-number-transact-sql.md)   
 [Ranking Functions &#40;Transact-SQL&#41;](../../t-sql/functions/ranking-functions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)  
  
  


