---
title: "Unicode Data | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Unicode [ODBC], data"
  - "data types [ODBC], Unicode"
  - "C data types [ODBC], Unicode"
  - "SQL data types [ODBC], Unicode"
ms.assetid: abc28718-e6d9-49fb-97ff-402d50c3c375
author: MightyPen
ms.author: genemi
manager: craigg
---
# Unicode Data
SQL Unicode data types are provided to describe data that resides in Unicode natively on the DBMS. A C Unicode data type is provided to allow an application to bind data to a Unicode buffer. The Driver Manager can convert data from a Unicode C type (SQL_C_WCHAR) to make it function with an ANSI driver.  
  
 An ODBC 3.0 or 2.*x* application will always bind to the ANSI data types. For optimum performance, an ODBC 3.5 (or higher) application should bind to the ANSI data C type if the SQL column type is ANSI, and should bind to the Unicode C data type if the SQL column type is Unicode.  
  
 The SQL Unicode type indicators are SQL_WCHAR, SQL_WVARCHAR, and SQL_WLONGVARCHAR. SQL_WCHAR data has a fixed string length, while SQL_WVARCHAR has a variable length with a declared maximum and SQL_WLONGVARCHAR has a variable length with a maximum that depends on the data source.  
  
 The C Unicode type indicator is SQL_C_WCHAR. This is the default for each of the SQL Unicode type indicators. All of the SQL types can be converted to SQL_C_WCHAR, and SQL_C_WCHAR can be converted to all of the SQL types. An application can retrieve data in one of three ways:  
  
-   Retrieve the data as SQL_C_CHAR.  
  
-   Retrieve the data as SQL_C_WCHAR.  
  
-   Declare the data as SQL_C_TCHAR. This is a macro that inserts SQL_C_WCHAR if the application is compiled as a Unicode application or inserts SQL_C_CHAR if it is compiled as an ANSI application.  
  
 SQL_C_TCHAR is declared in a function as follows:  
  
```  
SQLBindParameter(StatementHandle, 1, SQL_PARAM_INPUT, SQL_C_TCHAR, SQL_WCHAR, NameLen, 0, Name, 0, &Name)  
```  
  
 When the application is compiled as a Unicode application, the *ValueType* argument would be changed from SQL_C_TCHAR to SQL_C_WCHAR. When the application is compiled as an ANSI application, the *ValueType* argument would be changed to SQL_C_CHAR.  
  
 Unicode drivers must still support ANSI data types, including SQL_CHAR. If an application working with a Unicode driver binds to SQL_CHAR, the Driver Manager will not map the SQL_CHAR data to SQL_WCHAR. The Unicode driver must accept the SQL_CHAR data.  
  
 The Driver Manager stores driver and DSN names in Unicode and maps them to ANSI as needed. If a Unicode character cannot be mapped to an ANSI character (as can occur if characters from a code page that is not the native code page of the computer are used in driver and DSN names), the characters that could not be converted are represented by a default character supplied by the system.
