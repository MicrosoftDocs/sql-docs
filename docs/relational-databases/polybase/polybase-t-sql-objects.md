---
title: "PolyBase Transact-SQL reference"
description: "Use PolyBase to query your external data in Hadoop, Azure Blob Storage, Azure Data Lake Store, SQL Server, Oracle, Teradata, MongoDB, or CSV files."
author: MikeRayMSFT
ms.author: mikeray
ms.date: 01/17/2024
ms.service: sql
ms.subservice: polybase
ms.topic: tutorial
helpviewer_keywords:
  - "PolyBase, fundamentals"
  - "PolyBase, SQL statements"
  - "PolyBase, SQL objects"
monikerRange: ">=sql-server-linux-ver15||>=sql-server-2016||>=aps-pdw-2016||=azure-sqldw-latest"
---
# PolyBase Transact-SQL reference

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article reviews options for using [PolyBase](polybase-guide.md) to query external data in-place, referred to as data virtualization, for a variety of external data sources. 

## T-SQL syntax used in PolyBase

To use PolyBase, you must create external tables to reference your external data. Refer to:
  
 - [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md) 
 - [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)  
 - [CREATE EXTERNAL FILE FORMAT (Transact-SQL)](../../t-sql/statements/create-external-file-format-transact-sql.md)  
 - [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)  
 - [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)  

> [!NOTE]
> In order to use PolyBase you must have sysadmin or CONTROL SERVER level permissions on the database.

## Tutorials

For examples of queries, see [PolyBase Queries](../../relational-databases/polybase/polybase-queries.md).  

For more tutorials on various external data sources, review:

- [Hadoop](polybase-configure-hadoop.md)
- [Azure Blob Storage](polybase-configure-azure-blob-storage.md)
- [SQL Server](polybase-configure-sql-server.md)
- [Oracle](polybase-configure-oracle.md)
- [Teradata](polybase-configure-teradata.md)
- [MongoDB](polybase-configure-mongodb.md)
- [ODBC generic types](polybase-configure-odbc-generic.md)
- [S3-compatible object storage](polybase-configure-s3-compatible.md)
- [CSV](virtualize-csv.md)
- [Delta table](virtualize-delta.md)

## File metadata functions

Sometimes, you might need to know which file or folder source correlates to a specific row in the result set.

You can use functions `filepath` and `filename` to return file names and/or the path in the result set. Or you can use them to filter data based on the file name and/or folder path. In the following sections, you'll find short descriptions along samples.

### Filename function

This function returns the file name that the row originates from.

Return data type is **nvarchar(1024)**. For optimal performance, always cast result of filename function to appropriate data type. If you use character data type, make sure appropriate length is used.

The following sample reads the NYC Yellow Taxi data files for the last three months of 2017 and returns the number of rides per file. The `OPENROWSET` part of the query specifies which files will be read.

```sql
SELECT
    nyc.filename() AS [filename]
    ,COUNT_BIG(*) AS [rows]
FROM  
    OPENROWSET(
        BULK 'parquet/taxi/year=2017/month=9/*.parquet',
        DATA_SOURCE = 'SqlOnDemandDemo',
        FORMAT='PARQUET'
    ) nyc
GROUP BY nyc.filename();
```

The following example shows how `filename()` can be used in the `WHERE` clause to filter the files to be read. It accesses the entire folder in the `OPENROWSET` part of the query and filters files in the `WHERE` clause.

Your results will be the same as the prior example.

```sql
SELECT
    r.filename() AS [filename]
    ,COUNT_BIG(*) AS [rows]
FROM OPENROWSET(
    BULK 'csv/taxi/yellow_tripdata_2017-*.csv',
        DATA_SOURCE = 'SqlOnDemandDemo',
        FORMAT = 'CSV',
        PARSER_VERSION = '2.0',
        FIRSTROW = 2) 
        WITH (C1 varchar(200) ) AS [r]
WHERE
    r.filename() IN ('yellow_tripdata_2017-10.csv', 'yellow_tripdata_2017-11.csv', 'yellow_tripdata_2017-12.csv')
GROUP BY
    r.filename()
ORDER BY
    [filename];
```

### Filepath function

This function returns a full path or a part of path:

- When called without parameter, returns the full file path that a row originates from.
- When called with parameter, returns part of path that matches the wildcard on position specified in the parameter. For example, parameter value 1 would return part of path that matches the first wildcard.

Return data type is **nvarchar(1024)**. For optimal performance, always cast result of `filepath` function to appropriate data type. If you use character data type, make sure appropriate length is used.

The following sample reads NYC Yellow Taxi data files for the last three months of 2017. It returns the number of rides per file path. The `OPENROWSET` part of the query specifies which files will be read.

```sql
SELECT
    r.filepath() AS filepath
    ,COUNT_BIG(*) AS [rows]
FROM OPENROWSET(
        BULK 'csv/taxi/yellow_tripdata_2017-1*.csv',
        DATA_SOURCE = 'SqlOnDemandDemo',
        FORMAT = 'CSV',
        PARSER_VERSION = '2.0',
        FIRSTROW = 2
    )
    WITH (
        vendor_id INT
    ) AS [r]
GROUP BY
    r.filepath()
ORDER BY
    filepath;
```

The following example shows how `filepath()` can be used in the `WHERE` clause to filter the files to be read.

You can use the wildcards in the `OPENROWSET` part of the query and filter the files in the `WHERE` clause. Your results will be the same as the prior example.

```sql
SELECT
    r.filepath() AS filepath
    ,r.filepath(1) AS [year]
    ,r.filepath(2) AS [month]
    ,COUNT_BIG(*) AS [rows]
FROM OPENROWSET(
        BULK 'csv/taxi/yellow_tripdata_*-*.csv',
        DATA_SOURCE = 'SqlOnDemandDemo',
        FORMAT = 'CSV',
        PARSER_VERSION = '2.0',        
        FIRSTROW = 2
    )
WITH (
    vendor_id INT
) AS [r]
WHERE
    r.filepath(1) IN ('2017')
    AND r.filepath(2) IN ('10', '11', '12')
GROUP BY
    r.filepath()
    ,r.filepath(1)
    ,r.filepath(2)
ORDER BY
    filepath;
```

## Related content

- [Data virtualization with PolyBase in SQL Server](polybase-guide.md)
- [Get started with PolyBase in SQL Server 2022](polybase-get-started.md)
- [Performance considerations in PolyBase for SQL Server](polybase-performance.md)
