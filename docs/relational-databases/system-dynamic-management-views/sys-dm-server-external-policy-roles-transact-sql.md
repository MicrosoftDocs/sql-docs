---
title: "sys.dm_server_external_policy_roles (Transact-SQL)"
description: Reference documentation to explain sys.dm_server_external_policy_roles (Transact-SQL) dynamic management view.
author: srdan-bozovic-msft
ms.author: srbozovi
ms.date: "11/07/2022"
ms.service: sql
ms.subservice: system-objects
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

# sys.dm_server_external_policy_roles (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022-asdb.md)]

List all available roles, independently of them being used or not. Not all roles listed may be applicable to SQL Server. 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**role_name**|**nvarchar(128)**|ID for joining with other dynamic management views.|  
|**role_guid**|**nvarchar(128)**|The role name as defined in external policy source. Has to be unique.|  
|**modify_date**|**datetime2**|Time when the role definition was changed.|  
  
## Permissions  

Principals must have the **VIEW SERVER SECURITY STATE** permission.  

## See also

 [sys.dm_server_external_policy_principal_assigned_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-external-policy-principal-assigned-actions-transact-sql.md)  
 
- [Provision access by data owner for Azure SQL Database](/azure/purview/how-to-policies-data-owner-azure-sql-db)

- [Provision access by data owner for SQL Server on Azure Arc-enabled servers](/azure/purview/how-to-policies-data-owner-arc-sql-server)
