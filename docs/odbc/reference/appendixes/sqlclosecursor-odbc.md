---
title: "SQLCloseCursor_ODBC | Microsoft Docs"
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
  - "SQLCloseCursor function [ODBC], ODBC"
ms.assetid: 5e47e3f7-e1b8-451f-bf75-daa19b7c7271
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# SQLCloseCursor_ODBC
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLCloseCursor** function in the cursor library. For general information about **SQLCloseCursor**, see [SQLCloseCursor Function](../../../odbc/reference/syntax/sqlclosecursor-function.md).  
  
 The cursor library does not support calling **SQLCloseCursor** without an open cursor. Attempting this will return SQLSTATE 24000 (Invalid cursor state). Calling **SQLFreeStmt** with an *Option* of SQL_CLOSE when no cursor is open is supported by the cursor library.