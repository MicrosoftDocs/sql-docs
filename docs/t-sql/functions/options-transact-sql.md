---
title: "@@OPTIONS (Transact-SQL)"
description: "Returns information about the current SET options."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 09/08/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@OPTIONS"
  - "@@OPTIONS_TSQL"
helpviewer_keywords:
  - "SET statement, current SET options"
  - "@@OPTIONS function"
  - "current SET options"
dev_langs:
  - "TSQL"
---
# @@OPTIONS (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns information about the current SET options.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
@@OPTIONS
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return type

**integer**

## Remarks

The options can come from use of the `SET` command or from the `sp_configure user options` value. Session values configured with the `SET` command override the `sp_configure` options. Many tools, such as [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], automatically configure set options. Each user has an `@@OPTIONS` function that represents the configuration.

You can change the language and query-processing options for a specific user session by using the `SET` statement. `@@OPTIONS` can only detect the options that are set to ON or OFF.

The `@@OPTIONS` function returns a bitmap of the options, converted to a base 10 (decimal) integer. The bit settings are stored in the locations described in a table in the article [Configure the user options Server Configuration Option](../../database-engine/configure-windows/configure-the-user-options-server-configuration-option.md).

To decode the `@@OPTIONS` value, convert the integer returned by `@@OPTIONS` to binary, and then look up the values on the table at [Configure the user options Server Configuration Option](../../database-engine/configure-windows/configure-the-user-options-server-configuration-option.md). For example, if `SELECT @@OPTIONS;` returns the value `5496`, use the Windows programmer calculator (**calc.exe**) to convert decimal `5496` to binary. The result is `1010101111000`. The rightmost characters (binary 1, 2, and 4) are 0, indicating that the first three items in the table are off. Consulting the table, you see that those are `DISABLE_DEF_CNST_CHK`, `IMPLICIT_TRANSACTIONS`, and `CURSOR_CLOSE_ON_COMMIT`. The next item (`ANSI_WARNINGS` in the `1000` position) is on. Continue working left through the bit map, and down in the list of options. When the left-most options are 0, they are truncated by the type conversion. The bit map `1010101111000` is actually `001010101111000` to represent all 15 options.

