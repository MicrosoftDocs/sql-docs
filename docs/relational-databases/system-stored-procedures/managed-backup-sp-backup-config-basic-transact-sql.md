---
description: "managed_backup.sp_backup_config_basic (Transact-SQL)"
title: "managed_backup.sp_backup_config_basic (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/03/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
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
author: markingmyname
ms.author: maghan
---
# managed_backup.sp_backup_config_basic (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Configures the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] basic settings for a specific database or for an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  This procedure can be called on its own to create a basic managed backup configuration. However, if you plan to add advanced features or a custom schedule, first configure those settings using [managed_backup.sp_backup_config_advanced &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-advanced-transact-sql.md) and [managed_backup.sp_backup_config_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-schedule-transact-sql.md) before enabling managed backup with this procedure.  
   
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
 [!NOTE] 
 If @database name is set to NULL the settings will be applied at instance level(apply to all new databases created on the instance)
  
 @container_url  
 A URL that indicates the location of the backup. When @credential_name is NULL, this URL is a shared access signature (SAS) URL to a blob container in Azure Storage, and the backups use the new backup to block blob functionality. For more information, please review [Understanding SAS](/azure/storage/common/storage-sas-overview). When @credential_name is specified, then this is a storage account URL, and the backups use the deprecated backup to page blob functionality.  
  
> [!NOTE]  
>  Only a SAS URL is supported for this parameter at this time.  
  
 @retention_days  
 The retention period for the backup files in days. The @storage_url is INT. This is a required parameter when configuring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the first time on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. While changing the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration, this parameter is optional. If not specified then the existing configuration values are retained.  
  
 @credential_name  
 The name of the SQL Credential used to authenticate to the Azure storage account. @credentail_name is **SYSNAME**. When specified, the backup is stored to a page blob. If this parameter is NULL, the backup will be stored as a block blob. Backup to page blob is deprecated, so it is preferred to use the new block blob backup functionality. When used to change the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration, this parameter is optional. If not specified, then the existing configuration values are retained.  
  
> [!WARNING]
>  The **\@credential_name** parameter is not supported at this time. Only backup to block blob is supported, which requires this parameter to be NULL.  
  
## Return Code Value  
 0 (success) or 1 (failure)  
  
## Security  
  
### Permissions  
 Requires membership in **db_backupoperator** database role, with **ALTER ANY CREDENTIAL** permissions, and **EXECUTE** permissions on **sp_delete_backuphistory** stored procedure.  
  
## Examples  
The following example creates Shared Access Signatures that can be used to create a SQL Server Credential on a newly created container. The script creates a Shared Access Signature that is associated with a Stored Access Policy. For more information, see Shared Access Signatures, Part 1: Understanding the SAS Model. The script also writes the T-SQL command required to create the credential on SQL Server.
  
```powershell  
# Define global variables for the script  
$prefixName = '<a prefix name>'  # used as the prefix for the name for various objects  
$subscriptionName='<your subscription name>'   # the name of subscription name you will use  
$locationName = '<a data center location>'  # the data center region you will use  
$storageAccountName= $prefixName + 'storage' # the storage account name you will create or use  
$containerName= $prefixName + 'container'  # the storage container name to which you will attach the SAS policy with its SAS token  
$policyName = $prefixName + 'policy' # the name of the SAS policy  

# Set a variable for the name of the resource group you will create or use  
$resourceGroupName=$prefixName + 'rg'

# adds an authenticated Azure account for use in the session
Connect-AzAccount

# set the tenant, subscription and environment for use in the rest of
Set-AzContext -SubscriptionName $subscriptionName

# create a new resource group - comment out this line to use an existing resource group  
New-AzResourceGroup -Name $resourceGroupName -Location $locationName

# Create a new ARM storage account - comment out this line to use an existing ARM storage account  
New-AzStorageAccount -Name $storageAccountName -ResourceGroupName $resourceGroupName -Type Standard_RAGRS -Location $locationName   

# Get the access keys for the ARM storage account  
$accountKeys = Get-AzStorageAccountKey -ResourceGroupName $resourceGroupName -Name $storageAccountName  

# Create a new storage account context using an ARM storage account  
$storageContext = New-AzStorageContext -StorageAccountName $storageAccountName -StorageAccountKey $accountKeys[0].value 

# Creates a new container in Azure Blob Storage  
$container = New-AzStorageContainer -Context $storageContext -Name $containerName  
$cbc = $container.CloudBlobContainer  

# Sets up a Stored Access Policy and a Shared Access Signature for the new container  
$policy = New-AzStorageContainerStoredAccessPolicy -Container $containerName -Policy $policyName -Context $storageContext -ExpiryTime $(Get-Date).ToUniversalTime().AddYears(10) -Permission "rwld"
$sas = New-AzStorageContainerSASToken -Policy $policyName -Context $storageContext -Container $containerName
Write-Host 'Shared Access Signature= '$($sas.Substring(1))''  

# Outputs the Transact SQL to the clipboard and to the screen to create the credential using the Shared Access Signature  
Write-Host 'Credential T-SQL'  
$tSql = "CREATE CREDENTIAL [{0}] WITH IDENTITY='Shared Access Signature', SECRET='{1}'" -f $cbc.Uri,$sas.Substring(1)   
$tSql | clip  
Write-Host $tSql
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
  
