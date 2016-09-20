---
title: "CREATE TABLE AS SELECT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 95981667-5eb5-40d1-af3a-0423bf231c13
caps.latest.revision: 41
author: BarbKess
---
# CREATE TABLE AS SELECT (SQL Server PDW)
Creates a new table in SQL Server PDW that is populated with the results from a SELECT statement. Use this statement to copy data from one or more existing tables to a new table.  
  
For example, use the Create Table as Select (CTAS) statement to:  
  
-   Re-create the table with a  different hash distribution column.  
  
-   Create a clustered columnstore index on a subset of the columns in the table.  
  
-   Import data from an external table.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE TABLE [ database_name . [ schema_name ] . | schema_name. ] table_name   
        [ ( { column_name } [ ,...n ] ) ]  
    WITH (   
        DISTRIBUTION = { HASH( distribution_column_name ) | REPLICATE | ROUND_ROBIN }   
            [ , <CTAS_table_option> [ ,...n ] ]    
    )  
    AS <select_statement>   
[;]  
  
<CTAS_table_option> ::=  
    LOCATION = USER_DB  
    | CLUSTERED COLUMNSTORE INDEX  
    | HEAP  
    | CLUSTERED INDEX ( { index_column_name [ ASC | DESC ] } [ ,...n ] )  
    | PARTITION( partition_column_name RANGE [ LEFT | RIGHT  
        FOR VALUES ( [ boundary_value [,...n] ] ) ) ]  
  
<select_statement> ::=  
    [ WITH <common_table_expression> [ ,...n ] ]  
    SELECT <select_criteria>  
```  
  
## Arguments  
*database_name*  
The name of the database that will contain the new table. The default is the current database.  
  
*schema_name*  
The schema of the new table. Specifying *schema* is optional.  
  
*table_name*  
The name of the new table.  
  
To create a local temporary table:  
  
1.  Specify the table name with 3-part naming.  
  
2.  Precede the table name with #.  
  
3.  Specify the `LOCATION = USER_DB` option.  
  
For more details on permitted table names, see [Object Naming Rules &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/object-naming-rules-sql-server-pdw.md)  
  
*column_name*  [, …]  
The list of one or more column names for the new table. For details on permitted column names, see [Object Naming Rules &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/object-naming-rules-sql-server-pdw.md).  
  
The column list is optional for the CTAS statement. When columns are listed, the column names do not need to match the column names in the select results. However, the number of columns in the column list must match the number of columns in the select results. You cannot specify NULL | NOT NULL for the columns in the CTAS statement; the nullability property is derived from the columns and expressions in the SELECT results.  
  
LOCATION = USER_DB  
Table is located in the user database. This option is required and used only when creating a temporary table. This option is not allowed when the table is not a temporary table.  
  
**CLUSTERED COLUMNSTORE INDEX**  
Creates a columnstore index that contains the columns listed in the select statement, and uses the column data types as they are defined in the source table.  
  
For best performance and data compression, we recommend using the clustered columnstore index instead of the clustered index.  
  
CLUSTERED INDEX ( *index_column_name* [,…n] )  
Creates a clustered index on one or more columns, called key columns.  This stores the data by row. Use *index_column_name* to specify the name of one or more key columns in the index.  
  
For best performance and data compression, we recommend using the clustered columnstore index instead of the clustered index.  
  
DISTRIBUTION = { HASH ( *distribution_column_name* ) | **REPLICATE** | ROUND_ROBIN}  
The physical data storage method. This option is required for the CTAS statement.  
  
HASH ( *distribution_column_name* )  
Distributes table rows across the Compute nodes. Assigns each row to one distribution by hashing the value in *distribution_column_name*.  For more information about how to choose a hash distributed column, see [Distributed and Replicated Tables &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/distributed-and-replicated-tables-sql-server-pdw.md).  
  
REPLICATE  
Stores each table in full once per Compute node.  
  
ROUND_ROBIN  
Distributes the rows evenly across all the distributions in a round-robin fashion.  
  
PARTITION ( *partition_column_name* RANGE [ **LEFT** | RIGHT ] FOR VALUES ( [ *boundary_value* [,...n] ] ))  
Creates one or more table partitions, which are horizontal table slices that allow you to perform operations on subsets of rows. Unlike the distribution column, table partitions do not determine the distribution where each row is stored. Instead, table partitions determine how the rows are grouped and stored within each distribution.  
  
*partition_column_name*  
Specifies the column to partition the rows on. This column can be any data type. SQL Server PDW sorts the partition column values in ascending order. The low-to-high ordering goes from LEFT to RIGHT for the purpose of the RANGE specification.  
  
RANGE [ **LEFT** or RANGE RIGHT ]  
Specifies whether the boundary value belongs to the partition on the left (lower values) or the partition on the right (higher values). The default is LEFT.  
  
FOR VALUES( *boundary_value* \[WITH common_table_expression &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/with-common-table-expression-sql-server-pdw.md).  
  
SELECT <select_criteria>  
Populates the new table with the results from a SELECT statement. *select_criteria* is the body of the SELECT statement that determines which data to copy to the new table. For information about SELECT statements, see [SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/select-sql-server-pdw.md).  
  
## Permissions  
Requires membership in the **db_ddladmin** fixed database role, or:  
  
-   **CREATE TABLE** permission on the database, and  
  
-   **ALTER SCHEMA** permission on the schema that will contain the new table  
  
Also requires **SELECT** permission on any objects referenced in the *select_criteria*.  
  
## <a name="GeneralRemarks"></a>General Remarks  
For more information about creating tables and temporary tables, see [CREATE TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-table-sql-server-pdw.md)  
  
The CTAS statement creates a non-partitioned table by default, even if the source table is partitioned. To create a partitioned table with the CTAS statement, you must specify the partition option.  
  
## Limitations and Restrictions  
[SET ROWCOUNT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/set-rowcount-sql-server-pdw.md) has no effect on this statement. To achieve a similar behavior, use [TOP &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/top-sql-server-pdw.md).  
  
## Locking  
Takes an exclusive lock on the table. Takes a shared lock on the DATABASE, SCHEMA, and SCHEMARESOLUTION objects.  
  
## <a name="Examples"></a>Examples  
  
### A. Create a distributed table by using CREATE TABLE AS SELECT (CTAS)  
The following example creates a new table named `myTable_hd`, using the column definitions and data from the source table `dimCustomer`.  
  
```  
USE AdventureWorksPDW2012;  
CREATE TABLE myTable_hd   
WITH   
  (   
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = HASH (CustomerKey)  
  )  
