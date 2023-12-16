---
title: "SQLSetPos (Visual FoxPro ODBC Driver)"
description: "SQLSetPos (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLSetPos function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLSetPos (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Level 2.

## Remarks

Sets the cursor position in a rowset. You can use `SQLSetPos` with [SQLGetData](sqlgetdata-visual-foxpro-odbc-driver.md) to retrieve rows from unbound columns after positioning the cursor to a specific row in the rowset.

For more information, see [SQLSetPos Function](../reference/syntax/sqlsetpos-function.md) in the *ODBC Programmer's Reference*.
