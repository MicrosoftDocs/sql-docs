---
title: "Overview of Key Management for Always Encrypted | Microsoft Docs"
ms.custom: ""
ms.date: "07/20/2016"
ms.prod: sql
ms.prod_service: security, sql-database"
ms.reviewer: vanto
ms.technology: security
ms.topic: conceptual
ms.assetid: 07a305b1-4110-42f0-b7aa-28a4e32e912a
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Overview of Key Management for Always Encrypted
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]


[Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md) uses two types of cryptographic keys to protect your data - one key to encrypt your data, and another key to encrypt the key that encrypts your data. The column encryption key encrypts your data, the column master key encrypts the column encryption key. This article provides a detailed overview for managing these encryption keys.

When discussing Always Encrypted keys and key management it is important to understand the distinction between the actual cryptographic keys, and the metadata objects that *describe* the keys. We use the terms **column encryption key** and **column master key** to refer to the actual cryptographic keys, and we use **column encryption key metadata** and **column master key metadata** to refer to the Always Encrypted key *descriptions* in the database.

- ***Column encryption keys*** are content-encryption keys used to encrypt data. As the name implies, you use column encryption keys to encrypt data in database columns. You can encrypt 1 or more columns with the same column encryption key, or you can use multiple column encryption keys depending on your application requirements. The column encryption keys are themselves encrypted, and only the encrypted values of the column encryption keys are stored in the database (as part of the column encryption key metadata). The column encryption key metadata is stored in the [sys.column_encryption_keys (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md) and [sys.column_encryption_key_values (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-column-encryption-key-values-transact-sql.md) catalog views. Column encryption keys used with the AES-256 algorithm are 256-bit long.


- ***Column master keys*** are key-protecting keys used to encrypt column encryption keys. Column master keys must be stored in a trusted key store, such as Windows Certificate Store, Azure Key Vault, or a hardware security module. The database only contains metadata about column master keys (the type of key store and location). The column master key metadata is stored in the [sys.column_master_keys (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md) catalog view.  

It is important to note that the key metadata in the database system does not contain plaintext column master keys or plaintext column encryption keys. The database only contains information about the type and location of column master keys, and encrypted values of column encryption keys. This means that plaintext keys are never exposed to the database system ensuring that data protected using Always Encrypted is safe, even if the database system gets compromised. To ensure the database system cannot gain access to the plaintext keys, be sure to run your key management tools on a different machine than the one hosting your database - review the [Security Considerations for Key Management](#SecurityForKeyManagement) section below for details.

Because the database only contains encrypted data (in Always Encrypted protected columns), and cannot access the plaintext keys, it cannot decrypt the data. This means that querying Always Encrypted columns will simply return encrypted values, so client applications that need to encrypt or decrypt protected data must be able to access the column master key, and related column encryption keys. For details, see [Always Encrypted (client development)](../../../relational-databases/security/encryption/always-encrypted-client-development.md).



## Key Management Tasks

The process of managing keys can be divided into the following high-level tasks:

- **Key provisioning** - Creating the physical keys in a trusted key store (for example, in the Windows Certificate Store, Azure Key Vault, or a hardware security module), encrypting column encryption keys with column master keys, and creating metadata for both types of keys in the database.

- **Key rotation** - Periodically replacing an existing key with a new key. You may need to rotate a key if the key has been compromised, or in order to comply with your organization's policies or compliance regulations that mandate cryptographic keys must be rotated. 


## <a name="KeyManagementRoles"></a> Key Management Roles

There are two distinct roles of users who manage Always Encrypted keys; Security Administrators and Database Administrators (DBAs):

- **Security Administrator** - generates column encryption keys and column master keys and manages key stores containing the column master keys. To perform these tasks, a Security Administrator needs to be able to access the keys and the key store, but does not need access to the database.
- **DBA** - manages metadata about the keys in the database. To perform key management tasks, a DBA needs to be able to manage key metadata in the database, but does not need access to the keys or the key store holding the column master keys.

Considering the above roles, there are two different ways to perform key management tasks for Always Encrypted; *with role separation*, and *without role separation*. Depending on the needs of your organization you can select the key management process that best suits your requirements.

## Managing Keys with Role Separation
When Always Encrypted keys are managed with role separation, different people in an organization assume the Security Administrator and DBA roles. A key management process with role separation ensures DBAs have no access to the keys or key stores holding the actual keys, and Security Administrators have no access to the database containing sensitive data. Managing keys with role separation is recommended if your goal is to ensure DBAs in your organization cannot access sensitive data. 

**Note:** Security Administrators generate and work with the plaintext keys, so they should never perform their tasks on the same computers hosting a database system, or computers that can be accessed by DBAs or anyone else who might be potential adversaries. 

## Managing Keys without Role Separation
When Always Encrypted keys are managed without role separation, a single person can assume both Security Administrator and DBA roles, which implies that person needs to be able to access and manage both the keys/key stores and the key metadata. Managing keys without role separation can be recommended for organizations using the DevOps model, or if the database is hosted in the cloud and the primary goal is to restrict cloud administrators (but not on-premises DBAs), from accessing sensitive data.



## Tools for Managing Always Encrypted Keys

Always Encrypted keys can be managed using [SQL Server Management Studio (SSMS)](https://msdn.microsoft.com/library/ms174173.aspx) and [PowerShell](../../scripting/sql-server-powershell.md):

- **SQL Server Management Studio (SSMS)** - provides dialogs and wizards that combine tasks involving key store access and database access, so SSMS does not support role separation, but it makes configuring your keys easy. For more information about managing keys using SSMS, see:
    - [Provisioning Column Master Keys](../../../relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio.md#provisioncmk)
    - [Provisioning Column Encryption Keys](../../../relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio.md#provisioncek)
    - [Rotating Column Master Keys](../../../relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio.md#rotatecmk)
    - [Rotating Column Encryption Keys](../../../relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio.md#rotatecek)


- **SQL Server PowerShell** - includes cmdlets for managing Always Encrypted keys with and without role separation. For more information, see:
    - [Configure Always Encrypted Keys using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-keys-using-powershell.md)
    - [Rotate Always Encrypted Keys using PowerShell](../../../relational-databases/security/encryption/rotate-always-encrypted-keys-using-powershell.md)


## <a name="SecurityForKeyManagement"></a> Security Considerations for Key Management

The primary objective of Always Encrypted is to ensure sensitive data stored in a database is safe, even if the database system or its hosting environment gets compromised. Examples of security attacks where Always Encrypted can help prevent sensitive data leaks include:

- A malicious high-privilege database user, such as a DBA, querying sensitive data columns.
- A rogue administrator of a computer hosting a SQL Server instance, scanning memory of a SQL Server process, or SQL Server process dump files.
- A malicious data center operator querying a customer database, examining SQL Server dump files, or examining the memory of a computer hosting customer data in the cloud.
- Malware running on a computer hosting the database.

To ensure Always Encrypted is effective in preventing these types of attacks, your key management process must ensure the column master keys and column encryption keys, as well as credentials to a key store containing the column master keys, are never revealed to a potential attacker. Here are a few guidelines, you should follow:

- Never generate column master keys or column encryption keys on a computer hosting your database. Instead generate the keys on a separate computer, which is either dedicated for key management, or is a machine hosting applications that will need access to the keys anyway. This means that **you should never run tools used to generate the keys on the computer hosting your database** because if an attacker accesses a computer used to provision or maintain your Always Encrypted keys, the attacker can potentially get your keys, even if the keys only appear in the tool's memory for a short time.
- To ensure your key management process does not inadvertently reveal column master keys or column encryption keys, it is critical to identify potential adversaries and security threats before defining and implementing a key management process. For example, if your goal is to ensure DBAs have no access to sensitive data, then a DBA cannot be responsible for generating the keys. A DBA, however, *can* manage key metadata in the database, as the metadata does not contain the plaintext keys.

## Next Steps

- [Create and Store Column Master Keys (Always Encrypted)](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md)
- [Configure Always Encrypted Keys using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-keys-using-powershell.md)
- [Rotate Always Encrypted Keys using PowerShell](../../../relational-databases/security/encryption/rotate-always-encrypted-keys-using-powershell.md)
- [Configure Always Encrypted using SQL Server Management Studio](../../../relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio.md)

## Additional Resources

- [Always Encrypted (Database Engine)](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [Always Encrypted (Client Development)](../../../relational-databases/security/encryption/always-encrypted-client-development.md)
- [Always Encrypted Wizard tutorial (Azure Key Vault)](https://azure.microsoft.com/documentation/articles/sql-database-always-encrypted-azure-key-vault/)
- [Always Encrypted Wizard tutorial (Windows Certificate Store)](https://azure.microsoft.com/documentation/articles/sql-database-always-encrypted/)




