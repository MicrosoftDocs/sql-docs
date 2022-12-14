---
title: "ISJSON (Transact-SQL)"
description: "ISJSON (Transact-SQL)"
author: "uc-msft"
ms.author: "umajay"
ms.date: 04/26/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "ISJSON"
  - "ISJSON_TSQL"
helpviewer_keywords:
  - "ISJSON function"
  - "JSON, validating"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017"
---
# ISJSON (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

  Tests whether a string contains valid JSON.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
ISJSON ( expression [, json_type_constraint] )  
```  
  
## Arguments

 *expression*  
 The string to test.  
  
 *json_type_constraint*

  Specifies the JSON type to check in the input. Valid values are VALUE, ARRAY, OBJECT or SCALAR. Introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

> [!NOTE]
> The argument *json_type_constraint* is not supported in Azure Synapse Analytics

## Return value


 Returns 1 if the string contains valid JSON; otherwise, returns 0. Returns null if *expression* is null. If the statement omits *json_type_constraint*, the function tests if the input is a valid JSON object or array and returns 1 otherwise, it returns 0. If the *json_type_constraint* is specified then the function checks for the JSON type as follows:

|Value|Description|  
|-----|-----------|
|VALUE|Tests for a valid JSON value. This can be a JSON object, array, number, string or one of the three literal values (false, true, null)|
|ARRAY|Tests for a valid JSON array|
|OBJECT|Tests for a valid JSON object|
|SCALAR|Tests for a valid JSON scalar – number or string|

 The **json_type_constraint** value SCALAR can be used to test for IETF RFC 8259 conformant JSON document that contains only a JSON scalar value at top level. A JSON document that doesn't contain a JSON scalar value at top level conforms with IETF RFC 4627.

Does not return errors.  

## Remarks  

 **ISJSON** does not check the uniqueness of keys at the same level.  
  
## Examples  
  
### Example 1

The following example runs a statement block conditionally if the parameter value `@param` contains valid JSON.  
  
```sql  
DECLARE @param <data type>
SET @param = <value>

IF (ISJSON(@param) > 0)  
BEGIN  
     -- Do something with the valid JSON value of @param.  
END
```  
  
### Example 2  

The following example returns rows in which the column `json_col` contains valid JSON.  
  
```sql  
SELECT id, json_col
FROM tab1
WHERE ISJSON(json_col) = 1 
```  

### Example 3

The following example returns rows in which the column `json_col` contains valid JSON SCALAR value at top level.  
  
```sql  
SELECT id, json_col
FROM tab1
WHERE ISJSON(json_col, SCALAR) = 1 
```

### Example 4

The following example returns 1 since the input is a valid JSON value - *true*.  
  
```sql  
SELECT ISJSON('true', VALUE)
```

### Example 5

The following example returns 0 since the input is an invalid JSON value.
  
```sql  
SELECT ISJSON('test string', VALUE)
```

### Example 6

The following example returns 1 since the input is a valid JSON scalar according to RFC 8259.
  
```sql  
SELECT ISJSON('"test string"', SCALAR)
```

## See also  

 [JSON Data &#40;SQL Server&#41;](../../relational-databases/json/json-data-sql-server.md)  
