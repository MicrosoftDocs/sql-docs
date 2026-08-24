---
title: "SQLGetConnectOption Function"
description: "SQLGetConnectOption Function"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
f1_keywords:
  - "SQLGetConnectOption"
helpviewer_keywords:
  - "SQLGetConnectOption function [ODBC]"
apilocation: "sqlsrv32.dll"
apiname: "SQLGetConnectOption"
apitype: "dllExport"
---
# SQLGetConnectOption Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: Deprecated  
  
 **Summary**  
 In ODBC *3.x*, the ODBC *2.x* function **SQLGetConnectOption** has been replaced by **SQLGetConnectAttr**. For more information, see [SQLGetConnectAttr](../../../odbc/reference/syntax/sqlgetconnectattr-function.md).  
  
> [!NOTE]
>  For more information about what the Driver Manager maps this function to when an ODBC *2.x* application is working with an ODBC *3.x* driver, see [Mapping Deprecated Functions](../../../odbc/reference/appendixes/mapping-deprecated-functions.md) in Appendix G: Driver Guidelines for Backward Compatibility.  
> 
> [!NOTE]
>  The attribute SQL_ASYNC_DBC_FUNCTION_ENABLE introduced in ODBC 3.8 is not supported by **SQLGetConnectOption**. Applications that use the asynchronous operation on a connection handle must use **SQLGetConnectAttr**.  
  
## Related content

- [ODBC API reference](odbc-api-reference.md)
- [ODBC Header Files](../install/odbc-header-files.md)
