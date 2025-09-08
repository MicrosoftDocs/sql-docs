---
title: "Server Configuration: filestream access level"
description: "Become familiar with the filestream_access_level option. See how it changes the FILESTREAM access level for an instance of SQL Server."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "FILESTREAM [SQL Server], access level"
  - "filestream access level"
---
# Server configuration: filestream access level

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `filestream access level` option to change the FILESTREAM access level for this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

Before this option has any effect, the Windows administration settings for FILESTREAM must be enabled. You can enable these settings when you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.

| Value | Definition |
| --- | --- |
| `0` | Disables FILESTREAM support for this instance. |
| `1` | Enables FILESTREAM for [!INCLUDE [tsql](../../includes/tsql-md.md)] access. |
| `2` | Enables FILESTREAM for [!INCLUDE [tsql](../../includes/tsql-md.md)] and Win32 streaming access. |
| `3` | Enables FILESTREAM for [!INCLUDE [tsql](../../includes/tsql-md.md)], Win32 streaming access, and remote clients. |

## Related content

- [SQL Server installation guide](../install-windows/install-sql-server.md)
- [Enable and configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md)
