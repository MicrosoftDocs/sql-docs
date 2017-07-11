---
title: "ORDER BY expression-list | Microsoft Docs"
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
  - "ORDER BY clause [ODBC]"
  - "SQL grammar [ODBC], order by clause"
ms.assetid: 5ef88186-a99f-4e2c-a3f3-98a42d4f03a5
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# ORDER BY expression-list
Expressions can be used in the ORDER BY clause. For example, in the following clauses the table is ordered by three key expressions: a+b, c+d, and e.  
  
```  
SELECT * FROM emp  
ORDER BY a+b,c+d,e  
```  
  
 No ordering is allowed on set functions or an expression that contains a set function.