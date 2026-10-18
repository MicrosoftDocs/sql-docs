---
title: "Server Configuration: max ucs send boxcars"
description: "Learn about UCS flow control with the max ucs send boxcars configuration option, and when to change these options in SQL Server."
author: sergten
ms.author: sergeyten
ms.reviewer: vanto
ms.date: 08/26/2025
ms.service: sql
ms.topic: how-to
ms.custom:
  - build-2025
helpviewer_keywords:
  - "max ucs send boxcars"
---

# Server configuration: max ucs send boxcars

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

Use the `max ucs send boxcars` server configuration option to control the maximum number of UCS boxcars that can be used to send messages from the primary to the secondary replica. This option is useful for controlling the flow of data between replicas in an [Always On availability group](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) over a wide-area network (WAN).

> [!NOTE]  
> The `max ucs send boxcars` server configuration option is available starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

## Overview

The Universal Communication Service (UCS) protocol is used by SQL Server [Always On availability groups](../availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) to send log blocks between primary and secondary replicas. UCS determines whether the secondary replica falls behind the primary replica by measuring the time it takes for the primary to receive an acknowledgment that the change was hardened on the secondary.

While UCS communicates between endpoints, it enters *flow control* when it detects that the secondary replica can't keep up with applying changes from the primary replica. This process works well for fast local networks where network lag isn't a contributing factor, since the bulk of the delay is attributed to processing on the secondary replica. However, when UCS communicates over a wide-area network (WAN) with significant network lag, such as in geo-replication, the increased network latency can cause the secondary replica to fall behind. In this scenario, flow control is inefficient.

To address this network lag scenario, UCS needs to defer entering flow control. This is achieved by changing the limit on the number of *UCS boxcars* that can be used to send messages from the primary to the secondary replica. UCS packets are grouped together in a boxcar to allow for more efficient transmission over a network. When you increase the maximum number of UCS boxcars, more packets can be transferred at a time, which in turn defers entering flow control.

## Override UCS boxcars with the Windows registry

Starting in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you can control the number of UCS boxcars through the creation of a registry value that SQL Server reads during initialization.

- Registry key: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL16.<instance_name>\MSSQLServer\ServiceBroker`
- Value name: `MaxPendingSend`
- Value type: `REG_SZ`

> [!IMPORTANT]  
> You must create the value as `REG_SZ` and not `REG_DWORD`, because SQL Server checks the type and ignores the value if it isn't the expected type.

This mechanism has several drawbacks:

- Database administrators might not have access to the registry, either on the same machine as the SQL Server instance, or remotely.

- Database administrators and system administrators are usually two distinct roles with different sets of responsibilities, and hence different sets of permissions.

## Override UCS boxcars with sp_configure

Starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], you can control the number of UCS boxcars with the `max ucs send boxcars` server configuration option, with the following considerations:

- This setting is an advanced `sp_configure` option.

- The minimum value is `256` (the default), and the maximum value is `2048`. However, you can use a value of `0` to reset the value to default.

- This configuration option takes precedence over the registry setting.

- This setting takes effect after a SQL Server instance restart.

```sql
USE master;
GO

EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
GO

EXEC sp_configure 'max ucs send boxcars', 1234;
RECONFIGURE;
GO
```

## Order of precedence

The following table shows examples of precedence, depending on which override mechanism you use.

| Rule | `sp_configure` value | Registry value | Effective value |
| --- | --- | --- | --- |
| `max ucs send boxcars` values that are non-default (that is, not equal to 256) take precedence over the registry values. | 1024 | 2048 | 1024 |
| If `max ucs send boxcars` is set to its default value of 256, the registry value takes effect. | 256 | 2048 | 2048 |
| If `max ucs send boxcars` is set to `0`, it uses the default value, which takes precedence over the registry value. This way, if database administrators don't have permission to edit the registry, they can still apply the default value. | 0 | 2048 | 256 |

## Remarks

You must run `RECONFIGURE` after setting the `max ucs send boxcars` server configuration option to apply the new configuration. The setting only takes effect after restarting the SQL Server instance.

When a non-default value is in effect for the number of UCS boxcars, SQL Server logs an information message into the error log. The information message contains the effective value and the source of override: `sp_configure` or `registry`.

```output
2024-10-08 13:38:26.11 Server      UCS transport default sending capacity is overridden with the value of 1234. Override source: sp_configure. This is an informational message only. No user action is required.
```

The internal error code for the message is `33338`. The message isn't logged if the default value is set.

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [Monitor and troubleshoot availability groups](../availability-groups/windows/always-on-availability-groups-troubleshooting-and-monitoring-guide.md)
- [Add a secondary replica to an Always On Availability Group](../availability-groups/windows/add-a-secondary-replica-to-an-availability-group-sql-server.md)
