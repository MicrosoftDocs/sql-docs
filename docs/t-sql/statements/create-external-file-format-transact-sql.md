---
title: "CREATE EXTERNAL FILE FORMAT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/28/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE EXTERNAL FILE FORMAT"
  - "CREATE_EXTERNAL_FILE_FORMAT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "External"
  - "External, file format"
  - "PolyBase, external file format"
ms.assetid: abd5ec8c-1a0e-4d38-a374-8ce3401bc60c
caps.latest.revision: 25
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# CREATE EXTERNAL FILE FORMAT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw_md](../../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]

  Creates a PolyBase external file format definition for external data stored in Hadoop, Azure blob storage, or Azure Data Lake Store. Creating an external file format is a prerequisite for creating a PolyBase external table. By creating an external file format, you specify the actual layout of the data referenced by an external table.  
  
 PolyBase supports these file formats:  
  
-   Delimited text  
  
-   Hive RCFile  
  
-   Hive ORC  
  
-   Parquet  
  
 To create an external table, see [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Create an external file format for PARQUET files.  
CREATE EXTERNAL FILE FORMAT file_format_name  
WITH (  
    FORMAT_TYPE = PARQUET  
     [ , DATA_COMPRESSION = {  
        'org.apache.hadoop.io.compress.SnappyCodec'  
      | 'org.apache.hadoop.io.compress.GzipCodec'      }  
    ]);  
  
--Create an external file format for ORC files.  
CREATE EXTERNAL FILE FORMAT file_format_name  
WITH (  
    FORMAT_TYPE = ORC  
     [ , DATA_COMPRESSION = {  
        'org.apache.hadoop.io.compress.SnappyCodec'  
      | 'org.apache.hadoop.io.compress.DefaultCodec'      }  
    ]);  
  
--Create an external file format for RCFILE.  
CREATE EXTERNAL FILE FORMAT file_format_name  
WITH (  
    FORMAT_TYPE = RCFILE,  
    SERDE_METHOD = {  
        'org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe'  
      | 'org.apache.hadoop.hive.serde2.columnar.ColumnarSerDe'  
    }  
    [ , DATA_COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec' ]);  
  
--Create an external file format for DELIMITED TEXT files.  
CREATE EXTERNAL FILE FORMAT file_format_name  
WITH (  
    FORMAT_TYPE = DELIMITEDTEXT  
    [ , FORMAT_OPTIONS ( <format_options> [ ,...n  ] ) ]  
    [ , DATA_COMPRESSION = {  
           'org.apache.hadoop.io.compress.GzipCodec'  
         | 'org.apache.hadoop.io.compress.DefaultCodec'  
        }  
     ]);  
  
<format_options> ::=  
{  
    FIELD_TERMINATOR = field_terminator  
    | STRING_DELIMITER = string_delimiter  
    | DATE_FORMAT = datetime_format  
    | USE_TYPE_DEFAULT = { TRUE | FALSE } 
    | Encoding = {'UTF8' | 'UTF16'} 
}  
```  
  
## Arguments  
 *file_format_name*  
 Specifies a name for the external file format.  
  
 FORMAT_TYPE  
 Specifies the format of the external data.  
  
 PARQUET  
 Specifies a Parquet  format.  
  
 ORC  
 Specifies an Optimized Row Columnar (ORC) format. This option requires Hive version 0.11 or higher on the external Hadoop cluster. In Hadoop, the ORC file format offers better compression and performance than the RCFILE file format.  
  
 RCFILE (in combination with SERDE_METHOD = *SERDE_method*)  
 Specifies a Record Columnar file format (RcFile). This option requires you to specify a Hive Serializer and Deserializer (SerDe) method. This requirement is the same if you use Hive/HiveQL in Hadoop to query RC files. Note, the SerDe method is case sensitive.  
  
 Examples of specifying RCFile with the two SerDe methods that PolyBase supports.  
  
-   FORMAT_TYPE = RCFILE, SERDE_METHOD = 'org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe'  
  
-   FORMAT_TYPE = RCFILE, SERDE_METHOD = 'org.apache.hadoop.hive.serde2.columnar.ColumnarSerDe'  
  
 DELIMITEDTEXT  
 Specifies a text format with column delimiters, also called field terminators.  
  
 FIELD_TERMINATOR = *field_terminator*  
 Applies only to delimited text files. This specifies one or more characters that mark the end of each field (column) in the text-delimited file. The default is the pipe character ꞌ|ꞌ. For guaranteed support, we recommend to use one or more ascii characters.
  
  
 Examples:  
  
-   FIELD_TERMINATOR = '|'  
  
-   FIELD_TERMINATOR = ' '  
  
-   FIELD_TERMINATOR = ꞌ\tꞌ  
  
-   FIELD_TERMINATOR = '~|~'  
  
 STRING_DELIMITER = *string_delimiter*  
 Specifies the field terminator for data of type string in the text-delimited file. The string delimiter is one or more characters in length and is enclosed with single quotes. The default is the empty string "". For guaranteed support, we recommend to use one or more ascii characters.
 
  
 Examples:  

-   STRING_DELIMITER = '"'

-   STRING_DELIMITER = '0x22' -- Double quote hex
  
-   STRING_DELIMITER = '*'  
  
-   STRING_DELIMITER = ꞌ,ꞌ  
  
-   STRING_DELIMITER = '0x7E0x7E'  -- Two tildas (e.g. ~~)
  
 DATE_FORMAT = *datetime_format*  
 Specifies a custom format for all date and time data that might appear in a delimited text file. If the source file uses default datefime formats, this option is not necessary. Only one custom datetime format is allowed per file. You cannot specify multiple custom datetime formats per file. However, you can use multiple datetime formats if each one is the default format for its respective data type in the external table definition.
 
 
PolyBase only uses the custom date format for importing the data. It does not use the custom format for writing data to an external file.

 When DATE_FORMAT is not specified or is the empty string, PolyBase will use the following default formats:  
  
-   DateTime: 'yyyy-MM-dd HH:mm:ss'  
  
-   SmallDateTime: 'yyyy-MM-dd HH:mm'  
  
-   Date: 'yyyy-MM-dd'  
  
-   DateTime2: 'yyyy-MM-dd HH:mm:ss'  
  
-   DateTimeOffset: 'yyyy-MM-dd HH:mm:ss'  
  
-   Time: 'HH:mm:ss'  
  
 **Example date formats** are in the following table.  
  
 Notes about the table:  
  
-   Year, month, and day can have a variety of formats and orders. The table only shows ymd. Month can have 1 or 2 digits, or 3 characters. Day can have 1 or 2 digits. Year can have 2 or 4 digits.  
  
-   Milliseconds (fffffff) is not required.  
  
-   Am, pm (tt) is not required. The default is AM.  
  
|Date Type|Example|Description|  
|---------------|-------------|-----------------|  
|DateTime|DATE_FORMAT = 'yyyy-MM-dd HH:mm:ss.fff'|In addition to year, month and day, this includes 00-24 hours, 00-59 minutes, 00-59 seconds, and 3 digits for milliseconds.|  
|DateTime|DATE_FORMAT = 'yyyy-MM-dd hh:mm:ss.ffftt'|In addition to year, month and day, this includes 00-12 hours, 00-59 minutes, 00-59 seconds, 3 digits for milliseconds, and AM, am, PM, or pm .|  
|SmallDateTime|DATE_FORMAT =  'yyyy-MM-dd HH:mm'|In addition to year, month, and day, this includes 00-23 hours, 00-59 minutes.|  
|SmallDateTime|DATE_FORMAT =  'yyyy-MM-dd hh:mmtt'|In addition to year, month, and day, this includes 00-11 hours, 00-59 minutes, no seconds, and AM, am, PM, or pm.|  
|Date|DATE_FORMAT =  'yyyy-MM-dd'|Year, month, and day. No time element is included.|  
|Date|DATE_FORMAT = 'yyyy-MMM-dd'|Year, month, and day. When month is specified with 3 M’s, the input value is one or the strings Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, or Dec.|  
|DateTime2|DATE_FORMAT = 'yyyy-MM-dd HH:mm:ss.fffffff'|In addition to year, month, and day, this includes 00-23 hours, 00-59 minutes, 00-59 seconds, and 7 digits for milliseconds.|  
|DateTime2|DATE_FORMAT = 'yyyy-MM-dd hh:mm:ss.ffffffftt'|In addition to year, month, and day, this includes 00-11 hours, 00-59 minutes, 00-59 seconds, 7 digits for milliseconds, and AM, am, PM, or pm.|  
|DateTimeOffset|DATE_FORMAT = 'yyyy-MM-dd HH:mm:ss.fffffff zzz'|In addition to year, month, and day, this includes 00-23 hours, 00-59 minutes, 00-59 seconds, and 7 digits for milliseconds, and the timezone offset which you put in the input file as {+&#124;-}HH:ss. For example, since Los Angeles time without daylight savings is 8 hours behind UTC, a value of -08:00 in the input file specifies the timezone for Los Angeles.|  
|DateTimeOffset|DATE_FORMAT = 'yyyy-MM-dd hh:mm:ss.ffffffftt zzz'|In addition to year, month, and day, this includes 00-11 hours, 00-59 minutes, 00-59 seconds, 7 digits for milliseconds, (AM, am, PM, or pm), and the timezone offset. See the description in the previous row.|  
|Time|DATE_FORMAT = 'HH:mm:ss'|There is no date value, only 00-23 hours, 00-59 minutes, and 00-59 seconds.|  
  
 All supported date formats:  
  
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
  
-   To separate month, day and year values, you can use ' – ', ' / ', or ' . '. For simplicity, the table uses only the ' – ' separator.  
  
-   To specify the month as text use three or more characters. Months with 1 or 2 characters will be interpreted as a number.  
  
-   To separate time values, use the ' : ' symbol.  
  
-   Letters enclosed in square brackets are optional.  
  
-   The letters 'tt' designate [AM|PM|am|pm]. AM is the default. When 'tt' is specified, the hour value (hh) must be in the range of 0 to 12.  
  
-   The letters 'zzz' designate the time zone offset for the system's current time zone in the format {+|-}HH:ss].  
  
 USE_TYPE_DEFAULT = { TRUE | **FALSE** }  
 Specifies how to handle missing values in delimited text files when PolyBase retrieves data from the text file.  
  
 TRUE  
 When retrieving data from the text file, store each missing value by using the default value for the data type of the corresponding column in the external table definition. For example, replace a missing value with:  
  
-   0 if the column is defined as a numeric column.  
  
-   Empty string "" if the column is a string column.  
  
-   1900-01-01 if the column is a date column.  
  
 FALSE  
 Store all missing values as NULL. Any NULL values that are stored by using the word NULL in the delimited text file will be imported as the string 'NULL'.  
  
   Encoding = {'UTF8' | 'UTF16'}   
 In Azure SQL Data Warehouse, PolyBase can read UTF8 and UTF16-LE encoded delimited text files. In SQL Server and PDW, PolyBase does not support reading UTF16 encoded files.
  
 DATA_COMPRESSION = *data_compression_method*  
 Specifies the data compression method for the external data. When DATA_COMPRESSION is not specified, the default is uncompressed data.  
  
 The DELIMITEDTEXT format type supports these compression methods:  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec'  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.GzipCodec'  
  
 The RCFILE format type supports this compression method:  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec'  
  
 The ORC file format type supports these compression methods:  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec'  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'  
  
 The PARQUET file format type supports these compression methods:  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.GzipCodec'  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'  
  
## Permissions  
 Requires ALTER ANY EXTERNAL FILE FORMAT permission.  
  
## General Remarks  
 The external file format is database-scoped in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]. It is server-scoped in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].  
  
 The format options are all optional and only apply to delimited text files.  
  
 When the data is stored in one of the compressed formats, PolyBase will first decompress the data before returning the data records.  
  
## Limitations and Restrictions   
  
 The row delimiter in delimited-text files must be supported by Hadoop’s LineRecordReader i.e. it must be either '\r', '\n', or '\r\n'. These are not user-configurable.  
  
 The combinations of supported SerDe methods with RCFiles, and the supported data compression methods are listed previously in this article. Not all combinations are supported.  
  
 The maximum number of concurrent PolyBase queries is 32. When 32 concurrent queries are running, each query can read a maximum of 33,000 files from the external file location. The root folder and each subfolder also count as a file. If the degree of concurrency is less than 32, the external file location can contain more than 33,000 files.  
  
 Because of the limitation on number of files in the external table, we recommend storing less than 30,000 files in the root and subfolders of the external file location. Also, we recommend keeping the number of subfolders under the root directory to a small number. When too many files are referenced a Java Virtual Machine out-of-memory exception might occur.  
  
## Locking  
 Takes a shared lock on the EXTERNAL FILE FORMAT object.  
  
## Performance  
 Using compressed files always comes with the tradeoff between transferring less data between the external data source and SQL Server while increasing the CPU usage to compress and decompress the data.  
  
 Gzip compressed text files are not splittable. To improve performance for Gzip compressed text files, we recommend generating multiple files that are all stored in the same directory within the external data source. This allows PolyBase to read and decompress the data faster by using multiple reader and decompression processes. The ideal number of compressed files is the maximum number of data reader processes per compute node. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], the maximum number of data reader processes is 8 per node in the current release. In [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], the maximum number of data reader processes per node varies by SLO. See [Azure SQL Data Warehouse loading patterns and strategies](https://blogs.msdn.microsoft.com/sqlcat/2016/02/06/azure-sql-data-warehouse-loading-patterns-and-strategies/) for details.  
  
## Examples  
  
### A. Create a DELIMITEDTEXT external file format  
 This example creates an external file format named *textdelimited1* for a text-delimited file. The FORMAT_OPTIONS specify the fields in the file will be separated with a pipe character '|'. The text file is also compressed with the Gzip codec. If DATA_COMPRESSION is not specified, the text file is uncompressed.  
  
 For a delimited text file, the data compression method can either be the default Codec, 'org.apache.hadoop.io.compress.DefaultCodec', or the Gzip Codec, 'org.apache.hadoop.io.compress.GzipCodec'.  
  
```  
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
 This example creates an external file format for a RCFile that uses the serialization/deserialization method org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe. It also specifies to use the Default Codec for the data compression method. If DATA_COMPRESSION is not specified, the default is no compression.  
  
```  
CREATE EXTERNAL FILE FORMAT rcfile1  
WITH (  
    FORMAT_TYPE = RCFILE,  
    SERDE_METHOD = 'org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe',  
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec'  
);  
```  
  
### C. Create an ORC external file format  
 This example creates an external file format for an ORC file that compresses the data with the org.apache.io.compress.SnappyCodec data compression method. If DATA_COMPRESSION is not specified, the default is no compression.  
  
```  
CREATE EXTERNAL FILE FORMAT orcfile1  
WITH (  
    FORMAT_TYPE = ORC,  
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'  
);  
```  
  
### D. Create a PARQUET external file format  
 This example creates an external file format for a Parquet file that compresses the data with the org.apache.io.compress.SnappyCodec data compression method. If DATA_COMPRESSION is not specified, the default is no compression.  
  
```  
CREATE EXTERNAL FILE FORMAT parquetfile1  
WITH (  
    FORMAT_TYPE = PARQUET,  
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'  
);  
```  
  
## See Also  
 [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md)   
 [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)   
 [CREATE EXTERNAL TABLE AS SELECT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-as-select-transact-sql.md)   
 [CREATE TABLE AS SELECT &#40;Azure SQL Data Warehouse&#41;](../../t-sql/statements/create-table-as-select-azure-sql-data-warehouse.md)   
 [sys.external_file_formats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-external-file-formats-transact-sql.md)  
  
  
