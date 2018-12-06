--:setvar Login N'REDMOND\glenga'
--:setvar Password N'Love&War'
--:setvar InstanceName @@SERVERNAME
--<snippetsp_configbidi>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

USE master;
GO

-- Create the test databases if they do not exist.
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'test1')
	CREATE DATABASE test1;

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'test2')
	CREATE DATABASE test2;
GO

-- Enable the server as a Distributor, if not already done.
DECLARE @distributor AS sysname;
EXEC sp_helpdistributor @distributor = @distributor OUTPUT;
IF @distributor <> @@SERVERNAME
BEGIN
	EXEC master..sp_adddistributor @distributor = @@SERVERNAME;
	EXEC master..sp_adddistributiondb @database= 'distribution';
END
GO

-- Enable the server as a Publisher, if not already done.
IF NOT EXISTS (SELECT * FROM msdb..MSdistpublishers WHERE name = @@SERVERNAME)
	EXEC master..sp_adddistpublisher 
		@publisher = @@SERVERNAME, 
		@distribution_db = 'distribution', 
		@working_directory = 'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\repldata'; 
GO

-- Enable the databases for transactional publishing.
EXEC sp_replicationdboption N'test1', N'publish', true;
EXEC sp_replicationdboption N'test2', N'publish', true;
GO

-- Create a table in test1 and populate with 10 rows.
USE test1;
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name LIKE 'two_way_test1')
	DROP TABLE two_way_test1;
GO

CREATE TABLE two_way_test1
(pkcol int primary key not null,
 intcol int,
 charcol char(100),
 datecol datetime
);
GO

INSERT INTO two_way_test1 VALUES (1, 10, 'row1', GETDATE());
INSERT INTO two_way_test1 VALUES (2, 20, 'row2', GETDATE());
INSERT INTO two_way_test1 VALUES (3, 30, 'row3', GETDATE());
INSERT INTO two_way_test1 VALUES (4, 40, 'row4', GETDATE());
INSERT INTO two_way_test1 VALUES (5, 50, 'row5', GETDATE());
INSERT INTO two_way_test1 VALUES (6, 60, 'row6', GETDATE());
INSERT INTO two_way_test1 VALUES (7, 70, 'row7', GETDATE());
INSERT INTO two_way_test1 VALUES (8, 80, 'row8', GETDATE());
INSERT INTO two_way_test1 VALUES (9, 90, 'row9', GETDATE());
INSERT INTO two_way_test1 VALUES (10, 100, 'row10', GETDATE());
GO

-- Create a table in test2 and populate with 10 rows
USE test2;
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name LIKE 'two_way_test2')
	DROP TABLE two_way_test2;
GO

CREATE TABLE two_way_test2
(pkcol int primary key not null,
 intcol int,
 charcol char(100),
 datecol datetime
);
GO

INSERT INTO two_way_test2 VALUES (1, 10, 'row1', GETDATE());
INSERT INTO two_way_test2 VALUES (2, 20, 'row2', GETDATE());
INSERT INTO two_way_test2 VALUES (3, 30, 'row3', GETDATE());
INSERT INTO two_way_test2 VALUES (4, 40, 'row4', GETDATE());
INSERT INTO two_way_test2 VALUES (5, 50, 'row5', GETDATE());
INSERT INTO two_way_test2 VALUES (6, 60, 'row6', GETDATE());
INSERT INTO two_way_test2 VALUES (7, 70, 'row7', GETDATE());
INSERT INTO two_way_test2 VALUES (8, 80, 'row8', GETDATE());
INSERT INTO two_way_test2 VALUES (9, 90, 'row9', GETDATE());
INSERT INTO two_way_test2 VALUES (10, 100, 'row10', GETDATE());
GO

-- Add the transactional publication and article in test1
USE test1;
GO

DECLARE @publication AS sysname; 
DECLARE @article1 AS sysname; 
DECLARE @article2 AS sysname; 
DECLARE @login AS sysname;
DECLARE @password AS nvarchar(512);
SET @publication = N'two_way_pub_test1';
SET @article1 = N'two_way_test1';
SET @article2 = N'two_way_test2';
SET @login = $(Login);
SET @password = $(Password);

EXEC sp_addlogreader_agent 
	@job_login = @login, 
	@job_password = @password,
	@publisher_security_mode = 1;

EXEC sp_addpublication @publication = @publication, 
    @restricted = N'false', 
	@sync_method = N'native', 
    @repl_freq = N'continuous', 
	@description = N'publ1', 
    @status = N'active', 
	@allow_push = N'true', 
    @allow_pull = N'true', 
	@allow_anonymous = N'false', 
    @enabled_for_internet = N'false', 
    @independent_agent = N'true', 
	@immediate_sync = N'false', 
    @allow_sync_tran = N'false', 
    @autogen_sync_procs = N'false', 
	@retention = 60;

