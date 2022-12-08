---
title: "Rotate Always Encrypted keys using SQL Server Management Studio | Microsoft Docs"
description: Learn about rotating Always Encrypted column master keys and column encryption keys with SQL Server Management Studio.
ms.custom: ""
ms.date: 10/01/2019
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
f1_keywords: 
  - "SQL13.SWB.COLUMNMASTERKEY.ROTATION.F1"
  - "sql13.SWB.COLUMNMASTERKEY.CLEANUP.F1"
helpviewer_keywords: 
  - "Always Encrypted, configure with SSMS"
ms.assetid: 29816a41-f105-4414-8be1-070675d62e84
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Rotate Always Encrypted keys using SQL Server Management Studio
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes tasks for rotating Always Encrypted column master keys and column encryption keys with [SQL Server Management Studio (SSMS)](../../../ssms/download-sql-server-management-studio-ssms.md).

For an overview of Always Encrypted key management, including best practice recommendations and important security considerations, see [Overview of key management for Always Encrypted](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md).

> [!NOTE]
> Using column master keys stored in a [managed HSM](/azure/key-vault/managed-hsm/overview) in Azure Key Vault requires SSMS 18.9 or a later version.

<a name="rotatecmk"></a>
## Rotate Column Master Keys 

The rotation of a column master key is the process of replacing an existing column master key with a new column master key. You may need to rotate a key if it has been compromised, or in order to comply with your organization's policies or compliance regulations that mandate cryptographic keys must be rotated on a regular basis. A column master key rotation involves decrypting column encryption keys that are protected with the current column master key, re-encrypting them using the new column master key, and updating the key metadata. 

### Step 1: Provision a new column master key

