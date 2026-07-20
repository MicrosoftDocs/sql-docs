---
title: "sys.sql_logins (Transact-SQL)"
description: sys.sql_logins (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 04/29/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sql_logins_TSQL"
  - "sql_logins_TSQL"
  - "sys.sql_logins"
  - "sql_logins"
helpviewer_keywords:
  - "sys.sql_logins catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
ms.custom:
  - build-2025
---
# sys.sql_logins (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

Returns one row for every [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication login.

| Column name | Data type | Description |
| --- | --- | --- |
| `<inherited columns>` | N/A | Inherits from `sys.server_principals`. |
| `is_policy_checked` | **bit** | Password policy is checked. |
| `is_expiration_checked` | **bit** | Password expiration is checked. |
| `password_hash` | **varbinary(256)** | Hash of SQL login password. In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions, the stored password information is calculated using SHA-512 of the salted password. Starting with [!INCLUDE [ssSQL25](../../includes/sssql25-md.md)], an iterated hash algorithm, RFC2898 (PBKDF), is used. The first byte of the hash indicates the version: `0x02` for version 2 ([!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions) and `0x03` for version 3 ([!INCLUDE [ssSQL25](../../includes/sssql25-md.md)] and later versions). |

For a list of columns that this view inherits, see [sys.server_principals](sys-server-principals-transact-sql.md). The columns `owning_principal_id` and `is_fixed_role` isn't inherited from sys.server_principals.

## Remarks

To view both [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication logins and Windows authentication logins, see [sys.server_principals](sys-server-principals-transact-sql.md).

When contained database users are enabled, connections can be made without logins. To identify those accounts, see [sys.database_principals](sys-database-principals-transact-sql.md).

## Permissions

In SQL Server, any [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication login can see their own login name, and the `sa` login. To see other logins, the principal requires `ALTER ANY LOGIN`, `VIEW SERVER SECURITY DEFINITION`, or a permission on the login.

To view the contents of the `password_hash column`, `CONTROL SERVER` is required. Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], `VIEW ANY CRYPTOGRAPHICALLY SECURED DEFINITION` permission is required.

In Azure SQL Database, only members of the special database role **loginmanager** in `master` or the Microsoft Entra Admin and Server Admin can see all logins.

[!INCLUDE [sscatviewperm-md](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Related content

- [System catalog views](catalog-views-transact-sql.md)
- [Security Catalog Views](security-catalog-views-transact-sql.md)
- [Password Policy](../security/password-policy.md)
- [Principals (Database Engine)](../security/authentication-access/principals-database-engine.md)
