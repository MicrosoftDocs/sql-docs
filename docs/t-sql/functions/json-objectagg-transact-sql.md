---
title: "JSON_OBJECTAGG (Transact-SQL)"
description: JSON_OBJECTAGG constructs a JSON object from an aggregation of SQL data or columns.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: umajay, jovanpop, randolphwest
ms.date: 10/27/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "JSON_OBJECTAGG"
  - "JSON_OBJECTAGG_TSQL"
helpviewer_keywords:
  - "JSON_OBJECTAGG function"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---
# JSON_OBJECTAGG (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The `JSON_OBJECTAGG` syntax constructs a JSON object from an aggregation of SQL data or columns. `JSON_OBJECTAGG` can also be used in a `SELECT` statement with `GROUP BY GROUPING SETS` clause.

The key/value pairs can be specified as input values, column, variable references.

> [!NOTE]  
> To create a JSON array from an aggregate instead, use [JSON_ARRAYAGG](json-arrayagg-transact-sql.md).

Both **json** aggregate functions `JSON_OBJECTAGG` and `JSON_ARRAYAGG` are:

- generally available for Azure SQL Database, Azure SQL Managed Instance (with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy)**), SQL database in Microsoft Fabric, and Fabric Data Warehouse.

- in preview for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
JSON_OBJECTAGG ( json_key_value [ json_null_clause ] [ RETURNING JSON ] )

json_key_value ::= <json_name> : <value_expression>

json_null_clause ::= NULL ON NULL | ABSENT ON NULL
```

## Arguments

#### json_key_value

The key / value pair of the JSON object.

#### *json_null_clause*

Optional. Omits the entire property of an object if the value is `NULL`, or use JSON null as property value. If omitted, `NULL ON NULL` is default.

## Return value

Returns a valid JSON object string of **nvarchar(max)** type. If the `RETURNING JSON` option is included then the JSON object is returned as **json** type.

## Examples

### A. Return JSON object with one key

The following example returns a JSON object with one key and null value.

```sql
SELECT JSON_OBJECTAGG('key':NULL);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```json
{"key":null}
```

### B. Construct JSON object from result set

The following example constructs a JSON object with three properties from a result set.

```sql
SELECT JSON_OBJECTAGG(c1:c2)
FROM (
    VALUES('key1', 'c'), ('key2', 'b'), ('key3', 'a')
) AS t(c1, c2);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```json
{"key1":"c","key2":"b","key3":"a"}
```

### C. Return result with two columns

The following example returns a result with two columns. The first column contains the `object_id` value. The second column contains a JSON object where the key is the column name and value is the `column_id`.

```sql
SELECT TOP (5) c.object_id,
               JSON_OBJECTAGG(c.name:c.column_id) AS columns
FROM sys.columns AS c
GROUP BY c.object_id;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| object_id | column_list |
| --- | --- |
| 3 | `{"bitpos":12,"cid":6,"colguid":13,"hbcolid":3,"maxinrowlen":8,"nullbit":11,"offset":10,"ordkey":7,"ordlock":14,"rcmodified":4,"rscolid":2,"rsid":1,"status":9,"ti":5}` |
| 5 | `{"cmprlevel":9,"fgidfs":7,"fillfact":10,"idmajor":3,"idminor":4,"lockres":17,"maxint":13,"maxleaf":12,"maxnullbit":11,"minint":15,"minleaf":14,"numpart":5,"ownertype":2,"rcrows":8,"rowsetid":1,"rsguid":16,"scope_id":18,"status":6}` |
| 6 | `{"cloneid":6,"dbfragid":8,"id":1,"partid":3,"rowsetid":7,"segid":5,"status":9,"subid":2,"version":4}` |
| 7 | `{"auid":1,"fgid":5,"ownerid":3,"pcdata":10,"pcreserved":11,"pcused":9,"pgfirst":6,"pgfirstiam":8,"pgroot":7,"status":4,"type":2}` |
| 8 | `{"fileid":2,"filename":4,"name":3,"status":1}` |

### D. Return a JSON object as JSON type

The following example returns a JSON object as **json** type.

```sql
SELECT JSON_OBJECTAGG('a':1 RETURNING JSON);
```

**Result**

```json
{"a":1}
```

### E. Return aggregated result with four columns

The following example returns a result with four columns from a `SELECT` statement containing `SUM` and `JSON_OBJECTAGG` aggregates with `GROUP BY GROUPING SETS`. The first two columns return the `id` and `type` column value. The third column `total_amount` returns the value of `SUM` aggregate on the `amount` column. The fourth column `json_total_name_amount` returns the value of `JSON_OBJECTAGG` aggregate on the `name` and `amount` columns.

```sql
WITH T
AS (SELECT *
    FROM (
        VALUES (1, 'k1', 'a', 2), (1, 'k2', 'b', 3), (1, 'k3', 'b', 4), (2, 'j1', 'd', 7), (2, 'j2', 'd', 9)
    ) AS b(id, name, type, amount))
SELECT id,
       type,
       SUM(amount) AS total_amount,
       JSON_OBJECTAGG(name:amount) AS json_total_name_amount
FROM T
GROUP BY GROUPING SETS((id), (type), (id, type), ());
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| id | type | total_amount | json_total_name_amount |
| --- | --- | --- | --- |
| 1 | a | 2 | `{"k1":2}` |
| `NULL` | a | 2 | `{"k1":2}` |
| 1 | b | 7 | `{"k3":4,"k2":3}` |
| `NULL` | b | 7 | `{"k3":4,"k2":3}` |
| 2 | d | 16 | `{"j2":9,"j1":7}` |
| `NULL` | d | 16 | `{"j2":9,"j1":7}` |
| `NULL` | `NULL` | 25 | `{"k1":2,"k3":4,"k2":3,"j2":9,"j1":7}` |
| 1 | `NULL` | 9 | `{"k2":3,"k3":4,"k1":2}` |
| 2 | `NULL` | 16 | `{"j2":9,"j1":7}` |

## Related content

- [JSON path expressions in the SQL Database Engine](../../relational-databases/json/json-path-expressions-sql-server.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
- [JSON_ARRAYAGG (Transact-SQL)](json-arrayagg-transact-sql.md)
