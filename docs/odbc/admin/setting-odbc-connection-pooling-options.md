---
description: "Setting ODBC Connection Pooling Options"
title: "Setting ODBC Connection Pooling Options | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "connection pooling [ODBC]"
  - "ODBC data source administrator [ODBC], connection pooling options"
  - "ODBC data source administrator [ODBC], performance monitoring"
ms.assetid: 037e2f78-f204-40f4-b4ab-d9cdf562012b
author: David-Engel
ms.author: v-davidengel
---
# Setting ODBC Connection Pooling Options
Connection pooling enables an application to use a connection from a pool of connections that do not need to be reestablished for each use. You can use the **Connection Pooling** tab of the **ODBC Data Source Administrator** dialog box to enable and disable performance monitoring. Double-click a driver name to set the connection time-out period.  
  
 At the driver level, connection pooling is enabled by the CPTimeout registry value. This selective per-driver enabling allows a system administrator to enable connection pooling for just the drivers that can support it. It is accomplished by setting the default value of CPTimeout during the driver's setup program. Double-click a driver name to set the connection time-out period.  
  
 For more information about connection pooling, see [ODBC Connection Pooling](../../odbc/reference/develop-app/driver-manager-connection-pooling.md).  
  
## Performance Monitoring  
 Performance monitoring tracks connection performance by recording a variety of statistics. These statistics can be customized by the developer to include items such as the following:  
  
|Counter|Definition|  
|-------------|----------------|  
|ODBC Hard Connection Counter per Second|The number of actual connections per second that are made to the server. The first time your environment carries a heavy load, this counter will go up very quickly. After a few seconds, it will drop to zero. This is the normal situation when connection pooling is working. When the connections to the server have been established, they will be used and placed in the pool for reuse.|  
|ODBC Hard Disconnect Counter per Second|The number of hard disconnects per second issued to the server. These are actual connections to the server that are being released by connection pooling. This value will increase from zero when you stop all clients on the system and the connections start to time out.|  
|ODBC Soft Connection Counter per Second|The number of connections satisfied by the pool per second-in other words, connections from that pool that were handed to users. This counter indicates whether pooling is working. Depending on the load on your server, it is not uncommon for this to show 40-60 soft connections per second.|  
|ODBC Soft Disconnection Counter per Second|The number of disconnects per second issued by the applications. When the application releases or disconnects, the connection is placed back in the pool.|  
|ODBC Current Active Connection Counter|The number of connections in the pool that are currently in use.|  
|ODBC Current Free Connection Counter|The current number of free connections available in the pool. These are live connections that are available for use.|  
|Pools Currently Active|The number of pools currently active. This counter was added in Windows 8, for drivers that manage connections in the connection pool. For more information, see [Driver-Aware Connection Pooling](../../odbc/reference/develop-app/driver-aware-connection-pooling.md).|  
|Pools Created|The number of pools active, including active and removed pools. This counter was added in Windows 8, for drivers that manage connections in the connection pool. For more information, see [Driver-Aware Connection Pooling](../../odbc/reference/develop-app/driver-aware-connection-pooling.md).|  
  
 You must specify your own monitoring parameters. Samples for performance monitoring have been included with this version of ODBC.
