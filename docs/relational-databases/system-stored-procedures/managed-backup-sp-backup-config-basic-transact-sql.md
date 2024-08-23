---
title: "managed_backup.sp_backup_config_basic (Transact-SQL)"
description: Configures the SQL Server Managed Backup to Azure basic settings for a specific database or for an instance of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_backup_config_basic_TSQL"
  - "sp_backup_config_basic"
  - "managed_backup.sp_backup_config_basic"
  - "managed_backup.sp_backup_config_basic_TSQL"
helpviewer_keywords:
  - "managed_backup.sp_backup_config_basic"
  - "sp_backup_config_basic"
dev_langs:
  - "TSQL"
---
# managed_backup.sp_backup_config_basic (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Configures the [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] basic settings for a specific database or for an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!NOTE]  
> This procedure can be called on its own to create a basic managed backup configuration. However, if you plan to add advanced features or a custom schedule, configure those settings using [managed_backup.sp_backup_config_advanced](managed-backup-sp-backup-config-advanced-transact-sql.md) and [managed_backup.sp_backup_config_schedule](managed-backup-sp-backup-config-schedule-transact-sql.md), before enabling managed backup with this procedure.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
EXEC managed_backup.sp_backup_config_basic
    [ @enable_backup = ] { 0 | 1 }
    , [ @database_name = ] 'database_name'
    , [ @container_url = ] 'Azure_Storage_blob_container'
    , [ @retention_days = ] retention_period_in_days
    , [ @credential_name = ] 'sql_credential_name'
[ ; ]
```

## Arguments

#### [ @enable_backup = ] { 0 | 1 }

Enable or disable [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] for the specified database. *@enable_backup* is **bit**.

Required parameter when configuring [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] for the first instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If you're changing an existing [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] configuration, this parameter is optional. In that case, any configuration values not specified retain their existing values.

For more information, see [Enable SQL Server managed backup to Azure](../backup-restore/enable-sql-server-managed-backup-to-microsoft-azure.md).

#### [ @database_name = ] '*database_name*'

The database name for enabling managed backup on a specific database.

If *@database_name* is set to `NULL`, the settings are at instance level (applies to all new databases created on the instance).

#### [ @container_url = ] '*Azure_Storage_blob_container*'

A URL that indicates the location of the backup. When *@credential_name* is `NULL`, this URL is a shared access signature (SAS) URL to a blob container in Azure Storage, and the backups use the new backup to block blob functionality. For more information, review [Grant limited access to Azure Storage resources using shared access signatures (SAS)](/azure/storage/common/storage-sas-overview). When *@credential_name* is specified, then this is a storage account URL, and the backups use the deprecated backup to page blob functionality.

If the SAS URL has the SAS token included, you must separate it from the SAS token at the question mark, and don't include the question mark.

For example, `https://managedbackupstorage.blob.core.windows.net/backupcontainer?sv=2014-02-14&sr=c&sig=xM2LXVo1Erqp7LxQ%9BxqK9QC6%5Qabcd%9LKjHGnnmQWEsDf%5Q%se=2015-05-14T14%3B93%4V20X&sp=rwdl` results in the following two values:

| Type | Output |
| --- | --- |
| **Container URL** | `https://managedbackupstorage.blob.core.windows.net/backupcontainer` |
| **SAS token** | `sv=2014-02-14&sr=c&sig=xM2LXVo1Erqp7LxQ%9BxqK9QC6%5Qabcd%9LKjHGnnmQWEsDf%5Q%se=2015-05-14T14%3B93%4V20X&sp=rwdl` |

> [!NOTE]  
> Only a SAS URL is supported for this parameter at this time.

#### [ @retention_days = ] *retention_period_in_days*

The retention period for the backup files in days. *@retention_days* is **int**. This is a required parameter when configuring [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] for the first time on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. When you change the [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] configuration, this parameter is optional. If not specified then the existing configuration values are retained.

#### [ @credential_name = ] '*sql_credential_name*'

The name of the SQL credential used to authenticate to the Azure storage account. *@credential_name* is **sysname**. When specified, the backup is stored to a page blob. If this parameter is `NULL`, the backup is stored as a block blob. Backing up to page blob is deprecated, so it's preferred to use the new block blob backup functionality. When used to change the [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] configuration, this parameter is optional. If not specified, then the existing configuration values are retained.

> [!WARNING]  
> The *@credential_name* parameter isn't supported at this time. Only backup to block blob is supported, which requires this parameter to be `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires membership in the **db_backupoperator** database role, with ALTER ANY CREDENTIAL permissions, and EXECUTE permissions on the `sp_delete_backuphistory` stored procedure.

## Examples

### A. Create storage account container and SAS URL

You can create both the storage account container and the shared access signature (SAS) URL by using the latest Azure PowerShell commands. The following example creates a new container `myContainer` in the `mystorageaccount` storage account, and then obtains a SAS URL for it with full permissions.

For more information about shared access signatures, see [Grant limited access to Azure Storage resources using shared access signatures (SAS)](/azure/storage/common/storage-sas-overview). For an example PowerShell script, see [Create a Shared Access Signature](../backup-restore/sql-server-backup-to-url.md#SAS).

```powershell
$context = New-AzureStorageContext -StorageAccountName mystorageaccount -StorageAccountKey (Get-AzureStorageKey -StorageAccountName mystorageaccount).Primary
New-AzureStorageContainer -Name myContainer -Context $context
New-AzureStorageContainerSASToken -Name myContainer -Permission rwdl -FullUri -Context $context
```

### B. Enable SQL Server Managed Backup to Azure

The following example enables [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] for the instance of SQL Server it's executed on, sets the retention policy to 30 days, and sets the destination to a container named `myContainer` in a storage account named `mystorageaccount`.

```sql
USE msdb;
GO

EXEC managed_backup.sp_backup_config_basic @enable_backup = 1,
    @container_url = 'https://mystorageaccount.blob.core.windows.net/myContainer',
    @retention_days = 30;
GO
```

### C. Disable SQL Server Managed Backup to Azure

The following example disables [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] for the instance of SQL Server it's executed on.

```sql
USE msdb;
GO

EXEC managed_backup.sp_backup_config_basic @enable_backup = 0;
GO
```

## Related content

- [managed_backup.sp_backup_config_advanced (Transact-SQL)](managed-backup-sp-backup-config-advanced-transact-sql.md)
- [managed_backup.sp_backup_config_schedule (Transact-SQL)](managed-backup-sp-backup-config-schedule-transact-sql.md)
