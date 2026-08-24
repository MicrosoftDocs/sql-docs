---
title: "BULK INSERT (Transact-SQL)"
description: Transact-SQL reference for the BULK INSERT statement.
author: markingmyname
ms.author: maghan
ms.reviewer: jovanpop, randolphwest, wiassaf
ms.date: 03/20/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "BULK_TSQL"
  - "BULK_INSERT"
  - "BULK_INSERT_TSQL"
  - "BULK INSERT"
helpviewer_keywords:
  - "tables [SQL Server], importing data into"
  - "inserting files"
  - "views [SQL Server], importing data into"
  - "BULK INSERT statement"
  - "views [SQL Server], exporting data from"
  - "importing data, bulk import"
  - "bulk importing [SQL Server], BULK INSERT statement"
  - "file importing [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# BULK INSERT (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi-fabricdw.md)]

Imports a data file into a database table or view in a user-specified format in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

::: moniker range="=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"

```syntaxsql
BULK INSERT
   { database_name.schema_name.table_or_view_name | schema_name.table_or_view_name | table_or_view_name }
      FROM 'data_file'
     [ WITH
    (
   [ [ , ] DATA_SOURCE = 'data_source_name' ]

   -- text formatting options
   [ [ , ] CODEPAGE = { 'RAW' | 'code_page' | 'ACP' | 'OEM' } ]
   [ [ , ] DATAFILETYPE = { 'char' | 'widechar' | 'native' | 'widenative' } ]
   [ [ , ] ROWTERMINATOR = 'row_terminator' ]
   [ [ , ] FIELDTERMINATOR = 'field_terminator' ]
   [ [ , ] FORMAT = 'CSV' ]
   [ [ , ] FIELDQUOTE = 'quote_characters' ]
   [ [ , ] FIRSTROW = first_row ]
   [ [ , ] LASTROW = last_row ]

   -- input file format options
   [ [ , ] FORMATFILE = 'format_file_path' ]
   [ [ , ] FORMATFILE_DATA_SOURCE = 'data_source_name' ]

   -- error handling options
   [ [ , ] MAXERRORS = max_errors ]
   [ [ , ] ERRORFILE = 'file_name' ]
   [ [ , ] ERRORFILE_DATA_SOURCE = 'errorfile_data_source_name' ]

   -- database options
   [ [ , ] KEEPIDENTITY ]
   [ [ , ] KEEPNULLS ]
   [ [ , ] FIRE_TRIGGERS ]
   [ [ , ] CHECK_CONSTRAINTS ]
   [ [ , ] TABLOCK ]

   -- source options
   [ [ , ] ORDER ( { column [ ASC | DESC ] } [ , ...n ] ) ]
   [ [ , ] ROWS_PER_BATCH = rows_per_batch ]
   [ [ , ] KILOBYTES_PER_BATCH = kilobytes_per_batch ]
   [ [ , ] BATCHSIZE = batch_size ]

    ) ]
```

::: moniker-end

::: moniker range="=fabric"

```syntaxsql
BULK INSERT
   { database_name.schema_name.table_or_view_name | schema_name.table_or_view_name | table_or_view_name }
      FROM 'data_file'
     [ WITH
    (
   [ [ , ] DATA_SOURCE = 'data_source_name' ]

   -- text formatting options
   [ [ , ] CODEPAGE = { 'code_page' | 'ACP' } ]
   [ [ , ] DATAFILETYPE = { 'char' | 'widechar' } ]
   [ [ , ] ROWTERMINATOR = 'row_terminator' ]
   [ [ , ] FIELDTERMINATOR = 'field_terminator' ]
   [ [ , ] FORMAT = { 'CSV' | 'PARQUET' } ]
   [ [ , ] FIELDQUOTE = 'quote_characters' ]
   [ [ , ] FIRSTROW = first_row ]
   [ [ , ] LASTROW = last_row ]

   -- input file format options
   [ [ , ] FORMATFILE = 'format_file_path' ]
   [ [ , ] FORMATFILE_DATA_SOURCE = 'data_source_name' ]

   -- error handling options
   [ [ , ] MAXERRORS = max_errors ]
   [ [ , ] ERRORFILE = 'file_name' ]
   [ [ , ] ERRORFILE_DATA_SOURCE = 'errorfile_data_source_name' ]

    ) ]
```

::: moniker-end

## Arguments

The `BULK INSERT` statement has different arguments and options in different platforms. The differences are summarized in the following table:

| Feature | SQL Server | Azure SQL Database and Azure SQL Managed Instance | Fabric Data Warehouse |
| --- | --- |
| Data source | Local path, Network path (UNC), or Azure Storage | Azure Storage | Azure Storage, One Lake |
| Source authentication | Windows authentication, SAS | Microsoft Entra ID, SAS token, managed identity | Microsoft Entra ID |
| Unsupported options | `*` wildcards in path, `FORMAT = 'PARQUET'` | `*` wildcards in path, `FORMAT = 'PARQUET'` | `DATAFILETYPE = {'native' | 'widenative'}` |
| Enabled options but without effect | | | `KEEPIDENTITY`, `FIRE_TRIGGERS`, `CHECK_CONSTRAINTS`, `TABLOCK`, `ORDER`, `ROWS_PER_BATCH`, `KILOBYTES_PER_BATCH`, and `BATCHSIZE` aren't applicable. They don't throw a syntax error, but they don't have any effect |

