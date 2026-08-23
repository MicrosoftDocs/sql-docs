---
title: "VECTOR_SEARCH (Transact-SQL)"
description: VECTOR_SEARCH search for vectors similar to a given query vectors using an approximate nearest neighbors vector search algorithm.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: pookam, randolphwest, wiassaf
ms.date: 03/18/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - sqlcon-2026
ai-usage: ai-assisted
f1_keywords:
  - "VECTOR_SEARCH"
  - "VECTOR_SEARCH_TSQL"
helpviewer_keywords:
  - "VECTOR_SEARCH function"
  - "vector, search"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =fabric-sqldb"
---

# VECTOR_SEARCH (Transact-SQL) (Preview)

[!INCLUDE [sqlserver2025-asdb-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-fabricsqldb.md)]

Search for vectors similar to a given query vectors using an approximate nearest neighbors vector search algorithm. To learn more about how vector indexing and vector search works, and the differences between exact and approximate search, refer to [Vector search and vector indexes in the SQL Database Engine](../../sql-server/ai/vectors.md).

## Azure SQL Database and SQL database in Fabric

The feature is in preview. Make sure to check out the [current limitations](#limitations) before using it.

[!INCLUDE [preview-note](../../includes/preview.md)]

> [!IMPORTANT]
> For optimal performance and to access the latest vector search capabilities, use vector indexes created with the latest version. For more information about upgrading existing indexes and comparing versions, see [CREATE VECTOR INDEX - Upgrade vector indexes to the latest version](../statements/create-vector-index-transact-sql.md#upgrade-vector-indexes-to-the-latest-version).

### Regional availability

This feature is being deployed across Azure SQL Database and SQL database in Microsoft Fabric. During the rollout, availability and behavior might vary by region and by index version. If a feature or syntax isn't available, it becomes available automatically as deployment completes. For current regional availability status, see [Feature availability by region](/azure/azure-sql/database/region-availability#vector-search).

> [!WARNING]
> **Deprecation notice**: The `TOP_N` parameter in `VECTOR_SEARCH` is deprecated and maintained only for backward compatibility with earlier version vector indexes. New implementations should use `SELECT TOP (N) WITH APPROXIMATE` syntax instead. For more information, see [Syntax](#syntax).

## SQL Server 2025 Preview feature

In SQL Server 2025 this function is in preview and is subject to change. In order to use this feature, you must enable the `PREVIEW_FEATURES` [database scoped configuration](../statements/alter-database-scoped-configuration-transact-sql.md).

Make sure to check out the [current limitations](#limitations) before using it.

> [!NOTE]
> The latest version of Vector Indexes is only available in Azure SQL Database and SQL database in Microsoft Fabric currently.

## Key enhancements with latest vector indexes

Vector indexes created with the latest version introduce significant enhancements:

- **Full DML support**: Removes the previous limitation that made vector-indexed tables read-only after index creation. You can now perform INSERT, UPDATE, DELETE, and MERGE operations while maintaining vector index functionality with automatic, real-time index maintenance
- **Iterative filtering**: Predicates in the WHERE clause are applied during the vector search process, not after retrieval
- **Optimizer-driven**: The query optimizer automatically determines whether to use the DiskANN index or kNN search based on query characteristics
- **Advanced quantization**: Vector quantization techniques have been integrated to provide better storage efficiency and faster query performance, with these optimizations being transparent to users

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

**With latest version Vector Indexes:**

> [!IMPORTANT]
> When querying tables that use the latest vector index version, approximate vector search must use the TOP (N) APPROXIMATE syntax. This syntax requirement indicates that the query is explicitly requesting approximate nearest‑neighbor results.

```syntaxsql
SELECT TOP (N) WITH APPROXIMATE
    column_list
FROM VECTOR_SEARCH(
        TABLE = object [ AS source_table_alias ]
        , COLUMN = vector_column
        , SIMILAR_TO = query_vector
        , METRIC = { 'cosine' | 'dot' | 'euclidean' }
    ) [ AS result_table_alias ]
[ WHERE predicate ]
ORDER BY distance;
```

**With earlier version Vector Indexes:**

```syntaxsql
VECTOR_SEARCH(
    TABLE = object [ AS source_table_alias ]
    , COLUMN = vector_column
    , SIMILAR_TO = query_vector
    , METRIC = { 'cosine' | 'dot' | 'euclidean' }
    , TOP_N = k
) [ AS result_table_alias ]
```

> [!IMPORTANT]
> The `TOP_N` parameter is not supported with latest version vector indexes. Use the `SELECT TOP (N) WITH APPROXIMATE` syntax shown above. For more information, see [Error using legacy syntax](#error-using-legacy-syntax).

## Arguments

### *TABLE = object [AS source_table_alias]*

Table on which perform the search. It must be a base table. Views, temporary tables, both local and global, aren't supported.

### *COLUMN = vector_column*

The vector column in which search is performed. The column must be a [vector](../data-types/vector-data-type.md) data type.

### *SIMILAR_TO = query_vector*

The vector used for search. It must be a variable or a column of **vector** type.

### *METRIC = { 'cosine' | 'dot' | 'euclidean' }*

The distance metric used to calculate the distance between the query vector and the vectors in the specified column. An ANN (Approximate Nearest Neighbor) index is used only if a matching ANN index, with the same metric and on the same column, is found. If there are no compatible ANN indexes, a warning is raised and the kNN (k-nearest neighbor) algorithm is used.

### *TOP_N = \<k>*

> [!WARNING]
> This parameter is deprecated and maintained only for backward compatibility with earlier version vector indexes. For latest version indexes, use `SELECT TOP (N) WITH APPROXIMATE` syntax instead. New implementations should use the latest syntax.

The maximum number of similar vectors that must be returned. It must be a positive **integer**. This parameter is not supported with vector indexes created using the latest version.

### *result_table_alias*

The alias is used to reference the result set.

## Return result set

The result set returned by the `VECTOR_SEARCH` function includes:

- All columns from the table specified in the `TABLE` argument.

- An additional column named `distance`, which represents the distance between the vector in the column specified by the `COLUMN` argument and the vector provided in the `SIMILAR_TO` argument.

The distance column is generated by the `VECTOR_SEARCH` function itself, while all other columns come from the table referenced in the `TABLE` argument.

If you use an alias for the table in the `TABLE` argument, you must use that same alias to reference its columns in the `SELECT` statement. You can't use the alias assigned to `VECTOR_SEARCH` to reference columns from the table specified in `TABLE`. This behavior is easier to understand if you think of the result set built by taking the output of `VECTOR_SEARCH` and merging it with the table data.

If the table specified in the `TABLE` argument already contains a column named `distance`, the behavior will be similar to a SQL join between two tables that share a column name. In such cases, you must use table aliases to disambiguate the column references—otherwise, an error will be raised.

> [!IMPORTANT]
> The `distance` column is the only valid ordering key for approximate vector search results.

## Limitations
- **Ascending order only**: The `distance` column must be ordered in ascending order (ASC). Descending order (DESC) is not supported.


### Version-specific behavior

The behavior of `VECTOR_SEARCH` varies depending on the vector index version.

<a id="post-filtering-only"></a>

#### Earlier vector index versions

> [!NOTE]
> These limitations apply only to vector indexes created with earlier versions. Migrate to the latest version to enable iterative filtering. See [Migrating from earlier vector index versions](../statements/create-vector-index-transact-sql.md#migrating-from-earlier-vector-index-versions).

**Post-filtering only**: Vector search happens before applying any predicate. Additional predicates are applied only after the most similar vectors are returned. The following sample returns the top 10 rows with embeddings most similar to the query vector `@qv`, then applies the predicate specified in the `WHERE` clause. If none of the 10 rows associated with the vectors returned by the vector search have the `accepted` column equal to 1, the result is empty.

```sql
SELECT TOP (10) s.id,
                s.title,
                r.distance
FROM VECTOR_SEARCH(
         TABLE = dbo.sessions AS s,
         COLUMN = embedding,
         SIMILAR_TO = @qv,
         METRIC = 'cosine',
         TOP_N = 10
     ) AS r
WHERE accepted = 1
ORDER BY r.distance;
```

### General limitations

`VECTOR_SEARCH` can't be used in the body of a view.

## Examples

### A. Basic vector similarity search

> [!IMPORTANT]
> When querying tables that use the latest vector index version, approximate vector search must use the `TOP (N) WITH APPROXIMATE` syntax. This syntax requirement indicates that the query is explicitly requesting approximate nearest‑neighbor results.

The following example finds the 10 most similar articles to `Pink Floyd music style` in the `wikipedia_articles_embeddings` table.

```sql
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'Pink Floyd music style' USE MODEL Ada2Embeddings);

SELECT TOP (10) WITH APPROXIMATE
    t.id,
    t.title,
    r.distance
FROM VECTOR_SEARCH(
        TABLE = dbo.wikipedia_articles_embeddings AS t,
        COLUMN = content_vector,
        SIMILAR_TO = @qv,
        METRIC = 'cosine'
    ) AS r
ORDER BY r.distance;
```

The syntax for querying depends on the vector index version:

| Vector index version | Syntax example |
| -------------------- | -------------- |
| Latest version | Use `SELECT TOP (N) WITH APPROXIMATE` without `TOP_N` parameter |
| Earlier versions (deprecated) | Use `TOP_N` parameter in `VECTOR_SEARCH` function |

> [!TIP]
> To determine your vector index version, see [Migrating from earlier vector index versions](../statements/create-vector-index-transact-sql.md#migrating-from-earlier-vector-index-versions).

**For earlier version indexes (deprecated syntax):**

```sql
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'Pink Floyd music style' USE MODEL Ada2Embeddings);

SELECT TOP (10)
    t.id,
    t.title,
    r.distance
FROM VECTOR_SEARCH(
        TABLE = dbo.wikipedia_articles_embeddings AS t,
        COLUMN = content_vector,
        SIMILAR_TO = @qv,
        METRIC = 'cosine',
        TOP_N = 10  -- Deprecated: Use SELECT TOP (N) WITH APPROXIMATE instead
    ) AS r
ORDER BY r.distance;
```

> [!NOTE]
> Using the `TOP_N` parameter with latest version vector indexes returns error Msg 42274. For detailed information, see [Error using legacy syntax](#error-using-legacy-syntax) in the Expected behaviors section.

### B. Complete workflow with index creation

A basic end-to-end example using `CREATE VECTOR INDEX` and the related `VECTOR_SEARCH` function. The embeddings are mocked. In a real world scenario, embeddings are generated using an embedding model and [AI_GENERATE_EMBEDDINGS](ai-generate-embeddings-transact-sql.md), or an external library such as [OpenAI SDK](https://github.com/openai/openai-dotnet?tab=readme-ov-file#how-to-generate-text-embeddings).

> [!NOTE]
> Latest version vector indexes require at least 100 rows of data before index creation. This example inserts 100 rows to meet this requirement. For more information, see [Minimum data requirements](../statements/create-vector-index-transact-sql.md#minimum-data-requirements).

The following code block demonstrates the `VECTOR_SEARCH` function with mock embeddings:

1. Enables the preview feature (required for SQL Server 2025 only; not needed for Azure SQL Database or SQL database in Fabric).
1. Create a sample table `dbo.Articles` with a column `embedding` with data type **vector(5)**.
1. Insert 100 rows of sample data with mock embedding data.
1. Create a vector index on `dbo.Articles.embedding`.
1. Demonstrate the vector similarity search with the `VECTOR_SEARCH` function.

```sql
-- Step 0: Enable Preview Feature (SQL Server 2025 only)
ALTER DATABASE SCOPED CONFIGURATION
SET PREVIEW_FEATURES = ON;
GO

-- Step 1: Create a sample table with a VECTOR(5) column
CREATE TABLE dbo.Articles
(
    id INT PRIMARY KEY,
    title NVARCHAR(100),
    content NVARCHAR(MAX),
    embedding VECTOR(5) -- mocked embeddings
);
GO

-- Step 2: Insert sample data (100 rows required for latest version indexes)
INSERT INTO Articles (id, title, content, embedding)
SELECT
    value AS id,
    'Article ' || [value],
    'Content for article ' || [value],
    CAST(JSON_ARRAY(
        CAST(value * 0.01 AS FLOAT),
        CAST(value * 0.02 AS FLOAT),
        CAST(value * 0.03 AS FLOAT),
        CAST(value * 0.04 AS FLOAT),
        CAST(value * 0.05 AS FLOAT)
    ) AS VECTOR(5))
FROM GENERATE_SERIES(1, 100);
GO

-- Step 3: Create a vector index on the embedding column
CREATE VECTOR INDEX vec_idx ON Articles(embedding)
WITH (METRIC = 'cosine', TYPE = 'diskann');
GO

-- Step 4: Perform a vector similarity search
DECLARE @qv VECTOR(5) = '[0.3, 0.3, 0.3, 0.3, 0.3]';
SELECT TOP(3) WITH APPROXIMATE
    t.id,
    t.title,
    t.content,
    s.distance
FROM
    VECTOR_SEARCH(
        TABLE = Articles AS t,
        COLUMN = embedding,
        SIMILAR_TO = @qv,
        METRIC = 'cosine'
    ) AS s
ORDER BY s.distance, t.title;
```

The syntax for querying depends on the vector index version:

| Vector index version | Syntax example |
| -------------------- | -------------- |
| Latest version | Use `SELECT TOP (N) WITH APPROXIMATE` without `TOP_N` parameter |
| Earlier versions (deprecated) | Use `TOP_N` parameter in `VECTOR_SEARCH` function |

**For earlier version indexes (deprecated syntax):**

```sql
DECLARE @qv VECTOR(5) = '[0.3, 0.3, 0.3, 0.3, 0.3]';
SELECT TOP(3)
    t.id,
    t.title,
    t.content,
    s.distance
FROM
    VECTOR_SEARCH(
        TABLE = Articles AS t,
        COLUMN = embedding,
        SIMILAR_TO = @qv,
        METRIC = 'cosine',
        TOP_N = 3  -- Deprecated: Use SELECT TOP (N) WITH APPROXIMATE instead
    ) AS s
ORDER BY s.distance, t.title;
```

### C. Vector search with iterative filtering

The following example demonstrates iterative filtering with latest version vector indexes. The query finds similar articles while applying predicates during the search process.

With latest version indexes, the predicates in the WHERE clause are applied **during** the vector search process (not after). The engine keeps searching until it finds 5 qualifying rows that match all criteria:

- Vector similarity to "machine learning algorithms"
- Category equals 'Technology'
- Published status equals 1

This ensures you get exactly 5 results (if they exist) without manually tuning the search parameters. For a detailed comparison between earlier and latest versions, see [Iterative filtering behavior](#iterative-filtering-behavior) in the Expected behaviors section.

```sql
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'machine learning algorithms' USE MODEL Ada2Embeddings);

SELECT TOP (5) WITH APPROXIMATE
    t.id,
    t.title,
    t.category,
    r.distance
FROM VECTOR_SEARCH(
        TABLE = dbo.wikipedia_articles AS t,
        COLUMN = content_vector,
        SIMILAR_TO = @qv,
        METRIC = 'cosine'
    ) AS r
WHERE t.category = 'Technology'
  AND t.published = 1
ORDER BY r.distance;
```

> [!NOTE]
> Certain SQL operations like GROUP BY, aggregate functions, and window functions require a subquery pattern. For details, see [Combining vector search with other SQL operations](#combining-vector-search-with-other-sql-operations).

### D. Multi-table joins with INNER JOIN

The following sample demonstrates the INNER JOIN with filtering on multiple tables. Use when embeddings are stored in a separate table from the main entity data.

```sql
-- Assuming a schema with separate tables for articles and embeddings
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'artificial intelligence and machine learning' USE MODEL Ada2Embeddings);

SELECT TOP (10) WITH APPROXIMATE
    a.id,
    a.title,
    a.category,
    vs.distance
FROM wikipedia_articles a
INNER JOIN VECTOR_SEARCH(
    TABLE = wikipedia_articles_embeddings AS e,
    COLUMN = content_vector,
    SIMILAR_TO = @qv,
    METRIC = 'cosine'
) AS vs ON a.id = e.article_id
WHERE e.approved = 1                           -- Iterative filter on embedding table
  AND a.category IN ('Technology', 'Science')  -- Filter on main table
  AND a.views > 50000
ORDER BY vs.distance;
```

**Key features of this example:**

- **Table alias scope**: The alias `e` from `TABLE = wikipedia_articles_embeddings AS e` is available in the WHERE clause for iterative filtering with latest version indexes

## Expected behaviors

### Error using legacy syntax

If you attempt to use the `TOP_N` parameter in `VECTOR_SEARCH` when querying a table with a latest version vector index, SQL Server returns the following error:

```output
Msg 42274, Level 16, State 1
Vector search with version 3 index does not support explicit TOP_N parameter.
```

**To resolve this error:**

1. Remove the `TOP_N` parameter from the `VECTOR_SEARCH` function
2. Use `SELECT TOP (N) WITH APPROXIMATE` syntax instead

**Incorrect (produces error with latest version index):**

```sql
SELECT TOP (10) 
    t.id,
    r.distance
FROM VECTOR_SEARCH(
        TABLE = dbo.wikipedia_articles AS t,
        COLUMN = title_vector,
        SIMILAR_TO = @qv,
        METRIC = 'cosine',
        TOP_N = 10  -- This parameter causes the error with latest version indexes
    ) AS r;
```

**Correct (works with latest version index):**

```sql
SELECT TOP (10) WITH APPROXIMATE  -- Specify TOP and WITH APPROXIMATE here
    t.id,
    r.distance
FROM VECTOR_SEARCH(
        TABLE = dbo.wikipedia_articles AS t,
        COLUMN = title_vector,
        SIMILAR_TO = @qv,
        METRIC = 'cosine'
        -- No TOP_N parameter
    ) AS r
ORDER BY r.distance;
```

### Iterative filtering behavior

The latest version introduces significant improvements over earlier vector index versions:

| Aspect | Earlier version | Latest version |
| ------ | --------------- | -------------- |
| **Predicate application** | Relational predicates were applied after vector search returned a fixed number of nearest neighbors (post-filtering only) | Relational predicates are applied during the vector search process (iterative filtering) |
| **Result completeness** | Queries could return fewer rows—or no rows—if the initial nearest neighbors didn't satisfy filters, even when qualifying rows existed | Queries return the expected number of rows when qualifying data exists, without manual tuning or query rewrites |
| **TOP (N) tuning** | Users often had to guess or oversample TOP (N) values to compensate for post-filtering | No need to guess TOP (N) values. The engine searches until enough qualifying rows are found or the search space is exhausted |
| **Query optimization** | Not applicable | SQL Server automatically selects the most efficient execution strategy, including switching between vector index seeks and kNN scans when appropriate |

For a practical example of iterative filtering, see [Example C: Vector search with iterative filtering](#c-vector-search-with-iterative-filtering).

### ORDER BY clause requirements

When using `SELECT TOP (N) WITH APPROXIMATE`, the ORDER BY clause has specific requirements:

- **ORDER BY must be present**: Queries without an ORDER BY clause fail with error .
- **Only distance column allowed**: The ORDER BY clause must reference only the distance column from the VECTOR_SEARCH result set. Including additional columns fails with error. To sort by multiple columns, use the subquery pattern described in [Multiple ORDER BY columns](#multiple-order-by-columns).
- **Ascending order only**: The distance column must be ordered in ascending order (ASC). Descending order (DESC) is not supported.

**Valid ORDER BY:**

```sql
SELECT TOP (10) WITH APPROXIMATE
    t.id,
    t.title,
    r.distance
FROM VECTOR_SEARCH(
    TABLE = products,
    COLUMN = embedding,
    SIMILAR_TO = @query_vector,
    METRIC = 'cosine'
) AS r
INNER JOIN products t ON t.id = r.id
ORDER BY r.distance;  -- ✓ Valid
```

**Invalid ORDER BY patterns:**

```sql
-- Missing ORDER BY
SELECT TOP (10) WITH APPROXIMATE
    r.distance
FROM VECTOR_SEARCH(
    TABLE = products,
    COLUMN = embedding,
    SIMILAR_TO = @query_vector,
    METRIC = 'cosine'
) AS r;
-- ✗ Error Msg 42248: APPROXIMATE cannot be used in a query without ORDER BY

-- Multiple columns in ORDER BY
SELECT TOP (10) WITH APPROXIMATE
    t.title,
    r.distance
FROM VECTOR_SEARCH(
    TABLE = products,
    COLUMN = embedding,
    SIMILAR_TO = @query_vector,
    METRIC = 'cosine'
) AS r
INNER JOIN products t ON t.id = r.id
ORDER BY r.distance, t.title;
-- ✗ Error Msg 42271: TOP WITH APPROXIMATE and VECTOR_SEARCH requires ORDER BY 
-- on distance column ascending, and no other columns

-- Descending order
SELECT TOP (10) WITH APPROXIMATE
    r.distance
FROM VECTOR_SEARCH(
    TABLE = products,
    COLUMN = embedding,
    SIMILAR_TO = @query_vector,
    METRIC = 'cosine'
) AS r
ORDER BY r.distance DESC;
-- ✗ Error Msg 42271: TOP WITH APPROXIMATE and VECTOR_SEARCH requires ORDER BY 
-- on distance column ascending, and no other columns
```

### Behavior without a vector index

`VECTOR_SEARCH` can execute queries even when no vector index exists on the target column. Without an index, the query performs a full table scan (k-nearest neighbor (kNN) search) to calculate distances for all rows.

### Query behavior without TOP WITH APPROXIMATE

When using `VECTOR_SEARCH` without `SELECT TOP (N) WITH APPROXIMATE`, the query behavior depends on the presence of `TOP` and `ORDER BY` clauses:

- **No TOP, no ORDER BY, or ORDER BY non-distance**: Full table scan (brute-force search) that calculates and returns distances for all rows
- **TOP (no APPROXIMATE) with ORDER BY distance**: Executes as a kNN (k-nearest neighbors) search, which is an exact nearest neighbor search

**Example - Full scan with distance column:**

```sql
-- Returns all rows with calculated distances
SELECT 
    t.id,
    t.title,
    r.distance
FROM VECTOR_SEARCH(
    TABLE = dbo.wikipedia_articles AS t,
    COLUMN = title_vector,
    SIMILAR_TO = @qv,
    METRIC = 'cosine'
) AS r
ORDER BY t.id;  -- Not ordering by distance
```

**Example - kNN search (exact nearest neighbors):**

```sql
-- Returns exact top 10 nearest neighbors using kNN
SELECT TOP (10)
    t.id,
    t.title,
    r.distance
FROM VECTOR_SEARCH(
    TABLE = dbo.wikipedia_articles AS t,
    COLUMN = title_vector,
    SIMILAR_TO = @qv,
    METRIC = 'cosine'
) AS r
ORDER BY r.distance;  -- No WITH APPROXIMATE = exact kNN
```

### TOP WITH APPROXIMATE without VECTOR_SEARCH

Using `SELECT TOP (N) WITH APPROXIMATE` without a `VECTOR_SEARCH` function in the query results in an error. The `WITH APPROXIMATE` clause requires a `VECTOR_SEARCH` function to be present.

**Incorrect - This query fails:**

```sql
-- Error: WITH APPROXIMATE requires VECTOR_SEARCH
SELECT TOP (10) WITH APPROXIMATE
    id,
    title,
    VECTOR_DISTANCE('cosine', title_vector, @qv) AS distance
FROM dbo.wikipedia_articles
WHERE title_vector IS NOT NULL
ORDER BY VECTOR_DISTANCE('cosine', title_vector, @qv);
```

**Correct - Use VECTOR_SEARCH:**

```sql
-- Correct: WITH APPROXIMATE with VECTOR_SEARCH
SELECT TOP (10) WITH APPROXIMATE
    t.id,
    t.title,
    r.distance
FROM VECTOR_SEARCH(
    TABLE = dbo.wikipedia_articles AS t,
    COLUMN = title_vector,
    SIMILAR_TO = @qv,
    METRIC = 'cosine'
) AS r
ORDER BY r.distance;
```

### TRUNCATE TABLE restrictions

Tables with vector indexes cannot be truncated using `TRUNCATE TABLE`. To remove all data from a vector-indexed table:

1. Drop the vector index
2. Truncate the table
3. Repopulate the table with at least 100 rows
4. Recreate the vector index

**Example workflow:**

```sql
-- Step 1: Drop the vector index
DROP INDEX idx_vector ON wikipedia_articles;

-- Step 2: Truncate the table
TRUNCATE TABLE wikipedia_articles;

-- Step 3: Repopulate with data (at least 100 rows)
-- ... insert operations ...

-- Step 4: Recreate the vector index
CREATE VECTOR INDEX idx_vector 
ON wikipedia_articles(title_vector)
WITH (METRIC = 'cosine');
```

### Table hints for vector search

You can use table hints with the `VECTOR_SEARCH` function to control query execution behavior. The `FORCE_ANN_ONLY` table hint forces the query optimizer to use only the approximate nearest neighbor (ANN) index, even when the optimizer might otherwise choose a different execution strategy.

**Syntax:**

```sql
FROM VECTOR_SEARCH(
    TABLE      = table_name,
    COLUMN     = column_name,
    SIMILAR_TO = vector_value,
    METRIC     = 'metric_name'
) AS alias WITH (FORCE_ANN_ONLY)
```

**Example:**

The following example forces the use of the approximate nearest neighbor index for the vector search query:

```sql
DECLARE @qembedding VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'artificial intelligence' USE MODEL Ada2Embeddings);

SELECT TOP 50 WITH APPROXIMATE
    t.id,
    t.title,
    r.distance
FROM VECTOR_SEARCH(
    TABLE      = dbo.wikipedia_articles AS t,
    COLUMN     = title_vector,
    SIMILAR_TO = @qembedding,
    METRIC     = 'cosine'
) AS r WITH (FORCE_ANN_ONLY)
ORDER BY r.distance;
```

Use `FORCE_ANN_ONLY` when you want to ensure the query uses the approximate nearest neighbor index strategy, overriding the optimizer's automatic strategy selection.

> [!NOTE]  
> Using `FORCE_ANN_ONLY` requires both:
>
> - A vector index on the target column
> - `SELECT TOP (N) WITH APPROXIMATE` in the query
>
> If either requirement is missing, the query fails because it cannot use the approximate nearest neighbor strategy that the hint forces.

## Combining vector search with other SQL operations

The `VECTOR_SEARCH` function with `TOP (N) WITH APPROXIMATE` has specific requirements for its usage. Some SQL operations can be used directly with vector search, while others require a subquery pattern.

### Operations that require subquery pattern

When you need to perform operations that aren't directly compatible with `TOP (N) WITH APPROXIMATE`, use vector search in a subquery (inner query), then apply your operations in the outer query. This pattern maintains the performance benefits of approximate vector search while enabling full SQL functionality.

> [!TIP]
> The subquery pattern works for any operation that can't be combined directly with `TOP (N) WITH APPROXIMATE`. Apply vector search in the inner query, then use any SQL operation in the outer query.

### Common scenarios

The following table lists operations that require the subquery pattern:

| Operation | Example use case |
| --------- | ---------------- |
| GROUP BY | Calculate statistics per category |
| Aggregate functions | Overall COUNT, AVG, MIN, MAX across results |
| Window functions | ROW_NUMBER, RANK, DENSE_RANK, NTILE |
| Set operations | UNION, UNION ALL, EXCEPT, INTERSECT |
| Multiple ORDER BY columns | Sort by distance, then by date or title |
| DISTINCT | Remove duplicate results |
| CROSS APPLY | Apply vector search per row from outer table |

#### GROUP BY and aggregate functions

The following example finds the average distance of top matching articles by category. For this example, a `category` column has been added to the `wikipedia_articles` table to classify articles.

```sql
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'machine learning' USE MODEL Ada2Embeddings);

SELECT 
    category,
    COUNT(*) AS article_count,
    AVG(distance) AS avg_distance,
    MIN(distance) AS closest_match
FROM (
    SELECT TOP (100) WITH APPROXIMATE
        t.id,
        t.title,
        t.category,
        r.distance
    FROM VECTOR_SEARCH(
        TABLE = dbo.wikipedia_articles AS t,
        COLUMN = title_vector,
        SIMILAR_TO = @qv,
        METRIC = 'cosine'
    ) AS r
    ORDER BY r.distance
) AS vector_results
GROUP BY category
ORDER BY avg_distance;
```

#### Window functions

The following example ranks articles by similarity and assigns quartiles.

```sql
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'neural networks' USE MODEL Ada2Embeddings);

SELECT 
    id,
    title,
    category,
    distance,
    ROW_NUMBER() OVER (ORDER BY distance) AS rank,
    NTILE(4) OVER (ORDER BY distance) AS quartile
FROM (
    SELECT TOP (100) WITH APPROXIMATE
        t.id,
        t.title,
        t.category,
        r.distance
    FROM VECTOR_SEARCH(
        TABLE = dbo.wikipedia_articles AS t,
        COLUMN = title_vector,
        SIMILAR_TO = @qv,
        METRIC = 'cosine'
    ) AS r
    ORDER BY r.distance
) AS vector_results
WHERE distance < 0.5
ORDER BY rank;
```

#### Set operations (UNION, INTERSECT, EXCEPT)

The following example combines results from two different search queries using UNION.

```sql
DECLARE @qv1 VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'artificial intelligence' USE MODEL Ada2Embeddings);
DECLARE @qv2 VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'machine learning' USE MODEL Ada2Embeddings);

SELECT id, title, 'AI Search' AS source
FROM (
    SELECT TOP (50) WITH APPROXIMATE
        t.id,
        t.title,
        r.distance
    FROM VECTOR_SEARCH(
        TABLE = dbo.wikipedia_articles AS t,
        COLUMN = title_vector,
        SIMILAR_TO = @qv1,
        METRIC = 'cosine'
    ) AS r
    ORDER BY r.distance
) AS ai_results

UNION

SELECT id, title, 'ML Search' AS source
FROM (
    SELECT TOP (50) WITH APPROXIMATE
        t.id,
        t.title,
        r.distance
    FROM VECTOR_SEARCH(
        TABLE = dbo.wikipedia_articles AS t,
        COLUMN = title_vector,
        SIMILAR_TO = @qv2,
        METRIC = 'cosine'
    ) AS r
    ORDER BY r.distance
) AS ml_results
ORDER BY id;
```

#### DISTINCT

The following example gets distinct categories from top matching articles.

```sql
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'deep learning' USE MODEL Ada2Embeddings);

SELECT DISTINCT category
FROM (
    SELECT TOP (100) WITH APPROXIMATE
        t.id,
        t.category,
        r.distance
    FROM VECTOR_SEARCH(
        TABLE = dbo.wikipedia_articles AS t,
        COLUMN = title_vector,
        SIMILAR_TO = @qv,
        METRIC = 'cosine'
    ) AS r
    ORDER BY r.distance
) AS vector_results
WHERE distance < 0.7
ORDER BY category;
```

#### Multiple ORDER BY columns

The following example sorts by distance, then by title for ties.

```sql
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'quantum computing' USE MODEL Ada2Embeddings);

SELECT 
    id,
    title,
    category,
    distance
FROM (
    SELECT TOP (100) WITH APPROXIMATE
        t.id,
        t.title,
        t.category,
        r.distance
    FROM VECTOR_SEARCH(
        TABLE = dbo.wikipedia_articles AS t,
        COLUMN = title_vector,
        SIMILAR_TO = @qv,
        METRIC = 'cosine'
    ) AS r
    ORDER BY r.distance
) AS vector_results
ORDER BY distance, title;
```

#### CROSS APPLY

When using `SELECT TOP (N) WITH APPROXIMATE` with `VECTOR_SEARCH`, you can't use `CROSS APPLY` or `OUTER APPLY` in the same `FROM` clause. This applies even if there's no outer reference within the `VECTOR_SEARCH` function.

A query with `CROSS APPLY` would logically perform multiple vector searches (one per row from the outer table) and merge all results into a single ordered stream. The approximate nearest neighbor algorithm can't efficiently merge results from multiple independent vector searches while maintaining the approximate guarantees and performance characteristics.

**Pattern that produces an error:**

```sql
-- This query is NOT supported
SELECT TOP (100) WITH APPROXIMATE
    o.id,
    vs.title,
    vs.distance
FROM Orders AS o
    CROSS APPLY VECTOR_SEARCH(
        TABLE = Products,
        COLUMN = embedding,
        SIMILAR_TO = o.customer_preference_vector,
        METRIC = 'cosine'
    ) AS vs
WHERE o.order_date > '2026-01-01'
ORDER BY vs.distance;
```

This pattern attempts to find similar products for each order's customer preference, but can't be executed with an approximate vector search plan.

**Recommended pattern:**

To achieve similar results, use a subquery with `TOP (N) WITH APPROXIMATE` in the `CROSS APPLY` itself:

```sql
-- This query IS supported
SELECT
    o.id,
    vs.title,
    vs.distance
FROM Orders AS o
    CROSS APPLY (
        SELECT TOP (10) WITH APPROXIMATE
            p.title,
            r.distance
        FROM VECTOR_SEARCH(
            TABLE = Products AS p,
            COLUMN = embedding,
            SIMILAR_TO = o.customer_preference_vector,
            METRIC = 'cosine'
        ) AS r
        ORDER BY r.distance
    ) AS vs
WHERE o.order_date > '2026-01-01';
```

In this pattern:

- Each vector search is limited to the top 10 results within the subquery
- The outer query doesn't use `WITH APPROXIMATE`
- Results are properly scoped per row from the outer table

### Operations that work directly

The following operations can be used directly with `VECTOR_SEARCH` without requiring a subquery:

- **INNER JOIN** - See [Example D: Multi-table joins with INNER JOIN](#d-multi-table-joins-with-inner-join)
- **WHERE predicates** - Apply iterative or post-filtering
- **Single ORDER BY distance**

## Related content

- [Vector search and vector indexes in the SQL Database Engine](../../sql-server/ai/vectors.md)
- [Vector data type](../data-types/vector-data-type.md)
- [CREATE VECTOR INDEX (Transact-SQL) (Preview)](../statements/create-vector-index-transact-sql.md)
- [sys.dm_db_vector_indexes (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-vector-indexes-transact-sql.md)
- [Azure SQL Database Vector Search Samples](https://github.com/Azure-Samples/azure-sql-db-vector-search)
