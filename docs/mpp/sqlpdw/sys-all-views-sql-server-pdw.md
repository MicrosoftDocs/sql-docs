---
title: "sys.all_views (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 32b050ba-ba4a-4a56-b9c2-0516250aed20
caps.latest.revision: 11
author: BarbKess
---
# sys.all_views (SQL Server PDW)
Shows user defined and system objects.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|<inherited columns from sys.objects>||See [sys.objects &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-objects-sql-server-pdw.md)||  
|is_replicated|**bit**|1 = View is replicated.|Always 0.|  
|has_replication_filter|**bit**|1 = View has a replication filter.|Always 0.|  
|has_opaque_metadata|**bit**|1 = VIEW_METADATA option specified for view.|Always 0.|  
|has_unchecked_assembly_data|**bit**|Information not available.|Always 0, no CLR support.|  
|with_check_option|**bit**|1 = WITH CHECK OPTION was specified in the view definition.|Always 0.|  
|is_date_correlation_view|**bit**|1 = View was created automatically by the system to store correlation information between **datetime** columns.|Always 0.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