#### *database_name*

The database name in which the specified table or view resides. If not specified, *database_name* is the current database.

#### *schema_name*

Specifies the name of the table or view schema. *schema_name* is optional if the default schema for the user performing the bulk-import operation is schema of the specified table or view. If *schema* isn't specified and the default schema of the user performing the bulk-import operation is different from the specified table or view, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message, and the bulk-import operation is canceled.

#### *table_name*

Specifies the name of the table or view to bulk import data into. Only views in which all columns refer to the same base table can be used. For more information about the restrictions for loading data into views, see [INSERT](insert-transact-sql.md).

#### FROM '*data_file*'

Specifies the full path of the data file that contains data to import into the specified table or view.

::: moniker range=">=sql-server-2017 || >=sql-server-linux-2017"

`BULK INSERT` can import data from a disk or Azure Storage (including network, floppy disk, hard disk, and so on).

```sql
BULK INSERT bing_covid_19_data
FROM 'C:\\bing_covid-19_data\public\curated\covid-19\latest\bing_covid-19_data.csv';
```

*data_file* must specify a valid path from the server on which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running. If *data_file* is a remote file, specify the Universal Naming Convention (UNC) name. A UNC name has the form `\\SystemName\ShareName\Path\FileName`. For example:

```sql
BULK INSERT bing_covid_19_data
FROM '\\ShareX\bing_covid-19_data\public\curated\covid-19\latest\bing_covid-19_data.csv';
```

::: moniker-end

Azure SQL Database and Fabric Data Warehouse support reading data from URI, but they don't support on-premises file paths.

```sql
BULK INSERT bing_covid_19_data
FROM 'https://<data-lake>.blob.core.windows.net/public/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv';
```

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the *data_file* can be in Azure Storage. In that case, you need to specify `data_source_name` option as well. For an example, see [Import data from a file in Azure Storage](#f-import-data-from-a-file-in-azure-storage).

::: moniker range="=fabric"

Fabric Data Warehouse supports two different path styles for specifying source path:

- `https://<storage account>.blob.core.windows.net/<container name>/<path to file>`
- `abfss://<container name>@<storage account>.dfs.core.windows.net/<path to file>`

Fabric Data Warehouse supports `*` wildcards that can match any character in the URI, and enable you to define a URI pattern for the files that should be imported. For example:

```sql
BULK INSERT bing_covid_19_data
FROM 'https://<data-lake>.blob.core.windows.net/public/curated/covid-19/bing_covid-19_data/latest/*.csv';
```

::: moniker-end

> [!NOTE]  
> Replace `<data-lake>.blob.core.windows.net` with an appropriate URL.

#### DATA_SOURCE

**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)].

Specifies a named external data source that points to an Azure Storage root location for the file import.

```sql
CREATE EXTERNAL DATA SOURCE pandemicdatalake
WITH (LOCATION = 'https://<data-lake>.blob.core.windows.net/public/');
```

> [!NOTE]  
> Replace `<data-lake>.blob.core.windows.net` with an appropriate URL.

For more information, see [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md).

The file path in the `FROM` clause must be a relative path, which will be appended to the root location defined in the external data source.

```sql
BULK INSERT bing_covid_19_data
FROM 'curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (DATA_SOURCE = 'pandemicdatalake', FIRSTROW = 2, LASTROW = 100, FIELDTERMINATOR = ',');
```

> [!NOTE]  
> For simplicity, the following examples use relative paths and predefined external data sources.

#### CODEPAGE

Specifies the code page of the data in the data file. `CODEPAGE` is relevant only if the data contains **char**, **varchar**, or **text** columns with character values greater than `127` or less than `32`. For an example, see [Specify a code page](#d-specify-a-code-page).

```sql
BULK INSERT bing_covid_19_data
FROM '/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (DATA_SOURCE = 'pandemicdatalake', FIRSTROW = 2, CODEPAGE = '65001');
```

`CODEPAGE` isn't a supported option on Linux for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]. For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], only the `'RAW'` option is allowed for `CODEPAGE`.

You should specify a collation name for each column in a [format file](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md).

::: moniker range="=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"

| `CODEPAGE` value | Description |
| --- | --- |
| `ACP` | Columns of **char**, **varchar**, or **text** data type are converted from the [!INCLUDE [vcpransi](../../includes/vcpransi-md.md)]/[!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows code page (ISO 1252) to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] code page. |
| `OEM` (default) | Columns of **char**, **varchar**, or **text** data type are converted from the system `OEM` code page to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] code page. |
| `RAW` | No conversion from one code page to another occurs. `RAW` is the fastest option. |
| *code_page* | Specific code page number, for example, 850.<br /><br />Versions before [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] don't support code page 65001 (UTF-8 encoding). |

::: moniker-end

::: moniker range="=fabric"