EXEC sp_addarticle @publication = @publication,
    @article = @article1, 
	@source_owner = N'dbo', 
    @source_object = @article1, 
    @destination_table = @article2, 
    @type = N'logbased', 
	@creation_script = null, 
    @description = null, 
	@pre_creation_cmd = N'drop', 
    @schema_option = 0x00000000000000F1, 
	@status = 16, 
    @vertical_partition = N'false', 
    @ins_cmd = N'CALL sp_ins_two_way_test2', 
    @del_cmd = N'XCALL sp_del_two_way_test2', 
    @upd_cmd = N'XCALL sp_upd_two_way_test2', 
    @filter = null, 
	@sync_object = null;
GO

-- Add the transactional publication and article in test2
USE test2
GO

DECLARE @publication AS sysname; 
DECLARE @article1 AS sysname; 
DECLARE @article2 AS sysname; 
DECLARE @login AS sysname;
DECLARE @password AS nvarchar(512);
SET @publication = N'two_way_pub_test2';
SET @article1 = N'two_way_test1';
SET @article2 = N'two_way_test2';
SET @login = $(Login);
SET @password = $(Password);

EXEC sp_addlogreader_agent 
	@job_login = @login, 
	@job_password = @password,
	@publisher_security_mode = 1;

EXEC sp_addpublication @publication = @publication, 
    @restricted = N'false', 
	@sync_method = N'native', 
    @repl_freq = N'continuous', 
	@description = N'Pub2',
    @status = N'active', 
	@allow_push = N'true', 
    @allow_pull = N'true', 
	@allow_anonymous = N'false', 
    @enabled_for_internet = N'false', 
    @independent_agent = N'true', 
	@immediate_sync = N'false', 
    @allow_sync_tran = N'false', 
    @autogen_sync_procs = N'false', 
	@retention = 60;

EXEC sp_addarticle @publication = @publication,
    @article = @article2, 
	@source_owner = N'dbo', 
    @source_object = @article2, 
	@destination_table = @article1, 
	@type = N'logbased', 
    @creation_script = null, 
    @description = null, 
	@pre_creation_cmd = N'drop', 
    @schema_option = 0x00000000000000F1, 
	@status = 16, 
    @vertical_partition = N'false', 
    @ins_cmd = N'CALL sp_ins_two_way_test1', 
    @del_cmd = N'XCALL sp_del_two_way_test1', 
    @upd_cmd = N'XCALL sp_upd_two_way_test1', 
    @filter = null, 
	@sync_object = null;
GO

-- Add the transactional subscription in test1
USE test1
GO

DECLARE @publication AS sysname;
DECLARE @subscriber AS sysname;
DECLARE @subscription_db AS sysname;
SET @publication = N'two_way_pub_test1';
SET @subscriber = $(SubServer2);
SET @subscription_db = N'test2';

EXEC sp_addsubscription @publication = @publication, 
    @article = N'all', 
	@subscriber = @subscriber, 
    @destination_db = @subscription_db, 
	@sync_type = N'none', 
    @status = N'active', 
	@update_mode = N'read only', 
    @loopback_detection = 'true';

EXEC sp_addpushsubscription_agent 
	@publication = @publication, 
	@subscriber = @subscriber, 
	@subscriber_db = @subscription_db, 
	@job_login = $(Login), 
	@job_password = $(Password);
GO

-- Add the transactional subscription in test2
USE test2
GO

DECLARE @publication AS sysname;
DECLARE @subscriber AS sysname;
DECLARE @subscription_db AS sysname;
SET @publication = N'two_way_pub_test2';
SET @subscriber = $(SubServer1);
SET @subscription_db = N'test1';

EXEC sp_addsubscription @publication = @publication, 
    @article = N'all', 
	@subscriber = @subscriber, 
    @destination_db = @subscription_db, 
	@sync_type = N'none', 
    @status = N'active', 
	@update_mode = N'read only', 
    @loopback_detection = 'true';

EXEC sp_addpushsubscription_agent 
	@publication = @publication, 
	@subscriber = @subscriber, 
	@subscriber_db = @subscription_db, 
	@job_login = $(Login), 
	@job_password = $(Password);
GO

-- Create custom stored procedures in test1 
USE test1
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name LIKE 'sp_ins_two_way_test1' and type = 'P')
    DROP proc sp_ins_two_way_test1;
IF EXISTS (SELECT * FROM sysobjects WHERE name LIKE 'sp_upd_two_way_test1' and type = 'P')
    DROP proc sp_upd_two_way_test1;
IF EXISTS (SELECT * FROM sysobjects WHERE name LIKE 'sp_del_two_way_test1' and type = 'P')
    DROP proc sp_del_two_way_test1;
GO

