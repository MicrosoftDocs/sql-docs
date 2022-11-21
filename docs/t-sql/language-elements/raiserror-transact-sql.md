---
title: "RAISERROR (Transact-SQL)"
description: "RAISERROR (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/09/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "RAISERROR"
  - "RAISERROR_TSQL"
  - "RAISEERROR_TSQL"
helpviewer_keywords:
  - "sysmessages system table"
  - "errors [SQL Server], RAISERROR statement"
  - "user-defined error messages [SQL Server]"
  - "system flags"
  - "generating errors [SQL Server]"
  - "TRY block [SQL Server]"
  - "recording errors"
  - "ad hoc messages"
  - "RAISERROR statement"
  - "CATCH block"
  - "messages [SQL Server], RAISERROR statement"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# RAISERROR (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!NOTE]  
> The `RAISERROR` statement does not honor `SET XACT_ABORT`. New applications should use `THROW` instead of `RAISERROR`.

Generates an error message and initiates error processing for the session. `RAISERROR` can either reference a user-defined message stored in the `sys.messages` catalog view, or build a message dynamically. The message is returned as a server error message to the calling application or to an associated `CATCH` block of a `TRY...CATCH` construct. New applications should use [THROW](../../t-sql/language-elements/throw-transact-sql.md) instead.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server and Azure SQL Database:

```syntaxsql
RAISERROR ( { msg_id | msg_str | @local_variable }
    { , severity, state }
    [ , argument [ , ...n ] ] )
    [ WITH option [ , ...n ] ]
```

Syntax for Azure Synapse Analytics and Parallel Data Warehouse:

