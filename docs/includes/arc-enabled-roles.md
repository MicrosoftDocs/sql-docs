---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 02/06/2024
ms.service: sql
ms.topic: include
---

When you install Azure extension for SQL Server, the installation:

1. Creates a server level role: SQLArcExtensionServerRole
1. Creates a database level role: SQLArcExtensionUserRole
1. Adds NT AUTHORITY\SYSTEM account to each role
1. Maps NT AUTHORITY\SYSTEM at the database level for each database
1. Grants minimum permissions for the enabled features

In addition, Azure extension for SQL Server revokes permissions for these roles when they are no longer needed for specific features.

If you uninstall Azure extension for SQL Server, the server level roles are removed.

Uninstallation does not remove the mapping of NT AUTHORITY\SYSTEM automatically.
