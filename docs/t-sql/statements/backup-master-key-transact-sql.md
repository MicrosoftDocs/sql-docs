---
title: "BACKUP MASTER KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "BACKUP MASTER KEY"
  - "DUMP_MASTER_KEY_TSQL"
  - "BACKUP_MASTER_KEY_TSQL"
  - "DUMP MASTER KEY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "BACKUP MASTER KEY statement"
  - "exporting Database Master Keys"
  - "encryption [SQL Server], Database Master Key"
  - "cryptography [SQL Server], Database Master Key"
  - "backing up master keys [SQL Server]"
  - "database master key [SQL Server], exporting"
ms.assetid: 0e25fe22-2536-4d7e-ba4a-1921e880f367
author: VanMSFT
ms.author: vanto
manager: craigg
---
# BACKUP MASTER KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Exports the database master key.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
BACKUP MASTER KEY TO FILE = 'path_to_file'   
    ENCRYPTION BY PASSWORD = 'password'  
```  
  
## Arguments  
 FILE ='*path_to_file*'  
 Specifies the complete path, including file name, to the file to which the master key will be exported. This may be a local path or a UNC path to a network location.  
  
 PASSWORD ='*password*'  
 Is the password used to encrypt the master key in the file. This password is subject to complexity checks. For more information, see [Password Policy](../../relational-databases/security/password-policy.md).  
  
## Remarks  
 The master key must be open and, therefore, decrypted before it is backed up. If it is encrypted with the service master key, the master key does not have to be explicitly opened. But if the master key is encrypted only with a password, it must be explicitly opened.  
  
 We recommend that you back up the master key as soon as it is created, and store the backup in a secure, off-site location.  
  
## Permissions  
 Requires CONTROL permission on the database.  
  
## Examples  
 The following example creates a backup of the `AdventureWorks2012` master key. Because this master key is not encrypted by the service master key, a password must be specified when it is opened.  
  
```  
USE AdventureWorks2012;  
OPEN MASTER KEY DECRYPTION BY PASSWORD = 'sfj5300osdVdgwdfkli7';  
BACKUP MASTER KEY TO FILE = 'c:\temp\exportedmasterkey'   
    ENCRYPTION BY PASSWORD = 'sd092735kjn$&adsg';  
GO   
```  
  
## See Also  
 [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md)   
 [OPEN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/open-master-key-transact-sql.md)   
 [CLOSE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/close-master-key-transact-sql.md)   
 [RESTORE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-master-key-transact-sql.md)   
 [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-master-key-transact-sql.md)   
 [DROP MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-master-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
