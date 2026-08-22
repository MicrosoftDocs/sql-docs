---
title: "sys.vector_indexes (Transact-SQL)"
description: "sys.vector_indexes contains a row per vector index."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest, markingmyname
ms.date: 08/11/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.vector_indexes"
  - "vector_indexes"
  - "sys.vector_indexes_TSQL"
  - "vector_indexes_TSQL"
helpviewer_keywords:
  - "sys.vector_indexes catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17"
---
# sys.vector_indexes (Transact-SQL)

[!INCLUDE [SQL Server 2025](../../includes/applies-to-version/sqlserver2025.md)]

Contains a row per vector index.

| Column name | Data type | Description |
| --- | --- | --- |
| **\<inherited columns>** | | Inherits columns from [sys.indexes](sys-indexes-transact-sql.md). |
| **vector_index_type** | varchar(20) | Type of vector index (DiskANN only for now) |
| **distance_metric** | varchar(20) | Metric used to create the vector index |
| **build_parameters** | nvarchar(max) | Internal usage only |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Examples

The following example returns all indexes for the table `[dbo].[wikipedia_articles_embeddings]` used in the [DiskANN sample](https://github.com/Azure-Samples/azure-sql-db-vector-search/tree/main/DiskANN/Wikipedia) available in the [GitHub sample repo](https://github.com/Azure-Samples/azure-sql-db-vector-search).

```sql
SELECT
  object_id,
  index_id,
  vector_index_type,
  distance_metric,
  build_parameters
FROM
  sys.vector_indexes AS vi
WHERE
  object_id = OBJECT_ID('[dbo].[wikipedia_articles_embeddings]')
```

## Related content

- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [sys.indexes (Transact-SQL)](sys-indexes-transact-sql.md)
- [CREATE VECTOR INDEX (Transact-SQL) (Preview)](../../t-sql/statements/create-vector-index-transact-sql.md)
- [sys.dm_db_vector_indexes (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-vector-indexes-transact-sql.md)
