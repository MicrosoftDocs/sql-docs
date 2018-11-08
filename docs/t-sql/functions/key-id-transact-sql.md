---
title: "KEY_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "Key_ID"
  - "Key_ID_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "identification numbers [SQL Server], symmetric keys"
  - "KEY_ID function"
  - "symmetric keys [SQL Server], IDs"
  - "IDs [SQL Server], symmetric keys"
ms.assetid: d7309542-dbbe-41dc-b42e-5d9a1c8b4838
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# KEY_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the ID of a symmetric key in the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
Key_ID ( 'Key_Name' )  
```  
  
## Arguments  
 **'** *Key_Name* **'**  
 The name of a symmetric key in the database.  
  
## Return Types  
 **int**  
  
## Remarks  
 The name of a temporary key must start with a number sign (#).  
  
## Permissions  
 Because temporary keys are only available in the session in which they are created, no permissions are required to access them. To access a key that is not temporary, the caller needs some permission on the key and must not have been denied VIEW permission on the key.  
  
## Examples  
  
### A. Returning the ID of a symmetric key  
 The following example returns the ID of a key called `ABerglundKey1`.  
  
```  
SELECT KEY_ID('ABerglundKey1');  
```  
  
### B. Returning the ID of a temporary symmetric key  
 The following example returns the ID of a temporary symmetric key. Note that `#` is prepended to the key name.  
  
```  
SELECT KEY_ID('#ABerglundKey2');  
```  
  
## See Also  
 [KEY_GUID &#40;Transact-SQL&#41;](../../t-sql/functions/key-guid-transact-sql.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [sys.symmetric_keys &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-symmetric-keys-transact-sql.md)   
 [sys.key_encryptions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-key-encryptions-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
