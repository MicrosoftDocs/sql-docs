---
title: "JSON_PATH_EXISTS (Transact-SQL)"
description: "JSON_PATH_EXISTS (Transact-SQL)"
author: "uc-msft"
ms.author: "umajay"
ms.date: 4/26/2022
ms.prod: sql
ms.technology: t-sql
ms.topic: reference
ms.custom:
  - "event-tier1-build-2022"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || >= sql-server-ver16 || >= sql-server-linux-ver16"
---
# JSON_PATH_EXISTS (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Tests whether a specified SQL/JSON path exists in the input JSON string.  
  
## Syntax  
  
```syntaxsql  
JSON_PATH_EXISTS( value_expression, sql_json_path )  
```  
  
## Arguments

*value_expression*
Is a character expression.

*sql_json_path*
Is a valid SQL/JSON path to test in the input.
  
## Return value  

Returns a bit value of 1 or 0 or NULL. Returns NULL if the value_expression or input is a SQL null value. Returns 1 if the given SQL/JSON path exists in the input or returns a non-empty sequence. Returns 0 otherwise.
  
The **JSON_PATH_EXISTS** function does not return errors.
  
## Examples  
  
### Example 1

The following example returns 1 since the input JSON string contains the specified SQL/JSON path.
  
```sql
DECLARE @jsonInfo NVARCHAR(MAX)

SET @jsonInfo=N'{"info":{"address":[{"town":"Paris"},{"town":"London"}]}}';

SELECT JSON_PATH_EXISTS(@jsonInfo,'$.info.address'); -- 1
```  
  
### Example 2

The following example returns 0 since the input JSON string contains the specified SQL/JSON path.  
  
```sql  
DECLARE @jsonInfo NVARCHAR(MAX)

SET @jsonInfo=N'{"info":{"address":[{"town":"Paris"},{"town":"London"}]}}';

SELECT JSON_PATH_EXISTS(@jsonInfo,'$.info.addresses'); -- 0 
```

## See also

[JSON Data &#40;SQL Server&#41;](../../relational-databases/json/json-data-sql-server.md) 
