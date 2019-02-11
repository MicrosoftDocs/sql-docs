---
title: "DBCC PDW_SHOWPARTITIONSTATS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
author: uc-msft
ms.author: umajay
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# DBCC PDW_SHOWPARTITIONSTATS (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

Displays the size and number of rows for each partition of a table in a [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
Show the partition stats for a table  
DBCC PDW_SHOWPARTITIONSTATS ( " [ database_name . [ schema_name ] . ] | [ schema_name.] table_name  ")  
[;]  
```  
  
## Arguments  
 [ *database_name* . [ *schema_name* ] . | *schema_name* . ] *table_name*  
 The one, two, or three-part name of the table to be displayed.  For two or three-part table names, the name must be enclosed with double quotes (""). Using quotes around a one-part table name is optional.  
  
## Permissions
Requires **VIEW SERVER STATE** permission.
  
## Result Sets  
This is the results for the DBCC PDW_SHOWPARTITIONSTATS command.
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|partition_number|int|Partition number.|  
|used_page_count|bigint|Number of pages used for the data.|  
|reserved_page_count|bigint|Number of pages allocated to the partition.|  
|row_count|bigint|Number of rows in the partition.|  
|pdw_node_id|int|Compute node for the data.|  
|distribution_id|int|Distribution id for the data.|  
  
## Examples: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
### A. DBCC PDW_SHOWPARTITIONSTATS Basic Syntax Examples  
The following examples display the space used and number of rows by partition for the FactInternetSales table in the [!INCLUDE[ssawPDW](../../includes/ssawpdw-md.md)] database.
  
```sql
DBCC PDW_SHOWPARTITIONSTATS ("ssawPDW.dbo.FactInternetSales");  
DBCC PDW_SHOWPARTITIONSTATS ("dbo.FactInternetSales");  
DBCC PDW_SHOWPARTITIONSTATS (FactInternetSales);  
```  
## See also
[DBCC PDW_SHOWEXECUTIONPLAN &#40;Transact-SQL&#41;](dbcc-pdw-showexecutionplan-transact-sql.md)  
[DBCC PDW_SHOWSPACEUSED &#40;Transact-SQL&#41;](dbcc-pdw-showspaceused-transact-sql.md)  
  
