<snippetCheckForSyncTriggerSub>
DECLARE @retcode int, @trigger_op char(10), @table_id int
SELECT @table_id = object_id('tablename')
EXEC @retcode = sp_check_for_sync_trigger @table_id, @trigger_op OUTPUT
IF @retcode = 1
RETURN
</snippetCheckForSyncTriggerSub>

<snippetCheckForSyncTriggerPub>
DECLARE @retcode int, @trigger_op char(10), @table_id int, @fonpublisher int
SELECT @table_id = object_id('tablename')
SELECT @fonpublisher = 1
EXEC @retcode = sp_check_for_sync_trigger @table_id, @trigger_op OUTPUT, @fonpublisher
IF @retcode = 1
RETURN
</snippetCheckForSyncTriggerPub>