--<snippetSelectExamples1>
USE AdventureWorks2012;
GO
SELECT *
FROM Production.Product
ORDER BY Name ASC;
-- Alternate way.
USE AdventureWorks2012;
GO
SELECT p.*
FROM Production.Product AS p
ORDER BY Name ASC;
GO
--</snippetSelectExamples1>

--<snippetSelectExamples2>
USE AdventureWorks2012;
GO
SELECT Name, ProductNumber, ListPrice AS Price
FROM Production.Product 
ORDER BY Name ASC;
GO
--</snippetSelectExamples2>

--<snippetSelectExamples3>
USE AdventureWorks2012;
GO
SELECT Name, ProductNumber, ListPrice AS Price
FROM Production.Product 
WHERE ProductLine = 'R' 
AND DaysToManufacture < 4
ORDER BY Name ASC;
GO
--</snippetSelectExamples3>

--<snippetSelectExamples4>
USE AdventureWorks2012;
GO
SELECT p.Name AS ProductName, 
NonDiscountSales = (OrderQty * UnitPrice),
Discounts = ((OrderQty * UnitPrice) * UnitPriceDiscount)
FROM Production.Product AS p 
INNER JOIN Sales.SalesOrderDetail AS sod
ON p.ProductID = sod.ProductID 
ORDER BY ProductName DESC;
GO
--</snippetSelectExamples4>

--<snippetSelectExamples5>
USE AdventureWorks2012;
GO
SELECT 'Total income is', ((OrderQty * UnitPrice) * (1.0 - UnitPriceDiscount)), ' for ',
p.Name AS ProductName 
FROM Production.Product AS p 
INNER JOIN Sales.SalesOrderDetail AS sod
ON p.ProductID = sod.ProductID 
ORDER BY ProductName ASC;
GO
--</snippetSelectExamples5>

--<snippetSelectExamples6>
USE AdventureWorks2012;
GO
SELECT DISTINCT JobTitle
FROM HumanResources.Employee
ORDER BY JobTitle;
GO
--</snippetSelectExamples6>

--<snippetSelectExamples7>
USE tempdb;
GO
IF OBJECT_ID (N'#Bicycles',N'U') IS NOT NULL
DROP TABLE #Bicycles;
GO
SELECT * 
INTO #Bicycles
FROM AdventureWorks2012.Production.Product
WHERE ProductNumber LIKE 'BK%';
GO
--</snippetSelectExamples7>

--<snippetSelectExamples8>
USE AdventureWorks2012;
GO
IF OBJECT_ID('dbo.NewProducts', 'U') IS NOT NULL
    DROP TABLE dbo.NewProducts;
GO
ALTER DATABASE AdventureWorks2012 SET RECOVERY BULK_LOGGED;
GO

SELECT * INTO dbo.NewProducts
FROM Production.Product
WHERE ListPrice > $25 
AND ListPrice < $100;
GO
ALTER DATABASE AdventureWorks2012 SET RECOVERY FULL;
GO
--</snippetSelectExamples8>

--<snippetSelectExamples9>
USE AdventureWorks2012;
GO
SELECT DISTINCT Name
FROM Production.Product AS p 
WHERE EXISTS
    (SELECT *
     FROM Production.ProductModel AS pm 
     WHERE p.ProductModelID = pm.ProductModelID
           AND pm.Name LIKE 'Long-Sleeve Logo Jersey%');
GO

-- OR

USE AdventureWorks2012;
GO
SELECT DISTINCT Name
FROM Production.Product
WHERE ProductModelID IN
    (SELECT ProductModelID 
     FROM Production.ProductModel
     WHERE Name LIKE 'Long-Sleeve Logo Jersey%');
GO
--</snippetSelectExamples9>

--<snippetSelectExamples10>
USE AdventureWorks2012;
GO
SELECT DISTINCT p.LastName, p.FirstName 
FROM Person.Person AS p 
JOIN HumanResources.Employee AS e
    ON e.BusinessEntityID = p.BusinessEntityID WHERE 5000.00 IN
    (SELECT Bonus
     FROM Sales.SalesPerson AS sp
     WHERE e.BusinessEntityID = sp.BusinessEntityID);
GO
--</snippetSelectExamples10>

