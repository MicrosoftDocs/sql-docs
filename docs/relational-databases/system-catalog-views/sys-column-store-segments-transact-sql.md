---
title: "sys.column_store_segments (Transact-SQL)"
description: "sys.column_store_segments returns one row for each column segment in a columnstore index. There is one column segment per column per rowgroup."
author: rwestMSFT
ms.author: randolphwest
ms.date: 1/12/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "column_store_segments"
  - "sys.column_store_segments_TSQL"
  - "sys.column_store_segments"
  - "column_store_segments_TSQL"
helpviewer_keywords:
  - "sys.column_store_segments catalog view"
dev_langs:
  - "TSQL"
---

# sys.column_store_segments (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns one row for each column segment in a columnstore index. There is one column segment per column per rowgroup. For example, a columnstore index with 10 rowgroups and 34 columns has 340 rows in this view.

| Column name | Data type | Description |
| --- | --- | --- |
| `partition_id` | **bigint** | Indicates the partition ID. Is unique within a database. |
| `hobt_id` | **bigint** | ID of the heap or B-tree index (HoBT) for the table that has this columnstore index. |
| `column_id` | **int** | ID of the columnstore column. |
| `segment_id` | **int** | ID of the rowgroup. For backward compatibility, the column name continues to be called segment_id even though this is the rowgroup ID. You can uniquely identify a segment using `<hobt_id, partition_id, column_id, segment_id>`. |
| `version` | **int** | Version of the column segment format. |
| `encoding_type` | **int** | Type of encoding used for that segment:<br /><br />1 = VALUE_BASED - non-string/binary with no dictionary (similar to 4 with some internal variations)<br /><br />2 = VALUE_HASH_BASED - non-string/binary column with common values in dictionary<br /><br />3 = STRING_HASH_BASED - string/binary column with common values in dictionary<br /><br />4 = STORE_BY_VALUE_BASED - non-string/binary with no dictionary<br /><br />5 = STRING_STORE_BY_VALUE_BASED - string/binary with no dictionary<br /><br />For more information, see the [Remarks](#remarks) section. |
| `row_count` | **int** | Number of rows in the row group. |
| `has_nulls` | **int** | 1 if the column segment has NULLs. |
| `base_id` | **bigint** | Base value ID if encoding type 1 is being used. If encoding type 1 is not being used, `base_id` is set to -1. |
| `magnitude` | **float** | Magnitude if encoding type 1 is being used. If encoding type 1 is not being used, `magnitude` is set to -1. |
| `primary_dictionary_id` | **int** | A value of 0 represents the global dictionary. A value of -1 indicates that there is no global dictionary created for this column. |
| `secondary_dictionary_id` | **int** | A non-zero value points to the local dictionary for this column in the current segment (for example, the rowgroup). A value of -1 indicates that there is no local dictionary for this segment. |
| `min_data_id` | **bigint** | For internal use only. |
| `max_data_id` | **bigint** | For internal use only. |
| `null_value` | **bigint** | Value used to represent nulls. |
| `on_disk_size` | **bigint** | Size of segment in bytes. |
| `collation_id` | **int** | Current collation when the segment was created. Maps to an internal ID. For internal use only.<br /><br />**Applies to:** [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)] |
| `min_deep_data` | **varbinary(18)** | Used for segment elimination.<sup>1</sup> For internal use only.<br /><br />**Applies to:** [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)] |
| `max_deep_data` | **varbinary(18)** | Used for segment elimination.<sup>1</sup> For internal use only.<br /><br />**Applies to:** [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)] |

<sup>1</sup> After upgrading to a version of the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] that supports min/max segment elimination for an expanded set of data types (introduced in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]), the `min_deep_data` and `max_deep_data` are `NULL` until the columnstore index is rebuilt using an `ALTER INDEX ... REBUILD` or `CREATE INDEX ... WITH (DROP_EXISTING = ON)` statement.

## Remarks

The columnstore segment encoding type is selected by the [!INCLUDE[ssDE-md](../../includes/ssde-md.md)] by analyzing the segment data with the goal of achieving the lowest storage cost. If data is mostly distinct, the [!INCLUDE[ssDE-md](../../includes/ssde-md.md)] uses value-based encoding. If data is mostly not distinct, the [!INCLUDE[ssDE-md](../../includes/ssde-md.md)] uses hash-based encoding. The choice between string-based and value-based encoding is related to the type of data being stored, whether string data or binary data. All encodings take advantage of bit-packing and run-length encoding when possible.

Columnstore segment elimination applies to numeric, date, and time data types, and the **datetimeoffset** data type with scale less than or equal to two. Beginning in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], segment elimination capabilities expand to string and binary data types, the **uniqueidentifier** data type, and the **datetimeoffset** data type for scale greater than two. Segment elimination does not apply to LOB data types such as **varchar(max)**, **nvarchar(max)**, and **varbinary(max)**. For more information, see [What's new in columnstore indexes](../indexes/columnstore-indexes-what-s-new.md#sql-server-2022-16x).

## Permissions

The `VIEW DEFINITION` permission on the view is required. The following columns return `NULL` unless the user also has the `SELECT` permission: `has_nulls`, `base_id`, `magnitude`, `min_data_id`, `max_data_id`, and `null_value`.

[!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).

## Examples

The following query returns information about segments of a columnstore index.

```sql
SELECT i.name, p.object_id, p.index_id, i.type_desc,
    COUNT(*) AS number_of_segments
FROM sys.column_store_segments AS s
INNER JOIN sys.partitions AS p
    ON s.hobt_id = p.hobt_id
INNER JOIN sys.indexes AS i
    ON p.object_id = i.object_id
WHERE i.type = 5 OR i.type = 6
GROUP BY i.name, p.object_id, p.index_id, i.type_desc;
```

## Related content

- [Columnstore indexes: overview](../indexes/columnstore-indexes-overview.md)
- [Performance tuning with ordered columnstore indexes](../indexes/ordered-columnstore-indexes.md)
- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
- [sys.columns (Transact-SQL)](sys-columns-transact-sql.md)
- [sys.all_columns (Transact-SQL)](sys-all-columns-transact-sql.md)
- [sys.computed_columns (Transact-SQL)](sys-computed-columns-transact-sql.md)
- [sys.column_store_dictionaries (Transact-SQL)](sys-column-store-dictionaries-transact-sql.md)
