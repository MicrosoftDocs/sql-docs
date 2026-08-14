---
title: Literals (SSIS)
description: Numeric, string, and Boolean literals
ms.reviewer: randolphwest
ms.date: 08/13/2026
ms.service: sql
ms.subservice: integration-services
ms.topic: concept-article
helpviewer_keywords:
  - "string literals"
  - "numeric literals [Integration Services]"
  - "Boolean literals"
  - "expressions [Integration Services], literals"
  - "literals [Integration Services]"
  - "mapping literals [Integration Services]"
---
# Numeric, string, and Boolean literals

[!INCLUDE [sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

Expressions can include numeric, string, and Boolean literals. The expression evaluator supports various numeric literals such as integers, decimals, and floating-point constants. The expression evaluator also supports the long and float suffixes, which specify how the expression evaluator handles values, and scientific notation in numeric literals.

## Numeric literals

The expression evaluator supports integral and nonintegral numeric data types. It also supports lineage identifiers, which are the unique numeric identifiers for package elements. Lineage identifiers are numbers, but you can't use them in mathematical operations.

The expression evaluator supports suffixes, which you can use to indicate how the expression evaluator treats the numeric literal. For example, you can indicate the integer `37` is treated as a long integer data type by writing `37L` or `37l`.

The following table lists suffixes for numeric literals.

| Suffix | Description |
| --- | --- |
| `L` or `l` | A long numeric literal. |
| `U` or `u` | An unsigned numeric literal. |
| `E` or `e` | The exponent in scientific notation |

The following table lists numeric expression elements and their regular expressions.

| Expression element | Regular expression | Description |
| --- | --- | --- |
| Digits expressed as `D`. | `[0-9]` | Any digit. |
| Scientific notation expressed as `E`. | `[Ee][+-]?{D}+` | Upper or lowercase `e`, optionally `+` or `-`, and one or more digits as defined in `D`. |
| Integer suffix expressed as `IS`. | <code>(([lL]?[uU]?)&#124;([uU]?[lL]?))</code> | Optionally, upper or lowercase `u` and `l` or a combination of `u` and `l`. `U` or `u` indicates an unsigned value. `L` or `l` indicates a long value. |
| Float suffix expressed as `FS`. | <code>([f&#124;F]&#124;[l&#124;L])</code> | Upper or lowercase `f` or `l`. `F` or `f` indicates a float value (DT_R4 data type). `L` or `l` indicates a long value (DT_R8 data type). |
| Hexadecimal digit expressed as `H`. | `[a-fA-F0-9]` | Any hexadecimal digit. |

The following table describes valid numeric literals using the regular expression language.

| Regular expression | Description |
| --- | --- |
| `{D}+{IS}` | An integral numeric literal with at least one digit (`D`) and, optionally, the long and unsigned suffix (`IS`). Examples: `457`, `785u`, `986L`, and `7945ul`. |
| `{D}+{E}{FS}` | A nonintegral numeric literal with at least one digit (`D`), scientific notation, and the long or the float suffix. Examples: `4E8l`, `13e-2f`, and `5E+L`. |
| `{D}*"."{D}+{E}?{FS}` | A nonintegral numeric literal with a decimal place, a decimal fraction with at least one digit (`D`), an optional exponent (`E`), and one float or one long identifier (`FS`). This numeric literal has the DT_R4 or DT_R8 data type. Examples: `6.45E3f`, `.89E-2l`, and `1.05E+7F`. |
| `{D}+"."{D}*{E}?{FS}` | A nonintegral numeric literal with at least one significant digit (`D`), a decimal place, an exponent (`E`), and one float or one long identifier (`FS`). This numeric literal has the DT_R4 or DT_R8 data type. Examples: `1.E-4f`, `4.6E6L`, and `8.365E+2f`. |
| `{D}*.{D}+` | A nonintegral numeric literal with precision and scale. It has a decimal place and a decimal fraction with at least one digit (`D`). This numeric literal has the DT_NUMERIC data type. Examples: `.9`, `5.8`, and `0.346`. |
| `{D}+.{D}*` | A nonintegral numeric literal with precision and scale. It has at least one significant digit (`D`) and a decimal place. This numeric literal has the DT_NUMERIC data type. Examples: `6.`, `0.2`, and `8.0`. |
| `#{D}+` | A lineage identifier. It consists of the pound (`#`) character and at least one digit (`D`). Examples: `#123`. |
| `0[xX]{H}+{uU}` | A numeric literal in hexadecimal format. It includes a zero, an upper or lowercase `x`, at least one uppercase `H`, and, optionally, the unsigned suffix. Examples: `0xFF0A` and `0X000010000U`. |

For more information about the data types the expression evaluator uses, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).

Expressions can include numeric literals with different data types. When the expression evaluator evaluates these expressions, it converts the data to compatible types. For more information, see [Integration Services Data Types in Expressions](integration-services-data-types-in-expressions.md).

However, conversion between some data types requires an explicit cast. The expression evaluator provides the cast operator for performing explicit data type conversion. For more information, see [Cast (SSIS Expression)](cast-ssis-expression.md).

<a id="mapping-numeric-literals-to-integration-services-data-types"></a>

### Map numeric literals to Integration Services data types

The expression evaluator converts numeric literals as follows:

- An integral numeric literal maps to an integer data type based on its suffix:

  | Suffix | Result type |
  | --- | --- |
  | None | DT_I4 |
  | `U` | DT_UI4 |
  | `L` | DT_I8 |
  | `UL` | DT_UI8 |

  > [!IMPORTANT]  
  > If the long (`L` or `l`) suffix is absent, the expression evaluator maps signed values to the DT_I4 data type and unsigned values to the DT_UI4 data type even if the value overflows the data type.

- A numeric literal that includes an exponent converts to either the DT_R4 or the DT_R8 data type. If the expression includes the long suffix, it converts to DT_R8. If it includes the float suffix, it converts to the DT_R4 data type.

- If a nonintegral numeric literal includes `F` or `f`, it maps to the DT_R4 data type. If it includes `L` or `l` and the number is an integer, it maps to the DT_I8 data type. If it's a real number, it maps to the DT_R8 data type. If it includes the long suffix, it converts to the DT_R8 data type.

- A nonintegral numeric literal with precision and scale maps to the DT_NUMERIC data type.

## String literals

Enclose string literals in quotation marks. The expression language provides a set of escape sequences for commonly escaped characters such as nonprinting characters and quotation marks.

A string literal consists of zero or more characters surrounded by quotation marks. If a string contains quotation marks, escape them for the expression to parse. A string can contain any two-byte character except `\x0000`, because the `\x0000` character is the null terminator of a string.



Strings can include other characters that require an escape sequence. The following table lists escape sequences for string literals.

| Escape sequence | Description |
| --- | --- |
| `\a` | Alert |
| `\b` | Backspace |
| `\f` | Form feed |
| `\n` | New line |
| `\r` | Carriage return |
| `\t` | Horizontal tab |
| `\v` | Vertical tab |
| `\"` | Quotation mark |
| `\` | Backslash |
| `\xhhhh` | Unicode character in hexadecimal notation |

## Boolean literals

The expression evaluator supports the usual Boolean literals: **True** and **False**. The expression evaluator isn't case-sensitive and permits any combination of uppercase and lowercase letters. For example, `TRUE` works as well as `True`.

> [!NOTE]  
> In an expression, you must delimit a Boolean literal with spaces.
