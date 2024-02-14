---
title: "SQLSetConnectOption (Visual FoxPro ODBC Driver)"
description: "SQLSetConnectOption (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLSetConnectOption function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLSetConnectOption (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

Support: Partial

## ODBC API conformance

Level 1.

## Remarks

Sets options that govern aspects of connections. This function is partially supported: The driver supports all values for the `fOption` argument but doesn't support some of `vParam` values for the `fOption` argument SQL_TXN_ISOLATION.

The following table describes only those arguments with behavior specific to the Visual FoxPro ODBC Driver implementation of `SQLSetConnectOption`.

| `fOption` | Remarks |
| --- | --- |
| `SQL_AUTOCOMMIT` | If you choose `SQL_AUTOCOMMIT_OFF`, your application must explicitly commit or roll back transactions with [SQLTransact](sqltransact-visual-foxpro-odbc-driver.md); the Visual FoxPro ODBC Driver doesn't automatically commit a transactable statement upon completion. The driver does begin a transaction if the statement is transactable. |
| `SQL_CURRENT_QUALIFIER` | Can be a fully qualified [database](visual-foxpro-terminology.md) name or fully qualified path to a directory containing zero or more [free tables](visual-foxpro-terminology.md). |
| `SQL_LOGINTIMEOUT` | Returns `Driver not capable` error. |
| `SQL_CURSORS` | Returns `Driver not capable` error. |
| `SQL_PACKET_SIZE` | Returns `Driver not capable` error. |
| `SQL_TXN_ISOLATION` | The driver allows only `SQL_TXN_READ_COMMITTED`.<br /><br />The following `vParam` values aren't supported:<br /><br />`SQL_TXN_READ_UNCOMMITTED`<br />`SQL_TXN_REAPEATABLE_READ`<br />`SQL_TXN_SERIALIZABLE` |

For more information, see [SQLSetConnectOption Function](../reference/syntax/sqlsetconnectoption-function.md) in the *ODBC Programmer's Reference*.
