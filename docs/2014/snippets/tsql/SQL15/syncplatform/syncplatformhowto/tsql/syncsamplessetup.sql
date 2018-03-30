--<snippetOCS_SQL_SyncSamplesSetup>
--
-- Create a database for the Synchronization Services samples.
--
USE master
GO

IF EXISTS (SELECT [name] FROM [master].[sys].[databases] 
			   WHERE [name] = N'SyncSamplesDb')
	BEGIN
		DROP DATABASE SyncSamplesDb
	END

CREATE DATABASE SyncSamplesDb
GO

USE SyncSamplesDb
GO

CREATE SCHEMA Sales
GO

------------------------------------
--
-- Create tables for the Synchronization Services samples.
--
--<snippetOCS_SQL_SyncSamplesSetup_CustomerTable>
CREATE TABLE SyncSamplesDb.Sales.Customer(
	CustomerId uniqueidentifier NOT NULL PRIMARY KEY DEFAULT NEWID(), 
	CustomerName nvarchar(100) NOT NULL,
	SalesPerson nvarchar(100) NOT NULL,
	CustomerType nvarchar(100) NOT NULL)
--</snippetOCS_SQL_SyncSamplesSetup_CustomerTable>
GO

CREATE TABLE SyncSamplesDb.Sales.CustomerContact(
	CustomerId uniqueidentifier NOT NULL,
	PhoneNumber nvarchar(100) NOT NULL,
	PhoneType nvarchar(100) NOT NULL,
	CONSTRAINT PK_CustomerContact PRIMARY KEY (CustomerId, PhoneType))
GO

CREATE TABLE SyncSamplesDb.Sales.OrderHeader(
	OrderId uniqueidentifier NOT NULL PRIMARY KEY DEFAULT NEWID(),
	CustomerId uniqueidentifier NOT NULL, 
	OrderDate datetime NOT NULL DEFAULT GETDATE(),
	OrderStatus nvarchar(100) NOT NULL)
GO

CREATE TABLE SyncSamplesDb.Sales.OrderDetail(
	OrderDetailId int NOT NULL, 
	OrderId uniqueidentifier NOT NULL, 
	Product nvarchar(100) NOT NULL, 
	Quantity int NOT NULL DEFAULT 1,
	CONSTRAINT PK_OrderDetail PRIMARY KEY (OrderDetailId, OrderId))
GO

CREATE TABLE SyncSamplesDb.Sales.Vendor(
	VendorId uniqueidentifier NOT NULL PRIMARY KEY DEFAULT NEWID(),
	VendorName nvarchar(100) NOT NULL,
	CreditRating tinyint NOT NULL,
	PreferredVendor bit NOT NULL)
GO

------------------------------------
--
-- Create FOREIGN KEY constraints between some of the tables.
--
ALTER TABLE SyncSamplesDb.Sales.CustomerContact
ADD CONSTRAINT FK_CustomerContact_Customer FOREIGN KEY (CustomerId)
	REFERENCES SyncSamplesDb.Sales.Customer (CustomerId)
GO

ALTER TABLE SyncSamplesDb.Sales.OrderHeader
ADD CONSTRAINT FK_OrderHeader_Customer FOREIGN KEY (CustomerId)
	REFERENCES SyncSamplesDb.Sales.Customer (CustomerId)
GO

ALTER TABLE SyncSamplesDb.Sales.OrderDetail
ADD CONSTRAINT FK_OrderDetail_OrderHeader FOREIGN KEY (OrderId)
	REFERENCES SyncSamplesDb.Sales.OrderHeader (OrderId)
GO

------------------------------------
--
-- Add the tracking columns for bidirectional 
-- and download only synchronization:
-- * Add a timestamp column to record the logical time that the row was last updated.
-- * Add a bigint column to record the logical time that the row was inserted.
--	 A bigint column is used because a table can have only one timestamp column.
-- * Add InsertId and UpdateId columns to identify where changes were made.
--   Specify a default of 0 to indicate a server update.

-- Customer
--<snippetOCS_SQL_SyncSamplesSetup_CustomerTrackingColumns>
ALTER TABLE SyncSamplesDb.Sales.Customer 
	ADD UpdateTimestamp timestamp
ALTER TABLE SyncSamplesDb.Sales.Customer 
	ADD InsertTimestamp binary(8) DEFAULT @@DBTS + 1
