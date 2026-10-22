---
title: "VECTOR_NORM (Transact-SQL)"
description: VECTOR_NORM takes a vector as an input and returns the norm of the vector (which is a measure of its length or magnitude) in a given norm type.
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
  - "VECTOR_NORM"
  - "VECTOR_NORM_TSQL"
helpviewer_keywords:
  - "VECTOR_NORM function"
  - "vector, norm calculation"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---
# VECTOR_NORM (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

Use `VECTOR_NORM` to take a vector as an input and return the norm of the vector (which is a measure of its length or magnitude) in a given [norm type](https://mathworld.wolfram.com/VectorNorm.html).

For example, if you want to calculate the Euclidean norm (which is the most common norm type), you can use:

```sql
SELECT VECTOR_NORM ( vector, 'norm2' )
FROM ...
```

> [!NOTE]  
> `VECTOR_NORM` is available in Azure SQL Managed Instance with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
VECTOR_NORM ( vector , norm_type )
```

## Arguments

### vector

An expression that evaluates to **vector** data type.

### norm_type

A string with the name of the norm type to use to calculate the norm of the given vector. The following norm types are supported:

- `norm1` - The 1-norm, which is the sum of the absolute values of the vector components.
- `norm2` - The 2-norm, also known as the Euclidean Norm, which is the square root of the sum of the squares of the vector components.
- `norminf` - The infinity norm, which is the maximum of the absolute values of the vector components.

## Return value

The function returns a **float** value that represents the norm of the vector using the specified norm type.

An error is returned if *norm_type* isn't a valid norm type and if the vector isn't of the [vector data type](../data-types/vector-data-type.md).

## Examples

### Example 1

The following example creates a vector with three dimensions from a string with a JSON array.

```sql
DECLARE @v AS VECTOR(3) = '[1, 2, 3]';

SELECT VECTOR_NORM(@v, 'norm2') AS norm2,
       VECTOR_NORM(@v, 'norm1') AS norm1,
       VECTOR_NORM(@v, 'norminf') AS norminf;
```

The expected return values would be:

| `norm2` | `norm1` | `norminf` |
| --- | --- | --- |
| 3.7416573867739413 | 6.0 | 3.0 |

### Example 2

The following example calculates the norm of each vector in a table.

```sql
CREATE TABLE dbo.vectors
(
    ID INT PRIMARY KEY,
    v VECTOR(3) NOT NULL
);

INSERT INTO dbo.vectors (ID, v)
VALUES (1, '[0.1, -2, 42]'),
       (2, '[2, 0.1, -42]');

SELECT ID, VECTOR_NORM(v, 'norm2') AS norm
FROM dbo.vectors;
```

## Related content

- [Vector data type](../data-types/vector-data-type.md)
- [Intelligent applications and AI](/azure/azure-sql/database/ai-artificial-intelligence-intelligent-applications)
- [Vector functions](vector-functions-transact-sql.md)
