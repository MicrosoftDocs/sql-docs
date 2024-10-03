---
title: "THROW (Transact-SQL)"
description: THROW raises an exception and transfers execution to a CATCH block of a TRY...CATCH construct.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/26/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "THROW_TSQL"
  - "THROW"
helpviewer_keywords:
  - "THROW statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# THROW (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Raises an exception and transfers execution to a `CATCH` block of a [TRY...CATCH](try-catch-transact-sql.md) construct.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
THROW [ { error_number | @local_variable }
    , { message | @local_variable }
    , { state | @local_variable } ]
[ ; ]
```

## Arguments

#### *error_number*

A constant or variable that represents the exception. The *error_number* argument is **int**, and must be greater than or equal to 50,000, and less than or equal to 2,147,483,647.

#### *message*

A string or variable that describes the exception. The *message* argument is **nvarchar(2048)**.

#### *state*

A constant or variable between 0 and 255 that indicates the state to associate with the message. The *state* argument is **tinyint**.

## Remarks

Use *state* to help you identify the source of an error in your stored procedure, trigger, or statement batch. For example, if you use the same message in multiple places, a unique *state* value can help you locate where the error occurred.

The statement before the `THROW` statement must be followed by the semicolon (`;`) statement terminator.

If a `TRY...CATCH` construct isn't available, the statement batch is terminated. The line number and procedure where the exception is raised are set. The severity is set to `16`.

If the `THROW` statement is specified without parameters, it must appear inside a `CATCH` block. This causes the caught exception to be raised. Any error that occurs in a `THROW` statement causes the statement batch to be terminated.

`%` is a reserved character in the message text of a `THROW` statement and must be escaped. Double the `%` character to return `%` as part of the message text, for example `'The increase exceeded 15%% of the original value'`.

## Differences between RAISERROR and THROW

The following table lists differences between the [RAISERROR](raiserror-transact-sql.md) and `THROW` statements.

| RAISERROR statement | THROW statement |
| --- | --- |
| If a *msg_id* is passed to `RAISERROR`, the ID must be defined in `sys.messages`. | The *error_number* parameter doesn't have to be defined in `sys.messages`. |
| The *msg_str* parameter can contain `printf` formatting styles. | The *message* parameter doesn't accept `printf` style formatting. |
| The *severity* parameter specifies the severity of the exception. | There's no *severity* parameter. When `THROW` is used to initiate the exception, the severity is always set to `16`. However, when `THROW` is used to rethrow an existing exception, the severity is set to that exception's severity level. |
| Doesn't honor [SET XACT_ABORT](../statements/set-xact-abort-transact-sql.md). | Transactions are *rolled back* if [SET XACT_ABORT](../statements/set-xact-abort-transact-sql.md) is `ON`. |

## Examples

### A. Use THROW to raise an exception

The following example shows how to use the `THROW` statement to raise an exception.

```sql
THROW 51000, 'The record does not exist.', 1;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Msg 51000, Level 16, State 1, Line 1
The record does not exist.
```

### B. Use THROW to raise an exception again

The following example shows how to use the `THROW` statement to raise the last thrown exception again.

```sql
USE tempdb;
GO
CREATE TABLE dbo.TestRethrow
(    ID INT PRIMARY KEY
);
BEGIN TRY
    INSERT dbo.TestRethrow(ID) VALUES(1);

--  Force error 2627, Violation of PRIMARY KEY constraint to be raised.
    INSERT dbo.TestRethrow(ID) VALUES(1);
END TRY
BEGIN CATCH

    PRINT 'In catch block.';
    THROW;
END CATCH;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
In catch block.
Msg 2627, Level 14, State 1, Line 1
Violation of PRIMARY KEY constraint 'PK__TestReth__3214EC272E3BD7D3'. Cannot insert duplicate key in object 'dbo.TestRethrow'.
The statement has been terminated.
```

### C. Use FORMATMESSAGE with THROW

The following example shows how to use the [FORMATMESSAGE](../functions/formatmessage-transact-sql.md) function with `THROW` to throw a customized error message. The example first creates a user-defined error message by using `sp_addmessage`. Because the `THROW` statement doesn't allow for substitution parameters in the *message* parameter in the way that `RAISERROR` does, the `FORMATMESSAGE` function is used to pass the three parameter values expected by error message `60000`.

```sql
EXEC sys.sp_addmessage
    @msgnum = 60000,
    @severity = 16,
    @msgtext = N'This is a test message with one numeric parameter (%d), one string parameter (%s), and another string parameter (%s).',
    @lang = 'us_english';
GO

DECLARE @msg NVARCHAR(2048) = FORMATMESSAGE(60000, 500, N'First string', N'second string');

THROW 60000, @msg, 1;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Msg 60000, Level 16, State 1, Line 2
This is a test message with one numeric parameter (`500`), one string parameter (First string), and another string parameter (second string).
```

## Related content

- [RAISERROR (Transact-SQL)](raiserror-transact-sql.md)
- [FORMATMESSAGE (Transact-SQL)](../functions/formatmessage-transact-sql.md)
- [ERROR_MESSAGE (Transact-SQL)](../functions/error-message-transact-sql.md)
- [ERROR_NUMBER (Transact-SQL)](../functions/error-number-transact-sql.md)
- [&#x40;&#x40;ERROR (Transact-SQL)](../functions/error-transact-sql.md)
- [XACT_STATE (Transact-SQL)](../functions/xact-state-transact-sql.md)
- [SET XACT_ABORT (Transact-SQL)](../statements/set-xact-abort-transact-sql.md)
