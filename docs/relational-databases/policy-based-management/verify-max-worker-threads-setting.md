---
title: "Verify Max Worker Threads Setting"
description: "Verify Max Worker Threads Setting"
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Verify max worker threads setting

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks the max worker threads server option for potentially incorrect settings. Setting the max worker threads option to a small value might prevent enough threads from servicing incoming client requests in a timely manner and could lead to "thread starvation." However, setting the option to a large value can waste address space, because each active thread consumes up to 4 MB on 64-bit servers.

## Best practices recommendations

Set the max worker threads option to 0. This enables [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to automatically determine the correct number of active worker threads based on user requests.

## For more information

[Configure the max worker threads (server configuration option)](../../database-engine/configure-windows/configure-the-max-worker-threads-server-configuration-option.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