ALTER TABLE SyncSamplesDb.Sales.Customer 
	ADD UpdateId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
ALTER TABLE SyncSamplesDb.Sales.Customer 
	ADD InsertId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
--</snippetOCS_SQL_SyncSamplesSetup_CustomerTrackingColumns>
GO

-- We add indexes to the Customer and Customer_Tombstone tables
-- to emphasize that you should take indexes into account
-- when you implement change-tracking in the server database.
-- Balance server performance against synchronization performance.
--<snippetOCS_SQL_SyncSamplesSetup_CustomerIndexes>
CREATE NONCLUSTERED INDEX IX_Customer_UpdateTimestamp
ON Sales.Customer(UpdateTimestamp)

CREATE NONCLUSTERED INDEX IX_Customer_InsertTimestamp
ON Sales.Customer(InsertTimestamp)

CREATE NONCLUSTERED INDEX IX_Customer_UpdateId
ON Sales.Customer(UpdateId)

CREATE NONCLUSTERED INDEX IX_Customer_InsertId
ON Sales.Customer(InsertId)
--</snippetOCS_SQL_SyncSamplesSetup_CustomerIndexes>
GO

-- CustomerContact
ALTER TABLE SyncSamplesDb.Sales.CustomerContact 
	ADD UpdateTimestamp timestamp
ALTER TABLE SyncSamplesDb.Sales.CustomerContact 
	ADD InsertTimestamp binary(8) DEFAULT @@DBTS + 1
ALTER TABLE SyncSamplesDb.Sales.CustomerContact 
	ADD UpdateId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
ALTER TABLE SyncSamplesDb.Sales.CustomerContact 
	ADD InsertId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
GO

-- OrderHeader
ALTER TABLE SyncSamplesDb.Sales.OrderHeader 
	ADD UpdateTimestamp timestamp
ALTER TABLE SyncSamplesDb.Sales.OrderHeader 
	ADD InsertTimestamp binary(8) DEFAULT @@DBTS + 1
ALTER TABLE SyncSamplesDb.Sales.OrderHeader 
	ADD UpdateId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
ALTER TABLE SyncSamplesDb.Sales.OrderHeader 
	ADD InsertId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
GO

-- OrderDetail
ALTER TABLE SyncSamplesDb.Sales.OrderDetail 
	ADD UpdateTimestamp timestamp
ALTER TABLE SyncSamplesDb.Sales.OrderDetail 
	ADD InsertTimestamp binary(8) DEFAULT @@DBTS + 1
ALTER TABLE SyncSamplesDb.Sales.OrderDetail 
	ADD UpdateId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
ALTER TABLE SyncSamplesDb.Sales.OrderDetail 
	ADD InsertId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
GO

-- Vendor
-- The data type for the ID columns is int
-- because the value is mapped by using the
-- usp_GetOriginatorId procedure, which is 
-- defined later in this script.
ALTER TABLE SyncSamplesDb.Sales.Vendor 
	ADD UpdateTimestamp timestamp
ALTER TABLE SyncSamplesDb.Sales.Vendor 
	ADD InsertTimestamp binary(8) DEFAULT @@DBTS + 1
ALTER TABLE SyncSamplesDb.Sales.Vendor 
	ADD UpdateId int NOT NULL DEFAULT 0
ALTER TABLE SyncSamplesDb.Sales.Vendor 
	ADD InsertId int NOT NULL DEFAULT 0
GO

------------------------------------
--
-- Create tombstone tables to store deletes.
-- Each tombstone table includes all columns from the base table, except those
-- we added for tracking. All columns are required if you must have
-- access to the whole row for conflict resolution.
--
--<snippetOCS_SQL_SyncSamplesSetup_CustomerTombstone>
CREATE TABLE SyncSamplesDb.Sales.Customer_Tombstone(
	CustomerId uniqueidentifier NOT NULL PRIMARY KEY NONCLUSTERED, 
	CustomerName nvarchar(100) NOT NULL,
	SalesPerson nvarchar(100) NOT NULL,
	CustomerType nvarchar(100) NOT NULL,
	DeleteId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
	DeleteTimestamp timestamp)
--</snippetOCS_SQL_SyncSamplesSetup_CustomerTombstone>
GO

