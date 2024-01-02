---
title: "Data type limitations"
description: Learn about the data type limitations for Microsoft ODBC Desktop Database Drivers.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC desktop database drivers [ODBC], data types"
  - "data types [ODBC], desktop database drivers"
  - "desktop database drivers [ODBC], data types"
---
# Data type limitations

The Microsoft ODBC Desktop Database Drivers impose the following limitations on data types:

| Data type | Description |
| --- | --- |
| All data types | Type conversion failures might result in the affected column being set to `NULL`. |
| `BINARY` | Creating a zero-length `BINARY` column actually returns a 255-byte `BINARY` column. |
| `DATE` | The DATE data type can't be converted to another data type (or itself) by the `CONVERT` function. |
| `DECIMAL` (exact numeric) | Not supported. |
| Floating-point data types | The number of decimal places in a floating-point number might be limited by the number format set in the **International** section of the Windows Control Panel. |
| `NUMERIC` | Supports maximum precision and a scale of `28`. |
| `TIMESTAMP` | The `TIMESTAMP` data type can't be converted to itself by the `CONVERT` function. |
| `TINYINT` | `TINYINT` values are always unsigned. |
| Zero-length strings | When a dBASE, Microsoft Excel, Paradox, or Text driver is used, inserting a zero-length string into a column inserts a `NULL` instead. |
