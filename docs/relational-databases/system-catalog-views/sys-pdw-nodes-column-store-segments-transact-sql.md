---
title: "sys.pdw_nodes_column_store_segments (Transact-SQL)"
description: sys.pdw_nodes_column_store_segments (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/28/2018"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
ms.custom: seo-dt-2019
dev_langs:
  - "TSQL"
ms.assetid: e2fdf8e9-1b74-4682-b2d4-c62aca053d7f
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.pdw_nodes_column_store_segments (Transact-SQL)

[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

Contains a row for each column in a columnstore index.

| Column name                 | Data type  | Description                                                  |
| :-------------------------- | :--------- | :----------------------------------------------------------- |
| **partition_id**            | **bigint** | Indicates the partition ID. Is unique within a database.     |
| **hobt_id**                 | **bigint** | ID of the heap or B-tree index (hobt) for the table that has this columnstore index. |
| **column_id**               | **int**    | ID of the columnstore column.                                |
| **segment_id**              | **int**    | ID of the column segment. For backward compatibility, the column name continues to be called segment_id even though this is the rowgroup ID. You can uniquely identify a segment using <hobt_id, partition_id, column_id>, <segment_id>. |
| **version**                 | **int**    | Version of the column segment format.                        |
| **encoding_type**           | **int**    | Type of encoding used for that segment:<br /><br /> 1 = VALUE_BASED     -  non-string/binary with no dictionary (similar to 4 with some internal variations)<br /><br /> 2 = VALUE_HASH_BASED   - non-string/binary column with common values in dictionary<br /><br /> 3 = STRING_HASH_BASED  - string/binary column with common values in dictionary<br /><br /> 4 = STORE_BY_VALUE_BASED - non-string/binary with no dictionary<br /><br /> 5 = STRING_STORE_BY_VALUE_BASED - string/binary with no dictionary<br /><br /> All encodings take advantage of bit-packing and run-length encoding when possible. |
| **row_count**               | **int**    | Number of rows in the row group.                             |
| **has_nulls**               | **int**    | 1 if the column segment has null values.                     |
| **base_id**                 | **bigint** | Base value ID if encoding type 1 is being used.  If encoding type 1 is not being used, base_id is set to 1. |
| **magnitude**               | **float**  | Magnitude if encoding type 1 is being used.  If encoding type 1 is not being used, magnitude is set to 1. |
| **primary__dictionary_id**  | **int**    | ID of primary dictionary. A non-zero value points to the local dictionary for this column in the current segment (i.e. the rowgroup). A value of -1 indicates that there is no local dictionary for this segment. |
| **secondary_dictionary_id** | **int**    | ID of secondary dictionary. A non-zero value points to the local dictionary for this column in the current segment (i.e. the rowgroup). A value of -1 indicates that there is no local dictionary for this segment. |
| **min_data_id**             | **bigint** | Minimum data ID in the column segment.                       |
| **max_data_id**             | **bigint** | Maximum data ID in the column segment.                       |
| **null_value**              | **bigint** | Value used to represent nulls.                               |
| **on_disk_size**            | **bigint** | Size of segment in bytes.                                    |
| **pdw_node_id**             | **int**    | Unique identifier of a [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] node. |

## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

Join sys.pdw_nodes_column_store_segments with other system tables to determine the number of columnstore segments per logical table.

```sql
SELECT  sm.name           as schema_nm
,       tb.name           as table_nm
,       nc.name           as col_nm
,       nc.column_id
,       COUNT(*)          as segment_count
FROM    sys.[schemas] sm
JOIN    sys.[tables] tb                   ON  sm.[schema_id]          = tb.[schema_id]
JOIN    sys.[pdw_table_mappings] mp       ON  tb.[object_id]          = mp.[object_id]
JOIN    sys.[pdw_nodes_tables] nt         ON  nt.[name]               = mp.[physical_name]
JOIN    sys.[pdw_nodes_partitions] np     ON  np.[object_id]          = nt.[object_id]
                                          AND np.[pdw_node_id]        = nt.[pdw_node_id]
                                          AND np.[distribution_id]    = nt.[distribution_id]
JOIN    sys.[pdw_nodes_columns] nc        ON  np.[object_id]          = nc.[object_id]
                                          AND np.[pdw_node_id]        = nc.[pdw_node_id]
                                          AND np.[distribution_id]    = nc.[distribution_id]
JOIN    sys.[pdw_nodes_column_store_segments] rg  ON  rg.[partition_id]         = np.[partition_id]
                                                      AND rg.[pdw_node_id]      = np.[pdw_node_id]
                                                      AND rg.[distribution_id]  = np.[distribution_id]
                                                      AND rg.[column_id]        = nc.[column_id]
GROUP BY    sm.name
,           tb.name
,           nc.name
,           nc.column_id  
ORDER BY    table_nm
,           nc.column_id
,           sm.name ;
```

## Permissions

Requires **VIEW SERVER STATE** permission.

## See Also

[Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
[CREATE COLUMNSTORE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-columnstore-index-transact-sql.md)  
[sys.pdw_nodes_column_store_row_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-nodes-column-store-row-groups-transact-sql.md)  
[sys.pdw_nodes_column_store_dictionaries &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-nodes-column-store-dictionaries-transact-sql.md)
