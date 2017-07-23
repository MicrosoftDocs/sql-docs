---
title: "nchar and nvarchar (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/22/2017
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "nvarchar data type"
  - "nchar data type"
ms.assetid: 81ee5637-ee31-4c4d-96d0-56c26a742354
caps.latest.revision: 44
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# nchar and nvarchar (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Character data types that are either fixed-length, **nchar**, or variable-length, **nvarchar**, Unicode data and use the UNICODE UCS-2 character set.
  
## Arguments  
**nchar** [ ( n ) ]  
Fixed-length Unicode string data. *n* defines the string length and must be a value from 1 through 4,000. The storage size is two times *n* bytes. When the collation code page uses double-byte characters, the storage size is still *n* bytes. Depending on the string, the storage size of *n* bytes can be less than the value specified for *n*. The ISO synonyms for **nchar** are **national char** and **national character**..
  
**nvarchar** [ ( n | **max** ) ]  
Variable-length Unicode string data. *n* defines the string length and can be a value from 1 through 4,000. **max** indicates that the maximum storage size is 2^31-1 bytes (2 GB). The storage size, in bytes, is two times the actual length of data entered + 2 bytes. The ISO synonyms for **nvarchar** are **national char varying** and **national character varying**.
  
## Remarks  
When *n* is not specified in a data definition or variable declaration statement, the default length is 1. When *n* is not specified with the CAST function, the default length is 30.
  
Use **nchar** when the sizes of the column data entries are probably going to be similar.
  
Use **nvarchar** when the sizes of the column data entries are probably going to vary considerably.
  
**sysname** is a system-supplied user-defined data type that is functionally equivalent to **nvarchar(128)**, except that it is not nullable. **sysname** is used to reference database object names.
  
Objects that use **nchar** or **nvarchar** are assigned the default collation of the database unless a specific collation is assigned using the COLLATE clause.
  
SET ANSI_PADDING is always ON for **nchar** and **nvarchar**. SET ANSI_PADDING OFF does not apply to the **nchar** or **nvarchar** data types.
  
Prefix Unicode character string constants with the letter N. Without the N prefix, the string is converted to the default code page of the database. This default code page may not recognize certain characters.
 
> [!NOTE]  
>  When prefixing a string constant with the letter N, the implicit conversion will result in a Unicode string if the constant to convert does not exceed the max length for a Unicode string data type (4,000). Otherwise, the implicit conversion will result in a Unicode large-value (max).
  
> [!WARNING]  
>  Each non-null  **varchar(max)** or **nvarchar(max)** column requires 24 bytes of additional fixed allocation which counts against the 8,060 byte row limit during a sort operation. This can create an implicit limit to the number of non-null **varchar(max)** or **nvarchar(max)** columns that can be created in a table. No special error is provided when the table is created (beyond the usual warning that the maximum row size exceeds the allowed maximum of 8060 bytes) or at the time of data insertion. This large row size can cause errors (such as error 512) during some normal operations, such as a clustered index key update, or sorts of the full column set, which users cannot anticipate until performing an operation.  
  
## Converting Character Data  
For information about converting character data, see [char and varchar &#40;Transact-SQL&#41;](../../t-sql/data-types/char-and-varchar-transact-sql.md).
  
## Examples  
  
```sql
CREATE TABLE dbo.MyTable  
(  
  MyNCharColumn nchar(15)  
,MyNVarCharColumn nvarchar(20)
  
);  
  
GO  
INSERT INTO dbo.MyTable VALUES (N'Test data', N'More test data');  
GO  
SELECT MyNCharColumn, MyNVarCharColumn  
FROM dbo.MyTable;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
MyNCharColumn   MyNVarCharColumn  
--------------- --------------------  
Test data       More test data  
  
(1 row(s) affected)  
```  
  
## See also
[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[COLLATE &#40;Transact-SQL&#41;](http://msdn.microsoft.com/library/4ba6b7d8-114a-4f4e-bb38-fe5697add4e9)  
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)  
[LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)  
[SET ANSI_PADDING &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-padding-transact-sql.md)  
[SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)  
[Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)
  
  
