---
description: "Project Settings (Conversion) (SybaseToSQL)"
title: "Project Settings (Conversion) (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/19/2020"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: eeb80fa5-f530-4f21-beee-25f5a4b8ace6
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.sybase.projectsettingconversion.f1"
---

# Project Settings (Conversion) (SybaseToSQL)

The **Conversion** page of the **Project Settings** dialog box contains settings that customize how SSMA converts SAP Adaptive Server Enterprise (ASE) syntax to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL syntax.

The **Conversion** pane is available in the **Project Settings** and **Default Project Settings** dialog boxes:

- If you want to specify settings for all SSMA projects, on the **Tools** menu, select **Default Project Settings**, click **General** at the bottom of the left pane, and then click **Conversion**.

- To specify settings for the current project, on the **Tools** menu, select **Project Settings**, click **General** at the bottom of the left pane, and then click **Conversion**.

## Miscellaneous section

### @@ERROR

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL and ASE use different error codes.

Use this setting to specify the type of message (Warning or Error) that SSMA shows in the Output or Error List pane when it encounters a reference to `@@ERROR` in the ASE code.

- If you select **Convert and mark with warning**, SSMA will convert the statements and mark them with warning comments.
- If you select **Mark with error**, SSMA will skip conversion and mark the statements with error comments.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Convert and mark with warning|
|Optimistic|Convert and mark with warning|
|Full|Mark with error|

### Conversion of LIKE operator

Specifies whether to convert `LIKE` operands to match SAP ASE behavior. The point is that ASE trims trailing blanks in a like pattern. The workaround is to make a cast of right expression to a fixed length data type with a maximum precision.
  
- Select **Simple conversion** to convert the expressions without any correction.
- To use the ASE behavior select **Cast to fixed length.**
  
When you select a conversion mode in the Mode box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Simple conversion|
|Optimistic|Simple conversion|
|Full|Cast to fixed length|

### CONVERT or CAST empty strings to numeric types

Specifies how to handle empty or blank strings within `CONVERT` or `CAST` expressions with numeric type as datatype argument. The following options are available for this setting:

- Select **Simple conversion** to convert the expressions without any correction.
- If **Empty string as zero numeric** is selected, then string parameter `{s}` will be replaced with `CASE ltrim(rtrim({s})) WHEN "" THEN 0 else {s} END` expression.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Simple conversion|
|Optimistic|Simple conversion|
|Full|Empty string as zero numeric|

### Concatenation of NULL

This setting specifies how to convert string concatenation with `NULL`. The following options can be set for this particular setting:

- If **Wrap with ISNULL function** option is selected, every non-constant `string_expression` in concatenation will be wrapped with `ISNULL(string_expression)` and `NULL`s will be replaced with empty string.
- **Keep current syntax** will maintain original syntax.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Keep current syntax|
|Optimistic|Keep current syntax|
|Full|Wrap with ISNULL function|

### Conversion of Empty strings

This setting specifies how to convert empty strings. The following options can be set for this particular setting:

- **Replace all string expressions with space**
- **Replace empty string constants with space**

To use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL behavior, select **Keep current syntax**.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Keep current syntax|
|Optimistic|Keep current syntax|
|Full|Replace all string expressions with space|

### CONVERT and CAST binary string conversion

The conversion of binary values to numbers can return different values on different platforms. For example, on x86 processors, `CONVERT(integer, 0x00000100)` returns `65536` in ASE, but `256` in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. ASE also returns different values depending on byte order.

Use this setting to control how SSMA converts `CONVERT` and `CAST` expressions that contain binary values:

- Select **Simple conversion** to convert the expressions without any warnings or correction. Use this setting if you know that the ASE server has a byte order that does not require any changes of the binary value.
- Select **Convert and correct** to have SSMA convert and correct the expressions for use on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The byte order in literal constants will be reversed. All other binary values (such as binary variables and columns) will be marked with errors. Use this value if you know that the ASE server has a byte order that requires changes to binary values.

Select **Convert and mark with warning** to have SSMA convert and correct the expressions, and mark all converted expressions with warning comments.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Convert and mark with warning|
|Optimistic|Simple conversion|
|Full|Convert and correct|

### Dynamic SQL

Use this setting to specify the type of message (Warning or Error) that SSMA shows in the **Output** or **Error List** pane when it encounters dynamic SQL in the ASE code.

- If you select **Convert and mark with warning**, SSMA will convert the dynamic SQL and mark the statements with warning comments.
- If you select **Mark with error**, SSMA will skip conversion and mark the statements with error comments.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Convert and mark with warning|
|Optimistic|Convert and mark with warning|
|Full|Mark with error|

