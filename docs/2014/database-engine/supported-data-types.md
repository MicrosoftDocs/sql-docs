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
|int|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](../Topic/int,%20bigint,%20smallint,%20and%20tinyint%20\(Transact-SQL\).md)|  
|bigint|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](../Topic/int,%20bigint,%20smallint,%20and%20tinyint%20\(Transact-SQL\).md)|  
|smallint|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](../Topic/int,%20bigint,%20smallint,%20and%20tinyint%20\(Transact-SQL\).md)|  
|tinyint|[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](../Topic/int,%20bigint,%20smallint,%20and%20tinyint%20\(Transact-SQL\).md)|  
|decimal|[decimal and numeric &#40;Transact-SQL&#41;](../Topic/decimal%20and%20numeric%20\(Transact-SQL\).md)|  
|numeric|[decimal and numeric &#40;Transact-SQL&#41;](../Topic/decimal%20and%20numeric%20\(Transact-SQL\).md)|  
|float|[float and real &#40;Transact-SQL&#41;](../Topic/float%20and%20real%20\(Transact-SQL\).md)|  
|real|[float and real &#40;Transact-SQL&#41;](../Topic/float%20and%20real%20\(Transact-SQL\).md)|  
|money|[money and smallmoney &#40;Transact-SQL&#41;](../Topic/money%20and%20smallmoney%20\(Transact-SQL\).md)|  
|smallmoney|[money and smallmoney &#40;Transact-SQL&#41;](../Topic/money%20and%20smallmoney%20\(Transact-SQL\).md)|  
  
 **String Data Types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|char(n)|[char and varchar &#40;Transact-SQL&#41;](../Topic/char%20and%20varchar%20\(Transact-SQL\).md)|  
|varchar(n) <sup>1</sup>|[char and varchar &#40;Transact-SQL&#41;](../Topic/char%20and%20varchar%20\(Transact-SQL\).md)|  
|nchar(n)|[nchar and nvarchar &#40;Transact-SQL&#41;](../Topic/nchar%20and%20nvarchar%20\(Transact-SQL\).md)|  
|nvarchar(n) <sup>1</sup>|[nchar and nvarchar &#40;Transact-SQL&#41;](../Topic/nchar%20and%20nvarchar%20\(Transact-SQL\).md)|  
|sysname|[nchar and nvarchar &#40;Transact-SQL&#41;](../Topic/nchar%20and%20nvarchar%20\(Transact-SQL\).md)|  
  
 <sup>1</sup> Limitation is 8060 bytes per row total, counting (n) in variable-length types.  
  
 For information about supported collations, see [Collations and Code Pages](../../2014/database-engine/collations-and-code-pages.md).  
  
 **Date and Time Data Types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|date|[date &#40;Transact-SQL&#41;](../Topic/date%20\(Transact-SQL\).md)|  
|time|[time &#40;Transact-SQL&#41;](../Topic/time%20\(Transact-SQL\).md)|  
|datetime|[datetime &#40;Transact-SQL&#41;](../Topic/datetime%20\(Transact-SQL\).md)|  
|datetime2|[datetime2 &#40;Transact-SQL&#41;](../Topic/datetime2%20\(Transact-SQL\).md)|  
|smalldatetime|[smalldatetime &#40;Transact-SQL&#41;](../Topic/smalldatetime%20\(Transact-SQL\).md)|  
  
 **Binary Data Types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|bit|[bit &#40;Transact-SQL&#41;](../Topic/bit%20\(Transact-SQL\).md)|  
|binary(n)|[binary and varbinary &#40;Transact-SQL&#41;](../Topic/binary%20and%20varbinary%20\(Transact-SQL\).md)|  
|varbinary(n) <sup>1</sup>|[binary and varbinary &#40;Transact-SQL&#41;](../Topic/binary%20and%20varbinary%20\(Transact-SQL\).md)|  
  
 <sup>1</sup> Limitation is 8060 bytes per row total, counting (n) in variable-length types.  
  
 **Other data types**  
  
|Data type|For more information|  
|---------------|--------------------------|  
|uniqueidentifier|[uniqueidentifier &#40;Transact-SQL&#41;](../Topic/uniqueidentifier%20\(Transact-SQL\).md)|  
  
 **Unsupported Data Types**  
  
 The following data types are not supported:  
  
||||  
|-|-|-|  
|DATETIMEOFFSET|GEOGRAPHY|GEOMETRY|  
|HIERARCHYID|Large Objects (LOBs). For example, varchar(max), nvarchar(max), varbinary(max), image, xml, text, and ntext.|ROWVERSION|  
|sql_variant|CLR functions|User-defined types (UDTs)|  
  
## See Also  
 [Transact-SQL Support for In-Memory OLTP](../../2014/database-engine/transact-sql-support-for-in-memory-oltp.md)   
 [Implementing LOB Columns in a Memory-Optimized Table](../../2014/database-engine/implementing-lob-columns-in-a-memory-optimized-table.md)   
 [Implementing SQL_VARIANT in a Memory-Optimized Table](../../2014/database-engine/implementing-sql-variant-in-a-memory-optimized-table.md)  
  
  