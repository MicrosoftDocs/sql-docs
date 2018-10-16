---
title: "Multiple hstmts (Paradox Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "multiple hstmts [ODBC]"
  - "Paradox driver [ODBC], multiple hstmts"
ms.assetid: 66aecd94-092d-43d4-9583-74f5e2990eac
author: MightyPen
ms.author: genemi
manager: craigg
---
# Multiple hstmts (Paradox Driver)
When the ODBC Paradox driver is used, if you want to use more than one *hstmt* to execute queries on a table, the table must have a unique index (Paradox primary key).