| `CODEPAGE` value | Description |
| --- | --- |
| `ACP` | Columns of **char**, **varchar**, or **text** data type are converted from the [!INCLUDE [vcpransi](../../includes/vcpransi-md.md)]/[!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows code page (ISO 1252) to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] code page. |
| *code_page* | Specific code page number, for example, 850.<br /><br />Versions before [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] don't support code page 65001 (UTF-8 encoding). |

::: moniker-end

#### DATAFILETYPE

Specifies that `BULK INSERT` performs the import operation using the specified data-file type value.

```sql
BULK INSERT bing_covid_19_data
FROM 'curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (DATA_SOURCE = 'pandemicdatalake', FIRSTROW = 2, DATAFILETYPE = 'char');
```

> [!NOTE]  
> Replace `<data-lake>.blob.core.windows.net` with an appropriate URL.

::: moniker range="=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"

| `DATAFILETYPE` value | All data represented in |
| --- | --- |
| `char` (default) | Character format.<br /><br />For more information, see [Use character format to import or export data](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md). |
| `widechar` | Unicode characters.<br /><br />For more information, see [Use Unicode character format to import or export data](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md). |
| `native` | Native (database) data types. Create the native data file by bulk importing data from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using the **bcp** utility.<br /><br />The native value offers a higher performance alternative to the char value. Native format is recommended when you bulk transfer data between multiple instances of SQL Server using a data file that doesn't contain any extended/double-byte character set (DBCS) characters.<br /><br />For more information, see [Use native format to import or export data](../../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md). |
| `widenative` | Native (database) data types, except in **char**, **varchar**, and **text** columns, in which data is stored as Unicode. Create the `widenative` data file by bulk importing data from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using the **bcp** utility.<br /><br />The `widenative` value offers a higher performance alternative to `widechar`. If the data file contains [!INCLUDE [vcpransi](../../includes/vcpransi-md.md)] extended characters, specify `widenative`.<br /><br />For more information, see [Use Unicode Native Format to Import or Export Data](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md). |

::: moniker-end

::: moniker range="=fabric"

| `DATAFILETYPE` value | All data represented in |
| --- | --- |
| `char` (default) | Character format.<br /><br />For more information, see [Use character format to import or export data](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md). |
| `widechar` | Unicode characters.<br /><br />For more information, see [Use Unicode character format to import or export data](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md). |

::: moniker-end

#### MAXERRORS

Specifies the maximum number of syntax errors allowed in the data before the bulk-import operation is canceled. Each row that can't be imported by the bulk-import operation is ignored and counted as one error. If *max_errors* isn't specified, the default is 10.

```sql
BULK INSERT bing_covid_19_data
FROM 'curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (DATA_SOURCE = 'pandemicdatalake', MAXERRORS = 0);
```

The `MAX_ERRORS` option doesn't apply to constraint checks or to converting **money** and **bigint** data types.

#### ERRORFILE

Specifies the file used to collect rows that have formatting errors and can't be converted to an OLE DB rowset. These rows are copied into this error file from the data file "as is."

```sql
BULK INSERT bing_covid_19_data
FROM 'curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (DATA_SOURCE = 'pandemicdatalake',
      ERRORFILE = 'https://<data-lake>.blob.core.windows.net/public/curated/covid-19/bing_covid-19_data/latest/errors');
```

> [!NOTE]  
> Replace `<data-lake>.blob.core.windows.net` with an appropriate URL.

The error file is created when the command is executed. An error occurs if the file already exists. Additionally, a control file with the extension `.ERROR.txt` is created, which references each row in the error file and provides error diagnostics. As soon as the errors have been corrected, the data can be loaded.

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the *error_file_path* can be in Azure Storage.

#### ERRORFILE_DATA_SOURCE

**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions.

Specifies a named external data source pointing to the Azure Storage location of the error file to keep track of errors found during the import.

```sql
BULK INSERT bing_covid_19_data
FROM 'curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (
    DATA_SOURCE = 'pandemicdatalake',
    ERRORFILE = 'curated/covid-19/bing_covid-19_data/latest/errors',
    ERRORFILE_DATA_SOURCE = 'pandemicdatalake'
);
```

For more details on creating external data sources, see [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md).

#### FIRSTROW

Specifies the number of the first row to load. The default is the first row in the specified data file. `FIRSTROW` is 1-based.

```sql
BULK INSERT bing_covid_19_data
FROM 'curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (DATA_SOURCE = 'pandemicdatalake', FIRSTROW = 2);
```

The `FIRSTROW` attribute isn't intended to skip column headers. The `BULK INSERT` statement doesn't support skipping headers. If you choose to skip rows, the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] looks only at the field terminators, and doesn't validate the data in the fields of skipped rows.

#### LASTROW

Specifies the number of the last row to load. The default is 0, which indicates the last row in the specified data file.

::: moniker range="=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"

#### BATCHSIZE