--<snippetSelectExamples11>
USE AdventureWorks2012;
GO
SELECT p1.ProductModelID
FROM Production.Product AS p1
GROUP BY p1.ProductModelID
HAVING MAX(p1.ListPrice) >= ALL
    (SELECT AVG(p2.ListPrice)
     FROM Production.Product AS p2
     WHERE p1.ProductModelID = p2.ProductModelID);
GO
--</snippetSelectExamples11>

--<snippetSelectExamples12>
USE AdventureWorks2012;
GO
SELECT DISTINCT pp.LastName, pp.FirstName 
FROM Person.Person pp JOIN HumanResources.Employee e
ON e.BusinessEntityID = pp.BusinessEntityID WHERE pp.BusinessEntityID IN 
(SELECT SalesPersonID 
FROM Sales.SalesOrderHeader
WHERE SalesOrderID IN 
(SELECT SalesOrderID 
FROM Sales.SalesOrderDetail
WHERE ProductID IN 
(SELECT ProductID 
FROM Production.Product p 
WHERE ProductNumber = 'BK-M68B-42')));
GO
--</snippetSelectExamples12>

--<snippetSelectExamples13>
USE AdventureWorks2012;
GO
SELECT SalesOrderID, SUM(LineTotal) AS SubTotal
FROM Sales.SalesOrderDetail
GROUP BY SalesOrderID
ORDER BY SalesOrderID;
GO
--</snippetSelectExamples13>

--<snippetSelectExamples14>
USE AdventureWorks2012;
GO
SELECT ProductID, SpecialOfferID, AVG(UnitPrice) AS [Average Price], 
    SUM(LineTotal) AS SubTotal
FROM Sales.SalesOrderDetail
GROUP BY ProductID, SpecialOfferID
ORDER BY ProductID;
GO
--</snippetSelectExamples14>

--<snippetSelectExamples15>
USE AdventureWorks2012;
GO
SELECT ProductModelID, AVG(ListPrice) AS [Average List Price]
FROM Production.Product
WHERE ListPrice > $1000
GROUP BY ProductModelID
ORDER BY ProductModelID;
GO
--</snippetSelectExamples15>

--<snippetSelectExamples16>
USE AdventureWorks2012;
GO
SELECT AVG(OrderQty) AS [Average Quantity], 
NonDiscountSales = (OrderQty * UnitPrice)
FROM Sales.SalesOrderDetail
GROUP BY (OrderQty * UnitPrice)
ORDER BY (OrderQty * UnitPrice) DESC;
GO
--</snippetSelectExamples16>

--<snippetSelectExamples17>
USE AdventureWorks2012;
GO
SELECT ProductID, AVG(UnitPrice) AS [Average Price]
FROM Sales.SalesOrderDetail
WHERE OrderQty > 10
GROUP BY ProductID
ORDER BY ProductID;
GO

-- Using GROUP BY ALL
USE AdventureWorks2012;
GO
SELECT ProductID, AVG(UnitPrice) AS [Average Price]
FROM Sales.SalesOrderDetail
WHERE OrderQty > 10
GROUP BY ALL ProductID
ORDER BY ProductID;
GO
--</snippetSelectExamples17>

--<snippetSelectExamples18>
USE AdventureWorks2012;
GO
SELECT ProductID, AVG(UnitPrice) AS [Average Price]
FROM Sales.SalesOrderDetail
WHERE OrderQty > 10
GROUP BY ProductID
ORDER BY AVG(UnitPrice);
GO
--</snippetSelectExamples18>

--<snippetSelectExamples19>
USE AdventureWorks2012;
GO
SELECT ProductID 
FROM Sales.SalesOrderDetail
GROUP BY ProductID
HAVING AVG(OrderQty) > 5
ORDER BY ProductID;
GO
--</snippetSelectExamples19>

--<snippetSelectExamples20>
USE AdventureWorks2012;
GO
SELECT Name, AVG(ListPrice) AS [Average List Price]
FROM Production.Product
GROUP BY Name
HAVING Name LIKE 'Mountain%'
ORDER BY Name;
GO
--</snippetSelectExamples20>

--<snippetSelectExamples21>
USE AdventureWorks2012;
GO
SELECT ProductID 
FROM Sales.SalesOrderDetail
WHERE UnitPrice < 25.00
GROUP BY ProductID
HAVING AVG(OrderQty) > 5
ORDER BY ProductID;
GO
--</snippetSelectExamples21>

