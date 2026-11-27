---
title: "Set the AUTO_CLOSE Database Option to OFF"
description: Check whether the AUTO_ CLOSE option is OFF. The AUTO_ CLOSE option has implications for performance in SQL Server.
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Set the auto_close database option to off

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks whether the `AUTO_CLOSE` option is set to `OFF`. If you set `AUTO_CLOSE` to `ON`, performance can suffer on frequently accessed databases. The database shuts down after the last user disconnects and reopens for the next connection. Each shutdown also removes the database's cached plans, so queries have to compile again after the database reopens.

## Best practices recommendations

If a database is accessed frequently, set the AUTO_CLOSE option to OFF for the database.

## For more information

[ALTER DATABASE SET Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
