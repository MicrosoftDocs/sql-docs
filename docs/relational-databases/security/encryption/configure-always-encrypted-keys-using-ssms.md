---
title: "Provision Always Encrypted keys using SQL Server Management Studio | Microsoft Docs"
description: Learn how to provision column master keys and column encryption keys for Always Encrypted using SQL Server Management Studio.
ms.custom: ""
ms.date: 04/15/2021
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
f1_keywords: 
  - "SQL13.SWB.COLUMNMASTERKEY.PAGE.F1"
  - "SQL13.SWB.COLUMNENCRYPTIONKEY.PAGE.F1"
helpviewer_keywords: 
  - "Always Encrypted, configure with SSMS"
ms.assetid: 29816a41-f105-4414-8be1-070675d62e84
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Provision Always Encrypted keys using SQL Server Management Studio
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides the steps to provision column master keys and column encryption keys for Always Encrypted using [SQL Server Management Studio (SSMS)](../../../ssms/download-sql-server-management-studio-ssms.md).

For an overview of Always Encrypted key management, including best practice recommendations and important security considerations, see [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md).

<a name="provisioncmk"></a>
## Provision Column Master Keys with the New Column Master Key Dialog

The **New Column Master Key** dialog allows you to generate a column master key or pick an existing key in a key store, and create column master key metadata for the created or selected key in the database.

1.	Using **Object Explorer**, navigate to the **Security>Always Encrypted Keys** folder under your database.
2.	Right click on the **Column Master Keys** folder and select **New Column Master Key...**. 
3.	In the **New Column Master Key** dialog, enter the name of the column master key metadata object.
4.	Select a key store:
    - **Certificate Store - Current User** - indicates the Current User certificate store location in the Windows Certificate Store, which is your personal store. 
    - **Certificate Store - Local computer** - indicates the Local computer certificate store location in the Windows Certificate Store. 
    - **Azure Key Vault** -  you'll need to sign in to Azure (click **Sign in**). Once you sign in, you'll be able to pick one of your Azure subscriptions and a key vault or a managed HSM (requires SSMS 18.9 or later).
        > [!NOTE]
        > The **New Column Master Key** dialog does not currently support key vaults using role permissions for authorization. Only key vaults using access policies are supported.

        > [!NOTE]
        > Using column master keys stored in a [managed HSM](/azure/key-vault/managed-hsm/overview) in Azure Key Vault requires SSMS 18.9 or a later version.

    - **Key Store Provider (KSP)** - indicates a key store that is accessible via a key store provider (KSP) that implements the Cryptography Next Generation (CNG) API. Typically, this type of a store is a hardware security module (HSM). After you select this option, you'll need to pick a KSP. **Microsoft Software Key Store Provider** is selected by default. If you want to use a column master key stored in an HSM, select a KSP for your device (it must be installed and configured on the computer before you open the dialog).
    -	**Cryptographic Service Provider (CSP)** - a key store that is accessible via a cryptographic service provider (CSP) that implements the Cryptography API (CAPI). Typically, such a store is a hardware security module (HSM). After you select this option, you'll need to pick a CSP.  If you want to use a column master key stored in an HSM, select a CSP for your device (it must be installed and configured on the computer before you open the dialog).
    
    > [!NOTE]
    > Since CAPI is a deprecated API, the Cryptographic Service Provider (CAPI) option is disabled by default. You can enable by creating the CAPI Provider Enabled DWORD value under the **[HKEY_CURRENT_USER\Software\Microsoft\Microsoft SQL Server\sql13\Tools\Client\Always Encrypted]** key in Windows Registry, and setting it to 1. You should use CNG instead of CAPI, unless your key store does not support CNG.
   
    For more information about the above key stores, see [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md).

5. If you're using [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] and your SQL Server instance is configured with a secure enclave, you can select the **Allow enclave computations** checkbox to make the master key enclave-enabled. See [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) for details. 

    > [!NOTE]
    > The **Allow enclave computations** checkbox does not appear if your SQL Server instance is not correctly configured with a secure enclave.

