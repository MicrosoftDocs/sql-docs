---
title: Virtualize a delta table with PolyBase
description: "Virtualize a delta table with PolyBase starting with SQL Server 2022."
author: HugoMSFT 
ms.author: hudequei 
ms.reviewer: wiassaf
ms.date: 07/25/2022
ms.service: sql
ms.subservice: polybase
ms.topic: tutorial
dev_langs:
  - "TSQL"
monikerRange: ">= sql-server-ver16 || >= sql-server-linux-ver16"
---

# Virtualize delta table with PolyBase
 [!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)] and later

[!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] can query data directly from a delta table folder. This concept, commonly referred to as data virtualization, allows the data to stay in its original location, but can be queried from a SQL Server instance with T-SQL commands like any other table. This feature uses PolyBase connectors, and minimizes the need for copying data via ETL processes.

In the example below, the delta table folder is stored on Azure Blob Storage and accessed via OPENROWQUERY or an external table.

For more information on data virtualization, [Introducing data virtualization with PolyBase](polybase-guide.md).

## Pre-configuration

### 1. Enable PolyBase in `sp_configure`

```sql
exec sp_configure @configname = 'polybase enabled', @configvalue = 1;

RECONFIGURE;
```

### 2. Create a user database

This exercise creates a sample database with default settings and location. You'll use this empty sample database to work with the data and store the scoped credential. In this example, a new empty database named `Delta_demo` will be used.

```sql
CREATE DATABASE [Delta_demo];
```

### 3. Create a master key and database scoped credential

The database master key in the user database is required to encrypt the database scoped credential secret, `delta_storage_dsc`. For this example, the delta table resides on Azure Data Lake Storage Gen2.

```sql
USE [Delta_demo];
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';  
```

```sql
CREATE DATABASE SCOPED CREDENTIAL delta_storage_dsc   
WITH IDENTITY = 'SHARED ACCESS SIGNATURE', 
SECRET = '<SAS Token>';  
```

### 4. Create external data source

Database scoped credential will be used for the external data source. In this example, the delta table resides in Azure Data Lake Storage Gen2, so use prefix `adls` and the `SHARED ACCESS SIGNATURE` identity method. For more information about the connectors and prefixes, including new settings for [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], refer to [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md?view=sql-server-ver16&preserve-view=true#location--prefixpathport-3).

```sql
CREATE EXTERNAL DATA SOURCE Delta_ED
WITH
(
 LOCATION = 'adls://<container>@<storage_account>.dfs.core.windows.net'
,CREDENTIAL = delta_storage_dsc 
);
```

For example, if your storage account is named `delta_lake_sample` and the container is named `sink`, the code would be:

```sql
CREATE EXTERNAL DATA SOURCE Delta_ED
WITH
(
 LOCATION = 'abs://sink@delta_lake_sample.dfs.core.windows.net'
,CREDENTIAL = delta_storage_dsc
)
```

## Use OPENROWQUERY to access the data

In this example the Data Table folder is named `Contoso`.

Since the external data source `Delta_ED` is mapped to a container level. The `Contoso` delta table folder is located in a root. To query a file in a folder structure, provide a folder mapping relative to the external data source's LOCATION parameter.

```sql
SELECT  * 
FROM    OPENROWSET
        (   BULK '/Contoso'
        ,   FORMAT = 'DELTA'
        ,   DATA_SOURCE = 'Delta_ED'
        ) as [result]
```

## Query data with an external table

CREATE EXTERNAL TABLE can also be used to virtualize the delta table data in SQL Server. The columns must be defined and strongly typed. While external tables take more effort to create, they also provide additional benefits over querying an external data source with OPENROWSET. You can:

1. Strengthen the definition of the data typing for a given column.
2. Define nullability.
3. Define COLLATION.
4. Create statistics for a column to optimize the quality of the query plan.
5. Create a more granular model within SQL Server for data access to enhance your security model.

For more information, see [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).

For the following example, the same data source will be used.

### 1. Create external file format

To define the file's formatting, an external file format is required. External file formats are also recommended due to reusability. For more information, see [CREATE EXTERNAL FILE FORMAT](../../t-sql/statements/create-external-file-format-transact-sql.md).


```sql
CREATE EXTERNAL FILE FORMAT DeltaTableFormat WITH(FORMAT_TYPE = DELTA);
```

### 2. Create external table

The delta table files are located at `/delta/Delta_yob/` and the external data source for this example is S3-compatible object storage, previously configured under the data source `s3_eds`. PolyBase can use the as LOCATION the delta table folder or the absolute file itself, which would be located at `delta/Delta_yob/_delta_log/00000000000000000000.json`.

```sql
-- Create External Table using delta
CREATE EXTERNAL TABLE extCall_Center_delta 
(      id int, 
       name VARCHAR(200),
       dob date
)WITH 
(     LOCATION = '/delta/Delta_yob/'
    , FILE_FORMAT = DeltaTableFormat
    , DATA_SOURCE = s3_eds
)
GO
```

## Next steps

For more tutorials on creating external data sources and external tables to a variety of data sources, see [PolyBase Transact-SQL reference](polybase-t-sql-objects.md).

- [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md)
- [CREATE EXTERNAL FILE FORMAT](../../t-sql/statements/create-external-file-format-transact-sql.md)
- [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md)
