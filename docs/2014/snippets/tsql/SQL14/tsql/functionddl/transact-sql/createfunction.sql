-- UDF Function DDL examples

--<snippetCreateFunction1>
USE AdventureWorks2012;
GO
IF OBJECT_ID (N'dbo.ISOweek', N'FN') IS NOT NULL
    DROP FUNCTION dbo.ISOweek;
GO
CREATE FUNCTION dbo.ISOweek (@DATE datetime)
RETURNS int
WITH EXECUTE AS CALLER
AS
BEGIN
     DECLARE @ISOweek int;
     SET @ISOweek= DATEPART(wk,@DATE)+1
          -DATEPART(wk,CAST(DATEPART(yy,@DATE) as CHAR(4))+'0104');
--Special cases: Jan 1-3 may belong to the previous year
     IF (@ISOweek=0) 
          SET @ISOweek=dbo.ISOweek(CAST(DATEPART(yy,@DATE)-1 
               AS CHAR(4))+'12'+ CAST(24+DATEPART(DAY,@DATE) AS CHAR(2)))+1;
--Special case: Dec 29-31 may belong to the next year
     IF ((DATEPART(mm,@DATE)=12) AND 
          ((DATEPART(dd,@DATE)-DATEPART(dw,@DATE))>= 28))
          SET @ISOweek=1;
     RETURN(@ISOweek);
END;
GO
SET DATEFIRST 1;
SELECT dbo.ISOweek(CONVERT(DATETIME,'12/26/2004',101)) AS 'ISO Week';
--</snippetCreateFunction1>

--<snippetCreateFunction2>
USE AdventureWorks2012;
GO
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
GO
--</snippetCreateFunction2>
--<snippetCreateFunction3>
SELECT * FROM Sales.ufn_SalesByStore (602);
--</snippetCreateFunction3>

--<snippetCreateFunction4>
USE AdventureWorks2012;
GO
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
-- Example invocation
SELECT EmployeeID, FirstName, LastName, JobTitle, RecursionLevel
FROM dbo.ufn_FindReports(1); 

GO
--</snippetCreateFunction4>
--<snippetCreateFunction5>
USE AdventureWorks2012;
GO
SELECT definition, type 
FROM sys.sql_modules AS m
JOIN sys.objects AS o ON m.object_id = o.object_id 
    AND type IN ('FN', 'IF', 'TF');
GO
--</snippetCreateFunction5>

--<snippetCreateFunction6>
IF OBJECT_ID(N'dbo.GetWeekDay', N'FN') IS NOT NULL
    DROP FUNCTION dbo.GetWeekDay;
GO
CREATE FUNCTION dbo.GetWeekDay           -- function name
(@Date datetime)                     -- input parameter name and data type
RETURNS int                          -- return parameter data type
AS
BEGIN                                -- begin body definition
RETURN DATEPART (weekday, @Date)     -- action performed
END;
GO
--</snippetCreateFunction6>
--<snippetCreateFunction7>
SELECT dbo.GetWeekDay(CONVERT(DATETIME,'20020201',101)) AS DayOfWeek;
GO
--</snippetCreateFunction7>
--<snippetCreateFunction8>
USE AdventureWorks2012;
GO
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
GO
--</snippetCreateFunction8>
--<snippetCreateFunction9>
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name, dbo.ufnGetInventoryStock(ProductID)AS CurrentSupply
FROM Production.Product
WHERE ProductModelID BETWEEN 75 and 80;
GO
--</snippetCreateFunction9>
--<snippetCreateFunction10>
USE AdventureWorks2012;
GO
SELECT ContactID, FirstName, LastName, JobTitle, ContactType
FROM dbo.ufnGetContactInformation(1209);
GO
SELECT ContactID, FirstName, LastName, JobTitle, ContactType
FROM dbo.ufnGetContactInformation(5);
GO
--</snippetCreateFunction10>
--<snippetCreateFunction11>
USE AdventureWorks2012;
GO
IF OBJECT_ID(N'dbo.ufnGetContactInformation', N'TF') IS NOT NULL
    DROP FUNCTION dbo.ufnGetContactInformation;
