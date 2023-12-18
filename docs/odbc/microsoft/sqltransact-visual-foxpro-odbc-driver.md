---
title: "SQLTransact (Visual FoxPro ODBC Driver)"
description: "SQLTransact (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLTransact function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLTransact (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Core level.

## Remarks

Requests a commit or rollback operation for all active operations on all statement handles (`hstmt`) associated with a connection or for all connections associated with the environment handle, `henv`. `SQLTransact` works only for data sources that are [databases](visual-foxpro-terminology.md).

If a commit fails when in manual mode, the transaction remains active; you can choose to roll back the transaction or retry the commit operation. If a commit operation fails when in automatic transaction mode, the transaction is rolled back automatically; the transaction can't be inactive.

For more information, see [SQLTransact Function](../reference/syntax/sqltransact-function.md) in the *ODBC Programmer's Reference*.