--<snippetOCS_SQL_SyncSamplesSetup_CustomerTombstoneIndexes>
CREATE CLUSTERED INDEX IX_Customer_Tombstone_DeleteTimestamp
ON Sales.Customer_Tombstone(DeleteTimestamp)

CREATE NONCLUSTERED INDEX IX_Customer_Tombstone_DeleteId
ON Sales.Customer_Tombstone(DeleteId)
--</snippetOCS_SQL_SyncSamplesSetup_CustomerTombstoneIndexes>


CREATE TABLE SyncSamplesDb.Sales.CustomerContact_Tombstone(
	CustomerId uniqueidentifier NOT NULL,
	PhoneNumber nvarchar(100) NOT NULL,
	PhoneType nvarchar(100) NOT NULL,
	DeleteId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
	DeleteTimestamp timestamp,
	CONSTRAINT PK_CustomerContact_Tombstone PRIMARY KEY NONCLUSTERED (CustomerId, PhoneType))
GO

CREATE TABLE SyncSamplesDb.Sales.OrderHeader_Tombstone(
	OrderId uniqueidentifier NOT NULL PRIMARY KEY NONCLUSTERED,
	CustomerId uniqueidentifier NOT NULL, 
	OrderDate datetime NOT NULL,
	OrderStatus nvarchar(100) NOT NULL,
	DeleteId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
	DeleteTimestamp timestamp)
GO

CREATE TABLE SyncSamplesDb.Sales.OrderDetail_Tombstone(
	OrderDetailId int NOT NULL, 
	OrderId uniqueidentifier NOT NULL, 
	Product nvarchar(100) NOT NULL, 
	Quantity int NOT NULL DEFAULT 1,
	DeleteId uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
	DeleteTimestamp timestamp,
	CONSTRAINT PK_OrderDetail_Tombstone PRIMARY KEY NONCLUSTERED (OrderDetailId, OrderId))
GO

CREATE TABLE SyncSamplesDb.Sales.Vendor_Tombstone(
	VendorId uniqueidentifier NOT NULL PRIMARY KEY NONCLUSTERED,
	VendorName nvarchar(100) NOT NULL,
	CreditRating tinyint NOT NULL,
	PreferredVendor bit NOT NULL,
	DeleteId int NOT NULL DEFAULT 0,
	DeleteTimestamp timestamp)
GO

------------------------------------
-- Create delete triggers.
-- When a delete occurs in the base table, the trigger inserts a row 
-- in the tombstones table. Before performing an insert, the trigger 
-- checks whether the tombstones table already contains a row that has 
-- the primary key of a deleted row. This occurs if a row has been deleted 
-- from the base table, reinserted, and deleted again. If such a row is 
-- detected in the tombstones table, the trigger deletes the row and 
-- reinserts it.
--

--<snippetOCS_SQL_SyncSamplesSetup_CustomerDeleteTrigger>
CREATE TRIGGER Customer_DeleteTrigger 
ON SyncSamplesDb.Sales.Customer FOR DELETE 
AS 
BEGIN 
    SET NOCOUNT ON
    DELETE FROM SyncSamplesDb.Sales.Customer_Tombstone 
		WHERE CustomerId IN (SELECT CustomerId FROM deleted)
    INSERT INTO SyncSamplesDb.Sales.Customer_Tombstone (CustomerId, CustomerName, SalesPerson, CustomerType) 
	SELECT CustomerId, CustomerName, SalesPerson, CustomerType FROM deleted
    SET NOCOUNT OFF
END
--</snippetOCS_SQL_SyncSamplesSetup_CustomerDeleteTrigger>
GO

CREATE TRIGGER CustomerContact_DeleteTrigger 
ON SyncSamplesDb.Sales.CustomerContact FOR DELETE 
AS 
BEGIN 
    SET NOCOUNT ON 
    DELETE FROM SyncSamplesDb.Sales.CustomerContact_Tombstone 
		WHERE CustomerId IN (SELECT CustomerId FROM deleted) AND
		PhoneType IN (SELECT PhoneType FROM deleted)
    INSERT INTO SyncSamplesDb.Sales.CustomerContact_Tombstone (CustomerId, PhoneNumber, PhoneType)
	SELECT CustomerId, PhoneNumber, PhoneType FROM deleted
    SET NOCOUNT OFF
END
GO

