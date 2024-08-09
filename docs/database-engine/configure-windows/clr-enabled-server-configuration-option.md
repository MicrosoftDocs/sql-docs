---
title: "Server configuration: clr enabled"
description: Learn how to use the clr enabled option to specify whether SQL Server can run user assemblies. See when common language runtime execution isn't supported.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/02/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "assemblies [CLR integration], verifying can run"
  - "clr enabled option"
---
# Server configuration: clr enabled

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `clr enabled` option to specify whether [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can run user assemblies. The `clr enabled` option provides the following values:

| Value | Description |
| --- | --- |
| `0` | Assembly execution not allowed on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `1` | Assembly execution allowed on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |

For WOW64 only: restart WOW64 servers to apply these changes. No restart is required for other server types.

When you run `RECONFIGURE`, and the run value of the `clr enabled` option is changed from `1` to `0`, all application domains containing user assemblies are immediately unloaded.

## Limitations

### Common language runtime (CLR) execution isn't supported under lightweight pooling

Disable one of two options: `clr enabled` or `lightweight pooling`. Features that rely upon CLR and that don't work properly in fiber mode include the **hierarchyid** data type, the `FORMAT` function, replication, and Policy-Based Management. For more information, see [Server configuration: lightweight pooling](lightweight-pooling-server-configuration-option.md).

Though the `clr enabled` configuration option is enabled in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], developing CLR user functions aren't supported in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

### Code access security no longer supported

[!INCLUDE [code-access-security](../includes/code-access-security.md)]

## Examples

The following example first displays the current setting of the `clr enabled` option and then enables the option by setting the option value to 1. To disable the option, set the value to 0.

```sql
EXEC sp_configure 'clr enabled';
EXEC sp_configure 'clr enabled' , '1';
RECONFIGURE;
```

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
