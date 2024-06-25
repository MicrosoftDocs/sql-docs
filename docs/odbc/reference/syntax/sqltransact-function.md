---
title: "SQLTransact Function"
description: "SQLTransact Function"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
f1_keywords:
  - "SQLTransact"
helpviewer_keywords:
  - "SQLTransact function [ODBC]"
apilocation: "sqlsrv32.dll"
apiname: "SQLTransact"
apitype: "dllExport"
---
# SQLTransact Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: Deprecated  
  
 **Summary**  
 In ODBC *3.x*, the ODBC *2.x* function **SQLTransact** has been replaced by **SQLEndTran**. For more information, see [SQLEndTran](../../../odbc/reference/syntax/sqlendtran-function.md).  
  
> [!NOTE]  
>  The attribute SQL_ASYNC_DBC_FUNCTION_ENABLE, which was introduced in ODBC 3.8, is not supported by **SQLTransact**. Applications using an asynchronous operation on a connection handle must use **SQLEndTran**.  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
