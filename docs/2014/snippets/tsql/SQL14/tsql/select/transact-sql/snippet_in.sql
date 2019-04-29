
--<snippet_IN_1>
USE AdventureWorks2012;
GO
SELECT p.FirstName, p.LastName, e.JobTitle
FROM Person.Person p
JOIN HumanResources.Employee AS e
    ON p.BusinessEntityID = e.BusinessEntityID
WHERE e.JobTitle = 'Design Engineer' 
   OR e.JobTitle = 'Tool Designer' 
   OR e.JobTitle = 'Marketing Assistant';
GO
--</snippet_IN_1>

--<snippet_IN_2>
USE AdventureWorks2012;
GO
SELECT p.FirstName, p.LastName, e.JobTitle
FROM Person.Person p
JOIN HumanResources.Employee AS e
    ON p.BusinessEntityID = e.BusinessEntityID
WHERE e.JobTitle IN ('Design Engineer', 'Tool Designer', 'Marketing Assistant');
GO
--</snippet_IN_2>

--<snippet_IN_3>
USE AdventureWorks2012;
GO
SELECT p.FirstName, p.LastName
FROM Person.Person AS p
    JOIN Sales.SalesPerson AS sp
    ON p.BusinessEntityID = sp.BusinessEntityID
WHERE p.BusinessEntityID IN
   (SELECT BusinessEntityID
   FROM Sales.SalesPerson
   WHERE SalesQuota > 250000);
GO
--</snippet_IN_3>

--<snippet_IN_4>
USE AdventureWorks2012;
GO
SELECT p.FirstName, p.LastName
FROM Person.Person AS p
    JOIN Sales.SalesPerson AS sp
    ON p.BusinessEntityID = sp.BusinessEntityID
WHERE p.BusinessEntityID NOT IN
   (SELECT BusinessEntityID
   FROM Sales.SalesPerson
   WHERE SalesQuota > 250000);
GO
--</snippet_IN_4>

