---
title: "sys.dm_os_virtual_address_dump (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_os_virtual_address_dump"
  - "sys.dm_os_virtual_address_dump_TSQL"
  - "sys.dm_os_virtual_address_dump"
  - "dm_os_virtual_address_dump_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_virtual_address_dump dynamic management view"
ms.assetid: 7b24ea55-3873-42fd-a86c-441c92eb6175
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_virtual_address_dump (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Returns information about a range of pages in the virtual address space of the calling process.  
  
> [!NOTE]  
>  This information is also returned by the **VirtualQuery** Windows API.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_virtual_address_dump**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**region_base_address**|**varbinary(8)**|Pointer to the base address of the region of pages. Is not nullable.|  
|**region_allocation_base_address**|**varbinary(8)**|Pointer to the base address of a range of pages allocated by the VirtualAlloc Windows API function. The page pointed to by the BaseAddress member is contained within this allocation range. Is not nullable.|  
|**region_allocation_protection**|**varbinary(8)**|Protection attributes when the region was first allocated. The value is one of the following:<br /><br /> -   PAGE_READONLY<br />-   PAGE_READWRITE<br />-   PAGE_NOACCESS<br />-   PAGE_WRITECOPY<br />-   PAGE_EXECUTE<br />-   PAGE_EXECUTE_READ<br />-   PAGE_EXECUTE_READWRITE<br />-   PAGE_EXECUTE_WRITECOPY<br />-   PAGE_GUARD<br />-   PAGE_NOCACHE<br /><br /> Is not nullable.|  
|**region_size_in_bytes**|**bigint**|Size of the region, in bytes, starting at the base address in which all the pages have the same attributes. Is not nullable.|  
|**region_state**|**varbinary(8)**|Current state of the region. This is one of the following:<br /><br /> -   MEM_COMMIT<br />-   MEM_RESERVE<br />-   MEM_FREE<br /><br /> Is not nullable.|  
|**region_current_protection**|**varbinary(8)**|Protection attributes. The value is one of the following:<br /><br /> -   PAGE_READONLY<br />-   PAGE_READWRITE<br />-   PAGE_NOACCESS<br />-   PAGE_WRITECOPY<br />-   PAGE_EXECUTE<br />-   PAGE_EXECUTE_READ<br />-   PAGE_EXECUTE_READWRITE<br />-   PAGE_EXECUTE_WRITECOPY<br />-   PAGE_GUARD<br />-   PAGE_NOCACHE<br /><br /> Is not nullable.|  
|**region_type**|**varbinary(8)**|Identifies the types of pages in the region. The value can be one of the following:<br /><br /> -   MEM_PRIVATE<br />-   MEM_MAPPED<br />-   MEM_IMAGE<br /><br /> Is not nullable.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
  


