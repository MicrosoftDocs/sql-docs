---
title: "JSON_ARRAYAGG (Transact-SQL)"
description: JSON_ARRAYAGG constructs a JSON array from an aggregation of SQL data or columns.
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
  - "JSON_ARRAYAGG"
  - "JSON_ARRAYAGG_TSQL"
helpviewer_keywords:
  - "JSON_ARRAYAGG function"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---
# JSON_ARRAYAGG (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

Constructs a JSON array from an aggregation of SQL data or columns. `JSON_ARRAYAGG` can also be used in a `SELECT` statement with `GROUP BY GROUPING SETS` clause.

> [!NOTE]  
> To create a JSON object from an aggregate instead, use [JSON_OBJECTAGG](json-objectagg-transact-sql.md).

Both **json** aggregate functions `JSON_OBJECTAGG` and `JSON_ARRAYAGG` are:

- generally available for Azure SQL Database, Azure SQL Managed Instance (with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy)**), SQL database in Microsoft Fabric, and Fabric Data Warehouse.

- in preview for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
JSON_ARRAYAGG (value_expression [ order_by_clause ] [ json_null_clause ] [ RETURNING json ] )

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

## Return value

Returns a valid JSON array string of **nvarchar(max)** type. If the `RETURNING json` option is included then the JSON array is returned as **json** type.

## Examples

### Example 1

The following example returns an empty JSON array.

```sql
SELECT JSON_ARRAYAGG(NULL);
```

**Result**

```json
[]
```

### Example 2

The following example constructs a JSON array with three elements from a result set.

```sql
SELECT JSON_ARRAYAGG(c1)
FROM (VALUES ('c'), ('b'), ('a')) AS t(c1);
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

| object_id | column_list |
| --- | --- |
| 3 | `["rsid","rscolid","hbcolid","rcmodified","ti","cid","ordkey","maxinrowlen","status","offset","nullbit","bitpos","colguid","ordlock"]` |
| 5 | `["rowsetid","ownertype","idmajor","idminor","numpart","status","fgidfs","rcrows","cmprlevel","fillfact","maxnullbit","maxleaf","maxint","minleaf","minint","rsguid","lockres","scope_id"]` |
| 6 | `["id","subid","partid","version","segid","cloneid","rowsetid","dbfragid","status"]` |
| 7 | `["auid","type","ownerid","status","fgid","pgfirst","pgroot","pgfirstiam","pcused","pcdata","pcreserved"]` |
| 8 | `["status","fileid","name","filename"]` |

### Example 5

The following example returns a result with four columns from a SELECT statement containing SUM and JSON_ARRAYAGG aggregates with GROUP BY GROUPING SETS. The first two columns return the `id` and `type` column value. The third column `total_amount` returns the value of SUM aggregate on the `amount` column. The fourth column `json_total_amount` returns the value of JSON_ARRAYAGG aggregate on the `amount` column.

```sql
WITH T
AS (SELECT *
    FROM (VALUES (1, 'k1', 'a', 2), (1, 'k2', 'b', 3), (1, 'k3', 'b', 4), (2, 'j1', 'd', 7), (2, 'j2', 'd', 9)) AS b(id, name, type, amount))
SELECT id,
       type,
       SUM(amount) AS total_amount,
       JSON_ARRAYAGG(amount) AS json_total_amount
FROM T
GROUP BY GROUPING SETS((id), (type), (id, type), ());
```

**Result**

| id | type | total_amount | json_total_name_amount |
| --- | --- | --- | --- |
| 1 | a | 2 | `[2]` |
| `NULL` | a | 2 | `[2]` |
| 1 | b | 7 | `[4,3]` |
| `NULL` | b | 7 | `[4,3]` |
| 2 | d | 16 | `[9,7]` |
| `NULL` | d | 16 | `[9,7]` |
| `NULL` | `NULL` | 25 | `[2,4,3,9,7]` |
| 1 | `NULL` | 9 | `[3,4,2]` |
| 2 | `NULL` | 16 | `[9,7]` |

### Example 6

The following example returns a JSON array as **json** type.

```sql
SELECT JSON_ARRAYAGG(1 RETURNING JSON);
```

**Result**

```json
[1]
```

## Related content

- [JSON path expressions in the SQL Database Engine](../../relational-databases/json/json-path-expressions-sql-server.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
- [JSON_OBJECTAGG (Transact-SQL)](json-objectagg-transact-sql.md)
