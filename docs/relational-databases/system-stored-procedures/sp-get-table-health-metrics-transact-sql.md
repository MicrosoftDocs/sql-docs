---
title: "sys.sp_get_table_health_metrics (Transact-SQL)"
description: The sys.sp_get_table_health_metrics system stored procedure returns file-level health metrics and anomaly detection for a Lakehouse table accessed through the SQL analytics endpoint in Microsoft Fabric.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: procha
ms.date: 06/15/2026
ms.service: fabric
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.sp_get_table_health_metrics_TSQL"
  - "sys.sp_get_table_health_metrics"
  - "sp_get_table_health_metrics_TSQL"
  - "sp_get_table_health_metrics"
helpviewer_keywords:
  - "sp_get_table_health_metrics"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---
# sys.sp_get_table_health_metrics (Transact-SQL)

[!INCLUDE [fabric-se](../../includes/applies-to-version/fabric-se.md)]

The `sys.sp_get_table_health_metrics` system stored procedure returns file-level storage health metrics for a Lakehouse table. The result set includes histogram distributions for file sizes, row counts, and deleted row counts, along with anomaly detection that identifies common storage conditions that degrade query performance.

This system stored procedure is available on the SQL analytics endpoint for Lakehouse tables in Microsoft Fabric.

## Syntax

```syntaxsql
sp_get_table_health_metrics [ @table_name = ] 'table_name'
[ ; ]
```

## Arguments

#### [ @table_name = ] '*table_name*'

The fully qualified name of the Lakehouse table to analyze. Use the format `schema.table_name`.

`@table_name` is **nvarchar(256)**, with no default. This parameter is required. The schema name is not required if the table is in the `dbo` schema.

## Return code values

`0` (success) or a nonzero number (failure).

## Result set

The `sp_get_table_health_metrics` result set is a single row.

