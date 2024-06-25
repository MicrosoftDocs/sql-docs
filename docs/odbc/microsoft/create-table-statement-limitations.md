---
title: CREATE TABLE statement limitations
description: CREATE TABLE statement limitations.
author: David-Engel
ms.author: davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "CREATE TABLE statement limitations [ODBC]"
  - "ODBC SQL grammar, CREATE TABLE statement limitations"
---
# CREATE TABLE statement limitations

When the Microsoft Access, Microsoft Excel, or Paradox driver is used, and the length of a text or binary column isn't specified (or is specified as `0`), the column length is set to `255`.

When the dBASE driver is used, and the length of a text or binary column isn't specified (or is specified as `0`), the column length is set to `254`.

A maximum of `255` columns is supported.

When the Microsoft Excel driver is used on a MicrosoftExcel 5.0, 7.0, or 97 data source, a worksheet can't be created with the same name as a worksheet that was previously dropped. When the Microsoft Excel driver is used to access a version 5.0, 7.0, or 97 worksheet, a `DROP TABLE` statement clears the worksheet, but doesn't delete the worksheet name.

When the Paradox driver is used, columns can't be added once an index is defined on a table. If the first column of the argument list of a `CREATE TABLE` statement creates an index, a second column can't be included in the argument list.
