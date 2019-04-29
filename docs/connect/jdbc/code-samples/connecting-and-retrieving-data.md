---
title: "Connecting and Retrieving Data | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: ce43cc20-46a3-42ff-a3fb-75ad1ed10e08
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connecting and Retrieving Data

[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

When you are working with the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)], there are two primary methods for establishing a connection to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database. One is to set connection properties in the connection URL, and then call the getConnection method of the DriverManager class to return a [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.  
  
> [!NOTE]  
> For a list of the connection properties supported by the JDBC driver, see [Setting the Connection Properties](../../../connect/jdbc/setting-the-connection-properties.md).  
  
The second method involves setting the connection properties by using setter methods of the [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md) class, and then calling the [getConnection](../../../connect/jdbc/reference/getconnection-method-sqlserverdatasource.md) method to return a SQLServerConnection object.  
  
The topics in this section describe the different ways in which you can connect to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, and they also demonstrate different techniques for retrieving data.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Connection URL Sample](../../../connect/jdbc/code-samples/connection-url-sample.md)|Describes how to use a connection URL to connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and then use an SQL statement to retrieve data.|  
|[Data Source Sample](../../../connect/jdbc/code-samples/data-source-sample.md)|Describes how to use a data source to connect to SQL Server and then use a stored procedure to retrieve data.|  
  
## See Also

[Sample JDBC Driver Applications](../../jdbc/code-samples/sample-jdbc-driver-applications.md)
  
