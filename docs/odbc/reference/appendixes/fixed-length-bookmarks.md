---
title: "Fixed-Length Bookmarks | Microsoft Docs"
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
  - "backward compatibility [ODBC], bookmarks"
  - "bookmarks [ODBC]"
  - "compatibility [ODBC], bookmarks"
  - "fixed-length bookmarks [ODBC]"
ms.assetid: cbd8185e-fb03-408f-b80b-1a2e164534fd
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Fixed-Length Bookmarks
If an ODBC 3*.x* driver should work with an ODBC 2.*x* application that uses fixed-length bookmarks, the driver must support the following:  
  
-   SQL_UB_ON as a value for the SQL_USE_BOOKMARKS statement option. (SQL_UB_ON is deprecated in ODBC 3*.x*.)  
  
-   The SQL_GET_BOOKMARK statement option.