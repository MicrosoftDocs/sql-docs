---
title: "DECRYPTBYCERT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DecryptByCert_TSQL"
  - "DECRYPTBYCERT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "certificates [SQL Server], decryption"
  - "decryption [SQL Server], certificates"
  - "DECRYPTBYCERT function"
ms.assetid: 4950d787-40fa-4e26-bce8-2cb2ceca12fb
caps.latest.revision: 38
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DECRYPTBYCERT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Decrypts data with the private key of a certificate.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DecryptByCert ( certificate_ID , { 'ciphertext' | @ciphertext }   
    [ , { 'cert_password' | @cert_password } ] )  
```  
  
## Arguments  
 *certificate_ID*  
 Is the ID of a certificate in the database. *certificate*_ID is **int**.  
  
 *ciphertext*  
 Is a string of data that has been encrypted with the public key of the certificate.  
  
 @ciphertext  
 Is a variable of type **varbinary** that contains data that has been encrypted with the certificate.  
  
 *cert_password*  
 Is the password that was used to encrypt the private key of the certificate. Must be Unicode.  
  
 @cert_password  
 Is a variable of type **nchar** or **nvarchar** that contains the password that was used to encrypt the private key of the certificate. Must be Unicode.  
  
## Return Types  
 **varbinary** with a maximum size of 8,000 bytes.  
  
## Remarks  
 This function decrypts data with the private key of a certificate. Cryptographic transformations that use asymmetric keys consume significant resources. Therefore, EncryptByCert and DecryptByCert are not suited for routine encryption of user data.  
  
## Permissions  
 Requires CONTROL permission on the certificate.  
  
## Examples  
 The following example selects rows from `[AdventureWorks2012].[ProtectedData04]` that are marked as `data encrypted by certificate JanainaCert02`. The example decrypts the ciphertext with the private key of certificate `JanainaCert02`, which it first decrypts with the password of the certificate, `pGFD4bb925DGvbd2439587y`. The decrypted data is converted from **varbinary** to **nvarchar**.  
  
```  
SELECT convert(nvarchar(max), DecryptByCert(Cert_Id('JanainaCert02'),  
    ProtectedData, N'pGFD4bb925DGvbd2439587y'))  
FROM [AdventureWorks2012].[ProtectedData04]   
WHERE Description   
    = N'data encrypted by certificate '' JanainaCert02''';  
GO  
```  
  
## See Also  
 [ENCRYPTBYCERT &#40;Transact-SQL&#41;](../../t-sql/functions/encryptbycert-transact-sql.md)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)   
 [ALTER CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-certificate-transact-sql.md)   
 [DROP CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-certificate-transact-sql.md)   
 [BACKUP CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/backup-certificate-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  