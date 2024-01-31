---
title: "SQLBindCol (Visual FoxPro ODBC Driver)"
description: "SQLBindCol (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLBindCol function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLBindCol (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Core level.

## Remarks

Assigns storage space for a result column and specifies the type of the result. When [SQLFetch](sqlfetch-visual-foxpro-odbc-driver.md) or [SQLExtendedFetch](sqlextendedfetch-visual-foxpro-odbc-driver.md) is called, the driver places the data for all bound columns in the assigned locations. See [SQLGetTypeInfo](sqlgettypeinfo-visual-foxpro-odbc-driver.md) for the mapping between ODBC and Visual FoxPro data types.

For more information, see [SQLBindCol Function](../reference/syntax/sqlbindcol-function.md) in the *ODBC Programmer's Reference*.
