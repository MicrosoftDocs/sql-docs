
--<snippetCreateTrigger1>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('Sales.reminder1', 'TR') IS NOT NULL
   DROP TRIGGER Sales.reminder1;
GO
CREATE TRIGGER reminder1
ON Sales.Customer
AFTER INSERT, UPDATE 
AS RAISERROR ('Notify Customer Relations', 16, 10);
GO
--</snippetCreateTrigger1>

--<snippetCreateTrigger2>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('Sales.reminder2','TR') IS NOT NULL
    DROP TRIGGER Sales.reminder2;
GO
CREATE TRIGGER reminder2
ON Sales.Customer
AFTER INSERT, UPDATE, DELETE 
AS
   EXEC msdb.dbo.sp_send_dbmail
        @profile_name = 'AdventureWorks2012 Administrator',
        @recipients = 'danw@Adventure-Works.com',
        @body = 'Don''t forget to print a report for the sales force.',
        @subject = 'Reminder';
GO
--</snippetCreateTrigger2>

--<snippetCreateTrigger3>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('Purchasing.LowCredit','TR') IS NOT NULL
   DROP TRIGGER Purchasing.LowCredit;
GO
-- This trigger prevents a row from being inserted in the Purchasing.PurchaseOrderHeader table
-- when the credit rating of the specified vendor is set to 5 (below average).

CREATE TRIGGER Purchasing.LowCredit ON Purchasing.PurchaseOrderHeader
AFTER INSERT
AS
IF EXISTS (SELECT *
           FROM Purchasing.PurchaseOrderHeader p 
           JOIN inserted AS i 
           ON p.PurchaseOrderID = i.PurchaseOrderID 
           JOIN Purchasing.Vendor AS v 
           ON v.BusinessEntityID = p.VendorID
           WHERE v.CreditRating = 5
          )
BEGIN
RAISERROR ('A vendor''s credit rating is too low to accept new
purchase orders.', 16, 1);
ROLLBACK TRANSACTION;
RETURN 
END;
GO

-- This statement attempts to insert a row into the PurchaseOrderHeader table
-- for a vendor that has a below average credit rating.
-- The AFTER INSERT trigger is fired and the INSERT transaction is rolled back.

INSERT INTO Purchasing.PurchaseOrderHeader (RevisionNumber, Status, EmployeeID,
VendorID, ShipMethodID, OrderDate, ShipDate, SubTotal, TaxAmt, Freight)
VALUES (
2
,3
,261	
,1652	
,4	
,GETDATE()
,GETDATE()
,44594.55	
,3567.564	
,1114.8638 );
GO
--</snippetCreateTrigger3>

USE AdventureWorks2012;
GO
IF OBJECT_ID ('HumanResources.trig1','TR') IS NOT NULL
   DROP TRIGGER HumanResources.trig1;
GO
-- Creating a trigger on a nonexistent table.
CREATE TRIGGER HumanResources.trig1
on HumanResources.Employee
AFTER INSERT, UPDATE, DELETE
AS 
   SELECT e.BusinessEntityID, e.BirthDate, x.info 
   FROM HumanResources.Employee AS e INNER JOIN does_not_exist AS x 
      ON e.BusinessEntityID = x.xID
GO
-- Here is the statement to actually see the text of the trigger.
SELECT t.object_id, m.definition
FROM sys.triggers AS t INNER JOIN sys.sql_modules AS m 
   ON t.object_id = m.object_id
WHERE t.type = 'TR' and t.name = 'trig1'
AND t.parent_class = 1
GO

-- Creating a trigger on an existing table, but with a nonexistent 
-- column.
USE AdventureWorks2012;
GO
IF OBJECT_ID ('HumanResources.trig2','TR') IS NOT NULL
   DROP TRIGGER HumanResources.trig2
GO
CREATE TRIGGER HumanResources.trig2 
ON HumanResources.Employee
AFTER INSERT, UPDATE
AS 
   DECLARE @fax varchar(12)
   SELECT @fax = 'AltPhone'
   FROM HumanResources.Employee
GO
-- Here is the statement to actually see the text of the trigger.
SELECT t.object_id, m.definition
FROM sys.triggers AS t INNER JOIN sys.sql_modules AS m 
   ON t.object_id = m.object_id
WHERE t.type = 'TR' and t.name = 'trig2'
AND t.parent_class = 1
GO

--</snippetCreateTrigger4>

--<snippetCreateTrigger5>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT * FROM sys.triggers
    WHERE parent_class = 0 AND name = 'safety')
