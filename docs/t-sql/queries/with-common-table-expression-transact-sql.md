---
title: "WITH common_table_expression (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "WITH common_table_expression"
  - "WITH_TSQL"
  - "WITH"
  - "common table expression"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "WITH common_table_expression clause"
  - "CTEs"
  - "hierarchical queries [SQL Server], WITH common_table_expression"
  - "recursive CTEs [SQL Server]"
  - "recursive queries [SQL Server]"
  - "common table expressions"
  - "MAXRECURSION hint"
  - "clauses [SQL Server], WITH common_table_expression"
ms.assetid: 27cfb819-3e8d-4274-8bbe-cbbe4d9c2e23
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# WITH common_table_expression (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Specifies a temporary named result set, known as a common table expression (CTE). This is derived from a simple query and defined within the execution scope of a single SELECT, INSERT, UPDATE, DELETE or MERGE statement. This clause can also be used in a CREATE VIEW statement as part of its defining SELECT statement. A common table expression can include references to itself. This is referred to as a recursive common table expression.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
[ WITH <common_table_expression> [ ,...n ] ]  
  
<common_table_expression>::=  
    expression_name [ ( column_name [ ,...n ] ) ]  
    AS  
    ( CTE_query_definition )  
```  
  
## Arguments  
 *expression_name*  
Is a valid identifier for the common table expression. *expression_name* must be different from the name of any other common table expression defined in the same WITH \<common_table_expression> clause, but *expression_name* can be the same as the name of a base table or view. Any reference to *expression_name* in the query uses the common table expression and not the base object.
  
 *column_name*  
 Specifies a column name in the common table expression. Duplicate names within a single CTE definition are not allowed. The number of column names specified must match the number of columns in the result set of the *CTE_query_definition*. The list of column names is optional only if distinct names for all resulting columns are supplied in the query definition.  
  
 *CTE_query_definition*  
 Specifies a SELECT statement whose result set populates the common table expression. The SELECT statement for *CTE_query_definition* must meet the same requirements as for creating a view, except a CTE cannot define another CTE. For more information, see the Remarks section and [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md).  
  
 If more than one *CTE_query_definition* is defined, the query definitions must be joined by one of these set operators: UNION ALL, UNION, EXCEPT, or INTERSECT.  
  
## Remarks  
  
## Guidelines for Creating and Using Common Table Expressions  
The following guidelines apply to nonrecursive common table expressions. For guidelines that apply to recursive common table expressions, see [Guidelines for Defining and Using Recursive Common Table Expressions](#guidelines-for-defining-and-using-recursive-common-table-expressions) that follows.  
  
-   A CTE must be followed by a single `SELECT`, `INSERT`, `UPDATE`, or `DELETE` statement that references some or all the CTE columns. A CTE can also be specified in a `CREATE VIEW` statement as part of the defining `SELECT` statement of the view.  
  
-   Multiple CTE query definitions can be defined in a nonrecursive CTE. The definitions must be combined by one of these set operators: `UNION ALL`, `UNION`, `INTERSECT`, or `EXCEPT`.  
  
-   A CTE can reference itself and previously defined CTEs in the same `WITH` clause. Forward referencing is not allowed.  
  
-   Specifying more than one WITH clause in a CTE is not allowed. For example, if a *CTE_query_definition* contains a subquery, that subquery cannot contain a nested WITH clause that defines another CTE.  
  
-   The following clauses cannot be used in the *CTE_query_definition*:  
  
    -   `ORDER BY` (except when a `TOP` clause is specified)  
  
    -   `INTO`  
  
    -   `OPTION` clause with query hints  
  
    -   `FOR BROWSE`  
  
-   When a CTE is used in a statement that is part of a batch, the statement before it must be followed by a semicolon.  
  
-   A query referencing a CTE can be used to define a cursor.  
  
-   Tables on remote servers can be referenced in the CTE.  
  
-   When executing a CTE, any hints that reference a CTE may conflict with other hints that are discovered when the CTE accesses its underlying tables, in the same manner as hints that reference views in queries. When this occurs, the query returns an error.  
  
## Guidelines for Defining and Using Recursive Common Table Expressions  
 The following guidelines apply to defining a recursive common table expression:  
  
-   The recursive CTE definition must contain at least two CTE query definitions, an anchor member and a recursive member. Multiple anchor members and recursive members can be defined; however, all anchor member query definitions must be put before the first recursive member definition. All CTE query definitions are anchor members unless they reference the CTE itself.  
  
-   Anchor members must be combined by one of these set operators: UNION ALL, UNION, INTERSECT, or EXCEPT. UNION ALL is the only set operator allowed between the last anchor member and first recursive member, and when combining multiple recursive members.  
  
-   The number of columns in the anchor and recursive members must be the same.  
  
-   The data type of a column in the recursive member must be the same as the data type of the corresponding column in the anchor member.  
  
-   The FROM clause of a recursive member must refer only one time to the CTE *expression_name*.  
  
-   The following items are not allowed in the *CTE_query_definition* of a recursive member:  
  
    -   `SELECT DISTINCT`  
  
    -   `GROUP BY`  
  
    -   `PIVOT` (When the database compatibility level is 110 or higher. See [Breaking Changes to Database Engine Features in SQL Server 2016](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md).)  
  
    -   `HAVING`  
  
    -   Scalar aggregation  
  
    -   `TOP`  
  
    -   `LEFT`, `RIGHT`, `OUTER JOIN` (`INNER JOIN` is allowed)  
  
    -   Subqueries  
  
    -   A hint applied to a recursive reference to a CTE inside a *CTE_query_definition*.  
  
 The following guidelines apply to using a recursive common table expression:  
  
-   All columns returned by the recursive CTE are nullable regardless of the nullability of the columns returned by the participating `SELECT` statements.  
  
-   An incorrectly composed recursive CTE may cause an infinite loop. For example, if the recursive member query definition returns the same values for both the parent and child columns, an infinite loop is created. To prevent an infinite loop, you can limit the number of recursion levels allowed for a particular statement by using the `MAXRECURSION` hint and a value between 0 and 32,767 in the OPTION clause of the `INSERT`, `UPDATE`, `DELETE`, or `SELECT` statement. This lets you control the execution of the statement until you resolve the code problem that is creating the loop. The server-wide default is 100. When 0 is specified, no limit is applied. Only one `MAXRECURSION` value can be specified per statement. For more information, see [Query Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-query.md).  
  
-   A view that contains a recursive common table expression cannot be used to update data.  
  
-   Cursors may be defined on queries using CTEs. The CTE is the *select_statement* argument that defines the result set of the cursor. Only fast forward-only and static (snapshot) cursors are allowed for recursive CTEs. If another cursor type is specified in a recursive CTE, the cursor type is converted to static.  
  
-   Tables on remote servers may be referenced in the CTE. If the remote server is referenced in the recursive member of the CTE, a spool is created for each remote table so the tables can be repeatedly accessed locally. If it is a CTE query, Index Spool/Lazy Spools is displayed in the query plan and will have the additional `WITH STACK` predicate. This is one way to confirm proper recursion.  
  
-   Analytic and aggregate functions in the recursive part of the CTE are applied to the set for the current recursion level and not to the set for the CTE. Functions like `ROW_NUMBER` operate only on the subset of data passed to them by the current recursion level and not the entire set of data passed to the recursive part of the CTE. For more information, see example K. Using analytical functions in a recursive CTE that follows.  
  
## Features and Limitations of Common Table Expressions in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The current implementation of CTEs in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] have the following features and limitations:  
  
-   A CTE can be specified in a `SELECT` statement.  
  
-   A CTE can be specified in a `CREATE VIEW` statement.  
  
-   A CTE can be specified in a `CREATE TABLE AS SELECT` (CTAS) statement.  
  
-   A CTE can be specified in a `CREATE REMOTE TABLE AS SELECT` (CRTAS) statement.  
  
-   A CTE can be specified in a `CREATE EXTERNAL TABLE AS SELECT` (CETAS) statement.  
  
-   A remote table can be referenced from a CTE.  
  
-   An external table can be referenced from a CTE.  
  
-   Multiple CTE query definitions can be defined in a CTE.  
  
-   A CTE must be followed by a single `SELECT` statement. `INSERT`, `UPDATE`, `DELETE`, and `MERGE` statements are not supported.  
  
-   A common table expression that includes references to itself (a recursive common table expression) is not supported.  
  
-   Specifying more than one `WITH` clause in a CTE is not allowed. For example, if a CTE query definition contains a subquery, that subquery cannot contain a nested `WITH` clause that defines another CTE.  
  
-   An `ORDER BY` clause cannot be used in the CTE_query_definition, except when a `TOP` clause is specified.  
  
-   When a CTE is used in a statement that is part of a batch, the statement before it must be followed by a semicolon.  
  
-   When used in statements prepared by `sp_prepare`, CTEs will behave the same way as other `SELECT` statements in PDW. However, if CTEs are used as part of CETAS prepared by `sp_prepare`, the behavior can defer from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and other PDW statements because of the way binding is implemented for `sp_prepare`. If `SELECT` that references CTE is using a wrong column that does not exist in CTE, the `sp_prepare` will pass without detecting the error, but the error will be thrown during `sp_execute` instead.  
  
## Examples  
  
### A. Creating a simple common table expression  
 The following example shows the total number of sales orders per year for each sales representative at [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)].  
  
```sql   
-- Define the CTE expression name and column list.  
WITH Sales_CTE (SalesPersonID, SalesOrderID, SalesYear)  
AS  
-- Define the CTE query.  
(  
    SELECT SalesPersonID, SalesOrderID, YEAR(OrderDate) AS SalesYear  
    FROM Sales.SalesOrderHeader  
    WHERE SalesPersonID IS NOT NULL  
)  
-- Define the outer query referencing the CTE name.  
SELECT SalesPersonID, COUNT(SalesOrderID) AS TotalSales, SalesYear  
FROM Sales_CTE  
GROUP BY SalesYear, SalesPersonID  
ORDER BY SalesPersonID, SalesYear;  
```  
  
### B. Using a common table expression to limit counts and report averages  
 The following example shows the average number of sales orders for all years for the sales representatives.  
  
```sql  
WITH Sales_CTE (SalesPersonID, NumberOfOrders)  
AS  
(  
    SELECT SalesPersonID, COUNT(*)  
    FROM Sales.SalesOrderHeader  
    WHERE SalesPersonID IS NOT NULL  
    GROUP BY SalesPersonID  
)  
SELECT AVG(NumberOfOrders) AS "Average Sales Per Person"  
FROM Sales_CTE;  
```  
  
### C. Using multiple CTE definitions in a single query  
 The following example shows how to define more than one CTE in a single query. Notice that a comma is used to separate the CTE query definitions. The FORMAT function, used to display the monetary amounts in a currency format, is available in SQL Server 2012 and higher.  
  
```sql  
WITH Sales_CTE (SalesPersonID, TotalSales, SalesYear)  
AS  
-- Define the first CTE query.  
(  
    SELECT SalesPersonID, SUM(TotalDue) AS TotalSales, YEAR(OrderDate) AS SalesYear  
    FROM Sales.SalesOrderHeader  
    WHERE SalesPersonID IS NOT NULL  
       GROUP BY SalesPersonID, YEAR(OrderDate)  
  
)  
,   -- Use a comma to separate multiple CTE definitions.  
  
