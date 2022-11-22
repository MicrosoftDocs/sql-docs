---
title: Setting the data source properties
description: Learn about data sources in JDBC and how to set their properties to configure database access with Java.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Setting the data source properties

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Data sources are the preferred mechanism by which to create JDBC connections in a Java Platform, Enterprise Edition (Java EE) environment. Data sources provide connections, pooled connections, and distributed connections without hard-coding connection properties into Java code. All [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] data sources can set or get the value of any property by using the appropriate setter and getter methods, respectively.

Java EE products, such as application servers and servlet/JSP engines, typically let you configure data sources for database access. Any property listed in the [Setting the Connection Properties](setting-the-connection-properties.md) topic can be specified wherever the configuration lets you enter a property as a property=value pair.

For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data sources, see the [SQLServerDataSource](reference/sqlserverdatasource-class.md) class. For an example of how to use the SQLServerDataSource class to make a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, see [Data source sample](data-source-sample.md).

## See also

[Connecting to SQL Server with the JDBC driver](connecting-to-sql-server-with-the-jdbc-driver.md)
