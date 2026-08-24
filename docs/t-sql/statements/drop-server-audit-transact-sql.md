---
title: "DROP SERVER AUDIT (Transact-SQL)"
description: DROP SERVER AUDIT drops a server audit object using the SQL Server Audit feature.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: randolphwest
ms.date: 11/25/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP SERVER AUDIT"
  - "DROP_SERVER_AUDIT_TSQL"
helpviewer_keywords:
  - "DROP SERVER AUDIT statement"
dev_langs:
  - "TSQL"
---
# DROP SERVER AUDIT (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

Drops a server audit object using the SQL Server Audit feature. For more information on SQL Server Audit, see [SQL Server Audit (Database Engine)](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DROP SERVER AUDIT audit_name
[ ; ]
```

## Remarks

You must set the State of an audit to the `OFF` option in order to make any changes to an Audit. If you run `DROP AUDIT` while an audit is enabled with any options other than `STATE = OFF`, you receive a `MSG_NEED_AUDIT_DISABLED` error message.

A `DROP SERVER AUDIT` removes the metadata for the Audit, but not the audit data that was collected before the command was issued.

`DROP SERVER AUDIT` doesn't drop associated server or database audit specifications. These specifications must be dropped manually or left orphaned and later mapped to a new server audit.

## Permissions

To create, alter, or drop a Server Audit Principal, you need `ALTER ANY SERVER AUDIT` or `CONTROL SERVER` permissions.

## Examples

The following example drops an audit called `HIPAA_Audit`.

```sql
ALTER SERVER AUDIT HIPAA_Audit
WITH (STATE = OFF);
GO

DROP SERVER AUDIT HIPAA_Audit;
GO
```

## Transact-SQL reference

- [CREATE SERVER AUDIT (Transact-SQL)](create-server-audit-transact-sql.md)
- [ALTER SERVER AUDIT (Transact-SQL)](alter-server-audit-transact-sql.md)
- [CREATE SERVER AUDIT SPECIFICATION (Transact-SQL)](create-server-audit-specification-transact-sql.md)
- [ALTER SERVER AUDIT SPECIFICATION (Transact-SQL)](alter-server-audit-specification-transact-sql.md)
- [DROP SERVER AUDIT SPECIFICATION (Transact-SQL)](drop-server-audit-specification-transact-sql.md)
- [CREATE DATABASE AUDIT SPECIFICATION (Transact-SQL)](create-database-audit-specification-transact-sql.md)
- [ALTER DATABASE AUDIT SPECIFICATION (Transact-SQL)](alter-database-audit-specification-transact-sql.md)
- [DROP DATABASE AUDIT SPECIFICATION (Transact-SQL)](drop-database-audit-specification-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](alter-authorization-transact-sql.md)

## Related content

- [sys.fn_get_audit_file (Transact-SQL)](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)
- [sys.server_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)
- [sys.server_file_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)
- [sys.server_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)
- [sys.server_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)
- [sys.database_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)
- [sys.database_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)
- [sys.dm_server_audit_status (Transact-SQL)](../../relational-databases/system-dynamic-management-objects/sys-dm-server-audit-status-transact-sql.md)
- [sys.dm_audit_actions (Transact-SQL)](../../relational-databases/system-dynamic-management-objects/sys-dm-audit-actions-transact-sql.md)
- [sys.dm_audit_class_type_map (Transact-SQL)](../../relational-databases/system-dynamic-management-objects/sys-dm-audit-class-type-map-transact-sql.md)
- [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)
