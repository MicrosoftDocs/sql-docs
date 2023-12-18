---
title: "SQLPrimaryKeys (Visual FoxPro ODBC Driver)"
description: "SQLPrimaryKeys (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLPrimaryKeys function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLPrimaryKeys (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Level 2.

## Remarks

Returns the column names that comprise the primary key for a table. The Visual FoxPro ODBC Driver implementation of `SQLPrimaryKeys` behaves as follows:

- Ignores the `szTableOwner` and `cbTableOwner` arguments.

- Works only for data sources that are [databases](visual-foxpro-terminology.md). The driver returns the error "Driver doesn't support this function" if the data source is a directory of [free tables](visual-foxpro-terminology.md).

For more information, see [SQLPrimaryKeys Function](../reference/syntax/sqlprimarykeys-function.md) in the *ODBC Programmer's Reference*.
