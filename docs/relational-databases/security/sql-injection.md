---
title: "SQL injection"
description: Learn how SQL injection attacks work. Mitigate such attacks by validating input and reviewing code for SQL injection in SQL Server.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 05/03/2024
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "SQL Injection"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# SQL injection

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

SQL injection is an attack in which malicious code is inserted into strings that are later passed to an instance of the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] for parsing and execution. Any procedure that constructs SQL statements should be reviewed for injection vulnerabilities, because the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] executes all syntactically valid queries that it receives. Even parameterized data can be manipulated by a skilled and determined attacker.

## How SQL injection works

The primary form of SQL injection consists of direct insertion of code into user-input variables that are concatenated with SQL commands and executed. A less direct attack injects malicious code into strings that are destined for storage in a table or as metadata. When the stored strings are then concatenated into a dynamic SQL command, the malicious code is executed.

The injection process works by prematurely terminating a text string and appending a new command. Because the inserted command might have extra strings appended to it before it executes, the malefactor terminates the injected string with a comment mark `--`. Subsequent text is ignored at execution time.

The following script shows a simple SQL injection. The script builds an SQL query by concatenating hard-coded strings together with a string entered by the user:

```csharp
var ShipCity;
ShipCity = Request.form ("ShipCity");
var sql = "select * from OrdersTable where ShipCity = '" + ShipCity + "'";
```

The user is prompted to enter the name of a city. If they enter `Redmond`, the query assembled by the script looks similar to the following example:

```sql
SELECT * FROM OrdersTable WHERE ShipCity = 'Redmond';
```

However, assume that the user enters the following text:

```sql
Redmond';drop table OrdersTable--
```

In this case, the script assembles the following query:

```sql
SELECT * FROM OrdersTable WHERE ShipCity = 'Redmond';drop table OrdersTable--'
```

The semicolon (`;`) denotes the end of one query and the start of another. The double hyphen (`--`) indicates that the rest of the current line is a comment and should be ignored. If the modified code is syntactically correct, it's executed by the server. When the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] processes this statement, it first selects all records in `OrdersTable` where `ShipCity` is `Redmond`. Then, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] drops `OrdersTable`.

As long as injected SQL code is syntactically correct, tampering can't be detected programmatically. Therefore, you must validate all user input and carefully review code that executes constructed SQL commands in the server that you use. Coding best practices are described in the following sections in this article.

## Validate all input

Always validate user input by testing type, length, format, and range. When you implement precautions against malicious input, consider the architecture and deployment scenarios of your application. Remember that programs designed to run in a secure environment can be copied to a nonsecure environment. The following suggestions should be considered best practices:

- Make no assumptions about the size, type, or content of the data that is received by your application. For example, you should make the following evaluation:

  - How does your application behave if an errant or malicious user enters a 2-GB video file where your application expects a postal code?

  - How does your application behave if a `DROP TABLE` statement is embedded in a text field?

- Test the size and data type of input and enforce appropriate limits. This can help prevent deliberate buffer overruns.

- Test the content of string variables and accept only expected values. Reject entries that contain binary data, escape sequences, and comment characters. This can help prevent script injection and can protect against some buffer overrun exploits.

- When you work with XML documents, validate all data against its schema as it is entered.

- Never build [!INCLUDE [tsql](../../includes/tsql-md.md)] statements directly from user input.

- Use stored procedures to validate user input.

- In multitiered environments, all data should be validated before admission to the trusted zone. Data that doesn't pass the validation process should be rejected and an error should be returned to the previous tier.

- Implement multiple layers of validation. Precautions you take against casually malicious users might be ineffective against determined attackers. A better practice is to validate input in the user interface and at all subsequent points where it crosses a trust boundary.

  For example, data validation in a client-side application can prevent simple script injection. However, if the next tier assumes that its input is already validated, any malicious user who can bypass a client can have unrestricted access to a system.

- Never concatenate user input that isn't validated. String concatenation is the primary point of entry for script injection.