Specifies the number of rows in a batch. Each batch is copied to the server as one transaction. If this fails, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] commits or rolls back the transaction for every batch. By default, all data in the specified data file is one batch. For information about performance considerations, see [Performance considerations](#performance-considerations) later in this article.

#### CHECK_CONSTRAINTS

Specifies that all constraints on the target table or view must be checked during the bulk-import operation. Without the `CHECK_CONSTRAINTS` option, any `CHECK` and `FOREIGN KEY` constraints are ignored, and after the operation, the constraint on the table is marked as not-trusted.

`UNIQUE` and `PRIMARY KEY` constraints are always enforced. When importing into a character column that is defined with a `NOT NULL` constraint, `BULK INSERT` inserts a blank string when there's no value in the text file.

At some point, you must examine the constraints on the whole table. If the table was non-empty before the bulk-import operation, the cost of revalidating the constraint might exceed the cost of applying `CHECK` constraints to the incremental data.

A situation in which you might want constraints disabled (the default behavior) is if the input data contains rows that violate constraints. With `CHECK` constraints disabled, you can import the data and then use [!INCLUDE [tsql](../../includes/tsql-md.md)] statements to remove the invalid data.

> [!NOTE]  
> The `MAXERRORS` option doesn't apply to constraint checking.

#### FIRE_TRIGGERS

Specifies that any insert triggers defined on the destination table execute during the bulk-import operation. If triggers are defined for `INSERT` operations on the target table, they're fired for every completed batch.

If `FIRE_TRIGGERS` isn't specified, no insert triggers execute.

#### KEEPIDENTITY

Specifies that identity value or values in the imported data file are to be used for the identity column. If `KEEPIDENTITY` isn't specified, the identity values for this column are verified but not imported and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically assigns unique values based on the seed and increment values specified during table creation. If the data file doesn't contain values for the identity column in the table or view, use a format file to specify that the identity column in the table or view is to be skipped when importing data; [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically assigns unique values for the column. For more information, see [DBCC CHECKIDENT](../database-console-commands/dbcc-checkident-transact-sql.md).

For more information, see about keeping identify values see [Keep identity values when bulk importing data](../../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md).

#### KEEPNULLS

Specifies that empty columns should retain a null value during the bulk-import operation, instead of having any default values for the columns inserted. For more information, see [Keep nulls or default values during bulk import](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md).

#### KILOBYTES_PER_BATCH

Specifies the approximate number of kilobytes (KB) of data per batch as *kilobytes_per_batch*. By default, `KILOBYTES_PER_BATCH` is unknown. For information about performance considerations, see [Performance considerations](#performance-considerations) later in this article.

#### ORDER

Specifies how the data in the data file is sorted. Bulk import performance is improved if the data being imported is sorted according to the clustered index on the table, if any. If the data file is sorted in an order other than the order of a clustered index key, or if there's no clustered index on the table, the `ORDER` clause is ignored. The column names supplied must be valid column names in the destination table. By default, the bulk insert operation assumes the data file is unordered. For optimized bulk import, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] also validates that the imported data is sorted.

*n* is a placeholder that indicates that multiple columns can be specified.

#### ROWS_PER_BATCH

Indicates the approximate number of rows of data in the data file.

By default, all the data in the data file is sent to the server as a single transaction, and the number of rows in the batch is unknown to the query optimizer. If you specify `ROWS_PER_BATCH` (with a value > 0), the server uses this value to optimize the bulk-import operation. The value specified for `ROWS_PER_BATCH` should be approximately the same as the actual number of rows. For information about performance considerations, see [Performance considerations](#performance-considerations) later in this article.

#### TABLOCK

Specifies that a table-level lock is acquired for the duration of the bulk-import operation. A table can be loaded concurrently by multiple clients if the table has no indexes and `TABLOCK` is specified. By default, locking behavior is determined by the table option **table lock on bulk load**. Holding a lock for the duration of the bulk-import operation reduces lock contention on the table, in some cases can significantly improve performance. For information about performance considerations, see [Performance considerations](#performance-considerations) later in this article.

For a columnstore index, the locking behavior is different because it's internally divided into multiple rowsets. Each thread loads data exclusively into each rowset by taking an exclusive (X) lock on the rowset allowing parallel data load with concurrent data load sessions. The use of `TABLOCK` option causes the thread to take an exclusive lock on the table (unlike the bulk update (BU) lock for traditional rowsets) which prevents other concurrent threads from loading data concurrently.

::: moniker-end

### Input file format options

#### FORMAT

**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions.

Specifies a comma-separated values file compliant to the [RFC 4180](https://tools.ietf.org/html/rfc4180) standard.

```sql
BULK INSERT bing_covid_19_data
FROM 'curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (DATA_SOURCE = 'pandemicdatalake', FORMAT = 'CSV');
```

::: moniker range="=fabric"

In Fabric Data Warehouse, the `BULK INSERT` statement supports the same formats as the `COPY INTO` statement, so `FORMAT = 'PARQUET'` is also supported.

::: moniker-end

#### FIELDQUOTE

**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions.

Specifies a character to use as the quote character in the CSV file. If not specified, the quote character (`"`) is used as the quote character, as defined in the [RFC 4180](https://tools.ietf.org/html/rfc4180) standard.

#### FORMATFILE

Specifies the full path of a format file. A format file describes the data file that contains stored responses created by using the **bcp** utility on the same table or view.

```sql
BULK INSERT bing_covid_19_data
FROM 'curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (DATA_SOURCE = 'pandemicdatalake',
      FORMATFILE = 'https://<data-lake>.blob.core.windows.net/public/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.fmt');
```

> [!NOTE]  
> Replace `<data-lake>.blob.core.windows.net` with an appropriate URL.

The format file should be used if:

- The data file contains greater or fewer columns than the table or view.
- The columns are in a different order.
- The column delimiters vary.
   - Varying column delimiters are not supported in Fabric Data Warehouse. In Fabric Data Warehouse, the first delimiter defined in the format file is applied to all columns.
- There are other changes in the data format. Format files are typically created by using the **bcp** utility and modified with a text editor as needed. For more information, see [bcp Utility](../../tools/bcp-utility.md) and [Create a format file with bcp](../../relational-databases/import-export/create-a-format-file-sql-server.md).

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], and in Azure SQL Database, `format_file_path` can be in Azure Storage.

#### FORMATFILE_DATA_SOURCE

**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions.

Specifies a named external data source pointing to the Azure Storage location of the format file to define the schema of imported data.

```sql
BULK INSERT bing_covid_19_data
FROM 'curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (
    DATA_SOURCE = 'pandemicdatalake',
    FORMATFILE = 'curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.fmt',
    FORMATFILE_DATA_SOURCE = 'pandemicdatalake'
);
```

#### FIELDTERMINATOR

Specifies the field terminator to be used for `char` and `widechar` data files. The default field terminator is `\t` (tab character). For more information, see [Specify field and row terminators](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md).

```sql
BULK INSERT bing_covid_19_data
FROM '/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (DATA_SOURCE = 'pandemicdatalake', FIELDTERMINATOR = ',', FIRSTROW = 2);
```

#### ROWTERMINATOR

Specifies the row terminator to be used for `char` and `widechar` data files.

```sql
BULK INSERT bing_covid_19_data
FROM '/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
WITH (DATA_SOURCE = 'pandemicdatalake', ROWTERMINATOR = '\r\n', FIRSTROW = 2);
```

The default row terminator is `\r\n` (carriage return and newline character). For more information, see [Specify field and row terminators](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md).

## Compatibility

`BULK INSERT` enforces strict data validation and data checks of data read from a file that could cause existing scripts to fail when they're executed on invalid data. For example, `BULK INSERT` verifies that:

- The native representations of **float** or **real** data types are valid.
- Unicode data has an even-byte length.

## Data types

### String-to-decimal data type conversions

The string-to-decimal data type conversions used in `BULK INSERT` follow the same rules as the [!INCLUDE [tsql](../../includes/tsql-md.md)] [CONVERT](../functions/cast-and-convert-transact-sql.md) function, which rejects strings representing numeric values that use scientific notation. Therefore, `BULK INSERT` treats such strings as invalid values and reports conversion errors.

To work around this behavior, use a format file to bulk import scientific notation **float** data into a decimal column. In the format file, explicitly describe the column as **real** or **float** data. For more information about these data types, see [float and real](../data-types/float-and-real-transact-sql.md).

Format files represent **real** data as the **SQLFLT4** data type and **float** data as the **SQLFLT8** data type. For information about non-XML format files, see [Specify file storage type using bcp](../../relational-databases/import-export/specify-file-storage-type-by-using-bcp-sql-server.md).

#### Example of importing a numeric value that uses scientific notation

This example uses the following table in the `bulktest` database:

```sql
CREATE TABLE dbo.t_float
(
    c1 FLOAT,
    c2 DECIMAL (5, 4)
);
```

The user wants to bulk import data into the `t_float` table. The data file, `C:\t_float-c.dat`, contains scientific notation **float** data; for example:

```input
8.0000000000000002E-2 8.0000000000000002E-2
```

When copying this sample, be aware of different text editors and encodings that save tabs characters (\t) as spaces. A tab character is expected later in this sample.

However, `BULK INSERT` can't import this data directly into `t_float`, because its second column, `c2`, uses the **decimal** data type. Therefore, a format file is necessary. The format file must map the scientific notation **float** data to the decimal format of column `c2`.

The following format file uses the **SQLFLT8** data type to map the second data field to the second column:

```xml
<?xml version="1.0"?>
<BCPFORMAT xmlns="http://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <RECORD>
    <FIELD ID="1" xsi:type="CharTerm" TERMINATOR="\t" MAX_LENGTH="30" />
    <FIELD ID="2" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="30" />
  </RECORD>
  <ROW>
    <COLUMN SOURCE="1" NAME="c1" xsi:type="SQLFLT8" />
    <COLUMN SOURCE="2" NAME="c2" xsi:type="SQLFLT8" />
  </ROW>
</BCPFORMAT>
```

To use this format file (using the file name `C:\t_floatformat-c-xml.xml`) to import the test data into the test table, issue the following [!INCLUDE [tsql](../../includes/tsql-md.md)] statement:

```sql
BULK INSERT bulktest.dbo.t_float
FROM 'C:\t_float-c.dat'
WITH (FORMATFILE = 'C:\t_floatformat-c-xml.xml');
```

> [!IMPORTANT]  
> Azure SQL Database and Fabric Data Warehouse support only reading from URI (for example, Azure Storage).

### Data types for bulk exporting or importing SQLXML documents

To bulk export or import SQLXML data, use one of the following data types in your format file:

| Data type | Effect |
| --- | --- |
| **SQLCHAR** or **SQLVARCHAR** | The data is sent in the client code page or in the code page implied by the collation). The effect is the same as specifying the `DATAFILETYPE = 'char'` without specifying a format file. |
| **SQLNCHAR** or **SQLNVARCHAR** | The data is sent as Unicode. The effect is the same as specifying the `DATAFILETYPE = 'widechar'` without specifying a format file. |
| **SQLBINARY** or **SQLVARBIN** | The data is sent without any conversion. |

## Remarks

For a comparison of the `BULK INSERT` statement, the `INSERT ... SELECT * FROM OPENROWSET(BULK...)` statement, and the `bcp` command, see [Bulk Import and Export of Data](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md).

For information about preparing data for bulk import, see [Prepare data for bulk export or import](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md).

The `BULK INSERT` statement can be executed within a user-defined transaction to import data into a table or view. Optionally, to use multiple matches for bulk importing data, a transaction can specify the `BATCHSIZE` clause in the `BULK INSERT` statement. If a multiple-batch transaction is rolled back, every batch that the transaction sent to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is rolled back.

## Interoperability

### Import data from a CSV file

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], `BULK INSERT` supports the CSV format, as does Azure SQL Database.

Before [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], comma-separated value (CSV) files aren't supported by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] bulk-import operations. However, in some cases, a CSV file can be used as the data file for a bulk import of data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For information about the requirements for importing data from a CSV data file, see [Prepare data for bulk export or import](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md).

## Log behavior

For information about when row-insert operations that are performed by bulk import into SQL Server are logged in the transaction log, see [Prerequisites for minimal logging in bulk import](../../relational-databases/import-export/prerequisites-for-minimal-logging-in-bulk-import.md). Minimal logging isn't supported in Azure SQL Database.

## Limitations

When using a format file with `BULK INSERT`, you can specify up to 1,024 fields only. This is same as the maximum number of columns allowed in a table. If you use a format file with `BULK INSERT` with a data file that contains more than 1,024 fields, `BULK INSERT` generates the 4822 error. The [bcp utility](../../tools/bcp-utility.md) doesn't have this limitation, so for data files that contain more than 1,024 fields, use `BULK INSERT` without a format file or use the **bcp** command.

## Performance considerations

If the number of pages to be flushed in a single batch exceeds an internal threshold, a full scan of the buffer pool might occur to identify which pages to flush when the batch commits. This full scan can hurt bulk-import performance. A likely case of exceeding the internal threshold occurs when a large buffer pool is combined with a slow I/O subsystem. To avoid buffer overflows on large machines, either don't use the `TABLOCK` hint (which removes the bulk optimizations) or use a smaller batch size (which preserves the bulk optimizations).

You should test various batch sizes with your data load to find out what works best for you. Keep in mind that the batch size has partial rollback implications. If your process fails and before you use `BULK INSERT` again, you might have to do additional manual work to remove a part of the rows that were inserted successfully, before a failure occurred.

With Azure SQL Database, consider temporarily increasing the performance level of the database or instance before the import if you're importing a large volume of data.

## Security

### Security account delegation (impersonation)

If a user uses a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, the security profile of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] process account is used. A login using SQL Server authentication can't be authenticated outside of the Database Engine. Therefore, when a `BULK INSERT` command is initiated by a login using SQL Server authentication, the connection to the data is made using the security context of the SQL Server process account (the account used by the SQL Server Database Engine service).

To successfully read the source data, you must grant the account used by the SQL Server Database Engine, access to the source data. In contrast, if a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] user logs on by using Windows Authentication, the user can read only those files that can be accessed by the user account, regardless of the security profile of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] process.

When executing the `BULK INSERT` statement by using **sqlcmd** or **osql**, from one computer, inserting data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a second computer, and specifying a *data_file* on third computer by using a UNC path, you might receive a 4861 error.

To resolve this error, use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication and specify a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login that uses the security profile of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] process account, or configure Windows to enable security account delegation. For information about how to enable a user account to be trusted for delegation, see Windows Help.