AS SELECT * FROM dimCustomer;  
```  
  
To create a distributed table using the round-robin distribution strategy, set `DISTRIBUTION = ROUND_ROBIN`.  
  
```  
USE AdventureWorksPDW2012;  
CREATE TABLE myTable_rr  
WITH   
  (   
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = ROUND_ROBIN  
  )  
AS SELECT * FROM dimCustomer;  
```  
  
### B. Create a replicated table by using CREATE TABLE AS SELECT (CTAS)  
The following example creates a replicated table. This will store a copy of the table on each distribution.  This also demonstrates that the column aliases in the SELECT statement are the names of the columns in the new table.  
  
```  
USE AdventureWorksPDW2012;   
CREATE TABLE myTable  
WITH   
  (   
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = ROUND_ROBIN  
  )  
AS SELECT CustomerKey AS c, LastName AS ln  
    FROM dimCustomer;  
```  
  
### C. Use a Query Hint with CREATE TABLE AS SELECT (CTAS)  
This query shows the basic syntax for using a query join hint with the CTAS statement. After the query is submitted to the Control node, SQL Server, running on the Compute nodes, will apply the hash join strategy when generating the SQL Server query plan. For more information on join hints and how to use the OPTION clause, see [OPTION &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/option-sql-server-pdw.md).  
  
```  
USE AdventureWorksPDW2012;  
CREATE TABLE dbo.FactInternetSalesNew  
WITH   
  (   
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = REPLICATE   
  )  
AS SELECT T1.* FROM dbo.FactInternetSales T1 JOIN dbo.DimCustomer T2  
ON ( T1.CustomerKey = T2.CustomerKey )  
OPTION ( HASH JOIN );  
```  
  
### D. Use CREATE TABLE AS SELECT (CTAS) to change collations  
This example uses CTAS to change the collation of all the columns in the DatabaseLog table to Serbian_Cyrillic_100_BIN. The example puts the results of the CTAS into the DatabaseLog2 table  
  
```  
/* CTAS the DatabaseLog table to DatabaseLog2 table with different collation */  
USE AdventureWorksPDW2012;  
If EXISTS (SELECT * from sys.tables t where t.name = 'DatabaseLog2')  
DROP TABLE [DatabaseLog2];  
  
CREATE TABLE DatabaseLog2  
WITH   
  (   
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = REPLICATE   
  )  
AS SELECT   
    [DatabaseUser] COLLATE Serbian_Cyrillic_100_BIN AS [DatabaseUser],  
    [Event] COLLATE Serbian_Cyrillic_100_BIN AS [Event],  
    [Schema] COLLATE Serbian_Cyrillic_100_BIN AS [Schema],  
    [Object] COLLATE Serbian_Cyrillic_100_BIN AS [Object],  
    [TSQL] COLLATE Serbian_Cyrillic_100_BIN AS [TSQL]  
FROM DatabaseLog;  
```  
  
### E. Use CREATE TABLE AS SELECT to import Hadoop data from an external table  
To import data from an external table, simply use CREATE TABLE AS SELECT to select from the external table. The syntax to select data from an external table into SQL Server PDW is the same as the syntax for selecting data from a distributed or replicated table.  
  
The following example defines an external table on a Hadoop cluster. It then uses CREATE TABLE AS SELECT to select from the external table. This imports the data from Hadoop text-delimited files and stores the data into a new SQL Server PDW table.  
  
```  
--Create the external table called ClickStream.  
CREATE EXTERNAL TABLE ClickStreamExt (   
    url varchar(50),  
    event_date date,  
    user_IP varchar(50)  
)  
WITH (  
    LOCATION = 'hdfs://MyHadoop:5000/tpch1GB/employee.tbl',  
    FORMAT_OPTIONS ( FIELD_TERMINATOR = '|')  
)  
;  
  
--Use your own processes to create the Hadoop text-delimited files on the Hadoop Cluster.  
  
--Use CREATE TABLE AS SELECT to import the Hadoop data into a new   
--PDW table called ClickStreamPDW  
CREATE TABLE ClickStreamPDW   
WITH  
  (  
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = HASH (user_IP)  
  )  
AS SELECT * FROM ClickStreamExt  
;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-table-sql-server-pdw.md)  
[CREATE EXTERNAL TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-external-table-sql-server-pdw.md)  
[CREATE EXTERNAL TABLE AS SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-external-table-as-select-sql-server-pdw.md)  
[DROP TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-table-sql-server-pdw.md)  
[ALTER TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-table-sql-server-pdw.md)  
  
