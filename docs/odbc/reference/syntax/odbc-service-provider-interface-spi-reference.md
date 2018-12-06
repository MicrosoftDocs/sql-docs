---
title: "ODBC Service Provider Interface (SPI) Reference | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: cdeffb4a-f344-4abe-97f3-be2ede1c8e59
author: MightyPen
ms.author: genemi
manager: craigg
---
# ODBC Service Provider Interface (SPI) Reference
Traditionally, ODBC defined an application programming interface (API). The functions in the API can be called by applications and they should be implemented inside both the Driver Manager and the driver.  
  
 With the addition of the driver-aware connection pooling feature, ODBC introduces the service provider interface (SPI). The functions in the SPI are used for communication between the Driver Manager and driver. SPI functions are implemented by the driver; the Driver Manager does not expose SPI functions to applications. Applications should not call these functions directly.  
  
 Include sqlspi.h for ODBC driver development.  
  
 This section contains the following topics  
  
-   [SQLCleanupConnectionPoolID](../../../odbc/reference/syntax/sqlcleanupconnectionpoolid-function.md)  
  
-   [SQLGetPoolID](../../../odbc/reference/syntax/sqlgetpoolid-function.md)  
  
-   [SQLPoolConnect](../../../odbc/reference/syntax/sqlpoolconnect-function.md)  
  
-   [SQLRateConnection](../../../odbc/reference/syntax/sqlrateconnection-function.md)  
  
-   [SQLSetConnectAttrForDbcInfo](../../../odbc/reference/syntax/sqlsetconnectattrfordbcinfo-function.md)  
  
-   [SQLSetConnectInfo](../../../odbc/reference/syntax/sqlsetconnectinfo-function.md)  
  
-   [SQLSetDriverConnectInfo](../../../odbc/reference/syntax/installation-and-configuration-wwi-oltp.md)  
  
## See Also  
 [Developing an ODBC Driver](../../../odbc/reference/develop-driver/developing-an-odbc-driver.md)   
 [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md)   
 [Driver Manager Connection Pooling](../../../odbc/reference/develop-app/driver-manager-connection-pooling.md)
