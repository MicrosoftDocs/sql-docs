---
title: COPY INTO (Transact-SQL)
titleSuffix: Azure Synapse Analytics and Microsoft Fabric
description: Use the COPY statement in Azure Synapse Analytics and Warehouse in Microsoft Fabric to load data from Azure Storage and OneLake.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: procha, fresantos, jovanpop
ms.date: 08/28/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "COPY_TSQL"
  - "COPY INTO"
  - "COPY"
  - "LOAD"
dev_langs:
  - TSQL
monikerRange: "=azure-sqldw-latest || =fabric"
---
# COPY INTO (Transact-SQL)

::: moniker range="=azure-sqldw-latest"

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

[!INCLUDE [synapse-fabric-migration](../../includes/synapse-fabric-migration.md)]

This article explains how to use the `COPY` statement in [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] for loading data from external storage accounts. The `COPY` statement provides the most flexibility for high-throughput data ingestion into [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].

> [!NOTE]  
> For [!INCLUDE [fabricdw](../../includes/fabric-dw.md)], see [COPY INTO](copy-into-transact-sql.md?view=fabric&preserve-view=true).

Use `COPY` for the following capabilities:

- Use lower privileged users to load data without needing strict CONTROL permissions on the data warehouse.
- Execute a single T-SQL statement without having to create any other database objects.
- Properly parse and load CSV files where *delimiters* (string, field, row) are escaped *within* string delimited columns.
- Specify a finer permission model without exposing storage account keys by using Shared Access Signatures (SAS).
- Use a different storage account for the `ERRORFILE` location (`REJECTED_ROW_LOCATION`).
- Customize default values for each target column and specify source data fields to load into specific target columns.
- Specify a custom row terminator, field terminator, and field quote for CSV files.
- Use SQL Server Date formats for CSV files.
- Specify wildcards and multiple files in the storage location path.
- Automatic schema discovery simplifies the process of defining and mapping source data into target tables.
- The automatic table creation process automatically creates the tables and works alongside automatic schema discovery.
- Directly load complex data types from Parquet files such as Maps and Lists into string columns, without using other tools to preprocess the data.

> [!NOTE]  
> To load complex data types from Parquet files, turn on automatic table creation by using `AUTO_CREATE_TABLE`.

For comprehensive examples and quickstarts using the `COPY` statement, see:

- [Quickstart: Bulk load data using the COPY statement](/azure/synapse-analytics/sql-data-warehouse/quickstart-bulk-load-copy-tsql)
- [Quickstart: Examples using the COPY statement and its supported authentication methods](/azure/synapse-analytics/sql-data-warehouse/quickstart-bulk-load-copy-tsql-examples)
- [Quickstart: Creating the COPY statement using the rich Synapse Studio UI](/azure/synapse-analytics/quickstart-load-studio-sql-pool)

[!INCLUDE [entra-id](../../includes/entra-id.md)]

## Syntax

```syntaxsql
COPY INTO [ schema. ] table_name
[ (Column_list) ]
FROM '<external_location>' [ ,...n ]
WITH
 (
 [ FILE_TYPE = { 'CSV' | 'PARQUET' | 'ORC' } ]
 [ , FILE_FORMAT = EXTERNAL FILE FORMAT OBJECT ]
 [ , CREDENTIAL = (AZURE CREDENTIAL) ]
 [ , ERRORFILE = ' [ http(s)://storageaccount/container ] /errorfile_directory [ / ] ] '
 [ , ERRORFILE_CREDENTIAL = (AZURE CREDENTIAL) ]
 [ , MAXERRORS = max_errors ]
 [ , COMPRESSION = { 'Gzip' | 'DefaultCodec' | 'Snappy' } ]
 [ , FIELDQUOTE = 'string_delimiter' ]
 [ , FIELDTERMINATOR =  'field_terminator' ]
 [ , ROWTERMINATOR = 'row_terminator' ]
 [ , FIRSTROW = first_row ]
 [ , DATEFORMAT = 'date_format' ]
 [ , ENCODING = { 'UTF8' | 'UTF16' } ]
 [ , IDENTITY_INSERT = { 'ON' | 'OFF' } ]
 [ , AUTO_CREATE_TABLE = { 'ON' | 'OFF' } ]
)
```

## Arguments

#### schema_name

Optional if the default schema for the user performing the operation is the schema of the specified table. If you don't specify schema, and the default schema of the user performing the `COPY` operation is different from the schema of the specified table, the `COPY` operation is canceled and an error message is returned.

#### table_name

The name of the table to COPY data into. The target table can be a temporary or permanent table and must already exist in the database. For automatic schema detection mode, don't provide a column list.

#### (column_list)

An optional list of one or more columns used to map source data fields to target table columns for loading data.

Don't specify a *column_list* when `AUTO_CREATE_TABLE = 'ON'`.

*column_list* must be enclosed in parentheses and delimited by commas. The column list is of the following format:

[(Column_name [default Default_value] [Field_number] [,...n])]

- *Column_name* - the name of the column in the target table.
- *Default_value* - the default value that replaces any NULL value in the input file. Default value applies to all file formats. COPY attempts to load NULL from the input file when a column is omitted from the column list or when there's an empty input file field. Default value precedes the keyword 'default'
- *Field_number* - the input file field number that is mapped to the target column.
- The field indexing starts at 1.

When you don't specify a column list, `COPY` maps columns based on the source and target order: Input field 1 goes to target column 1, field 2 goes to column 2, and so on.

#### External locations

The location where the files containing the data are staged. Currently, Azure Data Lake Storage (ADLS) Gen2 and Azure Blob Storage are supported:

- *External location* for Blob Storage: `https://<account>.blob.core.windows.net/<container>/<path>`
- *External location* for ADLS Gen2: `https://<account>.dfs.core.windows.net/<container>/<path>`

> [!NOTE]  
> The `.blob` endpoint is available for ADLS Gen2 as well and currently yields the best performance. Use the `.blob` endpoint when `.dfs`  isn't required for your authentication method.

- *Account* - The storage account name

- *Container* - The blob container name

- *Path* - the folder or file path for the data. The location starts from the container. If you specify a folder, `COPY` retrieves all files from the folder and all its subfolders. `COPY` ignores hidden folders and doesn't return files that begin with an underline (`_`) or a period (`.`) unless explicitly specified in the path. This behavior is the same even when specifying a path with a wildcard.

You can include wildcards in the path where:

