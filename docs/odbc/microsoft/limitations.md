---
title: "Limitations"
description: "Limitations"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "desktop database drivers [ODBC], limitations"
  - "ODBC desktop database drivers [ODBC], limitations"
---
# Limitations

This section describes limitations of the ODBC Desktop Database Drivers regarding the following sections:

## Function limitations

| Function | Limitations |
| --- | --- |
| Aggregate functions | An aggregate function and a non-aggregate column reference can't both be used as arguments to a single SQL statement. |
| Scalar functions | Scalar functions are supported only by using the ODBC canonical format. |
| Set functions | The set functions (`AVG`, `MAX`, `MIN`, and `SUM`) don't support the `DISTINCT` keyword. |
| Sorting | The maximum length of a sort key in a `GROUP BY` clause, `ORDER BY` clause, `SELECT DISTINCT` statement, or outer join is 255 bytes; the maximum length of all sort keys in a sort row is 65,500 bytes. |
| `CONVERT` | Type conversion failures result in the affected column being set to `NULL`.<br /><br />`DATE` and `TIMESTAMP` data type can't be converted to another data type (or itself) by the `CONVERT` function. |

## Statement limitations

| Statement | Limitations |
| --- | --- |
| `ALTER TABLE` | For more information, see [ALTER TABLE statement limitations](alter-table-statement-limitations.md). |
| `CALL` | Expressions aren't supported as parameters to a called procedure (applies to Microsoft Access driver). |
| `CREATE INDEX` | For more information, see [CREATE INDEX statement limitations](create-index-statement-limitations.md). |
| `CREATE TABLE` | For more information, see [CREATE TABLE statement limitations](create-table-statement-limitations.md). |
| `DELETE` | For more information, see [DELETE statement limitations](delete-statement-limitations.md). |
| `DROP INDEX` | The `DROP INDEX` statement isn't supported (applies to Microsoft Excel or Text drivers). |
| `DROP TABLE` | When the Microsoft Excel 5.0, 7.0, or 97 driver is used, the `DROP TABLE` statement clears the worksheet but doesn't delete the worksheet name. Because the worksheet name still exists in the workbook, another worksheet can't be created with the same name. |
| `INSERT` | For more information, see [INSERT statement limitations](insert-statement-limitations.md). |
| `SELECT DISTINCT` | The `DISTINCT` keyword doesn't apply to binary data. |
| `SELECT` | For more information, see [SELECT statement limitations](select-statement-limitations.md). |
| `UPDATE` | For more information, see [UPDATE statement limitations](update-statement-limitations.md). |

## Clauses, types, and other limitations

| Clause or type | Limitations |
| --- | --- |
| Column names | For more information, see [Column name limitations](column-name-limitations.md). |
| Data types | For more information, see [Data type limitations](data-type-limitations.md). |
| Date arithmetic | Date arithmetic isn't supported for subtracting a `DATE` data type from a `DATE` data type. |
| Identifiers | For more information, see [Identifiers limitations](identifiers-limitations.md). |
| Index name | When the Paradox driver is used, a primary index must have the same name as the table upon which it's defined. Other unique or non-unique indexes must have the same name as the table upon which they're defined. |
| Parameterized query | When the Microsoft Access driver is used, a parameterized query can be called using the following syntax: **CALL *query-name* [ ( *parameter* [ , *parameter* ] ... ) ]**. |
| Reserved keywords | For more information, see [Reserved keyword limitations](reserved-word-limitations.md). |
| `AND` predicate | A maximum of 40 is supported. |
| `DISTINCT` keyword | Not supported for `Long Text` fields (Microsoft Access) or `Memo` fields (dBASE). |
| `FROM` clause | The maximum number of tables in a `FROM` clause is 16. |
| `HAVING` clause | The maximum number of search conditions in a `HAVING` clause is 40. |
| `LIKE` predicate | For more information, see [LIKE predicate limitations](like-predicate-limitations.md). |
| `NOT NULL` | The `NOT NULL` constraint in the `CREATE TABLE` statement isn't supported. |
| `ORDER BY` clause | If a `SELECT` statement contains a `GROUP BY` clause and an `ORDER BY` clause, the `ORDER BY` clause can contain only a column in the result set or an expression in the `GROUP BY` clause. |
| Table names | For more information, see [Table name limitations](table-name-limitations.md). |
| Table references | A maximum of 16 table references can be included in any query statement. |
| Views | Not supported by the dBASE, Microsoft Excel, Paradox, or Text drivers. |
| `WHERE` clause | The maximum number of clauses in a `WHERE` clause is 40.<br /><br />`LONGVARBINARY` and `LONGVARCHAR` columns can be compared to literals of up to 255 characters in length, but can't be compared using parameters. |
| `WHERE CURRENT OF` clause | Not supported. |
| Strings | For more information, see [String limitations](string-limitations.md). |
