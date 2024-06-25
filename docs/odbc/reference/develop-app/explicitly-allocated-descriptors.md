---
title: "Explicitly Allocated Descriptors"
description: "Explicitly Allocated Descriptors"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "descriptors [ODBC], allocating and freeing"
  - "explicitly allocated descriptors [ODBC]"
  - "allocating and freeing descriptors [ODBC]"
---
# Explicitly Allocated Descriptors
An application can explicitly allocate an application descriptor on a connection at any time it is connected to the database. By specifying that descriptor handle as an attribute of a statement handle using **SQLSetStmtAttr**, the application directs the driver to use that descriptor in place of the corresponding implicitly allocated application descriptors. The application cannot specify alternate implementation descriptors.  
  
 An application can associate an explicitly allocated descriptor with more than one statement. Only when an application is actually connected to the database can a descriptor be an explicitly allocated descriptor. The application can free such a descriptor explicitly, or implicitly by freeing its connection.
