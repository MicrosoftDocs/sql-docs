---
description: "managed_backup.sp_backup_config_schedule (Transact-SQL)"
title: "managed_backup.sp_backup_config_schedule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/20/2020"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_backup_config_schedule_TSQL"
  - "managed_backup.sp_backup_config_schedule"
  - "managed_backup.sp_backup_config_schedule_TSQL"
  - "sp_backup_config_schedule"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "managed_backup.sp_backup_config_schedule"
  - "sp_backup_config_schedule"
ms.assetid: 82541160-d1df-4061-91a5-6868dd85743a
author: MashaMSFT
ms.author: mathoma
---
# managed_backup.sp_backup_config_schedule (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Configures automated or custom scheduling options for [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].  
    
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```vb  
EXEC managed_backup.sp_backup_config_schedule   
    [@database_name = ] 'database_name'
    ,[@scheduling_option = ] {'Custom' | 'System'}  
    ,[@full_backup_freq_type = ] {'Daily' | 'Weekly'}  
    ,[@days_of_week = ] 'days_of_the_week'  
    ,[@backup_begin_time = ] 'begin time of the backup window'  
    ,[@backup_duration = ] 'backup window length'  
    ,[@log_backup_freq = ] 'frequency of log backup'  
```  
  
##  <a name="Arguments"></a> Arguments  
 @database_name  
 The database name for enabling managed backup on a specific database. If NULL or *, then this managed backup applies to all databases on the server.  
  
 @scheduling_option  
 Specify 'System' for system-controlled backup scheduling. Specify 'Custom' for a custom schedule defined by the other paratmeters.  
  
 @full_backup_freq_type  
 The frequency type for the managed  backup operation, which can be set to 'Daily' or 'Weekly'.  
  
 @days_of_week  
 The days of the week for the backups when @full_backup_freq_type is set to Weekly. Specify full string names like 'Monday'.  You can also specify more than one day name, separated by Pipe. For example N'Monday | Wednesday | Friday'.  
  
 @backup_begin_time  
 The start time of the backup window. Backups will not be started outside of the time window, which is defined by a combination of @backup_begin_time and @backup_duration.  
  
 @backup_duration  
 The duration of the backup time window. Note that there is no guarantee that backups will be completed during the time window defined by @backup_begin_time and @backup_duration. Backup operations that are started in this time window but exceed the duration of the window will not be cancelled.  
  
 @log_backup_freq  
 This determines the frequency of transaction log backups. These backups happen at regular intervals rather than on the schedule specified for the database backups. @log_backup_freq can be in minutes or hours and `0:00` is valid, which indicates no log backups. Disabling log backups would only be appropriate for databases with a simple recovery model.  
  
> [!NOTE]  
>  If the recovery model changes from simple to full, you need to reconfigure the log_backup_freq from `0:00` to a non-zero value.  
  
## Return Code Value  
 0 (success) or 1 (failure)  
  
## Security  
  
### Permissions  
 Requires membership in **db_backupoperator** database role, with **ALTER ANY CREDENTIAL** permissions, and **EXECUTE** permissions on **sp_delete_backuphistory** stored procedure.  
  
## See Also  
 [managed_backup.sp_backup_config_basic (Transact-SQL)](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-basic-transact-sql.md)   
 [managed_backup.sp_backup_config_advanced &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-advanced-transact-sql.md)  
  
  
