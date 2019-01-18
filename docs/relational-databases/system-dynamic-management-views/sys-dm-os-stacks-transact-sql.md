---
title: "sys.dm_os_stacks (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_os_stacks"
  - "dm_os_stacks_TSQL"
  - "sys.dm_os_stacks"
  - "sys.dm_os_stacks_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_stacks dynamic management view"
ms.assetid: a69b06c4-28f0-4535-8fa1-9f132db4d916
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_stacks (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  This dynamic management view is used internally by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to do the following:  
  
-   Keep track of debug data such as outstanding allocations.  
  
-   Assume or validate logic that is used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components in places where the component assumes that a certain call has been made.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**stack_address**|**varbinary(8)**|Unique address for this stack allocation. Is not nullable.|  
|**frame_index**|**int**|Each line represents a function call that, when sorted in ascending order by frame index for a particular **stack_address**, returns the full call stack. Is not nullable.|  
|**frame_address**|**varbinary(8)**|Address of the function call. Is not nullable.|  
  
## Remarks  
 **sys.dm_os_stacks** requires that the symbols of the server and other components be present on the server to display the information correctly.  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   


## See Also  
  [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
  