For more information about this and other security considerations for using `BULK INSERT`, see [Use BULK INSERT or OPENROWSET(BULK...) to import data to SQL Server](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).

When importing from Azure Storage and the data isn't public (anonymous access), create a [DATABASE SCOPED CREDENTIAL](create-database-scoped-credential-transact-sql.md) based on a SAS key encrypted with a [database master key](create-master-key-transact-sql.md) (DMK), and then create an [external database source](create-external-data-source-transact-sql.md) for use in your `BULK INSERT` command.

Alternatively, create a [DATABASE SCOPED CREDENTIAL](create-database-scoped-credential-transact-sql.md) based on `MANAGED IDENTITY` to authorize requests for data access in non-public storage accounts. When using `MANAGED IDENTITY`, Azure Storage must grant permissions to the managed identity of the instance by adding the **Storage Blob Data Contributor** built-in Azure role-based access control (RBAC) role that provides read/write access to the managed identity for the necessary Azure Storage containers. Azure SQL Managed Instance have a system assigned managed identity, and can also have one or more user-assigned managed identities. You can use either system-assigned managed identities or user-assigned managed identities to authorize the requests. For authorization, the `default` identity of the managed instance would be used (that is, the primary user-assigned managed identity, or system-assigned managed identity if user-assigned managed identity isn't specified). For an example, see [Import data from a file in Azure Storage](#f-import-data-from-a-file-in-azure-storage).

> [!IMPORTANT]  
> Managed Identity applies to Azure SQL, and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

### Permissions

The following permissions apply to the location where the data is being bulk-imported (the target).

Requires `INSERT` and `ADMINISTER BULK OPERATIONS` permissions. In Azure SQL Database, `INSERT` and `ADMINISTER DATABASE BULK OPERATIONS` permissions are required.

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Cumulative Update 24 (CU24) and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Cumulative Update 3 (CU3), [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux supports the `ADMINISTER BULK OPERATIONS` permission and the **bulkadmin** role. In earlier versions, only the **sysadmin** role can perform bulk inserts for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux. For more information, see [Configure bulk import operations for SQL Server on Linux](../../linux/sql-server-linux-bulk-operations.md).

Additionally, `ALTER TABLE` permission is required if one or more of the following conditions is true:

- Constraints exist and the `CHECK_CONSTRAINTS` option isn't specified.

  Disabling constraints is the default behavior. To check constraints explicitly, use the `CHECK_CONSTRAINTS` option.

- Triggers exist and the `FIRE_TRIGGER` option isn't specified.

  By default, triggers aren't fired. To fire triggers explicitly, use the `FIRE_TRIGGER` option.

- You use the `KEEPIDENTITY` option to import identity value from data file.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

> [!IMPORTANT]  
> Azure SQL Database and Fabric Warehouse support only reading from Azure Storage.

### A. Use pipes to import data from a file

The following example imports order detail information into the `AdventureWorks2022.Sales.SalesOrderDetail` table from the specified data file by using a pipe (`|`) as the field terminator and `|\n` as the row terminator.

```sql
BULK INSERT AdventureWorks2022.Sales.SalesOrderDetail
FROM 'f:\orders\lineitem.tbl'
WITH (FIELDTERMINATOR = ' |', ROWTERMINATOR = ' |\n');
```

### B. Use the FIRE_TRIGGERS argument

The following example specifies the `FIRE_TRIGGERS` argument.

```sql
BULK INSERT AdventureWorks2022.Sales.SalesOrderDetail
FROM 'f:\orders\lineitem.tbl'
WITH (FIELDTERMINATOR = ' |', ROWTERMINATOR = ':\n', FIRE_TRIGGERS);
```

### C. Use line feed as a row terminator

The following example imports a file that uses the line feed as a row terminator such as a UNIX output:

```sql
DECLARE @bulk_cmd AS VARCHAR (1000);

SET @bulk_cmd = 'BULK INSERT AdventureWorks2022.Sales.SalesOrderDetail
FROM ''<drive>:\<path>\<filename>''
WITH (ROWTERMINATOR = ''' + CHAR(10) + ''')';

EXECUTE (@bulk_cmd);
```

> [!NOTE]  
> On Windows, `\n` is automatically replaced with `\r\n`.

### D. Specify a code page

The following example shows how to specify a code page.

```sql
BULK INSERT MyTable
FROM 'D:\data.csv'
WITH (CODEPAGE = '65001', DATAFILETYPE = 'char', FIELDTERMINATOR = ',');
```

### E. Import data from a CSV file

The following example shows how to specify a CSV file, skipping the header (first row), using `;` as field terminator and `0x0a` as line terminator:

```sql
BULK INSERT Sales.Invoices
FROM '\\share\invoices\inv-2016-07-25.csv'
WITH (
    FORMAT = 'CSV',
    FIRSTROW = 2,
    FIELDQUOTE = '\',
    FIELDTERMINATOR = ';',
    ROWTERMINATOR = '0x0a'
);
```

The following example shows how to specify a CSV file in UTF-8 format (using a `CODEPAGE` of `65001`), skipping the header (first row), using `;` as field terminator and `0x0a` as line terminator:

```sql
BULK INSERT Sales.Invoices
FROM '\\share\invoices\inv-2016-07-25.csv'
WITH (
    CODEPAGE = '65001',
    FORMAT = 'CSV',
    FIRSTROW = 2,
    FIELDQUOTE = '\',
    FIELDTERMINATOR = ';',
    ROWTERMINATOR = '0x0a'
);
```

<a id="f-import-data-from-a-file-in-azure-blob-storage"></a>

### F. Import data from a file in Azure Storage

#### Load data from a CSV in Azure Storage with SAS token

The following example shows how to load data from a CSV file in an Azure Storage location on which you've created a Shared Access Signature (SAS). The Azure Storage location is configured as an external data source, which requires a database scoped credential using a SAS key that's encrypted using a DMK in the user database.

> [!NOTE]  
> Make sure that you don't have a leading `?` in the SAS token, and that you have at least read permission on the object that should be loaded `srt=o&sp=r`, and that expiration period is valid (all dates are in UTC time).

(Optional) A DMK isn't required if a `DATABASE SCOPED CREDENTIAL` isn't required, because the blob is configured for public (anonymous) access.

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
```

(Optional) A `DATABASE SCOPED CREDENTIAL` isn't required because the blob is configured for public (anonymous) access.

Don't include a leading `?` in the SAS token. Make sure that you have at least read permission on the object that should be loaded (`srt=o&sp=r`), and that the expiration period is valid (all dates are in UTC time).

```sql
CREATE DATABASE SCOPED CREDENTIAL MyAzureBlobStorageCredential
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
     SECRET = '******srt = sco&sp = rwac&se = 2017-02-01T00:55:34Z&st = 2016-12-29T16:55:34Z***************';
```

> [!NOTE]  
> `CREDENTIAL` isn't required if a blob is configured for public (anonymous) access.

```sql
CREATE EXTERNAL DATA SOURCE MyAzureBlobStorage
WITH (
    TYPE = BLOB_STORAGE,
    LOCATION = 'https://****************.blob.core.windows.net/invoices',
    CREDENTIAL = MyAzureBlobStorageCredential
);

BULK INSERT Sales.Invoices
FROM 'inv-2017-12-08.csv'
WITH (DATA_SOURCE = 'MyAzureBlobStorage');
```

#### Load data from a CSV in Azure Storage with a managed identity

The following example shows how to use the `BULK INSERT` command to load data from a CSV file in an Azure Storage location using Managed Identity. The Azure Storage location is configured as an external data source.

(Optional) A DMK isn't required if a `DATABASE SCOPED CREDENTIAL` isn't required, because the blob is configured for public (anonymous) access.

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
```

(Optional) A `DATABASE SCOPED CREDENTIAL` isn't required, because the blob is configured for public (anonymous) access:

```sql
CREATE DATABASE SCOPED CREDENTIAL MyAzureBlobStorageCredential
WITH IDENTITY = 'Managed Identity';
```

Grant the Storage Blob Data Contributor role, to provide read/write access to the managed identity for the necessary Azure Storage containers.

> [!NOTE]  
> `CREDENTIAL` isn't required if a blob is configured for public (anonymous) access.

```sql
CREATE EXTERNAL DATA SOURCE MyAzureBlobStorage
WITH (
    TYPE = BLOB_STORAGE,
    LOCATION = 'https://****************.blob.core.windows.net/invoices',
    CREDENTIAL = MyAzureBlobStorageCredential
);

BULK INSERT Sales.Invoices
FROM 'inv-2017-12-08.csv'
WITH (DATA_SOURCE = 'MyAzureBlobStorage');
```

> [!IMPORTANT]  
> Managed identity applies to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, and Azure SQL.

### G. Import data from a file in Azure Storage and specify an error file

The following example shows how to load data from a CSV file in an Azure Storage location, which is configured as an external data source, and also specifying an error file. You need a database scoped credential using a shared access signature. If running on Azure SQL Database, `ERRORFILE` option should be accompanied by `ERRORFILE_DATA_SOURCE`, otherwise the import might fail with permissions error. The file specified in `ERRORFILE` shouldn't exist in the container.

```sql
BULK INSERT Sales.Invoices
FROM 'inv-2017-12-08.csv'
WITH (
    DATA_SOURCE = 'MyAzureInvoices',
    FORMAT = 'CSV',
    ERRORFILE = 'MyErrorFile',
    ERRORFILE_DATA_SOURCE = 'MyAzureInvoices'
);
```

For complete `BULK INSERT` examples including configuring the credential and external data source, see [Examples of bulk access to data in Azure Storage](../../relational-databases/import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md).

### More examples

Other `BULK INSERT` examples are provided in the following articles:

- [Examples of bulk import and export of XML documents](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
- [Keep identity values when bulk importing data](../../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md)
- [Keep nulls or default values during bulk import](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)
- [Specify field and row terminators](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md)
- [Use a format file to bulk import data](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)
- [Use character format to import or export data](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)
- [Use native format to import or export data](../../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md)
- [Use Unicode character format to import or export data](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md)
- [Use Unicode Native Format to Import or Export Data](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md)
- [Use a format file to skip a table column](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)
- [Use a format file to map table columns to data-file fields](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)

## Related content

- [Bulk import and export of data (SQL Server)](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md)
- [bcp utility](../../tools/bcp/bcp-utility.md)
- [Format files to import or export data (SQL Server)](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md)
- [INSERT (Transact-SQL)](insert-transact-sql.md)
- [OPENROWSET (Transact-SQL)](../functions/openrowset-transact-sql.md)
- [Prepare data for bulk export or import](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md)
- [sys.sp_tableoption (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md)
