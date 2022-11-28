---
description: "Standard Programming Interface"
title: "Standard Programming Interface | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC [ODBC], database access"
  - "SQL [ODBC], database access"
  - "database access [ODBC]"
  - "standardizing database access [ODBC], programming interface"
  - "programming interface standardization [ODBC]"
ms.assetid: a2fa727e-51f2-4123-ae25-0ee28e611231
author: David-Engel
ms.author: v-davidengel
---
# Standard Programming Interface
The programming interface is perhaps the most obvious candidate for standardization. In fact, when ODBC was being developed, ANSI and ISO already provided standards for embedded SQL and SQL modules. Although no standards existed for a database CLI, the SQL Access Group - an industry consortium of database vendors - was considering whether to create one; parts of ODBC later became the basis for their work.  
  
 One of the requirements for ODBC was that a single application binary had to work with multiple DBMSs. It is for this reason that ODBC does not use embedded SQL or module languages. Although the language in embedded SQL and module languages is standardized, each is tied to DBMS-specific precompilers. Thus, applications must be recompiled for each DBMS and the resulting binaries work only with a single DBMS. While this is acceptable for the low-volume applications found in the minicomputer and mainframe worlds, it is unacceptable in the personal computer world. First, it is a logistical nightmare to deliver multiple versions of high-volume, shrink-wrapped software to customers; second, personal computer applications often need to access multiple DBMSs simultaneously.  
  
 On the other hand, a call-level interface can be implemented through libraries, or database drivers, that reside on each local machine; a different driver is required for each DBMS. Because modern operating systems can load such libraries (such as dynamic-link libraries on the Microsoft® Windows® operating system) at run time, a single application can access data from different DBMSs without recompilation and can also access data from multiple databases simultaneously. As new database drivers become available, users can just install these on their computers without having to modify, recompile, or relink their database applications. Furthermore, a call-level interface was a good candidate for ODBC because Windows - the platform for which ODBC was originally developed - already made extensive use of such libraries.
