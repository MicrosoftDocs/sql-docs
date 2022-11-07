---
title: "sys.dm_server_external_policy_roles (Transact-SQL)"
description: Reference documentation to explain sys.dm_server_external_policy_roles (Transact-SQL) dynamic management view.
author: srdanbozovic
ms.author: srbozovi
ms.date: "11/07/2022"
ms.prod: sql
ms.technology: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_server_external_policy_roles_TSQL"
  - "sys.dm_server_external_policy_roles"
  - "dm_server_external_policy_roles"
  - "sys.dm_server_external_policy_roles_TSQL"
helpviewer_keywords:
  - "sys.dm_server_external_policy_roles dynamic management view"
dev_langs:
  - "TSQL"
---

# dm_server_external_policy_roles (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022-asdb.md)]

List all available roles, independently of them being used or not. Not all roles listed may be applicable to SQL Server. 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**role_name**|**nvarchar(max)**|ID for joining with other dynamic management views.|  
|**role_guid**|**nvarchar(max)**|The role name as defined in external policy source. Has to be unique.|  
|**modify_date**|**datetime**|Time when the role definition was changed.|  
  
## Permissions  

Principals must have the **VIEW SERVER SECURITY STATE** permission.  
    
