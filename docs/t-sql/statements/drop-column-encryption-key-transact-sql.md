---
title: "DROP COLUMN ENCRYPTION KEY (Transact-SQL)"
description: DROP COLUMN ENCRYPTION KEY (Transact-SQL)
author: jaszymas
ms.author: jaszymas
ms.date: "10/15/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP COLUMN ENCRYPTION"
  - "DROP COLUMN ENCRYPTION KEY"
  - "DROP_COLUMN_ENCRYPTION_TSQL"
  - "DROP_COLUMN_ENCRYPTION_KEY_TSQL"
helpviewer_keywords:
  - "DROP COLUMN ENCRYPTION KEY statement"
  - "column encryption key, drop"
dev_langs:
  - "TSQL"
---
# DROP COLUMN ENCRYPTION KEY (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Drops a column encryption key from a database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP COLUMN ENCRYPTION KEY key_name [;]  
```  

## Arguments
 *key_name*  
 Is the name by which the column encryption key to be dropped from the database.  
  
## Remarks
 A column encryption key cannot be dropped if it is used to encrypt any column in the database. All columns using the column encryption key must first be dropped.  
  
## Permissions  
 Requires **ALTER ANY COLUMN ENCRYPTION KEY** permission on the database.  
  
## Examples  
  
### A. Dropping a column encryption key  
 The following example drops a column encryption key called `MyCEK`.  
  
```sql  
DROP COLUMN ENCRYPTION KEY MyCEK;  
GO  
```  
  
## See Also  
 [CREATE COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-encryption-key-transact-sql.md)   
 [ALTER COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-column-encryption-key-transact-sql.md)   
 [CREATE COLUMN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-master-key-transact-sql.md)  
 [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md)   
 [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md)   
 [Overview of Key Management for Always Encrypted](../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md)   
 [Manage keys for Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves-manage-keys.md)   
  
  
