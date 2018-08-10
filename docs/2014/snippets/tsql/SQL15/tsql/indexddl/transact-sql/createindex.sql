--<snippetCreateIndex1>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_ProductVendor_VendorID')
    DROP INDEX IX_ProductVendor_VendorID ON Purchasing.ProductVendor;
GO
CREATE INDEX IX_ProductVendor_VendorID 
    ON Purchasing.ProductVendor (BusinessEntityID); 
GO
--</snippetCreateIndex1>

--<snippetCreateIndex2>
USE AdventureWorks2012
GO
IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_SalesPerson_SalesQuota_SalesYTD')
    DROP INDEX IX_SalesPerson_SalesQuota_SalesYTD ON Sales.SalesPerson ;
GO
CREATE NONCLUSTERED INDEX IX_SalesPerson_SalesQuota_SalesYTD
    ON Sales.SalesPerson (SalesQuota, SalesYTD);
GO
--</snippetCreateIndex2>

--<snippetCreateIndex3>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT name from sys.indexes
             WHERE name = N'AK_UnitMeasure_Name')
    DROP INDEX AK_UnitMeasure_Name ON Production.UnitMeasure;
GO
CREATE UNIQUE INDEX AK_UnitMeasure_Name 
    ON Production.UnitMeasure(Name);
GO
--</snippetCreateIndex3>

--<snippetCreateIndex4>
USE AdventureWorks2012;
GO
CREATE NONCLUSTERED INDEX IX_WorkOrder_ProductID
    ON Production.WorkOrder(ProductID)
    WITH (FILLFACTOR = 80,
        PAD_INDEX = ON,
        DROP_EXISTING = ON);
GO
--</snippetCreateIndex4>

--<snippetCreateIndex5>
USE AdventureWorks2012;
GO
--Set the options to support indexed views.
SET NUMERIC_ROUNDABORT OFF;
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT,
    QUOTED_IDENTIFIER, ANSI_NULLS ON;
GO
--Create view with schemabinding.
IF OBJECT_ID ('Sales.vOrders', 'view') IS NOT NULL
DROP VIEW Sales.vOrders ;
GO
CREATE VIEW Sales.vOrders
WITH SCHEMABINDING
AS
    SELECT SUM(UnitPrice*OrderQty*(1.00-UnitPriceDiscount)) AS Revenue,
        OrderDate, ProductID, COUNT_BIG(*) AS COUNT
    FROM Sales.SalesOrderDetail AS od, Sales.SalesOrderHeader AS o
    WHERE od.SalesOrderID = o.SalesOrderID
    GROUP BY OrderDate, ProductID;
GO
--Create an index on the view.
CREATE UNIQUE CLUSTERED INDEX IDX_V1 
    ON Sales.vOrders (OrderDate, ProductID);
GO
--This query can use the indexed view even though the view is 
--not specified in the FROM clause.
SELECT SUM(UnitPrice*OrderQty*(1.00-UnitPriceDiscount)) AS Rev, 
    OrderDate, ProductID
FROM Sales.SalesOrderDetail AS od
    JOIN Sales.SalesOrderHeader AS o ON od.SalesOrderID=o.SalesOrderID
        AND ProductID BETWEEN 700 and 800
        AND OrderDate >= CONVERT(datetime,'05/01/2002',101)
GROUP BY OrderDate, ProductID
ORDER BY Rev DESC;
GO
--This query can use the above indexed view.
SELECT  OrderDate, SUM(UnitPrice*OrderQty*(1.00-UnitPriceDiscount)) AS Rev
FROM Sales.SalesOrderDetail AS od
    JOIN Sales.SalesOrderHeader AS o ON od.SalesOrderID=o.SalesOrderID
        AND DATEPART(mm,OrderDate)= 3
        AND DATEPART(yy,OrderDate) = 2002
GROUP BY OrderDate
ORDER BY OrderDate ASC;
GO
--</snippetCreateIndex5>

--<snippetCreateIndex6>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_Address_PostalCode')
    DROP INDEX IX_Address_PostalCode ON Person.Address;
GO
CREATE NONCLUSTERED INDEX IX_Address_PostalCode
    ON Person.Address (PostalCode)
    INCLUDE (AddressLine1, AddressLine2, City, StateProvinceID);
GO
SELECT AddressLine1, AddressLine2, City, StateProvinceID, PostalCode
FROM Person.Address
WHERE PostalCode BETWEEN N'98000' and N'99999';
GO

--</snippetCreateIndex6>

--<snippetCreateIndex7>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT * FROM sys.indexes
            WHERE name = N'PXML_ProductModel_CatalogDescription')
    DROP INDEX PXML_ProductModel_CatalogDescription 
        ON Production.ProductModel;
GO
CREATE PRIMARY XML INDEX PXML_ProductModel_CatalogDescription
    ON Production.ProductModel (CatalogDescription);
GO
--</snippetCreateIndex7>

--<snippetCreateIndex8>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IXML_ProductModel_CatalogDescription_Path')
    DROP INDEX IXML_ProductModel_CatalogDescription_Path
        ON Production.ProductModel;
GO
CREATE XML INDEX IXML_ProductModel_CatalogDescription_Path 
    ON Production.ProductModel (CatalogDescription)
    USING XML INDEX PXML_ProductModel_CatalogDescription FOR PATH ;
GO
--</snippetCreateIndex8>

--<snippetCreateIndex9>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_ProductVendor_VendorID')
    DROP INDEX IX_ProductVendor_VendorID ON Purchasing.ProductVendor;
GO
CREATE INDEX IX_ProductVendor_VendorID 
ON Purchasing.ProductVendor (BusinessEntityID)
WITH (MAXDOP=8);
GO
--</snippetCreateIndex9>

--<snippetCreateIndex10>
-- Create a covering index by adding GroupName to the nonclustered index.
USE AdventureWorks2012;
GO
CREATE UNIQUE NONCLUSTERED INDEX AK_Department_Name
    ON HumanResources.Department ( Name ASC, GroupName)
    WITH (DROP_EXISTING = ON);
GO
--</snippetCreateIndex10>

--<snippetCreateIndex11>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT name FROM sys.indexes
    WHERE name = N'FIBillOfMaterialsWithEndDate' 
    AND object_id = OBJECT_ID(N'Production.BillOfMaterials'))
DROP INDEX FIBillOfMaterialsWithEndDate
    ON Production.BillOfMaterials;
GO
CREATE NONCLUSTERED INDEX "FIBillOfMaterialsWithEndDate"
    ON Production.BillOfMaterials (ComponentID, StartDate)
    WHERE EndDate IS NOT NULL;
GO
--</snippetCreateIndex11>



