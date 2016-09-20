---
title: "sys.default_constraints (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c251c870-369c-471a-a5c7-b1e7821c94d5
caps.latest.revision: 14
author: BarbKess
---
# sys.default_constraints (SQL Server PDW)
Contains a row for each column that has a constraint (default value) created as part of a CREATE TABLE or ALTER TABLE statement in SQL Server PDW.  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|<Columns inherited from sys.objects>||For a list of the inherited columns in the view, see [sys.objects &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-objects-sql-server-pdw.md).|  
|parent_column_id|**int**|ID of the column in parent_object_id to which this constraint applies.|  
|Definition|**nvarchar(4000)**|SQL expression that defines this constraint.|  
|is_system_named|**bit**|0 = User specified the constraint name.<br /><br />1 = System generated the constraint name.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
