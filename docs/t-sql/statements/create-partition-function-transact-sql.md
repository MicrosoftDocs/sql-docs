---
title: "CREATE PARTITION FUNCTION (Transact-SQL)"
description: CREATE PARTITION FUNCTION (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "4/22/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CREATE PARTITION FUNCTION"
  - "PARTITION"
  - "PARTITION FUNCTION"
  - "PARTITION_FUNCTION_TSQL"
  - "PARTITION_TSQL"
  - "CREATE_PARTITION_FUNCTION_TSQL"
helpviewer_keywords:
  - "RANGE RIGHT partition functions"
  - "RANGE LEFT partition functions"
  - "partitioned indexes [SQL Server], functions"
  - "functions [SQL Server], creating"
  - "partition functions [SQL Server], creating"
  - "partitioned tables [SQL Server], functions"
  - "CREATE PARTITION FUNCTION statement"
dev_langs:
  - "TSQL"
---
# CREATE PARTITION FUNCTION (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Creates a function in the current database that maps the rows of a table or index into partitions based on the values of a specified column. Using CREATE PARTITION FUNCTION is the first step in creating a [partitioned table or index](../../relational-databases/partitions/partitioned-tables-and-indexes.md). A table or index can have a maximum of 15,000 partitions.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE PARTITION FUNCTION partition_function_name ( input_parameter_type )  
AS RANGE [ LEFT | RIGHT ]   
FOR VALUES ( [ boundary_value [ ,...n ] ] )   
[ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *partition_function_name*  
 Is the name of the partition function. Partition function names must be unique within the database and comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 *input_parameter_type*  
 Is the data type of the column used for partitioning. All data types are valid for use as partitioning columns, except **text**, **ntext**, **image**, **xml**, **timestamp**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, alias data types, or CLR user-defined data types.  
  
 The actual column, known as a partitioning column, is specified in the CREATE TABLE or CREATE INDEX statement.  
  
 *boundary_value*  
 Specifies the boundary values for each partition of a partitioned table or index that uses *partition_function_name*. If *boundary_value* is empty, the partition function maps the whole table or index using *partition_function_name* into a single partition. Only one partitioning column, specified in a CREATE TABLE or CREATE INDEX statement, can be used.  
  
 *boundary_value* is a constant expression that can reference variables. This includes user-defined type variables, or functions and user-defined functions. It cannot reference [!INCLUDE[tsql](../../includes/tsql-md.md)] expressions. *boundary_value* must either match or be implicitly convertible to the data type supplied in *input_parameter_type*, and cannot be truncated during implicit conversion in a way that the size and scale of the value does not match that of its corresponding *input_parameter_type*.  

> [!NOTE]  
>  If *boundary_value* consists of **datetime** or **smalldatetime** literals, these literals are evaluated assuming that us_english is the session language. This behavior is deprecated. To make sure the partition function definition behaves as expected for all session languages, we recommend that you use constants that are interpreted the same way for all language settings, such as the yyyymmdd format; or explicitly convert literals to a specific style. To determine the language session of your server, run `SELECT @@LANGUAGE`.
>
> For more information, see [Nondeterministic conversion of literal date strings into DATE values](../data-types/nondeterministic-convert-date-literals.md).
  
 *...n*  
 Specifies the number of values supplied by *boundary_value*, not to exceed 14,999. The number of partitions created is equal to *n* + 1. The values do not have to be listed in order. If the values are not in order, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] sorts them, creates the function, and returns a warning that the values are not provided in order. The Database Engine returns an error if *n* includes any duplicate values.  
  
 **LEFT** | RIGHT  
 Specifies to which side of each boundary value interval, left or right, the *boundary_value* [ **,**_...n_ ] belongs, when interval values are sorted by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] in ascending order from left to right. If not specified, LEFT is the default.  
  
## Remarks  
 The scope of a partition function is limited to the database that it is created in. Within the database, partition functions reside in a separate namespace from the other functions.  
  
 Any rows whose partitioning column has null values are placed in the left-most partition, unless NULL is specified as a boundary value and RIGHT is indicated. In this case, the left-most partition is an empty partition, and NULL values are placed in the following partition.  
  
## Permissions  
 Any one of the following permissions can be used to execute CREATE PARTITION FUNCTION:  
  
-   ALTER ANY DATASPACE permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.  
  
-   CONTROL or ALTER permission on the database in which the partition function is being created.  
  
-   CONTROL SERVER or ALTER ANY DATABASE permission on the server of the database in which the partition function is being created.  
  
##  <a name="BKMK_examples"></a> Examples  
  
### A. Creating a RANGE LEFT partition function on an int column  
 The following partition function will partition a table or index into four partitions.  
  
```sql  
CREATE PARTITION FUNCTION myRangePF1 (int)  
AS RANGE LEFT FOR VALUES (1, 100, 1000);  
```  
  
 The following table shows how a table that uses this partition function on partitioning column **col1** would be partitioned.  
  
