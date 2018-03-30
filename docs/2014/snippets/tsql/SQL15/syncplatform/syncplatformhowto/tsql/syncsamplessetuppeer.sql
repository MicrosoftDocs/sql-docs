--<snippetOCSv2_SQL_SyncSamplesSetupPeer>
--
-- Create databases for the Sync Services peer synchronization samples.
-- Enable snapshot isolation, which is recommended for databases that
-- participate in sychronization.
--
USE master
GO

IF EXISTS (SELECT [name] FROM [master].[sys].[databases] 
			   WHERE [name] = N'SyncSamplesDb_Peer1')
	BEGIN
	
		DECLARE @SQL varchar(max)
		SELECT @SQL = '';
	
		SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';' 
		FROM master..sysprocesses 
		WHERE DBId = DB_ID('SyncSamplesDb_Peer1') AND SPId <> @@SPId

		EXEC(@SQL)
		
		DROP DATABASE SyncSamplesDb_Peer1
		
	END

CREATE DATABASE SyncSamplesDb_Peer1
GO

ALTER DATABASE SyncSamplesDb_Peer1 SET ALLOW_SNAPSHOT_ISOLATION ON
GO


IF EXISTS (SELECT [name] FROM [master].[sys].[databases] 
			   WHERE [name] = N'SyncSamplesDb_Peer2')
	
	BEGIN
	
		DECLARE @SQL varchar(max)
		SELECT @SQL = '';
	
		SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';' 
		FROM master..sysprocesses 
		WHERE DBId = DB_ID('SyncSamplesDb_Peer2') AND SPId <> @@SPId

		EXEC(@SQL)
		
		DROP DATABASE SyncSamplesDb_Peer2
		
	END

CREATE DATABASE SyncSamplesDb_Peer2
GO

ALTER DATABASE SyncSamplesDb_Peer2 SET ALLOW_SNAPSHOT_ISOLATION ON
GO


IF EXISTS (SELECT [name] FROM [master].[sys].[databases] 
			   WHERE [name] = N'SyncSamplesDb_Peer3')
			   
	BEGIN
	
		DECLARE @SQL varchar(max)
		SELECT @SQL = '';
	
		SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';' 
		FROM master..sysprocesses 
		WHERE DBId = DB_ID('SyncSamplesDb_Peer3') AND SPId <> @@SPId

		EXEC(@SQL)
		
		DROP DATABASE SyncSamplesDb_Peer3
		
	END

CREATE DATABASE SyncSamplesDb_Peer3
GO

ALTER DATABASE SyncSamplesDb_Peer3 SET ALLOW_SNAPSHOT_ISOLATION ON



-- Loop through the three sample peer databases and 
-- create objects in each database.
DECLARE @DbNames nvarchar(100)
SET @DbNames = 'SyncSamplesDb_Peer3__SyncSamplesDb_Peer2__SyncSamplesDb_Peer1__'

DECLARE @CurrentDbName nvarchar(100)

WHILE LEN(@DbNames) > 0
BEGIN

	SET @CurrentDbName = SUBSTRING(@DbNames, LEN(@DbNames) - 20 , 19)
	PRINT 'Creating objects in database ' + @CurrentDbName

	IF @CurrentDbName = 'SyncSamplesDb_Peer1'
	BEGIN
		USE SyncSamplesDb_Peer1		
	END
	ELSE IF @CurrentDbName = 'SyncSamplesDb_Peer2'
	BEGIN
		USE SyncSamplesDb_Peer2
	END
	ELSE IF @CurrentDbName = 'SyncSamplesDb_Peer3'
	BEGIN
		USE SyncSamplesDb_Peer3
	END

