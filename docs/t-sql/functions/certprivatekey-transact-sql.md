---
title: "CERTPRIVATEKEY (Transact-SQL) | Microsoft Docs"
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
  - "CERTPRIVATEKEY"
  - "CERTPRIVATEKEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CERTPRIVATEKEY"
ms.assetid: 33e0f01e-39ac-46da-94ff-fe53b1116df4
caps.latest.revision: 13
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CERTPRIVATEKEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns the private key of a certificate in binary format. This function takes three arguments.  
  
-   A certificate ID.  
  
-   An encryption password which is used to encrypt the private key bits when they are returned by the function, so that the keys are not exposed clear text to users.  
  
-   A decryption password which is optional. If a decryption password is specified, then it is used to decrypt the private key of the certificate otherwise database master key is used.  
  
 Only users that have access to certificate’s private key will be able to use this function. This function returns the private key in PVK format.  
  
## Syntax  
  
```  
  
CERTPRIVATEKEY   
    (  
          cert_ID   
        , ' encryption_password '   
      [ , ' decryption_password ' ]  
    )  
```  
  
#### Parameters  
  
## Arguments  
 *certificate_ID*  
 Is the **certificate_id** of the certificate. This is available from sys.certificates or by using the [CERT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/cert-id-transact-sql.md) function. *cert_id* is type **int**  
  
 *encryption_password*  
 The password used to encrypt the returned binary value.  
  
 *decryption_password*  
 The password used to decrypt the returned binary value.  
  
## Return Types  
 **varbinary**  
  
## Remarks  
 **CERTENCODED** and **CERTPRIVATEKEY** are used together to return different portions of a certificate in binary form.  
  
## Permissions  
 **CERTPRIVATEKEY** is available to public.  
  
## Examples  
  
```tsql  
CREATE DATABASE TEST1;  
GO  
USE TEST1  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Use 5tr0ng P^55Words'  
GO  
CREATE CERTIFICATE Shipping04   
WITH SUBJECT = 'Sammamish Shipping Records',   
EXPIRY_DATE = '20141031';  
GO  
SELECT CERTPRIVATEKEY(CERT_ID('Shipping04'), 'jklalkaa/; uia3dd');  
  
```  
  
 For a more complex example that uses **CERTPRIVATEKEY** and **CERTENCODED** to copy a certificate to another database, see example B in the topic [CERTENCODED &#40;Transact-SQL&#41;](../../t-sql/functions/certencoded-transact-sql.md).  
  
## See Also  
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)   
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)   
 [sys.certificates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md)  
  
  