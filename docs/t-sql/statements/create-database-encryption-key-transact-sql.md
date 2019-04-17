---
title: "CREATE DATABASE ENCRYPTION KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/24/2016"
ms.prod: sql
ms.prod_service: "pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DATABASE_ENCRYPTION_KEY_TSQL"
  - "ENCRYPTION_KEY_TSQL"
  - "sql13.swb.dbencryptionkeyo.f1"
  - "ENCRYPTION KEY"
  - "DATABASE ENCRYPTION KEY"
  - "CREATE_DATABASE_ENCRYPTION_KEY_TSQL"
  - "CREATE DATABASE ENCRYPTION KEY"
  - "sql13.swb.dbencryptionkeyg.f1"
  - "CREATE DATABASE ENCRYPTION"
  - "CREATE_DATABASE_ENCRYPTION_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database encryption key"
  - "CREATE DATABASE ENCRYPTION KEY statement"
  - "database encryption key, create"
ms.assetid: 2ee95a32-5140-41bd-9ab3-a947b9990688
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE DATABASE ENCRYPTION KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md)]

 Creates an encryption key that is used for transparently encrypting a database. For more information about transparent database encryption, see [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md).  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server  

CREATE DATABASE ENCRYPTION KEY  
       WITH ALGORITHM = { AES_128 | AES_192 | AES_256 | TRIPLE_DES_3KEY }  
   ENCRYPTION BY SERVER   
    {  
        CERTIFICATE Encryptor_Name |  
        ASYMMETRIC KEY Encryptor_Name  
    }  
[ ; ]  
```  
  
```  
-- Syntax for Parallel Data Warehouse  

CREATE DATABASE ENCRYPTION KEY  
       WITH ALGORITHM = { AES_128 | AES_192 | AES_256 | TRIPLE_DES_3KEY }  
   ENCRYPTION BY SERVER CERTIFICATE Encryptor_Name   
[ ; ]  
```  
  
## Arguments  
WITH ALGORITHM = { AES_128 | AES_192 | AES_256 | TRIPLE_DES_3KEY  }  
Specifies the encryption algorithm that is used for the encryption key.   
> [!NOTE]
>    Beginning with SQL Server 2016, all algorithms other than AES_128, AES_192, and AES_256 are deprecated. 
>   To use older algorithms (not recommended) you must set the database to database compatibility level 120 or lower.  
  
ENCRYPTION BY SERVER CERTIFICATE Encryptor_Name  
Specifies the name of the encryptor used to encrypt the database encryption key.  
  
ENCRYPTION BY SERVER ASYMMETRIC KEY Encryptor_Name  
Specifies the name of the asymmetric key used to encrypt the database encryption key. In order to encrypt the database encryption key with an asymmetric key, the asymmetric key must reside on an extensible key management provider.  
  
## Remarks  
A database encryption key is required before a database can be encrypted by using *Transparent Database Encryption* (TDE). When a database is transparently encrypted, the whole database is encrypted at the file level, without any special code modifications. The certificate or asymmetric key that is used to encrypt the database encryption key must be located in the master system database.  
  
Database encryption statements are allowed only on user databases.  
  
The database encryption key cannot be exported from the database. It is available only to the system, to users who have debugging permissions on the server, and to users who have access to the certificates that encrypt and decrypt the database encryption key.  
  
The database encryption key does not have to be regenerated when a database owner (dbo) is changed.  
  
A database encryption key is automatically created for a [!INCLUDE[ssSDS](../../includes/sssds-md.md)] database. You do not need to create a key using the CREATE DATABASE ENCRYPTION KEY statement.  
  
## Permissions  
Requires CONTROL permission on the database and VIEW DEFINITION permission on the certificate or asymmetric key that is used to encrypt the database encryption key.  
  
## Examples  
For additional examples using TDE, see [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md), [Enable TDE on SQL Server Using EKM](../../relational-databases/security/encryption/enable-tde-on-sql-server-using-ekm.md), and [Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md).  
  
The following example creates a database encryption key by using the `AES_256` algorithm, and protects the private key with a certificate named `MyServerCert`.  
  
```  
USE AdventureWorks2012;  
GO  
CREATE DATABASE ENCRYPTION KEY  
WITH ALGORITHM = AES_256  
ENCRYPTION BY SERVER CERTIFICATE MyServerCert;  
GO  
```  
  
## See Also  
[Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md)   
[SQL Server Encryption](../../relational-databases/security/encryption/sql-server-encryption.md)   
[SQL Server and Database Encryption Keys &#40;Database Engine&#41;](../../relational-databases/security/encryption/sql-server-and-database-encryption-keys-database-engine.md)   
[Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
[ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
[ALTER DATABASE ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-encryption-key-transact-sql.md)   
[DROP DATABASE ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-encryption-key-transact-sql.md)   
[sys.dm_database_encryption_keys &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md)  
    
