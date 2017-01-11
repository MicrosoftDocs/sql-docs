---
title: "sys.dm_pdw_lock_waits (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: fbc69245-7d66-45d8-9209-85aa9daa9ac7
caps.latest.revision: 7
author: BarbKess
---
# sys.dm_pdw_lock_waits (SQL Server PDW)
Holds information about the requests that are waiting for locks.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|wait_id|**bigint**|Position of the request in the waiting list.|0-based ordinal. This is not unique across all wait entries.|  
|session_id|**nvarchar(32)**|ID of the session in which the wait state occurred.|See session_id in [sys.dm_pdw_exec_sessions &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-sessions-sql-server-pdw.md).|  
|type|**nvarchar(255)**|Type of wait this entry represents.|Possible values:<br /><br />Shared<br /><br />SharedUpdate<br /><br />ExclusiveUpdate<br /><br />Exclusive|  
|object_type|**nvarchar(255)**|Type of object that is affected by the wait.|Possible values:<br /><br />OBJECT<br /><br />DATABASE<br /><br />SYSTEM<br /><br />SCHEMA<br /><br />APPLICATION|  
|object_name|**nvarchar(386)**|Name or GUID of the specified object that was affected by the wait.|Tables and views are displayed with three-part names.<br /><br />Indexes and statistics are displayed with four-part names.<br /><br />Names, principals, and databases are string names.|  
|request_id|**nvarchar(32)**|ID of the request on which the wait state occurred.|ID of the request.<br /><br />This is a GUID for load requests.|  
|request_time|**datetime**|Time at which the lock or resource was requested.||  
|acquire_time|**datetime**|Time at which the lock or resource was acquired.||  
|state|**nvarchar(50)**|State of the wait state.|Information not available.|  
|priority|**int**|Priority of the waiting item.|Information not available.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[Locking Behavior &#40;SQL Server PDW&#41;](../sqlpdw/locking-behavior-sql-server-pdw.md)  
  
