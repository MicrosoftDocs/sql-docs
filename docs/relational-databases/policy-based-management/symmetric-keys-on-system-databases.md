---
title: "Symmetric Keys on System Databases"
description: "Symmetric Keys on System Databases"
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Symmetric keys on system databases

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks for user-created symmetric keys in the master, `msdb`, model, and `tempdb` databases.

## Best practices recommendations

Don't create symmetric keys in the system databases.

## For more information

[Choose an encryption algorithm](../security/encryption/choose-an-encryption-algorithm.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
