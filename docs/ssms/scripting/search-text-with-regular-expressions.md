---
title: "Search text with regular expressions"
description: "Learn how to use regular expressions in the Find what field of a Find and Replace dialog box to specify a pattern to be matched."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/11/2024
ms.service: sql
ms.subservice: ssms
ms.topic: how-to
f1_keywords:
  - "vswildcardsbuilder"
  - "vsregexbuilder"
helpviewer_keywords:
  - "searches [SQL Server Management Studio], wildcards"
  - "Query Editor [SQL Server Management Studio], wildcard searches"
  - "wildcard options [SQL Server Management Studio]"
  - "searches [SQL Server Management Studio], regular expressions"
  - "Query Editor [SQL Server Management Studio], regular expression searches"
  - "regular expression options [SQL Server Management Studio]"
  - "searches [SQL Server Management Studio], regex"
  - "Query Editor [SQL Server Management Studio], regex searches"
  - "regex options [SQL Server Management Studio]"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# How to search text with regular expressions

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The following regular expressions can replace characters or digits in the **Find what** field of the [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **Find and Replace** dialog box.

## Prerequisites

- [Download SSMS](../download-sql-server-management-studio-ssms.md)

## How to enable regular expressions

Here are steps to enable regular expressions in search.

1. Go to **Edit** > **Find and Replace** > **Quick Find**.
1. Next to the search bar select the *down arrow* > **Find in Files**.
1. In the **Find and Replace** window expand **Find options** and select **Use Regular Expressions**.

The **Expression Builder** button next to the **Find what** field then becomes available. Select this button to display a list of the available regular expressions. When you choose any item from the **Expression Builder**, it's inserted into the **Find what** string.

The following table describes some of the regular expressions in the **Expression Builder**.

| Expression | Description |
| --- | --- |
| `.` | Match any single character (except a line break) |
| `.*` | Match any character zero or more times |
| `.+` | Match any character one or more times |
| `[abc]` | Match any character in the set `abc` |
| `[^abc]` | Match any character not in the set `abc` |
| `\d` | Match any numeric character |
| `(?([^\r\n])\s)` | Match any whitespace character |
| `\b` | Match at the beginning or end of the word |
| `^` | Match at beginning of line |
| `.$` | Match any line break |
| `\w\r?\n` | Match a word character at end of line |
| `(dog | cat)` | Capture and implicitly number the expression `dog | cat` |
| `(?<pet>dog | cat)` | Capture subexpression `dog | cat` and name it `pet` |

## Examples

Some examples of using regular expressions.

### Example 1: Finding All Select Statements

You want to find all SELECT statements in your SQL scripts.

  ```sql
  SELECT\s+.*\s+FROM
  ```

#### Explanation for example 1

- SELECT\s+: Matches the word "SELECT" followed by one or more whitespace characters.
- .*: Matches any character (except for line terminators) zero or more times.
- \s+FROM: Matches one or more whitespace characters followed by the word "FROM".

### Example 2: Finding Procedures with Specific Naming Patterns

You want to find all stored procedures that start with "usp_" in your SQL scripts.

  ```sql
  CREATE\s+PROCEDURE\s+usp_[A-Za-z0-9_]+
  ```

#### Explanation for example 2

- CREATE\s+PROCEDURE\s+: Matches the words "CREATE PROCEDURE" followed by one or more whitespace characters.
- usp_: Matches the literal string "usp_".
- [A-Za-z0-9_]+: Matches one or more alphanumeric characters or underscores.

### Example 3: Finding Comments in SQL Scripts

You want to identify all single-line comments (starting with --) in your SQL scripts.

  ```sql
  --.*
  ```

#### Explanation for example 3

- --: Matches the literal string "--".
- .*: Matches any character (except for line terminators) zero or more times.

### Example 4: Finding All Update Statements

You want to find all the UPDATE statements in your SQL scripts.

  ```sql
  UPDATE\s+.*\s+SET
  ```

#### Explanation for example 4

- UPDATE\s+: Matches the word "UPDATE" followed by one or more whitespace characters.
- .*: Matches any character (except for line terminators) zero or more times.
- \s+SET: Matches one or more whitespace characters followed by the word "SET".

### Example 5: Finding Table Names in DDL Statements

You want to extract table names from CREATE TABLE statements in your SQL scripts.

```sql
CREATE\s+TABLE\s+(\w+)
```

#### Explanation for example 5

- CREATE\s+TABLE\s+: Matches the words "CREATE TABLE" followed by one or more whitespace characters.
- (\w+): Matches one or more word characters (alphanumeric and underscore) and captures them for extraction.

For more examples, visit [Regular Expressions in Visual Studio](/visualstudio/ide/using-regular-expressions-in-visual-studio)

## Related content

- [Search and Replace](search-and-replace.md)
- [Search Documents Interactively](search-documents-interactively.md)
- [Search Documents Using Results Lists](search-documents-using-results-lists.md)
