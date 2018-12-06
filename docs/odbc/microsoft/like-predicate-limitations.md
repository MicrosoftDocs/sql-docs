---
title: "LIKE Predicate Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "LIKE predicate limitations [ODBC]"
  - "ODBC SQL grammar, LIKE predicate limitations"
ms.assetid: dbd39099-caf6-4c4c-9ad8-f6c63c1bd5e4
author: MightyPen
ms.author: genemi
manager: craigg
---
# LIKE Predicate Limitations
If data in a column is longer than 255 characters, the LIKE comparison will be based only on the first 255 characters.  
  
 A LIKE used in a procedure is supported only with constant patterns. The Desktop Database Drivers support SQL-92 LIKE pattern matching.  
  
 Use of an escape clause in a LIKE predicate is not supported.  
  
 A LIKE comparison should not be performed on a column containing data of a numeric or float data type. The results may be unpredictable. For more information, see the *Microsoft Jet Database Engine Programmer's Guide*.
