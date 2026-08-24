---
title: " [^] Wildcard to Exclude Characters (Transact-SQL)"
description: Matches any single character that isn't within the range or set specified between the square brackets [^].
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/15/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "wildcard"
  - "[^]_TSQL"
  - "[^]"
  - "Not Match"
helpviewer_keywords:
  - "wildcard characters [SQL Server]"
  - "[^] (wildcard - character(s) not to match)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# \[^\] (Wildcard - characters not to match) (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Matches any single character that isn't within the range or set specified between the square brackets `[^]`. These wildcard characters can be used in string comparisons that involve pattern matching, such as `LIKE` and `PATINDEX`.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A: Basic example

The following example uses the `[^]` operator to find the top five people in the `Contact` table who have a first name that starts with `Al` and has a third letter that isn't the letter `a`.

```sql
SELECT TOP 5 FirstName, LastName
FROM Person.Person
WHERE FirstName LIKE 'Al[^a]%';
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
FirstName     LastName
---------     --------
Alex          Adams
Alexandra     Adams
Allison       Adams
Alisha        Alan
Alexandra     Alexander
```

### B: Searching for ranges of characters

A wildcard set can include single characters or ranges of characters, as well as combinations of characters and ranges. The following example uses the `[^]` operator to find a string that doesn't begin with a letter or number.

```sql
SELECT [object_id], OBJECT_NAME(object_id) AS [object_name], name, column_id
FROM sys.columns
WHERE name LIKE '[^0-9A-z]%';
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
object_id     object_name   name    column_id
---------     -----------   ----    ---------
1591676718    JunkTable     _xyz    1
```

## Related content

- [LIKE (Transact-SQL)](like-transact-sql.md)
- [PATINDEX (Transact-SQL)](../functions/patindex-transact-sql.md)
- [Percent character (wildcard - characters to match) (Transact-SQL)](percent-character-wildcard-character-s-to-match-transact-sql.md)
- [\[\] (wildcard - characters to match) (Transact-SQL)](wildcard-character-s-to-match-transact-sql.md)
- [_ (Wildcard - match one character) (Transact-SQL)](wildcard-match-one-character-transact-sql.md)
