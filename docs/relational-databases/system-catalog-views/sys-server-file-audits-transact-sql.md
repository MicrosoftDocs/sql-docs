---
title: "sys.server_file_audits (Transact-SQL)"
description: sys.server_file_audits contains extended information about the file audit type in a SQL Server audit on a server instance.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: randolphwest
ms.date: 04/23/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "server_file_audits_TSQL"
  - "sys.server_file_audits_TSQL"
  - "sys.server_file_audits"
  - "server_file_audits"
helpviewer_keywords:
  - "sys.server_file_audits catalog view"
dev_langs:
  - TSQL
---
# sys.server_file_audits (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Contains extended information about the file audit type in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] audit on a server instance. For more information, see [SQL Server Audit (Database Engine)](../security/auditing/sql-server-audit-database-engine.md).

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `audit_id` | **int** | No | ID of the audit. |
| `name` | **sysname** | No | Name of the audit. |
| `audit_guid` | **uniqueidentifier** | Yes | GUID of the audit. |
| `create_date` | **datetime** | No | UTC date when the file audit was created. |
| `modify_date` | **datetime** | No | UTC date when the file audit was last modified. |
| `principal_id` | **int** | Yes | ID of the owner of the audit as registered on the server. |
| `type` | **char(2)** | No | Audit type:<br /><br />- `SL` = Windows Security event log<br />- `AL` = Windows Application event log<br />- `FL` = File on file system |
| `type_desc` | **nvarchar(60)** | Yes | Audit type description. |
| `on_failure` | **tinyint** | Yes | On failure condition:<br /><br />- `0` = Continue<br />- `1` = Shut down server instance<br />- `2` = Fail operation |
| `on_failure_desc` | **nvarchar(60)** | Yes | On failure to write an action entry:<br /><br />- `CONTINUE`<br />- `SHUTDOWN SERVER INSTANCE`<br />- `FAIL OPERATION` |
| `is_state_enabled` | **bit** | Yes | - `0` = Disabled<br />- `1` = Enabled |
| `queue_delay` | **int** | Yes | Suggested maximum time, in milliseconds, to wait before writing to disk. If `0`, the audit guarantees a write before the event can continue. |
| `predicate` | **nvarchar(3000)** | Yes | Predicate expression that is applied to the event. |
| `max_file_size` | **bigint** | Yes | Maximum size, in megabytes, of the audit:<br /><br />- `0` = Unlimited/Not applicable to the type of audit selected. |
| `max_rollover_files` | **int** | Yes | Maximum number of files to use with the rollover option. |
| `max_files` | **int** | Yes | Maximum number of files to use without the rollover option. |
| `reserve_disk_space` | **bit** | Yes | Amount of disk space to reserve per file. |
| `log_file_path` | **nvarchar(260)** | Yes | Path to where audit is located. File path for file audit, application log path for application log audit. |
| `log_file_name` | **nvarchar(260)** | Yes | Base name for the log file supplied in the `CREATE AUDIT DDL`. An incremental number is added to the base_log_name file as a suffix to create the log file name. |
| `retention_days` | **int** | Yes | Lifetime in days of the audit log file.<br /><br />- `0` = Unlimited.<br /><br />**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. |

## Permissions

Principals with the `ALTER ANY SERVER AUDIT` or `VIEW ANY DEFINITION` permission can access this catalog view. In addition, the principal can't be denied `VIEW ANY DEFINITION` permission.

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata visibility configuration](../security/metadata-visibility-configuration.md).

## Transact-SQL reference

- [CREATE SERVER AUDIT](../../t-sql/statements/create-server-audit-transact-sql.md)
- [ALTER SERVER AUDIT](../../t-sql/statements/alter-server-audit-transact-sql.md)
- [DROP SERVER AUDIT](../../t-sql/statements/drop-server-audit-transact-sql.md)
- [CREATE SERVER AUDIT SPECIFICATION](../../t-sql/statements/create-server-audit-specification-transact-sql.md)
- [ALTER SERVER AUDIT SPECIFICATION](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)
- [DROP SERVER AUDIT SPECIFICATION](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)
- [CREATE DATABASE AUDIT SPECIFICATION](../../t-sql/statements/create-database-audit-specification-transact-sql.md)
- [ALTER DATABASE AUDIT SPECIFICATION](../../t-sql/statements/alter-database-audit-specification-transact-sql.md)
- [DROP DATABASE AUDIT SPECIFICATION](../../t-sql/statements/drop-database-audit-specification-transact-sql.md)
- [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md)

## Related content

- [Create a Server Audit and Server Audit Specification](../security/auditing/create-a-server-audit-and-server-audit-specification.md)
- [sys.fn_get_audit_file (Transact-SQL)](../system-functions/sys-fn-get-audit-file-transact-sql.md)
- [sys.server_audits (Transact-SQL)](sys-server-audits-transact-sql.md)
- [sys.server_file_audits (Transact-SQL)](sys-server-file-audits-transact-sql.md)
- [sys.server_audit_specifications (Transact-SQL)](sys-server-audit-specifications-transact-sql.md)
- [sys.database_audit_specifications (Transact-SQL)](sys-database-audit-specifications-transact-sql.md)
- [sys.database_audit_specification_details (Transact-SQL)](sys-database-audit-specification-details-transact-sql.md)
- [sys.dm_server_audit_status (Transact-SQL)](../system-dynamic-management-objects/sys-dm-server-audit-status-transact-sql.md)
- [sys.dm_audit_actions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-audit-actions-transact-sql.md)
- [sys.dm_audit_class_type_map (Transact-SQL)](../system-dynamic-management-objects/sys-dm-audit-class-type-map-transact-sql.md)
