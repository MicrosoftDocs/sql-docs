---
title: "CREATE EXTERNAL TABLE AS SELECT (Transact-SQL)"
description: CREATE EXTERNAL TABLE AS SELECT (Transact-SQL)
author: ronortloff
ms.author: rortloff
ms.date: "10/20/2021"
ms.prod: sql
ms.prod_service: "synapse-analytics, pdw"
ms.topic: reference
f1_keywords:
  - "CREATE EXTERNAL TABLE AS SELECT"
  - "CREATE_EXTERNAL_TABLE_AS_SELECT"
  - "CREATE EXTERNAL TABLE AS SELECT_TSQL"
helpviewer_keywords:
  - "External, table"
  - "PolyBase, external table"
  - "External, table create as select"
  - "PolyBase, create table as select"
dev_langs:
  - "TSQL"
ms.assetid: 32dfe254-6df7-4437-bfd6-ca7d37557b0a
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# CREATE EXTERNAL TABLE AS SELECT (Transact-SQL)
[!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)]

[!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] introduces the ability to use Create External Table as Select (CETAS), creates an external table and then exports, in parallel, the result of a [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT statement to Azure Data Lake Storage Gen2, Azure Storage Account V2, and S3-compliant object storage.

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql 
CREATE EXTERNAL TABLE {[ [database_name  . [ schema_name ] . ] | schema_name . ] table_name }
    [(column_name [,...n ] ) ]
    WITH (   
        LOCATION = '<prefix>://<path>[:<port>]',  
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
 **[ [ *database_name* . [ *schema_name* ] . ] | *schema_name* . ] *table_name***
 is the one- to three-part name of the table to create in the database. For an external table, only the table metadata is stored in the relational database.
 
  **[ ( column_name [ ,...n ] ) ]**
  is the name of a table column.
  
  **LOCATION =  'prefix://path[:port]'** provides the connectivity protocol (prefix), path and optionally the port, to the external data source, where the result of the SELECT statement will write.
  
  If the destination is a S3-compliant object storage a bucket must first exist, but Polybase can create sub-folders if required. SQL Server 2022 supports Azure Data Lake Storage Gen2, Azure Storage Account V2, and S3-compliant object storage.

 **DATA_SOURCE = *external_data_source_name***
 specifies the name of the external data source object that contains the location where the external data is stored or will be stored. The location is either a Hadoop cluster or an Azure Blob storage. To create an external data source, use [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md).

 **FILE_FORMAT = *external_file_format_name***
 specifies the name of the external file format object that contains the format for the external data file. To create an external file format, use [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md).

 **REJECT options** don't apply at the time this CREATE EXTERNAL TABLE AS SELECT statement is run. Instead, they're specified here so that the database can use them at a later time when it imports data from the external table. Later, when the CREATE TABLE AS SELECT statement selects data from the external table, the database will use the reject options to determine the number or percentage of rows that can fail to import before it stops the import.

   - **REJECT_VALUE = *reject_value***
 specifies the value or the percentage of rows that can fail to import before the database halts the import.

   - **REJECT_TYPE = **value** | percentage**
 clarifies whether the REJECT_VALUE option is specified as a literal value or a percentage.

      - **Value**
 is used if REJECT_VALUE is a literal value, not a percentage. The database will stop importing rows from the external data file when the number of failed rows exceeds *reject_value*.

        For example, if REJECT_VALUE = 5 and REJECT_TYPE = value, the database will stop importing rows after five rows have failed to import.

      - **Percentage**
 is used if REJECT_VALUE is a percentage, not a literal value. The database will stop importing rows from the external data file when the *percentage* of failed rows exceeds *reject_value*. The percentage of failed rows is calculated at intervals.

   - **REJECT_SAMPLE_VALUE = *reject_sample_value***
 is required when REJECT_TYPE = percentage, this specifies the number of rows to attempt to import before the database recalculates the percentage of failed rows.

      For example, if REJECT_SAMPLE_VALUE = 1000, the database will calculate the percentage of failed rows after it has attempted to import 1000 rows from the external data file. If the percentage of failed rows is less than *reject_value*, the database will attempt to load another 1000 rows. The database continues to recalculate the percentage of failed rows after it attempts to import each additional 1000 rows.

     > [!NOTE]
     >  Because the database computes the percentage of failed rows at intervals, the actual percentage of failed rows can exceed *reject_value*.

     **Example:**

     This example shows how the three REJECT options interact with each other. For example, if REJECT_TYPE = percentage, REJECT_VALUE = 30, and REJECT_SAMPLE_VALUE = 100, the following scenario could occur:

     - The database attempts to load the first 100 rows, of which 25 fail and 75 succeed.
     - The percent of failed rows is calculated as 25%, which is less than the reject value of 30%. So, there's no need to halt the load.
     - The database attempts to load the next 100 rows. This time 25 succeed and 75 fail.
     - The percent of failed rows is recalculated as 50%. The percentage of failed rows has exceeded the 30% reject value.
     - The load fails with 50% failed rows after attempting to load 200 rows, which is larger than the specified 30% limit.

 **WITH *common_table_expression***
 specifies a temporary named result set, known as a common table expression (CTE). For more information, see [WITH common_table_expression &#40;Transact-SQL&#41;](../../t-sql/queries/with-common-table-expression-transact-sql.md) 

 **SELECT \<select_criteria>**
 populates the new table with the results from a SELECT statement. *select_criteria* is the body of the SELECT statement that determines which data to copy to the new table. For information about SELECT statements, see [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md).

**Column options**

column_name [ ,...n ]
Column names do not allow the column options mentioned in CREATE TABLE. Instead, you can provide an optional list of one or more column names for the new table. The columns in the new table will use the names you specify. When you specify column names, the number of columns in the column list must match the number of columns in the select results. If you don't specify any column names, the new target table will use the column names in the select statement results.

You cannot specify any other column options such as data types, collation, or nullability. Each of these attributes is derived from the results of the SELECT statement. However, you can use the SELECT statement to change the attributes. For an example, see [Use CETAS to change column attributes](#c-use-cetas-to-change-column-attributes).

## Permissions

 To run this command, the *database user* needs all of these permissions or memberships:

- **ALTER SCHEMA** permission on the local schema that will contain the new table or membership in the **db_ddladmin** fixed database role.
- **CREATE TABLE** permission or membership in the **db_ddladmin** fixed database role.
- **SELECT** permission on any objects referenced in the *select_criteria*.

 The login needs all of these permissions:

- **ADMINISTER BULK OPERATIONS**
- **ALTER ANY EXTERNAL DATA SOURCE**
- **ALTER ANY EXTERNAL FILE FORMAT**
- **Write** permission to read and write to the external location.
 
**The option *'allow polybase export'* must also be enabled on SP_CONFIGURE.**

 > [!IMPORTANT]
 >  The ALTER ANY EXTERNAL DATA SOURCE permission grants any principal the ability to create and modify any external data source object, so it also grants the ability to access all database scoped credentials on the database. This permission must be considered as highly privileged and must be granted only to trusted principals in the system.

## Error handling
 When CREATE EXTERNAL TABLE AS SELECT exports data to a text-delimited file, there's no rejection file for rows that fail to export.

 When you create the external table, the database attempts to connect to the external location. If the connection fails, the command will fail and the external table won't be created. It can take a minute or more for the command to fail because the database retries the connection at least three times.

 If CREATE EXTERNAL TABLE AS SELECT is canceled or fails, the database will make a one-time attempt to remove any new files and folders already created on the external data source.

##  <a name="GeneralRemarks"></a> General remarks
 After the CREATE EXTERNAL TABLE AS SELECT statement finishes, you can run [!INCLUDE[tsql](../../includes/tsql-md.md)] queries on the external table. These operations will import data into the database for the duration of the query unless you import by using the CREATE TABLE AS SELECT statement.

 The external table name and definition are stored in the database metadata. The data is stored in the external data source.

  The CREATE EXTERNAL TABLE AS SELECT statement always creates a nonpartitioned table, even if the source table is partitioned.

 For query plans, created with EXPLAIN, the database uses these query plan operations for external tables:

## Limitations and restrictions
 Because external table data resides outside of the database, backup and restore operations will only operate on data stored in the database. As a result, only the metadata will be backed up and restored.

 The database doesn't verify the connection to the external data source when restoring a database backup that contains an external table. If the original source isn't accessible, the metadata restore of the external table will still succeed, but SELECT operations on the external table will fail.

 The database doesn't guarantee data consistency between the database and the external data. You, the customer, are solely responsible to maintain consistency between the external data and the database.

 Data manipulation language (DML) operations aren't supported on external tables. For example, you can't use the [!INCLUDE[tsql](../../includes/tsql-md.md)] update, insert, or delete [!INCLUDE[tsql](../../includes/tsql-md.md)]statements to modify the external data.

 CREATE TABLE, DROP TABLE, CREATE STATISTICS, DROP STATISTICS, CREATE VIEW, and DROP VIEW are the only data definition language (DDL) operations allowed on external tables.

  [SET ROWCOUNT &#40;Transact-SQL&#41;](../../t-sql/statements/set-rowcount-transact-sql.md) has no effect on this CREATE EXTERNAL TABLE AS SELECT. To achieve a similar behavior, use [TOP &#40;Transact-SQL&#41;](../../t-sql/queries/top-transact-sql.md).

 CREATE EXTERNAL TABLE AS SELECT to Parquet  will cause errors, which can include rejected records when the following characters are present in the data:

- |
- “ (quotation mark character)
- /r/n
- /r
- /n

To use CREATE EXTERNAL TABLE AS SELECT containing these characters, you must first run the CREATE EXTERNAL TABLE AS SELECT statement to export the data to delimited text files where you can then convert them to Parquet by using an external tool.

## Locking
 Takes a shared lock on the SCHEMARESOLUTION object.

##  <a name="Examples"></a> Examples

### D. Use CREATE EXTERNAL TABLE AS SELECT exporting data as parquet

 The following example creates a new external table named `ext_sales` that uses the data from the table `SalesOrderDetail` of `AdventureWorks2019` database.

The result of the SELECT statement will be saved on an S3-compliant object storage previously configured and named `s3_eds`, and proper credential created as `s3_dsc`. The parquet file location will be `<ip>:<port>/cetas/sales.parquet` where `cetas` is the previously created storage bucket.

> [!NOTE]
>Delta format is only supported on SQL Server 2022 as Read-only.

```sql  
-- Credential to access the s3-compliant object storage
CREATE DATABASE SCOPED CREDENTIAL s3_dsc
WITH IDENTITY = 'S3 Access Key',
SECRET = '<accesskeyid>:<secretkeyid>'
GO

-- S3-compliant object storage data source
CREATE EXTERNAL DATA SOURCE s3_eds
WITH
( LOCATION = 's3://<ip>:<port>'
,CREDENTIAL = s3_dsc)

-- External File Format for PARQUET
CREATE EXTERNAL FILE FORMAT ParquetFileFormat WITH(FORMAT_TYPE = PARQUET);
GO

CREATE EXTERNAL TABLE ext_sales
WITH 
(   LOCATION = '/cetas/sales.parquet',
    DATA_SOURCE = s3_eds,  
    FILE_FORMAT = ParquetFileFormat
) AS SELECT * FROM ADVENTUREWORKS2019.[SALES].[SALESORDERDETAIL]
```

### E. Use CREATE EXTERNAL TABLE AS SELECT from delta table to parquet

The following example creates a new external table named `Delta_to_Parquet`, that uses Delta Table type of data located at an S3-Compliant object storage named `s3_delta`, and writes the result in another data source named `s3_parquet` as a parquet file. For that the example makes uses of OPENROWSET command.

```sql
-- External File Format for PARQUET
CREATE EXTERNAL FILE FORMAT ParquetFileFormat WITH(FORMAT_TYPE = PARQUET);
GO

CREATE EXTERNAL TABLE Delta_to_Parquet
WITH 
(   LOCATION = '/backup/sales.parquet',
    DATA_SOURCE = s3_parquet,  
    FILE_FORMAT = ParquetFileFormat
) AS 
SELECT * FROM OPENROWSET
(BULK '/delta/sales_fy22/', FORMAT = 'DELTA', DATA_SOURCE = 's3_delta') as [r]
```

## See also
 - [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md)
 - [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)
 - [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)
 - [CREATE TABLE &#40;Azure Synapse Analytics, Parallel Data Warehouse&#41;](~/t-sql/statements/create-table-azure-sql-data-warehouse.md)
 - [CREATE TABLE AS SELECT &#40;Azure Synapse Analytics&#41;](../../t-sql/statements/create-table-as-select-azure-sql-data-warehouse.md)
 - [DROP TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-table-transact-sql.md)
 - [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)



