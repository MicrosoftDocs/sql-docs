---
title: "Table name limitations"
description: "Table name limitations"
author: David-Engel
ms.author: davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC SQL grammar, table name limitations"
  - "table name limitations [ODBC]"
  - "Excel driver [ODBC], table name limitations"
---
# Table name limitations

Table names can contain any valid characters (for example, spaces). If table names contain any characters except letters, numbers, and underscores, the name must be delimited by enclosing it in back quotes (`).

When the Microsoft Excel driver is used, and a table name isn't qualified by a database reference, the default database is implied. If a name in Microsoft Excel includes the `!` character, it's automatically translated to the `$` character instead.

The Microsoft Excel table name that references \<filename> is supported for Microsoft Excel 3.0 and 4.0 files. The Microsoft Excel table name that references `<workbook-name>` is supported for Microsoft Excel 5.0, 7.0, or 97 files.

When the dBASE driver is used, characters with an ASCII value greater than 127 are converted to underscores.

When the Microsoft Access driver is used, the table name is limited to 64 characters.

When the dBASE, Microsoft Excel 3.0 or 4.0, Paradox, or Text driver is used, special MS-DOS keywords `CON`, `AUX`, `LPT1`, and `LPT2` shouldn't be used as table names.
