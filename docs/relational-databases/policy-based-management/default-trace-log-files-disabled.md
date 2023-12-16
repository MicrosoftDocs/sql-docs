---
title: "Default Trace Log Files Disabled"
description: "Default Trace Log Files Disabled"
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Default trace log files disabled

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks the value of the `sp_configure` stored procedure default trace enabled option to determine whether default trace is set ON (1) or OFF (0). When this option is enabled, default tracing provides information about configuration and DDL changes to the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. In some cases, this information can be helpful for customers and [!INCLUDE [msCoName](../../includes/msconame-md.md)] Customer Service and Support when they're troubleshooting issues with the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

## Best practices recommendations

Use the `sp_configure` stored procedure to enable tracing by setting the value of default trace enabled to 1.

## For more information

[Server configuration options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
