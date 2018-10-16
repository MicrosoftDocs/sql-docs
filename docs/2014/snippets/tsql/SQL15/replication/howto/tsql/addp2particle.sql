IF NOT EXISTS (SELECT * FROM sys.databases WHERE [name] = 'AdventureWorks2012Replica')
BEGIN
	CREATE DATABASE AdventureWorks2012Replica
END

--<snippetsp_addp2particle_createtables>
-- Create the new table at both nodes.
CREATE TABLE AdventureWorks2012.dbo.ProductTest (column1 int, Column2 int);
CREATE TABLE AdventureWorks2012Replica.dbo.ProductTest (column1 int, Column2 int);
GO
--</snippetsp_addp2particle_createtables>

/*
--<snippetsp_addp2particle_cmdline>
REM Bulk insert data into both the publication and subscription databases.
REM The BCP format depends on the snapshot format (native or character).
REM Execute at the command prompt.

bcp AdventureWorks2012..ProductTest in NewTable.bcp -T -SMYPUBLISHER n/c
bcp AdventureWorks2012Replica..ProductTest in NewTable.bcp -T -SMYPUBLISHER n/c
--</snippetsp_addp2particle_cmdline>
*/
--<snippetsp_addp2particle_createarticle>
--- Add the article to the publication.
DECLARE @publication AS sysname;
DECLARE @newtable AS sysname;
SET @publication = N'AdvWorksProductTran';
SET @newtable = N'ProductTest';

USE AdventureWorks2012

EXEC sp_addarticle 
  @publication = @publication,
  @article = @newtable,
  @source_table = @newtable,
  @destination_table = @newtable,
  @force_invalidate_snapshot = 0;
GO
--</snippetsp_addp2particle_createarticle>