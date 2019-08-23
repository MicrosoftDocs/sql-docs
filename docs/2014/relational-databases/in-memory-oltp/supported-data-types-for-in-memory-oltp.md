---
title: "Supported Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: a7380ef0-c9d7-49e4-b6de-fad34752b9f3
author: MightyPen
ms.author: genemi
manager: craigg
---
# Supported Data Types
  The following data types are **supported** in memory-optimized tables and natively compiled stored procedures:  
  
 **Numeric Data Types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|int|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](/sql/t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql)|  
|bigint|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](/sql/t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql)|  
|smallint|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](/sql/t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql)|  
|tinyint|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](/sql/t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql)|  
|decimal|[decimal and numeric &#40;Transact-SQL&#41;](/sql/t-sql/data-types/decimal-and-numeric-transact-sql)|  
|numeric|[decimal and numeric &#40;Transact-SQL&#41;](/sql/t-sql/data-types/decimal-and-numeric-transact-sql)|  
|float|[float and real &#40;Transact-SQL&#41;](/sql/t-sql/data-types/float-and-real-transact-sql)|  
|real|[float and real &#40;Transact-SQL&#41;](/sql/t-sql/data-types/float-and-real-transact-sql)|  
|money|[money and smallmoney &#40;Transact-SQL&#41;](/sql/t-sql/data-types/money-and-smallmoney-transact-sql)|  
|smallmoney|[money and smallmoney &#40;Transact-SQL&#41;](/sql/t-sql/data-types/money-and-smallmoney-transact-sql)|  
  
 **String Data Types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|char(n)|[char and varchar &#40;Transact-SQL&#41;](/sql/t-sql/data-types/char-and-varchar-transact-sql)|  
|varchar(n) <sup>1</sup>|[char and varchar &#40;Transact-SQL&#41;](/sql/t-sql/data-types/char-and-varchar-transact-sql)|  
|nchar(n)|[nchar and nvarchar &#40;Transact-SQL&#41;](/sql/t-sql/data-types/nchar-and-nvarchar-transact-sql)|  
|nvarchar(n) <sup>1</sup>|[nchar and nvarchar &#40;Transact-SQL&#41;](/sql/t-sql/data-types/nchar-and-nvarchar-transact-sql)|  
|sysname|[nchar and nvarchar &#40;Transact-SQL&#41;](/sql/t-sql/data-types/nchar-and-nvarchar-transact-sql)|  
  
 <sup>1</sup> Limitation is 8060 bytes per row total, counting (n) in variable-length types.  
  
 For information about supported collations, see [Collations and Code Pages](../../database-engine/collations-and-code-pages.md).  
  
 **Date and Time Data Types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|date|[date &#40;Transact-SQL&#41;](/sql/t-sql/data-types/date-transact-sql)|  
|time|[time &#40;Transact-SQL&#41;](/sql/t-sql/data-types/time-transact-sql)|  
|datetime|[datetime &#40;Transact-SQL&#41;](/sql/t-sql/data-types/datetime-transact-sql)|  
|datetime2|[datetime2 &#40;Transact-SQL&#41;](/sql/t-sql/data-types/datetime2-transact-sql)|  
|smalldatetime|[smalldatetime &#40;Transact-SQL&#41;](/sql/t-sql/data-types/smalldatetime-transact-sql)|  
  
 **Binary Data Types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|bit|[bit &#40;Transact-SQL&#41;](/sql/t-sql/data-types/bit-transact-sql)|  
|binary(n)|[binary and varbinary &#40;Transact-SQL&#41;](/sql/t-sql/data-types/binary-and-varbinary-transact-sql)|  
|varbinary(n) <sup>1</sup>|[binary and varbinary &#40;Transact-SQL&#41;](/sql/t-sql/data-types/binary-and-varbinary-transact-sql)|  
  
 <sup>1</sup> Limitation is 8060 bytes per row total, counting (n) in variable-length types.  
  
 **Other data types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|uniqueidentifier|[uniqueidentifier &#40;Transact-SQL&#41;](/sql/t-sql/data-types/uniqueidentifier-transact-sql)|  
  
 **Unsupported Data Types**  
  
 The following data types are not supported:  
  
||||  
|-|-|-|  
|DATETIMEOFFSET|GEOGRAPHY|GEOMETRY|  
|HIERARCHYID|Large Objects (LOBs). For example, varchar(max), nvarchar(max), varbinary(max), image, xml, text, and ntext.|ROWVERSION|  
|sql_variant|CLR functions|User-defined types (UDTs)|  
  
## See Also  
 [Transact-SQL Support for In-Memory OLTP](transact-sql-support-for-in-memory-oltp.md)   
 [Implementing LOB Columns in a Memory-Optimized Table](../../database-engine/implementing-lob-columns-in-a-memory-optimized-table.md)   
 [Implementing SQL_VARIANT in a Memory-Optimized Table](implementing-sql-variant-in-a-memory-optimized-table.md)  
  
  