- Don't accept the following strings in fields from which file names can be constructed: `AUX`, `CLOCK$`, `COM1` through `COM8`, `CON`, `CONFIG$`, `LPT1` through `LPT8`, `NUL`, and `PRN`.

When you can, reject input that contains the following characters.

| Input character | Meaning in Transact-SQL |
| --- | --- |
| `;` | Query delimiter. |
| `'` | Character data string delimiter. |
| `--` | Single-line comment delimiter. Text following `--` until the end of that line isn't evaluated by the server. |
| `/*** ... ***/` | Comment delimiters. Text between `/*` and `*/` isn't evaluated by the server. |
| `xp_` | Used at the start of the name of catalog-extended stored procedures, such as `xp_cmdshell`. |

### Use type-safe SQL parameters

The `Parameters` collection in the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] provides type checking and length validation. If you use the `Parameters` collection, input is treated as a literal value instead of as executable code. Another benefit of using the `Parameters` collection is that you can enforce type and length checks. Values outside the range trigger an exception. The following code fragment shows using the `Parameters` collection:

```csharp
SqlDataAdapter myCommand = new SqlDataAdapter("AuthorLogin", conn);
myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
SqlParameter parm = myCommand.SelectCommand.Parameters.Add("@au_id",
    SqlDbType.VarChar, 11);
parm.Value = Login.Text;
```

In this example, the `@au_id` parameter is treated as a literal value instead of as executable code. This value is checked for type and length. If the value of `@au_id` doesn't comply with the specified type and length constraints, an exception is thrown.

### Use parameterized input with stored procedures

Stored procedures might be susceptible to SQL injection if they use unfiltered input. For example, the following code is vulnerable:

```csharp
SqlDataAdapter myCommand =
    new SqlDataAdapter("LoginStoredProcedure '" + Login.Text + "'", conn);
```

If you use stored procedures, you should use parameters as their input.

### Use the Parameters collection with dynamic SQL

If you can't use stored procedures, you can still use parameters, as shown in the following code example.

```csharp
SqlDataAdapter myCommand = new SqlDataAdapter(
    "SELECT au_lname, au_fname FROM Authors WHERE au_id = @au_id", conn);
SqlParameter parm = myCommand.SelectCommand.Parameters.Add("@au_id",
    SqlDbType.VarChar, 11);
parm.Value = Login.Text;
```

### <a id="filtering-input"></a> Filter input

Filtering input might also be helpful in protecting against SQL injection by removing escape characters. However, because of the large number of characters that might pose problems, filtering isn't a reliable defense. The following example searches for the character string delimiter.

```csharp
private string SafeSqlLiteral(string inputSQL)
{
    return inputSQL.Replace("'", "''");
}
```

### LIKE clauses

If you use a `LIKE` clause, wildcard characters still must be escaped:

```csharp
s = s.Replace("[", "[[]");
s = s.Replace("%", "[%]");
s = s.Replace("_", "[_]");
```

## <a id="reviewing-code-for-sql-injection"></a> Review code for SQL injection

You should review all code that calls `EXECUTE`, `EXEC`, or `sp_executesql`. You can use queries similar to the following to help you identify procedures that contain these statements. This query checks for 1, 2, 3, or 4 spaces after the words `EXECUTE` or `EXEC`.

```sql
SELECT object_Name(id)
FROM syscomments
WHERE UPPER(TEXT) LIKE '%EXECUTE (%'
    OR UPPER(TEXT) LIKE '%EXECUTE  (%'
    OR UPPER(TEXT) LIKE '%EXECUTE   (%'
    OR UPPER(TEXT) LIKE '%EXECUTE    (%'
    OR UPPER(TEXT) LIKE '%EXEC (%'
    OR UPPER(TEXT) LIKE '%EXEC  (%'
    OR UPPER(TEXT) LIKE '%EXEC   (%'
    OR UPPER(TEXT) LIKE '%EXEC    (%'
    OR UPPER(TEXT) LIKE '%SP_EXECUTESQL%';
```

### <a id="wrapping-parameters-with-quotename-and-replace"></a> Wrap parameters with QUOTENAME() and REPLACE()

