---
title: "Always Encrypted Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "05/04/2016"
ms.prod: sql
ms.reviewer: vanto
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.alwaysencryptedwizard.encryption.f1"
  - "sql13.swb.alwaysencryptedwizard.f1"
  - "sql.swb.alwaysencryptedwizard.masterkey.f1"
helpviewer_keywords: 
  - "Wizard, Always Encrypted"
ms.assetid: 68daddc9-ce48-49aa-917f-6dec86ad5af5
author: aliceku
ms.author: aliceku
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Always Encrypted Wizard
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

Use the **Always Encrypted Wizard** to help protect sensitive data  stored in a SQL Server database. Always Encrypted allows clients to encrypt sensitive data inside client applications and never reveal the encryption keys to SQL Server. As a result, Always Encrypted provides a separation between those who own the data (and can view it) and those who manage the data (but should have no access).  For a full description of the feature, see [Always Encrypted &#40;Database Engine&#41;](../../../relational-databases/security/encryption/always-encrypted-database-engine.md).  
 
 - For an end-to-end walkthrough that shows how to configure Always Encrypted with the wizard and use it in a client application, see [SQL Database tutorial: Protect sensitive data with Always Encrypted](https://azure.microsoft.com/documentation/articles/sql-database-always-encrypted/).  
 
 - For a video that includes using the wizard, see [Keeping Sensitive Data Secure with Always Encrypted](https://channel9.msdn.com/events/DataDriven/SQLServer2016/AlwaysEncrypted). Also, see the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Security Team blog [SSMS Encryption Wizard - Enabling Always Encrypted in a Few Easy Steps](https://blogs.msdn.com/b/sqlsecurity/archive/2015/11/01/ssms-encryption-wizard-enabling-always-encrypted-made-easy.aspx).  
 
 - **Permissions:** To query encrypted columns and to select keys using this wizard you must have the `VIEW ANY COLUMN MASTER KEY DEFINITION` and `VIEW ANY COLUMN ENCRYPTION KEY DEFINITION` permissions. To create new keys, you must also have the `ALTER ANY COLUMN MASTER KEY` and `ALTER ANY COLUMN ENCRYPTION KEY` permissions.  
 
 #### To Open the Always Encrypted Wizard
 
 1.  Connect to your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with the Object Explorer component of [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].  
   
 2.  Right-click your database, point to **Tasks**, and then click **Encrypt Columns**.  
   
 ## Column Selection Page
 - Locate a table and column, and then select an encryption type  (deterministic or randomized) and an encryption key for selected columns. To decrypt an column that is currently encrypted, select **Plaintext**. To rotate a column encryption key, select different encryption key and the wizard will decrypt the column and re-encrypt the column with the new key. (Encrypting temporal and In-Memory tables is supported by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] but cannot be configured by this wizard.)  
 
## Master Key Configuration Page  
 - Create a new column master key in the Windows Certificate Store or Azure Key Vault. For more information, see the links below under Key Storage.  
 
 - If you chose an auto-generated column encryption key in the Column Selection page, you must configure a column master key that the generated column encryption key will be encrypted with. If you already have a column master key defined in your database, you can select it. (To use an existing column master key, the user must have permission to access the key.) Or, you can generate a column master key in a selected key store (Windows Certificate Store or Azure Key Vault) and define the key in the database.  
 
 ### **Key Storage**  
 
 - Choose where the column master key will be stored.  
 
   - **Storing a master key in Windows cert** For more information, see [Using Certificate Stores](/windows/desktop/SecCrypto/using-certificate-stores)  
 
   - **Storing a master key in AKV** For more information, see [Get Started with Azure Key Vault](https://azure.microsoft.com/documentation/articles/key-vault-get-started/).  
 
 - To generate a column master key in the Azure Key Vault, the user must have the **WrapKey**, **UnwrapKey**, **Verify**, and **Sign** permissions to the key vault. Users might also need the **Get**, **List**, **Create**, **Delete**, **Update**, **Import**, **Backup**, and **Restore** permissions. For more information, see [What is Azure Key Vault?](https://azure.microsoft.com/documentation/articles/key-vault-whatis/) and   [Set-AzKeyVaultAccessPolicy](https://msdn.microsoft.com/library/mt603625.aspx).  
 
 - The wizard only supported two options. Hardware Security Modules and customer stores must be configured using [CREATE COLUMN MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-column-master-key-transact-sql.md)[!INCLUDE[tsql](../../../includes/tsql-md.md)].  
 
 ## Always Encrypted Terms  
 
 - **Deterministic encryption** uses a method which always generates the same encrypted value for any given plain text value. Using deterministic encryption allows grouping, filtering by equality, and joining tables based on encrypted values, but can also allow unauthorized users to guess information about encrypted values by examining patterns in the encrypted column. This weakness is increased when there is a small set of possible encrypted values, such as True/False, or North/South/East/West region. Deterministic encryption must use a column collation with a binary2 sort order for character columns.  
 
 - **Randomized encryption** uses a method that encrypts data in a less predictable manner. Randomized encryption is more secure, but prevents equality searches, grouping, indexing, and joining on encrypted columns.  

 - **Column master keys** are protecting keys used to encrypt column encryption keys. Column master keys must be stored in a trusted key store. Information about column master keys, including their location, is stored in the database in system catalog views.  

 - **Column encryption keys** are used to encrypt sensitive data stored in database columns. All values in a column can be encrypted using a single column encryption key. Encrypted values of column encryption keys are stored in the database in system catalog views. You should store column encryption keys in a secure/trusted location for backup.  

 ## See Also  
 - [Always Encrypted &#40;Database Engine&#41;](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)   
 - [Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](../../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md)  