--<snippetSelectExamples22>
USE AdventureWorks2012;
GO
SELECT ProductID, AVG(OrderQty) AS AverageQuantity, SUM(LineTotal) AS Total
FROM Sales.SalesOrderDetail
GROUP BY ProductID
HAVING SUM(LineTotal) > $1000000.00
AND AVG(OrderQty) < 3;
GO
--</snippetSelectExamples22>

--<snippetSelectExamples23>
USE AdventureWorks2012;
GO
SELECT ProductID, Total = SUM(LineTotal)
FROM Sales.SalesOrderDetail
GROUP BY ProductID
HAVING SUM(LineTotal) > $2000000.00;
GO
--</snippetSelectExamples23>

--<snippetSelectExamples24>
USE AdventureWorks2012;
GO
SELECT ProductID, SUM(LineTotal) AS Total
FROM Sales.SalesOrderDetail
GROUP BY ProductID
HAVING COUNT(*) > 1500;
GO
--</snippetSelectExamples24>

--<snippetSelectExamples25>
USE AdventureWorks2012;
GO
SELECT ProductID, LineTotal
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
ORDER BY ProductID, LineTotal
COMPUTE SUM(LineTotal) BY ProductID;
GO
--</snippetSelectExamples25>

--<snippetSelectExamples26>
USE AdventureWorks2012;
GO
SELECT ProductID, LineTotal
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
ORDER BY ProductID, LineTotal
COMPUTE SUM(LineTotal), MAX(LineTotal) BY ProductID;
GO
--</snippetSelectExamples26>

--<snippetSelectExamples27>
USE AdventureWorks2012;
GO
SELECT ProductID, OrderQty, UnitPrice, LineTotal
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $2.00
COMPUTE SUM(OrderQty), SUM(LineTotal);
GO
--</snippetSelectExamples27>

--<snippetSelectExamples28>
USE AdventureWorks2012;
GO
SELECT ProductID, OrderQty, UnitPrice, LineTotal
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
ORDER BY ProductID
COMPUTE SUM(OrderQty), SUM(LineTotal) BY ProductID
COMPUTE SUM(OrderQty), SUM(LineTotal);
GO
--</snippetSelectExamples28>

--<snippetSelectExamples29>
USE AdventureWorks2012;
GO
SELECT ProductID, OrderQty, LineTotal
FROM Sales.SalesOrderDetail
COMPUTE SUM(OrderQty), SUM(LineTotal);
GO
--</snippetSelectExamples29>

--<snippetSelectExamples30>
USE AdventureWorks2012;
GO
SELECT ProductID, OrderQty, UnitPrice, LineTotal
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
ORDER BY ProductID, OrderQty, LineTotal
COMPUTE SUM(LineTotal) BY ProductID, OrderQty
COMPUTE SUM(LineTotal) BY ProductID;
GO
--</snippetSelectExamples30>

--<snippetSelectExamples31>
USE AdventureWorks2012;
GO
SELECT ProductID, LineTotal
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
ORDER BY ProductID
COMPUTE SUM(LineTotal) BY ProductID;
GO
--</snippetSelectExamples31>

--<snippetSelectExamples32>
USE AdventureWorks2012;
GO
SELECT ProductID, SUM(LineTotal) AS Total
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
GROUP BY ProductID
ORDER BY ProductID;
GO
--</snippetSelectExamples32>

--<snippetSelectExamples33>
USE AdventureWorks2012;
GO
SELECT ProductID, OrderQty, SUM(LineTotal) AS Total
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
GROUP BY ProductID, OrderQty
ORDER BY ProductID, OrderQty
COMPUTE SUM(SUM(LineTotal)) BY ProductID, OrderQty
COMPUTE SUM(SUM(LineTotal));
GO
--</snippetSelectExamples33>

--<snippetSelectExamples34>
USE AdventureWorks2012;
GO
SELECT ProductID, SUM(LineTotal) AS Total
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
GROUP BY ProductID, OrderQty
WITH CUBE
ORDER BY ProductID;
GO
--</snippetSelectExamples34>

