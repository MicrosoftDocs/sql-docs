---
title: Back up to URL with Managed Identity
description: Describes how to back up and restore databases with for SQL Server enabled by Azure Arc using managed identity
author: PratimDasgupta
ms.author: prdasgu
ms.reviewer: vanto
ms.date: 11/18/2025
ms.topic: how-to
ms.custom:
  - ignite-2025
monikerRange: ">=sql-server-ver17 || >=sql-server-linux-ver17"
---
# Back up to URL with managed identity - SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

This article describes how to use a managed identity to back up and restore databases with Azure Blob storage using [SQL Server enabled by Azure Arc](overview.md).

<!-- Content matches /azure-sql/virtual-machines/windows/backup-restore-to-url-using-managed-identities, and should be maintained in both places -->

For SQL Server on Azure VMs, review [Backup and restore to URL using managed identities](/azure/azure-sql/virtual-machines/windows/backup-restore-to-url-using-managed-identities).

## Prerequisites

To back up and restore databases with Azure Blob storage using managed identities, you need the following:

- [SQL Server 2025 enabled by Azure Arc](deployment-options.md) that's been assigned a [primary managed identity](managed-identity.md).
- An [Azure Blob storage account](/azure/storage/common/storage-account-create).
- Valid network access to the Azure Blob storage and Windows Firewall permissions on the host to allow the outbound connection, and valid storage account service endpoints.
- The primary managed identity for the SQL Server instance needs to have the `Storage Blob Data Contributor` role assigned to the storage account.

## Check storage account permissions

The primary managed identity for the SQL Server instance needs to have the `Storage Blob Data Contributor` role assigned to the storage account. This role allows the managed identity to write to and read from the storage account.

Use the Azure portal to check the permissions assigned to the managed identity by following these steps:

1. Go to your [Storage account](https://portal.azure.com/#browse/Microsoft.Storage%2FStorageAccounts) in the Azure portal.

1. Select **Access Control (IAM)** to open the **Access Control (IAM)** pane.

1. On the **Access Control (IAM)** pane, select **Role assignments** to view the list of users and groups that have been assigned roles for the storage account.

1. Filter by the `Storage Blob Data Contributor` role and verify that you see the managed identity for your SQL Server instance listed:

   :::image type="content" source="media/backup-to-url/arc-server-blob-write-access.png" alt-text="Screenshot of Azure portal Access Control for the storage account. Demonstrates that the Arc server managed identity needs to be added as a Storage Blob Data Contributor." lightbox="media/backup-to-url/arc-server-blob-write-access.png":::

## Create a server credential using managed identities

In order to use the T-SQL commands `BACKUP DATABASE <database name> TO URL` and `RESTORE <database name> FROM URL` with managed identities, you need to create a server credential that uses the managed identity. The credential name represents the Azure storage URL and indicates where the database backup is stored.

The following example shows how to create a credential for a managed identity:

```sql
CREATE CREDENTIAL [https://<storage-account-name>.blob.core.windows.net/<container-name>]
    WITH IDENTITY = 'Managed Identity';
```

The `WITH IDENTITY = 'Managed Identity'` clause requires a primary managed identity assigned to the SQL Server instance.

## BACKUP to URL with a managed identity

After you create the credential, you can use it to back up databases to Azure Blob storage.

The following example shows how to back up a database to Azure Blob storage using the managed identity credential:

```sql
BACKUP DATABASE [AdventureWorks]
    TO URL = 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak';
```

## RESTORE from URL with a managed identity

The following example shows how to restore a database from Azure Blob storage using the managed identity credential:

```sql
RESTORE DATABASE [AdventureWorks]
    FROM URL = 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak';
```

## Error messages

The error messages listed in the [SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/backup-restore-to-url-using-managed-identities) article also apply to SQL Server enabled by Azure Arc.

## Related content

- [Tutorial: Set up Microsoft Entra authentication for SQL Server enabled by Azure Arc](entra-authentication-setup-tutorial.md)
- [Backup and restore to URL using managed identities](/azure/azure-sql/virtual-machines/windows/backup-restore-to-url-using-managed-identities)
- [Restore to a point-in-time](point-in-time-restore.md)
- [Manage inventory of SQL Server resources with Azure Arc](view-inventory.md)
- [Recovery models (SQL Server)](../../relational-databases/backup-restore/recovery-models-sql-server.md)