|Partition|1|2|3|4|  
|---------------|-------|-------|-------|-------|  
|**Values**|**col1** <= `1`|**col1** > `1` AND **col1** <= `100`|**col1** > `100` AND **col1** <=`1000`|**col1** > `1000`|  
  
### B. Creating a RANGE RIGHT partition function on an int column  
 The following partition function uses the same values for *boundary_value* [ **,**_...n_ ] as the previous example, except it specifies RANGE RIGHT.  
  
```sql  
CREATE PARTITION FUNCTION myRangePF2 (int)  
AS RANGE RIGHT FOR VALUES (1, 100, 1000);  
```  
  
 The following table shows how a table that uses this partition function on partitioning column **col1** would be partitioned.  
  
|Partition|1|2|3|4|  
|---------------|-------|-------|-------|-------|  
|**Values**|**col1** \< `1`|**col1** >= `1` AND **col1** \< `100`|**col1** >= `100` AND **col1** \< `1000`|**col1** >= `1000`| 
  
### C. Creating a RANGE RIGHT partition function on a datetime column  
 The following partition function partitions a table or index into 12 partitions, one for each month of a year's worth of values in a **datetime** column.  
  
```sql  
CREATE PARTITION FUNCTION [myDateRangePF1] (datetime)  
AS RANGE RIGHT FOR VALUES ('20030201', '20030301', '20030401',  
               '20030501', '20030601', '20030701', '20030801',   
               '20030901', '20031001', '20031101', '20031201');  
```  
  
 The following table shows how a table or index that uses this partition function on partitioning column **datecol** would be partitioned.  
  
|Partition|1|2|...|11|12|  
|---------------|-------|-------|---------|--------|--------|  
|**Values**|**datecol** \< `February 1, 2003`|**datecol** >= `February 1, 2003` AND **datecol** \< `March 1, 2003`||**datecol** >= `November 1, 2003` AND **col1** \< `December 1, 2003`|**datecol** >= `December 1, 2003`| 
  
### D. Creating a partition function on a char column  
 The following partition function partitions a table or index into four partitions.  
  
```sql  
CREATE PARTITION FUNCTION myRangePF3 (char(20))  
AS RANGE RIGHT FOR VALUES ('EX', 'RXE', 'XR');  
```  
  
 The following table shows how a table that uses this partition function on partitioning column **col1** would be partitioned.  
  
|Partition|1|2|3|4|  
|---------------|-------|-------|-------|-------|  
|**Values**|**col1** \< `EX`...|**col1** >= `EX` AND **col1** \< `RXE`...|**col1** >= `RXE` AND **col1** \< `XR`...|**col1** >= `XR`| 
  
### E. Creating 15,000 partitions  
 The following partition function partitions a table or index into 15,000 partitions.  
  
```sql  
--Create integer partition function for 15,000 partitions.  
DECLARE @IntegerPartitionFunction nvarchar(max) = 
    N'CREATE PARTITION FUNCTION IntegerPartitionFunction (int) 
    AS RANGE RIGHT FOR VALUES (';  
DECLARE @i int = 1;  
WHILE @i < 14999  
BEGIN  
SET @IntegerPartitionFunction += CAST(@i as nvarchar(10)) + N', ';  
SET @i += 1;  
END  
SET @IntegerPartitionFunction += CAST(@i as nvarchar(10)) + N');';  
EXEC sp_executesql @IntegerPartitionFunction;  
GO  
```  
  
### F. Creating partitions for multiple years  
 The following partition function partitions a table or index into 50 partitions on a **datetime2** column. There is one partition for each month between January 2007 and January 2011.  
  
```sql  
--Create date partition function with increment by month.  
DECLARE @DatePartitionFunction nvarchar(max) = 
    N'CREATE PARTITION FUNCTION DatePartitionFunction (datetime2) 
    AS RANGE RIGHT FOR VALUES (';  
DECLARE @i datetime2 = '20070101';  
WHILE @i < '20110101'  
BEGIN  
SET @DatePartitionFunction += '''' + CAST(@i as nvarchar(10)) + '''' + N', ';  
SET @i = DATEADD(MM, 1, @i);  
END  
SET @DatePartitionFunction += '''' + CAST(@i as nvarchar(10))+ '''' + N');';  
EXEC sp_executesql @DatePartitionFunction;  
GO  
```  
  
## Next steps

Learn more about table partitioning and related concepts in the following articles: 

- [CREATE PARTITION SCHEME (Transact-SQL)](create-partition-scheme-transact-sql.md)
- [Partitioned tables and indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md)
- [Modify a Partition Function](../../relational-databases/partitions/modify-a-partition-function.md)
- [Modify a Partition Scheme](../../relational-databases/partitions/modify-a-partition-scheme.md)
- [sys.partition_functions (Transact-SQL)](../../relational-databases/system-catalog-views/sys-partition-functions-transact-sql.md)
- [sys.partition_schemes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-partition-schemes-transact-sql.md)
