---
title: "JSON functions (Transact-SQL)"
description: Use JSON functions to validate or change JSON text, or to extract simple or complex values.
author: "jovanpop-msft"
ms.author: "jovanpop"
ms.reviewer: randolphwest
ms.date: 05/06/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "JSON functions"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017"
---
# JSON functions (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

Use the functions described on the pages in this section to validate or change JSON text or to extract simple or complex values.

| Function | Description |
| --- | --- |
| [ISJSON](isjson-transact-sql.md) | Tests whether a string contains valid JSON. |
| [JSON_VALUE](json-value-transact-sql.md) | Extracts a scalar value from a JSON string. |
| [JSON_QUERY](json-query-transact-sql.md) | Extracts an object or an array from a JSON string. |
| [JSON_MODIFY](json-modify-transact-sql.md) | Updates the value of a property in a JSON string and returns the updated JSON string. |
| [JSON_PATH_EXISTS](json-path-exists-transact-sql.md) | Tests whether a specified SQL/JSON path exists in the input JSON string. |

For more info about the built-in support for JSON in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md).

## Related content

- [Validate, query, and change JSON data with built-in functions (SQL Server)](../../relational-databases/json/validate-query-and-change-json-data-with-built-in-functions-sql-server.md)
- [JSON Path Expressions (SQL Server)](../../relational-databases/json/json-path-expressions-sql-server.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
