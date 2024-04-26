---
title: "Execute user-defined functions"
description: Learn how to execute a user defined function using Transact-SQL.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/26/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "invoking user-defined functions"
  - "user-defined functions [SQL Server], executing"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Execute user-defined functions

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Execute a user defined function using Transact-SQL.

Scalar functions must be invoked by using at least the two-part name of the function (`<schema>.<function>`). For more information, see [CREATE FUNCTION (Transact-SQL)](../../t-sql/statements/create-function-transact-sql.md).

## Limitations

In Transact-SQL, parameters can be supplied either by using `<value>` or by using `@parameter_name = <value>`. A parameter isn't part of a transaction. Therefore, if a parameter is changed in a transaction that is later rolled back, the value of the parameter doesn't revert to its previous value. The value returned to the caller is always the value at the time the module returns.

## Permissions

Permissions aren't required to run the [EXECUTE](../../t-sql/language-elements/execute-transact-sql.md) statement. However, permissions are required on the *securables* referenced within the `EXECUTE` string. For example, if the string contains an [INSERT](../../t-sql/statements/insert-transact-sql.md) statement, the caller of the `EXECUTE` statement must have `INSERT` permission on the target table. Permissions are checked at the time `EXECUTE` statement is encountered, even if the `EXECUTE` statement is included within a module. For more information, see [EXECUTE](../../t-sql/language-elements/execute-transact-sql.md).

## <a id="TsqlProcedure"></a> Use Transact-SQL

This example uses the `ufnGetSalesOrderStatusText` scalar-valued function that is available in most editions of [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)]. The purpose of the function is to return a text value for sales status from a given integer. Vary the example by passing integers 1 through 7 to the `@Status` parameter.

```sql
USE [AdventureWorks2022]
GO

-- Declare a variable to return the results of the function.
DECLARE @ret NVARCHAR(15);

-- Execute the function while passing a value to the @status parameter
EXEC @ret = dbo.ufnGetSalesOrderStatusText @Status = 5;

-- View the returned value.
-- The Execute and Select statements must be executed at the same time.
SELECT N'Order Status: ' + @ret;
```

Here's the result.

```output
Order Status: Shipped
```

## Related content

- [User-defined functions](user-defined-functions.md)
- [CREATE FUNCTION (Transact-SQL)](../../t-sql/statements/create-function-transact-sql.md)
