---
title: "Thread Support (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "thread support [ODBC]"
  - "Visual FoxPro ODBC driver [ODBC], thread support"
  - "FoxPro ODBC driver [ODBC], thread support"
  - "multithreaded applications [ODBC]"
ms.assetid: 0c6abbbc-012b-41aa-bded-5e7e362d015b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Thread Support (Visual FoxPro ODBC Driver)
The Visual FoxPro ODBC Driver is thread-safe. Access to environment handles (*hen*), connection handles (*hdbc*), and statement handles (*hstmt*) is wrapped in appropriate semaphores to prevent other processes from accessing and potentially altering the driver's internal data structures.  
  
 In a multithreaded application, you can cancel a function that is running synchronously on an *hstmt* by calling [SQLCancel](../../odbc/microsoft/sqlcancel-visual-foxpro-odbc-driver.md) on a separate thread.  
  
 The driver uses a separate thread to fetch data when you use progressive fetching. To use progressive fetching for a data source, select the **Fetch data in background** check box on the [ODBC Visual FoxPro Setup dialog box](../../odbc/microsoft/odbc-visual-foxpro-setup-dialog-box.md) or use the BackgroundFetch attribute keyword in your connection string. Avoid using background fetch when you call the driver from multithreaded applications. For information about connection string attribute keywords, see [Using Connection Strings](../../odbc/microsoft/using-connection-strings.md).  
  
 For more information about threads and **SQLCancel**, see [SQLCancel](../../odbc/reference/syntax/sqlcancel-function.md) in the *ODBC Programmer's Reference*.
