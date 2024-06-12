---
title: "sp_estimate_data_compression_savings (Transact-SQL)"
description: The sp_estimate_data_compression_savings system stored procedure returns the current size of the requested object and estimates the object size for the requested compression state.
author: markingmyname
ms.author: maghan
ms.reviewer: dimitri-furman, wiassaf, randolphwest
ms.date: 06/10/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_estimate_data_compression_savings_TSQL"
  - "sp_estimate_data_compression_savings"
helpviewer_keywords:
  - "compression [SQL Server], estimating"
  - "sp_estimate_data_compression_savings"
dev_langs:
  - "TSQL"
---
# sp_estimate_data_compression_savings (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the current size of the requested object and estimates the object size for the requested compression state. Compression can be evaluated for whole tables or parts of tables. This includes heaps, clustered indexes, nonclustered indexes, columnstore indexes, indexed views, and table and index partitions. The objects can be compressed by using row, page, columnstore, or columnstore archive compression. If the table, index, or partition is already compressed, you can use this procedure to estimate the size of the table, index, or partition if it's recompressed or stored without compression.

The `sys.sp_estimate_data_compression_savings` system stored procedure is available in Azure SQL Database and Azure SQL Managed Instance.

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you can compress off-row XML data in columns using the **xml** data type, reducing storage and memory requirements. For more information, see [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) and [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md). `sp_estimate_data_compression_savings` supports XML compression estimates.

> [!NOTE]  
> Compression and `sp_estimate_data_compression_savings` aren't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

To estimate the size of the object if it were to use the requested compression setting, this stored procedure samples the source object and loads this data into an equivalent table and index created in `tempdb`. The table or index created in `tempdb` is then compressed to the requested setting and the estimated compression savings is computed.

To change the compression state of a table, index, or partition, use the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) statements. For general information about compression, see [Data Compression](../data-compression/data-compression.md).

> [!NOTE]  
> If the existing data is fragmented, you might be able to reduce its size without using compression by rebuilding the index. For indexes, the fill factor will be applied during an index rebuild. This could increase the size of the index.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_estimate_data_compression_savings
    [ @schema_name = ] N'schema_name'
    , [ @object_name = ] N'object_name'
    , [ @index_id = ] index_id
    , [ @partition_number = ] partition_number
    , [ @data_compression = ] N'data_compression'
    [ , [ @xml_compression = ] xml_compression ]
[ ; ]
```

## Arguments

#### [ @schema_name = ] N'*schema_name*'

The name of the database schema that contains the table or indexed view. *@schema_name* is **sysname**, with no default. If *@schema_name* is `NULL`, the default schema of the current user is used.

#### [ @object_name = ] N'*object_name*'

The name of the table or indexed view that the index is on. *@object_name* is **sysname**, with no default.

#### [ @index_id = ] *index_id*

The ID of the index. *@index_id* is **int**, and can be one of the following values:

- the ID number of an index
- `NULL`
- `0` if *object_id* is a heap

To return information for all indexes for a base table or view, specify `NULL`. If you specify `NULL`, you must also specify `NULL` for *@partition_number*.

#### [ @partition_number = ] *partition_number*

The partition number in the object. *@partition_number* is **int**, and can be one of the following values:

- the partition number of an index or heap
- `NULL`
- `1` for a nonpartitioned index or heap

To specify the partition, you can also specify the [$PARTITION](../../t-sql/functions/partition-transact-sql.md) function. To return information for all partitions of the owning object, specify `NULL`.

#### [ @data_compression = ] N'*data_compression*'

Specifies the type of compression to be evaluated. *@data_compression* is **nvarchar(60)**, and can be one of the following values:

- `NONE`
- `ROW`
- `PAGE`
- `COLUMNSTORE`
- `COLUMNSTORE_ARCHIVE`

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, `NULL` is also a possible value. *@data_compression* can't be `NULL` if *@xml_compression* is `NULL`.

#### [ @xml_compression = ] *xml_compression*

**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Specifies whether to calculate savings for XML compression. *@xml_compression* is **bit**, and can be one of the following values:

- `NULL` (default)
- `0`
- `1`

*@xml_compression* can't be `NULL` if *@data_compression* is `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

The following result set is returned to provide current and estimated size for the table, index, or partition.

| Column name | Data type | Description |
| --- | --- | --- |
| `object_name` | **sysname** | Name of the table or the indexed view. |
| `schema_name` | **sysname** | Schema of the table or indexed view. |
| `index_id` | **int** | Index ID of an index:<br /><br />`0` = Heap<br />`1` = Clustered index<br />`>1` = Nonclustered index |
| `partition_number` | **int** | Partition number. Returns `1` for a nonpartitioned table or index. |
| `size_with_current_compression_setting (KB)` | **bigint** | Size of the requested table, index, or partition as it currently exists. |
| `size_with_requested_compression_setting (KB)` | **bigint** | Estimated size of the table, index, or partition that uses the requested compression setting; and, if applicable, the existing fill factor, and assuming there's no fragmentation. |
| `sample_size_with_current_compression_setting (KB)` | **bigint** | Size of the sample with the current compression setting. This size includes any fragmentation. |
| `sample_size_with_requested_compression_setting (KB)` | **bigint** | Size of the sample that is created by using the requested compression setting; and, if applicable, the existing fill factor and no fragmentation. |

