---
title: "sqllogship Application"
description: The sqllogship application performs a backup, copy, or restore operation and clean-up tasks for a log shipping configuration on a SQL Server database.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "sqllogship"
ms.assetid: 8ae70041-f3d9-46e4-8fa8-31088572a9f8
author: markingmyname
ms.author: maghan
---
# sqllogship Application
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]
  The **sqllogship** application performs a backup, copy, or restore operation and associated clean-up tasks for a log shipping configuration. The operation is performed on a specific instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] for a specific database.  
  
 ![Topic link icon](../database-engine/configure-windows/media/topic-link.gif "Topic link icon") For the syntax conventions, see [Command Prompt Utility Reference &#40;Database Engine&#41;](../tools/command-prompt-utility-reference-database-engine.md).  
  
## Syntax  
  
```  
  
sqllogship -server instance_name { -backup primary_id | -copy secondary_id | -restore secondary_id } [ -verboselevel level ] [ -logintimeout timeout_value ] [ -querytimeout timeout_value ]  
```  
  
## Arguments  
 **-server** _instance_name_  
 Specifies the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] where the operation will run. The server instance to specify depends on which log-shipping operation is being specified. For **-backup**, *instance_name* must be the name of the primary server in a log shipping configuration. For **-copy** or **-restore**, *instance_name* must be the name of a secondary server in a log shipping configuration.  
  
 **-backup** _primary_id_  
 Performs a backup operation for the primary database whose primary ID is specified by *primary_id*. You can obtain this ID by selecting it from the [log_shipping_primary_databases](../relational-databases/system-tables/log-shipping-primary-databases-transact-sql.md) system table or by using the [sp_help_log_shipping_primary_database](../relational-databases/system-stored-procedures/sp-help-log-shipping-primary-database-transact-sql.md) stored procedure.  
  
 The backup operation creates the log backup in the backup directory. The **sqllogship** application then cleans out any old backup files, based on the file retention period. Next, the application logs history for the backup operation on the primary server and the monitor server. Finally, the application runs [sp_cleanup_log_shipping_history](../relational-databases/system-stored-procedures/sp-cleanup-log-shipping-history-transact-sql.md), which cleans out old history information, based on the retention period.  
  
 **-copy** _secondary_id_  
 Performs a copy operation to copy backups from the specified secondary server for the secondary database, or databases, whose secondary ID is specified by *secondary_id*. You can obtain this ID by selecting it from the [log_shipping_secondary](../relational-databases/system-tables/log-shipping-secondary-transact-sql.md) system table or by using the [sp_help_log_shipping_secondary_database](../relational-databases/system-stored-procedures/sp-help-log-shipping-secondary-database-transact-sql.md) stored procedure.  
  
 The operation copies the backup files from the backup directory to the destination directory. The **sqllogship** application then logs the history for the copy operation on the secondary server and the monitor server.  
  
 **-restore** _secondary_id_  
 Performs a restore operation on the specified secondary server for the secondary database, or databases, whose secondary ID is specified by *secondary_id*. You can obtain this ID by using the **sp_help_log_shipping_secondary_database** stored procedure.  
  
 Any backup files in the destination directory that were created after the most recent restore point are restored to the secondary database, or databases. The **sqllogship** application then cleans out any old backup files, based on the file retention period. Next, the application logs history for the restore operation on the secondary server and the monitor server. Finally, the application runs **sp_cleanup_log_shipping_history**, which cleans out old history information, based on the retention period.  
  
 **-verboselevel** _level_  
 Specifies the level of messages added to the log shipping history. *level* is one of the following integers:  
  
|Level|Description|  
|-----------|-----------------|  
|0|Output no tracing and debugging messages.|  
|1|Output error-handling messages.|  
|2|Output warnings and error-handling messages.|  
|**3**|Output informational messages, warnings, and error-handling messages. This is the default value.|  
|4|Output all debugging and tracing messages.|  
  
 **-logintimeout** _timeout_value_  
 Specifies the amount of time allotted for attempting to log in to the server instance before the attempt times out. The default is 15 seconds. *timeout_value* is **int**_._  
  
 **-querytimeout** _timeout_value_  
 Specifies the amount of time allotted for starting the specified operation before the attempt times out. The default is no timeout period. *timeout_value* is **int**_._  
  
## Remarks  
 We recommend that you use the backup, copy, and restore jobs to perform the backup, copy and restore when possible. To start these jobs from a batch operation or other application, call the [sp_start_job](../relational-databases/system-stored-procedures/sp-start-job-transact-sql.md) stored procedure.  
  
 The log shipping history created by **sqllogship** is interspersed with the history created by log shipping backup, copy, and restore jobs. If you plan to use **sqllogship** repeatedly to perform backup, copy, or restore operations for a log shipping configuration, consider disabling the corresponding log shipping job or jobs. For more information, see [Disable or Enable a Job](../ssms/agent/disable-or-enable-a-job.md).  
  
 The **sqllogship** application, SqlLogShip.exe, is installed in the x:\Program Files\Microsoft SQL Server\130\Tools\Binn directory.  
  
## Permissions  
 **sqllogship** uses Windows Authentication. The Windows Authentication account where the command is run requires Windows directory access and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] permissions. The requirement depends on whether the **sqllogship** command specifies the **-backup**, **-copy**, or **-restore** option.  
  
|Option|Directory access|Permissions|  
|------------|----------------------|-----------------|  
|**-backup**|Requires read/write access to the backup directory.|Requires the same permissions as the BACKUP statement. For more information, see [BACKUP &#40;Transact-SQL&#41;](../t-sql/statements/backup-transact-sql.md).|  
|**-copy**|Requires read access to the backup directory and write access to the copy directory.|Requires the same permissions as the [sp_help_log_shipping_secondary_database](../relational-databases/system-stored-procedures/sp-help-log-shipping-secondary-database-transact-sql.md) stored procedure.|  
|**-restore**|Requires read/write access to the copy directory.|Requires the same permissions as the RESTORE statement. For more information, see [RESTORE &#40;Transact-SQL&#41;](../t-sql/statements/restore-statements-transact-sql.md).|  
  
> [!NOTE]  
>  To find out the paths of the backup and copy directories, you can run the **sp_help_log_shipping_secondary_database** stored procedure or view the **log_shipping_secondary** table in **msdb**. The paths of the backup directory and destination directory are in the **backup_source_directory** and **backup_destination_directory** columns, respectively.  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [log_shipping_primary_databases &#40;Transact-SQL&#41;](../relational-databases/system-tables/log-shipping-primary-databases-transact-sql.md)   
 [log_shipping_secondary &#40;Transact-SQL&#41;](../relational-databases/system-tables/log-shipping-secondary-transact-sql.md)   
 [sp_cleanup_log_shipping_history &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-cleanup-log-shipping-history-transact-sql.md)   
 [sp_help_log_shipping_primary_database &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-help-log-shipping-primary-database-transact-sql.md)   
 [sp_help_log_shipping_secondary_database &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-help-log-shipping-secondary-database-transact-sql.md)   
 [sp_start_job &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-start-job-transact-sql.md)  
  
  
