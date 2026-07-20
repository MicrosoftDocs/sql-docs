---
title: "Recursive Queries Using Common Table Expressions"
description: "Learn how recursive common table expressions (CTE) work."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf, xiaoyul
ms.date: 09/11/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "WITH common_table_expression clause recursive"
  - "recursive CTEs"
  - "hierarchical queries [SQL Server], WITH common_table_expression recursive"
  - "recursive CTEs [SQL Server]"
  - "recursive queries [SQL Server]"
  - "common table expressions"
  - "MAXRECURSION hint"
  - "recursive clauses [SQL Server], WITH common_table_expression"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Recursive queries using common table expressions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

A common table expression (CTE) provides the significant advantage of being able to reference itself, thus creating a recursive CTE. A recursive CTE is one in which an initial CTE is repeatedly executed to return subsets of data until the complete result set is obtained.

A query is referred to as a recursive query when it references a recursive CTE. Returning hierarchical data is a common use of recursive queries. For example, displaying employees in an organizational chart, or data in a bill of materials scenario in which a parent product has one or more components and those components might have subcomponents, or might be components, of other parents.

A recursive CTE can greatly simplify the code required to run a recursive query within a `SELECT`, `INSERT`, `UPDATE`, `DELETE`, or `CREATE VIEW` statement. In earlier versions of SQL Server, a recursive query usually requires using temporary tables, cursors, and logic to control the flow of the recursive steps. For more information about common table expressions, see [WITH common_table_expression](with-common-table-expression-transact-sql.md).

In Microsoft Fabric, Fabric Data Warehouse and the SQL analytics endpoint both support standard, sequential, and [nested CTEs](nested-common-table-expression.md), but not recursive CTEs.

## Structure of a recursive CTE

The structure of the recursive CTE in Transact-SQL is similar to recursive routines in other programming languages. Although a recursive routine in other languages returns a scalar value, a recursive CTE can return multiple rows.

A recursive CTE consists of three elements:

1. Invocation of the routine.

   The first invocation of the recursive CTE consists of one or more CTE query definitions joined by `UNION ALL`, `UNION`, `EXCEPT`, or `INTERSECT` operators. Because these query definitions form the base result set of the CTE structure, they're referred to as anchor members.

   CTE query definitions are considered anchor members unless they reference the CTE itself. All anchor-member query definitions must be positioned before the first recursive member definition, and a `UNION ALL` operator must be used to join the last anchor member with the first recursive member.

1. Recursive invocation of the routine.

   The recursive invocation includes one or more CTE query definitions joined by `UNION ALL` operators that reference the CTE itself. These query definitions are referred to as recursive members.

1. Termination check.

   The termination check is implicit; recursion stops when no rows are returned from the previous invocation.

> [!NOTE]  
> An incorrectly composed recursive CTE might cause an infinite loop. For example, if the recursive member query definition returns the same values for both the parent and child columns, an infinite loop is created. When testing the results of a recursive query, you can limit the number of recursion levels allowed for a specific statement by using the `MAXRECURSION` hint and a value between 0 and 32,767 in the `OPTION` clause of the `INSERT`, `UPDATE`, `DELETE`, or `SELECT` statement.

For more information, see:

