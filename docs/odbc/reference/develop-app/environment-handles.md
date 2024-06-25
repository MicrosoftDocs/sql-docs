---
title: "Environment Handles"
description: "Environment Handles"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "environment handles [ODBC]"
  - "handles [ODBC], environment"
---
# Environment Handles
An *environment* is a global context in which to access data; associated with an environment is any information that is global in nature, such as:  
  
-   The environment's state  
  
-   The current environment-level diagnostics  
  
-   The handles of connections currently allocated on the environment  
  
-   The current settings of each environment attribute  
  
 Within a piece of code that implements ODBC (the Driver Manager or a driver), an environment handle identifies a structure to contain this information.  
  
 Environment handles are not frequently used in ODBC applications. They are always used in calls to **SQLDataSources** and **SQLDrivers** and sometimes used in calls to **SQLAllocHandle**, **SQLEndTran**, **SQLFreeHandle**, **SQLGetDiagField**, and **SQLGetDiagRec**.  
  
 Each piece of code that implements ODBC (the Driver Manager or a driver) contains one or more environment handles. For example, the Driver Manager maintains a separate environment handle for each application that is connected to it. Environment handles are allocated with **SQLAllocHandle** and freed with **SQLFreeHandle**.
