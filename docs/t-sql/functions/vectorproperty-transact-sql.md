---
title: "VECTORPROPERTY (Transact-SQL)"
description: The VECTORPROPERTY function returns specific properties of a given vector.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: pookam
ms.date: 11/18/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.collection:
  - ce-skilling-ai-copilot
ms.update-cycle: 180-days
ms.custom:
  - ignite-2025
f1_keywords:
  - "VECTORPROPERTY"
  - "VECTORPROPERTY_TSQL"
helpviewer_keywords:
  - "VECTORPROPERTY function"
  - "vector, property"
dev_langs:
  - TSQL
monikerRange: ">=sql-server-ver17 || >=sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# VECTORPROPERTY (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The `VECTORPROPERTY` function returns specific properties of a given vector. The function requires two arguments: the vector itself and the property to be retrieved.

> [!NOTE]  
> `VECTORPROPERTY` is available in Azure SQL Managed Instance with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
VECTORPROPERTY(vector , property)
```

## Arguments

### vector

A valid vector of the **vector** type. This can be an expression, such as a variable or a reference to a column.

The input vector might be specified as `[tablename].[colname]` or as a valid variable of the **vector** type.

## property

An expression specifying the name of the database property to return. The property supports one of the following values:

| Property | Description | Value Returned |
| --- | --- | --- |
| `Dimensions` | Return vector's dimensions count | **Integer** value with dimension count. |
| `BaseType` | Return vector's base type | **sysname** with the name of the data type. |

The default base type for vectors is currently set to **float** (32-bit).

## Return value

The function returns the specific properties of a given vector based on the property selected. For example:

- If the property is `Dimensions`, the function returns an **integer** value representing the dimension count of the vector.
- If the property is `BaseType`, the function returns the name of the data type (**sysname**).

## Examples

### Retrieve dimension count

In the following example, declare a vector with three dimensions and retrieve its dimensions count. The `VECTORPROPERTY` function returns the integer value `3` representing the dimension count.

```sql
DECLARE @v AS VECTOR(3) = '[1,2,3]';

SELECT VECTORPROPERTY(@v, 'Dimensions');
```

### Return count of dimensions

The following example retrieves the dimensions count of a vector column `embeddings` from a table `mytable`.

```sql
CREATE TABLE mytable
(
    id INT IDENTITY NOT NULL PRIMARY KEY,
    embeddings VECTOR(3) NOT NULL
);

INSERT INTO mytable (embeddings)
VALUES ('[4, -2, -1]'),
       ('[1, 3, -5]'),
       ('[7, -8, -10]'),
       ('[4.0, 0.2, -1.1]'),
       ('[0, 0, 0]'),
       ('[10, 10, 10]'),
       ('[-0.1, -0.2, -0.3]');

SELECT VECTORPROPERTY(t.embeddings, 'dimensions')
FROM mytable AS t;
```

Returns:

```output
Dimensions
----------
3
3
3
3
3
3
3
```

## Related content

- [Vector search and vector indexes in the SQL Database Engine](../../sql-server/ai/vectors.md)
- [Azure SQL Database Vector Search Samples](https://github.com/Azure-Samples/azure-sql-db-vector-search)