CREATE TRIGGER OrderHeader_DeleteTrigger 
ON SyncSamplesDb.Sales.OrderHeader FOR DELETE 
AS 
BEGIN 
	SET NOCOUNT ON
    DELETE FROM SyncSamplesDb.Sales.OrderHeader_Tombstone 
		WHERE OrderId IN (SELECT OrderId FROM deleted)
    INSERT INTO SyncSamplesDb.Sales.OrderHeader_Tombstone (OrderId, CustomerId, OrderDate, OrderStatus) 
	SELECT OrderId, CustomerId, OrderDate, OrderStatus FROM deleted
    SET NOCOUNT OFF
END
GO

CREATE TRIGGER OrderDetail_DeleteTrigger 
ON SyncSamplesDb.Sales.OrderDetail FOR DELETE 
AS 
BEGIN 
    SET NOCOUNT ON 
    DELETE FROM SyncSamplesDb.Sales.OrderDetail_Tombstone 
		WHERE OrderDetailId IN (SELECT OrderDetailId FROM deleted) AND
		OrderId IN (SELECT OrderId FROM deleted)	
    INSERT INTO SyncSamplesDb.Sales.OrderDetail_Tombstone (OrderDetailId, OrderId, Product, Quantity)
	SELECT OrderDetailId, OrderId, Product, Quantity FROM deleted
    SET NOCOUNT OFF
END
GO

CREATE TRIGGER Vendor_DeleteTrigger 
ON SyncSamplesDb.Sales.Vendor FOR DELETE 
AS 
BEGIN 
	SET NOCOUNT ON
    DELETE FROM SyncSamplesDb.Sales.Vendor_Tombstone 
		WHERE VendorId IN (SELECT VendorId FROM deleted)
    INSERT INTO SyncSamplesDb.Sales.Vendor_Tombstone (VendorId, VendorName, CreditRating, PreferredVendor) 
	SELECT VendorId, VendorName, CreditRating, PreferredVendor FROM deleted
    SET NOCOUNT OFF
END
GO

------------------------------------
-- Create the stored procedures that are used in some examples
-- to apply changes to the server. These procedures are
-- designed to work in cases in which there might be
-- conflicting data changes.

-- Insert procedure
CREATE PROCEDURE usp_CustomerApplyInsert (	 
		@sync_client_id uniqueidentifier,
		@sync_force_write int,
		@sync_row_count int out,
        @CustomerId uniqueidentifier,
		@CustomerName nvarchar(100),
		@SalesPerson nvarchar(100),
		@CustomerType nvarchar(100))        
AS	

	-- Try to apply an insert if the RetryWithForceWrite option
	-- was not specified for the sync adapter's insert command.
	IF @sync_force_write = 0
	BEGIN
		INSERT INTO Sales.Customer 
		(CustomerId, CustomerName, SalesPerson, CustomerType, InsertId, UpdateId)
		VALUES (@CustomerId, @CustomerName, @SalesPerson, @CustomerType, @sync_client_id, @sync_client_id)
	END
	ELSE
	-- Try to apply an insert if the RetryWithForceWrite option
	-- was specified for the sync adapter's insert command.
	BEGIN
		-- If the row does not exist, try to insert it.
		-- You might want to include code here to handle 
		-- possible error conditions.
		IF NOT EXISTS (SELECT CustomerId FROM Sales.Customer
				   WHERE CustomerId = @CustomerId)
		BEGIN
				INSERT INTO Sales.Customer 
				(CustomerId, CustomerName, SalesPerson, CustomerType, InsertId, UpdateId)
				VALUES (@CustomerId, @CustomerName, @SalesPerson, @CustomerType, @sync_client_id, @sync_client_id)		
		END
		ELSE
		-- The row exists, possibly due to a client-insert/
		-- server-insert conflict. Change the insert into an update.
		BEGIN
			UPDATE Sales.Customer 
			SET CustomerName = @CustomerName, SalesPerson = @SalesPerson,
			CustomerType = @CustomerType, UpdateId = @sync_client_id
			WHERE CustomerId = @CustomerId
		END
	END

	SET @sync_row_count = @@rowcount

GO -- End insert procedure


