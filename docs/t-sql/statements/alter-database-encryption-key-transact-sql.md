---
title: "ALTER DATABASE ENCRYPTION KEY (Transact-SQL)"
description: ALTER DATABASE ENCRYPTION KEY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "04/16/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_DATABASE_ENCRYPTION_KEY_TSQL"
  - "ALTER DATABASE ENCRYPTION"
  - "ALTER_DATABASE_ENCRYPTION_TSQL"
  - "ALTER DATABASE ENCRYPTION KEY"
helpviewer_keywords:
  - "database encryption key, alter"
  - "ALTER DATABASE ENCRYPTION KEY"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || >=sql-server-2017 || >=sql-server-linux-2017"
---
# ALTER DATABASE ENCRYPTION KEY (Transact-SQL)

[!INCLUDE [sql-pdw](../../includes/applies-to-version/sql-pdw.md)]

  Alters an encryption key and certificate that is used for transparently encrypting a database. For more information about transparent database encryption, see [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md).  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server  
  
ALTER DATABASE ENCRYPTION KEY  
      REGENERATE WITH ALGORITHM = { AES_128 | AES_192 | AES_256 | TRIPLE_DES_3KEY }  
   |  
   ENCRYPTION BY SERVER   
    {  
        CERTIFICATE Encryptor_Name |  
        ASYMMETRIC KEY Encryptor_Name  
    }  
[ ; ]  
```
  
  
```syntaxsql
-- Syntax for Parallel Data Warehouse  
  
ALTER DATABASE ENCRYPTION KEY  
    {  
      {  
        REGENERATE WITH ALGORITHM = { AES_128 | AES_192 | AES_256 | TRIPLE_DES_3KEY }  
        [ ENCRYPTION BY SERVER CERTIFICATE Encryptor_Name ]  
      }  
      |  
      ENCRYPTION BY SERVER   CERTIFICATE Encryptor_Name    
    }  
[ ; ]  
```  
 
## Arguments
 REGENERATE WITH ALGORITHM = { AES_128 \| AES_192 \| AES_256 \| TRIPLE_DES_3KEY }  
 Specifies the encryption algorithm that is used for the encryption key.  
  
 ENCRYPTION BY SERVER CERTIFICATE *Encryptor_Name*  
 Specifies the name of the certificate used to encrypt the database encryption key.  
  
 ENCRYPTION BY SERVER ASYMMETRIC KEY Encryptor_Name  
 Specifies the name of the asymmetric key used to encrypt the database encryption key.  
  
## Remarks  
 The certificate or asymmetric key that is used to encrypt the database encryption key must be located in the master system database.  
  
 When the database owner (dbo) is changed, the database encryption key does not have to be regenerated.
  
 After a database encryption key has been modified twice, a log backup must be performed before the database encryption key can be modified again.  
  
## Permissions  
 Requires CONTROL permission on the database and VIEW DEFINITION permission on the certificate or asymmetric key that is used to encrypt the database encryption key.  
  
## Examples  
 The following example alters the database encryption key to use the `AES_256` algorithm.  
  
```sql  
-- Uses AdventureWorks  
  
ALTER DATABASE ENCRYPTION KEY  
REGENERATE WITH ALGORITHM = AES_256;  
GO  
```  
  
## Related content

- [Transparent data encryption (TDE)](../../relational-databases/security/encryption/transparent-data-encryption.md)
- [SQL Server encryption](../../relational-databases/security/encryption/sql-server-encryption.md)
- [SQL Server and Database Encryption Keys (Database Engine)](../../relational-databases/security/encryption/sql-server-and-database-encryption-keys-database-engine.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
- [ALTER DATABASE SET options (Transact-SQL)](alter-database-transact-sql-set-options.md)
- [CREATE DATABASE ENCRYPTION KEY (Transact-SQL)](create-database-encryption-key-transact-sql.md)
- [DROP DATABASE ENCRYPTION KEY (Transact-SQL)](drop-database-encryption-key-transact-sql.md)
- [sys.dm_database_encryption_keys &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md)
