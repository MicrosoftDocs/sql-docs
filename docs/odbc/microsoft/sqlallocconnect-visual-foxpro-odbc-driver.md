---
title: "SQLAllocConnect (Visual FoxPro ODBC Driver)"
description: "SQLAllocConnect (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLAllocConnect function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLAllocConnect (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Core level.

## Remarks

Allocates memory for a connection handle, `hdbc`, within the environment identified by `henv`. The Driver Manager processes this call and calls the driver's `SQLAllocConnect` whenever [SQLConnect](sqlconnect-visual-foxpro-odbc-driver.md), `SQLBrowseConnect`, or [SQLDriverConnect](sqldriverconnect-visual-foxpro-odbc-driver.md) is called.

For more information, see [SQLAllocConnect Function](../reference/syntax/sqlallocconnect-function.md) in the *ODBC Programmer's Reference*.