-- Update Procedure
--<snippetOCS_SQL_SyncSamplesSetup_CustomerUpdateProc>
CREATE PROCEDURE usp_CustomerApplyUpdate (	
	@sync_last_received_anchor binary(8), 
	@sync_client_id uniqueidentifier,
	@sync_force_write int,
	@sync_row_count int out,
    @CustomerId uniqueidentifier,
    @CustomerName nvarchar(100),
	@SalesPerson nvarchar(100),
	@CustomerType nvarchar(100))        
AS		
	-- Try to apply an update if the RetryWithForceWrite option
	-- was not specified for the sync adapter's update command.
	IF @sync_force_write = 0
	BEGIN	
		UPDATE Sales.Customer 
		SET CustomerName = @CustomerName, SalesPerson = @SalesPerson,
		CustomerType = @CustomerType, UpdateId = @sync_client_id
		WHERE CustomerId = @CustomerId
		AND (UpdateTimestamp <= @sync_last_received_anchor
		OR UpdateId = @sync_client_id)
	END
	ELSE
	-- Try to apply an update if the RetryWithForceWrite option
	-- was specified for the sync adapter's update command.
	BEGIN
		--If the row exists, update it.
		-- You might want to include code here to handle 
		-- possible error conditions.
		IF EXISTS (SELECT CustomerId FROM Sales.Customer
				   WHERE CustomerId = @CustomerId)
		BEGIN
			UPDATE Sales.Customer 
			SET CustomerName = @CustomerName, SalesPerson = @SalesPerson,
			CustomerType = @CustomerType, UpdateId = @sync_client_id
			WHERE CustomerId = @CustomerId			
		END
		
		-- The row does not exist, possibly due to a client-update/
		-- server-delete conflict. Change the update into an insert.
		ELSE
		BEGIN
			INSERT INTO Sales.Customer 
				   (CustomerId, CustomerName, SalesPerson,
					CustomerType, UpdateId)
			VALUES (@CustomerId, @CustomerName, @SalesPerson,
					@CustomerType, @sync_client_id)
		END
	END

	SET @sync_row_count = @@rowcount
--</snippetOCS_SQL_SyncSamplesSetup_CustomerUpdateProc>

GO -- End update procedure


-- Delete procedure
CREATE PROCEDURE usp_CustomerApplyDelete (	
	@sync_last_received_anchor binary(8), 
	@sync_client_id uniqueidentifier,
	@sync_force_write int,
	@sync_row_count int out,
    @CustomerId uniqueidentifier)
AS	

	-- Delete the specified row if the anchor and ID
	-- values allow it, or if the RetryWithForceWrite 
	-- option was specified for the sync adapter's delete 
	-- command.
	DELETE FROM Sales.Customer
	WHERE (CustomerId = @CustomerId)
	AND (@sync_force_write = 1
	OR (UpdateTimestamp <= @sync_last_received_anchor
	OR UpdateId = @sync_client_id))
	SET @sync_row_count = @@rowcount

	-- Set the DeleteId in the tombstone table.
	IF (@sync_row_count > 0)  
	BEGIN
		UPDATE Sales.Customer_Tombstone
		SET DeleteId = @sync_client_id
		WHERE (CustomerId = @CustomerId)
	END

GO -- End delete procedure

------------------------------------
-- Create a mapping table and stored procedure. 
-- These are used to map client IDs, which are GUIDs,
-- to integer values. This mapping is not required, but
-- it can be more convenient than using the GUID value
-- on the server. The procedure is specified as the 
-- command for the SelectClientIdCommand property of
-- DbServerSyncProvider.
--<snippetOCS_SQL_SyncSamplesSetup_Mapping>
CREATE TABLE IdMapping(
	ClientId uniqueidentifier NOT NULL PRIMARY KEY, 
	OriginatorId int NOT NULL)
GO

--Insert a mapping for the server.
INSERT INTO IdMapping VALUES ('00000000-0000-0000-0000-000000000000', 0)
GO

CREATE PROCEDURE usp_GetOriginatorId
	@sync_client_id uniqueidentifier,
	@sync_originator_id int out

AS
     SELECT @sync_originator_id = OriginatorId FROM IdMapping WHERE ClientId = @sync_client_id 

     IF ( @sync_originator_id IS NULL )
     BEGIN
          SELECT @sync_originator_id = MAX(OriginatorId) + 1 FROM IdMapping
          INSERT INTO IdMapping VALUES (@sync_client_id, @sync_originator_id)
     END
