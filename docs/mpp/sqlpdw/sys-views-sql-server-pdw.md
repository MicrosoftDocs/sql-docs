---
title: "sys.views (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 78287324-b416-4b53-bd64-90d26dd8781e
caps.latest.revision: 13
author: BarbKess
---
# sys.views (SQL Server PDW)
Contains a row for each view object. The visibility of table objects is limited to securables that a user either owns or on which the user has been granted some permission.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|<inherited columns from sys.objects>||See [sys.objects &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-objects-sql-server-pdw.md)||  
|is_replicated|**bit**|1 = View is replicated.|Always 0.|  
|has_replication_filter|**bit**|1 = View has a replication filter.|Always 0.|  
|has_opaque_metadata|**bit**|1 = VIEW_METADATA option specified for view.|Always 0.|  
|has_unchecked_assembly_data|**bit**|Information not available.|Always 0, no CLR support.|  
|with_check_option|**bit**|1 = WITH CHECK OPTION was specified in the view definition.|Always 0.|  
|is_date_correlation_view|**bit**|1 = View was created automatically by the system to store correlation information between **datetime** columns.|Always 0.|  
|is_tracked_by_cdc|**bit**|Information not available.|Always 0.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SQL Server Catalog Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sql-server-catalog-views-sql-server-pdw.md)  
[sys.all_views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-all-views-sql-server-pdw.md)  
  
