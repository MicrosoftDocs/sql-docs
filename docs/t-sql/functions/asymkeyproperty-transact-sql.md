---
title: "ASYMKEYPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ASYMKEYPROPERTY_TSQL"
  - "ASYMKEYPROPERTY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ASYMKEYPROPERTY"
ms.assetid: a30581f2-e1b1-4996-93e6-527ff96b7c42
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# ASYMKEYPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns the properties of an asymmetric key.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
ASYMKEYPROPERTY (Key_ID , 'algorithm_desc' | 'string_sid' | 'sid')  
```  
  
## Arguments  
*Key_ID*  
The Key_ID of an asymmetric key in the database. If you only know the key name, use ASYMKEY_ID to find the Key_ID. *Key_ID* has data type **int**.
  
**'**algorithm_desc**'**  
Specifies that the output returns the algorithm description of the asymmetric key. Only available for asymmetric keys created from an EKM module.
  
**'**string_sid**'**  
Specifies that the output returns the SID of the asymmetric key, in **nvarchar()** format.
  
**'**sid**'**  
Specifies that the output returns the SID of the asymmetric key in binary format.
  
## Return types  
**sql_variant**
  
## Permissions  
Requires appropriate permission(s) on the asymmetric key, and requires that the caller has not been denied VIEW permission on the asymmetric key. See [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md) for more information about asymmetric key permissions.
  
## Examples  
The following example returns the properties of the asymmetric key of Key_ID 256.
  
```sql
SELECT   
ASYMKEYPROPERTY(256, 'algorithm_desc') AS Algorithm,  
ASYMKEYPROPERTY(256, 'string_sid') AS String_SID,  
ASYMKEYPROPERTY(256, 'sid') AS SID ;  
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
[ASYMKEY_ID &#40;Transact-SQL&#41;](../../t-sql/functions/asymkey-id-transact-sql.md)  
[SYMKEYPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/symkeyproperty-transact-sql.md)
  
  
