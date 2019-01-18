---
title: "sys.dm_os_windows_info (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_os_windows_info"
  - "dm_os_windows_info_TSQL"
  - "sys.dm_os_windows_info"
  - "sys.dm_os_windows_info_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_windows_info dynamic management view"
ms.assetid: adc81283-fdc2-46c0-bb48-abe82bbf2459
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_os_windows_info (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns one row that displays Windows operating system version information.  
  
  Only applies to SQL Server running on Windows. To see similar informaton for SQL Server running on a non-Windows host, such as Linux, use [sys.dm_os_host_info &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/sys-dm-os-host-info-transact-sql.md). 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**windows_release**|**nvarchar(256)**|For Windows, returns the release number. For a list of values and descriptions, see [Operating System Version (Windows)](/windows/desktop/SysInfo/operating-system-version). Cannot be NULL.|  
|**windows_service_pack_level**|**nvarchar(256)**| For Windows, returns the service pack number. Cannot be NULL. |  
|**windows_sku**|**int**|For Windows, returns the Windows Stock Keeping Unit (SKU) ID. For a list of SKU IDs and descriptions, see [GetProductInfo Function](https://msdn.microsoft.com/library/ms724358.aspx). Is NULLable. |  
|**os_language_version**|**int**| For Windows, returns the Windows locale identifier (LCID) of the operating system. For a list of LCID values and descriptions, see [Locale IDs Assigned by Microsoft](https://go.microsoft.com/fwlink/?LinkId=208080). Cannot be NULL.|  
  
  
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
  
  

