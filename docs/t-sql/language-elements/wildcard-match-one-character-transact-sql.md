---
title: "_ (Wildcard - Match One Character) (Transact-SQL)"
description: Use the underscore character _ to match any single character in a string comparison operation.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/15/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "Match"
  - "wildcard"
  - "_TSQL"
  - "Match One"
  - "_"
helpviewer_keywords:
  - "wildcard characters [SQL Server]"
  - "_ (wildcard - match one character)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# _ (Wildcard - match one character) (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Use the underscore character `_` to match any single character in a string comparison operation that involves pattern matching, such as `LIKE` and `PATINDEX`.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Basic example

The following example returns all database names that begin with the letter `m` and have the letter `d` as the third letter. The underscore character specifies that the second character of the name can be any letter. The `model` and `msdb` databases meet this criteria. The `master` database does not.

```sql
SELECT name FROM sys.databases
WHERE name LIKE 'm_d%';
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
name
-----
model
msdb
```

You might have additional databases that meet this criteria.

You can use multiple underscores to represent multiple characters. Changing the `LIKE` criteria to include two underscores `'m__%` includes the `master` database in the result.

### B. More complex example

The following example uses the `_` operator to find all the people in the `Person` table, who have a three-letter first name that ends in `an`.

```sql
SELECT FirstName, LastName
FROM Person.Person
WHERE FirstName LIKE '_an'
ORDER BY FirstName;
```

### C. Escape the underscore character

The following example returns the names of the fixed database roles like **db_owner** and **db_ddladmin**, but it also returns the **dbo** user.

```sql
SELECT name FROM sys.database_principals
WHERE name LIKE 'db_%';
```

The underscore in the third character position is taken as a wildcard, and isn't filtering for only principals starting with the letters `db_`. To escape the underscore, enclose it in brackets `[_]`.

```sql
SELECT name FROM sys.database_principals
WHERE name LIKE 'db[_]%';
```

Now the `dbo` user is excluded.

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
name
-------------
db_owner
db_accessadmin
db_securityadmin
...
```

## Related content

- [LIKE (Transact-SQL)](like-transact-sql.md)
- [PATINDEX (Transact-SQL)](../functions/patindex-transact-sql.md)
- [Percent character (wildcard - characters to match) (Transact-SQL)](percent-character-wildcard-character-s-to-match-transact-sql.md)
- [\[\] (wildcard - characters to match) (Transact-SQL)](wildcard-character-s-to-match-transact-sql.md)
- [\[^\] (Wildcard - characters not to match) (Transact-SQL)](wildcard-character-s-not-to-match-transact-sql.md)
