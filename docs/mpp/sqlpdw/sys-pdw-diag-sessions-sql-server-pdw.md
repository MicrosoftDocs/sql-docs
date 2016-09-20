---
title: "sys.pdw_diag_sessions (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ca111ddc-2787-4205-baf0-1a242c0257a9
caps.latest.revision: 10
author: BarbKess
---
# sys.pdw_diag_sessions (SQL Server PDW)
Holds information regarding the various diagnostic sessions that have been created on the system.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|**name**|**nvarchar(255)**|Name of the diagnostics session.<br /><br />Key for this view.||  
|**xml_data**|**nvarchar(4000)**|XML payload describing the session.||  
|**is_active**|**bit**|Flag indicating whether the flag is active.||  
|**host_address**|**nvarchar(255)**|Address of the machine hosting the session definition (Control node).||  
|**principal_id**|**int**|ID of the user that created the session at the database level.||  
|**database_id**|**int**|ID of the database that is the scope of the diagnostic session.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
