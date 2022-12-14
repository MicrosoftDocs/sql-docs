---
title: "ENCRYPTBYASYMKEY (Transact-SQL)"
description: "ENCRYPTBYASYMKEY (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ENCRYPTBYASYMKEY"
  - "ENCRYPTBYASYMKEY_TSQL"
helpviewer_keywords:
  - "ENCRYPTBYASYMKEY function"
  - "encryption [SQL Server], asymmetric keys"
  - "asymmetric keys [SQL Server], ENCRYPTBYASYMKEY function"
dev_langs:
  - "TSQL"
---
# ENCRYPTBYASYMKEY (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This function encrypts data with an asymmetric key.  
  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
EncryptByAsymKey ( Asym_Key_ID , { 'plaintext' | @plaintext } )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*asym_key_ID*  
The ID of an asymmetric key in the database. *asym_key_ID* has an **int** data type.  
  
*cleartext*  
A string of data that `ENCRYPTBYASYMKEY` will encrypt with the asymmetric key. *cleartext* can have a
 
+ **binary**
+ **char**
+ **nchar**
+ **nvarchar**
+ **varbinary**
  
or
  
+ **varchar**
 
data type.  
  
**\@plaintext**  
A variable holding a value that `ENCRYPTBYASYMKEY` will encrypt with the asymmetric key. **\@plaintext** can have a
  
+ **binary**
+ **char**
+ **nchar**
+ **nvarchar**
+ **varbinary**
  
or
  
+ **varchar**
 
data type.  
  
## Return Types  
**varbinary**, with a maximum size of 8,000 bytes.  
  
## Remarks  
Encryption and decryption operations that use asymmetric keys consume significant resources, and so become expensive compared with symmetric key encryption and decryption. We suggest that developers avoid asymmetric key encryption and decryption operations on large datasets - for example, user data datasets stored in database tables. Instead, we suggest that developers first encrypt that data with a strong symmetric key, and then encrypt that symmetric key with an asymmetric key.  
  
Depending on the algorithm, `ENCRYPTBYASYMKEY` returns **NULL** if the input exceeds a certain number of bytes. The specific limits:

+ a 512-bit RSA key can encrypt up to 53 bytes
+ a 1024-bit key can encrypt up to 117 bytes
+ a 2048-bit key can encrypt up to 245 bytes

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], both certificates and asymmetric keys serve as wrappers over RSA keys.  
  
## Examples  
This example encrypts the text stored in `@cleartext` with the asymmetric key `JanainaAsymKey02`. The statement inserts the encrypted data into the `ProtectedData04` table.  
  
```sql  
INSERT INTO AdventureWorks2012.Sales.ProtectedData04   
    VALUES( N'Data encrypted by asymmetric key ''JanainaAsymKey02''',  
    EncryptByAsymKey(AsymKey_ID('JanainaAsymKey02'), @cleartext) );  
GO  
```  
  
## See Also  
 [DECRYPTBYASYMKEY &#40;Transact-SQL&#41;](../../t-sql/functions/decryptbyasymkey-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