-- Define the second CTE query, which returns sales quota data by year for each sales person.  
Sales_Quota_CTE (BusinessEntityID, SalesQuota, SalesQuotaYear)  
AS  
(  
       SELECT BusinessEntityID, SUM(SalesQuota)AS SalesQuota, YEAR(QuotaDate) AS SalesQuotaYear  
       FROM Sales.SalesPersonQuotaHistory  
       GROUP BY BusinessEntityID, YEAR(QuotaDate)  
)  
  
-- Define the outer query by referencing columns from both CTEs.  
SELECT SalesPersonID  
  , SalesYear  
  , FORMAT(TotalSales,'C','en-us') AS TotalSales  
  , SalesQuotaYear  
  , FORMAT (SalesQuota,'C','en-us') AS SalesQuota  
  , FORMAT (TotalSales -SalesQuota, 'C','en-us') AS Amt_Above_or_Below_Quota  
FROM Sales_CTE  
JOIN Sales_Quota_CTE ON Sales_Quota_CTE.BusinessEntityID = Sales_CTE.SalesPersonID  
                    AND Sales_CTE.SalesYear = Sales_Quota_CTE.SalesQuotaYear  
ORDER BY SalesPersonID, SalesYear;    
```  
  
Here is a partial result set.  
  
```  
SalesPersonID SalesYear   TotalSales    SalesQuotaYear SalesQuota  Amt_Above_or_Below_Quota  
------------- ---------   -----------   -------------- ---------- ----------------------------------   
274           2005        $32,567.92    2005           $35,000.00  ($2,432.08)  
274           2006        $406,620.07   2006           $455,000.00 ($48,379.93)  
274           2007        $515,622.91   2007           $544,000.00 ($28,377.09)  
274           2008        $281,123.55   2008           $271,000.00  $10,123.55  
```  
  
### D. Using a recursive common table expression to display multiple levels of recursion  
 The following example shows the hierarchical list of managers and the employees who report to them. The example begins by creating and populating the `dbo.MyEmployees` table.  
  
```sql  
-- Create an Employee table.  
CREATE TABLE dbo.MyEmployees  
(  
EmployeeID smallint NOT NULL,  
FirstName nvarchar(30)  NOT NULL,  
LastName  nvarchar(40) NOT NULL,  
Title nvarchar(50) NOT NULL,  
DeptID smallint NOT NULL,  
ManagerID int NULL,  
 CONSTRAINT PK_EmployeeID PRIMARY KEY CLUSTERED (EmployeeID ASC)   
);  
-- Populate the table with values.  
INSERT INTO dbo.MyEmployees VALUES   
 (1, N'Ken', N'Sánchez', N'Chief Executive Officer',16,NULL)  
