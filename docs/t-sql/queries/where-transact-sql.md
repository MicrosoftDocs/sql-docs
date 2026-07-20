---
title: "WHERE (Transact-SQL)"
description: The WHERE clause specifies the search condition for the rows returned by the query.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 10/01/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "WHERE_TSQL"
  - "WHERE"
helpviewer_keywords:
  - "retrieving rows"
  - "clauses [SQL Server], WHERE"
  - "WHERE clause, about WHERE clause"
  - "row retrieval [SQL Server], WHERE clause"
  - "WHERE clause"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# WHERE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Specifies the search condition for the rows returned by the query.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[ WHERE <search_condition> ]
```

## Arguments

#### \<*search_condition*>

Defines the condition to be met for the rows to be returned. There's no limit to the number of predicates that can be included in a search condition. For more information about search conditions and predicates, see [Search condition](search-condition-transact-sql.md).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

The following examples show how to use some common search conditions in the `WHERE` clause.

### A. Find a row by using a simple equality

```sql
-- Uses AdventureWorksDW
SELECT EmployeeKey, LastName
FROM DimEmployee
WHERE LastName = 'Smith';
```

### B. Find rows that contain a value as part of a string

```sql
-- Uses AdventureWorksDW
SELECT EmployeeKey, LastName
FROM DimEmployee
WHERE LastName LIKE '%Smi%';
```

### C. Find rows by using a comparison operator

```sql
-- Uses AdventureWorksDW
SELECT EmployeeKey, LastName
FROM DimEmployee
WHERE EmployeeKey <= 500;
```

### D. Find rows that meet any of three conditions

```sql
-- Uses AdventureWorksDW
SELECT EmployeeKey, LastName
FROM DimEmployee
WHERE EmployeeKey = 1
      OR EmployeeKey = 8
      OR EmployeeKey = 12;
```

### E. Find rows that must meet several conditions

```sql
-- Uses AdventureWorksDW
SELECT EmployeeKey, LastName
FROM DimEmployee
WHERE EmployeeKey <= 500
      AND LastName LIKE '%Smi%'
      AND FirstName LIKE '%A%';
```

### F. Find rows that are in a list of values

```sql
-- Uses AdventureWorksDW
SELECT EmployeeKey, LastName
FROM DimEmployee
WHERE LastName IN ('Smith', 'Godfrey', 'Johnson');
```

### G. Find rows that have a value between two values

```sql
-- Uses AdventureWorksDW
SELECT EmployeeKey, LastName
FROM DimEmployee
WHERE EmployeeKey BETWEEN 100 AND 200;
```

## Related content

- [DELETE (Transact-SQL)](../statements/delete-transact-sql.md)
- [Predicates](predicates.md)
- [Search condition (Transact-SQL)](search-condition-transact-sql.md)
- [SELECT (Transact-SQL)](select-transact-sql.md)
- [UPDATE (Transact-SQL)](update-transact-sql.md)
- [MERGE (Transact-SQL)](../statements/merge-transact-sql.md)
