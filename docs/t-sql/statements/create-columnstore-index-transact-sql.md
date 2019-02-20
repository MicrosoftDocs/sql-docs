---
title: "CREATE COLUMNSTORE INDEX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/13/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE_COLUMNSTORE_INDEX_TSQL"
  - "COLUMNSTORE INDEX"
  - "COLUMNSTORE_INDEX_TSQL"
  - "CREATE CLUSTERED COLUMNSTORE INDEX"
  - "COLUMNSTORE_TSQL"
  - "CREATE NONCLUSTERED COLUMNSTORE INDEX"
  - "CREATE_NONCLUSTERED_COLUMNSTORE_INDEX_TSQL"
  - "CREATE COLUMNSTORE INDEX"
  - "CREATE_CLUSTERED_COLUMNSTORE_INDEX_TSQL"
  - "COLUMNSTORE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "index creation [SQL Server], columnstore indexes"
  - "columnstore index, creating"
  - "CREATE COLUMNSTORE INDEX statement"
  - "CREATE INDEX statement"
ms.assetid: 7e1793b3-5383-4e3d-8cef-027c0c8cb5b1
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE COLUMNSTORE INDEX (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-all-md](../../includes/tsql-appliesto-ss2012-all-md.md)]

Convert a rowstore table to a clustered columnstore index or create a nonclustered columnstore index. Use a columnstore index to efficiently run real-time operational analytics on an OLTP workload or to improve data compression and query performance for data warehousing workloads.  
  
> [!NOTE]  
> Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can create the table as a clustered columnstore index.   It is no longer necessary to first create a rowstore table and then convert it to a clustered columnstore index.  

> [!TIP]
> For information on index design guidelines, refer to the [SQL Server Index Design Guide](../../relational-databases/sql-server-index-design-guide.md).

