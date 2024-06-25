---
title: "INSERT statement limitations"
description: "INSERT statement limitations"
author: David-Engel
ms.author: davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC SQL grammar, INSERT statement limitations"
  - "INSERT statement limitations [ODBC]"
  - "truncation of data [ODBC]"
---
# INSERT statement limitations

Inserted data is truncated on the right without warning if it's too long to fit into the column.

Attempting to insert a value that is out of the range of a column's data type causes a `NULL` to be inserted into the column.

When a dBASE, Microsoft Excel, Paradox, or Text driver is used, inserting a zero-length string into a column actually inserts a `NULL` instead.

When the Microsoft Excel driver is used, if an empty string is inserted into a column, the empty string is converted to a `NULL`; a searched SELECT statement that is executed with an empty string in the `WHERE` clause doesn't succeed on that column.

A table isn't updatable by the Paradox driver under two conditions:

- When a unique index isn't defined on the table. This isn't true for an empty table, which can be updated with a single row even if a unique index isn't defined on the table. If a single row is inserted in an empty table that doesn't have a unique index, an application can't create a unique index or insert more data after the single row is inserted.

- If the Borland Database Engine isn't implemented, only read and append statements are allowed on the Paradox table.

When the Text driver is used, `NULL` values are represented by a blank-padded string in fixed-length files, but are represented by no spaces in delimited files. For example, in the following row containing three fields, the second field is a `NULL` value:

```output
"Smith:,, 123
```

When the Text driver is used, all column values can be padded with leading spaces. The length of any row must be less than or equal to 65,543 bytes.
