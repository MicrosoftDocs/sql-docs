---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 04/25/2024
ms.service: sql
ms.topic: include
---

When you install Azure extension for SQL Server, the installation:

1. Creates a server level role: SQLArcExtensionServerRole
1. Creates a database level role: SQLArcExtensionUserRole
1. Adds NT AUTHORITY\SYSTEM<sup>*</sup> account to each role
1. Maps NT AUTHORITY\SYSTEM<sup>*</sup> at the database level for each database
1. Grants minimum permissions for the enabled features

   <sup>*</sup>Alternatively, you can configure [!INCLUDE [ssazurearc](ssazurearc.md)] to run in least privilege mode (available in preview). For details, review [Operate SQL Server enabled by Azure Arc with least privilege (preview)](../sql-server/azure-arc/configure-least-privilege.md).

In addition, Azure extension for SQL Server revokes permissions for these roles when they're no longer needed for specific features.

`SqlServerExtensionPermissionProvider` is a Windows scheduled task. It runs hourly. It grants or revokes privileges in SQL Server when it detects:

- A new SQL Server instance is installed on the host
- A new database is created
- A feature is enabled or disabled

For details, review [Configure Windows service accounts and permissions for Azure extension for SQL Server](../sql-server/azure-arc/configure-windows-accounts-agent.md).

If you uninstall Azure extension for SQL Server, the server and database level roles are removed.