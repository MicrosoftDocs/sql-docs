---
title: "Unicode Function Arguments | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Unicode [ODBC], functions"
  - "functions [ODBC], Unicode functions"
ms.assetid: eafe8c7e-f6d2-44d7-99ee-cf2148a30f4f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Unicode Function Arguments
The ODBC 3.5 (or higher) Driver Manager supports both ANSI and Unicode versions of all functions that accept pointers to character strings or SQLPOINTER in their arguments. The Unicode functions are implemented as functions (with a suffix of *W*), not as macros. The ANSI functions (which can be called with or without a suffix of *A*) are identical to the current ODBC API functions.  
  
## Remarks  
 Unicode functions that always return or take strings or length arguments are passed as count-of-characters. For functions that return length information for server data, the display size and precision are described in number of characters. When a length (transfer size of the data) could refer to string or nonstring data, the length is described in octet lengths. For example, **SQLGetInfoW** will still take the length as count-of-bytes, but **SQLExecDirectW** will use count-of-characters.  
  
 Count-of-characters refers to the number of bytes (octets) for ANSI functions and the number of WCHAR (16-bit words) for UNICODE functions. In particular, a double-byte character sequence (DBCS) or a multibyte character sequence (MBCS) can be composed of multiple bytes. A UTF-16 Unicode character sequence can be composed of multiple WCHARs.  
  
 The following is a list of the ODBC API functions that support both Unicode (W) and ANSI (A) versions:  
  
|||  
|-|-|  
|**SQLBrowseConnect**|**SQLGetDiagField**|  
|**SQLColAttribute**|**SQLGetDiagRec**|  
|**SQLColAttributes**|**SQLGetInfo**|  
|**SQLColumnPrivileges**|**SQLGetStmtAttr**|  
|**SQLColumns**|**SQLNativeSQL**|  
|**SQLConnect**|**SQLPrepare**|  
|**SQLDataSources**|**SQLPrimaryKeys**|  
|**SQLDescribeCol**|**SQLProcedureColumns**|  
|**SQLDriverConnect**|**SQLProcedures**|  
|**SQLDrivers**|**SQLSetConnectAttr**|  
|**SQLError**|**SQLSetConnectOption**|  
|**SQLExecDirect**|**SQLSetCursorName**|  
|**SQLForeignKeys**|**SQLSetDescField**|  
|**SQLGetConnectAttr**|**SQLSetStmtAttr**|  
|**SQLGetConnectOption**|**SQLSpecialColumns**|  
|**SQLGetCursorName**|**SQLStatistics**|  
|**SQLGetDescField**|**SQLTablePrivileges**|  
|**SQLGetDescRec**|**SQLTables**|  
  
 The following is a list of the ODBC Installer and ODBC Translator functions that support both Unicode (W) and ANSI (A) versions:  
  
|||  
|-|-|  
|**SQLConfigDataSource**|**SQLInstallDriverManager**|  
|**SQLCreateDataSource**|**SQLInstallerError**|  
|**SQLDataSourceToDriver**|**SQLInstallODBC**|  
|**SQLDriverToDataSource**|**SQLReadFileDSN**|  
|**SQLGetAvailableDrivers**|**SQLRemoveDSNFromINI**|  
|**SQLGetInstalledDrivers**|**SQLValidDSN**|  
|**SQLGetTranslator**|**SQLWriteDSNToINI**|  
|**SQLInstallDriver**||  
  
> [!NOTE]
>  Deprecated functions have Unicode-to-ANSI mapping support because the ODBC 3*.x* Driver Manager supports recompiling ODBC 2.*x* applications with the UNICODE **#define**.  
  
 This section contains the following topics.  
  
-   [Unicode Applications](../../../odbc/reference/develop-app/unicode-applications.md)  
  
-   [Unicode Drivers](../../../odbc/reference/develop-app/unicode-drivers.md)  
  
-   [Function Mapping in the Driver Manager](../../../odbc/reference/develop-app/function-mapping-in-the-driver-manager.md)
