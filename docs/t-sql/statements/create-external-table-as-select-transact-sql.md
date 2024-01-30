---
title: "CREATE EXTERNAL TABLE AS SELECT (CETAS) (Transact-SQL)"
description: "CREATE EXTERNAL TABLE AS SELECT (CETAS) creates an external table and then exports, in parallel, the results of a T-SQL SELECT statement."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest, wiassaf, mlandzic, nzagorac
ms.date: 07/26/2023
ms.service: sql
ms.topic: reference
f1_keywords:
  - "CREATE EXTERNAL TABLE AS SELECT"
  - "CREATE_EXTERNAL_TABLE_AS_SELECT"
  - "CREATE EXTERNAL TABLE AS SELECT_TSQL"
helpviewer_keywords:
  - "External, table"
  - "PolyBase, external table"
  - "External, table create as select"
  - "PolyBase, create table as select"
dev_langs:
  - "TSQL"
ms.custom: devx-track-azurepowershell
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-mi-current"
---
# CREATE EXTERNAL TABLE AS SELECT (CETAS) (Transact-SQL)

::: moniker range=">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-ver16||>=sql-server-linux-ver16"

[!INCLUDE [sqlserver2022-asa-pdws](../../includes/applies-to-version/sqlserver2022-asa-pdw.md)]

Creates an external table and then exports, in parallel, the results of a [!INCLUDE [tsql](../../includes/tsql-md.md)] SELECT statement.

- [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)] support Hadoop or Azure Blob storage.
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions support CREATE EXTERNAL TABLE AS SELECT (CETAS) to create an external table and then export, in parallel, the result of a [!INCLUDE [tsql](../../includes/tsql-md.md)] SELECT statement to Azure Data Lake Storage (ADLS) Gen2, Azure Storage Account V2, and S3-compatible object storage.

> [!NOTE]
> The capabilities and security of CETAS for [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] are different from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]. For more information, see the [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] version of [CREATE EXTERNAL TABLE AS SELECT](create-external-table-as-select-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true).

> [!NOTE]
> The capabilities and security of CETAS for serverless pools in [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)] are different from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [CETAS with Synapse SQL](/azure/synapse-analytics/sql/develop-tables-cetas#cetas-in-serverless-sql-pool).

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CREATE EXTERNAL TABLE { [ [ database_name . [ schema_name ] . ] | schema_name . ] table_name }
    [ (column_name [ , ...n ] ) ]
    WITH (
        LOCATION = 'hdfs_folder' | '<prefix>://<path>[:<port>]' ,
        DATA_SOURCE = external_data_source_name ,
        FILE_FORMAT = external_file_format_name
        [ , <reject_options> [ , ...n ] ]
    )
    AS <select_statement>
[;]

<reject_options> ::=
{
    | REJECT_TYPE = value | percentage
    | REJECT_VALUE = reject_value
    | REJECT_SAMPLE_VALUE = reject_sample_value
}

<select_statement> ::=
    [ WITH <common_table_expression> [ , ...n ] ]
    SELECT <select_criteria>
```

## Arguments

#### [ [ *database_name* . [ *schema_name* ] . ] | *schema_name* . ] *table_name*

The one- to three-part name of the table to create in the database. For an external table, relational database stores only the table metadata.

#### [ ( column_name [ ,...n ] ) ]

The name of a table column.

#### LOCATION

**Applies to:** [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)]

'*hdfs_folder*'**  
Specifies where to write the results of the SELECT statement on the external data source. The location is a folder name and can optionally include a path that's relative to the root folder of the Hadoop cluster or Blob storage. PolyBase creates the path and folder if it doesn't already exist.

The external files are written to `hdfs_folder` and named `QueryID_date_time_ID.format`, where `ID` is an incremental identifier and `format` is the exported data format. An example is `QID776_20160130_182739_0.orc`.

LOCATION must point to a folder and have a trailing `/`, for example: `aggregated_data/`.

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later

`prefix://path[:port]` provides the connectivity protocol (prefix), path and optionally the port, to the external data source, where the result of the SELECT statement will be written.

If the destination is S3-compatible object storage, a bucket must first exist, but PolyBase can create subfolders if necessary. [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] supports Azure Data Lake Storage Gen2, Azure Storage Account V2, and S3-compatible object storage. ORC files aren't currently supported.

#### DATA_SOURCE = *external_data_source_name*

Specifies the name of the external data source object that contains the location where the external data is stored or will be stored. The location is either a Hadoop cluster or an Azure Blob storage. To create an external data source, use [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).

#### FILE_FORMAT = *external_file_format_name*

Specifies the name of the external file format object that contains the format for the external data file. To create an external file format, use [CREATE EXTERNAL FILE FORMAT (Transact-SQL)](../../t-sql/statements/create-external-file-format-transact-sql.md).

#### REJECT options

REJECT options don't apply at the time this CREATE EXTERNAL TABLE AS SELECT statement is run. Instead, they're specified here so that the database can use them at a later time when it imports data from the external table. Later, when the CREATE TABLE AS SELECT statement selects data from the external table, the database will use the reject options to determine the number or percentage of rows that can fail to import before it stops the import.

- **REJECT_VALUE = *reject_value***

  Specifies the value or the percentage of rows that can fail to import before the database halts the import.

- **REJECT_TYPE = value | percentage**

  Clarifies whether the REJECT_VALUE option is a literal value or a percentage.

  - **value**

    Used if REJECT_VALUE is a literal value, not a percentage. The database stops importing rows from the external data file when the number of failed rows exceeds *reject_value*.

    For example, if `REJECT_VALUE = 5` and `REJECT_TYPE = value`, the database stops importing rows after five rows have failed to import.

  - **percentage**

    Used if REJECT_VALUE is a percentage, not a literal value. The database stops importing rows from the external data file when the *percentage* of failed rows exceeds *reject_value*. The percentage of failed rows is calculated at intervals. Only valid in dedicated SQL pools when `TYPE=HADOOP`.

- **REJECT_SAMPLE_VALUE = *reject_sample_value***

  Required when `REJECT_TYPE = percentage`. Specifies the number of rows to attempt to import before the database recalculates the percentage of failed rows. 

  For example, if REJECT_SAMPLE_VALUE = 1000, the database will calculate the percentage of failed rows after it has attempted to import 1000 rows from the external data file. If the percentage of failed rows is less than *reject_value*, the database attempts to load another 1000 rows. The database continues to recalculate the percentage of failed rows after it attempts to import each additional 1000 rows.

  > [!NOTE]  
  > Because the database computes the percentage of failed rows at intervals, the actual percentage of failed rows can exceed *reject_value*.

  **Example:**

  This example shows how the three REJECT options interact with each other. For example, if `REJECT_TYPE = percentage, REJECT_VALUE = 30, REJECT_SAMPLE_VALUE = 100`, the following scenario could occur:

  - The database attempts to load the first 100 rows, of which 25 fail and 75 succeed.
  - The percent of failed rows is calculated as 25%, which is less than the reject value of 30%. So, there's no need to halt the load.
  - The database attempts to load the next 100 rows. This time, 25 succeed and 75 fail.
  - The percent of failed rows is recalculated as 50%. The percentage of failed rows has exceeded the 30% reject value.
  - The load fails with 50% failed rows after attempting to load 200 rows, which is larger than the specified 30% limit.

#### WITH *common_table_expression*

Specifies a temporary named result set, known as a common table expression (CTE). For more information, see [WITH common_table_expression (Transact-SQL)](../../t-sql/queries/with-common-table-expression-transact-sql.md)

#### SELECT <select_criteria>

Populates the new table with the results from a SELECT statement. *select_criteria* is the body of the SELECT statement that determines which data to copy to the new table. For information about SELECT statements, see [SELECT (Transact-SQL)](../../t-sql/queries/select-transact-sql.md).

> [!NOTE]
> ORDER BY clause in SELECT has no effect on CETAS.

**Column options**

- **column_name [ ,...n ]**

  Column names do not allow the column options mentioned in CREATE TABLE. Instead, you can provide an optional list of one or more column names for the new table. The columns in the new table use the names you specify. When you specify column names, the number of columns in the column list must match the number of columns in the select results. If you don't specify any column names, the new target table uses the column names in the select statement results.

  You can't specify any other column options such as data types, collation, or nullability. Each of these attributes is derived from the results of the SELECT statement. However, you can use the SELECT statement to change the attributes. For an example, see [Use CETAS to change column attributes](#c-use-cetas-to-change-column-attributes).

## Permissions

 To run this command, the *database user* needs all of these permissions or memberships:

- **ALTER SCHEMA** permission on the local schema that will contain the new table or membership in the **db_ddladmin** fixed database role.
- **CREATE TABLE** permission or membership in the **db_ddladmin** fixed database role.
- **SELECT** permission on any objects referenced in the *select_criteria*.

 The login needs all of these permissions:

- **ADMINISTER BULK OPERATIONS**
- **ALTER ANY EXTERNAL DATA SOURCE**
- **ALTER ANY EXTERNAL FILE FORMAT**
- In general, you need to have permissions to **List** folder content and **Write** to the LOCATION folder for CETAS.
- In [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)], **Write** permission to read and write to the external folder on the Hadoop cluster or in Azure Blob storage.
- In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], it is also required to set proper permissions on the external location. **Write** permission to output the data to the location and **Read** permission to access it.
- For Azure Blob Storage and Azure Data Lake Gen2 the `SHARED ACCESS SIGNATURE` token must be granted the following privileges on the container: **Read**, **Write**, **List**, **Create**.
- For Azure Blog Storage, the `Allowed Services`: `Blob` checkbox must be selected to generate the SAS token.
- For Azure Data Lake Gen2, the `Allowed Services`: `Container` and `Object` checkboxes must be selected to generate the SAS token.

 > [!IMPORTANT]  
 > The ALTER ANY EXTERNAL DATA SOURCE permission grants any principal the ability to create and modify any external data source object, so it also grants the ability to access all database scoped credentials on the database. This permission must be considered as highly privileged and must be granted only to trusted principals in the system.

