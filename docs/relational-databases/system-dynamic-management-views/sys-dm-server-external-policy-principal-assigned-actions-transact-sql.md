---
title: "sys.dm_server_external_policy_principal_assigned_actions (Transact-SQL)"
description: Reference documentation to explain sys.dm_server_external_policy_principal_assigned_actions (Transact-SQL) dynamic management view.
author: srdan-bozovic-msft
ms.author: srbozovi
ms.date: "11/07/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_server_external_policy_principal_assigned_actions_TSQL"
  - "sys.dm_server_external_policy_principal_assigned_actions"
  - "dm_server_external_policy_principal_assigned_actions"
  - "sys.dm_server_external_policy_principal_assigned_actions_TSQL"
helpviewer_keywords:
  - "sys.dm_server_external_policy_principal_assigned_actions dynamic management view"
dev_langs:
  - "TSQL"
---

# sys.dm_server_external_policy_principal_assigned_actions (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Lists Azure AD principals, joined with roles, joined with their data actions.

> [!NOTE]  
>  This view returns one record per assignment. If the same action has been assigned at multiple scopes (like via different role-assignments or different scopes), there will be multiple rows with the same action name in the result set.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**principal_sid**|**varbinary(85)**|SID (Security-IDentifier) of the principal.|  
|**principal_aad_object_id**|**nvarchar(36)**|Azure AD Object ID of the principal. Is unique within an Azure tenant.|  
|**action_type**|**nvarchar(256)**|The type of an operation: Connect, Select.|  
|**action_namespace**|**nvarchar(20)**|The path or namespace on which the action type part applies to.|  
|**role_name**|**nvarchar(128)**|ID for joining with other dynamic management views.|  
|**role_guid**|**nvarchar(128)**|The role name as defined in external policy source. Has to be unique.|  
|**policy_guid**|**nvarchar(128)**|Unique identifier of the policy that defines this assignment.|  
|**role_assignment_scope**|**nvarchar(4000)**|The hierarchical representation of the resource(s) that this assignment applies to.|  
|**role_assignment_type**|**int**|Type of the assignment: 1, 2|  
|**role_assignment_type_desc**|**nvarchar(5)**|Type of the assignment description: Allow, Deny|  
  
## Permissions  

Principals must have the **VIEW SERVER SECURITY STATE** permission.  

## See also

- [Provision access by data owner for Azure SQL Database](/azure/purview/how-to-policies-data-owner-azure-sql-db)

- [Provision access by data owner for SQL Server on Azure Arc-enabled servers](/azure/purview/how-to-policies-data-owner-arc-sql-server)
