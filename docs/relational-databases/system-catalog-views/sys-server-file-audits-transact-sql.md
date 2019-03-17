---
title: "sys.server_file_audits (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/05/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "server_file_audits_TSQL"
  - "sys.server_file_audits_TSQL"
  - "sys.server_file_audits"
  - "server_file_audits"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.server_file_audits catalog view"
ms.assetid: 553288a0-be57-4d79-ae53-b7cbd065e127
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.server_file_audits (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains extended information about the file audit type in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] audit on a server instance. For more information, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|audit_id|**int**|ID of the audit.|  
|name|**sysname**|Name of the audit.|  
|audit_guid|**uniqueidentifier**|GUID of the audit.|  
|create_date|**datetime**|UTC date when the file audit was created.|  
|modify_date|**datatime**|UTC date when the file audit was last modified.|  
|principal_id|**int**|ID of the owner of the audit as registered on the server.|  
|type|**char(2)**|Audit type:<br /><br /> 0 = NT Security event log<br /><br /> 1 = NT Application event log<br /><br /> 2 = File on file system|  
|type_desc|**nvarchar(60)**|Audit type description.|  
|on_failure|**tinyint**|On Failure condition:<br /><br /> 0 = Continue<br /><br /> 1 = Shut down server instance<br /><br /> 2 = Fail operation|  
|on_failure_desc|**nvarchar(60)**|On Failure to write an action entry:<br /><br /> CONTINUE<br /><br /> SHUTDOWN SERVER INSTANCE<br /><br /> FAIL OPERATION|  
|is_state_enabled|**tinyint**|0 = Disabled<br /><br /> 1 = Enabled|  
|queue_delay|**int**|Suggested maximum time, in milliseconds, to wait before writing to disk. If 0, the audit will guarantee a write before the event can continue.|  
|predicate|**nvarchar(8000)**|Predicate expression that is applied to the event.|  
|max_file_size|**bigint**|Maximum size, in megabytes, of the audit:<br /><br /> 0 = Unlimited/Not applicable to the type of audit selected.|  
|max_rollover_files|**int**|Maximum number of files to use with the rollover option.|  
|max_files|**int**|Maximum number of files to use without the rollover option.|  
|reserved_disk_space|**int**|Amount of disk space to reserve per file.|  
|log_file_path|**nvarchar(260)**|Path to where audit is located. File path for file audit, application log path for application log audit.|  
|log_file_name|**nvarchar(260)**|Base name for the log file supplied in the CREATE AUDIT DDL. An incremental number is added to the base_log_name file as a suffix to create the log file name.|  
  
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
 [sys.server_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)   
 [sys.server_file_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)   
 [sys.server_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)   
 [sys.database_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)   
 [sys.database_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)   
 [sys.dm_server_audit_status &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)   
 [sys.dm_audit_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)   
 [sys.dm_audit_class_type_map &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql.md)   
 [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)  
  
  
