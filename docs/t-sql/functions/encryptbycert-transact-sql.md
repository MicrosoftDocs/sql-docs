---
title: "ENCRYPTBYCERT (Transact-SQL)"
description: "ENCRYPTBYCERT (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "ENCRYPTBYCERT"
  - "ENCRYPTBYCERT_TSQL"
helpviewer_keywords:
  - "certificates [SQL Server], encryption"
  - "encryption [SQL Server], certificates"
  - "ENCRYPTBYCERT function"
dev_langs:
  - "TSQL"
---
# ENCRYPTBYCERT (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Encrypts data with the public key of a certificate.  
  
:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
EncryptByCert ( certificate_ID , { 'cleartext' | @cleartext } )  
```  
  
## Arguments
_certificate\_ID_  
The ID of a certificate in the database. **int**.  
  
_cleartext_  
A string of data that will be encrypted with the certificate.  
  
**\@cleartext**  
A variable of one of the following types that contains data that will be encrypted with the public key of the certificate:

* **nvarchar** 
* **char**
* **varchar**
* **binary** 
* **varbinary**
* **nchar**
  
## Return Types  
**varbinary** with a maximum size of 8,000 bytes.  
  
## Remarks  
This function encrypts data with the certificate's public key. The ciphertext can only be decrypted with the corresponding private key. These asymmetric transformations are costly when compared to encryption and decryption using a symmetric key. As such, asymmetric encryption isn't recommended when working with large datasets.
  
## Examples  
This example encrypts the plaintext stored in `@cleartext` with the certificate called `JanainaCert02`. The encrypted data is inserted into table `ProtectedData04`.  
  
```sql  
INSERT INTO [AdventureWorks2022].[ProtectedData04]   
    VALUES ( N'Data encrypted by certificate ''Shipping04''',  
    EncryptByCert(Cert_ID('JanainaCert02'), @cleartext) );  
GO  
```  
  
## Related content

- [DECRYPTBYCERT (Transact-SQL)](decryptbycert-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../statements/create-certificate-transact-sql.md)
- [ALTER CERTIFICATE (Transact-SQL)](../statements/alter-certificate-transact-sql.md)
- [DROP CERTIFICATE (Transact-SQL)](../statements/drop-certificate-transact-sql.md)
- [BACKUP CERTIFICATE (Transact-SQL)](../statements/backup-certificate-transact-sql.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
