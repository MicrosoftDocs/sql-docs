---
title: "sys.partitions (Transact-SQL)"
description: sys.partitions contains a row for each partition of all the tables and most types of indexes in the database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/25/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "partitions"
  - "partitions_TSQL"
  - "sys.partitions_TSQL"
  - "sys.partitions"
helpviewer_keywords:
  - "sys.partitions catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sys.partitions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Contains a row for each partition of all the tables and most types of indexes in the database. Special index types such as Full-Text, Spatial, and XML aren't included in this view. All tables and indexes in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] contain at least one partition, whether or not they are explicitly partitioned.

| Column name | Data type | Description |
| --- | --- | --- |
| `partition_id` | **bigint** | Indicates the partition ID. Unique within a database. |
| `object_id` | **int** | Indicates the ID of the object to which this partition belongs. Every table or view is composed of at least one partition. |
| `index_id` | **int** | Indicates the ID of the index within the object to which this partition belongs.<br /><br />0 = heap<br />1 = clustered index<br />2 or greater = nonclustered index |
| `partition_number` | **int** | A 1-based partition number within the owning index or heap. For non-partitioned tables and indexes, the value of this column is `1`. |
| `hobt_id` | **bigint** | Indicates the ID of the data heap or B-tree (HoBT) that contains the rows for this partition. |
| `rows` | **bigint** | Indicates the approximate number of rows in this partition. |
| `filestream_filegroup_id` | **smallint** | **Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions.<br /><br />Indicates the ID of the FILESTREAM filegroup stored on this partition. |
| `data_compression` | **tinyint** | Indicates the state of compression for each partition:<br /><br />0 = NONE<br />1 = ROW<br />2 = PAGE<br />3 = COLUMNSTORE <sup>1</sup><br />4 = COLUMNSTORE_ARCHIVE <sup>2</sup><br /><br />**Note:** Full text indexes are compressed in any edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `data_compression_desc` | **nvarchar(60)** | Indicates the state of compression for each partition. Possible values for rowstore tables are `NONE`, `ROW`, and `PAGE`. Possible values for columnstore tables are `COLUMNSTORE` and `COLUMNSTORE_ARCHIVE`. |
| `xml_compression` | **bit** | **Applies to**: [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)] and later versions.<br /><br />Indicates the state of XML compression for each partition:<br /><br />0 = OFF<br />1 = ON |
| `xml_compression_desc` | **varchar(3)** | **Applies to**: [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)] and later versions.<br /><br />Indicates the state of XML compression for each partition. Possible values are `OFF` and `ON`. |

<sup>1</sup> **Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions.

<sup>2</sup> **Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions.

## Permissions

Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Examples

### Determine space used by object and show related partition information

The following query returns all the object in a database, the amount of space used in each object, and partition information related to each object.

```sql
SELECT object_name(object_id) AS ObjectName,
    total_pages / 128. AS SpaceUsed_MB,
    p.partition_id,
    p.object_id,
    p.index_id,
    p.partition_number,
    p.rows,
    p.data_compression_desc
FROM sys.partitions AS p
INNER JOIN sys.allocation_units AS au
    ON p.partition_id = au.container_id
ORDER BY SpaceUsed_MB DESC;
```

## Related content

- [Object Catalog Views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
