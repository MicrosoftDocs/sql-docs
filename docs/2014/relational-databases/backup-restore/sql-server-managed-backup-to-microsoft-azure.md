---
title: "SQL Server Managed  Backup to Windows Azure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: afa01165-39e0-4efe-ac0e-664edb8599fd
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server Managed  Backup to Windows Azure
  [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] manages and automates SQL Server backups to the Windows Azure Blob storage service. The backup strategy used by [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is based on the retention period and the transaction workload on the database. [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] supports point in time restore for the retention time period specified.   
[!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] can be enabled at the database level or at the instance level to manage all the databases on the instance of SQL Server. The SQL Server can be running on-premises or in hosted environments like the Windows Azure virtual machine. [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is recommended for SQL Server running on Windows Azure Virtual Machines.  
  
## Benefits of Automating SQL Server Backup Using [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]  
  
-   Currently automating backups for multiple databases requires developing a backup strategy, writing custom code, and scheduling backups. Using [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)], you only are required provide the retention period settings and the storage location. [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] schedules, performs and maintains the backups.  
  
     [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] can be configured at the database level or at configured with default settings for an instance of SQL Server. Automating backup using [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] has the following benefits:  
  
    -   By setting the defaults at the instance level, you can apply these settings to any database created thereafter, thus removing the risk of new databases not being backed up and data loss.  
  
    -   The option of enabling [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] and setting the retention period at the database level, allows you to override the default settings set at the instance level. This allows you to have more granular control on the recoverability for a specific database.  
  
-   With [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)], you do not have to specify the type or frequency of the backups for a database.  You specify the retention period, and [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] determines the type and frequency of backups for a database stores the backups on Windows Azure Blob storage service. For more details on the set of criteria that [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] uses to create the backup strategy,, see the [Components and Concepts](#Concepts) section in this topic.  
  
-   When configured to use encryption, you have additional security for the backup data. For more information, see [Backup Encryption](backup-encryption.md)  
  
 For more details on the benefits of using Windows Azure Blob storage for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups, see [SQL Server Backup and Restore with Windows Azure Blob Storage Service](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md)  
  
## Terms and Definitions  
 [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]  
 A SQL Server feature that automates database backup and maintains the backups based on the retention period.  
  
 Retention Period  
 The retention period is used by [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] to determine what backup files should be retained in the storage in order to recover a database to a point in time within the time frame specified.  The supported values are in the range of 1-30 days.  
  
 Log Chain  
 A continuous sequence of log backups is called a log chain. A log chain starts with a full backup of the database.  
  
##  <a name="Concepts"></a> Requirements, Concepts, and Components  
  
  
###  <a name="Security"></a> Permissions  
 Transact-SQL is the main interface used to configure and monitor [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. In general, to run the configuration stored procedures, **db_backupoperator** database role with **ALTER ANY CREDENTIAL** permissions, and `EXECUTE` permissions on **sp_delete_backuphistory** stored procedure is required.  Stored procedures and functions used to review information typically require `Execute` permissions on the stored procedure and `Select` on the function respectively.  
  
###  <a name="Prereqs"></a> Prerequisites  
 **Prerequisites:**  
  
 **Windows Azure Storage service** is used by [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] to store the backup files.    The concepts, structure, and requirements for creating a Windows Azure storage account is explained in detail in the [Introduction to Key Components and Concepts](sql-server-backup-to-url.md#intorkeyconcepts) section of the **SQL Server Backup to URL** topic.  
  
 **SQL Credential** is used to store the information required to authenticate to the Windows Azure storage account. The SQL Credential object stores the account name and the access key information. For more information, see the [Introduction to Key Components and Concepts](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md) section in the **SQL Server Backup to URL** topic. For a walkthrough on how to create a SQL Credential to store Windows Azure Storage authentication information, see [Lesson 2: Create a SQL Server Credential](../../tutorials/lesson-2-create-a-sql-server-credential.md).  
  
###  <a name="Concepts_Components"></a> Concepts and Key Components  
 The [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is a feature that manages the backup operations. It stores the metadata in the **msdb** database and uses system jobs to write full database and transaction log backups.  
  
#### Components  
 Transact-SQL is the main interface to interact with [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. System stored procedures are used for enabling, configuring, and monitoring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. System functions are used to retrieve existing configuration settings, parameter values, and backup file information. Extended events are used to surface errors and warnings. Alert mechanisms are enabled through SQL Agent jobs and SQL Server Policy Based Management. The following is a list of the objects and a description of its functionality in relation to [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].  
  
 PowerShell cmdlets are also available to configure [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. SQL Server Management Studio supports restoring backups created by [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] by using the **Restore Database** task  
  
|||  
|-|-|  
|System Object|Description|  
|**MSDB**|Stores the metadata, backup history for all the backups created by [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].|  
|[smart_admin.set_db_backup &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/dn451013(v=sql.120).aspx)|System stored procedure for enabling and configuring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for a database.|  
|[smart_admin.set_instance_backup &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/dn451009(v=sql.120).aspx)|System stored procedure for enabling and configuring default settings [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the SQL Server instance.|  
|[smart_admin.sp_ backup_master_switch &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/managed-backup-sp-backup-master-switch-transact-sql)|System stored procedure to pause and resume [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].|  
|[smart_admin.sp_set_parameter &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/managed-backup-sp-set-parameter-transact-sql)|System stored procedure to enable and configure monitoring for [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. Examples: enabling extended events, mail settings for notifications.|  
|[smart_admin.sp_backup_on_demand &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/managed-backup-sp-backup-on-demand-transact-sql)|System stored procedure that is used to perform an ad-hoc backup for a database that is enabled to use [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] without breaking the log chain.|  
|[smart_admin.fn_backup_db_config &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/managed-backup-fn-backup-db-config-transact-sql)|System function that returns the current [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] status and configuration values for a database, or for all the databases on the instance.|  
|[smart_admin.fn_is_master_switch_on &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/managed-backup-fn-is-master-switch-on-transact-sql)|System function that returns the status of the master switch.|  
|[smart_admin.sp_get_backup_diagnostics &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/managed-backup-sp-get-backup-diagnostics-transact-sql)|System stored procedure used to return the events logged by Extended Events.|  
|[smart_admin.fn_get_parameter &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/managed-backup-fn-get-parameter-transact-sql)|System function that returns the current values for backup system settings such as monitoring and mail settings for alerts.|  
|[smart_admin.fn_available_backups &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/managed-backup-fn-available-backups-transact-sql)|Stored Procedure used to retrieve available backups for a specified database or for all the databases in an instance.|  
|[smart_admin.fn_get_current_xevent_settings &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/managed-backup-fn-get-current-xevent-settings-transact-sql)|System function that returns the current extended event settings.|  
|[smart_admin.fn_get_health_status &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/managed-backup-fn-get-health-status-transact-sql)|System function that returns the aggregated counts of errors logged by Extended Events for a specified period.|  
|[Monitor SQL Server Managed Backup to Windows Azure](sql-server-managed-backup-to-microsoft-azure.md)|Extended Events for monitoring, email notification of errors and warnings, SQL Server Policy Based Management for [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] .|  
  
#### Backup Strategy  
 **Backup Strategy used by [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]:**  
  
 The type of backups scheduled and the backup frequency is determined based on the workload of the database. The retention period settings are used to determine the length of time a backup file should be retained in the storage and the ability to recover the database to a point-in-time within the retention period.  
  
 **Backup Container and File Naming Conventions:**  
  
 [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] names the Windows Azure storage container using the SQL Server Instance Name for all databases except availability databases.  For availability databases, the Availability Group GUID is used to name the Windows Azure storage container.  
  
 The backup file for non availability databases are named using the following convention: The name is created using the first 40 characters of the database name, the database GUID without the '-', and the timestamp. The underscore character is inserted between segments as separators. The **.bak** file extension is used for full backup and **.log** for log backups. For Avaialbility Group databases, in addition to the file naming convention described above, the Availability Group database GUID is added after the 40 characters of the database name. The Availability Group database GUID value is the value for group_database_id in sys.databases.  
  
 **Full Database Backup:** [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] agent schedules a full database backup if any of the following is true.  
  
-   A database is [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] enabled for the first time, or when [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is enabled with default settings at the instance level.  
  
-   The log growth since last full database backup is equal to or larger than 1 GB.  
  
-   The maximum time interval of one week has passed since the last full database backup.  
  
-   The log chain is broken. [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] periodically checks to see whether the log chain is intact by comparing the first and last LSNs of the backup files. If there is break in the log chain for any reason, [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] schedules a full database backup. The most common reason for log chain breaks is probably a backup command issued using Transact-SQL or through the Backup task in SQL Server Management Studio.  Other common scenarios include accidental deletion of the backup log files, or accidental overwrites of backups.  
  
 **Transaction Log Backup:** [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] schedules a log backup if any of the following is true:  
  
-   There is no log backup history that can be found. This is usually true when [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is enabled for the first time.  
  
-   The transaction log space used is 5 MB or larger.  
  
-   The maximum time interval of 2 hours since the last log backup is reached.  
  
-   Any time the transaction log backup is lagging behind a full database backup. The goal is to keep the log chain ahead of full backup.  
  
#### Retention Period Settings  
 When enabling backup you must set the retention period in days: The minimum is 1 day, and maximum is 30 days.  
  
 [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] based on the retention period settings, assesses the ability to recover to a point in time in the specified time, to determine what backup files to keep and identifying the backup files to delete. The backup_finish_date of the backup is used to determine and match the time specified by the retention period settings.  
  
#### Important Considerations  
 There are some considerations that are important to understand their impact on [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] operations. They are listed below:  
  
-   For a database, if there is an existing full database backup job running, then [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] waits for the current job to be completed before doing another full database backup for the same database. Similarly, only one transaction log backup can be running at a given time. However, a full database backup and a transaction log backup can run concurrently. Failures are logged as Extended Events.  
  
-   If more than 10 concurrent full database backups are scheduled, a warning is issued through the debug channel of Extended Events. [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] then maintains a priority queue for the remaining databases that require a backup until the all backups are scheduled and completed.  
  
###  <a name="support_limits"></a> Support Limitations  
 The following are some limitations specific to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]:  
  
-   [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] agent supports database backups only: Full and Log Backups.  File backup automation is not supported.  
  
-   [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] operations are currently supported using Transact-SQL. Monitoring and troubleshooting can be done by using Extended Events. PowerShell and SMO support is limited to configuring storage and retention period default settings for an instance of SQL Server, and monitoring the backup status and overall health based on SQL Server Policy Based Management policies.  
  
-   System Databases are not supported.  
  
-   Windows Azure Blob Storage service is the only supported backup storage option. Backups to disk or tape are not supported.  
  
-   Currently, the maximum file size allowed for a Page Blob in Windows Azure Storage is 1 TB. Backup files larger than 1 TB will fail. In order to avoid this situation, we recommend that for large databases, use compression and test the backup file size prior to setting up [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. You can either test by backing up to a local disk or manually backing up to Windows Azure storage using `BACKUP TO URL` Transact-SQL statement. For more information, see [SQL Server Backup to URL](sql-server-backup-to-url.md).  
  
-   Recovery Models: Only databases set to Full or Bulk-logged model are supported.  Databases set to simple recovery model are not supported.  
  
-   [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] may have some limitations when it is configured with other technologies supporting backup, high availability, or disaster recovery. For more information, see [SQL Server Managed Backup to Windows Azure: Interoperability and Coexistence](../../database-engine/sql-server-managed-backup-to-windows-azure-interoperability-and-coexistence.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
|||  
|-|-|  
|**Task descriptions**|**Topic**|  
|Basic tasks like configuring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for a database, or configuring default settings at the instance level, disabling [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] at instance or database level, pausing and restarting [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].|[SQL Server Managed Backup to Windows Azure - Retention and Storage Settings](../../database-engine/sql-server-managed-backup-to-windows-azure-retention-and-storage-settings.md)|  
|**Tutorial:** Step by Step instructions to configuring and monitoring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].|[Setting up SQL Server Managed Backup to Windows Azure](enable-sql-server-managed-backup-to-microsoft-azure.md)|  
|**Tutorial:** Step by Step instructions to configuring and monitoring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for databases in Availability Group.|[Setting up SQL Server Managed Backup to Windows Azure for Availability Groups](../../database-engine/setting-up-sql-server-managed-backup-to-windows-azure-for-availability-groups.md)|  
|Tools and Concepts and tasks related to monitoring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] .|[Monitor SQL Server Managed Backup to Windows Azure](sql-server-managed-backup-to-microsoft-azure.md)|  
|Tools and steps to troubleshooting [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].|[Troubleshooting SQL Server Managed  Backup to Windows Azure](../../database-engine/troubleshooting-sql-server-managed-backup-to-windows-azure.md)|  
  
## See Also  
 [SQL Server Backup and Restore with Windows Azure Blob Storage Service](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md)   
 [SQL Server Backup to URL](sql-server-backup-to-url.md)   
 [SQL Server Managed Backup to Windows Azure: Interoperability and Coexistence](../../database-engine/sql-server-managed-backup-to-windows-azure-interoperability-and-coexistence.md)   
 [Troubleshooting SQL Server Managed  Backup to Windows Azure](../../database-engine/troubleshooting-sql-server-managed-backup-to-windows-azure.md)  
  
  
