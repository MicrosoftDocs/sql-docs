---
title: "Guest Permissions on User Databases"
description: Determine whether the guest user has permission to access user databases in SQL Server. Revoke the guest user permission if it isn't required.
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Guest permissions on user databases

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule determines whether the guest user has permission to access the database. This rule applies to user databases only.

## Best practices recommendations

Revoke the guest user permission to access the database if it isn't required.

The guest user can't be dropped, but guest user can be disabled by revoking its CONNECT permission by executing REVOKE CONNECT FROM GUEST within any database other than `master`, `tempdb`, or `msdb`.

## For more information

[Securing SQL Server](../security/securing-sql-server.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
