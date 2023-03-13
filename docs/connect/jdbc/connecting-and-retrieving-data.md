---
title: Connecting and retrieving data
description: Learn how to connect to a SQL database and retrieve data using the Microsoft JDBC Driver for SQL Server and these code samples.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connecting and retrieving data

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When you are working with the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], there are two primary methods for establishing a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. One is to set connection properties in the connection URL, and then call the getConnection method of the DriverManager class to return a [SQLServerConnection](reference/sqlserverconnection-class.md) object.

> [!NOTE]
> For a list of the connection properties supported by the JDBC driver, see [Setting the connection properties](setting-the-connection-properties.md).

The second method involves setting the connection properties by using setter methods of the [SQLServerDataSource](reference/sqlserverdatasource-class.md) class, and then calling the [getConnection](reference/getconnection-method-sqlserverdatasource.md) method to return a SQLServerConnection object.

The topics in this section describe the different ways in which you can connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, and they also demonstrate different techniques for retrieving data.

## In this section

| Topic                                             | Description                                                                                                                                                   |
| ------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Connection URL Sample](connection-url-sample.md) | Describes how to use a connection URL to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and then use an SQL statement to retrieve data. |
| [Data Source Sample](data-source-sample.md)       | Describes how to use a data source to connect to SQL Server and then use a stored procedure to retrieve data.                                                 |

## See also

[Sample JDBC driver applications](sample-jdbc-driver-applications.md)
