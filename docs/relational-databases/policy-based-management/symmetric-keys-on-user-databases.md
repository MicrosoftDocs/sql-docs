---
title: "Symmetric Keys on User Databases"
description: "Symmetric Keys on User Databases"
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Symmetric keys on user databases

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks whether keys that have a length of less than 128 bytes don't use the RC2 or RC4 encryption algorithm.

## Best practices recommendations

Use AES 128 bit or larger to create symmetric keys for data encryption. If AES isn't supported by your operating system, use 3DES.

## For more information

[Choose an encryption algorithm](../security/encryption/choose-an-encryption-algorithm.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
