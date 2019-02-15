---
title: "managed_backup.sp_backup_config_basic (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/03/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_backup_config_basic_TSQL"
  - "sp_backup_config_basic"
  - "managed_backup.sp_backup_config_basic"
  - "managed_backup.sp_backup_config_basic_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "managed_backup.sp_backup_config_basic"
  - "sp_backup_config_basic"
ms.assetid: 3ad73051-ae9a-4e41-a889-166146e5508f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# managed_backup.sp_backup_config_basic (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Configures the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] basic settings for a specific database or for an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  This procedure can be called on its own to create a basic managed backup configuration. However, if you plan to add advanced features or a custom schedule, first configure those settings using [managed_backup.sp_backup_config_advanced &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-advanced-transact-sql.md) and [managed_backup.sp_backup_config_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-schedule-transact-sql.md) before enabling managed backup with this procedure.  
   
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```Transact-SQL   
EXEC managed_backup.sp_backup_config_basic  
    [@enable_backup = ] { 0 | 1}    ,[@database_name = ] 'database_name'    ,[@container_url = ] 'Azure_Storage_blob_container  
    ,[@retention_days = ] 'retention_period_in_days'    ,[@credential_name = ] 'sql_credential_name'  
```  
  
##  <a name="Arguments"></a> Arguments  
 @enable_backup  
 Enable or disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the specified database. The @enable_backup is **BIT**. Required parameter when configuring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the first instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you are changing an existing [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration, this parameter is optional. In that case, any configuration values not specified retain their existing values.  
  
 @database_name  
 The database name for enabling managed backup on a specific database.  
  
 @container_url  
 A URL that indicates the location of the backup. When @credential_name is NULL, this URL is a shared access signature (SAS) URL to a blob container in Azure Storage, and the backups use the new backup to block blob functionality. For more information, please review [Understanding SAS](https://azure.microsoft.com/documentation/articles/storage-dotnet-shared-access-signature-part-1/). When @credential_name is specified, then this is a storage account URL, and the backups use the deprecated backup to page blob functionality.  
  
> [!NOTE]  
>  Only a SAS URL is supported for this parameter at this time.  
  
 @retention_days  
 The retention period for the backup files in days. The @storage_url is INT. This is a required parameter when configuring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the first time on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. While changing the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration, this parameter is optional. If not specified then the existing configuration values are retained.  
  
 @credential_name  
 The name of the SQL Credential used to authenticate to the Windows Azure storage account. @credentail_name is **SYSNAME**. When specified, the backup is stored to a page blob. If this parameter is NULL, the backup will be stored as a block blob. Backup to page blob is deprecated, so it is preferred to use the new block blob backup functionality. When used to change the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration, this parameter is optional. If not specified, then the existing configuration values are retained.  
  
> [!WARNING]
>  The **@credential_name** parameter is not supported at this time. Only backup to block blob is supported, which requires this parameter to be NULL.  
  
## Return Code Value  
 0 (success) or 1 (failure)  
  
## Security  
  
### Permissions  
 Requires membership in **db_backupoperator** database role, with **ALTER ANY CREDENTIAL** permissions, and **EXECUTE** permissions on **sp_delete_backuphistory** stored procedure.  
  
## Examples  
 You can create both the storage account container and the SAS URL by using the latest Azure PowerShell commands. The following example creates a new container, mycontainer, in the mystorageaccount storage account and then obtains a SAS URL for it with full permissions.  
  
```powershell  
$context = New-AzureStorageContext -StorageAccountName mystorageaccount -StorageAccountKey (Get-AzureStorageKey -StorageAccountName mystorageaccount).Primary  
New-AzureStorageContainer -Name mycontainer -Context $context  
New-AzureStorageContainerSASToken -Name mycontainer -Permission rwdl -FullUri -Context $context  
```  
  
 The following example enables [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the instance of SQL Server it is executed on, sets the retention policy to 30 days, sets the destination to a container named 'mycontainer' in a storage account named 'mystorageaccount'.  
  
```Transact-SQL 
Use msdb;  
Go  
   EXEC managed_backup.sp_backup_config_basic  
                @enable_backup=1  
                ,@container_url = 'https://mystorageaccount.blob.core.windows.net/mycontainer'  
                ,@retention_days=30;   
GO  
  
```
  
 The following example disables [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the instance of SQL Server it is executed on.  
  
```Transact-SQL  
Use msdb;  
Go  
EXEC managed_backup.sp_backup_config_basic  
                @enable_backup=0;  
GO  
  
```  
  
## See Also  
 [managed_backup.sp_backup_config_advanced &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-advanced-transact-sql.md)   
 [managed_backup.sp_backup_config_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-schedule-transact-sql.md)  
  
  
