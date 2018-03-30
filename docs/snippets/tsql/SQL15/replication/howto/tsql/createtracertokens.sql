--<snippetsp_tracertokens>
DECLARE @publication AS sysname;
DECLARE @tokenID AS int;
SET @publication = N'AdvWorksProductTran'; 

USE [AdventureWorks2012]

-- Insert a new tracer token in the publication database.
EXEC sys.sp_posttracertoken 
  @publication = @publication,
  @tracer_token_id = @tokenID OUTPUT;
SELECT 'The ID of the new tracer token is ''' + 
	CONVERT(varchar,@tokenID) + '''.'
GO

-- Wait 10 seconds for the token to make it to the Subscriber.
WAITFOR DELAY '00:00:10';
GO

-- Get latency information for the last inserted token.
DECLARE @publication AS sysname;
DECLARE @tokenID AS int;
SET @publication = N'AdvWorksProductTran'; 

CREATE TABLE #tokens (tracer_id int, publisher_commit datetime)

-- Return tracer token information to a temp table.
INSERT #tokens (tracer_id, publisher_commit)
EXEC sys.sp_helptracertokens @publication = @publication;
SET @tokenID = (SELECT TOP 1 tracer_id FROM #tokens
ORDER BY publisher_commit DESC)
DROP TABLE #tokens

-- Get history for the tracer token.
EXEC sys.sp_helptracertokenhistory 
  @publication = @publication, 
  @tracer_id = @tokenID;
GO
--</snippetsp_tracertokens>