------------------------------------
--
-- Create two tables for the Sync Services peer synchronization samples.
--
EXEC('

CREATE SCHEMA Sales

--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerTable>
CREATE TABLE Sales.Customer(
	CustomerId uniqueidentifier NOT NULL PRIMARY KEY DEFAULT NEWID(), 
	CustomerName nvarchar(100) NOT NULL,
	SalesPerson nvarchar(100) NOT NULL,
	CustomerType nvarchar(100) NOT NULL)
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerTable>

CREATE TABLE Sales.CustomerContact(
	CustomerId uniqueidentifier NOT NULL,
	PhoneNumber nvarchar(100) NOT NULL,
	PhoneType nvarchar(100) NOT NULL,
	CONSTRAINT PK_CustomerContact PRIMARY KEY (CustomerId, PhoneType))
')

EXEC('
ALTER TABLE Sales.CustomerContact
ADD CONSTRAINT FK_CustomerContact_Customer FOREIGN KEY (CustomerId)
	REFERENCES Sales.Customer (CustomerId)
')

------------------------------------
--
-- Create tables to store scope metadata and to identify which
-- tables are included in each scope. Create indexes, and insert rows
-- for the "Sales" scope and its tables. Scope is not related to 
-- the database schema name, but in this example, they are both named "Sales".
--
EXEC('

CREATE SCHEMA Sync

--<snippetOCSv2_SQL_SyncSamplesSetupPeer_ScopeInfo>
CREATE TABLE Sync.ScopeInfo(       
	scope_local_id int IDENTITY(1,1),
    scope_id uniqueidentifier default NEWID(),
    scope_name nvarchar(100) NOT NULL PRIMARY KEY,
    scope_sync_knowledge varbinary(max) NULL,
    scope_tombstone_cleanup_knowledge varbinary(max) NULL,
    scope_timestamp timestamp,
    scope_cleanup_timestamp bigint)
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_ScopeInfo>

--<snippetOCSv2_SQL_SyncSamplesSetupPeer_ScopeTableMap>
CREATE TABLE Sync.ScopeTableMap(    	
    scope_name nvarchar(100) ,
    table_name nvarchar(100)     
    )
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_ScopeTableMap>
    
')

EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_ScopeTableMapIndex>
CREATE UNIQUE CLUSTERED INDEX Clustered_ScopeTableMap ON Sync.ScopeTableMap(scope_name, table_name)
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_ScopeTableMapIndex>
')

EXEC('
SET NOCOUNT ON
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_ScopeMetadataInserts>
INSERT INTO Sync.ScopeInfo(scope_name) VALUES (''Sales'')
INSERT INTO Sync.ScopeTableMap(scope_name, table_name) VALUES (''Sales'', ''Sales.Customer'')
INSERT INTO Sync.ScopeTableMap(scope_name, table_name) VALUES (''Sales'', ''Sales.CustomerContact'')
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_ScopeMetadataInserts>
SET NOCOUNT OFF
')

------------------------------------
-- Create tracking tables for each base table.
--
EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerTrackingTable>
CREATE TABLE Sync.Customer_Tracking(
    
    CustomerId uniqueidentifier NOT NULL PRIMARY KEY,          
    
    update_scope_local_id int NULL, 
    scope_update_peer_key int,
    scope_update_peer_timestamp bigint,
    local_update_peer_key int,
    local_update_peer_timestamp timestamp,

    create_scope_local_id int NULL,
    scope_create_peer_key int,
    scope_create_peer_timestamp bigint,
    local_create_peer_key int,
    local_create_peer_timestamp bigint,

    sync_row_is_tombstone int, 
    restore_timestamp bigint, 
    last_change_datetime datetime default NULL)
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerTrackingTable>

CREATE TABLE Sync.CustomerContact_Tracking(

    CustomerId uniqueidentifier NOT NULL, 
	PhoneType nvarchar(100) NOT NULL,
	         
    update_scope_local_id int NULL, 
    scope_update_peer_key int,
    scope_update_peer_timestamp bigint,
    local_update_peer_key int,
    local_update_peer_timestamp timestamp,

    create_scope_local_id int NULL,
    scope_create_peer_key int,
    scope_create_peer_timestamp bigint,
    local_create_peer_key int,
    local_create_peer_timestamp bigint,

    sync_row_is_tombstone int, 
    restore_timestamp bigint, 
    last_change_datetime datetime DEFAULT NULL)
') 

EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_Customer_TrackingIndex>
CREATE NONCLUSTERED INDEX NonClustered_Customer_Tracking
ON Sync.Customer_Tracking ([local_update_peer_timestamp])
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_Customer_TrackingIndex>
')

EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerContact_TrackingIndex>
CREATE NONCLUSTERED INDEX NonClustered_CustomerContact_Tracking
ON Sync.CustomerContact_Tracking ([local_update_peer_timestamp])
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerContact_TrackingIndex>
')-- End of EXEC for table creation.

------------------------------------
-- Create triggers to insert information into tracking tables.
--

-- Insert triggers
EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerInsertTrigger>
CREATE TRIGGER Customer_InsertTrigger ON Sales.Customer FOR INSERT
AS
    
    UPDATE Sync.Customer_Tracking SET sync_row_is_tombstone = 0, 
	local_update_peer_key = 0, restore_timestamp = NULL, update_scope_local_id = NULL, 
	last_change_datetime = GetDate() FROM Sync.Customer_Tracking t JOIN inserted i ON t.[CustomerId] = i.[CustomerId]
    
    INSERT INTO Sync.Customer_Tracking([CustomerId], create_scope_local_id, local_create_peer_key, local_create_peer_timestamp, update_scope_local_id, local_update_peer_key, restore_timestamp, sync_row_is_tombstone, last_change_datetime)
    SELECT [CustomerId], NULL, 0, @@DBTS+1, NULL, 0, NULL, 0, GetDate()
	FROM inserted WHERE  ( [CustomerId] NOT IN (SELECT [CustomerId] FROM Sync.Customer_Tracking ) )		
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerInsertTrigger>
')

EXEC('
CREATE TRIGGER CustomerContact_InsertTrigger ON Sales.CustomerContact FOR INSERT
AS
    
    UPDATE Sync.CustomerContact_Tracking SET sync_row_is_tombstone = 0, 
	local_update_peer_key = 0, restore_timestamp = NULL, update_scope_local_id = NULL, 
	last_change_datetime = GetDate() FROM Sync.CustomerContact_Tracking t JOIN inserted i ON t.[CustomerId] = i.[CustomerId] AND t.[PhoneType] = i.[PhoneType]
    
    INSERT INTO Sync.CustomerContact_Tracking([CustomerId], [PhoneType], create_scope_local_id, local_create_peer_key, local_create_peer_timestamp, update_scope_local_id, local_update_peer_key, restore_timestamp, sync_row_is_tombstone, last_change_datetime)
    SELECT [CustomerId], [PhoneType], NULL, 0, @@DBTS+1, NULL, 0, NULL, 0, GetDate()
    FROM inserted WHERE  ( 
    [CustomerId] NOT IN (SELECT [CustomerId] FROM Sync.CustomerContact_Tracking ) AND
    [PhoneType] NOT IN (SELECT [PhoneType] FROM Sync.CustomerContact_Tracking ))
')


-- Update triggers
EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerUpdateTrigger>
CREATE TRIGGER Customer_UpdateTrigger ON Sales.Customer FOR UPDATE
AS    
    UPDATE t    
	SET 
		update_scope_local_id = NULL, local_update_peer_key = 0, 
		restore_timestamp = NULL, last_change_datetime = GetDate() 
	FROM Sync.Customer_Tracking t JOIN inserted i ON t.[CustomerId] = i.[CustomerId]     	
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerUpdateTrigger>
')

EXEC('
CREATE TRIGGER CustomerContact_UpdateTrigger ON Sales.CustomerContact FOR UPDATE
AS    
    UPDATE t    
	SET 
		update_scope_local_id = NULL, local_update_peer_key = 0, 
		restore_timestamp = NULL, last_change_datetime = GetDate() 
	FROM Sync.CustomerContact_Tracking t JOIN inserted i ON t.[CustomerId] = i.[CustomerId]	AND
	t.[PhoneType] = i.[PhoneType]
')


-- Delete triggers
EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerDeleteTrigger>
CREATE TRIGGER Customer_DeleteTrigger ON Sales.Customer FOR DELETE
AS
BEGIN
    UPDATE t 
        SET 
			sync_row_is_tombstone = 1, update_scope_local_id = NULL, 
			local_update_peer_key = 0, restore_timestamp = NULL,
			last_change_datetime = GetDate() 
        FROM Sync.Customer_Tracking t JOIN deleted d ON t.[CustomerId] = d.[CustomerId]
END 
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerDeleteTrigger>
')

EXEC('
CREATE TRIGGER CustomerContact_DeleteTrigger ON Sales.CustomerContact FOR DELETE
AS
BEGIN
    UPDATE t 
        SET 
			sync_row_is_tombstone = 1, update_scope_local_id = NULL, 
			local_update_peer_key = 0, restore_timestamp = NULL,
			last_change_datetime = GetDate() 
        FROM Sync.CustomerContact_Tracking t JOIN deleted d ON t.[CustomerId] = d.[CustomerId] AND
		t.[PhoneType] = d.[PhoneType]
END   	
')


------------------------------------
--
-- Create stored procedures that SELECT and apply data and metadata changes.
--

-- Procedures to select incremental changes from each table.


EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcSelectChanges>
create procedure Sync.sp_Customer_SelectChanges (
    @sync_min_timestamp bigint,
    @sync_metadata_only int,
    @sync_scope_local_id int,
    @sync_initialize int
)
as

--if @sync_initialize = 0
--begin
	-- Perform additional logic if required.
--end
	
begin
    select  t.CustomerId, 
			c.CustomerName,
			c.SalesPerson,
			c.CustomerType, 
            t.sync_row_is_tombstone,
            t.local_update_peer_timestamp as sync_row_timestamp, 
            case when (t.update_scope_local_id is null or t.update_scope_local_id <> @sync_scope_local_id) 
                 then case when (t.restore_timestamp is null) then t.local_update_peer_timestamp else t.restore_timestamp end else t.scope_update_peer_timestamp end as sync_update_peer_timestamp,
            case when (t.update_scope_local_id is null or t.update_scope_local_id <> @sync_scope_local_id) 
                 then t.local_update_peer_key else t.scope_update_peer_key end as sync_update_peer_key,
            case when (t.create_scope_local_id is null or t.create_scope_local_id <> @sync_scope_local_id) 
                 then t.local_create_peer_timestamp else t.scope_create_peer_timestamp end as sync_create_peer_timestamp,
            case when (t.create_scope_local_id is null or t.create_scope_local_id <> @sync_scope_local_id) 
                 then t.local_create_peer_key else t.scope_create_peer_key end as sync_create_peer_key
    from Sales.Customer c right join Sync.Customer_Tracking t on c.CustomerId = t.CustomerId
    where t.local_update_peer_timestamp > @sync_min_timestamp
end
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcSelectChanges>
')
  
EXEC('
create procedure Sync.sp_CustomerContact_SelectChanges (
    @sync_min_timestamp bigint,
    @sync_metadata_only int,
    @sync_scope_local_id int,
    @sync_initialize int
)
as

--if @sync_initialize = 0
--begin
	-- Perform additional logic if required.
--end

begin
    select  t.CustomerId, 
			t.PhoneType,
            c.PhoneNumber, 
            t.sync_row_is_tombstone,
            t.local_update_peer_timestamp as sync_row_timestamp, 
            case when (t.update_scope_local_id is null or t.update_scope_local_id <> @sync_scope_local_id) 
                 then case when (t.restore_timestamp is null) then t.local_update_peer_timestamp else t.restore_timestamp end else t.scope_update_peer_timestamp end as sync_update_peer_timestamp,
            case when (t.update_scope_local_id is null or t.update_scope_local_id <> @sync_scope_local_id) 
                 then t.local_update_peer_key else t.scope_update_peer_key end as sync_update_peer_key,
            case when (t.create_scope_local_id is null or t.create_scope_local_id <> @sync_scope_local_id) 
                 then t.local_create_peer_timestamp else t.scope_create_peer_timestamp end as sync_create_peer_timestamp,
            case when (t.create_scope_local_id is null or t.create_scope_local_id <> @sync_scope_local_id) 
                 then t.local_create_peer_key else t.scope_create_peer_key end as sync_create_peer_key
    from Sales.CustomerContact c right join Sync.CustomerContact_Tracking t on c.CustomerId = t.CustomerId and c.PhoneType = t.PhoneType
    where t.local_update_peer_timestamp > @sync_min_timestamp
end
')

-- Procedures to apply incremental inserts to each base table
-- and metadata tracking table.
EXEC('
CREATE PROCEDURE Sync.sp_Customer_ApplyInsert (						
        @CustomerId uniqueidentifier,
		@CustomerName nvarchar(100),
		@SalesPerson nvarchar(100),
		@CustomerType nvarchar(100),
		@sync_row_count int OUT)        
AS

    IF NOT EXISTS (SELECT CustomerId FROM Sync.Customer_Tracking 
					WHERE CustomerId = @CustomerId)
	    INSERT INTO Sales.Customer (CustomerId, CustomerName, SalesPerson, CustomerType) 
	    VALUES (@CustomerId, @CustomerName, @SalesPerson, @CustomerType)
	    SET @sync_row_count = @@rowcount
')

EXEC('
create procedure Sync.sp_Customer_InsertMetadata (
        @CustomerId uniqueidentifier,
        @sync_scope_local_id int,
        @sync_row_is_tombstone int,
        @sync_create_peer_key int ,
        @sync_create_peer_timestamp bigint,                 
        @sync_update_peer_key int ,
        @sync_update_peer_timestamp timestamp,                              
        @sync_check_concurrency int,    
        @sync_row_timestamp timestamp,  
        @sync_row_count int out)        
as  
begin
update Sync.Customer_Tracking set 
    [create_scope_local_id] = @sync_scope_local_id, 
    [scope_create_peer_key] = @sync_create_peer_key,
    [scope_create_peer_timestamp] = @sync_create_peer_timestamp,
    [local_create_peer_key] = 0,
    [local_create_peer_timestamp] = @@DBTS+1,
    [update_scope_local_id] = @sync_scope_local_id,
    [scope_update_peer_key] = @sync_update_peer_key,
    [scope_update_peer_timestamp] = @sync_update_peer_timestamp,
    [local_update_peer_key] = 0,
    [restore_timestamp] = NULL,
    [sync_row_is_tombstone] = @sync_row_is_tombstone 
    where [CustomerId] = @CustomerId
    and (@sync_check_concurrency = 0 or local_update_peer_timestamp = @sync_row_timestamp) 
    
	set @sync_row_count = @@ROWCOUNT
	
	if (@sync_row_count = 0 )
	begin
		-- inserting metadata for row if it does not already exist
		-- this can happen if a node sees a delete for a row it never had, we insert only metadata
		-- for the row in that case
		insert into Sync.Customer_Tracking (	
		[CustomerId],
		[create_scope_local_id], 
		[scope_create_peer_key],
		[scope_create_peer_timestamp],
		[local_create_peer_key],
		[local_create_peer_timestamp],
		[update_scope_local_id],
		[scope_update_peer_key],
		[scope_update_peer_timestamp],
		[local_update_peer_key],
		[restore_timestamp],
		[sync_row_is_tombstone] )values (
		@CustomerId,
		@sync_scope_local_id, 
		@sync_create_peer_key,
		@sync_create_peer_timestamp,
		0,
		@@DBTS+1,
		@sync_scope_local_id,
		@sync_update_peer_key,
		@sync_update_peer_timestamp,
		0,
		NULL,
		@sync_row_is_tombstone)
		set @sync_row_count = @@ROWCOUNT
	end
end
')


EXEC('
CREATE PROCEDURE Sync.sp_CustomerContact_ApplyInsert (				
        @CustomerId uniqueidentifier,
		@PhoneNumber nvarchar(100),
		@PhoneType nvarchar(100),
		@sync_row_count int OUT)        
AS	
    IF NOT EXISTS (SELECT CustomerId, PhoneType FROM Sync.CustomerContact_Tracking 
					WHERE CustomerId = @CustomerId AND PhoneType = @PhoneType)
	    INSERT INTO Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) 
		VALUES (@CustomerId, @PhoneNumber, @PhoneType)
	    SET @sync_row_count = @@rowcount	
')

EXEC('
create procedure Sync.sp_CustomerContact_InsertMetadata (
        @CustomerId uniqueidentifier,
		@PhoneType nvarchar(100),
        @sync_scope_local_id int,
        @sync_row_is_tombstone int,
        @sync_create_peer_key int ,
        @sync_create_peer_timestamp bigint,                 
        @sync_update_peer_key int ,
        @sync_update_peer_timestamp timestamp,                              
        @sync_check_concurrency int,    
        @sync_row_timestamp timestamp,  
        @sync_row_count int out)        
as  
begin
update Sync.CustomerContact_Tracking set 
    [create_scope_local_id] = @sync_scope_local_id, 
    [scope_create_peer_key] = @sync_create_peer_key,
    [scope_create_peer_timestamp] = @sync_create_peer_timestamp,
    [local_create_peer_key] = 0,
    [local_create_peer_timestamp] = @@DBTS+1,
    [update_scope_local_id] = @sync_scope_local_id,
    [scope_update_peer_key] = @sync_update_peer_key,
    [scope_update_peer_timestamp] = @sync_update_peer_timestamp,
    [local_update_peer_key] = 0,
    [restore_timestamp] = NULL,
    [sync_row_is_tombstone] = @sync_row_is_tombstone 
    where [CustomerId] = @CustomerId and [PhoneType] = @PhoneType
    and (@sync_check_concurrency = 0 or local_update_peer_timestamp = @sync_row_timestamp) 
    
	set @sync_row_count = @@ROWCOUNT
	
	if (@sync_row_count = 0 )
	begin
		-- inserting metadata for row if it does not already exist
		-- this can happen if a node sees a delete for a row it never had, we insert only metadata
		-- for the row in that case
		insert into Sync.CustomerContact_Tracking (	
		[CustomerId],
		[PhoneType],
		[create_scope_local_id], 
		[scope_create_peer_key],
		[scope_create_peer_timestamp],
		[local_create_peer_key],
		[local_create_peer_timestamp],
		[update_scope_local_id],
		[scope_update_peer_key],
		[scope_update_peer_timestamp],
		[local_update_peer_key],
		[restore_timestamp],
		[sync_row_is_tombstone] )values (
		@CustomerId,
		@PhoneType,
		@sync_scope_local_id, 
		@sync_create_peer_key,
		@sync_create_peer_timestamp,
		0,
		@@DBTS+1,
		@sync_scope_local_id,
		@sync_update_peer_key,
		@sync_update_peer_timestamp,
		0,
		NULL,
		@sync_row_is_tombstone)
		set @sync_row_count = @@ROWCOUNT
	end
end                
')

-- Procedures to apply incremental updates to each base table
-- and metadata tracking table.
EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcApplyUpdate>
CREATE PROCEDURE Sync.sp_Customer_ApplyUpdate (									
        @CustomerId uniqueidentifier,
		@CustomerName nvarchar(100),
		@SalesPerson nvarchar(100),
		@CustomerType nvarchar(100),
		@sync_min_timestamp bigint , 								
		@sync_row_count int OUT,
		@sync_force_write int)        
AS		
	--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcApplyUpdate_UPDATE>
	UPDATE c
	SET c.CustomerName = @CustomerName, c.SalesPerson = @SalesPerson, c.CustomerType = @CustomerType      
	FROM Sales.Customer c JOIN Sync.Customer_Tracking t ON c.CustomerId = t.CustomerId
	WHERE ((t.local_update_peer_timestamp <= @sync_min_timestamp) OR @sync_force_write = 1)
		AND t.CustomerId = @CustomerId  
	SET @sync_row_count = @@rowcount
	--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcApplyUpdate_UPDATE>
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcApplyUpdate>                   		
')


EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcUpdateMetadata>
create procedure Sync.sp_Customer_UpdateMetadata (
		@CustomerId uniqueidentifier,
		@sync_scope_local_id int,
        @sync_row_is_tombstone int,
        @sync_create_peer_key int,
        @sync_create_peer_timestamp bigint,                 
        @sync_update_peer_key int,
        @sync_update_peer_timestamp timestamp,                      
        @sync_row_timestamp timestamp,
        @sync_check_concurrency int,        
        @sync_row_count int out)        
as	
	declare @was_tombstone int
	select @was_tombstone = sync_row_is_tombstone from Sync.Customer_Tracking 
	where CustomerId = @CustomerId
	
	if (@was_tombstone is not null and @was_tombstone=1 and @sync_row_is_tombstone=0)
		-- tombstone is getting resurrected, update creation version as well
		update Sync.Customer_Tracking set
			[update_scope_local_id] = @sync_scope_local_id, 
            [scope_update_peer_key] = @sync_update_peer_key,
            [scope_update_peer_timestamp] = @sync_update_peer_timestamp,
            [local_update_peer_key] = 0,
            [restore_timestamp] = NULL,
            [create_scope_local_id] = @sync_scope_local_id, 
            [scope_create_peer_key] = @sync_create_peer_key, 
            [scope_create_peer_timestamp] =  @sync_create_peer_timestamp, 
            [sync_row_is_tombstone] = @sync_row_is_tombstone 						
		where CustomerId = @CustomerId 			
		and (@sync_check_concurrency = 0 or local_update_peer_timestamp = @sync_row_timestamp)
	else	
		update Sync.Customer_Tracking set
			[update_scope_local_id] = @sync_scope_local_id, 
            [scope_update_peer_key] = @sync_update_peer_key,
            [scope_update_peer_timestamp] = @sync_update_peer_timestamp,
            [local_update_peer_key] = 0,
            [restore_timestamp] = NULL,
            [sync_row_is_tombstone] = @sync_row_is_tombstone 						
		where CustomerId = @CustomerId 			
		and (@sync_check_concurrency = 0 or local_update_peer_timestamp = @sync_row_timestamp)
	set @sync_row_count = @@rowcount
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcUpdateMetadata>   
')

EXEC('
CREATE PROCEDURE Sync.sp_CustomerContact_ApplyUpdate (											
        @CustomerId uniqueidentifier,
		@PhoneNumber nvarchar(100),
		@PhoneType nvarchar(100),
		@sync_min_timestamp bigint ,
		@sync_row_count int OUT,
		@sync_force_write int)        
AS    	
	
	UPDATE c
	SET c.PhoneNumber = @PhoneNumber, c.PhoneType = @PhoneType    
	FROM Sales.CustomerContact c JOIN Sync.CustomerContact_Tracking t ON c.CustomerId = t.CustomerId AND 
		c.PhoneType = t.PhoneType
	WHERE ((t.local_update_peer_timestamp <= @sync_min_timestamp) OR @sync_force_write = 1)
		AND t.CustomerId = @CustomerId  
		AND t.PhoneType = @PhoneType
	SET @sync_row_count = @@rowcount
')

EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcUpdateMetadata>
create procedure Sync.sp_CustomerContact_UpdateMetadata (
		@CustomerId uniqueidentifier,
		@PhoneType nvarchar(100),
		@sync_scope_local_id int,
        @sync_row_is_tombstone int,
        @sync_create_peer_key int ,
        @sync_create_peer_timestamp bigint,                 
        @sync_update_peer_key int ,
        @sync_update_peer_timestamp timestamp,                      
        @sync_row_timestamp timestamp,
        @sync_check_concurrency int,        
        @sync_row_count int out)        
as	
	declare @was_tombstone int
	select @was_tombstone = sync_row_is_tombstone from Sync.CustomerContact_Tracking 
	where CustomerId = @CustomerId and PhoneType = @PhoneType
	
	if (@was_tombstone is not null and @was_tombstone=1 and @sync_row_is_tombstone=0)
		-- tombstone is getting resurrected, update creation version as well
		update Sync.CustomerContact_Tracking set
			[update_scope_local_id] = @sync_scope_local_id, 
            [scope_update_peer_key] = @sync_update_peer_key,
            [scope_update_peer_timestamp] = @sync_update_peer_timestamp,
            [local_update_peer_key] = 0,
            [restore_timestamp] = NULL,
            [create_scope_local_id] = @sync_scope_local_id, 
            [scope_create_peer_key] = @sync_create_peer_key, 
            [scope_create_peer_timestamp] =  @sync_create_peer_timestamp, 
            [sync_row_is_tombstone] = @sync_row_is_tombstone 						
		where CustomerId = @CustomerId and PhoneType = @PhoneType	
		and (@sync_check_concurrency = 0 or local_update_peer_timestamp = @sync_row_timestamp)
	else	
		update Sync.CustomerContact_Tracking set
			[update_scope_local_id] = @sync_scope_local_id, 
            [scope_update_peer_key] = @sync_update_peer_key,
            [scope_update_peer_timestamp] = @sync_update_peer_timestamp,
            [local_update_peer_key] = 0,
            [restore_timestamp] = NULL,
            [sync_row_is_tombstone] = @sync_row_is_tombstone 						
		where CustomerId = @CustomerId and PhoneType = @PhoneType 
		and (@sync_check_concurrency = 0 or local_update_peer_timestamp = @sync_row_timestamp)
	set @sync_row_count = @@rowcount
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcUpdateMetadata>   
')


-- Procedures to apply incremental deletes to each base table
-- and metadata tracking table.
EXEC('
CREATE PROCEDURE Sync.sp_Customer_ApplyDelete(
	@CustomerId uniqueidentifier,	
	@sync_min_timestamp bigint,
	@sync_force_write int,	     	
	@sync_row_count int OUT)	 
AS  
	DELETE c
	FROM Sales.Customer c JOIN Sync.Customer_Tracking t ON c.CustomerId = t.CustomerId
	WHERE (t.local_update_peer_timestamp <= @sync_min_timestamp OR @sync_force_write = 1)       
		AND t.CustomerId = @CustomerId            
	SET @sync_row_count = @@rowcount              
')

EXEC('
CREATE PROCEDURE Sync.sp_Customer_DeleteMetadata(
    @CustomerId uniqueidentifier,			
	@sync_row_timestamp timestamp,	
	@sync_check_concurrency int,	
	@sync_row_count int OUT) 	
AS    
	DELETE t
	FROM Sync.Customer_Tracking t 
	WHERE t.CustomerId = @CustomerId 
		AND (@sync_check_concurrency = 0 OR t.local_update_peer_timestamp = @sync_row_timestamp)
	SET @sync_row_count = @@rowcount           	
')

EXEC('
CREATE PROCEDURE Sync.sp_CustomerContact_ApplyDelete(
	@CustomerId uniqueidentifier,
	@PhoneType nvarchar(100),
	@sync_min_timestamp bigint,
	@sync_force_write int,			
	@sync_row_count int OUT)	    
AS	
	DELETE c
	FROM Sales.CustomerContact c JOIN Sync.CustomerContact_Tracking t ON c.CustomerId = t.CustomerId
		AND c.PhoneType = t.PhoneType
	WHERE (t.local_update_peer_timestamp <= @sync_min_timestamp OR @sync_force_write = 1)         
		AND t.CustomerId = @CustomerId
		AND t.PhoneType = @PhoneType         
	SET @sync_row_count = @@rowcount              
')

EXEC('
CREATE PROCEDURE Sync.sp_CustomerContact_DeleteMetadata(	
    @CustomerId uniqueidentifier,
	@PhoneType nvarchar(100),			
	@sync_row_timestamp timestamp,	
    @sync_check_concurrency int,	
	@sync_row_count int OUT) 	
AS    
	DELETE t
	FROM Sync.CustomerContact_Tracking t 
	WHERE t.CustomerId = @CustomerId AND t.PhoneType = @PhoneType
		AND (@sync_check_concurrency = 0 OR t.local_update_peer_timestamp = @sync_row_timestamp)
	SET @sync_row_count = @@rowcount           	
')


-- Procedures to select conflicting rows from each base table and
-- metadata tracking table.
EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcSelectRow>
create procedure Sync.sp_Customer_SelectRow
        @CustomerId uniqueidentifier,
		@sync_scope_local_id int
as
	select  t.CustomerId, 
			c.CustomerName,
			c.SalesPerson,
			c.CustomerType, 
            t.sync_row_is_tombstone,
            t.local_update_peer_timestamp as sync_row_timestamp, 
            case when (t.update_scope_local_id is null or t.update_scope_local_id <> @sync_scope_local_id) 
                 then case when (t.restore_timestamp is null) then t.local_update_peer_timestamp else t.restore_timestamp end else t.scope_update_peer_timestamp end as sync_update_peer_timestamp,
            case when (t.update_scope_local_id is null or t.update_scope_local_id <> @sync_scope_local_id) 
                 then t.local_update_peer_key else t.scope_update_peer_key end as sync_update_peer_key,
            case when (t.create_scope_local_id is null or t.create_scope_local_id <> @sync_scope_local_id) 
                 then t.local_create_peer_timestamp else t.scope_create_peer_timestamp end as sync_create_peer_timestamp,
            case when (t.create_scope_local_id is null or t.create_scope_local_id <> @sync_scope_local_id) 
                 then t.local_create_peer_key else t.scope_create_peer_key end as sync_create_peer_key
    from Sales.Customer c right join Sync.Customer_Tracking t on c.CustomerId = t.CustomerId    
    where c.CustomerId = @CustomerId 
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcSelectRow>
')

EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerContactProcSelectRow>
create procedure Sync.sp_CustomerContact_SelectRow
        @CustomerId uniqueidentifier,
        @PhoneType nvarchar(100),
		@sync_scope_local_id int
as
	select  t.CustomerId, 
			t.PhoneType,
            c.PhoneNumber, 
            t.sync_row_is_tombstone,
            t.local_update_peer_timestamp as sync_row_timestamp, 
            case when (t.update_scope_local_id is null or t.update_scope_local_id <> @sync_scope_local_id) 
                 then case when (t.restore_timestamp is null) then t.local_update_peer_timestamp else t.restore_timestamp end else t.scope_update_peer_timestamp end as sync_update_peer_timestamp,
            case when (t.update_scope_local_id is null or t.update_scope_local_id <> @sync_scope_local_id) 
                 then t.local_update_peer_key else t.scope_update_peer_key end as sync_update_peer_key,
            case when (t.create_scope_local_id is null or t.create_scope_local_id <> @sync_scope_local_id) 
                 then t.local_create_peer_timestamp else t.scope_create_peer_timestamp end as sync_create_peer_timestamp,
            case when (t.create_scope_local_id is null or t.create_scope_local_id <> @sync_scope_local_id) 
                 then t.local_create_peer_key else t.scope_create_peer_key end as sync_create_peer_key
    from Sales.CustomerContact c right join Sync.CustomerContact_Tracking t on c.CustomerId = t.CustomerId and c.PhoneType = t.PhoneType   
    where c.CustomerId = @CustomerId and c.PhoneType = @PhoneType
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerContactProcSelectRow>
')


-- Procedures to select metadata that can be cleaned up from each
-- metadata tracking table.
EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcSelectMetadata>
CREATE PROCEDURE Sync.sp_Customer_SelectMetadata     
	@metadata_aging_in_days int,
	@sync_scope_local_id int
AS
	IF @metadata_aging_in_days = -1
		BEGIN
			SELECT	CustomerId,
					local_update_peer_timestamp as sync_row_timestamp,  
					case when (update_scope_local_id is null or update_scope_local_id <> @sync_scope_local_id) 
						then case when (restore_timestamp is null) then local_update_peer_timestamp else restore_timestamp end else scope_update_peer_timestamp end as sync_update_peer_timestamp,
					case when (update_scope_local_id is null or update_scope_local_id <> @sync_scope_local_id) 
						then local_update_peer_key else scope_update_peer_key end as sync_update_peer_key,
					case when (create_scope_local_id is null or create_scope_local_id <> @sync_scope_local_id) 
						then local_create_peer_timestamp else scope_create_peer_timestamp end as sync_create_peer_timestamp,
					case when (create_scope_local_id is null or create_scope_local_id <> @sync_scope_local_id) 
						then local_create_peer_key else scope_create_peer_key end as sync_create_peer_key
			FROM Sync.Customer_Tracking
			WHERE sync_row_is_tombstone = 1
		END
	
	ELSE
		BEGIN
			SELECT	CustomerId,
					local_update_peer_timestamp as sync_row_timestamp,  
					case when (update_scope_local_id is null or update_scope_local_id <> @sync_scope_local_id) 
						then case when (restore_timestamp is null) then local_update_peer_timestamp else restore_timestamp end else scope_update_peer_timestamp end as sync_update_peer_timestamp,
					case when (update_scope_local_id is null or update_scope_local_id <> @sync_scope_local_id) 
						then local_update_peer_key else scope_update_peer_key end as sync_update_peer_key,
					case when (create_scope_local_id is null or create_scope_local_id <> @sync_scope_local_id) 
						then local_create_peer_timestamp else scope_create_peer_timestamp end as sync_create_peer_timestamp,
					case when (create_scope_local_id is null or create_scope_local_id <> @sync_scope_local_id) 
						then local_create_peer_key else scope_create_peer_key end as sync_create_peer_key
			FROM Sync.Customer_Tracking
			WHERE sync_row_is_tombstone = 1 AND
			DATEDIFF(day, last_change_datetime, GETDATE()) > @metadata_aging_in_days
		END
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_CustomerProcSelectMetadata>
')

EXEC('
CREATE PROCEDURE Sync.sp_CustomerContact_SelectMetadata  
	@metadata_aging_in_days int,
	@sync_scope_local_id int  
AS
	IF @metadata_aging_in_days = -1
		BEGIN
			SELECT	CustomerId,
					PhoneType,
					local_update_peer_timestamp as sync_row_timestamp,  
					case when (update_scope_local_id is null or update_scope_local_id <> @sync_scope_local_id) 
						then case when (restore_timestamp is null) then local_update_peer_timestamp else restore_timestamp end else scope_update_peer_timestamp end as sync_update_peer_timestamp,
					case when (update_scope_local_id is null or update_scope_local_id <> @sync_scope_local_id) 
						then local_update_peer_key else scope_update_peer_key end as sync_update_peer_key,
					case when (create_scope_local_id is null or create_scope_local_id <> @sync_scope_local_id) 
						then local_create_peer_timestamp else scope_create_peer_timestamp end as sync_create_peer_timestamp,
					case when (create_scope_local_id is null or create_scope_local_id <> @sync_scope_local_id) 
						then local_create_peer_key else scope_create_peer_key end as sync_create_peer_key 
					FROM Sync.CustomerContact_Tracking
			WHERE sync_row_is_tombstone = 1
		END
	
	ELSE
		BEGIN
			SELECT	CustomerId,
					PhoneType,
					local_update_peer_timestamp as sync_row_timestamp,  
					case when (update_scope_local_id is null or update_scope_local_id <> @sync_scope_local_id) 
						then case when (restore_timestamp is null) then local_update_peer_timestamp else restore_timestamp end else scope_update_peer_timestamp end as sync_update_peer_timestamp,
					case when (update_scope_local_id is null or update_scope_local_id <> @sync_scope_local_id) 
						then local_update_peer_key else scope_update_peer_key end as sync_update_peer_key,
					case when (create_scope_local_id is null or create_scope_local_id <> @sync_scope_local_id) 
						then local_create_peer_timestamp else scope_create_peer_timestamp end as sync_create_peer_timestamp,
					case when (create_scope_local_id is null or create_scope_local_id <> @sync_scope_local_id) 
						then local_create_peer_key else scope_create_peer_key end as sync_create_peer_key
			FROM Sync.CustomerContact_Tracking
			WHERE sync_row_is_tombstone = 1 AND
			DATEDIFF(day, last_change_datetime, GETDATE()) > @metadata_aging_in_days
		END
')

EXEC('
--<snippetOCSv2_SQL_SyncSamplesSetupPeer_ProcSelectSharedScopes>
CREATE PROCEDURE Sync.sp_SelectSharedScopes
      @sync_scope_name nvarchar(100)      
AS
   SELECT ScopeTableMap2.table_name AS sync_table_name, 
          ScopeTableMap2.scope_name AS sync_shared_scope_name
   FROM Sync.ScopeTableMap ScopeTableMap1 JOIN Sync.ScopeTableMap ScopeTableMap2
   ON ScopeTableMap1.table_name = ScopeTableMap2.table_name
   AND ScopeTableMap1.scope_name = @sync_scope_name
   WHERE ScopeTableMap2.scope_name <> @sync_scope_name
--</snippetOCSv2_SQL_SyncSamplesSetupPeer_ProcSelectSharedScopes>
')

------------------------------------
-- Insert test data.
--
--
-- Wrap the inserts in a procedure so that each snippet
-- can call the procedure to reset the database after
-- the snippet completes. The procecure for the first
-- peer includes inserts into the base tables. The other
-- peers receive inserts during the first syncyhronization
-- session.
IF @CurrentDbName <> 'SyncSamplesDb_Peer1'
BEGIN

EXEC('
CREATE PROCEDURE usp_ResetPeerData

AS
	SET NOCOUNT ON

	DELETE FROM Sync.CustomerContact_Tracking
	DELETE FROM Sync.Customer_Tracking
	DELETE FROM Sales.CustomerContact
	DELETE FROM Sales.Customer
	
	SET NOCOUNT OFF
')
END

ELSE
BEGIN

EXEC('
CREATE PROCEDURE usp_ResetPeerData

AS
	SET NOCOUNT ON

	DELETE FROM Sync.CustomerContact_Tracking
	DELETE FROM Sync.Customer_Tracking
	DELETE FROM Sales.CustomerContact
	DELETE FROM Sales.Customer

	--INSERT INTO Customer.
	INSERT INTO Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N''Aerobic Exercise Company'', N''James Bailey'', N''Wholesale'')
	INSERT INTO Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N''Exemplary Cycles'', N''James Bailey'', N''Retail'')
	INSERT INTO Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N''Tandem Bicycle Store'', N''Brenda Diaz'', N''Wholesale'')
	INSERT INTO Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N''Rural Cycle Emporium'', N''Brenda Diaz'', N''Retail'')
	INSERT INTO Sales.Customer (CustomerName, SalesPerson, CustomerType) VALUES (N''Sharp Bikes'', N''Brenda Diaz'', N''Retail'')

	--Declare variables that are used in subsequent inserts.
	DECLARE @CustomerId uniqueidentifier
	DECLARE @InsertString nvarchar(1024)

	--INSERT INTO CustomerContact.
	SELECT @CustomerId = CustomerId FROM Sales.Customer WHERE CustomerName = N''Exemplary Cycles''
	SET @InsertString = ''INSERT INTO Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES ('''''' + CAST(@CustomerId AS nvarchar(38)) + '''''', ''''959-555-0151'''', ''''Business'''')''
	EXECUTE sp_executesql @InsertString

	SELECT @CustomerId = CustomerId FROM Sales.Customer WHERE CustomerName = N''Tandem Bicycle Store''
	SET @InsertString = ''INSERT INTO Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES ('''''' + CAST(@CustomerId AS nvarchar(38)) + '''''', ''''107-555-0138'''', ''''Business'''')''
	EXECUTE sp_executesql @InsertString

	SELECT @CustomerId = CustomerId FROM Sales.Customer WHERE CustomerName = N''Rural Cycle Emporium''
	SET @InsertString = ''INSERT INTO Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES ('''''' + CAST(@CustomerId AS nvarchar(38)) + '''''', ''''158-555-0142'''', ''''Business'''')''
	EXECUTE sp_executesql @InsertString

	SELECT @CustomerId = CustomerId FROM Sales.Customer WHERE CustomerName = N''Rural Cycle Emporium''
	SET @InsertString = ''INSERT INTO Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) VALUES ('''''' + CAST(@CustomerId AS nvarchar(38)) + '''''', ''''453-555-0167'''', ''''Mobile'''')''
	EXECUTE sp_executesql @InsertString

	SET NOCOUNT OFF
') -- End of usp_ResetPeerData
END

EXEC usp_ResetPeerData

SET @DbNames = SUBSTRING(@DbNames, 0, LEN(@DbNames) - 20)

END -- End of loop to create database objects.
--</snippetOCSv2_SQL_SyncSamplesSetupPeer>