---
title: "Return data from a stored procedure"
description: Learn how to return data from a procedure to a calling program by using result sets, output parameters, and return codes.
ms.service: sql
ms.subservice: stored-procedures
ms.topic: conceptual
helpviewer_keywords: 
  - "stored procedures [SQL Server], returning data"
  - "returning data from stored procedure"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
ms.custom: FY22Q2Fresh
ms.date: "12/15/2021"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Return data from a stored procedure

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

There are three ways of returning data from a procedure to a calling program: result sets, output parameters, and return codes. This article provides information on the three approaches.
  
## Return data using result sets

If you include a SELECT statement in the body of a stored procedure (but not a SELECT ... INTO or INSERT ... SELECT), the rows specified by the SELECT statement will be sent directly to the client. For large result sets, the stored procedure execution won't continue to the next statement until the result set has been completely sent to the client. For small result sets, the results will be spooled for return to the client and execution will continue. If multiple such SELECT statements are run during the execution of the stored procedure, multiple result sets will be sent to the client. This behavior also applies to nested Transact-SQL batches, nested stored procedures, and top-level Transact-SQL batches.

### Examples of returning data using a result set

The following examples use the [AdventureWorks2019 sample database](../../samples/adventureworks-install-configure.md). This example shows a stored procedure that returns the `LastName` and `SalesYTD` values for all `SalesPerson` rows that also appear in the `vEmployee` view.
  
 ```sql
USE AdventureWorks2019;  
GO

IF OBJECT_ID('Sales.uspGetEmployeeSalesYTD', 'P') IS NOT NULL  
    DROP PROCEDURE Sales.uspGetEmployeeSalesYTD;  
GO  

CREATE PROCEDURE Sales.uspGetEmployeeSalesYTD  
AS    
    SET NOCOUNT ON;

    SELECT LastName, SalesYTD  
    FROM Sales.SalesPerson AS sp  
    JOIN HumanResources.vEmployee AS e ON e.BusinessEntityID = sp.BusinessEntityID;
    
    RETURN;
GO 
```  

## Return data using an output parameter  

If you specify the output keyword for a parameter in the procedure definition, the procedure can return the current value of the parameter to the calling program when the procedure exits. To save the value of the parameter in a variable that can be used in the calling program, the calling program must use the output keyword when executing the procedure. For more information about what data types can be used as output parameters, see [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md).
  
### Examples of output parameters

The following example shows a procedure with an input and an output parameter. The `@SalesPerson` parameter would receive an input value specified by the calling program. The SELECT statement uses the value passed into the input parameter to obtain the correct `SalesYTD` value. The SELECT statement also assigns the value to the `@SalesYTD` output parameter, which returns the value to the calling program when the procedure exits.
  
```sql
USE AdventureWorks2019;  
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

    RETURN;
GO 
```  
  
The following example calls the procedure created in the first example and saves the output value returned from the called procedure in the `@SalesYTD` variable, which is local to the calling program.

The example:

- Declares the variable `@SalesYTDBySalesPerson` to receive the output value of the procedure.
- Executes the `Sales.uspGetEmployeeSalesYTD` procedure specifying a last name for the input parameter. Save the output value in the variable `@SalesYTDBySalesPerson`.
- Calls [PRINT](../../t-sql/language-elements/print-transact-sql.md) to display the value saved to `@SalesYTDBySalesPerson`.
  
```sql
DECLARE @SalesYTDBySalesPerson money;

EXECUTE Sales.uspGetEmployeeSalesYTD  
    N'Blythe', @SalesYTD = @SalesYTDBySalesPerson OUTPUT;  

PRINT 'Year-to-date sales for this employee is ' +   
    CONVERT(varchar(10),@SalesYTDBySalesPerson);  
GO
``` 


  
Input values can also be specified for output parameters when the procedure is executed. This allows the procedure to receive a value from the calling program, change or perform operations with the value, and then return the new value to the calling program. In the previous example, the `@SalesYTDBySalesPerson` variable can be assigned a value before the program calls the `Sales.uspGetEmployeeSalesYTD` procedure. The execute statement would pass the `@SalesYTDBySalesPerson` variable value into the `@SalesYTD` output parameter. Then in the procedure body, the value could be used for calculations that generate a new value. The new value would be passed back out of the procedure through the output parameter, updating the value in the `@SalesYTDBySalesPerson` variable when the procedure exits. This is often referred to as "pass-by-reference capability."  
  
If you specify output for a parameter when you call a procedure and that parameter isn't defined by using output in the procedure definition, you get an error message. However, you can execute a procedure with output parameters and not specify output when executing the procedure. No error is returned, but you can't use the output value in the calling program.
  
### Use the cursor data type in output parameters

[!INCLUDE[tsql](../../includes/tsql-md.md)] procedures can use the **cursor** data type only for output parameters. If the **cursor** data type is specified for a parameter, both the varying and output keywords must be specified for that parameter in the procedure definition. A parameter can be specified as only output, but if the varying keyword is specified in the parameter declaration, the data type must be **cursor** and the output keyword must also be specified.