-- snippetSelectExamples35 and others that use CUBE or ROLLUP are not used in Katmai and higher 
--<snippetSelectExamples35>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('dbo.CubeExample', 'U') IS NOT NULL
DROP TABLE dbo.CubeExample;
GO
CREATE TABLE dbo.CubeExample(
ProductName varchar(30) NULL,
CustomerName varchar(30) NULL,
Orders int NULL
)

INSERT dbo.CubeExample (ProductName, CustomerName, Orders)
VALUES ('Filo Mix', 'Romero y tomillo', 10)
      ,('Outback Lager', 'Wilman Kala', 10)
      ,('Filo Mix', 'Romero y tomillo', 20)
      ,('Ikura', 'Wilman Kala', 10)
      ,('Ikura', 'Romero y tomillo', 10)
      ,('Outback Lager', 'Wilman Kala', 20)
      ,('Filo Mix', 'Wilman Kala', 30)
      ,('Ikura', 'Wilman Kala', 40)
      ,('Ikura', 'Romero y tomillo', 10)
      ,('Filo Mix', 'Romero y tomillo', 50);
GO
--</snippetSelectExamples35>

--<snippetSelectExamples36>
USE AdventureWorks2012;
GO
SELECT ProductName, CustomerName, SUM(Orders)
FROM CubeExample
GROUP BY ProductName, CustomerName
ORDER BY ProductName;
GO
--</snippetSelectExamples36>

--<snippetSelectExamples37>
USE AdventureWorks2012;
GO
SELECT ProductName, CustomerName, SUM(Orders) AS [Total Orders]
FROM CubeExample
GROUP BY ProductName, CustomerName
WITH CUBE;
GO
--</snippetSelectExamples37>

--<snippetSelectExamples38>
USE AdventureWorks2012;
GO
SELECT ProductModelID, p.Name AS ProductName, SUM(OrderQty) AS [Product Qty]
FROM Production.Product AS p 
INNER JOIN Sales.SalesOrderDetail AS sod
ON p.ProductID = sod.ProductID 
GROUP BY ProductModelID, p.Name
WITH CUBE;
GO
--</snippetSelectExamples38>

--<snippetSelectExamples39>
USE AdventureWorks2012;
GO
SELECT ProductModelID, GROUPING(ProductModelID), p.Name AS ProductName, GROUPING(p.Name), SUM(OrderQty)
FROM Production.Product AS p 
INNER JOIN Sales.SalesOrderDetail AS sod
ON p.ProductID = sod.ProductID 
GROUP BY ProductModelID, p.Name
WITH CUBE;
GO
--</snippetSelectExamples39>

--<snippetSelectExamples40>
USE AdventureWorks2012;
GO
SELECT ProductName, CustomerName, SUM(Orders) AS [Sum Orders]
FROM dbo.CubeExample
GROUP BY ProductName, CustomerName
WITH ROLLUP;
GO
--</snippetSelectExamples40>

--<snippetSelectExamples41>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('dbo.Personnel', 'U') IS NOT NULL
DROP TABLE dbo.Personnel;
GO
CREATE TABLE dbo.Personnel
(
    CompanyName VARCHAR(20) NOT NULL,
    Department   VARCHAR(15) NOT NULL,
    NumEmployees int NOT NULL
);
GO
INSERT dbo.Personnel 
VALUES ('Du monde entier', 'Finance', 10)
      ,('Du monde entier', 'Engineering', 40)
      ,('Du monde entier', 'Marketing', 40)
      ,('Piccolo und mehr', 'Accounting', 20)
      ,('Piccolo und mehr', 'Personnel', 30)
      ,('Piccolo und mehr', 'Payroll', 40);
GO
--</snippetSelectExamples41>

--<snippetSelectExamples42>
USE AdventureWorks2012;
GO
SELECT CompanyName, Department, SUM(NumEmployees)
FROM dbo.Personnel
GROUP BY CompanyName, Department WITH ROLLUP;
GO
--</snippetSelectExamples42>

--<snippetSelectExamples43>
USE AdventureWorks2012;
GO
-- Add first row with a NULL customer name and 0 orders.
INSERT dbo.CubeExample (ProductName, CustomerName, Orders)
VALUES ('Ikura', NULL, 0)

