---
title: "Create User-defined Functions (Database Engine)"
description: "Create User-defined Functions (Database Engine)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/28/2022"
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "SCHEMABINDING clause"
  - "schema-bound functions [SQL Server]"
  - "user-defined functions [SQL Server], creating"
  - "CREATE FUNCTION statement"
  - "valid statements [SQL Server]"
  - "UDF"
  - "TVF"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create user-defined functions (Database Engine)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

This article describes how to create a user-defined function (UDF) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../includes/tsql-md.md)].

## <a id="Restrictions"></a> Limitations and restrictions

- User-defined functions can't be used to perform actions that modify the database state.

- User-defined functions can't contain an `OUTPUT INTO` clause that has a table as its target.

- User-defined functions can't return multiple result sets. Use a stored procedure if you need to return multiple result sets.

- Error handling is restricted in a user-defined function. A UDF doesn't support `TRY...CATCH`, `@ERROR` or `RAISERROR`.

- User-defined functions can't call a stored procedure, but can call an extended stored procedure.

- User-defined functions can't make use of dynamic SQL or temp tables. Table variables are allowed.

- `SET` statements aren't allowed in a user-defined function.

- The `FOR XML` clause isn't allowed.

- User-defined functions can be nested; that is, one user-defined function can call another. The nesting level is incremented when the called function starts execution, and decremented when the called function finishes execution. User-defined functions can be nested up to 32 levels. Exceeding the maximum levels of nesting causes the whole calling function chain to fail. Any reference to managed code from a Transact-SQL user-defined function counts as one level against the 32-level nesting limit. Methods invoked from within managed code don't count against this limit.

- The following Service Broker statements **cannot be included** in the definition of a [!INCLUDE[tsql](../../includes/tsql-md.md)]  user-defined function:

  - `BEGIN DIALOG CONVERSATION`
  - `END CONVERSATION`
  - `GET CONVERSATION GROUP`
  - `MOVE CONVERSATION`
  - `RECEIVE`
  - `SEND`

## Permissions

Requires `CREATE FUNCTION` permission in the database and `ALTER` permission on the schema in which the function is being created. If the function specifies a user-defined type, requires `EXECUTE` permission on the type.

## <a id="Scalar"></a> Scalar function examples

### Scalar function (scalar UDF)

The following example creates a multi-statement *scalar function (scalar UDF)* in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. The function takes one input value, a `ProductID`, and returns a single data value, the aggregated quantity of the specified product in inventory.

```sql
IF OBJECT_ID (N'dbo.ufnGetInventoryStock', N'FN') IS NOT NULL
    DROP FUNCTION ufnGetInventoryStock;
GO
CREATE FUNCTION dbo.ufnGetInventoryStock(@ProductID int)
RETURNS int
AS
-- Returns the stock level for the product.
BEGIN
    DECLARE @ret int;
    SELECT @ret = SUM(p.Quantity)
    FROM Production.ProductInventory p
    WHERE p.ProductID = @ProductID
        AND p.LocationID = '6';
     IF (@ret IS NULL)
        SET @ret = 0;
    RETURN @ret;
END;
```

The following example uses the `ufnGetInventoryStock` function to return the current inventory quantity for products that have a `ProductModelID` between 75 and 80.

```sql
SELECT ProductModelID, Name, dbo.ufnGetInventoryStock(ProductID)AS CurrentSupply
FROM Production.Product
WHERE ProductModelID BETWEEN 75 and 80;
```

For more information and examples of scalar functions, see [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md).

## <a id="TVF"></a> Table-valued function examples

### Inline table-valued function (TVF)

The following example creates an inline table-valued function (TVF) in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. The function takes one input parameter, a customer (store) ID, and returns the columns `ProductID`, `Name`, and the aggregate of year-to-date sales as `YTD Total` for each product sold to the store.

```sql
IF OBJECT_ID (N'Sales.ufn_SalesByStore', N'IF') IS NOT NULL
    DROP FUNCTION Sales.ufn_SalesByStore;
GO
CREATE FUNCTION Sales.ufn_SalesByStore (@storeid int)
RETURNS TABLE
AS
RETURN
(
    SELECT P.ProductID, P.Name, SUM(SD.LineTotal) AS 'Total'
    FROM Production.Product AS P
    JOIN Sales.SalesOrderDetail AS SD ON SD.ProductID = P.ProductID
    JOIN Sales.SalesOrderHeader AS SH ON SH.SalesOrderID = SD.SalesOrderID
    JOIN Sales.Customer AS C ON SH.CustomerID = C.CustomerID
    WHERE C.StoreID = @storeid
    GROUP BY P.ProductID, P.Name
);
```

The following example invokes the function and specifies customer ID 602.

```sql
SELECT * FROM Sales.ufn_SalesByStore (602);
```