-- Insert procedure
CREATE proc sp_ins_two_way_test1 @pkcol int, 
    @intcol int, 
    @charcol char(100), 
    @datecol datetime
AS
    INSERT INTO two_way_test1 (pkcol, intcol, charcol, 
        datecol) 
    VALUES (@pkcol, @intcol, @charcol, GETDATE());
GO

-- Update procedure
CREATE proc sp_upd_two_way_test1 @old_pkcol int, 
    @old_intcol int, 
    @old_charcol char(100), 
    @old_datecol datetime,
    @pkcol int, @intcol int, 
    @charcol char(100), 
    @datecol datetime
AS
    -- IF intcol conflict is detected, add values
    -- IF charcol conflict detected, concatenate values
    DECLARE  @curr_intcol int, @curr_charcol char(100);
    
    SELECT @curr_intcol = intcol, @curr_charcol = charcol 
    FROM two_way_test1 WHERE pkcol = @pkcol;
  
    IF @curr_intcol != @old_intcol
        SELECT @intcol = @curr_intcol + 
            (@intcol - @old_intcol);
  
    IF @curr_charcol != @old_charcol
        SELECT @charcol = rtrim(@curr_charcol) + 
            '_' + rtrim(@charcol);
  
    UPDATE two_way_test1 SET intcol = @intcol, 
        charcol = @charcol, datecol = GETDATE()
    WHERE pkcol = @old_pkcol;
GO

-- Delete procedure
CREATE proc sp_del_two_way_test1 @old_pkcol int, 
    @old_intcol int, 
    @old_charcol char(100), 
    @old_datecol datetime
AS
    DELETE two_way_test1 WHERE pkcol = @old_pkcol;
GO

-- Create custom stored procedures in test2
USE test2
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name LIKE 'sp_ins_two_way_test2' and type = 'P')
    DROP proc sp_ins_two_way_test2;
IF EXISTS (SELECT * FROM sysobjects WHERE name LIKE 'sp_upd_two_way_test2' and type = 'P')
    DROP proc sp_upd_two_way_test2;
IF EXISTS (SELECT * FROM sysobjects WHERE name LIKE 'sp_del_two_way_test2' and type = 'P')
    DROP proc sp_del_two_way_test2;
GO

-- Insert procedure
CREATE proc sp_ins_two_way_test2 @pkcol int, 
    @intcol int, 
    @charcol char(100), 
    @datecol datetime
AS
    INSERT INTO two_way_test2 (pkcol, intcol, charcol,datecol) 
        VALUES (@pkcol, @intcol, @charcol, GETDATE());
GO

-- Update procedure
CREATE proc sp_upd_two_way_test2 @old_pkcol int, 
    @old_intcol int, 
    @old_charcol char(100), 
    @old_datecol datetime,
    @pkcol int, 
    @intcol int, 
    @charcol char(100), 
    @datecol datetime
AS
    -- IF intcol conflict is detected, add values
    -- IF charcol conflict detected, concatenate values
    DECLARE  @curr_intcol int, @curr_charcol char(100);
  
    SELECT @curr_intcol = intcol, @curr_charcol = charcol 
    FROM two_way_test2 WHERE pkcol = @pkcol;
  
    IF @curr_intcol != @old_intcol
        SELECT @intcol = @curr_intcol + 
            (@intcol - @old_intcol);
  
    IF @curr_charcol != @old_charcol
        SELECT @charcol = rtrim(@curr_charcol) + 
        '_' + rtrim(@charcol);
  
    UPDATE two_way_test2 SET intcol = @intcol, 
        charcol = @charcol, datecol = GETDATE() 
    WHERE pkcol = @old_pkcol;
GO

-- Delete procedure
CREATE proc sp_del_two_way_test2 @old_pkcol int, 
    @old_intcol int, 
    @old_charcol char(100), 
    @old_datecol datetime
AS
    DELETE two_way_test2 WHERE pkcol = @old_pkcol;
  
GO

-- Execute updates to the first row in test1 and test2
USE test1
GO
UPDATE two_way_test1 SET intcol = 20 , charcol = 'updated at test1' WHERE pkcol = 1;
  
USE test2
GO
UPDATE two_way_test2 SET intcol = 60 , charcol = 'updated at test2' WHERE pkcol = 1;

-- Select data from both tables to verify that the changes were propagated
SELECT * FROM test1..two_way_test1 WHERE pkcol = 1;
SELECT * FROM test2..two_way_test2 WHERE pkcol = 1;
GO
--</snippetsp_configbidi>

-- CLEAN UP BIDI PUBS
--From Sub1
USE test2
GO

EXEC sp_removedbreplication
GO

USE test1
GO

EXEC sp_removedbreplication
GO
USE master
GO

DROP DATABASE test1
GO
DROP DATABASE test2
GO
