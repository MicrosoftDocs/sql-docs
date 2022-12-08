---
title: "sys.dm_os_stacks (Transact-SQL)"
description: sys.dm_os_stacks (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_stacks"
  - "dm_os_stacks_TSQL"
  - "sys.dm_os_stacks"
  - "sys.dm_os_stacks_TSQL"
helpviewer_keywords:
  - "sys.dm_os_stacks dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: a69b06c4-28f0-4535-8fa1-9f132db4d916
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_stacks (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

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

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   


## See Also  
  [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
