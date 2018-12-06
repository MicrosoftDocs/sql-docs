--------------------------------------------------------------------------
-- view the current settings for a merge article
--------------------------------------------------------------------------
-- Returns the properties for articles in the AdvWorksSalesOrders publication.

--<snippetsp_helptranarticle>
DECLARE @publication AS sysname;
SET @publication = N'AdvWorksProductTran';

USE [AdventureWorks2012]
EXEC sp_helparticle
  @publication = @publication;
GO
--</snippetsp_helptranarticle>

--------------------------------------------------------------------------
-- change a transactional publication
--------------------------------------------------------------------------
--<snippetsp_changetranarticle>
DECLARE @publication AS sysname;
DECLARE @article AS sysname;
DECLARE @option AS int;
SET @publication = N'AdvWorksProductTran';
SET @article = N'Product';
SET @option = (SELECT CAST(0x0000000002030073 AS int));

-- Change the schema options to replicate schema with XML.
USE [AdventureWorks2012]
EXEC sp_changearticle 
  @publication = @publication,
  @article = @article, 
  @property = N'schema_option', 
  @value = @option,
  @force_invalidate_snapshot = 1;
GO
--</snippetsp_changetranarticle>
