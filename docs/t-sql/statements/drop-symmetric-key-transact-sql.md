---
title: "DROP SYMMETRIC KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP SYMMETRIC KEY"
  - "DROP_SYMMETRIC_KEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "symmetric keys [SQL Server], removing"
  - "deleting symmetric keys"
  - "encryption [SQL Server], symmetric keys"
  - "removing symmetric keys"
  - "dropping symmetric keys"
  - "cryptography [SQL Server], symmetric keys"
  - "DROP SYMMETRIC KEY statement"
ms.assetid: 6150bc67-08cb-402e-9c24-b04c9654b434
author: VanMSFT
ms.author: vanto
manager: craigg
---
# DROP SYMMETRIC KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Removes a symmetric key from the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP SYMMETRIC KEY symmetric_key_name [REMOVE PROVIDER KEY]  
```  
  
## Arguments  
 *symmetric_key_name*  
 Is the name of the symmetric key to be dropped.  
  
 REMOVE PROVIDER KEY  
 Removes an Extensible Key Management (EKM) key from an EKM device. For more information about Extensible Key Management, see [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md).  
  
## Remarks  
 If the key is open in the current session the statement will fail.  
  
 If the asymmetric key is mapped to an Extensible Key Management (EKM) key on an EKM device and the **REMOVE PROVIDER KEY** option is not specified, the key will be dropped from the database but not the device, and a warning will be issued.  
  
## Permissions  
 Requires CONTROL permission on the symmetric key.  
  
## Examples  
 The following example removes a symmetric key named `GailSammamishKey6` from the current database.  
  
```  
CLOSE SYMMETRIC KEY GailSammamishKey6;  
DROP SYMMETRIC KEY GailSammamishKey6;  
GO  
```  
  
## See Also  
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [ALTER SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-symmetric-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [CLOSE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/close-symmetric-key-transact-sql.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)  
  
  
