---
title: "CREATE DATABASE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7820fefd-896b-423f-bef4-3eb89acfef29
caps.latest.revision: 66
author: BarbKess
---
# CREATE DATABASE (SQL Server PDW)
Creates a new database on a SQL Server PDW appliance. Use this statement to create all files associated with an appliance database and to set maximum size and auto-growth options for the database tables and transaction log.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
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
The name of the new database. For more information on permitted database names, see [Object Naming Rules &#40;SQL Server PDW&#41;](../sqlpdw/object-naming-rules-sql-server-pdw.md). You cannot name a database any of these [Reserved Database Names &#40;SQL Server PDW&#41;](../sqlpdw/reserved-database-names-sql-server-pdw.md).  
  
AUTOGROW = ON | **OFF**  
Specifies whether the *replicated_size*, *distributed_size*, and *log_size* parameters for this database will automatically grow as needed beyond their specified sizes. Default value is **OFF**.  
  
If AUTOGROW is ON, *replicated_size*, *distributed_size*, and *log_size* will grow as required (not in blocks of the initial specified size) with each data insert, update, or other action that requires more storage than has already been allocated.  
  
If AUTOGROW is OFF, the sizes will not grow automatically. SQL Server PDW will return an error when attempting an action that requires *replicated_size*, *distributed_size*, or *log_size* to grow beyond their specified value.  
  
AUTOGROW is either ON for all sizes or OFF for all sizes. For example, it is not possible to set AUTOGROW ON for *log_size*, but not set it for *replicated_size*.  
  
*replicated_size* [ GB ]  
A positive number. Sets the size (in integer or decimal gigabytes) for the total space allocated to replicated tables and corresponding data *on each Compute node*. For minimum and maximum *replicated_size* requirements, see [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md).  
  
If AUTOGROW is ON, replicated tables will be permitted to grow beyond this limit.  
  
If AUTOGROW is OFF, an error will be returned if a user attempts to create a new replicated table, insert data into an existing replicated table, or update an existing replicated table in a manner that would increase the size beyond *replicated_size*.  
  
*distributed_size* [ GB ]  
A positive number. The size, in integer or decimal gigabytes, for the total space allocated to distributed tables (and corresponding data) *across the appliance*. For minimum and maximum *distributed_size* requirements, see [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md).  
  
If AUTOGROW is ON, distributed tables will be permitted to grow beyond this limit.  
  
If AUTOGROW is OFF, an error will be returned if a user attempts to create a new distributed table, insert data into an existing distributed table, or update an existing distributed table in a manner that would increase the size beyond *distributed_size*.  
  
*log_size* [ GB ]  
A positive number. The size (in integer or decimal gigabytes) for the transaction log *across the appliance*.  
  
For minimum and maximum *log_size* requirements, see [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md).  
  
If AUTOGROW is ON, the log file is permitted to grow beyond this limit. Use the [DBCC SHRINKLOG &#40;SQL Server PDW&#41;](../sqlpdw/dbcc-shrinklog-sql-server-pdw.md) statement to reduce the size of the log files to their original size.  
  
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
Databases are created with database compatibility level 120, which is the compatibility level for SQL Server 2014. This ensures the database will be able to use all of the SQL Server 2014 functionality that PDW uses.  
  
## Limitations and Restrictions  
The CREATE DATABASE statement is not allowed in an explicit transaction. For more information, see [Transactions &#40;SQL Server PDW&#41;](../sqlpdw/transactions-sql-server-pdw.md).  
  
For information on minimum and maximum constraints on databases, see [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md).  
  
At the time a database is created, there must be enough available free space *on each Compute node* to allocate the combined total of the following sizes:  
  
-   SQL Server database with tables the size of *replicated_table_size*.  
  
-   SQL Server database with tables the size of (*distributed_table_size* / number of Compute nodes ).  
  
-   SQL Server logs the size of (*log_size* / number of Compute nodes.  
  
## Locking  
Takes a shared lock on the DATABASE object.  
  
## Metadata  
After this operation succeeds, an entry for this database will appear in the [sys.databases &#40;SQL Server PDW&#41;](../sqlpdw/sys-databases-sql-server-pdw.md) and [sys.objects &#40;SQL Server PDW&#41;](../sqlpdw/sys-objects-sql-server-pdw.md)metadata views.  
  
## Examples  
  
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
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[ALTER DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/alter-database-sql-server-pdw.md)  
[DROP DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/drop-database-sql-server-pdw.md)  
  