,(273, N'Brian', N'Welcker', N'Vice President of Sales',3,1)  
,(274, N'Stephen', N'Jiang', N'North American Sales Manager',3,273)  
,(275, N'Michael', N'Blythe', N'Sales Representative',3,274)  
,(276, N'Linda', N'Mitchell', N'Sales Representative',3,274)  
,(285, N'Syed', N'Abbas', N'Pacific Sales Manager',3,273)  
,(286, N'Lynn', N'Tsoflias', N'Sales Representative',3,285)  
,(16,  N'David',N'Bradley', N'Marketing Manager', 4, 273)  
,(23,  N'Mary', N'Gibson', N'Marketing Specialist', 4, 16);  
```  
  
```sql  
USE AdventureWorks2012;  
GO  
WITH DirectReports(ManagerID, EmployeeID, Title, EmployeeLevel) AS   
(  
    SELECT ManagerID, EmployeeID, Title, 0 AS EmployeeLevel  
    FROM dbo.MyEmployees   
    WHERE ManagerID IS NULL  
    UNION ALL  
    SELECT e.ManagerID, e.EmployeeID, e.Title, EmployeeLevel + 1  
    FROM dbo.MyEmployees AS e  
        INNER JOIN DirectReports AS d  
        ON e.ManagerID = d.EmployeeID   
)  
SELECT ManagerID, EmployeeID, Title, EmployeeLevel   
FROM DirectReports  
ORDER BY ManagerID;   
```  
  
### E. Using a recursive common table expression to display two levels of recursion  
 The following example shows managers and the employees reporting to them. The number of levels returned is limited to two.  
  
```sql  
USE AdventureWorks2012;  
GO  
WITH DirectReports(ManagerID, EmployeeID, Title, EmployeeLevel) AS   
(  
    SELECT ManagerID, EmployeeID, Title, 0 AS EmployeeLevel  
    FROM dbo.MyEmployees   
    WHERE ManagerID IS NULL  
    UNION ALL  
    SELECT e.ManagerID, e.EmployeeID, e.Title, EmployeeLevel + 1  
    FROM dbo.MyEmployees AS e  
        INNER JOIN DirectReports AS d  
        ON e.ManagerID = d.EmployeeID   
)  
SELECT ManagerID, EmployeeID, Title, EmployeeLevel   
FROM DirectReports  
WHERE EmployeeLevel <= 2 ;  
```  
  
### F. Using a recursive common table expression to display a hierarchical list  
 The following example builds on Example D by adding the names of the manager and employees, and their respective titles. The hierarchy of managers and employees is additionally emphasized by indenting each level.  
  
```sql  
USE AdventureWorks2012;  
GO  
WITH DirectReports(Name, Title, EmployeeID, EmployeeLevel, Sort)  
AS (SELECT CONVERT(varchar(255), e.FirstName + ' ' + e.LastName),  
        e.Title,  
        e.EmployeeID,  
        1,  
        CONVERT(varchar(255), e.FirstName + ' ' + e.LastName)  
    FROM dbo.MyEmployees AS e  
    WHERE e.ManagerID IS NULL  
    UNION ALL  
    SELECT CONVERT(varchar(255), REPLICATE ('|    ' , EmployeeLevel) +  
        e.FirstName + ' ' + e.LastName),  
        e.Title,  
        e.EmployeeID,  
        EmployeeLevel + 1,  
        CONVERT (varchar(255), RTRIM(Sort) + '|    ' + FirstName + ' ' +   
                 LastName)  
    FROM dbo.MyEmployees AS e  
    JOIN DirectReports AS d ON e.ManagerID = d.EmployeeID  
    )  
