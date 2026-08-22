---
title: "Rotate asymmetric keys for TDE by using Azure Key Vault on SQL Server"
description: Manually rotate the Azure Key Vault asymmetric key used for SQL Server Transparent Data Encryption with Extensible Key Management.
author: Pietervanhove
ms.author: pivanho
ms.reviewer: randolphwest, vanto
ms.date: 06/16/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
ai-usage: ai-assisted
helpviewer_keywords:
  - "TDE key rotation"
  - "EKM key rotation"
  - "Azure Key Vault rotation"
  - "SQL Server Connector"
---
# Rotate asymmetric keys for TDE by using Azure Key Vault on SQL Server

[!INCLUDE [sqlserver](../../../includes/applies-to-version/sqlserver.md)]

This article shows how to manually rotate the asymmetric key used by Transparent Data Encryption (TDE) with Extensible Key Management (EKM) and Azure Key Vault.

Use this procedure after you configure TDE with Azure Key Vault. For initial setup instructions, see [Set up Transparent Data Encryption with Azure Key Vault for SQL Server](set-up-transparent-data-encryption-with-azure-key-vault-for-sql-server.md).

> [!IMPORTANT]
> SQL Server doesn't automatically rotate the asymmetric key used for TDE. Rotation is a manual operation.
>
> Rotating the logical TDE protector is an online operation and usually completes quickly, because SQL Server re-encrypts only the database encryption key (DEK), not the entire database.
>
> Don't delete previous key versions after rotation. Earlier versions might still be required to restore older backups, transaction logs, or other recovery artifacts.

<a id="before-you-begin"></a>

## Prerequisites

- Complete the initial EKM and TDE setup.
- Verify that SQL Server can currently access the key vault.
- Ensure you have permissions to create credentials, logins, and asymmetric keys in SQL Server.
- Ensure your authentication model is still valid:
  - Service principal: app registration and client secret are valid.
  - Managed identity: the SQL Server managed identity is still enabled and has the required key permissions.

## Rotate the key

Choose your authentication model and follow the matching steps.

# [Service principal](#tab/ServicePrincipal)

1. In Azure Key Vault or Managed HSM, rotate the key:
   - Create a new version of the same key name, or
   - Create a new key name.

1. In SQL Server, create a credential for the rotation operation if needed.

   ```sql
   CREATE CREDENTIAL <new_credential_name>
      WITH IDENTITY = <key_vault_name_or_uri_without_https>,
      SECRET = '<client_id_without_hyphens><client_secret>'
      FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM;
   ```

1. Add the credential to the setup principal that creates the next asymmetric key.

   ```sql
   ALTER LOGIN [<domain>\<login>]
      ADD CREDENTIAL [<new_credential_name>];
   ```

1. Create the new SQL Server asymmetric key from Azure Key Vault.

   > [!NOTE]
   > For manual rotation, SQL Server supports both a versionless key name and a versioned key reference:
   >
   > - Versionless: use `<key_name>` to let SQL Server use the highest available key version in Azure Key Vault.
   > - Versioned: use `<key_name>/<version>` to pin encryption operations to a specific key version.
   >
   > You don't need to create a different AKV key name to rotate. Rotate keys by creating a new key version and then creating a new SQL Server asymmetric key that references the versionless or versioned form.

   Use either a versionless key name or a specific key version:

   ```sql
   CREATE ASYMMETRIC KEY <new_ekm_key_name>
      FROM PROVIDER [AzureKeyVault_EKM]
      WITH PROVIDER_KEY_NAME = '<key_name_or_key_name/version>',
      CREATION_DISPOSITION = OPEN_EXISTING;
   ```

1. Create a new login from the new asymmetric key.

   ```sql
   CREATE LOGIN <new_login_name>
      FROM ASYMMETRIC KEY <new_ekm_key_name>;
   ```

1. Move the credential mapping from the setup login to the new asymmetric-key login.

   ```sql
   ALTER LOGIN [<domain>\<login>]
      DROP CREDENTIAL [<new_credential_name>];

   ALTER LOGIN <new_login_name>
      ADD CREDENTIAL [<new_credential_name>];
   ```

1. Re-encrypt the database encryption key (DEK) by using the new asymmetric key.

   ```sql
   USE [<database_name>];
   GO
   ALTER DATABASE ENCRYPTION KEY
      ENCRYPTION BY SERVER ASYMMETRIC KEY <new_ekm_key_name>;
   ```

# [Managed identity](#tab/ManagedIdentity)

1. In Azure Key Vault or Managed HSM, rotate the key:
   - Create a new version of the same key name, or
   - Create a new key name.

1. In SQL Server, create a credential for the rotation operation if needed.

   ```sql
   CREATE CREDENTIAL [<vault-hostname>]
      WITH IDENTITY = 'Managed Identity'
      FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM;
   ```

1. Add the credential to the setup principal that creates the next asymmetric key.

   ```sql
   ALTER LOGIN [<domain>\<login>]
      ADD CREDENTIAL [<vault-hostname>];
   ```

1. Create the new SQL Server asymmetric key from Azure Key Vault.

   > [!NOTE]
   > For manual rotation, SQL Server supports both a versionless key name and a versioned key reference:
   >
   > - Versionless: use `<key_name>` to let SQL Server use the highest available key version in Azure Key Vault.
   > - Versioned: use `<key_name>/<version>` to pin encryption operations to a specific key version.
   >
   > You don't need to create a different AKV key name to rotate. Rotate keys by creating a new key version and then creating a new SQL Server asymmetric key that references the versionless or versioned form.

   Use either a versionless key name or a specific key version:

   ```sql
   CREATE ASYMMETRIC KEY <new_ekm_key_name>
      FROM PROVIDER [AzureKeyVault_EKM]
      WITH PROVIDER_KEY_NAME = '<key_name_or_key_name/version>',
      CREATION_DISPOSITION = OPEN_EXISTING;
   ```

1. Create a new login from the new asymmetric key.

   ```sql
   CREATE LOGIN <new_login_name>
      FROM ASYMMETRIC KEY <new_ekm_key_name>;
   ```

1. Move the credential mapping from the setup login to the new asymmetric-key login.

   ```sql
   ALTER LOGIN [<domain>\<login>]
      DROP CREDENTIAL [<vault-hostname>];

   ALTER LOGIN <new_login_name>
      ADD CREDENTIAL [<vault-hostname>];
   ```

1. Re-encrypt the database encryption key (DEK) by using the new asymmetric key.

   ```sql
   USE [<database_name>];
   GO
   ALTER DATABASE ENCRYPTION KEY
      ENCRYPTION BY SERVER ASYMMETRIC KEY <new_ekm_key_name>;
   ```

---

## Verify rotation

1. Verify encryption metadata for the database.

   ```sql
   SELECT encryptor_type,
          encryption_state_desc,
          encryptor_thumbprint
   FROM sys.dm_database_encryption_keys
   WHERE database_id = DB_ID('<database_name>');
   ```

1. Verify the SQL Server asymmetric key thumbprint.

   ```sql
   SELECT name,
          algorithm_desc,
          thumbprint
   FROM sys.asymmetric_keys
   WHERE name = '<new_ekm_key_name>';
   ```

1. Confirm that the thumbprints match.

## Related content

- [Set up Transparent Data Encryption with Azure Key Vault for SQL Server](set-up-transparent-data-encryption-with-azure-key-vault-for-sql-server.md)
- [Use SQL Server Connector with SQL Encryption Features](use-sql-server-connector-with-sql-encryption-features.md)
- [SQL Server Connector maintenance and troubleshooting](sql-server-connector-maintenance-troubleshooting.md)
