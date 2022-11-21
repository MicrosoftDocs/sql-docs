---
title: "CREATE EXTERNAL FILE FORMAT (Transact-SQL)"
description: "CREATE EXTERNAL FILE FORMAT (Transact-SQL) Creates an external file format object defining external data stored in Hadoop, Azure Blob Storage, Azure Data Lake Store or for the input and output streams associated with external streams."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 10/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CREATE EXTERNAL FILE FORMAT"
  - "CREATE_EXTERNAL_FILE_FORMAT"
helpviewer_keywords:
  - "External"
  - "External, file format"
  - "PolyBase, external file format"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE EXTERNAL FILE FORMAT (Transact-SQL)

[!INCLUDE [sqlserver2016-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdbmi-asa-pdw.md)]

Creates an external file format object defining external data stored in Hadoop, Azure Blob Storage, Azure Data Lake Store or for the input and output streams associated with external streams. Creating an external file format is a prerequisite for creating an External Table. By creating an External File Format, you specify the actual layout of the data referenced by an external table. To create an External Table, see [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md).

The following file formats are supported:

- **Delimited text**

- **Hive RCFile**

  Does not apply to [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)], [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] or [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] .
  

- **Hive ORC**

  Does not apply to [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)], [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] or [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] .

- **Parquet**

- **JSON**

  Applies to Azure SQL Edge only. For information on using OPENROWSET to import JSON data in other platforms, see [Import JSON documents into SQL Server](../../relational-databases/json/import-json-documents-into-sql-server.md) or [Query JSON files using serverless SQL pool in Azure Synapse Analytics](/azure/synapse-analytics/sql/query-json-files).

- **Delta**

  Applies only to [serverless SQL pools in Azure Synapse Analytics](/azure/synapse-analytics/sql/query-delta-lake-format) and [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].

## Syntax

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

### [Delimited text](#tab/delimited)

```syntaxsql
-- Create an external file format for DELIMITED (CSV/TSV) files.
CREATE EXTERNAL FILE FORMAT file_format_name
WITH (
        FORMAT_TYPE = DELIMITEDTEXT
    [ , FORMAT_OPTIONS ( <format_options> [ ,...n  ] ) ]
    [ , DATA_COMPRESSION = {
           'org.apache.hadoop.io.compress.GzipCodec'
        }
     ]);

<format_options> ::=
{
    FIELD_TERMINATOR = field_terminator
    | STRING_DELIMITER = string_delimiter
    | First_Row = integer -- ONLY AVAILABLE FOR AZURE SYNAPSE ANALYTICS
    | DATE_FORMAT = datetime_format
    | USE_TYPE_DEFAULT = { TRUE | FALSE }
    | Encoding = {'UTF8' | 'UTF16'}
}
```
<!---'org.apache.hadoop.io.compress.DefaultCodec' removed from delimited text -->
### [RC](#tab/rc)

```syntaxsql
--Create an external file format for RC files.
CREATE EXTERNAL FILE FORMAT file_format_name
WITH (
    FORMAT_TYPE = RCFILE,
    SERDE_METHOD = {
        'org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe'
      | 'org.apache.hadoop.hive.serde2.columnar.ColumnarSerDe'
    }
    [ , DATA_COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec' ]);
```

> [!NOTE]  
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

### [ORC](#tab/orc)

```syntaxsql
--Create an external file format for ORC file.
CREATE EXTERNAL FILE FORMAT file_format_name
WITH (
         FORMAT_TYPE = ORC
     [ , DATA_COMPRESSION = {
        'org.apache.hadoop.io.compress.SnappyCodec'
      | 'org.apache.hadoop.io.compress.DefaultCodec' }
    ]);
```

> [!NOTE]  
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

### [Parquet](#tab/parquet)

```syntaxsql
--Create an external file format for PARQUET files.
CREATE EXTERNAL FILE FORMAT file_format_name
WITH (
         FORMAT_TYPE = PARQUET
     [ , DATA_COMPRESSION = {
        'org.apache.hadoop.io.compress.SnappyCodec'
      | 'org.apache.hadoop.io.compress.GzipCodec' }
    ]);
```

### [JSON](#tab/json)

```syntaxsql
-- Create an external file format for JSON files.
CREATE EXTERNAL FILE FORMAT file_format_name
WITH (
    FORMAT_TYPE = JSON
     [ , DATA_COMPRESSION = {
        'org.apache.hadoop.io.compress.SnappyCodec'
      | 'org.apache.hadoop.io.compress.GzipCodec'
      | 'org.apache.hadoop.io.compress.DefaultCodec' }
    ]);
```

