---
title: "CREATE COLUMN MASTER KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/24/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SQL13.SWB.NEWCOLUMNMASTERKEYDEF.GENERAL.F1"
  - "SQL13.SWB.COLUMNMASTERKEYDEF.GENERAL.F1"
  - "CREATE COLUMN MASTER KEY"
  - "COLUMN MASTER KEY"
  - "CREATE_COLUMN_MASTER_KEY_TSQL"
  - "COLUMN_MASTER_KEY_TSQL"
  - "SQL13.SWB.NEWCOLUMNMASTERKEY.GENERAL.F1"
  - "SQL13.SWB.COLUMNMASTERKEY.GENERAL.F1"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "column master key definition"
  - "column master key, create"
  - "CREATE COLUMN MASTER KEY statement"
  - "Always Encrypted, create column master key"
ms.assetid: f8926b95-e146-4e3f-b56b-add0c0d0a30e
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE COLUMN MASTER KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Creates a column master key metadata object in a database. A column master key metadata entry represents a key, stored in an external key store, that protects (encrypt) column encryption keys when you're using the [Always Encrypted &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-database-engine.md) feature. Multiple column master keys allow for periodic key rotation to enhance security. You can create a column master key in a key store and its corresponding metadata object in the database by using the Object Explorer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or PowerShell. For details, see [Overview of Key Management for Always Encrypted](../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
 

> [!IMPORTANT]
> Creating enclave-enabled keys (with ENCLAVE_COMPUTATIONS) requires [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).

## Syntax  

```  
CREATE COLUMN MASTER KEY key_name   
    WITH (  
        KEY_STORE_PROVIDER_NAME = 'key_store_provider_name',  
        KEY_PATH = 'key_path'   
        [,ENCLAVE_COMPUTATIONS (SIGNATURE = signature)]
         )   
[;]  
```  
  
## Arguments  
*key_name*  
The name of the column master key in the database.  
  
*key_store_provider_name*  
Specifies the name of a key store provider. A key store provider is a client-side software component that holds a key store that has the column master key. A client driver, enabled with Always Encrypted, uses a key store provider name to look up a key store provider in the driver's registry of key store providers. The driver uses the provider to decrypt column encryption keys, protected by a column master key, stored in the underlying key store. A plaintext value of the column encryption key is then used to encrypt query parameters, corresponding to encrypted database columns, or to decrypt query results from encrypted columns.  
  
Always Encrypted-enabled client driver libraries include key store providers for popular key stores.   
  
A set of available providers depends on the type and the version of the client driver. See the Always Encrypted documentation for particular drivers:

[Develop Applications using Always Encrypted with the .NET Framework Provider for SQL Server](../../relational-databases/security/encryption/develop-using-always-encrypted-with-net-framework-data-provider.md)


The following table shows the names of system providers:  
  
