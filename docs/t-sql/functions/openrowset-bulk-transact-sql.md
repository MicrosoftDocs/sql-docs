---
title: "OPENROWSET BULK (Transact-SQL)"
description: "OPENROWSET BULK function reads data from an external data source."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest, hudequei, wiassaf, jovanpop, fresantos
ms.date: 05/11/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017 || =azuresqldb-current || >=sql-server-linux-2017 || =fabric || =fabric-sqldb"
---
# OPENROWSET BULK (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabric SE DW](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]

The `OPENROWSET` function reads data from one or many files and returns the content as a rowset. Depending on a service, the file might be stored in Azure Blob Storage, Azure Data Lake storage, on-premises disk, network shares, etc. You can read various file formats such as text/CSV, Parquet, or JSON-lines.

The `OPENROWSET` function can be referenced in the `FROM` clause of a query as if it were a table name. It can be used to read data in `SELECT` statement, or to update target data in the `UPDATE`, `INSERT`, `DELETE`, `MERGE`, `CTAS`, or `CETAS` statements.

- `OPENROWSET(BULK)` is designed for reading data from external data files. 
- `OPENROWSET` without `BULK` is designed for reading from another database engine. For more information, see [OPENROWSET (Transact-SQL)](openrowset-transact-sql.md).

This article and the argument set in the `OPENROWSET(BULK)` varies between the platforms.

- For Microsoft Fabric syntax, [select Fabric in the version dropdown list](openrowset-bulk-transact-sql.md?view=fabric&preserve-view=true).
- For SQL Server, Azure SQL Database, and Azure SQL Managed Instance syntax, [select your platform in the version dropdown list](openrowset-bulk-transact-sql.md?view=sql-server-ver17&preserve-view=true).

Details and links to similar examples on other platforms:

