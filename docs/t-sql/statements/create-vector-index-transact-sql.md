---
title: "CREATE VECTOR INDEX (Transact-SQL)"
description: CREATE VECTOR INDEX creates an index on vector data to allow approximate nearest neighbor search.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: pookam, randolphwest, wiassaf
ms.date: 09/15/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - sqlcon-2026
ai-usage: ai-assisted
f1_keywords:
  - "VECTOR INDEX"
  - "CREATE VECTOR INDEX"
  - "CREATE_VECTOR_INDEX_TSQL"
  - "VECTOR_INDEX_TSQL"
helpviewer_keywords:
  - "vector indexes [SQL Server], creating"
  - "index creation [SQL Server], vector indexes"
  - "CREATE VECTOR INDEX statement"
  - "CREATE INDEX statement"
  - "DISKANN"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# CREATE VECTOR INDEX (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

Create an approximate index on a vector column to improve performance of nearest neighbors searches. To learn more about how vector indexing and vector search works, and the differences between exact and approximate search, see [Vector search and vector indexes in the SQL Database Engine](../../sql-server/ai/vectors.md).

## Feature availability


- Vector index and `VECTOR_SEARCH` are generally available (GA) in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb-md](../../includes/fabric-sqldb.md)], and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] with the **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy). 
- Vector index and `VECTOR_SEARCH` are preview features in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] with the **SQL Server 2025** update policy.

