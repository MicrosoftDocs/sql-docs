---
title: "SQL Server Login Password Expiration"
description: Check whether password expiration of each SQL Server login is enabled to help counter a possible attack in SQL Server.
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Sql server login password expiration

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks whether "Password expiration" of each [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login is enabled. If [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is enabled and if the operating system version is earlier than [!INCLUDE [winserver2003](../../includes/winserver2003-md.md)], an attacker could repeatedly exploit a known [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login password.

## Best practices recommendations

We recommend that you upgrade the operating system to [!INCLUDE [winserver2003](../../includes/winserver2003-md.md)].

If [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication isn't required in your environment, use Windows Authentication. For more information, see [Choose an authentication mode](../security/choose-an-authentication-mode.md).

Enable "Password expiration" for all the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Use [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) to configure the password policy for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login.

## For more information

[Password Policy](../security/password-policy.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