### [Delta table](#tab/delta)

```syntaxsql
-- Create an external file format for delta table files (serverless SQL pools in Synapse analytics and SQL Server 2022).
CREATE EXTERNAL FILE FORMAT file_format_name
WITH (
         FORMAT_TYPE = DELTA
      );
```

---

## Arguments

#### *file_format_name*

Specifies a name for the external file format.

### FORMAT_TYPE = [ PARQUET | ORC | RCFILE | DELIMITEDTEXT | DELTA ]

Specifies the format of the external data.

- PARQUET

  Specifies a Parquet format.

- ORC

  Specifies an Optimized Row Columnar (ORC) format. This option requires Hive version 0.11 or higher on the external Hadoop cluster. In Hadoop, the ORC file format offers better compression and performance than the RCFILE file format.

- RCFILE (in combination with SERDE_METHOD = *SERDE_method*)

  Specifies a Record Columnar file format (RcFile). This option requires you to specify a Hive Serializer and Deserializer (SerDe) method. This requirement is the same if you use Hive/HiveQL in Hadoop to query RC files. Note, the SerDe method is case-sensitive.

  Examples of specifying RCFile with the two SerDe methods that PolyBase supports.

  - FORMAT_TYPE = RCFILE, SERDE_METHOD = `org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe`

  - FORMAT_TYPE = RCFILE, SERDE_METHOD = `org.apache.hadoop.hive.serde2.columnar.ColumnarSerDe`

- DELIMITEDTEXT

  Specifies a text format with column delimiters, also called field terminators.

- JSON

  Specifies a JSON format. Applies to Azure SQL Edge only.

- DELTA

  Specifies a Delta Lake format. Applies to serverless SQL pools in Azure Synapse Analytics and [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]

### DATA_COMPRESSION = *data_compression_method*

Specifies the data compression method for the external data. When DATA_COMPRESSION isn't specified, the default is uncompressed data.

To work properly, Gzip compressed files must have the ".gz" file extension.

#### [Delimited text](#tab/delimited)

The DELIMITEDTEXT format type supports this compression method:

- DATA COMPRESSION = `org.apache.hadoop.io.compress.GzipCodec`
<!--- - DATA COMPRESSION ='org.apache.hadoop.io.compress.DefaultCodec' removed from delimited text -->

#### [RC](#tab/rc)

The RCFILE format type supports this compression method:

- DATA COMPRESSION = `org.apache.hadoop.io.compress.DefaultCodec`

#### [ORC](#tab/orc)

The ORC file format type supports these compression methods:

- DATA COMPRESSION = `org.apache.hadoop.io.compress.DefaultCodec`
- DATA COMPRESSION = `org.apache.hadoop.io.compress.SnappyCodec`

#### [Parquet](#tab/parquet)

The PARQUET file format type supports the following compression methods:

- DATA COMPRESSION = `org.apache.hadoop.io.compress.GzipCodec`
- DATA COMPRESSION = `org.apache.hadoop.io.compress.SnappyCodec`

#### [JSON](#tab/json)

The JSON file format type supports the following compression methods:

- DATA COMPRESSION = `org.apache.hadoop.io.compress.GzipCodec`
- DATA COMPRESSION = `org.apache.hadoop.io.compress.SnappyCodec`
- DATA COMPRESSION = `org.apache.hadoop.io.compress.DefaultCodec`

### [Delta table](#tab/delta)

N/A

---

### Delimited text format options

The format options described in this section are optional and only apply to delimited text files.

#### FIELD_TERMINATOR = *field_terminator*

Applies only to delimited text files. The field terminator specifies one or more characters that mark the end of each field (column) in the text-delimited file. The default is the pipe character `|`. For guaranteed support, we recommend using one or more ASCII characters.

Examples:

- `FIELD_TERMINATOR = '|'`
- `FIELD_TERMINATOR = ' '`
- `FIELD_TERMINATOR = ꞌ\tꞌ`
- `FIELD_TERMINATOR = '~|~'`

#### STRING_DELIMITER

`STRING_DELIMITER = *string_delimiter*`

Specifies the field terminator for data of type string in the text-delimited file. The string delimiter is one or more characters in length and is enclosed with single quotes. The default is the empty string "". For guaranteed support, we recommend using one or more ASCII characters.

Examples:

- `STRING_DELIMITER = '"'`

