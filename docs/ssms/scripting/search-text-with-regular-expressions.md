---
title: "Search text with regular expressions"
description: "Learn how to use regular expressions in the Find what field of a Find and Replace dialog box to specify a pattern to be matched."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/20/2023
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
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
# Search text with regular expressions

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The following regular expressions can replace characters or digits in the **Find what** field of the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **Find and Replace** dialog box.

## Search using regular expressions

1. To enable the use of regular expressions in the **Find what** field during **Quick Find**, **Find in Files**, **Quick Replace**, or **Replace in Files** operations, select the **Use** option under **Find Options** and then choose **Use Regular Expressions**.

1. The **Expression Builder** button next to the **Find what** field then becomes available. Select this button to display a list of the available regular expressions. When you choose any item from the **Expression Builder**, it is inserted into the **Find what** string.

 The following table describes the regular expressions available in the **Expression Builder**.

| Expression | Description |
| --- | --- |
| `.` | Match any single character (except a line break) |
| `.*` | Match any character zero or more times |
| `.+` | Match any character one or more times |
| `[abc]` | Match any character in the set `abc` |
| `[^abc]` | Match any character not in the set `abc` |
| `\d` | Match any numeric character |
| `(?([^\r\n])\s)` | Match any whitespace character |
| `\b` | Match at beginning or end of word |
| `^` | Match at beginning of line |
| `.$` | Match any line break |
| `\w\r?\n` | Match a word character at end of line |
| `(dog|cat)` | Capture and implicitly number the expression `dog|cat` |
| `(?<pet>dog|cat)` | Capture subexpression `dog|cat` and name it `pet` |

## See also

- [Search and Replace](search-and-replace.md)
- [Search Documents Interactively](search-documents-interactively.md)
- [Search Documents Using Results Lists](search-documents-using-results-lists.md)
