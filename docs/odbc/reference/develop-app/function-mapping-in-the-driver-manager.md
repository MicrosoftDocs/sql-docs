---
title: "Function Mapping in the Driver Manager | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Unicode [ODBC], functions"
  - "driver manager [ODBC], function mapping"
  - "functions [ODBC], Unicode functions"
ms.assetid: ff093b29-671a-4fc0-86c9-08a311a98e54
author: MightyPen
ms.author: genemi
manager: craigg
---
# Function Mapping in the Driver Manager
The driver manager supports two entry points for functions that take string arguments. The undecorated function (**SQLDriverConnect**) is the ANSI form of the function. The Unicode form is decorated with a *W* (**SQLDriverConnectW**.)  
  
 The ODBC header file also supports functions decorated with an *A,* (**SQLDriverConnectA**) for the convenience of mixed ANSI/Unicode applications. Calls made to the **A** functions are actually calls into the undecorated entry point (**SQLDriverConnect**.)  
  
 If the application is compiled with the _UNICODE **#define**, the ODBC header file will map undecorated function calls (**SQLDriverConnect**) to the Unicode version (**SQLDriverConnectW**.)  
  
 The Driver Manager recognizes a driver as a Unicode driver if **SQLConnectW** is supported by the driver.  
  
 If the driver is a Unicode driver, the Driver Manager makes function calls as follows:  
  
-   Passes a function without string arguments or parameters directly through to the driver.  
  
-   Passes Unicode functions (with the *W* suffix) directly through to the driver.  
  
-   Converts an ANSI function (with the *A* suffix) to a Unicode function (with the *W* suffix) by converting the string arguments into Unicode characters and passes the Unicode function to the driver.  
  
 If the driver is an ANSI driver, the Driver Manager makes function calls as follows:  
  
-   Passes functions without string arguments or parameters directly through to the driver.  
  
-   Converts Unicode functions (with the *W* suffix) to an ANSI function call and passes it to the driver.  
  
-   Passes an ANSI function directly to the driver.  
  
 The Driver Manager is Unicode-enabled internally. As a result, the optimum performance is obtained by a Unicode application working with a Unicode driver, because the Driver Manager simply passes Unicode functions through to the driver. When an ANSI application is working with an ANSI driver, the Driver Manager must convert strings from ANSI to Unicode when processing some functions, such as **SQLDriverConnect**. After processing the function, the Driver Manager must then convert the Unicode string back to ANSI before sending the function to the ANSI driver.  
  
 An application should not modify or read its bound parameter buffers when the driver returns SQL_STILL_EXECUTING or SQL_NEED_DATA. The Driver Manager leaves the buffers bound to ANSI until the driver returns SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, or SQL_ERROR. A multithreaded application should not gain access to any bound parameter values that another thread is executing an SQL statement on. The Driver Manager converts the data from Unicode to ANSI "in place," and the other thread might see ANSI data in these buffers while the driver is still processing the SQL statement. Applications that bind Unicode data to an ANSI driver must not bind two different columns to the same address.
