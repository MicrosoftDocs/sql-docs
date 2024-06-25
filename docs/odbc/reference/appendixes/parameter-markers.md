---
title: "Parameter Markers"
description: "Parameter Markers"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "minimum SQL syntax supported [ODBC]"
  - "ODBC drivers [ODBC], minimum SQL syntax supported"
  - "parameter markers [ODBC]"
---
# Parameter Markers
In accordance with the SQL-92 specification, an application cannot place parameter markers in the following locations. For a more comprehensive list, see the SQL-92 specification.  
  
-   In a **SELECT** list  
  
-   As both *expressions* in a *comparison-predicate*  
  
-   As both operands of a binary operator  
  
-   As both the first and second operands of a **BETWEEN** operation  
  
-   As both the first and third operands of a **BETWEEN** operation  
  
-   As both the expression and the first value of an **IN** operation  
  
-   As the operand of a unary + or - operation  
  
-   As the argument of a *set-function-reference*  
  
 For more information about parameter markers, see the SQL-92 specification. For more information about parameters, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md).
