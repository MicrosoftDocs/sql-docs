---
title: "sys.dm_os_host_info (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/10/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
f1_keywords: 
  - "sys.dm_os_host_info"
  - "sys.dm_os_host_info_TSQL"
  - "dm_os_host_info"
  - "dm_os_host_info_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_host_info dynamic management view"
ms.assetid: 9bb6ef86-957b-4ca1-ad20-ca2f8460a86d
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_host_info (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]

Returns one row that displays operating system version information.  
  
|Column name |Data type |Description |  
|-----------------|---------------|-----------------|  
|**host_platform** |**nvarchar(256)** |The type of operating system: Windows or Linux |
|**host_distribution** |**nvarchar(256)** |Description of the operating system. |
|**host_release**|**nvarchar(256)**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows operating system release (version number). For a list of values and descriptions, see [Operating System Version (Windows)](/windows/desktop/SysInfo/operating-system-version). <br> For Linux, returns an empty string. |  
|**host_service_pack_level**|**nvarchar(256)**|Service pack level of the Windows operating system. <br> For Linux, returns an empty string. |  
|**host_sku**|**int**|Windows Stock Keeping Unit (SKU) ID. For a list of SKU IDs and descriptions, see [GetProductInfo Function](https://msdn.microsoft.com/library/ms724358.aspx). Is nullable. <br> For Linux, returns NULL. |  
|**os_language_version**|**int**|Windows locale identifier (LCID) of the operating system. For a list of LCID values and descriptions, see [Locale IDs Assigned by Microsoft](https://go.microsoft.com/fwlink/?LinkId=208080). Cannot be null.|  

## Remarks  
This view is similar to [sys.dm_os_windows_info](../../relational-databases/system-dynamic-management-views/sys-dm-os-windows-info-transact-sql.md), adding columns to differentiate Windows and Linux.
  
## Security  
  
### Permissions  
The `SELECT` permission on `sys.dm_os_host_info` is granted to the `public` role by default. If revoked, requires `VIEW SERVER STATE` permission on the server.   
 
> [!CAUTION]
>  Beginning with version [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] CTP 1.3, [!INCLUDE[ssManStudioFull_md](../../includes/ssmanstudiofull-md.md)] version 17 requires `SELECT` permission on `sys.dm_os_host_info` in order to connect to [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)]. If `SELECT` permission is revoked from `public`, only logins with `VIEW SERVER STATE` permission can connect with the newest version of SSMS. (Other tools, such as `sqlcmd.exe` can connect without `SELECT` permission on `sys.dm_os_host_info`.)

  
## Examples  
 The following example returns all columns from the **sys.dm_os_host_info** view.  
  
```  
SELECT host_platform, host_distribution, host_release, 
    host_service_pack_level, host_sku, os_language_version  
FROM sys.dm_os_host_info;  
```  

Here is a sample result set on Windows:
 
 |host_platform |host_distribution |host_release |host_service_pack_level |host_sku |os_language_version |
 |----- |----- |----- |----- |----- |----- |
 |Windows	|Windows Server 2012 R2 Standard	|6.3	|	|7	|1033 |  

Here is a sample result set on Linux:
 
 |host_platform |host_distribution |host_release |host_service_pack_level |host_sku |os_language_version |
 |----- |----- |----- |----- |----- |----- |
 |Linux	|Ubuntu	|16.04	|	|NULL	|1033 |  

  
## See Also  
 [sys.dm_os_sys_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md)   
 [sys.dm_os_windows_info (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-os-windows-info-transact-sql.md)  
 

