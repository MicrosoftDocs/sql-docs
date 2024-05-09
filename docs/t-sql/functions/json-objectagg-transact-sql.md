---
title: "JSON_OBJECTAGG (Transact-SQL)"
description: JSON_OBJECTAGG constructs a JSON object from an aggregation of SQL data or columns.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: umajay
ms.date: 05/02/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "JSON_OBJECTAGG"
  - "JSON_OBJECTAGG_TSQL"
helpviewer_keywords:
  - "JSON_OBJECTAGG function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# JSON_OBJECTAGG (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

 Constructs a JSON object from an aggregation of SQL data or columns.
  
 The key/value pairs can be specified as input values, column, variable references.

 To create a JSON array from an aggregate instead, use [JSON_ARRAYAGG](json-arrayagg-transact-sql.md).

> [!NOTE]
> This feature is currently in preview.

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax
  
```syntaxsql
JSON_OBJECTAGG ( json_key_value [ json_null_clause ] )
 
json_key_value ::= <json_name> : <value_expression> 

json_null_clause ::= NULL ON NULL | ABSENT ON NULL 

```  
  
## Arguments

#### json_key_value

The key / value pair of the JSON object.

#### *json_null_clause*  

Optional. Omit the entire property of an object if the value is `NULL`, or use JSON null as property value. If omitted, `NULL ON NULL` is default. 
  
## Return value
 
## Remarks

 
## Examples
  
### Example 1

The following example returns a JSON object with one key and null value.
  
```sql
select JSON_OBJECTAGG ( 'key':null )
```  

**Result**

```json  
{"key":null}
```

### Example 2

The following example constructs a JSON object with three properties from a resulset.  
  
```sql  
select JSON_OBJECTAGG( c1:c2 )
from (
	values('key1', 'c'), ('key2', 'b'), ('key3','a')
) as t(c1, c2);
```

**Result**

```json  
{"key1":"c","key2":"b","key3":"a"}
```

### Example 3

The following example returns a result with two columns. The first column contains the `object_id` value. The second column contains a JSON object where the key is the column name and value is the `column_id`.  
  
```sql  
select top(5) c.object_id, JSON_OBJECTAGG(c.name:c.column_id) as columns
  from sys.columns as c
 group by c.object_id;
```

**Result**
|object_id|column_list|
|--------|--------------|
|3|`{"bitpos":12,"cid":6,"colguid":13,"hbcolid":3,"maxinrowlen":8,"nullbit":11,"offset":10,"ordkey":7,"ordlock":14,"rcmodified":4,"rscolid":2,"rsid":1,"status":9,"ti":5}`|
|5|`{"cmprlevel":9,"fgidfs":7,"fillfact":10,"idmajor":3,"idminor":4,"lockres":17,"maxint":13,"maxleaf":12,"maxnullbit":11,"minint":15,"minleaf":14,"numpart":5,"ownertype":2,"rcrows":8,"rowsetid":1,"rsguid":16,"scope_id":18,"status":6}`|
|6|`{"cloneid":6,"dbfragid":8,"id":1,"partid":3,"rowsetid":7,"segid":5,"status":9,"subid":2,"version":4}`|
|7|`{"auid":1,"fgid":5,"ownerid":3,"pcdata":10,"pcreserved":11,"pcused":9,"pgfirst":6,"pgfirstiam":8,"pgroot":7,"status":4,"type":2}`|
|8|`{"fileid":2,"filename":4,"name":3,"status":1}`|

## Related content

- [JSON Path Expressions (SQL Server)](../../relational-databases/json/json-path-expressions-sql-server.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
- [JSON_ARRAYAGG (Transact-SQL)](json-arrayagg-transact-sql.md)
