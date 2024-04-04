USE AdventureWorks2022;
GO

SELECT pp.FirstName,
    pp.LastName,
    e.NationalIDNumber
FROM HumanResources.Employee AS e WITH (INDEX (AK_Employee_NationalIDNumber))
INNER JOIN Person.Person AS pp
    ON e.BusinessEntityID = pp.BusinessEntityID
WHERE LastName = 'Johnson';
GO

-- Force a table scan by using INDEX = 0.
USE AdventureWorks2022;
GO

SELECT pp.LastName,
    pp.FirstName,
    e.JobTitle
FROM HumanResources.Employee AS e WITH (INDEX = 0)
INNER JOIN Person.Person AS pp
    ON e.BusinessEntityID = pp.BusinessEntityID
WHERE LastName = 'Johnson';
GO

