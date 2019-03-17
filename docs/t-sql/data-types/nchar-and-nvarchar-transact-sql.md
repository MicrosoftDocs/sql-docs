---
title: "nchar and nvarchar (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/22/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "nvarchar data type"
  - "nchar data type"
ms.assetid: 81ee5637-ee31-4c4d-96d0-56c26a742354
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# nchar and nvarchar (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Character data types that are either fixed-length, **nchar**, or variable-length, **nvarchar**. Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], when a [Supplementary Character (SC)](../../relational-databases/collations/collation-and-unicode-support.md#Supplementary_Characters) enabled collation is used, these data types store the full range of [Unicode](../../relational-databases/collations/collation-and-unicode-support.md#Unicode_Defn) character data and use the [UTF-16](https://www.wikipedia.org/wiki/UTF-16) character encoding. If a non-SC collation is specified, then these data types store only the subset of character data supported by the [UCS-2](https://www.wikipedia.org/wiki/Universal_Coded_Character_Set#Encoding_forms) character encoding.
  
## Arguments  
**nchar** [ ( n ) ]  
Fixed-length string data. *n* defines the string length in byte-pairs and must be a value from 1 through 4,000. The storage size is two times *n* bytes. For [UCS-2](https://www.wikipedia.org/wiki/UTF-16#U+0000_to_U+D7FF_and_U+E000_to_U+FFFF) encoding, the storage size is two times *n* bytes and the number of characters that can be stored is also *n*. For UTF-16 encoding, the storage size is still two times *n* bytes but the number of characters that can be stored may be smaller than *n* because Supplementary Characters use two byte-pairs (also called [surrogate-pair](https://www.wikipedia.org/wiki/UTF-16#U+010000_to_U+10FFFF)). The ISO synonyms for **nchar** are **national char** and **national character**.
  
**nvarchar** [ ( n | **max** ) ]  
Variable-length string data. *n* defines the string length in byte-pairs and can be a value from 1 through 4,000. **max** indicates that the maximum storage size is 2^30-1 characters (2 GB). The storage size is two times *n* bytes + 2 bytes. For [UCS-2](https://www.wikipedia.org/wiki/UTF-16#U+0000_to_U+D7FF_and_U+E000_to_U+FFFF) encoding, the storage size is two times *n* bytes + 2 bytes and the number of characters that can be stored is also *n*. For UTF-16 encoding, the storage size is still two times *n* bytes + 2 bytes but the number of characters that can be stored may be smaller than *n* because Supplementary Characters use two byte-pairs (also called [surrogate-pair](https://www.wikipedia.org/wiki/UTF-16#U+010000_to_U+10FFFF)). The ISO synonyms for **nvarchar** are **national char varying** and **national character varying**.
  
## Remarks  
When *n* is not specified in a data definition or variable declaration statement, the default length is 1. When *n* is not specified with the CAST function, the default length is 30.

If you use **nchar** or **nvarchar**, we recommend to:
- Use **nchar** when the sizes of the column data entries are consistent.  
- Use **nvarchar** when the sizes of the column data entries vary considerably.  
- Use **nvarchar(max)** when the sizes of the column data entries vary considerably, and the string length might exceed 4,000 byte-pairs.  
  
**sysname** is a system-supplied user-defined data type that is functionally equivalent to **nvarchar(128)**, except that it is not nullable. **sysname** is used to reference database object names.
  
Objects that use **nchar** or **nvarchar** are assigned the default collation of the database unless a specific collation is assigned using the COLLATE clause.
  
SET ANSI_PADDING is always ON for **nchar** and **nvarchar**. SET ANSI_PADDING OFF does not apply to the **nchar** or **nvarchar** data types.
  
Prefix a Unicode character string constants with the letter N to signal UCS-2 or UTF-16 input, depending on whether an SC collation is used or not. Without the N prefix, the string is converted to the default code page of the database that may not recognize certain characters. Starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)], when a UTF-8 enabled collation is used, the default code page is capable of storing UNICODE UTF-8 character set. 
 
> [!NOTE]  
> When prefixing a string constant with the letter N, the implicit conversion will result in a UCS-2 or UTF-16 string if the constant to convert does not exceed the max length for the nvarchar string data type (4,000). Otherwise, the implicit conversion will result in a large-value nvarchar(max).
  
> [!WARNING]  
> Each non-null **varchar(max)** or **nvarchar(max)** column requires 24 bytes of additional fixed allocation, which counts against the 8,060-byte row limit during a sort operation. These additional bytes can create an implicit limit to the number of non-null **varchar(max)** or **nvarchar(max)** columns in a table. No special error is provided when the table is created (beyond the usual warning that the maximum row size exceeds the allowed maximum of 8,060 bytes) or at the time of data insertion. This large row size can cause errors (such as error 512) that users may not anticipate during some normal operations.  Two examples of operations are a clustered index key update, or sorts of the full column set.
  
## Converting Character Data  
For information about converting character data, see [char and varchar &#40;Transact-SQL&#41;](../../t-sql/data-types/char-and-varchar-transact-sql.md).
  
## See also
[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[COLLATE &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/4ba6b7d8-114a-4f4e-bb38-fe5697add4e9)  
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)  
[LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)  
[SET ANSI_PADDING &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-padding-transact-sql.md)  
[SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)    
[Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)     
[Single-Byte and Multibyte Character Sets](/cpp/c-runtime-library/single-byte-and-multibyte-character-sets)  
  
