---
title: "SQL Server Agent Stored Procedures (Transact-SQL)"
description: Reference guide for SQL Server Agent system stored procedures used to manage jobs, schedules, alerts, and operators for automated tasks.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 02/24/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ai-usage: ai-assisted
helpviewer_keywords:
  - "system stored procedures [SQL Server], SQL Server Agent"
  - "SQL Server Agent, stored procedures"
dev_langs:
  - "TSQL"
---
# SQL Server Agent stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that are used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent to manage scheduled and event-driven activities.

## Job management

These procedures create, modify, and manage SQL Server Agent jobs.

| Stored procedure | Description |
| --- | --- |
| [sp_add_job](sp-add-job-transact-sql.md) | Creates a new job executed by the SQL Server Agent service. |
| [sp_delete_job](sp-delete-job-transact-sql.md) | Deletes a job from SQL Server Agent. |
| [sp_update_job](sp-update-job-transact-sql.md) | Changes the attributes of an existing job. |
| [sp_help_job](sp-help-job-transact-sql.md) | Returns information about jobs. |
| [sp_start_job](sp-start-job-transact-sql.md) | Instructs SQL Server Agent to execute a job immediately. |
| [sp_stop_job](sp-stop-job-transact-sql.md) | Instructs SQL Server Agent to stop the execution of a job. |
| [sp_help_jobactivity](sp-help-jobactivity-transact-sql.md) | Returns information about the runtime state of SQL Server Agent jobs. |
| [sp_help_jobcount](sp-help-jobcount-transact-sql.md) | Returns the number of jobs that a schedule is attached to. |
| [sp_help_jobhistory](sp-help-jobhistory-transact-sql.md) | Returns information about the execution history of jobs. |
| [sp_purge_jobhistory](sp-purge-jobhistory-transact-sql.md) | Removes the history records for a job. |
| [sp_manage_jobs_by_login](sp-manage-jobs-by-login-transact-sql.md) | Deletes or reassigns jobs belonging to a specified login. |

## Job steps

These procedures manage the individual steps within jobs.

| Stored procedure | Description |
| --- | --- |
| [sp_add_jobstep](sp-add-jobstep-transact-sql.md) | Adds a step to a job. |
| [sp_delete_jobstep](sp-delete-jobstep-transact-sql.md) | Removes a job step from a job. |
| [sp_update_jobstep](sp-update-jobstep-transact-sql.md) | Changes the settings for a job step. |
| [sp_help_jobstep](sp-help-jobstep-transact-sql.md) | Returns information about job steps. |
| [sp_help_jobsteplog](sp-help-jobsteplog-transact-sql.md) | Returns information about the SQL Server Agent job step log. |
| [sp_delete_jobsteplog](sp-delete-jobsteplog-transact-sql.md) | Deletes the SQL Server Agent job step log for a job. |

## Schedules

These procedures create and manage schedules for jobs.

| Stored procedure | Description |
| --- | --- |
| [sp_add_schedule](sp-add-schedule-transact-sql.md) | Creates a schedule that can be used by any number of jobs. |
| [sp_delete_schedule](sp-delete-schedule-transact-sql.md) | Deletes a schedule. |
| [sp_update_schedule](sp-update-schedule-transact-sql.md) | Changes the settings for a schedule. |
| [sp_help_schedule](sp-help-schedule-transact-sql.md) | Returns information about schedules. |
| [sp_attach_schedule](sp-attach-schedule-transact-sql.md) | Attaches a schedule to a job. |
| [sp_detach_schedule](sp-detach-schedule-transact-sql.md) | Removes the association between a schedule and a job. |
| [sp_add_jobschedule](sp-add-jobschedule-transact-sql.md) | Creates a schedule for a job. |
| [sp_delete_jobschedule](sp-delete-jobschedule-transact-sql.md) | Deletes a schedule for a job. |
| [sp_update_jobschedule](sp-update-jobschedule-transact-sql.md) | Changes the settings for a job's schedule. |
| [sp_help_jobschedule](sp-help-jobschedule-transact-sql.md) | Returns information about job schedules. |
| [sp_help_jobs_in_schedule](sp-help-jobs-in-schedule-transact-sql.md) | Returns information about jobs attached to a specific schedule. |

## Alerts

These procedures create and manage alerts that respond to events.

| Stored procedure | Description |
| --- | --- |
| [sp_add_alert](sp-add-alert-transact-sql.md) | Creates an alert. |
| [sp_delete_alert](sp-delete-alert-transact-sql.md) | Deletes an alert. |
| [sp_update_alert](sp-update-alert-transact-sql.md) | Changes the settings for an alert. |
| [sp_help_alert](sp-help-alert-transact-sql.md) | Returns information about alerts. |

## Operators

These procedures manage operators who receive alert notifications.

| Stored procedure | Description |
| --- | --- |
| [sp_add_operator](sp-add-operator-transact-sql.md) | Creates an operator for use with alerts and jobs. |
| [sp_delete_operator](sp-delete-operator-transact-sql.md) | Deletes an operator. |
| [sp_update_operator](sp-update-operator-transact-sql.md) | Changes the settings for an operator. |
| [sp_help_operator](sp-help-operator-transact-sql.md) | Returns information about operators. |
| [sp_notify_operator](sp-notify-operator-transact-sql.md) | Sends a notification to an operator. |

## Notifications

These procedures manage notifications for job completion and alerts.

