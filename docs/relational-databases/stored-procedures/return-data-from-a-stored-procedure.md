---
title: "Return Data from a Stored Procedure | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.technology: stored-procedures
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "stored procedures [SQL Server], returning data"
  - "returning data from stored procedure"
ms.assetid: 7a428ffe-cd87-4f42-b3f1-d26aa8312bf7
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Return Data from a Stored Procedure
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  There are three ways of returning data from a procedure to a calling program: result sets, output parameters, and return codes. This topic provides information on the three approaches.  
  
  ## Returning Data Using Result Sets
 If you include a SELECT statement in the body of a stored procedure (but not a SELECT ... INTO or INSERT ... SELECT), the rows specified by the SELECT statement will be sent directly to the client.  For large result sets the stored procedure execution will not continue to the next statement until the result set has been completely sent to the client.  For small result sets the results will be spooled for return to the client and execution will continue.  If multiple such SELECT statements are run during the exeuction of the stored proceudre, multiple result sets will be sent to the client.  This behavior also applies to nested TSQL batches, nested stored procedures and top-level TSQL batches.
 
 
 ### Examples of Returning Data Using a Result Set 
  The following example shows a stored procedure that returns the LastName and SalesYTD values for all SalesPerson rows that also appear in the vEmployee view.
  
 ```sql
USE AdventureWorks2012;  
GO  
IF OBJECT_ID('Sales.uspGetEmployeeSalesYTD', 'P') IS NOT NULL  
    DROP PROCEDURE Sales.uspGetEmployeeSalesYTD;  
GO  
CREATE PROCEDURE Sales.uspGetEmployeeSalesYTD  
AS    
  
    SET NOCOUNT ON;  
    SELECT LastName, SalesYTD  
    FROM Sales.SalesPerson AS sp  
    JOIN HumanResources.vEmployee AS e ON e.BusinessEntityID = sp.BusinessEntityID  
    
RETURN  
GO  
  
```  

  
## Returning Data Using an Output Parameter  
 If you specify the OUTPUT keyword for a parameter in the procedure definition, the procedure can return the current value of the parameter to the calling program when the procedure exits. To save the value of the parameter in a variable that can be used in the calling program, the calling program must use the OUTPUT keyword when executing the procedure. For more information about what data types can be used as output parameters, see [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md).  
  
### Examples of Output Parameter  
 The following example shows a procedure with an input and an output parameter. The `@SalesPerson` parameter would receive an input value specified by the calling program. The SELECT statement uses the value passed into the input parameter to obtain the correct `SalesYTD` value. The SELECT statement also assigns the value to the `@SalesYTD` output parameter, which returns the value to the calling program when the procedure exits.  
  
```sql
USE AdventureWorks2012;  
GO  
IF OBJECT_ID('Sales.uspGetEmployeeSalesYTD', 'P') IS NOT NULL  
    DROP PROCEDURE Sales.uspGetEmployeeSalesYTD;  
GO  
CREATE PROCEDURE Sales.uspGetEmployeeSalesYTD  
@SalesPerson nvarchar(50),  
@SalesYTD money OUTPUT  
AS    
  
    SET NOCOUNT ON;  
    SELECT @SalesYTD = SalesYTD  
    FROM Sales.SalesPerson AS sp  
    JOIN HumanResources.vEmployee AS e ON e.BusinessEntityID = sp.BusinessEntityID  
    WHERE LastName = @SalesPerson;  
RETURN  
GO  
  
```  
  
 The following example calls the procedure created in the first example and saves the output value returned from the called procedure in the `@SalesYTD` variable, which is local to the calling program.  
  
```sql
-- Declare the variable to receive the output value of the procedure.  
DECLARE @SalesYTDBySalesPerson money;  
-- Execute the procedure specifying a last name for the input parameter  
-- and saving the output value in the variable @SalesYTDBySalesPerson  
EXECUTE Sales.uspGetEmployeeSalesYTD  
    N'Blythe', @SalesYTD = @SalesYTDBySalesPerson OUTPUT;  
-- Display the value returned by the procedure.  
PRINT 'Year-to-date sales for this employee is ' +   
    convert(varchar(10),@SalesYTDBySalesPerson);  
GO  
  
```  
  
 Input values can also be specified for OUTPUT parameters when the procedure is executed. This allows the procedure to receive a value from the calling program, change or perform operations with the value, and then return the new value to the calling program. In the previous example, the `@SalesYTDBySalesPerson` variable can be assigned a value before the program calls the `Sales.uspGetEmployeeSalesYTD` procedure. The execute statement would pass the `@SalesYTDBySalesPerson` variable value into the `@SalesYTD` OUTPUT parameter. Then in the procedure body, the value could be used for calculations that generate a new value. The new value would be passed back out of the procedure through the OUTPUT parameter, updating the value in the `@SalesYTDBySalesPerson` variable when the procedure exits. This is often referred to as "pass-by-reference capability."  
  
 If you specify OUTPUT for a parameter when you call a procedure and that parameter is not defined by using OUTPUT in the procedure definition, you get an error message. However, you can execute a procedure with output parameters and not specify OUTPUT when executing the procedure. No error is returned, but you cannot use the output value in the calling program.  
  
