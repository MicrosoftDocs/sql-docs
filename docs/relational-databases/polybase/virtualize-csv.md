---
title: Virtualize a CSV file with PolyBase
description: "Virtualize a CSV file with PolyBase starting with SQL Server 2022."
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf, randolphwest
ms.date: 12/11/2023
ms.service: sql
ms.subservice: polybase
ms.topic: tutorial
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16"
---

# Virtualize CSV file with PolyBase

[!INCLUDE [sqlserver2022-and-later](../../includes/applies-to-version/sqlserver2022-and-later.md)]

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] can query data directly from CSV files. This concept, commonly referred to as data virtualization, allows the data to stay in its original location, but can be queried from a SQL Server instance with T-SQL commands like any other table. This feature uses PolyBase connectors, and minimizes the need for copying data via ETL processes.

In the following example, the CSV file is stored on Azure Blob Storage and accessed via OPENROWSET or an external table.

For more information on data virtualization, [Introducing data virtualization with PolyBase](polybase-guide.md).

## Preconfiguration

### 1. Enable PolyBase in `sp_configure`

```sql
exec sp_configure @configname = 'polybase enabled', @configvalue = 1;

RECONFIGURE;
```

### 2. Create a user database

This exercise creates a sample database with default settings and location. You use this empty sample database to work with the data and store the scoped credential. In this example, a new empty database named `CSV_Demo` is used.

```sql
CREATE DATABASE [CSV_Demo];
```

### 3. Create a master key and database scoped credential

The database master key in the user database is required to encrypt the database scoped credential secret, `blob_storage`.

```sql
USE [CSV_Demo];
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password';
```

```sql
CREATE DATABASE SCOPED CREDENTIAL blob_storage
WITH IDENTITY = '<user_name>', Secret = '<password>';
```

### 4. Create external data source

A database scoped credential is used for the external data source. In this example, the CSV file resides in Azure Blob Storage, so use prefix `abs` and the `SHARED ACCESS SIGNATURE` identity method. For more information about the connectors and prefixes, including new settings for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], refer to [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md?view=sql-server-ver16&preserve-view=true#location--prefixpathport-3).

```sql
CREATE EXTERNAL DATA SOURCE Blob_CSV
WITH
(
 LOCATION = 'abs://<container>@<storage_account>.blob.core.windows.net'
,CREDENTIAL = blob_storage
);
```

For example, if your storage account is named `s3sampledata` and the container is named `import`, the code would be:

```sql
CREATE EXTERNAL DATA SOURCE Blob_CSV
WITH
(
 LOCATION = 'abs://import@s3sampledata.blob.core.windows.net'
,CREDENTIAL = blob_storage
)
```

## Use OPENROWSET to access the data

In this example, the file is named `call_center.csv`, and the data starts on the second row.

Since the external data source `Blob_CSV` is mapped to a container level. The `call_center.csv` is located in a subfolder called `2022` in the root of the container. To query a file in a folder structure, provide a folder mapping relative to the external data source's LOCATION parameter.

```sql
SELECT * FROM OPENROWSET
(
    BULK '/2022/call_center.csv',
    FORMAT = 'CSV',
    DATA_SOURCE = 'Blob_CSV',
    FIRSTROW = 2
)
WITH (
    cc_call_center_sk INT,
    cc_call_center_id CHAR(16),
    cc_rec_start_date DATE,
    cc_rec_end_date DATE,
    cc_closed_date_sk INT,
    cc_open_date_sk INT,
    cc_name VARCHAR(50),
    cc_class VARCHAR(50),
    cc_employees INT,
    cc_sq_ft INT,
    cc_hours CHAR(20),
    cc_manager VARCHAR(40),
    cc_mkt_id INT,
    cc_mkt_class CHAR(50),
    cc_mkt_desc VARCHAR(100),
    cc_market_manager VARCHAR(40),
    cc_division INT,
    cc_division_name VARCHAR(50),
    cc_company INT,
    cc_company_name CHAR(50),
    cc_street_number CHAR(10),
    cc_street_name VARCHAR(60),
    cc_street_type CHAR(15),
    cc_suite_number CHAR(10),
    cc_city VARCHAR(60),
    cc_county VARCHAR(30),
    cc_state CHAR(2),
    cc_zip CHAR(10),
    cc_country VARCHAR(20),
    cc_gmt_offset DECIMAL(5, 2),
    cc_tax_percentage DECIMAL(5, 2)
) AS [cc];
```

## Query data with an external table

CREATE EXTERNAL TABLE can also be used to virtualize the CSV data in SQL Server. The columns must be defined and strongly typed. While external tables take more effort to create, they also provide additional benefits over querying an external data source with OPENROWSET. You can:

- Strengthen the definition of the data typing for a given column
- Define nullability
- Define COLLATION
- Create statistics for a column to optimize the quality of the query plan
- Create a more granular model within SQL Server for data access to enhance your security model

For more information, see [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).

For the following example, the same data source is used.

### 1. Create external file format

To define the file's formatting, an external file format is required. External file formats are also recommended due to reusability.

In the following example, the data starts on the second row.

```sql
CREATE EXTERNAL FILE FORMAT csv_ff
WITH (
    FORMAT_TYPE = DELIMITEDTEXT,
    FORMAT_OPTIONS(
        FIELD_TERMINATOR = ',',
        STRING_DELIMITER = '"',
        FIRST_ROW = 2)
);

```

### 2. Create external table

LOCATION is the folder and file path of the `call_center.csv` file relative to the path of the location in the external data source, defined by DATA_SOURCE. In this case, the file lies in a subfolder called `2022`. Use FILE_FORMAT to specify the path to the `csv_ff` external file format in the SQL Server.

```sql
CREATE EXTERNAL TABLE extCall_Center_csv (
    cc_call_center_sk INT NOT NULL,
    cc_call_center_id CHAR(16) NOT NULL,
    cc_rec_start_date DATE,
    cc_rec_end_date DATE,
    cc_closed_date_sk INT,
    cc_open_date_sk INT,
    cc_name VARCHAR(50),
    cc_class VARCHAR(50),
    cc_employees INT,
    cc_sq_ft INT,
    cc_hours CHAR(20),
    cc_manager VARCHAR(40),
    cc_mkt_id INT,
    cc_mkt_class CHAR(50),
    cc_mkt_desc VARCHAR(100),
    cc_market_manager VARCHAR(MAX),
    cc_division VARCHAR(50),
    cc_division_name VARCHAR(50),
    cc_company VARCHAR(60),
    cc_company_name CHAR(50),
    cc_street_number CHAR(10),
    cc_street_name VARCHAR(60),
    cc_street_type CHAR(15),
    cc_suite_number CHAR(10),
    cc_city VARCHAR(60),
    cc_county VARCHAR(30),
    cc_state CHAR(20),
    cc_zip CHAR(20),
    cc_country VARCHAR(MAX),
    cc_gmt_offset DECIMAL(5, 2),
    cc_tax_percentage DECIMAL(5, 2)
    )
WITH (
    LOCATION = '/2022/call_center.csv',
    DATA_SOURCE = Blob_CSV,
    FILE_FORMAT = csv_ff
);
GO
```

## Related content

- [PolyBase Transact-SQL reference](polybase-t-sql-objects.md)
- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)
- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)](../../t-sql/statements/create-external-file-format-transact-sql.md)
- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
