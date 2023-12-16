---
title: "SQLFreeStmt (Visual FoxPro ODBC Driver)"
description: "SQLFreeStmt (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLFreeStmt function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLFreeStmt (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Core level.

## Remarks

Stops processing associated with a specific `hstmt`, closes any open cursors associated with the `hstmt`, discards pending results, and optionally frees all resources associated with the statement handle.

For more information, see [SQLFreeStmt Function](../reference/syntax/sqlfreestmt-function.md) in the *ODBC Programmer's Reference*.
