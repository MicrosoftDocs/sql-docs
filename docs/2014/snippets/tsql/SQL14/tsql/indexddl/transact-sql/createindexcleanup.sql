USE AdventureWorks2012;
GO
IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_SalesPerson_SalesQuota_SalesYTD')
    DROP INDEX IX_SalesPerson_SalesQuota_SalesYTD ON Sales.SalesPerson ;
GO
IF OBJECT_ID ('Sales.vOrders', 'view') IS NOT NULL
DROP VIEW Sales.vOrders;
GO
IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_Address_PostalCode')
    DROP INDEX IX_Address_PostalCode ON Person.Address;
GO
IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IXML_ProductModel_CatalogDescription_Path')
    DROP INDEX IXML_ProductModel_CatalogDescription_Path
        ON Production.ProductModel;
GO
