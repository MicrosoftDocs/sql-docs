---
title: "ASYMKEY_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "AsymKey_ID"
  - "ASYMKEY_ID_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "asymmetric keys [SQL Server], AsymKey_ID"
  - "ASYMKEY_ID function"
  - "encryption [SQL Server], asymmetric keys"
  - "identification numbers [SQL Server], asymmetric keys"
  - "IDs [SQL Server], asymmetric keys"
  - "cryptography [SQL Server], asymmetric keys"
ms.assetid: d697daf8-2106-4ebb-b09a-ca0be465d747
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# ASYMKEY_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the ID of an asymmetric key.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
ASYMKEY_ID ( 'Asym_Key_Name' )  
```  
  
## Arguments  
*Asym_Key_Name*  
The name of an asymmetric key in the database.
  
## Return types
 **int**  
  
## Permissions  
Requires appropriate permission(s) on the asymmetric key, and requires that the caller has not been denied VIEW permission on the asymmetric key. See [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md) for more information about asymmetric key permissions.
  
## Examples  
This example returns the ID of asymmetric key `ABerglundKey11`.
  
```sql
SELECT ASYMKEY_ID('ABerglundKey11');  
GO  
```  
  
## See also
[CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)  
[ALTER ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-asymmetric-key-transact-sql.md)  
[DROP ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-asymmetric-key-transact-sql.md)  
[SIGNBYASYMKEY &#40;Transact-SQL&#41;](../../t-sql/functions/signbyasymkey-transact-sql.md)  
[VERIFYSIGNEDBYASYMKEY &#40;Transact-SQL&#41;](../../t-sql/functions/verifysignedbyasymkey-transact-sql.md)  
[Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
[sys.asymmetric_keys &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-asymmetric-keys-transact-sql.md)  
[Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)  
[ASYMKEYPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/asymkeyproperty-transact-sql.md)  
[KEY_ID &#40;Transact-SQL&#41;](../../t-sql/functions/key-id-transact-sql.md)
  
  
