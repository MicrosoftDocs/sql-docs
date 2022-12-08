---
title: "sys.fn_PageResCracker (Transact-SQL) | Microsoft Docs"
description: Learn about the sys.fn_PageResCracker system function. See examples and view additional available resources.
ms.custom: ""
ms.date: "09/18/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "fn_PageResCracker"
  - "sys.fn_PageResCracker_TSQL"
  - "fn_PageResCracker_TSQL"
  - "sys.fn_PageResCracker"
  - "sys.dm_db_page_info"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_PageResCracker function"
  - "page_resource"
  - "sys.fn_PageResCracker function"
  - "sys.dm_db_page_info"
  - "page info"
author: "bluefooted"
ms.author: "pamela"
---
# sys.fn_PageResCracker (Transact-SQL)
[!INCLUDE[SQL Server 2019](../../includes/applies-to-version/sqlserver2019.md)]

Returns the `db_id`, `file_id`, and `page_id` for the given `page_resource` value. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
```  
sys.fn_PageResCracker ( page_resource )  
```  
  
## Arguments  
*page_resource*    
Is the 8-byte hexadecimal format of a database page resource.
  
## Tables Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|db_id|**int**|Database ID|  
|file_id|**int**|File ID|  
|page_id|**int**|Page ID|  
  
## Remarks  
`sys.fn_PageResCracker` is used to convert the 8-byte hexadecimal representation of a database page to a rowset that contains the database ID, file ID and page ID of the page.   

You can obtain a valid page resource from the `page_resource` column of the [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) dynamic management view or the [sys.sysprocesses &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysprocesses-transact-sql.md) system view. If an invalid page resource is used then the return is NULL.  
The primary use of `sys.fn_PageResCracker` is to facilitate joins between these views and the [sys.dm_db_page_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-page-info-transact-sql.md) dynamic management function in order to obtain information about the page, such as the object to which it belongs.
  
## Permissions  
The user needs `VIEW SERVER STATE` permission on the server.  
  
## Examples  
The `sys.fn_PageResCracker` function can be used in conjunction with [sys.dm_db_page_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-page-info-transact-sql.md) to troubleshoot page related waits and blocking in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  The following script is an example of how you can use these functions to gather database page information for all active requests that are currently waiting on some type of page resource. 
  
```sql  
SELECT page_info.* 
FROM sys.dm_exec_requests AS d  
CROSS APPLY sys.fn_PageResCracker (d.page_resource) AS r  
CROSS APPLY sys.dm_db_page_info(r.db_id, r.file_id, r.page_id, 'DETAILED') AS page_info
```  
  
## See Also  
 [sys.dm_db_page_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-page-info-transact-sql.md)  
 [sys.sysprocesses &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysprocesses-transact-sql.md)   
 [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)  
  
  
