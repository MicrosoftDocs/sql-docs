---
title: "sys.dm_io_pending_io_requests (Transact-SQL)"
description: sys.dm_io_pending_io_requests (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/30/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_io_pending_io_requests"
  - "dm_io_pending_io_requests"
  - "dm_io_pending_io_requests_TSQL"
  - "sys.dm_io_pending_io_requests_TSQL"
helpviewer_keywords:
  - "sys.dm_io_pending_io_requests dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: d1fb46dd-5c74-4c04-9ecf-8934b1bedb5b
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_io_pending_io_requests (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a row for each pending I/O request in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_io_pending_io_requests**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
>

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**io_completion_request_address**|**varbinary(8)**|Memory address of the IO request. Is not nullable.|  
|**io_type**|**nvarchar(60)**|Type of pending I/O request. Is not nullable.|  
|**io_pending_ms_ticks**|**bigint**|Internal use only. Is not nullable.| 
|**io_pending**|**int**|Indicates whether the I/O request is pending (1) or has been completed by the operating system (0). An I/O request can still be pending even when OS has completed the request, but [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has not yet performed a context switch in which it would process the I/O request and remove it from this list. Is not nullable. <br /> **Value** <br /> 0 = Pending SQL Server <br /> 1 = Pending OS <br />|  
|**io_completion_routine_address**|**varbinary(8)**|Internal function to call when the I/O request is completed. Is nullable.|  
|**io_user_data_address**|**varbinary(8)**|Internal use only. Is nullable.|  
|**scheduler_address**|**varbinary(8)**|Scheduler on which this I/O request was issued. The I/O request will appear on the pending I/O list of the scheduler. For more information, see [sys.dm_os_schedulers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-schedulers-transact-sql.md). Is not nullable.|  
|**io_handle**|**varbinary(8)**|File handle of the file that is used in the I/O request. Is nullable.|  
|**io_offset**|**bigint**|Offset of the I/O request. Is not nullable.|  
|**io_handle_path**|**nvarchar(256)**| Path of file that is used in the I/O request. Is nullable.|
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|

  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [I O Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/i-o-related-dynamic-management-views-and-functions-transact-sql.md)  
  