### Using the Cursor Data Type in OUTPUT Parameters  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] procedures can use the **cursor** data type only for OUTPUT parameters. If the **cursor** data type is specified for a parameter, both the VARYING and OUTPUT keywords must be specified for that parameter in the procedure definition. A parameter can be specified as only OUTPUT but if the VARYING keyword is specified in the parameter declaration, the data type must be **cursor** and the OUTPUT keyword must also be specified.  
  
> [!NOTE]  
>  The **cursor** data type cannot be bound to application variables through the database APIs such as OLE DB, ODBC, ADO, and DB-Library. Because OUTPUT parameters must be bound before an application can execute a procedure, procedures with **cursor** OUTPUT parameters cannot be called from the database APIs. These procedures can be called from [!INCLUDE[tsql](../../includes/tsql-md.md)] batches, procedures, or triggers only when the **cursor** OUTPUT variable is assigned to a [!INCLUDE[tsql](../../includes/tsql-md.md)] local **cursor** variable.  
  
### Rules for Cursor Output Parameters  
 The following rules pertain to **cursor** output parameters when the procedure is executed:  
  
-   For a forward-only cursor, the rows returned in the cursor's result set are only those rows at and beyond the position of the cursor at the conclusion of the procedure execution, for example:  
  
    -   A nonscrollable cursor is opened in a procedure on a result set named RS of 100 rows.  
  
    -   The procedure fetches the first 5 rows of result set RS.  
  
    -   The procedure returns to its caller.  
  
    -   The result set RS returned to the caller consists of rows from 6 through 100 of RS, and the cursor in the caller is positioned before the first row of RS.  
  
-   For a forward-only cursor, if the cursor is positioned before the first row when the procedure exits, the entire result set is returned to the calling batch, procedure, or trigger. When returned, the cursor position is set before the first row.  
  
-   For a forward-only cursor, if the cursor is positioned beyond the end of the last row when the procedure exits, an empty result set is returned to the calling batch, procedure, or trigger.  
  
    > [!NOTE]  
    >  An empty result set is not the same as a null value.  
  
-   For a scrollable cursor, all the rows in the result set are returned to the calling batch, procedure, or trigger when the procedure exits. When returned, the cursor position is left at the position of the last fetch executed in the procedure.  
  
-   For any type of cursor, if the cursor is closed, then a null value is passed back to the calling batch, procedure, or trigger. This will also be the case if a cursor is assigned to a parameter, but that cursor is never opened.  
  
    > [!NOTE]  
    >  The closed state matters only at return time. For example, it is valid to close a cursor part of the way through the procedure, to open it again later in the procedure, and return that cursor's result set to the calling batch, procedure, or trigger.  
  
### Examples of Cursor Output Parameters  
 In the following example, a procedure is created that specified an output parameter, `@currency_cursor` using the **cursor** data type. The procedure is then called in a batch.  
 
 First, create the procedure that declares and then opens a cursor on the Currency table.  
  
```sql
USE AdventureWorks2012;  
GO  
IF OBJECT_ID ( 'dbo.uspCurrencyCursor', 'P' ) IS NOT NULL  
    DROP PROCEDURE dbo.uspCurrencyCursor;  
GO  
CREATE PROCEDURE dbo.uspCurrencyCursor   
    @CurrencyCursor CURSOR VARYING OUTPUT  
AS  
    SET NOCOUNT ON;  
    SET @CurrencyCursor = CURSOR  
    FORWARD_ONLY STATIC FOR  
      SELECT CurrencyCode, Name  
      FROM Sales.Currency;  
    OPEN @CurrencyCursor;  
GO  
  
```  
  
 Next, execute a batch that declares a local cursor variable, executes the procedure to assign the cursor to the local variable, and then fetches the rows from the cursor.  
  
```sql
USE AdventureWorks2012;  
GO  
DECLARE @MyCursor CURSOR;  
EXEC dbo.uspCurrencyCursor @CurrencyCursor = @MyCursor OUTPUT;  
WHILE (@@FETCH_STATUS = 0)  
BEGIN;  
     FETCH NEXT FROM @MyCursor;  
END;  
CLOSE @MyCursor;  
DEALLOCATE @MyCursor;  
GO  
  
```  
  
## Returning Data Using a Return Code  
 A procedure can return an integer value called a return code to indicate the execution status of a procedure. You specify the return code for a procedure using the RETURN statement. As with OUTPUT parameters, you must save the return code in a variable when the procedure is executed in order to use the return code value in the calling program. For example, the assignment variable `@result` of data type **int** is used to store the return code from the procedure `my_proc`, such as:  
  
