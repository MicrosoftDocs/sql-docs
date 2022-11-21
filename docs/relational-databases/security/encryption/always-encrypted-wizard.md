---
title: "Configure column encryption using Always Encrypted Wizard | Microsoft Docs"
description: Learn how to set the Always Encrypted configuration for database columns by using the Always Encrypted Wizard in SQL Server.
ms.custom: ""
ms.date: "10/30/2019"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.alwaysencryptedwizard.encryption.f1"
  - "sql13.swb.alwaysencryptedwizard.f1"
  - "sql13.swb.alwaysencryptedwizard.masterkey.f1"
  
helpviewer_keywords: 
  - "Wizard, Always Encrypted"
ms.assetid: 68daddc9-ce48-49aa-917f-6dec86ad5af5
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configure column encryption using Always Encrypted Wizard
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The Always Encrypted Wizard is a powerful tool that allows you to set the desired [Always Encrypted](always-encrypted-database-engine.md) configuration for selected database columns. Depending on the current configuration and the desired target configuration, the wizard can encrypt a column, decrypt it (remove encryption), or re-encrypt it (for example, using a new column encryption key or an encryption type that is different from the current type, configured for the column). Multiple columns can be configured in a single run of the wizard.

The wizard allows you to encrypt columns with existing column encryption keys, or you can choose to generate a new column encryption key or both a new column encryption key and a new column master key. 

The wizard works by moving data out of the database and performing cryptographic operations within the SSMS process. The wizard creates a new table (or tables) with the desired encryption configuration in the database, loads all data from the original tables, performs the requested cryptographic operations, uploads the data to the new table(s), and then swaps the original table(s) with the new table(s).

> [!NOTE]
> Running cryptographic operations can take a long time. During that time, your database is not available to write transactions. PowerShell is a recommended tool for cryptographic operations on larger tables. See [Configure column encryption using Always Encrypted with PowerShell](configure-column-encryption-using-powershell.md).

::: moniker range=">=sql-server-ver15"

> [!NOTE]
> If you are using [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] and your SQL Server instance is configured with a secure enclave, you can run cryptographic operations in-place, without moving data out of the database. See [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md). Note that the wizard does not support in-place encryption.

::: moniker-end

Use PowerShell is a recommended 

 - For an end-to-end walk-through that shows how to configure Always Encrypted with the wizard and use it in a client application, see the following Azure SQL Database tutorials:
    - [Protect sensitive data in Azure SQL Database with Always Encrypted and column master keys in Windows certificate store](/azure/azure-sql/database/always-encrypted-certificate-store-configure)
    - [Protect sensitive data in Azure SQL Database with Always Encrypted and column master keys in Azure Key Vault](/azure/sql-database/sql-database-always-encrypted-azure-key-vault)

 - For a video that includes using the wizard, see [Keeping Sensitive Data Secure with Always Encrypted](https://channel9.msdn.com/events/DataDriven-SQLServer2016/AlwaysEncrypted). Also, see the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Security Team blog [SSMS Encryption Wizard - Enabling Always Encrypted in a Few Easy Steps](https://techcommunity.microsoft.com/t5/SQL-Server/SSMS-Encryption-Wizard-Enabling-Always-Encrypted-in-a-Few-Easy/ba-p/384545).  
 - For information about Always Encrypted keys, see [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md).
 - For information about encryption types supported in Always Encrypted, see [Selecting Deterministic or Randomized Encryption](always-encrypted-database-engine.md#selecting--deterministic-or-randomized-encryption).
 
 ## Permissions
To perform cryptographic operations using the wizard, you must have the **VIEW ANY COLUMN MASTER KEY DEFINITION** and **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** permissions. You also need key store permissions to create, access and use your column master key. For detailed information on key store permissions, go to [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md) and find a section relevant for your key store.

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
> The wizard does not support cryptographic operations on temporal and in-memory tables. You can create empty temporal or in-memory tables using Transact-SQL and insert data using your application.

## Master Key Configuration Page
If you have selected an autogenerated column encryption key for any column on the previous page, in this page you need to either select an existing column master key or configure a new column master key that will encrypt the column encryption key. 

When configuring a new column master key, you can either pick an existing key in Windows Certificate Store or in Azure Key Vault and have the wizard to create just a metadata object for the key in the database, or you can choose to generate both the key and the metadata object describing the key in the database. 

For more information about creating and storing column master keys in Windows Certificate Store, Azure Key Vault or other key stores, see [Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md).

> [!TIP]
> The wizard allows you to browse and create keys only in Windows Certificate Store and Azure Key Vault. It also auto-generates the names of both the new keys and the database metadata objects describing the keys. If you need more control for how your keys are provisioned (and more choices for a key store containing your column master key), you can use the **New Column Master Key** and **New Column Encryption Key** dialogs to create the keys first, and then run the wizard and pick the keys you have created. See [Provision Column Master Keys with the New Column Master Key Dialog](configure-always-encrypted-keys-using-ssms.md#provision-column-master-keys-with-the-new-column-master-key-dialog) and [Provision Column Encryption Keys with the New Column Encryption Key Dialog](configure-always-encrypted-keys-using-ssms.md#provision-column-encryption-keys-with-the-new-column-encryption-key-dialog). 

## Next Steps
- [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)

## See Also  
 - [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
 - [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md) 
 - [Configure Always Encrypted using SQL Server Management Studio](configure-always-encrypted-using-sql-server-management-studio.md)
 - [Provision Always Encrypted keys using PowerShell](configure-always-encrypted-keys-using-powershell.md)
 - [Configure column encryption using Always Encrypted with PowerShell](configure-column-encryption-using-powershell.md)
 - [Configure column encryption using Always Encrypted with a DAC package](configure-always-encrypted-using-dacpac.md)