- `STRING_DELIMITER = '0x22'` (Double quote hex)

- `STRING_DELIMITER = '*'`

- `STRING_DELIMITER = ꞌ,ꞌ`

- `STRING_DELIMITER = '0x7E0x7E'` (Two tildes, for example, `~~`)

#### FIRST_ROW = *first_row_int*

Specifies the row number that is read first in all files during a PolyBase load. This parameter can take values 1 - 15. If the value is set to two, the first row in every file (header row) is skipped when the data is loaded. Rows are skipped based on the existence of row terminators (/r/n, /r, /n). When this option is used for export, rows are added to the data to make sure the file can be read with no data loss. If the value is set to >2, the first row exported is the Column names of the external table.

#### DATE_FORMAT = *datetime_format*

Specifies a custom format for all date and time data that might appear in a delimited text file. If the source file uses default datetime formats, this option isn't necessary. Only one custom datetime format is allowed per file. You can't specify more than one custom datetime formats per file. However, you can use more than one datetime formats if each one is the default format for its respective data type in the external table definition.

> [!IMPORTANT]  
> PolyBase only uses the custom date format for importing the data. It doesn't use the custom format for writing data to an external file.

When DATE_FORMAT isn't specified or is the empty string, PolyBase uses the following default formats:

- **datetime**: `'yyyy-MM-dd HH:mm:ss'`

- **smalldatetime**: `'yyyy-MM-dd HH:mm'`

- **date**: `'yyyy-MM-dd'`

- **datetime2**: `'yyyy-MM-dd HH:mm:ss'`

- **datetimeoffset**: `'yyyy-MM-dd HH:mm:ss'`

- **time**: `'HH:mm:ss'`

> [!IMPORTANT]  
> Specifying custom `DATE_FORMAT` will override all default type formats. This means that you will need to have the same date formats in all datetime, date, and time cells in your files. With the overridden `DATE_FORMAT` you cannot have date and time values in different format.

**Example date formats** are in the following table:

Notes about the table:

- Year, month, and day can have several formats and orders. The table shows only the **ymd** format. Month can have one or two digits, or three characters. Day can have one or two digits. Year can have two or four digits.

- Milliseconds (`fffffff`) aren't required.

- `am` or `pm` (`tt`) isn't required. The default is `AM`.

|Date Type|Example|Description|
|---------------|-------------|-----------------|
|**datetime**|DATE_FORMAT = `yyyy-MM-dd HH:mm:ss.fff`|In addition to year, month and day, this date format includes 00-24 hours, 00-59 minutes, 00-59 seconds, and 3 digits for milliseconds.|
|**datetime**|DATE_FORMAT = `yyyy-MM-dd hh:mm:ss.ffftt`|In addition to year, month and day, this date format includes 00-12 hours, 00-59 minutes, 00-59 seconds, 3 digits for milliseconds, and AM, am, PM, or pm. |
|**smalldatetime**|DATE_FORMAT =  `yyyy-MM-dd HH:mm`|In addition to year, month, and day, this date format includes 00-23 hours, 00-59 minutes.|
|**smalldatetime**|DATE_FORMAT =  `yyyy-MM-dd hh:mmtt`|In addition to year, month, and day, this date format includes 00-11 hours, 00-59 minutes, no seconds, and AM, am, PM, or pm.|
|**date**|DATE_FORMAT =  `yyyy-MM-dd`|Year, month, and day. No time element is included.|
|**date**|DATE_FORMAT = `yyyy-MMM-dd`|Year, month, and day. When month is specified with `MMM`, the input value is one or the strings, `Jan`, `Feb`, `Mar`, `Apr`, `May`, `Jun`, `Jul`, `Aug`, `Sep`, `Oct`, `Nov`, or `Dec`.|
|**datetime2**|DATE_FORMAT = `yyyy-MM-dd HH:mm:ss.fffffff`|In addition to year, month, and day, this date format includes 00-23 hours, 00-59 minutes, 00-59 seconds, and 7 digits for milliseconds.|
|**datetime2**|DATE_FORMAT = `yyyy-MM-dd hh:mm:ss.ffffffftt`|In addition to year, month, and day, this date format includes 00-11 hours, 00-59 minutes, 00-59 seconds, 7 digits for milliseconds, and AM, am, PM, or pm.|
|**datetimeoffset**|DATE_FORMAT = `yyyy-MM-dd HH:mm:ss.fffffff zzz`|In addition to year, month, and day, this date format includes 00-23 hours, 00-59 minutes, 00-59 seconds, and 7 digits for milliseconds, and the timezone offset which you put in the input file as `{+&#124;-}HH:ss`. For example, since Los Angeles time without daylight savings is 8 hours behind UTC, a value of -08:00 in the input file specifies the timezone for Los Angeles.|
|**datetimeoffset**|DATE_FORMAT = `yyyy-MM-dd hh:mm:ss.ffffffftt zzz`|In addition to year, month, and day, this date format includes 00-11 hours, 00-59 minutes, 00-59 seconds, 7 digits for milliseconds, (AM, am, PM, or pm), and the timezone offset. See the description in the previous row.|
|**time**|DATE_FORMAT = `HH:mm:ss`|There is no date value, only 00-23 hours, 00-59 minutes, and 00-59 seconds.|

