---
title: "DBCC PDW_SHOWSPACEUSED (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ff2c6124-aad1-4d64-8223-eeb25d88b2be
caps.latest.revision: 23
author: BarbKess
---
# DBCC PDW_SHOWSPACEUSED (SQL Server PDW)
Displays the number of rows, disk space reserved, and disk space used for a specific table, or for all tables in a SQL Server PDW database.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
Show the space used for all user tables   
and system tables in the current database  
DBCC PDW_SHOWSPACEUSED  
[;]  
  
Show the space used for a table  
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
|----------|-------------|---------------|  
|reserved_space|bigint|Total space used for the database, in KB.|  
|data_space|bigint|Space used for data, in KB.|  
|index_space|bigint|Space used for indexes, in KB.|  
|unused_space|bigint|Space that is part of the reserved space and not used, in KB.|  
|pdw_node_id|int|Compute node that is being used for the data.|  
  
This is the result set for one table.  
  
|Column|Data Type|Description|Range|  
|----------|-------------|---------------|---------|  
|rows|bigint|Number of rows.||  
|reserved_space|bigint|Total space reserved for the object, in KB.||  
|data_space|bigint|Space used for the data, in KB.||  
|index_space|bigint|Space used for indexes, in KB.||  
|unused_space|bigint|Space that is part of the reserved space and not used, in KB.||  
|pdw_node_id|int|Compute node that is used for reporting the space usage.||  
|distribution_id|int|Distribution that is used for reporting the space usage.|Value is -1 for replicated tables.|  
  
## Examples  
  
### A. DBCC PDW_SHOWSPACEUSED Basic Syntax Examples  
The following examples show multiple ways to display the number of rows, disk space reserved, and disk space used by the FactInternetSales table in the **AdventureWorksPDW2012** database.  
  
```  
USE AdventureWorksPDW2012;  
DBCC PDW_SHOWSPACEUSED ( "AdventureWorksPDW2012.dbo.FactInternetSales" );  
DBCC PDW_SHOWSPACEUSED ( "AdventureWorksPDW2012..FactInternetSales" );  
DBCC PDW_SHOWSPACEUSED ( "dbo.FactInternetSales" );  
DBCC PDW_SHOWSPACEUSED ( FactInternetSales );  
```  
  
### B. Show the disk space used by all tables in the current database  
The following example shows the disk space reserved and used by all user tables and system tables in the **AdventureWorksPDW2012** database.  
  
```  
USE AdventureWorksPDW2012;  
DBCC PDW_SHOWSPACEUSED;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
