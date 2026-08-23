---
title: "ERROR_SEVERITY (Transact-SQL)"
description: "ERROR_SEVERITY (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/19/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "ERROR_SEVERITY_TSQL"
  - "ERROR_SEVERITY"
helpviewer_keywords:
  - "errors [SQL Server], severity"
  - "messages [SQL Server], severity"
  - "TRY...CATCH [SQL Server]"
  - "CATCH block"
  - "ERROR_SEVERITY function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# ERROR_SEVERITY (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

This function returns the severity value of the error where an error occurs, if that error caused the `CATCH` block of a `TRY...CATCH` construct to execute.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ERROR_SEVERITY ( )
```

## Return types

**int**

## Return value

When called in a `CATCH` block where an error occurs, `ERROR_SEVERITY` returns the severity value of the error that caused the `CATCH` block to run.

`ERROR_SEVERITY` returns `NULL` if called outside the scope of a `CATCH` block.

## Remarks

`ERROR_SEVERITY` supports calls anywhere within the scope of a `CATCH` block.

`ERROR_SEVERITY` returns the error severity value of an error, regardless of how many times it runs or where it runs within the scope of the `CATCH` block. This contrasts with a function like [&#x40;&#x40;ERROR](error-transact-sql.md), which only returns an error number in the statement immediately following the one that causes an error.

`ERROR_SEVERITY` typically operates in a nested `CATCH` block. `ERROR_SEVERITY` returns the error severity value specific to the scope of the `CATCH` block that referenced that `CATCH` block. For example, the `CATCH` block of an outer `TRY...CATCH` construct could have an inner `TRY...CATCH` construct. Inside that inner `CATCH` block, `ERROR_SEVERITY` returns the severity value of the error that invoked the inner `CATCH` block. If `ERROR_SEVERITY` runs in the outer `CATCH` block, it returns the error severity value of the error that invoked that outer `CATCH` block.

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### A. Use ERROR_SEVERITY in a CATCH block

This example shows a stored procedure that generates a divide-by-zero error. `ERROR_SEVERITY` returns the severity value of that error.

```sql
BEGIN TRY
-- Generate a divide-by-zero error.
    SELECT 1 / 0;
END TRY
BEGIN CATCH
    SELECT ERROR_SEVERITY() AS ErrorSeverity;
END CATCH
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------

(0 row(s) affected)

ErrorSeverity
-------------
16

(1 row(s) affected)
```

### B. Use ERROR_SEVERITY in a CATCH block with other error-handling tools

This example shows a `SELECT` statement that generates a divide by zero error. The stored procedure returns information about the error.

```sql
BEGIN TRY
-- Generate a divide-by-zero error.
    SELECT 1 / 0;
END TRY
BEGIN CATCH
    SELECT ERROR_NUMBER() AS ErrorNumber,
           ERROR_SEVERITY() AS ErrorSeverity,
           ERROR_STATE() AS ErrorState,
           ERROR_PROCEDURE() AS ErrorProcedure,
           ERROR_LINE() AS ErrorLine,
           ERROR_MESSAGE() AS ErrorMessage;
END CATCH
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------

(0 row(s) affected)

ErrorNumber ErrorSeverity ErrorState  ErrorProcedure  ErrorLine   ErrorMessage
----------- ------------- ----------- --------------- ----------- ----------------------------------
8134        16            1           NULL            4           Divide by zero error encountered.

(1 row(s) affected)
```

## Related content

- [Messages (for errors) catalog views - sys.messages](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)
- [Database Engine error severities](../../relational-databases/errors-events/database-engine-error-severities.md)
- [TRY...CATCH (Transact-SQL)](../language-elements/try-catch-transact-sql.md)
- [ERROR_LINE (Transact-SQL)](error-line-transact-sql.md)
- [ERROR_MESSAGE (Transact-SQL)](error-message-transact-sql.md)
- [ERROR_NUMBER (Transact-SQL)](error-number-transact-sql.md)
- [ERROR_PROCEDURE (Transact-SQL)](error-procedure-transact-sql.md)
- [ERROR_STATE (Transact-SQL)](error-state-transact-sql.md)
- [RAISERROR (Transact-SQL)](../language-elements/raiserror-transact-sql.md)
- [@@ERROR (Transact-SQL)](error-transact-sql.md)
- [Errors and events reference (Database Engine)](../../relational-databases/errors-events/errors-and-events-reference-database-engine.md)
