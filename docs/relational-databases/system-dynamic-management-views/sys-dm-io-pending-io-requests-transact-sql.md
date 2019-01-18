---
title: "sys.dm_io_pending_io_requests (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_io_pending_io_requests"
  - "dm_io_pending_io_requests"
  - "dm_io_pending_io_requests_TSQL"
  - "sys.dm_io_pending_io_requests_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_io_pending_io_requests dynamic management view"
ms.assetid: d1fb46dd-5c74-4c04-9ecf-8934b1bedb5b 
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_io_pending_io_requests (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a row for each pending I/O request in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_io_pending_io_requests**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**io_completion_request_address**|**varbinary(8)**|Memory address of the IO request. Is not nullable.|  
|**io_type**|**nvarchar(60)**|Type of pending I/O request. Is not nullable.|  
|**io_pending_ms_ticks**|**bigint**|Internal use only. Is not nullable.| 
|**io_pending**|**int**|Indicates whether the I/O request is pending or has been completed by Windows. An I/O request can still be pending even when Windows has completed the request, but [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has not yet performed a context switch in which it would process the I/O request and remove it from this list. Is not nullable.|  
|**io_completion_routine_address**|**varbinary(8)**|Internal function to call when the I/O request is completed. Is nullable.|  
|**io_user_data_address**|**varbinary(8)**|Internal use only. Is nullable.|  
|**scheduler_address**|**varbinary(8)**|Scheduler on which this I/O request was issued. The I/O request will appear on the pending I/O list of the scheduler. For more information, see [sys.dm_os_schedulers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-schedulers-transact-sql.md). Is not nullable.|  
|**io_handle**|**varbinary(8)**|File handle of the file that is used in the I/O request. Is nullable.|  
|**io_offset**|**bigint**|Offset of the I/O request. Is not nullable.|  
|**io_handle_path**|**nvarchar(256)**| Path of file that is used in the I/O request. Is nullable.|
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [I O Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/i-o-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  


