:setvar Login N'REDMOND\glenga'
:setvar Password N'Fun@Work'
:setvar Server N'glengatest2'
:setvar FtpLogin N'GLENGATEST2\REPLFTP_GLENGATEST2'
:setvar FtpPassword N'VicTor11'
:setvar AlternateFolder N'C:\ftppub\snapshots'

--------------------------------------------------------------------------
-- add a merge publication that uses FTP for snapshot delivery
--------------------------------------------------------------------------
--<snippetsp_createmergepub_ftp>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

-- Declarations for adding a merge publication.
DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @ftp_server AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
DECLARE @ftp_login AS sysname;
DECLARE @ftp_password AS sysname;
DECLARE @ftp_directory AS sysname;
DECLARE @snapshot_folder AS sysname;
DECLARE @article AS sysname;
DECLARE @owner AS sysname;
SET @publicationDB = N'AdventureWorks'; 
SET @publication = N'AdvWorksSalesOfferMergeFtp'; 
SET @ftp_server = $(Server);
SET @login = $(Login);
SET @password = $(Password);
SET @ftp_login = $(FtpLogin);
SET @ftp_password = $(FtpPassword);
SET @ftp_directory = N'\snapshots\ftp';
-- The snapshot folder is the root FTP folder on the server 
-- with the \snapshot subdirectory.
SET @snapshot_folder = $(AlternateFolder);
SET @article = N'SpecialOffer'; 
SET @owner = N'Sales' 

-- Enable merge replication on the publication database.
USE master
EXEC sp_replicationdboption 
	@dbname = @publicationDB, 
	@optname=N'merge publish',
	@value = N'true' ;

-- Create a new merge publication, enabling FTP snapshot delivery. 
-- Specify the publication compatibility level or it will default to 
-- SQL Server 2000.
USE [AdventureWorks]
EXEC sp_addmergepublication 
-- Specify the required parameters.
	@publication = @publication,
	@publication_compatibility_level = N'90RTM',
	@enabled_for_internet = N'true',
	@snapshot_in_defaultfolder = N'true',
	@alt_snapshot_folder = @snapshot_folder,
	@ftp_address = @ftp_server,
	@ftp_subdirectory = @ftp_directory,
	@ftp_login = @ftp_login,
	@ftp_password = @ftp_password;

-- Create the snapshot job for the publication, using the defaults.
EXEC sp_addpublication_snapshot 
	@publication = @publication, 
	@job_login = @login, 
	@job_password = @password;

-- Add an unfiltered article for the Customer table.
EXEC sp_addmergearticle 
	@publication = @publication, 
	@article = @article, 
	@source_object = @article, 
	@type = N'table', 
	@source_owner = @owner, 
	@destination_owner = @owner, 
	@column_tracking = N'true'; 

-- Start the snapshot job for the publication.
EXEC sp_startpublication_snapshot 
	@publication = @publication;
GO
--</snippetsp_createmergepub_ftp>