SELECT EmployeeID, Name, Title, EmployeeLevel  
FROM DirectReports   
ORDER BY Sort;  
```  
  
### G. Using MAXRECURSION to cancel a statement  
 `MAXRECURSION` can be used to prevent a poorly formed recursive CTE from entering into an infinite loop. The following example intentionally creates an infinite loop and uses the `MAXRECURSION` hint to limit the number of recursion levels to two.  
  
```sql  
USE AdventureWorks2012;  
GO  
--Creates an infinite loop  
WITH cte (EmployeeID, ManagerID, Title) as  
(  
    SELECT EmployeeID, ManagerID, Title  
    FROM dbo.MyEmployees  
    WHERE ManagerID IS NOT NULL  
  UNION ALL  
    SELECT cte.EmployeeID, cte.ManagerID, cte.Title  
    FROM cte   
    JOIN  dbo.MyEmployees AS e   
        ON cte.ManagerID = e.EmployeeID  
)  
--Uses MAXRECURSION to limit the recursive levels to 2  
SELECT EmployeeID, ManagerID, Title  
FROM cte  
OPTION (MAXRECURSION 2);  
```  
  
 After the coding error is corrected, MAXRECURSION is no longer required. The following example shows the corrected code.  
  
```sql  
USE AdventureWorks2012;  
GO  
WITH cte (EmployeeID, ManagerID, Title)  
AS  
(  
    SELECT EmployeeID, ManagerID, Title  
    FROM dbo.MyEmployees  
    WHERE ManagerID IS NOT NULL  
  UNION ALL  
    SELECT  e.EmployeeID, e.ManagerID, e.Title  
    FROM dbo.MyEmployees AS e  
    JOIN cte ON e.ManagerID = cte.EmployeeID  
)  
SELECT EmployeeID, ManagerID, Title  
FROM cte;  
```  
  
### H. Using a common table expression to selectively step through a recursive relationship in a SELECT statement  
 The following example shows the hierarchy of product assemblies and components that are required to build the bicycle for `ProductAssemblyID = 800`.  
  
```sql  
USE AdventureWorks2012;  
GO  
WITH Parts(AssemblyID, ComponentID, PerAssemblyQty, EndDate, ComponentLevel) AS  
(  
    SELECT b.ProductAssemblyID, b.ComponentID, b.PerAssemblyQty,  
        b.EndDate, 0 AS ComponentLevel  
    FROM Production.BillOfMaterials AS b  
    WHERE b.ProductAssemblyID = 800  
          AND b.EndDate IS NULL  
    UNION ALL  
    SELECT bom.ProductAssemblyID, bom.ComponentID, p.PerAssemblyQty,  
        bom.EndDate, ComponentLevel + 1  
    FROM Production.BillOfMaterials AS bom   
        INNER JOIN Parts AS p  
        ON bom.ProductAssemblyID = p.ComponentID  
        AND bom.EndDate IS NULL  
)  
SELECT AssemblyID, ComponentID, Name, PerAssemblyQty, EndDate,  
        ComponentLevel   
