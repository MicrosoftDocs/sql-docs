---
title: "CERTENCODED (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CERTENCODED"
  - "CERTENCODED_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CERTENCODED"
ms.assetid: 677a0719-7b9a-4f0b-bc61-41634563f924
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# CERTENCODED (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

This function returns the public portion of a certificate in binary format. This function takes a certificate ID as an argument, and returns the encoded certificate. To create a new certificate, pass the binary result to **CREATE CERTIFICATE ... WITH BINARY**.
  
## Syntax  
  
```sql
CERTENCODED ( cert_id )  
```  
  
## Arguments  
*cert_id*  
The **certificate_id** of the certificate. Find this value in sys.certificates; the [CERT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/cert-id-transact-sql.md) function will return it as well. *cert_id* has data type **int**.
  
## Return types
**varbinary**
  
## Remarks  
Use **CERTENCODED** and **CERTPRIVATEKEY** together to return, in binary form, different portions of a certificate.
  
## Permissions  
**CERTENCODED** is publicly available.
  
## Examples  
  
### Simple Example  
This example creates a certificate named `Shipping04`, and then uses the **CERTENCODED** function to return the binary encoding of the certificate. This example sets the certificate expiry date to October 31, 2040.
  
```sql
CREATE DATABASE TEST1;
GO
USE TEST1
CREATE CERTIFICATE Shipping04
ENCRYPTION BY PASSWORD = 'pGFD4bb925DGvbd2439587y'
WITH SUBJECT = 'Sammamish Shipping Records',
EXPIRY_DATE = '20401031';
GO
SELECT CERTENCODED(CERT_ID('Shipping04'));
  
```  
  
### B. Copying a Certificate to Another Database  
The more complex example creates two databases, `SOURCE_DB` and `TARGET_DB`. Then, create a certificate in `SOURCE_DB`, and then copy the certificate to the `TARGET_DB`. Finally, demonstrate that data encrypted in `SOURCE_DB` can be decrypted in `TARGET_DB` using the copy of the certificate.
  
To create the example environment, create the `SOURCE_DB` and `TARGET_DB` databases, and a master key in each database. Then, create a certificate in `SOURCE_DB`.
  
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
  
Next, extract the binary description of the certificate.
  
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
  
Then, create the duplicate certificate in the `TARGET_DB` database. Modify the following code for this to work, inserting the two binary values - @CERTENC and @CERTPVK - returned in the previous step. Don't surround these values with quotes.
  
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
  
This code, executed as a single batch, demonstrates that `TARGET_DB` can decrypt data originally encrypted in `SOURCE_DB`.
  
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
  
  