GO
CREATE FUNCTION dbo.ufnGetContactInformation(@ContactID int)
RETURNS @retContactInformation TABLE 
(
    -- Columns returned by the function
    ContactID int PRIMARY KEY NOT NULL, 
    FirstName nvarchar(50) NULL, 
    LastName nvarchar(50) NULL, 
    JobTitle nvarchar(50) NULL, 
    ContactType nvarchar(50) NULL
)
AS 
-- Returns the first name, last name, job title, and contact type for the specified contact.
BEGIN
    DECLARE 
        @FirstName nvarchar(50), 
        @LastName nvarchar(50), 
        @JobTitle nvarchar(50), 
        @ContactType nvarchar(50);
    -- Get common contact information
    SELECT 
        @ContactID = BusinessEntityID, 
        @FirstName = FirstName, 
        @LastName = LastName
    FROM Person.Person 
    WHERE BusinessEntityID = @ContactID;
    -- Get contact job title
    SELECT @JobTitle = 
        CASE 
            -- Check for employee
            WHEN EXISTS(SELECT * FROM Person.Person AS p 
                        WHERE p.BusinessEntityID = @ContactID AND p.PersonType = 'EM') 
                THEN (SELECT JobTitle 
                      FROM HumanResources.Employee AS e
                      WHERE e.BusinessEntityID = @ContactID)
            -- Check for vendor
            WHEN EXISTS(SELECT * FROM Person.Person AS p 
                        WHERE p.BusinessEntityID = @ContactID AND p.PersonType = 'VC') 
                THEN (SELECT ct.Name 
                      FROM Person.ContactType AS ct 
                      INNER JOIN Person.BusinessEntityContact AS bec 
                          ON bec.ContactTypeID = ct.ContactTypeID  
                      WHERE bec.PersonID = @ContactID)
                    
            -- Check for store
            WHEN EXISTS(SELECT * FROM Person.Person AS p 
                        WHERE p.BusinessEntityID = @ContactID AND p.PersonType = 'SC') 
                THEN (SELECT ct.Name 
                      FROM Person.ContactType AS ct 
                      INNER JOIN Person.BusinessEntityContact AS bec 
                          ON bec.ContactTypeID = ct.ContactTypeID  
                      WHERE bec.PersonID = @ContactID)
            ELSE NULL 
        END;
    -- Get contact type
    SET @ContactType = 
        CASE 
            -- Check for employee
            WHEN EXISTS(SELECT * FROM Person.Person AS p 
                        WHERE p.BusinessEntityID = @ContactID AND p.PersonType = 'EM') 
            THEN 'Employee'
            -- Check for vendor
            WHEN EXISTS(SELECT * FROM Person.Person AS p 
                        WHERE p.BusinessEntityID = @ContactID AND p.PersonType = 'VC')
            THEN 'Vendor Contact'
            -- Check for store
            WHEN EXISTS(SELECT * FROM Person.Person AS p 
                        WHERE p.BusinessEntityID = @ContactID AND p.PersonType = 'SC')
            THEN 'Store Contact'
            -- Check for individual consumer
            WHEN EXISTS(SELECT * FROM Person.Person AS p 
                        WHERE p.BusinessEntityID = @ContactID AND p.PersonType = 'IN') 
            THEN 'Consumer'
             -- Check for general contact
            WHEN EXISTS(SELECT * FROM Person.Person AS p 
                        WHERE p.BusinessEntityID = @ContactID AND p.PersonType = 'GC') 
            THEN 'General Contact'
        END;
    -- Return the information to the caller
    IF @ContactID IS NOT NULL 
    BEGIN
        INSERT @retContactInformation
        SELECT @ContactID, @FirstName, @LastName, @JobTitle, @ContactType;
    END;
    RETURN;
END;
GO
--</snippetCreateFunction11>
--<snippetCreateFunction12>
USE AdventureWorks2012;
GO

IF OBJECT_ID(N'Sales.ufn_CustomerNamesInRegion', N'IF') IS NOT NULL
    DROP FUNCTION Sales.ufn_CustomerNamesInRegion;
GO
CREATE FUNCTION Sales.ufn_CustomerNamesInRegion
                 ( @Region nvarchar(50) )
RETURNS table
AS
RETURN (
        SELECT DISTINCT s.Name AS Store, a.City
        FROM Sales.Store AS s
        INNER JOIN Person.BusinessEntityAddress AS bea 
            ON bea.BusinessEntityID = s.BusinessEntityID 
        INNER JOIN Person.Address AS a 
            ON a.AddressID = bea.AddressID
        INNER JOIN Person.StateProvince AS sp 
            ON sp.StateProvinceID = a.StateProvinceID
        WHERE sp.Name = @Region
       );
GO
-- Example of calling the function for a specific region
SELECT *
FROM Sales.ufn_CustomerNamesInRegion(N'Washington')
ORDER BY City;
GO
--</snippetCreateFunction12>
--<snippetCreateFunction13>
IF OBJECT_ID(N'dbo.ufn_CubicVolume', N'FN') IS NOT NULL
    DROP FUNCTION dbo.ufn_CubicVolume;
GO
CREATE FUNCTION dbo.ufn_CubicVolume
-- Input dimensions in centimeters.
   (@CubeLength decimal(4,1), @CubeWidth decimal(4,1),
    @CubeHeight decimal(4,1) )
RETURNS decimal(12,3) -- Cubic Centimeters.
WITH SCHEMABINDING
AS
BEGIN
   RETURN ( @CubeLength * @CubeWidth * @CubeHeight )
END;
GO
--</snippetCreateFunction13>

--<snippetCreateFunction14>
DECLARE @MyDecimalVar decimal(12,3);
EXEC @MyDecimalVar = dbo.ufn_CubicVolume @CubeLength = 12.3,
                        @CubeHeight = 4.5, @CubeWidth = 4.5;
SELECT @MyDecimalVar;
GO
--</snippetCreateFunction14>

--<snippetCreateFunction15>
DECLARE @MyDecimalVar decimal(12,3);
EXEC @MyDecimalVar = dbo.ufn_CubicVolume 12.3, 4.5, 4.5;
SELECT @MyDecimalVar;
GO
--</snippetCreateFunction15>

