---
title: "SQLRowCount (Cursor Library)"
description: "SQLRowCount (Cursor Library)"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLRowCount function [ODBC], Cursor Library"
---
# SQLRowCount (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLRowCount** function in the cursor library. For general information about **SQLRowCount**, see [SQLRowCount Function](../../../odbc/reference/syntax/sqlrowcount-function.md).  
  
 When an application calls **SQLRowCount** with the statement associated with the cursor, the cursor library returns the number of rows of data it has retrieved from the driver.  
  
 When an application calls **SQLRowCount** with the statement associated with a positioned update or delete statement, the cursor library returns the number of rows affected by the statement.  
  
 When an application calls **SQLRowCount** after a **SELECT** statement, the cursor library returns -1.
