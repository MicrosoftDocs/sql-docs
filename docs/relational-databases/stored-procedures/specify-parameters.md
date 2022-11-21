---
title: "Specify Parameters | Microsoft Docs"
description: Learn how to pass values into parameters and about how each of the parameter attributes is used during a procedure call.
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "parameters [SQL Server], stored procedures"
  - "stored procedures [SQL Server], parameters"
  - "output parameters [SQL Server]"
  - "input parameters [SQL Server]"
ms.assetid: 902314fe-5f9c-4d0d-a0b7-27e67c9c70ec
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specify Parameters
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  By specifying procedure parameters, calling programs are able to pass values into the body of the procedure. Those values can be used for a variety of purposes during procedure execution. Procedure parameters can also return values to the calling program if the parameter is marked as an OUTPUT parameter.  
  
 A procedure can have a maximum of 2100 parameters; each assigned a name, data type, and direction. Optionally, parameters can be assigned default values.  
  
 The following section provides information about passing values into parameters and about how each of the parameter attributes is used during a procedure call.  
  
## Passing Values into Parameters  
 The parameter values supplied with a procedure call must be constants or a variable; a function name cannot be used as a parameter value. Variables can be user-defined or system variables such as \@\@spid.  
  
 The following examples demonstrate passing parameter values to the procedure `uspGetWhereUsedProductID`. They illustrate how to pass parameters as constants and variables and also how to use a variable to pass the value of a function.  
  
```  
USE AdventureWorks2012;  
GO  
-- Passing values as constants.  
EXEC dbo.uspGetWhereUsedProductID 819, '20050225';  
GO  
-- Passing values as variables.  
DECLARE @ProductID int, @CheckDate datetime;  
SET @ProductID = 819;  
SET @CheckDate = '20050225';  
EXEC dbo.uspGetWhereUsedProductID @ProductID, @CheckDate;  
GO  
-- Try to use a function as a parameter value.  
-- This produces an error message.  
EXEC dbo.uspGetWhereUsedProductID 819, GETDATE();  
GO  
-- Passing the function value as a variable.  
DECLARE @CheckDate datetime;  
SET @CheckDate = GETDATE();  
EXEC dbo.uspGetWhereUsedProductID 819, @CheckDate;  
GO  
```  
  
## Specifying Parameter Names  
 When creating a procedure and declaring a parameter name, the parameter name must begin with a single \@ character and must be unique in the scope of the procedure.  
  
 Explicitly naming the parameters and assigning the appropriate values to each parameter in a procedure call allows the parameters to be supplied in any order. For example, if the procedure **my_proc** expects three parameters named **\@first**, **\@second**, and **\@third**, the values passed to the procedure can be assigned to the parameter names, such as: `EXECUTE my_proc @second = 2, @first = 1, @third = 3;`  
  
> [!NOTE]  
>  If one parameter value is supplied in the form **\@parameter =**_value_, all subsequent parameters must be supplied in this manner. If the parameter values are not passed in the form **\@parameter =**_value_, the values must be supplied in the identical order (left to right) as the parameters are listed in the CREATE PROCEDURE statement.  
  
