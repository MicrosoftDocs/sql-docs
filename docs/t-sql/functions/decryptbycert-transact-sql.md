---
title: "DECRYPTBYCERT (Transact-SQL)"
description: "DECRYPTBYCERT (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DecryptByCert_TSQL"
  - "DECRYPTBYCERT"
helpviewer_keywords:
  - "certificates [SQL Server], decryption"
  - "decryption [SQL Server], certificates"
  - "DECRYPTBYCERT function"
dev_langs:
  - "TSQL"
---
# DECRYPTBYCERT (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This function uses the private key of a certificate to decrypt encrypted data.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DecryptByCert ( certificate_ID , { 'ciphertext' | @ciphertext }   
    [ , { 'cert_password' | @cert_password } ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *certificate_ID*  
The ID of a certificate in the database. *certificate_ID* has an **int** data type.  
  
 *ciphertext*  
The string of data encrypted with the public key of the certificate.  
  
 @ciphertext  
A variable of type **varbinary** containing data encrypted with the certificate.  
  
 *cert_password*  
The password used to encrypt the private key of the certificate. *cert_password* must have a Unicode data format.  
  
 @cert_password  
A variable of type **nchar** or **nvarchar** containing the password used to encrypt the private key of the certificate. *\@cert_password* must have a Unicode data format.  

## Return Types  
**varbinary**, with a maximum size of 8,000 bytes.  
  
## Remarks  
This function decrypts data with the private key of a certificate. Cryptographic transformations that use asymmetric keys consume significant resources. Therefore, we suggest that developers avoid use of [ENCRYPTBYCERT](./encryptbycert-transact-sql.md) and DECRYPTBYCERT for routine user data encryption / decryption.  

## Permissions  
`DECRYPTBYCERT` requires CONTROL permission on the certificate.  
  
## Examples  
This example selects rows from `[AdventureWorks2012].[ProtectedData04]` marked as data originally encrypted by certificate `JanainaCert02`. The example first decrypts the private key of certificate `JanainaCert02` with the password of certificate `pGFD4bb925DGvbd2439587y`. Then, the example decrypts the ciphertext with this private key. The example converts the decrypted data from **varbinary** to **nvarchar**.  

```sql  
SELECT CONVERT(NVARCHAR(max), DecryptByCert(Cert_Id('JanainaCert02'),  
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
  
  