```syntaxsql
RAISERROR ( { msg_str | @local_variable }
    { , severity, state }
    [ , argument [ , ...n ] ] )
    [ WITH option [ , ...n ] ]
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *msg_id*

A user-defined error message number stored in the `sys.messages` catalog view using `sp_addmessage`. Error numbers for user-defined error messages should be greater than 50000. When *msg_id* is not specified, `RAISERROR` raises an error message with an error number of 50000.

#### *msg_str*

A user-defined message with formatting similar to the `printf` function in the C standard library. The error message can have a maximum of 2,047 characters. If the message contains 2,048 or more characters, only the first 2,044 are displayed and an ellipsis is added to indicate that the message has been truncated. Note that substitution parameters consume more characters than the output shows because of internal storage behavior. For example, the substitution parameter of *%d* with an assigned value of 2 actually produces one character in the message string but also internally takes up three additional characters of storage. This storage requirement decreases the number of available characters for message output.

When *msg_str* is specified, `RAISERROR` raises an error message with an error number of 50000.

*msg_str* is a string of characters with optional embedded conversion specifications. Each conversion specification defines how a value in the argument list is formatted and placed into a field at the location of the conversion specification in *msg_str*. Conversion specifications have this format:

% [[*flag*] [*width*] [. *precision*] [{h | l}]] *type*

The parameters that can be used in *msg_str* are:

***flag***

A code that determines the spacing and justification of the substituted value.

|Code|Prefix or justification|Description|
|----------|-----------------------------|-----------------|
|- (minus)|Left-justified|Left-justify the argument value within the given field width.|
|+ (plus)|Sign prefix|Preface the argument value with a plus (+) or minus (-) if the value is of a signed type.|
|0 (zero)|Zero padding|Preface the output with zeros until the minimum width is reached. When 0 and the minus sign (-) appear, 0 is ignored.|
|# (number)|0x prefix for hexadecimal type of x or X|When used with the o, x, or X format, the number sign (#) flag prefaces any nonzero value with 0, 0x, or 0X, respectively. When d, i, or u are prefaced by the number sign (#) flag, the flag is ignored.|
|' ' (blank)|Space padding|Preface the output value with blank spaces if the value is signed and positive. This is ignored when included with the plus sign (+) flag.|

***width***

An integer that defines the minimum width for the field into which the argument value is placed. If the length of the argument value is equal to or longer than *width*, the value is printed with no padding. If the value is shorter than *width*, the value is padded to the length specified in *width*.

An asterisk (*) means that the width is specified by the associated argument in the argument list, which must be an integer value.

***precision***

The maximum number of characters taken from the argument value for string values. For example, if a string has five characters and precision is 3, only the first three characters of the string value are used.

For integer values, *precision* is the minimum number of digits printed.

An asterisk (*) means that the precision is specified by the associated argument in the argument list, which must be an integer value.

**{h | l} *type***

Used with character types d, i, o, s, x, X, or u, and creates **shortint** (h) or **longint** (l) values.

|Type specification|Represents|
|------------------------|----------------|
|d or i|Signed integer|
|o|Unsigned octal|
|s|String|
|u|Unsigned integer|
|x or X|Unsigned hexadecimal|

These type specifications are based on the ones originally defined for the `printf` function in the C standard library. The type specifications used in `RAISERROR` message strings map to [!INCLUDE[tsql](../../includes/tsql-md.md)] data types, while the specifications used in `printf` map to C language data types. Type specifications used in `printf` are not supported by `RAISERROR` when [!INCLUDE[tsql](../../includes/tsql-md.md)] does not have a data type similar to the associated C data type. For example, the *%p* specification for pointers is not supported in `RAISERROR` because [!INCLUDE[tsql](../../includes/tsql-md.md)] does not have a pointer data type.

To convert a value to the [!INCLUDE[tsql](../../includes/tsql-md.md)] **bigint** data type, specify **%I64d**.

#### *@local_variable*

Is a variable of any valid character data type that contains a string formatted in the same manner as *msg_str*. *\@local_variable* must be **char** or **varchar**, or be able to be implicitly converted to these data types.

#### *severity*

The user-defined [severity level](../../relational-databases/errors-events/database-engine-error-severities.md) associated with this message. When using *msg_id* to raise a user-defined message created using `sp_addmessage`, the severity specified on `RAISERROR` overrides the severity specified in `sp_addmessage`.

For severity levels from 19 through 25, the WITH LOG option is required. Severity levels less than 0 are interpreted as 0. Severity levels greater than 25 are interpreted as 25.

> [!CAUTION]
> Severity levels from 20 through 25 are considered fatal. If a fatal severity level is encountered, the client connection is terminated after receiving the message, and the error is logged in the error and application logs.

You can specify `-1` to return the severity value associated with the error as shown in the following example.

```sql
RAISERROR (15600, -1, -1, 'mysp_CreateCustomer');
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
Msg 15600, Level 15, State 1, Line 1
An invalid parameter or option was specified for procedure 'mysp_CreateCustomer'.
```

#### *state*

An integer from 0 through 255. Negative values default to 1. Values larger than 255 should not be used.

If the same user-defined error is raised at multiple locations, using a unique state number for each location can help find which section of code is raising the errors.

#### *argument*

The parameters used in the substitution for variables defined in *msg_str* or the message corresponding to *msg_id*. There can be 0 or more substitution parameters, but the total number of substitution parameters cannot exceed 20. Each substitution parameter can be a local variable or any of these data types: **tinyint**, **smallint**, **int**, **char**, **varchar**, **nchar**, **nvarchar**, **binary**, or **varbinary**. No other data types are supported.

#### *option*

A custom option for the error and can be one of the values in the following table.

|Value|Description|
|-----------|-----------------|
|`LOG`|Logs the error in the error log and the application log for the instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Errors logged in the error log are currently limited to a maximum of 440 bytes. Only a member of the sysadmin fixed server role or a user with ALTER TRACE permissions can specify WITH LOG.<br /><br /> [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|
|`NOWAIT`|Sends messages immediately to the client.<br /><br /> [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssSDS](../../includes/sssds-md.md)]|
|`SETERROR`|Sets the `@@ERROR` and `ERROR_NUMBER` values to *msg_id* or 50000, regardless of the severity level.<br /><br /> [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssSDS](../../includes/sssds-md.md)]|

## Remarks

The errors generated by `RAISERROR` operate the same as errors generated by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] code. The values specified by `RAISERROR` are reported by the `ERROR_LINE`, `ERROR_MESSAGE`, `ERROR_NUMBER`, `ERROR_PROCEDURE`, `ERROR_SEVERITY`, `ERROR_STATE`, and `@@ERROR` system functions. When `RAISERROR` is run with a severity of 11 or higher in a TRY block, it transfers control to the associated `CATCH` block. The error is returned to the caller if `RAISERROR` is run:

- Outside the scope of any `TRY` block.

- With a severity of 10 or lower in a `TRY` block.

- With a severity of 20 or higher that terminates the database connection.

`CATCH` blocks can use `RAISERROR` to rethrow the error that invoked the `CATCH` block by using system functions such as `ERROR_NUMBER` and `ERROR_MESSAGE` to retrieve the original error information. `@@ERROR` is set to 0 by default for messages with a severity from 1 through 10.

When *msg_id* specifies a user-defined message available from the sys.messages catalog view, `RAISERROR` processes the message from the text column using the same rules as are applied to the text of a user-defined message specified using *msg_str*. The user-defined message text can contain conversion specifications, and `RAISERROR` will map argument values into the conversion specifications. Use `sp_addmessage` to add user-defined error messages and `sp_dropmessage` to delete user-defined error messages.

`RAISERROR` can be used as an alternative to PRINT to return messages to calling applications. `RAISERROR` supports character substitution similar to the functionality of the `printf` function in the C standard library, while the [!INCLUDE[tsql](../../includes/tsql-md.md)] `PRINT` statement does not. The `PRINT` statement is not affected by `TRY` blocks, while a `RAISERROR` run with a severity of 11 to 19 in a TRY block transfers control to the associated `CATCH` block. Specify a severity of 10 or lower to use `RAISERROR` to return a message from a `TRY` block without invoking the `CATCH` block.

Typically, successive arguments replace successive conversion specifications; the first argument replaces the first conversion specification, the second argument replaces the second conversion specification, and so on. For example, in the following `RAISERROR` statement, the first argument of `N'number'` replaces the first conversion specification of `%s`; and the second argument of `5` replaces the second conversion specification of `%d.`

```sql
RAISERROR (N'This is message %s %d.', -- Message text.
           10, -- Severity,
           1, -- State,
           N'number', -- First argument.
           5); -- Second argument.
