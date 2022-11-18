---
description: "SQLTransact Function"
title: "SQLTransact Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLTransact"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLTransact"
helpviewer_keywords: 
  - "SQLTransact function [ODBC]"
ms.assetid: 496249e0-8eff-4c60-8358-5543bc3ead9c
author: David-Engel
ms.author: v-davidengel
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
