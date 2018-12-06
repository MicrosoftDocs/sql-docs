---
title: "Environment Handles | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "environment handles [ODBC]"
  - "handles [ODBC], environment"
ms.assetid: 917f1b0c-272b-4e37-a1f5-87cd24b9fa21
author: MightyPen
ms.author: genemi
manager: craigg
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
