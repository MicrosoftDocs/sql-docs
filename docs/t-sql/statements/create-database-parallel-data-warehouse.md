---
title: "CREATE DATABASE (Parallel Data Warehouse) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 40cacde4-ac72-45f7-9564-d76e2b4a741a
caps.latest.revision: 13
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# CREATE DATABASE (Parallel Data Warehouse)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw_md](../../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]

  Creates a new database on a [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] appliance. Use this statement to create all files associated with an appliance database and to set maximum size and auto-growth options for the database tables and transaction log.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for Parallel Data Warehouse  
  
CREATE DATABASE database_name   
WITH (   
    [ AUTOGROW = ON | OFF , ]   
    REPLICATED_SIZE = replicated_size [ GB ] ,  
    DISTRIBUTED_SIZE = distributed_size [ GB ] ,  
    LOG_SIZE = log_size [ GB ] )  
[;]  
```  
  
## Arguments  
 *database_name*  
 The name of the new database. For more information on permitted database names, see "Object Naming Rules" and "Reserved Database Names" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].  
  
 AUTOGROW = ON | **OFF**  
 Specifies whether the *replicated_size*, *distributed_size*, and *log_size* parameters for this database will automatically grow as needed beyond their specified sizes. Default value is **OFF**.  
  
 If AUTOGROW is ON, *replicated_size*, *distributed_size*, and *log_size* will grow as required (not in blocks of the initial specified size) with each data insert, update, or other action that requires more storage than has already been allocated.  
  
 If AUTOGROW is OFF, the sizes will not grow automatically. [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will return an error when attempting an action that requires *replicated_size*, *distributed_size*, or *log_size* to grow beyond their specified value.  
  
 AUTOGROW is either ON for all sizes or OFF for all sizes. For example, it is not possible to set AUTOGROW ON for *log_size*, but not set it for *replicated_size*.  
  
 *replicated_size* [ GB ]  
 A positive number. Sets the size (in integer or decimal gigabytes) for the total space allocated to replicated tables and corresponding data *on each Compute node*. For minimum and maximum *replicated_size* requirements, see "Minimum and Maximum Values" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].  
  
 If AUTOGROW is ON, replicated tables will be permitted to grow beyond this limit.  
  
 If AUTOGROW is OFF, an error will be returned if a user attempts to create a new replicated table, insert data into an existing replicated table, or update an existing replicated table in a manner that would increase the size beyond *replicated_size*.  
  
 *distributed_size* [ GB ]  
 A positive number. The size, in integer or decimal gigabytes, for the total space allocated to distributed tables (and corresponding data) *across the appliance*. For minimum and maximum *distributed_size* requirements, see "Minimum and Maximum Values" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].  
  
 If AUTOGROW is ON, distributed tables will be permitted to grow beyond this limit.  
  
 If AUTOGROW is OFF, an error will be returned if a user attempts to create a new distributed table, insert data into an existing distributed table, or update an existing distributed table in a manner that would increase the size beyond *distributed_size*.  
  
 *log_size* [ GB ]  
 A positive number. The size (in integer or decimal gigabytes) for the transaction log *across the appliance*.  
  
 For minimum and maximum *log_size* requirements, see "Minimum and Maximum Values" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].  
  
 If AUTOGROW is ON, the log file is permitted to grow beyond this limit. Use the [DBCC SHRINKLOG (Azure SQL Data Warehouse)](../../t-sql/database-console-commands/dbcc-shrinklog-azure-sql-data-warehouse.md) statement to reduce the size of the log files to their original size.  
  
 If AUTOGROW is OFF, an error will be returned to the user for any action that would increase the log size on an individual Compute node beyond *log_size*.  
  
## Permissions  
 Requires the **CREATE ANY DATABASE** permission in the master database, or membership in the **sysadmin** fixed server role.  
  
 The following example provides the permission to create a database to the database user Fay.  
  
```  
USE master;  
GO  
GRANT CREATE ANY DATABASE TO [Fay];  
GO  
```  
  
## General Remarks  
 Databases are created with database compatibility level 120, which is the compatibility level for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. This ensures the database will be able to use all of the [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] functionality that PDW uses.  
  
## Limitations and Restrictions  
 The CREATE DATABASE statement is not allowed in an explicit transaction. For more information, see [Statements](../../t-sql/statements/statements.md).  
  
 For information on minimum and maximum constraints on databases, see "Minimum and Maximum Values" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].  
  
 At the time a database is created, there must be enough available free space *on each Compute node* to allocate the combined total of the following sizes:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database with tables the size of *replicated_table_size*.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database with tables the size of (*distributed_table_size* / number of Compute nodes ).  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logs the size of (*log_size* / number of Compute nodes).  
  
## Locking  
 Takes a shared lock on the DATABASE object.  
  
## Metadata  
 After this operation succeeds, an entry for this database will appear in the [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) and [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)metadata views.  
  
## Examples: [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### A. Basic database creation examples  
 The following example creates the database `mytest` with a storage allocation of 100 GB per Compute node for replicated tables, 500 GB per appliance for distributed tables, and 100 GB per appliance for the transaction log. In this example, AUTOGROW is off by default.  
  
```  
CREATE DATABASE mytest  
   WITH   
   (REPLICATED_SIZE = 100 GB,  
   DISTRIBUTED_SIZE = 500 GB,  
   LOG_SIZE = 100 GB );  
```  
  
 The following example creates the database `mytest` with the same parameters as above, except that AUTOGROW is turned on. This allows the database to grow outside the specified size parameters.  
  
```  
CREATE DATABASE mytest  
   WITH   
   (AUTOGROW = ON,  
   REPLICATED_SIZE = 100 GB,  
   DISTRIBUTED_SIZE = 500 GB,  
   LOG_SIZE = 100 GB);  
```  
  
### B. Creating a database with partial gigabyte sizes  
 The following example creates the database `mytest`, with AUTOGROW off, a storage allocation of 1.5 GB per Compute node for replicated tables, 5.25 GB per appliance for distributed tables, and 10 GB per appliance for the transaction log.  
  
```  
CREATE DATABASE mytest  
   WITH   
   (REPLICATED_SIZE = 1.5 GB,  
   DISTRIBUTED_SIZE = 5.25 GB,  
   LOG_SIZE = 10 GB);  
```  
  
## See Also  
 [ALTER DATABASE &#40;Parallel Data Warehouse&#41;](../../t-sql/statements/alter-database-parallel-data-warehouse.md)   
 [DROP DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-transact-sql.md)  
  
  