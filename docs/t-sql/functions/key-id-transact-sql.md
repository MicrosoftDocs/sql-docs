---
title: "KEY_ID (Transact-SQL)"
description: "KEY_ID (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "Key_ID"
  - "Key_ID_TSQL"
helpviewer_keywords:
  - "identification numbers [SQL Server], symmetric keys"
  - "KEY_ID function"
  - "symmetric keys [SQL Server], IDs"
  - "IDs [SQL Server], symmetric keys"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# KEY_ID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Returns the ID of a symmetric key in the current database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
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
  
```sql  
SELECT KEY_ID('ABerglundKey1');  
```  
  
### B. Returning the ID of a temporary symmetric key  
 The following example returns the ID of a temporary symmetric key. Note that `#` is prepended to the key name.  
  
```sql  
SELECT KEY_ID('#ABerglundKey2');  
```  
  
## Related content

- [KEY_GUID (Transact-SQL)](key-guid-transact-sql.md)
- [CREATE SYMMETRIC KEY (Transact-SQL)](../statements/create-symmetric-key-transact-sql.md)
- [sys.symmetric_keys (Transact-SQL)](../../relational-databases/system-catalog-views/sys-symmetric-keys-transact-sql.md)
- [sys.key_encryptions (Transact-SQL)](../../relational-databases/system-catalog-views/sys-key-encryptions-transact-sql.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