6.	Pick an existing key in your key store, or click the **Generate Key** or **Generate Certificate** button, to create a key in the key store. 
7.	Click **OK** and the new key will show up in the list. 

Once you complete the dialog, SQL Server Management Studio creates metadata for your column master key in the database. The dialog achieves this by generating and issuing a [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md) statement.

::: moniker range=">=sql-server-ver15"

If you're configuring an enclave-enabled column master key, SSMS also signs the metadata using the column master key. 

::: moniker-end

### Permissions for provisioning a column master key

You need the *ALTER ANY COLUMN MASTER KEY* database permission in the database for the dialog to create a column master key. You also need key store permissions to access and use your key column master key. For detailed information on key store permissions required for key management operations, go to [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md) and find a section relevant for your key store.

<a name="provisioncek"></a> 
## Provision Column Encryption Keys with the New Column Encryption Key Dialog

The **New Column Encryption Key** dialog allows you to generate a column encryption key, encrypt it with a column master key, and create the column encryption key metadata in the database.

1.	Using **Object Explorer**, navigate to the **Security/Always Encrypted Keys** folder under your database.
2.	Right click on the **Column Encryption Keys** folder and select **New Column Encryption Key...**. 
3.	In the **New Column Encryption Key** dialog, enter the name of the column encryption key metadata object.
4.	Select a metadata object that represents your column master key in the database.
5.	Click **OK**. 

Once you complete the dialog, SQL Server Management Studio generates a new column encryption key and then it retrieves the metadata for the column master key you selected from the database. SSMS then uses the column master key metadata to contact the key store containing your column master key and encrypt the column encryption key. Finally, SSMS creates the metadata data for the new column encryption in the database by generating and issuing a [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md) statement.

> [!NOTE]
> Using column master keys stored in a [managed HSM](/azure/key-vault/managed-hsm/overview) in Azure Key Vault requires SSMS 18.9 or a later version.

### Permissions for provisioning a column encryption key

You need the *ALTER ANY COLUMN ENCRYPTION KEY* and *VIEW ANY COLUMN MASTER KEY DEFINITION* database permissions in the database for the dialog to create the column encryption key metadata and to access column master key metadata. You also need key store permissions to access and use your column master key. For detailed information on key store permissions required for key management operations, go to [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md) and find a section relevant for your key store.

## Provision Always Encrypted Keys using the Always Encrypted Wizard

The [Always Encrypted Wizard](always-encrypted-wizard.md) is a tool for encrypting, decrypting, and re-encrypting selected database columns. While it can use already configured keys, it also allows you to generate a new column master key and a new column encryption. 

## Next Steps
- [Configure column encryption using Always Encrypted Wizard](always-encrypted-wizard.md)
- [Configure column encryption using Always Encrypted with a DAC package](configure-always-encrypted-using-dacpac.md)
- [Rotate Always Encrypted keys using SQL Server Management Studio](rotate-always-encrypted-keys-using-ssms.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
- [Migrate data to or from columns using Always Encrypted with SQL Server Import and Export Wizard](always-encrypted-migrate-using-import-export-wizard.md)

## See Also
- [Always Encrypted](always-encrypted-database-engine.md)
- [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md)
- [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md)
- [Configure Always Encrypted using SQL Server Management Studio](configure-always-encrypted-using-sql-server-management-studio.md)
- [Provision Always Encrypted keys using PowerShell](configure-always-encrypted-keys-using-powershell.md)
- [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md)
- [DROP COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/drop-column-master-key-transact-sql.md)
- [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md)
- [ALTER COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/alter-column-encryption-key-transact-sql.md)
- [DROP COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/drop-column-encryption-key-transact-sql.md) 
- [sys.column_master_keys (Transact-SQL)](../../system-catalog-views/sys-column-master-keys-transact-sql.md)
- [sys.column_encryption_keys (Transact-SQL)](../../system-catalog-views/sys-column-encryption-keys-transact-sql.md)