#### Supported date and time formats

External file format can describe a large number of date and time formats:

|datetime|smalldatetime|date|datetime2|datetimeoffset|
|--------------|-------------------|----------|---------------|--------------------|
|[M[M]]M-[d]d-[yy]yy HH:mm:ss[.fff]|[M[M]]M-[d]d-[yy]yy HH:mm[:00]|[M[M]]M-[d]d-[yy]yy|[M[M]]M-[d]d-[yy]yy HH:mm:ss[.fffffff]|[M[M]]M-[d]d-[yy]yy HH:mm:ss[.fffffff] zzz|
|[M[M]]M-[d]d-[yy]yy hh:mm:ss[.fff][tt]|[M[M]]M-[d]d-[yy]yy hh:mm[:00][tt]||[M[M]]M-[d]d-[yy]yy hh:mm:ss[.fffffff][tt]|[M[M]]M-[d]d-[yy]yy hh:mm:ss[.fffffff][tt] zzz|
|[M[M]]M-[yy]yy-[d]d HH:mm:ss[.fff]|[M[M]]M-[yy]yy-[d]d HH:mm[:00]|[M[M]]M-[yy]yy-[d]d|[M[M]]M-[yy]yy-[d]d HH:mm:ss[.fffffff]|[M[M]]M-[yy]yy-[d]d HH:mm:ss[.fffffff] zzz|
|[M[M]]M-[yy]yy-[d]d hh:mm:ss[.fff][tt]|[M[M]]M-[yy]yy-[d]d hh:mm[:00][tt]||[M[M]]M-[yy]yy-[d]d hh:mm:ss[.fffffff][tt]|[M[M]]M-[yy]yy-[d]d hh:mm:ss[.fffffff][tt] zzz|
|[yy]yy-[M[M]]M-[d]d HH:mm:ss[.fff]|[yy]yy-[M[M]]M-[d]d HH:mm[:00]|[yy]yy-[M[M]]M-[d]d|[yy]yy-[M[M]]M-[d]d HH:mm:ss[.fffffff]|[yy]yy-[M[M]]M-[d]d HH:mm:ss[.fffffff]  zzz|
|[yy]yy-[M[M]]M-[d]d hh:mm:ss[.fff][tt]|[yy]yy-[M[M]]M-[d]d hh:mm[:00][tt]||[yy]yy-[M[M]]M-[d]d hh:mm:ss[.fffffff][tt]|[yy]yy-[M[M]]M-[d]d hh:mm:ss[.fffffff][tt] zzz|
|[yy]yy-[d]d-[M[M]]M HH:mm:ss[.fff]|[yy]yy-[d]d-[M[M]]M HH:mm[:00]|[yy]yy-[d]d-[M[M]]M|[yy]yy-[d]d-[M[M]]M HH:mm:ss[.fffffff]|[yy]yy-[d]d-[M[M]]M HH:mm:ss[.fffffff]  zzz|
|[yy]yy-[d]d-[M[M]]M hh:mm:ss[.fff][tt]|[yy]yy-[d]d-[M[M]]M hh:mm[:00][tt]||[yy]yy-[d]d-[M[M]]M hh:mm:ss[.fffffff][tt]|[yy]yy-[d]d-[M[M]]M hh:mm:ss[.fffffff][tt] zzz|
|[d]d-[M[M]]M-[yy]yy HH:mm:ss[.fff]|[d]d-[M[M]]M-[yy]yy HH:mm[:00]|[d]d-[M[M]]M-[yy]yy|[d]d-[M[M]]M-[yy]yy HH:mm:ss[.fffffff]|[d]d-[M[M]]M-[yy]yy HH:mm:ss[.fffffff] zzz|
|[d]d-[M[M]]M-[yy]yy hh:mm:ss[.fff][tt]|[d]d-[M[M]]M-[yy]yy hh:mm[:00][tt]||[d]d-[M[M]]M-[yy]yy hh:mm:ss[.fffffff][tt]|[d]d-[M[M]]M-[yy]yy hh:mm:ss[.fffffff][tt] zzz|
|[d]d-[yy]yy-[M[M]]M HH:mm:ss[.fff]|[d]d-[yy]yy-[M[M]]M HH:mm[:00]|[d]d-[yy]yy-[M[M]]M|[d]d-[yy]yy-[M[M]]M HH:mm:ss[.fffffff]|[d]d-[yy]yy-[M[M]]M HH:mm:ss[.fffffff]  zzz|
|[d]d-[yy]yy-[M[M]]M hh:mm:ss[.fff][tt]|[d]d-[yy]yy-[M[M]]M hh:mm[:00][tt]||[d]d-[yy]yy-[M[M]]M hh:mm:ss[.fffffff][tt]|[d]d-[yy]yy-[M[M]]M hh:mm:ss[.fffffff][tt] zzz|

