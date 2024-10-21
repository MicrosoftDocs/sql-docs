---
title: "Project Settings (Conversion) (Db2ToSQL)"
description: Customize how SSMA converts Db2 syntax to SQL Server syntax.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.db2.projectsettingconvertion.f1"
---
# Project Settings (Conversion) (Db2ToSQL)

The Conversion page of the **Project Settings** dialog box contains settings that customize how SQL Server Migration Assistant (SSMA) converts Db2 syntax to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] syntax.

The Conversion pane is available in the **Project Settings** and **Default Project Settings** dialog boxes:

- To specify settings for all SSMA projects, on the **Tools** menu select **Default Project Settings**, select migration project type for which settings are required to be viewed or changed from **Migration Target Version** dropdown list, then select **General** at the bottom of the left pane, and then select **Conversion**.

- To specify settings for the current project, on the **Tools** menu select **Project Settings**, then select **General** at the bottom of the left pane, and then select **Conversion**.

## Conversion messages

### Generate messages about issues applied

Specifies whether SSMA generates informational messages during conversion, displays them in the Output pane, and adds them to the converted code.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic Mode:** No

**Full Mode:** No

## Miscellaneous options

### Cast ROWNUM expressions as integers

When SSMA converts ROWNUM expressions, it converts the expression into a TOP clause, followed by the expression. The following example shows ROWNUM in an Db2 DELETE statement:

```sql
DELETE FROM Table1
WHERE ROWNUM < expression
    AND Field1 >= 2
```

The following example shows the resulting [!INCLUDE [tsql](../../includes/tsql-md.md)]:

```sql
DELETE TOP (expression - 1)
FROM Table1
WHERE Field1 >= 2
```

The TOP requires that the TOP clauses expression evaluates to an integer. If the integer is negative, the statement produces an error.

- If you select **Yes**, SSMA casts the expression as an integer.

- If you select **No**, SSMA marks all non-integer expressions as an error in the converted code.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Full Mode:** No

**Optimistic Mode:** Yes

### Default schema mapping

This setting specifies how Db2 schemas are mapped to SQL Server schemas. Two options are available in this setting:

1. **Schema to database:** In this mode, the Db2 schema `sch1` is mapped by default to `dbo` SQL Server schema in SQL Server database `sch1`.

1. **Schema to schema:** In this mode, the Db2 schema `sch1` is mapped by default to `sch1` SQL Server schema in default SQL Server database provided in the connection dialog.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Schema to database

### Conversion ways of MERGE statement

- If you select **Using INSERT, UPDATE, DELETE statement**, SSMA converts MERGER statement into INSERT, UPDATE, DELETE statements.

- If you select **Using MERGE statement**, SSMA converts MERGER statement into MERGE statement in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Using MERGE statement

### Convert calls to subprograms that use default arguments

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] functions don't support the omission of parameters in the function call. Also, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] functions and procedures don't support expressions as default parameter values.

- If you select **Yes** and a function call omits parameters, SSMA inserts the keyword **default** into the function and call in the correct position. Then, it marks the call with a warning.

- If you select **No**, SSMA marks the function calls as errors.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Yes

### Convert COUNT function to COUNT_BIG

If your COUNT functions are likely to return values larger than 2,147,483,647, which is 2<sup>31</sup>-1, you should convert the functions to COUNT_BIG.

- If you select **Yes**, SSMA converts all uses of COUNT to COUNT_BIG.

- If you select **No**, the functions remain as COUNT. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns an error if the function returns a value larger than 2<sup>31</sup>-1.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Full Mode:** Yes

**Optimistic Mode:** No

### Convert FORALL statement to WHILE statement

Defines how SSMA will treat FORALL loops on PL/SQL collection elements.

- If you select **Yes**, SSMA creates a WHILE loop where collection elements are retrieved one by one.

- If you select **No**, SSMA generates a rowset from the collection using nodes( ) method and uses it as a single table. This is more efficient, but makes the output code less readable.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic Mode:** No

**Full Mode:** Yes

### Convert foreign keys with SET NULL referential action on column that is NOT NULL

Db2 allows creating foreign key constraints, where a SET `NULL` action couldn't possibly be performed because NULLs aren't permitted in the referenced column. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't allow such foreign key configuration.

- If you select **Yes**, SSMA generates referential actions as in Db2, but you need to make manual changes before loading the constraint to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For example, you can choose NO ACTION instead of SET `NULL`.

