---
title: "CREATE EXTERNAL FILE FORMAT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ce73d4d8-6b8f-4822-839e-ce35ca6c7787
caps.latest.revision: 30
author: BarbKess
---
# CREATE EXTERNAL FILE FORMAT (SQL Server PDW)
Creates a SQL Server PDW external file format definition for external data stored in Hadoop or Azure blob storage. This is a prerequisite for creating an external table.  
  
To create an external table, see [CREATE EXTERNAL TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
--Create an external file format for a Hadoop ORC File.  
CREATE EXTERNAL FILE FORMAT file_format_name  
WITH (  
    FORMAT_TYPE = ORC   
    [ , DATA_COMPRESSION = {  
        'org.apache.hadoop.io.compress.SnappyCodec'  
      | 'org.apache.hadoop.io.compress.DefaultCodec'  
      }  
    ]  
);  
  
--Create an external file format for a Hadoop RcFile.  
CREATE EXTERNAL FILE FORMAT file_format_name  
WITH (  
    FORMAT_TYPE = RCFILE,   
    SERDE_METHOD = {  
        'org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe'  
      | 'org.apache.hadoop.hive.serde2.columnar.ColumnarSerDe'  
    }  
    [ , DATA_COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec' ]  
);  
  
--Create an external file format for a Hadoop text-delimited file.  
CREATE EXTERNAL FILE FORMAT file_format_name  
WITH (  
    FORMAT_TYPE = DELIMITEDTEXT  
    [ , FORMAT_OPTIONS ( <format_options> [ ,...n ] ) ]  
    [ , DATA_COMPRESSION = {  
           'org.apache.hadoop.io.compress.GzipCodec'  
         | 'org.apache.hadoop.io.compress.DefaultCodec'  
        }   
    ]  
  
);  
  
<format_options> ::=  
{  
    FIELD_TERMINATOR = field_terminator  
    | STRING_DELIMITER = string_delimiter  
    | DATE_FORMAT = datetime_format  
    | USE_TYPE_DEFAULT = { TRUE | FALSE }  
}  
```  
  
## Arguments  
*file_format_name*  
Specifies a name for the new external file format.  
  
ORC  
Specifies Optimized Row Columnar for the external file format. This option requires Hive version 0.11 or higher. In conjunction with HDP 2.0, ORC file formats offer better compression and performance than the RCFILE formats.  
  
RCFILE, SERDE_METHOD = *SERDE_method*  
Specifies Record Columnar File (RcFile) for the external file format. This option requires a Hive Serializer and Deserializer (SerDe) method. The SerDe method is case sensitive.  
  
Examples:  
  
-   FORMAT_TYPE = RCFILE, SERDE_METHOD = 'org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe'  
  
-   FORMAT_TYPE = RCFILE, SERDE_METHOD = 'org.apache.hadoop.hive.serde2.columnar.ColumnarSerDe'  
  
DELIMITEDTEXT  
Specifies the external file format contains text with delimiters.  
  
FIELD_TERMINATOR = *field_terminator*  
The terminator for each field (column) in the text-delimited file on Hadoop. The field_terminator can be can be one or more characters. The default is the pipe character ꞌ|ꞌ.  
  
Examples:  
  
-   FIELD_TERMINATOR = '|'  
  
-   FIELD_TERMINATOR = ' '  
  
-   FIELD_TERMINATOR = ꞌ\tꞌ  
  
-   FIELD_TERMINATOR = '~|~'  
  
STRING_DELIMITER = *string_delimiter*  
Specifies the delimiter for data of type string in the text-delimited file on Hadoop. The string delimiter is one or more characters in length and is enclosed with single quotes. The default is the empty string "".  
  
Examples:  
  
-   STRING_DELIMITER = '*'  
  
-   STRING_DELIMITER = ꞌ,ꞌ  
  
-   STRING_DELIMITER = '0x7E0x7E'  
  
DATE_FORMAT = *datetime_format*  
Specifies a single format for all date and time data. If DATE_FORMAT is not specified or is the empty string, PolyBase will use these default formats:  
  
**Default format** when DATE_FORMAT is not specified.  
  
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
|-------------|-----------|---------------|  
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
  
Supported date formats.  
  
|datetime|smalldatetime|date|datetime2|datetimeoffset|  
|------------|-----------------|--------|-------------|------------------|  
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
  
-   The letters 'tt' designate [AM|PM|am|pm]. AM is the default. When 'tt' is specified, the hour value (hh) must be in the range of 00 to 12.  Otherwise, HH specifies 00 to 24.  
  
-   The letters 'zzz' designate the time zone offset for the system's current time zone in the format {+|-}HH:ss].  
  
USE_TYPE_DEFAULT = { TRUE | **FALSE** }  
Specifies how to handle missing values when SQL Server PDW is importing data from HDFS text files into SQL Server PDW, and when SQL Server PDW is exporting data from SQL Server PDW query results to the HDFS text file. No action occurs when CREATE EXTERNAL TABLE AS SELECT is run.  
  
TRUE  
For importing, all missing values in the HDFS delimited text file are stored using the column default in the external table definition. For example, a missing value in the HDFS text file imports to:  
  
-   The default constraint if a default constraint is defined on the column.  
  
-   0 if the column is defined as a numeric column.  
  
-   Empty string "" if the column is a string column.  
  
FALSE  
For importing, all missing values in the HDFS delimited text file are stored as NULL in the imported row data. Any NULL values that are stored by using the word NULL in the Hadoop file will be imported as the string 'NULL'.  
  
For exporting, NULL column values in the SQL Server PDW data are stored as empty values in the HDFS text file. For example, if the field terminator is '|', a NULL value is stored by writing nothing between the field terminators: '||'.  
  
DATA_COMPRESSION = *data_compression_method*  
Specifies the data compression method for the data in the external file. When DATA_COMPRESSION is not specified, the default is uncompressed data.  
  
The DELIMITEDTEXT format type supports these compression methods:  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.GzipCodec'  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec'  
  
The RCFILE format type supports this compression method:  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec'  
  
The ORC file format type supports these compression methods:  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'  
  
-   DATA COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec'  
  
## Metadata  
The external file format is stored as a server object  
  
## Permissions  
Requires CONTROL SERVER or ALTER ANY EXTERNAL FILE FORMAT permission.  
  
## General Remarks  
Each external file format is stored in the master database as a server-level object.  
  
For operations on compressed external file formats, PDW will perform the compression when the external table is created with CREATE EXTERNAL TABLE AS SELECT. PDW does not perform the compression when the external table is created with CREATE EXTERNAL TABLE. In all cases, PDW decompresses the data in order to return the query results. For predicate pushdown on external compressed delimited text files, Hadoop decompresses the data, performs the predicate calculation, and then compresses the results for transfer back to PDW.  
  
## Limitations and Restrictions  
The row delimiter must be UTF-8 and supported by Hadoop’s LineRecordReader. The row delimiter must be either '\r', '\n', or '\r\n'. These are not user-configurable.  
  
PolyBase supports only UTF-8 encoding which is Hadoop’s standard encoding.  
  
PolyBase supports the '\r', '\n', and '\r\n' row delimiters. The default for Hadoop’s LineRecordReader is '\n'.  
  
## Locking  
Takes a shared lock on the EXTERNALFILEFORMAT object.  
  
## Performance  
**Recommendations for compressed external file format**s  
  
The impact of compressed files on performance is a tradeoff between transferring less data between the Hadoop Cluster and PDW, and increasing the CPU usage to compress and decompress the data. Usually, the PDW CPU’s are busy and compression will slow performance. Network bandwidth might be limited for transferring data back and forth to Azure storage blob over the Internet. In this case, compression could improve performance.  
  
If you are using Hadoop to generate the data for a compressed external file, we recommend generating multiple files that are all stored in the same directory. Compressed files are decompressed as a serial operation. By reading multiple compressed files, PDW can spread the decompression across the Compute nodes. The ideal number of compressed files is proportionate to the number of Compute nodes multiplied by 8. This allows PDW to read and decompress, in parallel, one or more compressed files per distribution across all the Compute nodes.  
  
## Examples  
  
### Create a delimited text file format  
This example creates an external file format named *textdelimited1* for a text-delimited Hadoop file. The FORMAT_OPTIONS specify the fields in the Hadoop file will be separated with a pipe character '|'. The text file is also compressed with the Gzip codec. If DATA_COMPRESSION is not specified, the text file is uncompressed.  
  
The data compression method can specify either the default Codec, 'org.apache.hadoop.io.compress.DefaultCodec', or the Gzip Codec, 'org.apache.hadoop.io.compress.GzipCodec'.  
  
```  
CREATE EXTERNAL FILE FORMAT textdelimited1  
WITH (  
    FORMAT_TYPE = DELIMITEDTEXT,  
    FORMAT_OPTIONS (   
        FIELD_TERMINATOR = '|',  
        DATE_FORMAT = 'MM/dd/yyyy'  
    ),  
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.GzipCodec'  
)  
;  
```  
  
## Create an RcFile external file format  
This example creates a file format for a Hadoop RcFile that uses the serialization/deserialization method org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe. It also specifies to use the Default Codec for the data compression method. If DATA_COMPRESSION is not specified, the default is no compression.  
  
> [!IMPORTANT]  
> The GZip Codec does not work with RcFiles, even though it compiles successfully.  
  
```  
CREATE EXTERNAL FILE FORMAT rcfile1  
WITH (  
    FORMAT_TYPE = RCFILE,  
    SERDE_METHOD = 'org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe',  
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.DefaultCodec'  
)  
;  
```  
  
## Create an ORC external file format  
This example creates a file format for a Hadoop ORC file that compresses the data with the org.apache.io.compress.SnappyCodec data compression method. If DATA_COMPRESSION is not specified, the default is no compression.  
  
```  
CREATE EXTERNAL FILE FORMAT textdelimited1  
WITH (  
    FORMAT_TYPE = ORC,  
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'  
)  
;  
```  
  
## See Also  
[PolyBase &#40;SQL Server PDW&#41;](../sqlpdw/polybase-sql-server-pdw.md)  
[CREATE EXTERNAL TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-sql-server-pdw.md)  
[CREATE EXTERNAL TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-as-select-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