[Example C](#c-review-options-bitmask-with-a-pivot-query) provides a query that automatically maps the `@@OPTIONS` bitmask to user options.

## Examples

### A. Demonstration of how changes affect behavior

The following example demonstrates the difference in concatenation behavior with two different setting of the `CONCAT_NULL_YIELDS_NULL` option.

```sql
SELECT @@OPTIONS AS OriginalOptionsValue;
SET CONCAT_NULL_YIELDS_NULL OFF;
SELECT 'abc' + NULL AS ResultWhen_OFF, @@OPTIONS AS OptionsValueWhen_OFF;
  
SET CONCAT_NULL_YIELDS_NULL ON;
SELECT 'abc' + NULL AS ResultWhen_ON, @@OPTIONS AS OptionsValueWhen_ON;
```

### B. Test a client NOCOUNT setting

The following example sets `NOCOUNT``ON` and then tests the value of `@@OPTIONS`. The `NOCOUNT``ON` option prevents the message about the number of rows affected from being sent back to the requesting client for every statement in a session. The value of `@@OPTIONS` is set to `512` (0x0200). This represents the NOCOUNT option. This example tests whether the NOCOUNT option is enabled on the client. For example, it can help track performance differences on a client.

```sql
SET NOCOUNT ON
IF @@OPTIONS & 512 > 0
RAISERROR ('Current user has SET NOCOUNT turned on.', 1, 1)
```

### C. Review @@OPTIONS bitmask with a PIVOT query

The following example uses table-valued constructors to generate a numbers-list reference and then compares the value of `@@OPTIONS` with a bitwise operator. An APPLY clause performs string concatenation to generate a character bitmask, and another generates aliases to review against the documented values from [Configure the user options Server Configuration Option](../../database-engine/configure-windows/configure-the-user-options-server-configuration-option.md).

```sql
SELECT S.Bits,
    FLAGS.*
FROM (
    SELECT optRef,
        posRef,
        flagCheck
    FROM (
        SELECT ones.n + tens.n * 10
        FROM ( VALUES (0), (1), (2), (3), (4), (5), (6), (7), (8), (9) ) ones(n),
            ( VALUES (0), (1), (2), (3), (4), (5), (6), (7), (8), (9) ) tens(n)
        ) f1(powRef)
    CROSS APPLY (
        SELECT POWER(2, powRef)
        WHERE powRef <= 16
        ) f2(binRef)
    CROSS JOIN (
        VALUES (@@OPTIONS)
        ) f3(optRef)
    CROSS APPLY (
        SELECT (optRef & binRef) / binRef
        ) f4(flagRef)
    CROSS APPLY (
        SELECT RIGHT(CONVERT(VARCHAR(2), CAST(powRef AS VARBINARY(1)), 2), 1) [posRef],
            CAST(flagRef AS INT) [flagCheck]
        ) pref
    ) TP
PIVOT( MAX( flagCheck ) FOR posRef IN ( [0], [1], [2], [3], [4], [5], [6], [7], [8], [9], [A], [B], [C], [D], [E], [F] )) P
CROSS APPLY (
    SELECT CONCAT ( '', [0], [1], [2], [3], [4], [5], [6], [7], [8], [9], [A], [B], [C], [D], [E], [F] ),
        CONCAT ( '', [F], [E], [D], [C], [B], [A], [9], [8], [7], [6], [5], [4], [3], [2], [1], [0] )
    ) S (stib, Bits)
CROSS APPLY (
    SELECT
          CAST(P.[0] AS BIT) /* 1     */ [DISABLE_DEF_CNST_CHK] -- Controls interim or deferred constraint checking.
        , CAST(P.[1] AS BIT) /* 2     */ [IMPLICIT_TRANSACTIONS] -- For dblib network library connections, controls whether a transaction is started implicitly when a statement is executed. The IMPLICIT_TRANSACTIONS setting has no effect on ODBC or OLEDB connections.
        , CAST(P.[2] AS BIT) /* 4     */ [CURSOR_CLOSE_ON_COMMIT] -- Controls behavior of cursors after a commit operation has been performed.
        , CAST(P.[3] AS BIT) /* 8     */ [ANSI_WARNINGS] -- Controls truncation and NULL in aggregate warnings.
        , CAST(P.[4] AS BIT) /* 16    */ [ANSI_PADDING] -- Controls padding of fixed-length variables.
        , CAST(P.[5] AS BIT) /* 32    */ [ANSI_NULLS] -- Controls NULL handling when using equality operators.
        , CAST(P.[6] AS BIT) /* 64    */ [ARITHABORT] -- Terminates a query when an overflow or divide-by-zero error occurs during query execution.
        , CAST(P.[7] AS BIT) /* 128   */ [ARITHIGNORE] -- Returns NULL when an overflow or divide-by-zero error occurs during a query.
        , CAST(P.[8] AS BIT) /* 256   */ [QUOTED_IDENTIFIER] -- Differentiates between single and double quotation marks when evaluating an expression.
        , CAST(P.[9] AS BIT) /* 512   */ [NOCOUNT] -- Turns off the message returned at the end of each statement that states how many rows were affected.
        , CAST(P.[A] AS BIT) /* 1024  */ [ANSI_NULL_DFLT_ON] -- Alters the session's behavior to use ANSI compatibility for nullability. New columns defined without explicit nullability are defined to allow nulls.
        , CAST(P.[B] AS BIT) /* 2048  */ [ANSI_NULL_DFLT_OFF] -- Alters the session's behavior not to use ANSI compatibility for nullability. New columns defined without explicit nullability do not allow nulls.
        , CAST(P.[C] AS BIT) /* 4096  */ [CONCAT_NULL_YIELDS_NULL] -- Returns NULL when concatenating a NULL value with a string.
        , CAST(P.[D] AS BIT) /* 8192  */ [NUMERIC_ROUNDABORT] -- Generates an error when a loss of precision occurs in an expression.
        , CAST(P.[E] AS BIT) /* 16384 */ [XACT_ABORT] -- Rolls back a transaction if a Transact-SQL statement raises a run-time error.*/
    ) AS Flags;
```

## See also

- [Configuration Functions (Transact-SQL)](../../t-sql/functions/configuration-functions-transact-sql.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Configure the user options Server Configuration Option](../../database-engine/configure-windows/configure-the-user-options-server-configuration-option.md)