## Error handling

When CREATE EXTERNAL TABLE AS SELECT exports data to a text-delimited file, there's no rejection file for rows that fail to export.

When you create the external table, the database attempts to connect to the external location. If the connection fails, the command fails, and the external table is not created. It can take a minute or more for the command to fail because the database retries the connection at least three times.

If CREATE EXTERNAL TABLE AS SELECT is canceled or fails, the database makes a one-time attempt to remove any new files and folders already created on the external data source.

In [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)], the database reports any Java errors that occur on the external data source during the data export.

## <a id="GeneralRemarks"></a> Remarks

After the CREATE EXTERNAL TABLE AS SELECT statement finishes, you can run [!INCLUDE [tsql](../../includes/tsql-md.md)] queries on the external table. These operations import data into the database for the duration of the query unless you import by using the CREATE TABLE AS SELECT statement.

The external table name and definition are stored in the database metadata. The data is stored in the external data source.

The CREATE EXTERNAL TABLE AS SELECT statement always creates a nonpartitioned table, even if the source table is partitioned.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the option `allow polybase export` must be enabled by using `sp_configure`. For more information, see [Set `allow polybase export` configuration option](../../database-engine/configure-windows/allow-polybase-export.md).

For query plans in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)], created with EXPLAIN, the database uses these query plan operations for external tables: External shuffle move, External broadcast move, External partition move.

In [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)], as a prerequisite for creating an external table, the appliance administrator needs to configure Hadoop connectivity. For more information, see "Configure Connectivity to External Data (Analytics Platform System)" in the Analytics Platform System documentation, which you can download from the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=48241).

## Limitations and restrictions

Because external table data resides outside of the database, backup and restore operations only operate on data stored in the database. As a result, only the metadata is backed up and restored.

The database doesn't verify the connection to the external data source when restoring a database backup that contains an external table. If the original source isn't accessible, the metadata restore of the external table will still succeed, but SELECT operations on the external table fail.

The database doesn't guarantee data consistency between the database and the external data. You, the customer, are solely responsible to maintain consistency between the external data and the database.

Data manipulation language (DML) operations aren't supported on external tables. For example, you can't use the [!INCLUDE [tsql](../../includes/tsql-md.md)] update, insert, or delete [!INCLUDE [tsql](../../includes/tsql-md.md)] statements to modify the external data.

CREATE TABLE, DROP TABLE, CREATE STATISTICS, DROP STATISTICS, CREATE VIEW, and DROP VIEW are the only data definition language (DDL) operations allowed on external tables.

### Limitations and restrictions for Azure Synapse Analytics

- In [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] dedicated SQL pools, and [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)], PolyBase can consume a maximum of 33,000 files per folder when running 32 concurrent PolyBase queries. This maximum number includes both files and subfolders in each HDFS folder. If the degree of concurrency is less than 32, a user can run PolyBase queries against folders in HDFS that contain more than 33,000 files. We recommend that users of Hadoop and PolyBase keep file paths short and use no more than 30,000 files per HDFS folder. When too many files are referenced, a JVM out-of-memory exception occurs.

- In serverless SQL pools, external tables can't be created in a location where you currently have data. To reuse a location that has been used to store data, the location must be manually deleted on ADLS. For more limitations and best practices, see [Filter optimization best practices](/azure/synapse-analytics/sql/best-practices-serverless-sql-pool#filter-optimization).

