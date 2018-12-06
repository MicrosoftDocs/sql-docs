---
title: "ODBC Service Provider Interface Summary | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: ace6085b-355b-435b-8734-503fc3c12ec2
author: MightyPen
ms.author: genemi
manager: craigg
---
# ODBC Service Provider Interface Summary
The following table describes ODBC Service Provider interface functions. For more information about the syntax and semantics for each function, see [ODBC Service Provider Interface (SPI) Reference](../../../odbc/reference/syntax/odbc-service-provider-interface-spi-reference.md).  
  
|Function name|Purpose|  
|-------------------|-------------|  
|[SQLSetConnectAttrForDbcInfo](../../../odbc/reference/syntax/sqldatasourcetodriver-function.md)|Same as [SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md), but it sets the attribute on the connection information token instead of on the connection handle.|  
|[SQLSetDriverConnectInfo](../../../odbc/reference/syntax/sqldrivertodatasource-function.md)|Sets the connection string into the connection info token for an application's [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md) call.|  
|[SQLSetConnectInfo](../../../odbc/reference/syntax/sqldatasourcetodriver-function.md)|Sets the data source, user ID, and password into the connection info token for an application's [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md) call.|  
|[SQLGetPoolID](../../../odbc/reference/syntax/sqldatasourcetodriver-function.md)|Retrieves the pool ID.|  
|[SQLRateConnection](../../../odbc/reference/syntax/sqldatasourcetodriver-function.md)|Determines if a driver can reuse an existing connection in the connection pool.|  
|[SQLPoolConnect](../../../odbc/reference/syntax/sqldatasourcetodriver-function.md)|Create a new connection if no connection in the pool can be reused.|  
|[SQLCleanupConnectionPoolID](../../../odbc/reference/syntax/sqldatasourcetodriver-function.md)|Informs a driver that a pool ID was timed out.|
