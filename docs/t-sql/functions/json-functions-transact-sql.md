---
title: "JSON Functions (Transact-SQL)"
description: "JSON Functions (Transact-SQL)"
author: "jovanpop-msft"
ms.author: "jovanpop"
ms.date: 06/03/2020
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
helpviewer_keywords:
  - "JSON functions"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017"
---
# JSON Functions (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

Use the functions described on the pages in this section to validate or change JSON text or to extract simple or complex values.  
  
|Function|Description|  
|--------------|-----------------|  
|[ISJSON](../../t-sql/functions/isjson-transact-sql.md)|Tests whether a string contains valid JSON.|  
|[JSON_VALUE](../../t-sql/functions/json-value-transact-sql.md)|Extracts a scalar value from a JSON string.|  
|[JSON_QUERY](../../t-sql/functions/json-query-transact-sql.md)|Extracts an object or an array from a JSON string.|  
|[JSON_MODIFY](../../t-sql/functions/json-modify-transact-sql.md)|Updates the value of a property in a JSON string and returns the updated JSON string.|
|[JSON_PATH_EXISTS](../../t-sql/functions/json-path-exists-transact-sql.md)|Tests whether a specified SQL/JSON path exists in the input JSON string.|


 For more info about the built-in support for JSON in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [JSON Data &#40;SQL Server&#41;](../../relational-databases/json/json-data-sql-server.md).  

## See Also

 - [Validate, Query, and Change JSON Data with Built-in Functions &#40;SQL Server&#41;](../../relational-databases/json/validate-query-and-change-json-data-with-built-in-functions-sql-server.md)
 - [JSON Path Expressions &#40;SQL Server&#41;](../../relational-databases/json/json-path-expressions-sql-server.md)
 - [JSON Data &#40;SQL Server&#41;](../../relational-databases/json/json-data-sql-server.md)  
