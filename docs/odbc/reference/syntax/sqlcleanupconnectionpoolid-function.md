---
title: "SQLCleanupConnectionPoolID Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLCleanupConnectionPoolID function [ODBC]"
ms.assetid: 1fc61908-e003-4587-b91a-32f40569fb99
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLCleanupConnectionPoolID Function
**Conformance**  
 Version Introduced: ODBC 3.81 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLCleanupConnectionPoolID** informs a driver that a pool ID was timed out. A pool ID can timeout whenever all connections in a pool associated with that pool ID were timed out. See [Pooling in the Microsoft Data Access Components](https://msdn.microsoft.com/library/ms810829.aspx) for more information about connection timeout.  
  
## Syntax  
  
```  
SQLRETURN  SQLCleanupConnectionPoolID (  
                SQLHENV    EnvironmentHandle  
                SQLPOOLID  PoolID );  
```  
  
## Arguments  
 *EnvironmentHandle*  
 [Input] The environment handle of the pool.  
  
 *PoolID*  
 [Input] The pool associated to the pool ID that was timed out.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 The Driver Manager will not process diagnostic information returned from **SQLCleanupConnectionPoolID**.  
  
 An application cannot receive the error message returned by the driver.  
  
## Remarks  
 **SQLCleanupConnectionPoolID** can be called at any time, but the Driver Manager guarantees that no other thread is simultaneously calling **SQLGetPoolID** and no other thread is simultaneously calling **SQLRateConnection** and **SQLPoolConnect** with a connection info token assigned with that pool ID. Therefore, the driver must make sure this function is thread safe.  
  
 A driver can clean up the resources associated with the pool ID.  
  
 Applications should not call this function directly. An ODBC driver that supports driver-aware connection pooling must implement this function.  
  
 Include sqlspi.h for ODBC driver development.  
  
## See Also  
 [Developing an ODBC Driver](../../../odbc/reference/develop-driver/developing-an-odbc-driver.md)   
 [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md)   
 [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md)
