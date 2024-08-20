---
title: "JSON Functions (Transact-SQL)"
description: Use JSON functions to validate or change JSON text, or to extract simple or complex values.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, randolphwest
ms.date: 08/20/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "JSON functions"
dev_langs:
  - "TSQL"
ms.custom:
  - build-2024
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# JSON functions (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw.md)]

Use the functions described in this article support querying, manipulating, and construction JSON data. Use JSON functions to validate or change JSON documents, or to extract basic or complex values.

|Function|Description|  
|--------------|-----------------|  
| [ISJSON](isjson-transact-sql.md) | Tests whether a string contains valid JSON. |  
| [JSON_ARRAY](json-array-transact-sql.md) | Constructs JSON array text from zero or more expressions. |
| [JSON_ARRAYAGG](json-arrayagg-transact-sql.md) | Constructs a JSON array from an aggregation of SQL data or columns. |
| [JSON_MODIFY](json-modify-transact-sql.md) | Updates the value of a property in a JSON string and returns the updated JSON string. |
| [JSON_OBJECT](json-object-transact-sql.md) | Constructs JSON object text from zero or more expressions. |
| [JSON_OBJECTAGG](json-objectagg-transact-sql.md) | Constructs a JSON object from an aggregation of SQL data or columns. |
| [JSON_PATH_EXISTS](json-path-exists-transact-sql.md) | Tests whether a specified SQL/JSON path exists in the input JSON string. |
| [JSON_QUERY](json-query-transact-sql.md) | Extracts an object or an array from a JSON string. |  
| [JSON_VALUE](json-value-transact-sql.md) | Extracts a scalar value from a JSON string. |
| [OPENJSON](openjson-transact-sql.md) | Parses JSON text and returns objects and properties from the JSON input as rows and columns. |

For more info about the built-in support for JSON in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md).  

## Related content

- [Validate, query, and change JSON data with built-in functions (SQL Server)](../../relational-databases/json/validate-query-and-change-json-data-with-built-in-functions-sql-server.md)
- [JSON Path Expressions (SQL Server)](../../relational-databases/json/json-path-expressions-sql-server.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
