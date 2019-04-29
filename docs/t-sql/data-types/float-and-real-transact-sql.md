---
title: "float and real (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/22/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "float"
  - "real_TSQL"
  - "real"
  - "float_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "numeric data type, floating point"
  - "float data type"
  - "floating point data [SQL Server]"
  - "real data type"
ms.assetid: 08ea66b7-624e-4d8b-86bc-750ff76cdfc5
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# float and real (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Approximate-number data types for use with floating point numeric data. Floating point data is approximate; therefore, not all values in the data type range can be represented exactly. The ISO synonym for **real** is **float(24)**.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
**float** [ **(**_n_**)** ]
Where *n* is the number of bits that are used to store the mantissa of the **float** number in scientific notation and, therefore, dictates the precision and storage size. If *n* is specified, it must be a value between **1** and **53**. The default value of *n* is **53**.
  
|*n* value|Precision|Storage size|  
|---|---|---|
|**1-24**|7 digits|4 bytes|  
|**25-53**|15 digits|8 bytes|  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] treats *n* as one of two possible values. If **1**<=n<=**24**, *n* is treated as **24**. If **25**<=n<=**53**, *n* is treated as **53**.  
  
The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **float**[**(n)**] data type complies with the ISO standard for all values of *n* from **1** through **53**. The synonym for **double precision** is **float(53)**.
  
## Remarks  
  
|Data type|Range|Storage|  
|---|---|---|
|**float**|- 1.79E+308 to -2.23E-308, 0 and 2.23E-308 to 1.79E+308|Depends on the value of *n*|  
|**real**|- 3.40E + 38 to -1.18E - 38, 0 and 1.18E - 38 to 3.40E + 38|4 Bytes|  
  
##  Converting float and real data  
Values of **float** are truncated when they are converted to any integer type.
  
When you want to convert from **float** or **real** to character data, using the STR string function is usually more useful than CAST( ). This is because STR enables more control over formatting. For more information, see [STR &#40;Transact-SQL&#41;](../../t-sql/functions/str-transact-sql.md) and [Functions &#40;Transact-SQL&#41;](../../t-sql/functions/functions.md).
  
Conversion of **float** values that use scientific notation to **decimal** or **numeric** is restricted to values of precision 17 digits only. Any value < 5E-18 rounds down to 0.
  
## See also
[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)  
[Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)  
[SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)
  
  
