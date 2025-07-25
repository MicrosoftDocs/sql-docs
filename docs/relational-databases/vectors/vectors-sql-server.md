---
title: Vector Search & Vector Index in the SQL Database Engine
description: How to create, manage, and search vectors in the SQL Database Engine.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: damauri, pookam, jovanpop, randolphwest, mikeray
ms.date: 07/24/2025
ms.service: sql
ms.topic: language-reference
ms.collection:
  - ce-skilling-ai-copilot
ms.custom:
  - intro-quickstart
helpviewer_keywords:
  - "Vectors"
  - "Vectors, built-in support"
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric"
---

# Overview of vector search and vector indexes in the SQL Database Engine

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The SQL Database Engine provides the ability to store any kind of data and run any kind of query: structured and unstructured, and to perform vector search on that data. It is a good choice for scenarios where you need to do search across all these data together, and you don't want to use a separate service for search that would complicate your architecture.

> [!NOTE]
> - Vector support in preview and is subject to change. Make sure to read preview usage terms in [Service Level Agreements (SLA) for Online Services](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services). 

Vector features are available in Azure SQL Managed Instance configured with the [Always-up-to-date](/azure/azure-sql/managed-instance/update-policy#always-up-to-date-update-policy) policy. 

## Vectors 

Vectors are ordered arrays of numbers (typically floats) that can represent information about some data. For example, an image can be represented as a vector of pixel values, or a string of text can be represented as a vector of ASCII values. The process to turn data into a vector is called vectorization. The **[vector](../../t-sql/data-types/vector-data-type.md)** data type in SQL Server is designed to store these arrays of numbers efficiently.

## Embeddings

Embeddings are vectors that represent important features of data. Embeddings are often learned by using a deep learning model, and machine learning and AI models utilize them as features. Embeddings can also capture semantic similarity between similar concepts. For example, in generating an embedding for the words `person` and `human`, we would expect their embeddings (vector representation) to be similar in value since the words are also semantically similar.

Azure OpenAI features models to create embeddings from text data. The service breaks text out into tokens and generates embeddings using models pretrained by OpenAI. To learn more, see [Creating embeddings with Azure OpenAI](/azure/ai-services/openai/concepts/understand-embeddings).

Once embeddings are generated, they can be stored into a SQL Server database. This allows you to store the embeddings alongside the data they represent, and to perform vector search queries to find similar data points.

## Vector search

Vector search refers to the process of finding all vectors in a dataset that are similar to a specific query vector. Therefore, a query vector for the word `human` searches the entire dataset for similar vectors, and thus similar words: in this example it should find the word `person` as a close match. This closeness, or distance, is measured using a distance metric such as cosine distance. The closer vectors are, the more similar they are.

SQL Server provides built-in support for vectors via the **vector** data type. Vectors are stored in an optimized binary format but exposed as JSON arrays for convenience. Each element of the vector is stored using single-precision (4 bytes) floating-point value. Along with the data type there are dedicated functions to operate on vectors. For example, it's possible to find the distance between two vectors using the [VECTOR_DISTANCE](../../t-sql/functions/vector-distance-transact-sql.md) function. The function returns a scalar value with the distance between two vectors based on the distance metric you specify.

Since vectors are typically managed as arrays of floats, creating a vector can be done simply casting a JSON array to a **vector** data type. For example, the following code creates a vector from a JSON array:

```sql
SELECT 
    CAST('[1.0, -0.2, 30]' AS VECTOR(3)) AS v1,
    CAST(JSON_ARRAY(1.0, -0.2, 30) AS VECTOR(3)) AS v2;
```

Or, use implicit casting

```sql
DECLARE @v1 VECTOR(3) = '[1.0, -0.2, 30]';
DECLARE @v2 VECTOR(3) = JSON_ARRAY(1.0, -0.2, 30);
SELECT @v1 as v1, @v2 as v2;
```

Same goes for converting a vector into a JSON array:

```sql
DECLARE @v VECTOR(3) = '[1.0, -0.2, 30]';
SELECT 
    CAST(@v AS NVARCHAR(MAX)) AS s,
    CAST(@v AS JSON) AS j
```

### Exact Search and Vector Distance (Exact Nearest Neighbors)

Exact Search, also known as k-Nearest Neighbor (k-NN) search, involves calculating the distance between a given vector and all other vectors in a dataset, sorting the results, and selecting the closest neighbors based on a specified distance metric. This method guarantees precise retrieval of the nearest neighbors but can be computationally intensive, especially for large datasets.

Vector Distance functions are used to measure the closeness between vectors. Common distance metrics include Euclidean distance, cosine similarity, and dot product. These functions are essential for performing k-NN searches and ensuring accurate results.

Exact Nearest Neighbor (ENN) Vector Search performs an exhaustive distance calculation across all indexed vectors to guarantee the retrieval of the closest neighbors based on a specified distance metric. This method is precise but resource-intensive, making it suitable for smaller datasets or scenarios where accuracy is paramount.

In the SQL Database Engine, k-NN searches can be performed using the [VECTOR_DISTANCE](../../t-sql/functions/vector-distance-transact-sql.md) function, which allows for efficient calculation of distances between vectors and facilitates the retrieval of the nearest neighbors.

The following example shows how to do k-NN to return the top 10 most similar vectors stored in the `content_vector` table to the given query vector `@qv`.

```sql
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDING(N'Pink Floyd music style' USE MODEL Ada2Embeddings);
SELECT TOP (10) id, VECTOR_DISTANCE('cosine', @qv, [content_vector]) AS distance, title
FROM [dbo].[wikipedia_articles_embeddings]
ORDER BY distance
```

Using an exact search is recommended when you don't have many vectors to search on (less than 50,000 vectors as a general recommendation). The table can contain many more vectors as long as your search predicates reduce the number of vectors to use for neighbor search to 50,000 or fewer.

### Approximate Vector Index and Vector Search (Approximate Nearest Neighbors)

> [!NOTE]
> Approximate Vector Index and Vector Search is in preview and is currently available only in SQL Server 2025 preview.

Identifying all vectors close to a given query vector requires substantial resources to calculate the distance between the query vector and the vectors stored in the table. Searching for all vectors close to a given query vector involves a complete scan of the table and significant CPU usage. This is called a "K-Nearest Neighbors" or "KNN" query and returns the "k" closest vectors.

Vectors are used to find similar data for AI models to answer user queries. This involves querying the database for the "k" vectors nearest to the query vector using distance metrics like dot (inner) product, cosine similarity, or Euclidean distance.

KNN queries often struggle with scalability, making it acceptable in many cases to trade off some accuracy, particularly recall, for significant speed gains. This method is known as Approximate Nearest Neighbors (ANN). 

Recall is an important concept that should become familiar to everyone using or planning to use vectors and embeddings. In fact, recall measures the proportion of the approximate nearest neighbors that are identified by the algorithm, compared to the exact nearest neighbors that an exhaustive search would return. Therefore, it is a good measurement of the quality of the approximation that the algorithm is doing. A perfect recall, which is equivalent to no approximation, is 1. 

For AI applications, the trade-off is quite reasonable. Since vector embeddings already approximate concepts, using ANN doesn't significantly affect the results, provided the recall is close to 1. This ensures that the returned results are very similar to those from KNN, while offering vastly improved performance and significantly reduced resource usage, which is highly beneficial for operational databases.

It is important to understand that the term "index" when used referring to a [vector index](../../t-sql/statements/create-vector-index-transact-sql.md) has a different meaning than the index you are used to working with in relational databases. In fact, a vector index returns approximate results.

In MSSQL engine, vector indexes are based on the [DiskANN](https://www.microsoft.com/en-us/research/publication/diskann-fast-accurate-billion-point-nearest-neighbor-search-on-a-single-node) algorithm. DiskANN relies on creating a graph to navigate quickly through all the indexed vectors to find the closest match to a given vector. DiskANN is a graph-based system for indexing and searching large sets of vector data using limited computational resources. It efficiently uses SSDs and minimal memory to handle significantly more data than in-memory indices, while maintaining high queries per second (QPS) and low latency, ensuring a balance between memory, CPU and I/O usage and search performance.

An Approximate Nearest Neighbors algorithm search can be done first creating a vector index using the [CREATE VECTOR INDEX](../../t-sql/statements/create-vector-index-transact-sql.md) T-SQL command and then using [VECTOR_SEARCH](../../t-sql/functions/vector-search-transact-sql.md) T-SQL function to run the approximate search.

```sql
DECLARE @qv VECTOR(1536) = AI_GENERATE_EMBEDDING(N'Pink Floyd music style' USE MODEL Ada2Embeddings);
SELECT 
    t.id, s.distance, t.title
FROM
    VECTOR_SEARCH(
        TABLE = [dbo].[wikipedia_articles_embeddings] AS t, 
        COLUMN = [content_vector], 
        SIMILAR_TO = @qv, 
        METRIC = 'cosine', 
        TOP_N = 10
    ) AS s
ORDER BY s.distance
```

## Related content

- [Vector data type](../../t-sql/data-types/vector-data-type.md)
- [Vector functions](../../t-sql/functions/vector-functions-transact-sql.md)
- [VECTOR_DISTANCE (Transact-SQL)](../../t-sql/functions/vector-distance-transact-sql.md)
- [VECTOR_SEARCH (Transact-SQL)](../../t-sql/functions/vector-search-transact-sql.md)
- [CREATE VECTOR INDEX (Transact-SQL)](../../t-sql/statements/create-vector-index-transact-sql.md)
- [Azure SQL Database Vector Search Samples](https://github.com/Azure-Samples/azure-sql-db-vector-search)
- [Intelligent applications with Azure SQL Database](/azure/azure-sql/database/ai-artificial-intelligence-intelligent-applications)
