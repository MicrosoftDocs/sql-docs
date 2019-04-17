---
title: "CREATE EXTERNAL TABLE AS SELECT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.service: sql-data-warehouse
ms.subservice: design
ms.topic: conceptual
f1_keywords: 
  - "CREATE EXTERNAL TABLE AS SELECT"
  - "CREATE_EXTERNAL_TABLE_AS_SELECT"
  - "CREATE EXTERNAL TABLE AS SELECT_TSQL"
helpviewer_keywords: 
  - "External, table"
  - "PolyBase, external table"
  - "External, table create as select"
  - "PolyBase, create table as select"
ms.assetid: 32dfe254-6df7-4437-bfd6-ca7d37557b0a
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# CREATE EXTERNAL TABLE AS SELECT (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Creates an external table and then exports, in parallel, the results of a [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT statement to Hadoop or Azure Storage Blob.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CREATE EXTERNAL TABLE [ [database_name  . [ schema_name ] . ] | schema_name . ] table_name   
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
 [ [ *database_name* . [ *schema_name* ] . ] | *schema_name* . ] *table_name*  
 The one to three-part name of the table to create in the database. For an external table, only the table metadata is stored in the relational database.  
  
 LOCATION =  '*hdfs_folder*'  
 Specifies where to write the results of the SELECT statement on the external data source. The location is a folder name and can optionally include a path that is relative to the root folder of the Hadoop Cluster or Azure Storage Blob.  PolyBase will create the path and folder if it does not already exist.  
  
 The external files are written to *hdfs_folder* and named *QueryID_date_time_ID.format*, where *ID* is an incremental identifier and *format* is the exported data format. For example, QID776_20160130_182739_0.orc.  
  
 DATA_SOURCE = *external_data_source_name*  
 Specifies the name of the external data source object that contains the location where the external data is stored or will be stored. The location is either a Hadoop Cluster or an Azure Storage Blob. To create an external data source, use [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md).  
  
 FILE_FORMAT = *external_file_format_name*  
 Specifies the name of the external file format object that contains the format for the external data file. To create an external file format, use [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md).  
  
 Reject Options  
 The reject options do not apply at the time this CREATE EXTERNAL TABLE AS SELECT statement is run. Instead, they are specified here so that the database can use them at a later time when it imports data from the external table. Later, when the CREATE TABLE AS SELECT statement selects data from the external table, the database will use the reject options to determine the number or percentage of rows that can fail to import before it stops the import.  
  
 REJECT_VALUE = *reject_value*  
 Specifies the value or the percentage of rows that can fail to import before database halts the import.  
  
 REJECT_TYPE = **value** | percentage  
 Clarifies whether the REJECT_VALUE option is specified as a literal value or a percentage.  
  
 value  
 REJECT_VALUE is a literal value, not a percentage.  The database will stop importing rows from the external data file when the number of failed rows exceeds *reject_value*.  
  
 For example, if REJECT_VALUE = 5 and REJECT_TYPE = value, the database will stop importing rows after 5 rows have failed to import.  
  
 percentage  
 REJECT_VALUE is a percentage, not a literal value. The database will stop importing rows from the external data file when the *percentage* of failed rows exceeds *reject_value*. The percentage of failed rows is calculated at intervals.  
  
 REJECT_SAMPLE_VALUE = *reject_sample_value*  
 Required when REJECT_TYPE = percentage, this specifies the number of rows to attempt to import before the database recalculates the percentage of failed rows.  
  
 For example, if REJECT_SAMPLE_VALUE = 1000, the database will calculate the percentage of failed rows after it has attempted to import 1000 rows from the external data file. If the percentage of failed rows is less than *reject_value*, the database will attempt to load another 1000 rows. The database continues to recalculate the percentage of failed rows after it attempts to import each additional 1000 rows.  
  
> [!NOTE]  
>  Since the database computes the percentage of failed rows at intervals, the actual percentage of failed rows can exceed *reject_value*.  
  
 Example:  
  
 This example shows how the three REJECT options interact with each other. For example, if REJECT_TYPE = percentage, REJECT_VALUE = 30, and REJECT_SAMPLE_VALUE = 100, the following scenario could occur:  
  
