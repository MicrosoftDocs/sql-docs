---
title: "SQLFetch (Visual FoxPro ODBC Driver)"
description: "SQLFetch (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLFetch function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLFetch (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Core level.

## Remarks

Retrieves one row from a result set into the locations specified by the previous calls to [SQLBindCol](sqlbindcol-visual-foxpro-odbc-driver.md). Prepares the driver for a call to [SQLGetData](sqlgetdata-visual-foxpro-odbc-driver.md) for the unbound columns.

For more information, see [SQLFetch Function](../reference/syntax/sqlfetch-function.md) in the *ODBC Programmer's Reference*.
