---
title: Vector & Embeddings Frequently Asked Questions (FAQ)
description: Answers to common questions about vector search and vector indexes in SQL Server.
author: yorek
ms.author: damauri
ms.reviewer: damauri
ms.date: 07/17/2025
ms.service: sql
ms.topic: language-reference
ms.collection:
  - ce-skilling-ai-copilot
ms.custom:
  - intro-quickstart
  - build-2025
helpviewer_keywords:
  - "Vectors"
  - "Vectors, built-in support"
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric"
---

# Overview of vector search and vector indexes in the SQL Database Engine

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

> [!NOTE]
> - Vector features are available in Azure SQL Managed Instance configured with the [Always-up-to-date](/azure/azure-sql/managed-instance/update-policy#always-up-to-date-update-policy) policy. 

## How do you keep embedding up to date?

Embeddings should be updated every time the underlying data that they represent changes. This is especially important for scenarios where the data is dynamic, such as user-generated content or frequently updated databases. There are several strategies to keep embeddings up to date, all fully described in this article: [Database and AI: solutions for keeping embeddings updated](https://devblogs.microsoft.com/azure-sql/database-and-ai-solutions-for-keeping-embeddings-updated/)

## What is the overhead storage and processing for vector search?

The overhead for vector search primarily involves the storage of the vector data type and the computational resources required for indexing and searching. The `VECTOR` data type is designed to be efficient in terms of storage, but the exact overhead can vary based on the size - the number of dimensions - of the vectors stored.

Read more about how to chose the right vector size in this article: [Embedding models and dimensions: optimizing the performance-resource usage ratio](https://devblogs.microsoft.com/azure-sql/embedding-models-and-dimensions-optimizing-the-performance-resource-usage-ratio/)

Keep in mind that a SQL Server data page can hold up to 8,060 bytes, so the size of the vector will impact how many vectors can be stored in a single page. For example, if you have a vector with 1024 dimensions and each dimension is a float (4 bytes), the total size of the vector would be 4,104 bytes (4096 bytes payload + 8 bytes header), therefore limiting the number of vectors that can fit in a single page to one. 

## What embedding model to use and when?

There are many embedding models available, and the choice of which one to use depends on the specific use case and the type of data being processed. Some models support multiple languages, while others support multimodal data (text, images, etc.). Some are available only online, others can be run locally. 

In addition to the model itself, consider the size of the model and the number of dimensions it produces. Larger models may provide better accuracy but require more computational resources and storage space, but in many cases having more dimension doesn't really change the quality that much, for common use cases.

Read more about how to choose the right embedding model in this article: [Embedding models and dimensions: optimizing the performance-resource usage ratio](https://devblogs.microsoft.com/azure-sql/embedding-models-and-dimensions-optimizing-the-performance-resource-usage-ratio/)

## What about sparse vectors?

At this time, the `VECTOR` data type in SQL Server is designed for dense vectors, which are arrays of floating-point numbers where most of the elements are non-zero. Sparse vectors, which contain a significant number of zero elements, are not natively supported. 

## What are some perf benchmarks for sql vector search

There aren't official benchmarks available yet, as performance can vary widely based on the specific use case, the size of the dataset, and the complexity of the queries. However, SQL Server's vector search capabilities are designed to be efficient and scalable, leveraging indexing techniques to optimize search performance.

## What if I have more than one column that I would like to use for generating embeddings?

If you have multiple columns that you want to use for generating embeddings, you have two main options:

- Create one embedding for each column or,
- Concatenate the values of multiple columns into a single string and then generate a single embedding for that concatenated string.

Details on the two options and the related database design considerations are available in this article: [Efficiently and Elegantly Modeling Embeddings in Azure SQL and SQL Server](https://devblogs.microsoft.com/azure-sql/efficiently-and-elegantly-modeling-embeddings-in-azure-sql-and-sql-server/)

## What about re-ranking

Re-ranking is a technique used to improve the relevance of search results by re-evaluating the initial results based on additional criteria or models. In SQL Server, you can implement re-ranking by combining vector search with full-text (which provides BM25 ranking) or additional SQL queries or machine learning models to refine the results based on specific business logic or user preferences.

An explanation of how to implement re-ranking in SQL Server is available in this article: [Enhancing Search Capabilities in SQL Server and Azure SQL with Hybrid Search and RRF Re-Ranking](https://devblogs.microsoft.com/azure-sql/enhancing-search-capabilities-in-sql-server-and-azure-sql-with-hybrid-search-and-rrf-re-ranking/)

## When to use AI Search (now AI Foundry) vs using SQL for vectors search scenarios?

AI Search (now AI Foundry) is a specialized service designed for advanced search scenarios, including vector search, natural language processing, and AI-driven insights. It provides a comprehensive set of features for building intelligent search applications, such as built-in support for various AI models, advanced ranking algorithms, and integration with other AI services.

Azure SQL and SQL Server provides ability to store any kind of data and run any kind of query: structured and unstructured, and to perform vector search on that data. It is a good choice for scenarios where you need to do search across all these data together, and you don't want to use a separate service for search that would complicate your architecture. Azure SQL and SQL Server offers critical enterprise security features to make sure data is always protected, such as Row-Level Security (RLS), Dynamic Data Masking (DDM), Always Encrypted, Immutable Ledger Tables, and Transparent Data Encryption (TDE).

Here's an example of a single query that can be run in Azure SQL or SQL Server that combines vector, geospatial, structured and unstructured data all at once. The sample query retrieves the top 50 most relevant restaurants based on the description of the restaurant, the location of the restaurant, and the user's preferences, using vector search for the description and geospatial search for the location, filtering also by star numbers, number of reviews, category and so on:

```sql
declare @p geography = geography::Point(47.6694141, -122.1238767, 4326);
declare @e vector(1536) = ai_generate_embedding('I want to eat a good focaccia' use model Text3Embedding);
select top(50)
    b.id as business_id,
    b.name as business_name,
    r.id as review_id,
    r.stars,
    r.review,    
    vector_distance('cosine', re.embedding, @e) as semantic_distance,
    @p.STDistance(geo_location) as geo_distance
from
    dbo.reviews r
inner join  
    dbo.reviews_embeddings re on r.id = re.review_id
inner join 
    dbo.business b on r.business_id = b.id
where
    b.city = 'Redmond'
and
    @p.STDistance(b.geo_location) < 5000 -- 5 km
and
    r.stars >= 4
and
    b.reviews >= 30
and
    json_value(b.custom_attributes, '$.local_recommended') = 'true'
and
    vector_distance('cosine', re.embedding, @e) < 0.2
order by
    semantic_distance desc
```

In the above sample Exact Nearest Neighbor (ENN) search is used to find the most relevant reviews based on the semantic distance of the embeddings, while also filtering by geospatial distance and other business attributes. This query showcases the power of combining vector search with traditional SQL capabilities to create a rich and efficient search experience.

If you want to use Approximate Nearest Neighbor (ANN) search, you can create a vector index on the `reviews_embeddings` table and use the `VECTOR_SEARCH` function to perform the search.


## Where can I find a self-paced lab to learn more about embeddings and vector search?

You can use the funny and interactive self-paced lab "Azure SQL Cryptozoology AI Embeddings Lab". Read everything about it here: [Azure SQL Cryptozoology AI Embeddings Lab Now Available!](https://devblogs.microsoft.com/azure-sql/azure-sql-cryptozoology-ai-embeddings-lab-now-available/)


## Related content

- [Vector data type](../../t-sql/data-types/vector-data-type.md)
- [Vector functions](../../t-sql/functions/vector-functions-transact-sql.md)
- [VECTOR_DISTANCE (Transact-SQL)](../../t-sql/functions/vector-distance-transact-sql.md)
- [VECTOR_SEARCH (Transact-SQL)](../../t-sql/functions/vector-search-transact-sql.md)
- [CREATE VECTOR INDEX (Transact-SQL)](../../t-sql/statements/create-vector-index-transact-sql.md)
- [Azure SQL Database Vector Search Samples](https://github.com/Azure-Samples/azure-sql-db-vector-search)
- [Intelligent applications with Azure SQL Database](/azure/azure-sql/database/ai-artificial-intelligence-intelligent-applications)