FROM Parts AS p  
    INNER JOIN Production.Product AS pr  
    ON p.ComponentID = pr.ProductID  
ORDER BY ComponentLevel, AssemblyID, ComponentID;  
```  
  
### I. Using a recursive CTE in an UPDATE statement  
 The following example updates the `PerAssemblyQty` value for all parts that are used to build the product 'Road-550-W Yellow, 44' `(ProductAssemblyID``800`). The common table expression returns a hierarchical list of parts that are used to build `ProductAssemblyID 800` and the components that are used to create those parts, and so on. Only the rows returned by the common table expression are modified.  
  
```sql  
USE AdventureWorks2012;  
GO  
WITH Parts(AssemblyID, ComponentID, PerAssemblyQty, EndDate, ComponentLevel) AS  
(  
    SELECT b.ProductAssemblyID, b.ComponentID, b.PerAssemblyQty,  
        b.EndDate, 0 AS ComponentLevel  
    FROM Production.BillOfMaterials AS b  
    WHERE b.ProductAssemblyID = 800  
          AND b.EndDate IS NULL  
    UNION ALL  
    SELECT bom.ProductAssemblyID, bom.ComponentID, p.PerAssemblyQty,  
        bom.EndDate, ComponentLevel + 1  
    FROM Production.BillOfMaterials AS bom   
        INNER JOIN Parts AS p  
        ON bom.ProductAssemblyID = p.ComponentID  
        AND bom.EndDate IS NULL  
)  
UPDATE Production.BillOfMaterials  
SET PerAssemblyQty = c.PerAssemblyQty * 2  
FROM Production.BillOfMaterials AS c  
JOIN Parts AS d ON c.ProductAssemblyID = d.AssemblyID  
WHERE d.ComponentLevel = 0;  
```  
  
### J. Using multiple anchor and recursive members  
 The following example uses multiple anchor and recursive members to return all the ancestors of a specified person. A table is created and values inserted to establish the family genealogy returned by the recursive CTE.  
  
```sql  
-- Genealogy table  
IF OBJECT_ID('dbo.Person','U') IS NOT NULL DROP TABLE dbo.Person;  
GO  
CREATE TABLE dbo.Person(ID int, Name varchar(30), Mother int, Father int);  
GO  
INSERT dbo.Person   
VALUES(1, 'Sue', NULL, NULL)  
      ,(2, 'Ed', NULL, NULL)  
      ,(3, 'Emma', 1, 2)  
      ,(4, 'Jack', 1, 2)  
      ,(5, 'Jane', NULL, NULL)  
      ,(6, 'Bonnie', 5, 4)  
      ,(7, 'Bill', 5, 4);  
GO  
-- Create the recursive CTE to find all of Bonnie's ancestors.  
WITH Generation (ID) AS  
(  
-- First anchor member returns Bonnie's mother.  
    SELECT Mother   
    FROM dbo.Person  
    WHERE Name = 'Bonnie'  
UNION  
-- Second anchor member returns Bonnie's father.  
    SELECT Father   
    FROM dbo.Person  
    WHERE Name = 'Bonnie'  
UNION ALL  
-- First recursive member returns male ancestors of the previous generation.  
    SELECT Person.Father  
    FROM Generation, Person  
    WHERE Generation.ID=Person.ID  
UNION ALL  
-- Second recursive member returns female ancestors of the previous generation.  
    SELECT Person.Mother  
    FROM Generation, dbo.Person  
    WHERE Generation.ID=Person.ID  
)  
SELECT Person.ID, Person.Name, Person.Mother, Person.Father  
FROM Generation, dbo.Person  
WHERE Generation.ID = Person.ID;  
GO  
```  
  
###  <a name="bkmkUsingAnalyticalFunctionsInARecursiveCTE"></a> K. Using analytical functions in a recursive CTE  
 The following example shows a pitfall that can occur when using an analytical or aggregate function in the recursive part of a CTE.  
  
```sql  
DECLARE @t1 TABLE (itmID int, itmIDComp int);  
INSERT @t1 VALUES (1,10), (2,10);   
  
DECLARE @t2 TABLE (itmID int, itmIDComp int);   
INSERT @t2 VALUES (3,10), (4,10);   
  
WITH vw AS  
 (  
    SELECT itmIDComp, itmID  
    FROM @t1  
  
    UNION ALL  
  
    SELECT itmIDComp, itmID  
    FROM @t2  
)   
,r AS  
 (  
    SELECT t.itmID AS itmIDComp  
           , NULL AS itmID  
           ,CAST(0 AS bigint) AS N  
           ,1 AS Lvl  
    FROM (SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4) AS t (itmID)   
  
UNION ALL  
  
SELECT t.itmIDComp  
    , t.itmID  
    , ROW_NUMBER() OVER(PARTITION BY t.itmIDComp ORDER BY t.itmIDComp, t.itmID) AS N  
    , Lvl + 1  
FROM r   
    JOIN vw AS t ON t.itmID = r.itmIDComp  
)   
  
SELECT Lvl, N FROM r;  
```  
  
The following results are the expected results for the query.  
  
```  
Lvl  N  
1    0  
1    0  
1    0  
1    0  
2    4  
2    3  
2    2  
2    1  
```  
  
The following results are the actual results for the query.  
  
```  
Lvl  N  
1    0  
1    0  
1    0  
1    0  
2    1  
2    1  
2    1  
2    1  
```  
  
 `N` returns 1 for each pass of the recursive part of the CTE because only the subset of data for that recursion level is passed to `ROWNUMBER`. For each of the iterations of the recursive part of the query, only one row is passed to `ROWNUMBER`.  
  
## Examples: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### L. Using a common table expression within a CTAS statement  
 The following example creates a new table containing the total number of sales orders per year for each sales representative at [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)].  
  
```sql  
USE AdventureWorks2012;  
GO   
CREATE TABLE SalesOrdersPerYear  
WITH  
(  
    DISTRIBUTION = HASH(SalesPersonID)  
)  
AS  
    -- Define the CTE expression name and column list.  
    WITH Sales_CTE (SalesPersonID, SalesOrderID, SalesYear)  
    AS  
    -- Define the CTE query.  
    (  
        SELECT SalesPersonID, SalesOrderID, YEAR(OrderDate) AS SalesYear  
        FROM Sales.SalesOrderHeader  
        WHERE SalesPersonID IS NOT NULL  
    )  
    -- Define the outer query referencing the CTE name.  
    SELECT SalesPersonID, COUNT(SalesOrderID) AS TotalSales, SalesYear  
    FROM Sales_CTE  
    GROUP BY SalesYear, SalesPersonID  
    ORDER BY SalesPersonID, SalesYear;  
GO  
```  
  
### M. Using a common table expression within a CETAS statement  
 The following example creates a new external table containing the total number of sales orders per year for each sales representative at [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)].  
  
```sql  
USE AdventureWorks2012;  
GO    
CREATE EXTERNAL TABLE SalesOrdersPerYear  
WITH  
(  
    LOCATION = 'hdfs://xxx.xxx.xxx.xxx:5000/files/Customer',  
    FORMAT_OPTIONS ( FIELD_TERMINATOR = '|' )   
)  
AS  
    -- Define the CTE expression name and column list.  
    WITH Sales_CTE (SalesPersonID, SalesOrderID, SalesYear)  
    AS  
    -- Define the CTE query.  
    (  
        SELECT SalesPersonID, SalesOrderID, YEAR(OrderDate) AS SalesYear  
        FROM Sales.SalesOrderHeader  
        WHERE SalesPersonID IS NOT NULL  
    )  
    -- Define the outer query referencing the CTE name.  
    SELECT SalesPersonID, COUNT(SalesOrderID) AS TotalSales, SalesYear  
    FROM Sales_CTE  
    GROUP BY SalesYear, SalesPersonID  
    ORDER BY SalesPersonID, SalesYear;  
GO  
```  
  
### N. Using multiple comma separated CTEs in a statement  
 The following example demonstrates including two CTEs in a single statement. The CTEs cannot be nested (no recursion).  
  
```sql  
WITH   
 CountDate (TotalCount, TableName) AS  
    (  
     SELECT COUNT(datekey), 'DimDate' FROM DimDate  
    ) ,  
 CountCustomer (TotalAvg, TableName) AS  
    (  
     SELECT COUNT(CustomerKey), 'DimCustomer' FROM DimCustomer  
    )  
SELECT TableName, TotalCount FROM CountDate  
UNION ALL  
SELECT TableName, TotalAvg FROM CountCustomer;  
```  
  
## See Also  
 [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md)   
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [EXCEPT and INTERSECT &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-operators-except-and-intersect-transact-sql.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)  
  
 
