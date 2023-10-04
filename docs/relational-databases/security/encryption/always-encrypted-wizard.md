---
title: "Configure column encryption using Always Encrypted Wizard"
description: Learn how to set the Always Encrypted configuration for database columns by using the Always Encrypted Wizard in SQL Server.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: "10/30/2019"
ms.service: sql
ms.subservice: security
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.alwaysencryptedwizard.f1"
helpviewer_keywords:
  - "Wizard, Always Encrypted"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Configure column encryption using Always Encrypted Wizard

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The Always Encrypted Wizard is a powerful tool that allows you to set the desired [Always Encrypted](always-encrypted-database-engine.md) configuration for selected database columns. Depending on the current configuration and the desired target configuration, the wizard can encrypt a column, decrypt it (remove encryption), or re-encrypt it (for example, using a new column encryption key or an encryption type that is different from the current type, configured for the column). Multiple columns can be configured in a single run of the wizard.

The wizard allows you to encrypt columns with existing column encryption keys, or you can choose to generate a new column encryption key or both a new column encryption key and a new column master key. 

When your database is configured with a secure enclave, you can run cryptographic operations in-place, without moving data out of the database. The wizard removes all dependencies blocking the schema change of the column to be encrypted. It issues an in-place encryption for each column by using the enclave within the database engine. When the encryption is finished, the wizard recreates the dependencies. For more information about Always Encrypted with secure enclaves , see [Always Encrypted with secure enclaves](always-encrypted-enclaves.md).

When your database is *not* configured with a secure enclave or you are *not* using enclave-enabled keys, the wizard works by moving data out of the database and performing cryptographic operations within the SSMS process. The wizard creates a new table (or tables) with the desired encryption configuration in the database, loads all data from the original tables, performs the requested cryptographic operations, uploads the data to the new table(s), and then swaps the original table(s) with the new table(s).

> [!TIP]
> Using in-place encryption using Always Encrypted with secure enclaves, if available in your environment, may substantially reduce the time and the reliability of cryptographic operations. 

