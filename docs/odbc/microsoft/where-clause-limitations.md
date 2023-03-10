---
title: "WHERE Clause Limitations"
description: "WHERE Clause Limitations"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC SQL grammar, WHERE clause limitations"
  - "WHERE clause limitations [ODBC]"
---
# WHERE Clause Limitations
The maximum number of clauses in a WHERE clause is 40.  
  
 LONGVARBINARY and LONGVARCHAR columns can be compared to literals of up to 255 characters in length, but cannot be compared using parameters.
