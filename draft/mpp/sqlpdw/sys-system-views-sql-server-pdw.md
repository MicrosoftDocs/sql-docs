---
title: "sys.system_views (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ffbf2199-ff8b-4c3a-bc87-52eadaa21851
caps.latest.revision: 10
author: BarbKess
---
# sys.system_views (SQL Server PDW)
Same definition as sys.views, but shows system views only.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|<inherited columns from sys.objects>||See [sys.objects &#40;SQL Server PDW&#41;](../sqlpdw/sys-objects-sql-server-pdw.md)||  
|is_replicated|**bit**|1 = View is replicated.|Always 0.|  
|has_replication_filter|**bit**|1 = View has a replication filter.|Always 0.|  
|has_opaque_metadata|**bit**|1 = VIEW_METADATA option specified for view.|Always 0.|  
|has_unchecked_assembly_data|**bit**|Information not available.|Always 0, no CLR support.|  
|with_check_option|**bit**|1 = WITH CHECK OPTION was specified in the view definition.|Always 0.|  
|is_date_correlation_view|**bit**|1 = View was created automatically by the system to store correlation information between **datetime** columns.|Always 0.|  
|is_tracked_by_cdc|**bit**|Information not available.|Always 0.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
