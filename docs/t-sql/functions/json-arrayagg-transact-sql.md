---
title: "JSON_ARRAYAGG (Transact-SQL)"
description: JSON_ARRAYAGG constructs a JSON array from an aggregation of SQL data or columns.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: umajay
ms.date: 05/13/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "JSON_ARRAYAGG"
  - "JSON_ARRAYAGG_TSQL"
helpviewer_keywords:
  - "JSON_ARRAYAGG function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current"
---
# JSON_ARRAYAGG (Transact-SQL)

[!INCLUDE [asdb-asdbmi](../../includes/applies-to-version/asdb-asdbmi.md)]

 Constructs a JSON array from an aggregation of SQL data or columns.

 To create a JSON object from an aggregate instead, use [JSON_OBJECTAGG](json-objectagg-transact-sql.md).
  
> [!NOTE]
> This feature is currently in preview.

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax
  
```syntaxsql
JSON_ARRAYAGG (value_expression [ order_by_clause ] [ json_null_clause ] ) 

json_null_clause ::=  NULL ON NULL | ABSENT ON NULL 

order_by_clause ::= ORDER BY <column_list>
```  

## Arguments

#### value_expression

The value expression can be a column or expression in a query or constants/literals.

#### *json_null_clause*

Optional. *json_null_clause* can be used to control the behavior of `JSON_ARRAYAGG` function when *value_expression* is `NULL`. The option `NULL ON NULL` converts the SQL `NULL` value into a JSON null value when generating the value of the element in the JSON array. The option `ABSENT ON NULL` omits the element in the JSON array if the value is `NULL`. If omitted, `ABSENT ON NULL` is default.
  
#### *order_by_clause*

Optional. The order of elements in the resulting JSON array can be specified to order the input rows to the aggregate.

## Examples
  
### Example 1

The following example returns an empty JSON array.
  
```sql
SELECT JSON_ARRAYAGG(null);
```  

**Result**

```json  
[]
```

### Example 2

The following example constructs a JSON array with three elements from a result set.  
  
```sql  
SELECT JSON_ARRAYAGG( c1 )
FROM (
    VALUES ('c'), ('b'), ('a')
) AS t(c1);
```

**Result**

```json  
["c","b","a"]
```

### Example 3

The following example constructs a JSON array with three elements ordered by the value of the column.  
  
```sql  
SELECT JSON_ARRAYAGG( c1 ORDER BY c1)
FROM (
    VALUES ('c'), ('b'), ('a')
) AS t(c1);
```

**Result**

```json  
["a","b","c"]
```

### Example 4

The following example returns a result with two columns. The first column contains the `object_id` value. The second column contains a JSON array containing the names of the columns. The columns in the JSON array are ordered based on the `column_id` value.  

```sql  
SELECT TOP(5) c.object_id, JSON_ARRAYAGG(c.name ORDER BY c.column_id) AS column_list
FROM sys.columns AS c
GROUP BY c.object_id;
```

**Result**

|object_id|column_list|
|:--------|:--------------|
|3|`["rsid","rscolid","hbcolid","rcmodified","ti","cid","ordkey","maxinrowlen","status","offset","nullbit","bitpos","colguid","ordlock"]`|
|5|`["rowsetid","ownertype","idmajor","idminor","numpart","status","fgidfs","rcrows","cmprlevel","fillfact","maxnullbit","maxleaf","maxint","minleaf","minint","rsguid","lockres","scope_id"]`|
|6|`["id","subid","partid","version","segid","cloneid","rowsetid","dbfragid","status"]`|
|7|`["auid","type","ownerid","status","fgid","pgfirst","pgroot","pgfirstiam","pcused","pcdata","pcreserved"]`|
|8|`["status","fileid","name","filename"]`|

## Related content

- [JSON Path Expressions (SQL Server)](../../relational-databases/json/json-path-expressions-sql-server.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
- [JSON_OBJECTAGG (Transact-SQL)](json-objectagg-transact-sql.md)
