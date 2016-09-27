---
title: "sys.foreign_key_columns (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f8bf0846-c303-4370-a3c5-52e52689ca22
caps.latest.revision: 9
author: BarbKess
---
# sys.foreign_key_columns (SQL Server PDW)
SQL Server PDW does not support foreign key columns, but it supports this view to allow integration with tools and applications. This view returns 0 rows.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|constraint_object_id|**int**|Return 0 rows.|  
|constraint_column_id|**int**||  
|parent_object_id|**int**||  
|parent_column_id|**int**||  
|referenced_object_id|**int**||  
|referenced_column_id|**int**||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
