---
title: "DBCC PDW_SHOWSPACEUSED (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 73f598cf-b02a-4dba-8d89-9fc0b55a12b8
author: uc-msft
ms.author: umajay
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# DBCC PDW_SHOWSPACEUSED (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

Displays the number of rows, disk space reserved, and disk space used for a specific table, or for all tables in a [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
-- Show the space used for all user tables and system tables in the current database  
DBCC PDW_SHOWSPACEUSED  
[;]  
  
-- Show the space used for a table  
DBCC PDW_SHOWSPACEUSED ( " [ database_name . [ schema_name ] . ] | [ schema_name .] table_name  " )  
[;]  
```  
  
## Arguments  
 [ *database_name* . [ *schema_name* ] . | *schema_name* . ] *table_name*  
 The one, two, or three-part name of the table to be displayed. For two or three-part table names, the name must be enclosed with double quotes (""). Using quotes around a one-part table name is optional. When no table name is specified, the information is displayed for the current database.  
  
## Permissions  
Requires VIEW SERVER STATE permission.
  
## Result Sets  
This is the result set for all tables.
  
|Column|Data Type|Description|  
|------------|---------------|-----------------|  
|reserved_space|bigint|Total space used for the database, in KB.|  
|data_space|bigint|Space used for data, in KB.|  
|index_space|bigint|Space used for indexes, in KB.|  
|unused_space|bigint|Space that is part of the reserved space and not used, in KB.|  
|pdw_node_id|int|Compute node that is being used for the data.|  
  
This is the result set for one table.
  
|Column|Data Type|Description|Range|  
|------------|---------------|-----------------|-----------|  
|rows|bigint|Number of rows.||  
|reserved_space|bigint|Total space reserved for the object, in KB.||  
|data_space|bigint|Space used for the data, in KB.||  
|index_space|bigint|Space used for indexes, in KB.||  
|unused_space|bigint|Space that is part of the reserved space and not used, in KB.||  
|pdw_node_id|int|Compute node that is used for reporting the space usage.||  
|distribution_id|int|Distribution that is used for reporting the space usage.|Value is -1 for replicated tables.|  
  
## Examples: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
### A. DBCC PDW_SHOWSPACEUSED Basic Syntax  
The following examples show multiple ways to display the number of rows, disk space reserved, and disk space used by the FactInternetSales table in the [!INCLUDE[ssawPDW](../../includes/ssawpdw-md.md)] database.
  
```sql
-- Uses AdventureWorks  
  
DBCC PDW_SHOWSPACEUSED ( "AdventureWorksPDW2012.dbo.FactInternetSales" );  
DBCC PDW_SHOWSPACEUSED ( "AdventureWorksPDW2012..FactInternetSales" );  
DBCC PDW_SHOWSPACEUSED ( "dbo.FactInternetSales" );  
DBCC PDW_SHOWSPACEUSED ( FactInternetSales );  
```  
  
### B. Show the disk space used by all tables in the current database  
 The following example shows the disk space reserved and used by all user tables and system tables in the [!INCLUDE[ssawPDW](../../includes/ssawpdw-md.md)] database.  
  
```sql
-- Uses AdventureWorks  
  
DBCC PDW_SHOWSPACEUSED;  
```  
 ## See also
[DBCC PDW_SHOWEXECUTIONPLAN &#40;Transact-SQL&#41;](dbcc-pdw-showexecutionplan-transact-sql.md)  
[DBCC PDW_SHOWPARTITIONSTATS &#40;Transact-SQL&#41;](dbcc-pdw-showpartitionstats-transact-sql.md)

  