## Remarks

Use `sp_estimate_data_compression_savings` to estimate the savings that can occur when you enable a table or partition for row, page, columnstore, columnstore archive, or XML compression. For instance, if the average size of the row can be reduced by 40 percent, you can potentially reduce the size of the object by 40 percent. You might not receive a space savings because this depends on the fill factor and the size of the row. For example, if you have a row that is 8,000 bytes long and you reduce its size by 40 percent, you can still fit only one row on a data page. There are no savings.

If the results of running `sp_estimate_data_compression_savings` on an uncompressed table or index indicate that the size will increase, this means that many rows use almost the whole precision of the data types, and the addition of the small overhead needed for the compressed format is more than the savings from compression. In this rare case, don't enable compression.

If a table is already enabled for compression, you can use `sp_estimate_data_compression_savings` to estimate the average size of the row if the table is uncompressed.

An intent shared (IS) lock is acquired on the table during this operation. If an IS lock can't be obtained, the procedure is blocked. The table is scanned under the default read committed isolation level.

If the requested compression setting is same as the current compression setting, the stored procedure returns the estimated size with no data fragmentation, using the existing fill factor for indexes on the source object.

If the index or partition ID doesn't exist, no results are returned.

## Permissions

Requires `SELECT` permission on the table, `VIEW DATABASE STATE` and `VIEW DEFINITION` on the database containing the table and on `tempdb`.

## Limitations

In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and earlier versions, this procedure didn't apply to columnstore indexes, and therefore didn't accept the data compression parameters `COLUMNSTORE` and `COLUMNSTORE_ARCHIVE`. In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, and in Azure SQL Database and Azure SQL Managed Instance, columnstore indexes can be used both as a source object for estimation, and as a requested compression type.

When [Memory-Optimized TempDB Metadata](../databases/tempdb-database.md#memory-optimized-tempdb-metadata) is enabled, creation of columnstore indexes on temporary tables isn't supported. Because of this limitation, `sp_estimate_data_compression_savings` isn't supported with the `COLUMNSTORE` and `COLUMNSTORE_ARCHIVE` data compression parameters when Memory-Optimized TempDB Metadata is enabled.

## Considerations for columnstore indexes

Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)], and in Azure SQL Database and Azure SQL Managed Instance, `sp_estimate_compression_savings` supports estimating both columnstore and columnstore archive compression. Unlike page and row compression, applying columnstore compression to an object requires creating a new columnstore index. For this reason, when using the `COLUMNSTORE` and `COLUMNSTORE_ARCHIVE` options of this procedure, the type of the source object provided to the procedure determines the type of columnstore index used for the compressed size estimate. The following table illustrates the reference objects used to estimate compression savings for each source object type when the *@data_compression* parameter is set to either `COLUMNSTORE` or `COLUMNSTORE_ARCHIVE`.

| Source object | Reference object |
| --- | --- |
| **Heap | Clustered columnstore index |
| **Clustered index** | Clustered columnstore index |
| **Nonclustered index** | Nonclustered columnstore index (including the key columns and any included columns of the provided nonclustered index, and the partition column of the table, if any) |
| **Nonclustered columnstore index** | Nonclustered columnstore index (including the same columns as the provided nonclustered columnstore index) |
| **Clustered columnstore index** | Clustered columnstore index |

> [!NOTE]  
> When estimating columnstore compression from a rowstore source object (clustered index, nonclustered index or heap), if there are any columns in the source object that have a data type that isn't supported in a columnstore index, `sp_estimate_compression_savings` will fail with an error.

Similarly, when the *@data_compression* parameter is set to `NONE`, `ROW`, or `PAGE` and the source object is a columnstore index, the following table outlines the reference objects used.

| Source object | Reference object |
| --- | --- |
| **Clustered columnstore index** | Heap |
| **Nonclustered columnstore index** | Nonclustered index (including the columns contained in the nonclustered columnstore index as key columns, and the partition column of the table, if any, as an included column) |

> [!NOTE]  
> When estimating rowstore compression (NONE, ROW or PAGE) from a columnstore source object, be sure that the source index doesn't contain more than 32 key columns as this is the limit supported in a rowstore (nonclustered) index.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Estimate savings with ROW compression

The following example estimates the size of the `Production.WorkOrderRouting` table if it's compressed by using `ROW` compression.

```sql
EXEC sys.sp_estimate_data_compression_savings
     'Production', 'WorkOrderRouting', NULL, NULL, 'ROW';
GO
```

### B. Estimate savings with PAGE and XML compression

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions

The following example estimates the size of the `Production.ProductModel` table if it's compressed by using `PAGE` compression, and the *@xml_compression* value is enabled.

```sql
EXEC sys.sp_estimate_data_compression_savings
     'Production', 'ProductModel', NULL, NULL, 'PAGE', 1;
GO
```

## Related content

- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [sys.partitions (Transact-SQL)](../system-catalog-views/sys-partitions-transact-sql.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [Unicode Compression Implementation](../data-compression/unicode-compression-implementation.md)
