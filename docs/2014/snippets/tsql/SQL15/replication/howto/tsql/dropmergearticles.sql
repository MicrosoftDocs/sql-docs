USE AdventureWorks2012
GO

--<snippetsp_DropMergeArticle>
DECLARE @publication AS sysname;
DECLARE @table1 AS sysname;
DECLARE @table2 AS sysname;
DECLARE @table3 AS sysname;
DECLARE @salesschema AS sysname;
DECLARE @hrschema AS sysname;
DECLARE @filterclause AS nvarchar(1000);
SET @publication = N'AdvWorksSalesOrdersMerge'; 
SET @table1 = N'Employee'; 
SET @table2 = N'SalesOrderHeader'; 
SET @table3 = N'SalesOrderDetail'; 
SET @salesschema = N'Sales';
SET @hrschema = N'HumanResources';
SET @filterclause = N'Employee.LoginID = HOST_NAME()';

-- Drop the merge join filter between SalesOrderHeader and SalesOrderDetail.
EXEC sp_dropmergefilter 
  @publication = @publication, 
  @article = @table3, 
  @filtername = N'SalesOrderDetail_SalesOrderHeader', 
  @force_invalidate_snapshot = 1, 
  @force_reinit_subscription = 1;

-- Drops the merge join filter between Employee and SalesOrderHeader.
EXEC sp_dropmergefilter 
  @publication = @publication, 
  @article = @table2, 
  @filtername = N'SalesOrderHeader_Employee', 
  @force_invalidate_snapshot = 1, 
  @force_reinit_subscription = 1;

-- Drops the article for the SalesOrderDetail table.
EXEC sp_dropmergearticle 
  @publication = @publication, 
  @article = @table3,
  @force_invalidate_snapshot = 1, 
  @force_reinit_subscription = 1;

-- Drops the article for the SalesOrderHeader table.
EXEC sp_dropmergearticle 
  @publication = @publication, 
  @article = @table2, 
  @force_invalidate_snapshot = 1, 
  @force_reinit_subscription = 1;

-- Drops the article for the Employee table.
EXEC sp_dropmergearticle 
  @publication = @publication, 
  @article = @table1,
  @force_invalidate_snapshot = 1, 
  @force_reinit_subscription = 1;
GO
--</snippetsp_DropMergeArticle>