Skip to examples:  
-   [Examples for converting a rowstore table to columnstore](../../t-sql/statements/create-columnstore-index-transact-sql.md#convert)  
-   [Examples for nonclustered columnstore indexes](../../t-sql/statements/create-columnstore-index-transact-sql.md#nonclustered)  
  
Go to scenarios:  
-   [Columnstore indexes for real-time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md)  
-   [Columnstore indexes for data warehousing](../../relational-databases/indexes/columnstore-indexes-data-warehouse.md)  
  
Learn more:  
-   [Columnstore indexes guide](../../relational-databases/indexes/columnstore-indexes-overview.md)  
-   [Columnstore indexes feature summary](../../relational-databases/indexes/columnstore-indexes-what-s-new.md)  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database  
  
-- Create a clustered columnstore index on disk-based table.  
CREATE CLUSTERED COLUMNSTORE INDEX index_name  
    ON [database_name. [schema_name ] . | schema_name . ] table_name  
    [ WITH ( < with_option> [ ,...n ] ) ]  
    [ ON <on_option> ]  
[ ; ]  
  
--Create a non-clustered columnstore index on a disk-based table.  
CREATE [NONCLUSTERED]  COLUMNSTORE INDEX index_name   
    ON [database_name. [schema_name ] . | schema_name . ] table_name   
        ( column  [ ,...n ] )  
    [ WHERE <filter_expression> [ AND <filter_expression> ] ]
    [ WITH ( < with_option> [ ,...n ] ) ]  
    [ ON <on_option> ]   
[ ; ]  
  
<with_option> ::=  
      DROP_EXISTING = { ON | OFF } -- default is OFF  
    | MAXDOP = max_degree_of_parallelism 
    | ONLINE = { ON | OFF } 
    | COMPRESSION_DELAY  = { 0 | delay [ Minutes ] }  
    | DATA_COMPRESSION = { COLUMNSTORE | COLUMNSTORE_ARCHIVE }  
      [ ON PARTITIONS ( { partition_number_expression | range } [ ,...n ] ) ]  
  
<on_option>::=  
      partition_scheme_name ( column_name )   
    | filegroup_name   
    | "default"   
  
<filter_expression> ::=  
      column_name IN ( constant [ ,...n ]  
    | column_name { IS | IS NOT | = | <> | != | > | >= | !> | < | <= | !< } constant  
  
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
CREATE CLUSTERED COLUMNSTORE INDEX index_name   
    ON [ database_name . [ schema_name ] . | schema_name . ] table_name  
    [ WITH ( DROP_EXISTING = { ON | OFF } ) ] --default is OFF  
[;]  
```  
  
## Arguments  

Some of the options are not available in all database engine versions. The following table shows the versions when the options are introduced in CLUSTERED COLUMNSTORE and NONCLUSTERED COLUMNSTORE indexes:

|Option| CLUSTERED | NONCLUSTERED |
|---|---|---|
| COMPRESSION_DELAY | [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] | [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] |
| DATA_COMPRESSION | [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] | [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] | 
| ONLINE | [!INCLUDE[ssSQLv15_md](../../includes/sssqlv15-md.md)] | [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] |
| WHERE clause | N/A | [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] |

All options are available in Azure SQL Database.

### CREATE CLUSTERED COLUMNSTORE INDEX  
Create a clustered columnstore index in which all of the data is compressed and stored by column. The index includes all of the columns in the table, and stores the entire table. If the existing table is a heap or clustered index, the table is converted to a clustered columnstore index. If the table is already stored as a clustered columnstore index, the existing index is dropped and rebuilt.  
  
*index_name*  
Specifies the name for the new index.  
  
If the table already has a clustered columnstore index, you can specify the same name as the existing index, or you can use the DROP EXISTING option to specify a new name.  
  
ON [*database_name*. [*schema_name* ] . | *schema_name* . ] *table_name*  
   Specifies the one-, two-, or three-part name of the table to be stored as a clustered columnstore index. If the table is a heap or clustered index the table is converted from rowstore to a columnstore. If the table is already a columnstore, this statement rebuilds the clustered columnstore index.  
  
#### WITH options  
##### DROP_EXISTING = [OFF] | ON  
   `DROP_EXISTING = ON` specifies to drop the existing index, and create a new columnstore index.  
```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
       WITH (DROP_EXISTING = ON);
```
   The default, DROP_EXISTING = OFF expects the index name is the same as the existing name. An error occurs is the specified index name already exists.  
  
##### MAXDOP = *max_degree_of_parallelism*  
   Overrides the existing maximum degree of parallelism server configuration for the duration of the index operation. Use MAXDOP to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.  
  
   *max_degree_of_parallelism* values can be:  
   - 1 - Suppress parallel plan generation.  
   - \>1 - Restrict the maximum number of processors used in a parallel index operation to the specified number or fewer based on the current system workload. For example, when MAXDOP = 4, the number of processors used is 4 or less.  
   - 0 (default) - Use the actual number of processors or fewer based on the current system workload.  
  
```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
       WITH (MAXDOP = 2);
```
   For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md), and [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).  
 
###### COMPRESSION_DELAY = **0** | *delay* [ Minutes ]  
   For a disk-based table, *delay* specifies the minimum number of minutes a  delta rowgroup in the CLOSED state must remain in the delta rowgroup before SQL Server can compress it into the compressed rowgroup. Since disk-based tables don't track insert and update times on individual rows, SQL Server applies the delay to delta rowgroups in the CLOSED state.  
   The default is 0 minutes.  
   
```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
       WITH ( COMPRESSION_DELAY = 10 Minutes );
```

   For recommendations on when to use COMPRESSION_DELAY, see [Get started with Columnstore for real time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md).  
  
##### DATA_COMPRESSION = COLUMNSTORE | COLUMNSTORE_ARCHIVE  
   Specifies the data compression option for the specified table, partition number, or range of partitions. The options are as follows:   
- `COLUMNSTORE` is the default and specifies to compress with the most performant columnstore compression. This is the typical choice.  
- `COLUMNSTORE_ARCHIVE` further compresses the table or partition to a smaller size. Use this option for situations such as archival that require a smaller storage size and can afford more time for storage and retrieval.  
  
```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
       WITH ( DATA_COMPRESSION = COLUMNSTORE_ARCHIVE );
```
   For more information about compression, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  

###### ONLINE = [ON | OFF]
- `ON` specifies that the columnstore index remains online and available while the new copy of the index is being built.
- `OFF` specifies that the index is not available for use while the new copy is being built.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
       WITH ( ONLINE = ON );
```

#### ON options 
   With the ON options you can specify options for data storage, such as a partition scheme, a specific filegroup, or the default filegroup. If the ON option is not specified, the index uses the settings partition or filegroup settings of the existing table.  
  
   *partition_scheme_name* **(** _column_name_ **)**  
   Specifies the partition scheme for the table. The partition scheme must already exist in the database. To create the partition scheme, see [CREATE PARTITION SCHEME](../../t-sql/statements/create-partition-scheme-transact-sql.md).  
 
   *column_name* specifies the column against which a partitioned index is partitioned. This column must match the data type, length, and precision of the argument of the partition function that *partition_scheme_name* is using.  

   *filegroup_name*  
   Specifies the filegroup for storing the clustered columnstore index. If no location is specified and the table is not partitioned, the index uses the same filegroup as the underlying table or view. The filegroup must already exist.  

   **"**default**"**  
   To create the index on the default filegroup, use "default" or [ default ].  
  
   If "default" is specified, the QUOTED_IDENTIFIER option must be ON for the current session. QUOTED_IDENTIFIER is ON by default. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
  
### CREATE [NONCLUSTERED] COLUMNSTORE INDEX  
Create an in-memory nonclustered columnstore index on a rowstore table stored as a heap or clustered index. The index can have a filtered condition and does not need to include all of the columns of the underlying table. The columnstore index requires enough space to store a copy of the data. It is updateable and is updated as the underlying table is changed. The nonclustered columnstore index on a clustered index enables real-time analytics.  
  
*index_name*  
   Specifies the name of the index. *index_name*  must be unique within the table, but does not have to be unique within the database. Index names must follow the rules of [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 **(** _column_  [ **,**...*n* ] **)**  
    Specifies the columns to store. A nonclustered columnstore index is limited to 1024 columns.  
   Each column must be of a supported data type for columnstore indexes. See [Limitations and Restrictions](../../t-sql/statements/create-columnstore-index-transact-sql.md#LimitRest) for a list of the supported data types.  

ON [*database_name*. [*schema_name* ] . | *schema_name* . ] *table_name*  
   Specifies the one-, two-, or three-part name of the table that contains the index.  

#### WITH options
##### DROP_EXISTING = [OFF] | ON  
   DROP_EXISTING = ON The existing index is dropped and rebuilt. The index name specified must be the same as a currently existing index; however, the index definition can be modified. For example, you can specify different columns, or index options.
  
   DROP_EXISTING = OFF An error is displayed if the specified index name already exists. The index type cannot be changed by using DROP_EXISTING. In backward compatible syntax, WITH DROP_EXISTING is equivalent to WITH DROP_EXISTING = ON.  

###### MAXDOP = *max_degree_of_parallelism*  
   Overrides the [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md) configuration option for the duration of the index operation. Use MAXDOP to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.  
  
   *max_degree_of_parallelism* values can be:  
   - 1 - Suppress parallel plan generation.  
   - \>1 - Restrict the maximum number of processors used in a parallel index operation to the specified number or fewer based on the current system workload. For example, when MAXDOP = 4, the number of processors used is 4 or less.  
   - 0 (default) - Use the actual number of processors or fewer based on the current system workload.  
  
   For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).  
  
> [!NOTE]
>  Parallel index operations are not available in every edition of [!INCLUDE[msC](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and Supported Features for SQL Server 2016](../../sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
###### ONLINE = [ON | OFF]   
- `ON` specifies that the columnstore index remains online and available while the new copy of the index is being built.
- `OFF` specifies that the index is not available for use while the new copy is being built. In nonclustered index, the base table remains available, only the nonclustered columnstore index is not used to satisfy queries until the new index is complete. 

```sql
CREATE COLUMNSTORE INDEX ncci ON Sales.OrderLines (StockItemID, Quantity, UnitPrice, TaxRate) WITH ( ONLINE = ON );
```

##### COMPRESSION_DELAY = **0** | \<delay>[Minutes]  
   Specifies a lower bound on how long a row should stay in delta rowgroup before it is eligible for migration to compressed rowgroup. For example, a customer can say that if a row is unchanged for  120 minutes, make it  eligible for compressing into columnar storage format. For columnstore index on disk-based tables, we don't track the time when a row was inserted or updated,  we use the delta rowgroup closed time as a proxy for the row instead. The default duration is 0 minutes. A row is migrated to columnar storage once 1 million rows have been accumulated in delta rowgroup and it has been marked closed.  
  
###### DATA_COMPRESSION  
   Specifies the data compression option for the specified table, partition number, or range of partitions. Applies only to columnstore indexes, including both nonclustered columnstore and clustered columnstore indexes. The options are as follows:
   
- `COLUMNSTORE` - the default and specifies to compress with the most performant columnstore compression. This is the typical choice.  
- `COLUMNSTORE_ARCHIVE` -  COLUMNSTORE_ARCHIVE further compresses the table or partition to a smaller size. This can be used for archival, or for other situations that require a smaller storage size and can afford more time for storage and retrieval.  
  
 For more information about compression, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  
  
##### WHERE \<filter_expression> [ AND \<filter_expression> ]
  
   Called a filter predicate, this specifies which rows to include in the index. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates filtered statistics on the data rows in the filtered index.  
  
   The filter predicate uses simple comparison logic. Comparisons using NULL literals are not allowed with the comparison operators. Use the IS NULL and IS NOT NULL operators instead.  
  
   Here are some examples of filter predicates for the `Production.BillOfMaterials` table:  
   `WHERE StartDate > '20000101' AND EndDate <= '20000630'`    
   `WHERE ComponentID IN (533, 324, 753)`  
   `WHERE StartDate IN ('20000404', '20000905') AND EndDate IS NOT NULL`  
   
   For guidance on filtered indexes, see [Create Filtered Indexes](../../relational-databases/indexes/create-filtered-indexes.md).  
  
#### ON options  
   These options specify the filegroups on which the index is created.  
  
*partition_scheme_name* **(** _column_name_ **)**  
   Specifies the partition scheme that defines the filegroups onto which the partitions of a partitioned index is mapped. The partition scheme must exist within the database by executing [CREATE PARTITION SCHEME](../../t-sql/statements/create-partition-scheme-transact-sql.md). 
   *column_name* specifies the column against which a partitioned index is partitioned. This column must match the data type, length, and precision of the argument of the partition function that *partition_scheme_name* is using. *column_name* is not restricted to the columns in the index definition. When partitioning a columnstore index, [!INCLUDE[ssDE](../../includes/ssde-md.md)] adds the partitioning column as a column of the index, if it is not already specified.  
   If *partition_scheme_name* or *filegroup* is not specified and the table is partitioned, the index is placed in the same partition scheme, using the same partitioning column, as the underlying table.  
   A columnstore index on a partitioned table must be partition aligned.  
   For more information about partitioning indexes, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).  

*filegroup_name*  
   Specifies a filegroup name on which to create the index. If *filegroup_name* is not specified and the table is not partitioned, the index uses the same filegroup as the underlying table. The filegroup must already exist.  
 
**"**default**"**  
Creates the specified index on the default filegroup.  
  
The term default, in this context, is not a keyword. It is an identifier for the default filegroup and must be delimited, as in ON **"**default**"** or ON **[**default**]**. If "default" is specified, the QUOTED_IDENTIFIER option must be ON for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
  
##  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="GenRemarks"></a> General Remarks  
 A  columnstore index can be created on a temporary table. When the table is dropped or the session ends, the index is also dropped.  
 
## Filtered Indexes  
A filtered index is an optimized nonclustered index, suited for queries that select a small percentage of rows from a table. It uses a filter predicate to index a portion of the data in the table. A well-designed filtered index can improve query performance, reduce storage costs, and reduce maintenance costs.  
  
### Required SET Options for Filtered Indexes  
The SET options in the Required Value column are required whenever any of the following conditions occur:  
- Create a filtered index.  
- INSERT, UPDATE, DELETE, or MERGE operation modifies the data in a filtered index.  
- The filtered index is used by the query optimizer to produce the query plan.  
  
    |SET options|Required value|Default server value|Default<br /><br /> OLE DB and ODBC value|Default<br /><br /> DB-Library value|  
    |-----------------|--------------------|--------------------------|---------------------------------------|-----------------------------------|  
    |ANSI_NULLS|ON|ON|ON|OFF|  
    |ANSI_PADDING|ON|ON|ON|OFF|  
    |ANSI_WARNINGS*|ON|ON|ON|OFF|  
    |ARITHABORT|ON|ON|OFF|OFF|  
    |CONCAT_NULL_YIELDS_NULL|ON|ON|ON|OFF|  
    |NUMERIC_ROUNDABORT|OFF|OFF|OFF|OFF|  
    |QUOTED_IDENTIFIER|ON|ON|ON|OFF|   
  
     *Setting ANSI_WARNINGS to ON implicitly sets ARITHABORT to ON when the database compatibility level is set to 90 or higher. If the database compatibility level is set to 80 or earlier, the ARITHABORT option must explicitly be set to ON.  
  
 If the SET options are incorrect, the following conditions can occur:  
  
-   The filtered index is not created.  
  
-   The [!INCLUDE[ssDE](../../includes/ssde-md.md)] generates an error and rolls back INSERT, UPDATE, DELETE, or MERGE statements that change data in the index.  
  
-   Query optimizer does not consider the index in the execution plan for any Transact-SQL statements.  
  
 For more information about Filtered Indexes, see [Create Filtered Indexes](../../relational-databases/indexes/create-filtered-indexes.md). 
  
##  <a name="LimitRest"></a> Limitations and Restrictions  

**Each column in a columnstore index must be of one of the following common business data types:** 
-   datetimeoffset [ ( *n* ) ]  
-   datetime2 [ ( *n* ) ]  
-   datetime  
-   smalldatetime  
-   date  
-   time [ ( *n* ) ]  
-   float [ ( *n* ) ]  
-   real [ ( *n* ) ]  
-   decimal [ ( *precision* [ *, scale* ] **)** ]
-   numeric [ ( *precision* [ *, scale* ] **)** ]    
-   money  
-   smallmoney  
-   bigint  
-   int  
-   smallint  
-   tinyint  
-   bit  
-   nvarchar [ ( *n* ) ] 
-   nvarchar(max)  (Applies to [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] and Premium tier, Standard tier (S3 and above), and all VCore offerings tiers, in clustered columnstore indexes only)   
-   nchar [ ( *n* ) ]  
-   varchar [ ( *n* ) ]  
-   varchar(max)  (Applies to [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] and Premium tier, Standard tier (S3 and above), and all VCore offerings tiers, in clustered columnstore indexes only)
-   char [ ( *n* ) ]  
-   varbinary [ ( *n* ) ] 
-   varbinary (max)  (Applies to [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] and Azure SQL Database at Premium tier, Standard tier (S3 and above), and all VCore offerings tiers, in clustered columnstore indexes only)
-   binary [ ( *n* ) ]  
-   uniqueidentifier  (Applies to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later)
  
If the underlying table has a column of a data type that is not supported for columnstore indexes, you must omit that column from the nonclustered columnstore index.  
  
**Columns that use any of the following data types cannot be included in a columnstore index:**
-   ntext, text, and image  
-   nvarchar(max), varchar(max), and varbinary(max) (Applies to [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] and prior versions, and nonclustered columnstore indexes) 
-   rowversion (and timestamp)  
-   sql_variant  
-   CLR types (hierarchyid and spatial types)  
-   xml  
-   uniqueidentifier (Applies to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])  

**Nonclustered columnstore indexes:**
-   Cannot have more than 1024 columns.
-   Cannot be created as a constraint-based index. It is possible to have unique constraints, primary key constraints, and foreign key constraints on a table with a columnstore index. Constraints are always enforced with a row-store index. Constraints cannot be enforced with a columnstore (clustered or nonclustered) index.
-   Cannot include a sparse column.  
-   Cannot be changed by using the **ALTER INDEX** statement. To change the nonclustered index, you must drop and re-create the columnstore index instead. You can use **ALTER INDEX** to disable and rebuild a columnstore index.  
-   Cannot be created by using the **INCLUDE** keyword.  
-   Cannot include the **ASC** or **DESC** keywords for sorting the index. Columnstore indexes are ordered according to the compression algorithms. Sorting would eliminate many of the performance benefits.  
-   Cannot include large object (LOB) columns of type nvarchar(max), varchar(max), and varbinary(max) in nonclustered column store indexes. Only clustered columnstore indexes support LOB types, beginning in [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] version and Azure SQL Database configured at  Premium tier, Standard tier (S3 and above), and all VCore offerings tiers tier. Note, prior versions do not support LOB types in clustered and nonclustered columnstore indexes.


> [!NOTE]  
> Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can create a nonclustered columnstore index on an indexed view.  


 **Columnstore indexes cannot be combined with the following features:**  
-   Computed columns. Starting with SQL Server 2017, a clustered columnstore index can contain a non-persisted computed column. However, in SQL Server 2017, clustered columnstore indexes cannot contain persisted computed columns, and you cannot created nonclustered indexes on computed columns. 
-   Page and row compression, and **vardecimal** storage format (A columnstore index is already compressed in a different format.)  
-   Replication  
-   Filestream

You cannot use cursors or triggers on a table with a clustered columnstore index. This restriction does not apply to nonclustered columnstore indexes; you can use cursors and triggers on a table with a nonclustered columnstore index.

**[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] specific limitations**  
These limitations apply only to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. In this release, we introduced updateable clustered columnstore indexes. Nonclustered columnstore indexes were still read-only.  

-   Change tracking. You cannot use change tracking with nonclustered columnstore indexes (NCCI) because they are read-only. It does work for clustered columnstore indexes (CCI).  
-   Change data capture. You cannot use change data capture for nonclustered columnstore index (NCCI) because they are read-only. It does work for clustered columnstore indexes (CCI).  
-   Readable secondary. You cannot access  a clustered columnstore index (CCI) from a readable secondary of an Always OnReadable availability group.  You can access a nonclustered columnstore index (NCCI) from a readable secondary.  
-   Multiple Active Result Sets (MARS). [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] uses MARS for  read-only connections to tables with a columnstore index. However, [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] does not support MARS for concurrent data manipulation language (DML) operations on a table with a columnstore index. When this occurs, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] terminates the connections and aborts the transactions.  
-  Nonclustered columnstore indexes cannot be created on a view or indexed view.
  
 For information about the performance benefits and limitations of columnstore indexes, see [Columnstore Indexes Overview](../../relational-databases/indexes/columnstore-indexes-overview.md).
  
##  <a name="Metadata"></a> Metadata  
 All of the columns in a columnstore index are stored in the metadata as included columns. The columnstore index does not have key columns. These system views provide information about columnstore indexes.  
  
-   [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)  
-   [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)  
-   [sys.partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)  
-   [sys.column_store_segments &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-store-segments-transact-sql.md)  
-   [sys.column_store_dictionaries &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-store-dictionaries-transact-sql.md)  
-   [sys.column_store_row_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-store-row-groups-transact-sql.md)  

##  <a name="convert"></a> Examples for converting a rowstore table to columnstore  
  
### A. Convert a heap to a clustered columnstore index  
 This example creates a table as a heap and then converts it to a clustered columnstore index named cci_Simple. This changes the storage for the entire table from rowstore to columnstore.  
  
```sql  
CREATE TABLE SimpleTable(  
    ProductKey [int] NOT NULL,   
    OrderDateKey [int] NOT NULL,   
    DueDateKey [int] NOT NULL,   
    ShipDateKey [int] NOT NULL);  
GO  
CREATE CLUSTERED COLUMNSTORE INDEX cci_Simple ON SimpleTable;  
GO  
```  
  
### B. Convert a clustered index to a clustered columnstore index with the same name.  
 This example creates a table with clustered index, and then demonstrates the syntax of converting the clustered index to a clustered columnstore index. This changes the storage for the entire table from rowstore to columnstore.  
  
```sql  
CREATE TABLE SimpleTable (  
    ProductKey [int] NOT NULL,   
    OrderDateKey [int] NOT NULL,   
    DueDateKey [int] NOT NULL,   
    ShipDateKey [int] NOT NULL);  
GO  
CREATE CLUSTERED INDEX cl_simple ON SimpleTable (ProductKey);  
GO  
CREATE CLUSTERED COLUMNSTORE INDEX cl_simple ON SimpleTable  
WITH (DROP_EXISTING = ON);  
GO  
```  
  
### C. Handle nonclustered indexes when converting a rowstore table to a columnstore index.  
 This example shows how to handle nonclustered indexes when converting a rowstore table to a columnstore index. Actually, beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] no special action is required; [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically defines and rebuilds the nonclustered indexes on the new clustered columnstore index.  
  
 If you want to drop the nonclustered indexes, use the DROP INDEX statement prior to creating the columnstore index. The DROP EXISTING option only drops the clustered index that is being converted. It does not drop the nonclustered indexes.  
  
 In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], you could not create a nonclustered index on a columnstore index. This example shows how in previous releases you need to drop the nonclustered indexes before creating the columnstore index.  
  
```sql  
--Create the table for use with this example.  
CREATE TABLE SimpleTable (  
    ProductKey [int] NOT NULL,   
    OrderDateKey [int] NOT NULL,   
    DueDateKey [int] NOT NULL,   
    ShipDateKey [int] NOT NULL);  
GO  
  
--Create two nonclustered indexes for use with this example  
CREATE INDEX nc1_simple ON SimpleTable (OrderDateKey);  
CREATE INDEX nc2_simple ON SimpleTable (DueDateKey);   
GO  
  
--SQL Server 2012 and SQL Server 2014: you need to drop the nonclustered indexes  
--in order to create the columnstore index.   
  
DROP INDEX SimpleTable.nc1_simple;  
DROP INDEX SimpleTable.nc2_simple;  
  
--Convert the rowstore table to a columnstore index.  
CREATE CLUSTERED COLUMNSTORE INDEX cci_simple ON SimpleTable;   
GO  
```  
  
### D. Convert a large fact table from rowstore to columnstore  
 This example explains how to convert a large fact table from a rowstore table to a columnstore table.  
  
 To convert a rowstore table to a columnstore table.  
  
1.  First, create a small table to use in this example.  
  
    ```sql  
    --Create a rowstore table with a clustered index and a non-clustered index.  
    CREATE TABLE MyFactTable (  
        ProductKey [int] NOT NULL,  
        OrderDateKey [int] NOT NULL,  
         DueDateKey [int] NOT NULL,  
         ShipDateKey [int] NOT NULL )  
    )  
    WITH (  
        CLUSTERED INDEX ( ProductKey )  
    );  
  
    --Add a non-clustered index.  
    CREATE INDEX my_index ON MyFactTable ( ProductKey, OrderDateKey );  
    ```  
  
2.  Drop all non-clustered indexes from the rowstore table.  
  
    ```sql  
    --Drop all non-clustered indexes  
    DROP INDEX my_index ON MyFactTable;  
    ```  
  
3.  Drop the clustered index.  
  
    -   Do this only if you want to specify a new name for the index when it is converted to a clustered columnstore index. If you do not drop the clustered index, the new clustered columnstore index has the same name.  
  
        > [!NOTE]  
        > The name of the index might be easier to remember if you use your own name. All rowstore clustered indexes use the default name which is 'ClusteredIndex_\<GUID>'.  
  
    ```sql  
    --Process for dropping a clustered index.  
    --First, look up the name of the clustered rowstore index.  
    --Clustered rowstore indexes always use the DEFAULT name 'ClusteredIndex_<GUID>'.  
    SELECT i.name   
    FROM sys.indexes i   
    JOIN sys.tables t  
    ON ( i.type_desc = 'CLUSTERED' ) WHERE t.name = 'MyFactTable';  
  
    --Drop the clustered rowstore index.  
    DROP INDEX ClusteredIndex_d473567f7ea04d7aafcac5364c241e09 ON MyDimTable;  
    ```  
  
4.  Convert the rowstore table to a columnstore table with a clustered columnstore index.  
  
    ```sql  
    --Option 1: Convert to columnstore and name the new clustered columnstore index MyCCI.  
    CREATE CLUSTERED COLUMNSTORE INDEX MyCCI ON MyFactTable;  
  
    --Option 2: Convert to columnstore and use the rowstore clustered   
    --index name for the columnstore clustered index name.  
    --First, look up the name of the clustered rowstore index.  
    SELECT i.name   
    FROM sys.indexes i  
    JOIN sys.tables t   
    ON ( i.type_desc = 'CLUSTERED' )  
    WHERE t.name = 'MyFactTable';  
  
    --Second, create the clustered columnstore index and   
    --Replace ClusteredIndex_d473567f7ea04d7aafcac5364c241e09  
    --with the name of your clustered index.  
    CREATE CLUSTERED COLUMNSTORE INDEX   
    ClusteredIndex_d473567f7ea04d7aafcac5364c241e09  
     ON MyFactTable  
    WITH DROP_EXISTING = ON;  
    ```  
  
### E. Convert a columnstore table to a rowstore table with a clustered index  
 To convert a columnstore table to a rowstore table with a clustered index, use the CREATE INDEX statement with the DROP_EXISTING option.  
  
```sql  
CREATE CLUSTERED INDEX ci_MyTable   
ON MyFactTable  
WITH ( DROP EXISTING = ON );  
```  
  
### F. Convert a columnstore table to a rowstore heap  
 To convert a columnstore table to a rowstore heap, simply drop the clustered columnstore index.  
  
```sql  
DROP INDEX MyCCI   
ON MyFactTable;  
```  
  

### G. Defragment by rebuilding the entire clustered columnstore index  
   Applies to: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
  
 There are two ways to rebuild the full clustered columnstore index. You can use CREATE CLUSTERED COLUMNSTORE INDEX, or [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md) and the REBUILD option. Both methods achieve the same results.  
  
> [!NOTE]  
> Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], use `ALTER INDEX...REORGANIZE` instead of rebuilding with the methods described in this example.  
  
```sql  
--Determine the Clustered Columnstore Index name of MyDimTable.  
SELECT i.object_id, i.name, t.object_id, t.name   
FROM sys.indexes i   
JOIN sys.tables t  
ON (i.type_desc = 'CLUSTERED COLUMNSTORE')  
WHERE t.name = 'RowstoreDimTable';  
  
--Rebuild the entire index by using CREATE CLUSTERED INDEX.  
CREATE CLUSTERED COLUMNSTORE INDEX my_CCI   
ON MyFactTable  
WITH ( DROP_EXISTING = ON );  
  
--Rebuild the entire index by using ALTER INDEX and the REBUILD option.  
ALTER INDEX my_CCI  
ON MyFactTable  
REBUILD PARTITION = ALL  
WITH ( DROP_EXISTING = ON );  
```  
  
##  <a name="nonclustered"></a> Examples for nonclustered columnstore indexes  
  
### A. Create a columnstore index as a secondary index on a rowstore table  
 This example creates a nonclustered columnstore index on a rowstore table. Only one columnstore index can be created in this situation. The columnstore index requires extra storage since it contains a copy of the data in the rowstore table. This example creates a simple table and a clustered index, and then demonstrates the syntax of creating a nonclustered columnstore index.  
  
```sql  
CREATE TABLE SimpleTable  
(ProductKey [int] NOT NULL,   
OrderDateKey [int] NOT NULL,   
DueDateKey [int] NOT NULL,   
ShipDateKey [int] NOT NULL);  
GO  
CREATE CLUSTERED INDEX cl_simple ON SimpleTable (ProductKey);  
GO  
CREATE NONCLUSTERED COLUMNSTORE INDEX csindx_simple  
ON SimpleTable  
(OrderDateKey, DueDateKey, ShipDateKey);  
GO  
```  
  
### B. Create a simple nonclustered columnstore index using all options  
 The following example demonstrates the syntax of creating a nonclustered columnstore index by using all options.  
  
```sql  
CREATE NONCLUSTERED COLUMNSTORE INDEX csindx_simple  
ON SimpleTable  
(OrderDateKey, DueDateKey, ShipDateKey)  
WITH (DROP_EXISTING =  ON,   
    MAXDOP = 2)  
ON "default"  
GO  
```  
  
 For a more complex example using partitioned tables, see 
 [Columnstore Indexes Overview](../../relational-databases/indexes/columnstore-indexes-overview.md).  
  
### C. Create a nonclustered columnstore index with a filtered predicate  
 The following example creates a filtered nonclustered columnstore index on the Production.BillOfMaterials table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. The filter predicate can include columns that are not key columns in the filtered index. The predicate in this example selects only the rows where EndDate is non-NULL.  
  
```sql  
IF EXISTS (SELECT name FROM sys.indexes  
    WHERE name = N'FIBillOfMaterialsWithEndDate'   
    AND object_id = OBJECT_ID(N'Production.BillOfMaterials'))  
DROP INDEX FIBillOfMaterialsWithEndDate  
    ON Production.BillOfMaterials;  
GO  
CREATE NONCLUSTERED COLUMNSTORE INDEX "FIBillOfMaterialsWithEndDate"  
    ON Production.BillOfMaterials (ComponentID, StartDate)  
    WHERE EndDate IS NOT NULL;  
```  
  
###  <a name="ncDML"></a> D. Change the data in a nonclustered columnstore index  
   Applies to: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].
  
 Once you create a nonclustered columnstore index on a table, you cannot directly modify the data in that table. A query with INSERT, UPDATE, DELETE, or MERGE fails and returns an error message. To add or modify the data in the table, you can do one of the following:  
  
