---
title: "CERTPRIVATEKEY (Transact-SQL)"
description: "CERTPRIVATEKEY (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CERTPRIVATEKEY"
  - "CERTPRIVATEKEY_TSQL"
helpviewer_keywords:
  - "CERTPRIVATEKEY"
dev_langs:
  - "TSQL"
---
# CERTPRIVATEKEY (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This function returns the private key of a certificate in binary format. This function takes three arguments.
-   A certificate ID.  
-   An encryption password, used to encrypt the private key bits returned by the function. This approach does not expose the keys as clear text to users.  
-   An optional decryption password. A specified decryption password is used to decrypt the private key of the certificate. Otherwise, the database master key is used.  
  
Only users with access to the certificate private key can use this function. This function returns the private key in PVK format.
  
## Syntax  
  
```syntaxsql
CERTPRIVATEKEY   
    (  
          cert_ID   
        , ' encryption_password '   
      [ , ' decryption_password ' ]  
    )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
  