- Wildcard path name matching is case-sensitive
- You can escape a wildcard by using the backslash character (`\`)
- Wildcard expansion is applied recursively. For instance, all CSV files under `Customer1` (including subdirectories of `Customer1`) are loaded in the following example: `Account/Container/Customer1/*.csv`

> [!NOTE]  
> For best performance, avoid specifying wildcards that expand over a larger number of files. If possible, list multiple file locations instead of specifying wildcards.

You can specify multiple file locations only from the same storage account and container through a comma-separated list such as:

- `https://<account>.blob.core.windows.net/<container>/<path>`, `https://<account>.blob.core.windows.net/<container>/<path>`

#### FILE_TYPE = { 'CSV' | 'PARQUET' | 'ORC' }

`FILE_TYPE` specifies the format of the external data.

- CSV: Specifies a comma-separated values file compliant with the [RFC 4180](https://datatracker.ietf.org/doc/html/rfc4180) standard.
- PARQUET: Specifies a Parquet format.
- ORC: Specifies an Optimized Row Columnar (ORC) format.

> [!NOTE]  
> The 'Delimited Text' file type in PolyBase is replaced by the 'CSV' file format. You can configure the default comma delimiter through the `FIELDTERMINATOR` parameter.

#### FILE_FORMAT = external_file_format_name

`FILE_FORMAT` applies to Parquet and ORC files only. It specifies the name of the external file format object that stores the file type and compression method for the external data. To create an external file format, use [CREATE EXTERNAL FILE FORMAT](create-external-file-format-transact-sql.md).

#### CREDENTIAL (IDENTITY = '', SECRET = '')

`CREDENTIAL` specifies the authentication mechanism to access the external storage account. Authentication methods are:

| | CSV | Parquet | ORC |
| :---: | :---: | :---: | :---: |
| **Azure Blob Storage** | SAS/MSI/SERVICE PRINCIPAL/KEY/Entra | SAS/KEY | SAS/KEY |
| **Azure Data Lake Gen2** | SAS/MSI/SERVICE PRINCIPAL/KEY/Entra | SAS (blob <sup>1</sup> )/MSI (dfs <sup>2</sup> )/SERVICE PRINCIPAL/KEY/Entra | SAS (blob <sup>1</sup> )/MSI (dfs <sup>2</sup> )/SERVICE PRINCIPAL/KEY/Entra |

<sup>1</sup> The `blob` endpoint (`.blob.core.windows.net`) in your external location path is required for this authentication method.

<sup>2</sup>  The `dfs` endpoint (`.dfs.core.windows.net`) in your external location path is required for this authentication method.

> [!NOTE]  
>  
> - When authenticating by using Microsoft Entra ID or to a public storage account, you don't need to specify `CREDENTIAL`.
> - If your storage account is associated with a VNet, you must authenticate by using a managed identity.

- Authenticating with Shared Access Signatures (SAS)

  - `IDENTITY`: A constant with a value of `Shared Access Signature`.
  - `SECRET`: The [shared access signature](/azure/storage/common/storage-sas-overview) provides delegated access to resources in your storage account.

- Minimum permissions required: READ and LIST

- Authenticating with [*Service Principals*](/azure/sql-data-warehouse/sql-data-warehouse-load-from-azure-data-lake-store#create-a-credential)

  - `IDENTITY`: `<ClientID>@<OAuth_2.0_Token_EndPoint>`
  - `SECRET`: Microsoft Entra application service principal key

- Minimum RBAC roles required: Storage blob data contributor, Storage blob data contributor, Storage blob data owner, or Storage blob data reader

- Authenticating with Storage account key

  - `IDENTITY`: A constant with a value of `Storage Account Key`
  - `SECRET`: Storage account key

- Authenticating with [Managed Identity](/azure/sql-data-warehouse/load-data-from-azure-blob-storage-using-polybase#authenticate-using-managed-identities-to-load-optional) (VNet Service Endpoints)

  - `IDENTITY`: A constant with a value of `Managed Identity`

- Minimum RBAC roles required: Storage blob data contributor or Storage blob data owner for the Microsoft Entra registered [logical server in Azure](/azure/azure-sql/database/logical-servers). When using a dedicated SQL pool (formerly SQL DW) that isn't associated with a Synapse Workspace, this RBAC role isn't required, but the managed identity requires Access Control List (ACL) permissions on the target objects to enable read access to the source files.

- Authenticating with a Microsoft Entra user

  - CREDENTIAL isn't required

- Minimum RBAC roles required: Storage blob data contributor or Storage blob data owner for the Microsoft Entra user

#### ERRORFILE = Directory location

`ERRORFILE` applies only to CSV. It specifies the directory within the `COPY` statement where the rejected rows and the corresponding error file are written. You can specify the full path from the storage account or the path relative to the container. If the specified path doesn't exist, the warehouse creates one. A child directory is created with the name `_rejectedrows`. The `_` character ensures that the directory is escaped for other data processing unless explicitly named in the location parameter.

> [!NOTE]
> When you pass a relative path to `ERRORFILE`, make it relative to the container path you specify in *external_location*. 

Within this directory, the warehouse creates a folder based on the time of load submission in the format `YearMonthDay -HourMinuteSecond` (for example, `20180330-173205`). In this folder, the process writes two types of files: the reason (error) file and the data (row) file. Each file prepends the `queryID`, `distributionID`, and a file GUID. Because the data and the reason are in separate files, corresponding files have a matching prefix.

If `ERRORFILE` has the full path of the storage account defined, `COPY` uses `ERRORFILE_CREDENTIAL` to connect to that storage. Otherwise, it uses the value you specify for `CREDENTIAL`. When you use the same credential for the source data and `ERRORFILE`, restrictions that apply to `ERRORFILE_CREDENTIAL` also apply.

#### ERRORFILE_CREDENTIAL = (IDENTITY = '', SECRET = '')

`ERRORFILE_CREDENTIAL` applies only to CSV files. Supported data source and authentication methods are:

- Azure Blob Storage: SAS, service principal, or Microsoft Entra
- Azure Data Lake Gen2: SAS, MSI, service principal, or Microsoft Entra

- Authenticating with Shared Access Signatures (SAS)
  - `IDENTITY`: A constant with a value of `Shared Access Signature`.
  - `SECRET`: The [shared access signature](/azure/storage/common/storage-sas-overview) provides delegated access to resources in your storage account.
- Minimum permissions required: READ, LIST, WRITE, CREATE, DELETE

- Authenticating with [*Service Principals*](/azure/sql-data-warehouse/sql-data-warehouse-load-from-azure-data-lake-store#create-a-credential)
  - `IDENTITY`: `<ClientID>@<OAuth_2.0_Token_EndPoint>`
  - `SECRET`: Microsoft Entra application service principal key
- Minimum RBAC roles required: Storage blob data contributor or Storage blob data owner

> [!NOTE]  
> Use the OAuth 2.0 token endpoint **V1**

- Authenticating with [Managed Identity](/azure/sql-data-warehouse/load-data-from-azure-blob-storage-using-polybase#authenticate-using-managed-identities-to-load-optional) (VNet Service Endpoints)
  - `IDENTITY`: A constant with a value of `Managed Identity`
- Minimum RBAC roles required: Storage blob data contributor or Storage blob data owner for the Microsoft Entra registered SQL Database server

- Authenticating with a Microsoft Entra user
  - `CREDENTIAL` isn't required
- Minimum RBAC roles required: Storage blob data contributor or Storage blob data owner for the Microsoft Entra user

Using a storage account key with `ERRORFILE_CREDENTIAL` isn't supported.  

> [!NOTE]  
> If you use the same storage account for your error file and specify the `ERRORFILE` path relative to the root of the container, you don't need to specify the `ERROR_CREDENTIAL`.

#### MAXERRORS = max_errors

`MAXERRORS` specifies the maximum number of reject rows allowed in the load before the `COPY` operation fails. Each row that the `COPY` operation can't import is ignored and counted as one error. If you don't specify a value for maximum number of errors, the default is `0`.

`MAXERRORS` can't be used with `AUTO_CREATE_TABLE`.

When `FILE_TYPE` is `PARQUET`, exceptions that are caused by data type conversion errors (for example, Parquet binary to SQL integer) still cause `COPY INTO` to fail, ignoring `MAXERRORS`. 

#### COMPRESSION = { 'DefaultCodec ' | 'Snappy' | 'GZIP' | 'NONE'}

`COMPRESSION` is optional and specifies the data compression method for the external data.

- CSV supports GZIP.
- Parquet supports GZIP and Snappy.
- ORC supports DefaultCodec and Snappy.
- Zlib is the default compression for ORC.

The COPY command autodetects the compression type based on the file extension when you don't specify this parameter:

- `.gz` - **GZIP**
- `.snappy` - **Snappy**
- `.deflate` - **DefaultCodec**  (Parquet and ORC only)

The COPY command requires that gzip files don't contain any trailing garbage to operate normally. The gzip format strictly requires that files be composed of valid members without any additional information before, between, or after them. Any deviation from this format, such as the presence of trailing non-gzip data, results in the failure of the COPY command. To ensure COPY runs successfully, make sure to verify there's no trailing garbage at the end of gzip files.

#### FIELDQUOTE = '*field_quote*'

*FIELDQUOTE* applies to CSV and specifies a single character that's used as the quote character (string delimiter) in the CSV file. If you don't specify this value, the quote character (`"`) is used as the quote character as defined in the RFC 4180 standard. Hexadecimal notation is also supported for `FIELDQUOTE`. Extended ASCII and multibyte characters aren't supported with UTF-8 for `FIELDQUOTE`.

> [!NOTE]  
> FIELDQUOTE characters are escaped in string columns where there's a presence of a double FIELDQUOTE (delimiter).

#### FIELDTERMINATOR = '*field_terminator*'

`FIELDTERMINATOR` only applies to CSV. Specifies the field terminator that's used in the CSV file. You can specify the field terminator by using hexadecimal notation. The field terminator can be multicharacter. The default field terminator is a (`,`). Extended ASCII and multibyte characters aren't supported with UTF-8 for `FIELDTERMINATOR`.

#### ROWTERMINATOR = '*row_terminator*'

`ROWTERMINATOR` only applies to CSV. Specifies the row terminator that's used in the CSV file. You can specify the row terminator by using hexadecimal notation. The row terminator can be multicharacter. By default, the row terminator is `\r\n`.

The COPY command prefixes the `\r` character when specifying `\n` (newline) resulting in `\r\n`. To specify only the `\n` character, use hexadecimal notation (`0x0A`). When specifying multicharacter row terminators in hexadecimal, don't specify `0x` between each character.

Extended ASCII and multibyte characters aren't supported with UTF-8 for `ROWTERMINATOR`.

#### FIRSTROW = First_row_int

`FIRSTROW` applies to CSV and specifies the row number that's read first in all files for the COPY command. Values start from `1`, which is the default value. If you set the value to `2`, the first row in every file (header row) is skipped when the data is loaded. Rows are skipped based on the existence of row terminators.

#### DATEFORMAT = { 'mdy' | 'dmy' | 'ymd' | 'ydm' | 'myd' | 'dym' }

DATEFORMAT only applies to CSV and specifies the date format of the date mapping to SQL Server date formats. For an overview of all Transact-SQL date and time data types and functions, see [Date and Time Data Types and Functions (Transact-SQL)](../functions/date-and-time-data-types-and-functions-transact-sql.md). DATEFORMAT within the COPY command takes precedence over [DATEFORMAT configured at the session level](set-dateformat-transact-sql.md).

#### ENCODING = { 'UTF8' | 'UTF16' }

`ENCODING` only applies to CSV. Default is UTF8. Specifies the data encoding standard for the files loaded by the COPY command.

#### IDENTITY_INSERT = { 'ON' | 'OFF' }

`IDENTITY_INSERT` specifies whether the identity value or values in the imported data file are to be used for the identity column. If `IDENTITY_INSERT` is `OFF` (default), the identity values for this column are verified, but not imported. Note the following behavior with the COPY command:

- If `IDENTITY_INSERT` is OFF, and table has an identity column
  - You must specify a column list that doesn't map an input field to the identity column.
- If `IDENTITY_INSERT` is ON, and table has an identity column
  - If you pass a column list, it must map an input field to the identity column.
- Default value isn't supported for the IDENTITY column in the column list.
- You can set `IDENTITY_INSERT` for only one table at a time.

Azure Synapse Analytics automatically assigns unique values based on the seed and increment values specified during table creation. 

#### AUTO_CREATE_TABLE = { 'ON' | 'OFF' }

*AUTO_CREATE_TABLE* specifies if the table can be automatically created by working alongside automatic schema discovery. *AUTO_CREATE_TABLE* is available only for Parquet files in Azure Synapse Analytics.

- ON: Enables automatic table creation. The `COPY INTO` process automatically creates a new table by discovering the structure of the file to be loaded. You can also use it with preexisting tables to take advantage of automatic schema discovery of Parquet files.
- OFF: Automatic table creation isn't enabled. Default.

> [!NOTE]  
> The automatic table creation works alongside automatic schema discovery. The automatic table creation isn't enabled by default.

### Permissions

The user running the COPY command must have the following permissions:

- [ADMINISTER DATABASE BULK OPERATIONS](grant-database-permissions-transact-sql.md#remarks)
- [INSERT](grant-database-permissions-transact-sql.md#remarks)

Requires INSERT and ADMINISTER BULK OPERATIONS permissions. In [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], you need INSERT and ADMINISTER DATABASE BULK OPERATIONS permissions.

If the user running the COPY command also wants to create a new table and load data into it, they need CREATE TABLE and ALTER ON SCHEMA permissions.

For example, to allow `mike@contoso.com` to use COPY to create a new table in the `HR` schema and insert data from a Parquet file, use the following Transact-SQL sample:

```sql
GRANT ADMINISTER DATABASE BULK OPERATIONS to [mike@contoso.com];
GRANT INSERT to [mike@contoso.com];

GRANT CREATE TABLE to [mike@contoso.com];
GRANT ALTER on SCHEMA::HR to [mike@contoso.com];
```

## Remarks

The `COPY` statement accepts only UTF-8 and UTF-16 valid characters for row data and command parameters. The `COPY` statement might incorrectly interpret source files or parameters (such as `ROWTERMINATOR` or `FIELDTERMINATOR`) that use invalid characters and cause unexpected results such as data corruption or other failures. Ensure your source files and parameters are UTF-8 or UTF-16 compliant before you invoke the `COPY` statement.

The `MAXDOP` query hint isn't supported with `COPY INTO`.

To ensure reliable execution, don't change the source files and folders during the `COPY INTO` operation.
- Modifying, deleting, or replacing any referenced files or folders while the command is running can cause the operation to fail or result in inconsistent data ingestion.
- Before executing `COPY INTO`, verify that all source data is stable and won't be altered during the process.

If the source data has greater precision than the destination column definition, the value is truncated, not rounded, for numeric, date, and time types.

## Examples

### A. Load from a public storage account

The following example shows the simplest form of the `COPY` command, which loads data from a public storage account. For this example, the `COPY` statement's defaults match the format of the line item CSV file.

```sql
COPY INTO dbo.[lineitem]
FROM 'https://unsecureaccount.blob.core.windows.net/customerdatasets/folder1/lineitem.csv'
WITH (FIELDTERMINATOR = '|')
```

The default values of the `COPY` command are:

- `DATEFORMAT` = Session DATEFORMAT

- `MAXERRORS` = 0

- `COMPRESSION` default is uncompressed

- `FIELDQUOTE` = '"'

- `FIELDTERMINATOR` = ','

- `ROWTERMINATOR` = '\n'

> [!IMPORTANT]  
> `COPY` treats `\n` as `\r\n` internally. For more information, see the `ROWTERMINATOR` section.

- `FIRSTROW` = 1

- `ENCODING` = 'UTF8'

- `FILE_TYPE` = 'CSV'

- `IDENTITY_INSERT` = 'OFF'

<a id="b-load-authenticating-via-share-access-signature-sas"></a>

### B. Load authenticating via Shared Access Signature (SAS)

The following example loads files that use the line feed as a row terminator, such as a UNIX output. This example also uses a SAS key to authenticate to Azure Blob Storage.

```sql
COPY INTO test_1
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/'
WITH (
    FILE_TYPE = 'CSV',
    CREDENTIAL=(IDENTITY= 'Shared Access Signature', SECRET='<Your_SAS_Token>'),
    --CREDENTIAL should look something like this:
    --CREDENTIAL=(IDENTITY= 'Shared Access Signature', SECRET='?sv=2018-03-28&ss=bfqt&srt=sco&sp=rl&st=2016-10-17T20%3A14%3A55Z&se=2021-10-18T20%3A19%3A00Z&sig=IEoOdmeYnE9%2FKiJDSHFSYsz4AkNa%2F%2BTx61FuQ%2FfKHefqoBE%3D'),
    FIELDQUOTE = '"',
    FIELDTERMINATOR=';',
    ROWTERMINATOR='0X0A',
    ENCODING = 'UTF8',
    DATEFORMAT = 'ymd',
    MAXERRORS = 10,
    ERRORFILE = '/errorsfolder',--path starting from the storage container
    IDENTITY_INSERT = 'ON'
)
```

### C. Load with a column list with default values authenticating via Storage Account Key

This example loads files specifying a column list with default values.

```sql
--Note when specifying the column list, input field numbers start from 1
COPY INTO test_1 (Col_one default 'myStringDefault' 1, Col_two default 1 3)
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/'
WITH (
    FILE_TYPE = 'CSV',
    CREDENTIAL=(IDENTITY= 'Storage Account Key', SECRET='<Your_Account_Key>'),
    FIELDQUOTE = '"',
    FIELDTERMINATOR=',',
    ROWTERMINATOR='0x0A',
    ENCODING = 'UTF8',
    FIRSTROW = 2
)
```

### D. Load Parquet or ORC using existing file format object

This example uses a wildcard to load all Parquet files under a folder.

```sql
COPY INTO test_parquet
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/*.parquet'
WITH (
    FILE_FORMAT = myFileFormat,
    CREDENTIAL=(IDENTITY= 'Shared Access Signature', SECRET='<Your_SAS_Token>')
)
```

### E. Load specifying wildcards and multiple files

```sql
COPY INTO t1
FROM
'https://myaccount.blob.core.windows.net/myblobcontainer/folder0/*.txt',
    'https://myaccount.blob.core.windows.net/myblobcontainer/folder1'
WITH (
    FILE_TYPE = 'CSV',
    CREDENTIAL=(IDENTITY= '<client_id>@<OAuth_2.0_Token_EndPoint>',SECRET='<key>'),
    FIELDTERMINATOR = '|'
)
```

### F. Load using MSI credentials

```sql
COPY INTO dbo.myCOPYDemoTable
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder0/*.txt'
WITH (
    FILE_TYPE = 'CSV',
    CREDENTIAL = (IDENTITY = 'Managed Identity'),
    FIELDQUOTE = '"',
    FIELDTERMINATOR=','
)
```

### G. Load using automatic schema detection

```sql
COPY INTO [myCOPYDemoTable]
FROM 'https://myaccount.blob.core.windows.net/customerdatasets/folder1/lineitem.parquet'
WITH (
    FILE_TYPE = 'Parquet',
    CREDENTIAL = ( IDENTITY = 'Shared Access Signature',  SECRET='<key>'),
    AUTO_CREATE_TABLE = 'ON'
)
```

## FAQ

### How does the performance of the COPY command compare to PolyBase?

The performance of the COPY command can be better depending on your workload.

- The warehouse can't automatically split compressed files. For the best loading performance, consider splitting your input into multiple files when loading compressed CSVs.

- The warehouse can automatically split large uncompressed CSV files for parallel loading, so you usually don't need to manually split uncompressed CSV files. In certain cases where automatic file splitting isn't feasible due to data characteristics, manually splitting large CSVs might still benefit performance.

### What is the file splitting guidance for the COPY command loading compressed CSV files?

The following table outlines the number of files you should use. When you reach the recommended number of files, you get better performance with larger files. The number of files is determined by the number of compute nodes multiplied by 60. For example, at 6000 DWU, you have 12 compute nodes, so you have 12 * 60 = 720 partitions. For a simple file splitting experience, see [How to maximize COPY load throughput with file splits](https://techcommunity.microsoft.com/blog/azuresynapseanalyticsblog/how-to-maximize-copy-load-throughput-with-file-splits/1314474).

| DWU | Number of files |
| :---: | :---: |
| 100 | 60 |
| 200 | 60 |
| 300 | 60 |
| 400 | 60 |
| 500 | 60 |
| 1,000 | 120 |
| 1,500 | 180 |
| 2,000 | 240 |
| 2,500 | 300 |
| 3,000 | 360 |
| 5,000 | 600 |
| 6,000 | 720 |
| 7,500 | 900 |
| 10,000 | 1200 |
| 15,000 | 1800 |
| 30,000 | 3600 |

### What is the file splitting guidance for the COPY command loading Parquet or ORC files?

You don't need to split Parquet and ORC files because the COPY command automatically splits files. For best performance, Parquet and ORC files in the Azure storage account should be 256 MB or larger.

### Are there any limitations on the number or size of files?

There are no limitations on the number or size of files. However, for best performance, use files that are at least 4 MB. Also, limit the count of source files to a maximum of 5,000 files for better performance.

### Are there any known issues with the COPY statement?

If you have an Azure Synapse workspace that you created before December 7, 2020, you might run into a similar error message when authenticating by using Managed Identity: `com.microsoft.sqlserver.jdbc.SQLServerException: Managed Service Identity isn't enabled on this server. Please enable Managed Service Identity and try again.`

To work around this problem, re-register the workspace's managed identity:

1. Install Azure PowerShell. See [Install PowerShell](/powershell/azure/install-az-ps?toc=/azure/synapse-analytics/sql-data-warehouse/toc.json&bc=/azure/synapse-analytics/sql-data-warehouse/breadcrumb/toc.json).
1. Register your workspace's managed identity by using PowerShell: 
   ```powershell
   Connect-AzAccount
   Select-AzSubscription -SubscriptionId <subscriptionId>
   Set-AzSqlServer -ResourceGroupName your-database-server-resourceGroup -ServerName your-SQL-servername -AssignIdentity
   ```

## Related content

- [Loading overview with [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]](/azure/sql-data-warehouse/design-elt-data-loading)
  ::: moniker-end

::: moniker range="=fabric"

[!INCLUDE [fabricdw](../../includes/applies-to-version/fabric-dw.md)]

This article explains how to use the `COPY` statement in [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] for loading from Azure Storage and OneLake. The `COPY` statement provides the most flexibility for high-throughput data ingestion into your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)], and is a strategy to [Ingest data into your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)]](/fabric/data-warehouse/ingest-data).

In Fabric Data Warehouse, the `COPY` statement supports CSV, JSONL, and PARQUET file formats. Supported data sources include Azure Data Lake Storage Gen2, Azure Blob Storage, and OneLake.

For more information on using `COPY INTO` on your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)], see [Ingest data into your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] using the COPY statement](/fabric/data-warehouse/ingest-data-copy).

`COPY INTO` runs in the current user's SQL security context. SQL permission checks and audit attribution remain associated with that user. When you specify `CREDENTIAL = (IDENTITY = 'Workspace Identity')`, the workspace identity is used only to authorize access to the source files.

Use `COPY` for the following capabilities:

- Use lower privileged users to load data without needing strict CONTROL permissions on the warehouse.
- Execute a single T-SQL statement without having to create any other database objects.
- Properly parse and load CSV files where **delimiters** (string, field, row) **are escaped within string delimited columns**.
- Properly parse and load JSONL files where each line is a valid JSON object and fields are mapped using JSON path expressions.
- Specify a finer permission model without exposing storage account keys by using Shared Access Signatures (SAS).
- Use a different storage account for the `ERRORFILE` location (`REJECTED_ROW_LOCATION`).
- Customize default values for each target column and specify source data fields to load into specific target columns.
- Specify a custom row terminator, field terminator, and field quote for CSV files.
- Specify wildcards and multiple files in the storage location path.
- For more on data ingestion options and best practices, see [Ingest data into your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] using the COPY statement](/fabric/data-warehouse/ingest-data-copy).

## Syntax

```syntaxsql
COPY INTO [ warehouse_name. ] [ schema_name. ] table_name
[ (Column_list) ]
FROM '<external_location>' [ ,...n ]
WITH
 (
 [ FILE_TYPE = { 'CSV' | 'JSONL' | 'PARQUET' } ]
 [ , CREDENTIAL = (IDENTITY = '' , SECRET = '') ]
 [ , ERRORFILE = ' [ http(s)://storageaccount/container ] /errorfile_directory [ / ] ] '
 [ , ERRORFILE_CREDENTIAL = (AZURE CREDENTIAL) ]
 [ , MAXERRORS = max_errors ]
 [ , COMPRESSION = { 'Gzip' | 'Snappy' } ]
 [ , FIELDQUOTE = 'string_delimiter' ]
 [ , FIELDTERMINATOR =  'field_terminator' ]
 [ , ROWTERMINATOR = 'row_terminator' ]
 [ , FIRSTROW = first_row ]
 [ , DATEFORMAT = 'date_format' ]
 [ , ENCODING = { 'UTF8' | 'UTF16' } ]
 [ , PARSER_VERSION = { '1.0' | '2.0' } ]
 [ , MATCH_COLUMN_COUNT = { 'ON' | 'OFF' } ]
 [ , IDENTITY_INSERT = { 'ON' | 'OFF' } ]
)
```

## Arguments

#### warehouse_name

Optional if the current warehouse for the user performing the operation is the warehouse of the specified table. If you don't specify *warehouse*, and the specified schema and table don't exist on the current warehouse, `COPY` fails, and an error message is returned.

#### schema_name

Optional if the default schema for the user performing the operation is the schema of the specified table. If you don't specify *schema*, and the default schema of the user performing the `COPY` operation is different from the schema of the specified table, `COPY` is canceled, and an error message is returned.

#### table_name

The name of the table to `COPY` data into. The target table must already exist in the warehouse.

#### (column_list)

An optional list of columns used to map source data fields to target table columns during data load.

*column_list* must be enclosed in parentheses and delimited by commas. The syntax is:

[(Column_name [default Default_value] [Field_number | JSON_path] [,...n])]

- *Column_name* - the name of the column in the target table.
- *Default_value* - the default value that replaces any `NULL` value in the input file. Default value applies to all file formats. `COPY` attempts to load `NULL` from the input file when a column is omitted from the column list or when there's an empty input file field. Default value is preceded by the keyword 'default'
- *Field_number* - Applies to CSV files only. Specifies the ordinal position of the field in the input data. The field indexing starts at 1.
- *JSON_path* - Applies to JSONL files only. Specifies a JSON path expression that identifies the field to extract from each JSON object (for example, `$.CustomerName`).

When you don't specify *column_list*, `COPY` maps columns based on the source and target order: Input field 1 goes to target column 1, field 2 goes to column 2, and so on.

> [!NOTE]  
> When working with Parquet files on [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)], column names must match exactly in the source and destination. If the name of the column in target table is different than that of the column name in the parquet file, the target table column is filled with NULL.

When you don't specify a column list, `COPY` maps columns based on the source and target order: Input field 1 goes to target column 1, field 2 goes to column 2, and so on.

#### External location

Specifies where the files containing the data are staged. Currently Azure Data Lake Storage (ADLS) Gen2, Azure Blob Storage, and OneLake are supported:

- *External location* for Blob Storage: `https://<account>.blob.core.windows.net/<container>/<path>`
- *External location* for ADLS Gen2: `https://<account>.dfs.core.windows.net/<container>/<path>`
- *External location* for OneLake: `https://onelake.dfs.fabric.microsoft.com/<workspaceId>/<lakehouseId>/Files/`

Azure Data Lake Storage (ADLS) Gen2 offers better performance than Azure Blob Storage (legacy). Consider using an ADLS Gen2 account whenever possible.

> [!NOTE]  
> The `.blob` endpoint is available for ADLS Gen2 as well and currently yields the best performance. Use the `blob` endpoint when `dfs` isn't required for your authentication method.

- *Account* - The storage account name

- *Container* - The blob container name

- *Path* - the folder or file path for the data. The location starts from the container. If you specify a folder, `COPY` retrieves all files from the folder and all its subfolders. `COPY` ignores hidden folders and doesn't return files that begin with an underline (`_`) or a period (`.`) unless explicitly specified in the path. This behavior is the same even when specifying a path with a wildcard.

You can specify multiple file locations only from the same storage account and container through a comma-separated list such as:

- `https://<account>.blob.core.windows.net/<container>/<path>, https://<account>.blob.core.windows.net/<container>/<path>`

##### Wildcards

Wildcards can be included in the path:

- Wildcard path name matching is case-sensitive.
- You can escape a wildcard by using the backslash character (`\`).

> [!NOTE]  
> For best performance, avoid specifying wildcards that expand over a larger number of files. If possible, list multiple file locations instead of specifying wildcards.

##### External locations behind firewall

To access files on Azure Data Lake Storage (ADLS) Gen2 and Azure Blob Storage locations that are behind a firewall, the following prerequisites apply:

- A **workspace identity** for the Fabric workspace hosting your warehouse must be provisioned. For more information on how to set up a workspace identity, see [Workspace identity](/fabric/security/workspace-identity).
- To impersonate the Fabric Workspace Identity, your Microsoft Entra ID account must be a member of a workspace role. Item permissions alone don't grant access to use the workspace identity.
- Grant access to the underlying files through [Azure role-based access control (RBAC)](/azure/storage/blobs/assign-azure-role-data-access?tabs=portal) or [data lake ACLs](/azure/storage/blobs/data-lake-storage-access-control). Grant this access to the executing user when you don't specify a credential or to the workspace identity when you specify Workspace Identity.
- Your Fabric workspace hosting the warehouse must be added as a **resource instance rule**. For more information on how to add your Fabric workspace with a resource instance rule, see [Resource instance rule](/fabric/security/security-trusted-workspace-access).

#### FILE_TYPE = { 'CSV' | 'JSONL' | 'PARQUET' }

`FILE_TYPE` specifies the format of the external data.

- CSV: Specifies a comma-separated values file compliant with the [RFC 4180](https://datatracker.ietf.org/doc/html/rfc4180) standard.
- JSONL: Specifies a newline-delimited JSON (JSON Lines) file, where each line is a valid JSON object.
- PARQUET: Specifies a Parquet format.

#### CREDENTIAL (IDENTITY = '', SECRET = '')

`CREDENTIAL` specifies the credential that `COPY INTO` uses when authorizing access to the external storage account or OneLake source. It doesn't change the current SQL security context or the identity that executes the statement.

In Fabric Data Warehouse: 
- `COPY INTO` isn't supported where public access is disabled.
- For public storage accounts, the supported authentication mechanisms are Microsoft Entra ID, Shared Access Signature (SAS), or Storage Account Key (SAK). 
- For public storage accounts behind a firewall, Microsoft Entra ID and Workspace Identity are supported.
- `COPY INTO` using OneLake as the source supports Microsoft Entra ID and Workspace Identity.

The executing user's Microsoft Entra identity is the default credential for source access. No credential needs to be specified.

- Authenticating with Shared Access Signature (SAS)
  - `IDENTITY`: A constant with a value of `Shared Access Signature`.
  - `SECRET`: The [shared access signature](/azure/storage/common/storage-sas-overview) provides delegated access to resources in your storage account.
  - Minimum permissions required: READ and LIST.
- Authenticating with Storage Account Key
  - `IDENTITY`: A constant with a value of `Storage Account Key`.
  - `SECRET`: Storage account key.
- Use Fabric Workspace Identity
  - `IDENTITY`: A constant with a value of `Workspace Identity`.
  - `SECRET`: Not required.
  - The statement continues to run in the current user's SQL security context. The `CREDENTIAL` clause allows `COPY INTO` to impersonate the workspace identity only when authorizing access to the source. The executing user doesn't need direct permission on the source data.
  - For Azure Blob Storage and ADLS Gen2, find the workspace identity by the workspace name, and assign the **Storage Blob Data Reader** role on the storage account or container. For ADLS Gen2 directory-level access, grant the required ACL permissions.
  - For OneLake, find the workspace identity by the workspace name, add it to the workspace that contains the source data, and assign at least the Contributor workspace role.

#### ERRORFILE = Directory location

`ERRORFILE` applies to CSV and JSONL. Specifies the directory where the rejected rows and the corresponding error file should be written. You can specify the full path from the storage account or the path relative to the container. If the specified path doesn't exist, the system creates one on your behalf. A child directory is created with the name `_rejectedrows`. The `_` character ensures that the directory is escaped for other data processing unless explicitly named in the location parameter.

> [!NOTE]
> When you pass a relative path to `ERRORFILE`, make it relative to the container path you specify in *external_location*. 

Within this directory, the warehouse creates a folder based on the time of load submission in the format `YearMonthDay -HourMinuteSecond` (for example, `20180330-173205`). In this folder, the warehouse creates a folder with the statement ID, and under that folder, two types of files are written: an error.Json file containing the reject reasons, and a row.csv file containing the rejected rows.

If `ERRORFILE` has the full path of the storage account defined, then the `ERRORFILE_CREDENTIAL` is used to connect to that storage. Otherwise, the value mentioned for `CREDENTIAL` is used. When the same credential that is used for the source data is used for `ERRORFILE`, restrictions that apply to `ERRORFILE_CREDENTIAL` also apply.

When using a firewall protected Azure Storage Account, the error file is created in the same container specified in the storage account path. When considering using the `ERRORFILE` option in this scenario, it is also required to specify the `MAXERROR` parameter. If `ERRORFILE` has the full path of the storage account defined, then the `ERRORFILE_CREDENTIAL` is used to connect to that storage. Otherwise, the value mentioned for `CREDENTIAL` is used. 

#### ERRORFILE_CREDENTIAL = (IDENTITY = '', SECRET = '')

`ERRORFILE_CREDENTIAL` applies to CSV and JSONL files. On [!INCLUDE [fabricdw](../../includes/fabric-dw.md)], the only supported authentication mechanism is Shared Access Signature (SAS).

- Authenticating with Shared Access Signatures (SAS)
  - `IDENTITY`: A constant with a value of `Shared Access Signature`.
  - `SECRET`: The [shared access signature](/azure/storage/common/storage-sas-overview) provides delegated access to resources in your storage account.
- Minimum permissions required: READ, LIST, WRITE, CREATE, DELETE

> [!NOTE]  
> If you use the same storage account for your error file and specify the `ERRORFILE` path relative to the root of the container, you don't need to specify the `ERROR_CREDENTIAL`.

#### MAXERRORS = max_errors

`MAXERRORS` applies to CSV and JSONL. Specifies the maximum number of reject rows allowed in the load before the `COPY` operation fails. Each row that the `COPY` operation can't import is ignored and counted as one error. If you don't specify a maximum number of errors, the default is `0`.

In Fabric Data Warehouse, you can't use `MAXERRORS` when `FILE_TYPE` is `PARQUET`.  

#### COMPRESSION = { 'Snappy' | 'GZIP' | 'NONE'}

`COMPRESSION` is optional and specifies the data compression method for the external data.

- CSV supports GZIP.
- Parquet supports GZIP and Snappy.
- Not supported for JSONL

The `COPY` command autodetects the compression type based on the file extension when this parameter isn't specified:

- `.gz` - **GZIP**

Loading compressed files is currently only supported with parser version 1.0. 

The `COPY` command requires that gzip files don't contain any trailing garbage to operate normally. The gzip format strictly requires that files be composed of valid members without any additional information before, between, or after them. Any deviation from this format, such as the presence of trailing non-gzip data, results in the failure of the `COPY` command. To ensure `COPY` runs successfully, make sure to verify there's no trailing garbage at the end of gzip files.

#### FIELDQUOTE = '*field_quote*'

`FIELDQUOTE` only applies to CSV. Specifies a single character that is used as the quote character (string delimiter) in the CSV file. If you don't specify `FIELDQUOTE`, the quote character (`"`) is used as the quote character as defined in the RFC 4180 standard. Hexadecimal notation is also supported for `FIELDQUOTE`. Extended ASCII and multibyte characters aren't supported with UTF-8 for `FIELDQUOTE`.

> [!NOTE]
> FIELDQUOTE characters are escaped in string columns where there's a presence of a double FIELDQUOTE (delimiter).

#### FIELDTERMINATOR = '*field_terminator*'

`FIELDTERMINATOR` only applies to CSV. Specifies the field terminator that is used in the CSV file. You can also specify the field terminator using hexadecimal notation. The field terminator can be multicharacter. The default field terminator is a (,). Extended ASCII and multibyte characters aren't supported with UTF-8 for FIELDTERMINATOR.

#### ROWTERMINATOR = '*row_terminator*'

`ROWTERMINATOR` only applies to CSV. Specifies the row terminator that is used in the CSV file. You can specify the row terminator using hexadecimal notation. The row terminator can be multicharacter. The default terminators are `\r\n`, `\n`, and `\r`.

The `COPY` command prefixes the `\r` character when specifying `\n` (newline) resulting in `\r\n`. To specify only the `\n` character, use hexadecimal notation (`0x0A`). When specifying multicharacter row terminators in hexadecimal, don't specify 0x between each character.

Extended ASCII and multibyte characters aren't supported with UTF-8 for `ROWTERMINATOR`.

#### FIRSTROW = First_row_int

`FIRSTROW` only applies to CSV. Specifies the row number that is read first in all files for the `COPY` command. Values start from `1`, which is the default value. If you set the value to `2`, the first row in every file (header row) is skipped when the data is loaded. Rows are skipped based on the existence of row terminators.

#### DATEFORMAT = { 'mdy' | 'dmy' | 'ymd' | 'ydm' | 'myd' | 'dym' }

DATEFORMAT applies to CSV and JSONL. Specifies the date format of the date mapping to SQL Server date formats. For an overview of all Transact-SQL date and time data types and functions, see [Date and Time Data Types and Functions (Transact-SQL)](../functions/date-and-time-data-types-and-functions-transact-sql.md). DATEFORMAT within the `COPY` command takes precedence over [DATEFORMAT configured at the session level](set-dateformat-transact-sql.md).

#### ENCODING = { 'UTF8' | 'UTF16' }

`ENCODING` applies to CSV and JSONL. Default is UTF8. Specifies the data encoding standard for the files loaded by the `COPY` command.

#### PARSER_VERSION = { '1.0' | '2.0' }

`PARSER_VERSION` only applies to CSV files. The default value is 2.0. `PARSER_VERSION` specifies the file parser used for ingestion when the source file type is CSV. The 2.0 parser offers improved performance for ingestion of CSV files.  

Parser version 2.0 has the following limitations: 

- Compressed CSV files aren't supported.
- Files with UTF-16 encoding aren't supported.
- Multicharacter or multibyte `ROWTERMINATOR`, `FIELDTERMINATOR`, or `FIELDQUOTE` isn't supported. However, `\r\n` is accepted as a default `ROWTERMINATOR`.

When you use parser version 1.0 with UTF-8 files, multibyte and multicharacter terminators aren't supported for `FIELDTERMINATOR`. 

Parser version 1.0 is available for backward compatibility only. Use it only when you encounter these limitations.  

> [!NOTE]
> When you use `COPY INTO` with compressed CSV files or files with UTF-16 encoding, `COPY INTO` automatically switches to `PARSER_VERSION` 1.0, without user action required. For multicharacter terminators on `FIELDTERMINATOR` or `ROWTERMINATOR`, the `COPY INTO` statement fails. Use `PARSER_VERSION = '1.0'` if you need multicharacter separators.

#### MATCH_COLUMN_COUNT = { 'ON' | 'OFF' }

`MATCH_COLUMN_COUNT` only applies to CSV files. The default value is `OFF`. It specifies if the `COPY` command should check if the column count rows in source files match the column count of the destination table. The following behavior applies: 

- If `MATCH_COLUMN_COUNT` is `OFF`:
  - The command ignores exceeding columns from source rows.
    - The command inserts `NULL` values in nullable columns for rows with fewer columns.
  - If a value isn't provided to a non-nullable column, the `COPY` command fails.
- If `MATCH_COLUMN_COUNT` is `ON`:
  - The `COPY` command checks if the column count on each row in each file from the source matches the column count of the destination table.
- If there's a column count mismatch, the `COPY` command fails.

> [!NOTE]  
> `MATCH_COLUMN_COUNT` works independently from `MAXERRORS`. A column count mismatch causes `COPY INTO` to fail regardless of `MAXERRORS`.

#### IDENTITY_INSERT = { 'ON' | 'OFF' }

`IDENTITY_INSERT` specifies whether the identity value or values in the imported data file are to be used for the identity column. If `IDENTITY_INSERT` is `OFF` (default), the identity values for this column are not imported. Note the following behavior with the COPY command:

- If `IDENTITY_INSERT` is OFF, and table has an identity column
  - You must specify a column list that doesn't map an input field to the identity column.
- If `IDENTITY_INSERT` is ON, and table has an identity column
  - If you pass a column list, it must map an input field to the identity column.
- Default value isn't supported for the IDENTITY column in the column list.
- You can set `IDENTITY_INSERT` for only one table at a time.

For more information about IDENTITY columns in Fabric Data Warehouse, refer to [IDENTITY columns in Fabric Data Warehouse](/fabric/data-warehouse/identity).

## Use COPY INTO with OneLake

Use `COPY INTO` to load data directly from files stored in the Fabric OneLake, under existing items. This method eliminates the need for external staging accounts, such as ADLS Gen2 or Blob Storage, and enables workspace-governed, SaaS-native ingestion by using Fabric permissions. This functionality supports:

- Reading from any location within a Workspace and an Item
- Workspace-to-warehouse loads within the same tenant
- Native identity enforcement by using Microsoft Entra ID or Workspace Identity

Example:

```sql
COPY INTO t1
FROM 'https://onelake.dfs.fabric.microsoft.com/<workspaceId>/<lakehouseId>/Files/*.csv'
WITH (
    FILE_TYPE = 'CSV',
    FIRSTROW = 2
);
```

## Permissions

### Control plane permissions

The control plane permission requirement depends on the credential used for source access:

- **Without Workspace Identity:** Grant the user at least the Viewer [workspace role through **Manage access**](/fabric/data-warehouse/workspace-roles), or share the warehouse with the user through [Item Permissions](/fabric/data-warehouse/share-warehouse-manage-permissions) with at least Read permission. A user with item-level access doesn't need a workspace role to run `COPY INTO`.
- **With Workspace Identity:** When you specify `CREDENTIAL = (IDENTITY = 'Workspace Identity')`, the executing user must have at least the Viewer workspace role. Item permissions alone don't authorize a user to impersonate the workspace identity.

### Data plane permissions

After you grant [control plane permissions](#control-plane-permissions) through workspace roles or item permissions, if the user only has Read permissions at the [data plane level](/fabric/security/permission-model#compute-permissions), also grant the user `INSERT` and `ADMINISTER DATABASE BULK OPERATIONS` permissions by using T-SQL commands.

When you specify `CREDENTIAL = (IDENTITY = 'Workspace Identity')`, grant the executing user `INSERT` permission on the target table. `ADMINISTER DATABASE BULK OPERATIONS` permission isn't required when the workspace identity is impersonated for source access.

For example, the following T-SQL script grants these permissions to an individual user by using their Microsoft Entra ID.

```sql
GRANT ADMINISTER DATABASE BULK OPERATIONS to [mike@contoso.com];
GO

GRANT INSERT to [mike@contoso.com];
GO
```

For example, the following statement grants the table-level permission required when the user runs `COPY INTO` with Workspace Identity:

```sql
GRANT INSERT ON OBJECT::dbo.SalesOrders TO [mike@contoso.com];
GO
```

When you use the error file option, the user must have the minimal permission of Blob Storage Contributor on the Storage Account container.

When you use OneLake as the source, the user must have **Contributor** or higher permissions on both the **source workspace** (where the Lakehouse is located) and the **target workspace** (where the Warehouse resides). Microsoft Entra ID and Fabric workspace roles govern all access.

### Source permissions for Workspace Identity

Grant the workspace identity access to each source location used by `COPY INTO`:

- For Azure Blob Storage and ADLS Gen2, find the workspace identity by using the workspace name, and assign the **Storage Blob Data Reader** role on the storage account or container. For ADLS Gen2 directory-level access, grant the required ACL permissions. Assign permissions as you would for a Microsoft Entra user.
- For OneLake, add the workspace identity to the workspace that contains the source data by using the workspace name, and assign at least the Contributor workspace role.

> [!IMPORTANT]
> Sensitivity label policies vary by organization. `COPY INTO` can fail when the destination has a sensitivity label with restrictions that prevent the operation. If a sensitivity label causes the failure, remove the label from the destination before retrying the command.

## Remarks

The `COPY` statement accepts only UTF-8 and UTF-16 valid characters for row data and command parameters. If you use source files or parameters (such as `ROWTERMINATOR` or `FIELDTERMINATOR`) that contain invalid characters, the `COPY` statement might interpret them incorrectly and cause unexpected results, such as data corruption or other failures. Before you invoke the `COPY` statement, ensure your source files and parameters are UTF-8 or UTF-16 compliant.  

The `COPY INTO` statement has restrictions on the size of individual **varchar(max)** and **varbinary(max)** columns, as well as on the total row size that you can ingest.
- Parquet: maximum **varchar(max)**/**varbinary(max)** column size 16 MB, max row size 1 GB.
- CSV and JSONL: maximum **varchar(max)**/**varbinary(max)** column size 1 MB, max row size 16 MB.

To ensure reliable execution, don't change the source files and folders during the `COPY INTO` operation.
- Modifying, deleting, or replacing any referenced files or folders while the command is running can cause the operation to fail or result in inconsistent data ingestion.
- Before executing `COPY INTO`, verify that all source data is stable and isn't altered during the process.

If the source data has greater precision than the destination column definition, the value is truncated, not rounded, for numeric, date, and time types.

<a id="limitations-for-onelake-as-source-public-preview"></a>

## Limitations for OneLake as source for COPY INTO

- **When using COPY INTO, only Microsoft Entra ID and Fabric Workspace Identity are supported as the CREDENTIAL.** Other credential types, such as SAS tokens, shared keys, or connection strings, aren't permitted.

- **Warehouse items** aren't supported as source locations. Files must originate from other Fabric items that expose files through OneLake storage.

- **OneLake paths must use workspace and warehouse IDs.** Friendly names for workspaces or Lakehouses aren't supported at this time.

- **Contributor permissions are required on both workspaces when you use the executing user's Microsoft Entra identity.** The executing user must have at least the Contributor role on the source Lakehouse workspace and the target Warehouse workspace.

- **Workspace Identity has separate permission requirements.** When you specify `CREDENTIAL = (IDENTITY = 'Workspace Identity')`, the workspace identity must have at least the Contributor role on the workspace that contains the source data. The executing user doesn't need direct access to the source workspace, but must have at least the Viewer role on the target workspace and `INSERT` permission on the target table.

## Examples

For more information on using `COPY INTO` on your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)], see [Ingest data into your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] using the COPY statement](/fabric/data-warehouse/ingest-data-copy).

### A. Load from a public storage account

The following example shows the simplest form of the `COPY` command, which loads data from a public storage account. For this example, the `COPY` statement's defaults match the format of the line item CSV file.

```sql
COPY INTO dbo.[lineitem]
FROM 'https://unsecureaccount.blob.core.windows.net/customerdatasets/folder1/lineitem.csv'
```

The default values of the `COPY` command are:

- `MAXERRORS = 0`
- `COMPRESSION` (default is uncompressed)
- `FIELDQUOTE = '"'`
- `FIELDTERMINATOR = ','`
- `ROWTERMINATOR = '\n'`

    > [!IMPORTANT]  
    > `COPY` treats `\n` as `\r\n` internally. For more information, see the `ROWTERMINATOR` section.

- `FIRSTROW = 1`
- `ENCODING = 'UTF8'`
- `FILE_TYPE = 'CSV'`

<a id="b-load-authenticating-via-share-access-signature-sas"></a>

### B. Load authenticating via Shared Access Signature (SAS)

The following example loads files that use the line feed as a row terminator, such as a UNIX output. This example also uses a SAS key to authenticate to Azure Blob Storage.

```sql
COPY INTO test_1
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/'
WITH (
    FILE_TYPE = 'CSV',
    CREDENTIAL=(IDENTITY= 'Shared Access Signature', SECRET='<Your_SAS_Token>'),
    FIELDQUOTE = '"',
    FIELDTERMINATOR = ';',
    ROWTERMINATOR = '0X0A',
    ENCODING = 'UTF8',
    MAXERRORS = 10,
    ERRORFILE = '/errorsfolder'--path starting from the storage container
)
```

### C. Load with a column list with default values authenticating via Storage Account Key (SAK)

This example loads files specifying a column list with default values.

```sql
--Note when specifying the column list, input field numbers start from 1
COPY INTO test_1 (Col_one default 'myStringDefault' 1, Col_two default 1 3)
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/'
WITH (
    FILE_TYPE = 'CSV',
    CREDENTIAL=(IDENTITY= 'Storage Account Key', SECRET='<Your_account_key>'),
    FIELDQUOTE = '"',
    FIELDTERMINATOR=',',
    ROWTERMINATOR='0x0A',
    ENCODING = 'UTF8',
    FIRSTROW = 2
)
```

### D. Load Parquet

This example uses a wildcard to load all Parquet files under a folder by using the executing user's Entra ID.

```sql
COPY INTO test_parquet
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/*.parquet'
WITH (
    FILE_TYPE = 'PARQUET'
)
```

### E. Load JSONL

This example uses a wildcard to load all JSONL files under a folder by using the executing user's Entra ID.

```sql
COPY INTO test_jsonl
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/*.jsonl'
WITH (
    FILE_TYPE = 'JSONL'
)
```

### F. Map column names to field paths in JSONL documents

The following example shows a JSON Lines (JSONL) file, where each line represents a single JSON object:

```json
{"CountryKey": 0, "CountryName": "ALGERIA", "RegionID": 0, "Population": 34800000}
{"CountryKey": 1, "CountryName": "ARGENTINA", "RegionID": 1, "Population": 46044703}
{"CountryKey": 2, "CountryName": "BRAZIL", "RegionID": 1, "Population": 203080756}
```

In `COPY INTO`, you can map table columns to specific JSON fields by using JSON path expressions. This mapping lets you ingest only the required fields from the source data.

```sql 
COPY INTO Countries (
  CountryID '$.CountryKey', 
  CountryName '$.CountryName', 
  RegionID '$.RegionKey', 
  Population '$.Population'
)
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/countries.jsonl'
WITH (   
    FILE_TYPE = 'JSONL' 
)
```

### G. Load data by specifying wildcards and multiple files

```sql
COPY INTO t1
FROM
'https://myaccount.blob.core.windows.net/myblobcontainer/folder0/*.txt',
    'https://myaccount.blob.core.windows.net/myblobcontainer/folder1'
WITH (
    FILE_TYPE = 'CSV',
    CREDENTIAL=(IDENTITY= 'Shared Access Signature', SECRET='<Your_SAS_Token>')
    FIELDTERMINATOR = '|'
)
```

<a id="f-load-data-from-onelake"></a>

### H. Load data from OneLake

```sql
COPY INTO t1
FROM 'https://onelake.dfs.fabric.microsoft.com/<workspaceId>/<lakehouseId>/Files/*.csv'
WITH (
    FILE_TYPE = 'CSV',
    FIRSTROW = 2
);
```

### I. Load data from ADLS Gen2 using Fabric Workspace Identity

The following example runs `COPY INTO` in the current user's SQL security context and impersonates the Fabric Workspace Identity to access a CSV file in ADLS Gen2. Assign the workspace identity the **Storage Blob Data Reader** role on the source storage account or container. The executing user must have at least the Viewer workspace role and `INSERT` permission on the target table.

```sql
COPY INTO dbo.SalesOrders
FROM 'https://<storage-account>.dfs.core.windows.net/<container>/orders/*.csv'
WITH (
    FILE_TYPE = 'CSV',
    FIRSTROW = 2,
    CREDENTIAL = (IDENTITY = 'Workspace Identity')
);
```

### J. Load data from OneLake using Fabric Workspace Identity

The following example runs `COPY INTO` in the current user's SQL security context and impersonates the Fabric Workspace Identity to access a CSV file in OneLake. The executing user must have at least the Viewer workspace role and `INSERT` permission on the target table, but doesn't need direct permission on the source file.

```sql
COPY INTO dbo.SalesOrders
FROM 'https://onelake.dfs.fabric.microsoft.com/<workspace-id>/<item-id>/Files/orders/*.csv'
WITH (
    FILE_TYPE = 'CSV',
    FIRSTROW = 2,
    CREDENTIAL = (IDENTITY = 'Workspace Identity')
);
```

### K. Load data into IDENTITY columns with IDENTITY_INSERT

`COPY INTO` options override any session-level setting for `IDENTITY_INSERT`.

```sql
COPY INTO dbo.Employees (EmployeeID 1, FirstName 2, LastName 3)
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/'
WITH (
    FILE_TYPE = 'CSV',
    IDENTITY_INSERT = 'ON'
);
```

## Related content

- [Ingest data into your Warehouse in Microsoft Fabric](/fabric/data-warehouse/ingest-data)
- [Ingest data into your Warehouse using the COPY statement](/fabric/data-warehouse/ingest-data-copy)

::: moniker-end
