---
title: "ntext, text, and image (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "7/22/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ntext_TSQL"
  - "ntext"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "text data type, about text data type"
  - "text [SQL Server], data types"
  - "ntext data type"
  - "ntext data type, about ntext data type"
  - "image data type, about image data type"
ms.assetid: b0d8769c-7598-4f97-8162-ace5f182b5bc
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# ntext, text, and image (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Fixed and variable-length data types for storing large non-Unicode and Unicode character and binary data. Unicode data uses the UNICODE UCS-2 character set.
  
>**IMPORTANT!**  **ntext**, **text**, and **image** data types will be removed in a future version of SQL Server. Avoid using these data types in new development work, and plan to modify applications that currently use them. Use [nvarchar(max)](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md), [varchar(max)](../../t-sql/data-types/char-and-varchar-transact-sql.md), and [varbinary(max)](../../t-sql/data-types/binary-and-varbinary-transact-sql.md) instead.  
  
  
## Arguments  
**ntext**  
Variable-length Unicode data with a maximum string length of 2^30 - 1 (1,073,741,823) bytes. Storage size, in bytes, is two times the string length that is entered. The ISO synonym for **ntext** is **national text**.
  
**text**  
Variable-length non-Unicode data in the code page of the server and with a maximum string length of 2^31-1 (2,147,483,647). When the server code page uses double-byte characters, the storage is still 2,147,483,647 bytes. Depending on the character string, the storage size may be less than 2,147,483,647 bytes.
  
**image**  
Variable-length binary data from 0 through 2^31-1 (2,147,483,647) bytes.
  
## Remarks  
The following functions and statements can be used with **ntext**, **text**, or **image** data.
  
|Functions|Statements|  
|---|---|
|[DATALENGTH &#40;Transact-SQL&#41;](../../t-sql/functions/datalength-transact-sql.md)|[READTEXT &#40;Transact-SQL&#41;](../../t-sql/queries/readtext-transact-sql.md)|  
|[PATINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/patindex-transact-sql.md)|[SET TEXTSIZE &#40;Transact-SQL&#41;](../../t-sql/statements/set-textsize-transact-sql.md)|  
|[SUBSTRING &#40;Transact-SQL&#41;](../../t-sql/functions/substring-transact-sql.md)|[UPDATETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/updatetext-transact-sql.md)|  
|[TEXTPTR &#40;Transact-SQL&#41;](../../t-sql/functions/text-and-image-functions-textptr-transact-sql.md)|[WRITETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/writetext-transact-sql.md)|  
|[TEXTVALID &#40;Transact-SQL&#41;](../../t-sql/functions/text-and-image-functions-textvalid-transact-sql.md)||  
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)  
[SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)  
[Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)

