---
title: "sys.dm_server_audit_status (Transact-SQL)"
description: sys.dm_server_audit_status (Transact-SQL)
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: randolphwest
ms.date: 03/09/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_server_audit_status_TSQL"
  - "sys.dm_server_audit_status"
  - "dm_server_audit_status"
  - "sys.dm_server_audit_status_TSQL"
helpviewer_keywords:
  - "sys.dm_server_audit_status dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_server_audit_status (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Returns a row for each server audit indicating the current state of the audit. For more information, see [SQL Server Audit (Database Engine)](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).

| Column name | Data type | Description |
| --- | --- | --- |
| **audit_id** | **int** | ID of the audit. Maps to the `audit_id` column in the `sys.audits` catalog view. |
| **name** | **sysname** | Name of the audit. Same as the `name` column in the `sys.server_audits` catalog view. |
| **status** | **smallint** | Numeric status of the server audit:<br /><br />0 = Not Started<br />1 = Started<br />2 = Runtime Fail<br />3 = Target Create Fail<br />4 = Shutting Down |
| **status_desc** | **nvarchar(256)** | String that shows the status of the server audit:<br /><br />- NOT_STARTED<br />- STARTED<br />- RUNTIME_FAIL<br />- TARGET_CREATION_FAILED<br />- SHUTTING_DOWN |
| **status_time** | **datetime2** | Timestamp in UTC of the last status change for the audit. |
| **event_session_address** | **varbinary(8)** | Address of the Extended Events session associated with the audit. Related to the `address` column in the `sys.dm_xe_sessions` catalog view. |
| **audit_file_path** | **nvarchar(256)** | Full path and file name of the audit file target that is currently being used. Only populated for file audits. |
| **audit_file_size** | **bigint** | Approximate size of the audit file, in bytes. Only populated for file audits. |

## Permissions

Principals must have the **VIEW SERVER SECURITY STATE** permission.

[!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).

## See also

- [CREATE SERVER AUDIT (Transact-SQL)](../../t-sql/statements/create-server-audit-transact-sql.md)
- [ALTER SERVER AUDIT  (Transact-SQL)](../../t-sql/statements/alter-server-audit-transact-sql.md)
- [DROP SERVER AUDIT  (Transact-SQL)](../../t-sql/statements/drop-server-audit-transact-sql.md)
- [CREATE SERVER AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/create-server-audit-specification-transact-sql.md)
- [ALTER SERVER AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)
- [DROP SERVER AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)
- [CREATE DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/create-database-audit-specification-transact-sql.md)
- [ALTER DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/alter-database-audit-specification-transact-sql.md)
- [DROP DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/drop-database-audit-specification-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](../../t-sql/statements/alter-authorization-transact-sql.md)

## Next steps

- [sys.fn_get_audit_file (Transact-SQL)](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)
- [sys.server_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)
- [sys.server_file_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)
- [sys.server_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)
- [sys.server_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)
- [sys.database_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)
- [sys.database_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)
- [sys.dm_server_audit_status](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)
- [sys.dm_audit_actions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)
- [sys.dm_audit_class_type_map (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql.md)
- [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)
