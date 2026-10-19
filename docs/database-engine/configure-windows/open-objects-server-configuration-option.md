---
title: "Server Configuration: open objects"
description: "Learn about the disabled configuration option open objects. See how SQL Server now manages the number of open database objects."
author: adbadram
ms.author: adbadram
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "open objects option"
---
# Server configuration: open objects

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This option is still present in `sp_configure`, although its functionality has been disabled in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. (The setting has no effect.) In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the number of open database objects is managed dynamically and is limited only by the available memory. The `open objects` option available in `sp_configure` for backward compatibility with existing scripts.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