-   The database attempts to load the first 100 rows; 25 fail and 75 succeed.  
  
-   Percent of failed rows is calculated as 25%, which is less than the reject value of 30%. So, no need to halt the load.  
  
-   The database attempts to load the next 100 rows; this time 25 succeed and 75 fail.  
  
-   Percent of failed rows is recalculated as 50%. The percentage of failed rows has exceeded the 30% reject value.  
  
-   The load fails with 50% failed rows after attempting to load 200 rows, which is larger than the specified 30% limit.  
  
 WITH *common_table_expression*  
 Specifies a temporary named result set, known as a common table expression (CTE). For more information, see [WITH common_table_expression &#40;Transact-SQL&#41;](../../t-sql/queries/with-common-table-expression-transact-sql.md).  
  
 SELECT \<select_criteria> 
 Populates the new table with the results from a SELECT statement. *select_criteria* is the body of the SELECT statement that determines which data to copy to the new table. For information about SELECT statements, see [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md).  
  
## Permissions  
 To run this command the **database user** needs all of these permissions or memberships:  
  
-   **ALTER SCHEMA** permission on the local schema that will contain the new table or membership in the **db_ddladmin** fixed database role.  
  
-   **CREATE TABLE** permission or membership in the **db_ddladmin** fixed database role.  
  
-   **SELECT** permission on any objects referenced in the *select_criteria*.  
  
 The login needs all of these permissions:  
  
-   **ADMINISTER BULK OPERATIONS** permission  
  
-   **ALTER ANY EXTERNAL DATA SOURCE** permission  
  
-   **ALTER ANY EXTERNAL FILE FORMAT** permission  
  
-   The login must have write permission to read and write to the external folder on the Hadoop Cluster or Azure Storage Blob.  
 
 > [!IMPORTANT]  
 >  The ALTER ANY EXTERNAL DATA SOURCE  permission grants any principal the ability to create and modify any external data source object, and therefore, it also grants the ability to access all database scoped credentials on the database. This permission must be considered as highly privileged, and therefore must be granted only to trusted principals in the system.
  
## Error Handling  
 When CREATE EXTERNAL TABLE AS SELECT exports data to a text-delimited file, there is no rejection file for rows that fail to export.  
  
 When creating the external table, the database attempts to connect to the external Hadoop cluster or Azure Storage Blob. If the connection fails, the command will fail and the external table will not be created. It can take a minute or more for the command to fail since the database retries the connection at least 3 times.  
  
 If CREATE EXTERNAL TABLE AS SELECT is cancelled or fails, the database will make a one-time attempt to remove any new files and folders already created on the external data source.  
  
 The database will report any Java errors that occur on the external data source during the data export.  
  
##  <a name="GeneralRemarks"></a> General Remarks  
 After the CETAS statement finishes, you can run [!INCLUDE[tsql](../../includes/tsql-md.md)] queries on the external table. These operations will import data into the database for the duration of the query unless you import by using the CREATE TABLE AS SELECT statement.  
  
 The external table name and definition are stored in the database metadata. The data is stored in the external data source.  
  
 The external files are named *QueryID_date_time_ID.format*, where *ID* is an incremental identifier and *format* is the exported data format. For example, QID776_20160130_182739_0.orc.  
  
 The CETAS statement always creates a non-partitioned table, even if the source table is partitioned.  
  
 For query plans, created with EXPLAIN, the databaseuses these query plan operations for external tables:  
  
-   External shuffle move  
  
-   External broadcast move  
  
-   External partition move  
  
 **APPLIES TO:**  [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]As a prerequisite for creating an external table, the appliance administrator needs to configure hadoop connectivity. For more information, see Configure Connectivity to External Data (Analytics Platform System) in the APS documentation which you can download from [here](https://www.microsoft.com/download/details.aspx?id=48241).  
  
## Limitations and Restrictions  
 Since external table data resides outside of the database, backup and restore operations will only operate on data stored in the database. This means only the metadata will be backed up and restored.  
  
 The database does not verify the connection to the external data source when restoring a   database backup that contains an external table. If the original source is not accessible, the metadata restore of the external table will still succeed, but SELECT operations on the external table will fail.  
  
 The database does not guarantee data consistency between the databaseand the external data. You, the customer, are solely responsible to maintain consistency between the external data and the database.  
  
 Data manipulation language (DML) operations are not supported on external tables. For example, you cannot use the [!INCLUDE[tsql](../../includes/tsql-md.md)] update, insert, or delete [!INCLUDE[tsql](../../includes/tsql-md.md)]statements to modify the external data.  
  
 CREATE TABLE, DROP TABLE, CREATE STATISTICS, DROP STATISTICS, CREATE VIEW, and DROP VIEW are the only data definition language (DDL) operations allowed on external tables.  
  
 PolyBase can consume a maximum of 33k files per folder when running 32 concurrent PolyBase queries. This maximum number includes both files and subfolders in each HDFS folder. If the degree of concurrency is less than 32, a user can run PolyBase queries against folders in HDFS which contain more than 33k files. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends users of Hadoop and PolyBase keep file paths short and use no more than 30k files per HDFS folder. When too many files are referenced a JVM out-of-memory exception occurs.  
  
 [SET ROWCOUNT &#40;Transact-SQL&#41;](../../t-sql/statements/set-rowcount-transact-sql.md) has no effect on this CREATE EXTERNAL TABLE AS SELECT. To achieve a similar behavior, use [TOP &#40;Transact-SQL&#41;](../../t-sql/queries/top-transact-sql.md).  
  
 When CREATE EXTERNAL TABLE AS SELECT selects from an RCFile, the column values in the RCFile must not contain the pipe '|' character.  
  
## Locking  
 Takes a shared lock on the SCHEMARESOLUTION object.  
  
##  <a name="Examples"></a> Examples  
  
### A. Create a Hadoop table using CREATE EXTERNAL TABLE AS SELECT (CETAS)  
 The following example creates a new external table named `hdfsCustomer`, using the column definitions and data from the source table `dimCustomer`.  
  
 The table definition is stored in the database, and the results of the SELECT statement are exported to the '/pdwdata/customer.tbl' file on the Hadoop external data source *customer_ds*. The file is formatted according to the external file format *customer_ff*.  
  
 The file name is generated by the database, and contains the query ID for ease of aligning the file with the query that generated it.  
  
 The path `hdfs://xxx.xxx.xxx.xxx:5000/files/` preceding the Customer directory must already exist. However, if the Customer directory does not exist, the database will create the directory.  
  
> [!NOTE]  
>  This example specifies for 5000. If the port is not specified, the database uses 8020 as the default port.  
  
 The resulting Hadoop location and file name will be `hdfs:// xxx.xxx.xxx.xxx:5000/files/Customer/ QueryID_YearMonthDay_HourMinutesSeconds_FileIndex.txt.`.  
  
```  
  
      -- Example is based on AdventureWorks   
CREATE EXTERNAL TABLE hdfsCustomer  
WITH (  
        LOCATION='/pdwdata/customer.tbl',  
        DATA_SOURCE = customer_ds,  
        FILE_FORMAT = customer_ff  
) AS SELECT * FROM dimCustomer;  
```  
  
### B. Use a Query Hint with CREATE EXTERNAL TABLE AS SELECT (CETAS)  
 This query shows the basic syntax for using a query join hint with the CETAS statement. After the query is submitted the database uses the hash join strategy to generate the query plan. For more information on join hints and how to use the OPTION clause, see [OPTION Clause &#40;Transact-SQL&#41;](../../t-sql/queries/option-clause-transact-sql.md).  
  
> [!NOTE]  
>  This example specifies for 5000. If the port is not specified, the database uses 8020 as the default port.  
  
```  
  
      -- Example is based on AdventureWorks  
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
 [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md)   
 [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)   
 [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)   
 [CREATE TABLE &#40;Azure SQL Data Warehouse, Parallel Data Warehouse&#41;](~/t-sql/statements/create-table-azure-sql-data-warehouse.md)   
 [CREATE TABLE AS SELECT &#40;Azure SQL Data Warehouse&#41;](../../t-sql/statements/create-table-as-select-azure-sql-data-warehouse.md)   
 [DROP TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-table-transact-sql.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
  
  


