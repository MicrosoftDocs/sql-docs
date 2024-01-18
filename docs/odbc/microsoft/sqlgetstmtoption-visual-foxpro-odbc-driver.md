---
title: "SQLGetStmtOption (Visual FoxPro ODBC Driver)"
description: "SQLGetStmtOption (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLGetStmtOption function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLGetStmtOption (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Level One

## Remarks

Returns the current setting of a statement option.

| `fOption` | Returns |
| --- | --- |
| `SQL_GET_BOOKMARK` | 32-bit integer value that is the bookmark for the current record number |
| `SQL_ROW_NUMBER` | 32-bit integer specifying the position of the current row within the result set |
| `SQL_TRANSLATE_DLL` | Error: `Driver not capable` |

The Visual FoxPro ODBC Driver has no translation DLLs.

For more information, see [SQLGetStmtOption Function](../reference/syntax/sqlgetstmtoption-function.md) in the *ODBC Programmer's Reference*.