```sql
DECLARE @result int;  
EXECUTE @result = my_proc;  
```  
  
 Return codes are commonly used in control-of-flow blocks within procedures to set the return code value for each possible error situation. You can use the @@ERROR function after a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement to detect whether an error occurred during the execution of the statement.  Before the introduction of TRY/CATCH/THROW error handling in TSQL return codes were sometimes required to determine the success or failure of stored procedures.  Stored Procedures should always indicate failure with an error (generated with THROW/RAISERROR if necessary), and not rely on a return code to indicate the failure.  Also you should avoid using the return code to return application data.
  
### Examples of Return Codes  
 The following example shows the `usp_GetSalesYTD` procedure with error handling that sets special return code values for various errors. The following table shows the integer value that is assigned by the procedure to each possible error, and the corresponding meaning for each value.  
  
|Return code value|Meaning|  
|-----------------------|-------------|  
|0|Successful execution.|  
|1|Required parameter value is not specified.|  
|2|Specified parameter value is not valid.|  
|3|Error has occurred getting sales value.|  
|4|NULL sales value found for the salesperson.|  
  
```sql
USE AdventureWorks2012;  
GO  
IF OBJECT_ID('Sales.usp_GetSalesYTD', 'P') IS NOT NULL  
    DROP PROCEDURE Sales.usp_GetSalesYTD;  
GO  
CREATE PROCEDURE Sales.usp_GetSalesYTD  
@SalesPerson nvarchar(50) = NULL,  -- NULL default value  
@SalesYTD money = NULL OUTPUT  
AS    
  
-- Validate the @SalesPerson parameter.  
IF @SalesPerson IS NULL  
   BEGIN  
       PRINT 'ERROR: You must specify a last name for the sales person.'  
       RETURN(1)  
   END  
ELSE  
   BEGIN  
   -- Make sure the value is valid.  
   IF (SELECT COUNT(*) FROM HumanResources.vEmployee  
          WHERE LastName = @SalesPerson) = 0  
      RETURN(2)  
   END  
-- Get the sales for the specified name and   
-- assign it to the output parameter.  
SELECT @SalesYTD = SalesYTD   
FROM Sales.SalesPerson AS sp  
JOIN HumanResources.vEmployee AS e ON e.BusinessEntityID = sp.BusinessEntityID  
WHERE LastName = @SalesPerson;  
-- Check for SQL Server errors.  
IF @@ERROR <> 0   
   BEGIN  
      RETURN(3)  
   END  
ELSE  
   BEGIN  
   -- Check to see if the ytd_sales value is NULL.  
     IF @SalesYTD IS NULL  
       RETURN(4)   
     ELSE  
      -- SUCCESS!!  
        RETURN(0)  
   END  
-- Run the stored procedure without specifying an input value.  
EXEC Sales.usp_GetSalesYTD;  
GO  
-- Run the stored procedure with an input value.  
DECLARE @SalesYTDForSalesPerson money, @ret_code int;  
-- Execute the procedure specifying a last name for the input parameter  
-- and saving the output value in the variable @SalesYTD  
EXECUTE Sales.usp_GetSalesYTD  
    N'Blythe', @SalesYTD = @SalesYTDForSalesPerson OUTPUT;  
PRINT N'Year-to-date sales for this employee is ' +  
    CONVERT(varchar(10), @SalesYTDForSalesPerson);  
  
```  
  
 The following example creates a program to handle the return codes that are returned from the `usp_GetSalesYTD` procedure.  
  
```sql
-- Declare the variables to receive the output value and return code   
-- of the procedure.  
DECLARE @SalesYTDForSalesPerson money, @ret_code int;  
  
-- Execute the procedure with a title_id value  
-- and save the output value and return code in variables.  
EXECUTE @ret_code = Sales.usp_GetSalesYTD  
    N'Blythe', @SalesYTD = @SalesYTDForSalesPerson OUTPUT;  
--  Check the return codes.  
IF @ret_code = 0  
BEGIN  
   PRINT 'Procedure executed successfully'  
   -- Display the value returned by the procedure.  
   PRINT 'Year-to-date sales for this employee is ' + CONVERT(varchar(10),@SalesYTDForSalesPerson)  
END  
ELSE IF @ret_code = 1  
   PRINT 'ERROR: You must specify a last name for the sales person.'  
ELSE IF @ret_code = 2   
   PRINT 'ERROR: You must enter a valid last name for the sales person.'  
ELSE IF @ret_code = 3  
   PRINT 'ERROR: An error occurred getting sales value.'  
ELSE IF @ret_code = 4  
   PRINT 'ERROR: No sales recorded for this employee.'     
GO  
  
```  
  
## See Also  
 [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)   
 [PRINT &#40;Transact-SQL&#41;](../../t-sql/language-elements/print-transact-sql.md)   
 [SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)   
 [Cursors](../../relational-databases/cursors.md)   
 [RETURN &#40;Transact-SQL&#41;](../../t-sql/language-elements/return-transact-sql.md)   
 [@@ERROR &#40;Transact-SQL&#41;](../../t-sql/functions/error-transact-sql.md)  
  
  
