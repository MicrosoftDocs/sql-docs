
--<snippetCreateStats1>
USE AdventureWorks2012;
GO
CREATE STATISTICS ContactMail1
    ON Person.Person (BusinessEntityID, EmailPromotion)
    WITH SAMPLE 5 PERCENT;
--</snippetCreateStats1>

--<snippetCreateStats2>
CREATE STATISTICS NamePurchase
    ON AdventureWorks2012.Person.Person (BusinessEntityID, EmailPromotion)
    WITH FULLSCAN, NORECOMPUTE;
--</snippetCreateStats2>

--<snippetCreateStats3>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT name FROM sys.stats
    WHERE name = N'ContactPromotion1'
    AND object_id = OBJECT_ID(N'Person.Person'))
DROP STATISTICS Person.Person.ContactPromotion1
GO
CREATE STATISTICS ContactPromotion1
    ON Person.Person (BusinessEntityID, LastName, EmailPromotion)
WHERE EmailPromotion = 2
WITH SAMPLE 50 PERCENT;
GO

--</snippetCreateStats3>

--<snippetUpdateStats1>
USE AdventureWorks2012;
GO
UPDATE STATISTICS Sales.SalesOrderDetail;
GO
--</snippetUpdateStats1>

--<snippetUpdateStats2>
USE AdventureWorks2012;
GO
UPDATE STATISTICS Sales.SalesOrderDetail AK_SalesOrderDetail_rowguid;
GO
--</snippetUpdateStats2>

--<snippetUpdateStats3>
USE AdventureWorks2012;
GO
CREATE STATISTICS Products
    ON Production.Product ([Name], ProductNumber)
    WITH SAMPLE 50 PERCENT
-- Time passes. The UPDATE STATISTICS statement is then executed.
UPDATE STATISTICS Production.Product(Products) 
    WITH SAMPLE 50 PERCENT;
--</snippetUpdateStats3>

--<snippetUpdateStats4>
USE AdventureWorks2012;
GO
UPDATE STATISTICS Production.Product(Products)
    WITH FULLSCAN, NORECOMPUTE;
GO
--</snippetUpdateStats4>

--<snippetDropStats1>
-- Create the statistics groups.
USE AdventureWorks2012;
GO
CREATE STATISTICS VendorCredit
    ON Purchasing.Vendor (Name, CreditRating)
    WITH SAMPLE 50 PERCENT
CREATE STATISTICS CustomerTotal
    ON Sales.SalesOrderHeader (CustomerID, TotalDue)
    WITH FULLSCAN;
GO
DROP STATISTICS Purchasing.Vendor.VendorCredit, Sales.SalesOrderHeader.CustomerTotal;
--</snippetDropStats1>
