---
title: "JSON_OBJECT (Transact-SQL)"
description: "JSON_OBJECT (Transact-SQL)"
author: "uc-msft"
ms.author: "umajay"
ms.date: 05/24/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "JSON_OBJECT"
  - "JSON_OBJECT_TSQL"
helpviewer_keywords:
  - "JSON_OBJECT function"
  - "JSON, validating"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || >= sql-server-ver16 || >= sql-server-linux-ver16"
---
# JSON_OBJECT (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Constructs JSON object text from zero or more expressions.

## Syntax  
  
```syntaxsql  
JSON_OBJECT ( [ <json_key_value> [,...n] ] [ json_null_clause ] )

<json_key_value> ::= json_key_name : value_expression

<json_null_clause> ::=
	  NULL ON NULL
	| ABSENT ON NULL
```
  
## Arguments

 *json_key_name*
 Is a character expression that defines the value of the JSON key name.

 *value_expression*
 Is an expression that defines the value of the JSON key.

*json_null_clause* can be used to control the behavior of JSON_OBJECT function when value_expression is NULL. The option NULL ON NULL converts the SQL NULL value into a JSON null value when generating the JSON key value. The option ABSENT ON NULL will omit the entire key if the value is NULL. The default setting for this option is NULL ON NULL.

For more info about what you see in the output of the `JSON_OBJECT` function, see the following articles:  

-   [How FOR JSON converts SQL Server data types to JSON data types &#40;SQL Server&#41;](../../relational-databases/json/how-for-json-converts-sql-server-data-types-to-json-data-types-sql-server.md)  
    The `JSON_OBJECT` function uses the rules described in this `FOR JSON` article to convert SQL data types to JSON types in the JSON object output.  

-   [How FOR JSON escapes special characters and control characters &#40;SQL Server&#41;](../../relational-databases/json/how-for-json-escapes-special-characters-and-control-characters-sql-server.md)  
    The `JSON_OBJECT` function escapes special characters and represents control characters in the JSON output as described in this `FOR JSON` article.

## Return value
Returns a valid JSON object string of nvarchar(max) type.

## Remarks  

  
## Examples

### Example 1

The following example returns an empty JSON object.
  
```sql
SELECT JSON_OBJECT();
```  

**Result**

```json  
{}
```

### Example 2

The following example returns a JSON object with two keys.  
  
```sql  
SELECT JSON_OBJECT('name':'value', 'type':1)
```

**Result**

```json  
{"name":"value","type":1}
```
  
### Example 3

The following example returns a JSON object with one key since the value for one of the keys is NULL and the ABSENT ON NULL option is specified.  
  
```sql  
SELECT JSON_OBJECT('name':'value', 'type':NULL ABSENT ON NULL)
```

**Result**

```json  
{"name":"value"}
```
  
### Example 4
The following example returns a JSON object with two keys. One key contains a JSON string and another key contains a JSON array.  
  
```sql  
SELECT JSON_OBJECT('name':'value', 'type':JSON_ARRAY(1, 2))
```

**Result**

```json  
{"name":"value","type":[1,2]}
```

### Example 5

The following example returns a JSON object with a two keys. One key contains a JSON string and another key contains a JSON object.  
  
```sql  
SELECT JSON_OBJECT('name':'value', 'type':JSON_OBJECT('type_id':1, 'name':'a'))
```

**Result**

```json  
{"name":"value","type":{"type_id":1,"name":"a"}}
```
  
### Example 6

The following example returns a JSON object with the inputs specified as variables or SQL expressions.  
  
```sql  
DECLARE @id_key nvarchar(10) = N'id',@id_value nvarchar(64) = NEWID();
SELECT JSON_OBJECT('user_name':USER_NAME(), @id_key:@id_value, 'sid':(SELECT @@SPID))
```

**Result**

```json  
{"user_name":"dbo","id":"E2CBD8B4-13C1-4D2F-BFF7-E6D722F095FD","sid":63}
```

### Example 7

The following example returns a JSON object per row in the query.  
  
```sql  
SELECT s.session_id, JSON_OBJECT('security_id':s.security_id, 'login':s.login_name, 'status':s.status) as info
FROM sys.dm_exec_sessions AS s
WHERE s.is_user_process = 1;
```

**Result**

|session_id|info|  
|--------|---------------|
|51|`{"security_id":"AQYAAAAAAAVQAAAAY/0dmFnai5oioQHh9eNArBIkYd4=","login":"NT SERVICE\\SQLTELEMETRY$SQL22"`,"status":"sleeping"}|
|52|`{"security_id":"AQUAAAAAAAUVAAAAoGXPfnhLm1/nfIdwAMgbAA==","login":WORKGROUP\\sqluser","status":"running"}`|

## See also  

 [JSON Data &#40;SQL Server&#41;](../../relational-databases/json/json-data-sql-server.md)  
