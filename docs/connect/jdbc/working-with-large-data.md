---
title: "Working with Large Data | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 5b93569f-eceb-4f05-b49c-067564cd3c85
author: MightyPen
ms.author: genemi
manager: craigg
---
# Working with Large Data

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The JDBC driver provides support for adaptive buffering, which allows you to retrieve any kind of large-value data without the overhead of server cursors. With adaptive buffering, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] retrieves statement execution results from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as the application needs them, rather than all at once. The driver also discards the results as soon as the application can no longer access them.

In the [!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] JDBC Driver version 1.2, the buffering mode was "**full**" by default. If your application did not set the "responseBuffering" connection property to "**adaptive**" either in the connection properties or by using the [setResponseBuffering](../../connect/jdbc/reference/setresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) object, the driver supported reading the entire result from the server at once. In order to get the adaptive buffering behavior, your application had to set the "responseBuffering" connection property to "**adaptive**" explicitly.  
  
The **adaptive** value is the default buffering mode and the JDBC driver buffers the minimum possible data when necessary. For more information about using adaptive buffering, see [Using Adaptive Buffering](../../connect/jdbc/using-adaptive-buffering.md).  
  
 The topics in this section describe different ways that you can use to retrieve large-value data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
## In This Section  
  
| Topic                                                                                                                      | Description                                                              |
| -------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------ |
| [Reading Large Data Sample](../../connect/jdbc/reading-large-data-sample.md)                                               | Describes how to use a SQL statement to retrieve large-value data.       |
| [Reading Large Data with Stored Procedures Sample](../../connect/jdbc/reading-large-data-with-stored-procedures-sample.md) | Describes how to retrieve a large CallableStatement OUT parameter value. |
| [Updating Large Data Sample](../../connect/jdbc/updating-large-data-sample.md)                                             | Describes how to update a large-value data in a database.                |
  
## See Also

[Sample JDBC Driver Applications](../../connect/jdbc/sample-jdbc-driver-applications.md)  