### Multi-statement table-valued function (MSTVF)

The following example creates a multi-statement table-valued function (MSTVF) in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. The function takes a single input parameter, an `EmployeeID` and returns a list of all the employees who report to the specified employee directly or indirectly. The function is then invoked specifying employee ID 109.

```sql
IF OBJECT_ID (N'dbo.ufn_FindReports', N'TF') IS NOT NULL
    DROP FUNCTION dbo.ufn_FindReports;
GO
CREATE FUNCTION dbo.ufn_FindReports (@InEmpID INTEGER)
RETURNS @retFindReports TABLE
(
    EmployeeID int primary key NOT NULL,
    FirstName nvarchar(255) NOT NULL,
    LastName nvarchar(255) NOT NULL,
    JobTitle nvarchar(50) NOT NULL,
    RecursionLevel int NOT NULL
)
--Returns a result set that lists all the employees who report to the
--specific employee directly or indirectly.*/
AS
BEGIN
WITH EMP_cte(EmployeeID, OrganizationNode, FirstName, LastName, JobTitle, RecursionLevel) -- CTE name and columns
    AS (
        SELECT e.BusinessEntityID, e.OrganizationNode, p.FirstName, p.LastName, e.JobTitle, 0 -- Get the initial list of Employees for Manager n
        FROM HumanResources.Employee e
INNER JOIN Person.Person p
ON p.BusinessEntityID = e.BusinessEntityID
        WHERE e.BusinessEntityID = @InEmpID
        UNION ALL
        SELECT e.BusinessEntityID, e.OrganizationNode, p.FirstName, p.LastName, e.JobTitle, RecursionLevel + 1 -- Join recursive member to anchor
        FROM HumanResources.Employee e
            INNER JOIN EMP_cte
            ON e.OrganizationNode.GetAncestor(1) = EMP_cte.OrganizationNode
INNER JOIN Person.Person p
ON p.BusinessEntityID = e.BusinessEntityID
        )
-- copy the required columns to the result of the function
   INSERT @retFindReports
   SELECT EmployeeID, FirstName, LastName, JobTitle, RecursionLevel
   FROM EMP_cte
   RETURN
END;
GO
```

The following example invokes the function and specifies employee ID 1.

```sql
SELECT EmployeeID, FirstName, LastName, JobTitle, RecursionLevel
FROM dbo.ufn_FindReports(1);
```

For more information and examples of inline table-valued functions (inline TVFs) and multi-statement table-valued functions (MSTVFs), see [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md).

## Best practices

If a user-defined function (UDF) isn't created with the `SCHEMABINDING` clause, changes that are made to underlying objects can affect the definition of the function and produce unexpected results when it's invoked. We recommend that you implement one of the following methods to ensure that the function doesn't become outdated because of changes to its underlying objects:

- Specify the `WITH SCHEMABINDING` clause when you're creating the UDF. This ensures that the objects referenced in the function definition can't be modified unless the function is also modified.

- Execute the [sp_refreshsqlmodule](../../relational-databases/system-stored-procedures/sp-refreshsqlmodule-transact-sql.md) stored procedure after modifying any object that is specified in the definition of the UDF.

If creating a UDF that doesn't access data, specify the `SCHEMABINDING` option. This will prevent the query optimizer from generating unnecessary spool operators for query plans involving these UDFs. For more information on spools, see [Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md). For more information on creating a schema bound function, see [Schema-bound functions](../../relational-databases/user-defined-functions/user-defined-functions.md#SchemaBound).

Joining to an MSTVF in a `FROM` clause is possible, but can result in poor performance. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is unable to use all the optimized techniques on some statements that can be included in an MSTVF, resulting in a suboptimal query plan. To obtain the best possible performance, whenever possible use joins between base tables instead of functions.

MSTVFs have a fixed cardinality guess of 100 starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], and 1 for earlier [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions.

Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], optimizing an execution plan that uses MSTVFs can use interleaved execution, which results in using actual cardinality instead of the above heuristics.

For more information, see [Interleaved execution for multi-statement table valued functions](../../relational-databases/performance/intelligent-query-processing-details.md#interleaved-execution-for-mstvfs).

ANSI_WARNINGS isn't honored when you pass parameters in a stored procedure, user-defined function, or when you declare and set variables in a batch statement. For example, if a variable is defined as **char(3)**, and then set to a value larger than three characters, the data is truncated to the defined size and the `INSERT` or `UPDATE` statement succeeds.

## See also

- [User-Defined Functions](../../relational-databases/user-defined-functions/user-defined-functions.md)
- [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md)
- [ALTER FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-function-transact-sql.md)
- [DROP FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-function-transact-sql.md)
- [DROP PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-partition-function-transact-sql.md)
