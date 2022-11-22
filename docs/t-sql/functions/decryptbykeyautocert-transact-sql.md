---
title: "DECRYPTBYKEYAUTOCERT (Transact-SQL)"
description: "DECRYPTBYKEYAUTOCERT (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "09/09/2015"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DECRYPTBYKEYAUTOCERT"
  - "DECRYPTBYKEYAUTOCERT_TSQL"
helpviewer_keywords:
  - "DECRYPTBYKEYAUTOCERT function"
dev_langs:
  - "TSQL"
---
# DECRYPTBYKEYAUTOCERT (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This function decrypts data with a symmetric key. That symmetric key automatically decrypts with a certificate.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DecryptByKeyAutoCert ( cert_ID , cert_password   
    , { 'ciphertext' | @ciphertext }  
  [ , { add_authenticator | @add_authenticator }   
  [ , { authenticator | @authenticator } ] ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *cert_ID*  
The ID of the certificate used to protect the symmetric key. *cert_ID* has an **int** data type.  
  
*cert_password*  
The password used to encrypt the private key of the certificate. Can have a `NULL` value if the database master key protects the private key. *cert_password* has an **nvarchar** data type.  

'*ciphertext*'  
The string of data encrypted with the key. *ciphertext* has a **varbinary** data type.  

@ciphertext  
A variable of type **varbinary** containing data encrypted with the key.  

*add_authenticator*  
Indicates whether the original encryption process included, and encrypted, an authenticator together with the plaintext. Must match the value passed to [ENCRYPTBYKEY (Transact-SQL)](./encryptbykey-transact-sql.md) during the data encryption process. *add_authenticator* has a value of 1 if the encryption process used an authenticator. *add_authenticator* has an **int** data type.  
  
@add_authenticator  
A variable indicating whether the original encryption process included, and encrypted, an authenticator together with the plaintext. Must match the value passed to [ENCRYPTBYKEY (Transact-SQL)](./encryptbykey-transact-sql.md) during the data encryption process. *\@add_authenticator* has an **int** data type.  
  
*authenticator*  
The data used as the basis for the generation of the authenticator. Must match the value supplied to [ENCRYPTBYKEY (Transact-SQL)](./encryptbykey-transact-sql.md). *authenticator* has a **sysname** data type.  
  
@authenticator  
A variable containing data from which an authenticator generates. Must match the value supplied to [ENCRYPTBYKEY (Transact-SQL)](./encryptbykey-transact-sql.md). *\@authenticator* has a **sysname** data type.  
  
## Return Types  
**varbinary**, with a maximum size of 8,000 bytes.  
  
## Remarks  
`DECRYPTBYKEYAUTOCERT` combines the functionality of `OPEN SYMMETRIC KEY` and `DECRYPTBYKEY`. In a single operation, it first decrypts a symmetric key, and then decrypts encrypted ciphertext with that key.  
  
## Permissions  
Requires `VIEW DEFINITION` permission on the symmetric key, and `CONTROL` permission on the certificate.   
  
## Examples  
This example shows how `DECRYPTBYKEYAUTOCERT` can simplify decryption code. This code should run on an [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database that does not already have a database master key.  
  
```sql  
--Create the keys and certificate.  
USE AdventureWorks2012;  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'mzkvdlk979438teag$$ds987yghn)(*&4fdg^';  
OPEN MASTER KEY DECRYPTION BY PASSWORD = 'mzkvdlk979438teag$$ds987yghn)(*&4fdg^';  
CREATE CERTIFICATE HumanResources037   
   WITH SUBJECT = 'Sammamish HR',   
   EXPIRY_DATE = '10/31/2009';  
CREATE SYMMETRIC KEY SSN_Key_01 WITH ALGORITHM = DES  
    ENCRYPTION BY CERTIFICATE HumanResources037;  
GO  
----Add a column of encrypted data.  
ALTER TABLE HumanResources.Employee  
    ADD EncryptedNationalIDNumber varbinary(128);   
OPEN SYMMETRIC KEY SSN_Key_01  
   DECRYPTION BY CERTIFICATE HumanResources037 ;  
UPDATE HumanResources.Employee  
SET EncryptedNationalIDNumber  
    = EncryptByKey(Key_GUID('SSN_Key_01'), NationalIDNumber);  
GO  
--  
--Close the key used to encrypt the data.  
CLOSE SYMMETRIC KEY SSN_Key_01;  
--  
--There are two ways to decrypt the stored data.  
--  
--OPTION ONE, using DecryptByKey()  
--1. Open the symmetric key  
--2. Decrypt the data  
--3. Close the symmetric key  
OPEN SYMMETRIC KEY SSN_Key_01  
   DECRYPTION BY CERTIFICATE HumanResources037;  
SELECT NationalIDNumber, EncryptedNationalIDNumber    
    AS 'Encrypted ID Number',  
    CONVERT(nvarchar, DecryptByKey(EncryptedNationalIDNumber))   
    AS 'Decrypted ID Number'  
    FROM HumanResources.Employee;  
CLOSE SYMMETRIC KEY SSN_Key_01;  
--  
--OPTION TWO, using DecryptByKeyAutoCert()  
SELECT NationalIDNumber, EncryptedNationalIDNumber   
    AS 'Encrypted ID Number',  
    CONVERT(nvarchar, DecryptByKeyAutoCert ( cert_ID('HumanResources037') , NULL ,EncryptedNationalIDNumber))   
    AS 'Decrypted ID Number'  
    FROM HumanResources.Employee;  
```  
  
## See Also  
 [OPEN SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/open-symmetric-key-transact-sql.md)   
 [ENCRYPTBYKEY &#40;Transact-SQL&#41;](../../t-sql/functions/encryptbykey-transact-sql.md)   
 [DECRYPTBYKEY &#40;Transact-SQL&#41;](../../t-sql/functions/decryptbykey-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
