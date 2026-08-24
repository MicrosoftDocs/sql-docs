---
title: "ERROR_PROCEDURE (Transact-SQL)"
description: ERROR_PROCEDURE returns the name of the stored procedure or trigger where an error occurs.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/05/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "ERROR_PROCEDURE_TSQL"
  - "ERROR_PROCEDURE"
helpviewer_keywords:
  - "ERROR_PROCEDURE function"
  - "messages [SQL Server], trigger where occurred"
  - "errors [SQL Server], stored procedure where occurred"
  - "TRY...CATCH [SQL Server]"
  - "CATCH block"
  - "messages [SQL Server], stored procedure where occurred"
  - "errors [SQL Server], trigger where occurred"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# ERROR_PROCEDURE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

This function returns the name of the stored procedure or trigger where an error occurs, if that error caused the `CATCH` block of a `TRY...CATCH` construct to execute.

- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions return `schema_name.stored_procedure_name`
- [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] return `stored_procedure_name`

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ERROR_PROCEDURE ( )
```

## Return types

**nvarchar(128)**

## Return value

When called in a `CATCH` block, `ERROR_PROCEDURE` returns the name of the stored procedure or trigger in which the error originated.

`ERROR_PROCEDURE` returns `NULL` if the error didn't occur within a stored procedure or trigger.

`ERROR_PROCEDURE` returns `NULL` when called outside the scope of a `CATCH` block.

## Remarks

`ERROR_PROCEDURE` supports calls anywhere within the scope of a `CATCH` block.

`ERROR_PROCEDURE` returns the name of the stored procedure or trigger where an error occurs, regardless of how many times it runs, or where it runs, within the scope of the `CATCH` block. This result contrasts with a function like `@@ERROR`, which only returns an error number in the statement immediately following the one that causes an error.

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### A. Use ERROR_PROCEDURE in a CATCH block

This example shows a stored procedure that generates a divide-by-zero error. `ERROR_PROCEDURE` returns the name of the stored procedure where the error occurred.

```sql
-- Verify that the stored procedure does not already exist.
IF OBJECT_ID('usp_ExampleProc', 'P') IS NOT NULL
    DROP PROCEDURE usp_ExampleProc;
GO

-- Create a stored procedure that
-- generates a divide-by-zero error.
CREATE PROCEDURE usp_ExampleProc
AS
SELECT 1 / 0;
GO

BEGIN TRY
-- Execute the stored procedure inside the TRY block.
    EXECUTE usp_ExampleProc;
END TRY
BEGIN CATCH
    SELECT ERROR_PROCEDURE() AS ErrorProcedure;
END CATCH;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------

(0 row(s) affected)

ErrorProcedure
--------------------
usp_ExampleProc

(1 row(s) affected)
```

### B. Use ERROR_PROCEDURE in a CATCH block with other error-handling tools

This example shows a stored procedure that generates a divide-by-zero error. Along with the name of the stored procedure where the error occurred, the stored procedure returns information about the error.

```sql
-- Verify that the stored procedure does not already exist.
IF OBJECT_ID('usp_ExampleProc', 'P') IS NOT NULL
    DROP PROCEDURE usp_ExampleProc;
GO

-- Create a stored procedure that
-- generates a divide-by-zero error.
CREATE PROCEDURE usp_ExampleProc
AS
SELECT 1 / 0;
GO

BEGIN TRY
-- Execute the stored procedure inside the TRY block.
    EXECUTE usp_ExampleProc;
END TRY
BEGIN CATCH
    SELECT ERROR_NUMBER() AS ErrorNumber,
           ERROR_SEVERITY() AS ErrorSeverity,
           ERROR_STATE() AS ErrorState,
           ERROR_PROCEDURE() AS ErrorProcedure,
           ERROR_MESSAGE() AS ErrorMessage,
           ERROR_LINE() AS ErrorLine;
END CATCH;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------

(0 row(s) affected)

ErrorNumber ErrorSeverity ErrorState  ErrorProcedure   ErrorMessage                       ErrorLine
----------- ------------- ----------- ---------------- ---------------------------------- -----------
8134        16            1           usp_ExampleProc  Divide by zero error encountered.  6

(1 row(s) affected)
```

## Related content

- [Messages (for errors) catalog views - sys.messages](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)
- [TRY...CATCH (Transact-SQL)](../language-elements/try-catch-transact-sql.md)
- [ERROR_LINE (Transact-SQL)](error-line-transact-sql.md)
- [ERROR_MESSAGE (Transact-SQL)](error-message-transact-sql.md)
- [ERROR_NUMBER (Transact-SQL)](error-number-transact-sql.md)
- [ERROR_SEVERITY (Transact-SQL)](error-severity-transact-sql.md)
- [ERROR_STATE (Transact-SQL)](error-state-transact-sql.md)
- [RAISERROR (Transact-SQL)](../language-elements/raiserror-transact-sql.md)
- [@@ERROR (Transact-SQL)](error-transact-sql.md)
