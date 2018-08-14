--<snippetOCS_SQL_SyncSamplesSetupChangeTracking>
--
-- Create a database for the Synchronization Services samples.
--
USE master
GO

IF EXISTS (SELECT [name] FROM [master].[sys].[databases] 
			   WHERE [name] = N'SyncSamplesDb_ChangeTracking')
	BEGIN
		DROP DATABASE SyncSamplesDb_ChangeTracking
	END

CREATE DATABASE SyncSamplesDb_ChangeTracking
GO

USE SyncSamplesDb_ChangeTracking
GO

CREATE SCHEMA Sales
GO

------------------------------------
--
-- Create tables for the Synchronization Services samples.
--
--<snippetOCS_SQL_SyncSamplesSetupChangeTracking_CustomerTable>
CREATE TABLE SyncSamplesDb_ChangeTracking.Sales.Customer(
	CustomerId uniqueidentifier NOT NULL PRIMARY KEY DEFAULT NEWID(), 
	CustomerName nvarchar(100) NOT NULL,
	SalesPerson nvarchar(100) NOT NULL,
	CustomerType nvarchar(100) NOT NULL)
--</snippetOCS_SQL_SyncSamplesSetupChangeTracking_CustomerTable>
GO

CREATE TABLE SyncSamplesDb_ChangeTracking.Sales.CustomerContact(
	CustomerId uniqueidentifier NOT NULL,
	PhoneNumber nvarchar(100) NOT NULL,
	PhoneType nvarchar(100) NOT NULL,
	CONSTRAINT PK_CustomerContact PRIMARY KEY (CustomerId, PhoneType))
GO

CREATE TABLE SyncSamplesDb_ChangeTracking.Sales.OrderHeader(
	OrderId uniqueidentifier NOT NULL PRIMARY KEY DEFAULT NEWID(),
	CustomerId uniqueidentifier NOT NULL, 
	OrderDate datetime NOT NULL DEFAULT GETDATE(),
	OrderStatus nvarchar(100) NOT NULL)
GO

CREATE TABLE SyncSamplesDb_ChangeTracking.Sales.OrderDetail(
	OrderDetailId int NOT NULL, 
	OrderId uniqueidentifier NOT NULL, 
	Product nvarchar(100) NOT NULL, 
	Quantity int NOT NULL DEFAULT 1,
	CONSTRAINT PK_OrderDetail PRIMARY KEY (OrderDetailId, OrderId))
GO

CREATE TABLE SyncSamplesDb_ChangeTracking.Sales.Vendor(
	VendorId uniqueidentifier NOT NULL PRIMARY KEY DEFAULT NEWID(),
	VendorName nvarchar(100) NOT NULL,
	CreditRating tinyint NOT NULL,
	PreferredVendor bit NOT NULL)
GO

------------------------------------
--
-- Create FOREIGN KEY constraints between some of the tables.
--
ALTER TABLE SyncSamplesDb_ChangeTracking.Sales.CustomerContact
ADD CONSTRAINT FK_CustomerContact_Customer FOREIGN KEY (CustomerId)
	REFERENCES SyncSamplesDb_ChangeTracking.Sales.Customer (CustomerId)
GO

ALTER TABLE SyncSamplesDb_ChangeTracking.Sales.OrderHeader
ADD CONSTRAINT FK_OrderHeader_Customer FOREIGN KEY (CustomerId)
	REFERENCES SyncSamplesDb_ChangeTracking.Sales.Customer (CustomerId)
GO

ALTER TABLE SyncSamplesDb_ChangeTracking.Sales.OrderDetail
ADD CONSTRAINT FK_OrderDetail_OrderHeader FOREIGN KEY (OrderId)
	REFERENCES SyncSamplesDb_ChangeTracking.Sales.OrderHeader (OrderId)
GO


------------------------------------
-- Insert test data.
--
--

--Wrap the inserts in a procedure so that each snippet
--can call the procedure to reset the database after
--the snippet completes.
CREATE PROCEDURE usp_InsertSampleData

