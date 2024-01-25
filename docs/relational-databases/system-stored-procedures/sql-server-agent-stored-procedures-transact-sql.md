---
title: "SQL Server Agent stored procedures (Transact-SQL)"
description: "SQL Server Agent stored procedures (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "system stored procedures [SQL Server], SQL Server Agent"
  - "SQL Server Agent, stored procedures"
dev_langs:
  - "TSQL"
---
# SQL Server Agent stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that are used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent to manage scheduled and event-driven activities.

:::row:::
    :::column:::
        [sp_add_alert](sp-add-alert-transact-sql.md)

        [sp_add_category](sp-add-category-transact-sql.md)

        [sp_add_job](sp-add-job-transact-sql.md)

        [sp_add_jobschedule](sp-add-jobschedule-transact-sql.md)

        [sp_add_jobserver](sp-add-jobserver-transact-sql.md)

        [sp_add_jobstep](sp-add-jobstep-transact-sql.md)

        [sp_add_notification](sp-add-notification-transact-sql.md)

        [sp_add_operator](sp-add-operator-transact-sql.md)

        [sp_add_schedule](sp-add-schedule-transact-sql.md)

        [sp_add_targetservergroup](sp-add-targetservergroup-transact-sql.md)

        [sp_add_targetsvrgrp_member](sp-add-targetsvrgrp-member-transact-sql.md)

        [sp_apply_job_to_targets](sp-apply-job-to-targets-transact-sql.md)

        [sp_attach_schedule](sp-attach-schedule-transact-sql.md)

        [sp_cycle_agent_errorlog](sp-cycle-agent-errorlog-transact-sql.md)

        [sp_delete_alert](sp-delete-alert-transact-sql.md)

        [sp_delete_category](sp-delete-category-transact-sql.md)

        [sp_delete_job](sp-delete-job-transact-sql.md)

        [sp_delete_jobschedule](sp-delete-jobschedule-transact-sql.md)

        [sp_delete_jobserver](sp-delete-jobserver-transact-sql.md)

        [sp_delete_jobstep](sp-delete-jobstep-transact-sql.md)

        [sp_delete_jobsteplog](sp-delete-jobsteplog-transact-sql.md)

        [sp_delete_notification](sp-delete-notification-transact-sql.md)

        [sp_delete_operator](sp-delete-operator-transact-sql.md)

        [sp_delete_proxy](sp-delete-proxy-transact-sql.md)

        [sp_delete_schedule](sp-delete-schedule-transact-sql.md)

        [sp_delete_targetserver](sp-delete-targetserver-transact-sql.md)

        [sp_delete_targetservergroup](sp-delete-targetservergroup-transact-sql.md)

        [sp_delete_targetsvrgrp_member](sp-delete-targetsvrgrp-member-transact-sql.md)

        [sp_detach_schedule](sp-detach-schedule-transact-sql.md)

        [sp_enum_login_for_proxy](sp-enum-login-for-proxy-transact-sql.md)

        [sp_enum_proxy_for_subsystem](sp-enum-proxy-for-subsystem-transact-sql.md)

        [sp_enum_sqlagent_subsystems](sp-enum-sqlagent-subsystems-transact-sql.md)

        [sp_grant_proxy_to_subsystem](sp-grant-proxy-to-subsystem-transact-sql.md)

        [sp_grant_login_to_proxy](sp-grant-login-to-proxy-transact-sql.md)

        [sp_help_alert](sp-help-alert-transact-sql.md)

        [sp_help_category](sp-help-category-transact-sql.md)

        [sp_help_downloadlist](sp-help-downloadlist-transact-sql.md)

        [sp_help_job](sp-help-job-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_help_jobactivity](sp-help-jobactivity-transact-sql.md)

        [sp_help_jobcount](sp-help-jobcount-transact-sql.md)

        [sp_help_jobhistory](sp-help-jobhistory-transact-sql.md)

        [sp_help_jobs_in_schedule](sp-help-jobs-in-schedule-transact-sql.md)

        [sp_help_jobschedule](sp-help-jobschedule-transact-sql.md)

        [sp_help_jobserver](sp-help-jobserver-transact-sql.md)

        [sp_help_jobstep](sp-help-jobstep-transact-sql.md)

        [sp_help_jobsteplog](sp-help-jobsteplog-transact-sql.md)

        [sp_help_notification](sp-help-notification-transact-sql.md)

        [sp_help_operator](sp-help-operator-transact-sql.md)

        [sp_help_proxy](sp-help-proxy-transact-sql.md)

        [sp_help_schedule](sp-help-schedule-transact-sql.md)

        [sp_help_targetserver](sp-help-targetserver-transact-sql.md)

        [sp_help_targetservergroup](sp-help-targetservergroup-transact-sql.md)

        [sp_manage_jobs_by_login](sp-manage-jobs-by-login-transact-sql.md)

        [sp_msx_defect](sp-msx-defect-transact-sql.md)

        [sp_msx_enlist](sp-msx-enlist-transact-sql.md)

        [sp_msx_get_account](sp-msx-get-account-transact-sql.md)

        [sp_msx_set_account](sp-msx-set-account-transact-sql.md)

        [sp_notify_operator](sp-notify-operator-transact-sql.md)

        [sp_post_msx_operation](sp-post-msx-operation-transact-sql.md)

        [sp_purge_jobhistory](sp-purge-jobhistory-transact-sql.md)

        [sp_remove_job_from_targets](sp-remove-job-from-targets-transact-sql.md)

        [sp_resync_targetserver](sp-resync-targetserver-transact-sql.md)

        [sp_revoke_login_from_proxy](sp-revoke-login-from-proxy-transact-sql.md)

        [sp_revoke_proxy_from_subsystem](sp-revoke-proxy-from-subsystem-transact-sql.md)

        [sp_start_job](sp-start-job-transact-sql.md)

        [sp_stop_job](sp-stop-job-transact-sql.md)

        [sp_update_alert](sp-update-alert-transact-sql.md)

        [sp_update_category](sp-update-category-transact-sql.md)

        [sp_update_job](sp-update-job-transact-sql.md)

        [sp_update_jobschedule](sp-update-jobschedule-transact-sql.md)

        [sp_update_jobstep](sp-update-jobstep-transact-sql.md)

        [sp_update_notification](sp-update-notification-transact-sql.md)

        [sp_update_operator](sp-update-operator-transact-sql.md)

        [sp_update_proxy](sp-update-proxy-transact-sql.md)

        [sp_update_schedule](sp-update-schedule-transact-sql.md)

        [sp_update_targetservergroup](sp-update-targetservergroup-transact-sql.md)
    :::column-end:::
:::row-end:::

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