GO
--</snippetOCS_SQL_SyncSamplesSetup_Mapping>

------------------------------------
-- Create a stored procedure to return anchor
-- values that are used to batch changes. 
--<snippetOCS_SQL_SyncSamplesSetup_Batching>
CREATE PROCEDURE usp_GetNewBatchAnchor (
	@sync_last_received_anchor timestamp, 
	@sync_batch_size bigint,
	@sync_max_received_anchor timestamp out,
	@sync_new_received_anchor timestamp out,            
	@sync_batch_count int output)            
AS            
       -- Set a default batch size if a valid one is not passed in.
       IF  @sync_batch_size IS NULL OR @sync_batch_size <= 0
	     SET @sync_batch_size = 1000    

	   -- Before selecting the first batch of changes,
	   -- set the maximum anchor value for this synchronization session.
       -- After the first time that this procedure is called, 
	   -- Synchronization Services passes a value for @sync_max_received_anchor
       -- to the procedure. Batches of changes are synchronized until this 
       -- value is reached.
       IF @sync_max_received_anchor IS NULL
         SELECT  @sync_max_received_anchor = MIN_ACTIVE_ROWVERSION() - 1
       
       -- If this is the first synchronization session for a database,
       -- get the lowest timestamp value from the tables. By default,
       -- Synchronization Services uses a value of 0 for @sync_last_received_anchor
       -- on the first synchronization. If you do not set @sync_last_received_anchor,
       -- this can cause empty batches to be downloaded until the lowest
       -- timestamp value is reached.
       IF @sync_last_received_anchor IS NULL OR @sync_last_received_anchor = 0
       BEGIN
                
		SELECT @sync_last_received_anchor = MIN(TimestampCol) FROM (
		  SELECT MIN(UpdateTimestamp) AS TimestampCol FROM Sales.Customer
		  UNION
		  SELECT MIN(InsertTimestamp) AS TimestampCol FROM Sales.Customer
		  UNION
		  SELECT MIN(UpdateTimestamp) AS TimestampCol FROM Sales.OrderHeader
		  UNION
		  SELECT MIN(InsertTimestamp) AS TimestampCol FROM Sales.OrderHeader
		) MinTimestamp	
       
		SET @sync_new_received_anchor = @sync_last_received_anchor + @sync_batch_size

		-- Determine how many batches are required during the initial synchronization.
		IF @sync_batch_count <= 0
		  SET @sync_batch_count = ((@sync_max_received_anchor / @sync_batch_size) - (@sync_last_received_anchor /  @sync_batch_size))

		END

       ELSE
       BEGIN

        SET @sync_new_received_anchor = @sync_last_received_anchor + @sync_batch_size

        -- Determine how many batches are required during subsequent synchronizations.
		IF @sync_batch_count <= 0
          SET @sync_batch_count = ((@sync_max_received_anchor / @sync_batch_size) - (@sync_new_received_anchor /  @sync_batch_size)) + 1  
       
	   END

       -- Check whether this is the last batch.      
       IF @sync_new_received_anchor >= @sync_max_received_anchor
       BEGIN

         SET @sync_new_received_anchor = @sync_max_received_anchor        
         IF @sync_batch_count <= 0
           SET @sync_batch_count = 1

       END
GO
--</snippetOCS_SQL_SyncSamplesSetup_Batching>

-- Create a stored procedure to insert a large number of rows
-- into the Customer and OrderHeader tables to demonstrate
-- batching. 
CREATE PROCEDURE usp_InsertCustomerAndOrderHeader
	@customer_inserts int,
	@orderheader_inserts int,
    @sets_of_inserts int