-- The message text returned is: This is message number 5.
GO
```

If an asterisk (`*`) is specified for either the width or precision of a conversion specification, the value to be used for the width or precision is specified as an integer argument value. In this case, one conversion specification can use up to three arguments, one each for the width, precision, and substitution value.

For example, both of the following `RAISERROR` statements return the same string. One specifies the width and precision values in the argument list; the other specifies them in the conversion specification.

```sql
RAISERROR (N'<\<%*.*s>>', -- Message text.
           10, -- Severity,
           1, -- State,
           7, -- First argument used for width.
           3, -- Second argument used for precision.
           N'abcde'); -- Third argument supplies the string.
-- The message text returned is: <<    abc>>.
GO
RAISERROR (N'<\<%7.3s>>', -- Message text.
           10, -- Severity,
           1, -- State,
           N'abcde'); -- First argument supplies the string.
-- The message text returned is: <<    abc>>.
GO
```

## Permissions

Severity levels from 0 through 18 can be specified by any user. Severity levels from 19 through 25 can only be specified by members of the sysadmin fixed server role or users with ALTER TRACE permissions.

## Examples

### A. Returning error information from a CATCH block

The following code example shows how to use `RAISERROR` inside a `TRY` block to cause execution to jump to the associated `CATCH` block. It also shows how to use `RAISERROR` to return information about the error that invoked the `CATCH` block.

> [!NOTE]  
> `RAISERROR` only generates errors with state from 1 through 127. Because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] may raise errors with state 0, we recommend that you check the error state returned by ERROR_STATE before passing it as a value to the state parameter of `RAISERROR`.

```sql
BEGIN TRY
    -- RAISERROR with severity 11-19 will cause execution to
    -- jump to the CATCH block.
    RAISERROR ('Error raised in TRY block.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
END TRY
BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

    -- Use RAISERROR inside the CATCH block to return error
    -- information about the original error that caused
    -- execution to jump to the CATCH block.
    RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );
END CATCH;
```

### B. Creating an ad hoc message in sys.messages

The following example shows how to raise a message stored in the sys.messages catalog view. The message was added to the sys.messages catalog view by using the `sp_addmessage` system stored procedure as message number `50005`.

```sql
EXEC sp_addmessage @msgnum = 50005,
              @severity = 10,
              @msgtext = N'<\<%7.3s>>';
GO
RAISERROR (50005, -- Message id.
           10, -- Severity,
           1, -- State,
           N'abcde'); -- First argument supplies the string.
-- The message text returned is: <<    abc>>.
GO
EXEC sp_dropmessage @msgnum = 50005;
GO
```

### C. Using a local variable to supply the message text

The following code example shows how to use a local variable to supply the message text for a `RAISERROR` statement.

```sql
DECLARE @StringVariable NVARCHAR(50);
SET @StringVariable = N'<\<%7.3s>>';

RAISERROR (@StringVariable, -- Message text.
           10, -- Severity,
           1, -- State,
           N'abcde'); -- First argument supplies the string.
-- The message text returned is: <<    abc>>.
GO
```

## See also

- [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)
- [DECLARE @local_variable (Transact-SQL)](../../t-sql/language-elements/declare-local-variable-transact-sql.md)
- [PRINT &#40;Transact-SQL&#41;](../../t-sql/language-elements/print-transact-sql.md)
- [sp_addmessage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmessage-transact-sql.md)
- [sp_dropmessage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmessage-transact-sql.md)
- [sys.messages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)
- [xp_logevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xp-logevent-transact-sql.md)
- [@@ERROR &#40;Transact-SQL&#41;](../../t-sql/functions/error-transact-sql.md)
- [ERROR_LINE &#40;Transact-SQL&#41;](../../t-sql/functions/error-line-transact-sql.md)
- [ERROR_MESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/error-message-transact-sql.md)
- [ERROR_NUMBER &#40;Transact-SQL&#41;](../../t-sql/functions/error-number-transact-sql.md)
- [ERROR_PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/functions/error-procedure-transact-sql.md)
- [ERROR_SEVERITY &#40;Transact-SQL&#41;](../../t-sql/functions/error-severity-transact-sql.md)
- [ERROR_STATE &#40;Transact-SQL&#41;](../../t-sql/functions/error-state-transact-sql.md)
- [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md)
