--------------------------------------------------------------------------
-- add a transactional publication
--------------------------------------------------------------------------
--Declarations for adding a transactional publication
DECLARE @myPubDB AS sysname
DECLARE @myTranPub AS sysname
DECLARE @snapshare AS sysname;
SET @myPubDB = N'AdventureWorks2012' --publication database
SET @myTranPub = N'AdvWorksProductTranAltSnap' -- transactional publication name
SET @snapshare = CAST(SERVERPROPERTY('ServerName') AS sysname) + '\AltSnapshotFolder';

-- Enable transactional/snapshot replication on the publication database.
USE master
EXEC sp_replicationdboption @dbname=@myPubDB, @optname=N'publish',
@value = N'true' 

-- Create new transactional publication using an alternate snapshot folder. 
USE [AdventureWorks2012]
EXEC sp_addpublication @publication = @myTranPub, @snapshot_in_defaultfolder = N'false',
@alt_snapshot_folder = @snapshare, @status = N'active',
-- Other publication properties 
@description = N'Transactional publication of AdventureWorks database from Publisher MYDISTPUB.';

-- Create a new snapshot job for the publication, using the defaults.
EXEC sp_addpublication_snapshot @publication = @myTranPub
GO