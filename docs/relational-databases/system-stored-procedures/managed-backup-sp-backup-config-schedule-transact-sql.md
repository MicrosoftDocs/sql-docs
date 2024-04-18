---
title: "managed_backup.sp_backup_config_schedule (Transact-SQL)"
description: Configures automated or custom scheduling options for SQL Server Managed Backup to Azure.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_backup_config_schedule_TSQL"
  - "managed_backup.sp_backup_config_schedule"
  - "managed_backup.sp_backup_config_schedule_TSQL"
  - "sp_backup_config_schedule"
helpviewer_keywords:
  - "managed_backup.sp_backup_config_schedule"
  - "sp_backup_config_schedule"
dev_langs:
  - "TSQL"
---
# managed_backup.sp_backup_config_schedule (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Configures automated or custom scheduling options for [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
EXEC managed_backup.sp_backup_config_schedule
    [ @database_name = ] 'database_name'
    , [ @scheduling_option = ] { 'Custom' | 'System' }
    , [ @full_backup_freq_type = ] { 'Daily' | 'Weekly' }
    , [ @days_of_week = ] 'days_of_the_week'
    , [ @backup_begin_time = ] 'begin time of the backup window'
    , [ @backup_duration = ] 'backup window length'
    , [ @log_backup_freq = ] 'frequency of log backup'
[ ; ]
```

## Arguments

#### [ @database_name = ] '*database_name*'

The database name for enabling managed backup on a specific database.

If *@database_name* is set to `NULL`, the settings are applied at instance level (applies to all new databases created on the instance).

#### [ @scheduling_option = ] { 'Custom' | 'System' }

Specify `System` for system-controlled backup scheduling. Specify `Custom` for a custom schedule defined by the other parameters.

#### [ @full_backup_freq_type = ] { 'Daily' | 'Weekly' }

The frequency type for the managed backup operation, which can be set to `Daily` or `Weekly`.

#### [ @days_of_week = ] '*days_of_the_week*'

The days of the week for the backups when *@full_backup_freq_type* is set to `Weekly`. Specify full string names like `Monday`. You can also specify more than one day name, separated by the pipe symbol (`|`). For example, `N'Monday | Wednesday | Friday'`.

#### [ @backup_begin_time = ] '*begin time of the backup window*'

The start time of the backup window. Backups aren't started outside of the time window, which is defined by a combination of *@backup_begin_time* and *@backup_duration*. Format: `hh:mm`.

#### [ @backup_duration = ] '*backup window length*'

The duration of the backup time window. There's no guarantee that backups will be completed during the time window defined by *@backup_begin_time* and *@backup_duration*. Backup operations that are started in this time window but exceed the duration of the window won't be canceled. Format: `hh:mm`.

#### [ @log_backup_freq = ] '*frequency of log backup*'

This determines the frequency of transaction log backups. These backups happen at regular intervals rather than on the schedule specified for the database backups. *@log_backup_freq* can be in minutes or hours and `0:00` is valid, which indicates no log backups. Disabling log backups would only be appropriate for databases with a simple recovery model. Format: `hh:mm`.

> [!NOTE]  
> If the recovery model changes from simple to full, you need to reconfigure the *@log_backup_freq* from `0:00` to a non-zero value.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires membership in the **db_backupoperator** database role, with ALTER ANY CREDENTIAL permissions, and EXECUTE permissions on the `sp_delete_backuphistory` stored procedure.

### Examples

The following example configures managed backups for database `Test`, performing daily full backups beginning at 4am, with a maximum backup duration of 2 hours, and log frequency of 15 minutes.

```sql
USE msdb;
GO

EXEC managed_backup.sp_backup_config_schedule @database_name = 'Test',
    @scheduling_option = 'Custom',
    @full_backup_freq_type = 'Daily',
    @backup_begin_time = '04:00',
    @backup_duration = '02:00',
    @log_backup_freq = '00:15';
GO
```

## Related content

- [managed_backup.sp_backup_config_basic (Transact-SQL)](managed-backup-sp-backup-config-basic-transact-sql.md)
- [managed_backup.sp_backup_config_advanced (Transact-SQL)](managed-backup-sp-backup-config-advanced-transact-sql.md)
