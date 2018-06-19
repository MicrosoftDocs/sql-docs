--<snippetAWSelect4>
USE AdventureWorks2012;
GO
SELECT s.Name AS Store, p.FirstName, p.LastName, ct.Name AS Title 
FROM Sales.Store AS s
INNER JOIN Person.BusinessEntityContact AS bec 
    ON bec.BusinessEntityID = s.BusinessEntityID
INNER JOIN Person.ContactType AS ct
	ON ct.ContactTypeID = bec.ContactTypeID
INNER JOIN Person.Person AS p
	ON p.BusinessEntityID = bec.PersonID
ORDER BY s.Name;
GO
--</snippetAWSelect4>

--<snippetAWSelect5>
USE AdventureWorks2012;
GO
SELECT Name, SalesOrderNumber, OrderDate, TotalDue 
FROM Sales.Customer AS c
JOIN Sales.SalesOrderHeader AS so ON c.CustomerID = so.CustomerID
JOIN Sales.Store AS s ON s.BusinessEntityID = c.StoreID
ORDER BY Name, OrderDate;
GO
--</snippetAWSelect5>

--<snippetAWSelect6>
USE AdventureWorks2012;
GO
SELECT s.BusinessEntityID, s.Name AS Store, a.City, sp.Name AS State, cr.Name
    AS CountryRegion
FROM [Sales].[Store] AS s
INNER JOIN [Person].[BusinessEntityAddress]AS bea 
    ON bea.[BusinessEntityID] = s.[BusinessEntityID] 
INNER JOIN [Person].[Address] AS a 
    ON a.[AddressID] = bea.[AddressID]
INNER JOIN [Person].[StateProvince] AS sp 
    ON sp.[StateProvinceID] = a.[StateProvinceID]
INNER JOIN [Person].[CountryRegion] AS cr 
    ON cr.[CountryRegionCode] = sp.[CountryRegionCode]
INNER JOIN [Person].[AddressType] AS at 
    ON at.[AddressTypeID] = bea.[AddressTypeID]
ORDER BY s.BusinessEntityID ;
GO
--</snippetAWSelect6>
--<snippetAWSelect7>
USE AdventureWorks2012;
GO
SELECT PC.Name AS Category, PSC.Name AS Subcategory,
    PM.Name AS Model, P.Name AS Product
FROM Production.Product AS P
    FULL JOIN Production.ProductModel AS PM ON PM.ProductModelID = P.ProductModelID
    FULL JOIN Production.ProductSubcategory AS PSC ON PSC.ProductSubcategoryID = P.ProductSubcategoryID
    JOIN Production.ProductCategory AS PC ON PC.ProductCategoryID = PSC.ProductCategoryID
ORDER BY PC.Name, PSC.Name ;
GO
--</snippetAWSelect7>
--<snippetAWSelect8>
USE AdventureWorks2012;
GO
SELECT PM.ProductModelID, PM.Name AS [Product Model], Description, PL.CultureID, CL.Name AS Language
FROM Production.ProductModel AS PM 
    JOIN Production.ProductModelProductDescriptionCulture AS PL 
        ON PM.ProductModelID = PL.ProductModelID
    JOIN Production.Culture AS CL ON CL.CultureID = PL.CultureID
    JOIN Production.ProductDescription AS PD 
        ON PD.ProductDescriptionID = PL.ProductDescriptionID
ORDER BY PM.ProductModelID ;
GO
--</snippetAWSelect8>
--<snippetAWSelect9>
USE AdventureWorks2012 ;
GO
WITH Parts(AssemblyID, ComponentID, PerAssemblyQty, EndDate, ComponentLevel) AS
(
    SELECT b.ProductAssemblyID, b.ComponentID, b.PerAssemblyQty,
        b.EndDate, 0 AS ComponentLevel
    FROM Production.BillOfMaterials AS b
    WHERE b.ProductAssemblyID = 800
          AND b.EndDate IS NULL
    UNION ALL
    SELECT bom.ProductAssemblyID, bom.ComponentID, p.PerAssemblyQty,
        bom.EndDate, ComponentLevel + 1
    FROM Production.BillOfMaterials AS bom 
        INNER JOIN Parts AS p
        ON bom.ProductAssemblyID = p.ComponentID
        AND bom.EndDate IS NULL
)
SELECT AssemblyID, ComponentID, Name, PerAssemblyQty, EndDate,
        ComponentLevel 
