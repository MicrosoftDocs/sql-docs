---
title: "How FOR JSON Converts SQL Server Data Types to JSON Data Types"
description: "The FOR JSON clause uses the following rules to convert SQL Server data types to JSON types in the JSON output."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, umajay
ms.date: 07/23/2025
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "FOR JSON, data type conversion"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# How FOR JSON converts SQL Server data types to JSON data types

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-serverless-pool-only-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-svrless-only-fabricse-fabricdw-fabricsqldb.md)]

  The `FOR JSON` clause uses the following rules to convert SQL Server data types to JSON types in the JSON output.

|Category|SQL Server data type|JSON data type|  
|--------------|--------------|---------------|  
|Character & string types|**char**, **nchar**, **varchar**, **nvarchar**|string|  
|Numeric types|**int**, **bigint**, **float**, **decimal**, **numeric**|number|  
|Bit type|**bit**|**Boolean** (true or false)|  
|Date & time types|**date**, **datetime**, **datetime2**, **time**, **datetimeoffset**|string|  
|Binary types|**varbinary**, **binary**, **image**, **timestamp**/**rowversion**|BASE64-encoded string|
|CLR types|**geometry**, **geography**, other CLR types|Not supported. These types return an error.<br /><br /> In the `SELECT` statement, use `CAST` or `CONVERT`, or use a CLR property or method, to convert the source data to a SQL Server data type that can be converted successfully to a JSON type. For example, use `STAsText()` for the geometry type, or use `ToString()` for any CLR type. The type of the JSON output value is then derived from the return type of the conversion that you apply in the `SELECT` statement.|  
|Other types|**uniqueidentifier**, **money**|string|

## Learn more about JSON in the SQL Database Engine

For a visual introduction to the built-in JSON support, see the following videos:

- [JSON as a bridge between NoSQL and relational worlds](/shows/datadriven-sqlserver2016/json-as-bridge-betwen-nosql-relational-worlds)

## Related content

- [Format query results as JSON with FOR JSON](format-query-results-as-json-with-for-json-sql-server.md)
