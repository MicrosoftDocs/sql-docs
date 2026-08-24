---
title: Prepare Environment for a SQL VM Migration
titleSuffix: SQL Server migration in Azure Arc
description: Prepare your SQL Server instance enabled by Azure Arc for migration to SQL Server on Azure VMs.
author: danimir
ms.author: danil
ms.reviewer: randolphwest, mathoma
ms.date: 06/25/2026
ms.topic: how-to
---

# Prepare environment for a SQL VM migration - SQL Server migration in Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article helps you prepare your environment for a [SQL Server VM migration](migrate-to-sql-server-on-azure-vms.md) of your SQL Server instance enabled by Azure Arc to [SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview) in the Azure portal.

> [!NOTE]  
> - Migrating to SQL Server on Azure VMs through the Azure portal is currently in [preview](release-notes.md#preview).
> - You can provide feedback about your migration experience [directly to the product group](https://aka.ms/arc-migrations-feedback).

## Prerequisites

To migrate your SQL Server databases to SQL Server on Azure VMs through the Azure portal, you need the following prerequisites:

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- An instance of SQL Server [enabled by Azure Arc](overview.md) with the [latest version](release-notes.md) of the Azure extension for SQL Server. To upgrade your extension, see [Upgrade the extension](connect.md#upgrade-the-extension).
- You can choose to use an existing SQL Server on Azure VM, or you can provision a target SQL Server VM during the migration process. If you choose to use an existing SQL Server VM, it must be [registered with the SQL IaaS Agent extension](/azure/azure-sql/virtual-machines/windows/sql-agent-extension-manually-register-single-vm).

## Supported SQL Server versions

SQL Server VM migration works with every edition of SQL Server on Windows and Linux.

The following table lists the minimum supported SQL Server versions for migration:

| SQL Server version | Minimum required servicing update |
| --- | --- |
| SQL Server 2025 (17.x) |  [SQL Server 2025 RTM (17.0.1000.7)](../sql-server-2025-release-notes.md) |
| SQL Server 2022 (16.x) |  [SQL Server 2022 RTM (16.0.1000.6)](../sql-server-2022-release-notes.md) | 
| SQL Server 2019 (15.x) | [SQL Server 2019 RTM (15.0.2000.5)](../sql-server-2019-release-notes.md) |
| SQL Server 2017 (14.x) | [SQL Server 2017 RTM (14.0.1000.169)](../sql-server-2017-release-notes.md) | 
| SQL Server 2016 (13.x) | [SQL Server 2016 RTM (13.0.1400.361)](../sql-server-2016-release-notes.md) |
| SQL Server 2014 (12.x)| [SQL Server 2014 RTM (12.0.2000.8)](../sql-server-2014-release-notes.md) |
| SQL Server 2012 (11.x)| [SQL Server 2012 RTM (11.0.2100.60)](../sql-server-2012-release-notes.md) |

## Permissions

This section describes the permissions that you need to migrate your SQL Server databases to SQL Server on Azure VMs through the Azure portal.

On the source SQL Server instance, you need the following permissions:

- If you enable [least privilege](configure-least-privilege.md), necessary permissions such as **sysadmin** are [granted](configure-windows-accounts-agent.md#database-migration) as needed during the database migration process.
- If you can't use least privilege, you need **sysadmin** permissions on the source SQL Server instance.

You need the [Virtual Machine Contributor](/azure/role-based-access-control/built-in-roles/compute#virtual-machine-contributor) role on the target Azure VM to perform the migration.

## Create a storage account

You use an Azure Blob Storage account as intermediary storage for backup files between your SQL Server instance and your SQL Server on Azure VM. The storage account needs to be in the same Azure subscription and region as your SQL Server on Azure VM target.

To create a new storage account and a blob container inside the storage account:

1. [Create a storage account](/azure/storage/common/storage-account-create?tabs=azure-portal):
   1. Search for **Storage accounts** in the Azure portal, and select **Create**.
   1. On the **Basics** tab, select your subscription and resource group. The region should be the same as your SQL Server on Azure VM target.
   1. Leave **Preferred storage type** blank.
   1. Use default settings for the rest of the tabs, and select **Review + create**.
   1. After validation passes, select **Create**.
1. [Create a blob container](/azure/storage/blobs/storage-quickstart-blobs-portal) inside the storage account.
   1. Go to your new storage account in the Azure portal.
   1. Under **Data storage**, select **Containers**.
   1. Use **Add container** to open the **New container** pane.
   1. Enter a name for your container, leave options at their defaults, and select **Create** to create your container.
1. (Optional) If your Azure Storage is behind a firewall, your Azure Blob storage requires [additional configuration](/azure/azure-sql/managed-instance/log-replay-service-migrate#configure-azure-storage-behind-a-firewall) after your SQL Server VM is provisioned. Configure delegation for the subnet with the name **VM**.

## Grant permissions to Azure Blob Storage

SQL Server VM migration in Azure Arc uses a managed identity to authenticate to Azure Blob Storage.

You need to grant the following permissions:
- [Grant user access to the storage account](#grant-user-access-to-the-storage-account) where you plan to store backups during the migration process.
- [Grant user access to the resource group](#grant-user-access-to-the-resource-group) that contains the storage account.
- [Grant managed identity access to the storage account](#grant-managed-identity-access-to-the-storage-account) after your SQL Server VM is provisioned.

### Grant user access to the storage account

To access database backups during the migration process, assign the user who signs in to the Azure portal and performs the migration to the [Storage Blob Data Reader](/azure/role-based-access-control/built-in-roles/storage#storage-blob-data-reader) role for the storage account that contains the backups.

To assign the role, follow these steps:
1. In the Azure portal, go to the resource group that contains your storage account.
1. Select **Access control (IAM)** from the resource menu.
1. Use **+ Add** to select **Add role assignment** and open the **Add role assignment** pane.
1. Search for and select the **Storage Blob Data Reader** role. Then, select **Next**.

   :::image type="content" source="media/migration-sql-mi-prepare-log-replay-service/search-storage-blob-data-reader.png" alt-text="Screenshot of finding the Storage Blob Data Reader role on the IAM page for the storage account in the Azure portal." lightbox="media/migration-sql-mi-prepare-log-replay-service/search-storage-blob-data-reader.png":::

1. Use **+ Select members** to open the **Select members** pane, and search for the user account of the person performing the migration. If multiple people are migrating data, grant all of those users this access. Select the user account, and then use **Select** to save your selection. Check the option to assign access to **User, group, or service principal**.
1. Select **Review + assign** to go to the **Review + assign** tab, and then select **Review + assign** again to complete the role assignment.

### Grant user access to the resource group

To access database backups during the migration process, the user who signs in to the Azure portal and performs the migration needs to be assigned the **Reader** role on the resource group that contains the storage account.

To assign the role, follow these steps:
1. In the Azure portal, go to the resource group that contains your storage account.
1. Select **Access control (IAM)** from the resource menu.
1. Use **+ Add** to select **Add role assignment** and open the **Add role assignment** pane.
1. Search for and select the **Reader** role. Then, select **Next**.

   :::image type="content" source="media/migration-sql-mi-prepare-log-replay-service/search-reader-role.png" alt-text="Screenshot of finding the Reader role on the IAM page for the resource group in the Azure portal." lightbox="media/migration-sql-mi-prepare-log-replay-service/search-reader-role.png":::

1. Use **+ Select members** to open the **Select members** pane, and search for the user account of the person performing the migration. If multiple people are migrating data, grant all of those users this access. Select the user account, and then use **Select** to save your selection. Check the option to assign access to **User, group, or service principal** and then use **Next** to continue.
1. On the **Assignment type** tab, set the **Assignment type** to **Active** and the **Assignment duration** to **Permanent**:

   :::image type="content" source="media/migration-sql-mi-prepare-log-replay-service/reader-role-assignment-type.png" alt-text="Screenshot of setting the Assignment type to Active and the Assignment duration to Permanent on the Assignment type tab in the Azure portal." lightbox="media/migration-sql-mi-prepare-log-replay-service/reader-role-assignment-type.png":::

1. Select **Review + assign** to go to the **Review + assign** tab, and then select **Review + assign** again to complete the role assignment.

### Grant managed identity access to the storage account

After your SQL Server VM is provisioned, you need to assign the managed identity of your SQL Server VM the [Storage Blob Data Reader](/azure/role-based-access-control/built-in-roles/storage#storage-blob-data-reader) role so that it can access your Azure Blob Storage account during the migration process.

First, you must determine what kind of managed identity your SQL Server VM uses. To do so, follow these steps:
1. Go to your [Virtual machines resource](https://portal.azure.com/#view/Microsoft_Azure_ComputeHub/ComputeHubMenuBlade/~/virtualMachinesBrowse) in the Azure portal.
1. Under **Security**, select **Identity** to open the **Identity** pane. Choose between using the **System assigned** identity or a **User assigned** identity:
   1. On the **System assigned** tab, you can use the **Status** toggle to enable the system assigned identity if it's not already enabled. If the system assigned identity is enabled, you can then select **Azure role assignments** to go to the **Azure role assignments** page and then use **+Add role assignment (Preview)** to grant **Storage Blob Data Reader** permissions to the system assigned identity of the SQL Server VM by selecting **Storage** as the **Scope** and then selecting your resource.
   1. On the **User assigned** tab, you can see the list of user assigned identities that are attached to the SQL Server VM. If you want to add a new user assigned identity, select **+ Add user assigned identity**, and then select an existing user assigned identity from your subscription to attach it to the SQL Server VM. Make note of the name of the user assigned identity that you want to use for authentication to Azure Blob Storage, as you'll need it for the next steps.

To grant access for the user assigned managed identity to the storage account, follow these steps:
1. Go to the Azure Blob Storage account in the Azure portal that you intend to use for the migration.
1. Select **Access control (IAM)** from the resource menu.
1. Use **+ Add** to select **Add role assignment** and open the **Add role assignment** pane.
1. Search for and select the **Storage Blob Data Reader** role. Then, select **Next**.
1. Under **Assign access to** check the **Managed identity** option.
1. Use **Select members** to open the **Select members** pane.
1. Under **Managed identity**, select **User assigned managed identity**.
1. Search for the **Primary identity** name that you noted earlier from the **Identity** page of your [SQL Server VM](https://portal.azure.com/#view/HubsExtension/ServiceMenuBlade/~/SingleInstance/extension/SqlAzureExtension/menuId/AzureSqlHub/itemId/SingleInstance) and select it.
1. Use **Select** to save your selection.
1. Select **Review + assign** to go to the **Review + assign** tab, and then select **Review + assign** again to complete the role assignment.

## Upload backups to your Blob Storage account

When your blob container is ready and you've confirmed that your SQL Server VM can access the container, you can begin uploading your backups to your Azure Blob Storage account. When all of your backups are uploaded to your storage account, you're ready to proceed with the migration.

To upload your backups to Azure:
1. [Take backups on a SQL Server instance](#take-backups-on-a-sql-server-instance).
1. [Copy your backups to your Blob Storage account](#copy-backups-to-your-blob-storage-account).

Consider the following best practices:
- Take backups with `COMPRESSION` and `CHECKSUM` options to reduce the size of backup files and to prevent migrating a corrupt database.
- Take backups in smaller batches.
- Use parallel upload threads.
- Make the last backup file as small as possible.
- To migrate multiple databases by using the same Azure Blob Storage container, place all backup files for an individual database into a separate folder inside the container. Use flat-file structure for each database folder. Nesting folders inside database folders isn't supported.

### Take backups on a SQL Server instance

The steps in this section show you how to back up locally, but it's also possible to [back up directly to URL](../../relational-databases/backup-restore/sql-server-backup-to-url.md).

Set databases that you want to migrate to the full recovery model to allow log backups.

```sql
-- To permit log backups, before the full database backup, modify the database to use the full recovery
USE master;

ALTER DATABASE SampleDB
SET RECOVERY FULL;
GO
```

If you don't already have existing backups, then to manually make full, differential, and log backups of your database to local storage, use the following sample T-SQL scripts. `CHECKSUM` isn't required, but it's recommended to prevent migrating a corrupt database, and for faster restore times.

The following example takes a full database backup to the local disk:

```sql
-- Take full database backup to local disk
BACKUP DATABASE [SampleDB]
    TO DISK = 'C:\BACKUP\SampleDB_full.bak'
    WITH INIT, COMPRESSION, CHECKSUM;
GO
```

The following example takes a differential backup to the local disk:

```sql
-- Take differential database backup to local disk
BACKUP DATABASE [SampleDB]
    TO DISK = 'C:\BACKUP\SampleDB_diff.bak'
    WITH DIFFERENTIAL, COMPRESSION, CHECKSUM;
GO
```

The following example takes a transaction log backup to the local disk:

```sql
-- Take transactional log backup to local disk
BACKUP LOG [SampleDB]
    TO DISK = 'C:\BACKUP\SampleDB_log.trn'
    WITH COMPRESSION, CHECKSUM;
GO
```

### Copy backups to your Blob Storage account

After your backups are ready, and you want to start migrating databases to a SQL Server VM, use the following approaches to copy existing backups to your Blob Storage account:

- Download and install [AzCopy](/azure/storage/common/storage-use-azcopy-v10).
- Download and install [Azure Storage Explorer](/azure/vs-azure-tools-storage-manage-with-storage-explorer?tabs=windows).
- Use [Storage Explorer in the Azure portal](/azure/storage/blobs/storage-quickstart-blobs-portal?source=recommendations).

> [!NOTE]  
> To migrate multiple databases by using the same Azure Blob Storage container, place all backup files for an individual database into a separate folder inside the container. Use flat-file structure for each database folder. Nesting folders inside database folders isn't supported.

## Validate your SQL Server VM storage access

Validate that your SQL Server VM can access your Blob Storage account.

First, upload any database backup, such as `full_0_0.bak`, to your Azure Blob Storage container.

Next, connect to your SQL Server VM, and run a sample test query to determine if your SQL Server VM is able to access the backup in the container.

### [SAS token](#tab/sas-token)

If you're using a SAS token to authenticate to your storage account, then replace the `<sastoken>` with your SAS token and run the following query on your SQL Server VM:

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/databases]
    WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
    SECRET = '<sastoken>';

RESTORE HEADERONLY
    FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/full_0_0.bak';
```

### [Managed identity](#tab/managed-identity)

If you're using a managed identity to authenticate to your storage account, then update the `CREATE CREDENTIAL` with your storage account URL, and run the following sample query on your SQL Server VM:

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>]
    WITH IDENTITY = 'MANAGED IDENTITY';

RESTORE HEADERONLY
    FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/full_0_0.bak';
```

---

## Register SQL Server VMs with the SQL IaaS Agent extension

If your target SQL Server VM already exists, you must [register it with the SQL IaaS Agent extension](/azure/azure-sql/virtual-machines/windows/sql-agent-extension-manually-register-single-vm) before you can select it as a migration target in the Azure portal. If your target SQL Server VM doesn't exist yet, you can provision a new one during the migration process, and it will be automatically registered with the SQL IaaS Agent extension.

## Limitations

Consider the following limitations when you migrate your SQL Server databases to SQL Server on Azure VMs through the Azure portal:
- If you migrate a single database, you must place the database backups in a flat-file structure inside a database folder (including container root folder). You can't nest these folders, as nesting isn't supported.
- If you migrate multiple databases using the same Azure Blob Storage container, you must place backup files for different databases in separate folders inside the container.
- You can't overwrite existing databases in your target SQL Server on an Azure VM using DMS.
- SQL Server migration doesn't support configuring high availability and disaster recovery on your target to match source topology.
- The following server objects aren't supported:
   - SQL Server Agent jobs
   - Credentials
   - SQL Server Integration Services (SSIS) packages
   - Server audit
- You can't use an existing self-hosted integration runtime created from Azure Data Factory (ADF) for database migrations with DMS.
- VMs with target versions of SQL Server 2008 and older aren't supported when migrating to SQL Server on an Azure VM.
- Registration with the SQL IaaS Agent extension is required for migration. The extension only supports a default instance or single named instance.
- You can migrate a maximum of 100 databases to the same Azure VM as the target using one or more migrations simultaneously. Moreover, once a migration with 100 databases finishes, wait for at least 30 minutes before starting a new migration to the same SQL Server on an Azure VM as the target. Also, every migration operation (start migration, cutover) for each database takes a few minutes sequentially. For example, to migrate 100 databases, it might take approximately 200 (2 x 100) minutes to create the migration queues and approximately 100 (1 x 100) minutes to cutover all 100 databases (excluding backup and restore timing). Therefore, the migration becomes slower as the number of databases increases. You should either schedule a longer migration window in advance based on rigorous migration testing, or partition large numbers of databases into batches when migrating them to SQL Server on an Azure VM.
- Apart from configuring the Networking/Firewall of your Azure Storage account to allow your VM to access backup files, you also need to configure the Networking/Firewall of your SQL Server on an Azure VM to allow outbound connection to your storage account.
- You need to keep the target Azure VM powered on while the SQL Server migration is in progress. Also, when creating a new migration, failover or cancel any migrations that are in progress migration.
- Migrating from a SQL Server failover cluster instance (FCI) is not currently supported.

## Next step

> [!div class="nextstepaction"]
> [Migrate to SQL Server on Azure VMs](migrate-to-sql-server-on-azure-vms.md)

## Related content

- [SQL Server migration in Azure Arc Overview](migration-overview.md)
- [SQL Server enabled by Azure Arc](overview.md)
- [Migration experience feedback directly to the product group](https://aka.ms/arc-migrations-feedback)
