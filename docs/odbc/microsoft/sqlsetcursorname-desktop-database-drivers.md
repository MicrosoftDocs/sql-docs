---
title: "SQLSetCursorName (Desktop Database Drivers) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetCursorName function [ODBC], Desktop Database Drivers"
ms.assetid: 9bd7c87b-d99d-4e23-b2db-868d3b461c94
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetCursorName (Desktop Database Drivers)
Because the driver does not support a positioned update or delete by the WHERE CURRENT OF *cursorname* syntax, **SQLSetCursorName** is supported, but cannot be used for positioned updates. It can only be used when the Cursor Library is enabled and the application is using **SQLExtendedFetch**.
