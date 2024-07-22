---
title: "State Transition Checks"
description: "State Transition Checks"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "diagnostic information [ODBC], driver manager error checking"
  - "state transition checks [ODBC]"
  - "driver manager [ODBC], error checking"
---
# State Transition Checks
The Driver Manager checks that the state of the environment, connection, or statement is appropriate for the function being called. For example, a connection must be in an allocated state when **SQLConnect** is called; a statement must be in a prepared state when **SQLExecute** is called. The Driver Manager returns SQL_ERROR for state transition errors.