In each selected stored procedure, verify that all variables that are used in dynamic Transact-SQL are handled correctly. Data that comes from the input parameters of the stored procedure or that is read from a table should be wrapped in `QUOTENAME()` or `REPLACE()`. Remember that the value of *@variable* that is passed to `QUOTENAME()` is of **sysname**, and has a maximum length of 128 characters.

| *@variable* | Recommended wrapper |
| --- | --- |
| **Name of a securable** | `QUOTENAME(@variable)` |
| **String of <= 128 characters** | `QUOTENAME(@variable, '''')` |
| **String of > 128 characters** | `REPLACE(@variable,'''', '''''')` |

When you use this technique, a `SET` statement can be revised as follows:

```sql
-- Before:
SET @temp = N'SELECT * FROM authors WHERE au_lname ='''
    + @au_lname + N'''';

-- After:
SET @temp = N'SELECT * FROM authors WHERE au_lname = '''
    + REPLACE(@au_lname, '''', '''''') + N'''';
```

### Injection enabled by data truncation

Any dynamic [!INCLUDE [tsql](../../includes/tsql-md.md)] that is assigned to a variable, is truncated if it's larger than the buffer allocated for that variable. An attacker who is able to force statement truncation by passing unexpectedly long strings to a stored procedure can manipulate the result. For example, the following example stored procedure is vulnerable to injection enabled by truncation.

In this example, we have a `@command` buffer with a maximum length of 200 characters. We need a total of 154 characters to set the password of `'sa'`: 26 for the `UPDATE` statement, 16 for the `WHERE` clause, 4 for `'sa'`, and 2 for quotation marks surrounded by `QUOTENAME(@loginname)`: 200 - 26 - 16 - 4 - 2 = 154. But, because `@new` is declared as **sysname**, this variable can only hold 128 characters. We can overcome this by passing some single quotation marks in `@new`.

```sql
CREATE PROCEDURE sp_MySetPassword
    @loginname SYSNAME,
    @old SYSNAME,
    @new SYSNAME
AS
-- Declare variable.
DECLARE @command VARCHAR(200)

-- Construct the dynamic Transact-SQL.
SET @command = 'UPDATE Users SET password=' + QUOTENAME(@new, '''')
    + ' WHERE username=' + QUOTENAME(@loginname, '''') 
    + ' AND password=' + QUOTENAME(@old, '''')

-- Execute the command.
EXEC (@command);
GO
```

If an attacker passes 154 characters into a 128-character buffer, they can set a new password for `sa` without knowing the old password.

```sql
EXEC sp_MySetPassword 'sa',
    'dummy',
    '123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012'''''''''''''''''''''''''''''''''''''''''''''''''''
```

For this reason, you should use a large buffer for a command variable or directly execute the dynamic [!INCLUDE [tsql](../../includes/tsql-md.md)] inside the `EXECUTE` statement.

### Truncation when QUOTENAME(@variable, '''') and REPLACE() are used

Strings returned by `QUOTENAME()` and `REPLACE()` are silently truncated if they exceed the space that is allocated. The stored procedure that is created in the following example shows what can happen.

In this example, the data stored in temporary variables is truncated, because the buffer size of `@login`, `@oldpassword`, and `@newpassword` is only 128 characters, but `QUOTENAME()` can return up to 258 characters. If `@new` contains 128 characters, then `@newpassword` can be `123... n`, where `n` is the 127th character. Because the string returned by `QUOTENAME()` is truncated, it can be made to look like the following statement:

`UPDATE Users SET password ='1234...[127] WHERE username=' -- other stuff here`

```sql
CREATE PROCEDURE sp_MySetPassword
    @loginname SYSNAME,
    @old SYSNAME,
    @new SYSNAME
AS
-- Declare variables.
DECLARE @login SYSNAME;
DECLARE @newpassword SYSNAME;
DECLARE @oldpassword SYSNAME;
DECLARE @command VARCHAR(2000);

SET @login = QUOTENAME(@loginname, '''');
SET @oldpassword = QUOTENAME(@old, '''');
SET @newpassword = QUOTENAME(@new, '''');

-- Construct the dynamic Transact-SQL.
SET @command = 'UPDATE Users set password = ' + @newpassword
    + ' WHERE username = ' + @login
    + ' AND password = ' + @oldpassword;

-- Execute the command.
EXEC (@command);
GO
```