In [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] dedicated SQL pools, and [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)], when CREATE EXTERNAL TABLE AS SELECT selects from an RCFile, the column values in the RCFile must not contain the pipe (`|`) character.

[SET ROWCOUNT (Transact-SQL)](../../t-sql/statements/set-rowcount-transact-sql.md) has no effect on CREATE EXTERNAL TABLE AS SELECT. To achieve a similar behavior, use [TOP (Transact-SQL)](../../t-sql/queries/top-transact-sql.md).

Review [Naming and Referencing Containers, Blobs, and Metadata](/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata) for limitations on file names.

### Character errors

The following characters present in data may cause errors including rejected records with CREATE EXTERNAL TABLE AS SELECT to Parquet files.

In [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)], this also applies to ORC files.

- `|`
- `"` (quotation mark character)
- `\r\n`
- `\r`
- `\n`

To use CREATE EXTERNAL TABLE AS SELECT containing these characters, you must first run the CREATE EXTERNAL TABLE AS SELECT statement to export the data to delimited text files where you can then convert them to Parquet or ORC by using an external tool.

## Working with parquet

When working with parquet files, `CREATE EXTERNAL TABLE AS SELECT` will generate one parquet file per available CPU, up to the configured maximum degree of parallelism (MAXDOP). Each file can grow up to 190 GB, after that SQL Server will generate more Parquet files as needed.

The query hint `OPTION (MAXDOP n)` will only affect the SELECT part of `CREATE EXTERNAL TABLE AS SELECT`, it has no influence on the amount of parquet files. Only database-level MAXDOP and instance-level MAXDOP is considered.


## Locking

Takes a shared lock on the SCHEMARESOLUTION object.

## Supported data types

CETAS can be used to store result sets with the following SQL data types:

- binary
- varbinary
- char
- varchar
- nchar
- nvarchar
- smalldate
- date
- datetime
- datetime2
- datetimeoffset
- time
- decimal
- numeric
- float
- real
- bigint
- tinyint
- smallint
- int
- bigint
- bit
- money
- smallmoney

## Examples

### A. Create a Hadoop table by using CREATE EXTERNAL TABLE AS SELECT

**Applies to:** [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)]

The following example creates a new external table named `hdfsCustomer` that uses the column definitions and data from the source table `dimCustomer`.

The table definition is stored in the database, and the results of the SELECT statement are exported to the `/pdwdata/customer.tbl` file on the Hadoop external data source *customer_ds*. The file is formatted according to the external file format *customer_ff*.

The file name is generated by the database and contains the query ID for ease of aligning the file with the query that generated it.

The path `hdfs://xxx.xxx.xxx.xxx:5000/files/` preceding the Customer directory must already exist. If the Customer directory doesn't exist, the database creates the directory.

> [!NOTE]  
> This example specifies for 5000. If the port isn't specified, the database uses 8020 as the default port.

The resulting Hadoop location and file name will be `hdfs:// xxx.xxx.xxx.xxx:5000/files/Customer/ QueryID_YearMonthDay_HourMinutesSeconds_FileIndex.txt.`.

```sql
-- Example is based on AdventureWorks
CREATE EXTERNAL TABLE hdfsCustomer
    WITH (
            LOCATION = '/pdwdata/customer.tbl',
            DATA_SOURCE = customer_ds,
            FILE_FORMAT = customer_ff
            ) AS

SELECT *
FROM dimCustomer;
GO
```

### B. Use a query hint with CREATE EXTERNAL TABLE AS SELECT

**Applies to:** [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)]

This query shows the basic syntax for using a query join hint with the CREATE EXTERNAL TABLE AS SELECT statement. After the query is submitted, the database uses the hash join strategy to generate the query plan. For more information on join hints and how to use the OPTION clause, see [OPTION Clause (Transact-SQL)](../../t-sql/queries/option-clause-transact-sql.md).

> [!NOTE]  
> This example specifies for 5000. If the port isn't specified, the database uses 8020 as the default port.

```sql
-- Example is based on AdventureWorks
CREATE EXTERNAL TABLE dbo.FactInternetSalesNew
    WITH (
            LOCATION = '/files/Customer',
            DATA_SOURCE = customer_ds,
            FILE_FORMAT = customer_ff
            ) AS

SELECT T1.*
FROM dbo.FactInternetSales T1
INNER JOIN dbo.DimCustomer T2
    ON (T1.CustomerKey = T2.CustomerKey)
OPTION (HASH JOIN);
GO
```

### C. Use CETAS to change column attributes

**Applies to:** [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssaps-md](../../includes/ssaps-md.md)]

This example uses CETAS to change data types, nullability, and collation for several columns in the `FactInternetSales` table.

```sql
-- Example is based on AdventureWorks
CREATE EXTERNAL TABLE dbo.FactInternetSalesNew
    WITH (
            LOCATION = '/files/Customer',
            DATA_SOURCE = customer_ds,
            FILE_FORMAT = customer_ff
            ) AS

SELECT T1.ProductKey AS ProductKeyNoChange,
    T1.OrderDateKey AS OrderDate,
    T1.ShipDateKey AS ShipDate,
    T1.CustomerKey AS CustomerKeyNoChange,
    T1.OrderQuantity AS Quantity,
    T1.SalesAmount AS MONEY
FROM dbo.FactInternetSales T1
INNER JOIN dbo.DimCustomer T2
    ON (T1.CustomerKey = T2.CustomerKey)
OPTION (HASH JOIN);
GO
```

### D. Use CREATE EXTERNAL TABLE AS SELECT exporting data as parquet

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]

The following example creates a new external table named `ext_sales` that uses the data from the table `SalesOrderDetail` of [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)]. The [allow polybase export configuration option](../../database-engine/configure-windows/allow-polybase-export.md) must be enabled.

The result of the SELECT statement will be saved on S3-compatible object storage previously configured and named `s3_eds`, and proper credential created as `s3_dsc`. The parquet file location will be `<ip>:<port>/cetas/sales.parquet` where `cetas` is the previously created storage bucket.

> [!NOTE]  
> Delta format is currently only supported as read-only.

```sql
-- Credential to access the S3-compatible object storage
CREATE DATABASE SCOPED CREDENTIAL s3_dsc
    WITH IDENTITY = 'S3 Access Key',
        SECRET = '<accesskeyid>:<secretkeyid>'
GO

-- S3-compatible object storage data source
CREATE EXTERNAL DATA SOURCE s3_eds
    WITH (
            LOCATION = 's3://<ip>:<port>',
            CREDENTIAL = s3_dsc
            )

-- External File Format for PARQUET
CREATE EXTERNAL FILE FORMAT ParquetFileFormat
    WITH (FORMAT_TYPE = PARQUET);
GO

CREATE EXTERNAL TABLE ext_sales
    WITH (
            LOCATION = '/cetas/sales.parquet',
            DATA_SOURCE = s3_eds,
            FILE_FORMAT = ParquetFileFormat
            ) AS

SELECT *
FROM AdventureWorks2022.[Sales].[SalesOrderDetail];
GO
```

