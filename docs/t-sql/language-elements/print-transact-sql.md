---
title: "PRINT (Transact-SQL)"
description: "PRINT (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/16/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "PRINT_TSQL"
  - "PRINT"
helpviewer_keywords:
  - "PRINT statement"
  - "user-defined messages [SQL Server]"
  - "messages [SQL Server], PRINT statement"
  - "displaying user-defined messages"
  - "viewing user-defined messages"
  - "conditionally returning messages [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---

# PRINT (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns a user-defined message to the client. For example, in [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)], `PRINT` outputs to the Messages tab of the query results window.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
PRINT msg_str | @local_variable | string_expr
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *msg_str*

A character string or Unicode string constant. For more information, see [Constants](../data-types/constants-transact-sql.md).

#### *@local_variable*

A variable of any valid character data type. *@local_variable* must be **char**, **nchar**, **varchar**, or **nvarchar**, or it must be able to be implicitly converted to those data types.

#### *string_expr*

An expression that returns a string. Can include concatenated literal values, functions, and variables. For more information, see [Expressions](expressions-transact-sql.md).

## Remarks

A message string can be up to 8,000 characters long if it's a non-Unicode string, and 4,000 characters long if it's a Unicode string. Longer strings are truncated. The **varchar(max)** and **nvarchar(max)** data types are truncated to data types that are no larger than **varchar(8000)** and **nvarchar(4000)**.

[RAISERROR](raiserror-transact-sql.md) can also be used to return messages. `RAISERROR` has these advantages over `PRINT`:

- `RAISERROR` supports substituting arguments into an error message string using a mechanism modeled on the printf function of the C language standard library.

- `RAISERROR` can specify a unique error number, a severity, and a state code in addition to the text message.

- `RAISERROR` can be used to return user-defined messages created using the [sp_addmessage (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addmessage-transact-sql.md) system stored procedure.

## Examples

### A. Conditionally executing print (IF EXISTS)

The following example uses the `PRINT` statement to conditionally return a message.

```sql
IF @@OPTIONS & 512 <> 0
    PRINT N'This user has SET NOCOUNT turned ON.';
ELSE
    PRINT N'This user has SET NOCOUNT turned OFF.';
GO
```

### B. Build and display a string

The following example converts the results of the [GETDATE](../functions/getdate-transact-sql.md) function to a **nvarchar** data type, and concatenates it with literal text, which is returned by `PRINT`.

```sql
PRINT N'This message was printed on ' + RTRIM(CAST(GETDATE() AS NVARCHAR(30))) + N'.';
GO
```

The following example shows building the message text in a variable.

```sql
DECLARE @PrintMessage NVARCHAR(50);
SET @PrintMessage = N'This message was printed on ' + RTRIM(CAST(GETDATE() AS NVARCHAR(30))) + N'.';
PRINT @PrintMessage;
GO
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

### C. Conditionally execute print

The following example uses the `PRINT` statement to conditionally return a message.

```sql
IF DB_ID() = 1
    PRINT N'The current database is ''master''.';
ELSE
    PRINT N'The current database is not ''master''.';
GO
```

## Related content

- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [DECLARE @local_variable (Transact-SQL)](declare-local-variable-transact-sql.md)
- [RAISERROR (Transact-SQL)](raiserror-transact-sql.md)
