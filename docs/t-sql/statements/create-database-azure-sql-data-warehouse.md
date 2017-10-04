---
title: "CREATE DATABASE (Azure SQL Data Warehouse) | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "03/14/2017"
ms.prod: 
ms.reviewer: ""
ms.service: "sql-warehouse"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 42819b93-b757-4b2c-8179-d4be3c512c19
caps.latest.revision: 20
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# CREATE DATABASE (Azure SQL Data Warehouse)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx_md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Creates a new database.  
  
## Syntax  
  
```  
-- Syntax for Azure SQL Data Warehouse  
  
CREATE DATABASE database_name [ COLLATE collation_name ]  
(  
    [ MAXSIZE = { 250 | 500 | 750 | 1024 | 5120 | 10240 | 20480 | 30720 | 40960 | 51200 | 61440 | 71680 | 81920 | 92160 | 102400 | 153600 | 204800 | 245760 } GB ,]  
    EDITION = 'datawarehouse',  
    SERVICE_OBJECTIVE = { 'DW100' | 'DW200' | 'DW300' | 'DW400' | 'DW500' | 'DW600' | 'DW1000' | 'DW1200' | 'DW1500' | 'DW2000' | 'DW3000' | 'DW6000' }  
)  
[;]  
```  
  
## Arguments  
*database_name*  
The name of the new database. This name must be unique on the SQL server, which can host both [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] databases and [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] databases, and comply with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rules for identifiers. For more information, see [Identifiers](http://go.microsoft.com/fwlink/p/?LinkId=180386).  
  
*collation_name*  
Specifies the default collation for the database. Collation name can be either a Windows collation name or a SQL collation name. If not specified, the database is assigned the default collation, which is SQL_Latin1_General_CP1_CI_AS.  
  
For more information about the Windows and SQL collation names, see [COLLATE (Transact-SQL)](http://msdn.microsoft.com/library/ms184391.aspx).  
  
*EDITION*  
Specifies the service tier of the database. For [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] use 'datawarehouse' .  
  
*MAXSIZE*  
The maximum size the database may grow to. Setting this value prevents the growth of the database size beyond the size set. The default *MAXSIZE* when not specified is 10240 GB (10 TB).  Other possible values range from 250 GB up to 240 TB.  
  
SERVICE_OBJECTIVE  
Specifies the performance level. For more information about service objectives for [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], see [Scale Performance on SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-performance-scale/).  
  
## General Remarks  
Use [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/databasepropertyex-transact-sql.md) to see the database properties.  
  
Use [ALTER DATABASE &#40;Azure SQL Data Warehouse&#41;](../../t-sql/statements/alter-database-azure-sql-data-warehouse.md) to change the max size, or service objective values later.   

SQL Data Warehouse is set to COMPATIBILITY_LEVEL 130 and cannot be changed. For more details, see [Improved Query Performance with Compatibility Level 130 in Azure SQL Database](https://azure.microsoft.com/documentation/articles/sql-database-compatibility-level-query-performance-130/).
  
## Permissions  
Required permissions:  
  
-   Server level principal login, created by the provisioning process, or  
  
-   Member of the `dbmanager` database role.  
  
## Error Handling  
If the size of the database reaches MAXSIZE you will receive error code 40544. When this occurs, you cannot insert and update data, or create new objects (such as tables, stored procedures, views, and functions). You can still read and delete data, truncate tables, drop tables and indexes, and rebuild indexes. You can then update MAXSIZE to a value larger than your current database size or delete some data to free storage space. There may be as much as a fifteen-minute delay before you can insert new data.  
  
## Limitations and Restrictions  
You must be connected to the master database to create a new database.  
  
The `CREATE DATABASE` statement must be the only statement in a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch.

You cannot change the database collation after the database is created.   
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)]  
  
### A. Simple example  
A simple example for creating a data warehouse database. This creates the database with the smallest max size which is 10240 GB, the default collation which is SQL_Latin1_General_CP1_CI_AS, and the smallest compute power which is DW100.  
  
```  
CREATE DATABASE TestDW  
(EDITION = 'datawarehouse', SERVICE_OBJECTIVE='DW100');  
```  
  
### B. Create a data warehouse database with all the options  
An example of creating a a 10 terabyte data warehouse using all the options.  
  
```  
CREATE DATABASE TestDW COLLATE Latin1_General_100_CI_AS_KS_WS  
(MAXSIZE = 10240 GB, EDITION = 'datawarehouse', SERVICE_OBJECTIVE = 'DW1000');  
```  
  
## See Also  
[ALTER DATABASE &#40;Azure SQL Data Warehouse&#40;](../../t-sql/statements/alter-database-azure-sql-data-warehouse.md)
[CREATE TABLE &#40;Azure SQL Data Warehouse&#41;](../../t-sql/statements/create-table-azure-sql-data-warehouse.md) 
[DROP DATABASE &#40;Transact-SQL&#40;](../../t-sql/statements/drop-database-transact-sql.md) 
  

