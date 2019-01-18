:setvar Login N'REDMOND\glenga'
:setvar Password N'Love&War'
:setvar Server N'glengatest2'

--<snippetsp_createmergepub_NWpostupgrade>
-- To avoid storing the login and password in the script file, the value 
-- is passed into SQLCMD as a scripting variable. For information about 
-- how to use scripting variables on the command line and in SQL Server
-- Management Studio, see the "Executing Replication Scripts" section in
-- the topic "Programming Replication Using System Stored Procedures".

-- Enabling the replication database
-- Enable the replication database.
USE [Northwind]
GO

DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @article AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publicationDB = N'Northwind';
SET @publication = N'NwdCustomersMerge';
SET @article = N'Customers';
-- Specify the Windows account to run the Snapshot Agent.
SET @login = $(Login); 
-- Supply the password at runtime.
SET @password = $(Password); 

EXEC sp_replicationdboption 
	@dbname = @publicationDB, 
	@optname = N'merge publish', 
	@value = N'true';

-- Add the merge publication.
EXEC sp_addmergepublication 
	@publication = @publication, 
	@description = N'Merge publication of Northwind.', 
	@retention = 14, 
	@sync_mode = N'native', 
	@dynamic_filters = N'false', 
	@keep_partition_changes = N'false',
	-- Only set to '90RTM' if all Subscribers are SQL Server 2005.
	@publication_compatibility_level = N'90RTM',
	@replicate_ddl = 1,
	@allow_subscriber_initiated_snapshot = N'true',
	@allow_web_synchronization = N'false',
	@allow_partition_realignment = N'true',
	@retention_period_unit = N'day',
	@automatic_reinitialization_policy = 0,
	@conflict_logging = N'both';
 
EXEC sp_addpublication_snapshot 
	@publication = @publication, 
	@job_login = @login,
	@job_password = @password;

-- Add the merge article.
EXEC sp_addmergearticle 
	@publication = @publication, 
	@article = @article, 
	@source_owner = N'dbo', 
	@source_object = @article, 
	@type = N'table', 
	@description = null, 
	@column_tracking = N'true', 
	@schema_option = 0x0000000000034FD1,
	@partition_options = 0,
	@subscriber_upload_options = 0,
	@identityrangemanagementoption = N'manual',
	@delete_tracking = N'true',
	@compensate_for_errors = N'false',
	@stream_blob_columns = N'true';
GO
--</snippetsp_createmergepub_NWpostupgrade>