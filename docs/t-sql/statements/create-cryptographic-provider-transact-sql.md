---
title: "CREATE CRYPTOGRAPHIC PROVIDER (Transact-SQL)"
description: CREATE CRYPTOGRAPHIC PROVIDER (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CREATE_CRYPTOGRAPHIC_TSQL"
  - "CRYPTOGRAPHIC PROVIDER"
  - "PROVIDER_TSQL"
  - "CREATE CRYPTOGRAPHIC"
  - "CREATE CRYPTOGRAPHIC PROVIDER"
  - "CRYPTOGRAPHIC_PROVIDER_TSQL"
  - "CREATE_CRYPTOGRAPHIC_PROVIDER_TSQL"
  - "PROVIDER"
helpviewer_keywords:
  - "33085 (Database Engine error)"
  - "CREATE CRYPTOGRAPHIC PROVIDER statement"
  - "33032 (Database Engine error)"
dev_langs:
  - "TSQL"
---
# CREATE CRYPTOGRAPHIC PROVIDER (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates a cryptographic provider within [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from an Extensible Key Management (EKM) provider.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
CREATE CRYPTOGRAPHIC PROVIDER provider_name   
    FROM FILE = path_of_DLL  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *provider_name*  
 Is the name of the Extensible Key Management provider.  
  
 *path_of_DLL*  
 Is the path of the .dll file that implements the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Extensible Key Management interface. When using the **SQL Server Connector for Microsoft Azure Key Vault** the default location is **'C:\Program Files\SQL Server Connector for Microsoft Azure Key Vault\Microsoft.AzureKeyVaultService.EKM.dll'**.  
  
## Remarks  
 All keys created by a provider will reference the provider by its GUID. The GUID is retained across all versions of the DLL.  
  
 The DLL that implements SQLEKM interface must be digitally signed by using any certificate. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will verify the signature. This includes its certificate chain, which must have its root installed at the **Trusted Root Cert Authorities** location on a Windows system. If the signature is not verified correctly, the CREATE CRYPTOGRAPHIC PROVIDER statement will fail. For more information about certificates and certificate chains, see [SQL Server Certificates and Asymmetric Keys](../../relational-databases/security/sql-server-certificates-and-asymmetric-keys.md).  
  
 When an EKM provider dll does not implement all of the necessary methods, CREATE CRYPTOGRAPHIC PROVIDER can return error 33085:  
  
 `One or more methods cannot be found in cryptographic provider library '%.*ls'.`  
  
 When the header file used to create the EKM provider dll is out of date, CREATE CRYPTOGRAPHIC PROVIDER can return error 33032:  
  
 `SQL Crypto API version '%02d.%02d' implemented by provider is not supported. Supported version is '%02d.%02d'.`  
  
## Permissions  
 Requires CONTROL SERVER permission or membership in the **sysadmin** fixed server role.  
  
## Examples  
 The following example creates a cryptographic provider called `SecurityProvider` in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a .dll file. The .dll file is named `c:\SecurityProvider\SecurityProvider_v1.dll` and it is installed on the server. The provider's certificate must first be installed on the server.  
  
```sql  
-- Install the provider  
CREATE CRYPTOGRAPHIC PROVIDER SecurityProvider  
    FROM FILE = 'C:\SecurityProvider\SecurityProvider_v1.dll';  
```  
  
## See Also  
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)   
 [ALTER CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-cryptographic-provider-transact-sql.md)   
 [DROP CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-cryptographic-provider-transact-sql.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md)  
 [Set up SQL Server TDE Extensible Key Management by using Azure Key Vault](../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md)  
 [sys.cryptographic_providers](../../relational-databases/system-catalog-views/sys-cryptographic-providers-transact-sql.md)  
 [sys.dm_cryptographic_provider_properties](../../relational-databases/system-dynamic-management-views/sys-dm-cryptographic-provider-properties-transact-sql.md)
  
  
