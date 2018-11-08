---
title: "DECRYPTBYASYMKEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DECRYPTBYASYMKEY"
  - "DECRYPTBYASYMKEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "asymmetric keys [SQL Server], DECRYPTBYASYMKEY function"
  - "DECRYPTBYASYMKEY function"
  - "decryption [SQL Server], asymmetric keys"
ms.assetid: d9ebcd30-f01c-4cfe-b95e-ffe6ea13788b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# DECRYPTBYASYMKEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function uses an asymmetric key to decrypt encrypted data.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DecryptByAsymKey (Asym_Key_ID , { 'ciphertext' | @ciphertext }   
    [ , 'Asym_Key_Password' ] )  
```  
  
## Arguments  
 *Asym_Key_ID*  
The ID of an asymmetric key in the database. *Asym_Key_ID* has an **int** data type.  
  
 *ciphertext*  
The string of data encrypted with the asymmetric key.  
  
 @ciphertext  
A variable of type **varbinary**, containing data encrypted with the asymmetric key.  
  
 *Asym_Key_Password*  
The password used to encrypt the asymmetric key in the database.  
  
## Return Types  
**varbinary**, with a maximum size of 8,000 bytes.  
  
## Remarks  
Compared to symmetric encryption / decryption, asymmetric key encryption / decryption has a high cost. When working with large datasets - for example, user data stored in tables - we suggest that developers avoid asymmetric key encryption / decryption.  
  
## Permissions  
`DECRYPTBYASYMKEY` requires CONTROL permission on the asymmetric key.  
  
## Examples  
This example decrypts ciphertext originally encrypted with asymmetric key `JanainaAsymKey02`. `AdventureWorks2012.ProtectedData04` stored this asymmetric key. The example decrypted the returned data with asymmetric key `JanainaAsymKey02`. The example used password `pGFD4bb925DGvbd2439587y` to decrypt this asymmetric key. The example converted the returned plaintext to type **nvarchar**.  
  
```  
SELECT CONVERT(nvarchar(max),  
    DecryptByAsymKey( AsymKey_Id('JanainaAsymKey02'),   
    ProtectedData, N'pGFD4bb925DGvbd2439587y' ))   
AS DecryptedData   
FROM [AdventureWorks2012].[Sales].[ProtectedData04]   
WHERE Description = N'encrypted by asym key''JanainaAsymKey02''';  
GO  
```  
  
## See Also  
 [ENCRYPTBYASYMKEY &#40;Transact-SQL&#41;](../../t-sql/functions/encryptbyasymkey-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [ALTER ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-asymmetric-key-transact-sql.md)   
 [DROP ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-asymmetric-key-transact-sql.md)   
 [Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
