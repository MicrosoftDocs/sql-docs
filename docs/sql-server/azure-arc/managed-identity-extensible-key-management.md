---
title: "Managed Identity Support for Extensible Key Management (EKM) with Azure Key Vault (AKV)"
description: Learn how to use managed identities with SQL Server enabled by Azure Arc and Transparent Data Encryption (TDE) Extensible Key Management with Azure Key Vault.
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto
ms.date: 11/18/2025
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
monikerRange: ">=sql-server-ver17"
---
# Managed Identity support for Extensible Key Management with Azure Key Vault

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

This article shows you how to use managed identities for Extensible Key Management (EKM) with Azure Key Vault (AKV) on [SQL Server enabled by Azure Arc](overview.md).

## Overview

Starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], managed identities are supported for EKM with AKV and Managed Hardware Security Modules (HSM) on SQL Server enabled by Azure Arc. Managed identities are the recommended authentication method to allow different Azure services to authenticate the SQL Server enabled by Azure Arc resource without using passwords or secrets. For more information on managed identities, see [Managed identity types](/entra/identity/managed-identities-azure-resources/overview#managed-identity-types).

## Prerequisites

- Connect the instance of SQL Server to Azure Arc. For more information, see [Automatically connect your SQL Server to Azure Arc](connect.md).
- [Enable managed identity for SQL Server 2025](managed-identity.md).
- An Azure Key Vault and key created in the key vault. For more information, see [Create a key vault](../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md?tabs=portal#step-2-create-a-key-vault).
- The managed identity for the SQL Server enabled by Azure Arc needs to have the `Key Vault Crypto Service Encryption User` role assigned to the key vault if you're using [Azure role-based access control](../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md?tabs=portal#azure-role-based-access-control) or the *Unwrap Key* and *Wrap Key* permissions if you're using [vault access policy](../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md?tabs=portal#vault-access-policy).
- [Download and install the preview version of the SQL Server Connector](https://www.microsoft.com/download/details.aspx?id=108278).

## Step 1: Add registry key for the EKM provider

Before you can create a credential using a managed identity, you need to add a registry key to enable the EKM provider to use managed identities. This step needs to be performed by the computer administrator. For detailed steps, see [Step 4: Add registry key to support EKM provider](../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md?tabs=portal#step-4-add-registry-key-to-support-ekm-provider).

## Step 2: Configure the `master` database

1. Open [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)].

1. Configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to use EKM by running the following [!INCLUDE [tsql](../../includes/tsql-md.md)] script:

   ```sql
   -- Enable advanced options.
   USE master;
   GO

   EXECUTE sp_configure 'show advanced options', 1;
   GO

   RECONFIGURE;
   GO

   -- Enable EKM provider
   EXECUTE sp_configure 'EKM provider enabled', 1;
   GO

   RECONFIGURE;
   GO
   ```

1. Register the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Connector as an EKM provider with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   Create a cryptographic provider by using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Connector, which is an EKM provider for the Azure Key Vault. In this example, the provider name is `AzureKeyVault_EKM`.

   ```sql
   CREATE CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM
   FROM FILE = 'C:\Program Files\SQL Server Connector for Microsoft Azure Key Vault\Microsoft.AzureKeyVaultService.EKM.dll';
   GO
   ```

   > [!NOTE]  
   > The file path length can't exceed 256 characters.

## Step 3: Create a server credential using a managed identity

The following example shows how to create a credential for a managed identity to use with the Azure Key Vault:

```sql
CREATE CREDENTIAL [<akv-name>.vault.azure.net]
    WITH IDENTITY = 'Managed Identity'
    FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM;
```

You can check the AKV name by querying `sys.credentials`:

```sql
SELECT name, credential_identity
FROM sys.credentials;
```

The `WITH IDENTITY = 'Managed Identity'` clause requires a primary managed identity assigned to the SQL Server enabled by Azure Arc.

For more information on setting up EKM with AKV, see [Set up SQL Server TDE Extensible Key Management by using Azure Key Vault](../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md).

## Create a credential to use with Managed Hardware Security Modules (HSMs)

To create a credential to use with Azure Key Vault Managed Hardware Security Modules (HSMs), use the following syntax:

```sql
CREATE CREDENTIAL [<akv-name>.managedhsm.azure.net]
    WITH IDENTITY = 'Managed Identity'
    FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM;
```

For more information on setting up EKM with AKV, see [Set up SQL Server TDE Extensible Key Management by using Azure Key Vault](../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md).

## T-SQL commands to upgrade existing EKM configuration to use managed identities

If your current configuration is using EKM with AKV using a secret, you'll need to drop the existing credential and create a new credential using a managed identity. The following T-SQL commands show how to upgrade your existing EKM configuration to use managed identities:

1. Create the credential using a managed identity:

   ```sql
   CREATE CREDENTIAL [<akv-name>.vault.azure.net]
       WITH IDENTITY = 'Managed Identity'
       FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM;
   ```

1. If there's a credential using a secret associated with the SQL Server administration domain login, drop the existing credential:

   ```sql
   ALTER LOGIN [<domain>\<login>]
   DROP CREDENTIAL [<existing-credential-name>];
   ```

1. Associate the new credential with the SQL Server administration domain login:

   ```sql
   ALTER LOGIN [<domain>\<login>]
   ADD CREDENTIAL [<akv-name>.vault.azure.net];
   ```

You can check the encrypted database view to verify the database encryption using the following query:

```sql
SELECT *
FROM sys.dm_database_encryption_keys
WHERE database_id = db_id('<your-database-name>');
```

For more information on setting up EKM with AKV, see [Set up SQL Server TDE Extensible Key Management by using Azure Key Vault](../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md).

## Error messages

[Trace flag 4675](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4675) can be used to check credentials created with a managed identity. If the `CREATE CREDENTIAL` statement was executed without trace flag 4675 enabled, no error message is issued if the primary managed identity isn't set for the server. To troubleshoot this scenario, the credential must be deleted and recreated again once the trace flag is enabled.

## Limitations

- Server-level managed identity is only supported for SQL Server 2025 enabled by Azure Arc, and not on SQL Server on-premises. Server-level managed identity isn't supported for Linux.
- Managed identity support for EKM with AKV requires the latest [SQL Server Connector preview version](https://www.microsoft.com/download/details.aspx?id=45344).

## Related content

- [CREATE CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-credential-transact-sql.md)
- [Set trace flags with DBCC TRACEON (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