- If you select **No**, the constraint is marked as an error.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** No

### Convert function calls to procedure calls

Some Db2 functions are defined as autonomous transactions or contain statements that wouldn't be valid in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. In these cases, SSMA creates a procedure and a function that is a wrapper for the procedure. The converted function calls the implementing procedure.

SSMA can convert calls to the wrapper function into calls to the procedure. This creates more readable code and can improve performance. However, the context doesn't always allow it; for example, you can't replace a function call in SELECT list with a procedure call. SSMA has a few options to cover the common cases:

- If you select **Always**, SSMA attempts to convert wrapper function calls into procedure calls. If the current context doesn't allow this conversion, an error message is produced. This way, no function calls are left in the generated code.

- If you select **When possible**, SSMA makes a move to procedure calls only if the function has output parameters. When the move isn't possible, parameter's output attribute is removed. In all other cases SSMA leaves function calls.

- If you select **Never**, SSMA leaves all function calls as function calls. Sometimes this choice might be unacceptable because of performance reasons.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** When possible

### Convert LOCK TABLE statements

SSMA can convert many LOCK TABLE statements into table hints. SSMA can't convert any LOCK TABLE statements that contain PARTITION, SUBPARTITION, @dblink, and NOWAIT clauses, and marks such statements with conversion error messages.

- If you select **Yes**, SSMA converts supported LOCK TABLE statements into table hints.

- If you select **No**, SSMA marks all LOCK TABLE statements with conversion error messages.

The following table shows how SSMA converts Db2 lock modes:

| Db2 lock mode | SQL Server table hint |
| --- | --- |
| ROW SHARE | ROWLOCK, HOLDLOCK |
| ROW EXCLUSIVE | ROWLOCK, XLOCK, HOLDLOCK |
| SHARE UPDATE = ROW SHARE | ROWLOCK, HOLDLOCK |
| SHARE | TABLOCK, HOLDLOCK |
| SHARE ROW EXCLUSIVE | TABLOCK, XLOCK, HOLDLOCK |
| EXCLUSIVE | TABLOCKX, HOLDLOCK |

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Yes

### Convert OPEN-FOR statements for REF CURSOR OUT parameters

In Db2, the OPEN-FOR statement can be used to return a result set to a subprogram's OUT parameter of type REF CURSOR. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], stored procedures directly return the results of SELECT statements.

SSMA can convert many OPEN-FOR statements into SELECT statements.

- If you select **Yes**, SSMA converts the OPEN-FOR statement into a SELECT statement, which returns the result set to the client.

- If you select **No**, SSMA generates an error message in the converted code and in the Output pane.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Yes

### Convert record as a list of separates variables

SSMA can convert Db2 records into separates variables and into XML variables with specific structure.

- If you select **Yes**, SSMA converts the record into a list of separates variables when possible.

- If you select **No**, SSMA converts the record into XML variables with specific structure.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Yes

### Convert SUBSTR function calls to SUBSTRING function calls

SSMA can convert Db2 SUBSTR function calls into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] **substring** function calls, depending on the number of parameters. If SSMA can't convert a SUBSTR function call, or the number of parameters isn't supported, SSMA converts the SUBSTR function call into a custom SSMA function call.

- If you select **Yes**, SSMA converts SUBSTR function calls that use three parameters into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] **substring**. Other SUBSTR functions will be converted to call the custom SSMA function.

- If you select **No**, SSMA converts the SUBSTR function call into a custom SSMA function call.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic Mode:** Yes

**Full Mode:** No

### Convert subtypes

SSMA can convert PL/SQL subtypes in two ways:

- If you select **Yes**, SSMA creates [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] user-defined type from a subtype and use it for each variable of this subtype.

- If you select **No**, SSMA substitutes all source declarations of the subtype with the underlying type and convert the result as usual. In this case, no extra types are created in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** No

### Convert synonyms

Synonyms for the following Db2 objects can be migrated to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

- Tables and object tables
- Views and object views
- Stored procedures and functions
- Materialized views

Synonyms for the following Db2 objects can be replaced by direct references to the objects:

- Sequences
- Packages
- Java class schema objects
- User-defined object types

Other synonyms can't be migrated. SSMA generates error messages for the synonym and all references that use the synonym.

- If you select **Yes**, SSMA creates [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] synonyms and direct object references according to the previous lists.

- If you select **No**, SSMA creates direct object references for all synonyms listed here.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Yes

