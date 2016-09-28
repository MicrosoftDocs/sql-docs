---
title: "CREATE EXTERNAL TABLE AS SELECT (SQL Server PDW)"
ms.custom: na
ms.date: 08/10/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 4e140921-fc76-4807-a2ee-3199c0f6944b
caps.latest.revision: 44
author: BarbKess
---
# CREATE EXTERNAL TABLE AS SELECT (SQL Server PDW)
Creates a SQL Server PDW external table and then exports, in parallel, the results of a Transact\-SQL SELECT statement to Hadoop.  
  
Use the CREATE EXTERNAL TABLE AS SELECT (CETAS) statement to:  
  
-   Export a SQL Server PDW table to Hadoop.  
  
-   Import multiple Hadoop tables into SQL Server PDW, join them, and then export the results in parallel back to Hadoop.  
  
-   Join a Hadoop table and a SQL Server PDW table, and then export the results in parallel back to Hadoop.  
  
For more information, see [PolyBase &#40;SQL Server PDW&#41;](../sqlpdw/polybase-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE EXTERNAL TABLE [ database_name . [ schema_name ] . | schema_name. ] table_name   
    WITH (   
        LOCATION = 'hdfs_folder',  
        DATA_SOURCE = external_data_source_name,  
        FILE_FORMAT = external_file_format_name  
        [ , <reject_options> [ ,...n ] ]  
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
    [ WITH <common_table_expression> [ ,...n ] ]  
    SELECT <select_criteria>  
```  
  
## Arguments  
[ *database_name* . [ schema_name ] . | schema_name. ] *table_name*  
The one to three-part name of the table to create in SQL Server PDW. For an external table, only the table metadata is stored in SQL Server PDW. The metadata is stored in the Shell database on the Control node; no table data is stored on the Compute nodes.  
  
LOCATION =  '*hdfs_folder*'  
Specifies where to populate the data relative to the root folder of the Hadoop Cluster.  The location is a folder name and can optionally include a path.  PolyBase will create the path and folder under the root folder if it does not exist.  The root folder location is stored in the external data source.  
  
DATA_SOURCE = *external_data_source_name*  
Specifies the name of the external data source that contains the location where the external data is stored or will be stored. The location is either a Hadoop Cluster or Azure blob storage. To create an external data source, use [CREATE EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-data-source-sql-server-pdw.md).  
  
FILE_FORMAT = *external_file_format_name*  
Specifies the name of the external file format that contains the format for the Hadoop data file. To create an external file format, use [CREATE EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-file-format-sql-server-pdw.md).  
  
Reject Options  
The reject options do not apply when this statement is run. They need to be specified with CREATE EXTERNAL TABLE AS SELECT so that SQL Server PDW can use them at a later time when it imports data from the external table into SQL Server PDW. For example, when a future SELECT statement or CREATE TABLE AS SELECT statement selects data from the external table, SQL Server PDW will use the reject options to determine the number or percentage of rows that can fail to import before it stops the import.  
  
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
  
-   The load fails with 50% failed rows after attempting to load 200 rows, which is larger than the specified 30% limit.  
  
WITH *common_table_expression*  
Specifies a temporary named result set, known as a common table expression (CTE). For more information, see [WITH common_table_expression &#40;SQL Server PDW&#41;](../sqlpdw/with-common-table-expression-sql-server-pdw.md).  
  
SELECT <select_criteria>  
Populates the new table with the results from a SELECT statement. *select_criteria* is the body of the SELECT statement that determines which data to copy to the new table. For information about SELECT statements, see [SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md).  
  
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
When CREATE EXTERNAL TABLE AS SELECT exports data to a Hadoop text-delimited file, there is no rejection file for rows that fail to export.  
  
When creating the external table, SQL Server PDW attempts to connect to the external Hadoop cluster. If the connection fails, the command will fail and the external table will not be created. It can take a minute or more for the command to fail since SQL Server PDW retries the connection 3 times.  
  
If CREATE EXTERNAL TABLE AS SELECT is cancelled or fails, SQL Server PDW will make a one-time attempt to remove any new files and directories that were created on the external data source.  
  
SQL Server PDW will report any Java errors that occur on the Hadoop Cluster during the data export.  
  
## <a name="GeneralRemarks"></a>General Remarks  
As a prerequisite for creating an external table, the PDW region administrator needs to configure hadoop connectivity. For more information, see [Configure PolyBase Connectivity to External Data &#40;Analytics Platform System&#41;](../management/configure-polybase-connectivity-to-external-data-analytics-platform-system.md) in the Appliance Operations Guide.  
  
After the CETAS statement finishes, you can run SQL queries on the external table. These operations will import data into SQL Server PDW for the duration of the query unless you import by using the CREATE TABLE AS SELECT statement.  
  
The external table name and definition are stored in the SQL Server PDW metadata. The results of the SELECT statement are written to the file on an external Hadoop Cluster.  
  
The Hadoop text files are named *table_name*. *ID*.txt where *ID* is an incremental identifier. For example, if the external table name is JanuaryClicks, the SQL Server PDW table name is JanuaryClicks and the Hadoop file name is JanuaryClicks.1.txt. If you delete the SQL Server PDW JanuaryClicks table, the Hadoop JanuaryClicks.1.txt remains. If you run CETAS again to create January Clicks, this time the Hadoop file is called JanuaryClicks.2.txt.  
  
The CETAS statement always creates a non-partitioned table, even if the source table is partitioned.  
  
For query plans, created with EXPLAIN, SQL Server PDW has the following operations that are used specifically for external tables:  
  
-   External shuffle move  
  
-   External broadcast move  
  
-   External partition move  
  
## Limitations and Restrictions  
Since external table data resides off the appliance, backup and restore will only backup and restore the table definition stored in the Shell database on the Control node.  
  
SQL Server PDW does not verify the connection to the Hadoop Cluster when restoring a   database backup that contains an external table. If the original source HDFS file is not accessible, the metadata restore of the external table will still succeed, but SELECT operations that import data from the external table will fail.  
  
Since the external table data is stored off of the appliance, SQL Server PDW does not guarantee any data consistency between SQL Server PDW and the external HDFS data source file. It is the customer’s responsibility to maintain the consistency between HDFS data and SQL Server PDW data.  
  
DML operations are not supported on external tables. For example, you cannot use SQL Server PDW to insert, delete, or modify the HDFS data.  
  
CREATE TABLE, DROP TABLE, CREATE STATISTICS, DROP STATISTICS, CREATE VIEW, and DROP VIEW are the only DDL operations allowed on external Tables.  
  
PolyBase can consume a maximum of 33k files per folder when running 32 concurrent PolyBase queries. This maximum number includes both files and subfolders in each HDFS folder. If the degree of concurrency is less than 32, a user can run PolyBase queries against folders in HDFS which contain more than 33k files. Microsoft recommends users of Hadoop and PolyBase keep file paths short and use no more than 30k files per HDFS folder. When too many files are referenced a JVM out-of-memory exception occurs.  
  
[SET ROWCOUNT &#40;SQL Server PDW&#41;](../sqlpdw/set-rowcount-sql-server-pdw.md) has no effect on this statement. To achieve a similar behavior, use [TOP &#40;SQL Server PDW&#41;](../sqlpdw/top-sql-server-pdw.md).  
  
When CREATE EXTERNAL TABLE AS SELECT selects from an RCFile, the column values in the RCFile must not contain the pipe '|' character.  
  
## Locking  
Takes a shared lock on the SCHEMARESOLUTION object.  
  
## <a name="Examples"></a>Examples  
  
### A. Create a table using CREATE EXTERNAL TABLE AS SELECT (CETAS)  
The following example creates a new external table named `hdfsCustomer`, using the column definitions and data from the source table `dimCustomer`.  
  
The table definition is stored in SQL Server PDW, and the results of the SELECT statement are exported to the '/pdwdata/customer.tbl' file on the Hadoop external data source *customer_ds*. The file is formatted according to the external file format *customer_ff*.  
  
The file name is generated by SQL Server PDW, and contains the query ID for ease of aligning the file with the query that generated it.  
  
The path `hdfs://xxx.xxx.xxx.xxx:5000/files/` preceding the Customer directory must already exist. However, if the Customer directory does not exist, SQL Server PDW will create the directory.  
  
> [!NOTE]  
> This example specifies for 5000. If the port is not specified, SQL Server PDW uses 8020 as the default port.  
  
The resulting Hadoop location and file name will be `hdfs:// xxx.xxx.xxx.xxx:5000/files/Customer/ QueryID_YearMonthDay_HourMinutesSeconds_FileIndex.txt.`.  
  
```  
USE AdventureWorksPDW2012;  
CREATE EXTERNAL TABLE hdfsCustomer  
WITH (  
        LOCATION='/pdwdata/customer.tbl',  
        DATA_SOURCE = customer_ds,  
        FILE_FORMAT = customer_ff  
) AS SELECT * FROM dimCustomer;  
```  
  
### B. Use a Query Hint with CREATE EXTERNAL TABLE AS SELECT (CETAS)  
This query shows the basic syntax for using a query join hint with the CETAS statement. After the query is submitted to the Control node, SQL Server, running on the Compute nodes, will apply the hash join strategy when generating the SQL Server query plan. For more information on join hints and how to use the OPTION clause, see [OPTION &#40;SQL Server PDW&#41;](../sqlpdw/option-sql-server-pdw.md).  
  
> [!NOTE]  
> This example specifies for 5000. If the port is not specified, SQL Server PDW uses 8020 as the default port.  
  
```  
USE AdventureWorksPDW2012;  
CREATE EXTERNAL TABLE dbo.FactInternetSalesNew  
WITH   
    (   
        LOCATION = '/files/Customer',  
        DATA_SOURCE = customer_ds,  
        FILE_FORMAT = customer_ff  
    )  
AS SELECT T1.* FROM dbo.FactInternetSales T1 JOIN dbo.DimCustomer T2  
ON ( T1.CustomerKey = T2.CustomerKey )  
OPTION ( HASH JOIN );  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE EXTERNAL TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-sql-server-pdw.md)  
[CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md)  
[DROP TABLE &#40;SQL Server PDW&#41;](../sqlpdw/drop-table-sql-server-pdw.md)  
[ALTER TABLE &#40;SQL Server PDW&#41;](../sqlpdw/alter-table-sql-server-pdw.md)  
  
