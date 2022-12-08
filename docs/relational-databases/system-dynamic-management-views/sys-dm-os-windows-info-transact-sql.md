---
title: "sys.dm_os_windows_info (Transact-SQL)"
description: sys.dm_os_windows_info (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/30/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_windows_info"
  - "dm_os_windows_info_TSQL"
  - "sys.dm_os_windows_info"
  - "sys.dm_os_windows_info_TSQL"
helpviewer_keywords:
  - "sys.dm_os_windows_info dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: adc81283-fdc2-46c0-bb48-abe82bbf2459
---
# sys.dm_os_windows_info (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns one row that displays Windows operating system version information.  
  
  Only applies to SQL Server running on Windows. To see similar informaton for SQL Server running on a non-Windows host, such as Linux, use [sys.dm_os_host_info &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/sys-dm-os-host-info-transact-sql.md). 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**windows_release**|**nvarchar(256)**|For Windows, returns the release number. For a list of values and descriptions, see [Operating System Version (Windows)](/windows/desktop/SysInfo/operating-system-version). Cannot be NULL.|  
|**windows_service_pack_level**|**nvarchar(256)**| For Windows, returns the service pack number. Cannot be NULL. |  
|**windows_sku**|**int**|For Windows, returns the Windows Stock Keeping Unit (SKU) ID. For a list of SKU IDs and descriptions, see [GetProductInfo Function](/windows/win32/api/sysinfoapi/nf-sysinfoapi-getproductinfo). Is NULLable. |  
|**os_language_version**|**int**| For Windows, returns the Windows locale identifier (LCID) of the operating system. For a list of LCID values and descriptions, see [Locale IDs Assigned by Microsoft](/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c). Cannot be NULL.|  
  
  
## Permissions  
The SELECT permission on sys.dm_os_windows_info is granted to the public role by default. If revoked, requires VIEW SERVER STATE permission on the server.  

## Limitations and Restrictions
To see informaton for SQL running on a non-Windows host, such as Linux, use [sys.dm_os_host_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-host-info-transact-sql.md). 
  
## Examples  
 The following example returns all columns from the **sys.dm_os_windows_info** view.  
  
```  
SELECT windows_release, windows_service_pack_level, windows_sku, os_language_version  
FROM sys.dm_os_windows_info;  
```  
  
 Here is a sample result set.  
  
 `windows_release  windows_service_pack_level   windows_sku   os_language_version`  
  
 `---------------  ---------------------------  ------------  -------------------`  
  
 `6.0              Service Pack 2                4            1033`  
  
## See Also  
 [sys.dm_os_sys_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md)   
 [sys.dm_os_host_info](../../relational-databases/system-dynamic-management-views/sys-dm-os-host-info-transact-sql.md)  
  