-- Add second row with a NULL product and NULL customer with real value 
-- for orders.
INSERT dbo.CubeExample (ProductName, CustomerName, Orders)
VALUES (NULL, NULL, 50)

-- Add third row with a NULL product, NULL order amount, but a real 
-- customer name.
INSERT dbo.CubeExample (ProductName, CustomerName, Orders)
VALUES (NULL, 'Wilman Kala', NULL)

SELECT ProductName AS Prod, CustomerName AS Cust, 
SUM(Orders) AS 'Sum Orders',
GROUPING(ProductName) AS 'Group ProductName',
GROUPING(CustomerName) AS 'Group CustomerName'
FROM CubeExample
GROUP BY ProductName, CustomerName
WITH ROLLUP;
GO
--</snippetSelectExamples43>

--<snippetSelectExamples44>
USE AdventureWorks2012;
GO
SELECT pm.Name AS ProductModel, p.Name AS ProductName, SUM(OrderQty)
FROM Production.ProductModel AS pm
INNER JOIN Production.Product AS p 
ON pm.ProductModelID = p.ProductModelID
INNER JOIN Sales.SalesOrderDetail AS sod
ON p.ProductID = sod.ProductID 
GROUP BY pm.Name, p.Name
WITH ROLLUP;
GO
--</snippetSelectExamples44>

--<snippetSelectExamples45>
USE AdventureWorks2012;
GO
SELECT pp.FirstName, pp.LastName, e.NationalIDNumber
FROM HumanResources.Employee AS e WITH (INDEX(AK_Employee_NationalIDNumber))
JOIN Person.Person AS pp on e.BusinessEntityID = pp.BusinessEntityID
WHERE LastName = 'Johnson';
GO

-- Force a table scan by using INDEX = 0.
USE AdventureWorks2012;
GO
SELECT pp.LastName, pp.FirstName, e.JobTitle
FROM HumanResources.Employee AS e WITH (INDEX = 0) JOIN Person.Person AS pp
ON e.BusinessEntityID = pp.BusinessEntityID
WHERE LastName = 'Johnson';
GO
--</snippetSelectExamples45>

--<snippetSelectExamples46>
USE AdventureWorks2012;
GO
SELECT ProductID, OrderQty, SUM(LineTotal) AS Total
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
GROUP BY ProductID, OrderQty
ORDER BY ProductID, OrderQty
OPTION (HASH GROUP, FAST 10);
GO
--</snippetSelectExamples46>

--<snippetSelectExamples47>
USE AdventureWorks2012;
GO
SELECT BusinessEntityID, JobTitle, HireDate, VacationHours, SickLeaveHours
FROM HumanResources.Employee AS e1
UNION
SELECT BusinessEntityID, JobTitle, HireDate, VacationHours, SickLeaveHours
FROM HumanResources.Employee AS e2
OPTION (MERGE UNION);
GO
--</snippetSelectExamples47>

--<snippetSelectExamples48>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('dbo.Gloves', 'U') IS NOT NULL
DROP TABLE dbo.Gloves;
GO
-- Create Gloves table.
SELECT ProductModelID, Name
INTO dbo.Gloves
FROM Production.ProductModel
WHERE ProductModelID IN (3, 4);
GO

-- Here is the simple union.
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name
FROM Production.ProductModel
WHERE ProductModelID NOT IN (3, 4)
UNION
SELECT ProductModelID, Name
FROM dbo.Gloves
ORDER BY Name;
GO
--</snippetSelectExamples48>

--<snippetSelectExamples49>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('dbo.ProductResults', 'U') IS NOT NULL
DROP TABLE dbo.ProductResults;
GO
IF OBJECT_ID ('dbo.Gloves', 'U') IS NOT NULL
DROP TABLE dbo.Gloves;
GO
-- Create Gloves table.
SELECT ProductModelID, Name
INTO dbo.Gloves
FROM Production.ProductModel
WHERE ProductModelID IN (3, 4);
GO

USE AdventureWorks2012;
GO
SELECT ProductModelID, Name
INTO dbo.ProductResults
FROM Production.ProductModel
WHERE ProductModelID NOT IN (3, 4)
UNION
SELECT ProductModelID, Name
FROM dbo.Gloves;
GO

SELECT ProductModelID, Name 
FROM dbo.ProductResults;
--</snippetSelectExamples49>

