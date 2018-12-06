---
title: "SQLCompleteAsync Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
f1_keywords: 
  - "SQLCompleteAsync"
helpviewer_keywords: 
  - "SQLCompleteAsync function [ODBC]"
ms.assetid: 1b97c46a-d2e5-4540-8239-9d975e5321c6
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLCompleteAsync Function
**Conformance**  
 Version Introduced: ODBC 3.8  
  
 Standards Compliance: None  
  
 **Summary**  
 **SQLCompleteAsync** can be used to determine when an asynchronous function is complete using either notification- or polling-based processing. For more information about asynchronous operations, see [Asynchronous Execution](../../../odbc/reference/develop-app/asynchronous-execution.md).  
  
 **SQLCompleteAsync** is only implemented in the ODBC Driver Manager.  
  
 In notification based asynchronous processing mode, **SQLCompleteAsync** must be called after the Driver Manager raises the event object used for notification. **SQLCompleteAsync** completes the asynchronous processing and the asynchronous function will generate a return code.  
  
 In polling based asynchronous processing mode, **SQLCompleteAsync** is an alternative to calling the original asynchronous function, without needing to specify the arguments in the original asynchronous function call. **SQLCompleteAsync** can be used regardless whether the ODBC Cursor Library is enabled.  
  
## Syntax  
  
```vb  
  
SQLRETURN SQLCompleteAsync(  
      SQLSMALLINT HandleType,  
      SQLHANDLE   Handle,  
      RETCODE *   AsyncRetCodePtr);  
```  
  
## Arguments  
 *HandleType*  
 [Input] The type of the handle on which to complete asynchronous processing. Valid values are SQL_HANDLE_DBC or SQL_HANDLE_STMT.  
  
 *Handle*  
 [Input] The handle on which to complete asynchronous processing. If *Handle* is not a valid handle of the type specified by *HandleType*, **SQLCompleteAsync** returns SQL_INVALID_HANDLE.  
  
 If *Handle* is not a valid handle of the type specified by *HandleType*, **SQLCompleteAsync** returns SQL_INVALID_HANDLE.  
  
 *AsyncRetCodePtr*  
 [Output] Pointer to a buffer that will contain the return code of the asynchronous API. If *AsyncRetCodePtr* is NULL, **SQLCompleteAsync** returns SQL_ERROR.  
  
## Returns  
 SQL_SUCCESS, SQL_ERROR, SQL_NO_DATA, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 If **SQLCompleteAsync** returns SQL_SUCCESS, an application should get the return code of the asynchronous function from the buffer pointed to by *AsyncRetCodePtr*. The associated SQLSTATE, if any, can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a statement handle or a *HandleType* of SQL_HANDLE_DBC and a connection handle. Those diagnostic records are associated with the asynchronous function, not this **SQLCompleteAsync** function.  
  
 **SQLCompleteAsync** returns a code other than SQL_SUCCESS to indicate that **SQLCompleteAsync** is not called correctly. **SQLCompleteAsync** will not post any diagnostic record in this case. Possible return codes are:  
  
-   SQL_INVALID_HANDLE: The handle indicated by *HandleType* and *Handle* is not a valid handle.  
  
-   SQL_ERROR: *AsyncRetCodePtr* is NULL or asynchronous processing is not enabled on the handle.  
  
-   SQL_NO_DATA: In notification mode, an asynchronous operation is not in progress or the Driver Manager has not notified the application. In polling mode, an asynchronous operation is not in progress.  
  
## Comments  
 In polling based asynchronous processing mode, *AsyncRetCodePtr* might be SQL_STILL_EXECUTING when **SQLCompleteAsync** returns SQL_SUCCESS. Application should keep polling until *AsyncRetCodePtr* is not SQL_STILL_EXECUTING. In notification based asynchronous processing mode, *AsyncRetCodePtr* will never be SQL_STILL_EXECUTING.  
  
## See Also  
 [Asynchronous Execution (Polling Method)](../../../odbc/reference/develop-app/asynchronous-execution-polling-method.md)
