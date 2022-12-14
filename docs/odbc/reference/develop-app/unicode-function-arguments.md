---
description: "Unicode Function Arguments"
title: "Unicode Function Arguments | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Unicode [ODBC], functions"
  - "functions [ODBC], Unicode functions"
ms.assetid: eafe8c7e-f6d2-44d7-99ee-cf2148a30f4f
author: David-Engel
ms.author: v-davidengel
---
# Unicode Function Arguments
The ODBC 3.5 (or higher) Driver Manager supports both ANSI and Unicode versions of all functions that accept pointers to character strings or SQLPOINTER in their arguments. The Unicode functions are implemented as functions (with a suffix of *W*), not as macros. The ANSI functions (which can be called with or without a suffix of *A*) are identical to the current ODBC API functions.  
  
## Remarks  
 For Unicode functions that always return or take strings or length arguments, the arguments are passed as count-of-characters. For functions that return length information for server data, the display size and precision are described in number of characters. When a length (transfer size of the data) could refer to string or nonstring data, the length is described in octet lengths. For example, **SQLGetInfoW** will still take the length as count-of-bytes, but **SQLExecDirectW** will use count-of-characters.  
  
 Count-of-characters refers to the number of bytes (octets) for ANSI functions and the number of WCHAR (16-bit words) for UNICODE functions. In particular, a double-byte character sequence (DBCS) or a multibyte character sequence (MBCS) can be composed of multiple bytes. A UTF-16 Unicode character sequence can be composed of multiple WCHARs.  
  
 The following is a list of the ODBC API functions that support both Unicode (W) and ANSI (A) versions:  
  
:::row:::
   :::column span="":::
      **SQLBrowseConnect**<br>      **SQLColAttribute**<br>      **SQLColAttributes**<br>      **SQLColumnPrivileges**<br>      **SQLColumns** <br>      **SQLConnect** <br>      **SQLDataSources**<br>      **SQLDescribeCol**  <br>      **SQLDriverConnect** <br>      **SQLDrivers** <br>      **SQLError**  <br>      **SQLExecDirect**<br>      **SQLForeignKeys**<br>      **SQLGetConnectAttr** <br>      **SQLGetConnectOption** <br>      **SQLGetCursorName**<br>      **SQLGetDescField** <br>      **SQLGetDescRec** <br>      **SQLGetDiagField**
   :::column-end:::
   :::column span="":::
      **SQLGetDiagRec**        <br>      **SQLGetInfo**        <br>      **SQLGetStmtAttr**<br>      **SQLGetTypeInfo**<br>      **SQLNativeSql**<br>      **SQLPrepare**<br>      **SQLPrimaryKeys**<br>      **SQLProcedureColumns**<br>      **SQLProcedures**<br>      **SQLSetConnectAttr**<br>      **SQLSetConnectOption**<br>      **SQLSetCursorName**<br>      **SQLSetDescField**<br>      **SQLSetStmtAttr**<br>      **SQLSpecialColumns**<br>      **SQLStatistics**<br>      **SQLTablePrivileges**<br>      **SQLTables**
   :::column-end:::
:::row-end:::
  
 The following is a list of the ODBC Installer and ODBC Translator functions that support both Unicode (W) and ANSI (A) versions:  
  
:::row:::
   :::column span="":::
      **SQLConfigDataSource**<br>      **SQLCreateDataSource**<br>      **SQLDataSourceToDriver**<br>      **SQLDriverToDataSource**<br>      **SQLGetAvailableDrivers**<br>      **SQLGetInstalledDrivers**<br>      **SQLGetTranslator**<br>      **SQLInstallDriver**
   :::column-end:::
   :::column span="":::
      **SQLInstallDriverManager**  <br>      **SQLInstallerError**  <br>      **SQLInstallODBC**  <br>      **SQLReadFileDSN**  <br>      **SQLRemoveDSNFromINI**  <br>      **SQLValidDSN**  <br>      **SQLWriteDSNToINI**
   :::column-end:::
:::row-end:::
  
> [!NOTE]
>  Deprecated functions have Unicode-to-ANSI mapping support because the ODBC *3.x* Driver Manager supports recompiling ODBC *2.x* applications with the UNICODE **#define**.  
  
 This section contains the following topics.  
  
-   [Unicode Applications](../../../odbc/reference/develop-app/unicode-applications.md)  
  
-   [Unicode Drivers](../../../odbc/reference/develop-app/unicode-drivers.md)  
  
-   [Function Mapping in the Driver Manager](../../../odbc/reference/develop-app/function-mapping-in-the-driver-manager.md)
