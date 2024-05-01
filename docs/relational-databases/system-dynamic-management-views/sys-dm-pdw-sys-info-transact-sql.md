---
title: "sys.dm_pdw_sys_info (Transact-SQL)"
description: sys.dm_pdw_sys_info (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 04/15/2024
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_pdw_sys_info (Transact-SQL)
[!INCLUDE [applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Provides a set of appliance-level counters that reflect overall activity on the appliance.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
| `total_sessions` |**int**|Number of sessions currently in the system.|`0` to [maximum concurrent sessions](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-service-capacity-limits#workload-management)|  
| `idle_sessions` |**int**|Number of sessions currently idle.||  
| `active_requests` |**int**|Number of active requests currently running.||  
| `queued_requests` |**int**|Number of currently queued requests.||  
| `active_loads` |**int**|Number of loads currently running in the system.||  
| `queued_loads` |**int**|Number of queued loads waiting for execution.||  
| `active_backups` |**int**|Number of backups currently running.||  
| `active_restores` |**int**|Number of backup restores currently running.||  
  
## Examples

No filters are needed for this dynamic management view:

```sql
SELECT * FROM sys.dm_pdw_sys_info;
```

## Related content

- [SQL and Parallel Data Warehouse Dynamic Management Views](sql-and-parallel-data-warehouse-dynamic-management-views.md)