> [!NOTE]  
> The **cursor** data type cannot be bound to application variables through the database APIs such as OLE DB, ODBC, ADO, and DB-Library. Because output parameters must be bound before an application can execute a procedure, procedures with **cursor** output parameters cannot be called from the database APIs. These procedures can be called from [!INCLUDE[tsql](../../includes/tsql-md.md)] batches, procedures, or triggers only when the **cursor** output variable is assigned to a [!INCLUDE[tsql](../../includes/tsql-md.md)] local **cursor** variable.
  
### Rules for cursor output parameters

The following rules pertain to **cursor** output parameters when the procedure is executed:  
  
- For a forward-only cursor, the rows returned in the cursor's result set are only those rows at and beyond the position of the cursor at the conclusion of the procedure execution, for example:  
  
  - A nonscrollable cursor is opened in a procedure on a result set named `RS` of 100 rows.

  - The procedure fetches the first five rows of result set `RS`.
  
  - The procedure returns to its caller.
  
  - The result set `RS` returned to the caller consists of rows from 6 through 100 of `RS`, and the cursor in the caller is positioned before the first row of `RS`.
  
- For a forward-only cursor, if the cursor is positioned before the first row when the procedure exits, the entire result set is returned to the calling batch, procedure, or trigger. When returned, the cursor position is set before the first row.
  
- For a forward-only cursor, if the cursor is positioned beyond the end of the last row when the procedure exits, an empty result set is returned to the calling batch, procedure, or trigger.
  
    > [!NOTE]  
    >  An empty result set is not the same as a null value.
  
- For a scrollable cursor, all the rows in the result set are returned to the calling batch, procedure, or trigger when the procedure exits. When returned, the cursor position is left at the position of the last fetch executed in the procedure.
  
- For any type of cursor, if the cursor is closed, then a null value is passed back to the calling batch, procedure, or trigger. This will also be the case if a cursor is assigned to a parameter, but that cursor is never opened.
  
    > [!NOTE]  
    >  The closed state matters only at return time. For example, it is valid to close a cursor part of the way through the procedure, to open it again later in the procedure, and return that cursor's result set to the calling batch, procedure, or trigger.
  
### Examples of cursor output parameters

In the following example, a procedure is created that specified an output parameter, `@CurrencyCursor` using the **cursor** data type. The procedure is then called in a batch.

First, create the procedure that declares and then opens a cursor on the `Currency` table.
  
```sql
USE AdventureWorks2019;  
GO

IF OBJECT_ID ( 'dbo.uspCurrencyCursor', 'P' ) IS NOT NULL  
    DROP PROCEDURE dbo.uspCurrencyCursor;  
GO

CREATE PROCEDURE dbo.uspCurrencyCursor   
    @CurrencyCursor CURSOR VARYING OUTPUT  
AS  
    SET NOCOUNT ON;

    SET @CurrencyCursor = CURSOR FORWARD_ONLY STATIC FOR  
      SELECT CurrencyCode, Name  
      FROM Sales.Currency;  

    OPEN @CurrencyCursor;  
GO  
```  
  
Next, execute a batch that declares a local cursor variable, executes the procedure to assign the cursor to the local variable, and then fetches the rows from the cursor.
  
```sql
USE AdventureWorks2019;  
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

## Return data using a return code  

A procedure can return an integer value called a return code to indicate the execution status of a procedure. You specify the return code for a procedure using the [RETURN statement](../../t-sql/language-elements/return-transact-sql.md). As with output parameters, you must save the return code in a variable when the procedure is executed in order to use the return code value in the calling program. For example, the assignment variable `@result` of data type `int` is used to store the return code from the procedure `my_proc`, such as:  
  
```sql
DECLARE @result int;

