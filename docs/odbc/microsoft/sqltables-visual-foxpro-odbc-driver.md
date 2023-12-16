---
title: "SQLTables (Visual FoxPro ODBC Driver)"
description: "SQLTables (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLTables function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLTables (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Level 1.

## Remarks

Returns the list of table names specified by the parameter in the `SQLTables` statement. If no parameter is specified, returns the table names stored in the current data source. The driver returns the information as a result set.

Enumeration type calls will not receive a result set entry for remote views or local parameterized views. However, a call to `SQLTables` with a unique table name specifier will find a match for such a view if present with that name; this allows the API to be used to check for name conflicts prior to creation of a new table.

> [!NOTE]  
> The Visual FoxPro ODBC driver differentiates between [database tables](visual-foxpro-terminology.md) and [free tables](visual-foxpro-terminology.md), even when both types of tables are stored in the same directory on your system. If your data source is a directory of free tables, the Visual FoxPro ODBC Driver doesn't catalog or return the names of any tables that are associated with a database.

For more information, see [SQLTables Function](../reference/syntax/sqltables-function.md) in the *ODBC Programmer's Reference*.
