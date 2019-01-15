---
title: "DROP ASYMMETRIC KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP ASYMMETRIC KEY"
  - "DROP_ASYMMETRIC_KEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "asymmetric keys [SQL Server], removing"
  - "removing asymmetric keys"
  - "encryption [SQL Server], asymmetric keys"
  - "DROP ASYMMETRIC KEY statement"
  - "dropping asymmetric keys"
  - "deleting asymmetric keys"
  - "cryptography [SQL Server], asymmetric keys"
ms.assetid: bf94ac07-9b62-4318-b55b-1eed8f3a1ac6
author: CarlRabeler
ms.author: carlrabauthor: VanMSFT
ms.author: vanto
manager: craigg
---
# DROP ASYMMETRIC KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Removes an asymmetric key from the database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP ASYMMETRIC KEY key_name [ REMOVE PROVIDER KEY ]  
```  
  
## Arguments  
 *key_name*  
 Is the name of the asymmetric key to be dropped from the database.  
  
 REMOVE PROVIDER KEY  
 Removes an Extensible Key Management (EKM) key from an EKM device. For more information about Extensible Key Management, see [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md).  
  
## Remarks  
 An asymmetric key with which a symmetric key in the database has been encrypted, or to which a user or login is mapped, cannot be dropped. Before you drop such a key, you must drop any user or login that is mapped to the key. You must also drop or change any symmetric key encrypted with the asymmetric key. You can use the DROP ENCRYPTION option of [ALTER SYMMETRIC KEY](../../t-sql/statements/alter-symmetric-key-transact-sql.md) to remove encryption by an asymmetric key.  
  
 Metadata of asymmetric keys can be accessed by using the [sys.asymmetric_keys](../../relational-databases/system-catalog-views/sys-asymmetric-keys-transact-sql.md) catalog view. The keys themselves cannot be directly viewed from inside the database.  
  
 If the asymmetric key is mapped to an Extensible Key Management (EKM) key on an EKM device and the REMOVE PROVIDER KEY option is not specified, the key will be dropped from the database but not the device. A warning will be issued.  
  
## Permissions  
 Requires CONTROL permission on the asymmetric key.  
  
## Examples  
 The following example removes the asymmetric key `MirandaXAsymKey6` from the `AdventureWorks2012` database.  
  
```  
USE AdventureWorks2012;  
DROP ASYMMETRIC KEY MirandaXAsymKey6;  
```  
  
## See Also  
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [ALTER ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-asymmetric-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [ALTER SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-symmetric-key-transact-sql.md)  
  
  
