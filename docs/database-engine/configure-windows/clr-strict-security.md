---
title: "Server configuration: clr strict security"
description: Learn to configure common language runtime (CLR) strict security in SQL Server. Control the interpretation of the SAFE, EXTERNAL ACCESS, and UNSAFE permissions.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/02/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
f1_keywords:
  - "clr strict security"
  - "clr_strict_security_TSQL"
  - "strict_security_TSQL"
helpviewer_keywords:
  - "assemblies [CLR integration], strict security"
  - "clr strict security option"
---
# Server configuration: clr strict security

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Controls the interpretation of the `SAFE`, `EXTERNAL_ACCESS`, or `UNSAFE` permission in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information about these permissions, see [Designing assemblies](../../relational-databases/clr-integration/assemblies-designing.md).

| Value | Description |
| --- | --- |
| `0` | **Disabled.** Provided for backward compatibility. Setting this value to `0` isn't recommended. |
| `1` | **Enabled.** Causes the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] to ignore the `PERMISSION_SET` information on the assemblies, and always interpret them as `UNSAFE`. In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, `1` is the default value. |

## Code access security no longer supported

CLR uses Code Access Security (CAS) in the .NET Framework, which is no longer supported as a security boundary. A CLR assembly created with `PERMISSION_SET = SAFE` might be able to access external system resources, call unmanaged code, and acquire sysadmin privileges. In [!INCLUDE [sssql17](../../includes/sssql17-md.md)] and later versions, `clr strict security` treats `SAFE` and `EXTERNAL_ACCESS` assemblies as if they're marked `UNSAFE`.

We recommend that you sign all assemblies by a certificate or asymmetric key, with a corresponding login that has been granted `UNSAFE ASSEMBLY` permission in the `master` database. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] administrators can also add assemblies to a list of assemblies, which the Database Engine should trust. For more information, see [sys.sp_add_trusted_assembly](../../relational-databases/system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md).

## Remarks

When enabled, the `PERMISSION_SET` option in the `CREATE ASSEMBLY` and `ALTER ASSEMBLY` statements is ignored at run-time, but the `PERMISSION_SET` options are preserved in metadata. Ignoring this option minimizes breaking existing code statements.

`CLR strict security` is an `advanced option`.

After you enable strict security, any assemblies that aren't signed fail to load. You must either alter or drop and recreate each assembly so that it's signed with a certificate or asymmetric key that has a corresponding login with the `UNSAFE ASSEMBLY` permission on the server.

## Permissions

### Change this option

Requires `CONTROL SERVER` permission, or membership in the **sysadmin** fixed server role.

### Create a CLR assembly

The following permissions required to create a CLR assembly when `CLR strict security` is enabled:

- The user must have the `CREATE ASSEMBLY` permission

- One of the following conditions must also be true:

  - The assembly is signed with a certificate or asymmetric key that has a corresponding login with the `UNSAFE ASSEMBLY` permission on the server. Signing the assembly is recommended.

  - The database has the `TRUSTWORTHY` property set to `ON`, and the database is owned by a login that has the `UNSAFE ASSEMBLY` permission on the server. This option isn't recommended.

## Examples

The following example first displays the current setting of the `clr strict security` option, and then sets the option value to `1` (enabled).

```sql
EXEC sp_configure 'clr strict security';
GO
EXEC sp_configure 'clr strict security' , '1';
RECONFIGURE;
GO
```

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Server configuration: clr enabled](clr-enabled-server-configuration-option.md)
