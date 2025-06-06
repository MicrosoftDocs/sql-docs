---
title: Backup and restore to URL using managed identities
description: Learn how to back up and restore SQL Server databases to Azure Blob storage using managed identities for SQL Server on Azure VMs.
author: PratimDasgupta
ms.author: prdasgu
ms.reviewer: vanto, mathoma
ms.date: 02/16/2025
ms.service: azure-vm-sql-server
ms.subservice: security
ms.topic: how-to
ms.custom:
  - build-2025
---
# Backup and restore to URL using managed identities

[!INCLUDE [appliesto-sqlvm-windows-only](../../includes/appliesto-sqlvm-windows-only.md)]

This article teaches you to back up to and restore [SQL Server on Azure Virtual Machines (VM)](sql-server-on-azure-vm-iaas-what-is-overview.md) databases from a URL by using Microsoft Entra managed identities.

<!-- Content matches docs\sql-server\azure-arc\backup-to-url, and should be maintained in both places -->

## Overview

Starting with SQL Server 2022 Cumulative Update 17 (CU17), you can use managed identities with [SQL Server credentials](/sql/t-sql/statements/create-credential-transact-sql) to back up to and restore SQL Server on Azure VM databases from Azure Blob storage. [Managed identities](/entra/identity/managed-identities-azure-resources/overview) provide an identity for applications to use when connecting to resources that support Microsoft Entra authentication.

Using managed identities in the credentials for the `BACKUP TO URL` and `RESTORE FROM URL` T-SQL operations is only supported by SQL Server on Azure VMs. Using managed identities with SQL Server on-premises to `BACKUP TO URL` and `RESTORE FROM URL` isn't supported.

## Prerequisites

- A SQL Server on Azure VM with SQL Server 2022 CU17 or later [registered with the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md).
- The SQL Server instance that is backing up to, or restoring from, URL must be [configured with Microsoft Entra authentication](configure-azure-ad-authentication-for-sql-vm.md), whether or not it's the instance registered with the extension.
- An [Azure Blob storage account](/azure/storage/common/storage-account-create).
- Valid network access to the Azure Blob storage and Windows Firewall permissions on the host to allow the outbound connection, and valid storage account service endpoints.
- The primary managed identity for the SQL Server on Azure VM needs:
  - To be assigned with a user-assigned managed identity or system-assigned managed identity. For more information, see [Configure managed identities on Azure virtual machines (VMs)](/entra/identity/managed-identities-azure-resources/how-to-configure-managed-identities).
  - To have the `Storage Blob Data Contributor` role for the primary managed identity assigned to the storage account.

## Create a server credential using managed identities

In order to use the T-SQL commands `BACKUP DATABASE <database name> TO URL` and `RESTORE <database name> FROM URL` with managed identities, you need to create a server credential that uses the managed identity. The credential name represents the Azure storage URL and indicates where the database backup is stored.

The following example shows how to create a credential for a managed identity:

```sql
CREATE CREDENTIAL [https://<storage-account-name>.blob.core.windows.net/<container-name>] 
    WITH IDENTITY = 'Managed Identity'
```

The `WITH IDENTITY = 'Managed Identity'` clause requires a primary managed identity assigned to the SQL Server on Azure VM.

For more information on error messages that can occur if the primary managed identity isn't assigned or given proper permissions, see the [Error messages](#error-messages) section.

## BACKUP to URL with a managed identity

After you create the credential, you can use it to back up and restore databases to Azure Blob storage. Make sure that the primary managed identity for the SQL Server on Azure VM has the `Storage Blob Data Contributor` role assigned to the storage account.

The following example shows how to back up a database to Azure Blob storage using the managed identity credential:

```sql
BACKUP DATABASE [AdventureWorks] 
    TO URL = 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak' 
```

## RESTORE from URL with a managed identity

The following example shows how to restore a database from Azure Blob storage using the managed identity credential:

```sql
RESTORE DATABASE [AdventureWorks] 
    FROM URL = 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak' 
```

## Error messages

