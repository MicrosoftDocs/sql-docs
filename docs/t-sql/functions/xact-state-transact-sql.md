---
title: "XACT_STATE (Transact-SQL)"
description: "XACT_STATE (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman, randolphwest
ms.date: 12/17/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "XACT_STATE"
  - "XACT_STATE_TSQL"
helpviewer_keywords:
  - "states [SQL Server], transactions"
  - "uncommittable transactions"
  - "sessions [SQL Server], active transactions"
  - "XACT_STATE function"
  - "transactions [SQL Server], state"
  - "active transactions"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# XACT_STATE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Is a scalar function that reports the user transaction state of the current session. `XACT_STATE` indicates whether the session has an active user transaction, and whether the transaction is capable of being committed.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
XACT_STATE()
```

> [!NOTE]
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Return types

**smallint**

## Remarks

`XACT_STATE` returns the following values.

| Return value | Description |
| --- | --- |
| 1 | The current session has an active user transaction. The session can perform any actions, including writing data and committing the transaction. |
| 0 | There's no active user transaction for the current session. |
| -1 | The current session has an active user transaction, but an error occurred that caused the transaction to be classified as an uncommittable transaction. The session can't commit the transaction or roll back to a savepoint; it can only request a full rollback of the transaction. The session can't perform any write operations until it rolls back the transaction. The session can only perform read operations until it rolls back the transaction. After the transaction is rolled back, the session can perform both read and write operations and can begin a new transaction.<br /><br />When the outermost batch finishes running, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] automatically rolls back any active uncommittable transactions. If no error message was sent when the transaction entered an uncommittable state, when the batch finishes, an error message is sent to the client application. This message indicates that an uncommittable transaction was detected and rolled back. |

Both the `XACT_STATE` and `@@TRANCOUNT` functions can be used to detect whether the current session has an active user transaction. `@@TRANCOUNT` can't be used to determine whether that transaction is classified as an uncommittable transaction. `XACT_STATE` can't be used to determine whether there are inner transactions.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

The following example uses `XACT_STATE` in the `CATCH` block of a `TRY...CATCH` construct to determine whether to commit or roll back a transaction. Because `SET XACT_ABORT` is `ON`, the constraint violation error causes the transaction to enter an uncommittable state.

```sql
-- SET XACT_ABORT ON renders the transaction uncommittable
-- when the constraint violation occurs.
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;
        -- A FOREIGN KEY constraint exists on this table. This
        -- statement generates a constraint violation error.
        DELETE FROM Production.Product
            WHERE ProductID = 980;

    -- If the delete operation succeeds, commit the transaction. The CATCH
    -- block does not execute.
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    -- Test whether the transaction is uncommittable.
    IF XACT_STATE() = -1
    BEGIN  
        PRINT 'The transaction is in an uncommittable state.' +
              ' Rolling back transaction.'
        ROLLBACK TRANSACTION;
    END;

    -- Test whether the transaction is active and valid.
    IF XACT_STATE() = 1
    BEGIN
        PRINT 'The transaction is committable.' +
              ' Committing transaction.'
        COMMIT TRANSACTION;
    END;
END CATCH;
```

## Related context

- [@@TRANCOUNT (Transact-SQL)](trancount-transact-sql.md)
- [BEGIN TRANSACTION (Transact-SQL)](../language-elements/begin-transaction-transact-sql.md)
- [COMMIT TRANSACTION (Transact-SQL)](../language-elements/commit-transaction-transact-sql.md)
- [ROLLBACK TRANSACTION (Transact-SQL)](../language-elements/rollback-transaction-transact-sql.md)
- [SAVE TRANSACTION (Transact-SQL)](../language-elements/save-transaction-transact-sql.md)
- [TRY...CATCH (Transact-SQL)](../language-elements/try-catch-transact-sql.md)
