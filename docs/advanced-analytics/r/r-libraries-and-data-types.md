---
title: R-to-SQL data type conversions - SQL Server Machine Learning Services
description: Review the implicit and explicit data type converstions between R and SQL Server in data science and machine learning solutions.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/10/2018  
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# Data type mappings betweenR and SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

For R solutions that run on the R integration feature in SQL Server Machine Learning Services, review the list of unsupported data types, and data type conversions that might be performed implicitly when data is passed between R libraries and SQL Server.

## Base R version

SQL Server 2016 R Services and SQL Server 2017 Machine Learning Services with R, are aligned with specific releases of Microsoft R Open. For example, the latest release, SQL Server 2017 Machine Learning Services, is built on Microsoft R Open 3.3.3.

To view the R version associated with a particular instance of SQL Server, open **RGui**. For the default instance, the path would be as follows: `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\bin\x64\`

The tool loads base R and other libraries. Package version information is provided in a notification for each package that is loaded at session start up. 

## R and SQL Data Types

Whereas [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports several dozen data types, R has a limited number of scalar data types (numeric, integer, complex, logical, character, date/time and raw). As a result, whenever you use data from  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in R scripts, data might be implicitly converted to a compatible data type. However, often an exact conversion cannot be performed automatically, and an error is returned, such as "Unhandled SQL data type".

This section lists the implicit conversions that are provided, and lists unsupported data types. Some guidance is provided for mapping data types between R and SQL Server.

## Implicit data type conversions between R and SQL Server

The following table shows the changes in data types and values when data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is used in an R script and then returned to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

|SQL type|R class|RESULT SET type|Comments|
|-|-|-|-|
|**bigint**|`numeric`|**float**||
|**binary(n)**<br /><br /> n <= 8000|`raw`|**varbinary(max)**|Only allowed as input parameter and output|
|**bit**|`logical`|**bit**||
|**char(n)**<br /><br /> n <= 8000|`character`|**varchar(max)**||
|**datetime**|`POSIXct`|**datetime**|Represented as GMT|
|**date**|`POSIXct`|**datetime**|Represented as GMT|
|**decimal(p,s)**|`numeric`|**float**||
|**float**|`numeric`|**float**||
|**int**|`integer`|**int**||
|**money**|`numeric`|**float**||
|**numeric(p,s)**|`numeric`|**float**||
|**real**|`numeric`|**float**||
|**smalldatetime**|`POSIXct`|**datetime**|Represented as GMT|
|**smallint**|`integer`|**int**||
|**smallmoney**|`numeric`|**float**||
|**tinyint**|`integer`|**int**||
|**uniqueidentifier**|`character`|**varchar(max)**||
|**varbinary(n)**<br /><br /> n <= 8000|`raw`|**varbinary(max)**|Only allowed as input parameter and output|
|**varbinary(max)**|`raw`|**varbinary(max)**|Only allowed as input parameter and output|
|**varchar(n)**<br /><br /> n <= 8000|`character`|**varchar(max)**||


## Data types not supported by R

Of the categories of data types supported by the [SQL Server type system](../../t-sql/data-types/data-types-transact-sql.md), the following types are likely to pose problems when passed to R code:

+ Data types listed in the **Other** section of the SQL type system article: **cursor**, **timestamp**, **hierarchyid**, **uniqueidentifier**, **sql_variant**, **xml**, **table**
+ All spatial types
+ **image**

## Data types that might convert poorly

+ Most datetime types should work, except for **datetimeoffset** 
+ Most numeric data types are supported, but conversions might fail for **money** and **smallmoney**
+ **varchar** is supported, but because SQL Server uses Unicode as a rule, use of **nvarchar** and other Unicode text data types is recommended where possible.
+ Functions from the RevoScaleR library prefixed with rx can handle the SQL binary data types (**binary** and **varbinary**), but in most scenarios special handling will be required for these types. Most R code cannot work with binary columns.

  
 For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)

## Changes in data types between SQL Server 2016 and earlier versions

Microsoft SQL Server 2016 and Microsoft Azure SQL Database include improvements in data type conversions and in several other operations. Most of these improvements offer increased precision when you deal with floating-point types, as well as minor changes to operations on classic **datetime** types.

These improvements are all available by default when you use a database compatibility level of 130 or later. However, if you use a different compatibility level, or connect to a database using an older version, you might see differences in the precision of numbers or other results. 

For more information, see [SQL Server 2016 improvements in handling some data types and uncommon operations](https://support.microsoft.com/help/4010261/sql-server-2016-improvements-in-handling-some-data-types-and-uncommon-).
 

## Verify R and SQL data schemas in advance 

In general, whenever you have any doubt about how a particular data type or data structure is being used in R, use the  `str()` function to get the internal structure and type of the R object. The result of the function is printed to the R console and is also available in the query results, in the **Messages** tab in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. 

When retrieving data from a database for use in R code, you should always eliminate columns that cannot be used in R, as well as columns that are not useful for analysis, such as GUIDS (uniqueidentifier), timestamps and other columns used for auditing, or lineage information created by ETL processes. 

Note that inclusion of unnecessary columns can greatly reduce the performance of R code, especially if high cardinality columns are used as factors. Therefore, we recommend that you use SQL Server system stored procedures and information views to get the data types for a given table in advance, and eliminate or convert incompatible columns. For more information, see [Information Schema Views in Transact-SQL](../../relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)

If a particular [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type is not supported by R, but you need to use the columns of data in the R script, we recommend that you use the [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md) functions to ensure that the data type conversions are performed as intended before using the data in your R script.  

> [!WARNING]
> If you use the **rxDataStep** to drop incompatible columns while moving data, be aware that the arguments _varsToKeep_ and _varsToDrop_ are not supported for the **RxSqlServerData** data source type.


## Examples

### Example 1: Implicit conversion

The following example demonstrates how data is transformed when making the round-trip between SQL Server and R.

The query gets a series of values from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table, and uses the stored procedure  [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to output the values using the R runtime.

```sql
CREATE TABLE MyTable (    
 c1 int,    
 c2 varchar(10),    
 c3 uniqueidentifier    
);    
go    
INSERT MyTable VALUES(1, 'Hello', newid());    
INSERT MyTable VALUES(-11, 'world', newid());    
SELECT * FROM MyTable;    
  
