---
title: "Server configuration: xp_cmdshell"
description: "Learn about the xp_cmdshell option. See how it controls whether SQL Server can run the xp_cmdshell extended stored procedure. Find out how to turn it on."
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "xp_cmdshell"
---
# Server configuration: xp_cmdshell

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to enable the `xp_cmdshell` SQL Server configuration option. This option allows system administrators to control whether the [xp_cmdshell extended stored procedure](../../relational-databases/system-stored-procedures/xp-cmdshell-transact-sql.md) can be executed on a system. By default, the `xp_cmdshell` option is disabled on new installations.

Before enabling this option, it's important to consider the potential security implications.

- Newly developed code shouldn't use the `xp_cmdshell` stored procedure, and generally it should be left disabled.
- Some legacy applications require `xp_cmdshell` to be enabled. If they can't be modified to avoid the use of this stored procedure, you can enable it as described below.

> [!NOTE]  
> If `xp_cmdshell` must be used, as a security best practice it's recommended to only enable it for the duration of the actual task that requires it. Using `xp_cmdshell` can trigger security audit tools.

If you need to enable `xp_cmdshell`, you can use [Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md) or run the `sp_configure` system stored procedure as shown in the following code example:

```sql
USE master;
GO

EXECUTE sp_configure 'show advanced options', 1;
GO

RECONFIGURE;
GO

EXECUTE sp_configure 'xp_cmdshell', 1;
GO

RECONFIGURE;
GO

EXECUTE sp_configure 'show advanced options', 0;
GO

RECONFIGURE;
GO
```

## Related content

- [xp_cmdshell extended stored procedure](../../relational-databases/system-stored-procedures/xp-cmdshell-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)