### Equality check conversion

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL, if the `ANSI_NULLS` setting is on, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL returns `UNKNOWN` when any equality comparison contains a `NULL` value. If `ANSI_NULLS` is off, equality comparisons that contain `NULL` values return true when the compared column and expression or two expressions are both `NULL`. By default (`ANSINULL OFF`) SAP ASE equality comparisons behave like [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL with `ANSI_NULLS OFF`.

- If you select **Simple conversion**, SSMA will convert the ASE code to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL syntax without extra checks for `NULL` values. Use this setting if `ANSI_NULLS` is `OFF` in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL or if you want to revise equality comparisons on a per-case basis.
- If you select **Consider NULL values**, SSMA will add checks for `NULL` values by using the `IS NULL` and `IS NOT NULL` clauses.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Simple conversion|
|Optimistic|Simple conversion|
|Full|Consider NULL values|

### Format strings

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL no longer supports the `format_string` argument in `PRINT` and `RAISERROR` statements. The `format_string` argument allowed to put replaceable parameters directly in the string, and then replacing the parameters at runtime. Instead, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires the full string by using either a string literal, or a string built by using a variable. For more information, see the [PRINT ([!INCLUDE[tsql](../../includes/tsql-md.md)])](../../t-sql/language-elements/print-transact-sql.md) topic.

When SSMA encounters a `format_string` argument, it can either build a string literal using the variables or create a new variable and build a string by using that variable.

- To use a string literal for `PRINT` and `RAISERROR` functions, select **Create new string**.

    In this mode, if a PRINT or RAISERROR statement does not use placeholders and local variables, the statement is unchanged. Double percent characters (%%) are changed to a single percent character % in PRINT string literals.

    If a PRINT or RAISERROR statement uses placeholders and one or more local variables, such as in the following example:

    ```sql
    PRINT 'Total: %1!%%', @percent
    ```

    SSMA will convert it to the following syntax:

    ```sql
    PRINT 'Total: '+ CAST(@percent AS varchar(max)) + '%'
    ```

    If `format_string` is a variable, such as in the following statement:  

    ```sql
    PRINT @fmt, @arg1, @arg2
    ```

    SSMA cannot do a simple string conversion, and must create a new variable:

    ```sql
    DECLARE @print_format_1 varchar(max)
    SET @print_format_1 =
        REPLACE (@fmt, '%%', '%')
    SET @print_format_1 =
        REPLACE (@print_format_1, '%1!',
        CAST (@arg1 AS varchar(max)))
    SET @print_format_1 =
        REPLACE (@print_format_1, '%2!',
        CAST (@arg2 AS varchar(max)))
    PRINT @print_format_1
    ```

    When it uses **Create new string** mode, SSMA assumes that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] option `CONCAT_NULL_YIELDS_NULL` is `OFF`. Therefore, SSMA does not check for null arguments.

- To have SSMA build a new variable for each `PRINT` and `RAISERROR` statement, and then use that variable for the string value, select **Create new variable**.

    In this mode, if a `PRINT` or `RAISERROR` statement does not use placeholders and local variables, SSMA replaces all double percent characters (`%%`) with single percent characters to comply with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL syntax.

    If a `PRINT` or `RAISERROR` statement uses placeholders and one or more local variables, such as in the following example:

    ```sql
    PRINT 'Total: %1!%%', @percent
    ```

    SSMA will convert it to the following syntax:

    ```sql
    DECLARE @print_format_1 varchar(max)
    SET @print_format_1 = 'Total: %1!%'
    SET @print_format_1 =
        REPLACE (@print_format_1, '%1!',
        ISNULL(CAST (@percent AS VARCHAR(max)), ''))
    PRINT @print_format_1
    ```

    If `format_string` is a variable, such as in the following statement:

    ```sql
    PRINT @fmt, @arg1, @arg2
    ```

    SSMA creates a new variable as follows, checking for null values in each argument:

    ```sql
    DECLARE @print_format_1 varchar(max)
    SET @print_format_1 =
        REPLACE (@fmt, '%%', '%')
    SET @print_format_1 =
        REPLACE (@print_format_1, '%1!',
        ISNULL(CAST (@arg1 AS varchar(max)),''))
    SET @print_format_1 =
        REPLACE (@print_format_1, '%2!',
        ISNULL(CAST (@arg2 AS varchar(max)),''))
    PRINT @print_format_1
    ```

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Create new string|
|Optimistic|Create new string|
|Full|Create new variable|

### Insert an explicit value into a timestamp column

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL does not support inserting explicit values into a timestamp column.

