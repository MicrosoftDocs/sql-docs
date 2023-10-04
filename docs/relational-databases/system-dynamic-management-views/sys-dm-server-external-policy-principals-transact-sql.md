---
title: "sys.dm_server_external_policy_principals (Transact-SQL)"
description: Reference documentation to explain sys.dm_server_external_policy_principals (Transact-SQL) dynamic management view.
author: srdan-bozovic-msft
ms.author: srbozovi
ms.date: "11/07/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_server_external_policy_principals_TSQL"
  - "sys.dm_server_external_policy_principals"
  - "dm_server_external_policy_principals"
  - "sys.dm_server_external_policy_principals_TSQL"
helpviewer_keywords:
  - "sys.dm_server_external_policy_principals dynamic management view"
dev_langs:
  - "TSQL"
---

# sys.dm_server_external_policy_principals (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Lists all Azure AD principals that were given connect permissions through external policies.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**sid**|**varbinary(85)**|SID (Security-IDentifier) of the principal.|  
|**aad_object_id**|**nvarchar(72)**|Azure AD Object ID of the principal. Is unique within an Azure tenant.|  
|**type**|**nvarchar(4)**|Principal type: Z=External policy.|  
|**type_desc**|**nvarchar(60)**|Description of the principal type: RBAC_ASSIGNED_USER.|  
|**authentication_type**|**int**|Authentication type: 5.|  
|**authentication_type_desc**|**nvarchar(60)**|Description of the authentication type: 5=AZURE_IAM_RBAC.|  
  
## Permissions  

Principals must have the **VIEW SERVER SECURITY STATE** permission.  

## See also

 [sys.dm_server_external_policy_principal_assigned_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-external-policy-principal-assigned-actions-transact-sql.md)  
 
- [Provision access by data owner for Azure SQL Database](/azure/purview/how-to-policies-data-owner-azure-sql-db)

- [Provision access by data owner for SQL Server on Azure Arc-enabled servers](/azure/purview/how-to-policies-data-owner-arc-sql-server)
