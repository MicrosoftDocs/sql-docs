---
title: "CREATE VECTOR INDEX (Transact-SQL)"
description: "CREATE VECTOR INDEX creates an index on vector data to allow approximate nearest neighbor search"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: damauri
ms.date: 07/08/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
  - "TSQL"
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17"
ms.custom:
  - build-2025
---

# CREATE VECTOR INDEX (Transact-SQL)

[!INCLUDE [SQL Server 2025](../../includes/applies-to-version/_ss2025.md)]

Create an approximate index on a vector column to improve performances of nearest neighbors search. To learn more about how vector indexing and vector search works, and the differences between exact and approximate search, refer to [Vectors in the SQL Database Engine](../../relational-databases/vectors/vectors-sql-server.md).

## Preview feature

> [!NOTE]
> This feature in preview and is subject to change. Make sure to read preview usage terms in [Service Level Agreements (SLA) for Online Services](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services).

This feature is in preview. In order to use this feature you must enable the following [trace flags](../database-console-commands/dbcc-traceon-transact-sql.md):

```sql
DBCC TRACEON(466, 474, 13981, -1)
```

Make sure to check out the [current limitations](#limitations) before using it.

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

```syntaxsql
CREATE VECTOR INDEX index_name
ON object ( vector_column )  
[ WITH (
    [,] METRIC = { 'cosine' | 'dot' | 'euclidean' }
    [ [,] TYPE = 'DiskANN' ]
    [ [,] MAXDOP = max_degree_of_parallelism ]
    [ [,] DROP_EXISTING = { ON | OFF } ]
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

### METRIC = { 'cosine' | 'dot' | 'euclidean' }

A string with the name of the distance metric to use to calculate the distance between the two given vectors. The following distance metrics are supported:

- `cosine` - Cosine distance
- `euclidean` - Euclidean distance
- `dot` - (Negative) Dot product

## TYPE = 'DiskANN'

The type of [ANN algorithm](../../relational-databases/vectors/vectors-sql-server.md#approximate-vector-index-and-vector-search-approximate-nearest-neighbors) used to build the index. Only `DiskANN` is currently supported. DiskANN is the default value.

## MAXDOP = *max_degree_of_parallelism*

Overrides the **max degree of parallelism** configuration option for the index operation. For more information, see [Max degree of parallelism](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md). Use `MAXDOP` to limit the degree of parallelism and the resulting resource consumption for an index build operation.

*max_degree_of_parallelism* can be:

- `1`

  Suppresses parallel plan generation.

- \>1

  Restricts the maximum degree of parallelism used in a parallel index operation to the specified number or less based on the current system workload.

- `0` (default)

  Uses the degree of parallelism specified at the server, database, or workload group level, unless reduced based on the current system workload.

For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).

> [!NOTE]  
> Parallel index operations aren't available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md) or [Editions and supported features of SQL Server 2025 Preview](../../sql-server/editions-and-components-of-sql-server-2025.md).

## Limitations

The current preview has the following limitations:

- Vector index can't be partitioned. No partition support.
- The table must have a single column, integer, primary key clustered index.
- A table with a vector index becomes read only. No data modification is allowed while the vector index is present on the table.
- Vector indexes aren't replicated to subscribers.

## Permissions

The user must have `ALTER` permission on the table.

## Examples

Details of the database used in the sample can be found here: [Download and import the Wikipedia Article with Vector Embeddings](https://github.com/Azure-Samples/azure-sql-db-openai?tab=readme-ov-file#download-and-import-the-wikipedia-article-with-vector-embeddings).

Examples assume the existence of a table named `wikipedia_articles` with a column `title_vector` of type `vector` that stores title's embeddings of Wikipedia articles. `title_vector` is assumed to be an embedding generated with an embedding model like *text-embedding-ada-002* or *text-embedding-3-small*, which returns vectors with 1,536 dimensions.

For more examples, including end-to-end solutions, go to the [Azure SQL Database Vector Search Samples GitHub repo](https://github.com/Azure-Samples/azure-sql-db-vector-search).


### Example 1

The following example creates a vector index on the `title_vector` column using the `cosine` metric.

```sql
CREATE VECTOR INDEX vec_idx ON [dbo].[wikipedia_articles]([title_vector]) 
WITH (METRIC = 'cosine', TYPE = 'diskann'); 
```

### Example 2

The following example creates a vector index on the `title_vector` column using the (negative) `dot` product metric, limiting the parallelism to 8 and storing the vector in the `SECONDARY` filegroup.

```sql
CREATE VECTOR INDEX vec_idx ON [dbo].[wikipedia_articles]([title_vector]) 
WITH (METRIC = 'cosine', TYPE = 'diskann', MAXDOP = 8)
ON [SECONDARY]
```

### Example 3

A basic end-to-end example using `CREATE VECTOR INDEX` and the related `VECTOR_SEARCH` function. The embeddings are mocked. In a real world scenario, embeddings are generated using an embedding model and [AI_GENERATE_EMBEDDINGS](../functions/ai-generate-embeddings-transact-sql.md), or an external library such as [OpenAI SDK](https://github.com/openai/openai-dotnet?tab=readme-ov-file#how-to-generate-text-embeddings).

The following code block creates mock embeddings with the following steps:

1. Enables the trace flag, necessary in the current preview.
1. Create a sample table `dbo.Articles` with a column `embedding` with data type **vector(5)**.
1. Insert sample data with mock embedding data.
1. Create a vector index on `dbo.Articles.embedding`.
1. Demonstrate the vector similarity search with the `VECTOR_SEARCH()` function.

```sql
-- Step 0: Enable Preview Feature
DBCC TRACEON(466, 474, 13981, -1);
GO

-- Step 1: Create a sample table with a VECTOR(5) column
CREATE TABLE dbo.Articles 
(
    id INT PRIMARY KEY,
    title NVARCHAR(100),
    content NVARCHAR(MAX),
    embedding VECTOR(5) -- mocked embeddings
);

-- Step 2: Insert sample data
INSERT INTO Articles (id, title, content, embedding)
VALUES
(1, 'Intro to AI', 'This article introduces AI concepts.', '[0.1, 0.2, 0.3, 0.4, 0.5]'),
(2, 'Deep Learning', 'Deep learning is a subset of ML.', '[0.2, 0.1, 0.4, 0.3, 0.6]'),
(3, 'Neural Networks', 'Neural networks are powerful models.', '[0.3, 0.3, 0.2, 0.5, 0.1]'),
(4, 'Machine Learning Basics', 'ML basics for beginners.', '[0.4, 0.5, 0.1, 0.2, 0.3]'),
(5, 'Advanced AI', 'Exploring advanced AI techniques.', '[0.5, 0.4, 0.6, 0.1, 0.2]');

-- Step 3: Create a vector index on the embedding column
CREATE VECTOR INDEX vec_idx ON Articles(embedding)
WITH (metric = 'cosine', type = 'diskann');

-- Step 4: Perform a vector similarity search
DECLARE @qv VECTOR(5) = '[0.3, 0.3, 0.3, 0.3, 0.3]';
SELECT
    t.id,
    t.title,
    t.content,
    s.distance
FROM
    VECTOR_SEARCH(
        table = Articles AS t,
        column = embedding,
        similar_to = @qv,
        metric = 'cosine',
        top_n = 3
    ) AS s
ORDER BY s.distance, t.title;
```

## Related content

- [Overview of vectors in the SQL Database Engine](../../relational-databases/vectors/vectors-sql-server.md)
- [Vector data type](../data-types/vector-data-type.md)
- [VECTOR_SEARCH (Transact-SQL)](../functions/vector-search-transact-sql.md)
- [sys.vector_indexes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-vector-indexes-transact-sql.md)
- [Azure SQL Database Vector Search Samples](https://github.com/Azure-Samples/azure-sql-db-vector-search)