-   Disable or drop the columnstore index. You can then update the data in the table. If you disable the columnstore index, you can rebuild the columnstore index when you finish updating the data. For example,  
  
    ```sql  
    ALTER INDEX mycolumnstoreindex ON mytable DISABLE;  
    -- update mytable --  
    ALTER INDEX mycolumnstoreindex on mytable REBUILD  
    ```  
  
-   Load data into a staging table that does not have a columnstore index. Build a columnstore index on the staging table. Switch the staging table into an empty partition of the main table.  
  
-   Switch a partition from the table with the columnstore index into an empty staging table. If there is a columnstore index on the staging table, disable the columnstore index. Perform any updates. Build (or rebuild) the columnstore index. Switch the staging table back into the (now empty) partition of the main table.  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### A. Change a clustered index to a clustered columnstore index  
 By using the CREATE CLUSTERED COLUMNSTORE INDEX statement with DROP_EXISTING = ON, you can:  
  
-   Change a clustered index into a clustered columnstore index.  
  
-   Rebuild a clustered columnstore index.  
  
 This example creates the xDimProduct table as a rowstore table with a clustered index, and then uses CREATE CLUSTERED COLUMNSTORE INDEX to change the table from a rowstore table to a columnstore table.  
  
```sql  
-- Uses AdventureWorks  
  
IF EXISTS (SELECT name FROM sys.tables  
    WHERE name = N'xDimProduct'  
    AND object_id = OBJECT_ID (N'xDimProduct'))  
DROP TABLE xDimProduct;  
  
--Create a distributed table with a clustered index.  
CREATE TABLE xDimProduct (ProductKey, ProductAlternateKey, ProductSubcategoryKey)  
WITH ( DISTRIBUTION = HASH(ProductKey),  
    CLUSTERED INDEX (ProductKey) )  
AS SELECT ProductKey, ProductAlternateKey, ProductSubcategoryKey FROM DimProduct;  
  
--Change the existing clustered index   
--to a clustered columnstore index with the same name.  
--Look up the name of the index before running this statement.  
CREATE CLUSTERED COLUMNSTORE INDEX <index_name>   
ON xdimProduct   
WITH ( DROP_EXISTING = ON );  
```  
  
