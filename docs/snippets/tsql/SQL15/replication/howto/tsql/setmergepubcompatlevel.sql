:setvar Login N'REDMOND\glenga'
:setvar Password N'Time&Money'

-- Enable merge replication on the publication database, using the defaults.
USE master
EXEC sp_replicationdboption 
	@dbname = N'AdventureWorks2012', 
	@optname = N'merge publish',
	@value = N'true' ;
GO
--<snippetsp_mergepubcompatlevel_set>
-- To avoid storing the login and password in the script file, the values 
-- are passed into SQLCMD as scripting variables. For information about 
-- how to use scripting variables on the command line and in SQL Server
-- Management Studio, see the "Executing Replication Scripts" section in
-- the topic "Programming Replication Using System Stored Procedures".

--Add a new merge publication.
DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publicationDB = N'AdventureWorks2012'; 
SET @publication = N'AdvWorksSalesOrdersMerge' 
SET @login = $(Login);
SET @password = $(Password);

-- Create a new merge publication. 
USE [AdventureWorks2012]
EXEC sp_addmergepublication 
	@publication = @publication, 
	-- Set the compatibility level to SQL Server 2005.
	@publication_compatibility_level = '90RTM'; 

-- Create the snapshot job for the publication.
EXEC sp_addpublication_snapshot 
	@publication = @publication,
	@job_login = @login,
	@job_password = @password;
GO
--</snippetsp_mergepubcompatlevel_set>

--<snippetsp_mergepubcompatlevel_change>
DECLARE @publication AS sysname
SET @publication = N'AdvWorksSalesOrdersMerge' 

-- Change the publication compatibility level to 
-- SQL Server 2014.
EXEC sp_changemergepublication 
	@publication = @publication, 
	@property = N'publication_compatibility_level', 
	@value = N'120RTM'
GO
--</snippetsp_mergepubcompatlevel_change>

--<snippetsp_mergepubcompatlevel_get>
DECLARE @publication AS sysname
SET @publication = N'AdvWorksSalesOrdersMerge' 

EXEC sp_helpmergepublication 
	@publication = @publication;
GO
--</snippetsp_mergepubcompatlevel_get>