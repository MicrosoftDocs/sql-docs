-- Snippets from CREATE PROC topic.

--<SnippetCreateProc1>
USE AdventureWorks2012;
GO
IF OBJECT_ID ( 'HumanResources.uspGetAllEmployees', 'P' ) IS NOT NULL 
    DROP PROCEDURE HumanResources.uspGetAllEmployees;
GO
CREATE PROCEDURE HumanResources.uspGetAllEmployees
AS
    SET NOCOUNT ON;
    SELECT LastName, FirstName, Department
    FROM HumanResources.vEmployeeDepartmentHistory;
GO

--</SnippetCreateProc1>
--<SnippetExecProc1>
EXECUTE HumanResources.uspGetAllEmployees;
GO
-- Or
EXEC HumanResources.uspGetAllEmployees;
GO
-- Or, if this procedure is the first statement within a batch:
HumanResources.uspGetAllEmployees;
--</SnippetExecProc1>
--<SnippetCreateProc2>
USE AdventureWorks2012;
GO
IF OBJECT_ID ( 'HumanResources.uspGetEmployees', 'P' ) IS NOT NULL 
    DROP PROCEDURE HumanResources.uspGetEmployees;
GO
CREATE PROCEDURE HumanResources.uspGetEmployees 
    @LastName nvarchar(50), 
    @FirstName nvarchar(50) 
AS 
    
    SET NOCOUNT ON;
    SELECT FirstName, LastName, Department
    FROM HumanResources.vEmployeeDepartmentHistory
    WHERE FirstName = @FirstName AND LastName = @LastName;
GO
--</SnippetCreateProc2>
--<SnippetExecProc2>
EXECUTE HumanResources.uspGetEmployees N'Ackerman', N'Pilar';
-- Or
EXEC HumanResources.uspGetEmployees @LastName = N'Ackerman', @FirstName = N'Pilar';
GO
-- Or
EXECUTE HumanResources.uspGetEmployees @FirstName = N'Pilar', @LastName = N'Ackerman';
GO
-- Or, if this procedure is the first statement within a batch:
HumanResources.uspGetEmployees N'Ackerman', N'Pilar';
--</SnippetExecProc2>
--<SnippetCreateProc3>
USE AdventureWorks2012;
GO
IF OBJECT_ID ( 'HumanResources.uspGetEmployees2', 'P' ) IS NOT NULL 
    DROP PROCEDURE HumanResources.uspGetEmployees2;
GO
CREATE PROCEDURE HumanResources.uspGetEmployees2 
    @LastName nvarchar(50) = N'D%', 
    @FirstName nvarchar(50) = N'%'
AS 
    SET NOCOUNT ON;
    SELECT FirstName, LastName, Department
    FROM HumanResources.vEmployeeDepartmentHistory
    WHERE FirstName LIKE @FirstName AND LastName LIKE @LastName;
GO
--</SnippetCreateProc3>
--<SnippetExecProc3>
EXECUTE HumanResources.uspGetEmployees2;
-- Or
EXECUTE HumanResources.uspGetEmployees2 N'Wi%';
-- Or
EXECUTE HumanResources.uspGetEmployees2 @FirstName = N'%';
-- Or
EXECUTE HumanResources.uspGetEmployees2 N'[CK]ars[OE]n';
-- Or
EXECUTE HumanResources.uspGetEmployees2 N'Hesse', N'Stefen';
-- Or
EXECUTE HumanResources.uspGetEmployees2 N'H%', N'S%';
--</SnippetExecProc3>
--<SnippetCreateProc4>
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
--</SnippetCreateProc4>
--<SnippetExecProc4>
DECLARE @ComparePrice money, @Cost money 
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
    $'+ RTRIM(CAST(@ComparePrice AS varchar(20)))+'.'
--</SnippetExecProc4>
--<SnippetCreateProc5>
USE AdventureWorks2012;
GO
IF OBJECT_ID ( 'dbo.uspProductByVendor', 'P' ) IS NOT NULL 
    DROP PROCEDURE dbo.uspProductByVendor;
