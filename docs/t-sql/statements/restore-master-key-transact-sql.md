---
title: "RESTORE MASTER KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "RESTORE_MASTER_KEY_TSQL"
  - "RESTORE MASTER KEY"
  - "LOAD_MASTER_KEY_TSQL"
  - "LOAD MASTER KEY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database master key [SQL Server], importing"
  - "encryption [SQL Server], Database Master Key"
  - "copying Database Master Keys"
  - "importing Database Master Keys"
  - "cryptography [SQL Server], Database Master Key"
  - "transferring Database Master Keys"
  - "RESTORE MASTER KEY statement"
ms.assetid: 70ceb951-31a2-4fc4-a0c1-e6c18eeb3ae7
author: VanMSFT
ms.author: vanto
manager: craigg
---
# RESTORE MASTER KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Imports a database master key from a backup file.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
RESTORE MASTER KEY FROM FILE = 'path_to_file'   
    DECRYPTION BY PASSWORD = 'password'  
    ENCRYPTION BY PASSWORD = 'password'  
    [ FORCE ]  
```  
  
## Arguments  
 FILE ='*path_to_file*'  
 Specifies the complete path, including file name, to the stored database master key. *path_to_file* can be a local path or a UNC path to a network location.  
  
 DECRYPTION BY PASSWORD ='*password*'  
 Specifies the password that is required to decrypt the database master key that is being imported from a file.  
  
 ENCRYPTION BY PASSWORD ='*password*'  
 Specifies the password that is used to encrypt the database master key after it has been loaded into the database.  
  
 FORCE  
 Specifies that the RESTORE process should continue, even if the current database master key is not open, or if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot decrypt some of the private keys that are encrypted with it.  
  
## Remarks  
 When the master key is restored, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] decrypts all the keys that are encrypted with the currently active master key, and then encrypts these keys with the restored master key. This resource-intensive operation should be scheduled during a period of low demand. If the current database master key is not open or cannot be opened, or if any of the keys that are encrypted by it cannot be decrypted, the restore operation fails.  
  
 Use the FORCE option only if the master key is irretrievable or if decryption fails. Information that is encrypted only by an irretrievable key will be lost.  
  
 If the master key was encrypted by the service master key, the restored master key will also be encrypted by the service master key.  
  
 If there is no master key in the current database, RESTORE MASTER KEY creates a master key. The new master key will not be automatically encrypted with the service master key.  
  
## Permissions  
 Requires CONTROL permission on the database.  
  
## Examples  
 The following example restores the database master key of the `AdventureWorks2012` database.  
  
```  
USE AdventureWorks2012;  
RESTORE MASTER KEY   
    FROM FILE = 'c:\backups\keys\AdventureWorks2012_master_key'   
    DECRYPTION BY PASSWORD = '3dH85Hhk003#GHkf02597gheij04'   
    ENCRYPTION BY PASSWORD = '259087M#MyjkFkjhywiyedfgGDFD';  
GO  
```  
  
## See Also  
 [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md)   
 [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-master-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
