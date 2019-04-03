---
title: "Tables | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "tables [SQL Server]"
  - "table components [SQL Server]"
ms.assetid: 82d7819c-b801-4309-a849-baa63083e83f
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Tables
[!INCLUDE[tsql-appliesto-ss2016-all-md](../../includes/tsql-appliesto-ss2016-all-md.md)]

  Tables are database objects that contain all the data in a database. In tables, data is logically organized in a row-and-column format similar to a spreadsheet. Each row represents a unique record, and each column represents a field in the record. For example, a table that contains employee data for a company might contain a row for each employee and columns representing employee information such as employee number, name, address, job title, and home telephone number.  
  
-   The number of tables in a database is limited only by the number of objects allowed in a database (2,147,483,647). A standard user-defined table can have up to 1,024 columns. The number of rows in the table is limited only by the storage capacity of the server.  
  
-   You can assign properties to the table and to each column in the table to control the data that is allowed and other properties. For example, you can create constraints on a column to disallow null values or provide a default value if a value is not specified, or you can assign a key constraint on the table that enforces uniqueness or defines a relationship between tables.  
  
-   The data in the table can be compressed either by row or by page. Data compression can allow more rows to be stored on a page. For more information, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  
  
## Types of Tables  
 Besides the standard role of basic user-defined tables, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the following types of tables that serve special purposes in a database.  
  
### Partitioned Tables

Partitioned tables are tables whose data is horizontally divided into units which may be spread across more than one filegroup in a database. Partitioning makes large tables or indexes more manageable by letting you access or manage subsets of data quickly and efficiently, while maintaining the integrity of the overall collection. By default, [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] supports up to 15,000 partitions. For more information, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).  
  
### Temporary Tables
  
