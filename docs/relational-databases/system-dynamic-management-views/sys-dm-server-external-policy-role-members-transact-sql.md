---
title: "sys.dm_server_external_policy_role_members (Transact-SQL)"
description: Reference documentation to explain sys.dm_server_external_policy_role_members (Transact-SQL) dynamic management view.
author: srdan-bozovic-msft
ms.author: srbozovi
ms.date: "11/07/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_server_external_policy_role_members_TSQL"
  - "sys.dm_server_external_policy_role_members"
  - "dm_server_external_policy_role_members"
  - "sys.dm_server_external_policy_role_members_TSQL"
helpviewer_keywords:
  - "sys.dm_server_external_policy_role_members dynamic management view"
dev_langs:
  - "TSQL"
---

# sys.dm_server_external_policy_role_members (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Lists Azure AD principals assigned to a given role on a given resource scope. 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**principal_aad_object_id**|**int**|Azure AD Object ID of the principal. Is unique within an Azure tenant.|  
|**role_guid**|**nvarchar(36)**|The role name as defined in external policy source.|  
|**action_type**|**nvarchar(128)**|The type of an operation: Connect, Select.|  
|**policy_guid**|**nvarchar(128)**|Unique identifier of the policy that defines this assignment.|  
|**assignment_scope**|**nvarchar(4000)**|The hierarchical representation of the resource(s) that this assignment applies to.|  
|**assignment_type**|**int**|Type of the assignment: 1, 2|  
|**assignment_type_desc**|**nvarchar(5)**|Type of the assignment description: Allow, Deny|  
  
## Permissions  

Principals must have the **VIEW SERVER SECURITY STATE** permission.  

## See also

 [sys.dm_server_external_policy_principal_assigned_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-external-policy-principal-assigned-actions-transact-sql.md)  
 
- [Provision access by data owner for Azure SQL Database](/azure/purview/how-to-policies-data-owner-azure-sql-db)

- [Provision access by data owner for SQL Server on Azure Arc-enabled servers](/azure/purview/how-to-policies-data-owner-arc-sql-server)