AS
	DECLARE @set_count int
	SET @set_count = 0
	DECLARE @c_inserted int
    SET @c_inserted = 0
	DECLARE @o_inserted int
    SET @o_inserted = 0
	DECLARE @customer_id uniqueidentifier
    DECLARE @InsertString nvarchar(1024)
	
	WHILE @set_count < @sets_of_inserts
    BEGIN
	  WHILE @c_inserted < @customer_inserts
	  BEGIN
		INSERT INTO Sales.Customer (CustomerName, SalesPerson, CustomerType)
	      VALUES (N'Rural Cycle Emporium', N'Brenda Diaz', N'Retail')
		SET @c_inserted = @c_inserted + 1
	  END
	  SET @c_inserted = 0
	
	  SELECT TOP 1 @customer_id = CustomerId FROM Sales.Customer
      SET @InsertString = 'INSERT INTO Sales.OrderHeader (CustomerId, OrderDate, OrderStatus) VALUES (''' + CAST(@customer_id AS nvarchar(38)) + ''', ''2007-01-28'', ''Complete'')'

	  WHILE @o_inserted < @orderheader_inserts
	  BEGIN
	    EXECUTE sp_executesql @InsertString
	    SET @o_inserted = @o_inserted + 1
	  END
      SET @o_inserted = 0	 
	  SET @set_count = @set_count + 1 
    END
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

	DELETE FROM Sales.Vendor
	DELETE FROM Sales.OrderDetail
	DELETE FROM Sales.OrderHeader
	DELETE FROM Sales.CustomerContact
	DELETE FROM Sales.Customer
	
	DELETE FROM Sales.Vendor_Tombstone
	DELETE FROM Sales.OrderDetail_Tombstone
	DELETE FROM Sales.OrderHeader_Tombstone
	DELETE FROM Sales.CustomerContact_Tombstone
	DELETE FROM Sales.Customer_Tombstone

	--Insert into Customer.
	INSERT INTO SyncSamplesDb.Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Aerobic Exercise Company', N'James Bailey', N'Wholesale')
	INSERT INTO SyncSamplesDb.Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Exemplary Cycles', N'James Bailey', N'Retail')
	INSERT INTO SyncSamplesDb.Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Tandem Bicycle Store', N'Brenda Diaz', N'Wholesale')
	INSERT INTO SyncSamplesDb.Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Rural Cycle Emporium', N'Brenda Diaz', N'Retail')
	INSERT INTO SyncSamplesDb.Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Sharp Bikes', N'Brenda Diaz', N'Retail')

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
	SELECT @CustomerId = CustomerId FROM SyncSamplesDb.Sales.Customer WHERE CustomerName = N'Exemplary Cycles'
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''959-555-0151'', ''Business'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderHeader.
	-- First order
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderHeader (CustomerId, OrderDate, OrderStatus) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''2007-01-28'', ''Complete'')'
	EXECUTE sp_executesql @InsertString
	-- Second order
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderHeader (CustomerId, OrderDate, OrderStatus) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''2007-02-03'', ''Pending'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderDetail.
	-- First order details
	SELECT @OrderId = OrderId FROM SyncSamplesDb.Sales.OrderHeader WHERE OrderDate = N'2007-01-28'
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (1, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Chain'', 3)'
	EXECUTE sp_executesql @InsertString
	-- Second order details
	SELECT @OrderId = OrderId FROM SyncSamplesDb.Sales.OrderHeader WHERE OrderDate = N'2007-02-03'
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (1, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Mountain Tire'', 7)'
	EXECUTE sp_executesql @InsertString

	----------------------------------
	-------- Third Customer ----------
	----------------------------------
	--Insert into CustomerContact.
	SELECT @CustomerId = CustomerId FROM SyncSamplesDb.Sales.Customer WHERE CustomerName = N'Tandem Bicycle Store'
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''107-555-0138'', ''Business'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderHeader.
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderHeader (CustomerId, OrderDate, OrderStatus) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''2007-02-04'', ''Complete'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderDetail.
	SELECT @OrderId = OrderId FROM SyncSamplesDb.Sales.OrderHeader WHERE OrderDate = N'2007-02-04'
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (1, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Road Pedal'', 4)'
	EXECUTE sp_executesql @InsertString
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (2, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Road Rear Wheel'', 4)'
	EXECUTE sp_executesql @InsertString

	----------------------------------
	------- Fourth Customer ----------
	----------------------------------
	--Insert into CustomerContact.
	SELECT @CustomerId = CustomerId FROM SyncSamplesDb.Sales.Customer WHERE CustomerName = N'Rural Cycle Emporium'
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''158-555-0142'', ''Business'')'
	EXECUTE sp_executesql @InsertString

	--Insert into CustomerContact (second contact info).
	SELECT @CustomerId = CustomerId FROM SyncSamplesDb.Sales.Customer WHERE CustomerName = N'Rural Cycle Emporium'
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''453-555-0167'', ''Mobile'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderHeader.
	-- First order
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderHeader (CustomerId, OrderDate, OrderStatus) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''2007-03-12'', ''Complete'')'
	EXECUTE sp_executesql @InsertString
	-- Second order
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderHeader (CustomerId, OrderDate, OrderStatus) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''2007-04-14'', ''Back Ordered'')'
	EXECUTE sp_executesql @InsertString

	--Insert into OrderDetail.
	--First order details
	SELECT @OrderId = OrderId FROM SyncSamplesDb.Sales.OrderHeader WHERE OrderDate = N'2007-03-12'
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (1, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Hydration Pack'', 1)'
	EXECUTE sp_executesql @InsertString
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (2, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Men''''s Sports Shorts'', 3)'
	EXECUTE sp_executesql @InsertString
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (3, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Water Bottle'', 6)'
	EXECUTE sp_executesql @InsertString
	--Second order details
	SELECT @OrderId = OrderId FROM SyncSamplesDb.Sales.OrderHeader WHERE OrderDate = N'2007-04-14'
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (1, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Mountain Tire'', 5)'
	EXECUTE sp_executesql @InsertString
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (2, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Women''''s Mountain Shorts'', 5)'
	EXECUTE sp_executesql @InsertString
	SET @InsertString = 'INSERT INTO SyncSamplesDb.Sales.OrderDetail (OrderDetailId, OrderId, Product, Quantity) VALUES (3, ''' + CAST(@OrderId AS nvarchar(38)) + ''', ''Road Pedal'', 2)'
	EXECUTE sp_executesql @InsertString

	----------------------------------
	-------- Fifth Customer ----------
	----------------------------------
	--No additional inserts for Sharp Bikes


	
	--Insert into Vendor.
	INSERT INTO SyncSamplesDb.Sales.Vendor (VendorName, CreditRating, PreferredVendor) VALUES (N'Premier Sport, Inc.', 2, 1)
	INSERT INTO SyncSamplesDb.Sales.Vendor (VendorName, CreditRating, PreferredVendor) VALUES (N'Metro Sport Equipment', 1, 1)
	INSERT INTO SyncSamplesDb.Sales.Vendor (VendorName, CreditRating, PreferredVendor) VALUES (N'Mountain Works', 3, 0)
	INSERT INTO SyncSamplesDb.Sales.Vendor (VendorName, CreditRating, PreferredVendor) VALUES (N'Green Lake Bike Company', 2, 1)
	INSERT INTO SyncSamplesDb.Sales.Vendor (VendorName, CreditRating, PreferredVendor) VALUES (N'Compete, Inc.', 5, 0)


	SET NOCOUNT OFF

GO -- End of usp_InsertSampleData

EXEC usp_InsertSampleData

------------------------------------
--
--Verify table creation and inserts.
--

SELECT * FROM SyncSamplesDb.Sales.Customer
SELECT * FROM SyncSamplesDb.Sales.Customer_Tombstone
SELECT * FROM SyncSamplesDb.Sales.CustomerContact
SELECT * FROM SyncSamplesDb.Sales.CustomerContact_Tombstone
SELECT * FROM SyncSamplesDb.Sales.OrderHeader
SELECT * FROM SyncSamplesDb.Sales.OrderHeader_Tombstone
SELECT * FROM SyncSamplesDb.Sales.OrderDetail
SELECT * FROM SyncSamplesDb.Sales.OrderDetail_Tombstone
SELECT * FROM SyncSamplesDb.Sales.Vendor
SELECT * FROM SyncSamplesDb.Sales.Vendor_Tombstone

SELECT c.CustomerId, c.CustomerName, c.SalesPerson, c.CustomerType, cc.PhoneNumber, cc.PhoneType,
oh.OrderId, oh.OrderDate, oh.OrderStatus, od.OrderDetailId, od.Product, od.Quantity
FROM SyncSamplesDb.Sales.Customer c 
	JOIN SyncSamplesDb.Sales.CustomerContact cc
		ON c.CustomerId = cc.CustomerId
	JOIN SyncSamplesDb.Sales.OrderHeader oh
		ON c.CustomerId = oh.CustomerId
	JOIN SyncSamplesDb.Sales.OrderDetail od
		ON oh.OrderId = od.OrderId
ORDER BY c.CustomerName, oh.OrderDate, od.OrderDetailId
--</snippetOCS_SQL_SyncSamplesSetup>