DECLARE @publication AS sysname;
DECLARE @article AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';
SET @article = N'SalesOrderHeader';

-- Enable column-level conflict tracking.
-- Changing this property requires that existing subscriptions
-- be reinitialized and that a new snapshot be generated.
USE [AdventureWorks2012]
EXEC sp_changemergearticle 
  @publication = @publication,
  @article = @article, 
  @property = N'column_tracking', 
  @value = N'true',
  @force_invalidate_snapshot = 1,
  @force_reinit_subscription = 1;
GO