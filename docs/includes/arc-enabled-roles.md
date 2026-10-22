---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 04/28/2026
ms.service: sql
ms.topic: include
ai-usage: ai-assisted
---

When you install Azure Extension for SQL Server in non-least-privilege mode, the installation:

1. Creates a server-level role: `SQLArcExtensionServerRole`
1. Creates a database-level role: `SQLArcExtensionUserRole`
1. Adds the `NT AUTHORITY\SYSTEM` account to each role
1. Maps `NT AUTHORITY\SYSTEM` at the database level for each database
1. Grants minimum permissions for the enabled features

Alternatively, you can configure [!INCLUDE [ssazurearc](ssazurearc.md)] to run in least privilege mode. For more information, see [Operate SQL Server enabled by Azure Arc with least privilege](../sql-server/azure-arc/configure-least-privilege.md).

In addition, Azure Extension for SQL Server revokes permissions for these roles when they're no longer needed for specific features.

> [!NOTE]  
> The previously described actions require the Deployer to connect to SQL Server as `NT AUTHORITY\SYSTEM`. If the `NT AUTHORITY\SYSTEM` login is removed, disabled, or denied `CONNECT SQL` permission, the Deployer can't perform any of these actions, and the Azure Extension for SQL Server fails to provision. See [Prerequisites](../sql-server/azure-arc/prerequisites.md) for steps to verify and restore this login.

`SqlServerExtensionPermissionProvider` is a Windows task. It executes Deployer.exe to grant or revoke privileges in SQL Server when it detects:

- A new SQL Server instance is installed on the host
- A SQL Server instance is uninstalled from the host
- An instance-level feature is enabled or disabled, or settings are updated
- The extension service is restarted
- [Just-in-time (JIT) permissions](../sql-server/azure-arc/configure-windows-accounts-agent.md#just-in-time-sql-permissions) are enabled or disabled

> [!NOTE]  
> Before the July 2024 release, `SqlServerExtensionPermissionProvider` was a scheduled task that ran hourly.

For details, review [Configure Windows service accounts and permissions for Azure Extension for SQL Server](../sql-server/azure-arc/configure-windows-accounts-agent.md).

If you uninstall Azure Extension for SQL Server, the server-level and database-level roles are removed.