|Key store provider name|Underlying key store|  
    |-----------------------------|--------------------------|
    |'MSSQL_CERTIFICATE_STORE'|Windows Certificate Store| 
    |'MSSQL_CSP_PROVIDER'|A store, such as a hardware security module (HSM), that supports Microsoft CryptoAPI.|
    |'MSSQL_CNG_STORE'|A store, such as a hardware security module (HSM), that supports Cryptography API: Next Generation.|  
    |'Azure_Key_Vault'|See [Getting Started with Azure Key Vault](https://azure.microsoft.com/documentation/articles/key-vault-get-started/)|  
  

You can implement a custom key store provider to store column master keys in a store for which there's no built-in key store provider in your Always Encrypted-enabled client driver. The names of custom key store providers can't start with 'MSSQL_', which is a prefix reserved for [!INCLUDE[msCoName](../../includes/msconame-md.md)] key store providers. 

  
key_path  
The path of the key in the column master key store. The key path must be valid in the context of each client application that's expected to encrypt or decrypt data stored in a column (indirectly) protected by the referenced column master key. The client application has to be able to access the key. The format of the key path is specific to the key store provider. The following list describes the format of key paths for particular Microsoft system key store providers.  
  
-   **Provider name:** MSSQL_CERTIFICATE_STORE  
  
    **Key path format:** *CertificateStoreName*/*CertificateStoreLocation*/*CertificateThumbprint*  
  
     Where:  
  
    *CertificateStoreLocation*  
    Certificate store location, which must be Current User or Local Machine. For more information, see [Local Machine and Current User Certificate Stores](https://msdn.microsoft.com/library/windows/hardware/ff548653.aspx).  
  
    *CertificateStore*  
    Certificate store name, for example 'My'.  
  
    *CertificateThumbprint*  
    Certificate thumbprint.  
  
    **Examples:**  
  
    ```  
    N'CurrentUser/My/BBF037EC4A133ADCA89FFAEC16CA5BFA8878FB94'  
  
    N'LocalMachine/My/CA5BFA8878FB94BBF037EC4A133ADCA89FFAEC16'  
    ```  
  
-   **Provider name:** MSSQL_CSP_PROVIDER  
  
    **Key path format:** *ProviderName*/*KeyIdentifier*  
  
    Where:  
  
    *ProviderName*  
    The name of a Cryptography Service Provider (CSP), which implements CAPI, for the column master key store. If you use an HSM as a key store, the provider name must be the name of the CSP your HSM vendor supplies. The provider must be installed on a client computer.  
  
    *KeyIdentifier*  
    Identifier of the key, used as a column master key, in the key store.  
  
    **Examples:**  
  
    ```  
    N'My HSM CSP Provider/AlwaysEncryptedKey1'  
    ```  
  
-   **Provider name:** MSSQL_CNG_STORE  
  
    **Key path format:** *ProviderName*/*KeyIdentifier*  
  
    Where:  
  
    *ProviderName*  
    Name of the Key Storage Provider (KSP), which implements the Cryptography: Next Generation (CNG) API, for the column master key store. If you use an HSM as a key store, provider name must be the name of the KSP your HSM vendor supplies. The provider must be installed on a client computer.  
  
    *KeyIdentifier*  
    Identifier of the key, used as a column master key, in the key store.  
  
    **Examples:**  
  
    ```  
    N'My HSM CNG Provider/AlwaysEncryptedKey1'  
    ```  

-   **Provider name:** AZURE_KEY_STORE  
  
    **Key path format:** *KeyUrl*  
  
    Where:  
  
    *KeyUrl*  
    The URL of the key in Azure Key Vault

ENCLAVE_COMPUTATIONS  
Specifies that the column master key is enclave-enabled. All column encryption keys encrypted with this column master key can be shared with a server-side secure enclave and used for computations inside the enclave. For more information, see [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).

*signature*  
A binary literal that's a result of digitally signing *key path* and the ENCLAVE_COMPUTATIONS setting with the column master key. The signature reflects whether ENCLAVE_COMPUTATIONS is specified or not. The signature protects the signed values from being altered by unauthorized users. An Always Encrypted-enabled client driver verifies the signature and returns an error to the application if the signature is invalid. The signature must be generated using client-side tools. For more information, see [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).
  
  
## Remarks  

You must create a column master key metadata entry before you can create a column encryption key metadata entry in the database and before any column in the database can be encrypted using Always Encrypted. A column master key entry in the metadata doesn't contain the actual column master key, which must be stored in an external column key store (outside of SQL Server). The key store provider name and the column master key path in the metadata must be valid for a client application to use the column master key to decrypt a column encryption key that's encrypted with the column master key, and to query encrypted columns.


  
## Permissions  
Requires  the **ALTER ANY COLUMN MASTER KEY** permission.  
  
## Examples  
  
### A. Creating a column master key  
The following example creates a column master key metadata entry for a column master key stored in Certificate Store, for client applications that use the MSSQL_CERTIFICATE_STORE provider to access the column master key:  
  
```  
CREATE COLUMN MASTER KEY MyCMK  
WITH (  
     KEY_STORE_PROVIDER_NAME = N'MSSQL_CERTIFICATE_STORE',   
     KEY_PATH = 'Current User/Personal/f2260f28d909d21c642a3d8e0b45a830e79a1420'  
   );  
```  
  
Create a column master key metadata entry for a column master key that's accessed by client applications, which use the MSSQL_CNG_STORE provider:  
  
```  
CREATE COLUMN MASTER KEY MyCMK  
WITH (  
    KEY_STORE_PROVIDER_NAME = N'MSSQL_CNG_STORE',    
    KEY_PATH = N'My HSM CNG Provider/AlwaysEncryptedKey'  
);  
```  
  
Create a column master key metadata entry for a column master key stored in the Azure Key Vault, for client applications that use the AZURE_KEY_VAULT provider, to access the column master key.  
  
```  
CREATE COLUMN MASTER KEY MyCMK  
WITH (  
    KEY_STORE_PROVIDER_NAME = N'AZURE_KEY_VAULT',  
    KEY_PATH = N'https://myvault.vault.azure.net:443/keys/  
        MyCMK/4c05f1a41b12488f9cba2ea964b6a700');  
```  
  
Create a column master key metadata entry for a column master key stored in a custom column master key store:  
  
```  
CREATE COLUMN MASTER KEY MyCMK  
WITH (  
    KEY_STORE_PROVIDER_NAME = 'CUSTOM_KEY_STORE',    
    KEY_PATH = 'https://contoso.vault/sales_db_tce_key'  
);  
```  
### B. Creating an enclave-enabled column master key  
The following example creates a column master key metadata entry for an enclave-enabled column master key that's stored in Certificate Store, for client applications, which use the MSSQL_CERTIFICATE_STORE provider to access the column master key:  
  
```  
CREATE COLUMN MASTER KEY MyCMK  
WITH (  
     KEY_STORE_PROVIDER_NAME = N'MSSQL_CERTIFICATE_STORE',   
     KEY_PATH = 'Current User/Personal/f2260f28d909d21c642a3d8e0b45a830e79a1420'  
     ENCLAVE_COMPUTATIONS (SIGNATURE = 0xA80F5B123F5E092FFBD6014FC2226D792746468C901D9404938E9F5A0972F38DADBC9FCBA94D9E740F3339754991B6CE26543DEB0D094D8A2FFE8B43F0C7061A1FFF65E30FDDF39A1B954F5BA206AAC3260B0657232020542419990261D878318CC38EF4E853970ED69A8D4A306693B8659AAC1C4E4109DE5EB148FD0E1FDBBC32F002C1D8199D313227AD689279D8DEEF91064DF122C19C3767C463723AB663A6F8412AE17E745922C0E3A257EAEF215532588ACCBD440A03C7BC100A38BD0609A119E1EF7C5C6F1B086C68AB8873DBC6487B270340E868F9203661AFF0492CEC436ABF7C4713CE64E38CF66C794B55636BFA55E5B6554AF570CF73F1BE1DBD)
  );  
```  
  
Create a column master key metadata entry for an enclave-enabled column master key stored in the Azure Key Vault, for client applications that use the AZURE_KEY_VAULT provider, to access the column master key.  
  
```  
CREATE COLUMN MASTER KEY MyCMK  
WITH (  
    KEY_STORE_PROVIDER_NAME = N'AZURE_KEY_VAULT',  
    KEY_PATH = N'https://myvault.vault.azure.net:443/keys/MyCMK/4c05f1a41b12488f9cba2ea964b6a700');
    ENCLAVE_COMPUTATIONS (SIGNATURE = 0xA80F5B123F5E092FFBD6014FC2226D792746468C901D9404938E9F5A0972F38DADBC9FCBA94D9E740F3339754991B6CE26543DEB0D094D8A2FFE8B43F0C7061A1FFF65E30FDDF39A1B954F5BA206AAC3260B0657232020582413990261D878318CC38EF4E853970ED69A8D4A306693B8659AAC1C4E4109DE5EB148FD0E1FDBBC32F002C1D8199D313227AD689279D8DEEF91064DF122C19C3767C463723AB663A6F8412AE17E745922C0E3A257EAEF215532588ACCBD440A03C7BC100A38BD0609A119E1EF7C5C6F1B086C68AB8873DBC6487B270340E868F9203661AFF0492CEC436ABF7C4713CE64E38CF66C794B55636BFA55E5B6554AF570CF73F1BE1DBD)
  );
```  
  
## See Also
 
* [DROP COLUMN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-column-master-key-transact-sql.md)   
* [CREATE COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-encryption-key-transact-sql.md)
* [sys.column_master_keys (Transact-SQL)](../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md)
* [Always Encrypted &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-database-engine.md)  
* [Overview of Key Management for Always Encrypted](../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md)
  
