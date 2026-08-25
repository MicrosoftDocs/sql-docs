---
title: "[ ] Wildcard to Match Characters (Transact-SQL)"
description: Matches any single character within the specified range or set that is specified between brackets [ ].
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
  - "[ ]"
  - "[]"
  - "[_]_TSQL"
helpviewer_keywords:
  - "wildcard characters [SQL Server]"
  - "[ ] (wildcard - character(s) to match)"
  - "[ ] (wildcard - characters to match)"
  - "[] (wildcard - characters to match)"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# \[\] (wildcard - characters to match) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Matches any single character within the specified range or set that is specified between brackets (`[` and `]`). These wildcard characters can be used in string comparisons that involve pattern matching, such as `LIKE` and `PATINDEX`.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Basic example

The following example returns names that start with the letter `m`. `[n-z]` specifies that the second letter must be somewhere in the range from `n` to `z`. The percent wildcard `%` allows any or no characters starting with the third character. The `model` and `msdb` databases meet this criteria. The `master` database doesn't meet the criteria and is excluded from the result set.

```sql
SELECT name FROM sys.databases
WHERE name LIKE 'm[n-z]%';
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
name
-----
model
msdb
```

You might have more qualifying databases installed.

### B. More complex example

The following example uses the `[]` operator to find the IDs and names of all [!INCLUDE [ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)] employees who have addresses with a four-digit postal code.

```sql
SELECT e.BusinessEntityID, p.FirstName, p.LastName, a.PostalCode
FROM HumanResources.Employee AS e
INNER JOIN Person.Person AS p ON e.BusinessEntityID = p.BusinessEntityID
INNER JOIN Person.BusinessEntityAddress AS ea ON e.BusinessEntityID = ea.BusinessEntityID
INNER JOIN Person.Address AS a ON a.AddressID = ea.AddressID
WHERE a.PostalCode LIKE '[0-9][0-9][0-9][0-9]';
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
EmployeeID      FirstName      LastName      PostalCode
----------      ---------      ---------     ----------
290             Lynn           Tsoflias      3000
```

### C. Use a set that combines ranges and single characters

A wildcard set can include both single characters and ranges. The following example uses the `[]` operator to find a string that begins with a number or a series of special characters.

```sql
SELECT [object_id], OBJECT_NAME(object_id) AS [object_name], name, column_id
FROM sys.columns
WHERE name LIKE '[0-9!@#$.,;_]%';
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
object_id     object_name                          name    column_id
---------     -----------                         ----  ---------
615673241     vSalesPersonSalesByFiscalYears      2002    5
615673241     vSalesPersonSalesByFiscalYears      2003    6
615673241     vSalesPersonSalesByFiscalYears      2004    7
1591676718    JunkTable                           _xyz  1
```

## Related content

- [LIKE (Transact-SQL)](like-transact-sql.md)
- [PATINDEX (Transact-SQL)](../functions/patindex-transact-sql.md)
- [Percent character (wildcard - characters to match) (Transact-SQL)](percent-character-wildcard-character-s-to-match-transact-sql.md)
- [\[^\] (Wildcard - characters not to match) (Transact-SQL)](wildcard-character-s-not-to-match-transact-sql.md)
- [_ (Wildcard - match one character) (Transact-SQL)](wildcard-match-one-character-transact-sql.md)