Temporary tables are stored in **tempdb**. There are two types of temporary tables: local and global. They differ from each other in their names, their visibility, and their availability. Local temporary tables have a single number sign (#) as the first character of their names; they are visible only to the current connection for the user, and they are deleted when the user disconnects from the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Global temporary tables have two number signs (##) as the first characters of their names; they are visible to any user after they are created, and they are deleted when all users referencing the table disconnect from the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  


#### <a name="ctp23"></a> Reduced recompilations for workloads using temporary tables across multiple scopes

[!INCLUDE[ss2019](../../includes/sssqlv15-md.md) reduces recompilations for workloads using temporary tables across multiple scopes. Prior to this feature, when referencing a temporary table with a data manipulation language (DML) statement (`SELECT`, `INSERT`, `UPDATE`, `DELETE`), if the temporary table was created by an outer scope batch, this would result in a recompile of the DML statement each time it is executed. With this improvement, SQL Server performs additional lightweight checks to avoid unnecessary recompilations:

- Check if the outer-scope module used for creating the temporary table at compile time is the same one used for consecutive executions. 
- Keep track of any data definition language (DDL) changes made at initial compilation and  compare them with DDL operations for consecutive executions. 

The end result is a reduction in extraneous recompilations and CPU-overhead.  
System Tables  

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stores the data that defines the configuration of the server and all its tables in a special set of tables known as system tables. Users cannot directly query or update the system tables. The information in the system tables is made available through the system views. For more information, see [System Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/35a6161d-7f43-4e00-bcd3-3091f2015e90).  
  
### Wide Tables

Wide tables use [sparse columns](../../relational-databases/tables/use-sparse-columns.md) to increase the total of columns that a table can have to 30,000. Sparse columns are ordinary columns that have an optimized storage for null values. Sparse columns reduce the space requirements for null values at the cost of more overhead to retrieve nonnull values. A wide table has defined a [column set](../../relational-databases/tables/use-column-sets.md), which is an untyped XML representation that combines all the sparse columns of a table into a structured output. The number of indexes and statistics is also increased to 1,000 and 30,000, respectively. The maximum size of a wide table row is 8,019 bytes. Therefore, most of the data in any particular row should be NULL. The maximum number of nonsparse columns plus computed columns in a wide table remains 1,024.  
  
Wide tables have the following performance implications.  
  
-   Wide tables can increase the cost to maintain indexes on the table. We recommend that the number of indexes on a wide table be limited to the indexes that are required by the business logic. As the number of indexes increases, so does the DML compile-time and memory requirement. Nonclustered indexes should be filtered indexes that are applied to data subsets. For more information, see [Create Filtered Indexes](../../relational-databases/indexes/create-filtered-indexes.md).  
  
-   Applications can dynamically add and remove columns from wide tables. When columns are added or removed, compiled query plans are also invalidated. We recommend that you design an application to match the projected workload so that schema changes are minimized.  
  
-   When data is added and removed from a wide table, performance can be affected. Applications must be designed for the projected workload so that changes to the table data is minimized.  
  
-   Limit the execution of DML statements on a wide table that update multiple rows of a clustering key. These statements can require significant memory resources to compile and execute.  
  
-   Switch partition operations on wide tables can be slow and might require large amounts of memory to process. The performance and memory requirements are proportional to the total number of columns in both the source and target partitions.  
  
-   Update cursors that update specific columns in a wide table should list the columns explicitly in the FOR UPDATE clause. This will help optimize performance when you use cursors.  
  
## Common Table Tasks  
 The following table provides links to common tasks associated with creating or modifying a table.  
  
|Table Tasks|Topic|  
|-----------------|-----------|  
|Describes how to create a table.|[Create Tables &#40;Database Engine&#41;](../../relational-databases/tables/create-tables-database-engine.md)|  
|Describes how to delete a table.|[Delete Tables &#40;Database Engine&#41;](../../relational-databases/tables/delete-tables-database-engine.md)|  
|Describes how to create a new table that contains some or all of the columns in an existing table.|[Duplicate Tables](../../relational-databases/tables/duplicate-tables.md)|  
|Describes how to rename a table.|[Rename Tables &#40;Database Engine&#41;](../../relational-databases/tables/rename-tables-database-engine.md)|  
|Describes how to view the properties of the table.|[View the Table Definition](../../relational-databases/tables/view-the-table-definition.md)|  
|Describes how to determine whether other objects such as a view or stored procedure depend on a table.|[View the Dependencies of a Table](../../relational-databases/tables/view-the-dependencies-of-a-table.md)|  
  
 The following table provides links to common tasks associated with creating or modifying columns in a table.  
  
|Column Tasks|Topic|  
|------------------|-----------|  
|Describes how to add columns to an existing table.|[Add Columns to a Table &#40;Database Engine&#41;](../../relational-databases/tables/add-columns-to-a-table-database-engine.md)|  
|Describes how to delete columns from a table.|[Delete Columns from a Table](../../relational-databases/tables/delete-columns-from-a-table.md)|  
|Describes how to change the name of a column.|[Rename Columns &#40;Database Engine&#41;](../../relational-databases/tables/rename-columns-database-engine.md)|  
|Describes how to copy columns from one table to another, copying either just the column definition, or the definition and data.|[Copy Columns from One Table to Another &#40;Database Engine&#41;](../../relational-databases/tables/copy-columns-from-one-table-to-another-database-engine.md)|  
|Describes how to modify a column definition, by changing the data type or other property.|[Modify Columns &#40;Database Engine&#41;](../../relational-databases/tables/modify-columns-database-engine.md)|  
|Describes how to change the order in which the columns appear.|[Change Column Order in a Table](../../relational-databases/tables/change-column-order-in-a-table.md)|  
|Describes how to create a computed column in a table.|[Specify Computed Columns in a Table](../../relational-databases/tables/specify-computed-columns-in-a-table.md)|  
|Describes how to specify a default value for a column. This value is used if another value is not supplied.|[Specify Default Values for Columns](../../relational-databases/tables/specify-default-values-for-columns.md)|  
  
## See Also  
 [Primary and Foreign Key Constraints](../../relational-databases/tables/primary-and-foreign-key-constraints.md)   
 [Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md)  
  
  
