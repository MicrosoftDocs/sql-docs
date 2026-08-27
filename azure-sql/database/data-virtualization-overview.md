---
title: Data virtualization (Preview)
titleSuffix: Azure SQL Database
description: Learn about data virtualization capabilities of Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 06/17/2025
ms.service: azure-sql-database
ms.topic: concept-article
---

# Data virtualization with Azure SQL Database (Preview)

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](data-virtualization-overview.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/data-virtualization-overview.md?view=azuresql-mi&preserve-view=true)

The data virtualization feature of Azure SQL Database allows you to execute Transact-SQL (T-SQL) queries on files that store data in common data formats like CSV (with no need of using Delimited Text), Parquet, and Delta (1.0). You can query this data in Azure Data Lake Storage Gen2 or Azure Blob Storage, and combine it with locally stored relational data using joins. This way you can transparently access external data (in read-only mode) while keeping it in its original format and location - also known as data virtualization.

## Overview

Data virtualization provides two ways of querying files intended for different sets of scenarios:

- [OPENROWSET syntax](#query-data-sources-using-openrowset) – optimized for ad hoc querying of files. Typically used to quickly explore the content and the structure of a new set of files.
- [CREATE EXTERNAL TABLE syntax](#external-tables) – optimized for repetitive querying of files using identical syntax as if data were stored locally in the database. External tables require several preparation steps compared to the OPENROWSET syntax, but allow for more control over data access. External tables are typically used for analytical workloads and reporting.

In either case, an [external data source](#external-data-source) must be created using the [CREATE EXTERNAL DATA SOURCE](/sql/t-sql/statements/create-external-data-source-transact-sql?view=azuresqldb-current&preserve-view=true) T-SQL syntax, as demonstrated in this article.

### File formats

Parquet and delimited text (CSV) file formats are directly supported. The JSON file format is indirectly supported by specifying the CSV file format where queries return every document as a separate row. You can parse rows further using `JSON_VALUE` and `OPENJSON`.

### Storage types

Files can be stored in Azure Data Lake Storage Gen2 or Azure Blob Storage. To query files, you need to provide the location in a specific format and use the location type prefix corresponding to the type of external source and endpoint/protocol, such as the following examples:

```sql
--Blob Storage endpoint
abs://<container>@<storage_account>.blob.core.windows.net/<path>/<file_name>.parquet
--or
abs://<storage_account_name>.blob.core.windows.net/<container_name>/

--Data Lake endpoint
adls://<container>@<storage_account>.dfs.core.windows.net/<path>/<file_name>.parquet
--or
adls://<storage_account_name>.dfs.core.windows.net/<container_name>/
```

> [!IMPORTANT]  
> Always use endpoint-specific prefixes. The provided Location type prefix is used to choose the optimal protocol for communication and to use any advanced capabilities offered by the particular storage type.
> 
> The generic `https://` prefix is only supported for `BULK INSERT`, but not for other use cases including `OPENROWSET` or `EXTERNAL TABLE`.

## Get started

If you're new to data virtualization and want to quickly test functionality, start by querying public data sets available in [Azure Open Datasets](/azure/open-datasets/dataset-catalog), like the [Bing COVID-19 dataset](/azure/open-datasets/dataset-bing-covid-19?tabs=azure-storage) allowing anonymous access.

Use the following endpoints to query the Bing COVID-19 data sets:

- Parquet: `abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.parquet`
- CSV: `abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv`

For a quick start, run this simple T-SQL query to get first insights into the data set. This query uses [OPENROWSET](/sql/t-sql/functions/openrowset-transact-sql?view=azuresqldb-current&preserve-view=true) to query a file stored in a publicly available storage account:

```sql
--Quick query on a file stored in a publicly available storage account:
SELECT TOP 10 *
FROM OPENROWSET(
 BULK 'abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.parquet',
 FORMAT = 'parquet'
) AS filerows;
```

You can continue data set exploration by appending `WHERE`, `GROUP BY` and other clauses based on the result set of the first query.

Once you get familiar with querying public data sets, consider switching to nonpublic data sets that require providing credentials, granting access rights and configuring firewall rules. In many real-world scenarios you will operate primarily with private data sets.

### Access to nonpublic storage accounts

A user that is logged into an Azure SQL Database must be authorized to access and query files stored in non-public storage accounts. Authorization steps depend on how Azure SQL Database authenticates the storage. The types of authentications and any related parameters are not provided directly with each query. They are encapsulated in the database scoped credential object stored in the user database. The credential is used by the database to access the storage account anytime the query executes. 

Azure SQL Database supports the following authentication types:

- Shared access signature (SAS)
- Managed identity
- Microsoft Entra pass-through authentication via User Identity

### [Shared Access Signature](#tab/sas)

A shared access signature (SAS) provides delegated access to files in a storage account. SAS gives granular control over the type of access you grant, including validity interval, granted permissions, and acceptable IP address range. Once the SAS token is created, it cannot be revoked or deleted, and it allows access until its validity period expires.

1. You can get a SAS token multiple ways:
    - Navigate to the Azure portal -> your storage account -> **Shared access signature** -> Configure permissions -> Generate SAS and connection string. For more information, see [Generate a shared access signature](/azure/storage/blobs/blob-containers-portal). 
    - [Create and configure a SAS with Azure Storage Explorer](/azure/vs-azure-tools-storage-explorer-blobs). 
    - You can create a SAS token programmatically via PowerShell, Azure CLI, .NET, and REST API. For more information, see [Grant limited access to Azure Storage resources using shared access signatures (SAS)](/azure/storage/common/storage-sas-overview).
1. Grant **Read** and **List** permissions via the SAS to access external data. Currently, data virtualization with Azure SQL Database is read-only.
1. To create a database scoped credential in Azure SQL Database, you must first create the [database master key](/sql/t-sql/statements/create-master-key-transact-sql?view=azuresqldb-current&preserve-view=true), if one does not already exist. A database master key is required when the credential requires `SECRET`.

    ```sql
    -- Create MASTER KEY if it doesn't exist in the database:
    CREATE MASTER KEY 
    ENCRYPTION BY PASSWORD = '<Some Very Strong Password Here>';
    ```
    
1. When a SAS token is generated, it includes a question mark (`?`) at the beginning of the token. To use the token, you must remove the question mark (`?`) when creating a credential. For example:

    ```sql
    CREATE DATABASE SCOPED CREDENTIAL MyCredential
    WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
    SECRET = 'sv=secret string here';
    ```
    
### [Managed identity](#tab/managed-identity)

A managed identity is a feature of Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) that provides Azure services - like Azure SQL databases - with an identity managed in Microsoft Entra ID. This identity can be used to authorize requests for data access in nonpublic storage accounts. Azure SQL databases can have a system-assigned managed identity and one or more user-assigned managed identities. But only one managed identity can be used at a time.

The Azure storage administrator must first grant permissions to the managed identity to access the data. Grant permissions to the system-assigned managed identity of the SQL database the same way permissions are granted to any other Microsoft Entra user. For example:

1. In the Azure portal, navigate to your **Storage account**. 
1. In the **Access Control (IAM)** page of a storage account, select **Add role assignment**.
1. Choose the **Storage Blob Data Reader** Azure RBAC role. Select **Next**. This role provides read access to the managed identity for the necessary Azure Blob Storage containers.
    - Instead of granting membership to the Storage Blob Data Reader Azure RBAC role for the managed identity, you could also grant more granular permissions on a subset of files. All users who need access to read individual files in this container also must have **Execute** permission on all parent folders up to the root (the container). For more information, see [set ACLs in Azure Data Lake Storage Gen2](/azure/storage/blobs/data-lake-storage-explorer-acl).
1. On the next page, for **Assign access to**, select **Managed identity**. 
1. Select **+ Select members**. Search for and select the managed identity. For more information, see [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal).
1. Create the database scoped credential for managed identity authentication in your Azure SQL database, using [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](/sql/t-sql/statements/create-database-scoped-credential-transact-sql?view=azuresqldb-current&preserve-view=true). In the following example, `IDENTITY = 'Managed Identity'` is a hard-coded string. The same `IDENTITY = 'Managed Identity'` is used for both a system-assigned managed identity and a user-assigned managed identity. With a managed identity, a database master key is not required because the credential doesn't contain a `SECRET`.

    ```sql
    CREATE DATABASE SCOPED CREDENTIAL MyCredential
    WITH IDENTITY = 'Managed Identity';
    ```

### [User Identity](#tab/user-identity)

User identity, also known as "Microsoft Entra pass-through", is an authorization type where the identity of the Microsoft Entra user with access to the Azure SQL Database. 

1. Grant permissions to the Microsoft Entra ID user identity.
    - You need to be a member of the Storage Blob Data Owner, Storage Blob Data Contributor, or Storage Blob Data Reader role to use your identity to access the data. 
    - As an alternative, you can specify fine-grained ACL rules to access files and folders. Even if you are an Owner of a Storage Account, you still need to add yourself into one of the Storage Blob Data roles. To learn more about access control in Azure Data Lake Store Gen2, see [Access control in Azure Data Lake Storage Gen2](/azure/storage/blobs/data-lake-storage-access-control).
    - Microsoft Entra pass-through authentication does not support SQL authenticated login types. 

1. Create a database scoped credential for a Microsoft Entra ID account with access to the Azure SQL Database. Substitute `<UserCredential>` for the name of the identity, such as `identity-<random string>`. In the following example, `IDENTITY = 'User Identity'` is a hard-coded string. With a managed user identity, a database master key is not required because the credential doesn't contain a `SECRET`.

    ```sql
    CREATE DATABASE SCOPED CREDENTIAL <UserCredential>
    WITH IDENTITY = 'User Identity';
    ```
    
> [!IMPORTANT]
> A Microsoft Entra authentication token might be cached by the client applications. For example, Power BI caches Microsoft Entra tokens and reuses the same token for an hour. Long-running queries might fail if the token expires in the middle of the query execution. If you are experiencing query failures caused by the Microsoft Entra access token that expires in the middle of the query, consider switching to a managed identity or shared access signature (SAS).

---

### Access to public storage via anonymous accounts

If the desired dataset allows for public access (also known as anonymous access), no credential is required as long as the Azure Storage is properly configured, see [Configure anonymous read access for containers and blobs](/azure/storage/blobs/anonymous-read-access-configure).

## External data source

An external data source is an abstraction that enables easy referencing of a file location across multiple queries. To query public locations, all you need to specify while creating an external data source is the file location:

```sql
CREATE EXTERNAL DATA SOURCE MyExternalDataSource
WITH (
    LOCATION = 'abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest'
);
```

When accessing nonpublic storage accounts, along with the location, you also need to reference a database scoped credential with encapsulated authentication parameters. The following script creates an external data source pointing to the file path, and referencing a database-scoped credential.

```sql
--Create external data source pointing to the file path, and referencing database-scoped credential:
CREATE EXTERNAL DATA SOURCE MyPrivateExternalDataSource
WITH (
    LOCATION = 'abs://<privatecontainer>@privatestorageaccount.blob.core.windows.net/dataset/' 
       CREDENTIAL = [MyCredential]
);
```

## Query data sources using OPENROWSET

The [OPENROWSET](/sql/t-sql/functions/openrowset-transact-sql) syntax enables instant ad hoc querying while only creating the minimal number of database objects necessary.

`OPENROWSET` only requires creating the external data source (and possibly the credential) as opposed to the external table approach, which requires an [external file format](/sql/t-sql/statements/create-external-file-format-transact-sql?view=azuresqldb-current&preserve-view=true) and the [external table](/sql/t-sql/statements/create-external-table-transact-sql?view=azuresqldb-current&preserve-view=true) itself.

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

### Query multiple files and folders

The `OPENROWSET` command also allows querying multiple files or folders by using wildcards in the BULK path.

The following example uses the [NYC yellow taxi trip records open data set](/azure/open-datasets/dataset-taxi-yellow).

First, create the external data source:

```sql
--Create the data source first:
CREATE EXTERNAL DATA SOURCE NYCTaxiExternalDataSource
WITH (LOCATION = 'abs://nyctlc@azureopendatastorage.blob.core.windows.net');
```

Now we can query all files with .parquet extension in folders. For example, here we'll query only those files matching a name pattern: 

```sql
--Query all files with .parquet extension in folders matching name pattern:
SELECT TOP 10 *
FROM OPENROWSET(
 BULK 'yellow/puYear=*/puMonth=*/*.parquet',
 DATA_SOURCE = 'NYCTaxiExternalDataSource',
 FORMAT = 'parquet'
) AS filerows;
 ```

When querying multiple files or folders, all files accessed with the single `OPENROWSET` must have the same structure (such as the same number of columns and data types). Folders can't be traversed recursively.

### Schema inference

Automatic schema inference helps you quickly write queries and explore data when you don't know file schemas. Schema inference only works with parquet files.

While convenient, inferred data types might be larger than the actual data types because there might be enough information in the source files to ensure the appropriate data type is used. This can lead to poor query performance. For example, parquet files don't contain metadata about maximum character column length, so the instance infers it as varchar(8000).

Use the [sp_describe_first_results_set](/sql/relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql?view=azuresqldb-current&preserve-view=true) stored procedure to check the resulting data types of your query, such as the following example:

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

### File metadata functions

When querying multiple files or folders, you can use `filepath()` and `filename()` functions to read file metadata and get part of the path or full path and name of the file that the row in the result set originates from:

```sql
--Query all files and project file path and file name information for each row:
SELECT TOP 10 filerows.filepath(1) as [Year_Folder], filerows.filepath(2) as [Month_Folder],
filerows.filename() as [File_name], filerows.filepath() as [Full_Path], *
FROM OPENROWSET(
 BULK 'yellow/puYear=*/puMonth=*/*.parquet',
 DATA_SOURCE = 'NYCTaxiExternalDataSource',
 FORMAT = 'parquet') AS filerows;
```

When called without a parameter, the `filepath()` function returns the file path that the row originates from. When `DATA_SOURCE` is used in `OPENROWSET`, it returns the path relative to the `DATA_SOURCE`, otherwise it returns full file path.

When called with a parameter, it returns part of the path that matches the wildcard on the position specified in the parameter. For example, parameter value 1 would return part of the path that matches the first wildcard.

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

### Create view on top of OPENROWSET

You can create and use views to wrap OPENROWSET queries so that you can easily reuse the underlying query:

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

Views also enable reporting and analytic tools like Power BI to consume results of `OPENROWSET`.

## External tables

External tables encapsulate access to files making the querying experience almost identical to querying local relational data stored in user tables. Creating an external table requires the external data source and external file format objects to exist:

```sql
--Create external file format
CREATE EXTERNAL FILE FORMAT DemoFileFormat
WITH (
 FORMAT_TYPE=PARQUET
);

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

Just like `OPENROWSET`, external tables allow querying multiple files and folders by using wildcards. Schema inference isn't supported with external tables.

## Performance considerations

There's no hard limit to the number of files or the amount of data that can be queried, but query performance depends on the amount of data, data format, the way data is organized, and complexity of queries and joins.

### Query partitioned data

Data is often organized in subfolders also called partitions. You can instruct the query to read only particular folders and files. Doing so reduces the number of files and the amount of data the query needs to read and process, resulting in better performance. This type of query optimization is known as partition pruning or partition elimination. You can eliminate partitions from query execution by using metadata function `filepath()` in the `WHERE` clause of the query.

The following sample query reads NYC Yellow Taxi data files only for the last three months of 2017:

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
    )
WITH (
    vendorID INT
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

If your stored data isn't partitioned, consider partitioning it to improve query performance.

If you are using external tables, `filepath()` and `filename()` functions are supported but not in the `WHERE` clause. 

<!--
### Statistics

Collecting statistics on your external data is one of the most important things you can do for query optimization. The more the instance knows about your data, the faster it can execute queries. The SQL engine query optimizer is a cost-based optimizer. It compares the cost of various query plans, and then chooses the plan with the lowest cost. In most cases, it chooses the plan that executes the fastest.

### Automatic creation of statistics

Azure SQL Database analyzes incoming user queries for missing statistics. If statistics are missing, the query optimizer automatically creates statistics on individual columns in the query predicate or join condition in order to improve cardinality estimates for the query plan. Automatic creation of statistics is done synchronously so you might incur slightly degraded query performance if your columns are missing statistics. The time to create statistics for a single column depends on the size of the files targeted.

### OPENROWSET manual statistics

Single-column statistics for the `OPENROWSET` path can be created using the `sys.sp_create_openrowset_statistics` stored procedure, by passing the select query with a single column as a parameter:

```sql
EXEC sys.sp_create_openrowset_statistics N'
SELECT pickup_datetime
FROM OPENROWSET(
 BULK ''abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest/*.parquet'',
 FORMAT = ''parquet'') AS filerows
';
```

By default, the  instance uses 100% of the data provided in the dataset to create statistics. You can optionally specify the sample size as a percentage using the `TABLESAMPLE` options. To create single-column statistics for multiple columns, execute `sys.sp_create_openrowset_statistics` for each of the columns. You can't create multi-column statistics for the `OPENROWSET` path.

To update existing statistics, drop them first using the `sys.sp_drop_openrowset_statistics` stored procedure, and then recreate them using the `sys.sp_create_openrowset_statistics`:

```sql
EXEC sys.sp_drop_openrowset_statistics N'
SELECT pickup_datetime
FROM OPENROWSET(
 BULK ''abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest/*.parquet'',
 FORMAT = ''parquet'') AS filerows
';
```

### External table manual statistics

The syntax for creating statistics on external tables resembles the one used for ordinary user tables. To create statistics on a column, provide a name for the statistics object and the name of the column:

```sql
CREATE STATISTICS sVendor
ON tbl_TaxiRides (vendorID)
WITH FULLSCAN, NORECOMPUTE;
```

The `WITH` options are mandatory, and for the sample size, the allowed options are `FULLSCAN` and `SAMPLE n` percent. 

- To create single-column statistics for multiple columns, execute `CREATE STATISTICS` for each of the columns. 
- Multi-column statistics are not supported.
--> 

## Troubleshoot

Issues with query execution are typically caused by Azure SQL Database not being able to access file location. The related error messages might report insufficient access rights, nonexisting location or file path, file being used by another process, or that directory cannot be listed. In most cases this indicates that access to files is blocked by network traffic control policies or due to lack of access rights. This is what should be checked:

- Wrong or mistyped location path.
- SAS key validity: it could be expired, containing a typo, starting with a question mark.
- SAS key permissions allowed: **Read** at minimum, and **List** if wildcards are used.
- Blocked inbound traffic on the storage account. Check [Managing virtual network rules for Azure Storage](/azure/storage/common/storage-network-security?tabs=azure-portal#managing-virtual-network-rules).
- Managed Identity access rights: make sure the managed identity of the Azure SQL Database is granted access rights to the storage account.
- Compatibility level of the database must be 130 or higher for data virtualization queries to work.

## Limitations

- Currently, statistics on external tables are not supported in Azure SQL Database.
- Currently, `CREATE EXTERNAL TABLE AS SELECT` is not available on Azure SQL Database.
- [Row level security](/sql/relational-databases/security/row-level-security?view=azuresqldb-current&preserve-view=true) feature is not supported with external tables.
- [Dynamic data masking](/sql/relational-databases/security/dynamic-data-masking?view=azuresqldb-current&preserve-view=true) rule can't be defined for a column in an external table.
- Managed Identity does not support cross-tenant scenarios, if you Azure Storage Account is in a different tenant, Shared access signature is the supported method.

## Known issues

- When [parameterization for Always Encrypted](/sql/relational-databases/security/encryption/always-encrypted-query-columns-ssms?view=azuresqldb-current&preserve-view=true#param) is enabled in SQL Server Management Studio (SSMS), data virtualization queries fail with `Incorrect syntax near 'PUSHDOWN'` error message.

## Related content

- [OPENROWSET T-SQL](/sql/t-sql/functions/openrowset-transact-sql?view=azuresqldb-current&preserve-view=true)
- [CREATE EXTERNAL TABLE](/sql/t-sql/statements/create-external-table-transact-sql?view=azuresqldb-current&preserve-view=true)
- [CREATE EXTERNAL FILE FORMAT](/sql/t-sql/statements/create-external-file-format-transact-sql?view=azuresqldb-current&preserve-view=true)
- [CREATE EXTERNAL DATA SOURCE](/sql/t-sql/statements/create-external-data-source-transact-sql?view=azuresqldb-current&preserve-view=true)
