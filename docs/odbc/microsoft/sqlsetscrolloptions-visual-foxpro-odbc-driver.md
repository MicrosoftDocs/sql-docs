---
title: "SQLSetScrollOptions (Visual FoxPro ODBC Driver)"
description: "SQLSetScrollOptions (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLSetScrollOptions function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLSetScrollOptions (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

Support: Partial

## ODBC API conformance

Level 2.

## Remarks

Sets options that control the behavior of cursors associated with a statement handle, `hstmt`.

The Visual FoxPro ODBC Driver supports only SQL_CONCUR_READ_ONLY; it doesn't support the `fConcurrency` value `SQL_CONCUR_ROWVER`. The driver converts `SQL_KEYSET_SIZE`, `SQL_CURSOR_DYNAMIC`, and `SQL_CURSOR_KEYSET_DRIVEN` to `SQL_SCROLL_STATIC` with warning ODBC_01S02.

For more information, see [SQLSetScrollOptions Function](../reference/syntax/sqlsetscrolloptions-function.md) in the *ODBC Programmer's Reference*.
