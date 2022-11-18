---
description: "managed_backup.fn_available_backups (Transact-SQL)"
title: "managed_backup.fn_available_backups (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "smart_admin.fn_available_backups"
  - "smart_admin.fn_available_backups_TSQL"
  - "fn_available_backups_TSQL"
  - "fn_available_backups"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_available_backups"
  - "smart_admin.fn_available_backups"
ms.assetid: 7aa84474-16e5-49bd-a703-c8d1408ef107
author: MikeRayMSFT
ms.author: mikeray
---
# managed_backup.fn_available_backups (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Returns a table of 0, one or more rows of the available backup files for the specified database. The backup files returned are backups created by [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
managed_backup.fn_available_backups ([@database_name = ] 'database name')  
```  
  
##  <a name="Arguments"></a> Arguments  
 @database_name  
 The name of the database. The @database_name is NVARCHAR(512).  
  
## Table Returned  
 The table has a unique clustered constraint on (database_guid, backup_start_date, and first_lsn, backup_type).   
If a database is dropped and then recreated, the backup sets for all the databases are returned. The output is ordered by the database_guid, which uniquely identified each database.   
If there are gaps in LSN meaning that there is a break in the log chain, the table will contain a special row for each missing LSN segment.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|Backup_path|NVARCHAR(260) COLLATE Latin1_General_CI_AS_KS_WS|The URL of the backup file.|  
|backup_type|NVARCHAR(6)|'DB' for database backup 'LOG' for log backup|  
|expiration_date|DATETIME|The date on which this file is expected to be deleted. This is set based on the ability to recover the database to a point in time within the specified retention period.|  
|database_guid|UNIQUEIDENTIFIER|The GUID value for the specified database.  The GUID uniquely identifies a database.|  
|first_lsn|NUMERIC(25, 0)|Log sequence number of the first or oldest log record in the backup set. Can be NULL.|  
|last_lsn|NUMERIC(25, 0)|Log sequence number of the next log record after the backup set. Can be NULL.|  
|backup_start_date|DATETIME|Date and time the backup operation started.|  
|backup_finish_date|NVARCHAR(128)|Date and time the backup operation finished.|  
|machine_name|NVARCHAR(128)|Name of the computer where the SQL Server instance is installed and running [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].|  
|last_recovery_fork_id|UNIQUEIDENTIFIER|Identification number for the ending recovery fork.|  
|first_recovery_fork_id|UNIQUEIDENTIFIER|ID of the starting recovery fork. For data backups, first_recovery_fork_guid equals last_recovery_fork_guid.|  
|fork_point_lsn|NUMERIC(25, 0)|If first_recovery_fork_id is not equal to last_recovery_fork_id, this is the log sequence number of the fork point. Otherwise, this value is NULL.|  
|availability_group_guid|UNIQUEIDENTIFIER|If a database is an Always On database, this is the GUID of the availability group. Otherwise this value is NULL.|  
  
## Return Code Value  
 0 (success) or 1 (failure).  
  
## Security  
  
### Permissions  
 Requires **SELECT** permissions on this function.  
  
## Examples  
 The following example lists all the available backups backed up through [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the database 'MyDB'  
  
```  
SELECT *   
FROM msdb.managed_backup.fn_available_backups ('MyDB')  
  
```  
  
## See Also  
 [SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md)   
 [Restoring From Backups Stored in Microsoft Azure](../../relational-databases/backup-restore/restoring-from-backups-stored-in-microsoft-azure.md)  
  
  
