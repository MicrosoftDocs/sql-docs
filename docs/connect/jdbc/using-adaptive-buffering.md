---
title: "Using adaptive buffering"
description: "Learn how using adaptive buffering eliminates the overhead of server cursors when retrieving large-value data using the Microsoft JDBC Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using adaptive buffering

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Adaptive buffering is designed to retrieve any kind of large-value data without the overhead of server cursors. Applications can use the adaptive buffering feature with all versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are supported by the driver.

Normally, when the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] executes a query, the driver retrieves all of the results from the server into application memory. Although this approach minimizes resource consumption on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it can throw an OutOfMemoryError in the JDBC application for the queries that produce very large results.

In order to allow applications to handle very large results, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides adaptive buffering. With adaptive buffering, the driver retrieves statement execution results from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as the application needs them, rather than all at once. The driver also discards the results as soon as the application can no longer access them. The following are some examples where the adaptive buffering can be useful:

- **The query produces a very large result set:** The application can execute a SELECT statement that produces more rows than the application can store in memory. In previous releases, the application had to use a server cursor to avoid an OutOfMemoryError. Adaptive buffering provides the ability to do a forward-only read-only pass of an arbitrarily large result set without requiring a server cursor.

- **The query produces very large** [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) **columns or** [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) **OUT parameter values:** The application can retrieve a single value (column or OUT parameter) that is too large to fit entirely in application memory. Adaptive buffering allows the client application to retrieve such a value as a stream, by using the getAsciiStream, the getBinaryStream, or the getCharacterStream methods. The application retrieves the value from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as it reads from the stream.

> [!NOTE]  
> With adaptive buffering, the JDBC driver buffers only the amount of data that it has to. The driver does not provide any public method to control or limit the size of the buffer.

## Setting adaptive buffering

Starting with the JDBC driver version 2.0, the default behavior of the driver is "**adaptive**". In other words, in order to get the adaptive buffering behavior, your application does not have to request the adaptive behavior explicitly. In the version 1.2 release, however, the buffering mode was "**full**" by default and the application had to request the adaptive buffering mode explicitly.

There are three ways that an application can request that statement execution should use adaptive buffering:

- The application can set the connection property **responseBuffering** to "adaptive". For more information on setting the connection properties, see [Setting the connection properties](../../connect/jdbc/setting-the-connection-properties.md).

- The application can use the [setResponseBuffering](../../connect/jdbc/reference/setresponsebuffering-method-sqlserverdatasource.md) method of the [SQLServerDataSource](../../connect/jdbc/reference/sqlserverdatasource-class.md) object to set the response buffering mode for all connections created through that [SQLServerDataSource](../../connect/jdbc/reference/sqlserverdatasource-class.md) object.

- The application can use the [setResponseBuffering](../../connect/jdbc/reference/setresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) class to set the response buffering mode for a particular statement object.

When using the JDBC Driver version 1.2, applications needed to cast the statement object to a [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) class to use the [setResponseBuffering](../../connect/jdbc/reference/setresponsebuffering-method-sqlserverstatement.md) method. The code examples in the [Reading large data sample](../../connect/jdbc/reading-large-data-sample.md) and [Reading large data with stored procedures sample](../../connect/jdbc/reading-large-data-with-stored-procedures-sample.md) demonstrate this old usage.

However, with the JDBC driver version 2.0, applications can use the [isWrapperFor](../../connect/jdbc/reference/iswrapperfor-method-sqlserverstatement.md) method and the [unwrap](../../connect/jdbc/reference/unwrap-method-sqlserverstatement.md) method to access the vendor-specific functionality without any assumption about the implementation class hierarchy. For example code, see the [Updating large data sample](../../connect/jdbc/updating-large-data-sample.md) topic.

## Retrieving large data with adaptive buffering

When large values are read once by using the get\<Type>Stream methods, and the ResultSet columns and the CallableStatement OUT parameters are accessed in the order returned by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], adaptive buffering minimizes the application memory usage when processing the results. When using adaptive buffering:

- The get\<Type>Stream methods defined in the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) and [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) classes return read-once streams by default, although the streams can be reset if marked by the application. If the application wants to `reset` the stream, it has to call the `mark` method on that stream first.

