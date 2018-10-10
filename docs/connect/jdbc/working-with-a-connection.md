---
title: "Working with a Connection | Microsoft Docs"
ms.custom: ""
ms.date: "07/11/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: cf8ee392-8a10-40a3-ae32-31c7b1efdd04
author: MightyPen
ms.author: genemi
manager: craigg
---

# Working with a Connection

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The following sections provide examples of the different ways to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)].

> [!NOTE]  
> If you have problems connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the JDBC driver, see [Troubleshooting Connectivity](../../connect/jdbc/troubleshooting-connectivity.md) for suggestions on how to correct it.

## Creating a Connection by Using the DriverManager Class

The simplest approach to creating a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database is to load the JDBC driver and call the getConnection method of the DriverManager class, as in the following:

```java
Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");  
String connectionUrl = "jdbc:sqlserver://localhost;database=AdventureWorks;integratedSecurity=true;"  
Connection con = DriverManager.getConnection(connectionUrl);  
```

This technique will create a database connection using the first available driver in the list of drivers that can successfully connect with the given URL.

> [!NOTE]  
> When using the sqljdbc4.jar class library, applications do not need to explicitly register or load the driver by using the Class.forName method. When the getConnection method of the DriverManager class is called, an appropriate driver is located from the set of registered JDBC drivers. For more information, see Using the JDBC Driver.

## Creating a Connection by Using the SQLServerDriver Class

If you have to specify a particular driver in the list of drivers for DriverManager, you can create a database connection by using the [connect](../../connect/jdbc/reference/connect-method-sqlserverdriver.md) method of the [SQLServerDriver](../../connect/jdbc/reference/sqlserverdriver-class.md) class, as in the following:

```java
Driver d = (Driver) Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver").newInstance();  
String connectionUrl = "jdbc:sqlserver://localhost;database=AdventureWorks;integratedSecurity=true;"  
Connection con = d.connect(connectionUrl, new Properties());  
```

## Creating a Connection by Using the SQLServerDataSource Class

If you have to create a connection by using the [SQLServerDataSource](../../connect/jdbc/reference/sqlserverdatasource-class.md) class, you can use various setter methods of the class before you call the [getConnection](../../connect/jdbc/reference/getconnection-method.md) method, as in the following:

```java
SQLServerDataSource ds = new SQLServerDataSource();  
ds.setUser("MyUserName");  
ds.setPassword("*****");  
ds.setServerName("localhost");  
ds.setPortNumber(1433);
ds.setDatabaseName("AdventureWorks");  
Connection con = ds.getConnection();  
```

## Creating a Connection that Targets a Very Specific Data Source

If you have to make a database connection that targets a very specific data source, there are a number of approaches that you can take. Each approach depends on the properties that you set by using the connection URL.

To connect to the default instance on a remote server, use the following:

```java
String url = "jdbc:sqlserver://MyServer;integratedSecurity=true;"
```

To connect to a specific port on a server, use the following:

```java
String url = "jdbc:sqlserver://MyServer:1533;integratedSecurity=true;"
```

To connect to a named instance on a server, use the following:

```java
String url = "jdbc:sqlserver://209.196.43.19;instanceName=INSTANCE1;integratedSecurity=true;"
```

To connect to a specific database on a server, use the following:

```java
String url = "jdbc:sqlserver://172.31.255.255;database=AdventureWorks;integratedSecurity=true;"
```

For more connection URL examples, see [Building the Connection URL](../../connect/jdbc/building-the-connection-url.md).

## Creating a Connection with a Custom Login Time-out

If you have to adjust for server load or network traffic, you can create a connection that has a specific login time-out value described in seconds, as in the following:

```java
String url = "jdbc:sqlserver://MyServer;loginTimeout=90;integratedSecurity=true;"
```

## Create a Connection with Application-level Identity

If you have to use logging and profiling, you will have to identify your connection as originating from a specific application, as in the following:

```java
String url = "jdbc:sqlserver://MyServer;applicationName=MYAPP.EXE;integratedSecurity=true;"
```

## Closing a Connection

You can explicitly close a database connection by calling the [close](../../connect/jdbc/reference/close-method-sqlserverconnection.md) method of the SQLServerConnection class, as in the following:

```java
con.close();
```

This will release the database resources that the SQLServerConnection object is using, or return the connection to the connection pool in pooled scenarios.

> [!NOTE]  
> Calling the close method will also roll back any pending transactions.

## See Also

[Connecting to SQL Server with the JDBC Driver](../../connect/jdbc/connecting-to-sql-server-with-the-jdbc-driver.md)
