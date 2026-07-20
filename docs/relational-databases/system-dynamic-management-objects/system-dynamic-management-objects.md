---
title: System Dynamic Management Views and Functions (Transact-SQL)
description: Dynamic management views and functions return server state information that can be used to monitor the health of a server instance, diagnose problems, and tune performance.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/07/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "database scoped dynamic management objects [SQL Server]"
  - "DMVs [SQL Server]"
  - "dynamic management functions [SQL Server]"
  - "dynamic management functions [SQL Server], about dynamic management functions"
  - "dynamic management objects [SQL Server], about dynamic management objects"
  - "dynamic management views [SQL Server], about dynamic management views"
  - "dynamic management objects [SQL Server]"
  - "dynamic management views [SQL Server]"
  - "functions [SQL Server], dynamic management"
  - "views [SQL Server], dynamic management"
  - "metadata [SQL Server], dynamic management functions"
  - "metadata [SQL Server], dynamic management objects"
  - "metadata [SQL Server], dynamic management views"
  - "server scoped dynamic management functions [SQL Server]"
  - "server scoped dynamic management objects [SQL Server]"
  - "server scoped dynamic management views [SQL Server]"
  - "server state information [SQL Server]"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# System dynamic management views and functions

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Dynamic management objects include *dynamic management views* (DMVs) and *dynamic management functions* (DMFs). These objects return server state information that you can use to monitor the health of a server instance, diagnose problems, and tune performance.

Dynamic management objects are either *server-scoped* or *database-scoped*.

## Query dynamic management objects

All dynamic management objects exist in the `sys` schema and follow the naming convention `dm_*`.

When you use a dynamic management object, you must prefix the name of the object by using the `sys` schema. For example, to query the `dm_os_wait_stats` dynamic management view, run the following query:

```sql
SELECT wait_type,
       wait_time_ms
FROM sys.dm_os_wait_stats;
```

The following table describes how to reference dynamic management objects in [!INCLUDE [tsql](../../includes/tsql-md.md)]:

| T-SQL reference | Dynamic management view | Dynamic management function |
| ---: | :---: | :---: |
| One-part naming | No | No |
| Two-part naming | Yes | Yes |
| Three-part naming | Yes | Yes |
| Four-part naming | Yes | No |

## Remarks

Dynamic management objects return internal, implementation-specific state data. Their schemas and the data they return might change in future [!INCLUDE [ssde-md](../../includes/ssde-md.md)] releases. Therefore, dynamic management objects in future releases might not be compatible with the dynamic management objects in this release.

For example, in future [!INCLUDE [ssde-md](../../includes/ssde-md.md)] releases, Microsoft might augment the definition of any dynamic management view by adding columns to the end of the column list. Don't use the syntax `SELECT * FROM dynamic_management_view_name` in production code because the number of columns returned might change and break your application.

<a id="required-permissions"></a>

## Permissions

Querying a dynamic management object requires `SELECT` permission on the object, and depends on the object's scope and the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] version:

| Version | Server scope | Database scope |
| --- | --- | --- |
| [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions | `VIEW SERVER STATE` | `VIEW DATABASE STATE` |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions | `VIEW SERVER PERFORMANCE STATE`, or `VIEW SERVER SECURITY STATE` for security-related objects | `VIEW DATABASE PERFORMANCE STATE`, or `VIEW DATABASE SECURITY STATE` for security-related objects |

To selectively restrict access, create the user or login in `master` and then deny that user `SELECT` permission on the specific dynamic management views or functions you want to block. The user can't select from those objects afterward, regardless of database context.

> [!NOTE]  
> `DENY` takes precedence over `GRANT`. For example, a user granted `VIEW SERVER PERFORMANCE STATE` but denied `VIEW DATABASE PERFORMANCE STATE` can see server-level information but not database-level information.

## Related content

- [GRANT Server Permissions (Transact-SQL)](../../t-sql/statements/grant-server-permissions-transact-sql.md)
- [GRANT Database Permissions (Transact-SQL)](../../t-sql/statements/grant-database-permissions-transact-sql.md)
- [Transact-SQL reference (Database Engine)](../../t-sql/language-reference.md)