### B. Rebuild a clustered columnstore index  
 Building on the previous example, this example uses CREATE CLUSTERED COLUMNSTORE INDEX to rebuild the existing clustered columnstore index called cci_xDimProduct.  
  
```sql  
--Rebuild the existing clustered columnstore index.  
CREATE CLUSTERED COLUMNSTORE INDEX cci_xDimProduct   
ON xdimProduct   
WITH ( DROP_EXISTING = ON );  
```  
  
### C. Change the name of a clustered columnstore index  
 To change the name of a clustered columnstore index, drop the existing clustered columnstore index, and then recreate the index with a new name.  
  
 We recommend only doing this operation with a small table or an empty table. It takes a long time to drop a large clustered columnstore index and rebuild with a different name.  
  
 Using the cci_xDimProduct clustered columnstore index from the previous example, this example drops the cci_xDimProduct clustered columnstore index and then recreates the clustered columnstore index with the name mycci_xDimProduct.  
  
```sql  
--For illustration purposes, drop the clustered columnstore index.   
--The table continues to be distributed, but changes to a heap.  
DROP INDEX cci_xdimProduct ON xDimProduct;  
  
--Create a clustered index with a new name, mycci_xDimProduct.  
CREATE CLUSTERED COLUMNSTORE INDEX mycci_xDimProduct  
ON xdimProduct  
WITH ( DROP_EXISTING = OFF );  
```  
  
