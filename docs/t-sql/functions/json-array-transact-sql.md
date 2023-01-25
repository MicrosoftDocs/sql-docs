---
title: "JSON_ARRAY (Transact-SQL)"
description: "JSON_ARRAY (Transact-SQL)"
author: "uc-msft"
ms.author: "umajay"
ms.date: 05/24/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "JSON_ARRAY"
  - "JSON_ARRAY_TSQL"
helpviewer_keywords:
  - "JSON_ARRAY function"
  - "JSON, validating"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || >= sql-server-ver16 || >= sql-server-linux-ver16"
---
# JSON_ARRAY (Transact-SQL)


[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Constructs JSON array text from zero or more expressions.

## Syntax  
  
```syntaxsql  
JSON_ARRAY ( [ <json_array_value> [,...n] ] [ <json_null_clause> ]  )  

<json_array_value> ::= value_expression

<json_null_clause> ::=
	  NULL ON NULL
	| ABSENT ON NULL
```
  
## Arguments
*json_array_value*
 Is an expression that defines the value of the element in the JSON array.

*json_null_clause* can be used to control the behavior of JSON_OBJECT function when value_expression is NULL. The option NULL ON NULL converts the SQL NULL value into a JSON null value when generating the value of the element in the JSON array. The option ABSENT ON NULL will omit the element in the JSON array if the value is NULL. The default setting for this option is ABSENT ON NULL.

For more info about what you see in the output of the `JSON_ARRAY` function, see the following articles:  

-   [How FOR JSON converts SQL Server data types to JSON data types &#40;SQL Server&#41;](../../relational-databases/json/how-for-json-converts-sql-server-data-types-to-json-data-types-sql-server.md)  
    The `JSON_ARRAY` function uses the rules described in this `FOR JSON` article to convert SQL data types to JSON types in the JSON array output.  

-   [How FOR JSON escapes special characters and control characters &#40;SQL Server&#41;](../../relational-databases/json/how-for-json-escapes-special-characters-and-control-characters-sql-server.md)  
    The `JSON_ARRAY` function escapes special characters and represents control characters in the JSON output as described in this `FOR JSON` article.

## Return value
Returns a valid JSON array string of nvarchar(max) type.

## Remarks  

  
## Examples  

### Example 1

The following example returns an empty JSON array.
  
```sql
SELECT JSON_ARRAY();
```  

**Result**

```json  
[]
```

### Example 2

The following example returns a JSON array with four elements.  
  
```sql  
SELECT JSON_ARRAY('a', 1, 'b', 2)
```

**Result**

```json  
["a",1,"b",2]
```

### Example 3

The following example returns a JSON array with three elements since one of the input values is NULL. Since the *json_null_clause* is omitted and the default for this option is ABSENT ON NULL, the NULL value in one of the inputs is not converted to a JSON null value.
  
```sql  
SELECT JSON_ARRAY('a', 1, 'b', NULL)
```

**Result**

```json  
["a",1,"b"]
```
  
### Example 4

The following example returns a JSON array with four elements. The NULL ON NULL option is specified so that any SQL NULL value in the input will be converted to JSON null value in the JSON array.
  
```sql  
SELECT JSON_ARRAY('a', 1, NULL, 2 NULL ON NULL)
```

**Result**

```json  
["a",1,null,2]
```

### Example 5

The following example returns a JSON array with two elements. One element contains a JSON string and another element contains a JSON object.  
  
```sql  
SELECT JSON_ARRAY('a', JSON_OBJECT('name':'value', 'type':1))
```

**Result**

```json  
["a",{"name":"value","type":1}]
```

### Example 6

The following example returns a JSON array with three elements. One element contains a JSON string, another element contains a JSON object & another element contains a JSON array. 
  
```sql  
SELECT JSON_ARRAY('a', JSON_OBJECT('name':'value', 'type':1), JSON_ARRAY(1, null, 2 NULL ON NULL))
```

**Result**

```json  
["a",{"name":"value","type":1},[1,null,2]]
```

### Example 7

The following example returns a JSON array with the inputs specified as variables or SQL expressions.  
  
```sql  
DECLARE @id_value nvarchar(64) = NEWID();
SELECT JSON_ARRAY(1, @id_value, (SELECT @@SPID));
```

**Result**

```json  
[1,"4BEA4F9F-D169-414F-AF99-9270FDB2EA62",55]
```

### Example 8

The following example returns a JSON array per row in the query.  
  
```sql  
SELECT s.session_id, JSON_ARRAY(s.host_name, s.program_name, s.client_interface_name)
FROM sys.dm_exec_sessions AS s
WHERE s.is_user_process = 1;
```

**Result**

|session_id|info|  
|--------|---------------|
|52|`["WIN16-VM","Microsoft SQL Server Management Studio - Query",".Net SqlClient Data Provider"]`|
|55|`["WIN16-VM","Microsoft SQL Server Management Studio - Query",".Net SqlClient Data Provider"]`|
|56|`["WIN19-VM","SQLServerCEIP",".Net SqlClient Data Provider"]`|
  

## See also  

 [JSON Data &#40;SQL Server&#41;](../../relational-databases/json/json-data-sql-server.md)  