- To exclude timestamp columns from `INSERT` statements, select **Exclude column**.
- To print an error message every time that a timestamp column is in an `INSERT` statement, select **Mark with error**. In this mode, `INSERT` statements will not be converted and will be marked with error comments.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Exclude column|
|Optimistic|Exclude column|
|Full|Mark with error|

### Store temporary objects defined in procedures

This setting specifies if the temporary objects definitions which appear in the procedures should be stored in the source metadata during conversion.

- Select **Yes** to store into metadata.
- Select **No** if the objects need not be stored.

|Mode|Value|
|-|-|
|Default|Yes|
|Optimistic|Yes|
|Full|No|

### Proxy table conversion

Specifies if ASE proxy tables are converted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL tables, or are not converted and the code is marked with error comments.

- Select **Convert** to convert proxy tables to regular tables.
- Select **Mark with error** to simply mark the proxy table code with error comments.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Mark with error|
|Optimistic|Mark with error|
|Full|Mark with error|

### RAISERROR base message number

ASE user messages are stored in each database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user messages are centrally stored and made available through the `sys.messages` catalog view. In addition ASE user messages start at `20000`, but [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error messages start at `50001`.

This setting specifies the number to add to the ASE user message number to convert it to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user message. If your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has user messages in the `sys.messages` catalog view, you might have to change this number to a higher value. This is so the converted message numbers do not conflict with existing message numbers.

Note the following:
  
- ASE messages in the range `17000`-`19999` are from the `sysmessages` system table and are not converted.
- If the message number that is referenced in the `RAISERROR` statement is a constant, SSMA will add the base message number to the constant to determine the new user message number.
- If the message number that is referenced is a variable or expression, SSMA will create an intermediate local variable.
- In **Optimistic mode**, SSMA assumes that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] option `CONCAT_NULL_YIELDS_NULL` is `OFF` and makes no checks for `NULL` arguments.
- In **Full mode**, SSMA checks for `NULL` arguments.
- `RAISERROR` with `arg-list` argument is not converted.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|30001|
|Optimistic|30001|
|Full|30001|

### System objects

Use this setting to specify the type of message (Warning or Error) that SSMA shows in the **Output** or **Error List** pane when it encounters the use of ASE system objects.

- If you select **Convert and mark with warning**, SSMA will convert references to system objects and will mark statements with warning comments.
- If you select **Mark with error**, SSMA will not convert references to systems objects and will mark statements with error comments.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Convert and mark with warning|
|Optimistic|Convert and mark with warning|
|Full|Mark with error|

### Unresolved identifiers

Use this setting to specify the type of message (Warning or Error) that SSMA shows in the **Output** or **Error List** pane when it cannot resolve an identifier.

- If you select **Convert and mark with warning**, SSMA will attempt to convert references to unresolved identifiers and will mark statements with warning comments.
- If you select **Mark with error**, SSMA will not convert references to unresolved identifiers and will mark statements with error comments.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Convert and mark with warning|
|Optimistic|Convert and mark with warning|
|Full|Mark with error|

## System functions section

### CHARINDEX function

In ASE, `CHARINDEX` returns `NULL` only if all input expressions are `NULL`. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL will return `NULL` if any input expression is `NULL`.

- To use the ASE behavior, select **Replace function**. All calls to `CHARINDEX` function is substituted with a call to either `CHARINDEX_VARCHAR` or  `CHARINDEX_NVARCHAR` user defined function based on the type of parameters passed (created in the user database under the schema name `s2ss`) to emulate the SAP ASE behavior.
- To use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL behavior, select **Keep current syntax**.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Keep current syntax|
|Optimistic|Keep current syntax|
|Full|Replace function|
  
### DATALENGTH function

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL and ASE differ in the value returned by the `DATALENGTH` function when the value is a single space. In this case, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL returns `0` and ASE returns `1`.

- To use the ASE behavior, select **Replace function**. All calls to `DATALENGTH` function are substituted with `CASE` expression to emulate SAP ASE behavior.
- To use the default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL behavior, select **Keep current syntax**.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Keep current syntax|
|Optimistic|Keep current syntax|
|Full|Replace function|

### INDEX_COL function

ASE supports an optional `user_id` argument to the `INDEX_COL` function; however, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL does not support this argument. If you use the `user_id` argument, this function cannot be converted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL syntax.

- To use the ASE behavior, select **Convert function**. If the code contains the `user_id` argument, SSMA will display an error.
- To display an error message every time that `INDEX_COL` is encountered, select **Mark with error**. SSMA will not convert references to the function, and will mark the statement with error comments.

