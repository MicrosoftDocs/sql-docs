---
title: "sys.dm_server_accelerator_status (Transact-SQL)"
description: sys.dm_server_accelerator_status (Transact-SQL)
author: MikeRayMSFT
ms.author: mikeray
ms.date: "08/16/2022"
ms.prod: sql
ms.technology: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_server_accelerator_status"
  - "sys.dm_server_accelerator_status_TSQL"
  - "dm_server_accelerator_status_TSQL"
helpviewer_keywords:
  - "sys.dm_server_accelerator_status dynamic management view"
dev_langs:
  - "TSQL"
---

# sys.dm_server_accelerator_status (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

Returns information about integrated offload and acceleration solutions that are available to the current instance of SQL Server. Introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

Use this view to identify the available accelerators, the current mode description reason, the hardware availability, and the libraries and drivers configured for use and confirmed to be loaded. Use `mode_reason_desc` to verify and troubleshoot the state of the available SQL Server 

|Column name|Data type|Description|
|-----------------|---------------|-----------------|  
|accelerator|**nvarchar(60)**|Available accelerator.|
|accelerator_desc|**nvarchar(60)**|Description of available accelerators.|
|mode_desc|**nvarchar(60)**|The description of the mode.|
|mode_reason_desc|**nvarchar(60)**|The reason the mode is described that way|
|accelerator_hardware_detected|**bit**|1: Hardware is detected. <br><br> 0: Hardware is not detected. |
|accelerator_library_version|**nvarchar(60)**|The library version for the accelerator.|
|accelerator_driver_version|**nvarchar(60)**|The driver version for the accelerator.|

## Security  
  
### Permissions  
 Requires `VIEW SERVER STATE` permission on the server.  
  
## See Also  
 [sys.dm_server_registry &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-registry-transact-sql.md)  
  
