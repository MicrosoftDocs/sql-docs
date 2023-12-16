---
title: "Increase or Disable Blocked Process Threshold"
description: "Increase or Disable Blocked Process Threshold."
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Increase or disable blocked process threshold

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rules checks that the blocked process threshold option is set to 0 (disabled) or set to a value higher than or equal to 5 (seconds). Setting the blocked process threshold option to a value from 1 to 4 can cause the deadlock monitor to run constantly. Values 1 to 4 should only be used for troubleshooting, and never long term or in a production environment without the assistance of [!INCLUDE [msCoName](../../includes/msconame-md.md)] Customer Service and Support.

## Best practices recommendations

To resolve this problem, set the blocked process threshold option to a value of 5 (seconds) or higher, or disable blocked process threshold by setting the value to 0. To set the blocked process threshold to a value of `5` seconds, execute the following statement:

```
sp_configure 'show advanced options', 1 ;
GO
RECONFIGURE ;
GO
sp_configure 'blocked process threshold', 5 ;
GO
RECONFIGURE ;
GO
```

## For more information

[blocked process threshold Server Configuration Option](../../database-engine/configure-windows/blocked-process-threshold-server-configuration-option.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