|Mode|Value|
|-|-|
|Default|Mark with error|
|Optimistic|Mark with error|
|Full|Mark with error|

### INDEX_COLORDER function

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL does not have an `INDEX_COLORDER` system function.

- To use the ASE behavior, select **Convert function**. All calls to `INDEX_COLORDER` function is substituted with a call to a user defined function with same name `INDEX_COLORDER` (created in the user database under the schema name `s2ss`) which emulates the SAP ASE behavior.
- To print an error message every time that `INDEX_COLORDER` is encountered, select **Mark with error**. SSMA will not convert references to the function, and will mark the statement with error comments.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Mark with error|
|Optimistic|Mark with error|
|Full|Mark with error|

### LEFT and RIGHT Functions

`LEFT` and `RIGHT` functions in ASE behave differently for negative length parameter.

- To use the ASE behavior, select **Replace Function**. The length parameter is then replaced with `CASE` expression which would return `NULL` for negative value.
- To use the SQL Server behavior, select **Keep current syntax**.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Keep current syntax|
|Optimistic|Keep current syntax|
|Full|Replace function|

> [!NOTE]
> If the length parameter is a literal value and not a complex expression, the length value is always replaced with `NULL` irrespective of project setting.

### NEXT_IDENTITY function

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL does not have an `NEXT_IDENTITY` system function.

- To use the ASE behavior, select **Convert Function**. All calls to `NEXT_IDENTITY` function is substituted with expression `(IDENT_CURRENT(parameter Value) + IDENT_INCR(parameter Value)` which emulates the SAP ASE behavior.
- To print an error message every time that `NEXT_IDENTITY` is encountered, select **Mark with error**. SSMA will not convert references to the function, and will mark the statement with error comments.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Mark with error|
|Optimistic|Mark with error|
|Full|Mark with error|

**Default/Optimistic/Full Mode:** Mark with error

### PATINDEX function

Specifies whether to convert `PATINDEX` function to match SAP ASE behavior. The point is that ASE trims trailing blanks in a search pattern. The workaround is to make a cast of value expression to a fixed length data type with a maximum precision and apply `rtrim` function to search pattern.

- To use the ASE behavior select **Use**.  
- To use the default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL behavior, select **Do not use**.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Do not use|
|Optimistic|Do not use|
|Full|Use|

### REPLICATE function

The `REPLICATE` function repeats a string the specified number of times. In ASE, if you specify to repeat the string zero times, the result is `NULL`. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL, the result is an empty string.

- To use the ASE behavior, select **Replace function**. All calls to `REPLICATE` function is substituted with a call to either `REPLICATE_VARCHAR` or `REPLICATE_NVARCHAR` user defined function based on the type of parameters passed (created in the user database under the schema name `s2ss`) to emulate the SAP ASE behavior.
- To use the default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL behavior, select **Replace Function**.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Replace function|
|Optimistic|Replace function|
|Full|Replace function|

### TRIM (LTRIM, RTRIM) function

This setting specifies whether to replace calls to `TRIM`, `LTRIM` and `RTRIM` functions with the SAP ASE-equivalent syntax functions or to keep the current syntax. The following options are present for this particular setting:

- **Replace function**
- **Keep current syntax**

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Replace function|
|Optimistic|Replace function|
|Full|Replace function|

### SUBSTRING function

In ASE, the function `SUBSTRING(expression, start, length)` returns `NULL` if a start value greater than the number of characters in expression is specified, or if length equals zero. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL, the equivalent expression returns an empty string.

- To use the ASE behavior, select **Replace function**. All calls to `SUBSTRING` function is substituted with a call to `SUBSTRING_VARCHAR` or `SUBSTRING_NVARCHAR` or `SUBSTRING_VARBINARY` user defined function based on the type of parameters passed (created in the user database under the schema name `s2ss`) to emulate the SAP ASE behavior.
- To use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Azure SQL behavior, select **Keep current syntax**.

When you select a conversion mode in the **Mode** box, SSMA applies the following setting:

|Mode|Value|
|-|-|
|Default|Keep current syntax|
|Optimistic|Keep current syntax|
|Full|Replace function|

## Tables section

### Add primary key

Creates a new primary key in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL table if an SAP ASE table has no primary key or unique index.

|Mode|Value|
|-|-|
|Default|No|
|Optimistic|No|
|Full|Yes|

> [!NOTE]
> When connected to Azure SQL, it is **Yes** by default.

## See Also

[User Interface Reference (SybaseToSQL)](../../ssma/sybase/user-interface-reference-sybasetosql.md)