- The get\<Type>Stream methods defined in the [SQLServerClob](../../connect/jdbc/reference/sqlserverclob-class.md) and [SQLServerBlob](../../connect/jdbc/reference/sqlserverblob-class.md) classes return streams that can always be repositioned to the start position of the stream without calling the `mark` method.

When the application uses adaptive buffering, the values retrieved by the get\<Type>Stream methods can only be retrieved once. If you try to call any get\<Type> method on the same column or parameter after calling the get\<Type>Stream method of the same object, an exception is thrown with the message, "The data has been accessed and is not available for this column or parameter".

> [!NOTE]
> A call to ResultSet.close() in the middle of processing a ResultSet would require the Microsoft JDBC Driver for SQL Server to read and discard all remaining packets. This may take substantial time if the query returned a large data set and especially if the network connection is slow.

## Guidelines for using adaptive buffering

Developers should follow these important guidelines to minimize memory usage by the application:

- Avoid using the connection string property **selectMethod=cursor** to allow the application to process a very large result set. The adaptive buffering feature allows applications to process very large forward-only, read-only result sets without using a server cursor. Note that when you set **selectMethod=cursor**, all forward-only, read-only result sets produced by that connection are impacted. In other words, if your application routinely processes short result sets with a few rows, creating, reading, and closing a server cursor for each result set will use more resources on both client-side and server-side than is the case where the **selectMethod** is not set to **cursor**.

- Read large text or binary values as streams by using the getAsciiStream, the getBinaryStream, or the getCharacterStream methods instead of the getBlob or the getClob methods. Starting with the version 1.2 release, the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class provides new get\<Type>Stream methods for this purpose.

- Ensure that columns with potentially large values are placed last in the list of columns in a SELECT statement and that the get\<Type>Stream methods of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) are used to access the columns in the order they are selected.

- Ensure that OUT parameters with potentially large values are declared last in the list of parameters in the SQL used to create the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md). In addition, ensure that the get\<Type>Stream methods of the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) are used to access the OUT parameters in the order they are declared.

- Avoid executing more than one statement on the same connection simultaneously. Executing another statement before processing the results of the previous statement may cause the unprocessed results to be buffered into the application memory.

- There are some cases where using **selectMethod=cursor** instead of **responseBuffering=adaptive** would be more beneficial, such as:

  - If your application processes a forward-only, read-only result set slowly, such as reading each row after some user input, using **selectMethod=cursor** instead of **responseBuffering=adaptive** might help reduce resource usage by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

  - If your application processes two or more forward-only, read-only result sets at the same time on the same connection, using **selectMethod=cursor** instead of **responseBuffering=adaptive** might help reduce the memory required by the driver while processing these result sets.

  In both cases, you need to consider the overhead of creating, reading, and closing the server cursors.

In addition, the following list provides some recommendations for scrollable and forward-only updatable result sets:

- For scrollable result sets, when fetching a block of rows the driver always reads into memory the number of rows indicated by the [getFetchSize](../../connect/jdbc/reference/getfetchsize-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) object, even when the adaptive buffering is enabled. If scrolling causes an OutOfMemoryError, you can reduce the number of rows fetched by calling the [setFetchSize](../../connect/jdbc/reference/setfetchsize-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) object to set the fetch size to a smaller number of rows, even down to 1 row, if necessary. If this does not prevent an OutOfMemoryError, avoid including very large columns in scrollable result sets.

- For forward-only updatable result sets, when fetching a block of rows the driver normally reads into memory the number of rows indicated by the [getFetchSize](../../connect/jdbc/reference/getfetchsize-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) object, even when the adaptive buffering is enabled on the connection. If calling the [next](../../connect/jdbc/reference/next-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) object results in an OutOfMemoryError, you can reduce the number of rows fetched by calling the [setFetchSize](../../connect/jdbc/reference/setfetchsize-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) object to set the fetch size to a smaller number of rows, even down to 1 row, if necessary. You can also force the driver not to buffer any rows by calling the [setResponseBuffering](../../connect/jdbc/reference/setresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) object with "**adaptive**" parameter before executing the statement. Because the result set is not scrollable, if the application accesses a large column value by using one of the get\<Type>Stream methods, the driver discards the value as soon as the application reads it just as it does for the forward-only read-only result sets.

## See also

[Improving performance and reliability with the JDBC driver](../../connect/jdbc/improving-performance-and-reliability-with-the-jdbc-driver.md)
