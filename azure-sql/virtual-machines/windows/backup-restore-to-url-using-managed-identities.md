---
title: Backup and Restore to URL Using Managed Identities
description: Learn how to back up and restore SQL Server databases to Azure Blob storage using managed identities for SQL Server on Azure VMs.
author: PratimDasgupta
ms.author: prdasgu
ms.reviewer: vanto, mathoma, randolphwest
ms.date: 03/31/2026
ms.service: azure-vm-sql-server
ms.subservice: security
ms.topic: how-to
ms.custom:
  - build-2025
---
# Backup and restore to URL using managed identities

[!INCLUDE [appliesto-sqlvm-windows-only](../../includes/appliesto-sqlvm-windows-only.md)]

This article shows you how to back up and restore [SQL Server on Azure Virtual Machines (VM)](sql-server-on-azure-vm-iaas-what-is-overview.md) databases from a URL by using Microsoft Entra managed identities.

<!-- Content matches docs\sql-server\azure-arc\backup-to-url, and should be maintained in both places -->

## Overview

Starting with SQL Server 2022 Cumulative Update 17 (CU17), you can use managed identities with [SQL Server credentials](/sql/t-sql/statements/create-credential-transact-sql) to back up to and restore SQL Server on Azure VM databases from Azure Blob storage. [Managed identities](/entra/identity/managed-identities-azure-resources/overview) provide an identity for applications to use when connecting to resources that support Microsoft Entra authentication.

## Prerequisites

- A SQL Server on Azure VM with SQL Server 2022 CU17 or later [registered with the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md).
- The SQL Server instance that backs up to or restores from URL must be [configured with Microsoft Entra authentication](configure-azure-ad-authentication-for-sql-vm.md), whether or not it's the instance registered with the extension.
- An [Azure Blob storage account](/azure/storage/common/storage-account-create).
- Valid network access to the Azure Blob storage and Windows Firewall permissions on the host to allow the outbound connection, and valid storage account service endpoints.
- The primary managed identity for the SQL Server on Azure VM needs:
  - To be assigned a user-assigned managed identity or system-assigned managed identity. For more information, see [Configure managed identities on Azure virtual machines (VMs)](/entra/identity/managed-identities-azure-resources/how-to-configure-managed-identities).
  - To have the `Storage Blob Data Contributor` role for the primary managed identity assigned to the storage account.

## Create a server credential by using managed identities

To use the T-SQL commands `BACKUP DATABASE <database name> TO URL` and `RESTORE <database name> FROM URL` with managed identities, you need to create a server credential that uses the managed identity. The credential name represents the Azure storage URL and indicates where the database backup is stored.

The following example shows how to create a credential for a managed identity:

```sql
CREATE CREDENTIAL [https://<storage-account-name>.blob.core.windows.net/<container-name>]
    WITH IDENTITY = 'Managed Identity'
```

The `WITH IDENTITY = 'Managed Identity'` clause requires a primary managed identity assigned to the SQL Server on Azure VM.

For more information about error messages that can occur if the primary managed identity isn't assigned or given proper permissions, see the [Error messages](#error-messages) section.

> [!NOTE]
> The credential is case sensitive and must match the storage account name, which requires a lowercase name.

## BACKUP to URL by using a managed identity

After you create the credential, you can use it to back up and restore databases to Azure Blob storage. Make sure that the primary managed identity for the SQL Server on Azure VM has the `Storage Blob Data Contributor` role assigned to the storage account.

The following example shows how to back up a database to Azure Blob storage by using the managed identity credential:

```sql
BACKUP DATABASE [AdventureWorks]
    TO URL = 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak'
```

## RESTORE from URL by using a managed identity

The following example shows how to restore a database from Azure Blob storage by using the managed identity credential:

```sql
RESTORE DATABASE [AdventureWorks]
    FROM URL = 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak'
```

## Error messages

