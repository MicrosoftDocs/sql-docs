---
title: "Working with Statements and Result Sets | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: cc917534-f5f8-4844-87c8-597c48b4e06d
author: MightyPen
ms.author: genemi
manager: craigg
---

# Working with Statements and Result Sets

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When you work with the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] and the Statement and ResultSet objects that it provides, there are several techniques that you can use to improve the performance and reliability of your applications.

## Use the Appropriate Statement Object

When you use one of the JDBC driver Statement objects, such as the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md), [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md), or the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) object, make sure that you are using the appropriate object for the job.

- If you do not have OUT parameters, you do not need to use the SQLServerCallableStatement object. Instead, use the SQLServerStatement or the SQLServerPreparedStatement object.

- If you do not intend to execute the statement more than once, or do not have IN or OUT parameters, you do not need to use the SQLServerCallableStatement or the SQLServerPreparedStatement object. Instead, use the SQLServerStatement object.

## Use the Appropriate Concurrency for ResultSet Objects

Do not ask for updatable concurrency when you create statements that produce result sets unless you actually intend to update the results. The default forward-only, read-only cursor model is fastest for reading small result sets.

## Limit the Size of Your Result Sets

Consider using the [setMaxRows](../../connect/jdbc/reference/setmaxrows-method-sqlserverstatement.md) method (or SET ROWCOUNT or SELECT TOP N SQL syntax) to limit the number of rows returned from potentially large result sets. If you must deal with large result sets, consider using an adaptive response buffering by setting the connection string property responseBuffering=adaptive, which is the default mode. This approach allows the application to process large result sets without requiring the server-side cursors and minimizes the application memory usage. For more information, see [Using Adaptive Buffering](../../connect/jdbc/using-adaptive-buffering.md).

## Use the Appropriate Fetch Size

For read-only server cursors, the tradeoff is round trips to the server versus the amount of memory used in the driver. For updatable server cursors, the fetch size also influences the sensitivity of the result set to changes and concurrency on the server. Updates to rows within the current fetch buffer are not visible until an explicit [refreshRow](../../connect/jdbc/reference/refreshrow-method-sqlserverresultset.md) method is issued or until the cursor leaves the fetch buffer. Large fetch buffers will have better performance (fewer server round trips) but are less sensitive to changes and reduce concurrency on the server if CONCUR_SS_SCROLL_LOCKS (1009) is used. For maximum sensitivity to changes, use a fetch size of 1. However, note that this will incur a round trip to the server for every row fetched.

## Use Streams for Large IN Parameters

Use streams or BLOBs and CLOBs that are incrementally materialized to handle updating large column values or sending large IN parameters. The JDBC driver "chunks" these to the server in multiple round trips, allowing you to set and update values larger than what will fit in memory.

## See Also

[Improving Performance and Reliability with the JDBC Driver](../../connect/jdbc/improving-performance-and-reliability-with-the-jdbc-driver.md)
