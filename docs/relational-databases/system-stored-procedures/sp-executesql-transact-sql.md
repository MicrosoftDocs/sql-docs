---
title: "sp_executesql (Transact-SQL)"
description: sp_executesql executes a Transact-SQL statement or batch that can be reused many times, or one that is built dynamically.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_executesql"
  - "sp_executesql_TSQL"
helpviewer_keywords:
  - "sp_executesql"
  - "dynamic SQL"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sp_executesql (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Executes a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch that can be reused many times, or one that is built dynamically. The [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch can contain embedded parameters.

> [!CAUTION]  
> Runtime-compiled [!INCLUDE [tsql](../../includes/tsql-md.md)] statements can expose applications to malicious attacks. You should parameterize your queries when using `sp_executesql`. For more information, see [SQL injection](../security/sql-injection.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], and [!INCLUDE [ssazurepdw_md](../../includes/ssazurepdw_md.md)].

```syntaxsql
sp_executesql [ @stmt = ] N'statement'
[
    [ , [ @params = ] N'@parameter_name data_type [ { OUT | OUTPUT } ] [ , ...n ]' ]
    [ , [ @param1 = ] 'value1' [ , ...n ] ]
]
```

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

## Arguments

#### [ @stmt = ] N'*statement*'

A Unicode string that contains a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch. *@stmt* must be either a Unicode constant or a Unicode variable. More complex Unicode expressions, such as concatenating two strings with the `+` operator, aren't allowed. Character constants aren't allowed. Unicode constants must be prefixed with an `N`. For example, the Unicode constant `N'sp_who'` is valid, but the character constant `'sp_who'` isn't. The size of the string is limited only by available database server memory. On 64-bit servers, the size of the string is limited to 2 GB, the maximum size of **nvarchar(max)**.

*@stmt* can contain parameters having the same form as a variable name. For example:

```sql
N'SELECT * FROM HumanResources.Employee WHERE EmployeeID = @IDParameter';
```

Each parameter included in *@stmt* must have a corresponding entry in both the *@params* parameter definition list and the parameter values list.

#### [ @params = ] N'*@parameter_name* *data_type* [ ,... *n* ]'

A string that contains the definitions of all parameters that are embedded in *@stmt*. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates more parameter definitions. Every parameter specified in *@stmt* must be defined in *@params*. If the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch in *@stmt* doesn't contain parameters, *@params* isn't required. The default value for this parameter is `NULL`.

#### [ @param1 = ] '*value1*'

A value for the first parameter that is defined in the parameter string. The value can be a Unicode constant or a Unicode variable. There must be a parameter value supplied for every parameter included in *@stmt*. The values aren't required when the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch in *@stmt* has no parameters.

#### { OUT | OUTPUT }

Indicates that the parameter is an output parameter. **text**, **ntext**, and **image** parameters can be used as `OUTPUT` parameters, unless the procedure is a common language runtime (CLR) procedure. An output parameter that uses the `OUTPUT` keyword can be a cursor placeholder, unless the procedure is a CLR procedure.

#### [ *... n* ]

A placeholder for the values of extra parameters. Values can only be constants or variables. Values can't be more complex expressions such as functions, or expressions built by using operators.

## Return code values

`0` (success) or non-zero (failure).

## Result set

Returns the result sets from all the SQL statements built into the SQL string.

## Remarks

`sp_executesql` parameters must be entered in the specific order as described in the [Syntax](#syntax) section earlier in this article. If the parameters are entered out of order, an error message occurs.

`sp_executesql` has the same behavior as `EXECUTE` regarding batches, the scope of names, and database context. The [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch in the `sp_executesql` *@stmt* parameter isn't compiled until the `sp_executesql` statement is executed. The contents of *@stmt* are then compiled and executed as an execution plan separate from the execution plan of the batch that called `sp_executesql`. The `sp_executesql` batch can't reference variables declared in the batch that calls `sp_executesql`. Local cursors or variables in the `sp_executesql` batch aren't visible to the batch that calls `sp_executesql`. Changes in database context last only to the end of the `sp_executesql` statement.

`sp_executesql` can be used instead of stored procedures to execute a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement many times when the change in parameter values to the statement is the only variation. Because the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement itself remains constant and only the parameter values change, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer is likely to reuse the execution plan it generates for the first execution. In this scenario, performance is equivalent to that of a stored procedure.

> [!NOTE]  
> To improve performance, use fully qualified object names in the statement string.

`sp_executesql` supports the setting of parameter values separately from the [!INCLUDE [tsql](../../includes/tsql-md.md)] string, as shown in the following example.

```sql
DECLARE @IntVariable INT;
DECLARE @SQLString NVARCHAR(500);
DECLARE @ParmDefinition NVARCHAR(500);

/* Build the SQL string once */
SET @SQLString = N'SELECT BusinessEntityID, NationalIDNumber, JobTitle, LoginID
       FROM AdventureWorks2022.HumanResources.Employee
       WHERE BusinessEntityID = @BusinessEntityID';
SET @ParmDefinition = N'@BusinessEntityID tinyint';
/* Execute the string with the first parameter value. */
SET @IntVariable = 197;

EXECUTE sp_executesql @SQLString,
    @ParmDefinition,
    @BusinessEntityID = @IntVariable;

/* Execute the same string with the second parameter value. */
SET @IntVariable = 109;

EXECUTE sp_executesql @SQLString,
    @ParmDefinition,
    @BusinessEntityID = @IntVariable;
```

Output parameters can also be used with `sp_executesql`. The following example retrieves a job title from the `HumanResources.Employee` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database, and returns it in the output parameter `@max_title`.

```sql
DECLARE @IntVariable INT;
DECLARE @SQLString NVARCHAR(500);
DECLARE @ParmDefinition NVARCHAR(500);
DECLARE @max_title VARCHAR(30);

SET @IntVariable = 197;
SET @SQLString = N'SELECT @max_titleOUT = max(JobTitle)
   FROM AdventureWorks2022.HumanResources.Employee
   WHERE BusinessEntityID = @level';
SET @ParmDefinition = N'@level TINYINT, @max_titleOUT VARCHAR(30) OUTPUT';

EXECUTE sp_executesql @SQLString,
    @ParmDefinition,
    @level = @IntVariable,
    @max_titleOUT = @max_title OUTPUT;

SELECT @max_title;
```

Being able to substitute parameters in `sp_executesql` offers the following advantages over using the `EXECUTE` statement to execute a string:

- Because the actual text of the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement in the `sp_executesql` string doesn't change between executions, the query optimizer probably matches the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement in the second execution with the execution plan generated for the first execution. Therefore, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't have to compile the second statement.

- The [!INCLUDE [tsql](../../includes/tsql-md.md)] string is built only once.

- The integer parameter is specified in its native format. Casting to Unicode isn't required.

## Permissions

Requires membership in the **public** role.

## Examples

### A. Execute a SELECT statement

The following example creates and executes a `SELECT` statement that contains an embedded parameter named `@level`.

```sql
EXECUTE sp_executesql
    N'SELECT * FROM AdventureWorks2022.HumanResources.Employee
    WHERE BusinessEntityID = @level',
    N'@level TINYINT',
    @level = 109;
```

### B. Execute a dynamically built string

The following example shows using `sp_executesql` to execute a dynamically built string. The example stored procedure is used to insert data into a set of tables that are used to partition sales data for a year. There's one table for each month of the year that has the following format:

```sql
CREATE TABLE May1998Sales (
    OrderID INT PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME NULL CHECK (DATEPART(yy, OrderDate) = 1998),
    OrderMonth INT CHECK (OrderMonth = 5),
    DeliveryDate DATETIME NULL,
    CHECK (DATEPART(mm, OrderDate) = OrderMonth)
);
```

This sample stored procedure dynamically builds and executes an `INSERT` statement to insert new orders into the correct table. The example uses the order date to build the name of the table that should contain the data, and then incorporates that name into an `INSERT` statement.

> [!NOTE]  
> This is a basic example for `sp_executesql`. The example doesn't contain error checking, and doesn't include checks for business rules, such as guaranteeing that order numbers aren't duplicated between tables.

```sql
CREATE PROCEDURE InsertSales @PrmOrderID INT,
    @PrmCustomerID INT,
    @PrmOrderDate DATETIME,
    @PrmDeliveryDate DATETIME
AS
DECLARE @InsertString NVARCHAR(500);
DECLARE @OrderMonth INT;

-- Build the INSERT statement.
SET @InsertString = 'INSERT INTO ' +
    /* Build the name of the table. */
    SUBSTRING(DATENAME(mm, @PrmOrderDate), 1, 3) +
    CAST(DATEPART(yy, @PrmOrderDate) AS CHAR(4)) + 'Sales' +
    /* Build a VALUES clause. */
    ' VALUES (@InsOrderID, @InsCustID, @InsOrdDate,' +
    ' @InsOrdMonth, @InsDelDate)';

/* Set the value to use for the order month because
   functions are not allowed in the sp_executesql parameter
   list. */
SET @OrderMonth = DATEPART(mm, @PrmOrderDate);

EXEC sp_executesql @InsertString,
    N'@InsOrderID INT, @InsCustID INT, @InsOrdDate DATETIME,
       @InsOrdMonth INT, @InsDelDate DATETIME',
    @PrmOrderID,
    @PrmCustomerID,
    @PrmOrderDate,
    @OrderMonth,
    @PrmDeliveryDate;
GO
```

Using `sp_executesql` in this procedure is more efficient than using `EXECUTE` to execute a string. With `EXECUTE`, each `INSERT` string is unique because the parameter values are different. When `sp_executesql` is used, there is only one `INSERT` string generated for each language, month and year combination. (`DATENAME()` returns different values depending on the value of `LANGUAGE` in the session that is executing the `InsertSales` procedure.) Although both methods generate the same number of batches, the similarity of the `INSERT` strings generated by `sp_executesql` makes it more likely that the query optimizer reuses execution plans.

### C. Use the OUTPUT parameter

The following example uses an `OUTPUT` parameter to store the result set generated by the `SELECT` statement in the `@SQLString` parameter. Two `SELECT` statements are then executed that use the value of the `OUTPUT` parameter.

```sql
USE AdventureWorks2022;
GO

DECLARE @SQLString NVARCHAR(500);
DECLARE @ParmDefinition NVARCHAR(500);
DECLARE @SalesOrderNumber NVARCHAR(25);
DECLARE @IntVariable INT;

SET @SQLString = N'SELECT @SalesOrderOUT = MAX(SalesOrderNumber)
    FROM Sales.SalesOrderHeader
    WHERE CustomerID = @CustomerID';
SET @ParmDefinition = N'@CustomerID INT,
    @SalesOrderOUT NVARCHAR(25) OUTPUT';
SET @IntVariable = 22276;

EXECUTE sp_executesql @SQLString,
    @ParmDefinition,
    @CustomerID = @IntVariable,
    @SalesOrderOUT = @SalesOrderNumber OUTPUT;

-- This SELECT statement returns the value of the OUTPUT parameter.
SELECT @SalesOrderNumber;

-- This SELECT statement uses the value of the OUTPUT parameter in
-- the WHERE clause.
SELECT OrderDate,
    TotalDue
FROM Sales.SalesOrderHeader
WHERE SalesOrderNumber = @SalesOrderNumber;
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

### D. Execute a SELECT statement

The following example creates and executes a `SELECT` statement that contains an embedded parameter named `@level`.

```sql
EXECUTE sp_executesql
    N'SELECT * FROM AdventureWorksPDW2012.dbo.DimEmployee
    WHERE EmployeeKey = @level',
    N'@level TINYINT',
    @level = 109;
```

## Related content

- [EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
