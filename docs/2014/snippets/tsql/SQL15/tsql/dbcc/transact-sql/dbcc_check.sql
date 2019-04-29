--<snippetDBCCCheck1>
-- Check the current database.
DBCC CHECKALLOC;
GO
-- Check the AdventureWorks2012 database.
DBCC CHECKALLOC (AdventureWorks2012);
GO
--</snippetDBCCCheck1>
--<snippetDBCCCheck2>
-- Check the current database.
DBCC CHECKCATALOG;
GO
-- Check the AdventureWorks2012 database.
DBCC CHECKCATALOG (AdventureWorks2012);
GO
--</snippetDBCCCheck2>
--<snippetDBCCCheck3>
USE AdventureWorks2012;
GO
CREATE TABLE Table1 (Col1 int, Col2 char (30));
GO
INSERT INTO Table1 VALUES (100, 'Hello');
GO
ALTER TABLE Table1 WITH NOCHECK ADD CONSTRAINT chkTab1 CHECK (Col1 > 100);
GO
DBCC CHECKCONSTRAINTS(Table1);
GO
--</snippetDBCCCheck3>
--<snippetDBCCCheck4>
USE AdventureWorks2012;
GO
DBCC CHECKCONSTRAINTS ("Production.CK_ProductCostHistory_EndDate");
GO
--</snippetDBCCCheck4>
--<snippetDBCCCheck5>
DBCC CHECKCONSTRAINTS WITH ALL_CONSTRAINTS;
GO
--</snippetDBCCCheck5>
--<snippetDBCCCheck6>
-- Check the current database.
DBCC CHECKDB;
GO
-- Check the AdventureWorks2012 database without nonclustered indexes.
DBCC CHECKDB (AdventureWorks2012, NOINDEX);
GO
--</snippetDBCCCheck6>
--<snippetDBCCCheck7>
DBCC CHECKDB WITH NO_INFOMSGS;
GO
--</snippetDBCCCheck7>
--<snippetDBCCCheck8>
USE AdventureWorks2012;
GO
DBCC CHECKFILEGROUP;
GO
--</snippetDBCCCheck8>
--<snippetDBCCCheck9>
USE AdventureWorks2012;
GO
DBCC CHECKFILEGROUP (1, NOINDEX);
GO
--</snippetDBCCCheck9>
--<snippetDBCCCheck10>
USE master;
GO
DBCC CHECKFILEGROUP (1)
WITH ESTIMATEONLY;
--</snippetDBCCCheck10>

--<snippetDBCCCheck11>
USE AdventureWorks2012;
GO
DBCC CHECKIDENT ("Person.AddressType");
GO
--</snippetDBCCCheck11>
--<snippetDBCCCheck12>
USE AdventureWorks2012;
GO
DBCC CHECKIDENT ("Person.AddressType", NORESEED);
GO
--</snippetDBCCCheck12>
--<snippetDBCCCheck13>
USE AdventureWorks2012;
GO
DBCC CHECKIDENT ("Person.AddressType", RESEED, 10);
GO
--</snippetDBCCCheck13>
--<snippetDBCCCheck14>
USE AdventureWorks2012;
GO
DBCC CHECKTABLE ("HumanResources.Employee");
GO
--</snippetDBCCCheck14>
--<snippetDBCCCheck15>
USE AdventureWorks2012;
GO
DBCC CHECKTABLE ("HumanResources.Employee") WITH PHYSICAL_ONLY;
GO
--</snippetDBCCCheck15>
--<snippetDBCCCheck16>
USE AdventureWorks2012;
GO
DECLARE @indid int;
SET @indid = (SELECT index_id 
              FROM sys.indexes
              WHERE object_id = OBJECT_ID('Production.Product')
                    AND name = 'AK_Product_Name');
DBCC CHECKTABLE ("Production.Product", @indid);
--</snippetDBCCCheck16>