DROP TRIGGER safety
ON DATABASE;
GO
CREATE TRIGGER safety 
ON DATABASE 
FOR DROP_SYNONYM
AS 
   RAISERROR ('You must disable Trigger "safety" to drop synonyms!',10, 1)
   ROLLBACK
GO
DROP TRIGGER safety
ON DATABASE;
GO

--</snippetCreateTrigger5>

--<snippetCreateTrigger6>
IF EXISTS (SELECT * FROM sys.server_triggers
    WHERE name = 'ddl_trig_database')
DROP TRIGGER ddl_trig_database
ON ALL SERVER;
GO
CREATE TRIGGER ddl_trig_database 
ON ALL SERVER 
FOR CREATE_DATABASE 
AS 
    PRINT 'Database Created.'
    SELECT EVENTDATA().value('(/EVENT_INSTANCE/TSQLCommand/CommandText)[1]','nvarchar(max)')
GO
DROP TRIGGER ddl_trig_database
ON ALL SERVER;
GO
--</snippetCreateTrigger6>

--<snippetCreateTrigger7>
SELECT TE.*
FROM sys.trigger_events AS TE
JOIN sys.triggers AS T
ON T.object_id = TE.object_id
WHERE T.parent_class = 0
AND T.name = 'safety'
GO
--</snippetCreateTrigger7>

--<snippetCreateTrigger8>

USE AdventureWorks2012;
GO
IF OBJECT_ID('dbo.vw_ScrapReason','V') IS NOT NULL
    DROP VIEW dbo.vw_ScrapReason;
GO
CREATE VIEW dbo.vw_ScrapReason
AS (SELECT ScrapReasonID, Name, ModifiedDate
    FROM Production.ScrapReason);
GO
CREATE TRIGGER dbo.io_ScrapReason 
    ON dbo.vw_ScrapReason
INSTEAD OF INSERT
AS
BEGIN
--ScrapReasonID is not specified in the list of columns to be inserted 
--because it is an IDENTITY column.
    INSERT INTO Production.ScrapReason (Name, ModifiedDate)
        OUTPUT INSERTED.ScrapReasonID, INSERTED.Name, 
               INSERTED.ModifiedDate
    SELECT Name, getdate()
    FROM inserted;
END
GO
INSERT vw_ScrapReason (ScrapReasonID, Name, ModifiedDate)
VALUES (99, N'My scrap reason','20030404');
GO

--</snippetCreateTrigger8>

--<snippetAlterTrigger1>
USE AdventureWorks2012;
GO
IF OBJECT_ID(N'Sales.bonus_reminder', N'TR') IS NOT NULL
    DROP TRIGGER Sales.bonus_reminder;
GO
CREATE TRIGGER Sales.bonus_reminder
ON Sales.SalesPersonQuotaHistory
WITH ENCRYPTION
AFTER INSERT, UPDATE 
AS RAISERROR ('Notify Compensation', 16, 10);
GO
-- Now, change the trigger.
USE AdventureWorks2012;
GO
ALTER TRIGGER Sales.bonus_reminder
ON Sales.SalesPersonQuotaHistory
AFTER INSERT
AS RAISERROR ('Notify Compensation', 16, 10);
GO
--</snippetAlterTrigger1>

--<snippetDropTrigger1>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('employee_insupd', 'TR') IS NOT NULL
   DROP TRIGGER employee_insupd;
GO
--</snippetDropTrigger1>

--<snippetDropTrigger2>
USE AdventureWorks2012;
GO
IF EXISTS (SELECT * FROM sys.triggers
    WHERE parent_class = 0 AND name = 'safety')
DROP TRIGGER safety
ON DATABASE;
GO
--</snippetDropTrigger2>

--<snippetLogonTrigger1>
USE master;
GO
CREATE LOGIN login_test WITH PASSWORD = '3KHJ6dhx(0xVYsdf' MUST_CHANGE,
    CHECK_EXPIRATION = ON;
GO
GRANT VIEW SERVER STATE TO login_test;
GO
CREATE TRIGGER connection_limit_trigger
ON ALL SERVER WITH EXECUTE AS 'login_test'
FOR LOGON
AS
BEGIN
IF ORIGINAL_LOGIN()= 'login_test' AND
    (SELECT COUNT(*) FROM sys.dm_exec_sessions
            WHERE is_user_process = 1 AND
                original_login_name = 'login_test') > 3
    ROLLBACK;
END;
--</snippetLogonTrigger1>

