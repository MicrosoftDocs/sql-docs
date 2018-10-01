---
title: "State Transition Checks | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "diagnostic information [ODBC], driver manager error checking"
  - "state transition checks [ODBC]"
  - "driver manager [ODBC], error checking"
ms.assetid: 0706db7d-e125-4845-a13a-7fe4308f7360
author: MightyPen
ms.author: genemi
manager: craigg
---
# State Transition Checks
The Driver Manager checks that the state of the environment, connection, or statement is appropriate for the function being called. For example, a connection must be in an allocated state when **SQLConnect** is called; a statement must be in a prepared state when **SQLExecute** is called. The Driver Manager returns SQL_ERROR for state transition errors.
