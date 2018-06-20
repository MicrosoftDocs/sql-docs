:setvar Login N'REDMOND\glenga'
:setvar Password N'Love&War'
:setvar InstanceName N'GLENGATEST2'

--------------------------------------------------------------------------
-- add a merge publication
--------------------------------------------------------------------------
--<snippetsp_mergealtsnapshot>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

--Declarations for adding a merge publication
DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @article AS sysname;
DECLARE @owner AS sysname;
DECLARE @snapshot_share AS sysname;
SET @publicationDB = N'AdventureWorks2012'; 
SET @publication = N'AdvWorksSalesOrdersMergeAltSnapshot'; 
SET @article = N'SpecialOffer';
SET @owner = N'Sales';
SET @snapshot_share = '\\' + $(InstanceName) + '\AltSnapshotFolder';

-- Enable merge replication on the publication database, using defaults.
USE master
EXEC sp_replicationdboption 
	@dbname = @publicationDB, 
	@optname=N'merge publish',
	@value = N'true';

-- Create new merge publication with an alternate snapshot location. 
USE [AdventureWorks]
EXEC sp_addmergepublication 
-- required parameters
	@publication = @publication, 
	@snapshot_in_defaultfolder = N'false',
	@alt_snapshot_folder = @snapshot_share,
	@compress_snapshot = N'true';

-- Create the snapshot job for the publication.
EXEC sp_addpublication_snapshot 
	@publication = @publication,
	@job_login = $(Login),
	@job_password = $(Password);

-- Add an article.
EXEC sp_addmergearticle 
	@publication = @publication, 
	@article = @article, 
	@source_object = @article, 
	@type = N'table', 
	@source_owner = @owner, 
	@destination_owner = @owner;

-- Start the snapshot job.
EXEC sp_startpublication_snapshot
	@publication = @publication;
GO
--</snippetsp_mergealtsnapshot>