GO
CREATE PROCEDURE dbo.uspProductByVendor @Name varchar(30) = '%'
WITH RECOMPILE
AS
    SET NOCOUNT ON;
    SELECT v.Name AS 'Vendor name', p.Name AS 'Product name'
    FROM Purchasing.Vendor AS v 
    JOIN Purchasing.ProductVendor AS pv 
      ON v.BusinessEntityID = pv.BusinessEntityID 
    JOIN Production.Product AS p 
      ON pv.ProductID = p.ProductID
    WHERE v.Name LIKE @Name;
GO
--</SnippetCreateProc5>
--<SnippetCreateProc6>
USE AdventureWorks2012;
GO
IF OBJECT_ID ( 'HumanResources.uspEncryptThis', 'P' ) IS NOT NULL 
    DROP PROCEDURE HumanResources.uspEncryptThis;
GO
CREATE PROCEDURE HumanResources.uspEncryptThis
WITH ENCRYPTION
AS
    SET NOCOUNT ON;
    SELECT BusinessEntityID, JobTitle, NationalIDNumber, VacationHours, SickLeaveHours 
    FROM HumanResources.Employee;
GO
--</SnippetCreateProc6>
--<SnippetCreateProc7>
USE AdventureWorks2012;
GO
IF OBJECT_ID ( 'dbo.uspProc1', 'P' ) IS NOT NULL 
    DROP PROCEDURE dbo.uspProc1;
GO
CREATE PROCEDURE dbo.uspProc1
AS
    SET NOCOUNT ON;
    SELECT column1, column2 FROM table_does_not_exist
GO
--</SnippetCreateProc7>
--<SnippetExecProc7>
USE AdventureWorks2012;
GO
SELECT definition
FROM sys.sql_modules
WHERE object_id = OBJECT_ID('dbo.uspproc1');
--</SnippetExecProc7>
--<SnippetCreateProc8>
USE AdventureWorks2012;
GO
IF OBJECT_ID ( 'Purchasing.uspVendorAllInfo', 'P' ) IS NOT NULL 
    DROP PROCEDURE Purchasing.uspVendorAllInfo;
GO
CREATE PROCEDURE Purchasing.uspVendorAllInfo
WITH EXECUTE AS CALLER
AS
    SET NOCOUNT ON;
    SELECT v.Name AS Vendor, p.Name AS 'Product name', 
      v.CreditRating AS 'Rating', 
      v.ActiveFlag AS Availability
    FROM Purchasing.Vendor v 
    INNER JOIN Purchasing.ProductVendor pv
      ON v.BusinessEntityID = pv.BusinessEntityID 
    INNER JOIN Production.Product p
      ON pv.ProductID = p.ProductID 
    ORDER BY v.Name ASC;
GO
--</SnippetCreateProc8>
--<SnippetCreateProc9>
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
--</SnippetCreateProc9>
--<SnippetExecProc9>
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
--</SnippetExecProc9>
--<SnippetCreateProc10>
USE AdventureWorks2012;
GO
CREATE PROCEDURE HumanResources.uspEmployeesInDepartment 
@DeptValue int
WITH EXECUTE AS OWNER
AS
    SET NOCOUNT ON;
    SELECT e.BusinessEntityID, c.LastName, c.FirstName, e.JobTitle
    FROM Person.Person AS c 
    INNER JOIN HumanResources.Employee AS e
        ON c.BusinessEntityID = e.BusinessEntityID
    INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh
        ON e.BusinessEntityID = edh.BusinessEntityID
    WHERE edh.DepartmentID = @DeptValue
    ORDER BY c.LastName, c.FirstName;
GO

-- Execute the stored procedure by specifying department 5.
EXECUTE HumanResources.uspEmployeesInDepartment 5;
GO
--</SnippetCreateProc10>


--<SnippetCreateProc11>
USE AdventureWorks2012;
GO
IF OBJECT_ID(N'dbo.ProcTestDefaults', N'P')IS NOT NULL
   DROP PROCEDURE dbo.ProcTestDefaults;
GO
-- Create the stored procedure.
CREATE PROCEDURE dbo.ProcTestDefaults (
@p1 smallint = 42, 
@p2 char(1), 
@p3 varchar(8) = 'CAR')
AS 
   SET NOCOUNT ON;
   SELECT @p1, @p2, @p3
;
GO
--</SnippetCreateProc11>

