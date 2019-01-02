---
title: "CREATE TABLE AS SELECT (Azure SQL Data Warehouse) | Microsoft Docs"
ms.custom: ""
ms.date: "10/07/2016"
ms.prod: ""
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: d1e08f88-64ef-4001-8a66-372249df2533
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# CREATE TABLE AS SELECT (Azure SQL Data Warehouse)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

CREATE TABLE AS SELECT (CTAS) is one of the most important T-SQL features available. It is a fully parallelized operation that creates a new table based on the output of a SELECT statement. CTAS is the simplest and fastest way to create a copy of a table.   
 
 For example, use CTAS to:  
  
-   Re-create a table with a different hash distribution column.
-   Re-create a table as replicated.   
-   Create a columnstore index on just some of the columns in the table.  
-   Query or import external data.  

> [!NOTE]  
> Since CTAS adds to the capabilities of creating a table, this topic tries not to repeat the CREATE TABLE topic. Instead, it describes the differences between the CTAS and CREATE TABLE statements. For the CREATE TABLE details, see [CREATE TABLE (Azure SQL Data Warehouse)](https://msdn.microsoft.com/library/mt203953/) statement. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
<a name="syntax-bk"></a>

## Syntax   

```  
CREATE TABLE [ database_name . [ schema_name ] . | schema_name. ] table_name   
    [ ( column_name [ ,...n ] ) ]  
    WITH ( 
      <distribution_option> -- required
      [ , <table_option> [ ,...n ] ]    
    )  
    AS <select_statement>   
[;]  

<distribution_option> ::=
    { 
        DISTRIBUTION = HASH ( distribution_column_name ) 
      | DISTRIBUTION = ROUND_ROBIN 
      | DISTRIBUTION = REPLICATE
    }   

<table_option> ::= 
    {   
        CLUSTERED COLUMNSTORE INDEX --default for SQL Data Warehouse 
      | HEAP --default for Parallel Data Warehouse   
      | CLUSTERED INDEX ( { index_column_name [ ASC | DESC ] } [ ,...n ] ) --default is ASC 
    }  
    | PARTITION ( partition_column_name RANGE [ LEFT | RIGHT ] --default is LEFT  
        FOR VALUES ( [ boundary_value [,...n] ] ) ) 
  
<select_statement> ::=  
    [ WITH <common_table_expression> [ ,...n ] ]  
    SELECT select_criteria  

```  

<a name="arguments-bk"></a>
  
## Arguments  
For details, see the [Arguments section](https://msdn.microsoft.com/library/mt203953/#Arguments) in CREATE TABLE.  

<a name="column-options-bk"></a>

### Column options
`column_name` [ ,...`n` ]   
 Column names do not allow the [column options](https://msdn.microsoft.com/library/mt203953/#ColumnOptions) mentioned in CREATE TABLE.  Instead, you can provide an optional list of one or more column names for the new table. The columns in the new table will use the names you specify. When you specify column names, the number of columns in the column list must match the number of columns in the select results. If you don't specify any column names, the new target table will use the column names in the select statement results. 
  
 You cannot specify any other column options such as data types, collation, or nullability. Each of these attributes is derived from the results of the `SELECT` statement. However, you can use the SELECT statement to change the attributes. For an example, see [Use CTAS to change column attributes](#ctas-change-column-attributes-bk).   

<a name="table-distribution-options-bk"></a>

### Table distribution options

`DISTRIBUTION` = `HASH` ( *distribution_column_name* ) | ROUND_ROBIN | REPLICATE      
The CTAS statement requires a distribution option and does not have default values. This is different from CREATE TABLE which has defaults. 

For details and to understand how to choose the best distribution column, see the [Table distribution options](https://msdn.microsoft.com/library/mt203953/#TableDistributionOptions) section in CREATE TABLE. 

<a name="table-partition-options-bk"></a>

### Table partition options
The CTAS statement creates a non-partitioned table by default, even if the source table is partitioned. To create a partitioned table with the CTAS statement, you must specify the partition option. 

For details, see the [Table partition options](https://msdn.microsoft.com/library/mt203953/#TablePartitionOptions) section in CREATE TABLE.

<a name="select-options-bk"></a>

### Select options
The select statement is the fundamental difference between CTAS and CREATE TABLE.  

 `WITH` *common_table_expression*  
 Specifies a temporary named result set, known as a common table expression (CTE). For more information, see [WITH common_table_expression &#40;Transact-SQL&#41;](../../t-sql/queries/with-common-table-expression-transact-sql.md).  
  
 `SELECT` *select_criteria*  
 Populates the new table with the results from a SELECT statement. *select_criteria* is the body of the SELECT statement that determines which data to copy to the new table. For information about SELECT statements, see [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md).  
  
<a name="permissions-bk"></a>  
  
## Permissions  
CTAS requires `SELECT` permission on any objects referenced in the *select_criteria*.

For permissions to create a table, see [Permissions](https://msdn.microsoft.com/library/mt203953/#Permissions) in CREATE TABLE. 
  
<a name="general-remarks-bk"></a>
  
## General Remarks
For details, see [General Remarks](https://msdn.microsoft.com/library/mt203953/#GeneralRemarks) in CREATE TABLE.

<a name="limitations-bk"></a>

## Limitations and Restrictions  
Azure SQL Data Warehouse does not yet support auto create or auto update statistics.  In order to get the best performance from your queries, it's important to create statistics on all columns of all tables after you run CTAS and after any substantial changes occur in the data. For more information, see [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md).

[SET ROWCOUNT &#40;Transact-SQL&#41;](../../t-sql/statements/set-rowcount-transact-sql.md) has no effect on CTAS. To achieve a similar behavior, use [TOP &#40;Transact-SQL&#41;](../../t-sql/queries/top-transact-sql.md).  
 
For details, see [Limitations and Restrictions](https://msdn.microsoft.com/library/mt203953/#LimitationsRestrictions) in CREATE TABLE.

<a name="locking-behavior-bk"></a>
  
## Locking Behavior  
 For details, see [Locking Behavior](https://msdn.microsoft.com/library/mt203953/#LockingBehavior) in CREATE TABLE.
 
<a name="performance-bk"></a>
 
 ## Performance 

For a hash-distributed table, you can use CTAS to choose a different distribution column to achieve better performance for joins and aggregations. If choosing a different distribution column is not your goal, you will have the best CTAS performance if you specify the same distribution column since this will avoid re-distributing the rows. 

If you are using CTAS to create table and performance is not a factor, you can specify `ROUND_ROBIN` to avoid having to decide on a distribution column.

To avoid data movement in subsequent queries, you can specify `REPLICATE` at the cost of increased storage for loading a full copy of the table on each Compute node.  


<a name="examples-copy-table-bk"></a>

## Examples for copying a table

<a name="ctas-copy-table-bk"></a>

### A. Use CTAS to copy a table 
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse

Perhaps one of the most common uses of `CTAS` is creating a copy of a table so that you can change the DDL. If for example you originally created your table as `ROUND_ROBIN` and now want change it to a table distributed on a column, `CTAS` is how you would change the distribution column. `CTAS` can also be used to change partitioning, indexing, or column types.

Let's say you created this table using the default distribution type of `ROUND_ROBIN` distributed since no distribution column was specified in the `CREATE TABLE`.

```sql
CREATE TABLE FactInternetSales
(
	ProductKey int NOT NULL,
	OrderDateKey int NOT NULL,
	DueDateKey int NOT NULL,
	ShipDateKey int NOT NULL,
	CustomerKey int NOT NULL,
	PromotionKey int NOT NULL,
	CurrencyKey int NOT NULL,
	SalesTerritoryKey int NOT NULL,
	SalesOrderNumber nvarchar(20) NOT NULL,
	SalesOrderLineNumber tinyint NOT NULL,
	RevisionNumber tinyint NOT NULL,
	OrderQuantity smallint NOT NULL,
	UnitPrice money NOT NULL,
	ExtendedAmount money NOT NULL,
	UnitPriceDiscountPct float NOT NULL,
	DiscountAmount float NOT NULL,
	ProductStandardCost money NOT NULL,
	TotalProductCost money NOT NULL,
	SalesAmount money NOT NULL,
	TaxAmt money NOT NULL,
	Freight money NOT NULL,
	CarrierTrackingNumber nvarchar(25),
	CustomerPONumber nvarchar(25)
);
```

Now you want to create a new copy of this table with a clustered columnstore index so that you can take advantage of the performance of clustered columnstore tables. You also want to distribute this table on ProductKey since you are anticipating joins on this column and want to avoid data movement during joins on ProductKey. Lastly you also want to add partitioning on OrderDateKey so that you can quickly delete old data by dropping old partitions. Here is the CTAS statement which would copy your old table into a new table.

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
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse

This example uses CTAS to change data types, nullability, and collation for several columns in the DimCustomer2 table.  
  
```  
-- Original table 
CREATE TABLE [dbo].[DimCustomer2] (  
    [CustomerKey] int NOT NULL,  
    [GeographyKey] int NULL,  
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
    [CustomerKeyNoChange] int NOT NULL, 
    [CustomerKeyChangeNullable] int NULL, 
    [CustomerKeyChangeDataTypeNullable] decimal(10, 2) NULL, 
    [CustomerKeyChangeDataTypeNotNullable] decimal(10, 2) NOT NULL, 
    [GeographyKeyNoChange] int NULL, 
    [GeographyKeyChangeNotNullable] int NOT NULL, 
    [CustomerAlternateKeyNoChange] nvarchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, 
    [CustomerAlternateKeyNullable] nvarchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL, 
    [CustomerAlternateKeyChangeCollation] nvarchar(15) COLLATE Latin1_General_CS_AS_KS_WS NOT NULL
)
WITH (DISTRIBUTION = ROUND_ROBIN);
```
 
As a final step, you can use [RENAME &#40;Transact-SQL&#41;](../../t-sql/statements/rename-transact-sql.md) to switch the table names. This makes DimCustomer2 be the new table.

```
RENAME OBJECT DimCustomer2 TO DimCustomer2_old;
RENAME OBJECT test TO DimCustomer2;

DROP TABLE DimCustomer2_old;
```

<a name="examples-table-distribution-bk"></a>

## Examples for table distribution

<a name="ctas-change-distribution-method-bk"></a>

### C. Use CTAS to change the distribution method for a table
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse

This simple example shows how to change the distribution method for a table. To show the mechanics of how to do this, it changes a hash-distributed table to round-robin and then changes the round-robin table back to hash distributed. The final table matches the original table. 

In most cases you won't need to change a hash-distributed table to a round-robin table. More often, you might need to change a round-robin table to a hash distributed table. For example, you might initially load a new table as round-robin and then later move it to a hash-distributed table to get better join performance.

This example uses the AdventureWorksDW sample database. To load the SQL Data Warehouse version, see [Load sample data into SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-load-sample-databases/)
 
```
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

```
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
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse 

This example applies for converting round-robin or hash-distributed tables to a replicated table. This particular example takes the previous method of changing the distribution type one step further.  Since DimSalesTerritory is a dimension and likely a smaller table, you can choose to re-create the table as replicated to avoid data movement when joining to other tables. 

```
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
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse 

The following example creates a round-robin distributed table named `myTable (c, ln)`. The new table only has two columns. It uses the column aliases in the SELECT statement for the names of the columns.  
  
```  
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
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse
  
This query shows the basic syntax for using a query join hint with the CTAS statement. After the query is submitted, [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] applies the hash join strategy when it generates the query plan for each individual distribution. For more information on the hash join query hint, see [OPTION Clause &#40;Transact-SQL&#41;](../../t-sql/queries/option-clause-transact-sql.md).  
  
```  
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
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse  

To import data from an external table, simply use CREATE TABLE AS SELECT to select from the external table. The syntax to select data from an external table into [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] is the same as the syntax for selecting data from a regular table.  
  
 The following example defines an external table on data in an Azure blob storage account. It then uses CREATE TABLE AS SELECT to select from the external table. This imports the data from Azure blob storage text-delimited files and stores the data into a new [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] table.  
  
```  
--Use your own processes to create the text-delimited files on Azure blob storage.  
--Create the external table called ClickStream.  
CREATE EXTERNAL TABLE ClickStreamExt (   
    url varchar(50),  
    event_date date,  
    user_IP varchar(50)  
)  
WITH (  
    LOCATION='/logs/clickstream/2015/',  
    DATA_SOURCE = MyAzureStorage,  
    FILE_FORMAT = TextFileFormat)  
;  
  
--Use CREATE TABLE AS SELECT to import the Azure blob storage data into a new   
--SQL Data Warehouse table called ClickStreamData  
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
  
```  
-- Create the external table called ClickStream.  
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
>

<a name="ctas-replace-select-into-bk"></a>

### I. Use CTAS instead of SELECT..INTO  
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse

SQL Server code typically uses SELECT..INTO to populate a table with the results of a SELECT statement. This is an example of a SQL Server SELECT..INTO statement.

```sql
SELECT *
INTO    #tmp_fct
FROM    [dbo].[FactInternetSales]
```

This syntax is not supported in SQL Data Warehouse and Parallel Data Warehouse. This example shows how to rewrite the previous SELECT..INTO statement as a CTAS statement. You can choose any of the DISTRIBUTION options described in the CTAS syntax. This example uses the ROUND_ROBIN distribution method.

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

### J. Use CTAS and implicit joins to replace ANSI joins in the `FROM` clause of an `UPDATE` statement  
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse  

You may find you have a complex update that joins more than two tables together using ANSI joining syntax to perform the UPDATE or DELETE.

Imagine you had to update this table:

```sql
CREATE TABLE [dbo].[AnnualCategorySales]
(	[EnglishProductCategoryName]	NVARCHAR(50)	NOT NULL
,	[CalendarYear]					SMALLINT		NOT NULL
,	[TotalSalesAmount]				MONEY			NOT NULL
)
WITH
(
	DISTRIBUTION = ROUND_ROBIN
)
;
```

The original query might have looked something like this:

```sql
UPDATE	acs
SET		[TotalSalesAmount] = [fis].[TotalSalesAmount]
FROM	[dbo].[AnnualCategorySales] 	AS acs
JOIN	(
		SELECT	[EnglishProductCategoryName]
		,		[CalendarYear]
		,		SUM([SalesAmount])				AS [TotalSalesAmount]
		FROM	[dbo].[FactInternetSales]		AS s
		JOIN	[dbo].[DimDate]					AS d	ON s.[OrderDateKey]				= d.[DateKey]
		JOIN	[dbo].[DimProduct]				AS p	ON s.[ProductKey]				= p.[ProductKey]
		JOIN	[dbo].[DimProductSubCategory]	AS u	ON p.[ProductSubcategoryKey]	= u.[ProductSubcategoryKey]
		JOIN	[dbo].[DimProductCategory]		AS c	ON u.[ProductCategoryKey]		= c.[ProductCategoryKey]
		WHERE 	[CalendarYear] = 2004
		GROUP BY
				[EnglishProductCategoryName]
		,		[CalendarYear]
		) AS fis
ON	[acs].[EnglishProductCategoryName]	= [fis].[EnglishProductCategoryName]
AND	[acs].[CalendarYear]				= [fis].[CalendarYear]
;
```

Since SQL Data Warehouse does not support ANSI joins in the `FROM` clause of an `UPDATE` statement, you cannot use this SQL Server code over without changing it slightly.

You can use a combination of a `CTAS` and an implicit join to replace this code:

```sql
-- Create an interim table
CREATE TABLE CTAS_acs
WITH (DISTRIBUTION = ROUND_ROBIN)
AS
SELECT	ISNULL(CAST([EnglishProductCategoryName] AS NVARCHAR(50)),0)	AS [EnglishProductCategoryName]
,		ISNULL(CAST([CalendarYear] AS SMALLINT),0) 						AS [CalendarYear]
,		ISNULL(CAST(SUM([SalesAmount]) AS MONEY),0)						AS [TotalSalesAmount]
FROM	[dbo].[FactInternetSales]		AS s
JOIN	[dbo].[DimDate]					AS d	ON s.[OrderDateKey]				= d.[DateKey]
JOIN	[dbo].[DimProduct]				AS p	ON s.[ProductKey]				= p.[ProductKey]
JOIN	[dbo].[DimProductSubCategory]	AS u	ON p.[ProductSubcategoryKey]	= u.[ProductSubcategoryKey]
JOIN	[dbo].[DimProductCategory]		AS c	ON u.[ProductCategoryKey]		= c.[ProductCategoryKey]
WHERE 	[CalendarYear] = 2004
GROUP BY
		[EnglishProductCategoryName]
,		[CalendarYear]
;

-- Use an implicit join to perform the update
UPDATE  AnnualCategorySales
SET     AnnualCategorySales.TotalSalesAmount = CTAS_ACS.TotalSalesAmount
FROM    CTAS_acs
WHERE   CTAS_acs.[EnglishProductCategoryName] = AnnualCategorySales.[EnglishProductCategoryName]
AND     CTAS_acs.[CalendarYear]               = AnnualCategorySales.[CalendarYear]
;

--Drop the interim table
DROP TABLE CTAS_acs
;
```

<a name="ctas-replace-ansi-joins-bk"></a>

### K. Use CTAS to specify which data to keep instead of using ANSI joins in the FROM clause of a DELETE statement  
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse  

Sometimes the best approach for deleting data is to use `CTAS`. Rather than deleting the data simply select the data you want to keep. This especially true for `DELETE` statements that use ansi joining syntax since SQL Data Warehouse does not support ANSI joins in the `FROM` clause of a `DELETE` statement.

An example of a converted DELETE statement is available below:

```sql
CREATE TABLE dbo.DimProduct_upsert
WITH
(   Distribution=HASH(ProductKey)
,   CLUSTERED INDEX (ProductKey)
)
AS -- Select Data you wish to keep
SELECT     p.ProductKey
,          p.EnglishProductName
,          p.Color
FROM       dbo.DimProduct p
RIGHT JOIN dbo.stg_DimProduct s
ON         p.ProductKey = s.ProductKey
;

RENAME OBJECT dbo.DimProduct        TO DimProduct_old;
RENAME OBJECT dbo.DimProduct_upsert TO DimProduct;
```

<a name="ctas-simplify-merge-bk"></a>

### L. Use CTAS to simplify merge statements  
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse  

Merge statements can be replaced, at least in part, by using `CTAS`. You can consolidate the `INSERT` and the `UPDATE` into a single statement. Any deleted records would need to be closed off in a second statement.

An example of an `UPSERT` is available below:

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

### M. Explicitly state data type and nullability of output  
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse  

When migrating SQL Server code to SQL Data Warehouse, you might find you run across this type of coding pattern:

```sql
DECLARE @d decimal(7,2) = 85.455
,       @f float(24)    = 85.455

CREATE TABLE result
(result DECIMAL(7,2) NOT NULL
)
WITH (DISTRIBUTION = ROUND_ROBIN)

INSERT INTO result
SELECT @d*@f
;
```

Instinctively you might think you should migrate this code to a CTAS and you would be correct. However, there is a hidden issue here.

The following code does NOT yield the same result:

```sql
DECLARE @d decimal(7,2) = 85.455
,       @f float(24)    = 85.455
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

![CREATE TABLE AS SELECT results](../../t-sql/statements/media/create-table-as-select-results.png)

This is particularly important for data migrations. Even though the second query is arguably more accurate there is a problem. The data would be different compared to the source system and that leads to questions of integrity in the migration. This is one of those rare cases where the "wrong" answer is actually the right one!

The reason we see this disparity between the two results is down to implicit type casting. In the first example the table defines the column definition. When the row is inserted an implicit type conversion occurs. In the second example there is no implicit type conversion as the expression defines data type of the column. Notice also that the column in the second example has been defined as a NULLable column whereas in the first example it has not. When the table was created in the first example column nullability was explicitly defined. In the second example it was just left to the expression and by default this would result in a NULL definition.  

To resolve these issues you must explicitly set the type conversion and nullability in the `SELECT` portion of the `CTAS` statement. You cannot set these properties in the create table part.

The example below demonstrates how to fix the code:

```sql
DECLARE @d decimal(7,2) = 85.455
,       @f float(24)    = 85.455

CREATE TABLE ctas_r
WITH (DISTRIBUTION = ROUND_ROBIN)
AS
SELECT ISNULL(CAST(@d*@f AS DECIMAL(7,2)),0) as result
```

Note the following:
- CAST or CONVERT could have been used
- ISNULL is used to force NULLability not COALESCE
- ISNULL is the outermost function
- The second part of the ISNULL is a constant i.e. 0

> [!NOTE]
> For the nullability to be correctly set it is vital to use `ISNULL` and not `COALESCE`. `COALESCE` is not a deterministic function and so the result of the expression will always be NULLable. `ISNULL` is different. It is deterministic. Therefore when the second part of the `ISNULL` function is a constant or a literal then the resulting value will be NOT NULL.

This tip is not just useful for ensuring the integrity of your calculations. It is also important for table partition switching. Imagine you have this table defined as your fact:

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

However, the value field is a calculated expression it is not part of the source data.

To create your partitioned dataset you might want to do this:

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

The query would run perfectly fine. The problem comes when you try to perform the partition switch. The table definitions do not match. To make the table definitions match the CTAS needs to be modified.

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
 
## See Also  
 [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md)   
 [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)   
 [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)   
 [CREATE EXTERNAL TABLE AS SELECT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-as-select-transact-sql.md)   
 [CREATE TABLE &#40;Azure SQL Data Warehouse&#41;](../../t-sql/statements/create-table-azure-sql-data-warehouse.md) 
 [DROP TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-table-transact-sql.md)   
 [DROP EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-external-table-transact-sql.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [ALTER EXTERNAL TABLE &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/4ae1b23c-67f6-41d0-b614-7a8de914d145)  
  
  


