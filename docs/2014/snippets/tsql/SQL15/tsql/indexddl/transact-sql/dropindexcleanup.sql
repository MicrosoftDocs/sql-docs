--Restore indexes dropped in the DropIndex.sql script
USE AdventureWorks2012;
GO
CREATE NONCLUSTERED INDEX IX_ProductVendor_VendorID 
    ON Purchasing.ProductVendor (VendorID) ON [PRIMARY];
GO
CREATE NONCLUSTERED INDEX IX_PurchaseOrderHeader_EmployeeID 
    ON Purchasing.PurchaseOrderHeader (EmployeeID) ON [PRIMARY];
GO
CREATE NONCLUSTERED INDEX IX_VendorAddress_AddressID 
    ON Purchasing.VendorAddress (AddressID) ON [PRIMARY];
GO
CREATE UNIQUE CLUSTERED INDEX AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate 
ON Production.BillOfMaterials(ProductAssemblyID, ComponentID, StartDate) ON [PRIMARY];
GO
ALTER DATABASE AdventureWorks2012
REMOVE FILE File1;
GO 
ALTER DATABASE AdventureWorks2012
REMOVE FILEGROUP NewGroup;
GO 
ALTER TABLE Production.ProductCostHistory WITH NOCHECK
ADD CONSTRAINT PK_ProductCostHistory_ProductID_StartDate
  PRIMARY KEY CLUSTERED (ProductID, StartDate) ON [PRIMARY];    
GO
CREATE PRIMARY XML INDEX PXML_ProductModel_CatalogDescription 
    ON Production.ProductModel (CatalogDescription);
GO
  