- [Query hints](hints-transact-sql-query.md)
- [WITH common_table_expression](with-common-table-expression-transact-sql.md)
- [Recursive CTEs](https://techcommunity.microsoft.com/blog/sqlserver/recursive-ctes/383326)

## Pseudocode and semantics

The recursive CTE structure must contain at least one anchor member and one recursive member. The following pseudocode shows the components of a simple recursive CTE that contains a single anchor member and single recursive member.

```syntaxsql
WITH cte_name ( column_name [ ,...n ] )
AS
(
    CTE_query_definition -- Anchor member is defined.
    UNION ALL
    CTE_query_definition -- Recursive member is defined referencing cte_name.
)

-- Statement using the CTE
SELECT *
FROM cte_name
```

The semantics of the recursive execution is as follows:

1. Split the CTE expression into anchor and recursive members.
1. Run the anchor members creating the first invocation or base result set (`T0`).
1. Run the recursive members with `Ti` as an input and `Ti` + 1 as an output.
1. Repeat step 3 until an empty set is returned.
1. Return the result set. This is a `UNION ALL` of `T0` to `Tn`.

## Examples

The following example shows the semantics of the recursive CTE structure by returning a hierarchical list of employees, starting with the highest ranking employee, in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. A walkthrough of the code execution follows the example.

Create an employee table:

```sql
CREATE TABLE dbo.MyEmployees
(
    EmployeeID SMALLINT NOT NULL,
    FirstName NVARCHAR (30) NOT NULL,
    LastName NVARCHAR (40) NOT NULL,
    Title NVARCHAR (50) NOT NULL,
    DeptID SMALLINT NOT NULL,
    ManagerID INT NULL,
    CONSTRAINT PK_EmployeeID PRIMARY KEY CLUSTERED (EmployeeID ASC)
);
```

Populate the table with values:

```sql
INSERT INTO dbo.MyEmployees
VALUES
    (1, N'Ken', N'Sánchez', N'Chief Executive Officer', 16, NULL),
    (273, N'Brian', N'Welcker', N'Vice President of Sales', 3, 1),
    (274, N'Stephen', N'Jiang', N'North American Sales Manager', 3, 273),
    (275, N'Michael', N'Blythe', N'Sales Representative', 3, 274),
    (276, N'Linda', N'Mitchell', N'Sales Representative', 3, 274),
    (285, N'Syed', N'Abbas', N'Pacific Sales Manager', 3, 273),
    (286, N'Lynn', N'Tsoflias', N'Sales Representative', 3, 285),
    (16, N'David', N'Bradley', N'Marketing Manager', 4, 273),
    (23, N'Mary', N'Gibson', N'Marketing Specialist', 4, 16);
```

```sql
USE AdventureWorks2008R2;
GO

WITH DirectReports (ManagerID, EmployeeID, Title, DeptID, Level)
AS (
-- Anchor member definition
SELECT e.ManagerID,
           e.EmployeeID,
           e.Title,
           edh.DepartmentID,
           0 AS Level
    FROM dbo.MyEmployees AS e
         INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh
             ON e.EmployeeID = edh.BusinessEntityID
            AND edh.EndDate IS NULL
    WHERE ManagerID IS NULL
    UNION ALL
-- Recursive member definition
    SELECT e.ManagerID,
           e.EmployeeID,
           e.Title,
           edh.DepartmentID,
           Level + 1
    FROM dbo.MyEmployees AS e
         INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh
             ON e.EmployeeID = edh.BusinessEntityID
            AND edh.EndDate IS NULL
         INNER JOIN DirectReports AS d
             ON e.ManagerID = d.EmployeeID)
-- Statement that executes the CTE
SELECT ManagerID,
       EmployeeID,
       Title,
       DeptID,
       Level
FROM DirectReports
     INNER JOIN HumanResources.Department AS dp
         ON DirectReports.DeptID = dp.DepartmentID
WHERE dp.GroupName = N'Sales and Marketing'
      OR Level = 0;
GO
```

### Example code walkthrough

The recursive CTE, `DirectReports`, defines one anchor member and one recursive member.

The anchor member returns the base result set `T0`. This is the highest ranking employee in the company. That is, an employee who doesn't report to a manager.

Here's the result set returned by the anchor member:

```output
ManagerID EmployeeID Title                         Level
--------- ---------- ----------------------------- ------
NULL      1          Chief Executive Officer        0
```

The recursive member returns the direct subordinates of the employee in the anchor member result set. This is achieved by a join operation between the Employee table and the `DirectReports` CTE. It's this reference to the CTE itself that establishes the recursive invocation. Based on the employee in the CTE `DirectReports` as input (`Ti`), the join (`MyEmployees.ManagerID = DirectReports.EmployeeID`) returns as output (`Ti` + 1), the employees who have (`Ti`) as their manager.

Therefore, the first iteration of the recursive member returns this result set:

```output
ManagerID EmployeeID Title                         Level
--------- ---------- ----------------------------- ------
1         273        Vice President of Sales       1
```

The recursive member is activated repeatedly. The second iteration of the recursive member uses the single-row result set in step 3 (containing an `EmployeeID` of `273`) as the input value, and returns this result set:

```output
ManagerID EmployeeID Title                         Level
--------- ---------- ----------------------------- ------
273       16         Marketing Manager             2
273       274        North American Sales Manager  2
273       285        Pacific Sales Manager         2
```

The third iteration of the recursive member uses the previous result set as the input value, and returns this result set:

```output
ManagerID EmployeeID Title                         Level
--------- ---------- ----------------------------- ------
16        23         Marketing Specialist          3
274       275        Sales Representative          3
274       276        Sales Representative          3
285       286        Sales Representative          3
```

The final result set returned by the running query is the union of all result sets generated by the anchor and recursive members.

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
ManagerID EmployeeID Title                         Level
--------- ---------- ----------------------------- ------
NULL      1          Chief Executive Officer       0
1         273        Vice President of Sales       1
273       16         Marketing Manager             2
273       274        North American Sales Manager  2
273       285        Pacific Sales Manager         2
16        23         Marketing Specialist          3
274       275        Sales Representative          3
274       276        Sales Representative          3
285       286        Sales Representative          3
```

## Related content

- [WITH common_table_expression (Transact-SQL)](with-common-table-expression-transact-sql.md)
- [Query hints (Transact-SQL)](hints-transact-sql-query.md)
- [INSERT (Transact-SQL)](../statements/insert-transact-sql.md)
- [UPDATE (Transact-SQL)](update-transact-sql.md)
- [DELETE (Transact-SQL)](../statements/delete-transact-sql.md)
- [EXCEPT and INTERSECT (Transact-SQL)](../language-elements/set-operators-except-and-intersect-transact-sql.md)
- [Recursive CTEs](https://techcommunity.microsoft.com/blog/sqlserver/recursive-ctes/383326)
