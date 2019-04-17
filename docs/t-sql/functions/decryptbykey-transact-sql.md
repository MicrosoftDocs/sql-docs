---
title: "DECRYPTBYKEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DecryptByKey_TSQL"
  - "DECRYPTBYKEY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "symmetric keys [SQL Server], DecryptByKey function"
  - "decryption [SQL Server], keys"
  - "decryption [SQL Server], symmetric keys"
  - "DECRYPTBYKEY function"
ms.assetid: 6edf121f-ac62-4dae-90e6-6938f32603c9
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# DECRYPTBYKEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function uses a symmetric key to decrypt data.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DecryptByKey ( { 'ciphertext' | @ciphertext }   
    [ , add_authenticator, { authenticator | @authenticator } ] )  
```  
  
## Arguments  
*ciphertext*  
A variable of type **varbinary** containing data encrypted with the key.  
  
**@ciphertext**  
A variable of type **varbinary** containing data encrypted with the key.  
  
 *add_authenticator*  
Indicates whether the original encryption process included, and encrypted, an authenticator together with the plaintext. Must match the value passed to [ENCRYPTBYKEY (Transact-SQL)](./encryptbykey-transact-sql.md) during the data encryption process. *add_authenticator* has an **int** data type.  
  
 *authenticator*  
The data used as the basis for the generation of the authenticator. Must match the value supplied to [ENCRYPTBYKEY (Transact-SQL)](./encryptbykey-transact-sql.md). *authenticator* has a **sysname** data type.  

**@authenticator**  
A variable containing data from which an authenticator generates. Must match the value supplied to [ENCRYPTBYKEY (Transact-SQL)](./encryptbykey-transact-sql.md). *@authenticator* has a **sysname** data type.  

## Return Types  
**varbinary**, with a maximum size of 8,000 bytes. `DECRYPTBYKEY` returns NULL if the symmetric key used for data encryption is not open or if *ciphertext* is NULL.  
  
## Remarks  
`DECRYPTBYKEY` uses a symmetric key. The database must have this symmetric key already open. `DECRYPTBYKEY` will allow multiple keys open at the same time. You do not have to open the key immediately before cipher text decryption.  
  
Symmetric encryption and decryption typically operates relatively quickly, and it works well for operations involving large data volumes.  
  
## Permissions  
The symmetric key must already be open in the current session. See [OPEN SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/open-symmetric-key-transact-sql.md) for more information.  
  
## Examples  
  
### A. Decrypting by using a symmetric key  
This example decrypts ciphertext with a symmetric key.  
  
```  
-- First, open the symmetric key with which to decrypt the data.  
OPEN SYMMETRIC KEY SSN_Key_01  
   DECRYPTION BY CERTIFICATE HumanResources037;  
GO  
  
-- Now list the original ID, the encrypted ID, and the   
-- decrypted ciphertext. If the decryption worked, the original  
-- and the decrypted ID will match.  
SELECT NationalIDNumber, EncryptedNationalID   
    AS 'Encrypted ID Number',  
    CONVERT(nvarchar, DecryptByKey(EncryptedNationalID))   
    AS 'Decrypted ID Number'  
    FROM HumanResources.Employee;  
GO  
```  
  
### B. Decrypting by using a symmetric key and an authenticating hash  
This example decrypts data originally encrypted together with an authenticator.  
  
```  
-- First, open the symmetric key with which to decrypt the data  
OPEN SYMMETRIC KEY CreditCards_Key11  
   DECRYPTION BY CERTIFICATE Sales09;  
GO  
  
-- Now list the original card number, the encrypted card number,  
-- and the decrypted ciphertext. If the decryption worked,   
-- the original number will match the decrypted number.  
SELECT CardNumber, CardNumber_Encrypted   
    AS 'Encrypted card number', CONVERT(nvarchar,  
    DecryptByKey(CardNumber_Encrypted, 1 ,   
    HashBytes('SHA1', CONVERT(varbinary, CreditCardID))))   
    AS 'Decrypted card number' FROM Sales.CreditCard;  
GO  
  
```  
  
## See Also  
 [ENCRYPTBYKEY &#40;Transact-SQL&#41;](../../t-sql/functions/encryptbykey-transact-sql.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [ALTER SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-symmetric-key-transact-sql.md)   
 [DROP SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-symmetric-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)  
  
  
