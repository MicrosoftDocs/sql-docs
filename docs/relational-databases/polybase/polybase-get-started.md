---
title: Get started with PolyBase in SQL Server 2022
description: A tutorial for getting started with PolyBase in SQL Server 2022.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 01/17/2024
ms.service: sql
ms.subservice: polybase
ms.custom: linux-related-content
ms.topic: tutorial
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---

# Get started with PolyBase in SQL Server 2022

[!INCLUDE [sqlserver2016-windows-2017-linux-and-later](../../includes/applies-to-version/sqlserver2016-windows-2017-linux-and-later.md)]

This article guides you through a tutorial of working with multiple folders and files with PolyBase in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. This set of tutorial queries demonstrates various features of PolyBase.

[Data virtualization with PolyBase in SQL Server](polybase-guide.md) allows you to take advantage of metadata file functions to query multiple folders, files or perform folder elimination. The combination of schema discovery with folder and file elimination is a powerful capability that allows SQL to fetch just the required data from any Azure Storage Account or S3-compatible object storage solution.

## Prerequisites

Before using PolyBase in this tutorial, you must:

1. [Install PolyBase on Windows](polybase-installation.md) or [install PolyBase on Linux](polybase-linux-setup.md).
1. [Enable PolyBase in sp_configure](polybase-installation.md#enable) if necessary.
1. Allow external network access to access publicly available Azure Blob storage at `pandemicdatalake.blob.core.windows.net` and `azureopendatastorage.blob.core.windows.net`.

## Sample data sets

If you're new to data virtualization and want to quickly test functionality, start by querying public data sets available in Azure Open Datasets, like the Bing COVID-19 dataset allowing anonymous access.

Use the following endpoints to query the Bing COVID-19 data sets: 

- Parquet: `abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.parquet`
- CSV: `abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv`

For a quick start, run this simple T-SQL query to get first insights into the data set. This query uses [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md) to query a file stored in a publicly available storage account: 

```sql
SELECT TOP 10 * 
FROM OPENROWSET( 
 BULK 'abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.parquet', 
 FORMAT = 'parquet' 
) AS filerows;
```

You can continue data set exploration by appending `WHERE`, `GROUP BY` and other T-SQL clauses based on the result set of the first query.

If the first query fails on your SQL Server instance, network access is likely prevented to the public Azure storage account. Talk to your networking expert to enable access before you can proceed with querying.

Once you get familiar with querying public data sets, consider switching to nonpublic data sets that require providing credentials, granting access rights and configuring firewall rules. In many real-world scenarios you will operate primarily with private data sets.

## External data source

An external data source is an abstraction that enables easy referencing of a file location across multiple queries. To query public locations, all you need to specify while creating an external data source is the file location: 

```sql
CREATE EXTERNAL DATA SOURCE MyExternalDataSource 
WITH ( 
    LOCATION = 'abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest' 
);
```

> [!NOTE]
> If you receive an error message 46530, `External data sources are not supported with type GENERIC,` check the configuration option `PolyBase Enabled` in your SQL Server instance. It should be `1`.
>
> Run the following to enable PolyBase in your SQL Server instance:
>
> ```sql
> EXEC sp_configure @configname = 'polybase enabled', @configvalue = 1;
> RECONFIGURE;
> ```

When accessing nonpublic storage accounts, along with the location, you also need to reference a database scoped credential with encapsulated authentication parameters. The following script creates an external data source pointing to the file path, and referencing a database-scoped credential.

```sql
--Create external data source pointing to the file path, and referencing database-scoped credential: 
CREATE EXTERNAL DATA SOURCE MyPrivateExternalDataSource 
WITH ( 
    LOCATION = 'abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest' 
        CREDENTIAL = [MyCredential]);
```

## Query data sources using OPENROWSET

The [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md) syntax enables instant ad hoc querying while only creating the minimal number of database objects necessary.

`OPENROWSET` only requires creating the external data source (and possibly the credential) as opposed to the external table approach, which requires an [external file format](/sql/t-sql/statements/create-external-file-format-transact-sql?view=azuresqldb-mi-current&preserve-view=true) and the [external table](/sql/t-sql/statements/create-external-table-transact-sql?view=azuresqldb-mi-current&preserve-view=true) itself.

The `DATA_SOURCE` parameter value is automatically prepended to the BULK parameter to form the full path to the file. 

When using `OPENROWSET` provide the format of the file, such as the following example, which queries a single file: 

```sql
SELECT TOP 10 * 
FROM OPENROWSET( 
 BULK 'bing_covid-19_data.parquet', 
 DATA_SOURCE = 'MyExternalDataSource', 
 FORMAT = 'parquet' 
) AS filerows; 
```

## Query multiple files and folders

The `OPENROWSET` command also allows querying multiple files or folders by using wildcards in the BULK path.

The following example uses the [NYC yellow taxi trip records open data set](/azure/open-datasets/dataset-taxi-yellow):

First, create the external data source:

```sql
--Create the data source first
CREATE EXTERNAL DATA SOURCE NYCTaxiExternalDataSource 
WITH (LOCATION = 'abs://nyctlc@azureopendatastorage.blob.core.windows.net');
```

Now we can query all files with .parquet extension in folders. For example, here we'll query only those files matching a name pattern: 

```sql
SELECT TOP 10 * 
FROM OPENROWSET( 
 BULK 'yellow/puYear=*/puMonth=*/*.parquet', 
 DATA_SOURCE = 'NYCTaxiExternalDataSource', 
 FORMAT = 'parquet' 
) AS filerows;
```

When querying multiple files or folders, all files accessed with the single `OPENROWSET` must have the same structure (such as the same number of columns and data types). Folders can't be traversed recursively.

## Schema inference

Automatic schema inference helps you quickly write queries and explore data when you don't know file schemas. Schema inference only works with parquet files.

While convenient, inferred data types might be larger than the actual data types because there might be enough information in the source files to ensure the appropriate data type is used. This can lead to poor query performance. For example, parquet files don't contain metadata about maximum character column length, so the instance infers it as **varchar(8000)**.

Use the `sys.sp_describe_first_results_set` stored procedure to check the resulting data types of your query, such as the following example: 

```sql
EXEC sp_describe_first_result_set N'
 SELECT 
 vendorID, tpepPickupDateTime, passengerCount 
 FROM 
 OPENROWSET( 
  BULK ''yellow/*/*/*.parquet'', 
  DATA_SOURCE = ''NYCTaxiExternalDataSource'', 
  FORMAT=''parquet'' 
 ) AS nyc'; 
```

Once you know the data types, you can then specify them using the `WITH` clause to improve performance: 

```sql
SELECT TOP 100
 vendorID, tpepPickupDateTime, passengerCount 
FROM 
 OPENROWSET( 
  BULK 'yellow/*/*/*.parquet', 
  DATA_SOURCE = 'NYCTaxiExternalDataSource', 
  FORMAT='PARQUET' 
 ) 
WITH ( 
 vendorID varchar(4), -- we're using length of 4 instead of the inferred 8000 
 tpepPickupDateTime datetime2, 
 passengerCount int 
 ) AS nyc;
```

Since the schema of CSV files can't be automatically determined, columns must be always specified using the `WITH` clause: 

```sql
SELECT TOP 10 id, updated, confirmed, confirmed_change 
FROM OPENROWSET( 
 BULK 'bing_covid-19_data.csv', 
 DATA_SOURCE = 'MyExternalDataSource', 
 FORMAT = 'CSV', 
 FIRSTROW = 2 
) 
WITH ( 
 id int, 
 updated date, 
 confirmed int, 
 confirmed_change int 
) AS filerows; 
```

## File metadata functions

When querying multiple files or folders, you can use `filepath()` and `filename()` functions to read file metadata and get part of the path or full path and name of the file that the row in the result set originates from. In the following example, query all files and project file path and file name information for each row:

```sql
--Query all files and project file path and file name information for each row: 

SELECT TOP 10 filerows.filepath(1) as [Year_Folder], filerows.filepath(2) as [Month_Folder], 
filerows.filename() as [File_name], filerows.filepath() as [Full_Path], * 
FROM OPENROWSET( 
 BULK 'yellow/puYear=*/puMonth=*/*.parquet', 
 DATA_SOURCE = 'NYCTaxiExternalDataSource', 
 FORMAT = 'parquet') AS filerows; 

--List all paths: 
SELECT DISTINCT filerows.filepath(1) as [Year_Folder], filerows.filepath(2) as [Month_Folder] 
FROM OPENROWSET( 
 BULK 'yellow/puYear=*/puMonth=*/*.parquet', 
 DATA_SOURCE = 'NYCTaxiExternalDataSource', 
 FORMAT = 'parquet') AS filerows; 
```

- When called without a parameter, the `filepath()` function returns the file path that the row originates from. When `DATA_SOURCE` is used in `OPENROWSET`, it returns the path relative to the `DATA_SOURCE`, otherwise it returns full file path.

- When called with a parameter, the `filepath()` function returns part of the path that matches the wildcard on the position specified in the parameter. For example, the first parameter value would return part of the path that matches the first wildcard.

The `filepath()` function can also be used for filtering and aggregating rows:

```sql
SELECT 
 r.filepath() AS filepath 
 ,r.filepath(1) AS [year] 
 ,r.filepath(2) AS [month] 
 ,COUNT_BIG(*) AS [rows] 
FROM OPENROWSET( 
 BULK 'yellow/puYear=*/puMonth=*/*.parquet', 
DATA_SOURCE = 'NYCTaxiExternalDataSource', 
FORMAT = 'parquet' 
 ) AS r 
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

## Create view on top of OPENROWSET

You can [create views](../../t-sql/statements/create-view-transact-sql.md) to wrap `OPENROWSET` queries so that you can easily reuse the underlying query. Views also enable reporting and analytic tools like Power BI to consume results of OPENROWSET.

For example, consider the following view based on an `OPENROWSET` command:

```sql
CREATE VIEW TaxiRides AS 
SELECT * 
FROM OPENROWSET( 
 BULK 'yellow/puYear=*/puMonth=*/*.parquet', 
 DATA_SOURCE = 'NYCTaxiExternalDataSource', 
 FORMAT = 'parquet' 
) AS filerows;
```

It's also convenient to add columns with the file location data to a view using the `filepath()` function for easier and more performant filtering. Using views can reduce the number of files and the amount of data the query on top of the view needs to read and process when filtered by any of those columns:

```sql
CREATE VIEW TaxiRides AS 
SELECT * 
 , filerows.filepath(1) AS [year] 
 , filerows.filepath(2) AS [month] 
FROM OPENROWSET( 
 BULK 'yellow/puYear=*/puMonth=*/*.parquet', 
 DATA_SOURCE = 'NYCTaxiExternalDataSource', 
 FORMAT = 'parquet' 
) AS filerows; 
```

## External tables

[External tables](../../t-sql/statements/create-external-table-transact-sql.md) encapsulate access to files making the querying experience almost identical to querying local relational data stored in user tables. Creating an external table requires the external data source and external file format objects to exist:

```sql
--Create external file format 
CREATE EXTERNAL FILE FORMAT DemoFileFormat 
WITH ( 
 FORMAT_TYPE=PARQUET 
) 
GO 
 
--Create external table: 
CREATE EXTERNAL TABLE tbl_TaxiRides( 
 vendorID VARCHAR(100) COLLATE Latin1_General_BIN2, 
 tpepPickupDateTime DATETIME2, 
 tpepDropoffDateTime DATETIME2, 
 passengerCount INT, 
 tripDistance FLOAT, 
 puLocationId VARCHAR(8000), 
 doLocationId VARCHAR(8000), 
 startLon FLOAT, 
 startLat FLOAT, 
 endLon FLOAT, 
 endLat FLOAT, 
 rateCodeId SMALLINT, 
 storeAndFwdFlag VARCHAR(8000), 
 paymentType VARCHAR(8000), 
 fareAmount FLOAT, 
 extra FLOAT, 
 mtaTax FLOAT, 
 improvementSurcharge VARCHAR(8000), 
 tipAmount FLOAT, 
 tollsAmount FLOAT, 
 totalAmount FLOAT 
) 
WITH ( 
 LOCATION = 'yellow/puYear=*/puMonth=*/*.parquet', 
 DATA_SOURCE = NYCTaxiExternalDataSource, 
 FILE_FORMAT = DemoFileFormat 
); 
```

Once the external table is created, you can query it just like any other table:

```sql
SELECT TOP 10 * 
FROM tbl_TaxiRides; 
```

Just like OPENROWSET, external tables allow querying multiple files and folders by using wildcards. Schema inference isn't supported with external tables.

## External data sources

For more tutorials on creating external data sources and external tables to a variety of data sources, see [PolyBase Transact-SQL reference](polybase-t-sql-objects.md). 

For more tutorials on various external data sources, review:

- [Hadoop](polybase-configure-hadoop.md)
- [Azure Blob Storage](polybase-configure-azure-blob-storage.md)
- [SQL Server](polybase-configure-sql-server.md)
- [Oracle](polybase-configure-oracle.md)
- [Teradata](polybase-configure-teradata.md)
- [MongoDB](polybase-configure-mongodb.md)
- [ODBC Generic Types](polybase-configure-odbc-generic.md)
- [S3-compatible object storage](polybase-configure-s3-compatible.md)
- [CSV](virtualize-csv.md)
- [Delta table](virtualize-delta.md)

## Related content

- [Data virtualization with PolyBase in SQL Server](polybase-guide.md)
- [Frequently asked questions in PolyBase](polybase-faq.yml)
- [Performance considerations in PolyBase for SQL Server](polybase-performance.md)
- [PolyBase configuration and security for Hadoop](polybase-configuration.md)
