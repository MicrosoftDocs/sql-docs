---
title: "CREATE TABLE AS SELECT (Azure Synapse Analytics and Microsoft Fabric)"
description: "CREATE TABLE AS SELECT in Azure Synapse Analytics and Microsoft Fabric creates a new table based on the output of a SELECT statement. CTAS is the simplest and fastest way to create a copy of a table."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto, xiaoyul, mariyaali, periclesrocha
ms.date: 04/20/2023
ms.service: sql
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest||=fabric"
---
# CREATE TABLE AS SELECT

::: moniker range="=azure-sqldw-latest"
 
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

CREATE TABLE AS SELECT (CTAS) is one of the most important T-SQL features available. It's a fully parallelized operation that creates a new table based on the output of a SELECT statement. CTAS is the simplest and fastest way to create a copy of a table.

For example, use CTAS to:  
  
-   Re-create a table with a different hash distribution column.
-   Re-create a table as replicated.   
-   Create a columnstore index on just some of the columns in the table.  
-   Query or import external data.  

> [!NOTE]  
> Since CTAS adds to the capabilities of creating a table, this topic tries not to repeat the CREATE TABLE topic. Instead, it describes the differences between the CTAS and CREATE TABLE statements. For the CREATE TABLE details, see [CREATE TABLE (Azure Synapse Analytics)](create-table-azure-sql-data-warehouse.md) statement. 
  
- [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
- CTAS is supported in the [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)].

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
<a name="syntax-bk"></a>

## Syntax

```syntaxsql
CREATE TABLE { database_name.schema_name.table_name | schema_name.table_name | table_name }
    [ ( column_name [ ,...n ] ) ]  
    WITH ( 
      <distribution_option> -- required
      [ , <table_option> [ ,...n ] ]    
    )  
    AS <select_statement>  
    OPTION <query_hint> 
[;]  

<distribution_option> ::=
    { 
        DISTRIBUTION = HASH ( distribution_column_name ) 
      | DISTRIBUTION = HASH ( [distribution_column_name [, ...n]] )
      | DISTRIBUTION = ROUND_ROBIN 
      | DISTRIBUTION = REPLICATE
    }   

<table_option> ::= 
    {   
        CLUSTERED COLUMNSTORE INDEX --default for Synapse Analytics 
      | CLUSTERED COLUMNSTORE INDEX ORDER (column[,...n])
      | HEAP --default for Parallel Data Warehouse   
      | CLUSTERED INDEX ( { index_column_name [ ASC | DESC ] } [ ,...n ] ) --default is ASC 
    }  
      | PARTITION ( partition_column_name RANGE [ LEFT | RIGHT ] --default is LEFT  
        FOR VALUES ( [ boundary_value [,...n] ] ) ) 
  
<select_statement> ::=  
    [ WITH <common_table_expression> [ ,...n ] ]  
    SELECT select_criteria  

<query_hint> ::=
    {
        MAXDOP 
    }
```  

<a name="arguments-bk"></a>
  
## Arguments

