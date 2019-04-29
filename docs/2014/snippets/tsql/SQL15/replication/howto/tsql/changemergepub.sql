--------------------------------------------------------------------------
-- view the current settings for a merge publication
--------------------------------------------------------------------------
-- Returns the properties for the AdvWorksSalesOrders publication.

--<snippetsp_helpmergepublication>
DECLARE @publication AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';

USE [AdventureWorks2012]
EXEC sp_helpmergepublication @publication = @publication;
GO
--</snippetsp_helpmergepublication>

--------------------------------------------------------------------------
-- change a merge publication
--------------------------------------------------------------------------
-- Declarations for changing replication DDL setting for the 
-- AdvWorksSalesOrders publication.
--<snippetsp_changemergepublication>
DECLARE @publication AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge'; 

-- Disable DDL replication for the publication.
USE [AdventureWorks2012]
EXEC sp_changemergepublication 
  @publication = @publication, 
  @property = N'replicate_ddl', 
  @value = 0,
  @force_invalidate_snapshot = 0, 
  @force_reinit_subscription = 0;
GO
--</snippetsp_changemergepublication>
