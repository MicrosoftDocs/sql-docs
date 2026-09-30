---
title: "sys.vector_indexes (Transact-SQL)"
description: "sys.vector_indexes contains a row per vector index."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest, markingmyname
ms.date: 09/15/2026
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
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.vector_indexes (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

Contains a row per vector index.

| Column name | Data type | Description |
| --- | --- | --- |
| **\<inherited columns>** | | Inherits columns from [sys.indexes](sys-indexes-transact-sql.md). |
| **vector_index_type** | varchar(20) | Type of vector index (DiskANN only for now) |
| **distance_metric** | varchar(20) | Metric used to create the vector index |
| **build_parameters** | nvarchar(max) | Internal usage only |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Remarks

- [Vector indexes](/sql/t-sql/statements/create-vector-index-transact-sql?view=azuresqlmi-current&preserve-view=true) are generally available in Azure SQL Database, SQL database in Fabric, and Azure SQL Managed Instance in the **Always up to date** [update policy](/azure/azure-sql/managed-instance/update-policy?view=azuresql-mi&preserve-view=true).
- [Vector indexes](/sql/t-sql/statements/create-vector-index-transact-sql?view=azuresqlmi-current&preserve-view=true) are preview features in SQL Server 2025 and Azure SQL Managed Instance in the **SQL Server 2025** [update policy](/azure/azure-sql/managed-instance/update-policy?view=azuresql-mi&preserve-view=true).

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

- [CREATE VECTOR INDEX (Transact-SQL)](../../t-sql/statements/create-vector-index-transact-sql.md)
- [sys.dm_db_vector_indexes (Transact-SQL)](../system-dynamic-management-objects/sys-dm-db-vector-indexes-transact-sql.md)
