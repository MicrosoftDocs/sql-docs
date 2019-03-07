---
title: "KEY_NAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "KEY_NAME_TSQL"
  - "KEY_NAME"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "KEY_NAME function"
ms.assetid: 7b693e5d-2325-4bf9-9b45-ad6a23374b41
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# KEY_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the name of the symmetric key from either a symmetric key GUID or cipher text.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
KEY_NAME ( ciphertext | key_guid )   
```  
  
## Arguments  
 *ciphertext*  
 Is the text encrypted by the symmetric key. *cyphertext* is type **varbinary(8000)**.  
  
 *key_guid*  
 Is the GUID of the symmetric key. *key_guid* is type **uniqueidentifier**.  
  
## Returned Types  
 **varchar(128)**  
  
## Permissions  
 Beginning in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the visibility of metadata is limited to securables that a user either owns or on which the user has been granted some permission. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
  
### A. Displaying the name of a symmetric key using the key_guid  
 The **master** database contains a symmetric key named ##MS_ServiceMasterKey##. The following example gets the GUID of that key from the sys.symmetric_keys dynamic management view, assigns it to a variable, and then passes that variable to the KEY_NAME function to demonstrate how to return the name that corresponds to the GUID.  
  
```  
USE master;  
GO  
DECLARE @guid uniqueidentifier ;  
SELECT @guid = key_guid FROM sys.symmetric_keys  
WHERE name = '##MS_ServiceMasterKey##' ;  
-- Demonstration of passing a GUID to KEY_NAME to receive a name  
SELECT KEY_NAME(@guid) AS [Name of Key];  
```  
  
### B. Displaying the name of a symmetric key using the cipher text  
 The following example demonstrates the entire process of creating a symmetric key and populating data into a table. The example then shows how KEY_NAME returns the name of the key when passed the encrypted text.  
  
```  
-- Create a symmetric key  
CREATE SYMMETRIC KEY TestSymKey   
   WITH ALGORITHM = AES_128,  
   KEY_SOURCE = 'The square of the hypotenuse is equal to the sum of the squares of the sides',  
   IDENTITY_VALUE = 'Pythagoras'  
   ENCRYPTION BY PASSWORD = 'pGFD4bb925DGvbd2439587y' ;  
GO  
-- Create a table for the demonstration  
CREATE TABLE DemoKey  
(IDCol int IDENTITY PRIMARY KEY,  
SecretCol varbinary(256) NOT NULL)  
GO  
-- Open the symmetric key if not already open  
OPEN SYMMETRIC KEY TestSymKey   
    DECRYPTION BY PASSWORD = 'pGFD4bb925DGvbd2439587y';  
GO  
-- Insert a row into the DemoKey table  
DECLARE @key_GUID uniqueidentifier;  
SELECT @key_GUID = key_guid FROM sys.symmetric_keys  
WHERE name LIKE 'TestSymKey' ;  
INSERT INTO DemoKey(SecretCol)  
VALUES ( ENCRYPTBYKEY (@key_GUID, 'EncryptedText'))  
GO  
-- Verify the DemoKey data  
SELECT * FROM DemoKey;  
GO  
-- Decrypt the data  
DECLARE @ciphertext varbinary(256);  
SELECT @ciphertext = SecretCol  
FROM DemoKey WHERE IDCol = 1 ;  
SELECT CAST (  
DECRYPTBYKEY( @ciphertext)  
AS varchar(100) ) AS SecretText ;  
-- Use KEY_NAME to view the name of the key  
SELECT KEY_NAME(@ciphertext) AS [Name of Key] ;  
  
```  
  
## See Also  
 [sys.symmetric_keys &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-symmetric-keys-transact-sql.md)   
 [ENCRYPTBYKEY &#40;Transact-SQL&#41;](../../t-sql/functions/encryptbykey-transact-sql.md)   
 [DECRYPTBYKEYAUTOASYMKEY &#40;Transact-SQL&#41;](../../t-sql/functions/decryptbykeyautoasymkey-transact-sql.md)  
  
  