| Column name | Category | Data type | Description |
|---|---|---|---|
| `PotentialAnomalyType` | Anomaly detection | **int** | A numeric code representing the detected anomaly category. See [Anomaly type codes](#potential-anomaly-type-codes) for possible values. |
| `PotentialAnomalyDescription` | Anomaly detection | **nvarchar(256)** | A human-readable description of the detected anomaly, or `None` if no anomaly is detected. |
| `SnapshotVersion` | Table version | **int** | The current table snapshot. |
| `CheckpointVersion` | Checkpoint version | **int** | Last checkpoint version. NULL if no checkpoint exists. |
| `PhysicalRowCount` | Summary | **int** | Total row count across all files. Includes rows that were deleted but are still stored in table files. |
| `DeletedRowCount` | Summary  |  **int** | Total number of logically deleted rows. |
| `FileCount` | Summary | **int** | The total number of data files (Parquet files) that comprise the table. |
| `DeletedBitmapCount` | Summary | **int** | Total number of delete bitmaps (that is, files with any deleted rows). |
| `FileSizeInBytes` | Summary | **int** | Total size of all data files in bytes. |
| `FileRowCount[0]` | Histogram | **int** | Files with no data rows. |
| `FileRowCount[1,10)` | Histogram | **int** | Files with 1 to 9 rows. |
| `FileRowCount[10,100)` | Histogram | **int** | Files with 10 to 99 rows. |
| `FileRowCount[100,1k)` | Histogram | **int** | Files with 100 to 999 rows. |
| `FileRowCount[1k,10k)` | Histogram | **int** | Files with 1,000 to 9,999 rows. |
| `FileRowCount[10k,100k)` | Histogram | **int** | Files with 10,000 to 99,999 rows. |
| `FileRowCount[100k,1M)` | Histogram | **int** | Files with 100,000 to 999,999 rows. |
| `FileRowCount[1M,10M)` | Histogram | **int** | Files with 1,000,000 to 9,999,999 rows. |
| `FileRowCount[10M+)` | Histogram | **int** | Files with more than 10,000,000 rows. |
| `FileDeletedRowCount[0]` | Histogram | **int** | Files with no deleted rows. |
| `FileDeletedRowCount[1,10)` | Histogram | **int** | Files with 1 to 9 deleted rows. |
| `FileDeletedRowCount[10,100)` | Histogram | **int** | Files with 10 to 99 deleted rows. |
| `FileDeletedRowCount[100,1k)` | Histogram | **int** | Files with 100 to 999 deleted rows. |
| `FileDeletedRowCount[1k,10k)` | Histogram | **int** | Files with 1,000 to 9,999 deleted rows. |
| `FileDeletedRowCount[10k,100k)` | Histogram | **int** | Files with 10,000 to 99,999 deleted rows. |
| `FileDeletedRowCount[100k,1M)` | Histogram | **int** | Files with 100,000 to 999,999 deleted rows. |
| `FileDeletedRowCount[1M,10M)` | Histogram | **int** | Files with 1,000,000 to 9,999,999 deleted rows. |
| `FileDeletedRowCount[10M+)` | Histogram | **int** | Files with more than 10,000,000 deleted rows. |
| `FileSize[0]` | Histogram | **int** | Files with 0 bytes. |
| `FileSize[1,1KiB)` | Histogram | **int** | Files from 1 byte to less than 1 KiB. |
| `FileSize[1KiB,16KiB)` | Histogram | **int** | Files from 1 KiB to less than 16 KiB. |
| `FileSize[16KiB,256KiB)` | Histogram | **int** | Files from 16 KiB to less than 256 KiB. |
| `FileSize[256KiB,4MiB)` | Histogram | **int** | Files from 256 KiB to less than 4 MiB. |
| `FileSize[4MiB,64MiB)` | Histogram | **int** | Files from 4 MiB to less than 64 MiB. |
| `FileSize[64MiB,1GiB)` | Histogram | **int** | Files from 64 MiB to less than 1 GiB. |
| `FileSize[1GiB,16GiB)` | Histogram | **int** | Files from 1 GiB to less than 16 GiB. |
| `FileSize[16GiB+)` | Histogram | **int** | Files larger than 16 GiB. |

## Potential anomaly type codes

The `PotentialAnomalyType` column returns one of the following integer codes:

| Code | PotentialAnomalyDescription | Condition | Recommended action |
|---|---|---|---|
| `0` | None | No anomaly detected. The table's file layout is within acceptable parameters for the SQL analytics endpoint engine. | No action required. |
| `1` | Invalid file statistics | File metadata is inconsistent or corrupted. The procedure can't reliably assess table health. | Investigate the table's Delta log. Re-run ingestion or run `OPTIMIZE` to regenerate file metadata. |
| `2` | Many deleted rows | A significant proportion of rows across files is marked as deleted but not physically removed. | Run `OPTIMIZE` on the table from a Spark notebook or Lakehouse context to rewrite files without deleted rows. |
| `3` | Many small files | A large proportion of files is below the optimal size threshold for the SQL engine. | Run `OPTIMIZE` on the table to compact small files into larger ones. Consider adjusting upstream ingestion batch sizes. |
| `4` | No recent checkpoint | The Delta table's checkpoint is stale relative to the transaction log. | Run `OPTIMIZE` or manually trigger a checkpoint from Spark. |

> [!NOTE]
> The procedure reports only one anomaly type per execution. If multiple anomalies exist, the procedure returns the one with the highest severity. Run maintenance and execute the procedure again to check for additional issues.

## Interpret the results

If the `PotentialAnomalyType` column has a value other than zero, the procedure detected an anomaly in the table. The stored procedure analyzes the target table and provides a diagnostic based on parameters that are optimal for the Fabric Data Warehouse engine. The `PotentialAnomalyDescription` column provides a description of the anomaly that was detected, but you might need a broader interpretation of the results for better context.  

Use the histogram columns to understand whether the table's physical file layout is close to the expected shape for Data Warehouse query performance. A healthy table usually has most files in the `FileRowCount[1M,10M)` range, with the average row count close to 2 million rows per file. If most files are in lower row-count bins, such as `FileRowCount[1k,10k)`, `FileRowCount[10k,100k)`, or `FileRowCount[100k,1M)`, the table might have many undersized files. This condition can increase query planning and file-open overhead because the SQL engine must read from many small files instead of fewer, larger files.

For file size, a healthy table should have most files near the target average of 1.2 GB per file. In the histogram, files should concentrate in `FileSize[1GiB,16GiB)`, with some files in `FileSize[64MiB,1GiB)` depending on ingestion patterns and table size. If many files appear in smaller bins, especially `FileSize[1KiB,16KiB)`, `FileSize[16KiB,256KiB)`, `FileSize[256KiB,4MiB)`, or `FileSize[4MiB,64MiB)`, the table likely has a small-file problem and can benefit from compaction. 

Files that are too large can also reduce performance and operational flexibility. If many files appear in `FileSize[16GiB+)`, or if the average file size is higher than the 1.2 GB warehouse target, the table might have overly large files. Large files can reduce parallelism because fewer files are available for distributed scan work, and they can make maintenance operations such as rewrites or compaction more expensive. The best layout is usually a balanced distribution around the target file size rather than many small files or a small number of large files.

Use the summary columns to calculate table-level averages:

- Average rows per file = `PhysicalRowCount` / `FileCount`
- Average file size = `FileSizeInBytes` / `FileCount`

Compare those averages to the Data Warehouse targets. An average row count lower than 2 million rows per file, or an average file size lower than 1.2 GB, indicates fragmentation or ingestion patterns that produce files too small for efficient SQL query processing. Running `OPTIMIZE` can compact smaller files into larger files and improve scan efficiency.

Deleted row metrics indicate whether maintenance is needed to remove logically deleted rows. A healthy table should have most files in `FileDeletedRowCount[0]`, and `DeletedRowCount` should be low relative to `PhysicalRowCount`. If deleted rows are concentrated in higher bins, such as `FileDeletedRowCount[100k,1M)`, `FileDeletedRowCount[1M,10M)`, or `FileDeletedRowCount[10M+)`, the SQL engine must skip many deleted rows at read time, which can reduce query performance. Running `OPTIMIZE` rewrites affected files and physically removes deleted rows.

> [!NOTE]
> Deleted rows and older file versions can be required for Delta Lake time travel. Running `OPTIMIZE` can rewrite files to improve the current table layout, but historical versions remain available until they exceed the configured retention period and are removed by `VACUUM`. Before running `VACUUM`, confirm that the retention period still supports your required time travel, rollback, and audit scenarios. Removing old files can permanently limit the table versions available for time travel.

## Permissions

The caller must have at least **VIEW DEFINITION** permission on the target table through the SQL analytics endpoint.

## Remarks

- The SQL analytics endpoint is read-only. You can't run `OPTIMIZE` directly from the SQL analytics endpoint. Use a Spark notebook, Lakehouse context, or a Fabric data pipeline to execute maintenance operations. For a tutorial, see [Tutorial: Optimize Lakehouse tables based on health checks](/fabric/data-warehouse/tutorial-conditional-lakehouse-optimization).
- The procedure inspects file-level metadata only. It doesn't perform rowgroup-level analysis.
- For tables with no data files (empty tables), the procedure returns a result set with all histogram values set to `0` and `PotentialAnomalyType` set to `0`.

## Examples

### A. Check health of a single table

```sql
EXEC sys.sp_get_table_health_metrics @table_name = 'dbo.WebClickstreamEvents';
```

### B. Use positional parameter syntax

```sql
EXEC sys.sp_get_table_health_metrics 'sales.SalesOrderFacts';
```

## Next step

> [!div class="nextstepaction"]
> [Tutorial: Optimize Lakehouse tables based on health checks](/fabric/data-warehouse/tutorial-conditional-lakehouse-optimization)

## Related content

- [Delta Lake OPTIMIZE](/fabric/data-engineering/delta-optimization-and-v-order)
- [What is the SQL analytics endpoint for a Lakehouse?](/fabric/data-engineering/lakehouse-sql-analytics-endpoint)