> [!WARNING]  
>  Any parameter passed in the form **\@parameter =**_value_ with the parameter misspelled, will cause [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to generate an error and prevent procedure execution.  
  
## Specifying Parameter Data Types  
 Parameters must be defined with a data type when they are declared in a CREATE PROCEDURE statement. The data type of a parameter determines the type and range of values that are accepted for the parameter when the procedure is called. For example, if you define a parameter with a **tinyint** data type, only numeric values ranging from 0 to 255 are accepted when passed into that parameter. An error is returned if a procedure is executed with a value incompatible with the data type.  
  
## Specifying Parameter Default Values  
 A parameter is considered optional if the parameter has a default value specified when it is declared. It is not necessary to provide a value for an optional parameter in a procedure call.  
  
 The default value of a parameter is used when:  
  
-   No value for the parameter is specified in the procedure call.  
  
-   The DEFAULT keyword is specified as the value in the procedure call.  
  
> [!NOTE]  
>  If the default value is a character string that contains embedded blanks or punctuation, or if it starts with a number (for example, 6xxx), it must be enclosed in single, straight quotation marks.  

> [!NOTE] 
> Default parameters are not supported in Azure Synapse Analytics or Parallel Data Warehouse. 
  
 If no value can be specified appropriately as a default for the parameter, specify NULL as the default. It is a good idea to have the procedure return a customized message if the procedure is executed without a value for the parameter.  
  
 The following example creates the `uspGetSalesYTD` procedure with one input parameter, `@SalesPerson`. NULL is assigned as the default value for the parameter and is used in error handling statements to return a custom error message for cases when the procedure is executed without a value for the `@SalesPerson` parameter.  
  
```  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID('Sales.uspGetSalesYTD', 'P') IS NOT NULL  
    DROP PROCEDURE Sales.uspGetSalesYTD;  
GO  
CREATE PROCEDURE Sales.uspGetSalesYTD  
@SalesPerson nvarchar(50) = NULL  -- NULL default value  
AS   
    SET NOCOUNT ON;   
  
-- Validate the @SalesPerson parameter.  
IF @SalesPerson IS NULL  
BEGIN  
   PRINT 'ERROR: You must specify the last name of the sales person.'  
   RETURN  
END  
-- Get the sales for the specified sales person and   
-- assign it to the output parameter.  
SELECT SalesYTD  
FROM Sales.SalesPerson AS sp  
JOIN HumanResources.vEmployee AS e ON e.BusinessEntityID = sp.BusinessEntityID  
WHERE LastName = @SalesPerson;  
RETURN  
GO  
  
```  
  
 The following example executes the procedure. The first statement executes the procedure without specifying an input value. This causes the error handling statements in the procedure to return the custom error message. The second statement supplies an input value and returns the expected result set.  
  
```  
-- Run the procedure without specifying an input value.  
EXEC Sales.uspGetSalesYTD;  
GO  
-- Run the procedure with an input value.  
EXEC Sales.uspGetSalesYTD N'Blythe';  
GO  
```  
  
 Although parameters for which defaults have been supplied can be omitted, the list of parameters can only be truncated. For example, if a procedure has five parameters, both the fourth and the fifth parameters can be omitted. However the fourth parameter cannot be skipped as long as the fifth parameter is included, unless the parameters are supplied in the form **\@parameter =**_value_.  
  
## Specifying Parameter Direction  
 The direction of a parameter is either input, a value is passed into the body of the procedure, or output, the procedure returns a value to the calling program. The default is an input parameter.  
  
 To specify an output parameter, the OUTPUT keyword must be specified in the definition of the parameter in the CREATE PROCEDURE statement. The procedure returns the current value of the output parameter to the calling program when the procedure exits. The calling program must also use the OUTPUT keyword when executing the procedure to save the parameter's value in a variable that can be used in the calling program.  
  
 The following example creates the `Production.usp_GetList` procedure, which returns a list of products that have prices that do not exceed a specified amount. The example shows using multiple SELECT statements and multiple OUTPUT parameters. OUTPUT parameters allow an external procedure, a batch, or more than one [!INCLUDE[tsql](../../includes/tsql-md.md)] statement to access a value set during the procedure execution.  
  
```  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID ( 'Production.uspGetList', 'P' ) IS NOT NULL   
    DROP PROCEDURE Production.uspGetList;  
GO  
CREATE PROCEDURE Production.uspGetList @Product varchar(40)   
    , @MaxPrice money   
    , @ComparePrice money OUTPUT  
    , @ListPrice money OUT  
AS  
    SET NOCOUNT ON;  
    SELECT p.[Name] AS Product, p.ListPrice AS 'List Price'  
    FROM Production.Product AS p  
    JOIN Production.ProductSubcategory AS s   
      ON p.ProductSubcategoryID = s.ProductSubcategoryID  
    WHERE s.[Name] LIKE @Product AND p.ListPrice < @MaxPrice;  
-- Populate the output variable @ListPprice.  
SET @ListPrice = (SELECT MAX(p.ListPrice)  
        FROM Production.Product AS p  
        JOIN  Production.ProductSubcategory AS s   
          ON p.ProductSubcategoryID = s.ProductSubcategoryID  
        WHERE s.[Name] LIKE @Product AND p.ListPrice < @MaxPrice);  
-- Populate the output variable @compareprice.  
SET @ComparePrice = @MaxPrice;  
GO  
  
```  
  
 Execute `usp_GetList` to return a list of [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)] products (Bikes) that cost less than $700. The OUTPUT parameters **\@cost** and **\@compareprices** are used with control-of-flow language to return a message in the **Messages** window.  
  
> [!NOTE]  
>  The OUTPUT variable must be defined during the procedure creation and also during the use of the variable. The parameter name and variable name do not have to match. However, the data type and parameter positioning must match (unless **\@listprice=** _variable_ is used).  
  
```  
DECLARE @ComparePrice money, @Cost money ;  
EXECUTE Production.uspGetList '%Bikes%', 700,   
    @ComparePrice OUT,   
    @Cost OUTPUT  
IF @Cost <= @ComparePrice   
BEGIN  
    PRINT 'These products can be purchased for less than   
    $'+RTRIM(CAST(@ComparePrice AS varchar(20)))+'.'  
END  
ELSE  
    PRINT 'The prices for all products in this category exceed   
    $'+ RTRIM(CAST(@ComparePrice AS varchar(20)))+'.';  
  
```  
  
 Here is the partial result set:  
  
```  
Product                                            List Price  
-------------------------------------------------- ------------------  
Road-750 Black, 58                                 539.99  
Mountain-500 Silver, 40                            564.99  
Mountain-500 Silver, 42                            564.99  
...  
Road-750 Black, 48                                 539.99  
Road-750 Black, 52                                 539.99  
  
(14 row(s) affected)  
  
These items can be purchased for less than $700.00.  
```  
  
## See Also  
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)  
  
  
