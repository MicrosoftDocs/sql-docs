---
description: "SQLSetConnectOption Function"
title: "SQLSetConnectOption Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLSetConnectOption"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLSetConnectOption"
helpviewer_keywords: 
  - "SQLSetConnectOption function [ODBC]"
ms.assetid: 8cd2c2a2-25c8-4aff-951c-b593bbfc90ad
author: David-Engel
ms.author: v-davidengel
---
# SQLSetConnectOption Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: Deprecated  
  
 **Summary**  
 In ODBC 3*.x*, the ODBC 2.0 function **SQLSetConnectOption** has been replaced by **SQLSetConnectAttr**. For more information, see [SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md).  
  
> [!NOTE]
>  For more information about what the Driver Manager maps this function to when an ODBC 2*.x* application is working with an ODBC 3*.x* driver, see [Mapping Deprecated Functions](../../../odbc/reference/appendixes/mapping-deprecated-functions.md)".  
  
## Remarks  
 See [ODBC 64-Bit Information](../../../odbc/reference/odbc-64-bit-information.md), if your application will run on a 64-bit operating system.  
  
> [!NOTE]  
>  The attribute SQL_ASYNC_DBC_FUNCTION_ENABLE introduced in ODBC 3.8 is not supported by **SQLSetConnectOption**. Applications that use the asynchronous operation on connection handle must use **SQLSetConnectAttr**.  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