--<snippetSelectExamples50>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('dbo.Gloves', 'U') IS NOT NULL
DROP TABLE dbo.Gloves;
GO
-- Create Gloves table.
SELECT ProductModelID, Name
INTO dbo.Gloves
FROM Production.ProductModel
WHERE ProductModelID IN (3, 4);
GO

/* INCORRECT */
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name
FROM Production.ProductModel
WHERE ProductModelID NOT IN (3, 4)
ORDER BY Name
UNION
SELECT ProductModelID, Name
FROM dbo.Gloves;
GO

/* CORRECT */
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name
FROM Production.ProductModel
WHERE ProductModelID NOT IN (3, 4)
UNION
SELECT ProductModelID, Name
FROM dbo.Gloves
ORDER BY Name;
GO
--</snippetSelectExamples50>

--<snippetSelectExamples51>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('dbo.EmployeeOne', 'U') IS NOT NULL
DROP TABLE dbo.EmployeeOne;
GO
IF OBJECT_ID ('dbo.EmployeeTwo', 'U') IS NOT NULL
DROP TABLE dbo.EmployeeTwo;
GO
IF OBJECT_ID ('dbo.EmployeeThree', 'U') IS NOT NULL
DROP TABLE dbo.EmployeeThree;
GO

SELECT pp.LastName, pp.FirstName, e.JobTitle 
INTO dbo.EmployeeOne
FROM Person.Person AS pp JOIN HumanResources.Employee AS e
ON e.BusinessEntityID = pp.BusinessEntityID
WHERE LastName = 'Johnson';
GO
SELECT pp.LastName, pp.FirstName, e.JobTitle 
INTO dbo.EmployeeTwo
FROM Person.Person AS pp JOIN HumanResources.Employee AS e
ON e.BusinessEntityID = pp.BusinessEntityID
WHERE LastName = 'Johnson';
GO
SELECT pp.LastName, pp.FirstName, e.JobTitle 
INTO dbo.EmployeeThree
FROM Person.Person AS pp JOIN HumanResources.Employee AS e
ON e.BusinessEntityID = pp.BusinessEntityID
WHERE LastName = 'Johnson';
GO
-- Union ALL
SELECT LastName, FirstName, JobTitle
FROM dbo.EmployeeOne
UNION ALL
SELECT LastName, FirstName ,JobTitle
FROM dbo.EmployeeTwo
UNION ALL
SELECT LastName, FirstName,JobTitle 
FROM dbo.EmployeeThree;
GO

SELECT LastName, FirstName,JobTitle
FROM dbo.EmployeeOne
UNION 
SELECT LastName, FirstName, JobTitle 
FROM dbo.EmployeeTwo
UNION 
SELECT LastName, FirstName, JobTitle 
FROM dbo.EmployeeThree;
GO

SELECT LastName, FirstName,JobTitle 
FROM dbo.EmployeeOne
UNION ALL
(
SELECT LastName, FirstName, JobTitle 
FROM dbo.EmployeeTwo
UNION
SELECT LastName, FirstName, JobTitle 
FROM dbo.EmployeeThree
);
GO
--</snippetSelectExamples51>

--<snippetSelectExamples52>

USE AdventureWorks2012;
GO
SELECT *
FROM Sales.SalesOrderHeader AS h
INNER JOIN Sales.SalesOrderDetail AS d 
    ON h.SalesOrderID = d.SalesOrderID 
WHERE h.TotalDue > 100
AND (d.OrderQty > 5 OR d.LineTotal < 1000.00);
GO
--</snippetSelectExamples52>

--<snippetSelectExamples53_ktm>

USE AdventureWorks2012;
GO
SELECT *
FROM Sales.SalesOrderHeader AS h
INNER JOIN Sales.SalesOrderDetail AS d WITH (FORCESEEK)
    ON h.SalesOrderID = d.SalesOrderID 
WHERE h.TotalDue > 100
AND (d.OrderQty > 5 OR d.LineTotal < 1000.00);
GO

--</snippetSelectExamples53_ktm>

--<snippetSelectExamples54>
USE AdventureWorks2012;
GO
SELECT GroupName
FROM HumanResources.Department
WHERE Name = 'Engineering';
GO
--</snippetSelectExamples54>

