---
title: "DROP COLUMN ENCRYPTION KEY (Transact-SQL)"
description: DROP COLUMN ENCRYPTION KEY (Transact-SQL)
author: jaszymas
ms.author: jaszymas
ms.date: "01/30/2026"
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
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP COLUMN ENCRYPTION KEY key_name [;]  
```

## Arguments

*key_name*  
 The name of the column encryption key to drop from the database.  
  
## Remarks

A column encryption key can't be dropped if it's used to encrypt any column in the database. All columns using the column encryption key must first be decrypted or dropped.

To remove encryption from a column:

1. **Decrypt the column** - Use `ALTER TABLE` to modify the encrypted column, removing the encryption specification:

   ```sql
   ALTER TABLE dbo.Employees
   ALTER COLUMN SSN NVARCHAR(11);
   ```

2. **Drop the column encryption key** - After all columns using the key are decrypted, you can drop the key:

   ```sql
   DROP COLUMN ENCRYPTION KEY MyCEK;
   ```

Alternatively, if you no longer need the column data, you can drop the column entirely using `ALTER TABLE DROP COLUMN` before dropping the encryption key.

## Permissions

Requires **ALTER ANY COLUMN ENCRYPTION KEY** permission on the database.

## Examples

### A. Dropping a column encryption key

The following example drops a column encryption key called `MyCEK`.

```sql  
DROP COLUMN ENCRYPTION KEY MyCEK;  
GO  
```

## Related content

- [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](create-column-encryption-key-transact-sql.md)
- [ALTER COLUMN ENCRYPTION KEY (Transact-SQL)](alter-column-encryption-key-transact-sql.md)
- [CREATE COLUMN MASTER KEY (Transact-SQL)](create-column-master-key-transact-sql.md)
- [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md)
- [Overview of key management for Always Encrypted](../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md)
- [Manage keys for Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves-manage-keys.md)
