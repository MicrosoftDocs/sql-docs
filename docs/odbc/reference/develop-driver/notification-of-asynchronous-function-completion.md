---
title: "Notification of Asynchronous Function Completion | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 336565da-4203-4745-bce2-4f011c08e357
author: MightyPen
ms.author: genemi
manager: craigg
---
# Notification of Asynchronous Function Completion
In the Windows 8 SDK, ODBC added a mechanism to notify applications when an asynchronous operation completes, which we will refer to as "notification on completion". (See [Asynchronous Execution (Notification Method)](../../../odbc/reference/develop-app/asynchronous-execution-notification-method.md) for more information.) This topic discusses some of the issues for driver developers.  
  
## The Interface between the Driver Manager and Driver  
 The Driver Manager internally provides a callback function [SQLAsyncNotificationCallback Function](../../../odbc/reference/develop-driver/sqlasyncnotificationcallback-function.md). **SQLAsyncNotificationCallback** can only be called by the driver -- an application cannot directly call it. The driver calls **SQLAsyncNotificationCallback** whenever new data received from the server after last returning SQL_STILL_EXECUTING.  
  
 The Driver Manager provides a callback mechanism so a driver can notify the Driver Manager when some progress has been made in executing an asynchronous operation after the corresponding function returns SQL_STILL_EXECUTING  
  
 The Driver Manager sets the SQL_ATTR_ASYNC_DBC_NOTIFICATION_CALLBACK attribute on a driver connection handle with a non-NULL function pointer, which is of type SQL_ASYNC_NOTIFICATION_CALLBACK, for the driver to work in notification mode for any asynchronous operations on that handle. Similarly, the Driver Manager sets the SQL_ATTR_ASYNC_STMT_NOTIFICATION_CALLBACK attribute on a driver statement handle with a non-NULL function pointer, which is also of type SQL_ASYNC_NOTIFICATION_CALLBACK, for the driver to work in notification mode for any asynchronous operations on that handle.  
  
 If an asynchronous operation is performed on a driver handle, the asynchronous driver functions should work in a non-blocking style. If the operation cannot complete immediately, the driver function should return SQL_STILL_EXECUTING. This requirement is true for both polling mode and notification mode.  
  
 If a handle is in notification asynchronous mode, the driver must call the notification callback function, whose address is the value for the SQL_ATTR_ASYNC_DBC_NOTIFICATION_CALLBACK or SQL_ATTR_ASYNC_STMT_NOTIFICATION_CALLBACK attribute, once after returning SQL_STILL_EXECUTING. In other words, one returning SQL_STILL_EXECUTING must be paired with one invocation of the notification callback function. The driver should use the current value of the SQL_ATTR_ASYNC_DBC_NOTIFICATION_CONTEXT or SQL_ATTR_ASYNC_STMT_NOTIFICATION_CONTEXT handle attribute as the value for the call back function parameter *pContext*.  
  
 The driver must not call back in the thread that calls the driver function; there is no reason to notify progress before the function returns. The driver should use its own thread to callback. The Driver Manager will not use the driver's callback thread for executing extensive processing logic.  
  
 The Driver Manager will call the original function again after the driver calls back. The Driver Manager may use a thread that is neither an application thread nor a driver thread. If the driver uses some information associated with the thread (for example, security token or user identifier), the driver should save the required information in the initial asynchronous call and use the saved value before the whole asynchronous operation completes. Usually, only **SQLDriverConnect**, **SQLConnect**, or **SQLBrowseConnect** need to use that kind of information.  
  
## See Also  
 [Developing an ODBC Driver](../../../odbc/reference/develop-driver/developing-an-odbc-driver.md)