> [!NOTE]
> Running cryptographic operations can take a long time. During that time, your database is not available to write transactions. PowerShell is a recommended tool for cryptographic operations on larger tables. See [Configure column encryption using Always Encrypted with PowerShell](configure-column-encryption-using-powershell.md) or [Configure column encryption in-place with PowerShell](always-encrypted-enclaves-configure-encryption-powershell.md).

 - For an end-to-end walk-through that shows how to configure Always Encrypted with the wizard and use it in a client application, see the following Azure SQL Database tutorials:
    - [Protect sensitive data in Azure SQL Database with Always Encrypted and column master keys in Windows certificate store](/azure/azure-sql/database/always-encrypted-certificate-store-configure)
    - [Protect sensitive data in Azure SQL Database with Always Encrypted and column master keys in Azure Key Vault](/azure/sql-database/sql-database-always-encrypted-azure-key-vault)

 - For information about Always Encrypted keys, see [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md).
 - For information about encryption types supported in Always Encrypted, see [Selecting Deterministic or Randomized Encryption](always-encrypted-database-engine.md#selecting--deterministic-or-randomized-encryption).
 
 ## Permissions
To perform cryptographic operations using the wizard, you must have the **VIEW ANY COLUMN MASTER KEY DEFINITION** and **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** permissions. You also need key store permissions to create, access and use your column master key. For detailed information on key store permissions, go to [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md) or  and find a section relevant for your key store.

## Open the Always Encrypted Wizard
You can launch the wizard at three different levels: 
- At a database level - if you want to encrypt multiple columns located in different tables.
- At a table level - if you want to encrypt multiple columns located in the same table.
- At a column level - if you want to encrypt one specific column.
 
 1. Connect to your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with the Object Explorer component of [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].  
   
 2. To encrypt:
     1. Multiple columns located in different table in a database, right-click your database, point to **Tasks**, and then select **Encrypt Columns**.
     1. Multiple columns located in the same table, navigate to the table, right-click on it, and then select **Encrypt Columns**.
     1. An individual column, navigate to the column, right-click on it, and then select **Encrypt Columns**.
   
 ## Column Selection Page
In this page, you select columns you want to encrypt, re-encrypt, or decrypt, and you define the target encryption configuration for the selected columns.

To encrypt a plaintext column (a column that isn't encrypted), select an encryption type (**Deterministic** or **Randomized**) and an encryption key for the column.

To change an encryption type or to rotate (change) a column encryption key for an already encrypted column, select the desired encryption type and the key. 

If you want the wizard to encrypt or re-encrypt one or more columns using a new column encryption key, pick a key containing **(New)** in its name. The wizard will generate the key.

To decrypt a column that is currently encrypted, select **Plaintext** for the encryption type.

> [!NOTE]
> If you want to leverage in-place encryption and you're using existing keys, make sure you select enclave-enabled keys – annotated with **(enclave-enabled)**. 

> [!NOTE]
> The wizard does not support cryptographic operations on temporal and in-memory tables. You can create empty temporal or in-memory tables using Transact-SQL and insert data using your application.

## Master Key Configuration Page
If you have selected an autogenerated column encryption key for any column on the previous page, in this page you need to either select an existing column master key or configure a new column master key that will encrypt the column encryption key. 

When configuring a new column master key, you can either pick an existing key in Windows Certificate Store or in Azure Key Vault and have the wizard to create just a metadata object for the key in the database, or you can choose to generate both the key and the metadata object describing the key in the database. 

To use in-place encryption, make sure you select **Allow enclave computations** for a new column master key. Selecting this checkbox is allowed only if your database is configured with a secure enclave.

For more information about creating and storing column master keys in Windows Certificate Store, Azure Key Vault or other key stores, see [Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md) or [Manage keys for Always Encrypted with secure enclaves](../../../relational-databases/security/encryption/always-encrypted-enclaves-manage-keys.md).

> [!TIP]
> The wizard allows you to browse and create keys only in Windows Certificate Store and Azure Key Vault. It also auto-generates the names of both the new keys and the database metadata objects describing the keys. If you need more control for how your keys are provisioned (and more choices for a key store containing your column master key), you can use the **New Column Master Key** and **New Column Encryption Key** dialogs to create the keys first, and then run the wizard and pick the keys you have created. See [Provision Column Master Keys with the New Column Master Key Dialog](configure-always-encrypted-keys-using-ssms.md#provision-column-master-keys-with-the-new-column-master-key-dialog) or [Provision enclave-enabled keys](always-encrypted-enclaves-provision-keys.md) and [Provision Column Encryption Keys with the New Column Encryption Key Dialog](configure-always-encrypted-keys-using-ssms.md#provision-column-encryption-keys-with-the-new-column-encryption-key-dialog). 

## In-Place Encryption Settings Page
If you have configured a secure enclave in your database and you're using enclave-enabled keys, this page allows you to specify the enclave attestation parameters, required for in-place encryption. If you don't want to use in-place encryption, unselect **Use in-place encryption for eligible columns** to proceed with client-side encryption. We recommend you to leave this checkbox enabled so that the wizard can use in-place encryption.

For more information about enclave attestation, see [Configure attestation for Always Encrypted using Azure Attestation](/azure/azure-sql/database/always-encrypted-enclaves-configure-attestation) 

## Next steps
- [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md)
- [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](../../../connect/ado-net/sql/tutorial-always-encrypted-enclaves-develop-net-apps.md)

## See also  
 - [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
 - [Always Encrypted with secure enclaves](../../../relational-databases/security/encryption/always-encrypted-enclaves.md)
 - [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md) 
 - [Configure Always Encrypted using SQL Server Management Studio](configure-always-encrypted-using-sql-server-management-studio.md)
 - [Configure and use Always Encrypted with secure enclaves](configure-always-encrypted-enclaves.md)
 - [Provision Always Encrypted keys using PowerShell](configure-always-encrypted-keys-using-powershell.md)
 - [Provision enclave-enabled keys using PowerShell](always-encrypted-enclaves-provision-keys.md) 
 - [Configure column encryption using Always Encrypted with PowerShell](configure-column-encryption-using-powershell.md)
 - [Configure column encryption in-place with PowerShell](always-encrypted-enclaves-configure-encryption-powershell.md)
 - [Configure column encryption using Always Encrypted with a DAC package](configure-always-encrypted-using-dacpac.md)
 - [Configure column encryption in-place with DAC package](always-encrypted-enclaves-configure-encryption-dacpac.md)
 - [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md)