Use [Trace flag 4675](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql#tf4675) to check credentials created with a managed identity. If you run the `CREATE CREDENTIAL` statement without enabling trace flag 4675, no error message is returned if the primary managed identity isn't set for the server. To troubleshoot this scenario, you must delete and recreate the credential once the trace flag is enabled.

### No primary managed identity assigned

If you don't assign a primary managed identity to the SQL Server on Azure VM, the backup and restore operations fail with an error message that indicates the managed identity isn't selected.

```output
Msg 37563, Level 16, State 2, Line 14
The primary managed identity is not selected for this server. Enable the primary managed identity for Microsoft Entra authentication for this server. For more information see (https://aka.ms/sql-server-managed-identity-doc).`
```

### No `Storage Blob Data Contributor` role assigned

If you don't assign the `Storage Blob Data Contributor` role to the primary managed identity for the SQL Server on Azure VM, the `BACKUP` operation fails with an error message that indicates access is denied.

```output
Msg 3201, Level 16, State 1, Line 31
Cannot open backup device 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak'. Operating system error 5(Access is denied.).
Msg 3013, Level 16, State 1, Line 31
BACKUP DATABASE is terminating abnormally.
```

If you don't assign the `Storage Blob Data Contributor` role to the managed identity for the SQL Server on Azure VM, the `RESTORE` operation fails with an error message that indicates access is denied.

```output
Msg 3201, Level 16, State 1, Line 31
Cannot open backup device 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak'. Operating system error 5(Access is denied.).
Msg 3013, Level 16, State 1, Line 31
RESTORE DATABASE is terminating abnormally.
```

### Network or firewall issues

If you don't configure valid network access to the Azure Blob storage, and Windows Firewall permissions on the host, to allow the outbound connection, and valid storage account service endpoints aren't configured, the `BACKUP` operation fails with an error message that indicates access is denied.

```output
Msg 3201, Level 16, State 1, Line 31
Cannot open backup device 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak'. Operating system error 5(Access is denied.).
Msg 3013, Level 16, State 1, Line 31
BACKUP DATABASE is terminating abnormally.
```

If you don't configure valid network access to the Azure Blob storage, and Windows Firewall permissions on the host, to allow the outbound connection, and valid storage account service endpoints aren't configured, the `RESTORE` operation fails with an error message that indicates access is denied.

```output
Msg 3201, Level 16, State 1, Line 31
Cannot open backup device 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak'. Operating system error 5(Access is denied.).
Msg 3013, Level 16, State 1, Line 31
RESTORE DATABASE is terminating abnormally.
```

### Duplicate database name

When the original database with the same name exists in the storage, the restore of a new database to the same storage path fails with the following error:

```output
Msg 1834, Level 16, State 1, Line 35
RESTORE DATABASE AdventureWorks
FROM URL = 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak';
Msg 1834, Level 16, State 1, Line 35
The file 'C:\Server\sqlservr\data\AdventureWorks.mdf' cannot be overwritten.  It is being used by the database 'AdventureWorks'.
Msg 3156, Level 16, State 4, Line 35
File 'AdventureWorks' cannot be restored to 'C:\Server\sqlservr\data\AdventureWorks.mdf'. Use WITH MOVE to identify a valid location for the file.
```

To resolve this issue, drop the original database or move the used files to a different location before restoring the database. For more information, see [Restore a database to a new location (SQL Server)](/sql/relational-databases/backup-restore/restore-a-database-to-a-new-location-sql-server).

## Limitations

- SQL Server on Azure VM supports server-level managed identity, but SQL Server on-premises doesn't. Linux doesn't support server-level managed identity.

- SQL Server on Azure VM supports `BACKUP TO URL` and `RESTORE FROM URL` with a managed identity starting with SQL Server 2022 CU17. SQL Server on-premises supports `BACKUP TO URL` or `RESTORE FROM URL` with a managed identity starting with SQL Server 2025.

- Failover cluster instance (FCI) doesn't support managed identities.

- You can only run `BACKUP TO URL` by using the same managed identity that you use for SQL Server on Azure VM, whether the server has one or many instances of SQL Server on the VM.

## Related content

- [Enable Microsoft Entra authentication for SQL Server on Azure VMs](configure-azure-ad-authentication-for-sql-vm.md)
- [Create an Azure storage account](/azure/storage/common/storage-account-create)
- [SQL Server backup to URL for Microsoft Azure Blob Storage](/sql/relational-databases/backup-restore/sql-server-backup-to-url)
- [CREATE CREDENTIAL (Transact-SQL)](/sql/t-sql/statements/create-credential-transact-sql)
- [Trace flag 4675](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql#tf4675)
