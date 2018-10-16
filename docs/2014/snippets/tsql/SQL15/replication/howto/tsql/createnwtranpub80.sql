--<snippetsp_createtranpub_NWpreupgrade>
USE [Northwind]
GO

DECLARE @publication AS sysname
DECLARE @publicationDB AS sysname
DECLARE @article AS sysname
SET @publication = N'NwdProductTran'
SET @publicationDB = N'Northwind'
SET @article = N'Products'

-- Enable the replication database.
EXEC sp_replicationdboption 
    @dbname = @publicationDB, 
    @optname = N'publish', 
    @value = N'true'

-- Add the transactional publication.
EXEC sp_addpublication 
    @publication = @publication, 
    @sync_method = N'native', 
    @status = N'active', 
    @repl_freq = N'continuous', 
    @description = N'Transactional publication of Northwind.', 
    @allow_push = N'true', 
    @allow_pull = N'true', 
    @allow_sync_tran = N'true', 
    @autogen_sync_procs = N'true', 
    @allow_queued_tran = N'true'

-- Add a snapshot job.
EXEC sp_addpublication_snapshot 
    @publication = @publication

-- Add the transactional articles.
EXEC sp_addarticle 
    @publication = @publication, 
    @article = @article, 
    @source_owner = N'dbo', 
    @source_object = @article, 
    @destination_table = @article, 
    @type = N'logbased', 
    @schema_option = 0x00000000000080F3, 
    @ins_cmd = N'CALL sp_MSins_Products', 
    @del_cmd = N'XCALL sp_MSdel_Products', 
    @upd_cmd = N'XCALL sp_MSupd_Products', 
    @auto_identity_range = N'false'
GO
--</snippetsp_createtranpub_NWpreupgrade>