--<SnippetExecProc11>
-- Specifying a value only for one parameter (@p2).
EXECUTE dbo.ProcTestDefaults @p2 = 'A';
-- Specifying a value for the first two parameters.
EXECUTE dbo.ProcTestDefaults 68, 'B';
-- Specifying a value for all three parameters.
EXECUTE dbo.ProcTestDefaults 68, 'C', 'House';
-- Using the DEFAULT keyword for the first parameter.
EXECUTE dbo.ProcTestDefaults @p1 = DEFAULT, @p2 = 'D';
-- Specifying the parameters in an order different from the order defined in the procedure.
EXECUTE dbo.ProcTestDefaults DEFAULT, @p3 = 'Local', @p2 = 'E';
-- Using the DEFAULT keyword for the first and third parameters.
EXECUTE dbo.ProcTestDefaults DEFAULT, 'H', DEFAULT;
EXECUTE dbo.ProcTestDefaults DEFAULT, 'I', @p3 = DEFAULT;
--</SnippetExecProc11>

--<SnippetCreateProc12>
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
--</SnippetCreateProc12>
--<SnippetCreateProc13>
IF OBJECT_ID('dbo.my_proc', 'P') IS NOT NULL
    DROP PROCEDURE dbo.my_proc;
GO
CREATE PROCEDURE dbo.my_proc
    @first int = NULL,  -- NULL default value
    @second int = 2,    -- Default value of 2
    @third int = 3      -- Default value of 3
AS 
    SET NOCOUNT ON;
    SELECT @first, @second, @third;
GO
--</SnippetCreateProc13>

--<SnippetCreateProc14>
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
--</SnippetCreateProc14>

--<SnippetExecProc14>
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
--</SnippetExecProc14>
--<SnippetCreateProc15>
USE AdventureWorks2012;
GO
CREATE PROCEDURE HumanResources.uspGetEmployeesTest2 
    @LastName nvarchar(50), 
    @FirstName nvarchar(50) 
AS 
    
    SET NOCOUNT ON;
    SELECT FirstName, LastName, Department
    FROM HumanResources.vEmployeeDepartmentHistory
    WHERE FirstName = @FirstName AND LastName = @LastName
    AND EndDate IS NULL;
GO
--</SnippetCreateProc15>

--<SnippetExecProc15>
EXECUTE HumanResources.uspGetEmployeesTest2 N'Ackerman', N'Pilar';
-- Or
EXEC HumanResources.uspGetEmployeesTest2 @LastName = N'Ackerman', @FirstName = N'Pilar';
GO
-- Or
EXECUTE HumanResources.uspGetEmployeesTest2 @FirstName = N'Pilar', @LastName = N'Ackerman';
GO
--</SnippetExecProc15>
--<SnippetSp_stored_proc1>
USE AdventureWorks2012;
GO
EXECUTE sp_stored_procedures;
--</SnippetSp_stored_proc1>

--<SnippetSp_stored_proc2>
USE AdventureWorks2012;
GO
EXECUTE sp_stored_procedures N'uspLogError', N'dbo', N'AdventureWorks2012', 1;
--</SnippetSp_stored_proc2>

--<SnippetAlterProc1>
USE AdventureWorks2012;
GO
ALTER PROCEDURE Purchasing.uspVendorAllInfo
    @Product varchar(25) 
AS
    SET NOCOUNT ON;
    SELECT LEFT(v.Name, 25) AS Vendor, LEFT(p.Name, 25) AS 'Product name', 
    'Rating' = CASE v.CreditRating 
        WHEN 1 THEN 'Superior'
        WHEN 2 THEN 'Excellent'
        WHEN 3 THEN 'Above average'
        WHEN 4 THEN 'Average'
        WHEN 5 THEN 'Below average'
        ELSE 'No rating'
        END
    , Availability = CASE v.ActiveFlag
        WHEN 1 THEN 'Yes'
        ELSE 'No'
        END
    FROM Purchasing.Vendor AS v 
    INNER JOIN Purchasing.ProductVendor AS pv
      ON v.BusinessEntityID = pv.BusinessEntityID 
    INNER JOIN Production.Product AS p 
      ON pv.ProductID = p.ProductID 
    WHERE p.Name LIKE @Product
    ORDER BY v.Name ASC;
GO
--</SnippetAlterProc1>
--<SnippetAlterProc2>
EXEC Purchasing.uspVendorAllInfo N'LL Crankarm';
GO
--</SnippetAlterProc2>

