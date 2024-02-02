---
title: "SQLGetCursorName (Visual FoxPro ODBC Driver)"
description: "SQLGetCursorName (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLGetCursorName function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLGetCursorName (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Core level.

## Remarks

Returns the name of the cursor associated with the given `hstmt`. `SQLGetCursorName` is included in the Visual FoxPro ODBC Driver API because it's a part of Core level API functionality; it can't be used with other API functions because the driver doesn't support positioned updates.

For more information, see [SQLGetCursorName Function](../reference/syntax/sqlgetcursorname-function.md) in the *ODBC Programmer's Reference*.
