---
title: "sys.fn_get_audit_file (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/20/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "fn_get_audit_file_TSQL"
  - "sys.fn_get_audit_file_TSQL"
  - "fn_get_audit_file"
  - "sys.fn_get_audit_file"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.fn_get_audit_file function"
  - "fn_get_audit_file function"
ms.assetid: d6a78d14-bb1f-4987-b7b6-579ddd4167f5
caps.latest.revision: 27
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.fn_get_audit_file (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns information from an audit file created by a server audit in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
fn_get_audit_file ( file_pattern,   
    { default | initial_file_name | NULL },   
    { default | audit_record_offset | NULL } )  
```  
  
## Arguments  
 *file_pattern*  
 Specifies the directory or path and file name for the audit file set to be read. Type is **nvarchar(260)**. This argument must include both a path (drive letter or network share) and a file name that can include a wildcard. A single asterisk (*) can be used to collect multiple files from an audit file set. For example:  
  
-   **\<path>\\\*** - Collect all audit files in the specified location.  
  
-   **\<path>\LoginsAudit_{GUID}** - Collect all audit files that have the specified name and GUID pair.  
  
-   **\<path>\LoginsAudit_{GUID}_00_29384.sqlaudit** - Collect a specific audit file.  
  
> [!NOTE]  
>  Passing a path without a file name pattern will generate an error.  
  
 *initial_file_name*  
 Specifies the path and name of a specific file in the audit file set to start reading audit records from. Type is **nvarchar(260)**.  
  
> [!NOTE]  
>  The *initial_file_name* argument must contain valid entries or must contain either the default | NULL value.  
  
 *audit_record_offset*  
 Specifies a known location with the file specified for the initial_file_name. When this argument is used the function will start reading at the first record of the Buffer immediately following the specified offset.  
  
> [!NOTE]  
>  The *audit_record_offset* argument must contain valid entries or must contain either the default | NULL value. Type is **bitint**.  
  
## Tables Returned  
 The following table describes the audit file content that can be returned by this function.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|event_time|**datetime2**|Date and time when the auditable action is fired. Is not nullable.|  
|sequence_number|**int**|Tracks the sequence of records within a single audit record that was too large to fit in the write buffer for audits. Is not nullable.|  
|action_id|**varchar(4)**|ID of the action. Is not nullable.|  
|succeeded|**bit**|Indicates whether the action that triggered the event succeeded. Is not nullable. For all events other than login events, this only reports whether the permission check succeeded or failed, not the operation.<br /> 1 = success<br /> 0 = fail|  
|permission_bitmask|**varbinary(16)**|In some actions, this is the permissions that were grant, denied, or revoked.|  
|is_column_permission|**bit**|Flag indicating if this is a column level permission. Is not nullable. Returns 0 when the permission_bitmask = 0.<br /> 1 = true<br /> 0 = false|  
|session_id|**smallint**|ID of the session on which the event occurred. Is not nullable.|  
|server_principal_id|**int**|ID of the login context that the action is performed in. Is not nullable.|  
|database_principal_id|**int**|ID of the database user context that the action is performed in. Is not nullable. Returns 0 if this does not apply. For example, a server operation.|  
|target_server_principal_id|**int**|Server principal that the GRANT/DENY/REVOKE operation is performed on. Is not nullable. Returns 0 if not applicable.|  
|target_database_principal_id|**int**|The database principal the GRANT/DENY/REVOKE operation is performed on. Is not nullable. Returns 0 if not applicable.|  
|object_id|**int**|The ID of the entity on which the audit occurred. This includes the following:<br /> Server objects<br /> Databases<br /> Database objects<br /> Schema objects<br /> Is not nullable. Returns 0 if the entity is the Server itself or if the audit is not performed at an object level. For example, Authentication.|  
|class_type|**varchar(2)**|The type of auditable entity that the audit occurs on. Is not nullable.|  
|session_server_principal_name|**sysname**|Server principal for session. Is nullable.|  
|server_principal_name|**sysname**|Current login. Is nullable.|  
|server_principal_sid|**varbinary**|Current login SID. Is nullable.|  
|database_principal_name|**sysname**|Current user. Is nullable. Returns NULL if not available.|  
|target_server_principal_name|**sysname**|Target login of action. Is nullable. Returns NULL if not applicable.|  
|target_server_principal_sid|**varbinary**|SID of target login. Is nullable. Returns NULL if not applicable.|  
|target_database_principal_name|**sysname**|Target user of action. Is nullable. Returns NULL if not applicable.|  
|server_instance_name|**sysname**|Name of the server instance where the audit occurred. The standard server\instance format is used.|  
|database_name|**sysname**|The database context in which the action occurred. Is nullable. Returns NULL for audits occuring at the server level.|  
|schema_name|**sysname**|The schema context in which the action occurred. Is nullable. Returns NULL for audits occuring outside a schema.|  
|object_name|**sysname**|The name of the entity on which the audit occurred. This includes the following:<br /> Server objects<br /> Databases<br /> Database objects<br /> Schema objects<br /> Is nullable. Returns NULL if the entity is the Server itself or if the audit is not performed at an object level. For example, Authentication.|  
|statement|**nvarchar(4000)**|TSQL statement if it exists. Is nullable. Returns NULL if not applicable.|  
|additional_information|**nvarchar(4000)**|Unique information that only applies to a single event is returned as XML. A small number of auditable actions contain this kind of information.<br /><br /> One level of TSQL stack will be displayed in XML format for actions that have TSQL stack associated with them. The XML format will be:<br /><br /> `<tsql_stack><frame nest_level = '%u' database_name = '%.*s' schema_name = '%.*s' object_name = '%.*s' /></tsql_stack>`<br /><br /> Frame nest_level indicates the current nesting level of the frame. The Module name is represented in three part format (database_name, schema_name and object_name).  The module name will be parsed to escape invalid xml characters like `'\<'`, `'>'`, `'/'`, `'_x'`. They will be escaped as `_xHHHH\_`. The HHHH stands for the four-digit hexadecimal UCS-2 code for the character<br /><br /> Is nullable. Returns NULL when there is no additional information reported by the event.|  
|file_name|**varchar(260)**|The path and name of the audit log file that the record came from. Is not nullable.|  
|audit_file_offset|**bigint**|The buffer offset in the file that contains the audit record. Is not nullable.|  
|user_defined_event_id|**smallint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> User defined event id passed as an argument to **sp_audit_write**. **NULL** for system events (default) and non-zero for user-defined event. For more information, see [sp_audit_write &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-audit-write-transact-sql.md).|  
|user_defined_information|**nvarchar(4000)**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Used to record any extra information the user wants to record in |audit log by using the **sp_audit_write** stored procedure.|  
|audit_schema_version |**int** | |  
|sequence_group_id |**nvarbinary** | |  
|transaction_id |**bigint** | |  
|client_ip |**nvarchar(128)** | |  
|application_name |**nvarchar(128)** | |  
|duration_milliseconds |**bigint** | |  
|response_rows |**bigint** | |  
|affected_rows |**bigint** | |  
  
## Remarks  
 If the *file_pattern* argument passed to **fn_get_audit_file** references a path or file that does not exist, or if the file is not an audit file, the **MSG_INVALID_AUDIT_FILE** error message is returned.  
  
## Permissions  
 Requires the **CONTROL SERVER** permission.  
  
## Examples  
 This example reads from a file that is named `\\serverName\Audit\HIPPA_AUDIT.sqlaudit`.  
  
```  
SELECT * FROM sys.fn_get_audit_file ('\\serverName\Audit\HIPPA_AUDIT.sqlaudit',default,default);  
GO  
```  
  
 For a full example about how to create an audit, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
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
 [sys.server_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)   
 [sys.server_file_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)   
 [sys.server_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)   
 [sys.server_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)   
 [sys.database_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)   
 [sys.database_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)   
 [sys.dm_server_audit_status &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)   
 [sys.dm_audit_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)   
 [sys.dm_audit_class_type_map &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql.md)   
 [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)  
  
  