--<snippetSelectExamples55>
USE AdventureWorks2012;
GO
DECLARE @p AS int = 10;
SELECT TOP(@p)JobTitle, HireDate, VacationHours
FROM HumanResources.Employee
ORDER BY VacationHours DESC
GO
--</snippetSelectExamples55>

--<snippetSelectExamples56>
USE AdventureWorks2012;
GO
SELECT TOP(10)WITH TIES
pp.FirstName, pp.LastName, e.JobTitle, e.Gender, r.Rate
FROM Person.Person AS pp 
    INNER JOIN HumanResources.Employee AS e
        ON pp.BusinessEntityID = e.BusinessEntityID
    INNER JOIN HumanResources.EmployeePayHistory AS r
        ON r.BusinessEntityID = e.BusinessEntityID
ORDER BY Rate DESC;
--</snippetSelectExamples56>


--<snippetSelectExamples57>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('Person.USAddress') IS NOT NULL
DROP TABLE Person.USAddress;
GO
-- Determine the IDENTITY status of the source column AddressID.
SELECT OBJECT_NAME(object_id) AS TableName, name AS column_name, is_identity, seed_value, increment_value
FROM sys.identity_columns
WHERE name = 'AddressID';

-- Create a new table with columns from the existing table Person.Address. A new IDENTITY
-- column is created by using the IDENTITY function.
SELECT IDENTITY (int, 100, 5) AS AddressID, 
       a.AddressLine1, a.City, b.Name AS State, a.PostalCode
INTO Person.USAddress 
FROM Person.Address AS a
INNER JOIN Person.StateProvince AS b ON a.StateProvinceID = b.StateProvinceID
WHERE b.CountryRegionCode = N'US'; 

-- Verify the IDENTITY status of the AddressID columns in both tables.
SELECT OBJECT_NAME(object_id) AS TableName, name AS column_name, is_identity, seed_value, increment_value
FROM sys.identity_columns
WHERE name = 'AddressID';
--</snippetSelectExamples57>

--<snippetSelectExamples58>
USE master;
GO
-- Create a link to the remote data source. 
-- Specify a valid server name for @datasrc as 'server_name' or 'server_name\instance_name'.
EXEC sp_addlinkedserver @server = N'MyLinkServer',
    @srvproduct = N' ',
    @provider = N'SQLNCLI', 
    @datasrc = N'server_name',
    @catalog = N'AdventureWorks2012';
GO
USE AdventureWorks2012;
GO
-- Specify the remote data source in the FROM clause using a four-part name 
-- in the form linked_server.catalog.schema.object.
SELECT DepartmentID, Name, GroupName, ModifiedDate
INTO dbo.Departments
FROM MyLinkServer.AdventureWorks2012.HumanResources.Department
GO
-- Use the OPENQUERY function to access the remote data source.
SELECT DepartmentID, Name, GroupName, ModifiedDate
INTO dbo.DepartmentsUsingOpenQuery
FROM OPENQUERY(MyLinkServer, 'SELECT *
               FROM AdventureWorks2012.HumanResources.Department'); 
GO
-- Use the OPENDATASOURCE function to specify the remote data source.
-- Specify a valid server name for Data Source using the format server_name or server_name\instance_name.
SELECT DepartmentID, Name, GroupName, ModifiedDate
INTO dbo.DepartmentsUsingOpenDataSource
FROM OPENDATASOURCE('SQLNCLI',
    'Data Source=server_name;Integrated Security=SSPI')
    .AdventureWorks2012.HumanResources.Department;
GO
--</snippetSelectExamples58>
--<snippetSelectExamples59>
USE AdventureWorks2012;
GO
-- Select the first 10 random employees.
SELECT TOP(10)JobTitle, HireDate
FROM HumanResources.Employee;
GO
-- Select the first 10 employees hired most recently.
SELECT TOP(10)JobTitle, HireDate
FROM HumanResources.Employee
ORDER BY HireDate DESC;
--</snippetSelectExamples59>

--<snippetSelectExamples60>
USE AdventureWorks2012;
GO
SELECT TOP(5)PERCENT JobTitle, HireDate
FROM HumanResources.Employee
ORDER BY HireDate DESC;
--</snippetSelectExamples60>
