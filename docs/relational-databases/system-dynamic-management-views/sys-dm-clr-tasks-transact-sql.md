---
title: "sys.dm_clr_tasks (Transact-SQL)"
description: sys.dm_clr_tasks (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "02/24/2023"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_clr_tasks"
  - "sys.dm_clr_tasks_TSQL"
  - "dm_clr_tasks"
  - "dm_clr_tasks_TSQL"
helpviewer_keywords:
  - "sys.dm_clr_tasks dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_clr_tasks (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a row for all common language runtime (CLR) tasks that are currently running. A [!INCLUDE[tsql](../../includes/tsql-md.md)] batch that contains a reference to a CLR routine creates a separate task for execution of all the managed code in that batch. Multiple statements in the batch that require managed code execution use the same CLR task. The CLR task is responsible for maintaining objects and state pertaining to managed code execution, as well as the transitions between the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the common language runtime.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**task_address**|**varbinary(8)**|Address of the CLR task.|  
|**sos_task_address**|**varbinary(8)**|Address of the underlying [!INCLUDE[tsql](../../includes/tsql-md.md)] batch task.|  
|**appdomain_address**|**varbinary(8)**|Address of the application domain in which this task is running.|  
|**state**|**nvarchar(128)**|Current state of the task.|  
|**abort_state**|**nvarchar(128)**|State the abort is currently in (if the task was canceled) There are multiple states involved while aborting tasks.|  
|**type**|**nvarchar(128)**|Task type.|  
|**affinity_count**|**int**|Affinity of the task.|  
|**forced_yield_count**|**int**|Number of times the task was forced to yield.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## See also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Common Language Runtime Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/common-language-runtime-related-dynamic-management-views-transact-sql.md)  
  
