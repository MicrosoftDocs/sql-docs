---
title: "SQL Server Audit Records | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "audit records [SQL Server]"
ms.assetid: 7a291015-df15-44fe-8d53-c6d90a157118
author: VanMSFT
ms.author: vanto
manager: craigg
---
# SQL Server Audit Records
  The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Audit feature enables you to audit server-level and database-level groups of events and events. For more information, see [SQL Server Audit &#40;Database Engine&#41;](sql-server-audit-database-engine.md). [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 Audits consist of zero or more audit action items, which are recorded to an audit *target*. The audit target can be a binary file, the Windows Application event log or the Windows Security event log. The records sent to the target can contain the elements described in the following table.  
  
|Column name|Description|Type|Always available|  
|-----------------|-----------------|----------|----------------------|  
|**event_time**|Date/time when the auditable action is fired.|`datetime2`|Yes|  
|**sequence_no**|Tracks the sequence of records within a single audit record that was too large to fit in the write buffer for audits.|`int`|Yes|  
|**action_id**|ID of the action<br /><br /> Tip: To use **action_id** as a predicate it must be converted from a character string to a numeric value. For more information, see [Filter SQL Server Audit on action_id / class_type predicate](https://blogs.msdn.com/b/sqlsecurity/archive/2012/10/03/filter-sql-server-audit-on-action-id-class-type-predicate.aspx).|`varchar(4)`|Yes|  
|**succeeded**|Indicates if the action that triggered the event succeeded|`bit` - 1 = Success, 0 = Fail|Yes|  
|**permission_bitmask**|When applicable, shows the permissions that were granted, denied, or revoked|`bigint`|No|  
|**is_column_permission**|Flag indicating a column level permission|`bit` - 1 = True, 0 = False|No|  
|**session_id**|ID of the session on which the event occurred.|`int`|Yes|  
|**server_principal_id**|ID of the login context that the action is performed in.|`int`|Yes|  
|**database_principal_id**|ID of the database user context that the action is performed in.|`int`|No|  
|**object_ id**|The primary ID of the entity on which the audit occurred. This includes:<br /><br /> server objects<br /><br /> databases<br /><br /> database objects<br /><br /> schema objects|`int`|No|  
|**target_server_principal_id**|Server principal that the auditable action applies to.|`int`|Yes|  
|**target_database_principal_id**|Database principal that the auditable action applies to.|`int`|No|  
|**class_type**|Type of auditable entity that the audit occurs on.|`varchar(2)`|Yes|  
|**session_server_principal_name**|Server principal for the session.|`sysname`|Yes|  
|**server_principal_name**|Current login.|`sysname`|Yes|  
|**server_principal_sid**|Current login SID.|`varbinary`|Yes|  
|**database_principal_name**|Current user.|`sysname`|No|  
|**target_server_principal_name**|Target login of the action.|`sysname`|No|  
|**target_server_principal_sid**|SID of the target login.|`varbinary`|No|  
|**target_database_principal_name**|Target user of the action.|`sysname`|No|  
|**server_instance_name**|Name of the server instance where the audit occurred. Uses the standard machine\instance format.|`nvarchar(120)`|Yes|  
|**database_name**|The database context in which the action occurred.|`sysname`|No|  
|**schema_name**|The schema context in which the action occurred.|`sysname`|No|  
|**object_name**|The name of the entity on which the audit occurred. This includes:<br /><br /> server objects<br /><br /> databases<br /><br /> database objects<br /><br /> schema objects<br /><br /> TSQL statement (if any)|`sysname`|No|  
|**statement**|TSQL statement (if any)|`nvarchar(4000)`|No|  
|**additional_information**|Any additional information about the event, stored as XML.|`nvarchar(4000)`|No|  
  
## Remarks  
 Some actions do not populate a column's value because it might be non-applicable to the action.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Audit stores 4000 characters of data for character fields in an audit record. When the **additional_information** and **statement** values returned from an auditable action return more than 4000 characters, the **sequence_no** column is used to write multiple records into the audit report for a single audit action to record this data. The process is as follows:  
  
-   The **statement** column is divided into 4000 characters.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Audit writes as the first row for the audit record with the partial data. All the other fields are duplicated in each row.  
  
-   The **sequence_no** value is incremented.  
  
-   This process is repeated until all the data is recorded.  
  
 You can connect the data by reading the rows sequentially using the **sequence_no** value, and the **event_Time**, **action_id** and **session_id** columns to identify the action.  
  
## Related Content  
 [CREATE SERVER AUDIT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-server-audit-transact-sql)  
  
 [ALTER SERVER AUDIT  &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-server-audit-specification-transact-sql)  
  
 [DROP SERVER AUDIT  &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-server-audit-transact-sql)  
  
 [CREATE SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-server-audit-specification-transact-sql)  
  
 [ALTER SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-server-audit-transact-sql)  
  
 [DROP SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-server-audit-specification-transact-sql)  
  
 [CREATE DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-database-audit-specification-transact-sql)  
  
 [ALTER DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-audit-specification-transact-sql)  
  
 [DROP DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-database-encryption-key-transact-sql)  
  
 [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-authorization-transact-sql)  
  
 [sys.fn_get_audit_file &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/sys-fn-get-audit-file-transact-sql)  
  
 [sys.server_audits &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-audits-transact-sql)  
  
 [sys.server_file_audits &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-file-audits-transact-sql)  
  
 [sys.server_audit_specifications &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql)  
  
 [sys.server_audit_specification_details &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql)  
  
 [sys.database_audit_specifications &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql)  
  
 [sys.database_audit_specification_details &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql)  
  
 [sys.dm_server_audit_status &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql)  
  
 [sys.dm_audit_actions &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql)  
  
 [sys.dm_audit_class_type_map &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql)  
  
  
