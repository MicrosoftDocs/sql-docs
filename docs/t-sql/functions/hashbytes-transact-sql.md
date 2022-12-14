---
title: "HASHBYTES (Transact-SQL)"
description: "HASHBYTES (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "07/29/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "HASHBYTES_TSQL"
  - "HASHBYTES"
helpviewer_keywords:
  - "hash input"
  - "HASHBYTES"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# HASHBYTES (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the MD2, MD4, MD5, SHA, SHA1, or SHA2 hash of its input in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)   
  
## Syntax  
  
```syntaxsql
HASHBYTES ( '<algorithm>', { @input | 'input' } )  
  
<algorithm>::= MD2 | MD4 | MD5 | SHA | SHA1 | SHA2_256 | SHA2_512   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

`<algorithm>`  
Identifies the hashing algorithm to be used to hash the input. This is a required argument with no default. The single quotation marks are required. Beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], all algorithms other than SHA2_256, and SHA2_512 are deprecated.  
  
`@input`  
Specifies a variable containing the data to be hashed. `@input` is **varchar**, **nvarchar**, or **varbinary**.  
  
'*input*'  
Specifies an expression that evaluates to a character or binary string to be hashed.  
  
 The output conforms to the algorithm standard: 128 bits (16 bytes) for MD2, MD4, and MD5; 160 bits (20 bytes) for SHA and SHA1; 256 bits (32 bytes) for SHA2_256, and 512 bits (64 bytes) for SHA2_512.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later
  
 For [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and earlier, allowed input values are limited to 8000 bytes.  
  
## Return Value  
 **varbinary** (maximum 8000 bytes)  

## Remarks  
Consider using `CHECKSUM` or `BINARY_CHECKSUM` as alternatives to compute a hash value.

The MD2, MD4, MD5, SHA, and SHA1 algorithms are deprecated starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]. Use SHA2_256 or SHA2_512 instead. Older algorithms will continue working, but they will raise a deprecation event.

## Examples  
### Return the hash of a variable  
 The following example returns the `SHA2_256` hash of the **nvarchar** data stored in variable `@HashThis`.  
  
```sql  
DECLARE @HashThis NVARCHAR(32);  
SET @HashThis = CONVERT(NVARCHAR(32),'dslfdkjLK85kldhnv$n000#knf');  
SELECT HASHBYTES('SHA2_256', @HashThis);  
```  
  
### Return the hash of a table column  
 The following example returns the SHA2_256 hash of the values in column `c1` in the table `Test1`.  
  
```sql  
CREATE TABLE dbo.Test1 (c1 NVARCHAR(32));  
INSERT dbo.Test1 VALUES ('This is a test.');  
INSERT dbo.Test1 VALUES ('This is test 2.');  
SELECT HASHBYTES('SHA2_256', c1) FROM dbo.Test1;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
-------------------------------------------  
0x741238C01D9DB821CF171BF61D72260B998F7C7881D90091099945E0B9E0C2E3 
0x91DDCC41B761ACA928C62F7B0DA61DC763255E8247E0BD8DCE6B22205197154D  
(2 row(s) affected)  
```  
  
## See Also  
[Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)
[CHECKSUM_AGG &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-agg-transact-sql.md)
[CHECKSUM &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-transact-sql.md)
[BINARY_CHECKSUM  &#40;Transact-SQL&#41;](../../t-sql/functions/binary-checksum-transact-sql.md)
