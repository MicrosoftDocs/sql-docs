---
title: "sys.dm_audit_actions (Transact-SQL)"
description: sys.dm_audit_actions (Transact-SQL)
author: sravanisaluru
ms.author: srsaluru
ms.date: "03/23/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_audit_actions_TSQL"
  - "sys.dm_audit_actions"
  - "dm_audit_actions_TSQL"
  - "dm_audit_actions"
helpviewer_keywords:
  - "sys.dm_audit_actions dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: b987c2b9-998a-4a5f-a82d-280dc6963cbe
monikerRange: "=azuresqldb-current||>=sql-server-2016||=azuresqldb-mi-current"
---
# sys.dm_audit_actions (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a row for every audit action that can be reported in the audit log and every audit action group that can be configured as part of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Audit. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Audit, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**action_id**|**varchar(4)**|ID of the audit action. Related to the **action_id** value written to each audit record. Is nullable. NULL for audit groups.|  
|**action_in_log**|**bit**|Indicates whether an action can be written to an audit log. Values are as follows:<br /><br /> 1 = Yes<br /><br /> 0 = No|  
|**name**|**sysname**|Name of the audit action or action group. Isn't nullable.|  
|**class_desc**|**nvarchar(120)**|The name of the class of the object that the audit action applies to. Can be any one of the Server, Database, or Schema scope objects, but doesn't include Schema objects. Isn't nullable.|  
|**parent_class_desc**|**nvarchar(120)**|Name of the parent class for the object described by class_desc. Is NULL if the class_desc is Server.|  
|**covering_parent_action_name**|**nvarchar(120)**|Name of the audit action or audit group that contains the audit action described in this row. This is used to create a hierarchy of actions and covering actions. Is nullable.|  
|**configuration_level**|**nvarchar(10)**|Indicates that the action or action group specified in this row is configurable at the Group or Action level. Is NULL if the action isn't configurable.|  
|**containing_group_name**|**nvarchar(120)**|The name of the audit group that contains the specified action. Is NULL if the value in name is a group.|  
  
## Permissions  
This view is visible to the public.
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See also

- [CREATE SERVER AUDIT &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-transact-sql.md)   
- [ALTER SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-transact-sql.md)   
- [DROP SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-transact-sql.md)   
- [CREATE SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-specification-transact-sql.md)   
- [ALTER SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)   
- [DROP SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)   
- [CREATE DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-audit-specification-transact-sql.md)   
- [ALTER DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-audit-specification-transact-sql.md)   
- [DROP DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-audit-specification-transact-sql.md)   
- [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md)   
- [sys.fn_get_audit_file &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)   
- [sys.server_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)   
- [sys.server_file_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)   
- [sys.server_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)   
- [sys.server_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)   
- [sys.database_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)   
- [sys.database_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)   
- [sys.dm_server_audit_status &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)   
- [sys.dm_audit_class_type_map &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql.md)   
- [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)  
