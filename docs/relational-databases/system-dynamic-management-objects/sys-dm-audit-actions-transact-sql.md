---
title: "sys.dm_audit_actions (Transact-SQL)"
description: sys.dm_audit_actions returns a row for every audit action that can be reported in the audit log.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: randolphwest
ms.date: 12/16/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.dm_audit_actions_TSQL"
  - "sys.dm_audit_actions"
  - "dm_audit_actions_TSQL"
  - "dm_audit_actions"
helpviewer_keywords:
  - "sys.dm_audit_actions dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || =azuresqldb-mi-current"
---
# sys.dm_audit_actions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a row for every audit action that can be reported in the audit log and every audit action group that can be configured as part of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Audit. For more information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Audit, see [SQL Server Audit (Database Engine)](../security/auditing/sql-server-audit-database-engine.md).

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `action_id` | **varchar(4)** | Yes | ID of the audit action. Related to the `action_id` value written to each audit record. Can be `NULL` for audit groups. |
| `name` | **nvarchar(128)** | No | Name of the audit action or action group. |
| `class_desc` | **nvarchar(35)** | No | The name of the class of the object that the audit action applies to. Can be any one of the Server, Database, or Schema scope objects, but doesn't include Schema objects. |
| `covering_action_name` | **nvarchar(128)** | Yes | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `parent_class_desc` | **nvarchar(35)** | Yes | Name of the parent class for the object described by `class_desc`. Can be `NULL` if the `class_desc` is `Server`. |
| `covering_parent_action_name` | **nvarchar(128)** | Yes | Name of the audit action or audit group that contains the audit action described in this row. This value is used to create a hierarchy of actions and covering actions. |
| `configuration_level` | **nvarchar(128)** | Yes | Indicates that the action or action group specified in this row is configurable at the Group or Action level. Can be `NULL` if the action isn't configurable. |
| `containing_group_name` | **nvarchar(120)** | Yes | The name of the audit group that contains the specified action. Can be `NULL` if the value in `name` is a group. |
| `action_in_log` | **bit** | No | Indicates whether an action can be written to an audit log. Possible values:<br /><br />`1` = Yes<br />`0` = No |

## Permissions

This view is visible to the public.

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata visibility configuration](../security/metadata-visibility-configuration.md).

## Related tasks

- [CREATE SERVER AUDIT (Transact-SQL)](../../t-sql/statements/create-server-audit-transact-sql.md)
- [ALTER SERVER AUDIT (Transact-SQL)](../../t-sql/statements/alter-server-audit-transact-sql.md)
- [DROP SERVER AUDIT (Transact-SQL)](../../t-sql/statements/drop-server-audit-transact-sql.md)
- [CREATE SERVER AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/create-server-audit-specification-transact-sql.md)
- [ALTER SERVER AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)
- [DROP SERVER AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)
- [CREATE DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/create-database-audit-specification-transact-sql.md)
- [ALTER DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/alter-database-audit-specification-transact-sql.md)
- [DROP DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/drop-database-audit-specification-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](../../t-sql/statements/alter-authorization-transact-sql.md)

## Related content

- [sys.fn_get_audit_file (Transact-SQL)](../system-functions/sys-fn-get-audit-file-transact-sql.md)
- [sys.server_audits (Transact-SQL)](../system-catalog-views/sys-server-audits-transact-sql.md)
- [sys.server_file_audits (Transact-SQL)](../system-catalog-views/sys-server-file-audits-transact-sql.md)
- [sys.server_audit_specifications (Transact-SQL)](../system-catalog-views/sys-server-audit-specifications-transact-sql.md)
- [sys.server_audit_specification_details (Transact-SQL)](../system-catalog-views/sys-server-audit-specification-details-transact-sql.md)
- [sys.database_audit_specifications (Transact-SQL)](../system-catalog-views/sys-database-audit-specifications-transact-sql.md)
- [sys.database_audit_specification_details (Transact-SQL)](../system-catalog-views/sys-database-audit-specification-details-transact-sql.md)
- [sys.dm_server_audit_status (Transact-SQL)](sys-dm-server-audit-status-transact-sql.md)
- [sys.dm_audit_class_type_map (Transact-SQL)](sys-dm-audit-class-type-map-transact-sql.md)
- [Create a Server Audit and Server Audit Specification](../security/auditing/create-a-server-audit-and-server-audit-specification.md)
