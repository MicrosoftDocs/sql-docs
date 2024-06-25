---
title: "Fixed-Length Bookmarks"
description: "Fixed-Length Bookmarks"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "backward compatibility [ODBC], bookmarks"
  - "bookmarks [ODBC]"
  - "compatibility [ODBC], bookmarks"
  - "fixed-length bookmarks [ODBC]"
---
# Fixed-Length Bookmarks
If an ODBC *3.x* driver should work with an ODBC *2.x* application that uses fixed-length bookmarks, the driver must support the following:  
  
-   SQL_UB_ON as a value for the SQL_USE_BOOKMARKS statement option. (SQL_UB_ON is deprecated in ODBC *3.x*.)  
  
-   The SQL_GET_BOOKMARK statement option.
