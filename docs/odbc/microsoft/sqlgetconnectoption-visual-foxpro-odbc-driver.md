---
title: "SQLGetConnectOption (Visual FoxPro ODBC Driver)"
description: "SQLGetConnectOption (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLGetConnectOption function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLGetConnectOption (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

Support: Partial

## ODBC API conformance

Level 1.

## Remarks

Returns the current setting of a connection option. This function is partially supported: the driver supports all values for the `fOption` argument but doesn't support some of `vParam` values for the `fOption` argument `SQL_TXN_ISOLATION`.

The following table describes only those arguments with behavior specific to the Visual FoxPro ODBC Driver implementation of `SQLGetConnectOption`.

| `fOption` | Remarks |
| --- | --- |
| `SQL_AUTOCOMMIT` | If you choose `SQL_AUTOCOMMIT_OFF`, your application must explicitly commit or roll back transactions with [SQLTransact](sqltransact-visual-foxpro-odbc-driver.md); the Visual FoxPro ODBC Driver doesn't automatically commit a transactable statement upon completion. The driver does begin a transaction if the statement is transactable. |
| `SQL_CURRENT_QUALIFIER` | Can be a fully qualified database (`.dbc` file) name or fully qualified path to a directory containing zero or more tables (`.dbf` files). |
| `SQL_LOGINTIMEOUT` | Returns `Driver Not Capable` error. |
| `SQL_CURSORS` | Returns `Driver Not Capable` error. |
| `SQL_PACKET_SIZE` | Returns `Driver Not Capable` error. |
| `SQL_TXN_ISOLATION` | The driver allows only `SQL_TXN_READ_COMMITTED`.<br /><br />The following `vParam` values aren't supported:<br /><br />`SQL_TXN_READ_UNCOMMITTED`<br />`SQL_TXN_REAPEATABLE_READ`<br />`SQL_TXN_SERIALIZABLE` |

For more information, see [SQLGetConnectOption Function](../reference/syntax/sqlgetconnectoption-function.md) in the *ODBC Programmer's Reference*.
