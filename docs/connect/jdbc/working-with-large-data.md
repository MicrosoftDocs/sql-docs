---
title: "Working with large data"
description: "Learn how to work with large data types in the JDBC Driver for SQL Server in these sample applications."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Working with large data

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The JDBC driver provides support for adaptive buffering, which allows you to retrieve any kind of large-value data without the overhead of server cursors. With adaptive buffering, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] retrieves statement execution results from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as the application needs them, rather than all at once. The driver also discards the results as soon as the application can no longer access them.

In the [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] JDBC Driver version 1.2, the buffering mode was "**full**" by default. If your application did not set the "responseBuffering" connection property to "**adaptive**" either in the connection properties or by using the [setResponseBuffering](reference/setresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](reference/sqlserverstatement-class.md) object, the driver supported reading the entire result from the server at once. In order to get the adaptive buffering behavior, your application had to set the "responseBuffering" connection property to "**adaptive**" explicitly.

The **adaptive** value is the default buffering mode and the JDBC driver buffers the minimum possible data when necessary. For more information about using adaptive buffering, see [Using adaptive buffering](using-adaptive-buffering.md).

 The topics in this section describe different ways that you can use to retrieve large-value data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.

## In This Section

| Topic                                                                                                   | Description                                                              |
| ------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------ |
| [Reading large data sample](reading-large-data-sample.md)                                               | Describes how to use a SQL statement to retrieve large-value data.       |
| [Reading large data with stored procedures sample](reading-large-data-with-stored-procedures-sample.md) | Describes how to retrieve a large CallableStatement OUT parameter value. |
| [Updating large data sample](updating-large-data-sample.md)                                             | Describes how to update a large-value data in a database.                |

## See also

[Sample JDBC driver applications](sample-jdbc-driver-applications.md)
