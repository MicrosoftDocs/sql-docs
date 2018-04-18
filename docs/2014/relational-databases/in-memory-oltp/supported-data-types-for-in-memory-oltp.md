---
title: "Supported Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: a7380ef0-c9d7-49e4-b6de-fad34752b9f3
caps.latest.revision: 20
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Supported Data Types
  The following data types are **supported** in memory-optimized tables and natively compiled stored procedures:  
  
 **Numeric Data Types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|int|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](~/t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)|  
|bigint|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](~/t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)|  
|smallint|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](~/t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)|  
|tinyint|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](~/t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)|  
|decimal|[decimal and numeric &#40;Transact-SQL&#41;](~/t-sql/data-types/decimal-and-numeric-transact-sql.md)|  
|numeric|[decimal and numeric &#40;Transact-SQL&#41;](~/t-sql/data-types/decimal-and-numeric-transact-sql.md)|  
|float|[float and real &#40;Transact-SQL&#41;](~/t-sql/data-types/float-and-real-transact-sql.md)|  
|real|[float and real &#40;Transact-SQL&#41;](~/t-sql/data-types/float-and-real-transact-sql.md)|  
|money|[money and smallmoney &#40;Transact-SQL&#41;](~/t-sql/data-types/money-and-smallmoney-transact-sql.md)|  
|smallmoney|[money and smallmoney &#40;Transact-SQL&#41;](~/t-sql/data-types/money-and-smallmoney-transact-sql.md)|  
  
 **String Data Types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|char(n)|[char and varchar &#40;Transact-SQL&#41;](~/t-sql/data-types/char-and-varchar-transact-sql.md)|  
|varchar(n) <sup>1</sup>|[char and varchar &#40;Transact-SQL&#41;](~/t-sql/data-types/char-and-varchar-transact-sql.md)|  
|nchar(n)|[nchar and nvarchar &#40;Transact-SQL&#41;](~/t-sql/data-types/nchar-and-nvarchar-transact-sql.md)|  
|nvarchar(n) <sup>1</sup>|[nchar and nvarchar &#40;Transact-SQL&#41;](~/t-sql/data-types/nchar-and-nvarchar-transact-sql.md)|  
|sysname|[nchar and nvarchar &#40;Transact-SQL&#41;](~/t-sql/data-types/nchar-and-nvarchar-transact-sql.md)|  
  
 <sup>1</sup> Limitation is 8060 bytes per row total, counting (n) in variable-length types.  
  
 For information about supported collations, see [Collations and Code Pages](../../2014/database-engine/collations-and-code-pages.md).  
  
 **Date and Time Data Types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|date|[date &#40;Transact-SQL&#41;](~/t-sql/data-types/date-transact-sql.md)|  
|time|[time &#40;Transact-SQL&#41;](~/t-sql/data-types/time-transact-sql.md)|  
|datetime|[datetime &#40;Transact-SQL&#41;](~/t-sql/data-types/datetime-transact-sql.md)|  
|datetime2|[datetime2 &#40;Transact-SQL&#41;](~/t-sql/data-types/datetime2-transact-sql.md)|  
|smalldatetime|[smalldatetime &#40;Transact-SQL&#41;](~/t-sql/data-types/smalldatetime-transact-sql.md)|  
  
 **Binary Data Types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|bit|[bit &#40;Transact-SQL&#41;](~/t-sql/data-types/bit-transact-sql.md)|  
|binary(n)|[binary and varbinary &#40;Transact-SQL&#41;](~/t-sql/data-types/binary-and-varbinary-transact-sql.md)|  
|varbinary(n) <sup>1</sup>|[binary and varbinary &#40;Transact-SQL&#41;](~/t-sql/data-types/binary-and-varbinary-transact-sql.md)|  
  
 <sup>1</sup> Limitation is 8060 bytes per row total, counting (n) in variable-length types.  
  
 **Other data types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|uniqueidentifier|[uniqueidentifier &#40;Transact-SQL&#41;](~/t-sql/data-types/uniqueidentifier-transact-sql.md)|  
  
 **Unsupported Data Types**  
  
 The following data types are not supported:  
  
||||  
|-|-|-|  
|DATETIMEOFFSET|GEOGRAPHY|GEOMETRY|  
|HIERARCHYID|Large Objects (LOBs). For example, varchar(max), nvarchar(max), varbinary(max), image, xml, text, and ntext.|ROWVERSION|  
|sql_variant|CLR functions|User-defined types (UDTs)|  
  
## See Also  
 [Transact-SQL Support for In-Memory OLTP](transact-sql-support-for-in-memory-oltp.md)   
 [Implementing LOB Columns in a Memory-Optimized Table](../../2014/database-engine/implementing-lob-columns-in-a-memory-optimized-table.md)   
 [Implementing SQL_VARIANT in a Memory-Optimized Table](../relational-databases/in-memory-oltp/implementing-sql-variant-in-a-memory-optimized-table.md)  
  
  