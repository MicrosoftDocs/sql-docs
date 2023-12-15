---
title: "SQLSpecialColumns (Visual FoxPro ODBC Driver)"
description: "SQLSpecialColumns (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLSpecialColumns function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLSpecialColumns (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Level 1.

## Remarks

Retrieves the optimal set of columns that uniquely identifies a row in the table.

The Visual FoxPro ODBC Driver returns the columns that make up the primary key on the FoxPro table. (See [SQLPrimaryKeys](sqlprimarykeys-visual-foxpro-odbc-driver.md).) If called with `fColType` set to `SQL_ROWVER`, no columns are returned. `SQLSpecialColumns` works only for data sources that are [databases](visual-foxpro-terminology.md).

For more information, see [SQLSpecialColumns Function](../reference/syntax/sqlspecialcolumns-function.md) in the *ODBC Programmer's Reference*.
