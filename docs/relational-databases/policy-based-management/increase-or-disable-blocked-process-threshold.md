---
title: "Increase or Disable Blocked Process Threshold"
description: "Increase or Disable Blocked Process Threshold."
author: VanMSFT
ms.author: vanto
ms.date: 01/28/2026
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Increase or disable blocked process threshold

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks that the blocked process threshold option is set to 0 (disabled) or to a value of 5 seconds or higher. When you set the blocked process threshold to a value from 1 to 4, the deadlock monitor doesn't run because it only wakes every 5 seconds. If you configure the threshold to a value from 1 to 4, the system doesn't generate blocked process reports. Don't use values 1 to 4 in a production environment because they have no effect.

## Best practices recommendations

To resolve this issue, set the blocked process threshold option to a value of 5 seconds or higher, or disable it by setting the value to 0. To set the blocked process threshold to `5` seconds, execute the following statement:

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