Follow the steps in [Provision Column Master Keys with the New Column Master Key Dialog](configure-always-encrypted-keys-using-ssms.md#provision-column-master-keys-with-the-new-column-master-key-dialog).

### Step 2: Encrypt column encryption keys with the new column master key

A column master key typically protects one or more column encryption keys. Each column encryption key has an encrypted value stored in the database, that is the product of encrypting the column encryption key with the column master key.
In this step, encrypt each of the column encryption keys that are protected with the column master key you're rotating, with the new column master key, and store the new encrypted value in the database. As a result, each column encryption key that is affected by the rotation will have two encrypted values: one value encrypted with the existing column master key, and a new value encrypted with the new column master key.

1.	Using **Object Explorer**, navigate to the **Security>Always Encrypted Keys>Column Master Keys** folder and locate the column master key you're rotating.
2.	Right-click on the column master key and select **Rotate**.
3.	In the **Column Master Key Rotation** dialog, select the name of your new column master key, you created in Step 1, in the **Target** field.
4.	Review the list of column encryption keys, protected by the existing column master keys. These keys will be affected by the rotation.
5.	Click **OK**.

SQL Server Management Studio will obtain the metadata of the column encryption keys that are protected with the old column master key, and the metadata of the old and the new column master keys. Then, SSMS will use the column master key metadata to access the key store containing the old column master key and decrypt the column encryption key(s). Subsequently, SSMS will access the key store holding the new column master key to produce a new set of encrypted values of the column encryption keys, and then it will add the new values to the metadata (generating and issuing [ALTER COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/alter-column-encryption-key-transact-sql.md) statements).

> [!NOTE]
> Make sure each of the column encryption keys, encrypted with the old column master key, is not encrypted with any other column master key. In other words, each column encryption key, impacted by the rotation, must have exactly one encrypted value in the database. If any affected column encryption key has more than one encrypted value, you need to remove the value before you can proceed with the rotation (see *Step 4* on how to remove an encrypted value of a column encryption key).

### Step 3: Configure your applications with the new column master key

In this step, you need to make sure that all your client applications that query database columns protected with the column master key that you are rotating can access the new column master key  (that is, database columns encrypted with a column encryption key that is encrypted with the column master key, being rotated). This step depends on the type of key store your new column master key is in. For example:

- If the new column master key is a certificate stored in Windows Certificate Store, you need to deploy the certificate to the same certificate store location (*Current User* or *Local computer*) as the location specified in the key path of your column master key in the database. The application needs to be able to access the certificate:
  - If the certificate is stored in the *Current User* certificate store location, the certificate needs to be imported into the Current User store of the application's Windows identity (user).
  - If the certificate is stored in the *Local computer* certificate store location, the application's Windows identity must have permission to access the certificate.
- If the new column master key is stored in Microsoft Azure Key Vault, the application must be implemented so that it can authenticate to Azure and has permission to access the key.

For details, see [Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md).

> [!NOTE]
> At this point in the rotation, both the old column master key and the new column master key are valid and can be used to access the data.

### Step 4: Clean up column encryption key values encrypted with the old column master key

Once you have configured all your applications to use the new column master key, remove the values of column encryption keys that are encrypted with the *old* column master key from the database. Removing old values will ensure you're ready for the next rotation (remember, each column encryption key, protected with a column master key to be rotated, must have exactly one encrypted value).

Another reason to clean up the old value before archiving or removing the old column master key, is performance-related: when querying an encrypted column, an Always Encrypted-enabled client driver might need to attempt to decrypt two values: the old value and the new one. The driver doesn't know which of the two column master keys is valid in the application's environment so the driver will retrieve both encrypted values from the server. If decrypting one of the values fails, because it's protected with the column master key is that not available (for example, the old column master key that has been removed from the store), the driver will attempt to decrypt another value using the new column master key.

> [!WARNING]
> If you remove the value of a column encryption key before its corresponding column master key has been made available to an application, the application will no longer be able to decrypt the database column.

1.	Using **Object Explorer**, navigate to the **Security>Always Encrypted Keys** folder and locate the existing column master key you want to replace.
2.	Right-click on your existing column master key and select **Cleanup**.
3.	Review the list of column encryption key values to be removed.
4.	Click **OK**.

SQL Server Management Studio will issue [ALTER COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/alter-column-encryption-key-transact-sql.md) statements to drop encrypted values of column encryption keys that are encrypted with the old column master key.

### Step 5: Delete metadata for your old column master key

If you choose to remove the definition of the old column master key from the database, use the below steps.

1. Using **Object Explorer**, navigate to the **Security>Always Encrypted Keys>Column Master Keys** folder and locate the old column master key to be removed from the database.
2. Right-click on the old column master key and select **Delete**. (This will generate and issue a [DROP COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/drop-column-master-key-transact-sql.md) statement to remove the column master key metadata.)
3. Click **OK**.

> [!NOTE]
> It is highly recommended you do not permanently delete the old column master key after the rotation. Instead, you should keep the old column master key in its current key store or archive it in another secure place. If you restore your database from a backup file to a point in time before the new column master key was configured, you will need the old key to access the data.

### Permissions for rotating column master key

Rotating a column master key requires the following database permissions:

- **ALTER ANY COLUMN MASTER KEY** - required to create metadata for the new column master key and deleting the metadata for the old column master key.
- **ALTER ANY COLUMN ENCRYPTION KEY** - required to modify column encryption key metadata (add new encrypted values).

You also need key store permissions to be able to access both the old column master key and the new column master key in their key stores. For detailed information on key store permissions required for key management operations, go to [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md) and find a section relevant for your key store.

<a name="rotatecek"></a> 
## Rotate Column Encryption Keys

Rotating a column encryption key involves decrypting the data in all columns that are encrypted with the key to be rotated out, and re-encrypting the data using the new column encryption key.

>[!NOTE]
> Rotating a column encryption key can take a very long time if the tables containing columns encrypted with the key being rotated are large. While the data is being re-encrypted, your applications cannot write to the impacted tables. Therefore, your organization needs to plan a column encryption key rotation very carefully.
To rotate a column encryption key, use the Always Encrypted Wizard.

1.	Open the wizard for your database: right-click your database, point to **Tasks**, and then click **Encrypt Columns**.
2.	Review the **Introduction** page, and then click **Next**.
3.	On the **Column Selection** page, expand the tables and locate all columns you want to replace that are currently encrypted with the old column encryption key.
4.	For each column encrypted with the old column encryption key, set **Encryption Key** to a new autogenerated key. **Note:** Alternatively, you can create a new column encryption key before running the wizard - see [Provision Column Encryption Keys with the New Column Encryption Key Dialog](configure-always-encrypted-keys-using-ssms.md#provision-column-encryption-keys-with-the-new-column-encryption-key-dialog).
5.	On the **Master Key Configuration** page, select a location to store the new key, and select a master key source, and then click **Next**. **Note:** If you're using an existing column encryption key (not an autogenerated one), there's no action to perform on this page.
6.	On the **Validation page**, choose whether to run the script immediately or create a PowerShell script, and then click **Next**.
7.	On the **Summary** page, review the options you've selected, and then click **Finish** and close the wizard when completed.
8.	Using **Object Explorer**, navigate to the **Security/Always Encrypted Keys/Column Encryption Keys** folder and locate your old column encryption key, to be removed from the database. Right-click on the key and select **Delete**.

### Permissions for rotating column encryption keys

Rotating a column encryption key requires the following database permissions:
**ALTER ANY COLUMN MASTER KEY** - required if you use a new autogenerated column encryption key (a new column master key and its new metadata will also be generated).
**ALTER ANY COLUMN ENCRYPTION KEY** -required to add metadata for the new column encryption key.

You also need key store permissions to be able to access column master keys for both the new and the old column encryption key. For detailed information on key store permissions required for key management operations, go to [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md) and find a section relevant for your key store.

## Next Steps
- [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)

## See Also
- [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md) 
- [Configure Always Encrypted using SQL Server Management Studio](configure-always-encrypted-using-sql-server-management-studio.md)
- [Configure Always Encrypted using PowerShell](configure-always-encrypted-using-powershell.md)
- [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md)
- [DROP COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/drop-column-master-key-transact-sql.md)
- [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md)
- [ALTER COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/alter-column-encryption-key-transact-sql.md)
- [DROP COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/drop-column-encryption-key-transact-sql.md) 
- [sys.column_master_keys (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md)
- [sys.column_encryption_keys (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md)