---
title: "Data Definition Statements (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 0506298a-8580-4323-b25c-d120093bfc4a
caps.latest.revision: 16
author: BarbKess
---
# Data Definition Statements (SQL Server PDW)
SQL Server PDW includes data definition statements for manipulation of tables, indexes, and other database object  
  
This topic is organized by object type, as shown:  
  
[Database Management](#Database)  
  
[Table, Index, and Statistics Management](#TableIndexStatistics)  
  
[View Management](#View)  
  
## <a name="Database"></a>Database Management  
  
|Command|Description|  
|-----------|---------------|  
|[ALTER AUTHORIZATION](../sqlpdw/alter-authorization-sql-server-pdw.md)|Changes ownership of the database.|  
|[ALTER DATABASE](../sqlpdw/alter-database-sql-server-pdw.md)|Modifies the maximum size options for replicated tables, distributed tables, and the transaction log in the database.|  
|[BACKUP DATABASE](../sqlpdw/backup-database-sql-server-pdw.md)|Backs up a database to the Backup server.|  
|[CREATE DATABASE](../sqlpdw/create-database-sql-server-pdw.md)|Creates a new database.|  
|[DROP DATABASE](../sqlpdw/drop-database-sql-server-pdw.md)|Removes a database.|  
|[RENAME](../sqlpdw/rename-sql-server-pdw.md)|Renames a database.|  
|[RESTORE DATABASE](../sqlpdw/restore-database-sql-server-pdw.md)|Restores a database created with BACKUP DATABASE.|  
|[USE](../sqlpdw/use-sql-server-pdw.md)|Specifies the database context.|  
  
## <a name="TableIndexStatistics"></a>Table, Index, and Statistics Management  
  
|Statement|Description|  
|-------------|---------------|  
|[ALTER AUTHORIZATION](../sqlpdw/alter-authorization-sql-server-pdw.md)|Changes the ownership of a table.|  
|[ALTER INDEX](../sqlpdw/alter-index-sql-server-pdw.md)|Rebuilds indexes on a table.|  
|[ALTER TABLE](../sqlpdw/alter-table-sql-server-pdw.md)|Splits, merges, or switches table partitions.|  
|[CREATEXTERNAL DATA SOURCE](../sqlpdw/create-external-data-source-sql-server-pdw.md)|Creates a data source for an external Hadoop table.|  
|[CREATEXTERNAL FILE FORMAT](../sqlpdw/create-external-file-format-sql-server-pdw.md)|Creates a defined file format for an external Hadoop table.|  
|[CREATE INDEX](../sqlpdw/create-index-sql-server-pdw.md)|Creates a non-clustered index on a table.|  
|[CREATE STATISTICS](../sqlpdw/create-statistics-sql-server-pdw.md)|Creates query optimization statistics on a table.|  
|[CREATE EXTERNAL TABLE](../sqlpdw/create-external-table-sql-server-pdw.md)|Creates an external table that points to a Hadoop cluster for the table data.|  
|[CREATE EXTERNAL TABLE AS SELECT](../sqlpdw/create-external-table-as-select-sql-server-pdw.md)|Creates an external table on a Hadoop cluster and populates it with the results of a select statement.|  
|[CREATE REMOTE TABLE](../sqlpdw/create-remote-table-as-select-sql-server-pdw.md)|Copies data to a new table in a non-appliance SQL Server database.|  
|||  
|[CREATE TABLE](../sqlpdw/create-table-sql-server-pdw.md)|Creates a new table.|  
|[CREATE TABLE AS SELECT](../sqlpdw/create-table-as-select-sql-server-pdw.md)|Creates a table that contains the results of a select statement.|  
|[DROP INDEX](../sqlpdw/drop-index-sql-server-pdw.md)|Removes and deletes a non-clustered index.|  
|[DROP STATISTICS](../sqlpdw/drop-statistics-sql-server-pdw.md)|Removes and deletes statistics from a table.|  
|[DROP TABLE](../sqlpdw/drop-table-sql-server-pdw.md)|Removes and deletes a table.|  
|[RENAME](../sqlpdw/rename-sql-server-pdw.md)|Renames a table.|  
|[UPDATE STATISTICS](../sqlpdw/update-statistics-sql-server-pdw.md)|Updates statistics.|  
  
## <a name="View"></a>View Management  
  
|Statement|Description|  
|-------------|---------------|  
|[CREATE VIEW](../sqlpdw/create-view-sql-server-pdw.md)|Creates a view.|  
|[DROP VIEW](../sqlpdw/drop-view-sql-server-pdw.md)|Removes and deletes a view.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Security Statements &#40;SQL Server PDW&#41;](../sqlpdw/security-statements-sql-server-pdw.md)  
[Data Manipulation Statements &#40;SQL Server PDW&#41;](../sqlpdw/data-manipulation-statements-sql-server-pdw.md)  
[Queries &#40;SQL Server PDW&#41;](../sqlpdw/queries-sql-server-pdw.md)  
  
