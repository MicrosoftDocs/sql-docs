---
title: "sys.server_audits (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/05/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "server_audits_TSQL"
  - "sys.server_audits_TSQL"
  - "sys.server_audits"
  - "server_audits"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.server_audits catalog view"
ms.assetid: c2c4a000-1127-46a8-b1e9-947fd1136e1e
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.server_audits (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row for each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] audit in a server instance. For more information, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**audit_id**|**int**|ID of the audit.|  
|**name**|**sysname**|Name of the audit.|  
|**audit_guid**|**uniqueidentifier**|GUID for the audit that is used to enumerate audits with member Server&#124;Database audit specifications during server start-up and database attach operations.|  
|**create_date**|**datetime**|UTC date the audit was created.|  
|**modify_date**|**datetime**|UTC date the audit was last modified.|  
|**principal_id**|**int**|ID of the owner of the audit, as registered to the server.|  
|**type**|**char(2)**|Audit type:<br /><br /> SL - NT Security event log<br /><br /> AL - NT Application event log<br /><br /> FL - File on file system|  
|**type_desc**|**nvarchar(60)**|SECURITY LOG<br /><br /> APPICATION LOG<br /><br /> FILE|  
|**on_failure**|**tinyint**|On Failure to write an action entry:<br /><br /> 0 - Continue<br /><br /> 1 - Shutdown server instance<br /><br /> 2 - Fail operation|  
|**on_failure_desc**|**nvarchar(60)**|On Failure to write an action entry:<br /><br /> CONTINUE<br /><br /> SHUTDOWN SERVER INSTANCE<br /><br /> FAIL_OPERATION|  
|**is_state_enabled**|**tinyint**|0 - Disabled<br /><br /> 1 - Enabled|  
|**queue_delay**|**int**|Maximum time, in milliseconds, to wait before writing to disk. If 0, the audit will guarantee a write before an event can continue.|  
|**predicate**|**nvarchar(3000)**|The predicate expression that is applied to the event.|  
  
## Permissions  
 Principals with the **ALTER ANY SERVER AUDIT** or **VIEW ANY DEFINITION** permission have access to this catalog view. In addition, the principal must not be denied **VIEW ANY DEFINITION** permission.  
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
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
 [sys.server_file_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)   
 [sys.server_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)   
 [sys.server_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)   
 [sys.database_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)   
 [sys.database_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)   
 [sys.dm_server_audit_status &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)   
 [sys.dm_audit_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)   
 [sys.dm_audit_class_type_map &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql.md)   
 [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)  
  
  
