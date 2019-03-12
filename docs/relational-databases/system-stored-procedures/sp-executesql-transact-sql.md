---
title: "sp_executesql (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_executesql"
  - "sp_executesql_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_executesql"
  - "dynamic SQL"
ms.assetid: a8d68d72-0f4d-4ecb-ae86-1235b962f646
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_executesql (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Executes a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch that can be reused many times, or one that has been built dynamically. The [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch can contain embedded parameters.  
  
> [!IMPORTANT]  
>  Run time-compiled [!INCLUDE[tsql](../../includes/tsql-md.md)] statements can expose applications to malicious attacks.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
sp_executesql [ @stmt = ] statement  
[   
  { , [ @params = ] N'@parameter_name data_type [ OUT | OUTPUT ][ ,...n ]' }   
     { , [ @param1 = ] 'value1' [ ,...n ] }  
]  
```  
  
## Arguments  
 [ \@stmt= ] *statement*  
 Is a Unicode string that contains a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch. \@stmt must be either a Unicode constant or a Unicode variable. More complex Unicode expressions, such as concatenating two strings with the + operator, are not allowed. Character constants are not allowed. If a Unicode constant is specified, it must be prefixed with an **N**. For example, the Unicode constant **N'sp_who'** is valid, but the character constant **'sp_who'** is not. The size of the string is limited only by available database server memory. On 64-bit servers, the size of the string is limited to 2 GB, the maximum size of **nvarchar(max)**.  
  
> [!NOTE]  
>  \@stmt can contain parameters having the same form as a variable name, for example: `N'SELECT * FROM HumanResources.Employee WHERE EmployeeID = @IDParameter'`  
  
 Each parameter included in \@stmt must have a corresponding entry in both the \@params parameter definition list and the parameter values list.  
  
 [ \@params= ] N'\@*parameter_name**data_type* [ ,... *n* ] '  
 Is one string that contains the definitions of all parameters that have been embedded in \@stmt. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates additional parameter definitions. Every parameter specified in \@stmt must be defined in \@params. If the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch in \@stmt does not contain parameters, \@params is not required. The default value for this parameter is NULL.  
  
 [ \@param1= ] '*value1*'  
 Is a value for the first parameter that is defined in the parameter string. The value can be a Unicode constant or a Unicode variable. There must be a parameter value supplied for every parameter included in \@stmt. The values are not required when the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch in \@stmt has no parameters.  
  
 [ OUT | OUTPUT ]  
 Indicates that the parameter is an output parameter. **text**, **ntext**, and **image** parameters can be used as OUTPUT parameters, unless the procedure is a common language runtime (CLR) procedure. An output parameter that uses the OUTPUT keyword can be a cursor placeholder, unless the procedure is a CLR procedure.  
  
 *n*  
 Is a placeholder for the values of additional parameters. Values can only be constants or variables. Values cannot be more complex expressions such as functions, or expressions built by using operators.  
  
## Return Code Values  
 0 (success) or non-zero (failure)  
  
## Result Sets  
 Returns the result sets from all the SQL statements built into the SQL string.  
  
## Remarks  
 sp_executesql parameters must be entered in the specific order as described in the "Syntax" section earlier in this topic. If the parameters are entered out of order, an error message will occur.  
  
 sp_executesql has the same behavior as EXECUTE with regard to batches, the scope of names, and database context. The [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch in the sp_executesql \@stmt parameter is not compiled until the sp_executesql statement is executed. The contents of \@stmt are then compiled and executed as an execution plan separate from the execution plan of the batch that called sp_executesql. The sp_executesql batch cannot reference variables declared in the batch that calls sp_executesql. Local cursors or variables in the sp_executesql batch are not visible to the batch that calls sp_executesql. Changes in database context last only to the end of the sp_executesql statement.  
  
 sp_executesql can be used instead of stored procedures to execute a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement many times when the change in parameter values to the statement is the only variation. Because the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement itself remains constant and only the parameter values change, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer is likely to reuse the execution plan it generates for the first execution.  
  
> [!NOTE]  
>  To improve performance use fully qualified object names in the statement string.  
  
 sp_executesql supports the setting of parameter values separately from the [!INCLUDE[tsql](../../includes/tsql-md.md)] string as shown in the following example.  
  
```  
DECLARE @IntVariable int;  
DECLARE @SQLString nvarchar(500);  
DECLARE @ParmDefinition nvarchar(500);  
  
/* Build the SQL string one time.*/  
SET @SQLString =  
     N'SELECT BusinessEntityID, NationalIDNumber, JobTitle, LoginID  
       FROM AdventureWorks2012.HumanResources.Employee   
       WHERE BusinessEntityID = @BusinessEntityID';  
SET @ParmDefinition = N'@BusinessEntityID tinyint';  
/* Execute the string with the first parameter value. */  
SET @IntVariable = 197;  
EXECUTE sp_executesql @SQLString, @ParmDefinition,  
                      @BusinessEntityID = @IntVariable;  
/* Execute the same string with the second parameter value. */  
SET @IntVariable = 109;  
EXECUTE sp_executesql @SQLString, @ParmDefinition,  
                      @BusinessEntityID = @IntVariable;  
```  
  
 Output parameters can also be used with sp_executesql. The following example retrieves a job title from the `AdventureWorks2012.HumanResources.Employee` table and returns it in the output parameter `@max_title`.  
  
```  
DECLARE @IntVariable int;  
DECLARE @SQLString nvarchar(500);  
DECLARE @ParmDefinition nvarchar(500);  
DECLARE @max_title varchar(30);  
  
SET @IntVariable = 197;  
SET @SQLString = N'SELECT @max_titleOUT = max(JobTitle)   
   FROM AdventureWorks2012.HumanResources.Employee  
   WHERE BusinessEntityID = @level';  
SET @ParmDefinition = N'@level tinyint, @max_titleOUT varchar(30) OUTPUT';  
  
EXECUTE sp_executesql @SQLString, @ParmDefinition, @level = @IntVariable, @max_titleOUT=@max_title OUTPUT;  
SELECT @max_title;  
```  
  
 Being able to substitute parameters in sp_executesql offers the following advantages to using the EXECUTE statement to execute a string:  
  
-   Because the actual text of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement in the sp_executesql string does not change between executions, the query optimizer will probably match the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement in the second execution with the execution plan generated for the first execution. Therefore, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not have to compile the second statement.  
  
-   The [!INCLUDE[tsql](../../includes/tsql-md.md)] string is built only one time.  
  
-   The integer parameter is specified in its native format. Casting to Unicode is not required.  
  
## Permissions  
 Requires membership in the public role.  
  
## Examples  
  
### A. Executing a simple SELECT statement  
 The following example creates and executes a simple `SELECT` statement that contains an embedded parameter named `@level`.  
  
```  
EXECUTE sp_executesql   
          N'SELECT * FROM AdventureWorks2012.HumanResources.Employee   
          WHERE BusinessEntityID = @level',  
          N'@level tinyint',  
          @level = 109;  
```  
  
### B. Executing a dynamically built string  
 The following example shows using `sp_executesql` to execute a dynamically built string. The example stored procedure is used to insert data into a set of tables that are used to partition sales data for a year. There is one table for each month of the year that has the following format:  
  
```  
CREATE TABLE May1998Sales  
    (OrderID int PRIMARY KEY,  
    CustomerID int NOT NULL,  
    OrderDate  datetime NULL  
        CHECK (DATEPART(yy, OrderDate) = 1998),  
    OrderMonth int  
        CHECK (OrderMonth = 5),  
    DeliveryDate datetime  NULL,  
        CHECK (DATEPART(mm, OrderDate) = OrderMonth)  
    )  
```  
  
 This sample stored procedure dynamically builds and executes an `INSERT` statement to insert new orders into the correct table. The example uses the order date to build the name of the table that should contain the data, and then incorporates that name into an `INSERT` statement.  
  
> [!NOTE]  
>  This is a simple example for sp_executesql. The example does not contain error checking and does not include checks for business rules, such as guaranteeing that order numbers are not duplicated between tables.  
  
```  
CREATE PROCEDURE InsertSales @PrmOrderID INT, @PrmCustomerID INT,  
                 @PrmOrderDate DATETIME, @PrmDeliveryDate DATETIME  
AS  
DECLARE @InsertString NVARCHAR(500)  
DECLARE @OrderMonth INT  
  
-- Build the INSERT statement.  
SET @InsertString = 'INSERT INTO ' +  
       /* Build the name of the table. */  
       SUBSTRING( DATENAME(mm, @PrmOrderDate), 1, 3) +  
       CAST(DATEPART(yy, @PrmOrderDate) AS CHAR(4) ) +  
       'Sales' +  
       /* Build a VALUES clause. */  
       ' VALUES (@InsOrderID, @InsCustID, @InsOrdDate,' +  
       ' @InsOrdMonth, @InsDelDate)'  
  
/* Set the value to use for the order month because  
   functions are not allowed in the sp_executesql parameter  
   list. */  
SET @OrderMonth = DATEPART(mm, @PrmOrderDate)  
  
EXEC sp_executesql @InsertString,  
     N'@InsOrderID INT, @InsCustID INT, @InsOrdDate DATETIME,  
       @InsOrdMonth INT, @InsDelDate DATETIME',  
     @PrmOrderID, @PrmCustomerID, @PrmOrderDate,  
     @OrderMonth, @PrmDeliveryDate  
  
GO  
```  
  
 Using sp_executesql in this procedure is more efficient than using EXECUTE to execute a string. When sp_executesql is used, there are only 12 versions of the INSERT string that are generated, one for each monthly table. With EXECUTE, each INSERT string is unique because the parameter values are different. Although both methods generate the same number of batches, the similarity of the INSERT strings generated by sp_executesql makes it more likely that the query optimizer will reuse execution plans.  
  
### C. Using the OUTPUT Parameter  
 The following example uses an `OUTPUT` parameter to store the result set generated by the `SELECT` statement in the `@SQLString` parameter.Two `SELECT` statements are then executed that use the value of the `OUTPUT` parameter.  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @SQLString nvarchar(500);  
DECLARE @ParmDefinition nvarchar(500);  
DECLARE @SalesOrderNumber nvarchar(25);  
DECLARE @IntVariable int;  
SET @SQLString = N'SELECT @SalesOrderOUT = MAX(SalesOrderNumber)  
    FROM Sales.SalesOrderHeader  
    WHERE CustomerID = @CustomerID';  
SET @ParmDefinition = N'@CustomerID int,  
    @SalesOrderOUT nvarchar(25) OUTPUT';  
SET @IntVariable = 22276;  
EXECUTE sp_executesql  
    @SQLString  
    ,@ParmDefinition  
    ,@CustomerID = @IntVariable  
    ,@SalesOrderOUT = @SalesOrderNumber OUTPUT;  
-- This SELECT statement returns the value of the OUTPUT parameter.  
SELECT @SalesOrderNumber;  
-- This SELECT statement uses the value of the OUTPUT parameter in  
-- the WHERE clause.  
SELECT OrderDate, TotalDue  
FROM Sales.SalesOrderHeader  
WHERE SalesOrderNumber = @SalesOrderNumber;  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### D. Executing a simple SELECT statement  
 The following example creates and executes a simple `SELECT` statement that contains an embedded parameter named `@level`.  
  
```  
-- Uses AdventureWorks  
  
EXECUTE sp_executesql   
          N'SELECT * FROM AdventureWorksPDW2012.dbo.DimEmployee   
          WHERE EmployeeKey = @level',  
          N'@level tinyint',  
          @level = 109;  
```  
  
 For additional examples, see [sp_executesql (Transact-SQL)](https://msdn.microsoft.com/library/ms188001.aspx).  
  
## See Also  
 [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
