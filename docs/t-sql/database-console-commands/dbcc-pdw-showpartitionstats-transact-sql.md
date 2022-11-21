---
title: DBCC PDW_SHOWPARTITIONSTATS (Transact-SQL)
description: "DBCC PDW_SHOWPARTITIONSTATS displays the size and number of rows for each partition of a table in Azure Synapse Analytics or the Analytics Platform System (PDW)."
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

# DBCC PDW_SHOWPARTITIONSTATS (Transact-SQL)

[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

Displays the size and number of rows for each partition of a table in a [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] database.
  
![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
--Show the partition stats for a table  
DBCC PDW_SHOWPARTITIONSTATS ( " [ database_name . [ schema_name ] . ] | [ schema_name.] table_name  ")  
[;]  
```  

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments  

 `[ database_name . [ schema_name ] . | schema_name . ] table_name`  
 The one, two, or three-part name of the table to be displayed.  For two or three-part table names, the name must be enclosed with double quotes (""). Using quotes around a one-part table name is optional.  
  
## Permissions
Requires **VIEW SERVER STATE** permission.
  
## Result Sets  
This set is the results for the DBCC PDW_SHOWPARTITIONSTATS command.
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|partition_number|int|Partition number.|  
|used_page_count|bigint|Number of pages used for the data.|  
|reserved_page_count|bigint|Number of pages reserved for the partition.|  
|row_count|bigint|Number of rows in the partition.|  
|pdw_node_id|int|Compute node for the data.|  
|distribution_id|int|Distribution identifier for the data.|  
  
## Examples: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]
  
### A. DBCC PDW_SHOWPARTITIONSTATS Basic Syntax Examples  
The following examples display the space used and number of rows by partition for the `FactInternetSales` table in the [!INCLUDE[ssawPDW](../../includes/ssawpdw-md.md)] database.
  
```sql
DBCC PDW_SHOWPARTITIONSTATS ("ssawPDW.dbo.FactInternetSales");  
DBCC PDW_SHOWPARTITIONSTATS ("dbo.FactInternetSales");  
DBCC PDW_SHOWPARTITIONSTATS (FactInternetSales);  
```  

## See also

- [DBCC PDW_SHOWEXECUTIONPLAN &#40;Transact-SQL&#41;](dbcc-pdw-showexecutionplan-transact-sql.md)  
- [DBCC PDW_SHOWSPACEUSED &#40;Transact-SQL&#41;](dbcc-pdw-showspaceused-transact-sql.md)  
- [Table size queries](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-overview#table-size-queries)
