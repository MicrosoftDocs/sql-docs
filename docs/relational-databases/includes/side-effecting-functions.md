---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/18/2025
ms.service: sql
ms.topic: include
---
The following nondeterministic built-in functions **cannot** be used in a [!INCLUDE [tsql-md](../../includes/tsql-md.md)] user-defined function (UDF).

- `NEWID`
- `NEWSEQUENTIALID`
- `RAND`
- `TEXTPTR`

If you reference one of these functions inside a UDF, you get the following error:

```output
Msg 443, Level 16, State 1
Invalid use of a side-effecting operator <operator> within a function.
```

For a list of deterministic and nondeterministic built-in system functions, see [Deterministic and nondeterministic functions](../user-defined-functions/deterministic-and-nondeterministic-functions.md).
