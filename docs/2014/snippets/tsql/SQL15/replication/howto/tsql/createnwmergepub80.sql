--<snippetsp_createmergepub_NWpreupgrade>
-- Enable the replication database.
USE [Northwind]
GO

DECLARE @publicationDB AS sysname
DECLARE @publication AS sysname
DECLARE @article AS sysname
SET @publicationDB = N'Northwind' 
SET @publication = N'NwdCustomersMerge' 
SET @article = N'Customers' 

EXEC sp_replicationdboption 
    @dbname = @publicationDB, 
    @optname = N'merge publish', 
    @value = N'true'

-- Add the merge publication.
EXEC sp_addmergepublication 
    @publication = @publication, 
    @description = N'Merge publication of Northwind.', 
    @retention = 14, 
    @sync_mode = N'native', 
    @centralized_conflicts = N'true', 
    @dynamic_filters = N'false', 
    @keep_partition_changes = N'false'
 
EXEC sp_addpublication_snapshot 
    @publication = @publication

-- Add the merge articles.
EXEC sp_addmergearticle 
    @publication = @publication, 
    @article = @article, 
    @source_owner = N'dbo', 
    @source_object = @article, 
    @type = N'table', 
    @description = null, 
    @column_tracking = N'true', 
    @schema_option = 0x000000000000CFF1
 GO
--</snippetsp_createmergepub_NWpreupgrade>