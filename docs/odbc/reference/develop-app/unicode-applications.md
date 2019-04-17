---
title: "Unicode Applications | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Unicode [ODBC], compiling as Unicode application"
  - "Unicode [ODBC], functions"
  - "compiling Unicode applications [ODBC]"
  - "functions [ODBC], Unicode functions"
ms.assetid: 7986c623-2792-4e77-bfee-c86cbf84f08d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Unicode Applications
You can recompile an application as a Unicode application in one of two ways:  
  
-   Include the Unicode **#define** contained in the Sqlucode.h header file in the application.  
  
-   Compile the application with the compiler's Unicode option. (This option will be different for different compilers.)  
  
 To convert an ANSI application to a Unicode application, write the application to store and pass Unicode data. In addition, calls to functions that support SQLPOINTER arguments must be converted to use count of bytes.  
  
 After an application is compiled as a Unicode application, if the application calls an ODBC API function (without a suffix), the Driver Manager recognizes the application as a Unicode application and converts the function call to a Unicode function (with the *W* suffix) if the underlying driver supports Unicode. When an ANSI application makes a function call without a suffix, the Driver Manager converts it to ANSI if the underlying driver supports ANSI. If both the application and the driver support the same character encoding, the driver manager passes the calls through to the driver (with certain exceptions for ANSI applications).  
  
 An application can call both Unicode functions (with the *W* suffix) and ANSI functions (with or without the *A* suffix). Unicode and ANSI function calls can be mixed. If the cursor library is to be used, however, Unicode and ANSI function calls cannot be mixed. The cursor library is either Unicode or ANSI, not a mixture.  
  
 An application can be written such that it can be compiled as either a Unicode application or an ANSI application. In this case, character data types can be declared as SQL_C_TCHAR. This is a macro that inserts SQL_C_WCHAR if the application is compiled as a Unicode application or inserts SQL_C_CHAR if it is compiled as an ANSI application. The application programmer must be careful of functions that take SQLPOINTER as their argument, because the size of the length argument will change (for string data types) depending on whether the application is ANSI or Unicode.  
  
 A function can be called in one of three ways: as a Unicode-only function call (with the *W* suffix), as an ANSI-only function call (with the *A* suffix), or as the ODBC function call with no suffix. The arguments to the three forms of a function are identical. Only those functions with SQLCHAR \* arguments or SQLPOINTER arguments that point to strings require Unicode and ANSI forms. For functions that have arguments that can be declared as a character type, such as **SQLBindCol** or **SQLGetData** (which do not have Unicode and ANSI forms), the argument can be declared as the Unicode type, the ANSI type, or in the case of a C type argument, the SQL_C_TCHAR macro. For more information, see [Unicode Data](../../../odbc/reference/develop-app/unicode-data.md).  
  
 An application can be written as a Unicode application even if no Unicode drivers are available for it to work with. The Driver Manager will map Unicode functions and data types to ANSI. There are some restrictions to the Unicode to ANSI mappings that can be performed. The existence of a Unicode driver for the Unicode application to work with will result in better performance and will remove the restrictions inherent in the Unicode to ANSI mappings.