EXECUTE sp_execute_external_script    
 @language = N'R'    
 , @script = N'    
inputDataSet["cR"] <- c(4, 2)    
str(inputDataSet)    
outputDataSet <- inputDataSet'    
 , @input_data_1 = N'SELECT c1, c2, c3 FROM MyTable'    
 , @input_data_1_name = N'inputDataSet'    
 , @output_data_1_name = N'outputDataSet'    
 WITH RESULT SETS((C1 int, C2 varchar(max), C3 varchar(max), C4 float));  
```

**Results**

||||||
|-|-|-|-|-|
||C1|C2|C3|C4|
|1|1|Hello|6e225611-4b58-4995-a0a5-554d19012ef1|4|
|1|-11|world|6732ea46-2d5d-430b-8ao1-86e7f3351c3e|2|

Note the use of the `str` function in R to get the schema of the output data. This function returns the following information:

<code>'data.frame':2 obs. of  4 variables:</code>
<code> $ c1: int  1 -11</code>
<code> $ c2: Factor w/ 2 levels "Hello","world": 1 2</code>
<code> $ c3: Factor w/ 2 levels "6732EA46-2D5D-430B-8A01-86E7F3351C3E",..: 2 1</code>
<code> $ cR: num  4 2</code>

From this, you can see that the following data type conversions were implicitly performed as part of this query:

-   **Column C1**. The column is represented as **int** in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], `integer` in R, and **int** in the output result set.  
  
     No type conversion was performed.  
  
-   **Column C2**. The column is represented as **varchar(10)** in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], `factor` in R, and **varchar(max)** in the output.  
  
     Note how the output changes; any string from R (either a factor or a regular string) will be represented as **varchar(max)**, no matter what the length of the strings is.  
  
-   **Column C3**.  The column is represented as **uniqueidentifier** in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], `character` in R, and **varchar(max)** in the output.
  
     Note the data type conversion that happens. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports the **uniqueidentifier** but R does not; therefore, the identifiers are represented as strings.
  
-   **Column C4**. The column contains values generated by the R script and not present in the original data.


## Example 2: Dynamic column selection using R

The following example shows how you can use R code to check for invalid column types. The gets the schema of a specified table using the SQL Server system views, and removes any columns that have a specified invalid type.

```R
connStr <- "Server=.;Database=TestDB;Trusted_Connection=Yes"
data <- RxSqlServerData(connectionString = connStr, sqlQuery = "SELECT COLUMN_NAME FROM TestDB.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'testdata' AND DATA_TYPE <> 'image';")
columns <- rxImport(data)
columnList <- do.call(paste, c(as.list(columns$COLUMN_NAME), sep = ","))
sqlQuery <- paste("SELECT", columnList, "FROM testdata")
```

## See also

