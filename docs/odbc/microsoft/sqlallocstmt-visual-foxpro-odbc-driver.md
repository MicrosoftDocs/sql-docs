---
title: "SQLAllocStmt (Visual FoxPro ODBC Driver)"
description: "SQLAllocStmt (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLAllocStmt function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLAllocStmt (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Core level.

## Remarks

Allocates memory for a statement handle and associates the statement handle with the connection specified by `hdbc`. The Driver Manager passes this call to the driver, which allocates the memory for the `hstmt` structure.

For more information, see [SQLAllocStmt Function](../reference/syntax/sqlallocstmt-function.md) in the *ODBC Programmer's Reference*.