| Stored procedure | Description |
| --- | --- |
| [sp_add_notification](sp-add-notification-transact-sql.md) | Adds a notification to an alert. |
| [sp_delete_notification](sp-delete-notification-transact-sql.md) | Removes a notification from an alert. |
| [sp_update_notification](sp-update-notification-transact-sql.md) | Changes the notification settings for an alert. |
| [sp_help_notification](sp-help-notification-transact-sql.md) | Returns information about notifications for a given operator or alert. |

## Categories

These procedures organize jobs, alerts, and operators into categories.

| Stored procedure | Description |
| --- | --- |
| [sp_add_category](sp-add-category-transact-sql.md) | Adds a job, alert, or operator category. |
| [sp_delete_category](sp-delete-category-transact-sql.md) | Deletes a category. |
| [sp_update_category](sp-update-category-transact-sql.md) | Changes the name of a category. |
| [sp_help_category](sp-help-category-transact-sql.md) | Returns information about categories. |

## Proxies

These procedures manage proxy accounts for running job steps.

| Stored procedure | Description |
| --- | --- |
| [sp_add_proxy](sp-add-proxy-transact-sql.md) | Creates a SQL Server Agent proxy account. |
| [sp_delete_proxy](sp-delete-proxy-transact-sql.md) | Deletes a proxy account. |
| [sp_update_proxy](sp-update-proxy-transact-sql.md) | Changes the settings for a proxy account. |
| [sp_help_proxy](sp-help-proxy-transact-sql.md) | Returns information about proxy accounts. |
| [sp_grant_login_to_proxy](sp-grant-login-to-proxy-transact-sql.md) | Grants a login access to a proxy. |
| [sp_revoke_login_from_proxy](sp-revoke-login-from-proxy-transact-sql.md) | Removes a login's access to a proxy. |
| [sp_enum_login_for_proxy](sp-enum-login-for-proxy-transact-sql.md) | Lists the logins that can access a proxy. |
| [sp_grant_proxy_to_subsystem](sp-grant-proxy-to-subsystem-transact-sql.md) | Grants a proxy access to a subsystem. |
| [sp_revoke_proxy_from_subsystem](sp-revoke-proxy-from-subsystem-transact-sql.md) | Revokes a proxy's access to a subsystem. |
| [sp_enum_proxy_for_subsystem](sp-enum-proxy-for-subsystem-transact-sql.md) | Lists proxies that can access a subsystem. |
| [sp_enum_sqlagent_subsystems](sp-enum-sqlagent-subsystems-transact-sql.md) | Lists the SQL Server Agent subsystems. |

## Target servers (multiserver administration)

These procedures manage target servers in a multiserver administration environment.

| Stored procedure | Description |
| --- | --- |
| [sp_add_targetservergroup](sp-add-targetservergroup-transact-sql.md) | Creates a target server group. |
| [sp_delete_targetservergroup](sp-delete-targetservergroup-transact-sql.md) | Deletes a target server group. |
| [sp_update_targetservergroup](sp-update-targetservergroup-transact-sql.md) | Changes the name of a target server group. |
| [sp_help_targetservergroup](sp-help-targetservergroup-transact-sql.md) | Returns information about target server groups. |
| [sp_add_targetsvrgrp_member](sp-add-targetsvrgrp-member-transact-sql.md) | Adds a target server to a target server group. |
| [sp_delete_targetsvrgrp_member](sp-delete-targetsvrgrp-member-transact-sql.md) | Removes a target server from a target server group. |
| [sp_add_jobserver](sp-add-jobserver-transact-sql.md) | Targets a job at a specified server. |
| [sp_delete_jobserver](sp-delete-jobserver-transact-sql.md) | Removes a job target from a specified server. |
| [sp_help_jobserver](sp-help-jobserver-transact-sql.md) | Returns information about target servers for a job. |
| [sp_apply_job_to_targets](sp-apply-job-to-targets-transact-sql.md) | Applies a job to one or more target servers or target server groups. |
| [sp_remove_job_from_targets](sp-remove-job-from-targets-transact-sql.md) | Removes a job from specified target servers or target server groups. |
| [sp_delete_targetserver](sp-delete-targetserver-transact-sql.md) | Removes a server from the list of available target servers. |
| [sp_help_targetserver](sp-help-targetserver-transact-sql.md) | Returns information about target servers. |
| [sp_resync_targetserver](sp-resync-targetserver-transact-sql.md) | Resynchronizes a target server with the master server. |
| [sp_help_downloadlist](sp-help-downloadlist-transact-sql.md) | Lists jobs and their download status for a target server. |
| [sp_post_msx_operation](sp-post-msx-operation-transact-sql.md) | Posts operations for target servers to download. |
| [sp_msx_defect](sp-msx-defect-transact-sql.md) | Removes the current server from multiserver operations. |
| [sp_msx_enlist](sp-msx-enlist-transact-sql.md) | Adds the current server to the list of available servers. |
| [sp_msx_get_account](sp-msx-get-account-transact-sql.md) | Returns information about the credential used to log in to the master server. |
| [sp_msx_set_account](sp-msx-set-account-transact-sql.md) | Sets the account used by the target server to log in to the master server. |

## Error log management

These procedures manage the SQL Server Agent error log.

| Stored procedure | Description |
| --- | --- |
| [sp_cycle_agent_errorlog](sp-cycle-agent-errorlog-transact-sql.md) | Closes the current SQL Server Agent error log file and cycles the extension numbers. |

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
