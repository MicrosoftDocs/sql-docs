---
title: "How FOR JSON converts SQL Server data types to JSON data types (SQL Server) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "07/07/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-json"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "FOR JSON, data type conversion"
ms.assetid: da356f06-efd0-4ea3-8157-77395bf790d7
caps.latest.revision: 11
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# How FOR JSON converts SQL Server data types to JSON data types (SQL Server)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  The **FOR JSON** clause uses the following rules to convert SQL Server data types to JSON types in the JSON output.  
  
|Category|SQL Server data type|JSON data type|  
|--------------|--------------|---------------|  
|Character & string types|char, nchar, varchar, nvarchar|string|  
|Numeric types|int, bigint, float, decimal, numeric|number|  
|Bit type|bit|Boolean (true or false)|  
|Date & time types|date, datetime, datetime2, time, datetimeoffset|string|  
|Binary types|varbinary, binary, image, timestamp, rowversion|BASE64-encoded string|  
|CLR types|geometry, geography, other CLR types|Not supported. These types return an error.<br /><br /> In the SELECT statement, use CAST or CONVERT, or use a CLR property or method, to convert the source data to a SQL Server data type that can be converted successfully to a JSON type. For example, use **STAsText()** for the geometry type, or use **ToString()** for any CLR type. The type of the JSON output value is then derived from the return type of the conversion that you apply in the SELECT statement.|  
|Other types|uniqueidentifier, money|string|  

## Learn more about the built-in JSON support in SQL Server  
For lots of specific solutions, use cases, and recommendations, see the [blog posts about the built-in JSON support](http://blogs.msdn.com/b/sqlserverstorageengine/archive/tags/json/) in SQL Server and in Azure SQL Database by Microsoft Program Manager Jovan Popovic.
  
## See Also  
 [Format Query Results as JSON with FOR JSON &#40;SQL Server&#41;](../../relational-databases/json/format-query-results-as-json-with-for-json-sql-server.md)  
  
  