FROM Parts AS p
    INNER JOIN Production.Product AS pr
    ON p.ComponentID = pr.ProductID
ORDER BY ComponentLevel, AssemblyID, ComponentID;
GO
--</snippetAWSelect9>
--<snippetAWSelect10>
USE AdventureWorks2012;
GO
SELECT P.Name AS Product, L.Name AS [Inventory Location],
    SUM(PI.Quantity)AS [Qty Available]
FROM Production.Product AS P
    JOIN Production.ProductInventory AS PI ON P.ProductID = PI.ProductID
    JOIN Production.Location AS L ON PI.LocationID = L.LocationID
GROUP BY P.Name, L.Name
ORDER BY P.Name ;
GO
--</snippetAWSelect10>
--<snippetAWSelect11>
USE AdventureWorks2012;
GO
SELECT WorkOrderID, P.Name AS Product, OrderQty, DueDate
FROM Production.WorkOrder W 
    JOIN Production.Product P ON W.ProductID = P.ProductID
WHERE P.ProductSubcategoryID IN (1, 2, 3)
ORDER BY P.Name, DueDate;
GO
--</snippetAWSelect11>
--<snippetAWSelect12>
USE AdventureWorks2012;
GO
SELECT V.BusinessEntityID, V.Name AS Vendor, PA.AddressLine1, PA.AddressLine2, PA.City, SP.Name AS State, CR.Name AS CountryRegion
FROM Purchasing.Vendor AS V 
	JOIN Person.BusinessEntityAddress AS BEA on BEA.BusinessEntityID = V.BusinessEntityID
    JOIN Person.Address AS PA ON PA.AddressID = BEA.AddressID
    JOIN Person.StateProvince AS SP on SP.StateProvinceID = PA.StateProvinceID
    JOIN Person.CountryRegion AS CR ON CR.CountryRegionCode = SP.CountryRegionCode
GROUP BY V.BusinessEntityID, V.Name, PA.AddressLine1, PA.AddressLine2, PA.City, SP.Name, CR.Name
ORDER BY V.BusinessEntityID;
GO
--</snippetAWSelect12>
--<snippetAWSelect13>

USE AdventureWorks2012;
GO
SELECT P.ProductNumber, P.Name AS Product, V.Name AS Vendor, PV.LastReceiptCost
FROM Production.Product AS P
    JOIN Purchasing.ProductVendor AS PV ON P.ProductID = PV.ProductID
    JOIN Purchasing.Vendor AS V ON V.BusinessEntityID = PV.BusinessEntityID
ORDER BY P.Name ;
GO
--</snippetAWSelect13>
--<snippetAWSelect14>
USE AdventureWorks2012;
GO
SELECT v.Name as Vendor, p.FirstName, p.LastName, ct.Name AS JobTitle
FROM Purchasing.Vendor AS v
INNER JOIN Person.BusinessEntityContact AS bec 
    ON bec.BusinessEntityID = v.BusinessEntityID
INNER JOIN Person.ContactType ct
	ON ct.ContactTypeID = bec.ContactTypeID
INNER JOIN Person.Person p
	ON p.BusinessEntityID = bec.PersonID
ORDER BY v.Name;
GO
--</snippetAWSelect14>
--<snippetAWSelect15>
USE AdventureWorks2012;
GO
SELECT V.Name AS Vendor, SUM(PH.TotalDue)AS [Total Purchase],
    AVG(PH.TotalDue)AS [Average Purchase], MIN(PH.TotalDue) 
    AS [Minimum Purchase], MAX(PH.TotalDue)AS [Maximum Purchase] 
FROM Purchasing.Vendor AS V
JOIN Purchasing.PurchaseOrderHeader AS PH ON V.BusinessEntityID = PH.VendorID
GROUP BY V.Name
ORDER BY V.Name;
GO
--</snippetAWSelect15>