Details:

- To separate month, day and year values, you must use one of the following separators: `-`, `/`, or `.`. For simplicity, the table uses only the `-` separator.

- To specify the month as text, use three or more characters. Months with one or two characters are interpreted as a number.

- To separate time values, use the `:` symbol.

- Letters enclosed in square brackets are optional.

- The letters `tt` designate [AM|PM|am|pm]. AM is the default. When `tt` is specified, the hour value (hh) must be in the range of 0 to 12.

- The letters `zzz` designate the time zone offset for the system's current time zone in the format {+|-}HH:ss].

#### USE_TYPE_DEFAULT = { TRUE | FALSE }

 Specifies how to handle missing values in delimited text files when PolyBase retrieves data from the text file. The default value is FALSE.

- TRUE

  When retrieving data from the text file, store each missing value by using the default value for the data type of the corresponding column in the external table definition. For example, replace a missing value with:

  - 0 if the column is defined as a numeric column. Decimal columns aren't supported and will error.

  - Empty string "" if the column is a string column.

  - 1900-01-01 if the column is a date column.

- FALSE

  Store all missing values as NULL. Any NULL values that are stored by using the word NULL in the delimited text file are imported as the string `NULL`.

#### ENCODING = {'UTF8' | 'UTF16'}

In [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] (APS CU7.4), PolyBase can read UTF8 and UTF16-LE encoded delimited text files. In SQL Server, PolyBase doesn't support reading UTF16 encoded files.

## Permissions

 Requires ALTER ANY EXTERNAL FILE FORMAT permission.

## Remarks

 The external file format is database-scoped in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]. It is server-scoped in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].

 The format options are all optional and only apply to delimited text files.

 When the data is stored in one of the compressed formats, PolyBase first decompresses the data before returning the data records.

## Limitations

The row delimiter in delimited-text files must be supported by Hadoop's LineRecordReader. That is, it must be either `\r`, `\n`, or `\r\n`. These delimiters aren't user-configurable.

The combinations of supported SerDe methods with RCFiles, and the supported data compression methods are listed previously in this article. Not all combinations are supported.

The maximum number of concurrent PolyBase queries is 32. When 32 concurrent queries are running, each query can read a maximum of 33,000 files from the external file location. The root folder and each subfolder also count as a file. If the degree of concurrency is less than 32, the external file location can contain more than 33,000 files.

Because of the limitation on number of files in the external table, we recommend storing less than 30,000 files in the root and subfolders of the external file location. Also, we recommend keeping the number of subfolders under the root directory to a small number. When too many files are referenced, a Java Virtual Machine out-of-memory exception might occur.

 When exporting data to Hadoop or Azure Blob Storage via PolyBase, only the data is exported, not the column names(metadata) as defined in the CREATE EXTERNAL TABLE command.

## Locking

Takes a shared lock on the EXTERNAL FILE FORMAT object.

## Performance

Using compressed files always comes with the tradeoff between transferring less data between the external data source and SQL Server while increasing the CPU usage to compress and decompress the data.

