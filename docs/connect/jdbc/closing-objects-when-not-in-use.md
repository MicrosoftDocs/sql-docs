---
title: Closing objects when not in use
description: In JDBC programming, it's important to close objects when the aren't in use. Closing improves performance and frees up client and server resources quickly.
author: David-Engel
ms.author: v-davidengel
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Closing objects when not in use

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When you work with closable objects of [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], you should explicitly close them by using their close methods when they're no longer needed. This pattern particularly applies to [SQLServerResultSet](reference/sqlserverresultset-class.md) and the Statement objects like [SQLServerStatement](reference/sqlserverstatement-class.md), [SQLServerPreparedStatement](reference/sqlserverpreparedstatement-class.md), and [SQLServerCallableStatement](reference/sqlservercallablestatement-class.md). Closing improves performance by freeing up driver and server resources quickly, instead of waiting for the Java Virtual Machine garbage collector to do it for you.

Closing objects is crucial to maintaining good concurrency on the server when you're using scroll locks. Scroll locks in the last accessed fetch buffer are held until the result set is closed. Similarly, statement prepared handles are held until the statement is closed. If you're reusing a connection for multiple statements, closing statements before they go out of scope allows the server to clean up the prepared handles earlier.

## See also

[Improving performance and reliability with the JDBC driver](improving-performance-and-reliability-with-the-jdbc-driver.md)
