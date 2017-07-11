---
title: "DECRYPTBYKEYAUTOCERT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/09/2015"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DECRYPTBYKEYAUTOCERT"
  - "DECRYPTBYKEYAUTOCERT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DECRYPTBYKEYAUTOCERT function"
ms.assetid: 6b45fa2e-ffaa-46f7-86ff-5624596eda4a
caps.latest.revision: 26
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DECRYPTBYKEYAUTOCERT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Decrypts by using a symmetric key that is automatically decrypted with a certificate.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DecryptByKeyAutoCert ( cert_ID , cert_password   
    , { 'ciphertext' | @ciphertext }  
  [ , { add_authenticator | @add_authenticator }   
  [ , { authenticator | @authenticator } ] ] )  
```  
  
## Arguments  
 *cert_ID*  
 Is the ID of the certificate that is used to protect the symmetric key. *cert_ID* is **int**.  
  
 *cert_password*  
 Is the password that protects the private key of the certificate. Can be NULL if the private key is protected by the database master key. *cert_password* is **nvarchar**.  
  
 '*ciphertext*'  
 Is the data that was encrypted with the key. *ciphertext* is **varbinary**.  
  
 @ciphertext  
 Is a variable of type **varbinary** that contains data that was encrypted with the key.  
  
 *add_authenticator*  
 Indicates whether an authenticator was encrypted together with the plaintext. Must be the same value that is passed to EncryptByKey when encrypting the data.Is **1** if an authenticator was used. *add_authenticator* is **int**.  
  
 @add_authenticator  
 Indicates whether an authenticator was encrypted together with the plaintext. Must be the same value that is passed to EncryptByKey when encrypting the data.  
  
 *authenticator*  
 Is the data from which to generate an authenticator. Must match the value that was supplied to EncryptByKey. *authenticator* is **sysname**.  
  
 @authenticator  
 Is a variable that contains data from which to generate an authenticator. Must match the value that was supplied to EncryptByKey.  
  
## Return Types  
 **varbinary** with a maximum size of 8,000 bytes.  
  
## Remarks  
 DecryptByKeyAutoCert combines the functionality of OPEN SYMMETRIC KEY and DecryptByKey. In a single operation, it decrypts a symmetric key and uses that key to decrypt cipher text.  
  
## Permissions  
 Requires VIEW DEFINITION permission on the symmetric key and CONTROL permission on the certificate.  
  
## Examples  
 The following example shows how `DecryptByKeyAutoCert` can be used to simplify code that performs a decryption. This code should be run on an [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database that does not already have a database master key.  
  
```  
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
  
  
