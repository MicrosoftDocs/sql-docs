---
title: "VERIFYSIGNEDBYASYMKEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "VERIFYSIGNEDBYASYMKEY_TSQL"
  - "VERIFYSIGNEDBYASYMKEY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "verifying digitally signed data for changes"
  - "VERIFYSIGNEDBYASYMKEY"
  - "testing digitally signed data for changes"
  - "checking digitally signed data for changes"
  - "signatures [SQL Server]"
  - "digital signatures [SQL Server]"
ms.assetid: 9f7c6e0b-5ba4-4dbb-994d-5bd59f4908de
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# VERIFYSIGNEDBYASYMKEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Tests whether digitally signed data has been changed since it was signed.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
VerifySignedByAsymKey( Asym_Key_ID , clear_text , signature )  
```  
  
## Arguments  
 *Asym_Key_ID*  
 Is the ID of an asymmetric key certificate in the database.  
  
 *clear_text*  
 Is clear text data that is being verified.  
  
 *signature*  
 Is the signature that was attached to the signed data. *signature* is **varbinary**.  
  
## Return Types  
 **int**  
  
 Returns 1 when the signatures match; otherwise 0.  
  
## Remarks  
 **VerifySignedByAsymKey** decrypts the signature of the data by using the public key of the specified asymmetric key, and compares the decrypted value to a newly computed MD5 hash of the data. If the values match, the signature is confirmed to be valid.  
  
## Permissions  
 Requires VIEW DEFINITION permission on the asymmetric key.  
  
## Examples  
  
### A. Testing for data with a valid signature  
 The following example returns 1 if the selected data has not been changed since it was signed with asymmetric key `WillisKey74`. The example returns 0 if the data has been tampered with.  
  
```  
SELECT Data,  
     VerifySignedByAsymKey( AsymKey_Id( 'WillisKey74' ), SignedData,  
     DataSignature ) as IsSignatureValid  
FROM [AdventureWorks2012].[SignedData04]   
WHERE Description = N'data encrypted by asymmetric key ''WillisKey74''';  
GO  
RETURN;  
```  
  
### B. Returning a result set that contains data with a valid signature  
 The following example returns rows in `SignedData04` that contain data that has not been changed since it was signed with asymmetric key `WillisKey74`. The example calls the function `AsymKey_ID` to obtain the ID of the asymmetric key from the database.  
  
```  
SELECT Data   
FROM [AdventureWorks2012].[SignedData04]   
WHERE VerifySignedByAsymKey( AsymKey_Id( 'WillisKey74' ), Data,  
     DataSignature ) = 1  
AND Description = N'data encrypted by asymmetric key ''WillisKey74''';  
GO  
```  
  
## See Also  
 [ASYMKEY_ID &#40;Transact-SQL&#41;](../../t-sql/functions/asymkey-id-transact-sql.md)   
 [SIGNBYASYMKEY &#40;Transact-SQL&#41;](../../t-sql/functions/signbyasymkey-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [ALTER ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-asymmetric-key-transact-sql.md)   
 [DROP ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-asymmetric-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
