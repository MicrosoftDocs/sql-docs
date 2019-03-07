---
title: "CERTPRIVATEKEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CERTPRIVATEKEY"
  - "CERTPRIVATEKEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CERTPRIVATEKEY"
ms.assetid: 33e0f01e-39ac-46da-94ff-fe53b1116df4
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# CERTPRIVATEKEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

This function returns the private key of a certificate in binary format. This function takes three arguments.
-   A certificate ID.  
-   An encryption password, used to encrypt the private key bits returned by the function. This approach does not expose the keys as clear text to users.  
-   An optional decryption password. A specified decryption password is used to decrypt the private key of the certificate. Otherwise, the database master key is used.  
  
Only users with access to the certificate private key can use this function. This function returns the private key in PVK format.
  
## Syntax  
  
```sql
CERTPRIVATEKEY   
    (  
          cert_ID   
        , ' encryption_password '   
      [ , ' decryption_password ' ]  
    )  
```  
  
## Arguments  
*certificate_ID*  
The **certificate_id** of the certificate. Obtain this value from sys.certificates or from the [CERT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/cert-id-transact-sql.md) function. *cert_id* has data type **int**.
  
*encryption_password*  
The password used to encrypt the returned binary value.
  
*decryption_password*  
The password used to decrypt the returned binary value.
  
## Return types
**varbinary**
  
## Remarks  
Use **CERTENCODED** and **CERTPRIVATEKEY** together to return different portions of a certificate, in binary form.
  
## Permissions  
**CERTPRIVATEKEY** is publicly available.
  
## Examples  
  
```sql
CREATE DATABASE TEST1;  
GO  
USE TEST1  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Use 5tr0ng P^55Words'  
GO  
CREATE CERTIFICATE Shipping04   
WITH SUBJECT = 'Sammamish Shipping Records',   
EXPIRY_DATE = '20401031';  
GO  
SELECT CERTPRIVATEKEY(CERT_ID('Shipping04'), 'jklalkaa/; uia3dd');  
```  
  
See [CERTENCODED &#40;Transact-SQL&#41;](../../t-sql/functions/certencoded-transact-sql.md), Example B, for a more complex example that uses **CERTPRIVATEKEY** and **CERTENCODED** to copy a certificate to another database.
  
## See also
[Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)  
[CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)
[Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)
[sys.certificates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md)
  
  
