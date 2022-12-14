---
description: "Upgrading a 3.5 Driver to a 3.8 Driver"
title: "Upgrading a 3.5 Driver to a 3.8 Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
ms.assetid: ffba36ac-d22e-40b9-911a-973fa9e10bd3
author: David-Engel
ms.author: v-davidengel
---
# Upgrading a 3.5 Driver to a 3.8 Driver
This topic provides guidelines and considerations for upgrading an ODBC 3.5 driver to an ODBC 3.8 driver.  
  
##### Version Numbers  
 The following guidelines relate to version numbers:  
  
-   A driver should support SQL_OV_ODBC3_80 for SQL_ATTR_ODBC_VERSION, returning SQL_ERROR for values other than SQL_OV_ODBC2, SQL_OV_ODBC3, and SQL_OV_ODBC3_80. Future versions of the Driver Manager will assume that a driver supports an ODBC compliance level if the driver returns SQL_SUCCESS from [SQLSetEnvAttr Function](../../../odbc/reference/syntax/sqlsetenvattr-function.md).  
  
-   A version 3.8 driver should return 03.80 from **SQLGetInfo** when SQL_DRIVER_ODBC_VER is passed to *InfoType*. However, older Driver Managers, which were included in older versions of Microsoft Windows, will treat the driver as a version 3.5 driver, and issue a warning.  
  
     In Windows 7, the Driver Manager version is 03.80. In Windows 8, the Driver Manager version is 03.81 via the SQLGetInfo SQL_DM_VER (*InfoType* parameter). SQL_ODBC_VER reports the version as 03.80 in both Windows 7 and Windows 8.  
  
##### Driver-Specific C Data Types  
 A driver can have customized C data types when it works with a version 3.8 ODBC application. (For more information, see [C Data Types in ODBC](../../../odbc/reference/develop-app/c-data-types-in-odbc.md).) However, there is no requirement for a 3.8 driver to implement any driver-specific C types. But the driver should still perform the range check of C types; the Driver Manager will not do that for 3.8 drivers. To facilitate driver development, the value of the driver specific, C data type can be defined in the following format:  
  
```  
SQL_DRIVER_C_TYPE_BASE+0, SQL_DRIVER_C_TYPE_BASE+1  
```  
  
##### Driver-specific Data Types, Descriptor Types, Information Types, Diagnostic Types, and Attributes  
 When developing a new driver, you should use the driver-specific range for data types, descriptor types, information types, diagnostic types, and attributes. Driver-specific ranges and their base type values are discussed in [Driver-Specific Data Types, Descriptor Types, Information Types, Diagnostic Types, and Attributes](../../../odbc/reference/develop-app/driver-specific-data-types-descriptor-information-diagnostic.md).  
  
##### Connection Pooling  
 For better management of connection pooling, ODBC 3.8 introduces the SQL_ATTR_RESET_CONNECTION connection attribute in **SQLSetConnectAttr**. SQL_RESET_CONNECTION_YES is the only valid value for this attribute. SQL_ATTR_RESET_CONNECTION will be set just before the Driver Manager puts a connection in the connection pool, allowing the driver to reset the other connection attributes to their default values.  
  
 To avoid unnecessary communication with the server, a driver can defer the connection attribute reset until the next communication with the remote server, after the connection is reused from the pool.  
  
 Note that SQL_ATTR_RESET_CONNECTION is only used in communication between the Driver Manager and a driver. An application cannot set this attribute directly. All version 3.8 drivers should implement this connection attribute.  
  
##### Streamed Output Parameters  
 ODBC version 3.8 introduces streamed output parameters, a more scalable way to retrieve output parameters. (For more information, see [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md).) To support this feature, a driver should set SQL_GD_OUTPUT_PARAMS in the return value when SQL_GETDATA_EXTENSIONS is the *InfoType* in a **SQLGetInfo** call. Support for an SQL type with streamed output parameters must be implemented in the driver. The Driver Manager will not generate an error for an invalid SQL type. The SQL types that support streamed output parameters is defined in the driver.  
  
 A driver should return SQL_ERROR if the application used **SQLGetData** to retrieve a parameter that is not the same as the parameter returned by **SQLParamData**.  
  
##### Asynchronous Execution for Connection Operations (Polling Method)  
 A driver can enable asynchronous support for various connection operations.  
  
 Beginning in Windows 7, ODBC supports the polling method (for more information, see [Asynchronous Execution (Polling Method)](../../../odbc/reference/develop-app/asynchronous-execution-polling-method.md). There is no requirement for a version 3.8 ODBC driver to implement asynchronous operations on connection handles. Even if a driver does not implement asynchronous operations on connection handles, the driver should still implement the SQL_ASYNC_DBC_FUNCTIONS *InfoType* and return **SQL_ASYNC_DBC_NOT_CAPABLE**.  
  
 When asynchronous connection operations are enabled, the running time of a connection operation is the total time of all repeated calls. If the last repeated call occurs after the total time has exceeded the value set by the SQL_ATTR_CONNECTION_TIMEOUT connection attribute, and the operation has not finished, the driver returns SQL_ERROR and logs a diagnostic record with SQLState HYT01 and the message "Connection timeout expired". There is no timeout if the operation finished.  
  
##### SQLCancelHandle Function  
 ODBC 3.8 supports [SQLCancelHandle Function](../../../odbc/reference/syntax/sqlcancelhandle-function.md), which is used to cancel both connection and statement operations. A driver that supports **SQLCancelHandle** must export the function. A driver should not cancel any synchronous or asynchronous connection function that is in progress if the application calls **SQLCancel** or **SQLCancelHandle** on a statement handle. Similarly, a driver should not cancel any synchronous or asynchronous statement function that is in progress if an application calls **SQLCancelHandle** on the connection handle. Also, a driver should not cancel the browsing operation (**SQLBrowseConnect** returns SQL_NEED_DATA) if the application calls **SQLCancelHandle** on the connection handle. In these cases, a driver should return HY010, "function sequence error".  
  
 It is not necessary to support both **SQLCancelHandle** and asynchronous connection operations at the same time. A driver can support asynchronous connection operations but not **SQLCancelHandle**, or vice versa.  
  
##### Suspended Connections  
 The ODBC 3.8 Driver Manager can put a connection into suspended state. An application will call **SQLDisconnect** to release resources associated with the connection. In this case, a driver should try to release as many resources as possible without checking the state of the connection. For more information about the suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).  
  
##### Driver-Aware Connection Pooling  
 ODBC in Windows 8 allows drivers to customize connection pool behavior. For more information, see [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md).  
  
##### Asynchronous Execution (Notification Method)  
 ODBC 3.8 supports the notification method for asynchronous operations, available beginning on Windows 8. For more information, see [Asynchronous Execution (Notification Method)](../../../odbc/reference/develop-app/asynchronous-execution-notification-method.md).  
  
## See Also  
 [Developing an ODBC Driver](../../../odbc/reference/develop-driver/developing-an-odbc-driver.md)   
 [Microsoft-Supplied ODBC Drivers](../../../odbc/microsoft/microsoft-supplied-odbc-drivers.md)   
 [What's New in ODBC 3.8](../../../odbc/reference/what-s-new-in-odbc-3-8.md)
