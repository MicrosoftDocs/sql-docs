---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 11/18/2025
ms.service: sql
ms.topic: include
ms.custom:
  - ignite-2025
---

The following table identifies features originally released as preview and their current status:

| Feature | Current status | Version of latest update | Description |
| --- | --- | --- | --- |
| [Change event streaming](../../relational-databases/track-changes/change-event-streaming/overview.md) | Preview | RTM | Stream changes from SQL Server to Azure Event Hubs. |
| [Fuzzy string matching](../../relational-databases/fuzzy-string-match/overview.md) | Preview | RTM | Check if two strings are similar, and calculate the difference between two strings. |
| [EDIT_DISTANCE](../../t-sql/functions/edit-distance-transact-sql.md) | Preview | RTM | Calculate the number of insertions, deletions, substitutions, and transpositions needed to transform one string to another. |
| [EDIT_DISTANCE_SIMILARITY](../../t-sql/functions/edit-distance-similarity-transact-sql.md) | Preview | RTM | Calculate a similarity value ranging from 0 (indicating no match) to 100 (indicating full match). |
| [Half-precision (2-byte) vectors](../../t-sql/data-types/vector-data-type.md) | Preview | RTM | Store vectors using half-precision (2-byte) floating-point values, allowing up to 3996 dimensions in a single vector. |
| [JARO_WINKLER_DISTANCE](../../t-sql/functions/jaro-winkler-distance-transact-sql.md) | Preview | RTM | Calculate the edit distance between two strings giving preference to strings that match from the beginning for a set prefix length. |
| [JARO_WINKLER_SIMILARITY](../../t-sql/functions/jaro-winkler-similarity-transact-sql.md) | Preview | RTM | Calculate a similarity value ranging from 0 (indicating no match) to 100 (indicating full match). |
| [Vector index](../../sql-server/ai/vectors.md#vector-search) | Preview | RTM | Create and manage approximate vector indexes to find similar vectors to a given reference vector. |
| [CREATE VECTOR INDEX](../../t-sql/statements/create-vector-index-transact-sql.md) | Preview | RTM | Create an approximate index on a vector column to improve performances of nearest neighbors search. |
| [VECTOR_SEARCH](../../t-sql/functions/vector-search-transact-sql.md). | Preview | RTM | Search for vectors similar to a given query vectors using an approximate nearest neighbors vector search algorithm. |

> [!CAUTION]  
> Preview features aren't recommended for production environments.
