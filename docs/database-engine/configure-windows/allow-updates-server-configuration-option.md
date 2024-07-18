---
title: "Server configuration: allow updates"
description: "Learn about the obsolete SQL Server configuration option allow updates. See how using this option causes RECONFIGURE statements to fail."
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "allow updates option"
---
# Server configuration: allow updates

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This option is still present in the `sp_configure` stored procedure, although its functionality is unavailable in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The setting has no effect. Direct updates to the system tables aren't supported.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

Changing the `allow updates` option causes the `RECONFIGURE` statement to fail. Changes to the `allow updates` option should be removed from all scripts.

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