Therefore, the following statement sets the passwords of all users to the value that was passed in the previous code.

```sql
EXEC sp_MyProc '--', 'dummy', '12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678'
```

You can force string truncation by exceeding the allocated buffer space when you use `REPLACE()`. The stored procedure that is created in the following example shows what can happen.

In this example, data is truncated because the buffers allocated for `@login`, `@oldpassword` and `@newpassword` can hold only 128 characters, but `QUOTENAME()` can return up to 258 characters. If `@new` contains 128 characters, `@newpassword` can be `'123...n'`, where `n` is the 127th character. Because the string returned by `QUOTENAME()` is truncated, it can be made to look like the following statement:

`UPDATE Users SET password='1234...[127] WHERE username=' -- other stuff here`

```sql
CREATE PROCEDURE sp_MySetPassword
    @loginname SYSNAME,
    @old SYSNAME,
    @new SYSNAME
AS
-- Declare variables.
DECLARE @login SYSNAME;
DECLARE @newpassword SYSNAME;
DECLARE @oldpassword SYSNAME;
DECLARE @command VARCHAR(2000);

SET @login = REPLACE(@loginname, '''', '''''');
SET @oldpassword = REPLACE(@old, '''', '''''');
SET @newpassword = REPLACE(@new, '''', '''''');

-- Construct the dynamic Transact-SQL.
SET @command = 'UPDATE Users SET password = '''
    + @newpassword + ''' WHERE username = '''
    + @login + ''' AND password = ''' + @oldpassword + '''';

-- Execute the command.
EXEC (@command);
GO
```

As with `QUOTENAME()`, string truncation by `REPLACE()` can be avoided by declaring temporary variables that are large enough for all cases. When possible, you should call `QUOTENAME()` or `REPLACE()` directly inside the dynamic [!INCLUDE [tsql](../../includes/tsql-md.md)]. Otherwise, you can calculate the required buffer size as follows. For `@outbuffer = QUOTENAME(@input)`, the size of `@outbuffer` should be `2 * (len(@input) + 1)`. When you use `REPLACE()` and doubling quotation marks, as in the previous example, a buffer of `2 * len(@input)` is enough.

The following calculation covers all cases:

```sql
WHILE LEN(@find_string) > 0, required buffer size =
    ROUND(LEN(@input) / LEN(@find_string), 0)
        * LEN(@new_string) + (LEN(@input) % LEN(@find_string))
```

### Truncation when QUOTENAME(@variable, ']') is used

Truncation can occur when the name of a [!INCLUDE [ssde-md](../../includes/ssde-md.md)] securable is passed to statements that use the form `QUOTENAME(@variable, ']')`. The following example demonstrates this scenario.

In this example, `@objectname` must allow for 2 * 258 + 1 characters.

```sql
CREATE PROCEDURE sp_MyProc
    @schemaname SYSNAME,
    @tablename SYSNAME
AS
-- Declare a variable as sysname. The variable will be 128 characters.
DECLARE @objectname SYSNAME;

SET @objectname = QUOTENAME(@schemaname) + '.' + QUOTENAME(@tablename);
    -- Do some operations.
GO
```

When you concatenate values of type **sysname**, you should use temporary variables large enough to hold the maximum 128 characters per value. If possible, call `QUOTENAME()` directly inside the dynamic [!INCLUDE [tsql](../../includes/tsql-md.md)]. Otherwise, you can calculate the required buffer size as explained in the previous section.

## Related content

- [EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md)
- [REPLACE (Transact-SQL)](../../t-sql/functions/replace-transact-sql.md)
- [QUOTENAME (Transact-SQL)](../../t-sql/functions/quotename-transact-sql.md)
- [sp_executesql (Transact-SQL)](../system-stored-procedures/sp-executesql-transact-sql.md)
- [Securing SQL Server](securing-sql-server.md)
