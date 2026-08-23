---
title: "SYMKEYPROPERTY (Transact-SQL)"
description: "SYMKEYPROPERTY (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SYMKEYPROPERTY_TSQL"
  - "SYMKEYPROPERTY"
helpviewer_keywords:
  - "SYMKEYPROPERTY"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# SYMKEYPROPERTY (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Returns the algorithm of a symmetric key created from an EKM module.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SYMKEYPROPERTY ( Key_ID , 'algorithm_desc' | 'string_sid' | 'sid' )  
```  
  
## Arguments
 *Key_ID*  
 Is the Key_ID of a symmetric key in the database. To find the Key_ID when you only know the key name, use SYMKEY_ID. *Key_ID* is data type **int**.  
  
 **'**algorithm_desc**'**  
 Specifies that the output returns the algorithm description of the symmetric key. Only available for symmetric keys created from an EKM module.  
  
## Return Types  
 **sql_variant**  
  
## Permissions  
 Requires some permission on the symmetric key and that the caller has not been denied VIEW permission on the symmetric key.  
  
## Examples  
 The following example returns the algorithm of the symmetric key with Key_ID 256.  
  
```sql  
SELECT SYMKEYPROPERTY(256, 'algorithm_desc') AS Algorithm ;  
GO  
```  
  
## Related content

- [ASYMKEY_ID (Transact-SQL)](asymkey-id-transact-sql.md)
- [ALTER SYMMETRIC KEY (Transact-SQL)](../statements/alter-symmetric-key-transact-sql.md)
- [DROP SYMMETRIC KEY (Transact-SQL)](../statements/drop-symmetric-key-transact-sql.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
- [sys.symmetric_keys (Transact-SQL)](../../relational-databases/system-catalog-views/sys-symmetric-keys-transact-sql.md)
- [Security Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)
- [KEY_ID (Transact-SQL)](key-id-transact-sql.md)
- [ASYMKEYPROPERTY (Transact-SQL)](asymkeyproperty-transact-sql.md)
