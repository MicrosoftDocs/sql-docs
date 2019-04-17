---
title: "Closing Objects when Not In Use | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: ce8f9b35-c761-4b0c-9a46-985eef2c2e0b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Closing Objects when Not In Use
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  When you work with objects of [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], particularly the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) or one of the Statement objects such as [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md), [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) or [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md), you should explicitly close them by using their close methods when they are no longer needed. This improves performance by freeing up driver and server resources as soon as possible, instead of waiting for the Java Virtual Machine garbage collector to do it for you.  
  
 Closing objects is particularly crucial to maintaining good concurrency on the server when you are using scroll locks. Scroll locks in the last accessed fetch buffer are held until the result set is closed. Similarly, statement prepared handles are held until the statement is closed. If you are reusing a connection for multiple statements, closing the statements before you let them go out of scope will allow the server to clean up the prepared handles earlier.  
  
## See Also  
 [Improving Performance and Reliability with the JDBC Driver](../../connect/jdbc/improving-performance-and-reliability-with-the-jdbc-driver.md)  
  
  
