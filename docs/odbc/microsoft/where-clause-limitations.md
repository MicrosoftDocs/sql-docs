---
description: "WHERE Clause Limitations"
title: "WHERE Clause Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "ODBC SQL grammar, WHERE clause limitations"
  - "WHERE clause limitations [ODBC]"
ms.assetid: 46b54f74-e4a3-4318-87cf-8a97c38a2718
author: David-Engel
ms.author: v-davidengel
---
# WHERE Clause Limitations
The maximum number of clauses in a WHERE clause is 40.  
  
 LONGVARBINARY and LONGVARCHAR columns can be compared to literals of up to 255 characters in length, but cannot be compared using parameters.
