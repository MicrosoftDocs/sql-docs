---
title: DBCC PDW_SHOWSPACEUSED (Transact-SQL)
description: "DBCC PDW_SHOWSPACEUSED Displays the number of rows, disk space reserved, and disk space used for a specific table, or for all tables in an Azure Synapse Analytics or Analytics Platform System (PDW) database."
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/14/2022
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "language-reference"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest"
---

# DBCC PDW_SHOWSPACEUSED (Transact-SQL)

[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

Displays the number of rows, disk space reserved, and disk space used for a specific table, or for all tables in a [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax
  
```syntaxsql
-- Show the space used for all user tables and system tables in the current database  
DBCC PDW_SHOWSPACEUSED [ WITH IGNORE_REPLICATED_TABLE_CACHE ] 
[;]  
  
-- Show the space used for a table  
DBCC PDW_SHOWSPACEUSED ( " [ database_name . [ schema_name ] . ] | [ schema_name .] table_name  " ) [ WITH IGNORE_REPLICATED_TABLE_CACHE ] 
[;]  
```  

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments

#### `[ database_name . [ schema_name ] . | schema_name . ] table_name`  
The one, two, or three-part name of the table to be displayed. For two or three-part table names, the name must be enclosed with double quotes (""). Using quotes around a one-part table name is optional. When no table name is specified, the information is displayed for the current database.  

#### `WITH IGNORE_REPLICATED_TABLE_CACHE`
An optional parameter to view the size of the table without the replicated table cache size included.  The size of the replicated table cache is variable depending on the service level objective.  For more information, see [What is a replicated table?](/azure/sql-data-warehouse/design-guidance-for-replicated-tables#what-is-a-replicated-table)
  
## Permissions

Requires VIEW SERVER STATE permission.

## Remarks

There are also DMVs to provide more customizable queries for table size. For more information, see [Table size queries](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-overview#table-size-queries).
  
## Result Sets

The following is the result set for all tables.  Before a cache is created for a replicated Synapse table, the DBCC result reflects the total size of the underlying round robin table from each distribution.  After the cache is created, the result reflects the total size of the round robin tables and the cache.   
  
|Column|Data Type|Description|  
|------------|---------------|-----------------|  
|reserved_space|bigint|Total space used for the database, in KB.|  
|data_space|bigint|Space used for data, in KB.|  
|index_space|bigint|Space used for indexes, in KB.|  
|unused_space|bigint|Space that is part of the reserved space and not used, in KB.|  
|pdw_node_id|int|Compute node that is being used for the data.|  
  
The following is the result set for one table.
  
|Column|Data Type|Description|Range|  
|------------|---------------|-----------------|-----------|  
|rows|bigint|Number of rows.||  
|reserved_space|bigint|Total space reserved for the object, in KB.||  
|data_space|bigint|Space used for the data, in KB.||  
|index_space|bigint|Space used for indexes, in KB.||  
|unused_space|bigint|Space that is part of the reserved space and not used, in KB.||  
|pdw_node_id|int|Compute node that is used for reporting the space usage.||  
|distribution_id|int|Distribution that is used for reporting the space usage.|For Parallel Data Warehouse, its value is -1 for replicated tables.|  
  
## Examples: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  

### A. DBCC PDW_SHOWSPACEUSED Basic Syntax  

The following examples using the `AdventureWorks` sample database show multiple ways to display the number of rows, disk space reserved, and disk space used by the `FactInternetSales` table in the [!INCLUDE[ssawPDW](../../includes/ssawpdw-md.md)] database.
  
```sql
-- Uses AdventureWorks  
  
DBCC PDW_SHOWSPACEUSED ( "AdventureWorksPDW2012.dbo.FactInternetSales" );  
DBCC PDW_SHOWSPACEUSED ( "AdventureWorksPDW2012..FactInternetSales" );  
DBCC PDW_SHOWSPACEUSED ( "dbo.FactInternetSales" );  
DBCC PDW_SHOWSPACEUSED ( FactInternetSales );  
```  
  
### B. Show the disk space used by all tables in the current database  

 The following example using the `AdventureWorks` sample database shows the disk space reserved and used by all user tables and system tables in the [!INCLUDE[ssawPDW](../../includes/ssawpdw-md.md)] database.  
  
```sql
-- Uses AdventureWorks  
  
DBCC PDW_SHOWSPACEUSED;  
```  

## Next steps

- [DBCC PDW_SHOWEXECUTIONPLAN &#40;Transact-SQL&#41;](dbcc-pdw-showexecutionplan-transact-sql.md)  
- [DBCC PDW_SHOWPARTITIONSTATS &#40;Transact-SQL&#41;](dbcc-pdw-showpartitionstats-transact-sql.md)
- [Table size queries](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-overview#table-size-queries).