### Convert TO_CHAR(date, format)

SSMA can convert Db2 TO_CHAR(date, format) into procedures from sysdb database.

- If you select **Using TO_CHAR_DATE function**, SSMA converts the TO_CHAR(date, format) into TO_CHAR_DATE function using of English language for conversion.

- If you select **Using TO_CHAR_DATE_LS function (NLS care)**, SSMA converts the TO_CHAR(date, format) into TO_CHAR_DATE_LS function using of session language for conversion

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic Mode:** Using TO_CHAR_DATE function

**Full Mode:** Using TO_CHAR_DATE_LS function (NLS care)

### Convert transaction processing statements

SSMA can convert Db2 transaction processing statements:

- If you select **Yes**, SSMA converts Db2 transaction processing statements to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] statements.

- If you select **No**, SSMA marks the transaction processing statements as conversion errors.

> [!NOTE]  
> Db2 opens transactions implicitly. To emulate this behavior on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you must add BEGIN TRANSACTION statements manually where you want your transactions to start. Alternatively, you can execute the SET IMPLICIT_TRANSACTIONS ON command at the beginning of your session. SSMA adds SET IMPLICIT_TRANSACTIONS ON automatically when converting subroutines with autonomous transactions.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Yes

### Emulate Db2 null behavior in ORDER BY clauses

NULL values are ordered differently in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Db2:

- In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], `NULL` values are the lowest values in an ordered list. In an ascending list, `NULL` values appear first.

- In Db2, `NULL` values are the highest values in an ordered list. By default, `NULL` values appear last in an ascending-order list.

- Db2 has NULLS FIRST and NULLS LAST clauses, which enables you to change how Db2 orders NULLs.

SSMA can emulate Db2 ORDER BY behavior by checking for `NULL` values. It then first orders by `NULL` values in the specified order, and then orders by other values.

- If you select **Yes**, SSMA converts the Db2 statement in a way that emulates Db2 ORDER BY behavior.

- If you select **No**, SSMA ignores Db2 rules and generates an error message when it encounters the NULLS FIRST and NULLS LAST clauses.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic Mode:** No

**Full Mode:** Yes

### Emulate row count exceptions in SELECT

If a SELECT statement with an INTO clause doesn't return any rows, Db2 raises a NO_DATA_FOUND exception. If the statement returns two or more rows, the TOO_MANY_ROWS exception is raised. The converted statement in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't raise any exception if the row count is different from one.

- If you select **Yes**, SSMA adds call to sysdb procedure db_error_exact_one_row_check after each SELECT statement. This procedure emulates the NO_DATA_FOUND and TOO_MANY_ROWS exceptions. This is the default and it allows reproducing Db2 behavior as close as possible. You should always choose **Yes** if the source code has exception handlers that process these errors. If the SELECT statement occurs inside a user-defined function, this module is converted to a stored procedure, because executing stored procedures and raising exceptions isn't compatible with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] function context.

- If you select **No**, no exceptions are generated. That can be useful when SSMA converts a user-defined function and you want it to remain a function in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Yes

### Generate error for DBMS_SQL.PARSE

- If you select **Error**, SSMA generates an error at the conversion DBMS_SQL.PARSE.

- If you select **Warning**, SSMA generates a warning at the conversion DBMS_SQL.PARSE.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Error

### Generate ROWID column

When SSMA creates tables in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], it can create a ROWID column. When data is migrated, each row obtains a new UNIQUEIDENTIFIER value generated by the newid() function.

- If you select **Yes**, the ROWID column is created on all tables, and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] generates GUIDs as you insert values. Always choose **Yes** if you're planning to use the SSMA Tester.

- If you select **No**, ROWID columns aren't added to tables.

- **Add ROWID column for tables with triggers** add ROWID for the tables containing triggers (default).

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic Mode:** Add ROWID column for tables with triggers

**Full Mode:** Yes

### Generate unique index on ROWID column

Specifies whether SSMA generates unique index column on the ROWID generated column or not. If the option is set to `YES`, a unique index is generated. If it's set to `NO`, a unique index isn't generated on the ROWID column.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Yes

### Local modules conversion

Defines the type of Db2 nested subprogram (declared in standalone stored procedure or function) conversion.

- If you select **Inline**, the nested subprogram calls are replaced by its body.

- If you select **Stored procedures**, a nested subprogram is converted to a SQL Server stored procedure, and its calls are replaced on this procedure call.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Inline

