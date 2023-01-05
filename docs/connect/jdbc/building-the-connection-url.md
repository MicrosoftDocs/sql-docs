---
title: Building the connection URL
description: Learn the format of the connection string used by the Microsoft JDBC Driver for SQL Server. Samples of connection strings are included in the examples section.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/06/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Building the connection URL

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The general form of the connection URL is

`jdbc:sqlserver://[serverName[\instanceName][:portNumber]][;property=value[;property=value]]`

where:

- **jdbc:sqlserver://** (Required) is known as the subprotocol and is constant.

- **serverName** (Optional) is the address of the server to connect to. This address can be a DNS or IP address, or it can be localhost or 127.0.0.1 for the local computer. If not specified in the connection URL, the server name must be specified in the properties collection.

- **instanceName** (Optional) is the instance to connect to on serverName. If not specified, a connection to the default instance is made.

- **portNumber** (Optional) is the port to connect to on serverName. The default is 1433. If you're using the default, you don't have to specify the port, nor its preceding ':', in the URL.

    > [!NOTE]
    >  For optimal connection performance, you should set the portNumber when you connect to a named instance. This will avoid a round trip to the server to determine the port number. If both a portNumber and instanceName are used, the portNumber will take precedence and the instanceName will be ignored.

- **property** (Optional) is one or more option connection properties. For more information, see [Setting the connection properties](setting-the-connection-properties.md). Any property from the list can be specified. Properties can only be delimited by using the semicolon (';'), and they can't be duplicated.

> [!CAUTION]
> For security purposes, you should avoid building the connection URLs based on user input. You should only specify the server name and driver in the URL. For user name and password values, use the connection property collections. For more information about security in your JDBC applications, see [Securing JDBC driver applications](securing-jdbc-driver-applications.md).

## Connection properties

For a detailed list of properties that can be set in the connection string, see [Setting the connection properties](setting-the-connection-properties.md#properties).

## Connection examples

Connect to the default database on the local computer by using a user name and password:

`jdbc:sqlserver://localhost;encrypt=true;user=MyUserName;password=*****;`

> [!NOTE]
> Although the previous example uses a username and password in the connection string, you should use integrated security as it is more secure. For more information, see the [Connecting with Integrated Authentication](#Connectingintegrated) section later in this topic.

The following connection string shows an example of how to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database using integrated authentication and Kerberos from an application running on any operating system supported by the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)]:

```java
jdbc:sqlserver://;servername=server_name;encrypt=true;integratedSecurity=true;authenticationScheme=JavaKerberos
```

Connect to the default database on the local computer by using integrated authentication:

`jdbc:sqlserver://localhost;encrypt=true;integratedSecurity=true;`

Connect to a named database on a remote server:

`jdbc:sqlserver://localhost;encrypt=true;databaseName=AdventureWorks;integratedSecurity=true;`

Connect on the default port to the remote server:

`jdbc:sqlserver://localhost:1433;encrypt=true;databaseName=AdventureWorks;integratedSecurity=true;`

Connect by specifying a customized application name:

`jdbc:sqlserver://localhost;encrypt=true;databaseName=AdventureWorks;integratedSecurity=true;applicationName=MyApp;`

## Named and multiple SQL Server instances

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows for the installation of multiple database instances per server. Each instance is identified by a specific name. To connect to a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can either specify the port number of the named instance (preferred), or you can specify the instance name as a JDBC URL property or a **datasource** property. If no instance name or port number property is specified, a connection to the default instance is created. See the following examples:

To specify a port number, use the following format:

`jdbc:sqlserver://localhost:1433;encrypt=true;integratedSecurity=true;<more properties as required>;`

To use a JDBC URL property, use the following format:

`jdbc:sqlserver://localhost;encrypt=true;instanceName=instance1;integratedSecurity=true;<more properties as required>;`

## Escaping values in the connection URL

You might have to escape certain parts of the connection URL values if the values include special characters like spaces, semicolons, and quotation marks. The JDBC driver supports escaping these characters by enclosing them in braces. For example, {;} escapes a semicolon.

Before version 8.4, escaped values can contain special characters (especially '=', ';', '[]', and space) but can't contain braces. Values that must be escaped and contain braces should be added to a properties collection.

In version 8.4 and above, escaped values can contain special characters, including braces. However, closing braces must be escaped. For example, with a password of `pass";{}word`, a connection string would need to escape the password as follows:

`jdbc:sqlserver://localhost;encrypt=true;username=MyUsername;password={pass";{}}word};`

> [!NOTE]
> White space inside the braces is literal and not trimmed.

## <a name="Connectingintegrated"></a> Connecting with integrated authentication On Windows

The JDBC driver supports the use of Type 2 integrated authentication on Windows operating systems by using the `integratedSecurity` connection string property. To use integrated authentication, copy the mssql-jdbc_auth-\<version>-\<arch>.dll file to a directory on the Windows system path on the computer where the JDBC driver is installed.

The mssql-jdbc_auth-\<version>-\<arch>.dll files are installed in the following location:

\<*installation directory*>\sqljdbc_\<*version*>\\<*language*>\auth\

For any operating system supported by the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], see [Using Kerberos Integrated Authentication to Connect to SQL Server](using-kerberos-integrated-authentication-to-connect-to-sql-server.md) for a description of a feature added in [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)] that allows an application to connect to a database using integrated authentication with Type 4 Kerberos.

> [!NOTE]
> If you are running a 32-bit Java Virtual Machine (JVM), use the mssql-jdbc_auth-\<version>-\<arch>.dll file in the x86 folder, even if the operating system is the x64 version. If you are running a 64-bit JVM on a x64 processor, use the mssql-jdbc_auth-\<version>-\<arch>.dll file in the x64 folder.

Alternatively you can set the java.library.path system property to specify the directory of the mssql-jdbc_auth-\<version>-\<arch>.dll. For example, if the JDBC driver is installed in the default directory, you can specify the location of the DLL by using the following virtual machine (VM) argument when the Java application is started:

`-Djava.library.path=C:\Microsoft JDBC Driver 6.4 for SQL Server\sqljdbc_<version>\enu\auth\x86`

## Connecting with IPv6 addresses

The JDBC driver supports the use of IPv6 addresses with the connection properties collection, and with the serverName connection string property. The initial serverName value, such as jdbc:*sqlserver*://*serverName*, isn't supported for IPv6 addresses in connection strings. Using a name for *serverName* instead of a raw IPv6 address will work in every case in the connection. The following examples provide more information.

**To use the serverName property:**

`jdbc:sqlserver://;serverName=3ffe:8311:eeee:f70f:0:5eae:10.203.31.9\\instance1;encrypt=true;integratedSecurity=true;`

**To use the properties collection:**

`Properties pro = new Properties();`

`pro.setProperty("serverName", "serverName=3ffe:8311:eeee:f70f:0:5eae:10.203.31.9\\instance1");`

`Connection con = DriverManager.getConnection("jdbc:sqlserver://;encrypt=true;integratedSecurity=true;", pro);`

## See also

[Connecting to SQL Server with the JDBC driver](connecting-to-sql-server-with-the-jdbc-driver.md)
