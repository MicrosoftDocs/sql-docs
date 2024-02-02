---
title: "Database Engine stored procedures (Transact-SQL)"
description: "Database Engine stored procedures (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "Database Engine [SQL Server], stored procedures"
  - "system stored procedures [SQL Server], Database Engine"
  - "stored procedures [SQL Server], Database Engine"
dev_langs:
  - "TSQL"
---
# Database Engine stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that are used for general maintenance of an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

:::row:::
    :::column:::
        [sp_add_data_file_recover_suspect_db](sp-add-data-file-recover-suspect-db-transact-sql.md)

        [sp_add_log_file_recover_suspect_db](sp-add-log-file-recover-suspect-db-transact-sql.md)

        [sp_addextendedproc](sp-addextendedproc-transact-sql.md)

        [sp_addextendedproperty](sp-addextendedproperty-transact-sql.md)

        [sp_addmessage](sp-addmessage-transact-sql.md)

        [sp_addtype](sp-addtype-transact-sql.md)

        [sp_addumpdevice](sp-addumpdevice-transact-sql.md)

        [sp_altermessage](sp-altermessage-transact-sql.md)

        [sp_attach_db](sp-attach-db-transact-sql.md)

        [sp_attach_single_file_db](sp-attach-single-file-db-transact-sql.md)

        [sp_autostats](sp-autostats-transact-sql.md)

        [sp_bindefault](sp-bindefault-transact-sql.md)

        [sp_bindrule](sp-bindrule-transact-sql.md)

        [sp_bindsession](sp-bindsession-transact-sql.md)

        [sp_certify_removable](sp-certify-removable-transact-sql.md)

        [sp_clean_db_file_free_space](sp-clean-db-file-free-space-transact-sql.md)

        [sp_clean_db_free_space](sp-clean-db-free-space-transact-sql.md)

        [sp_configure](sp-configure-transact-sql.md)

        [sp_control_plan_guide](sp-control-plan-guide-transact-sql.md)

        [sp_create_plan_guide](sp-create-plan-guide-transact-sql.md)

        [sp_create_plan_guide_from_handle](sp-create-plan-guide-from-handle-transact-sql.md)

        [sp_create_removable](sp-create-removable-transact-sql.md)

        [sp_createstats](sp-createstats-transact-sql.md)

        [sp_cycle_errorlog](sp-cycle-errorlog-transact-sql.md)

        [sp_datatype_info](sp-datatype-info-transact-sql.md)

        [sp_db_increased_partitions](sp-db-increased-partitions.md)

        [sp_dbcmptlevel](sp-dbcmptlevel-transact-sql.md)

        [sp_dbmmonitoraddmonitoring](sp-dbmmonitoraddmonitoring-transact-sql.md)

        [sp_dbmmonitorchangealert](sp-dbmmonitorchangealert-transact-sql.md)

        [sp_dbmmonitorchangemonitoring](sp-dbmmonitorchangemonitoring-transact-sql.md)

        [sp_dbmmonitordropalert](sp-dbmmonitordropalert-transact-sql.md)

        [sp_dbmmonitordropmonitoring](sp-dbmmonitordropmonitoring-transact-sql.md)

        [sp_dbmmonitorhelpalert](sp-dbmmonitorhelpalert-transact-sql.md)

        [sp_dbmmonitorhelpmonitoring](sp-dbmmonitorhelpmonitoring-transact-sql.md)

        [sp_dbmmonitorresults](sp-dbmmonitorresults-transact-sql.md)

        [sp_delete_backuphistory](sp-delete-backuphistory-transact-sql.md)

        [sp_depends](sp-depends-transact-sql.md)

        [sp_describe_first_result_set](sp-describe-first-result-set-transact-sql.md)

        [sp_describe_undeclared_parameters](sp-describe-undeclared-parameters-transact-sql.md)

        [sp_detach_db](sp-detach-db-transact-sql.md)

        [sp_dropdevice](sp-dropdevice-transact-sql.md)

        [sp_dropextendedproc](sp-dropextendedproc-transact-sql.md)

        [sp_dropextendedproperty](sp-dropextendedproperty-transact-sql.md)

        [sp_dropmessage](sp-dropmessage-transact-sql.md)

        [sp_droptype](sp-droptype-transact-sql.md)

        [sp_execute](sp-execute-transact-sql.md)

        [sp_executesql](sp-executesql-transact-sql.md)

        [sp_get_endpoint_certificate](sp-get-endpoint-certificate-transact-sql.md)

        [sp_get_query_template](sp-get-query-template-transact-sql.md)

        [sp_getapplock](sp-getapplock-transact-sql.md)

        [sp_getbindtoken](sp-getbindtoken-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_help](sp-help-transact-sql.md)

        [sp_helpconstraint](sp-helpconstraint-transact-sql.md)

        [sp_helpdb](sp-helpdb-transact-sql.md)

        [sp_helpdevice](sp-helpdevice-transact-sql.md)

        [sp_helpextendedproc](sp-helpextendedproc-transact-sql.md)

        [sp_helpfile](sp-helpfile-transact-sql.md)

        [sp_helpfilegroup](sp-helpfilegroup-transact-sql.md)

        [sp_helpindex](sp-helpindex-transact-sql.md)

        [sp_helplanguage](sp-helplanguage-transact-sql.md)

        [sp_helpserver](sp-helpserver-transact-sql.md)

        [sp_helpsort](sp-helpsort-transact-sql.md)

        [sp_helpstats](sp-helpstats-transact-sql.md)

        [sp_helptext](sp-helptext-transact-sql.md)

        [sp_helptrigger](sp-helptrigger-transact-sql.md)

        [sp_indexoption](sp-indexoption-transact-sql.md)

        [sp_invalidate_textptr](sp-invalidate-textptr-transact-sql.md)

        [sp_lock](sp-lock-transact-sql.md)

        [sys.sp_merge_xtp_checkpoint_files](sys-sp-xtp-merge-checkpoint-files-transact-sql.md)

        [sp_monitor](sp-monitor-transact-sql.md)

        [sp_prepare](sp-prepare-transact-sql.md)

        [sp_prepexec](sp-prepexec-transact-sql.md)

        [sp_prepexecrpc](sp-prepexecrpc-transact-sql.md)

        [sp_procoption](sp-procoption-transact-sql.md)

        [sp_readerrorlog](sp-readerrorlog-transact-sql.md)

        [sp_recompile](sp-recompile-transact-sql.md)

        [sp_refreshview](sp-refreshview-transact-sql.md)

        [sp_releaseapplock](sp-releaseapplock-transact-sql.md)

        [sp_rename](sp-rename-transact-sql.md)

        [sp_renamedb](sp-renamedb-transact-sql.md)

        [sp_resetstatus](sp-resetstatus-transact-sql.md)

        [sp_sequence_get_range](sp-sequence-get-range-transact-sql.md)

        [sp_serveroption](sp-serveroption-transact-sql.md)

        [sp_set_session_context](sp-set-session-context-transact-sql.md)

        [sp_setnetname](sp-setnetname-transact-sql.md)

        [sp_settriggerorder](sp-settriggerorder-transact-sql.md)

        [sp_spaceused](sp-spaceused-transact-sql.md)

        [sp_tableoption](sp-tableoption-transact-sql.md)

        [sp_unbindefault](sp-unbindefault-transact-sql.md)

        [sp_unbindrule](sp-unbindrule-transact-sql.md)

        [sp_updateextendedproperty](sp-updateextendedproperty-transact-sql.md)

        [sp_updatestats](sp-updatestats-transact-sql.md)

        [sp_unprepare](sp-unprepare-transact-sql.md)

        [sp_validname](sp-validname-transact-sql.md)

        [sp_who](sp-who-transact-sql.md)

        [sys.sp_xtp_control_proc_exec_stats](sys-sp-xtp-control-proc-exec-stats-transact-sql.md)

        [sys.sp_flush_log](sys-sp-flush-log-transact-sql.md)

        [sys.sp_xtp_bind_db_resource_pool](sys-sp-xtp-bind-db-resource-pool-transact-sql.md)

        [sys.sp_xtp_control_query_exec_stats](sys-sp-xtp-control-query-exec-stats-transact-sql.md)

        [sys.sp_xtp_checkpoint_force_garbage_collection](sys-sp-xtp-checkpoint-force-garbage-collection-transact-sql.md)

        [sys.sp_xtp_force_gc](sys-sp-xtp-force-gc-transact-sql.md)

        [sys.sp_xtp_unbind_db_resource_pool](sys-sp-xtp-unbind-db-resource-pool-transact-sql.md)
    :::column-end:::
:::row-end:::

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
