--<snippetSP_UsingReturnCode1>
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
--</snippetSP_UsingReturnCode1>


--<snippetSP_UsingReturnCode2>
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
   PRINT 'EERROR: You must enter a valid last name for the sales person.'
ELSE IF @ret_code = 3
   PRINT 'ERROR: An error occurred getting sales value.'
ELSE IF @ret_code = 4
   PRINT 'ERROR: No sales recorded for this employee.'   
GO
--</snippetSP_UsingReturnCode2>
