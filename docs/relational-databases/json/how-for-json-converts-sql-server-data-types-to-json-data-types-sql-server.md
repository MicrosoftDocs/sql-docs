---
title: "How FOR JSON converts SQL Server data types to JSON data types (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "07/07/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "FOR JSON, data type conversion"
ms.assetid: da356f06-efd0-4ea3-8157-77395bf790d7
author: "jovanpop-msft"
ms.author: "jovanpop"
ms.reviewer: douglasl
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# How FOR JSON converts SQL Server data types to JSON data types (SQL Server)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

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

## Learn more about JSON in SQL Server and Azure SQL Database  
  
### Microsoft videos

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [SQL Server 2016 and JSON Support](https://channel9.msdn.com/Shows/Data-Exposed/SQL-Server-2016-and-JSON-Support)

-   [Using JSON in SQL Server 2016 and Azure SQL Database](https://channel9.msdn.com/Shows/Data-Exposed/Using-JSON-in-SQL-Server-2016-and-Azure-SQL-Database)

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven/SQLServer2016/JSON-as-a-bridge-betwen-NoSQL-and-relational-worlds)
  
## See Also  
 [Format Query Results as JSON with FOR JSON &#40;SQL Server&#41;](../../relational-databases/json/format-query-results-as-json-with-for-json-sql-server.md)  
  
  
