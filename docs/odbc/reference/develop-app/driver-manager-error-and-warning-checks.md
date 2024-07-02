---
title: "Driver Manager Error and Warning Checks"
description: "Driver Manager Error and Warning Checks"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "diagnostic information [ODBC], driver manager error checking"
  - "driver manager [ODBC], error checking"
---
# Driver Manager Error and Warning Checks
The Driver Manager completely or partially implements a number of functions and therefore checks for all or some of the errors and warnings in those functions.  
  
-   The Driver Manager implements **SQLDataSources** and **SQLDrivers** and checks for all errors and warnings in these functions.  
  
-   The Driver Manager checks whether a driver implements **SQLGetFunctions**. If the driver does not implement **SQLGetFunctions**, the Driver Manager implements and checks for all errors and warnings in it.  
  
-   The Driver Manager partially implements **SQLAllocHandle**, **SQLConnect**, **SQLDriverConnect**, **SQLBrowseConnect**, **SQLFreeHandle**, **SQLGetDiagRec**, and **SQLGetDiagField** and checks for some errors in these functions. It may return the same errors as the driver for some of these functions because both perform similar operations. For example, the Driver Manager or driver may return SQLSTATE IM008 (Dialog failed) if either one is unable to display a login dialog box for **SQLDriverConnect**.
