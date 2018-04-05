---
title: "DROP COLUMN MASTER KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/20/2016"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database"
ms.service: ""
ms.component: "t-sql|statements"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DROP COLUMN MASTER KEY"
  - "DROP_COLUMN_MASTER_KEY_TSQL"
  - "DROP COLUMN MASTER"
  - "DROP_COLUMN_MASTER_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.column_master_keys catalog view"
ms.assetid: fd5e77c8-a3ae-4795-bb46-b322c0500041
caps.latest.revision: 14
author: "edmacauley"
ms.author: "edmaca"
manager: "craigg"
ms.workload: "Inactive"
---
# DROP COLUMN MASTER KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Drops a column master key from a database. This is a metadata operation.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP COLUMN MASTER KEY key_name;  
```  
  
## Arguments  
 *key_name*  
 The name of the column master key.  
  
## Remarks  
 The column master key can only be dropped if there are no column encryption key values encrypted with the column master key. To drop column encryption key values, use the [DROP COLUMN ENCRYPTION KEY](../../t-sql/statements/drop-column-encryption-key-transact-sql.md) statement.  
  
## Permissions  
 Requires **ALTER ANY COLUMN MASTER KEY** permission on the database.  
  
## Examples  
  
### A. Dropping a column master key  
 The following example drops a column master key called `MyCMK`.  
  
```  
DROP COLUMN MASTER KEY MyCMK;  
GO  
```  
  
## See Also  
 [CREATE COLUMN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-master-key-transact-sql.md)   
 [CREATE COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-encryption-key-transact-sql.md)   
 [DROP COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-column-encryption-key-transact-sql.md)   
 [Always Encrypted &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-database-engine.md)   
 [sys.column_master_keys &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md)  
  
  
