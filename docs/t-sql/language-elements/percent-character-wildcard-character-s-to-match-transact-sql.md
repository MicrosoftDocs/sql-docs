---
title: "Wildcard search (%)"
description: "Percent character (Wildcard - Character(s) to Match) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/19/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "%"
  - "%_TSQL"
  - "wildcard"
helpviewer_keywords:
  - "conditions [SQL Server], wildcards"
  - "% (wildcard - character(s) to match)"
  - "matching conditions [SQL Server]"
  - "wildcard characters [SQL Server]"
  - "characters [SQL Server], wildcard"
dev_langs:
  - "TSQL"
---
# Percent character (wildcard - character(s) to match) (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Matches any string of zero or more characters. This wildcard character can be used as a prefix, a suffix, or in the middle of the string. The pattern string can contain more than one `%` wildcard.

## Examples

### Example A: Match end of string

The following example returns the first and last names of people in the `Person.Person` table of [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)], where the first name starts with `Dan`.

```sql
SELECT FirstName, LastName
FROM Person.Person
WHERE FirstName LIKE 'Dan%';
GO
```

### Example B: Match middle of string

The following example returns the first and last names of people in the `Person.Person` table of [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)], where the first name starts with `J` and ends with `n`.

```sql
SELECT FirstName, LastName
FROM Person.Person
WHERE FirstName LIKE 'J%n';
GO
```

## See also

- [LIKE (Transact-SQL)](../../t-sql/language-elements/like-transact-sql.md)
- [Operators (Transact-SQL)](../../t-sql/language-elements/operators-transact-sql.md)
- [Expressions (Transact-SQL)](../../t-sql/language-elements/expressions-transact-sql.md)
- [&#91; &#93; (Wildcard - Character(s) to Match)](../../t-sql/language-elements/wildcard-character-s-to-match-transact-sql.md)
- [&#91;^&#93; (Wildcard - Character(s) Not to Match)](../../t-sql/language-elements/wildcard-character-s-not-to-match-transact-sql.md)
- [_ (Wildcard - Match One Character)](../../t-sql/language-elements/wildcard-match-one-character-transact-sql.md)
