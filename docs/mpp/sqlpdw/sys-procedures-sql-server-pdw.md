---
title: "sys.procedures (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2746713e-956b-4d2e-a5af-261ddbfc63ef
caps.latest.revision: 7
author: BarbKess
---
# sys.procedures (SQL Server PDW)
Contains a row for every stored procedure in SQL Server PDW.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**<Columns inherited from sys.objects>**||For a list of columns that this view inherits, see [sys.objects &#40;SQL Server PDW&#41;](../sqlpdw/sys-objects-sql-server-pdw.md)|  
|**is_auto_executed**|**bit**|Always 0.|  
|**is_execution_replicated**|**bit**|Always 0.|  
|**is_repl_serializable_only**|**bit**|Always 0.|  
|**skips_repl_constraints**|**bit**|Always 0.|  
  
## Permissions  
Users can see information about objects for which they have some type of permission.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SQL Server Catalog Views &#40;SQL Server PDW&#41;](../sqlpdw/sql-server-catalog-views-sql-server-pdw.md)  
[CREATE PROCEDURE &#40;SQL Server PDW&#41;](../sqlpdw/create-procedure-sql-server-pdw.md)  
  
