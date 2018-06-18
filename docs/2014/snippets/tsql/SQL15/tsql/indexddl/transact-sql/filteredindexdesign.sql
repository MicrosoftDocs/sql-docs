--<snippetFilteredIndexDesign1>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT name FROM sys.indexes
    WHERE name = N'FIBillOfMaterialsWithEndDate'
    AND object_id = OBJECT_ID (N'Production.BillOfMaterials'))
DROP INDEX FIBillOfMaterialsWithEndDate
    ON Production.BillOfMaterials
GO
CREATE NONCLUSTERED INDEX FIBillOfMaterialsWithEndDate
    ON Production.BillOfMaterials (ComponentID, StartDate)
WHERE EndDate IS NOT NULL;
GO
--</snippetFilteredIndexDesign1>


--<snippetFilteredIndexDesign2>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT name FROM sys.indexes
    WHERE name = N'FIProductAccessories'
    AND object_id = OBJECT_ID ('Production.Product'))
DROP INDEX FIProductAccessories
    ON Production.Product;
GO
CREATE NONCLUSTERED INDEX FIProductAccessories
    ON Production.Product (Name, ListPrice)
WHERE ProductSubcategoryID >= 27 AND ProductSubcategoryID <= 36;
GO
--</snippetFilteredIndexDesign2>

--<snippetFilteredIndexDesign3>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('ViewOnBillOfMaterials') IS NOT NULL
DROP VIEW ViewOnBillOfMaterials;
GO
CREATE VIEW ViewOnBillOfMaterials AS 
SELECT ComponentID, StartDate, EndDate, StartDate + 2 AS ShipDate
FROM Production.BillOfMaterials
WHERE StartDate > '20000401';
GO
IF EXISTS (SELECT name FROM sys.indexes
    WHERE name = N'FIBillOfMaterialsByStartDate'
    AND object_ID = OBJECT_ID (N'Production.BillOfMaterials'))
DROP INDEX FIBillOfMaterialsByStartDate 
    ON Production.BillOfMaterials;
GO
CREATE NONCLUSTERED INDEX FIBillOfMaterialsByStartDate
    ON Production.BillOfMaterials (ComponentID, StartDate, EndDate)
WHERE StartDate > '20000801';
GO
--</snippetFilteredIndexDesign3>

--<snippetFilteredIndexDesign4>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('dbo.TestTable') IS NOT NULL
DROP TABLE dbo.TestTable;
GO
CREATE TABLE TestTable (a int, b varbinary(4));
GO
--</snippetFilteredIndexDesign4>

--<snippetFilteredIndexDesign5>
USE AdventureWorks2012;
GO
IF EXISTS ( SELECT name FROM sys.indexes
    WHERE name = N'FIBillOfMaterialsWithComponentID'
    AND object_id = OBJECT_ID (N'Production.BillOfMaterials'))
DROP INDEX FIBillOfMaterialsWithComponentID
    ON Production.BillOfMaterials;
GO
CREATE NONCLUSTERED INDEX FIBillOfMaterialsWithComponentID
    ON Production.BillOfMaterials (ComponentID, StartDate, EndDate)
WHERE ComponentID IN (533, 324, 753);
GO
SET SHOWPLAN_XML ON;
GO
DECLARE @p AS INT, @q AS INT;
SET @p = 533;
SET @q = 324;
SELECT StartDate, ComponentID from Production.BillOfMaterials 
WHERE ComponentID = @p OR ComponentID = @q;
GO
SET SHOWPLAN_XML OFF;
GO
--</snippetFilteredIndexDesign5>
