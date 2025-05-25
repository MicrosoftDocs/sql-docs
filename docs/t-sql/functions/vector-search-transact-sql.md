---
title: "VECTOR_SEARCH (Transact-SQL)"
description: "VECTOR_SEARCH search for vectors similar to a given query vectors using an approximate nearest neighbors vector search algorithm."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: damauri
ms.date: 05/01/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "VECTOR_SEARCH"
  - "VECTOR_SEARCH_TSQL"
helpviewer_keywords:
  - "VECTOR_SEARCH function"
  - "vector, search"
dev_langs:
  - "TSQL"
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17"
---

# VECTOR_SEARCH (Transact-SQL) (Preview)

[!INCLUDE [SQL Server 2025](../../includes/applies-to-version/_ss2025.md)]

Search for vectors similar to a given query vectors using an approximate nearest neighbors vector search algorithm. To learn more about how vector indexing and vector search works, and the differences between exact and approximate search, refer to [Vectors in the SQL Database Engine](../../relational-databases/vectors/vectors-sql-server.md).

## Preview feature

> [!NOTE]
> This function is in preview and is subject to change. Make sure to read preview usage terms in [Service Level Agreements (SLA) for Online Services](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services).

This feature is in preview. In order to use this feature, you must enable the following [trace flags](../database-console-commands/dbcc-traceon-transact-sql.md):

```sql
DBCC TRACEON(466, 474, 13981, -1)
```

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

```syntaxsql
VECTOR_SEARCH(
    TABLE = object [AS source_table_alias]
    , COLUMN = vector_column
    , SIMILAR_TO = query_vector
    , METRIC = { 'cosine' | 'dot' | 'euclidean' }
    , TOP_N = k
) [AS result_table_alias]
```  

## Arguments

### *TABLE = object [AS source_table_alias]*

Table on which perform the search. It must be a base table. Views, temporary tables, both local and global, are not supported.

### *COLUMN = vector_column*

The vector column in which search is performed. The column must be a [vector](../data-types/vector-data-type.md) data type.

### *SIMILAR_TO = query_vector*

The vector used for search. It must be a variable or a column of **vector** type.

### *METRIC = { 'cosine' | 'dot' | 'euclidean' }*

The distance metric used to calculate the distance between the query vector and the vectors in the specified column. An ANN (Approximate Nearest Neighbor) index is used only if a matching ANN index, with the same metric and on the same column, is found. If there are no compatible ANN indexes, a warning is raised and the KNN (k-Nearest Neighbor) algorithm is used.

### *TOP_N = \<k>*

The maximum number of similar vectors that must be returned. It must be a positive **integer**. 

### *result_table_alias*

The alias is used to reference the result set. 

## Return result set

The returned result set has all the columns from the table specified in TABLE argument, plus the additional `distance` column. The `distance` column contains the distance between the given vector in the COLUMN argument and the vector specified in SIMILAR_TO argument.

## Remarks

In the current preview, vector search happens before applying any predicate. 

The following sample returns the top 10 rows with embeddings most similar to the query vector `@qv`, then applies the predicate specified in the `WHERE` clause. If none of these 10 rows have the `accepted` column equal to 1, the result will be empty. 

```sql
SELECT
  s.id, 
  s.title,
  r.distance
FROM
  VECTOR_SEARCH(
    TABLE = dbo.sessions AS s, 
    COLUMN = embedding, 
    SIMILAR_TO = @qv, 
    METRIC = 'cosine', 
    TOP_N = 10 
  ) AS r
WHERE
  accepted = 1
ORDER BY
  r.distance
```

## Examples

### Example 1

The following example finds the 10 most similar articles to the `Pink Floyd music style` in the `wikipedia_articles_embeddings` table.

```sql
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDING(N'Pink Floyd music style' model Ada2Embeddings);
SELECT 
    t.id, s.distance, t.title
FROM
    VECTOR_SEARCH(
        TABLE = [dbo].[wikipedia_articles_embeddings] as t, 
        COLUMN = [content_vector], 
        SIMILAR_TO = @qv, 
        METRIC = 'cosine', 
        TOP_N = 10
    ) AS s
ORDER BY s.distance
```

### Example 2

Same as example 1, but this time the query vectors are taking from another table instead of a variable.

```sql
CREATE TABLE #t (
  id INT, 
  q NVARCHAR(MAX),
  v VECTOR(1536)
);
INSERT INTO 
  #t
SELECT 
    id, q, ai_generate_embeddings(q model Ada2Embeddings)
FROM
    (VALUES 
        (1, N'four legged furry animal'),
        (2, N'pink floyd music style')
    ) S(id, q)
;

SELECT 
    t.id, s.distance, t.title
FROM
    #t AS qv
CROSS APPLY
    VECTOR_SEARCH(
        TABLE = [dbo].[wikipedia_articles_embeddings] as t, 
        COLUMN = [content_vector], 
        SIMILAR_TO = t.v, 
        METRIC = 'cosine', 
        TOP_N = 10
    ) AS s
WHERE
  qv.id = 2
ORDER BY 
  s.distance
```

## Related content

- [Overview of vectors in the SQL Database Engine](../../relational-databases/vectors/vectors-sql-server.md)
- [Azure SQL Database Vector Search Samples](https://github.com/Azure-Samples/azure-sql-db-vector-search)
- [Vector data type](../data-types/vector-data-type.md)
