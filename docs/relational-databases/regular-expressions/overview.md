---
title: Work with Regular Expressions
description: Use regular expressions to filter and manipulate text strings in SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: abhtiwar, randolphwest, wiassaf
ms.date: 09/01/2026
ms.service: sql
ms.topic: quickstart
ai-usage: ai-assisted
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "Regular expressions functions"
  - "regex"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# Regular expressions

[!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

This article introduces regular expressions for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

A regular expression, or regex, is a sequence of characters that defines a search pattern for text. Regex is commonly used for a variety of tasks including pattern matching, data validation, data transformation, and querying. It offers a flexible and an efficient way to search, manipulate, and handle complex data operations.

This implementation of regular expression is based on the [RE2 regular expression library](https://github.com/google/re2/). For more information, visit [RE2 Regular Expression Syntax](https://cran.r-project.org/web/packages/re2/vignettes/re2_syntax.html#:~:text=The%20simplest%20regular%20expression%20is,matches%20a%20literal%20plus%20character).

Supported regex functions:
[!INCLUDE [regexp-function-descriptions](../../includes/regexp-function-descriptions.md)]

Regular expressions can be composed of literal characters and metacharacters, which have special meanings and functions.

A basic regular expression is a single literal character. Characters match themselves, except for the metacharacters. Metacharacters include `*`, `+`, `?`, `(`, `)`, or `|`. To match a metacharacter, escape it with a backslash. For example, `\*` matches the literal asterisk (`*`) character.

Two regular expressions can be altered or concatenated to form a new regular expression: if *e1* matches *s* and *e2* matches *t*, then *e1* | *e2* matches *s* or *t*, and *e1* *e2* matches *st*.

The metacharacters `*`, `+`, and `?` are repetition operators: *e1* `*` matches a sequence of zero or more (possibly different) strings, each of which match e1; e1 `+` matches one or more; *e1* `?` matches zero or one.

Operator precedence, from weakest to strongest binding, is as follows:
    - Alternation
    - Concatenation
    - Repetition operators

Explicit parentheses can be used to force different meanings, as in arithmetic expressions. Some examples: `ab|cd` is equivalent to `(ab)|(cd)` ; `ab` is equivalent to `a(b)`.

For more information about the regular expression syntax accepted by RE2, see  
[RE2 Regular Expression Syntax](https://cran.r-project.org/web/packages/re2/vignettes/re2_syntax.html). This page also lists some syntax accepted by PCRE, Perl, and Vim.

## Accepted regular expression characters

### Single character expressions

| Kinds of single-character expressions | Examples |
| --- | --- |
| any character, possibly including newline (s=true) | `.` |
| character class | `[xyz]` |
| negated character class | `[^xyz]` |
| [Perl character classes](#perl-character-classes) | `\d` |
| negated Perl character class | `\D` |
| [ASCII character classes](#ascii-character-classes) | `[[:alpha:]]` |
| negated ASCII character class | `[[:^alpha:]]` |
| Unicode character class (one-letter name) | `\pN` |
| Unicode character class | `\p{Greek}` |
| negated Unicode character class (one-letter name) | `\PN` |
| negated Unicode character class | `\P{Greek}` |

| Composites | Description |
| --- | --- |
| `xy` | x followed by y |
| `x | y` | x or y (prefer x) |

| Repetitions | Description |
| --- | --- |
| `x*` | zero or more x, prefer more |
| `x+` | one or more x, prefer more |
| `x?` | zero or one x, prefer one |
| `x{n,m}` | n or n+1 or ... or m x, prefer more |
| `x{n,}` | n or more x, prefer more |
| `x{n}` | exactly n x |
| `x*?` | zero or more x, prefer fewer |
| `x+?` | one or more x, prefer fewer |
| `x??` | zero or one x, prefer zero |
| `x{n,m}?` | n or n+1 or ... or m x, prefer fewer |
| `x{n,}?` | n or more x, prefer fewer |
| `x{n}?` | exactly n x |

> [!NOTE]  
> The counting forms `x{n,m}`, `x{n,}`, and `x{n}` reject forms that create a minimum or maximum repetition count above 1,000. Unlimited repetitions aren't subject to this restriction.

### Perl character classes

The following table lists currently supported Perl character classes.

| Perl character classes (all ASCII-only) | Description |
| --- | --- |
| `\d` | digits ( `[0-9]`) |
| `\D` | not digits ( `[^0-9]`) |
| `\s` | whitespace ( `[\t\n\f\r ]`) |
| `\S` | not whitespace ( `[^\t\n\f\r ]`) |
| `\w` | word characters ( `[0-9A-Za-z_]`) |
| `\W` | not word characters ( `[^0-9A-Za-z_]`) |

## ASCII character classes

The following table lists currently supported ASCII character classes.

| ASCII character classes | Description |
| --- | --- |
| `[[:alnum:]]` | alphanumeric ( `[0-9A-Za-z]`) |
| `[[:alpha:]]` | alphabetic ( `[A-Za-z]`) |
| `[[:ascii:]]` | ASCII ( `[\x00-\x7F]`) |
| `[[:blank:]]` | blank ( `[\t ]`) |
| `[[:cntrl:]]` | control ( `[\x00-\x1F\x7F]`) |
| `[[:digit:]]` | digits ( `[0-9]`) |
| `[[:graph:]]` | graphical ( `[!-~]` or ``[A-Za-z0-9!"#$%&'()*+,\-./:;<=>?@[\\\]^_`{|}~]``) |
| `[[:lower:]]` | lower case ( `[a-z]`) |
| `[[:print:]]` | printable ( `[ -~]` or `[ [:graph:]]`) |
| `[[:punct:]]` | punctuation ( `[!-/:-@[-\``{-~]`) |
| `[[:space:]]` | whitespace ( `[\t\n\v\f\r ]`) |
| `[[:upper:]]` | upper case ( `[A-Z]`) |
| `[[:word:]]` | word characters ( `[0-9A-Za-z_]`) |
| `[[:xdigit:]]` | hex digit ( `[0-9A-Fa-f]`) |

### Literal characters

- Letters
- Numbers
- Symbols

### Empty strings

| Empty strings | Description |
| --- | --- |
| `^` | At beginning of text or line (`m=true`) |
| `$` | At end of text (like `\z`, not `\Z`) or line (`m=true`) |
| `\A` | At beginning of text |
| `\b` | At ASCII word boundary (`\w` on one side and `\W`, `\A`, or `\z` on the other) |
| `\B` | Not at ASCII word boundary |
| `\z` | At end of text |

### Groupings

The capture group (subexpression) of the pattern with:

| Grouping | Description |
| --- | --- |
| `(re)` | Numbered capturing group (submatch) |
| `(?P<name>re)` | Named and numbered capturing group (submatch) |
| `(?:re)` | Non-capturing group |
| `(?<flags>)` | Set `<flags>` within current group; non-capturing |
| `(?<flags>:re)` | Set `<flags>` during `re`; non-capturing Grouping |

### Flags

Use flags to modify the expression behavior. For example:

| Flag | Description |
| --- | --- |
| `i` | Case-insensitive (default `false`) |
| `m` | Multi-line mode: `^` and `$` match begin/end line in addition to begin/end text (default `false`) |
| `s` | Let `.` match `\n` (default `false`) |
| `c` | Case-sensitive (default `true`) |

This implementation supports the POSIX standard of regular expressions following RE2, and has support for the PCRE/PCRE2 flavor of regular expressions syntax, which is compatible with most modern regular expression engines and tools. There are different flavors of regular expressions, such as POSIX, ANSI, Perl, and PCRE, which have different syntax and features. For more information about supported constructs and behavior of the underlying regex engine, see [RE2, a regular expression library](https://github.com/google/re2).

> [!NOTE]  
> Regular expression matching in SQL Server doesn't honor SQL collations for linguistic comparisons. This behavior is by design and consistent with most regular expression engines, as matching is based on the pattern and Unicode character properties rather than collation rules. As a result, it might lead to differences in behavior when compared to other string comparison functions like LIKE, especially on indexed columns with language-specific collations.

## Requirements

- A SQL client tool, such as SQL Server Management Studio or Visual Studio Code.
- A basic knowledge of SQL syntax and queries.
- A basic understanding of regular expression syntax and concepts.

### Platform support

- In Azure SQL Managed Instance, T-SQL regular expression functions are available with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy) only.
- T-SQL regular expression functions are available in Azure SQL Database, SQL database in Fabric, and starting in SQL Server 2025.
- T-SQL regular expression functions aren't available in SQL Server 2022 or earlier versions.
- T-SQL regular expression scalar functions are available as a preview feature in Fabric Data Warehouse. For more information, see [Regular expressions functions in Fabric Data Warehouse](/fabric/data-warehouse/regular-expressions). T-SQL regular expression table-valued functions aren't available in Fabric Data Warehouse.

## FAQ

### What is the performance impact of using regex?

Regex queries can have a performance impact depending on the complexity of the regex pattern, the size of the text data, and the number of rows involved. You can use the execution plan and statistics to monitor and optimize the performance of your regex queries.

## Limitations

The following limitations currently apply:

- LOB data types (**varchar(max)** or **nvarchar(max)**) for `string_expression` are supported in all `REGEXP_*` functions, but the input size is limited to 2 MB.
- Regular expression functions aren't supported in natively compiled stored procedures.