### E. Use CREATE EXTERNAL TABLE AS SELECT from delta table to parquet

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]

The following example creates a new external table named `Delta_to_Parquet`, that uses Delta Table type of data located at an S3-compatible object storage named `s3_delta`, and writes the result in another data source named `s3_parquet` as a parquet file. For that the example makes uses of OPENROWSET command. The [allow polybase export configuration option](../../database-engine/configure-windows/allow-polybase-export.md) must be enabled.

```sql
-- External File Format for PARQUET
CREATE EXTERNAL FILE FORMAT ParquetFileFormat
    WITH (FORMAT_TYPE = PARQUET);
GO

CREATE EXTERNAL TABLE Delta_to_Parquet
    WITH (
            LOCATION = '/backup/sales.parquet',
            DATA_SOURCE = s3_parquet,
            FILE_FORMAT = ParquetFileFormat
            ) AS

SELECT *
FROM OPENROWSET(BULK '/delta/sales_fy22/', FORMAT = 'DELTA', DATA_SOURCE = 's3_delta') AS [r];
GO
```

### F. Use CREATE EXTERNAL TABLE AS SELECT with a view as the source

**Applies to:** [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] serverless SQL pools and dedicated SQL pools.

In this example, we can see example of a template code for writing CETAS with a user-defined view as source, using managed identity as an authentication, and `wasbs:`.

```sql
CREATE DATABASE [<mydatabase>];
GO

USE [<mydatabase>];
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<strong password>';

CREATE DATABASE SCOPED CREDENTIAL [WorkspaceIdentity] WITH IDENTITY = 'managed identity';
GO

CREATE EXTERNAL FILE FORMAT [ParquetFF] WITH (
    FORMAT_TYPE = PARQUET,
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'
);
GO

CREATE EXTERNAL DATA SOURCE [SynapseSQLwriteable] WITH (
    LOCATION = 'wasbs://<mystoageaccount>.dfs.core.windows.net/<mycontainer>/<mybaseoutputfolderpath>',
    CREDENTIAL = [WorkspaceIdentity]
);
GO

CREATE EXTERNAL TABLE [dbo].[<myexternaltable>] WITH (
        LOCATION = '<myoutputsubfolder>/',
        DATA_SOURCE = [SynapseSQLwriteable],
        FILE_FORMAT = [ParquetFF]
) AS
SELECT * FROM [<myview>];
GO
```

### G. Use CREATE EXTERNAL TABLE AS SELECT with a view as the source

**Applies to:** [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] serverless SQL pools and dedicated SQL pools.

In this example, we can see example of a template code for writing CETAS with a user-defined view as source, using managed identity as an authentication, and `https:`.

```sql
CREATE DATABASE [<mydatabase>];
GO

USE [<mydatabase>];
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<strong password>';

CREATE DATABASE SCOPED CREDENTIAL [WorkspaceIdentity] WITH IDENTITY = 'managed identity';
GO

CREATE EXTERNAL FILE FORMAT [ParquetFF] WITH (
    FORMAT_TYPE = PARQUET,
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'
);
GO

CREATE EXTERNAL DATA SOURCE [SynapseSQLwriteable] WITH (
    LOCATION = 'https://<mystoageaccount>.dfs.core.windows.net/<mycontainer>/<mybaseoutputfolderpath>',
    CREDENTIAL = [WorkspaceIdentity]
);
GO

CREATE EXTERNAL TABLE [dbo].[<myexternaltable>] WITH (
        LOCATION = '<myoutputsubfolder>/',
        DATA_SOURCE = [SynapseSQLwriteable],
        FILE_FORMAT = [ParquetFF]
) AS
SELECT * FROM [<myview>];
GO
```

## Next steps

- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)
- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)](../../t-sql/statements/create-external-file-format-transact-sql.md)
- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
- [CREATE TABLE (Azure Synapse Analytics, Parallel Data Warehouse)](~/t-sql/statements/create-table-azure-sql-data-warehouse.md)
- [CREATE TABLE AS SELECT (Azure Synapse Analytics)](../../t-sql/statements/create-table-as-select-azure-sql-data-warehouse.md)
- [DROP TABLE (Transact-SQL)](../../t-sql/statements/drop-table-transact-sql.md)
- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)

::: moniker-end

::: moniker range="azuresqldb-mi-current"

[!INCLUDE [asmi](../../includes/applies-to-version/asmi.md)]

Creates an external table and then exports, in parallel, the results of a [!INCLUDE [tsql](../../includes/tsql-md.md)] SELECT statement.

You can use CREATE EXTERNAL TABLE AS SELECT (CETAS) to complete the following tasks:  

- Create an external table on top of Parquet or CSV files in Azure Blob storage or Azure Data Lake Storage (ADLS) Gen2.
- Export, in parallel, the results of a T-SQL SELECT statement into the created external table.
- For more data virtualization capabilities of [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], see [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview).

> [!NOTE]
> This content applies to [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] only. For other platforms, choose the appropriate version of [CREATE EXTERNAL TABLE AS SELECT](create-external-table-as-select-transact-sql.md?view=azure-sqldw-latest&preserve-view=true) from the dropdrown selector.

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CREATE EXTERNAL TABLE [ [database_name  . [ schema_name ] . ] | schema_name . ] table_name
    WITH (
        LOCATION = 'path_to_folder/',  
        DATA_SOURCE = external_data_source_name,  
        FILE_FORMAT = external_file_format_name
        [, PARTITION ( column_name [ , ...n ] ) ]
)
    AS <select_statement>  
[;]

<select_statement> ::=  
    [ WITH <common_table_expression> [ ,...n ] ]  
    SELECT <select_criteria>
