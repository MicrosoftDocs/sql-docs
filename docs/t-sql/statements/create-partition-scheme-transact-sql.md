---
title: "CREATE PARTITION SCHEME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE PARTITION SCHEME"
  - "SCHEME"
  - "PARTITION SCHEME"
  - "CREATE_PARTITION_SCHEME_TSQL"
  - "SCHEME_TSQL"
  - "PARTITION_SCHEME_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "partitioned indexes [SQL Server], schemes"
  - "partitioned tables [SQL Server], schemes"
  - "CREATE PARTITION SCHEME statement"
  - "partition schemes [SQL Server], creating"
  - "filegroups [SQL Server], partitions"
  - "partitioned indexes [SQL Server], filegroups"
  - "partitioned tables [SQL Server], filegroups"
  - "mapping partitions [SQL Server]"
ms.assetid: 5b21c53a-b4f4-4988-89a2-801f512126e4
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE PARTITION SCHEME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Creates a scheme in the current database that maps the partitions of a partitioned table or index to filegroups. The number and domain of the partitions of a partitioned table or index are determined in a partition function. A partition function must first be created in a [CREATE PARTITION FUNCTION](../../t-sql/statements/create-partition-function-transact-sql.md) statement before creating a partition scheme.  

>[!NOTE]
>In Azure SQL Database only primary filegroups are supported.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
CREATE PARTITION SCHEME partition_scheme_name  
AS PARTITION partition_function_name  
[ ALL ] TO ( { file_group_name | [ PRIMARY ] } [ ,...n ] )  
[ ; ]  
```  
  
## Arguments  
 *partition_scheme_name*  
 Is the name of the partition scheme. Partition scheme names must be unique within the database and comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 *partition_function_name*  
 Is the name of the partition function using the partition scheme. Partitions created by the partition function are mapped to the filegroups specified in the partition scheme. *partition_function_name* must already exist in the database. A single partition cannot contain both FILESTREAM and non-FILESTREAM filegroups.  
  
 ALL  
 Specifies that all partitions map to the filegroup provided in *file_group_name*, or to the primary filegroup if **[**PRIMARY**]** is specified. If ALL is specified, only one *file_group_name* can be specified.  
  
 *file_group_name* | **[** PRIMARY **]** [ **,**_...n_]  
 Specifies the names of the filegroups to hold the partitions specified by *partition_function_name*. *file_group_name* must already exist in the database.  
  
 If **[**PRIMARY**]** is specified, the partition is stored on the primary filegroup. If ALL is specified, only one *file_group_name* can be specified. Partitions are assigned to filegroups, starting with partition 1, in the order in which the filegroups are listed in [**,**_...n_]. The same *file_group_name* can be specified more than one time in [**,**_...n_]. If *n* is not sufficient to hold the number of partitions specified in *partition_function_name*, CREATE PARTITION SCHEME fails with an error.  
  
 If *partition_function_name* generates less partitions than filegroups, the first unassigned filegroup is marked NEXT USED, and an information message displays naming the NEXT USED filegroup. If ALL is specified, the sole *file_group_name* maintains its NEXT USED property for this *partition_function_name*. The NEXT USED filegroup will receive an additional partition if one is created in an ALTER PARTITION FUNCTION statement. To create additional unassigned filegroups to hold new partitions, use ALTER PARTITION SCHEME.  
  
 When you specify the primary filegroup in *file_group_name* [ 1**,**_...n_], PRIMARY must be delimited, as in **[**PRIMARY**]**, because it is a keyword.  
  
 Only PRIMARY is supported for [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)]. See example E below. 
  
## Permissions  
 The following permissions can be used to execute CREATE PARTITION SCHEME:  
  
-   ALTER ANY DATASPACE permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.  
  
-   CONTROL or ALTER permission on the database in which the partition scheme is being created.  
  
-   CONTROL SERVER or ALTER ANY DATABASE permission on the server of the database in which the partition scheme is being created.  
  
## Examples  
  
### A. Creating a partition scheme that maps each partition to a different filegroup  
 The following example creates a partition function to partition a table or index into four partitions. A partition scheme is then created that specifies the filegroups to hold each one of the four partitions. This example assumes the filegroups already exist in the database.  
  
```  
CREATE PARTITION FUNCTION myRangePF1 (int)  
AS RANGE LEFT FOR VALUES (1, 100, 1000);  
GO  
CREATE PARTITION SCHEME myRangePS1  
AS PARTITION myRangePF1  
TO (test1fg, test2fg, test3fg, test4fg);  
```  
  
 The partitions of a table that uses partition function `myRangePF1` on partitioning column **col1** would be assigned as shown in the following table.  
  
||||||  
|-|-|-|-|-|  
|**Filegroup**|`test1fg`|`test2fg`|`test3fg`|`test4fg`|  
|**Partition**|1|2|3|4|  
|**Values**|**col1** <= `1`|**col1** > `1` AND **col1** <= `100`|**col1** > `100` AND **col1** <= `1000`|**col1** > `1000`|  
  
### B. Creating a partition scheme that maps multiple partitions to the same filegroup  
 If all the partitions map to the same filegroup, use the ALL keyword. But if multiple, but not all, partitions are mapped to the same filegroup, the filegroup name must be repeated, as shown in the following example.  
  
```  
CREATE PARTITION FUNCTION myRangePF2 (int)  
AS RANGE LEFT FOR VALUES (1, 100, 1000);  
GO  
CREATE PARTITION SCHEME myRangePS2  
AS PARTITION myRangePF2  
TO ( test1fg, test1fg, test1fg, test2fg );  
```  
  
 The partitions of a table that uses partition function `myRangePF2` on partitioning column **col1** would be assigned as shown in the following table.  
  
||||||  
|-|-|-|-|-|  
|**Filegroup**|`test1fg`|`test1fg`|`test1fg`|`test2fg`|  
|**Partition**|1|2|3|4|  
|**Values**|**col1** <= `1`|**col1** > 1 AND **col1** <= `100`|**col1** > `100` AND **col1** <= `1000`|**col1** > `1000`|  
  
### C. Creating a partition scheme that maps all partitions to the same filegroup  
 The following example creates the same partition function as in the previous examples, and a partition scheme is created that maps all partitions to the same filegroup.  
  
```  
CREATE PARTITION FUNCTION myRangePF3 (int)  
AS RANGE LEFT FOR VALUES (1, 100, 1000);  
GO  
CREATE PARTITION SCHEME myRangePS3  
AS PARTITION myRangePF3  
ALL TO ( test1fg );  
```  
  
### D. Creating a partition scheme that specifies a 'NEXT USED' filegroup  
 The following example creates the same partition function as in the previous examples, and a partition scheme is created that lists more filegroups than there are partitions created by the associated partition function.  
  
```  
CREATE PARTITION FUNCTION myRangePF4 (int)  
AS RANGE LEFT FOR VALUES (1, 100, 1000);  
GO  
CREATE PARTITION SCHEME myRangePS4  
AS PARTITION myRangePF4  
TO (test1fg, test2fg, test3fg, test4fg, test5fg)  
```  
  
 Executing the statement returns the following message.  
  
Partition scheme 'myRangePS4' has been created successfully. 'test5fg' is marked as the next used filegroup in partition scheme 'myRangePS4'.  
  
  
 If partition function `myRangePF4` is changed to add a partition, filegroup `test5fg` receives the newly created partition.  

### E. Creating a partition schema only on PRIMARY - only PRIMARY is supported for [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)]

 The following example creates a partition function to partition a table or index into four partitions. A partition scheme is then created that specifies that all partitions are created in the PRIMARY filegroup.  
  
```  
CREATE PARTITION FUNCTION myRangePF1 (int)  
AS RANGE LEFT FOR VALUES (1, 100, 1000);  
GO  
CREATE PARTITION SCHEME myRangePS1  
AS PARTITION myRangePF1  
ALL TO ( [PRIMARY] );  
```
   
## See Also  
 [CREATE PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-function-transact-sql.md)   
 [ALTER PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/alter-partition-scheme-transact-sql.md)   
 [DROP PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/drop-partition-scheme-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Create Partitioned Tables and Indexes](../../relational-databases/partitions/create-partitioned-tables-and-indexes.md)   
 [sys.partition_schemes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-schemes-transact-sql.md)   
 [sys.data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)   
 [sys.destination_data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-destination-data-spaces-transact-sql.md)   
 [sys.partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)   
 [sys.tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)  
  
  