### Use ISNULL in string concatenation

Db2 and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] return different results when string concatenations include `NULL` values. Db2 treats the `NULL` value like an empty character set. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns `NULL`.

- If you select **Yes**, SSMA replaces the Db2 concatenation character (||) with the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] concatenation character (+). SSMA also checks the expressions on both sides of the concatenation for `NULL` values.

- If you select **No**, SSMA replaces the concatenation characters, but doesn't check for `NULL` values.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

### Use ISNULL in REPLACE function calls

ISNULL statement is used in REPLACE function calls to emulate Db2 behavior. The following options are present for this setting:

- YES
- NO

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic Mode:** No

**Full Mode:** Yes

### Use ISNULL in CONCAT function calls

ISNULL statement is used in CONCAT function calls to emulate Db2 behavior. The following options are present for this setting:

- YES
- NO

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic Mode:** No

**Full Mode:** Yes

### Use native convert function when possible

- If you select **Yes**, SSMA converts the TO_CHAR(date, format) into native convert function when possible.

- If you select **No**, SSMA converts the TO_CHAR(date, format) into TO_CHAR_DATE or TO_CHAR_DATE_LS (defined by "Convert TO_CHAR(date, format)" options).

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic Mode:** Yes

**Full Mode:** No

### Use SELECT...FOR XML when converting SELECT...INTO for record variable

Specifies whether to generate an XML result set when you select into a record variable.

- If you select **Yes**, the SELECT statement returns XML.

- If you select **No**, the SELECT statement returns a result set.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** No

## RETURNING clause conversion

### Convert RETURNING clause in DELETE statement to OUTPUT

Db2 provides a RETURNING clause as a way to immediately obtain deleted values. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides that functionality with the OUTPUT clause.

- If you select **Yes**, SSMA converts RETURNING clauses in DELETE statements to OUTPUT clauses. Because triggers on a table can change values, the returned value might be different in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] than it was in Db2.

- If you select **No**, SSMA generates a SELECT statement before DELETE statements to retrieve returned values.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Yes

### Convert RETURNING clause in INSERT statement to OUTPUT

Db2 provides a RETURNING clause as a way to immediately obtain inserted values. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides that functionality with the OUTPUT clause.

- If you select **Yes**, SSMA converts a RETURNING clause in an INSERT statement to OUTPUT. Because triggers on a table can change values, the returned value might be different in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] than it was in Db2.

- If you select **No**, SSMA emulates Db2 functionality by inserting and then selecting values from a reference table.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Yes

### Convert RETURNING clause in UPDATE statement to OUTPUT

Db2 provides a RETURNING clause as a way to immediately obtain updated values. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides that functionality with the OUTPUT clause.

- If you select **Yes**, SSMA converts RETURNING clauses in UPDATE statements to OUTPUT clauses. Because triggers on a table can change values, the returned value might be different in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] than it was in Db2.

- If you select **No**, SSMA will generate SELECT statements after UPDATE statements to retrieve returning values.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

**Default/Optimistic/Full Mode:** Yes

## Sequence conversion

### Convert sequence generator

In Db2, you can use a Sequence to generate unique identifiers.

SSMA can convert Sequences to the following.

- Using SQL Server sequence generator.

- Using SSMA sequence generator.

- Using column identity.

The default option when converting to SQL Server is to use the sequence generator. However, [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] don't support obtaining the current sequence value (such as that of Db2 sequence currval method). Refer to SSMA team blog site for guidance on migrating Db2 sequence currval method.

SSMA also provides an option to convert Db2 sequence to SSMA sequence emulator.

Finally, you can also convert sequence assigned to a column in table to SQL Server identity values. You must specify the mapping between the sequences to an identity column on Db2 **Table** tab

### Convert CURRVAL outside triggers

Visible only when the Convert Sequence Generator is set to **using column identity**. Because Db2 Sequences are objects separate from tables, many tables that use Sequences use a trigger to generate and insert a new sequence value. SSMA comments out these statements, or marks them as errors when the commenting out would generate errors.

- If you select **Yes**, SSMA marks all references to outside triggers on the converted sequence CURRVAL with a warning.

- If you select **No**, SSMA marks all references to outside triggers on the converted sequence CURRVAL with an error.

## Related content

- [User interface reference (Db2ToSQL)](user-interface-reference-db2tosql.md)
