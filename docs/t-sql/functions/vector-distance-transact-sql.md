---
title: "VECTOR_DISTANCE (Transact-SQL)"
description: VECTOR_DISTANCE calculates the distance between two vectors using a specified distance metric.
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
  - "VECTOR_DISTANCE"
  - "VECTOR_DISTANCE_TSQL"
helpviewer_keywords:
  - "VECTOR_DISTANCE function"
  - "vector, distance calculation"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# VECTOR_DISTANCE (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The `VECTOR_DISTANCE` function calculates the distance between two vectors using a specified distance metric. Vector distance is always exact and doesn't use any vector index, even if available. In order to use a vector index and thus perform an approximate vector search, you must use the [VECTOR_SEARCH](vector-search-transact-sql.md) function. To learn more about how vector indexing and vector search works, and the differences between exact and approximate search, refer to [Vector search and vector indexes in the SQL Database Engine](../../sql-server/ai/vectors.md).

> [!NOTE]  
> `VECTOR_DISTANCE` is available in Azure SQL Managed Instance with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
VECTOR_DISTANCE ( distance_metric , vector1 , vector2 )
```

## Arguments

### *distance_metric*

A string with the name of the distance metric to use to calculate the distance between the two given vectors. The following distance metrics are supported:

- `cosine` - Cosine distance
- `euclidean` - Euclidean distance
- `dot` - (Negative) Dot product

### *vector1*

An expression that evaluates to **vector** data type.

### *vector2*

An expression that evaluates to **vector** data type.

## Distance metrics

| Metric | Description | Range | Examples |
| --- | --- | --- | --- |
| `cosine` | Cosine (angular) distance | [0, 2] | `0`: identical vectors<br />`2`: opposing vectors |
| `euclidean` | Euclidean distance | [0, +∞] | `0`: identical vectors |
| `dot` | Dot product-based indication of distance,<br />obtained by calculating the *negative* dot product | [-∞, +∞] | Smaller numbers indicate more similar vectors |

## Return value

The function returns a scalar **float** value that represents the distance between the two vectors using the specified distance metric.

An error is returned if *distance_metric* isn't a valid metric and if the provided vectors aren't of the [vector data type](../data-types/vector-data-type.md).

## Examples

Details of the database used in the sample can be found on GitHub: [Download and import the Wikipedia Article with Vector Embeddings](https://github.com/Azure-Samples/azure-sql-db-openai?tab=readme-ov-file#download-and-import-the-wikipedia-article-with-vector-embeddings).

Examples assume the existence of a table named `wikipedia_articles` with a column `title_vector` of type `vector` that stores title's embeddings of Wikipedia articles. `title_vector` is assumed to be an embedding generated with an embedding model, like `text-embedding-ada-002` or `text-embedding-3-small`, which returns vectors with 1,536 dimensions.

For more examples, including end-to-end solutions, go to the [Azure SQL Database Vector Search Samples GitHub repo](https://github.com/Azure-Samples/azure-sql-db-vector-search).

### A. Create a vector from a JSON array

The following example creates a vector with three dimensions from a string with a JSON array.

```sql
DECLARE @v1 AS VECTOR(2) = '[1,1]';
DECLARE @v2 AS VECTOR(2) = '[-1,-1]';

SELECT VECTOR_DISTANCE('euclidean', @v1, @v2) AS euclidean,
       VECTOR_DISTANCE('cosine', @v1, @v2) AS cosine,
       VECTOR_DISTANCE('dot', @v1, @v2) AS negative_dot_product;
```

### B. Return top 10 similar articles based on titles

The following example returns the top 10 most similar articles to a given article, based on the cosine distance between their title vectors.

```sql
DECLARE @v AS VECTOR(1536);

SELECT @v = title_vector
FROM [dbo].[wikipedia_articles]
WHERE title = 'Alan Turing';

SELECT TOP (10) id,
                title,
                VECTOR_DISTANCE('cosine', @v, title_vector) AS distance
FROM [dbo].[wikipedia_articles]
ORDER BY distance;
```

### C. Return similar articles based on specific vector distance

The following example returns all the similar articles to a given article, based on the cosine distance between their title vectors, selecting only titles with a distance less than 0.3.

```sql
DECLARE @v AS VECTOR(1536);

SELECT @v = title_vector
FROM [dbo].[wikipedia_articles]
WHERE title = 'Alan Turing';

SELECT id,
       title,
       VECTOR_DISTANCE('cosine', @v, title_vector) AS distance
FROM [dbo].[wikipedia_articles]
WHERE VECTOR_DISTANCE('cosine', @v, title_vector) < 0.3
ORDER BY distance;
```

## Related content

- [Vector search and vector indexes in the SQL Database Engine](../../sql-server/ai/vectors.md)
- [Azure SQL Database Vector Search Samples](https://github.com/Azure-Samples/azure-sql-db-vector-search)
- [Vector data type](../data-types/vector-data-type.md)
