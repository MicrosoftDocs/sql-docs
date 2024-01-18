---
title: "Simulating IF-WHILE EXISTS - natively compiled module"
description: Learn how to simulate the EXISTS clause in conditional statements, which is not supported by natively compiled stored procedures in SQL Server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 01/12/2024
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Simulate an IF-WHILE EXISTS statement in a natively compiled module

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Natively compiled stored procedures do not support the `EXISTS` clause in conditional statements such as `IF` and `WHILE`.

The following example illustrates a workaround using a `BIT` variable with a `SELECT` statement to simulate an `EXISTS` clause:

```sql
DECLARE @exists BIT = 0;
SELECT TOP 1 @exists = 1 FROM MyTable WHERE ...;
IF @exists = 1;
```

## Related content

- [A Guide to Query Processing for Memory-Optimized Tables](a-guide-to-query-processing-for-memory-optimized-tables.md)
- [Transact-SQL Constructs Not Supported by In-Memory OLTP](transact-sql-constructs-not-supported-by-in-memory-oltp.md)