[Trace flag 4675](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql#tf4675) can be used to check credentials created with a managed identity. If the `CREATE CREDENTIAL` statement was executed without trace flag 4675 enabled, no error message is issued if the primary managed identity isn't set for the server. To troubleshoot this scenario, the credential must be deleted and recreated again once the trace flag is enabled.

### No primary managed identity assigned

If a primary managed identity isn't assigned to the SQL Server on Azure VM, the backup and restore operations will fail with an error message indicating that the managed identity isn't selected.

```sql
Msg 37563, Level 16, State 2, Line 14
The primary managed identity is not selected for this server. Enable the primary managed identity for Microsoft Entra authentication for this server. For more information see (https://aka.ms/sql-server-managed-identity-doc).`
```

### No `Storage Blob Data Contributor` role assigned

If the primary managed identity for the SQL Server on Azure VM isn't given the `Storage Blob Data Contributor` role to the storage account, the **BACKUP** operation will fail with an error message indicating that access is denied.

```sql
Msg 3201, Level 16, State 1, Line 31
Cannot open backup device 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak'. Operating system error 5(Access is denied.).
Msg 3013, Level 16, State 1, Line 31
BACKUP DATABASE is terminating abnormally. 
```

If the managed identity for the SQL Server on Azure VM isn't given the `Storage Blob Data Contributor` role to the storage account, the **RESTORE** operation will fail with an error message indicating that access is denied.

```sql
Msg 3201, Level 16, State 1, Line 31
Cannot open backup device 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak'. Operating system error 5(Access is denied.).
Msg 3013, Level 16, State 1, Line 31
RESTORE DATABASE is terminating abnormally. 
```

### Network or Firewall Issues

If valid network access to the Azure Blob storage and Windows Firewall permissions on the host to allow the outbound connection, and valid storage account service endpoints aren't configured, the **BACKUP** operation fails with an error message indicating that access is denied.

```sql
Msg 3201, Level 16, State 1, Line 31
Cannot open backup device 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak'. Operating system error 5(Access is denied.).
Msg 3013, Level 16, State 1, Line 31
BACKUP DATABASE is terminating abnormally. 
```

If valid network access to the Azure Blob storage and Windows Firewall permissions on the host to allow the outbound connection and valid storage account service endpoints aren't configured,  the **RESTORE** operation fails with an error message indicating that access is denied.

```sql
Msg 3201, Level 16, State 1, Line 31
Cannot open backup device 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak'. Operating system error 5(Access is denied.).
Msg 3013, Level 16, State 1, Line 31
RESTORE DATABASE is terminating abnormally.
```

### Duplicate database name

When the original database with the same name exists in the storage, the backup of a new database to the same storage path will fail with the following error:

```sql
Msg 1834, Level 16, State 1, Line 35
RESTORE DATABASE AdventureWorks 
from URL = 'https://<storage-account-name>.blob.core.windows.net/<container-name>/AdventureWorks.bak' 
Msg 1834, Level 16, State 1, Line 35 
The file 'C:\Server\sqlservr\data\AdventureWorks.mdf' cannot be overwritten.  It is being used by the database 'AdventureWorks'. 
Msg 3156, Level 16, State 4, Line 35 
File 'AdventureWorks' cannot be restored to 'C:\Server\sqlservr\data\AdventureWorks.mdf'. Use WITH MOVE to identify a valid location for the file.
```

To resolve this issue, drop the original database or move the used files to a different location before restoring the database. For more information, see [Restore a database to a new location (SQL Server)](/sql/relational-databases/backup-restore/restore-a-database-to-a-new-location-sql-server).

## Limitations

- Server-level managed identity is only supported for SQL Server on Azure VM, and not on SQL Server on-premises. Server-level managed identity isn't supported for Linux.

- `BACKUP TO URL` or `RESTORE FROM URL` with a managed identity is only supported for SQL Server on Azure VM. `BACKUP TO URL` or `RESTORE FROM URL` isn't supported by SQL Server on-premises.

- Managed identities aren't supported with failover cluster instance (FCI).

- `BACKUP TO URL` can only be executed with the same managed identity used for the SQL Server on Azure VM, whether the server has one or many instances of SQL Server on the VM.

## Related content

- [Enable Microsoft Entra authentication for SQL Server on Azure VMs](configure-azure-ad-authentication-for-sql-vm.md)
- [Create an Azure storage account](/azure/storage/common/storage-account-create)
- [SQL Server backup to URL for Microsoft Azure Blob Storage](/sql/relational-databases/backup-restore/sql-server-backup-to-url)
- [CREATE CREDENTIAL (Transact-SQL)](/sql/t-sql/statements/create-credential-transact-sql)
- [Trace flag 4675](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql#tf4675)
