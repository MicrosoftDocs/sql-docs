---
title: "sys.indexes (Transact-SQL)"
description: "The sys.indexes catalog view contains a row per index or heap of a tabular object, such as a table, view, or table-valued function."
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/24/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.indexes"
  - "indexes"
  - "sys.indexes_TSQL"
  - "indexes_TSQL"
helpviewer_keywords:
  - "sys.indexes catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.indexes (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Contains a row per index or heap of a tabular object, such as a table, view, or table-valued function.

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | ID of the object to which this index belongs. |
| `name` | **sysname** | Name of the index. name is unique only within the object.<br /><br />NULL = Heap |
| `index_id` | **int** | ID of the index. index_id is unique only within the object.<br /><br />0 = Heap<br />1 = Clustered index<br />> 1 = Nonclustered index |
| `type` | **tinyint** | Type of index:<br /><br />0 = Heap<br />1 = Clustered rowstore (B-tree)<br />2 = Nonclustered rowstore (B-tree)<br />3 = XML<br />4 = Spatial<br />5 = Clustered columnstore index <sup>2</sup><br />6 = Nonclustered columnstore index <sup>1</sup><br />7 = Nonclustered hash index <sup>2</sup><br />9 = JSON <sup>5</sup> |
| `type_desc` | **nvarchar(60)** | Description of index type:<br /><br />- HEAP<br />- CLUSTERED<br />- NONCLUSTERED<br />- XML<br />- SPATIAL<br />- CLUSTERED COLUMNSTORE <sup>2</sup><br />- NONCLUSTERED COLUMNSTORE <sup>1</sup><br />- NONCLUSTERED HASH <sup>2, 8</sup><br />- JSON <sup>5</sup> |
| `is_unique` | **bit** | 1 = Index is unique.<br />0 = Index isn't unique.<br /><br />Always 0 for clustered columnstore indexes. |
| `data_space_id` | **int** | ID of the data space for this index. Data space is either a filegroup or partition scheme.<br /><br />0 = object_id is a table-valued function or in-memory index. |
| `ignore_dup_key` | **bit** | 1 = IGNORE_DUP_KEY is ON.<br />0 = IGNORE_DUP_KEY is OFF. |
| `is_primary_key` | **bit** | 1 = Index is part of a PRIMARY KEY constraint.<br /><br />Always 0 for clustered columnstore indexes. |
| `is_unique_constraint` | **bit** | 1 = Index is part of a UNIQUE constraint.<br /><br />Always 0 for clustered columnstore indexes. |
| `fill_factor` | **tinyint** | > 0 = FILLFACTOR percentage used when the index was created or rebuilt.<br />0 = Default value<br /><br />Always 0 for clustered columnstore indexes. |
| `is_padded` | **bit** | 1 = PADINDEX is ON.<br />0 = PADINDEX is OFF.<br /><br />Always 0 for clustered columnstore indexes. |
| `is_disabled` | **bit** | 1 = Index is disabled.<br />0 = Index isn't disabled. |
| `is_hypothetical` | **bit** | 1 = Index is hypothetical and can't be used directly as a data access path. Hypothetical indexes hold column-level statistics.<br /><br />0 = Index isn't hypothetical. |
| `allow_row_locks` | **bit** | 1 = Index allows row locks.<br />0 = Index doesn't allow row locks.<br /><br />Always 0 for clustered columnstore indexes. |
| `allow_page_locks` | **bit** | 1 = Index allows page locks.<br />0 = Index doesn't allow page locks.<br /><br />Always 0 for clustered columnstore indexes. |
| `has_filter` | **bit** | 1 = Index has a filter and only contains rows that satisfy the filter definition.<br />0 = Index doesn't have a filter. |
| `filter_definition` | **nvarchar(max)** | Expression for the subset of rows included in the filtered index.<br /><br />NULL for heap, nonfiltered index, or insufficient permissions on the table. |
| `compression_delay` | **int** | > 0 = Columnstore index compression delay specified in minutes.<br /><br />NULL = Columnstore index rowgroup compression delay is managed automatically. |
| `suppress_dup_key_messages` <sup>3, 6, 7</sup> | **bit** | 1 = Index is configured to suppress duplicate key messages during an index rebuild operation.<br /><br />0 = Index isn't configured to suppress duplicate key messages during an index rebuild operation. |
| `auto_created` <sup>6</sup> | **bit** | 1 = Index was created by the automatic tuning.<br />0 = Index was created by the user. |
| `optimize_for_sequential_key` <sup>4, 6, 7</sup> | **bit** | 1 = Index has last-page insert optimization enabled.<br />0 = Default value. Index has last-page insert optimization disabled. |

<sup>1</sup> **Applies to:** [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and later versions.

<sup>2</sup> **Applies to:** [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and later versions.

<sup>3</sup> **Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions.

<sup>4</sup> **Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions.

<sup>5</sup> **Applies to:** [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

<sup>6</sup> **Applies to:** [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

<sup>7</sup> **Applies to:** [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)].

<sup>8</sup> `NONCLUSTERED HASH` indexes are supported only on memory-optimized tables. The `sys.hash_indexes` view shows the current hash indexes and the hash properties. For more information, see [sys.hash_indexes](sys-hash-indexes-transact-sql.md).

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata visibility configuration](../security/metadata-visibility-configuration.md).

## Examples

The following example returns all indexes for the table `Production.Product` in the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
SELECT i.name AS index_name,
       i.type_desc,
       is_unique,
       ds.type_desc AS filegroup_or_partition_scheme,
       ds.name AS filegroup_or_partition_scheme_name,
       ignore_dup_key,
       is_primary_key,
       is_unique_constraint,
       fill_factor,
       is_padded,
       is_disabled,
       allow_row_locks,
       allow_page_locks
FROM sys.indexes AS i
     INNER JOIN sys.data_spaces AS ds
         ON i.data_space_id = ds.data_space_id
WHERE is_hypothetical = 0
      AND i.index_id <> 0
      AND i.object_id = OBJECT_ID('Production.Product');
GO
```

## Related content

- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [sys.index_columns](sys-index-columns-transact-sql.md)
- [sys.xml_indexes](sys-xml-indexes-transact-sql.md)
- [sys.objects](sys-objects-transact-sql.md)
- [sys.key_constraints](sys-key-constraints-transact-sql.md)
- [sys.filegroups](sys-filegroups-transact-sql.md)
- [sys.partition_schemes](sys-partition-schemes-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
- [In-Memory OLTP overview and usage scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
