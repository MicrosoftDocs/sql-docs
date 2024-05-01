---
title: "ORDER BY expression-list"
description: "ORDER BY expression-list"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ORDER BY clause [ODBC]"
  - "SQL grammar [ODBC], order by clause"
---
# ORDER BY expression-list
Expressions can be used in the ORDER BY clause. For example, in the following clauses the table is ordered by three key expressions: a+b, c+d, and e.  
  
```  
SELECT * FROM emp  
ORDER BY a+b,c+d,e  
```  
  
 No ordering is allowed on set functions or an expression that contains a set function.
