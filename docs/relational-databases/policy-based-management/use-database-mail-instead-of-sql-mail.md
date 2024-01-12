---
title: "Use Database Mail Instead of SQL Mail"
description: "Use Database Mail Instead of SQL Mail."
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Use database mail instead of sql mail

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks the `sys.configurations` catalog view to determine whether the SQL Mail XPs server-wide configuration option is set to ON.

## Best practices recommendations

SQL Mail will be removed in a future version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. To send mail, use Database Mail.

SQL Mail runs in-process to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service. If SQL Mail goes down, so does the server. Database Mail runs outside [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in a separate process, is scalable, and doesn't require Extended MAPI client components to be installed on the production server.

## For more information

[Database Mail](../database-mail/database-mail.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
