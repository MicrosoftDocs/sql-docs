---
title: "Trustworthy Bit"
description: "Trustworthy Bit"
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Trustworthy bit

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule determines whether the dbo role for a database is assigned to the sysadmin fixed server role and the database has its trustworthy bit set to ON.

If these conditions are met, a privileged database user can elevate privileges to the sysadmin role. In this role, the user can create and run unsafe assemblies that compromise the system.

## Best practices recommendations

Turn off the trustworthy bit or revoke sysadmin permissions from the dbo database role.

## For more information

[ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
