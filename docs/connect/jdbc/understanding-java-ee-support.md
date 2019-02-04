---
title: "Understanding Java EE Support | Microsoft Docs"
ms.custom: ""
ms.date: "01/21/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: a9448b80-b7a3-49cf-8bb4-322c73676005
author: MightyPen
ms.author: genemi
manager: craigg
---
# Understanding Java EE Support

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The following sections document how the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides support for the Java Platform, Enterprise Edition (Java EE) and JDBC 3.0 optional API features. The source code examples provided in this Help system provide a good reference for getting started with these features.  
  
First, make sure that your Java environment (JDK, JRE) includes the javax.sql package. This is a required package for any JDBC application that uses the optional API. JDK 1.5 and later versions already contain this package, so you don't have to install it separately.  
  
## Driver Name

The driver class name is **com.microsoft.sqlserver.jdbc.SQLServerDriver**. For JDBC Drivers 4.1, 4.2, and 6.0, the driver is contained in the **sqljdbc.jar**, **sqljdbc4.jar**, **sqljdbc41.jar**, or **sqljdbc42.jar** files.

For JDBC Driver 6.2, the driver is contained in **mssql-jdbc-6.2.2.jre7.jar** or **mssql-jdbc-6.2.2.jre8.jar**.

For JDBC Driver 6.4, the driver is contained in **mssql-jdbc-6.4.0.jre7.jar**, **mssql-jdbc-6.4.0.jre8.jar**, or **mssql-jdbc-6.4.0.jre9.jar**.

For JDBC Driver 7.0, the driver is contained in **mssql-jdbc-7.0.0.jre8.jar**, or **mssql-jdbc-7.0.0.jre10.jar**.

For JDBC Driver 7.2, the driver is contained in **mssql-jdbc-7.2.0.jre8.jar**, or **mssql-jdbc-7.2.0.jre11.jar**.
  
The class name is used whenever you load the driver with the JDBC DriverManager class. It's also used whenever you must specify the class name of the driver in any driver configuration. For example, configuring a data source within a Java EE application server might require you enter the driver class name.  
  
## Data Sources

The JDBC driver provides support for Java EE / JDBC 3.0 data sources. The JDBC driver [SQLServerXADataSource](../../connect/jdbc/reference/sqlserverxadatasource-class.md) class is implemented by `com.microsoft.sqlserver.jdbc.SQLServerXADataSource`.  
  
### Datasource Names

You can make database connections by using data sources. The data sources available with JDBC driver are described in the following table:  
  
|DataSource Type|Class Name and Description|  
|---------------|--------------------------|  
|DataSource|`com.microsoft.sqlserver.jdbc.SQLServerDataSource` <br/> <br/> The non pooling data source.|  
|ConnectionPoolDataSource|`com.microsoft.sqlserver.jdbc.SQLServerConnectionPoolDataSource` <br/> <br/> The data source to configure JAVA EE application server connection pools. Typically used when the application runs within a JAVA EE application server.|  
|XADataSource|`com.microsoft.sqlserver.jdbc.SQLServerXADataSource` <br/> <br/> The data source to configure JAVA EE XA data sources. Typically used when the application runs within a JAVA EE application server and an XA transaction manager.|  
  
### Data Source Properties

All data sources support the ability to set and get any property that is associated with the underlying driver's property set.  
  
Examples:  
  
`setServerName("localhost");`  
`setDatabaseName("AdventureWorks");`  
  
The following shows how an application connects by using a data source:  

```java
//initialize JNDI ..  
Context ctx = new InitialContext(System.getProperties());
...
DataSource ds = (DataSource) ctx.lookup("MyDataSource");
Connection c = ds.getConnection("user", "pwd");  
```

For more information about the data source properties, see [Setting the Data Source Properties](../../connect/jdbc/setting-the-data-source-properties.md).  
  
## See Also

[Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
