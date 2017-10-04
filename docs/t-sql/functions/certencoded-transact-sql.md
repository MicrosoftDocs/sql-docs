---
title: "CERTENCODED (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CERTENCODED"
  - "CERTENCODED_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CERTENCODED"
ms.assetid: 677a0719-7b9a-4f0b-bc61-41634563f924
caps.latest.revision: 14
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CERTENCODED (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

Returns the public portion of a certificate in binary format. This function takes a certificate ID and returns the encoded certificate. The binary result can be passed to **CREATE CERTIFICATE … WITH BINARY** to create a new certificate.
  
## Syntax  
  
```sql
CERTENCODED ( cert_id )  
```  
  
## Arguments  
*cert_id*  
Is the **certificate_id** of the certificate. This is available from sys.certificates or by using the [CERT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/cert-id-transact-sql.md) function. *cert_id* is type **int**
  
## Return types
**varbinary**
  
## Remarks  
**CERTENCODED** and **CERTPRIVATEKEY** are used together to return different portions of a certificate in binary form.
  
## Permissions  
**CERTENCODED** is available to public.
  
## Examples  
  
### Simple Example  
The following example creates a certificate named `Shipping04` and then uses the **CERTENCODED** function to return the binary encoding of the certificate.
  
```sql
CREATE DATABASE TEST1;  
GO  
USE TEST1  
CREATE CERTIFICATE Shipping04   
ENCRYPTION BY PASSWORD = 'pGFD4bb925DGvbd2439587y'  
WITH SUBJECT = 'Sammamish Shipping Records',   
EXPIRY_DATE = '20161031';  
GO  
SELECT CERTENCODED(CERT_ID('Shipping04'));  
  
```  
  
### B. Copying a Certificate to Another Database  
The following more complicated example, creates two databases, `SOURCE_DB` and `TARGET_DB`. The goal is to create a certificate in the `SOURCE_DB`, and then copy the certificate to the `TARGET_DB`, and then demonstrate that data encrypted in `SOURCE_DB` can be decrypted in `TARGET_DB` using the copy of the certificate.
  
To create the example environment, create the `SOURCE_DB` and `TARGET_DB` databases, and a master key in each. Then create a certificate in `SOURCE_DB`.
  
```sql
USE master;  
GO  
CREATE DATABASE SOURCE_DB;  
GO  
USE SOURCE_DB;  
GO  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'S0URCE_DB KEY Pa$$W0rd';  
GO  
CREATE DATABASE TARGET_DB;  
GO  
USE TARGET_DB  
GO  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Pa$$W0rd in TARGET_DB';  
GO  
  
-- Create a certificate in SOURCE_DB  
USE SOURCE_DB;  
GO  
CREATE CERTIFICATE SOURCE_CERT WITH SUBJECT = 'SOURCE_CERTIFICATE';  
GO  
```  
  
Now extract the binary description of the certificate.
  
```sql
DECLARE @CERTENC VARBINARY(MAX);  
DECLARE @CERTPVK VARBINARY(MAX);  
SELECT @CERTENC = CERTENCODED(CERT_ID('SOURCE_CERT'));  
SELECT @CERTPVK = CERTPRIVATEKEY(CERT_ID('SOURCE_CERT'),  
       'CertEncryptionPa$$word');  
SELECT @CERTENC AS BinaryCertificate;  
SELECT @CERTPVK AS EncryptedBinaryCertificate;  
GO  
```  
  
Create the duplicate certificate in the `TARGET_DB` database. You must modify the following code, inserting the two binary values returned in the previous step.
  
```sql
-- Create the duplicate certificate in the TARGET_DB database  
USE TARGET_DB  
GO  
CREATE CERTIFICATE TARGET_CERT  
FROM BINARY = <insert the binary value of the @CERTENC variable>  
WITH PRIVATE KEY (  
BINARY = <insert the binary value of the @CERTPVK variable>  
, DECRYPTION BY PASSWORD = 'CertEncryptionPa$$word');  
-- Compare the certificates in the two databases  
-- The two certificates should be the same   
-- except for name and (possibly) the certificate_id  
SELECT * FROM SOURCE_DB.sys.certificates  
UNION  
SELECT * FROM TARGET_DB.sys.certificates;  
```  
  
The following code executed as a single batch demonstrates that data encrypted in `SOURCE_DB` can be decrypted in `TARGET_DB`.
  
```sql
USE SOURCE_DB;  
  
DECLARE @CLEARTEXT nvarchar(100);  
DECLARE @CIPHERTEXT varbinary(8000);  
DECLARE @UNCIPHEREDTEXT_Source nvarchar(100);  
SET @CLEARTEXT = N'Hello World';  
SET @CIPHERTEXT = ENCRYPTBYCERT(CERT_ID('SOURCE_CERT'), @CLEARTEXT);  
SET @UNCIPHEREDTEXT_Source =   
    DECRYPTBYCERT(CERT_ID('SOURCE_CERT'), @CIPHERTEXT)  
-- Encryption and decryption result in SOURCE_DB  
SELECT @CLEARTEXT AS SourceClearText, @CIPHERTEXT AS SourceCipherText,   
       @UNCIPHEREDTEXT_Source AS SourceDecryptedText;  
  
-- SWITCH DATABASE  
USE TARGET_DB;  
  
DECLARE @UNCIPHEREDTEXT_Target nvarchar(100);  
SET @UNCIPHEREDTEXT_Target = DECRYPTBYCERT(CERT_ID('TARGET_CERT'), @CIPHERTEXT);  
-- Encryption and decryption result in TARGET_DB  
SELECT @CLEARTEXT AS ClearTextInTarget, @CIPHERTEXT AS CipherTextInTarget, @UNCIPHEREDTEXT_Target AS DecriptedTextInTarget;   
GO  
```  
  
## See also
[Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)  
[CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)  
[CERTPRIVATEKEY &#40;Transact-SQL&#41;](../../t-sql/functions/certprivatekey-transact-sql.md)  
[sys.certificates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md)
  
  
