---
title: "SQLSetCursorName (Visual FoxPro ODBC Driver)"
description: "SQLSetCursorName (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLSetCursorName function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLSetCursorName (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Core level.

## Remarks

Associates a cursor name with an active statement handle, `hstmt`. `SQLSetCursorName` is included in the Visual FoxPro ODBC Driver API because it's a part of Core Level ODBC API functionality; it can't be used with other API functions because the driver doesn't support positioned updates.

For more information, see [SQLSetCursorName Function](../reference/syntax/sqlsetcursorname-function.md) in the *ODBC Programmer's Reference*.
