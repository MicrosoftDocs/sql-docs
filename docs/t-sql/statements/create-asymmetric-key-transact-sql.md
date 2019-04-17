---
title: "CREATE ASYMMETRIC KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/07/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ASYMMETRIC_KEY_TSQL"
  - "CREATE ASYMMETRIC KEY"
  - "CREATE_ASYMMETRIC_KEY_TSQL"
  - "ASYMMETRIC KEY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "asymmetric keys [SQL Server], creating"
  - "encryption [SQL Server], asymmetric keys"
  - "CREATE ASYMMETRIC KEY statement"
  - "asymmetric keys [SQL Server]"
  - "cryptography [SQL Server], asymmetric keys"
ms.assetid: 141bc976-7631-49f6-82bd-a235028645b1
author: VanMSFT
ms.author: vanto
manager: craigg
---
# CREATE ASYMMETRIC KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Creates an asymmetric key in the database.  
  
 This feature is incompatible with database export using Data Tier Application Framework (DACFx). You must drop all asymmetric keys before exporting.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
CREATE ASYMMETRIC KEY Asym_Key_Name   
   [ AUTHORIZATION database_principal_name ]  
   [ FROM <Asym_Key_Source> ]  
   [ WITH <key_option> ] 
   [ ENCRYPTION BY <encrypting_mechanism> ] 
   [ ; ]
  
<Asym_Key_Source>::=  
     FILE = 'path_to_strong-name_file'  
   | EXECUTABLE FILE = 'path_to_executable_file'  
   | ASSEMBLY Assembly_Name  
   | PROVIDER Provider_Name  
  
<key_option> ::=  
   ALGORITHM = <algorithm>  
      |  
   PROVIDER_KEY_NAME = 'key_name_in_provider'  
      |  
      CREATION_DISPOSITION = { CREATE_NEW | OPEN_EXISTING }  
  
<algorithm> ::=  
      { RSA_4096 | RSA_3072 | RSA_2048 | RSA_1024 | RSA_512 }   
  
<encrypting_mechanism> ::=  
    PASSWORD = 'password'   
```  
  
## Arguments  
 FROM *Asym_Key_Source*  
 Specifies the source from which to load the asymmetric key pair.  
  
 AUTHORIZATION *database_principal_name*  
 Specifies the owner of the asymmetric key. The owner cannot be a role or a group. If this option is omitted, the owner will be the current user.  
  
 FILE ='*path_to_strong-name_file*'  
 Specifies the path of a strong-name file from which to load the key pair.  
  
> [!NOTE]  
>  This option is not available in a contained database.  
  
 EXECUTABLE FILE ='*path_to_executable_file*'  
 Specifies an assembly file from which to load the public key. Limited to 260 characters by MAX_PATH from the Windows API.  
  
> [!NOTE]  
>  This option is not available in a contained database.  
  
 ASSEMBLY *Assembly_Name*  
 Specifies the name of an assembly from which to load the public key.  
  
ENCRYPTION BY *\<key_name_in_provider>*
 Specifies how the key is encrypted. Can be a certificate, password, or asymmetric key.  
  
 KEY_NAME ='*key_name_in_provider*'  
 Specifies the key name from the external provider. For more information about external key management, see [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md).  
  
 CREATION_DISPOSITION = CREATE_NEW  
 Creates a new key on the Extensible Key Management device. PROV_KEY_NAME must be used to specify key name on the device. If a key already exists on the device the statement fails with error.  
  
 CREATION_DISPOSITION = OPEN_EXISTING  
 Maps a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] asymmetric key to an existing Extensible Key Management key. PROV_KEY_NAME must be used to specify key name on the device. If CREATION_DISPOSITION = OPEN_EXISTING is not provided, the default is CREATE_NEW.  
  
 ALGORITHM = \<algorithm>  
 Five algorithms can be provided; RSA_4096, RSA_3072, RSA_2048, RSA_1024, and RSA_512.  
  
 RSA_1024 and RSA_512 are deprecated. To use RSA_1024 or RSA_512 (not recommended) you must set the database to database compatibility level 120 or lower.  
  
 PASSWORD = '*password*'  
 Specifies the password with which to encrypt the private key. If this clause is not present, the private key will be encrypted with the database master key. *password* is a maximum of 128 characters. *password* must meet the Windows password policy requirements of the computer that is running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Remarks  
 An *asymmetric key* is a securable entity at the database level. In its default form, this entity contains both a public key and a private key. When executed without the FROM clause, CREATE ASYMMETRIC KEY generates a new key pair. When executed with the FROM clause, CREATE ASYMMETRIC KEY imports a key pair from a file or imports a public key from an assembly.  
  
 By default, the private key is protected by the database master key. If no database master key has been created, a password is required to protect the private key. If a database master key does exist, the password is optional.  
  
 The private key can be 512, 1024, or 2048 bits long.  
  
## Permissions  
 Requires CREATE ASYMMETRIC KEY permission on the database. If the AUTHORIZATION clause is specified, requires IMPERSONATE permission on the database principal, or ALTER permission on the application role. Only Windows logins, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins, and application roles can own asymmetric keys. Groups and roles cannot own asymmetric keys.  
  
## Examples  
  
### A. Creating an asymmetric key  
 The following example creates an asymmetric key named `PacificSales09` by using the `RSA_2048` algorithm, and protects the private key with a password.  
  
```  
CREATE ASYMMETRIC KEY PacificSales09   
    WITH ALGORITHM = RSA_2048   
    ENCRYPTION BY PASSWORD = '<enterStrongPasswordHere>';   
GO  
```  
  
### B. Creating an asymmetric key from a file, giving authorization to a user  
 The following example creates the asymmetric key `PacificSales19` from a key pair stored in a file, and then authorizes user `Christina` to use the asymmetric key.  
  
```  
CREATE ASYMMETRIC KEY PacificSales19 AUTHORIZATION Christina   
    FROM FILE = 'c:\PacSales\Managers\ChristinaCerts.tmp'    
    ENCRYPTION BY PASSWORD = '<enterStrongPasswordHere>';  
GO  
```  
  
### C. Creating an asymmetric key from an EKM provider  
 The following example creates the asymmetric key `EKM_askey1` from a key pair stored in a file. It then encrypts it using an Extensible Key Management provider called `EKMProvider1`, and a key on that provider called `key10_user1`.  
  
```  
CREATE ASYMMETRIC KEY EKM_askey1   
    FROM PROVIDER EKM_Provider1  
    WITH   
        ALGORITHM = RSA_2048,   
        CREATION_DISPOSITION = CREATE_NEW  
        , PROVIDER_KEY_NAME  = 'key10_user1' ;  
GO  
```  
  
## See Also  
 [Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)   
 [ALTER ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-asymmetric-key-transact-sql.md)   
 [DROP ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-asymmetric-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md)  
  
  
