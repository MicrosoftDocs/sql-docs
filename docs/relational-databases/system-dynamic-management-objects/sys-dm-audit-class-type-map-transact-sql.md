---
title: "sys.dm_audit_class_type_map (Transact-SQL)"
description: sys.dm_audit_class_type_map returns a table that lists securable classes that can be mapped to the class_type column in the audit log.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: randolphwest
ms.date: 12/16/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.dm_audit_class_type_map_TSQL"
  - "sys.dm_audit_class_type_map"
  - "dm_audit_class_type_map_TSQL"
  - "dm_audit_class_type_map"
helpviewer_keywords:
  - "sys.dm_audit_class_type_map dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || =azuresqldb-mi-current"
---
# sys.dm_audit_class_type_map (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a table that lists securable classes that can be mapped to the `class_type` column in the audit log. For more information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Audit, see [SQL Server Audit (Database Engine)](../security/auditing/sql-server-audit-database-engine.md).

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `class_type` | **varchar(2)** | No | The class type of the entity that was audited. Maps to the `class_type` written to the audit log returned by the `get_audit_file()` function. |
| `class_type_desc` | **nvarchar(35)** | No | The name of the class of the object that was audited. |
| `securable_class_desc` | **nvarchar(35)** | Yes | The securable class that maps to the `class_type` being audited. Can be `NULL` if the `class_type` doesn't map to a securable object. Can be joined with `class_desc` in `sys.dm_audit_actions.` |

## Permissions

This view is visible to the public.

To use the `sys.fn_get_audit_file` function, [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions require `CONTROL SERVER` permission on the server, while [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER SECURITY AUDIT` permission on the server.

## Examples

This [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] example reads a locally stored Audit file and joins it with the `sys.dm_audit_class_type_map` view.

```sql
SELECT *
FROM sys.fn_get_audit_file('D:\SQLData\Audits\*.sqlaudit', DEFAULT, DEFAULT) AS audit_file
     INNER JOIN sys.dm_audit_class_type_map AS dm_audit_class_type_map
         ON audit_file.class_type = dm_audit_class_type_map.class_type;
GO
```

## Transact-SQL reference

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
- [sys.dm_audit_class_type_map](sys-dm-audit-class-type-map-transact-sql.md)
- [Create a Server Audit and Server Audit Specification](../security/auditing/create-a-server-audit-and-server-audit-specification.md)
