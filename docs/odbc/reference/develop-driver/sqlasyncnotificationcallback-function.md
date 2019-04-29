---
title: "SQLAsyncNotificationCallback Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: c56aedc9-f7f7-4641-b605-f0f98ed4400c
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLAsyncNotificationCallback Function
**Conformance**  
 Version Introduced: ODBC 3.8  
  
 Standards Compliance: None  
  
 **Summary**  
 **SQLAsyncNotificationCallback** allows a driver to call back to the Driver Manager when there is some progress for the current asynchronous operation after the driver returns SQL_STILL_EXECUTING. **SQLAsyncNotificationCallback** can only called by the driver.  
  
 Drivers do not call **SQLAsyncNotificationCallback** with function name **SQLAsyncNotificationCallback**. Instead, the Driver Manager passes a function pointer to a driver as the value for the SQL_ATTR_ASYNC_DBC_NOTIFICATION_CALLBACK or SQL_ATTR_ASYNC_STMT_NOTIFICATION_CALLBACK attribute of the corresponding connection handle or statement handle, respectively. Different handles may be assigned different function pointer values. The type of the function pointer is defined as SQL_ASYNC_NOTIFICATION_CALLBACK.  
  
 **SQLAsyncNotificationCallback** is thread-safe. A driver can choose to use multiple threads calling **SQLAsyncNotificationCallback** on different handles simultaneously.  
  
## Syntax  
  
```  
typedef SQLRETURN (SQL_API *SQL_ASYNC_NOTIFICATION_CALLBACK)(  
   SQLPOINTER pContex,   
   BOOL fLast);  
```  
  
## Arguments  
 *pContex*  
 Pointer to a data structure defined by the Driver Manager. The value is passed to the driver via SQLSetConnectAttr(SQL_ATTR_ASYNC_DBC_NOTIFICATION_CONTEXT) or SQLSetStmtAttr(SQL_ATTR_ASYNC_STMT_NOTIFICATION_CONTEXT).  The driver does not have access to the value.  
  
 *fLast*  
 Used by a driver to indicates that this callback function invocation is the last one for the current asynchronous operation. The driver will return a return code other than SQL_STILL_EXECUTING when the Driver Manager calls the function again. The Driver Manager may use this information, for example, to inform the application in advance that the asynchronous operation will complete.  
  
 If *Handle* is not a valid handle of the type specified by *HandleType*, **SQLCancelHandle** returns SQL_INVALID_HANDLE.  
  
## Returns  
 SQL_SUCCESS or SQL_ERROR.  
  
## Diagnostics  
 **SQLAsyncNotificationCallback** can return SQL_ERROR for the following two situations (these indicate an implementation problem in the driver or Driver Manager.  
  
|Error|Description|  
|-----------|-----------------|  
|Connection or statement did not request notification.||  
|Invalid *handle*|The driver passed in an invalid handle, which failed the internal Driver Manager validation tests.|  
  
## See Also  
 [Asynchronous Execution (Polling Method)](../../../odbc/reference/develop-app/asynchronous-execution-polling-method.md)