EXECUTE @result = my_proc;
GO
```  
  
Return codes are commonly used in control-of-flow blocks within procedures to set the return code value for each possible error situation. You can use the @@ERROR function after a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement to detect whether an error occurred during the execution of the statement. Before the introduction of TRY/CATCH/THROW error handling in TSQL return codes were sometimes required to determine the success or failure of stored procedures. Stored Procedures should always indicate failure with an error (generated with THROW/RAISERROR if necessary), and not rely on a return code to indicate the failure. Also you should avoid using the return code to return application data.
  
### Examples of return codes

The following example shows the `usp_GetSalesYTD` procedure with error handling that sets special return code values for various errors. The following table shows the integer value that is assigned by the procedure to each possible error, and the corresponding meaning for each value.
  
|Return code value|Meaning|  
|-----------------------|-------------|  
|0|Successful execution.|  
|1|Required parameter value is not specified.|  
|2|Specified parameter value is not valid.|  
|3|Error has occurred getting sales value.|  
|4|NULL sales value found for the salesperson.|  

The example creates a procedure named `Sales.usp_GetSalesYTD`, which:

- Declares the `@SalesPerson` parameter and sets its default value to NULL. This parameter is intended to take the last name of a sales person. 
- Validates the `@SalesPerson` parameter. 
    - If `@SalesPerson` is NULL, the procedure prints a message and returns the return code 1.
    - Otherwise, if the `@SalesPerson` parameter isn't NULL, the procedure checks the count of rows in the `HumanResources.vEmployee` table with a last name equal to the value of `@SalesPerson`. If the count is zero, the procedure returns the return code 2.
- Queries the year-to-date sales for the sales person with the specified last name and assigns it to the `@SalesYTD` output parameter.
- Checks for SQL Server errors by testing [&#x40;&#x40;ERROR (Transact-SQL)](../../t-sql/functions/error-transact-sql.md). 
    - If @@ERROR isn't equal to zero, the procedure returns the return code 3.
    - If @@ERROR was equal to zero, the procedure checks to see if the `@SalesYTD` parameter value is NULL. If no year to date sales were found, the procedure returns the return code 4.
    - If neither of the preceding conditions are true, the procedure returns the return code 0.
- If reached, the final statement in the stored procedure invokes the stored procedure recursively without specifying an input value.

At the end of the example, code is provided to execute the `Sales.usp_GetSalesYTD` procedure while specifying a last name for the input parameter and saving the output value in the variable `@SalesYTD`.

```sql
USE AdventureWorks2019;  
GO
  
CREATE PROCEDURE Sales.usp_GetSalesYTD 
    @SalesPerson NVARCHAR(50) = NULL, 
    @SalesYTD MONEY=NULL OUTPUT
AS
    IF @SalesPerson IS NULL 
    BEGIN
        PRINT 'ERROR: You must specify a last name for the sales person.'
        RETURN (1)
    END
    ELSE 
    BEGIN
        IF(SELECT COUNT(*)FROM HumanResources.vEmployee WHERE LastName=@SalesPerson)=0
            RETURN (2)
    END

    SELECT @SalesYTD=SalesYTD
    FROM Sales.SalesPerson AS sp
         JOIN HumanResources.vEmployee AS e ON e.BusinessEntityID=sp.BusinessEntityID
    WHERE LastName=@SalesPerson;

    IF @@ERROR<>0 
    BEGIN
        RETURN (3)
    END 
    ELSE 
    BEGIN
        IF @SalesYTD IS NULL 
            RETURN (4)
        ELSE 
            RETURN (0)
    END

    EXEC Sales.usp_GetSalesYTD;
GO


DECLARE @SalesYTDForSalesPerson money, @ret_code int;  

EXECUTE Sales.usp_GetSalesYTD  N'Blythe', @SalesYTD = @SalesYTDForSalesPerson OUTPUT;  

PRINT N'Year-to-date sales for this employee is ' +  
    CONVERT(varchar(10), @SalesYTDForSalesPerson);  
GO
```  
  
The following example creates a program to handle the return codes that are returned from the `usp_GetSalesYTD` procedure.

The example:

- Declares variables `@SalesYTDForSalesPerson` and `@ret_code` to receive the output value and return code of the procedure.
- Executes the `Sales.usp_GetSalesYTD` procedure with an input value specified for @SalesPerson and saves the output value and return code in variables.
- Checks the return code in `@ret_code` and calls [PRINT (Transact-SQL)](../../t-sql/language-elements/print-transact-sql.md) to display an appropriate message.

```sql

DECLARE @SalesYTDForSalesPerson money, @ret_code int;  
  
EXECUTE @ret_code = Sales.usp_GetSalesYTD  
    N'Blythe', @SalesYTD = @SalesYTDForSalesPerson OUTPUT;  

IF @ret_code = 0  
    BEGIN  
        PRINT 'Procedure executed successfully';
        PRINT 'Year-to-date sales for this employee is ' + CONVERT(varchar(10),@SalesYTDForSalesPerson);
    END  
ELSE IF @ret_code = 1  
   PRINT 'ERROR: You must specify a last name for the sales person.';
ELSE IF @ret_code = 2   
   PRINT 'ERROR: You must enter a valid last name for the sales person.';
ELSE IF @ret_code = 3  
   PRINT 'ERROR: An error occurred getting sales value.';
ELSE IF @ret_code = 4  
   PRINT 'ERROR: No sales recorded for this employee.';
GO
```  
  
## Next steps

For more information about stored procedures and related concepts, see the following articles:

- [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)
- [PRINT &#40;Transact-SQL&#41;](../../t-sql/language-elements/print-transact-sql.md)
- [SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)
- [RETURN &#40;Transact-SQL&#41;](../../t-sql/language-elements/return-transact-sql.md)
- [@@ERROR &#40;Transact-SQL&#41;](../../t-sql/functions/error-transact-sql.md)  
