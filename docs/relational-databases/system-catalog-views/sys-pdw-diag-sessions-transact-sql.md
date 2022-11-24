---
title: "sys.pdw_diag_sessions (Transact-SQL)"
description: sys.pdw_diag_sessions (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 4d23688a-cddb-4eed-8231-ecde2a0b0e65
monikerRange: ">=aps-pdw-2016"
---
# sys.pdw_diag_sessions (Transact-SQL)
[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

  Holds information regarding the various diagnostic sessions that have been created on the system.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|**name**|**nvarchar(255)**|Name of the diagnostics session.<br /><br /> Key for this view.||  
|**xml_data**|**nvarchar(4000)**|XML payload describing the session.||  
|**is_active**|**bit**|Flag indicating whether the flag is active.||  
|**host_address**|**nvarchar(255)**|Address of the machine hosting the session definition (Control node).||  
|**principal_id**|**int**|ID of the user that created the session at the database level.||  
|**database_id**|**int**|ID of the database that is the scope of the diagnostic session.|  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
