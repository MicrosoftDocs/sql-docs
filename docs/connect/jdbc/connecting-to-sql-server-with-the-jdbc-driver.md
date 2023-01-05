---
title: Connecting to SQL Server with the JDBC driver
description: When connecting to the database using the Microsoft JDBC Driver for SQL Server, all interaction with the database goes through the SQLServerConnection object.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connecting to SQL Server with the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

One of the most fundamental things that you'll do with the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] is to make a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. All interaction with the database occurs through the [SQLServerConnection](reference/sqlserverconnection-class.md) object, and because the JDBC driver has such a flat architecture, almost all interesting behavior touches the SQLServerConnection object.

If a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is only listening on an IPv6 port, set the java.net.preferIPv6Addresses system property to make sure that IPv6 is used instead of IPv4 to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:

```java
System.setProperty("java.net.preferIPv6Addresses", "true");
```

The articles in this section describe how to make and work with a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.

## In this section

|Article|Description|
|-----------|-----------------|
|[Building the connection URL](building-the-connection-url.md)|Describes how to form a connection URL for connecting to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Also describes connecting to named instances of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|
|[Setting the connection properties](setting-the-connection-properties.md)|Describes the various connection properties and how they can be used when you connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|
|[Setting the data source Properties](setting-the-data-source-properties.md)|Describes how to use data sources in a Java Platform, Enterprise Edition (Java EE) environment.|
|[Working with a connection](working-with-a-connection.md)|Describes the various ways in which to create an instance of a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|
|[Using connection pooling](using-connection-pooling.md)|Describes how the JDBC driver supports the use of connection pooling.|
|[Using database mirroring &#40;JDBC&#41;](using-database-mirroring-jdbc.md)|Describes how the JDBC driver supports the use of database mirroring.|
|[JDBC driver support for High Availability, disaster recovery](jdbc-driver-support-for-high-availability-disaster-recovery.md)|Describes how to develop an application that will connect to an Always On availability group.|
|[Using Kerberos Integrated Authentication to Connect to SQL Server](using-kerberos-integrated-authentication-to-connect-to-sql-server.md)|Discusses a Java implementation for applications to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database using Kerberos integrated authentication.|
|[Connecting to an Azure SQL database](connecting-to-an-azure-sql-database.md)|Discusses connectivity issues for databases on Azure SQL.|

## See also

[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)
