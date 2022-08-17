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

Use this view to identify the available accelerators, the current mode description reason, the hardware availability, and the libraries and drivers configured for use and confirmed to be loaded. Use `mode_reason_desc` to verify and troubleshoot the state of the available accelerators. 

|Column name|Data type|Description|
|-----------------|---------------|-----------------|  
|accelerator|**nvarchar(60)**|Available accelerator.|
|accelerator_desc|**nvarchar(60)**|Description of available accelerators.|
|mode_desc|**nvarchar(60)**|The description of the mode.|
|mode_reason_desc|**nvarchar(60)**|The reason for the mode, used for troubleshooting.<BR /><BR />NONE_HARDWARE_OFFLOAD_NOT_ENABLED (0) – Status when the hardware offloading is not enabled on the instance. This status is the default on a Windows Server when the `sp_configure 'hardware_offload_config'` command has not been enabled.<BR /><BR />NONE_HARDWARE_OFFLOAD_LINUX_NOT_SUPPORTED (1) – The current message on Linux platforms as Intel QAT is currently not supported.<BR /><BR />NONE_ACCELERATOR_CONFIG_NOT_ENABLED (2) – The ‘hardware_offload_config’ may be configured, but the Intel QAT accelerator mode is not enabled via the ALTER SERVER CONIGURATION command.  For example: `ALTER SERVER CONFIGURATION SET HARDWARE_OFFLOAD = ON (ACCELERATOR = QAT)`<BR /><BR />NONE_ACCELERATOR_LOAD_FAILED (3) – There is a failure loading qatzip.dll that is part of the Intel driver solution. For assistance on this status, it is necessary to connect support from the [Intel QuickAssist Technology Landing Page](https://developer.intel.com/quickassist).<BR /><BR />NONE_ACCELERATOR_PROC_FAILED (4) – There is a failure locating proc addresses in qatzip.dll that is part of the Intel driver solution. For assistance on this status, it is necessary to connect support from the [Intel QuickAssist Technology Landing Page](https://developer.intel.com/quickassist).<BR /><BR />NONE_ACCELERATOR_VERSION_NOT_COMPATIBLE (7) – The installed `qatzip.dll` and `isa-l.dll` versions are not compatible with SQL Server. It is required to install the latest supported version of the drivers from the [Intel QuickAssist Technology Landing Page](https://developer.intel.com/quickassist).<BR /><BR />NONE_ACCELERATOR_INITIALIZATION_FAILED (8) – There was a failure initializing the Intel QAT accelerator. It is recommended to check the error log for the availability of the hardware, and the Intel QAT driver and library versions. For assistance on this status, it is necessary to connect support from the [Intel QuickAssist Technology Landing Page](https://developer.intel.com/quickassist).<BR /><BR />NONE_ACCELERATOR_SESSION_FAILED (9) – There was a failure setting up the Intel QAT accelerator. It is recommended to check the error log for the availability of the hardware, and the Intel QAT driver and library versions. It is recommended to verify that the drivers were successfully installed. For assistance on this status, it is necessary to connect support from the [Intel QuickAssist Technology Landing Page](https://developer.intel.com/quickassist).<BR /><BR />NONE_ACCELERATOR_LIBRARY_NOT_FOUND (10) – The qatzip.dll or isa-l.dll libraries are not available. It is recommended to verify that the drivers were successfully installed. For assistance on this status, it is necessary to connect support from the [Intel QuickAssist Technology Landing Page](https://developer.intel.com/quickassist).<BR /><BR />SOFTWARE_MODE_NON_ENTERPRISE_SKU (11) – SQL Server 2022 instance with the Intel® QuickAssist Technology enabled is leveraging software mode for the non-enterprise edition instance.<BR /><BR />SOFTWARE_MODE_ACCELERATOR_HARDWARE_NOT_FOUND (12) – The SQL Server instance with Intel QuickAssist Technology enabled is leveraging software mode Software mode due to the hardware device not being available. This could be due to device or driver failure. For assistance on this status, it is necessary to connect support from the [Intel QuickAssist Technology Landing Page](https://developer.intel.com/quickassist).<BR><BR>SOFTWARE_MODE_SOFTWARE_FORCE_OVERRIDE (13) – User forced software mode for SQL Server Enterprise edition using  `ALTER SERVER CONFIGURATION SET HARDWARE_OFFLOAD = ON (ACCELERATOR = QAT, MODE = SOFTWARE) `<BR><BR>HARDWARE_MODE_ENTERPRISE_SKU (14) – The SQL Server 2022 instance with Intel QuickAssist Technology enabled is leveraging hardware support, with software fallback, for SQL Server Enterprise edition. This is the required `mode_reason_desc` in order to support hardware offloading for any accelerator in SQL Server 2022 (16.x) and later. |
|accelerator_hardware_detected|**tinyint**|1: Hardware is detected. <br><br> 0: Hardware is not detected. |
|accelerator_library_version|**nvarchar(60)**|The library version for the accelerator.|
|accelerator_driver_version|**nvarchar(60)**|The driver version for the accelerator.|

## Security  
  
## Remarks

The `sys.dm_server_accelerator_status` lists available accelerators for your version of SQL Server. A row for Intel QuickAssist Technology (QAT) for backup/restore compression appears by default starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], even if the QAT hardware is not present and the QAT driver is not installed. Other hardware or software accelerators may be added in the future cumulative updates and major releases of SQL Server.  
### Permissions  
 Requires `VIEW PERFORMANCE STATE` permission on the server.  
  
## See Also  
 [sys.dm_server_registry &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-registry-transact-sql.md)  
  
