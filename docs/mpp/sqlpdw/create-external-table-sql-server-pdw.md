---
title: "CREATE EXTERNAL TABLE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5373c362-9541-4eeb-8c40-22cde55f88ff
caps.latest.revision: 48
author: BarbKess
---
# CREATE EXTERNAL TABLE (SQL Server PDW)
This statement creates a SQL Server PDW external table that points to data in a Hadoop cluster or Azure blob storage.  
  
Create an external table in order to:  
  
-   Query Hadoop data from within SQL Server PDW.  
  
-   Import and store Hadoop data into SQL Server PDW by using the CREATE TABLE AS SELECT statement.  
  
For more information, see [PolyBase &#40;SQL Server PDW&#41;](../sqlpdw/polybase-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
--Create a new external table in SQL Server PDW  
CREATE EXTERNAL TABLE [ database_name . [ schema_name ] . | schema_name. ] table_name   
    ( <column_definition> [ ,...n ] )  
    WITH (   
        LOCATION = 'hdfs_folder_or_filepath',  
        DATA_SOURCE = external_data_source_name,  
        FILE_FORMAT = external_file_format_name  
        [ , <reject_options> [ ,...n ] ]  
    )  
[;]  
  
<reject_options> ::=  
{  
    | REJECT_TYPE = value | percentage  
    | REJECT_VALUE = reject_value  
    | REJECT_SAMPLE_VALUE = reject_sample_value  
  
}  
```  
  
## Arguments  
*database_name* . [ schema_name ] . | schema_name. ] *table_name*  
The one to three-part name of the table to create in SQL Server PDW. For an external table, only the table metadata is stored in SQL Server PDW. The metadata is stored in the Shell database on the Control node; no table data is stored on the Compute nodes.  
  
<column_definition> [ ,...*n* ]  
CREATE EXTERNAL TABLE allows one or more column definitions. Both CREATE EXTERNAL TABLE and CREATE TABLE use the same syntax for defining a column. An exception to this, you cannot use the DEFAULT CONSTRAINT on external tables.  
  
For the full details about column definitions and their data types, see [CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md).  
  
For external tables that use the Hive RCFile format, the column and type definitions must map to the exact schema of the external file. When defining data type, use the following mappings between SQL and Hive data types. The types include all versions of Hive unless stated otherwise.  
  
> [!NOTE]  
> When storing variations of the SQL Server date and time data types (date, time, smalldatetime, datetime, and datetime2) to a RCFile, the SQL Server data types are converted to the Unix **timestamp** data type (a 32-bit integer), limiting the date range to 1970-01-01 through 2038-1-19. To work around this limitation, cast the SQL Server type to a text type in the SELECT portion of CETAS. The resulting EXTERNAL TABLE definition will have the text type specified for that column. Cast the type back into a SQL Server datetime type when selecting from it.  
  
|SQL Data Type|.NET Data Type|Hive Data Type|Hadoop/Java Data Type|Comments|  
|-----------------|------------------|------------------|--------------------------|------------|  
|tinyint|Byte|tinyint|ByteWritable|For unsigned numbers only.|  
|smallint|Int16|smallint|ShortWritable||  
|int|Int32|int|IntWritable||  
|bigint|Int64|bigint|LongWritable||  
|bit|Boolean|boolean|BooleanWritable||  
|float|Double|double|DoubleWritable||  
|real|Single|float|FloatWritable||  
|money|Decimal|double|DoubleWritable||  
|smallmoney|Decimal|double|DoubleWritable||  
|nchar|String<br /><br />Char[]|string|text||  
|nvarchar|String<br /><br />Char[]|string|Text||  
|char|String<br /><br />Char[]|string|Text||  
|varchar|String<br /><br />Char[]|string|Text||  
|binary|Byte[]|binary|BytesWritable|Applies to Hive 0.8 and later.|  
|varbinary|Byte[]|binary|BytesWritable|Applies to Hive 0.8 and later.|  
|date|DateTime|timestamp|TimestampWritable||  
|smalldatetime|DateTime|timestamp|TimestampWritable||  
|datetime2|DateTime|timestamp|TimestampWritable||  
|datetime|DateTime|timestamp|TimestampWritable||  
|time|TimeSpan|timestamp|TimestampWritable||  
|decimal|Decimal|decimal|BigDecimalWritable|Applies to Hive0.11 and later.|  
  
LOCATION =  '*hdfs_folder_or_filepath*'  
Specifies the folder or the file path and file name where the data file is located in the Hadoop Cluster. The location starts from the root folder of the Hadoop Cluster; the root folder is specified in [CREATE EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-data-source-sql-server-pdw.md).  
  
If you specify LOCATION to be a folder, a PolyBase query that selects from the external table will return files from the folder and all of the subfolders. Just like Hadoop, PolyBase does not return hidden folders. It also does not return files for which the file name begins with an underline (_) or a period (.).  
  
In this example, if LOCATION='/webdata/', a query will return rows from mydata.txt and mydata2.txt.  It will not return mydata3.txt because it is a subfolder of a hidden folder. It will not return _hidden.txt because it is a hidden file.  
  
![Recursive data for external tables](../sqlpdw/media/APS_PolyBase_folder_traversal.png "APS_PolyBase_folder_traversal")  
  
DATA_SOURCE = *external_data_source_name*  
Specifies the name of the external data source that contains the location where the external data is stored or will be stored. The location is either a Hadoop Cluster or Azure blob storage. To create an external data source, use [CREATE EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-data-source-sql-server-pdw.md).  
  
FILE_FORMAT = *external_file_format_name*  
Specifies the name of the external file format that contains the format for the Hadoop data file. To create an external file format, use [CREATE EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-file-format-sql-server-pdw.md).  
  
Reject Options  
The reject options do not apply when CREATE EXTERNAL TABLE runs. Instead, they are stored with the table metadata so that SQL Server PDW can use them at a later time when it imports data from the external table into SQL Server PDW. For example, when a future SELECT statement or CREATE TABLE AS SELECT statement selects data from the external table, SQL Server PDW will use the reject options to determine the number or percentage of rows that can fail to import before it stops the import.  
  
REJECT_VALUE = *reject_value*  
Specifies the value or the percentage of rows that can fail to import before SQL Server PDW halts the import.  
  
REJECT_TYPE = **value** | percentage  
Clarifies whether the REJECT_VALUE option is specified as a literal value or a percentage.  
  
value  
REJECT_VALUE is a literal value, not a percentage. SQL Server PDW will stop importing rows from the external data file when the number of failed rows exceeds *reject_value*.  
  
For example, if REJECT_VALUE = 5 and REJECT_TYPE = value, SQL Server PDW will stop importing rows after 5 rows have failed to import.  
  
percentage  
REJECT_VALUE is a percentage, not a literal value. SQL Server PDW will stop importing rows from the external data file when the *percentage* of failed rows exceeds *reject_value*. The percentage of failed rows is calculated at intervals.  
  
REJECT_SAMPLE_VALUE = *reject_sample_value*  
Required when REJECT_TYPE = percentage. It specifies the number of rows to attempt to import before SQL Server PDW recalculates the percentage of failed rows.  
  
For example, if REJECT_SAMPLE_VALUE = 1000, SQL Server PDW will calculate the percentage of failed rows after it has attempted to import 1000 rows from the external data file. If the percentage of failed rows is less than *reject_value*, SQL Server PDW will attempt to load another 1000 rows. SQL Server PDW continues to recalculate the percentage of failed rows after it attempts to import each additional 1000 rows.  
  
> [!NOTE]  
> Since SQL Server PDW computes the percentage of failed rows at intervals, the actual percentage of failed rows can exceed *reject_value*.  
  
Example:  
  
This example shows how the three REJECT options interact with each other. For example, if REJECT_TYPE = percentage, REJECT_VALUE = 30, and REJECT_SAMPLE_VALUE = 100, the following scenario could occur:  
  
-   SQL Server PDW attempts to load the first 100 rows; 25 fail and 75 succeed.  
  
-   Percent of failed rows is calculated as 25%, which is less than the reject value of 30%. So, no need to halt the load.  
  
-   SQL Server PDW attempts to load the next 100 rows; this time 25 succeed and 75 fail.  
  
-   Percent of failed rows is recalculated as 50%. The percentage of failed rows has exceeded the 30% reject value.  
  
-   The load fails with 50% failed rows after attempting to load 200 rows, which is larger than the specified 30% limit..  
  
## Permissions  
The **CREATE EXTERNAL TABLE AS SELECT** statement requires the following permissions:  
  
-   The database user must have membership in the **db_ddladmin** fixed database role or the **ALTER SCHEMA** permission on the local schema that will contain the new table.  
  
-   The database user must have membership in the **db_ddladmin** fixed database role or the **CREATE TABLE** permission.  
  
-   The database user must have **SELECT** permission on any objects referenced in the *select_criteria*.  
  
-   The login must have the **ADMINISTER BULK OPERATIONS** permission.  
  
-   The login must have the **ALTER ANY EXTERNAL DATA SOURCE** permission.  
  
-   The login must have the **ALTER ANY EXTERNAL FILE FORMAT** permission.  
  
-   The login must have write permission on the Hadoop Cluster folder.  
  
## Error Handling  
When CREATE EXTERNAL TABLE runs, SQL Server PDW attempts to connect to the external Hadoop cluster. If the connection fails, the command will fail and the external table will not be created. It can take a minute or more for the command to fail since SQL Server PDW retries the connection 3 times.  
  
## General Remarks  
The row data for an external table is stored in the Hadoop Distributed File System (HDFS) with text-delimited or RcFile format. These data files are created and managed by your own processes.  
  
The external Hadoop data is imported into SQL Server PDW when the data is required by a query predicate, or when a CREATE TABLE AS SELECT statement selects from the external table. During query execution, the data is stored in a SQL Server PDW temporary table. After the query finishes, SQL Server PDW removes and deletes the temporary table. During CREATE TABLE AS SELECT, SQL Server PDW imports and stores the data into a permanent tablein SQL Server PDW.  
  
When executing queries on external Hadoop tables, SQL Server PDW performs *predicate pushdown* to compute parts of the query in Hadoop before copying data into SQL Server PDW. To perform predicate pushdown, SQL Server PDW issues map-reduce jobs to run on Hadoop data by using the job tracker ID specified in the external data source. All SQL Server PDW supported data types can be part of a predicate or expression to be evaluated on Hadoop.  
  
For more information, see [Queries on External Tables With Predicate Pushdown &#40;SQL Server PDW&#41;](../sqlpdw/queries-on-external-tables-with-predicate-pushdown-sql-server-pdw.md).  
  
In AU2, by default a PolyBase query on an external table folder returned data stored in the root folder only. Beginning with AU3, by default a query on an external table returns data from the root folder and all of the subfolders. The root folder is specified by the LOCATION parameter in CREATE EXTERNAL TABLE. To avoid unwanted results, verify that subfolders in external file folders do not contain extra data that should not be returned in query results. For backwards compatibility, your appliance administrator can change the default setting to read data from only the root folder of the external file location. To do this, set <polybase.recursive.traversal> to 'false' in the appliance configuration file called core-site.xml.  
  
## Limitations and Restrictions  
By design, you cannot export data to an external table that was created with the CREATE EXTERNAL TABLE statement. To export data to a new external table, use CREATE EXTERNAL TABLE AS SELECT.  
  
You can have multiple external tables among the SQL Server PDW user databases and they can each be connected to the same or different Hadoop cluster. When connections exist to different clusters, all Hadoop clusters must be running the same version of Hadoop.  
  
Since the data for an external table resides off the appliance, it is not under the control of SQL Server PDW, and can be changed or removed at any time by an external process. As a result:  
  
-   CREATE TABLE, DROP TABLE, CREATE STATISTICS, DROP STATISTICS, CREATE VIEW, and DROP VIEW are the only DDL operations allowed on external Tables.  
  
-   External table columns cannot use the DEFAULT constraint.  
  
-   A query will fail if the connection to the Hadoop cluster fails.  
  
-   Query results against an external table are not guaranteed to be deterministic. The same query can return different results each time it runs against an external table.  
  
-   A query can fail if the off-appliance table data is removed or relocated.  
  
PolyBase can consume a maximum of 33k files per folder when running 32 concurrent PolyBase queries. This maximum number includes both files and subfolders in each HDFS folder. If the degree of concurrency is less than 32, a user can run PolyBase queries against folders in HDFS which contain more than 33k files. Microsoft recommends users of Hadoop and PolyBase keep file paths short and use no more than 30k files per HDFS folder. When too many files are referenced a JVM out-of-memory exception occurs.  
  
## Locking  
Takes a shared lock on the SCHEMARESOLUTION object.  
  
## Examples  
  
### A. Create an external table with data in text-delimited format.  
This example shows all the steps required to create an external table that has data formatted in text-delimited files. It defines an external data source mydatasource and an external file format myfileformat. These sever-level objects are then referenced in the CREATE EXTERNAL TABLE statement. For more information, see [CREATE EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-data-source-sql-server-pdw.md) and [CREATE EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-file-format-sql-server-pdw.md).  
  
```  
CREATE EXTERNAL DATA SOURCE mydatasource  
WITH (  
    TYPE = HADOOP,  
    LOCATION = 'hdfs://xxx.xxx.xxx.xxx:8020'  
)  
  
CREATE EXTERNAL FILE FORMAT myfileformat  
WITH (  
    FORMAT_TYPE = DELIMITEDTEXT,   
    FORMAT_OPTIONS (FIELD_TERMINATOR ='|')  
);  
  
CREATE EXTERNAL TABLE ClickStream (   
    url varchar(50),  
    event_date date,  
    user_IP varchar(50)  
)  
WITH (  
        LOCATION='/webdata/employee.tbl',  
        DATA_SOURCE = mydatasource,  
        FILE_FORMAT = myfileformat  
    )  
;  
```  
  
### B. Create an external table with data in RCFile format.  
This example shows all the steps required to create an external table that has data formatted as RCFiles. It defines an external data source mydatasource_rc and an external file format myfileformat_rc. These sever-level objects are then referenced in the CREATE EXTERNAL TABLE statement. For more information, see [CREATE EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-data-source-sql-server-pdw.md) and [CREATE EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-file-format-sql-server-pdw.md).  
  
> [!NOTE]  
> SQL Server PDW requires that all data sources are of the same format type. You cannot have some data sources with text-delimited file formats and some with RCFILE formats.  
  
```  
CREATE EXTERNAL DATA SOURCE mydatasource_rc  
WITH (  
    TYPE = HADOOP,  
    LOCATION = 'hdfs://xxx.xxx.xxx.xxx:8020'  
)  
  
CREATE EXTERNAL FILE FORMAT myfileformat_rc  
WITH (  
    FORMAT = RCFILE,  
    SERDE_METHOD = 'org.apache.hadoop.hive.serde2.columnar.LazyBinaryColumnarSerDe'  
)  
;  
  
CREATE EXTERNAL TABLE ClickStream_rc (   
    url varchar(50),  
    event_date date,  
    user_ip varchar(50)  
)  
WITH (  
        LOCATION='/webdata/employee_rc.tbl',  
        DATA_SOURCE = mydatasource_rc,  
        FILE_FORMAT = myfileformat_rc  
    )  
;  
```  
  
### C. Create an external table with data in ORC format.  
This example shows all the steps required to create an external table that has data formatted as ORC files. It defines an external data source mydatasource_orc and an external file format myfileformat_orc. These sever-level objects are then referenced in the CREATE EXTERNAL TABLE statement. For more information, see [CREATE EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-data-source-sql-server-pdw.md) and [CREATE EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-file-format-sql-server-pdw.md).  
  
> [!NOTE]  
> SQL Server PDW requires that all data sources are of the same format type. You cannot have some data sources with text-delimited file formats, some with RCFILE formats, and some with ORC file formats.  
  
```  
CREATE EXTERNAL DATA SOURCE mydatasource_orc  
WITH (  
    TYPE = HADOOP,  
    LOCATION = 'hdfs://xxx.xxx.xxx.xxx:8020'  
)  
  
CREATE EXTERNAL FILE FORMAT myfileformat_orc  
WITH (  
    FORMAT = ORC,  
    COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'  
)  
;  
  
CREATE EXTERNAL TABLE ClickStream_orc (   
    url varchar(50),  
    event_date date,  
    user_ip varchar(50)  
)  
WITH (  
        LOCATION='/webdata/',  
        DATA_SOURCE = mydatasource_orc,  
        FILE_FORMAT = myfileformat_orc  
    )  
;  
```  
  
### D. Retrieving HDFS data in SQL Server PDW  
Clickstream is an external table that connects to the employee.tbl delimited text file on a Hadoop cluster. The following SQL Server PDW query runs against data on a Hadoop cluster.  
  
```  
SELECT TOP 10 (url) FROM ClickStream WHERE user_ip = 'xxx.xxx.xxx.xxx'  
;  
```  
  
### E. Join external tables  
  
```  
SELECT url.description  
FROM ClickStream cs  
JOIN UrlDescription url ON cs.url = url.name  
WHERE cs.url = 'msdn.microsoft.com'  
;  
```  
  
### F. Join HDFS data with PDW data  
  
```  
SELECT cs.user_ip FROM ClickStream cs  
JOIN User u ON cs.user_ip = u.user_ip  
WHERE cs.url = 'www.microsoft.com'  
;  
```  
  
### G. Import row data from HDFS into a distributed PDW Table  
  
```  
CREATE TABLE ClickStream_PDW  
WITH ( DISTRIBUTION = HASH (url) )  
AS SELECT url, event_date, user_ip FROM ClickStream  
;  
```  
  
### H. Import row data from HDFS into a replicated PDW Table  
  
```  
CREATE TABLE ClickStream_PDW  
WITH ( DISTRIBUTION = REPLICATE )  
AS SELECT url, event_date, user_ip   
FROM ClickStream  
;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
