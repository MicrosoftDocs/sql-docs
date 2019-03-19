---
title: "HASHBYTES (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "HASHBYTES_TSQL"
  - "HASHBYTES"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "hash input"
  - "HASHBYTES"
ms.assetid: 0ea6a4d1-313e-4f70-b939-dd2cd570f6d6
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# HASHBYTES (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the MD2, MD4, MD5, SHA, SHA1, or SHA2 hash of its input in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
HASHBYTES ( '<algorithm>', { @input | 'input' } )  
  
<algorithm>::= MD2 | MD4 | MD5 | SHA | SHA1 | SHA2_256 | SHA2_512   
```  
  
## Arguments  
 **'**\<algorithm>**'**  
 Identifies the hashing algorithm to be used to hash the input. This is a required argument with no default. The single quotation marks are required. Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], all algorithms other than SHA2_256, and SHA2_512 are deprecated. Older algorithms (not recommended) will continue working, but they will raise a deprecation event.  
  
 **@input**  
 Specifies a variable containing the data to be hashed. **@input** is **varchar**, **nvarchar**, or **varbinary**.  
  
 **'** *input* **'**  
 Specifies an expression that evaluates to a character or binary string to be hashed.  
  
 The output conforms to the algorithm standard: 128 bits (16 bytes) for MD2, MD4, and MD5; 160 bits (20 bytes) for SHA and SHA1; 256 bits (32 bytes) for SHA2_256, and 512 bits (64 bytes) for SHA2_512.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
 For [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and earlier, allowed input values are limited to 8000 bytes.  
  
## Return Value  
 **varbinary** (maximum 8000 bytes)  

## Remarks  
Consider using `CHECKSUM` or `BINARY_CHECKSUM` as alternatives to compute a hash value.

The MD2, MD4, MD5, SHA, and SHA1 algorithms are not available under compatibility level 130 and above. Use SHA2_256 or SHA2_512 instead.

## Examples  
### Return the hash of a variable  
 The following example returns the `SHA1` hash of the **nvarchar** data stored in variable `@HashThis`.  
  
```sql  
DECLARE @HashThis nvarchar(4000);  
SET @HashThis = CONVERT(nvarchar(4000),'dslfdkjLK85kldhnv$n000#knf');  
SELECT HASHBYTES('SHA1', @HashThis);  
```  
  
### Return the hash of a table column  
 The following example returns the SHA1 hash of the values in column `c1` in the table `Test1`.  
  
```sql  
CREATE TABLE dbo.Test1 (c1 nvarchar(50));  
INSERT dbo.Test1 VALUES ('This is a test.');  
INSERT dbo.Test1 VALUES ('This is test 2.');  
SELECT HASHBYTES('SHA1', c1) FROM dbo.Test1;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
-------------------------------------------  
0x0E7AAB0B4FF0FD2DFB4F0233E2EE7A26CD08F173  
0xF643A82F948DEFB922B12E50B950CEE130A934D6  
  
(2 row(s) affected)  
```  
  
## See Also  
[Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)  
[CHECKSUM_AGG &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-agg-transact-sql.md)  
[CHECKSUM &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-transact-sql.md)  
[BINARY_CHECKSUM  &#40;Transact-SQL&#41;](../../t-sql/functions/binary-checksum-transact-sql.md)  
  
