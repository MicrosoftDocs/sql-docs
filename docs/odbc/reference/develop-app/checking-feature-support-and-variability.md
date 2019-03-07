---
title: "Checking Feature Support and Variability | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interoperability [ODBC], feature support and variability"
  - "interoperability [ODBC], writing interoperable applications"
  - "feature support in interoperable applications [ODBC]"
  - "feature variability in interoperable applications [ODBC]"
ms.assetid: ff45f220-9b8b-4c44-82f8-a8e9913fffea
author: MightyPen
ms.author: genemi
manager: craigg
---
# Checking Feature Support and Variability
To check feature support and variability, applications generally call **SQLGetInfo**, **SQLGetFunctions**, and **SQLGetTypeInfo**. A good starting place is the driver's API and SQL grammar conformance levels. These describe broad levels of feature support. The application can then call **SQLGetInfo** with other options to determine the support or variability of features it needs, **SQLGetFunctions** to determine whether functions it needs beyond the returned conformance level are supported, and **SQLGetTypeInfo** to determine what SQL data types are supported.  
  
 An application can determine whether a statement or connection attribute is supported by calling **SQLSetStmtAttr** or **SQLSetConnectAttr** with that attribute. If the function returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, the attribute is supported; if it returns SQL_ERROR and SQLSTATE HYC00 (Optional feature not implemented), the attribute is not supported.  
  
 Applications can also determine a limited amount of information before connecting to the driver by calling **SQLDrivers**.
