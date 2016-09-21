---
title: "sys.foreign_keys (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 180b16a4-2e29-412b-87b5-c3e54d00962f
caps.latest.revision: 9
author: BarbKess
---
# sys.foreign_keys (SQL Server PDW)
SQL Server PDW does not support foreign keys, but it supports this view to allow integration of tools and applications. This view returns 0 rows.  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|<Columns inherited from sys.objects>||Returns 0 rows.|  
|referenced_object_id|**int**||  
|key_index_id|**int**||  
|is_disabled|**bit**||  
|is_not_for_replication|**bit**||  
|is_not_trusted|**bit**||  
|delete_referential_action|**tinyint**||  
|delete_referential_action_desc|**nvarchar(60)**||  
|update_referential_action|**tinyint**||  
|update_referential_action_desc|**nvarchar(60)**||  
|is_system_named|**bit**||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