```

## Arguments

#### [ [ *database_name* . [ *schema_name* ] . ] | *schema_name* . ] *table_name*

The one to three-part name of the table to create. For an external table, only the table metadata is stored. No actual data is moved or stored.

#### LOCATION = *'path_to_folder'*

Specifies where to write the results of the SELECT statement on the external data source. The root folder is the data location specified in the external data source. LOCATION must point to a folder and have a trailing `/`. Example: `aggregated_data/`.

The destination folder for the CETAS must be empty. If the path and folder do not already exist, they are created automatically.

#### DATA_SOURCE = *external_data_source_name*

Specifies the name of the external data source object that contains the location where the external data will be stored. To create an external data source, use [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](create-external-data-source-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true).

#### FILE_FORMAT = *external_file_format_name*

Specifies the name of the external file format object that contains the format for the external data file. To create an external file format, use [CREATE EXTERNAL FILE FORMAT (Transact-SQL)](create-external-file-format-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true). Only external file formats with FORMAT_TYPE=PARQUET and FORMAT_TYPE=DELIMITEDTEXT are currently supported. GZip compression for DELIMITEDTEXT format is not supported.

#### [, PARTITION ( column name [ , ...n ] ) ]

Partitions the output data into multiple parquet file paths. Partitioning happens per given columns (`column_name`), matching the wildcards (*) in the LOCATION to respective partitioning column. Number of columns in the PARTITION part must match the number of wildcards in the LOCATION. There must be at least one column that is not used for partitioning.

#### WITH *<common_table_expression>*

Specifies a temporary named result set, known as a common table expression (CTE). For more information, see [WITH common_table_expression (Transact-SQL)](/sql/t-sql/queries/with-common-table-expression-transact-sql?view=azuresqldb-mi-current&preserve-view=true).

#### SELECT <select_criteria>

Populates the new table with the results from a SELECT statement. *select_criteria* is the body of the SELECT statement that determines which data to copy to the new table. For information about SELECT statements, see [SELECT (Transact-SQL)](/sql/t-sql/queries/select-transact-sql?view=azuresqldb-mi-current&preserve-view=true).

> [!NOTE]
> ORDER BY clause in SELECT is not supported for CETAS.

## Permissions

### Permissions in storage

You need to have permissions to list folder content and write to the LOCATION path for CETAS to work.

Supported authentication methods are managed identity or a Shared Access Signature (SAS) token.

- If you are using managed identity for authentication, make sure that the service principal of your SQL managed instance has a role of **Storage Blob Data Contributor** on the destination container.
- If you are using an SAS token, **Read**, **Write**, and **List** permissions are required.
- For Azure Blog Storage, the `Allowed Services`: `Blob` checkbox must be selected to generate the SAS token.
- For Azure Data Lake Gen2, the `Allowed Services`: `Container` and `Object` checkboxes must be selected to generate the SAS token.

A user-assigned managed identity is not supported. Microsoft Entra passthrough authentication is not supported. Microsoft Entra ID is ([formerly Azure Active Directory](/azure/active-directory/fundamentals/new-name)).

### Permissions in the SQL managed instance

 To run this command, the *database user* needs all of these permissions or memberships:

- **ALTER SCHEMA** permission on the local schema that will contain the new table or membership in the **db_ddladmin** fixed database role.
- **CREATE TABLE** permission or membership in the **db_ddladmin** fixed database role.
- **SELECT** permission on any objects referenced in the *select_criteria*.

 The login needs all of these permissions:

- **ADMINISTER BULK OPERATIONS**
- **ALTER ANY EXTERNAL DATA SOURCE**
- **ALTER ANY EXTERNAL FILE FORMAT**

 > [!IMPORTANT]  
 > The ALTER ANY EXTERNAL DATA SOURCE permission grants any principal the ability to create and modify any external data source object, so it also grants the ability to access all database scoped credentials on the database. This permission must be considered as highly privileged and must be granted only to trusted principals in the system.

## Supported data types

CETAS stores result sets with following SQL data types:

- binary
- varbinary
- char
- varchar
- nchar
- nvarchar
- smalldatetime
- date
- datetime
- datetime2
- datetimeoffset
- time
- decimal
- numeric
- float
- real
- bigint
- tinyint
- smallint
- int
- bigint
- bit
- money
- smallmoney

> [!NOTE]
> LOBs larger than 1MB can't be used with CETAS.


## Limitations and restrictions

- CREATE EXTERNAL TABLE AS SELECT (CETAS) for [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] is disabled by default. For more information, see the next section, [Disabled by default](#disabled-by-default).
- For more information on limitations or known issues with data virtualization in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], see [Limitations and Known issues](/azure/azure-sql/managed-instance/data-virtualization-overview#limitations).

Because external table data resides outside of the database, backup and restore operations only operate on data stored in the database. As a result, only the metadata is backed up and restored.

The database doesn't verify the connection to the external data source when restoring a database backup that contains an external table. If the original source isn't accessible, the metadata restore of the external table still succeed, but SELECT operations on the external table fail.

The database doesn't guarantee data consistency between the database and the external data. You, the customer, are solely responsible to maintain consistency between the external data and the database.

Data manipulation language (DML) operations aren't supported on external tables. For example, you can't use the [!INCLUDE [tsql](../../includes/tsql-md.md)] update, insert, or delete [!INCLUDE [tsql](../../includes/tsql-md.md)]statements to modify the external data.

CREATE TABLE, DROP TABLE, CREATE STATISTICS, DROP STATISTICS, CREATE VIEW, and DROP VIEW are the only data definition language (DDL) operations allowed on external tables.

External tables can't be created in a location where you currently have data. To reuse a location that has been used to store data, the location must be manually deleted on ADLS. 

[SET ROWCOUNT (Transact-SQL)](../../t-sql/statements/set-rowcount-transact-sql.md) has no effect on CREATE EXTERNAL TABLE AS SELECT. To achieve a similar behavior, use [TOP (Transact-SQL)](../../t-sql/queries/top-transact-sql.md).

Review [Naming and Referencing Containers, Blobs, and Metadata](/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata) for limitations on file names.

### Storage types

Files can be stored in Azure Data Lake Storage Gen2 or Azure Blob Storage. To query files, you need to provide the location in a specific format and use the location type prefix corresponding to the type of external source and endpoint/protocol, such as the following examples:

```sql
--Blob Storage endpoint
abs://<container>@<storage_account>.blob.core.windows.net/<path>/<file_name>.parquet

--Data Lake endpoint
adls://<container>@<storage_account>.blob.core.windows.net/<path>/<file_name>.parquet
```

> [!IMPORTANT]  
> The provided Location type prefix is used to choose the optimal protocol for communication and to leverage any advanced capabilities offered by the particular storage type.
> Using the generic `https://` prefix is disabled. Always use endpoint-specific prefixes.

## Disabled by default

