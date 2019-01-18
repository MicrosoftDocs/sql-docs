--<snippetsp_StartMergeSnapshot>
-- Start the snapshot agent if not already running.
DECLARE @publication AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';

EXEC sp_startpublication_snapshot
      @publication = @publication;
GO
--</snippetsp_StartMergeSnapshot>