For details, see the [Arguments section](create-table-azure-sql-data-warehouse.md#arguments) in CREATE TABLE.  

<a name="column-options-bk"></a>

### Column options

`column_name` [ ,...`n` ]   
 Column names don't allow the [column options](create-table-azure-sql-data-warehouse.md#ColumnOptions) mentioned in CREATE TABLE.  Instead, you can provide an optional list of one or more column names for the new table. The columns in the new table use the names you specify. When you specify column names, the number of columns in the column list must match the number of columns in the select results. If you don't specify any column names, the new target table uses the column names in the select statement results. 
  
 You can't specify any other column options such as data types, collation, or nullability. Each of these attributes is derived from the results of the `SELECT` statement. However, you can use the SELECT statement to change the attributes. For an example, see [Use CTAS to change column attributes](#ctas-change-column-attributes-bk).   

<a name="table-distribution-options-bk"></a>

### Table distribution options

For details and to understand how to choose the best distribution column, see the [Table distribution options](create-table-azure-sql-data-warehouse.md#TableDistributionOptions) section in CREATE TABLE. For recommendations on which distribution to choose for a table based on actual usage or sample queries, see [Distribution Advisor in Azure Synapse SQL](/azure/synapse-analytics/sql/distribution-advisor).

`DISTRIBUTION` = `HASH` (*distribution_column_name*) | ROUND_ROBIN | REPLICATE
The CTAS statement requires a distribution option and doesn't have default values. This is different from CREATE TABLE, which has defaults.

`DISTRIBUTION = HASH ( [distribution_column_name [, ...n]] )`
Distributes the rows based on the hash values of up to eight columns, allowing for more even distribution of the base table data, reducing the data skew over time and improving query performance.

> [!NOTE]
>
> - To enable feature, change the database's compatibility level to 50 with this command. For more information on setting the database compatibility level, see [ALTER DATABASE SCOPED CONFIGURATION](alter-database-scoped-configuration-transact-sql.md). For example: `ALTER DATABASE SCOPED CONFIGURATION SET DW_COMPATIBILITY_LEVEL = 50;`
> - To disable the multi-column distribution (MCD) feature, run this command to change the database's compatibility level to AUTO. For example: `ALTER DATABASE SCOPED CONFIGURATION SET DW_COMPATIBILITY_LEVEL = AUTO;` Existing MCD tables will stay but become unreadable. Queries over MCD tables will return this error: `Related table/view is not readable because it distributes data on multiple columns and multi-column distribution is not supported by this product version or this feature is disabled.`
>   - To regain access to MCD tables, enable the feature again.
>   - To load data into a MCD table, use CTAS statement and the data source needs be Synapse SQL tables.  
>   - CTAS on MCD HEAP target tables is not supported. Instead, use [INSERT SELECT](insert-transact-sql.md?view=azure-sqldw-latest&preserve-view=true) as a workaround to load data into MCD HEAP tables.
> - Using SSMS for [generating a script](../../ssms/scripting/generate-scripts-sql-server-management-studio.md) to create MCD tables is currently supported beyond SSMS version 19.

For details and to understand how to choose the best distribution column, see the [Table distribution options](create-table-azure-sql-data-warehouse.md#TableDistributionOptions) section in CREATE TABLE. 


For recommendations on the best distribution do to use based on your workloads, see the [Synapse SQL Distribution Advisor (Preview)](/azure/synapse-analytics/sql/distribution-advisor).

<a name="table-partition-options-bk"></a>

### Table partition options

The CTAS statement creates a nonpartitioned table by default, even if the source table is partitioned. To create a partitioned table with the CTAS statement, you must specify the partition option. 

For details, see the [Table partition options](create-table-azure-sql-data-warehouse.md#TablePartitionOptions) section in CREATE TABLE.

<a name="select-options-bk"></a>

### SELECT statement

The SELECT statement is the fundamental difference between CTAS and CREATE TABLE.  

#### `WITH` *common_table_expression*  

 Specifies a temporary named result set, known as a common table expression (CTE). For more information, see [WITH common_table_expression (Transact-SQL)](../queries/with-common-table-expression-transact-sql.md).  
  
#### `SELECT` *select_criteria*  

 Populates the new table with the results from a SELECT statement. *select_criteria* is the body of the SELECT statement that determines which data to copy to the new table. For information about SELECT statements, see [SELECT (Transact-SQL)](../queries/select-transact-sql.md).  
 
### Query hint
Users can set MAXDOP to an integer value to control the maximum degree of parallelism.  When MAXDOP is set to 1, the query is executed by a single thread.

 
<a name="permissions-bk"></a>  
  
## Permissions
CTAS requires `SELECT` permission on any objects referenced in the *select_criteria*.

For permissions to create a table, see [Permissions](create-table-azure-sql-data-warehouse.md#permissions) in CREATE TABLE. 
  
<a name="general-remarks-bk"></a>
  
## Remarks
For details, see [General Remarks](create-table-azure-sql-data-warehouse.md#GeneralRemarks) in CREATE TABLE.

<a name="limitations-bk"></a>

## Limitations and restrictions

An ordered clustered columnstore index can be created on columns of any data types supported in [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] except for string columns.  

[SET ROWCOUNT (Transact-SQL)](../statements/set-rowcount-transact-sql.md) has no effect on CTAS. To achieve a similar behavior, use [TOP (Transact-SQL)](../queries/top-transact-sql.md).  
 
For details, see [Limitations and Restrictions](create-table-azure-sql-data-warehouse.md#LimitationsRestrictions) in CREATE TABLE.

<a name="locking-behavior-bk"></a>
  
## Locking behavior

 For details, see [Locking Behavior](create-table-azure-sql-data-warehouse.md#LockingBehavior) in CREATE TABLE.
 
<a name="performance-bk"></a>
 
 ## Performance 

For a hash-distributed table, you can use CTAS to choose a different distribution column to achieve better performance for joins and aggregations. If choosing a different distribution column isn't your goal, you'll have the best CTAS performance if you specify the same distribution column since this will avoid redistributing the rows. 

If you're using CTAS to create table and performance isn't a factor, you can specify `ROUND_ROBIN` to avoid having to decide on a distribution column.

To avoid data movement in subsequent queries, you can specify `REPLICATE` at the cost of increased storage for loading a full copy of the table on each Compute node.  

<a name="examples-copy-table-bk"></a>

## Examples for copying a table

<a name="ctas-copy-table-bk"></a>

### A. Use CTAS to copy a table

Applies to: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

Perhaps one of the most common uses of `CTAS` is creating a copy of a table so that you can change the DDL. If, for example,  you originally created your table as `ROUND_ROBIN` and now want change it to a table distributed on a column, `CTAS` is how you would change the distribution column. `CTAS` can also be used to change partitioning, indexing, or column types.

Let's say you created this table by specifying `HEAP` and using the default distribution type of `ROUND_ROBIN`.

```sql
CREATE TABLE FactInternetSales
(
    ProductKey INT NOT NULL,
    OrderDateKey INT NOT NULL,
    DueDateKey INT NOT NULL,
    ShipDateKey INT NOT NULL,
    CustomerKey INT NOT NULL,
    PromotionKey INT NOT NULL,
    CurrencyKey INT NOT NULL,
    SalesTerritoryKey INT NOT NULL,
    SalesOrderNumber NVARCHAR(20) NOT NULL,
    SalesOrderLineNumber TINYINT NOT NULL,
    RevisionNumber TINYINT NOT NULL,
    OrderQuantity SMALLINT NOT NULL,
    UnitPrice MONEY NOT NULL,
    ExtendedAmount MONEY NOT NULL,
    UnitPriceDiscountPct FLOAT NOT NULL,
    DiscountAmount FLOAT NOT NULL,
    ProductStandardCost MONEY NOT NULL,
    TotalProductCost MONEY NOT NULL,
    SalesAmount MONEY NOT NULL,
    TaxAmt MONEY NOT NULL,
    Freight MONEY NOT NULL,
    CarrierTrackingNumber NVARCHAR(25),
    CustomerPONumber NVARCHAR(25)
)
WITH( 
 HEAP, 
 DISTRIBUTION = ROUND_ROBIN 
);
```

Now you want to create a new copy of this table with a clustered columnstore index so that you can take advantage of the performance of clustered columnstore tables. You also want to distribute this table on `ProductKey` since you're anticipating joins on this column and want to avoid data movement during joins on `ProductKey`. Lastly you also want to add partitioning on `OrderDateKey` so that you can quickly delete old data by dropping old partitions. Here's the CTAS statement that would copy your old table into a new table:

```sql
CREATE TABLE FactInternetSales_new
WITH
(
    CLUSTERED COLUMNSTORE INDEX,
    DISTRIBUTION = HASH(ProductKey),
    PARTITION
    (
        OrderDateKey RANGE RIGHT FOR VALUES
        (
        20000101,20010101,20020101,20030101,20040101,20050101,20060101,20070101,20080101,20090101,
        20100101,20110101,20120101,20130101,20140101,20150101,20160101,20170101,20180101,20190101,
        20200101,20210101,20220101,20230101,20240101,20250101,20260101,20270101,20280101,20290101
        )
    )
)
AS SELECT * FROM FactInternetSales;
```

Finally you can rename your tables to swap in your new table and then drop your old table.

```sql
RENAME OBJECT FactInternetSales TO FactInternetSales_old;
RENAME OBJECT FactInternetSales_new TO FactInternetSales;

DROP TABLE FactInternetSales_old;
```

<a name="examples-column-bk"></a>

## Examples for column options

<a name="ctas-change-column-attributes-bk"></a>

### B. Use CTAS to change column attributes

Applies to: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

This example uses CTAS to change data types, nullability, and collation for several columns in the `DimCustomer2` table.  
  
```sql  
-- Original table 
CREATE TABLE [dbo].[DimCustomer2] (  
    [CustomerKey] INT NOT NULL,  
    [GeographyKey] INT NULL,  
    [CustomerAlternateKey] nvarchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL  
)  
WITH (CLUSTERED COLUMNSTORE INDEX, DISTRIBUTION = HASH([CustomerKey]));  
  
-- CTAS example to change data types, nullability, and column collations  
CREATE TABLE test  
WITH (HEAP, DISTRIBUTION = ROUND_ROBIN)  
AS  
SELECT  
    CustomerKey AS CustomerKeyNoChange,  
    CustomerKey*1 AS CustomerKeyChangeNullable,  
    CAST(CustomerKey AS DECIMAL(10,2)) AS CustomerKeyChangeDataTypeNullable,  
    ISNULL(CAST(CustomerKey AS DECIMAL(10,2)),0) AS CustomerKeyChangeDataTypeNotNullable,  
    GeographyKey AS GeographyKeyNoChange,  
    ISNULL(GeographyKey,0) AS GeographyKeyChangeNotNullable,  
    CustomerAlternateKey AS CustomerAlternateKeyNoChange,  
    CASE WHEN CustomerAlternateKey = CustomerAlternateKey 
        THEN CustomerAlternateKey END AS CustomerAlternateKeyNullable,  
    CustomerAlternateKey COLLATE Latin1_General_CS_AS_KS_WS AS CustomerAlternateKeyChangeCollation  
FROM [dbo].[DimCustomer2]  
  
-- Resulting table 
CREATE TABLE [dbo].[test] (
    [CustomerKeyNoChange] INT NOT NULL, 
    [CustomerKeyChangeNullable] INT NULL, 
    [CustomerKeyChangeDataTypeNullable] DECIMAL(10, 2) NULL, 
    [CustomerKeyChangeDataTypeNotNullable] DECIMAL(10, 2) NOT NULL, 
    [GeographyKeyNoChange] INT NULL, 
    [GeographyKeyChangeNotNullable] INT NOT NULL, 
    [CustomerAlternateKeyNoChange] NVARCHAR(15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, 
    [CustomerAlternateKeyNullable] NVARCHAR(15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL, 
    [CustomerAlternateKeyChangeCollation] NVARCHAR(15) COLLATE Latin1_General_CS_AS_KS_WS NOT NULL
)
WITH (DISTRIBUTION = ROUND_ROBIN);
```
 
As a final step, you can use [RENAME (Transact-SQL)](../statements/rename-transact-sql.md) to switch the table names. This makes DimCustomer2 be the new table.

```sql
RENAME OBJECT DimCustomer2 TO DimCustomer2_old;
RENAME OBJECT test TO DimCustomer2;

DROP TABLE DimCustomer2_old;
```

<a name="examples-table-distribution-bk"></a>

## Examples for table distribution

<a name="ctas-change-distribution-method-bk"></a>

### C. Use CTAS to change the distribution method for a table
Applies to: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

This simple example shows how to change the distribution method for a table. To show the mechanics of how to do this, it changes a hash-distributed table to round-robin and then changes the round-robin table back to hash distributed. The final table matches the original table.

In most cases, you don't need to change a hash-distributed table to a round-robin table. More often, you might need to change a round-robin table to a hash distributed table. For example, you might initially load a new table as round-robin and then later move it to a hash-distributed table to get better join performance.

This example uses the AdventureWorksDW sample database. To load the Azure Synapse Analytics version, see [Quickstart: Create and query a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics using the Azure portal](/azure/synapse-analytics/sql-data-warehouse/create-data-warehouse-portal).

 
```sql
-- DimSalesTerritory is hash-distributed.
-- Copy it to a round-robin table.
CREATE TABLE [dbo].[myTable]   
WITH   
  (   
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = ROUND_ROBIN  
  )  
AS SELECT * FROM [dbo].[DimSalesTerritory]; 

-- Switch table names

RENAME OBJECT [dbo].[DimSalesTerritory] to [DimSalesTerritory_old];
RENAME OBJECT [dbo].[myTable] TO [DimSalesTerritory];

DROP TABLE [dbo].[DimSalesTerritory_old];
```  

Next, change it back to a hash distributed table.

```sql
-- You just made DimSalesTerritory a round-robin table.
-- Change it back to the original hash-distributed table. 
CREATE TABLE [dbo].[myTable]   
WITH   
  (   
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = HASH(SalesTerritoryKey) 
  )  
AS SELECT * FROM [dbo].[DimSalesTerritory]; 

-- Switch table names

RENAME OBJECT [dbo].[DimSalesTerritory] to [DimSalesTerritory_old];
RENAME OBJECT [dbo].[myTable] TO [DimSalesTerritory];

DROP TABLE [dbo].[DimSalesTerritory_old];
```
 
<a name="ctas-change-to-replicated-bk"></a>

### D. Use CTAS to convert a table to a replicated table

Applies to: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

This example applies for converting round-robin or hash-distributed tables to a replicated table. This particular example takes the previous method of changing the distribution type one step further.  Since `DimSalesTerritory` is a dimension and likely a smaller table, you can choose to re-create the table as replicated to avoid data movement when joining to other tables. 

```sql
-- DimSalesTerritory is hash-distributed.
-- Copy it to a replicated table.
CREATE TABLE [dbo].[myTable]   
WITH   
  (   
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = REPLICATE 
  )  
AS SELECT * FROM [dbo].[DimSalesTerritory]; 

-- Switch table names

RENAME OBJECT [dbo].[DimSalesTerritory] to [DimSalesTerritory_old];
RENAME OBJECT [dbo].[myTable] TO [DimSalesTerritory];

DROP TABLE [dbo].[DimSalesTerritory_old];
```
 
### E. Use CTAS to create a table with fewer columns
Applies to: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

The following example creates a round-robin distributed table named `myTable (c, ln)`. The new table only has two columns. It uses the column aliases in the SELECT statement for the names of the columns.  
  
```sql  
CREATE TABLE myTable  
WITH   
  (   
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = ROUND_ROBIN  
  )  
AS SELECT CustomerKey AS c, LastName AS ln  
    FROM dimCustomer; 
```  

<a name="examples-query-hints-bk"></a>

## Examples for query hints

<a name="ctas-query-hint-bk"></a>

### F. Use a Query Hint with CREATE TABLE AS SELECT (CTAS)

Applies to: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]
  
This query shows the basic syntax for using a query join hint with the CTAS statement. After the query is submitted, [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] applies the hash join strategy when it generates the query plan for each individual distribution. For more information on the hash join query hint, see [OPTION Clause (Transact-SQL)](../queries/option-clause-transact-sql.md).  
  
```sql  
CREATE TABLE dbo.FactInternetSalesNew  
WITH   
  (   
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = ROUND_ROBIN   
  )  
AS SELECT T1.* FROM dbo.FactInternetSales T1 JOIN dbo.DimCustomer T2  
ON ( T1.CustomerKey = T2.CustomerKey )  
OPTION ( HASH JOIN );  
```  

<a name="examples-external-tables"></a>

## Examples for external tables

<a name="ctas-azure-blob-storage-bk"></a>

### G. Use CTAS to import data from Azure Blob storage

Applies to: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  

To import data from an external table, use CREATE TABLE AS SELECT to select from the external table. The syntax to select data from an external table into [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] is the same as the syntax for selecting data from a regular table.  
  
 The following example defines an external table on data in an Azure Blob Storage account. It then uses CREATE TABLE AS SELECT to select from the external table. This imports the data from Azure Blob Storage text-delimited files and stores the data into a new [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] table.  
  
```sql  
--Use your own processes to create the text-delimited files on Azure Blob Storage.  
--Create the external table called ClickStream.  
CREATE EXTERNAL TABLE ClickStreamExt (   
    url VARCHAR(50),  
    event_date DATE,  
    user_IP VARCHAR(50)  
)  
WITH (  
    LOCATION='/logs/clickstream/2015/',  
    DATA_SOURCE = MyAzureStorage,  
    FILE_FORMAT = TextFileFormat)  
;  
  
--Use CREATE TABLE AS SELECT to import the Azure Blob Storage data into a new   
--Synapse Analytics table called ClickStreamData  
CREATE TABLE ClickStreamData   
WITH  
  (  
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = HASH (user_IP)  
  )  
AS SELECT * FROM ClickStreamExt  
;  
```  

<a name="ctas-import-Hadoop-bk"></a>
  
### H. Use CTAS to import Hadoop data from an external table

Applies to: [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
To import data from an external table, simply use CREATE TABLE AS SELECT to select from the external table. The syntax to select data from an external table into [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] is the same as the syntax for selecting data from a regular table.  
  
 The following example defines an external table on a Hadoop cluster. It then uses CREATE TABLE AS SELECT to select from the external table. This imports the data from Hadoop text-delimited files and stores the data into a new [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] table.  
  
```sql  
-- Create the external table called ClickStream.  
CREATE EXTERNAL TABLE ClickStreamExt (   
    url VARCHAR(50),  
    event_date DATE,  
    user_IP VARCHAR(50)  
)  
WITH (  
    LOCATION = 'hdfs://MyHadoop:5000/tpch1GB/employee.tbl',  
    FORMAT_OPTIONS ( FIELD_TERMINATOR = '|')  
)  
;  
  
-- Use your own processes to create the Hadoop text-delimited files 
-- on the Hadoop Cluster.  
  
-- Use CREATE TABLE AS SELECT to import the Hadoop data into a new 
-- table called ClickStreamPDW  
CREATE TABLE ClickStreamPDW   
WITH  
  (  
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = HASH (user_IP)  
  )  
AS SELECT * FROM ClickStreamExt  
;   
```  
 
<a name="examples-workarounds-bk"></a>
 
## Examples using CTAS to replace SQL Server code

Use CTAS to work around some unsupported features. Besides being able to run your code on the data warehouse, rewriting existing code to use CTAS will usually improve performance. This is a result of its fully parallelized design. 

> [!NOTE]
> Try to think "CTAS first". If you think you can solve a problem using `CTAS` then that is generally the best way to approach it - even if you are writing more data as a result.

<a name="ctas-replace-select-into-bk"></a>

### I. Use CTAS instead of SELECT..INTO

Applies to: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

SQL Server code typically uses SELECT..INTO to populate a table with the results of a SELECT statement. This is an example of a SQL Server SELECT..INTO statement.

```sql
SELECT *
INTO    #tmp_fct
FROM    [dbo].[FactInternetSales]
```

This syntax isn't supported in [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and Parallel Data Warehouse. This example shows how to rewrite the previous SELECT..INTO statement as a CTAS statement. You can choose any of the DISTRIBUTION options described in the CTAS syntax. This example uses the ROUND_ROBIN distribution method.

```sql
CREATE TABLE #tmp_fct
WITH
(
    DISTRIBUTION = ROUND_ROBIN
)
AS
SELECT  *
FROM    [dbo].[FactInternetSales]
;
```

<a name="ctas-replace-implicit-joins-bk"></a>
### J. Use CTAS to simplify merge statements

Applies to: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  

Merge statements can be replaced, at least in part, by using `CTAS`. You can consolidate the `INSERT` and the `UPDATE` into a single statement. Any deleted records would need to be closed off in a second statement.

An example of an `UPSERT` follows:

```sql
CREATE TABLE dbo.[DimProduct_upsert]
WITH
(   DISTRIBUTION = HASH([ProductKey])
,   CLUSTERED INDEX ([ProductKey])
)
AS
-- New rows and new versions of rows
SELECT      s.[ProductKey]
,           s.[EnglishProductName]
,           s.[Color]
FROM      dbo.[stg_DimProduct] AS s
UNION ALL  
-- Keep rows that are not being touched
SELECT      p.[ProductKey]
,           p.[EnglishProductName]
,           p.[Color]
FROM      dbo.[DimProduct] AS p
WHERE NOT EXISTS
(   SELECT  *
    FROM    [dbo].[stg_DimProduct] s
    WHERE   s.[ProductKey] = p.[ProductKey]
)
;

RENAME OBJECT dbo.[DimProduct]          TO [DimProduct_old];
RENAME OBJECT dbo.[DimProduct_upsert]  TO [DimProduct];

```

<a name="ctas-data-type-and-nullability-bk"></a>

### K. Explicitly state data type and nullability of output

Applies to: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  

When migrating SQL Server code to [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], you might find you run across this type of coding pattern:

```sql
DECLARE @d DECIMAL(7,2) = 85.455
,       @f FLOAT(24)    = 85.455

CREATE TABLE result
(result DECIMAL(7,2) NOT NULL
)
WITH (DISTRIBUTION = ROUND_ROBIN)

INSERT INTO result
SELECT @d*@f
;
```

Instinctively you might think you should migrate this code to a CTAS and you would be correct. However, there's a hidden issue here.

The following code does NOT yield the same result:

```sql
DECLARE @d DECIMAL(7,2) = 85.455
,       @f FLOAT(24)    = 85.455
;

CREATE TABLE ctas_r
WITH (DISTRIBUTION = ROUND_ROBIN)
AS
SELECT @d*@f as result
;
```

Notice that the column "result" carries forward the data type and nullability values of the expression. This can lead to subtle variances in values if you aren't careful.

Try the following as an example:

```sql
SELECT result,result*@d
from result
;

SELECT result,result*@d
from ctas_r
;
```

The value stored for result is different. As the persisted value in the result column is used in other expressions the error becomes even more significant.

:::image type="content" source="../statements/media/create-table-as-select-azure-sql-data-warehouse\create-table-as-select-results.png" alt-text="A screenshot from SQL Server Management Studio (SSMS) of the CREATE TABLE AS SELECT results.":::

This is important for data migrations. Even though the second query is arguably more accurate there's a problem. The data would be different compared to the source system and that leads to questions of integrity in the migration. This is one of those rare cases where the "wrong" answer is actually the right one!

The reason we see this disparity between the two results is down to implicit type casting. In the first example, the table defines the column definition. When the row is inserted an implicit type conversion occurs. In the second example, there's no implicit type conversion as the expression defines data type of the column. Notice also that the column in the second example has been defined as a NULLable column whereas in the first example it hasn't. When the table was created in the first example column nullability was explicitly defined. In the second example, it was left to the expression and by default this would result in a `NULL` definition.  

To resolve these issues, you must explicitly set the type conversion and nullability in the `SELECT` portion of the `CTAS` statement. You can't set these properties in the create table part.

This example demonstrates how to fix the code:

```sql
DECLARE @d DECIMAL(7,2) = 85.455
,       @f FLOAT(24)    = 85.455

CREATE TABLE ctas_r
WITH (DISTRIBUTION = ROUND_ROBIN)
AS
SELECT ISNULL(CAST(@d*@f AS DECIMAL(7,2)),0) as result
```

Note the following in the example:

- CAST or CONVERT could have been used.
- ISNULL is used to force NULLability not COALESCE.
- ISNULL is the outermost function.
- The second part of the ISNULL is a constant, `0`.

> [!NOTE]
> For the nullability to be correctly set it is vital to use `ISNULL` and not `COALESCE`. `COALESCE` is not a deterministic function and so the result of the expression will always be NULLable. `ISNULL` is different. It is deterministic. Therefore when the second part of the `ISNULL` function is a constant or a literal then the resulting value will be NOT NULL.

This tip isn't just useful for ensuring the integrity of your calculations. It's also important for table partition switching. Imagine you have this table defined as your fact:

```sql
CREATE TABLE [dbo].[Sales]
(
    [date]      INT     NOT NULL
,   [product]   INT     NOT NULL
,   [store]     INT     NOT NULL
,   [quantity]  INT     NOT NULL
,   [price]     MONEY   NOT NULL
,   [amount]    MONEY   NOT NULL
)
WITH
(   DISTRIBUTION = HASH([product])
,   PARTITION   (   [date] RANGE RIGHT FOR VALUES
                    (20000101,20010101,20020101
                    ,20030101,20040101,20050101
                    )
                )
)
;
```

However, the value field is a calculated expression it isn't part of the source data.

To create your partitioned dataset, consider the following example:

```sql
CREATE TABLE [dbo].[Sales_in]
WITH    
(   DISTRIBUTION = HASH([product])
,   PARTITION   (   [date] RANGE RIGHT FOR VALUES
                    (20000101,20010101
                    )
                )
)
AS
SELECT
    [date]    
,   [product]
,   [store]
,   [quantity]
,   [price]   
,   [quantity]*[price]  AS [amount]
FROM [stg].[source]
OPTION (LABEL = 'CTAS : Partition IN table : Create')
;
```

The query would run perfectly fine. The problem comes when you try to perform the partition switch. The table definitions don't match. To make the table definitions, match the CTAS needs to be modified.

```sql
CREATE TABLE [dbo].[Sales_in]
WITH    
(   DISTRIBUTION = HASH([product])
,   PARTITION   (   [date] RANGE RIGHT FOR VALUES
                    (20000101,20010101
                    )
                )
)
AS
SELECT
    [date]    
,   [product]
,   [store]
,   [quantity]
,   [price]   
,   ISNULL(CAST([quantity]*[price] AS MONEY),0) AS [amount]
FROM [stg].[source]
OPTION (LABEL = 'CTAS : Partition IN table : Create');
```

You can see therefore that type consistency and maintaining nullability properties on a CTAS is a good engineering best practice. It helps to maintain integrity in your calculations and also ensures that partition switching is possible.

### L. Create an ordered clustered columnstore index with MAXDOP 1

```sql
CREATE TABLE Table1 WITH (DISTRIBUTION = HASH(c1), CLUSTERED COLUMNSTORE INDEX ORDER(c1) )
AS SELECT * FROM ExampleTable
OPTION (MAXDOP 1);
```

 
## Next steps

- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../statements/create-external-data-source-transact-sql.md)
- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)](../statements/create-external-file-format-transact-sql.md)
- [CREATE EXTERNAL TABLE (Transact-SQL)](../statements/create-external-table-transact-sql.md)
- [CREATE EXTERNAL TABLE AS SELECT (Transact-SQL)](../statements/create-external-table-as-select-transact-sql.md)
- [CREATE TABLE (Azure Synapse Analytics)](../statements/create-table-azure-sql-data-warehouse.md)
- [DROP TABLE (Transact-SQL)](../statements/drop-table-transact-sql.md)
- [DROP EXTERNAL TABLE (Transact-SQL)](../statements/drop-external-table-transact-sql.md)
- [ALTER TABLE (Transact-SQL)](../statements/alter-table-transact-sql.md)
- [ALTER EXTERNAL TABLE (Transact-SQL)](create-external-table-transact-sql.md)

::: moniker-end

::: moniker range="=fabric"
[!INCLUDE[applies-to-version/fabricdw](../../includes/applies-to-version/fabric-dw.md)]

CREATE TABLE AS SELECT (CTAS) is one of the most important T-SQL features available. It's a fully parallelized operation that creates a new table based on the output of a SELECT statement. CTAS is the simplest and fastest way to create a copy of a table.
 
For example, use CTAS in [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)] to:  
  
- Create a copy of a table with some of the columns of the source table.  
- Create a table that is the result of a query that joins other tables.

For more information on using CTAS on your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)], see [Ingest data into your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] using Transact-SQL](/fabric/data-warehouse/ingest-data-tsql).

> [!NOTE]  
> Since CTAS adds to the capabilities of creating a table, this topic tries not to repeat the CREATE TABLE topic. Instead, it describes the differences between the CTAS and CREATE TABLE statements. For the CREATE TABLE details, see [CREATE TABLE](create-table-azure-sql-data-warehouse.md?version=fabric&preserve-view=true) statement. 
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
<a name="syntax-bk-fabric"></a>

## Syntax

```syntaxsql
CREATE TABLE { database_name.schema_name.table_name | schema_name.table_name | table_name }
    AS <select_statement>  
[;]  

<select_statement> ::=  
    SELECT select_criteria  
```  

<a name="arguments-bk-fabric"></a>
  
## Arguments

For details, see the [Arguments in CREATE TABLE for Microsoft Fabric](create-table-azure-sql-data-warehouse.md?version=fabric&preserve-view=true#arguments).

<a name="column-options-bk-fabric"></a>

### Column options

`column_name` [ ,...`n` ]   
 Column names don't allow the [column options](create-table-azure-sql-data-warehouse.md?version=fabric&preserve-view=true#ColumnOptions) mentioned in CREATE TABLE.  Instead, you can provide an optional list of one or more column names for the new table. The columns in the new table use the names you specify. When you specify column names, the number of columns in the column list must match the number of columns in the select results. If you don't specify any column names, the new target table uses the column names in the select statement results.
  
 You can't specify any other column options such as data types, collation, or nullability. Each of these attributes is derived from the results of the `SELECT` statement. However, you can use the SELECT statement to change the attributes. 

<a name="select-options-bk-fabric"></a>

### SELECT statement

The SELECT statement is the fundamental difference between CTAS and CREATE TABLE.  

#### `SELECT` *select_criteria*  

 Populates the new table with the results from a SELECT statement. *select_criteria* is the body of the SELECT statement that determines which data to copy to the new table. For information about SELECT statements, see [SELECT (Transact-SQL)](../queries/select-transact-sql.md?version=fabric&preserve-view=true).  
 
<a name="permissions-bk-fabric"></a>  

> [!NOTE]  
> In Microsoft Fabric, the use of variables in CTAS is not allowed.
  
## Permissions

CTAS requires `SELECT` permission on any objects referenced in the *select_criteria*.

For permissions to create a table, see [Permissions](create-table-azure-sql-data-warehouse.md?version=fabric&preserve-view=true#permissions) in CREATE TABLE. 
  
<a name="general-remarks-bk-fabric"></a>
  
## Remarks

For details, see [General Remarks](create-table-azure-sql-data-warehouse.md?version=fabric&preserve-view=true#GeneralRemarks) in CREATE TABLE.

<a name="limitations-bk-fabric"></a>

## Limitations and restrictions

[SET ROWCOUNT (Transact-SQL)](../statements/set-rowcount-transact-sql.md) has no effect on CTAS. To achieve a similar behavior, use [TOP (Transact-SQL)](../queries/top-transact-sql.md?version=fabric&preserve-view=true).  
 
For details, see [Limitations and Restrictions](create-table-azure-sql-data-warehouse.md?version=fabric&preserve-view=true#LimitationsRestrictions) in CREATE TABLE.

<a name="locking-behavior-bk-fabric"></a>
  
## Locking behavior

 For details, see [Locking Behavior](create-table-azure-sql-data-warehouse.md?version=fabric&preserve-view=true#LockingBehavior) in CREATE TABLE.
 
<a name="examples-copy-table-bk-fabric"></a>

## Examples for copying a table

For more information on using CTAS on your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)], see [Ingest data into your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] using Transact-SQL](/fabric/data-warehouse/ingest-data-tsql).

<a name="ctas-copy-table-bk"></a>

### A. Use CTAS to change column attributes
 
This example uses CTAS to change data types and nullability for several columns in the `DimCustomer2` table.  
  
```sql  
-- Original table 
CREATE TABLE [dbo].[DimCustomer2] (  
    [CustomerKey] INT NOT NULL,  
    [GeographyKey] INT NULL,  
    [CustomerAlternateKey] VARCHAR(15)NOT NULL  
)  
  
-- CTAS example to change data types and nullability of columns
CREATE TABLE test  
AS  
SELECT  
    CustomerKey AS CustomerKeyNoChange,  
    CustomerKey*1 AS CustomerKeyChangeNullable,  
    CAST(CustomerKey AS DECIMAL(10,2)) AS CustomerKeyChangeDataTypeNullable,  
    ISNULL(CAST(CustomerKey AS DECIMAL(10,2)),0) AS CustomerKeyChangeDataTypeNotNullable,  
    GeographyKey AS GeographyKeyNoChange,  
    ISNULL(GeographyKey,0) AS GeographyKeyChangeNotNullable,  
    CustomerAlternateKey AS CustomerAlternateKeyNoChange,  
    CASE WHEN CustomerAlternateKey = CustomerAlternateKey 
        THEN CustomerAlternateKey END AS CustomerAlternateKeyNullable,  
FROM [dbo].[DimCustomer2]  
  
-- Resulting table 
CREATE TABLE [dbo].[test] (
    [CustomerKeyNoChange] INT NOT NULL, 
    [CustomerKeyChangeNullable] INT NULL, 
    [CustomerKeyChangeDataTypeNullable] DECIMAL(10, 2) NULL, 
    [CustomerKeyChangeDataTypeNotNullable] DECIMAL(10, 2) NOT NULL, 
    [GeographyKeyNoChange] INT NULL, 
    [GeographyKeyChangeNotNullable] INT NOT NULL, 
    [CustomerAlternateKeyNoChange] VARCHAR(15) NOT NULL, 
    [CustomerAlternateKeyNullable] VARCHAR(15) NULL, 
NOT NULL
)
```

### B. Use CTAS to create a table with fewer columns

The following example creates a table named `myTable (c, ln)`. The new table only has two columns. It uses the column aliases in the SELECT statement for the names of the columns.  
  
```sql  
CREATE TABLE myTable  
AS SELECT CustomerKey AS c, LastName AS ln  
    FROM dimCustomer; 
```  

<a name="ctas-replace-select-into-bk-fabric"></a>

### C. Use CTAS instead of SELECT..INTO

SQL Server code typically uses SELECT..INTO to populate a table with the results of a SELECT statement. This is an example of a SQL Server SELECT..INTO statement.

```sql
SELECT *
INTO    NewFactTable
FROM    [dbo].[FactInternetSales]
```

This example shows how to rewrite the previous SELECT..INTO statement as a CTAS statement. 

```sql
CREATE TABLE NewFactTable
AS
SELECT  *
FROM    [dbo].[FactInternetSales]
;
```

<a name="ctas-replace-implicit-joins-bk-fabric"></a>

### D. Use CTAS to simplify merge statements

Merge statements can be replaced, at least in part, by using `CTAS`. You can consolidate the `INSERT` and the `UPDATE` into a single statement. Any deleted records would need to be closed off in a second statement.

An example of an `UPSERT` follows:

```sql
CREATE TABLE dbo.[DimProduct_upsert]
AS
-- New rows and new versions of rows
SELECT      s.[ProductKey]
,           s.[EnglishProductName]
,           s.[Color]
FROM      dbo.[stg_DimProduct] AS s
UNION ALL  
-- Keep rows that are not being touched
SELECT      p.[ProductKey]
,           p.[EnglishProductName]
,           p.[Color]
FROM      dbo.[DimProduct] AS p
WHERE NOT EXISTS
(   SELECT  *
    FROM    [dbo].[stg_DimProduct] s
    WHERE   s.[ProductKey] = p.[ProductKey]
)
;
```

## Next steps

- [Create tables on [!INCLUDE[fabric-data-warehouse](../../includes/fabric-dw.md)] in [!INCLUDE[microsoft-fabric](../../includes/fabric.md)]](/fabric/data-warehouse/create-table)
- [What is data warehousing in [!INCLUDE [fabric](../../includes/fabric.md)]?](/fabric/data-warehouse/data-warehousing)
- [Ingest data into your [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] using Transact-SQL](/fabric/data-warehouse/ingest-data-tsql)
::: moniker-end
