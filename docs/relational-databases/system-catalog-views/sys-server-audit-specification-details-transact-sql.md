---
title: "sys.server_audit_specification_details (Transact-SQL)"
description: sys.server_audit_specification_details (Transact-SQL)
author: sravanisaluru
ms.author: srsaluru
ms.date: "03/23/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "server_audit_specification_details"
  - "sys.server_audit_specification_details_TSQL"
  - "server_audit_specification_details_TSQL"
  - "sys.server_audit_specification_details"
helpviewer_keywords:
  - "sys.server_audit_specification_details catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 792724dc-402e-4b17-9f2c-029d910bf88e
---
# sys.server_audit_specification_details (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains information about the server audit specification details (actions) in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] audit on a server instance. For more information, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md). For a list of all audit_action_id's and their names, query [sys.dm_audit_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|server_specification_id|**int**|ID of the audit server specification|  
|audit_action_id|**int**|ID of the audit action|  
|audit_action_name|**sysname**|Name of group or name of audit action|  
|class|**tinyint**|Reserved|  
|class_desc|**nvarchar(60)**|Reserved|  
|major_id|**int**|Reserved|  
|minor_id|**int**|Reserved|  
|audited_principal_id|**int**|Reserved|  
|audited_result|**nvarchar(60)**|Audited result:<br /><br /> - SUCCESS AND FAILURE<br /><br /> - SUCCESS<br /><br /> - FAILURE|  
|is_group|**bit**|Whether the audited object is a group:<br /><br /> 0 - Not a group<br /><br /> 1 - Group|  
  
## Permissions  
 Principals with the **ALTER ANY SERVER AUDIT** or **VIEW ANY DEFINITION** permission have access to this catalog view. In addition, the principal must not be denied **VIEW ANY DEFINITION** permission.  
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [CREATE SERVER AUDIT &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-transact-sql.md)   
 [ALTER SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-transact-sql.md)   
 [DROP SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-transact-sql.md)   
 [CREATE SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-specification-transact-sql.md)   
 [ALTER SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)   
 [DROP SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)   
 [CREATE DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-audit-specification-transact-sql.md)   
 [ALTER DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-audit-specification-transact-sql.md)   
 [DROP DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-audit-specification-transact-sql.md)   
 [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md)   
 [sys.fn_get_audit_file &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)   
 [sys.server_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)   
 [sys.server_file_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)   
 [sys.server_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)   
 [sys.database_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)   
 [sys.database_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)   
 [sys.dm_server_audit_status &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)   
 [sys.dm_audit_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)   
 [sys.dm_audit_class_type_map &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql.md)   
 [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)  
  
  