Gzip compressed text files aren't splittable. To improve performance for Gzip compressed text files, we recommend generating multiple files that are all stored in the same directory within the external data source. This file structure allows PolyBase to read and decompress the data faster by using multiple reader and decompression processes. The ideal number of compressed files is the maximum number of data reader processes per compute node. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], the maximum number of data reader processes is 8 per node except [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] Gen2, which is 20 readers per node. In [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], the maximum number of data reader processes per node varies by SLO. See [[!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] loading patterns and strategies](https://blogs.msdn.microsoft.com/sqlcat/2017/05/17/azure-sql-data-warehouse-loading-patterns-and-strategies/) for details.

## Examples

### A. Create a DELIMITEDTEXT external file format

This example creates an external file format named *textdelimited1* for a text-delimited file. The options listed for FORMAT_OPTIONS specify that the fields in the file should be separated using a pipe character `|`. The text file is also compressed with the Gzip codec. If DATA_COMPRESSION isn't specified, the text file is uncompressed.

For a delimited text file, the data compression method can either be the default Codec, `org.apache.hadoop.io.compress.DefaultCodec`, or the Gzip Codec, `org.apache.hadoop.io.compress.GzipCodec`.

```sql
CREATE EXTERNAL FILE FORMAT textdelimited1
WITH (
    FORMAT_TYPE = DELIMITEDTEXT,
    FORMAT_OPTIONS (
        FIELD_TERMINATOR = '|',
        DATE_FORMAT = 'MM/dd/yyyy' ),
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.GzipCodec'
);
```

### B. Create an RCFile external file format

This example creates an external file format for a RCFile that uses the `serialization/deserialization` method `org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe`. It also specifies to use the Default Codec for the data compression method. If DATA_COMPRESSION isn't specified, the default is no compression.

```sql
CREATE EXTERNAL FILE FORMAT rcfile1
WITH (
    FORMAT_TYPE = RCFILE,
    SERDE_METHOD = 'org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe',
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec'
);
```

### C. Create an ORC external file format

This example creates an external file format for an ORC file that compresses the data with the `org.apache.io.compress.SnappyCodec` data compression method. If DATA_COMPRESSION isn't specified, the default is no compression.

```sql
CREATE EXTERNAL FILE FORMAT orcfile1
WITH (
    FORMAT_TYPE = ORC,
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'
);
```

### D. Create a PARQUET external file format

This example creates an external file format for a Parquet file that compresses the data with the `org.apache.io.compress.SnappyCodec` data compression method. If DATA_COMPRESSION isn't specified, the default is no compression.

```sql
CREATE EXTERNAL FILE FORMAT parquetfile1
WITH (
    FORMAT_TYPE = PARQUET,
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'
);
```

### E. Create a delimited text file skipping header row

**Applies to:** Azure Synapse Analytics, and [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later.

This example creates an external file format for CSV file with a single header row. For more information, see [Virtualize CSV file with PolyBase](../../relational-databases/polybase/virtualize-csv.md).

```sql
CREATE EXTERNAL FILE FORMAT skipHeader_CSV
WITH (FORMAT_TYPE = DELIMITEDTEXT,
      FORMAT_OPTIONS(
          FIELD_TERMINATOR = ',',
          STRING_DELIMITER = '"',
          FIRST_ROW = 2,
          USE_TYPE_DEFAULT = True)
)
```

### F. Create a JSON external file format

**Applies to:** Azure SQL Edge.

This example creates an external file format for a JSON file that compresses the data with the `org.apache.io.compress.SnappyCodec` data compression method. If DATA_COMPRESSION isn't specified, the default is no compression. This example applies to Azure SQL Edge and is currently not supported for other SQL products.

```sql
CREATE EXTERNAL FILE FORMAT jsonFileFormat
WITH (
    FORMAT_TYPE = JSON,
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'
);
```

### G. Create a Delta table external file format

This example creates an external file format for Delta table type file format. This example applies to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]. For more information, see [Virtualize delta table file with PolyBase](../../relational-databases/polybase/virtualize-delta.md).

```sql
CREATE EXTERNAL FILE FORMAT DeltaFileFormat
WITH(
    FORMAT_TYPE = DELTA
    );
```

## Next steps

- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)
- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
- [CREATE EXTERNAL TABLE AS SELECT (Transact-SQL)](../../t-sql/statements/create-external-table-as-select-transact-sql.md)
- [CREATE TABLE AS SELECT &#40;Azure Synapse Analytics&#41;](../../t-sql/statements/create-table-as-select-azure-sql-data-warehouse.md)
- [sys.external_file_formats (Transact-SQL)](../../relational-databases/system-catalog-views/sys-external-file-formats-transact-sql.md)
