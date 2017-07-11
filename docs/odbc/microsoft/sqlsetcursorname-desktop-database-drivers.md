---
title: "SQLSetCursorName (Desktop Database Drivers) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "SQLSetCursorName function [ODBC], Desktop Database Drivers"
ms.assetid: 9bd7c87b-d99d-4e23-b2db-868d3b461c94
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# SQLSetCursorName (Desktop Database Drivers)
Because the driver does not support a positioned update or delete by the WHERE CURRENT OF *cursorname* syntax, **SQLSetCursorName** is supported, but cannot be used for positioned updates. It can only be used when the Cursor Library is enabled and the application is using **SQLExtendedFetch**.