- For more information on `OPENROWSET` in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], see [Data virtualization with Azure SQL Database](/azure/azure-sql/database/data-virtualization-overview?view=azuresql-db&preserve-view=true).
- For more information on `OPENROWSET` in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], see [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview?view=azuresql-mi&preserve-view=true#query-data-sources-using-openrowset).
- For information and examples with serverless SQL pools in Azure Synapse, see [How to use OPENROWSET using serverless SQL pool in Azure Synapse Analytics](/azure/synapse-analytics/sql/develop-openrowset).
- Dedicated SQL pools in Azure Synapse don't support the `OPENROWSET` function.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

::: moniker range="=azuresqldb-mi-current || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =fabric-sqldb"

## Syntax 

For SQL Server, Azure SQL Database, SQL database in Fabric, and Azure SQL Managed Instance:

```syntaxsql
OPENROWSET( BULK 'data_file_path',
            <bulk_option> ( , <bulk_option> )*
)
[
    WITH (  ( <column_name> <sql_datatype> [ '<column_path>' | <column_ordinal> ] )+ )
]

<bulk_option> ::=
   DATA_SOURCE = 'data_source_name' |

   -- file format options
   CODEPAGE = { 'ACP' | 'OEM' | 'RAW' | 'code_page' } |
   DATAFILETYPE = { 'char' | 'widechar' } |
   FORMAT = <file_format> |

   FORMATFILE = 'format_file_path' |
   FORMATFILE_DATA_SOURCE = 'data_source_name' |

   SINGLE_BLOB |
   SINGLE_CLOB |
   SINGLE_NCLOB |

   -- Text/CSV options
   ROWTERMINATOR = 'row_terminator' |
   FIELDTERMINATOR =  'field_terminator' |
   FIELDQUOTE = 'quote_character' |

   -- Error handling options
   MAXERRORS = maximum_errors |
   ERRORFILE = 'file_name' |
   ERRORFILE_DATA_SOURCE = 'data_source_name' |

   -- Execution options
   FIRSTROW = first_row |
   LASTROW = last_row |

   ORDER ( { column [ ASC | DESC ] } [ , ...n ] ) [ UNIQUE ] ] |

   ROWS_PER_BATCH = rows_per_batch
```
::: moniker-end

::: moniker range="=fabric"

### Syntax for Fabric Data Warehouse

```syntaxsql
OPENROWSET( BULK 'data_file_path',
            <bulk_option> ( , <bulk_option> )*
)
[
    WITH (  ( <column_name> <sql_datatype> [ '<column_path>' | <column_ordinal> ] )+ )
]

<bulk_option> ::=
   DATA_SOURCE = 'data_source_name' |

   -- file format options
   CODEPAGE = { 'ACP' | 'OEM' | 'RAW' | 'code_page' } |
   DATAFILETYPE = { 'char' | 'widechar' } |
   FORMAT = <file_format> |

   -- Text/CSV options
   ROWTERMINATOR = 'row_terminator' |
   FIELDTERMINATOR =  'field_terminator' |
   FIELDQUOTE = 'quote_character' |
   ESCAPECHAR = 'escape_char' |
   HEADER_ROW = [true|false] |
   PARSER_VERSION = 'parser_version' |

   -- Error handling options
   MAXERRORS = maximum_errors |
   ERRORFILE = 'file_name' |

   -- Execution options
   FIRSTROW = first_row |
   LASTROW = last_row |

   ROWS_PER_BATCH = rows_per_batch
```

::: moniker-end

Some `OPENROWSET` options are format‑specific, while others are universal. For example, row and field delimiters are meaningful only for delimited text (CSV/TSV), whereas options like DATA_SOURCE and MAXERRORS apply to all formats. The table below summarizes which options are supported for the most common formats.

| Options | CSV(1.0) | CSV(2.0) | PARQUET | JSONL |
|---|---|---|---|---|
| DATA_SOURCE, ROWS_PER_BATCH, MAXERRORS | Supported | Supported | Supported | Supported |
| ERRORFILE, ERRORFILE_DATA_SOURCE, FORMATFILE, FORMATFILE_DATA_SOURCE | Supported | Supported | Not supported | Supported |
| CODEPAGE, DATAFILETYPE  | Supported | Supported | Not supported | Supported | 
| FIRSTROW  | Supported | Supported | Not supported | Supported |
| ROWTERMINATOR, FIELDTERMINATOR, FIELDQUOTE, ESCAPECHAR  | Supported | Supported | Not supported | Not supported | 
| PARSER_VERSION | Supported | Supported | Not supported | Not supported |
| LASTROW  | Supported | Not supported | Not supported | Not supported |
| HEADER_ROW | Not supported | Supported | Not supported | Not supported |     
| SINGLE_BLOB, SINGLE_CLOB, SINGLE_NCLOB | Not supported | Not supported | Not supported | Not supported |

## Arguments

The arguments of the `BULK` option allow for significant control over where to start and end reading data, how to deal with errors, and how data is interpreted. For example, you can specify that the data file is read as a single-row, single-column rowset of type **varbinary**, **varchar**, or **nvarchar**. The default behavior is described in the argument descriptions that follow.

For information about how to use the `BULK` option, see the [Remarks](#remarks) section later in this article. For information about the permissions that the `BULK` option requires, see the [Permissions](#permissions) section, later in this article.

For information on preparing data for bulk import, see [Prepare data for bulk export or import](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md).

<a id="bulk-data_file_path"></a>

### BULK

The path or URI of the data file(s) whose data is to be read and returned as row set.

The URI can reference Azure Data Lake storage or Azure Blob storage. The URI of the data file(s) whose data is to be read and returned as row set. 

The supported path formats are:

::: moniker range="=azuresqldb-mi-current || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017"

- `<drive letter>:\<file path>` to access files on local disk
- `\\<network-share\<file path>` to access files on network shares
- `adls://<container>@<storage>.dfs.core.windows.net/<file path>` to access Azure Data Lake Storage
- `abs://<storage>.blob.core.windows.net/<container>/<file path>` to access Azure Blob Storage
- `s3://<ip-address>:<port>/<file path>` to access s3-compatible storage

> [!NOTE]
> This article and the supported URI patterns differ on different platforms. For the URI patterns that are available in Microsoft Fabric Data Warehouse, [select Fabric in the version dropdown list](openrowset-bulk-transact-sql.md?view=fabric&preserve-view=true#bulk-data_file_path).

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the *data_file* can be in Azure Blob Storage. For examples, see [Examples of bulk access to data in Azure Blob Storage](../../relational-databases/import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md).

::: moniker-end

::: moniker range="=fabric"

- `https://<storage>.blob.core.windows.net/<container>/<file path>` to access Azure Blob Storage or Azure Data Lake Storage
- `https://<storage>.dfs.core.windows.net/<container>/<file path>` to access Azure Data Lake Storage
- `abfss://<container>@<storage>.dfs.core.windows.net/<file path>` to access Azure Data Lake Storage
- `https://onelake.dfs.fabric.microsoft.com/<workspaceId>/<lakehouseId>/Files/<file path>` - to access OneLake in Microsoft Fabric

When accessing data stored in Azure Data Lake Storage Gen2, use the `abfss://<container>@<storage>.dfs.core.windows.net/<file path>` or `https://<storage>.dfs.core.windows.net/<container>/<file path>` URI formats instead of the blob endpoint. Both provide full support for the hierarchical namespace (HNS), which enables directory semantics, optimized file operations, and POSIX-style access control lists (ACLs). 

In contrast, the `blob` endpoint does not expose hierarchical namespace features and treats all paths as flat object keys. This can lead to reduced performance, limited directory behavior, and incompatibility with engines that expect Azure Data Lake Storage Gen2 filesystem semantics.

> [!NOTE]
> This article and the supported URI patterns differ on different platforms. For the URI patterns that are available in SQL Server, Azure SQL Database, and Azure SQL Managed Instance, [select the product in the version dropdown list](openrowset-bulk-transact-sql.md?view=sql-server-ver17&preserve-view=true#bulk-data_file_path).

::: moniker-end

The URI can include the `*` character to match any sequence of characters, allowing `OPENROWSET` to pattern-match against the URI. Additionally, it can end with `/**` to enable recursive traversal through all subfolders. In SQL Server, this behavior is available beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

For example:

```sql
SELECT TOP 10 *
FROM OPENROWSET(
    BULK '<scheme:>//pandemicdatalake.blob.core.windows.net/public/curated/covid-19/bing_covid-19_data/latest/*.parquet'
);
```

The storage types that can be referenced by the URI are shown in the following table:

| Version | On-premises | Azure storage | OneLake in Fabric | S3 | Google Cloud (GCS) |
|---|---|---|---|---|---|
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] | Yes | Yes | No | No | No |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | Yes | Yes | No | Yes | No |
| [!INCLUDE [azsql-md](../../includes/ssazure-sqldb.md)] | No | Yes | No | No | No |
| [!INCLUDE [mi-md](../../includes/ssazuremi-md.md)] | No | Yes | No | No | No |
| [!INCLUDE [ssod-md](../../includes/sssod-md.md)] in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]| No | Yes | Yes | No | No |
| [!INCLUDE [fabric-md](../../includes/fabric.md)] [!INCLUDE [dw-md](../../includes/fabric-dw.md)] and [!INCLUDE [se-md](../../includes/fabric-se.md)] | No | Yes | Yes | Yes, using [OneLake in Fabric shortcuts](/fabric/onelake/onelake-shortcuts) | Yes, using [OneLake in Fabric shortcuts](/fabric/onelake/onelake-shortcuts) |
| [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] | No | Yes, using [OneLake in Fabric shortcuts](/fabric/onelake/onelake-shortcuts) | Yes | Yes, using [OneLake in Fabric shortcuts](/fabric/onelake/onelake-shortcuts) | Yes, using [OneLake in Fabric shortcuts](/fabric/onelake/onelake-shortcuts) |

::: moniker range="=fabric"

You can use `OPENROWSET(BULK)` to read data directly from files stored in the OneLake in Microsoft Fabric, specifically from the **Files folder** of a Fabric Lakehouse. This eliminates the need for external staging accounts (such as ADLS Gen2 or Blob Storage) and enables workspace-governed, SaaS-native ingestion using Fabric permissions. This functionality supports:

- Reading from `Files` folders in Lakehouses
- Workspace-to-warehouse loads within the same tenant
- Native identity enforcement using Microsoft Entra ID

See the [limitations](../statements/copy-into-transact-sql.md?view=fabric&preserve-view=true#limitations-for-onelake-as-source) that are applicable both to `COPY INTO` and `OPENROWSET(BULK)`.

::: moniker-end

#### DATA_SOURCE 

`DATA_SOURCE` defines the root location of the data file path. It enables you use relative paths in BULK path. The data source is created with [CREATE EXTERNAL DATA SOURCE](../statements/create-external-data-source-transact-sql.md?view=sql-server-ver17&preserve-view=true).

In addition to the root location, it can define custom credential that can be used to access the files on that location.

For example:

```sql
CREATE EXTERNAL DATA SOURCE root
WITH (LOCATION = '<scheme:>//pandemicdatalake.blob.core.windows.net/public')
GO
SELECT *
FROM OPENROWSET(
    BULK '/curated/covid-19/bing_covid-19_data/latest/*.parquet',
    DATA_SOURCE = 'root'
);
```

### File format options

#### CODEPAGE

Specifies the code page of the data in the data file. `CODEPAGE` is relevant only if the data contains **char**, **varchar**, or **text** columns with character values more than 127 or less than 32. The valid values are 'ACP', 'OEM', 'RAW' or '*code_page*':

| CODEPAGE value | Description |
| --- | --- |
| `ACP` | Converts columns of **char**, **varchar**, or **text** data type from the ANSI/[!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows code page (ISO 1252) to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] code page. |
| `OEM` (default) | Converts columns of **char**, **varchar**, or **text** data type from the system OEM code page to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] code page. |
| `RAW` | No conversion occurs from one code page to another. This is the fastest option. |
| `code_page` | Indicates the source code page on which the character data in the data file is encoded; for example, 850. |

::: moniker range="=azuresqldb-mi-current || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017"

> [!IMPORTANT]  
>  Versions before [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] don't support code page 65001 (UTF-8 encoding).
> `CODEPAGE` isn't a supported option on Linux.

> [!NOTE]  
> We recommend that you specify a collation name for each column in a format file, except when you want the 65001 option to have priority over the collation/code page specification.

::: moniker-end

#### DATAFILETYPE

Specifies that `OPENROWSET(BULK)` should read single-byte (ASCII, UTF8) or multi-byte (UTF16) file content. The valid values are **char** and **widechar**:

|DATAFILETYPE value|All data represented in:|
|------------------------|------------------------------|
| **char** (default) |Character format.<br /><br />For more information, see [Use Character Format to Import or Export Data](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md).|
| **widechar** |Unicode characters.<br /><br />For more information, see [Use Unicode Character Format to Import or Export Data](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md).|

#### FORMAT

Specifies the format of the referenced file, for example:

```sql
SELECT *
FROM OPENROWSET(BULK N'<data-file-path>',
                FORMAT='CSV') AS cars;
```

The valid values are  'CSV' (comma separated values file compliant to the [RFC 4180](https://datatracker.ietf.org/doc/html/rfc4180) standard), 'PARQUET', 'DELTA' (version 1.0), and 'JSONL', depending on version:

| Version | CSV | PARQUET | DELTA | JSONL |
|---|---|---|---|---|
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] | Yes | No | No | No |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions | Yes | Yes | Yes | No |
| [!INCLUDE [azsql-md](../../includes/ssazure-sqldb.md)] | Yes | Yes | Yes | No |
| [!INCLUDE [mi-md](../../includes/ssazuremi-md.md)] | Yes | Yes | Yes | No |
| [!INCLUDE [ssod-md](../../includes/sssod-md.md)] in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] | Yes | Yes | Yes | No |
| [!INCLUDE [fabric-md](../../includes/fabric.md)] [!INCLUDE [dw-md](../../includes/fabric-dw.md)] and [!INCLUDE [se-md](../../includes/fabric-se.md)] | Yes | Yes | No | Yes |
| [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] | Yes | Yes | No | No | 

::: moniker range="=fabric"

> [!IMPORTANT]
> The `OPENROWSET` function can read only **newline-delimited** JSON format.
> The newline character must be used as a separator between JSON documents, and cannot be placed in the middle of a JSON document.

The `FORMAT` option doesn't need to be specified if the file extension in the path ends with `.csv`, `.tsv`, `.parquet`, `.parq`, `.jsonl`, `.ldjson`, or `.ndjson`. For example, the `OPENROWSET(BULK)` function knows that the format is parquet based on extension in the following example:

```sql
SELECT *
FROM OPENROWSET(
    BULK 'https://pandemicdatalake.blob.core.windows.net/public/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.parquet'
);
```

If the file path doesn't end with one of these extensions, you need to specify a `FORMAT`, for example:

```sql
SELECT TOP 10 *
FROM OPENROWSET(
      BULK 'abfss://nyctlc@azureopendatastorage.blob.core.windows.net/yellow/**',
      FORMAT='PARQUET'
)
```
::: moniker-end

::: moniker range="=azuresqldb-mi-current || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017"

#### FORMATFILE

Specifies the full path of a format file. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports two types of format files: XML and non-XML.

```sql
SELECT TOP 10 *
FROM OPENROWSET(
      BULK 'D:\XChange\test-csv.csv',
      FORMATFILE= 'D:\XChange\test-format-file.xml'
)
```

A format file is required to define column types in the result set. The only exception is when `SINGLE_CLOB`, `SINGLE_BLOB`, or `SINGLE_NCLOB` is specified; in which case, the format file isn't required.

For information about format files, see [Use a format file to bulk import data (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md).

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the format_file_path can be in Azure Blob Storage. For examples, see [Examples of bulk access to data in Azure Blob Storage](../../relational-databases/import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md).

#### FORMATFILE_DATA_SOURCE 

`FORMATFILE_DATA_SOURCE` defines the root location of the format file path. It enables you to use relative paths in FORMATFILE option.

```sql
CREATE EXTERNAL DATA SOURCE root
WITH (LOCATION = '//pandemicdatalake/public/curated')
GO
SELECT *
FROM OPENROWSET(
    BULK '//pandemicdatalake/public/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv'
    FORMATFILE = 'covid-19/bing_covid-19_data/latest/bing_covid-19_data.fmt',
    FORMATFILE_DATA_SOURCE = 'root'
);
```

The format file data source is created with [CREATE EXTERNAL DATA SOURCE](../statements/create-external-data-source-transact-sql.md).
In addition to the root location, it can define custom credential that can be used to access the files on that location.

::: moniker-end

### Text/CSV options

#### ROWTERMINATOR

Specifies the row terminator to be used for **char** and **widechar** data files, for example:

```sql
SELECT *
FROM OPENROWSET(
    BULK '<data-file-path>',
    ROWTERMINATOR = '\n'
);
```

The default row terminator is `\r\n` (newline character). For more information, see [Specify field and row terminators](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md).

#### FIELDTERMINATOR

Specifies the field terminator to be used for **char** and **widechar** data files, for example:

```sql
SELECT *
FROM OPENROWSET(
    BULK '<data-file-path>',
    FIELDTERMINATOR = '\t'
);
```

The default field terminator is `,` (comma). For more information, see [Specify Field and Row Terminators](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md?view=fabric&preserve-view=true). For example, to read tab-delimited data from a file:

<a id="fieldquote--field_quote"></a>

#### FIELDQUOTE

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], this argument specifies a character that is used as the quote character in the CSV file, like in the following New York example:

```csv
Empire State Building,40.748817,-73.985428,"20 W 34th St, New York, NY 10118","\icons\sol.png"
Statue of Liberty,40.689247,-74.044502,"Liberty Island, New York, NY 10004","\icons\sol.png"
```

Only a single character can be specified as the value for this option. If not specified, the quote character (`"`) is used as the quote character as defined in the [RFC 4180](https://datatracker.ietf.org/doc/html/rfc4180) standard. The `FIELDTERMINATOR` character (for example, a comma) can be placed within the field quotes and it will be considered as a regular character in the cell wrapped with the `FIELDQUOTE` characters. 

For example, in order to read the previous New York sample CSV dataset, use `FIELDQUOTE = '"'`. The address field's values will be retained as a single value, not split into multiple values by the commas within the `"` (quote) characters.

```sql
SELECT *
FROM OPENROWSET(
    BULK '<data-file-path>',
    FIELDQUOTE = '"'
);
```

::: moniker range="=fabric"

#### PARSER_VERSION

**Applies to:** Fabric Data Warehouse only

Specifies parser version to be used when reading files. Currently supported `CSV` parser versions are 1.0 and 2.0:
- PARSER_VERSION = '1.0'
- PARSER_VERSION = '2.0'

```sql
SELECT TOP 10 *
FROM OPENROWSET(
      BULK 'abfss://nyctlc@azureopendatastorage.blob.core.windows.net/yellow/**',
      FORMAT='CSV',
      PARSER_VERSION = '2.0'
)
```

CSV parser version 2.0 is the default implementation optimized for performance, but it does not support all legacy options and encodings available in version 1.0. When using OPENROWSET, Fabric Data Warehouse automatically falls back to version 1.0 if you use the options supported only in that version, even when the version is not explicitly specified. In some cases, you may need to explictly specify the version 1.0 to resolve errors caused by unsupported features reported by parser version 2.0.

CSV parser version 1.0 specifics:

- Following options aren't supported: HEADER_ROW.
- Default terminators are `\r\n`, `\n` and `\r`. 
- If you specify `\n` (newline) as the row terminator, it is automatically prefixed with a `\r` (carriage return) character, which results in a row terminator of `\r\n`.

CSV parser version 2.0 specifics:

- Not all data types are supported.
- Maximum character column length is 8000.
- Maximum row size limit is 8 MB.
- Following options aren't supported: `DATA_COMPRESSION`.
- Quoted empty string ("") is interpreted as empty string.
- DATEFORMAT SET option isn't honored.
- Supported format for **date** data type: `YYYY-MM-DD`
- Supported format for **time** data type: `HH:MM:SS[.fractional seconds]`
- Supported format for **datetime2** data type: `YYYY-MM-DD HH:MM:SS[.fractional seconds]`
- Default terminators are `\r\n` and `\n`.

#### ESCAPE_CHAR

Specifies the character in the file that is used to escape itself and all delimiter values in the file, for example:

```csv
Place,Address,Icon
Empire State Building,20 W 34th St\, New York\, NY 10118,\\icons\\sol.png
Statue of Liberty,Liberty Island\, New York\, NY 10004,\\icons\\sol.png
```

If the escape character is followed by a value other than itself, or any of the delimiter values, the escape character is dropped when reading the value. 

The `ESCAPECHAR` parameter is applied regardless of whether the `FIELDQUOTE` is or isn't enabled. It won't be used to escape the quoting character. The quoting character must be escaped with another quoting character. The quoting character can appear within column value only if value is encapsulated with quoting characters.

In the following example, comma (`,`) and backslash (`\`) are escaped and represented as `\,` and `\\`:

```sql
SELECT *
FROM OPENROWSET(
    BULK '<data-file-path>',
    ESCAPECHAR = '\'
);
```

#### HEADER_ROW

Specifies whether a CSV file contains header row that should not be returned with other data rows. An example of CSV file with a header is shown in the following example:

```csv
Place,Latitude,Longitude,Address,Area,State,Zipcode
Empire State Building,40.748817,-73.985428,20 W 34th St,New York,NY,10118
Statue of Liberty,40.689247,-74.044502,Liberty Island,New York,NY,10004
```

Default is `FALSE`. Supported in `PARSER_VERSION='2.0'` in Fabric Data Warehouse. If `TRUE`, the column names will be read from the first row according to `FIRSTROW` argument. If `TRUE` and schema is specified using `WITH`, binding of column names will be done by column name, not ordinal positions.

```sql
SELECT *
FROM OPENROWSET(
    BULK '<data-file-path>',
    HEADER_ROW = TRUE
);
```

::: moniker-end

### Error handling options

#### ERRORFILE

Specifies the file used to collect rows that have formatting errors and can't be converted to an OLE DB rowset. These rows are copied into this error file from the data file "as is."

```sql
SELECT *
FROM OPENROWSET(
    BULK '<data-file-path>',
    ERRORFILE = '<error-file-path>'
);
```

The error file is created at the start of the command execution. An error is raised if the file already exists. Additionally, a control file that has the extension .ERROR.txt is created. This file references each row in the error file and provides error diagnostics. After the errors are corrected, the data can be loaded.

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the `error_file_path` can be in Azure Blob Storage.

<a id="errorfile-data-source-name"></a>

#### ERRORFILE_DATA_SOURCE

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], this argument is a named external data source pointing to the location of the error file that will contain errors found during the import.

```sql
CREATE EXTERNAL DATA SOURCE root
WITH (LOCATION = '<root-error-file-path>')
GO
SELECT *
FROM OPENROWSET(
    BULK '<data-file-path>',
    ERRORFILE = '<relative-error-file-path>',
    ERRORFILE_DATA_SOURCE = 'root'
);
```

For more information, see [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../statements/create-external-data-source-transact-sql.md).

#### MAXERRORS

Specifies the maximum number of syntax errors or nonconforming rows, as defined in the format file, which can occur before `OPENROWSET` throws an exception. Until `MAXERRORS` is reached, `OPENROWSET` ignores each bad row, not loading it, and counts the bad row as one error.

```sql
SELECT *
FROM OPENROWSET(
    BULK '<data-file-path>',
    MAXERRORS = 0
);
```

The default for *maximum_errors* is 10.

> [!NOTE]  
> `MAX_ERRORS` doesn't apply to `CHECK` constraints, or to converting **money** and **bigint** data types.

### Data processing options

#### FIRSTROW

Specifies the number of the first row to load. The default is 1. This indicates the first row in the specified data file. The row numbers are determined by counting the row terminators. `FIRSTROW` is 1-based.

#### LASTROW

Specifies the number of the last row to load. The default is 0. This indicates the last row in the specified data file.

#### ROWS_PER_BATCH

Specifies the approximate number of rows of data in the data file. This value is an estimate, and should be an approximation (within one order of magnitude) of the actual number of rows. By default, `ROWS_PER_BATCH` is estimated based on file characteristics (number of files, file sizes, size of the returned data types). Specifying `ROWS_PER_BATCH = 0` is the same as omitting `ROWS_PER_BATCH`. For example:

```sql
SELECT TOP 10 *
FROM OPENROWSET(
    BULK '<data-file-path>',
    ROWS_PER_BATCH = 100000
);
```

::: moniker range="=azuresqldb-mi-current || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017"

#### ORDER ( { *column* [ ASC | DESC ] } [ ,... *n* ] [ UNIQUE ] )

An optional hint that specifies how the data in the data file is sorted. By default, the bulk operation assumes the data file is unordered. Performance can improve if the query optimizer can exploit the order to generate a more efficient query plan. The following list provides examples for when specifying a sort can be beneficial:

- Inserting rows into a table that has a clustered index, where the rowset data is sorted on the clustered index key.
- Joining the rowset with another table, where the sort and join columns match.
- Aggregating the rowset data by the sort columns.
- Using the rowset as a source table in the `FROM` clause of a query, where the sort and join columns match.

#### UNIQUE

Specifies that the data file doesn't have duplicate entries.

If the actual rows in the data file aren't sorted according to the order that is specified, or if the `UNIQUE` hint is specified and duplicates keys are present, an error is returned.

Column aliases are required when `ORDER` is used. The column alias list must reference the derived table that is being accessed by the `BULK` clause. The column names that are specified in the `ORDER` clause refer to this column alias list. Large value types (**varchar(max)**, **nvarchar(max)**, **varbinary(max)**, and **xml**) and large object (LOB) types (**text**, **ntext**, and **image**) columns can't be specified.

### Content options

#### SINGLE_BLOB

Returns the contents of *data_file* as a single-row, single-column rowset of type **varbinary(max)**.

> [!IMPORTANT]  
> We recommend that you import XML data only using the `SINGLE_BLOB` option, rather than `SINGLE_CLOB` and `SINGLE_NCLOB`, because only `SINGLE_BLOB` supports all Windows encoding conversions.

#### SINGLE_CLOB

By reading *data_file* as ASCII, returns the contents as a single-row, single-column rowset of type **varchar(max)**, using the collation of the current database.

#### SINGLE_NCLOB

By reading *data_file* as Unicode, returns the contents as a single-row, single-column rowset of type **nvarchar(max)**, using the collation of the current database.

```sql
SELECT * FROM OPENROWSET(
    BULK N'C:\Text1.txt',
    SINGLE_NCLOB
) AS Document;
```
::: moniker-end

::: moniker range="=azuresqldb-mi-current || =azuresqldb-current || =fabric"

### WITH Schema

The `WITH` schema specifies the columns that define the result set of the `OPENROWSET` function. It includes column definitions for every column that will be returned as a result and outlines the mapping rules that bind the underlying file columns to the columns in the result set.

In the following example:

- The `country_region` column has **varchar(50)** type and referencing the underlying column with the same name
- The `date` column is referencing a CSV/Parquet column or JSONL property with a different physical name
- The `cases` column is referencing the third column in the file
- The `fatal_cases` column is referencing a nested Parquet property or JSONL sub-object

```sql
SELECT *
FROM OPENROWSET(<...>) 
WITH (
        country_region varchar(50), --> country_region column has varchar(50) type and referencing the underlying column with the same name
        [date] DATE '$.updated',   --> date is referencing a CSV/Parquet column or JSONL property with a different physical name
        cases INT 3,             --> cases is referencing third column in the file
        fatal_cases INT '$.statistics.deaths'  --> fatal_cases is referencing a nested Parquet property or JSONL sub-object
     );
```

#### <column_name>

The name of the column that will be returned in the result rowset. The data for this column is read from the underlying file column with the same name, unless overridden by `<column_path>` or `<column_ordinal>`. The name of the column must follow the [rules for column name identifiers](../../relational-databases/databases/database-identifiers.md#rules-for-regular-identifiers).

#### <column_type>

The T-SQL type of the column in the result set. The values from the underlying file are converted to this type when `OPENROWSET` returns the results. For more information, see [Data types in Fabric Warehouse](/fabric/data-warehouse/data-types#data-types-in-warehouse).

#### <column_path>

A dot-separated path (for example `$.description.location.lat`) used to reference nested fields in complex types like Parquet.

#### <column_ordinal>

A number representing the physical index of the column that will be mapped to the column in the `WITH` clause.

::: moniker-end

## Permissions

`OPENROWSET` with external data sources, requires the following permissions:

- `ADMINISTER DATABASE BULK OPERATIONS`
  or
- `ADMINISTER BULK OPERATIONS`

The following T-SQL example grants `ADMINISTER DATABASE BULK OPERATIONS` to a principal.

```sql
GRANT ADMINISTER DATABASE BULK OPERATIONS TO [<principal_name>];
```

If the target storage account is private, the principal must also have the **Storage Blob Data Reader** role (or higher) assigned at the container or storage account level.

## Remarks

- A `FROM` clause that is used with `SELECT` can call `OPENROWSET(BULK...)` instead of a table name, with full `SELECT` functionality.
- `OPENROWSET` with the `BULK` option requires a correlation name, also known as a range variable or alias, in the `FROM` clause. Failure to add the `AS <table_alias>` results in the error Msg 491: "A correlation name must be specified for the bulk rowset in the from clause." 
- Column aliases can be specified. If a column alias list isn't specified, the format file must have column names. Specifying column aliases overrides the column names in the format file, such as:

  - `FROM OPENROWSET(BULK...) AS table_alias`
  - `FROM OPENROWSET(BULK...) AS table_alias(column_alias,...n)`

- A `SELECT...FROM OPENROWSET(BULK...)` statement queries the data in a file directly, without importing the data into a table. 
- A `SELECT...FROM OPENROWSET(BULK...)` statement can list bulk-column aliases by using a format file to specify column names, and also data types.

::: moniker range="=azuresqldb-mi-current || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017"

- Using `OPENROWSET(BULK...)` as a source table in an `INSERT` or `MERGE` statement bulk imports data from a data file into a table. For more information, see [Use BULK INSERT or OPENROWSET(BULK...) to import data to SQL Server](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).
- When the `OPENROWSET BULK` option is used with an `INSERT` statement, the `BULK` clause supports table hints. In addition to the regular table hints, such as `TABLOCK`, the `BULK` clause can accept the following specialized table hints: `IGNORE_CONSTRAINTS` (ignores only the `CHECK` and `FOREIGN KEY` constraints), `IGNORE_TRIGGERS`, `KEEPDEFAULTS`, and `KEEPIDENTITY`. For more information, see [Table Hints (Transact-SQL)](../queries/hints-transact-sql-table.md).
- For information about how to use `INSERT...SELECT * FROM OPENROWSET(BULK...)` statements, see [Bulk Import and Export of Data (SQL Server)](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md). For information about when row-insert operations that are performed by bulk import are logged in the transaction log, see [Prerequisites for minimal logging in bulk import](../../relational-databases/import-export/prerequisites-for-minimal-logging-in-bulk-import.md).
- When used to import data with the full recovery model, `OPENROWSET (BULK ...)` doesn't optimize logging.

> [!NOTE]
> When you use `OPENROWSET`, it's important to understand how [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] handles impersonation. For information about security considerations, see [Use BULK INSERT or OPENROWSET(BULK...) to import data to SQL Server](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).

::: moniker-end

::: moniker range="=fabric"

In Microsoft Fabric Data Warehouse, supported features are summarized in the table:

| Feature           | Supported | Not available |
|-------------------|-----------|----------------------|
| **File formats**  | Parquet, CSV, JSONL | Delta, Azure Cosmos DB, JSON, relational databases |
| **Authentication**| EntraID/SPN passthrough, public storage | SAS/SAK, SPN, Managed access |
| **Storage**       | Azure Blob Storage, Azure Data Lake Storage, OneLake in Microsoft Fabric |  |
| **Options**       | Only full/absolute URI in `OPENROWSET`  | Relative URI path in `OPENROWSET`, `DATA_SOURCE` |
| **Partitioning**  | You can use the `filepath()` function in a query. |  |

::: moniker-end

::: moniker range="=azuresqldb-mi-current || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017"

### Bulk importing SQLCHAR, SQLNCHAR, or SQLBINARY data

`OPENROWSET(BULK...)` assumes that, if not specified, the maximum length of `SQLCHAR`, `SQLNCHAR`, or `SQLBINARY` data doesn't exceed 8,000 bytes. If the data being imported is in a LOB data field that contains any **varchar(max)**, **nvarchar(max)**, or **varbinary(max)** objects that exceed 8,000 bytes, you must use an XML format file that defines the maximum length for the data field. To specify the maximum length, edit the format file and declare the MAX_LENGTH attribute.

> [!NOTE]  
> An automatically generated format file doesn't specify the length or maximum length for a LOB field. However, you can edit a format file and specify the length or maximum length manually.

### Bulk exporting or importing SQLXML documents

To bulk export or import SQLXML data, use one of the following data types in your format file.

| Data type | Effect |
| --- | --- |
| `SQLCHAR` or `SQLVARYCHAR` | The data is sent in the client code page, or in the code page implied by the collation. |
| `SQLNCHAR` or `SQLNVARCHAR` | The data is sent as Unicode. |
| `SQLBINARY` or `SQLVARYBIN` | The data is sent without any conversion. |

::: moniker-end

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

## Examples

This section provides general examples to demonstrate how to use `OPENROWSET BULK` syntax. 

::: moniker range="=azuresqldb-mi-current || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017"

### A. Use OPENROWSET to BULK INSERT file data into a varbinary(max) column

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only.

The following example creates a small table for demonstration purposes, and inserts file data from a file named `Text1.txt` located in the `C:` root directory into a **varbinary(max)** column.

```sql
CREATE TABLE myTable (
    FileName NVARCHAR(60),
    FileType NVARCHAR(60),
    Document VARBINARY(MAX)
);
GO

INSERT INTO myTable (
    FileName,
    FileType,
    Document
)
SELECT 'Text1.txt' AS FileName,
    '.txt' AS FileType,
    *
FROM OPENROWSET(
    BULK N'C:\Text1.txt',
    SINGLE_BLOB
) AS Document;
GO
```

### B. Use the OPENROWSET BULK provider with a format file to retrieve rows from a text file

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only.

The following example uses a format file to retrieve rows from a tab-delimited text file, `values.txt` that contains the following data:

```output
1     Data Item 1
2     Data Item 2
3     Data Item 3
```

The format file, `values.fmt`, describes the columns in `values.txt`:

```output
9.0
2
1  SQLCHAR  0  10 "\t"    1  ID           SQL_Latin1_General_Cp437_BIN
2  SQLCHAR  0  40 "\r\n"  2  Description  SQL_Latin1_General_Cp437_BIN
```

This query retrieves that data:

```sql
SELECT a.* FROM OPENROWSET(
    BULK 'C:\test\values.txt',
   FORMATFILE = 'C:\test\values.fmt'
) AS a;
```

### C. Specify a format file and code page

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only.

The following example shows how to use both the format file and code page options at the same time.

```sql
INSERT INTO MyTable
SELECT a.* FROM OPENROWSET (
    BULK N'D:\data.csv',
    FORMATFILE = 'D:\format_no_collation.txt',
    CODEPAGE = '65001'
) AS a;
```

### D. Access data from a CSV file with a format file

**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions only.

```sql
SELECT * FROM OPENROWSET(
    BULK N'D:\XChange\test-csv.csv',
    FORMATFILE = N'D:\XChange\test-csv.fmt',
    FIRSTROW = 2,
    FORMAT = 'CSV'
) AS cars;
```

### E. Access data from a CSV file without a format file

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only.

```sql
SELECT * FROM OPENROWSET(
   BULK 'C:\Program Files\Microsoft SQL Server\MSSQL14\MSSQL\DATA\inv-2017-01-19.csv',
   SINGLE_CLOB
) AS DATA;
```

```sql
SELECT *
FROM OPENROWSET('MSDASQL',
    'Driver={Microsoft Access Text Driver (*.txt, *.csv)}',
    'SELECT * FROM E:\Tlog\TerritoryData.csv'
);
```

> [!IMPORTANT]  
> The ODBC driver should be 64-bit. Open the **Drivers** tab of the [Connect to an ODBC Data Source (SQL Server Import and Export Wizard)](../../integration-services/import-export-data/connect-to-an-odbc-data-source-sql-server-import-and-export-wizard.md) application in Windows to verify this. There's 32-bit `Microsoft Text Driver (*.txt, *.csv)` that will not work with a 64-bit version of `sqlservr.exe`.

### F. Access data from a file stored on Azure Blob Storage

**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions only.

In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, the following example uses an external data source that points to a container in an Azure storage account and a database scoped credential created for a shared access signature.

```sql
SELECT * FROM OPENROWSET(
   BULK 'inv-2017-01-19.csv',
   DATA_SOURCE = 'MyAzureInvoices',
   SINGLE_CLOB
) AS DataFile;
```

For complete `OPENROWSET` examples including configuring the credential and external data source, see [Examples of bulk access to data in Azure Blob Storage](../../relational-databases/import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md).

<a id="j-importing-into-a-table-from-a-file-stored-on-azure-blob-storage"></a>

### G. Import into a table from a file stored on Azure Blob Storage

The following example shows how to use the `OPENROWSET` command to load data from a csv file in an Azure Blob storage location on which you created the SAS key. The Azure Blob storage location is configured as an external data source. This requires a database scoped credential using a shared access signature that is encrypted using a master key in the user database.

```sql
-- Optional: a MASTER KEY is not required if a DATABASE SCOPED CREDENTIAL is not required because the blob is configured for public (anonymous) access!
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
GO

-- Optional: a DATABASE SCOPED CREDENTIAL is not required because the blob is configured for public (anonymous) access!
CREATE DATABASE SCOPED CREDENTIAL MyAzureBlobStorageCredential
    WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
    SECRET = '******srt=sco&sp=rwac&se=2017-02-01T00:55:34Z&st=2016-12-29T16:55:34Z***************';

-- Make sure that you don't have a leading ? in the SAS token, and that you
-- have at least read permission on the object that should be loaded srt=o&sp=r,
-- and that expiration period is valid (all dates are in UTC time)
CREATE EXTERNAL DATA SOURCE MyAzureBlobStorage
WITH (
    TYPE = BLOB_STORAGE,
    LOCATION = 'https://****************.blob.core.windows.net/curriculum',
    -- CREDENTIAL is not required if a blob is configured for public (anonymous) access!
    CREDENTIAL = MyAzureBlobStorageCredential
);

INSERT INTO achievements
WITH (TABLOCK) (
    id,
    description
)
SELECT * FROM OPENROWSET(
    BULK 'csv/achievements.csv',
    DATA_SOURCE = 'MyAzureBlobStorage',
    FORMAT = 'CSV',
    FORMATFILE = 'csv/achievements-c.xml',
    FORMATFILE_DATA_SOURCE = 'MyAzureBlobStorage'
) AS DataFile;
```

### H. Use a managed identity for an external source

**Applies to:** [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)] and [!INCLUDE[ssazure-sqldb](../../includes/ssazure-sqldb.md)]

The following example creates a credential by using a managed identity, creates an external source and then loads data from a CSV hosted on the external source.

First, create the credential and specify blob storage as the external source:

```sql
CREATE DATABASE SCOPED CREDENTIAL sampletestcred
WITH IDENTITY = 'MANAGED IDENTITY';

CREATE EXTERNAL DATA SOURCE SampleSource
WITH (
    LOCATION = 'abs://****************.blob.core.windows.net/curriculum',
    CREDENTIAL = sampletestcred
);
```

Next, load data from the CSV file hosted on blob storage:

```sql
SELECT * FROM OPENROWSET(
    BULK 'Test - Copy.csv',
    DATA_SOURCE = 'SampleSource',
    SINGLE_CLOB
) as test;
```

### I. Use OPENROWSET to access several Parquet files using S3-compatible object storage

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

The following example uses access several Parquet files from different location, all stored on S3-compatible object storage:

```sql
CREATE DATABASE SCOPED CREDENTIAL s3_dsc
WITH IDENTITY = 'S3 Access Key',
SECRET = 'contosoadmin:contosopwd';
GO

CREATE EXTERNAL DATA SOURCE s3_eds
WITH
(
    LOCATION = 's3://10.199.40.235:9000/movies',
    CREDENTIAL = s3_dsc
);
GO

SELECT * FROM OPENROWSET(
    BULK (
        '/decades/1950s/*.parquet',
        '/decades/1960s/*.parquet',
        '/decades/1970s/*.parquet'
    ),
    FORMAT = 'PARQUET',
    DATA_SOURCE = 's3_eds'
) AS data;
```

### J. Use OPENROWSET to access several Delta tables from Azure Data Lake Gen2

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

In this example, the data table container is named `Contoso`, and is located on an Azure Data Lake Gen2 storage account.

```sql
CREATE DATABASE SCOPED CREDENTIAL delta_storage_dsc
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = '<SAS Token>';

CREATE EXTERNAL DATA SOURCE Delta_ED
WITH (
    LOCATION = 'adls://<container>@<storage_account>.dfs.core.windows.net',
    CREDENTIAL = delta_storage_dsc
);

SELECT *
FROM OPENROWSET(
    BULK '/Contoso',
    FORMAT = 'DELTA',
    DATA_SOURCE = 'Delta_ED'
) AS result;
```

### K. Use OPENROWSET to query public-anonymous dataset

The following example uses the publicly available [NYC yellow taxi trip records open data set](/azure/open-datasets/dataset-taxi-yellow).

Create the data source first:

```sql
CREATE EXTERNAL DATA SOURCE NYCTaxiExternalDataSource
WITH (LOCATION = 'abs://nyctlc@azureopendatastorage.blob.core.windows.net');
```

Query all files with `.parquet` extension in folders matching name pattern:

```sql
SELECT TOP 10 *
FROM OPENROWSET(
 BULK 'yellow/puYear=*/puMonth=*/*.parquet',
 DATA_SOURCE = 'NYCTaxiExternalDataSource',
 FORMAT = 'parquet'
) AS filerows;
```

::: moniker-end

::: moniker range="=fabric"

### A. Read a parquet file from Azure Blob Storage

In the following example you can see how to read 100 rows from a Parquet file:

```sql
SELECT TOP 100 * 
FROM OPENROWSET(
    BULK 'https://pandemicdatalake.blob.core.windows.net/public/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.parquet'
);
```

### B. Read a custom CSV file

In the following example you can see how to read rows from a CSV file with a header row and explicitly specified terminator characters that are separating rows and fields:

```sql
SELECT *
FROM OPENROWSET(
BULK 'https://pandemicdatalake.blob.core.windows.net/public/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.csv',
 HEADER_ROW = TRUE,
 ROW_TERMINATOR = '\n',
 FIELD_TERMINATOR = ',');
```

### C. Specify the file column schema while reading a file

In the following example you can see how to explicitly specify the schema of row that will be returned as a result of the `OPENROWSET` function:

```sql
SELECT *
FROM OPENROWSET(
BULK 'https://pandemicdatalake.blob.core.windows.net/public/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.parquet') 
WITH (
        updated DATE
        ,confirmed INT
        ,deaths INT
        ,iso2 VARCHAR(8000)
        ,iso3 VARCHAR(8000)
        );
```

<a id="reading-partitioned-data-sets"></a>

### D. Read partitioned data sets

In the following example you can see how to use the `filepath()` function to read the parts of URI from the matched file path:

```sql
SELECT TOP 10 
  files.filepath(2) AS area
, files.*
FROM OPENROWSET(
BULK 'https://<storage account>.blob.core.windows.net/public/NYC_Property_Sales_Dataset/*_*.csv',
 HEADER_ROW = TRUE) 
AS files
WHERE files.filepath(1) = '2009';
```

### E. Specify the file column schema while reading a JSONL file

In the following example, you can see how to explicitly specify the schema of row that will be returned as a result of the `OPENROWSET` function:

```sql
SELECT TOP 10 *
FROM OPENROWSET(
BULK 'https://pandemicdatalake.dfs.core.windows.net/public/curated/covid-19/bing_covid-19_data/latest/bing_covid-19_data.jsonl') 
WITH (
        country_region varchar(50),
        date DATE '$.updated',
        cases INT '$.confirmed',
        fatal_cases INT '$.deaths'
     );
```

If a column name doesn't match the physical name of a column in the properties if the JSONL file, you can specify the physical name in JSON path after the type definition. You can use multiple properties. For example, `$.location.latitude` to reference the nested properties in parquet complex types or JSON sub-objects.

### More examples

- [Browse file content using OPENROWSET function](/fabric/data-warehouse/browse-file-content-with-openrowset)
- [Performance Guidelines for Fabric Data Warehouse](/fabric/data-warehouse/guidelines-warehouse-performance)
- [Ingest data into your Warehouse using Transact-SQL](/fabric/data-warehouse/ingest-data-tsql)

::: moniker-end

::: moniker range="=fabric-sqldb"

### A. Use OPENROWSET to read a CSV file from a Fabric Lakehouse

In this example, `OPENROWSET` will be used to read a CSV file available on Fabric Lakehouse, named `customer.csv`, stored under the `Files/Contoso/` folder. Since no Data Source and Database Scoped Credentials are provided, Fabric SQL database authenticates with the user's Entra ID context. 

```sql
SELECT * FROM OPENROWSET 
( BULK ' abfss://<workspace id>@<tenant>.dfs.fabric.microsoft.com/<lakehouseid>/Files/Contoso/customer.csv' 
, FORMAT = 'CSV' 
, FIRST_ROW = 2 
) WITH 
(  
    CustomerKey INT,  
    GeoAreaKey INT,  
    StartDT DATETIME2,  
    EndDT DATETIME2,  
    Continent NVARCHAR(50),  
    Gender NVARCHAR(10),  
    Title NVARCHAR(10),  
    GivenName NVARCHAR(100),  
    MiddleInitial VARCHAR(2),  
    Surname NVARCHAR(100),  
    StreetAddress NVARCHAR(200),  
    City NVARCHAR(100),  
    State NVARCHAR(100),  
    StateFull NVARCHAR(100),  
    ZipCode NVARCHAR(20),  
    Country_Region NCHAR(2),  
    CountryFull NVARCHAR(100),  
    Birthday DATETIME2,  
    Age INT,  
    Occupation NVARCHAR(100),  
    Company NVARCHAR(100),  
    Vehicle NVARCHAR(100),  
    Latitude DECIMAL(10,6),  
    Longitude DECIMAL(10,6) ) AS DATA 
```
 
### B. Use OPENROWSET to read file from Fabric Lakehouse and insert into a new table 

In this example, `OPENROWSET` will first be used to read data from a parquet file named`store.parquet`. Then, `INSERT` the data into a new table called `Store`. The parquet file is located in Fabric Lakehouse, since no DATA_SOURCE and database-scoped credential are provided, SQL database in Fabric authenticates with the user's Entra ID context. 
 
```sql
SELECT * 
FROM OPENROWSET 
(BULK 'abfss://<workspace id>@<tenant>.dfs.fabric.microsoft.com/<lakehouseid>/Files/Contoso/store.parquet' 
, FORMAT = 'parquet' )
 AS dataset; 

-- insert into new table 
SELECT * 
INTO Store 
FROM OPENROWSET 
(BULK 'abfss://<workspace id>@<tenant>.dfs.fabric.microsoft.com/<lakehouseid>/Files/Contoso/store.parquet' 
, FORMAT = 'parquet' ) 
 AS STORE; 
```

::: moniker-end

::: moniker range="=azuresqldb-mi-current || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017"
### More examples

For more examples that show using `OPENROWSET(BULK...)`, see the following articles:

- [Bulk Import and Export of Data (SQL Server)](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md)
- [Examples of bulk import and export of XML documents (SQL Server)](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
- [Keep identity values when bulk importing data (SQL Server)](../../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md)
- [Keep nulls or default values during bulk import (SQL Server)](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)
- [Use a format file to bulk import data (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)
- [Use character format to import or export data (SQL Server)](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)
- [Use a Format File to Skip a Table Column (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)
- [Use a format file to skip a data field (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)
- [Use a format file to map table columns to data-file fields (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)
- [Query data sources using OPENROWSET in Azure SQL Managed Instances](/azure/azure-sql/managed-instance/data-virtualization-overview#query-data-sources-using-openrowset)
- [Specify field and row terminators (SQL Server)](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md)

::: moniker-end

## Related content

- [DELETE (Transact-SQL)](../statements/delete-transact-sql.md)
- [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](../queries/from-transact-sql.md)
- [INSERT (Transact-SQL)](../statements/insert-transact-sql.md)
- [OPENDATASOURCE (Transact-SQL)](opendatasource-transact-sql.md)
- [OPENQUERY (Transact-SQL)](openquery-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [UPDATE (Transact-SQL)](../queries/update-transact-sql.md)
- [WHERE (Transact-SQL)](../queries/where-transact-sql.md)
