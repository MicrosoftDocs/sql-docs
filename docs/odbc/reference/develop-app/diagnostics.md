---
description: "Diagnostics"
title: "Diagnostics | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "diagnostic information [ODBC]"
  - "functions [ODBC], diagnostic information"
  - "diagnostic information [ODBC], about diagnostic information"
ms.assetid: 450abd88-90a1-4fbc-b417-8efbdd8e1dea
author: David-Engel
ms.author: v-davidengel
---
# Diagnostics
Functions in ODBC return diagnostic information in two ways. The return code indicates the overall success or failure of the function, while diagnostic records provide detailed information about the function. At least one diagnostic record - the header record - is returned even if the function succeeds.  
  
 Diagnostic information is used at development time to catch programming errors such as invalid handles and syntax errors in hard-coded SQL statements. It is used at run time to catch run-time errors and warnings such as data truncation, access violations, and syntax errors in SQL statements entered by the user.  
  
 This section contains the following topics.  
  
-   [Return Codes](../../../odbc/reference/develop-app/return-codes-odbc.md)  
  
-   [Diagnostic Records](../../../odbc/reference/develop-app/diagnostic-records.md)  
  
-   [Using SQLGetDiagRec and SQLGetDiagField](../../../odbc/reference/develop-app/using-sqlgetdiagrec-and-sqlgetdiagfield.md)  
  
-   [Implementing SQLGetDiagRec and SQLGetDiagField](../../../odbc/reference/develop-app/implementing-sqlgetdiagrec-and-sqlgetdiagfield.md)  
  
-   [Diagnostic Handling Examples](../../../odbc/reference/develop-app/diagnostic-handling-examples.md)