### D. Convert a columnstore table to a rowstore table with a clustered index  
 There might be a situation for which you want to drop a clustered columnstore index and create a clustered index. This stores the table in rowstore format. This example converts a columnstore table to a rowstore table with a clustered index with the same name. None of the data is lost. All data goes to the rowstore table and the columns listed becomes the key columns in the clustered index.  
  
```sql  
--Drop the clustered columnstore index and create a clustered rowstore index.   
--All of the columns are stored in the rowstore clustered index.   
--The columns listed are the included columns in the index.  
CREATE CLUSTERED INDEX cci_xDimProduct    
ON xdimProduct (ProductKey, ProductAlternateKey, ProductSubcategoryKey, WeightUnitMeasureCode)  
WITH ( DROP_EXISTING = ON);  
```  
  
### E. Convert a columnstore table back to a rowstore heap  
 Use [DROP INDEX (SQL Server PDW)](https://msdn.microsoft.com/f59cab43-9f40-41b4-bfdb-d90e80e9bf32) to drop the clustered columnstore index and convert the table to a rowstore heap. This example converts the cci_xDimProduct table to a rowstore heap. The table continues to be distributed, but is stored as a heap.  
  
```sql  
--Drop the clustered columnstore index. The table continues to be distributed, but changes to a heap.  
DROP INDEX cci_xdimProduct ON xdimProduct;  
```  

