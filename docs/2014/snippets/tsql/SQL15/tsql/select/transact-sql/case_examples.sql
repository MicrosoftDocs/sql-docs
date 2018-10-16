--<snippetCASE1>
USE AdventureWorks2012;
GO
SELECT   ProductNumber, Category =
      CASE ProductLine
         WHEN 'R' THEN 'Road'
         WHEN 'M' THEN 'Mountain'
         WHEN 'T' THEN 'Touring'
         WHEN 'S' THEN 'Other sale items'
         ELSE 'Not for sale'
      END,
   Name
FROM Production.Product
ORDER BY ProductNumber;
GO
--</snippetCASE1>


--<snippetCASE1>
USE AdventureWorks2012;
GO
SELECT   ProductNumber, Category =
      CASE ProductLine
         WHEN 'R' THEN 'Road'
         WHEN 'M' THEN 'Mountain'
         WHEN 'T' THEN 'Touring'
         WHEN 'S' THEN 'Other sale items'
         ELSE 'Not for sale'
      END,
   Name
FROM Production.Product
ORDER BY ProductNumber;
GO
--</snippetCASE1>


--<snippetCASE2>
USE AdventureWorks2012;
GO
SELECT   ProductNumber, Name, "Price Range" = 
      CASE 
         WHEN ListPrice =  0 THEN 'Mfg item - not for resale'
         WHEN ListPrice < 50 THEN 'Under $50'
         WHEN ListPrice >= 50 and ListPrice < 250 THEN 'Under $250'
         WHEN ListPrice >= 250 and ListPrice < 1000 THEN 'Under $1000'
         ELSE 'Over $1000'
      END
FROM Production.Product
ORDER BY ProductNumber ;
GO
--</snippetCASE2>

--<snippetCASE3>
USE AdventureWorks2012;
GO
SELECT FirstName, LastName, TelephoneNumber, "When to Contact" = 
     CASE
          WHEN TelephoneSpecialInstructions IS NULL THEN 'Any time'
          ELSE TelephoneSpecialInstructions
     END
FROM Person.vAdditionalContactInfo;
--</snippetCASE3>

--<snippetCASE4>
SELECT BusinessEntityID, SalariedFlag
FROM HumanResources.Employee
ORDER BY CASE SalariedFlag WHEN 1 THEN BusinessEntityID END DESC
        ,CASE WHEN SalariedFlag = 0 THEN BusinessEntityID END;
GO
--</snippetCASE4>

IF OBJECT_ID ('dbo.GetContactInformation') IS NOT NULL
	DROP FUNCTION dbo.GetContactInformation;
GO
--<snippetCASE5>
	USE AdventureWorks2012;
	GO
	CREATE FUNCTION dbo.GetContactInformation(@BusinessEntityID int)
	RETURNS @retContactInformation TABLE 
	(
	BusinessEntityID int NOT NULL,
	FirstName nvarchar(50) NULL,
	LastName nvarchar(50) NULL,
	ContactType nvarchar(50) NULL,
    PRIMARY KEY CLUSTERED (BusinessEntityID ASC)
) 
AS 
-- Returns the first name, last name and contact type for the specified contact.
BEGIN
    DECLARE 
        @FirstName nvarchar(50), 
        @LastName nvarchar(50), 
        @ContactType nvarchar(50);

    -- Get common contact information
    SELECT 
        @BusinessEntityID = BusinessEntityID, 
		@FirstName = FirstName, 
        @LastName = LastName
    FROM Person.Person 
    WHERE BusinessEntityID = @BusinessEntityID;

    SET @ContactType = 
        CASE 
            -- Check for employee
            WHEN EXISTS(SELECT * FROM HumanResources.Employee AS e 
                WHERE e.BusinessEntityID = @BusinessEntityID) 
                THEN 'Employee'

            -- Check for vendor
            WHEN EXISTS(SELECT * FROM Person.BusinessEntityContact AS bec
                WHERE bec.BusinessEntityID = @BusinessEntityID) 
                THEN 'Vendor'

            -- Check for store
            WHEN EXISTS(SELECT * FROM Purchasing.Vendor AS v          
                WHERE v.BusinessEntityID = @BusinessEntityID) 
                THEN 'Store Contact'

            -- Check for individual consumer
            WHEN EXISTS(SELECT * FROM Sales.Customer AS c 
                WHERE c.PersonID = @BusinessEntityID) 
                THEN 'Consumer'
        END;

    -- Return the information to the caller
    IF @BusinessEntityID IS NOT NULL 
    BEGIN
        INSERT @retContactInformation
        SELECT @BusinessEntityID, @FirstName, @LastName, @ContactType;
    END;

    RETURN;
END;
GO

SELECT BusinessEntityID, FirstName, LastName, ContactType
FROM dbo.GetContactInformation(2200);
GO
SELECT BusinessEntityID, FirstName, LastName, ContactType
FROM dbo.GetContactInformation(5);
--</snippetCASE5>


--<snippetCASE6>
USE AdventureWorks2012;
GO
UPDATE HumanResources.Employee
SET VacationHours = 
    ( CASE
         WHEN ((VacationHours - 10.00) < 0) THEN VacationHours + 40
         ELSE (VacationHours + 20.00)
       END
    )
OUTPUT Deleted.BusinessEntityID, Deleted.VacationHours AS BeforeValue, 
       Inserted.VacationHours AS AfterValue
WHERE SalariedFlag = 0; 
--</snippetCASE6>

--<snippetCASE7>
USE AdventureWorks2012;
GO
SELECT JobTitle, MAX(ph1.Rate)AS MaximumRate
FROM HumanResources.Employee AS e
JOIN HumanResources.EmployeePayHistory AS ph1 ON e.BusinessEntityID = ph1.BusinessEntityID
GROUP BY JobTitle
HAVING (MAX(CASE WHEN Gender = 'M' 
        THEN ph1.Rate 
        ELSE NULL END) > 40.00
     OR MAX(CASE WHEN Gender  = 'F' 
        THEN ph1.Rate  
        ELSE NULL END) > 42.00)
ORDER BY MaximumRate DESC;
--</snippetCASE7>

--<snippetCASE8>
SELECT BusinessEntityID, LastName, TerritoryName, CountryRegionName
FROM Sales.vSalesPerson
WHERE TerritoryName IS NOT NULL
ORDER BY CASE CountryRegionName WHEN 'United States' THEN TerritoryName
         ELSE CountryRegionName END;
--</snippetCASE8>