> [!WARNING]
> **Deprecation notice**: Vector indexes created by using an earlier data structure are supported in the current release but will be retired in a future version. To ensure future compatibility and access to the latest vector search capabilities, migrate existing vector indexes by using the steps in the [Migrating from earlier vector index versions](#migrating-from-earlier-vector-index-versions) section.

## SQL Server 2025 preview feature

In SQL Server 2025 and Azure SQL Managed Instance in the **SQL Server 2025** [update policy](/azure/azure-sql/managed-instance/update-policy?view=azuresql-mi&preserve-view=true), vector indexes and the `VECTOR_SEARCH` function are in preview and subject to change. To use this feature, you must enable the `PREVIEW_FEATURES` [database scoped configuration](alter-database-scoped-configuration-transact-sql.md).

Check the [current limitations](#limitations-and-considerations) before using it.

> [!NOTE]
> The latest version of vector indexes is available in Azure SQL Database, SQL database in Microsoft Fabric, and Azure SQL Managed Instance with the **Always-up-to-date** update policy.

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
CREATE VECTOR INDEX index_name
ON object ( vector_column )
[ WITH (
    [ , ] METRIC = { 'cosine' | 'dot' | 'euclidean' }
    [ [ , ] TYPE = 'DiskANN' ]
    [ [ , ] MAXDOP = max_degree_of_parallelism ]
) ]
[ ON { filegroup_name | "default" } ]
[;]
```

## Arguments

### *index_name*

The name of the index. Index names must be unique within a table but don't have to be unique within a database. Index names must follow the rules of [identifiers](../../relational-databases/databases/database-identifiers.md).

### *object*

Table on which the index is created. It must be a base table. Views, temporary tables, both local and global, aren't supported.

### *vector_column*

Column to use to create the vector index. It must be of **vector** type.

### METRIC

A string with the name of the distance metric to use to calculate the distance between the two given vectors. The following distance metrics are supported:

- `cosine` - Cosine distance
- `euclidean` - Euclidean distance
- `dot` - (Negative) Dot product

### TYPE

The type of [ANN algorithm](../../sql-server/ai/vectors.md#approximate-vector-index-and-vector-search-approximate-nearest-neighbors) used to build the index. Only `DiskANN` is currently supported. DiskANN is the default value.

### MAXDOP

Overrides the **max degree of parallelism** configuration option for the index operation. For more information, see [Server configuration: max degree of parallelism](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md). Use `MAXDOP` to limit the degree of parallelism and the resulting resource consumption for an index build operation.

*max_degree_of_parallelism* can be:

- `1`

   Suppresses parallel plan generation.

- \>`1`

   Restricts the maximum degree of parallelism used in a parallel index operation to the specified number or less based on the current system workload.

- `0` (default)

   Uses the degree of parallelism specified at the server, database, or workload group level, unless reduced based on the current system workload.

   For more information, see [Configure parallel index operations](../../relational-databases/indexes/configure-parallel-index-operations.md).

   Parallel index operations aren't available in every edition of SQL Server. [!INCLUDE [editions-latest](../../includes/editions-latest.md)]

## Upgrade vector indexes to the latest version

> [!IMPORTANT]
> **Deprecation notice**: The current release supports vector indexes created by using an earlier data structure, but a future version will retire that support. To ensure future compatibility and access to the latest vector search capabilities, migrate existing vector indexes by using the following steps.

Newly created vector indexes automatically use the latest data structure, which provides:

- **[Full DML support](#dml-support)**: Removes the previous limitation that made vector-indexed tables read-only after index creation. You can now perform `INSERT`, `UPDATE`, `DELETE`, and `MERGE` operations while maintaining vector index functionality with automatic, real-time index maintenance.
- **[Iterative filtering](../functions/vector-search-transact-sql.md#iterative-filtering-behavior)**: Applies predicates in the `WHERE` clause during the vector search process, not after retrieval.
- **Optimizer-driven**: The query optimizer automatically determines whether to use the DiskANN index or kNN search based on query characteristics.
- **Advanced quantization**: Integrates vector quantization techniques to provide better storage efficiency and faster query performance. These optimizations are transparent to users.

For details about [earlier vector index version limitations](#earlier-vector-index-version-limitations), see the Limitations and considerations section.

### Migrating from earlier vector index versions

To enable the latest capabilities, you must drop and recreate vector indexes that use an earlier version. This section explains how to identify, migrate, and verify vector index versions.

#### Step 1: Identify existing vector indexes

Use the following query to identify vector indexes that require migration:

```sql
SELECT
    i.name AS index_name,
    t.name AS table_name,
    JSON_VALUE(v.build_parameters, '$.Version') AS index_version,
    CASE
        WHEN JSON_VALUE(v.build_parameters, '$.Version') >= '3'
            THEN 'Uses latest version (no migration required)'
        WHEN JSON_VALUE(v.build_parameters, '$.Version') < '3'
            THEN 'Created using an earlier version (migration recommended)'
        ELSE 'Unknown format'
    END AS migration_status
FROM sys.vector_indexes AS v
    INNER JOIN sys.indexes AS i
        ON v.object_id = i.object_id
        AND v.index_id = i.index_id
    INNER JOIN sys.tables AS t
        ON v.object_id = t.object_id
ORDER BY t.name, i.name;
```

##### How to interpret the results

**Uses latest version**

- Already supports iterative filtering, full DML support, optimizer-driven execution, and improved quantization.
- No migration required.

**Created using an earlier version**

- Uses legacy post-filter behavior.
- Doesn't support the latest vector search capabilities.
- Migration is strongly recommended to ensure future compatibility.

#### Step 2: Drop and recreate the vector index

You can't upgrade vector indexes that use an earlier format in place. To enable the latest DiskANN capabilities, drop and recreate the index.

> [!WARNING]
> **Service impact**: Dropping a vector index immediately disables approximate vector search on the affected table until the index is recreated. Plan migrations during maintenance windows for production systems.

##### Drop the existing index

```sql
DROP INDEX vec_idx ON dbo.wikipedia_articles;
```

##### Recreate the index

```sql
CREATE VECTOR INDEX vec_idx
    ON dbo.wikipedia_articles (title_vector)
    WITH (
        TYPE = 'DISKANN',
        METRIC = 'COSINE'
    );
```

> [!NOTE]
> Vector indexes created by using the current `CREATE VECTOR INDEX` statement automatically use the latest DiskANN format. No additional options or flags are required.

#### Step 3: Verify the index version

After you recreate the index, verify that it's using the latest version:

```sql
SELECT
    i.name AS index_name,
    t.name AS table_name,
    JSON_VALUE(v.build_parameters, '$.Version') AS index_version
FROM sys.vector_indexes AS v
    INNER JOIN sys.indexes AS i
        ON v.object_id = i.object_id
        AND v.index_id = i.index_id
    INNER JOIN sys.tables AS t
        ON v.object_id = t.object_id
WHERE i.name = 'vec_idx';
```

The `index_version` column should display `3` for the latest version.

#### Error behavior with version incompatibility

If you attempt to use the `TOP_N` parameter in `VECTOR_SEARCH` with a latest version vector index, SQL Server returns the following error:

```output
Msg 42274, Level 16, State 1
Vector search with version 3 index does not support explicit TOP_N parameter.
```

To resolve this error, remove the `TOP_N` parameter from `VECTOR_SEARCH` and use the `SELECT TOP (N) WITH APPROXIMATE` syntax instead. For detailed information, see [Error using legacy syntax](../functions/vector-search-transact-sql.md#error-using-legacy-syntax).

## Limitations and considerations

### Earlier vector index version limitations

Earlier vector index versions have the following limitations. To check your index version, see [Verify the index version](#step-3-verify-the-index-version).

- **[Post-filtering only](../functions/vector-search-transact-sql.md#iterative-filtering-behavior)**: Predicates apply only after vector retrieval, not during the search process. This behavior can result in fewer rows returned than expected when you apply filters.

- **Read-only tables**: Tables with vector indexes are read-only. You can't perform DML operations (`INSERT`, `UPDATE`, `DELETE`, `MERGE`) after creating the vector index. Use the `ALLOW_STALE_VECTOR_INDEX` database scoped configuration to enable DML operations if you can tolerate stale search results.

- **Manual TOP_N tuning**: You must manually adjust the `TOP_N` parameter in `VECTOR_SEARCH` to compensate for post-filtering. You often need oversized values to get the desired number of results.

### Current limitations (applies to the latest version too)

The current preview has the following limitations:

- You can't partition vector indexes.

- The table must have a primary key clustered index on an **int** column.

- Vector indexes aren't replicated to subscribers.

- You can't truncate tables with vector indexes by using `TRUNCATE TABLE`. To remove all data, drop the vector index first, truncate the table, repopulate with at least 100 rows, then recreate the index. For more information, see [TRUNCATE TABLE restrictions](../functions/vector-search-transact-sql.md#truncate-table-restrictions).

- Vector indexes can't be deployed with DacPac. You can deploy databases that contain vector indexes with BACPAC starting with [DacFx version 170.5.96](https://www.nuget.org/packages/Microsoft.SqlServer.DacFx/170.5.96). Vector indexes are created after the table data is loaded, and the indexed table must contain at least 100 rows with non-`NULL` vector values.

  During a schema-only DACPAC deployment, the deployment can succeed without creating the vector index if the target table contains fewer than 100 rows with non-`NULL` vector values. DacFx doesn't automatically create the skipped index when rows are added later. After loading at least 100 qualifying rows, create the vector index by using a post-deployment script, publishing the DACPAC again, or running `CREATE VECTOR INDEX`.

  After an import or deployment, verify that the vector index was created by querying `sys.vector_indexes`.

  As a workaround before DacFx version 170.5.96, drop vector indexes before exporting the database, and recreate the indexes after import.

### Minimum data requirements

Vector indexes require a minimum number of rows with non-`NULL` vector values before you can create the index.

- **Minimum row count**: The table must have at least 100 rows with non-`NULL` vector values.
- **Error behavior**: If you try to create a vector index on a table with fewer than 100 rows, the operation fails and returns error Msg 42266.

**Example error:**

```
Msg 42266, Level 16, State 1
Cannot create a vector index. The table contains only 8 rows with non-null vectors, 
but at least 100 are required for vector index creation.
```

**Best practice**: Populate the table with at least 100 rows before creating the vector index. For development and testing scenarios where you need fewer rows, `VECTOR_SEARCH` works without an index by using a brute-force scan approach. However, this method degrades performance with larger datasets.

## DML support

When you create a DiskANN vector index by using the latest version, the table is no longer read-only. You can modify data by using standard data manipulation language (DML) operations, and the changes automatically appear in vector search results.

This capability makes vector search suitable for live, transactional workloads where data changes over time.

### Behavior notes

- DML operations don't require dropping or rebuilding the vector index.
- Changes are visible to vector search queries after the transaction commits.
- For large-scale data replacement (for example, deleting most rows and inserting an entirely new set of embeddings), consider dropping and recreating the vector index after the data load to ensure optimal search quality.

> [!NOTE]
> DML support is only available with vector indexes created by using the latest version. Earlier versions require tables to be read-only or use the `ALLOW_STALE_VECTOR_INDEX` database scoped configuration.

### Monitoring vector index maintenance

Vector indexes perform background maintenance to incorporate DML changes. Use the [sys.dm_db_vector_indexes](../../relational-databases/system-dynamic-management-objects/sys-dm-db-vector-indexes-transact-sql.md) dynamic management view to monitor index health and maintenance task status.

## Combining vector indexes with traditional indexes

Vector indexes work alongside traditional B-tree indexes to provide optimal query performance. When using iterative filtering with `VECTOR_SEARCH`, consider creating traditional indexes on columns used in filter predicates.

For detailed information about iterative filtering behavior and how it differs from earlier versions, see [Iterative filtering behavior](../functions/vector-search-transact-sql.md#iterative-filtering-behavior).

> [!TIP]
> The query optimizer automatically selects the best execution strategy (approximate nearest neighbor index vs. kNN search). To force the use of the approximate nearest neighbor index, use the `FORCE_ANN_ONLY` table hint. For more information, see [Table hints for vector search](../functions/vector-search-transact-sql.md#table-hints-for-vector-search).

**Example scenario:**

```sql
-- Create vector index for similarity search
CREATE VECTOR INDEX idx_embeddings_vector
ON product_embeddings(embedding)
WITH (METRIC = 'cosine');

-- Create traditional index for filter columns
CREATE NONCLUSTERED INDEX idx_embeddings_filters
ON product_embeddings(category);
```

**Performance benefit:**

When executing queries with iterative filtering, the SQL Server query optimizer uses both index types:

```sql
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDINGS(N'wireless headphones' USE MODEL EmbeddingModel);

SELECT TOP (10) WITH APPROXIMATE
    p.name,
    p.price,
    vs.distance
FROM products p
INNER JOIN VECTOR_SEARCH(
    TABLE = product_embeddings AS e,
    COLUMN = embedding,
    SIMILAR_TO = @qv,
    METRIC = 'cosine'
) AS vs ON p.id = e.product_id
WHERE e.approved = 1             
  AND e.category = 'Electronics'  -- Can use traditional index
ORDER BY vs.distance;
```

In this query:

- The **vector index** identifies similar embeddings based on the query vector
- The **traditional index** on `(category)` filters candidates efficiently during the iterative search process

This composite strategy can improve query performance significantly compared to using only a vector index, particularly when filter predicates have high selectivity.


## Data quality and maintenance guidance for vector indexes

### Avoid datasets with high duplicate embeddings

Vector indexing works best when embeddings represent diverse semantic content. Don't use datasets with a high proportion of duplicate vectors for vector indexing.

High duplication can lead to:

- **Poor result quality**: Duplicate vectors appear repeatedly in results, crowding out more relevant semantic matches.
- **Reduced effectiveness**: Duplicate embeddings displace better neighbors, lowering the usefulness of similarity search.
- **Unnecessary resource usage**: Vector indexes are expensive to build and maintain, and duplicates add cost without adding value.

**Best practice**: Deduplicate embeddings before creating a vector index to improve both performance and result quality.

### Large-scale data replacement scenarios

Vector indexes support inserts, updates, and deletes. However, when you replace most or all embeddings—for example, re-embedding a dataset with a new model—the existing index might no longer reflect the new data distribution.

In large-scale replacement scenarios:

- Vector search queries continue to return valid results.
- Recall and ranking quality might degrade, because the index structure was built for a different embedding distribution.

**Best practice**: When performing near-complete data replacement (delete and insert of fresh embeddings), drop and recreate the vector index after loading the new data. Recreating the index ensures it's optimized for the new embedding distribution and restores predictable query behavior.

## Known issues

For more information, review [Known issues](../../sql-server/sql-server-2025-known-issues.md#vector-index).

## Permissions

You need `ALTER` permission on the table.

## Examples

Download and import the [Wikipedia article with vector embeddings](https://github.com/Azure-Samples/azure-sql-db-openai?tab=readme-ov-file#download-and-import-the-wikipedia-article-with-vector-embeddings) sample.

These examples assume a table named `wikipedia_articles` with a column `title_vector` of type `vector` that stores the title embeddings of Wikipedia articles. The `title_vector` uses an embedding model like *text-embedding-ada-002* or *text-embedding-3-small*, which returns vectors with 1,536 dimensions.

For more examples, including end-to-end solutions, see the [Azure SQL Database Vector Search Samples GitHub repo](https://github.com/Azure-Samples/azure-sql-db-vector-search).

### Example 1

The following example creates a vector index on the `title_vector` column using the `cosine` metric.

```sql
CREATE VECTOR INDEX vec_idx
    ON [dbo].[wikipedia_articles] ([title_vector])
        WITH (METRIC = 'COSINE', TYPE = 'DISKANN');
```

### Example 2

The following example creates a vector index on the `title_vector` column using the (negative) `dot` product metric. It limits the parallelism to 8 and stores the vector in the `SECONDARY` filegroup.

```sql
CREATE VECTOR INDEX vec_idx
    ON [dbo].[wikipedia_articles] ([title_vector])
        WITH (METRIC = 'DOT', TYPE = 'DISKANN', MAXDOP = 8)
    ON [SECONDARY];
```

### Example 3

This example uses `CREATE VECTOR INDEX` and the related `VECTOR_SEARCH` function. The embeddings are mocked. In a real-world scenario, generate embeddings by using an embedding model and [AI_GENERATE_EMBEDDINGS](../functions/ai-generate-embeddings-transact-sql.md), or use an external library such as [OpenAI SDK](https://github.com/openai/openai-dotnet?tab=readme-ov-file#how-to-generate-text-embeddings).

> [!NOTE]
> Latest version vector indexes require at least 100 rows of data before you can create the index. This example inserts 100 rows to meet this requirement. For more information, see [Minimum data requirements](#minimum-data-requirements).

The following code block demonstrates `CREATE VECTOR INDEX` with mock embeddings:

1. Enable the preview feature (required for SQL Server 2025 only; not needed for Azure SQL Database or SQL database in Fabric).
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
        TOP_N = 3
    ) AS s
ORDER BY s.distance, t.title;
```

### Example 4: Working with DML operations

The following examples demonstrate DML operations on a table with a vector index created by using the latest version.

#### Delete rows

Deleting rows removes them from both the table and vector search results.

```sql
DELETE FROM dbo.wikipedia_articles
WHERE id = 12345;
```

After the delete operation finishes, the removed row no longer appears in vector search queries.

#### Insert new rows

You can insert new rows with embeddings, and they become immediately searchable without rebuilding the index.

```sql
INSERT INTO dbo.wikipedia_articles (id, title, title_vector)
VALUES (
    99999,
    N'Quantum Computing Basics',
    AI_GENERATE_EMBEDDINGS(N'Quantum Computing Basics' USE MODEL Ada2Embeddings)
);
```

Newly inserted embeddings are automatically incorporated into the vector index and can be returned by subsequent vector search queries.

#### Update existing rows

Updating vector or non-vector columns is fully supported.

```sql
DECLARE @new_embedding VECTOR(1536);
SET @new_embedding = AI_GENERATE_EMBEDDINGS(N'Updated article title' USE MODEL Ada2Embeddings);

UPDATE dbo.wikipedia_articles
SET title_vector = @new_embedding,
    title = N'Updated article title'
WHERE id = 50000;
```

If you update a vector column, the index updates accordingly so future vector searches use the new embedding.

#### Use MERGE for complex operations

The `MERGE` statement allows you to perform insert, update, and delete operations in a single statement.

```sql
MERGE INTO dbo.wikipedia_articles AS target
USING (
    SELECT 
        id,
        title,
        AI_GENERATE_EMBEDDINGS(title USE MODEL Ada2Embeddings) AS title_vector
    FROM dbo.staging_articles
) AS source
ON target.id = source.id
WHEN MATCHED THEN
    UPDATE SET 
        title = source.title,
        title_vector = source.title_vector
WHEN NOT MATCHED BY TARGET THEN
    INSERT (id, title, title_vector)
    VALUES (source.id, source.title, source.title_vector)
WHEN NOT MATCHED BY SOURCE AND target.id > 100000 THEN
    DELETE;
```

The vector index automatically updates to reflect all changes made by the `MERGE` statement.

## Related content

- [Vector search and vector indexes in the SQL Database Engine](../../sql-server/ai/vectors.md)
- [Vector data type](../data-types/vector-data-type.md)
- [VECTOR_SEARCH (Transact-SQL)](../functions/vector-search-transact-sql.md)
- [sys.vector_indexes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-vector-indexes-transact-sql.md)
- [sys.dm_db_vector_indexes (Transact-SQL)](../../relational-databases/system-dynamic-management-objects/sys-dm-db-vector-indexes-transact-sql.md)
- [Azure SQL Database Vector Search Samples](https://github.com/Azure-Samples/azure-sql-db-vector-search)
