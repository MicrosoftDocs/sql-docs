---
title: "Server configuration: Agent XPs"
description: Discover how to use the Agent XPs option to enable SQL Server Agent extended stored procedures. View an example that uses this option.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "Agent XPs option"
  - "extended stored procedures [SQL Server], SQL Server Agent"
---
# Server configuration: Agent XPs

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `Agent XPs` option to enable the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures on this server. When this option isn't enabled, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent node isn't available in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer.

When you use the [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] tool to start the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service, these extended stored procedures are enabled automatically. For more information, see [Surface area configuration](../../relational-databases/security/surface-area-configuration.md).

> [!NOTE]  
> [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)] Object Explorer doesn't display the contents of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]Agent node unless these extended stored procedures are enabled regardless of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service state.

The possible values are:

| Value | Description |
| --- | --- |
| `0` (default) | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures aren't available |
| `1` | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures are available |

The setting takes effect immediately without a server stop and restart.

## Examples

The following example enables the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures.

1. Connect to the Database Engine from Microsoft SQL Server Management Studio.
1. Select **New Query** from the menu.
1. Copy and paste the following example into the query window, and select **Execute**.

```sql
EXEC sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
EXEC sp_configure 'Agent XPs', 1;
GO
RECONFIGURE
GO
```

## Related content

- [Automated Administration Tasks (SQL Server Agent)](../../ssms/agent/automated-administration-tasks-sql-server-agent.md)
- [Start, Stop, or Pause the SQL Server Agent Service](../../ssms/agent/start-stop-or-pause-the-sql-server-agent-service.md)
