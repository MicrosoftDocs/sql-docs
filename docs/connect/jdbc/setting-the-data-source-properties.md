---
title: "Setting the Data Source Properties | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: f3363d05-07fc-4bf8-ae5e-2a7a968808ad
author: MightyPen
ms.author: genemi
manager: craigg
---

# Setting the Data Source Properties

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Data sources are the preferred mechanism by which to create JDBC connections in a Java Platform, Enterprise Edition (Java EE) environment. Data sources provide connections, pooled connections, and distributed connections without hard-coding connection properties into Java code. All [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] data sources can set or get the value of any property by using the appropriate setter and getter methods, respectively.

Java EE products, such as application servers and servlet/JSP engines, typically let you configure data sources for database access. Any property listed in the [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md) topic can be specified wherever the configuration lets you enter a property as a property=value pair.

For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data sources, see the [SQLServerDataSource](../../connect/jdbc/reference/sqlserverdatasource-class.md) class. For an example of how to use the SQLServerDataSource class to make a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, see [Data Source Sample](../../connect/jdbc/data-source-sample.md).

## See Also

[Connecting to SQL Server with the JDBC Driver](../../connect/jdbc/connecting-to-sql-server-with-the-jdbc-driver.md)
