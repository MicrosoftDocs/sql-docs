---
title: "Execute User-defined Functions"
description: "Execute User-defined Functions"
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/28/2022"
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "invoking user-defined functions"
  - "user-defined functions [SQL Server], executing"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Execute user-defined functions

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Execute a user defined function using Transact-SQL.

## <a id="Restrictions"></a> Limitations and restrictions

In Transact-SQL, parameters can be supplied either by using *value* or by using @*parameter_name*=*value.* A parameter isn't part of a transaction; therefore, if a parameter is changed in a transaction that is later rolled back, the value of the parameter doesn't revert to its previous value. The value returned to the caller is always the value at the time the module returns.

## Permissions

Permissions aren't required to run the [EXECUTE](../../t-sql/language-elements/execute-transact-sql.md) statement. However, permissions **are required** on the securables referenced within the EXECUTE string. For example, if the string contains an [INSERT](../../t-sql/statements/insert-transact-sql.md) statement, the caller of the EXECUTE statement must have INSERT permission on the target table. Permissions are checked at the time EXECUTE statement is encountered, even if the EXECUTE statement is included within a module. For more information, see [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md)

## <a id="TsqlProcedure"></a> Use Transact-SQL

This example uses the `ufnGetSalesOrderStatusText` scalar-valued function that is available in most editions of `AdventureWorks`.  The purpose of the function is to return a text value for sales status from a given integer.  Vary the example by passing integers 1 through 7 to the `@Status` parameter.

```sql
USE [AdventureWorks2016CTP3]
GO

-- Declare a variable to return the results of the function.
DECLARE @ret nvarchar(15);

-- Execute the function while passing a value to the @status parameter
EXEC @ret = dbo.ufnGetSalesOrderStatusText @Status = 5;

-- View the returned value.  The Execute and Select statements must be executed at the same time.
SELECT N'Order Status: ' + @ret;

-- Result:
-- Order Status: Shipped
```

## See also

- [User-Defined Functions](user-defined-functions.md)
- [CREATE FUNCTION (Transact SQL](../../t-sql/statements/create-function-transact-sql.md)
