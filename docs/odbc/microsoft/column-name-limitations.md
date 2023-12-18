---
title: "Column name limitations"
description: "Column name limitations"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "desktop database drivers [ODBC], column names"
  - "ODBC desktop database drivers [ODBC], column names"
---
# Column name limitations

Column names can contain any valid characters (for example, spaces). If column names contain any characters except letters, numbers, and underscores, the name must be delimited by enclosing it in back quotes (`).

When the Microsoft Access or Microsoft Excel driver is used, column names are limited to 64 characters, and longer names generate an error. When the Paradox driver is used, the maximum column name is 25 characters. When the Text driver is used, the maximum column name is 64 characters, and longer names are truncated.

When the dBASE driver is used, characters with an ASCII value greater than 127 are converted to underscores.

When the Microsoft Excel driver is used, if column names are present, they must be in the first row. A name that in Microsoft Excel would use the `!` character must be enclosed in back quotes (`` ` ``). The `!` character is converted to the `$` character, because the `!` character isn't legal in an ODBC name, even when the name is enclosed in back quotes. All other valid Microsoft Excel characters (except the pipe character `|`) can be used in a column name, including spaces. A delimited identifier must be used for a Microsoft Excel column name to include a space. Unspecified column names are replaced with driver-generated names, for example, `Col1` for the first column.

The pipe character (`|`) can't be used in a column name, whether the name is enclosed in back quotes or not.

When the Text driver is used, the driver provides a default name if a column name isn't specified. For example, the driver calls the first column `F1`, the second column `F2`, and so on.