AS

	SET NOCOUNT ON

	DELETE FROM SyncSamplesDb_ChangeTracking.Sales.Vendor
	DELETE FROM SyncSamplesDb_ChangeTracking.Sales.OrderDetail
	DELETE FROM SyncSamplesDb_ChangeTracking.Sales.OrderHeader
	DELETE FROM SyncSamplesDb_ChangeTracking.Sales.CustomerContact
	DELETE FROM SyncSamplesDb_ChangeTracking.Sales.Customer

	--Insert into Customer.
	INSERT INTO SyncSamplesDb_ChangeTracking.Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Aerobic Exercise Company', N'James Bailey', N'Wholesale')
	INSERT INTO SyncSamplesDb_ChangeTracking.Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Exemplary Cycles', N'James Bailey', N'Retail')
	INSERT INTO SyncSamplesDb_ChangeTracking.Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Tandem Bicycle Store', N'Brenda Diaz', N'Wholesale')
	INSERT INTO SyncSamplesDb_ChangeTracking.Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Rural Cycle Emporium', N'Brenda Diaz', N'Retail')
	INSERT INTO SyncSamplesDb_ChangeTracking.Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Sharp Bikes', N'Brenda Diaz', N'Retail')

	--Declare variables that are used in subsequent inserts.
	DECLARE @CustomerId uniqueidentifier
	DECLARE @OrderId uniqueidentifier
	DECLARE @InsertString nvarchar(1024)

	----------------------------------
	-------- First Customer ----------
	----------------------------------
	--No additional inserts for Aerobic Exercise Company


	----------------------------------
	------- Second Customer ----------
	----------------------------------
	--Insert into CustomerContact.
	SELECT @CustomerId = CustomerId FROM SyncSamplesDb_ChangeTracking.Sales.Customer WHERE CustomerName = N'Exemplary Cycles'
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''959-555-0151'', ''Business'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderHeader.
	-- First order
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderHeader (CustomerId, OrderDate, OrderStatus) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''2007-01-28'', ''Complete'')'
	EXECUTE sp_executesql @InsertString
	-- Second order
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderHeader (CustomerId, OrderDate, OrderStatus) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''2007-02-03'', ''Pending'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderDetail.
	-- First order details
	SELECT @OrderId = OrderId FROM SyncSamplesDb_ChangeTracking.Sales.OrderHeader WHERE OrderDate = N'2007-01-28'
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (1, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Chain'', 3)'
	EXECUTE sp_executesql @InsertString
	-- Second order details
	SELECT @OrderId = OrderId FROM SyncSamplesDb_ChangeTracking.Sales.OrderHeader WHERE OrderDate = N'2007-02-03'
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (1, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Mountain Tire'', 7)'
	EXECUTE sp_executesql @InsertString

	----------------------------------
	-------- Third Customer ----------
	----------------------------------
	--Insert into CustomerContact.
	SELECT @CustomerId = CustomerId FROM SyncSamplesDb_ChangeTracking.Sales.Customer WHERE CustomerName = N'Tandem Bicycle Store'
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''107-555-0138'', ''Business'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderHeader.
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderHeader (CustomerId, OrderDate, OrderStatus) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''2007-02-04'', ''Complete'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderDetail.
	SELECT @OrderId = OrderId FROM SyncSamplesDb_ChangeTracking.Sales.OrderHeader WHERE OrderDate = N'2007-02-04'
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (1, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Road Pedal'', 4)'
	EXECUTE sp_executesql @InsertString
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (2, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Road Rear Wheel'', 4)'
	EXECUTE sp_executesql @InsertString

	----------------------------------
	------- Fourth Customer ----------
	----------------------------------
	--Insert into CustomerContact.
	SELECT @CustomerId = CustomerId FROM SyncSamplesDb_ChangeTracking.Sales.Customer WHERE CustomerName = N'Rural Cycle Emporium'
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''158-555-0142'', ''Business'')'
	EXECUTE sp_executesql @InsertString

	--Insert into CustomerContact (second contact info).
	SELECT @CustomerId = CustomerId FROM SyncSamplesDb_ChangeTracking.Sales.Customer WHERE CustomerName = N'Rural Cycle Emporium'
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''453-555-0167'', ''Mobile'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderHeader.
	-- First order
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderHeader (CustomerId, OrderDate, OrderStatus) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''2007-03-12'', ''Complete'')'
	EXECUTE sp_executesql @InsertString
	-- Second order
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderHeader (CustomerId, OrderDate, OrderStatus) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''2007-04-14'', ''Back Ordered'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderDetail.
	--First order details
	SELECT @OrderId = OrderId FROM SyncSamplesDb_ChangeTracking.Sales.OrderHeader WHERE OrderDate = N'2007-03-12'
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (1, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Hydration Pack'', 1)'
	EXECUTE sp_executesql @InsertString
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (2, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Men''''s Sports Shorts'', 3)'
	EXECUTE sp_executesql @InsertString
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (3, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Water Bottle'', 6)'
	EXECUTE sp_executesql @InsertString
	--Second order details
	SELECT @OrderId = OrderId FROM SyncSamplesDb_ChangeTracking.Sales.OrderHeader WHERE OrderDate = N'2007-04-14'
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (1, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Mountain Tire'', 5)'
	EXECUTE sp_executesql @InsertString
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (2, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Women''''s Mountain Shorts'', 5)'
	EXECUTE sp_executesql @InsertString
	SET @InsertString = 'INSERT INTO SyncSamplesDb_ChangeTracking.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (3, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Road Pedal'', 2)'
	EXECUTE sp_executesql @InsertString

	----------------------------------
	-------- Fifth Customer ----------
	----------------------------------
	--No additional inserts for Sharp Bikes


	
	--Insert into Vendor.
	INSERT INTO SyncSamplesDb_ChangeTracking.Sales.Vendor (VendorName, CreditRating, PreferredVendor) VALUES (N'Premier Sport, Inc.', 2, 1)
	INSERT INTO SyncSamplesDb_ChangeTracking.Sales.Vendor (VendorName, CreditRating, PreferredVendor) VALUES (N'Metro Sport Equipment', 1, 1)
	INSERT INTO SyncSamplesDb_ChangeTracking.Sales.Vendor (VendorName, CreditRating, PreferredVendor) VALUES (N'Mountain Works', 3, 0)
	INSERT INTO SyncSamplesDb_ChangeTracking.Sales.Vendor (VendorName, CreditRating, PreferredVendor) VALUES (N'Green Lake Bike Company', 2, 1)
	INSERT INTO SyncSamplesDb_ChangeTracking.Sales.Vendor (VendorName, CreditRating, PreferredVendor) VALUES (N'Compete, Inc.', 5, 0)


	SET NOCOUNT OFF

GO -- End of usp_InsertSampleData

EXEC usp_InsertSampleData

------------------------------------
--
--Verify table creation and inserts.
--

SELECT * FROM SyncSamplesDb_ChangeTracking.Sales.Customer
SELECT * FROM SyncSamplesDb_ChangeTracking.Sales.CustomerContact
SELECT * FROM SyncSamplesDb_ChangeTracking.Sales.OrderHeader
SELECT * FROM SyncSamplesDb_ChangeTracking.Sales.OrderDetail
SELECT * FROM SyncSamplesDb_ChangeTracking.Sales.Vendor

SELECT c.CustomerId, c.CustomerName, c.SalesPerson, c.CustomerType, cc.PhoneNumber, cc.PhoneType,
oh.OrderId, oh.OrderDate, oh.OrderStatus, od.OrderDetailId, od.Product, od.Quantity
FROM SyncSamplesDb_ChangeTracking.Sales.Customer c 
	JOIN SyncSamplesDb_ChangeTracking.Sales.CustomerContact cc
		ON c.CustomerId = cc.CustomerId
	JOIN SyncSamplesDb_ChangeTracking.Sales.OrderHeader oh
		ON c.CustomerId = oh.CustomerId
	JOIN SyncSamplesDb_ChangeTracking.Sales.OrderDetail od
		ON oh.OrderId = od.OrderId
ORDER BY c.CustomerName, oh.OrderDate, od.OrderDetailId


--Set snapshot isolation and enable SQL Server change tracking the database.
--<snippetOCS_SQL_SyncSamplesSetupChangeTracking_EnableChangeTrackingOnDb>
ALTER DATABASE SyncSamplesDb_ChangeTracking SET ALLOW_SNAPSHOT_ISOLATION ON

ALTER DATABASE SyncSamplesDb_ChangeTracking
SET CHANGE_TRACKING = ON
(CHANGE_RETENTION = 2 DAYS, AUTO_CLEANUP = ON)
--</snippetOCS_SQL_SyncSamplesSetupChangeTracking_EnableChangeTrackingOnDb>

--<snippetOCS_SQL_SyncSamplesSetupChangeTracking_EnableChangeTrackingOnCustomerTable>
ALTER TABLE SyncSamplesDb_ChangeTracking.Sales.Customer
ENABLE CHANGE_TRACKING
--</snippetOCS_SQL_SyncSamplesSetupChangeTracking_EnableChangeTrackingOnCustomerTable>


ALTER TABLE SyncSamplesDb_ChangeTracking.Sales.CustomerContact
ENABLE CHANGE_TRACKING
WITH (TRACK_COLUMNS_UPDATED = OFF)

ALTER TABLE SyncSamplesDb_ChangeTracking.Sales.OrderHeader
ENABLE CHANGE_TRACKING
WITH (TRACK_COLUMNS_UPDATED = OFF)

ALTER TABLE SyncSamplesDb_ChangeTracking.Sales.OrderDetail
ENABLE CHANGE_TRACKING
WITH (TRACK_COLUMNS_UPDATED = OFF)

ALTER TABLE SyncSamplesDb_ChangeTracking.Sales.Vendor
ENABLE CHANGE_TRACKING
WITH (TRACK_COLUMNS_UPDATED = OFF)
--</snippetOCS_SQL_SyncSamplesSetupChangeTracking>