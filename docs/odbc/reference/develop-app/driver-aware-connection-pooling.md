---
description: "Driver-Aware Connection Pooling"
title: "Driver-Aware Connection Pooling | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
ms.assetid: 53e7e3f7-edab-4d0b-8943-45442ba3ebc9
author: David-Engel
ms.author: v-davidengel
---
# Driver-Aware Connection Pooling
Driver aware connection pooling is a new feature of the Driver Manager in Windows 8. Driver aware connection pooling allows driver writers to customize the connection pooling behavior in their ODBC driver.  
  
> [!NOTE]  
>  Driver aware connection pooling is not supported with cursor library. An application will receive error message if it attempts to enable cursor library via SQLSetConnectAttr, when driver aware connection pooling is enabled.  
  
 Driver aware connection pooling addresses the following problems related to Driver Manager connection pooling:  
  
 **Pool Fragmentation** The Driver Manager will only return a connection from the pool if it is an exact match with the connection string of a new connection request.  One reason for the Driver Manager to require an exact match is that the Driver Manager does not understand every driver-specific connection string keyword and its value.  However, some connection string keyword values (such as the name of the database) may not require an exact match, since the driver can change the database in less than the time needed to open a new connection (the exact time difference depends on the data source). And, differences in some connection attributes (such as SQL_ATTR_CURRENT_CATALOG) can take more time to change than differences in other attributes (such as SQL_ATTR_LOGIN_TIMEOUT). This, too, can prevent the Driver Manager from using the lowest-cost, reusable connection from the pool. When a driver has to create many new connections, an application's performance can decrease and the data source scalability can decrease. Pool fragmentation can be reduced with driver-aware connection pooling because a driver can better estimate the cost of reusing a connection in the pool for a connection request.  
  
 **No consideration of application preference** Some data sources can efficiently open new connections (compared to resetting some attributes), so, an application may prefer to open a new connection instead of trying to reuse a slightly mismatched connection from the pool and reset some values (although this may be slower during the connection pool initialization phrase). But some applications may keep the server load smaller and open fewer connections, although there may be a bigger cost to fix the mismatches for correct behavior. Without driver-aware connection pooling, you cannot specify this kind of preference effectively, because the Driver Manager does not recognize all driver-specific connection attributes. Driver-aware connection pooling allows a driver to obtain the user preference (with a driver-specific attribute of SQLSetConnectAttr) so that it can better estimate the cost of reusing a connection from the pool based on a user's preference.  
  
 For more information about driver-aware connection pooling, see [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md).  
  
## Determining Driver Support  
 Driver-aware connection pooling is an optional feature that a driver may not support. To determine if a driver supports it, use the SQL_DRIVER_AWARE_POOLING_SUPPORTED *InfoType* of [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md).  
  
## How to Enable Driver-Aware Connection Pooling  
 An application can use a driver's connection-pooling awareness by setting the SQL_ATTR_CONNECTION_POOLING attribute to SQL_CP_DRIVER_AWARE with [SQLSetEnvAttr](../../../odbc/reference/syntax/sqlsetenvattr-function.md). If a driver does not support connection-pool awareness, Driver Manager connection pooling will be used (same as if SQL_CP_ONE_PER_HENV had been specified, instead of SQL_CP_DRIVER_AWARE). ODBC 2.x and 3.x applications can enable this feature.  
  
## See Also  
 [Developing an ODBC Driver](../../../odbc/reference/develop-driver/developing-an-odbc-driver.md)
