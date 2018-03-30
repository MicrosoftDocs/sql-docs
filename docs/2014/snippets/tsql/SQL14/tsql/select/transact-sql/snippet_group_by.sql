
--<snippet_GROUP_BY_1>
USE AdventureWorks2012 ;
GO
SELECT SalesOrderID, SUM(LineTotal) AS SubTotal
FROM Sales.SalesOrderDetail sod
GROUP BY SalesOrderID
ORDER BY SalesOrderID ;
--</snippet_GROUP_BY_1>

--<snippet_GROUP_BY_2>
USE AdventureWorks2012 ;
GO
SELECT a.City, COUNT(bea.AddressID) EmployeeCount
FROM Person.BusinessEntityAddress bea 
JOIN Person.Address AS a ON a.AddressID = bea.AddressID
JOIN HumanResources.Employee AS e 
ON e.BusinessEntityID = bea.BusinessEntityID
GROUP BY a.City
ORDER BY a.City;
--</snippet_GROUP_BY_2>

