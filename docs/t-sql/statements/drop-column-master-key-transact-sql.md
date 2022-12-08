---
title: "DROP COLUMN MASTER KEY (Transact-SQL)"
description: DROP COLUMN MASTER KEY (Transact-SQL)
author: jaszymas
ms.author: jaszymas
ms.date: "10/15/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP COLUMN MASTER KEY"
  - "DROP_COLUMN_MASTER_KEY_TSQL"
  - "DROP COLUMN MASTER"
  - "DROP_COLUMN_MASTER_TSQL"
helpviewer_keywords:
  - "sys.column_master_keys catalog view"
dev_langs:
  - "TSQL"
---
# DROP COLUMN MASTER KEY (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Drops a column master key from a database. This is a metadata operation.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
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
  
```sql  
DROP COLUMN MASTER KEY MyCMK;  
GO  
```  
  
## See Also  
 [CREATE COLUMN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-master-key-transact-sql.md)   
 [CREATE COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-encryption-key-transact-sql.md)   
 [DROP COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-column-encryption-key-transact-sql.md)   
 [sys.column_master_keys &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md)  
 [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md)   
 [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md)   
 [Overview of Key Management for Always Encrypted](../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md)   
 [Manage keys for Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves-manage-keys.md)   
  
  
