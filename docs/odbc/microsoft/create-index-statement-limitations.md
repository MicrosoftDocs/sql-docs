---
title: "CREATE INDEX statement limitations"
description: "CREATE INDEX statement limitations"
author: David-Engel
ms.author: davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "CREATE INDEX statement limitations [ODBC]"
  - "ODBC SQL grammar, CREATE INDEX statement limitations"
---
# CREATE INDEX statement limitations

The `CREATE INDEX` statement isn't supported for the Microsoft Excel or Text drivers.

An index can be defined on a maximum of 10 columns. If more than 10 columns are included in a `CREATE INDEX` statement, the index isn't recognized and the table is treated as though no index were created.

The dBASE driver can't create an index on a `LOGICAL` column.

When the dBASE driver is used, response time on large files can be improved by building an `.mdx` (or `.ndx`) index on the column (field) specified in the `WHERE` clauses of a `SELECT` statement. Existing `.mdx` indexes are automatically applied for `=`, `>`, `<`, `>=`, `=<`, and `BETWEEN` operators in a `WHERE` clause, and `LIKE` predicates, as well as in join predicates.

When the dBASE driver is used, the index created by a `CREATE UNIQUE INDEX` statement is nonunique, and duplicate values can be inserted into the indexed column. Only one record from a set with identical key values can be added to the index.

When the Paradox driver is used, a unique index must be defined upon a contiguous subset of the columns in a table, including the first column. A table can't be updated by the Paradox driver if a unique index isn't defined on the table or when the Paradox driver is used without the implementation of the Borland Database Engine.