CREATE EXTERNAL TABLE AS SELECT (CETAS) allows you to export data from your SQL managed instance into an external storage account, so there is potential for data exfiltration risk with these capabilities. Therefore, CETAS is disabled by default for [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

### Enable CETAS

CETAS for [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] can only be enabled via a method that requires elevated Azure permissions, and cannot be enabled via T-SQL. Because of the risk of unauthorized data exfiltration, CETAS cannot be enabled via the `sp_configure` T-SQL stored procedure, but instead requires that the user action outside of the SQL managed instance. 

#### Permissions to enable CETAS

To enable via Azure PowerShell, your user running the command must have **Contributor** or **SQL Security Manager** Azure RBAC roles for your SQL managed instance.

A custom role can be created for this as well, requiring the **Read** and **Write** action for the `Microsoft.Sql/managedInstances/serverConfigurationOptions` action.

#### Methods to enable CETAS

#### [PowerShell](#tab/powershell)

In order to invoke the PowerShell commands on a computer, [Az package version 9.7.0](https://www.powershellgallery.com/packages/Az/9.7.0) or newer must be installed locally. Or, consider using the [Azure Cloud Shell](/azure/cloud-shell/overview) to run Azure PowerShell at [shell.azure.com](https://shell.azure.com/).

First, log in to Azure and set the proper context for your subscription:

```powershell
Login-AzAccount
$SubscriptionID = "<YourSubscriptionIDHERE>"
Select-AzSubscription -SubscriptionName $SubscriptionID
```

To manage the server configuration option "allowPolybaseExport", adjust the following PowerShell scripts to your subscription and SQL managed instance name, then run the commands. For more information, see [Set-AzSqlServerConfigurationOption](/powershell/module/az.sql/set-azsqlserverconfigurationoption) and [Get-AzSqlServerConfigurationOption](/powershell/module/az.sql/get-azsqlserverconfigurationoption).

```powershell
# Enable ServerConfigurationOption with name "allowPolybaseExport"
Set-AzSqlServerConfigurationOption -ResourceGroupName "resource_group_name" -InstanceName "ManagedInstanceName" `
-Name "allowPolybaseExport" -Value 1
```

To disable the server configuration option "allowPolybaseExport":

```powershell
# Disable ServerConfigurationOption with name "allowPolybaseExport"
Set-AzSqlServerConfigurationOption -ResourceGroupName "resource_group_name" -InstanceName "ManagedInstanceName" `
-Name "allowPolybaseExport" -Value 0
```

To get the current value of the server configuration option "allowPolybaseExport":

```powershell
# Get ServerConfigurationOptions with name "allowPolybaseExport"
Get-AzSqlServerConfigurationOption -ResourceGroupName "resource_group_name" -InstanceName "ManagedInstanceName" `
-Name "allowPolybaseExport"
```
    
#### [Azure CLI](#tab/azurecli)

You can use [the Azure CLI](/cli/azure/install-azure-cli) to manage the server configuration option "allowPolybaseExport".

First, open either a Windows Command Prompt (CMD) or PowerShell and sign into Azure. If needed, [switch to your subscription](/cli/azure/manage-azure-subscriptions-azure-cli).

```azurecli-interactive
az login
$subscriptionId = "<your subscription ID>"
az account set --subscription $subscriptionId
```

Use the `az sql mi server-configuration-option set` to enable the server configuration option with name "allowPolybaseExport":

```azurecli-interactive
# Enable server-configuration-option with name "allowPolybaseExport"
az sql mi server-configuration-option set --resource-group 'resource_group_name' --managed-instance-name 'ManagedInstanceName' \
    --name 'allowPolybaseExport' --value '1'
```

To disable the server configuration option "allowPolybaseExport":

```azurecli-interactive
# Disable server-configuration-option with name "allowPolybaseExport"
az sql mi server-configuration-option set --resource-group 'resource_group_name' --managed-instance-name 'ManagedInstanceName' \
    --name 'allowPolybaseExport' --value '0'
```

To get the current value of the server configuration option "allowPolybaseExport":

```azurecli-interactive
# Get server-configuration-option with name "allowPolybaseExport"
az sql mi server-configuration-option show --resource-group 'resource_group_name' --managed-instance-name 'ManagedInstanceName' \
    --name 'allowPolybaseExport'
```
    
Azure CLI argument aliases:

| Short version | Aliases | 
|:--|:--|
| -g | --resource-group | 
| --mi | --instance-name  --managed-instance  --managed-instance-name |
| -n | --name  --server-configuration-option-name | 
| --value | --server-configuration-option-value | 
    
#### [API](#tab/api)

You can invoke a generic API command via Azure Cloud Shell to switch the `allowPolybaseExport` server configuration option to `1`. 

In order to enable CETAS on your SQL managed instance, adjust the following Azure PowerShell script variables to your subscription and SQL managed instance name and run it in your Azure Cloud Shell. 

> [!NOTE]
> For the following Azure Cloud Shell steps regarding configuring or verifying CETAS, use the same PowerShell console throughout. Later scripts to verify the status depend on the context set by the original script.

```powershell
# Enter your Azure subscription ID
$SubscriptionID = "<YourSubscriptionIDHERE>"

# Enter your managed instance name – for example, "sqlmi1"
$ManagedInstanceName = "<YourManagedInstanceNameHere>"

# ===============================================================================
# INVOKING THE API CALL -- REST OF THE SCRIPT IS NOT USER CONFIGURABLE
# ===============================================================================
# Log in and select a subscription if needed.

if ((Get-AzContext ) -eq $null)
{
    echo "Logging to Azure subscription"
    Login-AzAccount
}
Select-AzSubscription -SubscriptionName $SubscriptionID

# Build the URI for the API call.
$resource_group_name = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName
$uriFull = "https://management.azure.com/subscriptions/" + $SubscriptionID + "/resourceGroups/" + $resource_group_name+ "/providers/Microsoft.Sql/managedInstances/" + $ManagedInstanceName + "/serverConfigurationOptions/allowPolybaseExport" + "?api-version=2022-08-01-preview"

# Build the API request body.
$properties = @{serverConfigurationOptionValue = 1}
$bodyFull = @{properties = $properties}
$jsonBody = $bodyFull | ConvertTo-Json

# Get auth token and build the HTTP request header.
$azProfile = [Microsoft.Azure.Commands.Common.Authentication.Abstractions.AzureRmProfileProvider]::Instance.Profile
$currentAzureContext = Get-AzContext
$profileClient = New-Object Microsoft.Azure.Commands.ResourceManager.Common.RMProfileClient($azProfile)
$token = $profileClient.AcquireAccessToken($currentAzureContext.Tenant.TenantId)
$authToken = $token.AccessToken
$headers = @{}
$headers.Add("Authorization", "Bearer "+"$authToken")

# Invoke API call. API call creates an Asynchronous operation that will change the configuration setting. 
# In rare cases that your SQL MI is down when the async operation will run, the async operation may fail.
# You can check the status of the Async operation using the callback URL in the response header.
# Look for response header parameter: Azure-AsyncOperation.
Invoke-WebRequest -Method PUT -Headers $headers -Uri $uriFull -ContentType "application/json" -Body $jsonBody
```
    
> [!NOTE]
> Setting the server configuration option via invoking the API is an asynchronous operation. It is expected that the asynchronous operation executes quickly and completes within seconds, but its execution may fail for various reasons, for example, if your SQL managed instance was down at the time of operation execution. It is therefore important to verify the outcome of the change to your configuration option. 

To check the asynchronous operation outcome of the API call, there are two methods:

> [!IMPORTANT]
> When executing the following scripts, use the same Azure Cloud Shell console where you ran the script to set the configuration option. This script depends on the context set by the original `Invoke-WebRequest` script.

- Use PowerShell to invoke HTTP GET towards the server configuration API and check if the configuration option contains the value you desired. The following PowerShell script shows how to invoke a GET request to check the server configuration option. Look for the response JSON and find the `serverConfigurationOptionValue` to make sure it corresponds to what you have set in previous step. Use the same PowerShell console where you ran the script to set the configuration option, because the script depends on the context set by the original script.

   ```powershell
   # You can query the configuration option directly to see if it contains the new changed value.
   # Get the configuration status using the call below. This will return JSON with 
   # serverConfigurationOptionValue variable value. 
   # It should match what you provided in your original request:
   Invoke-WebRequest -Method GET -Headers $headers -Uri $uriFull -ContentType "application/json" 
   ```

- Use PowerShell to invoke HTTP GET towards the callback URL for the asynchronous operation and read the response JSON looking for "status" variable and its value. When you invoke the API to perform server configuration change, the response header will contain a callback URL, which you can use to check the result and status of the asynchronous operation. The header value `Azure-AsyncOperation` contains the callback URL. 
 
   This is an example response header:

   ```cli
   [Azure-AsyncOperation, https://management.azure.com/subscriptions/abcdefgh-abcd-abcd-abcd-subscription/resourceGroups/<ResourceGroupName>/providers/Microsoft.Sql/locations/eastus/serverConfigurationOptionAzureAsyncOperation/abcdefgh-abcd-abcd-abcd-abcdef123456?api-version=2022-08-01-preview]
   ```

   The following PowerShell example uses the callback URL from the response header, beginning with `https://`:

   ```powershell
   # If you got the Azure-AsyncOperation URL from response header, 
   # use the below to query the operation for status:
   $AzAsyncOpURL = "https://management.azure.com/subscriptions/abcdefgh-abcd-abcd-abcd-subscription/resourceGroups/<ResourceGroupName>/providers/Microsoft.Sql/locations/eastus/serverConfigurationOptionAzureAsyncOperation/abcdefgh-abcd-abcd-abcd-abcdef123456?api-version=2022-08-01-preview"
   Invoke-WebRequest -Method GET -Headers $headers -Uri $AzAsyncOpURL -ContentType "application/json"
   ```

---

### Verify status of CETAS

At any time you can check the current status of the CETAS configuration option. 

Connect to your SQL managed instance. Run the following T-SQL and observe the `value` column of the response. Once the server configuration change is complete, the results of this query should match your desired setting.

   ```sql
   SELECT [name], [value] FROM sys.configurations WHERE name ='allow polybase export';
   ```

## Troubleshoot

For more steps to troubleshoot data virtualization in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], see [Troubleshoot](/azure/azure-sql/managed-instance/data-virtualization-overview#troubleshoot). Error handling and common error messages for CETAS in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] follows.

### Error handling

When CREATE EXTERNAL TABLE AS SELECT exports data to a text-delimited file, there's no rejection file for rows that fail to export.

When you create the external table, the database attempts to connect to the external location. If the connection fails, the command fails, and the external table won't be created. It can take a minute or more for the command to fail because the database retries the connection at least three times.

### Common error messages

These common error messages have quick explanations for CETAS for Azure SQL Managed Instance.  

1. Specifying a location already existing in the storage. 

   Solution: Clear storage location (including snapshot), or change location parameter in query.

   Sample error message: `Msg 15842: Cannot create external table. External table location already exists.`

1. Column values formatted using JSON objects. 

   Solution: Convert value column to a single VARCHAR or NVARCHAR column, or a set of columns with explicitly defined types.

   Sample error message: `Msg 16549: Values in column value are formatted as JSON objects and cannot be written using CREATE EXTERNAL TABLE AS SELECT.`

1. Location parameter invalid (for example, multiple `//`). 

   Solution: Fix location parameter.

   Sample error message: `Msg 46504: External option 'LOCATION' is not valid. Ensure that the length and range are appropriate.`

1. Missing one of the required options (DATA_SOURCE, FILE_FORMAT, LOCATION). 

   Solution: Add the missing parameter to CETAS query.

   Sample error message: `Msg 46505: Missing required external DDL option 'FILE_FORMAT'`

1. Access problems (invalid credential, expired credential or credential with insufficient permissions). Alternate possibility is an invalid path, where the SQL managed instance received an Error 404 from storage.

   Solution: Verify credential validity and permissions. Alternatively, validate the path is valid and storage exists. Use the URL path `adls://<container>@<storage_account>.blob.core.windows.net/<path>/`.

   Sample error message: `Msg 15883: Access check for '<Read/Write>' operation against '<Storage>' failed with HRESULT = '0x...'`

1. Location part of DATA_SOURCE contains wildcards.

   Solution: Remove wildcards from the location.

   Sample error message: `Msg 16576: Location provided by DATA_SOURCE '<data source name>' cannot contain any wildcards.`

1. Number of wildcards in LOCATION parameter and number of partitioned columns do not match. 

   Solution: Ensure same number of wildcards in LOCATION as partition columns.

   Sample error message: `Msg 16577: Number of wildcards in LOCATION must match the number of columns in PARTITION clause.`

1. Column name in PARTITION clause does not match any columns in the list. 

   Solution: Make sure that columns in PARTITION are valid.

   Sample error message: `Msg 16578: The column name '<column name>' specified in the PARTITION option does not match any column specified in the column list`

1. Column specified more than once in PARTITION list. 

   Solution: Make sure that columns in PARTITION clause are unique.

   Sample error message: `Msg 16579: A column has been specified more than once in the PARTITION list. Column '<column name>' is specified more than once.`

1. Column was specified more than once in PARTITION list, or it matches no columns from SELECT list. 

   Solution: Ensure no duplicates are there in partition list, and the partition columns exist in SELECT part.

   Sample error messages: `Msg 11569: Column <column name> has been specified more than once in the partition columns list. Please try again with a valid parameter.` or `Msg 11570: Column <column name> specified in the partition columns list does not match any columns in SELECT clause. Please try again with a valid parameter.`

1. Using all columns in PARTITION list. 

   Solution: At least one of the columns from SELECT part must not be in PARTITION part of the query.

   Sample error message: `Msg 11571: All output columns in DATA_EXPORT query are declared as PARTITION columns. DATA_EXPORT query requires at least one column to export.`

1. Feature is disabled. 

   Solution: Enable the feature, using the [Disabled by default](#disabled-by-default) section in this article. 

   Sample error message: `Msg 46552: Writing into an external table is disabled. See 'https://go.microsoft.com/fwlink/?linkid=2201073' for more information`

## Locking

Takes a shared lock on the SCHEMARESOLUTION object.

## Examples

### A. Use CETAS with a view to create an external table using the managed identity

This example provides code for writing CETAS with a view as source, using system managed identity an authentication.

```sql
CREATE DATABASE [<mydatabase>];
GO

USE [<mydatabase>];
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<strong password>';

CREATE DATABASE SCOPED CREDENTIAL [WorkspaceIdentity] WITH IDENTITY = 'managed identity';
GO

CREATE EXTERNAL FILE FORMAT [ParquetFF] WITH (
    FORMAT_TYPE = PARQUET,
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'
);
GO

CREATE EXTERNAL DATA SOURCE [SQLwriteable] WITH (
    LOCATION = 'adls://container@mystorageaccount.blob.core.windows.net/mybaseoutputfolderpath',
    CREDENTIAL = [WorkspaceIdentity]
);
GO

CREATE EXTERNAL TABLE [dbo].[<myexternaltable>] WITH (
        LOCATION = '<myoutputsubfolder>/',
        DATA_SOURCE = [SQLwriteable],
        FILE_FORMAT = [ParquetFF]
) AS
SELECT * FROM [<myview>];
GO
```

### B. Use CETAS with a view to create an external table with SAS authentication

This example provides code for writing CETAS with a view as source, using an SAS token as authentication.

```sql
CREATE DATABASE [<mydatabase>];
GO

USE [<mydatabase>];
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<strong password>';

CREATE DATABASE SCOPED CREDENTIAL SAS_token
WITH
  IDENTITY = 'SHARED ACCESS SIGNATURE',
  -- Remove ? from the beginning of the SAS token
  SECRET = '<azure_shared_access_signature>' ;
GO

CREATE EXTERNAL FILE FORMAT [ParquetFF] WITH (
    FORMAT_TYPE = PARQUET,
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'
);
GO

CREATE EXTERNAL DATA SOURCE [SQLwriteable] WITH (
    LOCATION = 'adls://container@mystorageaccount.blob.core.windows.net/mybaseoutputfolderpath',
    CREDENTIAL = [SAS_token]
);
GO

CREATE EXTERNAL TABLE [dbo].[<myexternaltable>] WITH (
        LOCATION = '<myoutputsubfolder>/',
        DATA_SOURCE = [SQLwriteable],
        FILE_FORMAT = [ParquetFF]
) AS
SELECT * FROM [<myview>];
GO
```

### C. Create an external table into a single parquet file on the storage

The next two examples show how to offload some of the data from a local table into an external table stored as parquet file(s) on Azure Blob storage container. They're designed to work with [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. This example shows creating an external table as a single parquet file, where the next example shows how to create an external table and partition it into multiple folders with parquet files.

The example below works using managed identity for authentication. As such, make sure that your Azure SQL Managed Instance service principal has **Storage Blob Data Contributor** role on your Azure Blob Storage Container. Alternatively, you can modify the example and use Shared Access Secret (SAS) tokens for authentication.

The following sample, you create an external table into a single parquet file in Azure Blob Storage, selecting from `SalesOrderHeader` table for orders older than 1-Jan-2014:

```sql
--Example 1: Creating an external table into a single parquet file on the storage, selecting from SalesOrderHeader table for orders older than 1-Jan-2014:
USE [AdventureWorks2022]
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Strong Password';
GO

CREATE DATABASE SCOPED CREDENTIAL [CETASCredential]
    WITH IDENTITY = 'managed identity';
GO

CREATE EXTERNAL DATA SOURCE [CETASExternalDataSource]
WITH (
    LOCATION = 'abs://container@storageaccount.blob.core.windows.net',
    CREDENTIAL = [CETASCredential] );
GO

CREATE EXTERNAL FILE FORMAT [CETASFileFormat]
WITH(
    FORMAT_TYPE=PARQUET,
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'
    );
GO

-- Count how many rows we plan to offload
SELECT COUNT(*) FROM [AdventureWorks2022].[Sales].[SalesOrderHeader] WHERE
        OrderDate < '2013-12-31';

-- CETAS write to a single file, archive all data older than 1-Jan-2014:
CREATE EXTERNAL TABLE SalesOrdersExternal
WITH (
    LOCATION = 'SalesOrders/',
    DATA_SOURCE = [CETASExternalDataSource],
    FILE_FORMAT = [CETASFileFormat])
AS 
    SELECT 
        *
    FROM 
        [AdventureWorks2022].[Sales].[SalesOrderHeader]
    WHERE
        OrderDate < '2013-12-31';

-- you can query the newly created external table
SELECT COUNT (*) FROM SalesOrdersExternal;
```

### D. Create a partitioned external table into multiple parquet files stored in a folder tree

This example builds on the previous example to show how to create an external table and partition it into multiple folders with parquet files. You can use partitioned tables to gain performance benefits if your data set is large.

Create an external table from `SalesOrderHeader` data, using the steps from Example B, but partition the external table by `OrderDate` year and month. When querying partitioned external tables, we can benefit from partition elimination for performance.

```sql
--CETAS write to a folder hierarchy (partitioned table):
CREATE EXTERNAL TABLE SalesOrdersExternalPartitioned
WITH (
    LOCATION = 'PartitionedOrders/year=*/month=*/', 
    DATA_SOURCE = CETASExternalDataSource,
    FILE_FORMAT = CETASFileFormat,
    --year and month will correspond to the two respective wildcards in folder path    
    PARTITION (
        [Year],
        [Month]
        ) 
    )
AS
    SELECT
        *,
        YEAR(OrderDate) AS [Year],
        MONTH(OrderDate) AS [Month]
    FROM [AdventureWorks2022].[Sales].[SalesOrderHeader]
    WHERE
        OrderDate < '2013-12-31';
GO

-- you can query the newly created partitioned external table
SELECT COUNT (*) FROM SalesOrdersExternalPartitioned;
```

## Next steps

- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md?version=azuresqldb-mi-current&preserve-view=true)
- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)](../../t-sql/statements/create-external-file-format-transact-sql.md?version=azuresqldb-mi-current&preserve-view=true)
- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md?version=azuresqldb-mi-current&preserve-view=true)
- [DROP TABLE (Transact-SQL)](../../t-sql/statements/drop-table-transact-sql.md?version=azuresqldb-mi-current&preserve-view=true)
- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md?version=azuresqldb-mi-current&preserve-view=true)

::: moniker-end
