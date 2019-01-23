---
title: "ENCRYPTBYKEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ENCRYPTBYKEY_TSQL"
  - "ENCRYPTBYKEY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "authenticators [SQL Server]"
  - "encryption [SQL Server], symmetric keys"
  - "symmetric keys [SQL Server], ENCRYPTBYKEY function"
  - "ENCRYPTBYKEY function"
ms.assetid: 0e11f8c5-f79d-46c1-ab11-b68ef05d6787
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# ENCRYPTBYKEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Encrypts data by using a symmetric key.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
EncryptByKey ( key_GUID , { 'cleartext' | @cleartext }  
    [, { add_authenticator | @add_authenticator }  
     , { authenticator | @authenticator } ] )  
```  
  
## Arguments  
 *key_GUID*  
 Is the GUID of the key to be used to encrypt the *cleartext*. **uniqueidentifier**.  
  
 '*cleartext*'  
 Is the data that is to be encrypted with the key.  
  
 @cleartext  
 Is a variable of type **nvarchar**, **char**, **varchar**, **binary**, **varbinary**, or **nchar** that contains data that is to be encrypted with the key.  
  
 *add_authenticator*  
 Indicates whether an authenticator will be encrypted together with the *cleartext*. Must be 1 when using an authenticator. **int**.  
  
 @add_authenticator  
 Indicates whether an authenticator will be encrypted together with the *cleartext*. Must be 1 when using an authenticator. **int**.  
  
 *authenticator*  
 Is the data from which to derive an authenticator. **sysname**.  
  
 @authenticator  
 Is a variable that contains data from which to derive an authenticator.  
  
## Return Types  
 **varbinary** with a maximum size of 8,000 bytes.  
  
 Returns NULL if the key is not open, if the key does not exist, or if the key is a deprecated RC4 key and the database is not in compatibility level 110 or higher.  
 
 Returns NULL if the *cleartext* value is NULL.
  
## Remarks  
 EncryptByKey uses a symmetric key. This key must be open. If the symmetric key is already open in the current session, you do not have to open it again in the context of the query.  
  
 The authenticator helps you deter whole-value substitution of encrypted fields. For example, consider the following table of payroll data.  
  
|Employee_ID|Standard_Title|Base_Pay|  
|------------------|---------------------|---------------|  
|345|Copy Room Assistant|Fskj%7^edhn00|  
|697|Chief Financial Officer|M0x8900f56543|  
|694|Data Entry Supervisor|Cvc97824%^34f|  
  
 Without breaking the encryption, a malicious user can infer significant information from the context in which the ciphertext is stored. Because a Chief Financial Officer is paid more than a Copy Room Assistant, it follows that the value encrypted as M0x8900f56543 must be greater than the value encrypted as Fskj%7^edhn00. If so, any user with ALTER permission on the table can give the Copy Room Assistant a raise by replacing the data in his Base_Pay field with a copy of the data stored in the Base_Pay field of the Chief Financial Officer. This whole-value substitution attack bypasses encryption altogether.  
  
 Whole-value substitution attacks can be thwarted by adding contextual information to the plaintext before encrypting it. This contextual information is used to verify that the plaintext data has not been moved.  
  
 If an authenticator parameter is specified when data is encrypted, the same authenticator is required to decrypt the data by using DecryptByKey. At encryption time, a hash of the authenticator is encrypted together with the plaintext. At decryption time, the same authenticator must be passed to DecryptByKey. If the two do not match, the decryption will fail. This indicates that the value has been moved since it was encrypted. We recommend using a column containing a unique and unchanging value as the authenticator. If the authenticator value changes, you might lose access to the data.  
  
 Symmetric encryption and decryption is relatively fast, and is suitable for working with large amounts of data.  
  
> [!IMPORTANT]  
>  Using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] encryption functions together with the ANSI_PADDING OFF setting could cause data loss because of implicit conversions. For more information about ANSI_PADDING, see [SET ANSI_PADDING &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-padding-transact-sql.md).  
  
## Examples  
 The functionality illustrated in the following examples relies on keys and certificates created in [How To: Encrypt a Column of Data](../../relational-databases/security/encryption/encrypt-a-column-of-data.md).  
  
### A. Encrypting a string with a symmetric key  
 The following example adds a column to the `Employee` table, and then encrypts the value of the Social Security number that is stored in column `NationalIDNumber`.  
  
```  
USE AdventureWorks2012;  
GO  
  
-- Create a column in which to store the encrypted data.  
ALTER TABLE HumanResources.Employee  
    ADD EncryptedNationalIDNumber varbinary(128);   
GO  
  
-- Open the symmetric key with which to encrypt the data.  
OPEN SYMMETRIC KEY SSN_Key_01  
   DECRYPTION BY CERTIFICATE HumanResources037;  
  
-- Encrypt the value in column NationalIDNumber with symmetric key  
-- SSN_Key_01. Save the result in column EncryptedNationalIDNumber.  
UPDATE HumanResources.Employee  
SET EncryptedNationalIDNumber  
    = EncryptByKey(Key_GUID('SSN_Key_01'), NationalIDNumber);  
GO  
```  
  
### B. Encrypting a record together with an authentication value  
  
```  
USE AdventureWorks2012;  
  
-- Create a column in which to store the encrypted data.  
ALTER TABLE Sales.CreditCard   
    ADD CardNumber_Encrypted varbinary(128);   
GO  
  
-- Open the symmetric key with which to encrypt the data.  
OPEN SYMMETRIC KEY CreditCards_Key11  
    DECRYPTION BY CERTIFICATE Sales09;  
  
-- Encrypt the value in column CardNumber with symmetric   
-- key CreditCards_Key11.  
-- Save the result in column CardNumber_Encrypted.    
UPDATE Sales.CreditCard  
SET CardNumber_Encrypted = EncryptByKey(Key_GUID('CreditCards_Key11'),   
    CardNumber, 1, CONVERT( varbinary, CreditCardID) );  
GO  
```  
  
## See Also  
 [DECRYPTBYKEY &#40;Transact-SQL&#41;](../../t-sql/functions/decryptbykey-transact-sql.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [ALTER SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-symmetric-key-transact-sql.md)   
 [DROP SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-symmetric-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [HASHBYTES &#40;Transact-SQL&#41;](../../t-sql/functions/hashbytes-transact-sql.md)  
  
  
