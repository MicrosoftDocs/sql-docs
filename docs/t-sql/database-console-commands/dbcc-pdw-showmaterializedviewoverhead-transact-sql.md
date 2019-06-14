---
title: "DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD  (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/16/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
author: XiaoyuL-Preview 
ms.author: xiaoyul
manager: craigg
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
---
# DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD   (Transact-SQL) (preview)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Displays the number of delta rows held for materialized views in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].  The overhead ratio is calculated as TOTAL_ROWS / MAX (1, BASE_VIEW_ROWS) 

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax

```
DBCC PDW_SHOWMATERIZLIEDVIEWOVERHEAD ( " [ schema_name .] table_name  " )
[;]
```
  
## Arguments

 *schema_name*  
 Is the name of the schema to which the view belongs.

*table_name*

## Remarks

As the underlying tables in the definition of a materialized view are modified, a materialized view delta store is maintained.  Selecting from a materialized view includes scanning the clustered columnstore structure for the materialized view and applying the delta changes from the materialized view delta store.   If the number of materialized view delta store records is high, `select` performance will degrade.  Users can rebuild the materialized view to recreate the clustered columnstore structure and eliminate the rows in the materialized view delta store.
  
## Permissions  
  
Requires VIEW-SERVER-STATE permission on the Appliance.
  
## Example  

This example returns the delta space used for a materialized view.

```sql
DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD ( "dbo.MyIndexedView" )
```

Output:

|OBJECT_ID|BASE_VIEW_ROWS|TOTAL_ROWS|OVERHEAD_RATIO|
|--------|--------|--------|--------|  
|1234|1|3 |3.0 |

</br>

|OBJECT_ID |BASE_VIEW_ROWS|TOTAL_ROWS|OVERHEAD_RATIO|
|--------|--------|--------|--------|
|4567|0|0|0.0|
|OBJECT_ID|BASE_VIEW_ROWS|TOTAL_ROWS|OVERHEAD_RATIO|
|789|0|2|2.0|
| | | | |

## See also