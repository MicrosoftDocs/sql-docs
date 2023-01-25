---
title: Extended Events for Stretch Database
description: Extended Events for Stretch Database
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql-server-stretch-database
ms.topic: conceptual
ms.custom: seo-dt-2019
---
# Extended Events for Stretch Database

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

Stretch Database provides a set of extended events for troubleshooting.

For more info, see [Extended Events](../../relational-databases/extended-events/extended-events.md). For info about how to start an extended events session for troubleshooting, see [Create an Extended Events Session](/previous-versions/sql/sql-server-2016/hh213147(v=sql.130))

## List of extended events for Stretch Database

Event name|Event description
---------|---------
remote_data_archive_db_ddl|Occurs when the database T-SQL ddl for stretching data is processed.
remote_data_archive_provision_operation|Occurs when a provisioning operation starts or ends.
remote_data_archive_query_rewrite|Occurs when RelOp_Get is replaced during query rewrite for Stretch.
remote_data_archive_table_ddl|Occurs when the table T-SQL ddl for stretching data is processed.
remote_data_archive_telemetry|Occurs whenever an on premise system transmits a telemetry event to Azure DB.
remote_data_archive_telemetry_rejected|Occurs whenever an AzureDB Stretch telemetry event is rejected
repopulate_stretch_schema_task_queue_complete|Reports the completion of repopulating stretch schema task queue.
repopulate_stretch_schema_task_queue_start|Reports the start of repopulating stretch schema task queue.
stretch_codegen_errorlog|Reports the output from the code generator
stretch_codegen_start|Reports the start of stretch code generation
stretch_create_remote_table_start|Reports the start of remote table creation
stretch_database_disable_completed|Reports the completion of an ALTER DATABASE SET REMOTE_DATA_ARCHIVE OFF command
stretch_database_enable_completed|Reports the completion of an ALTER DATABASE SET REMOTE_DATA_ARCHIVE ON command
stretch_database_reauthorize_completed|Reports the completion of a sp_rda_reauthorize_db spec proc
stretch_index_reconciliation_codegen_completed|Reports the completion of code generation for stretch remote index operation
stretch_index_update_step_completed|Reports the duration of a stretched index update operation
stretch_migration_debug_trace|Debug trace of stretch migration actions.
stretch_migration_dequeue_migration|Event raised when a stretch migration task is dequeued for a database.
stretch_migration_queue_migration|Queue a packet for starting migration of the database and object.
stretch_migration_requeue_migration|Event raised when a stretch migration task packet is requeued.
stretch_migration_start_migration|Start migration of the database and object.
stretch_migration_start_unmigration|Start unmigration of the database and object.
stretch_remote_column_execution_completed|Reports the completion of remote execution for the generated code for a stretched column
stretch_remote_column_reconciliation_codegen_completed|Reports the completion of code generation for stretch remote column reconciliation
stretch_remote_index_execution_completed|Reports the completion of remote execution for the generated code for a stretched index
stretch_schema_queue_task|Reports when a packet is about to be queued for processing a schema task for the database and object.
stretch_schema_script_execution_completed|Reports the completion of stretch script execution during processing stretch schema task.
stretch_schema_script_execution_skipped|Reports the skipping of stretch script execution during processing stretch schema task.
stretch_schema_script_execution_start|Reports the start of stretch script execution during processing stretch schema task.
stretch_schema_task_failed|Reports the failure of a stretch schema function during the stretch schema task.
stretch_schema_task_skipped|Reports the stretch schema task is skipped during the stretch schema function.
stretch_schema_task_start|Reports the start of stretch schema function during the stretch schema task.
stretch_schema_task_succeeded|Reports the successful completion of stretch schema function during the stretch schema task.
stretch_sp_migration_get_batch_id|Call sp_stretch_get_batch_id
stretch_sync_metadata_start|Reports the start of metadata checks during the migration task.
stretch_table_codegen_completed|Reports the completion of code generation for a stretched table
stretch_table_complete_data_reconciliation|Complete data reconciliation of the database and object.
stretch_table_data_reconciliation_event|Reports the completion of the data reconciliation of a batch of rows
stretch_table_data_reconciliation_results_event|Reports an error or completion of a successful data reconciliation of several batches of rows
stretch_table_hinted_admin_delete_event|Reports the execution of a Stretch delete DML operation that uses an admin hint
stretch_table_hinted_admin_update_event|Reports the execution of a Stretch update DML operation that uses an admin hint
stretch_table_provisioning_step_completed|Reports the duration of a stretched table provisioning operation
stretch_table_query_error|Reports an error thrown during Stretch query rewrite
stretch_table_remote_creation_completed|Reports the completion of remote execution for the generated code for a stretched table
stretch_table_row_migration_event|Reports the completion of the migration of a batch of rows
stretch_table_row_migration_results_event|Reports an error or completion of a successful migration of several batches of rows
stretch_table_row_unmigration_event|Reports the completion of the unmigration of a batch of rows
stretch_table_row_unmigration_results_event|Reports an error or completion of a successful unmigration of several batches of rows
stretch_table_start_data_reconciliation|Start data reconciliation of the database and object.
stretch_table_unprovision_completed|Reports the completion removal of local resources for a table that was unstretched
stretch_table_validation_error|Reports the completion of validation for a table when the user enables stretch
stretch_unprovision_table_start|Reports the start of stretch table unprovisioning

## See also

- [Manage and troubleshoot Stretch Database](manage-and-troubleshoot-stretch-database.md)
