---
title: "KEY_GUID (Transact-SQL)"
description: "KEY_GUID (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "Key_GUID_TSQL"
  - "Key_GUID"
helpviewer_keywords:
  - "symmetric keys [SQL Server], GUIDs"
  - "KEY_GUID function"
  - "GUIDs [SQL Server]"
dev_langs:
  - "TSQL"
---
# KEY_GUID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the GUID of a symmetric key in the database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
Key_GUID( 'Key_Name' )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 **'** *Key_Name* **'**  
 The name of a symmetric key in the database.  
  
## Return Types  
 **uniqueidentifier**  
  
## Remarks  
 If an identity value was specified when the key was created, its GUID is an MD5 hash of that identity value. If no identity value was specified, the server generated the GUID.  
  
 If the key is a temporary key, the key name must start with a number sign (#).  
  
## Permissions  
 Because temporary keys are only available in the session in which they are created, no permissions are required to access them. To access a key that is not temporary, the caller requires some permission on the key and must not have been denied VIEW permission on the key.  
  
## Examples  
 The following example returns the GUID of a symmetric key called `ABerglundKey1`.  
  
```sql  
SELECT Key_GUID('ABerglundKey1');  
```  
  
## See Also  
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [sys.symmetric_keys &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-symmetric-keys-transact-sql.md)   
 [sys.key_encryptions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-key-encryptions-transact-sql.md)  
  
  
