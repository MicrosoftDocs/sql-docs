---
title: "ALTER DATABASE (Azure SQL Data Warehouse) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: ""
ms.prod_service: "sql-data-warehouse"
ms.reviewer: ""
ms.service: "sql-data-warehouse"
ms.component: "t-sql|statements"
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: da712a46-5f8a-4888-9d33-773e828ba845
caps.latest.revision: 20
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# ALTER DATABASE (Azure SQL Data Warehouse)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Modifies the name, maximum size, or service objective for a database.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ALTER DATABASE database_name  

  MODIFY NAME = new_database_name  
| MODIFY ( <edition_option> [, ... n] )  
  
<edition_option> ::=   
      MAXSIZE = { 
            250 | 500 | 750 | 1024 | 5120 | 10240 | 20480 
          | 30720 | 40960 | 51200 | 61440 | 71680 | 81920 
          | 92160 | 102400 | 153600 | 204800 | 245760 
      } GB  
      | SERVICE_OBJECTIVE = { 
            'DW100' | 'DW200' | 'DW300' | 'DW400' | 'DW500' 
          | 'DW600' | 'DW1000' | 'DW1200' | 'DW1500' | 'DW2000' 
          | 'DW3000' | 'DW6000' | 'DW1000c' | 'DW1500c' | 'DW2000c' 
          | 'DW2500c' | 'DW3000c' | 'DW5000c' | 'DW6000c' | 'DW7500c' 
          | 'DW10000c' | 'DW15000c' | 'DW30000c'
      }  
```  
  
## Arguments  
*database_name*  
Specifies the name of the database to be modified.  

MODIFY NAME = *new_database_name*  
Renames the database with the name specified as *new_database_name*.  
  
MAXSIZE  
The default is 10,240 GB (10 TB).  

**Applies to:** Optimized for Elasticity performance tier

The maximum allowable size for the database. The database cannot grow beyond MAXSIZE. 

**Applies to:** Optimized for Compute performance tier

The maximum allowable size for rowstore data in the database. Data stored in rowstore tables, a columnstore index's deltastore, or a nonclustered index on a clustered columnstore index cannot grow beyond MAXSIZE.  Data compressed into columnstore format does not have a size limit and is not constrained by MAXSIZE. 
  
SERVICE_OBJECTIVE  
Specifies the performance level. For more information about service objectives for [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)], see [Performance Tiers](https://azure.microsoft.com/documentation/articles/performance-tiers/).  
  
## Permissions  
Requires these permissions:  
  
-   Server-level principal login (the one created by the provisioning process), or  
  
-   Member of the `dbmanager` database role.  
  
The owner of the database cannot alter the database unless the owner is a member of the `dbmanager` role.  
  
## General Remarks  
The current database must be a different database than the one you are altering, therefore **ALTER must be run while connected to the master database**.  
  
SQL Data Warehouse is set to COMPATIBILITY_LEVEL 130 and cannot be changed. For more details, see [Improved Query Performance with Compatibility Level 130 in Azure SQL Database](https://azure.microsoft.com/documentation/articles/sql-database-compatibility-level-query-performance-130/).
  
To decrease the size of a database, use [DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md).  
  
## Limitations and Restrictions  
To run ALTER DATABASE, the database must be online and cannot be in a paused state.  
  
The ALTER DATABASE statement must run in autocommit mode, which is the default transaction management mode. This is set in the connection settings.  
  
The ALTER DATABASE statement cannot be part of a user-defined transaction.

You cannot change the database collation.  
  
## Examples  
Before you run these examples, make sure the database you are altering is not the current database. The current database must be a different database than the one you are altering, therefore **ALTER must be run while connected to the master database**.  

### A. Change the name of the database  

```  
ALTER DATABASE AdventureWorks2012  
MODIFY NAME = Northwind;  
```  
  
### B. Change max size for the database  
  
```  
ALTER DATABASE dw1 MODIFY ( MAXSIZE=10240 GB );  
```  
  
### C. Change the performance level  
  
```  
ALTER DATABASE dw1 MODIFY ( SERVICE_OBJECTIVE= 'DW1200' );  
```  
  
### D. Change the max size and the performance level  
  
```  
ALTER DATABASE dw1 MODIFY ( MAXSIZE=10240 GB, SERVICE_OBJECTIVE= 'DW1200' );  
```  
  
## See Also  
[CREATE DATABASE (Azure SQL Data Warehouse)](../../t-sql/statements/create-database-azure-sql-data-warehouse.md)
[SQL Data Warehouse list of reference topics](https://azure.microsoft.com/en-us/documentation/articles/sql-data-warehouse-overview-reference/)  
  
