--<snippetOCSv3_SQL_SyncSamplesSetupSqlPeer>
--
-- Create databases for the Sync Services peer synchronization samples
-- that use SqlSyncProvider.
--
USE master
GO

IF EXISTS (SELECT [name] FROM [master].[sys].[databases] 
			   WHERE [name] = N'SyncSamplesDb_SqlPeer1')
	BEGIN
	
		DECLARE @SQL varchar(max)
		SELECT @SQL = '';
	
		SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';' 
		FROM master..sysprocesses 
		WHERE DBId = DB_ID('SyncSamplesDb_SqlPeer1') AND SPId <> @@SPId

		EXEC(@SQL)
		
		DROP DATABASE SyncSamplesDb_SqlPeer1
		
	END

CREATE DATABASE SyncSamplesDb_SqlPeer1
GO


IF EXISTS (SELECT [name] FROM [master].[sys].[databases] 
			   WHERE [name] = N'SyncSamplesDb_SqlPeer2')
	
	BEGIN
	
		DECLARE @SQL varchar(max)
		SELECT @SQL = '';
	
		SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';' 
		FROM master..sysprocesses 
		WHERE DBId = DB_ID('SyncSamplesDb_SqlPeer2') AND SPId <> @@SPId

		EXEC(@SQL)
		
		DROP DATABASE SyncSamplesDb_SqlPeer2
		
	END

CREATE DATABASE SyncSamplesDb_SqlPeer2
GO

IF EXISTS (SELECT [name] FROM [master].[sys].[databases] 
			   WHERE [name] = N'SyncSamplesDb_SqlPeer3')
			   
	BEGIN
	
		DECLARE @SQL varchar(max)
		SELECT @SQL = '';
	
		SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';' 
		FROM master..sysprocesses 
		WHERE DBId = DB_ID('SyncSamplesDb_SqlPeer3') AND SPId <> @@SPId

		EXEC(@SQL)
		
		DROP DATABASE SyncSamplesDb_SqlPeer3
		
	END

CREATE DATABASE SyncSamplesDb_SqlPeer3
GO

USE SyncSamplesDb_SqlPeer1
GO

------------------------------------
--
-- Create two tables for the Sync Services peer synchronization samples.
--

--<snippetOCSv3_SQL_SyncSamplesSetupSqlPeer_CustomerTable>
CREATE TABLE Customer(
	CustomerId uniqueidentifier NOT NULL PRIMARY KEY DEFAULT NEWID(), 
	CustomerName nvarchar(100) NOT NULL,
	SalesPerson nvarchar(100) NOT NULL,
	CustomerType nvarchar(100) NOT NULL)
--</snippetOCSv3_SQL_SyncSamplesSetupSqlPeer_CustomerTable>

CREATE TABLE CustomerContact(
	CustomerId uniqueidentifier NOT NULL,
	PhoneNumber nvarchar(100) NOT NULL,
	PhoneType nvarchar(100) NOT NULL,
	CONSTRAINT PK_CustomerContact PRIMARY KEY (CustomerId, PhoneType))

ALTER TABLE CustomerContact
ADD CONSTRAINT FK_CustomerContact_Customer FOREIGN KEY (CustomerId)
	REFERENCES Customer (CustomerId)
	
GO

------------------------------------
-- Insert test data.
--
--
-- Wrap the inserts in a procedure so that each snippet
-- can call the procedure to reset the database after
-- the snippet completes.
CREATE PROCEDURE usp_ResetSqlPeerData

AS
	SET NOCOUNT ON
	
	--INSERT INTO Customer.
	INSERT INTO Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Aerobic Exercise Company', N'James Bailey', N'Wholesale')
	INSERT INTO Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Exemplary Cycles', N'James Bailey', N'Retail')
	INSERT INTO Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Tandem Bicycle Store', N'Brenda Diaz', N'Wholesale')
	INSERT INTO Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Rural Cycle Emporium', N'Brenda Diaz', N'Retail')
	INSERT INTO Customer (CustomerName, SalesPerson, CustomerType) VALUES (N'Sharp Bikes', N'Brenda Diaz', N'Retail')

	--Declare variables that are used in subsequent inserts.
	DECLARE @CustomerId uniqueidentifier
	DECLARE @InsertString nvarchar(1024)

	--INSERT INTO CustomerContact.
	SELECT @CustomerId = CustomerId FROM Customer WHERE CustomerName = N'Exemplary Cycles'
	SET @InsertString = 'INSERT INTO CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''959-555-0151'', ''Business'')'
	EXECUTE sp_executesql @InsertString

	SELECT @CustomerId = CustomerId FROM Customer WHERE CustomerName = N'Tandem Bicycle Store'
	SET @InsertString = 'INSERT INTO CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''107-555-0138'', ''Business'')'
	EXECUTE sp_executesql @InsertString

	SELECT @CustomerId = CustomerId FROM Customer WHERE CustomerName = N'Rural Cycle Emporium'
	SET @InsertString = 'INSERT INTO CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''158-555-0142'', ''Business'')'
	EXECUTE sp_executesql @InsertString

	SELECT @CustomerId = CustomerId FROM Customer WHERE CustomerName = N'Rural Cycle Emporium'
	SET @InsertString = 'INSERT INTO CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''453-555-0167'', ''Mobile'')'
	EXECUTE sp_executesql @InsertString

	SET NOCOUNT OFF

GO

EXEC usp_ResetSqlPeerData
--</snippetOCSv3_SQL_SyncSamplesSetupSqlPeer>

/*

	IF EXISTS (SELECT [name] FROM [sys].[objects] 
				   WHERE [name] = N'CustomerContact_tracking')
		DROP TABLE CustomerContact_tracking
		
	IF EXISTS (SELECT [name] FROM [sys].[objects] 
		   WHERE [name] = N'Customer_tracking')
		DROP TABLE Customer_tracking

	IF EXISTS (SELECT [name] FROM [sys].[objects] 
		   WHERE [name] = N'scope_config')
		DROP TABLE scope_config

	IF EXISTS (SELECT [name] FROM [sys].[objects] 
		   WHERE [name] = N'scope_info')
		DROP TABLE scope_info

*/