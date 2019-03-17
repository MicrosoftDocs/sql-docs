---
title: "Configure Managed Backup (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "08/23/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.managedbackup.configure.f1"
ms.assetid: 79397cf6-0611-450a-b0d8-e784a76e3091
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Configure Managed Backup (SQL Server Management Studio)
  The **Managed Backup** dialog allows you to configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] defaults for the instance. This topic describes how to use this dialog to configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] default settings for the instance and options you must consider when doing so. When [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is configured for the instance, the settings are applied to any new database created thereafter.  
  
 If you want to configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for a specific database, see [Enable and Configure SQL Server Managed Backup to Windows Azure for a Database](../../2014/database-engine/sql-server-managed-backup-to-windows-azure-retention-and-storage-settings.md#DatabaseConfigure).  
 
> [!NOTE] 
> SQL Server Managed Backup is not supported with proxy servers. 
  
## Task List  
  
## [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] Functions Using Managed Backup Interface in SQL Server Management Studio  
 In this release, you can only configure instance level default settings using the **Management Backup** interface. You cannot configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for a database, pause or resume [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] operations, or setup email notifications. For information on how to perform operations not currently supported through the **Managed Backup** interface, see [SQL Server Managed Backup to Windows Azure - Retention and Storage Settings](../../2014/database-engine/sql-server-managed-backup-to-windows-azure-retention-and-storage-settings.md).  
  
## Permissions  
 **View Managed Backup Node is SQL Server Management Studio:** To view  **Managed Backup** node in **Object Explorer**, you must either be a System Admin or have the following permissions specifically granted to your user account:  
  
-   `db_backupoperator`  
  
-   `VIEW SERVER STATE`  
  
-   `ALTER ANY CREDENTIAL`  
  
-   `VIEW ANY DEFINITION`  
  
-   `EXECUTE` on `smart_admin.fn_is_master_switch_on`.  
  
-   `SELECT` on `smart_admin.fn_backup_instance_config`.  
  
 **To Configure Managed Backup:** to configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] in SQL Server Management Studio, you must be a System Administrator or have the following permissions:  
  
 Membership in `db_backupoperator` database role, with `ALTER ANY CREDENTIAL` permissions, and `EXECUTE` permissions on `sp_delete_backuphistory` stored procedure.  
  
 `SELECT` permissions on the `smart_admin.fn_get_current_xevent_settings` function.  
  
 `EXECUTE` permissions on the `smart_admin.sp_get_backup_diagnostics` stored procedure. In addition, it requires `VIEW SERVER STATE` permissions as it internally calls other system objects that require this permission.  
  
 `EXECUTE` permissions on `smart_admin.sp_set_instance_backup`, and `smart_admin.sp_backup_master_switch`.  
  
## Configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] using SQL Server Management Studio  
 From the **object explorer**, expand the **Management** node, and right click on **Managed Backup**. Select **Configure**. This opens the **Managed Backup** dialog.  
  
 Check **Enable Managed Backup** option and specify the configuration values:  
  
 The **File retention** period is specified in days and should be between 1 and 30.  
  
 The **SQL Credential** you select should match the storage account. If you currently do not have a SQL Credential that stores the authentication information, you can create one by clicking **Create**. You can also create credential by using the CREATE CREDENTIAL Transact-SQL statement, and provide the storage account name for Identity and the access key for the SECRET parameters. For more information, see [Create a Credential](../relational-databases/backup-restore/sql-server-backup-to-url.md#credential).  
  
 Specify the **Storage URL** for the Windows Azure storage account, the SQL Credential that stores the authentication information for the storage account, and the retention period for the backup files.  
  
 The storage URL format is: https://\<StorageAccount>.blob.core.windows.net/  
  
 To set the encryption settings at the instance level, check **Encrypt Backup** option, and specify the algorithm and a Certificate or Asymmetric Key to use for the encryption.  This is set at the instance level is used for all the new databases created once this configuration has been applied.  
  
> [!WARNING]  
>  This dialog cannot be used to specify the encryption options without configuring [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)]. These encryption options only apply to [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] operations. To use encryption for other backup procedures, see [Backup Encryption](../relational-databases/backup-restore/backup-encryption.md).  
  
### Considerations  
 If you configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] at the instance level, the settings are applied to any new database created thereafter.  However, existing database will not automatically inherit these settings. To configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] on previously existing databases, you must configure each database specifically. For more information, see [Enable and Configure SQL Server Managed Backup to Windows Azure for a Database](../../2014/database-engine/sql-server-managed-backup-to-windows-azure-retention-and-storage-settings.md#DatabaseConfigure).  
  
 If [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] has been paused using the `smart_admin.sp_backup_master_switch`, you will see a warning message " Managed Backup is disabled and the current configurations will not take effect..." when you try to complete the configuration. Use the `smart_admin.sp_backup_master_switch` stored and set the @new_state=1. This will resume [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] services and the configuration settings will take into effect. For more information on the stored procedure, see [smart_admin.sp_ backup_master_switch &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/managed-backup-sp-backup-master-switch-transact-sql).  
  
## See Also  
 [SQL Server Managed Backup to Windows Azure: Interoperability and Coexistence](../../2014/database-engine/sql-server-managed-backup-to-windows-azure-interoperability-and-coexistence.md)  
  
  
