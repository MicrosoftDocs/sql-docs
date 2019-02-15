---
title: "Building the Connection URL | Microsoft Docs"
ms.custom: ""
ms.date: "07/11/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 44996746-d373-4f59-9863-a8a20bb8024a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Building the Connection URL
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  The general form of the connection URL is  
  
 `jdbc:sqlserver://[serverName[\instanceName][:portNumber]][;property=value[;property=value]]`  
  
 where:  
  
-   **jdbc:sqlserver://** (Required) is known as the subprotocol and is constant.  
  
-   **serverName** (Optional) is the address of the server to connect to. This could be a DNS or IP address, or it could be localhost or 127.0.0.1 for the local computer. If not specified in the connection URL, the server name must be specified in the properties collection.  
  
-   **instanceName** (Optional) is the instance to connect to on serverName. If not specified, a connection to the default instance is made.  
  
-   **portNumber** (Optional) is the port to connect to on serverName. The default is 1433. If you're using the default, you don't have to specify the port, nor its preceding ':', in the URL.  
  
    > [!NOTE]  
    >  For optimal connection performance, you should set the portNumber when you connect to a named instance. This will avoid a round trip to the server to determine the port number. If both a portNumber and instanceName are used, the portNumber will take precedence and the instanceName will be ignored.  
  
-   **property** (Optional) is one or more option connection properties. For more information, see [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md). Any property from the list can be specified. Properties can only be delimited by using the semicolon (';'), and they can't be duplicated.  
  
> [!CAUTION]  
>  For security purposes, you should avoid building the connection URLs based on user input. You should only specify the server name and driver in the URL. For user name and password values, use the connection property collections. For more information about security in your JDBC applications, see [Securing JDBC Driver Applications](../../connect/jdbc/securing-jdbc-driver-applications.md).  
  
## Connection Examples  
 Connect to the default database on the local computer by using a user name and password:  
  
 `jdbc:sqlserver://localhost;user=MyUserName;password=*****;`  
  
> [!NOTE]  
>  Although the previous example uses a username and password in the connection string, you should use integrated security as it is more secure. For more information, see the [Connecting with Integrated Authentication](#Connectingintegrated) section later in this topic.  
  
 The following connection string shows an example of how to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database using integrated authentication and Kerberos from an application running on any operating system supported by the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)]:  
  
```java
jdbc:sqlserver://;servername=server_name;integratedSecurity=true;authenticationScheme=JavaKerberos  
```  
  
 Connect to the default database on the local computer by using integrated authentication:  
  
 `jdbc:sqlserver://localhost;integratedSecurity=true;`  
  
 Connect to a named database on a remote server:  
  
 `jdbc:sqlserver://localhost;databaseName=AdventureWorks;integratedSecurity=true;`  
  
 Connect on the default port to the remote server:  
  
 `jdbc:sqlserver://localhost:1433;databaseName=AdventureWorks;integratedSecurity=true;`  
  
 Connect by specifying a customized application name:  
  
 `jdbc:sqlserver://localhost;databaseName=AdventureWorks;integratedSecurity=true;applicationName=MyApp;`  
  
## Named and Multiple SQL Server Instances  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows for the installation of multiple database instances per server. Each instance is identified by a specific name. To connect to a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can either specify the port number of the named instance (preferred), or you can specify the instance name as a JDBC URL property or a **datasource** property. If no instance name or port number property is specified, a connection to the default instance is created. See the following examples:  
  
 To use a port number, do the following:  
  
 `jdbc:sqlserver://localhost:1433;integratedSecurity=true;<more properties as required>;`  
  
 To use a JDBC URL property, do the following:  
  
 `jdbc:sqlserver://localhost;instanceName=instance1;integratedSecurity=true;<more properties as required>;`  
  
## Escaping Values in the Connection URL  
 You might have to escape certain parts of the connection URL values because of the inclusion of special characters such as spaces, semicolons, and quotation marks. The JDBC driver supports escaping these characters if they are enclosed in braces. For example, {;} escapes a semicolon.  
  
 Escaped values can contain special characters (especially '=', ';', '[]', and space) but cannot contain braces. Values that must be escaped and contain braces should be added to a properties collection.  
  
> [!NOTE]  
>  White space inside the braces is literal and not trimmed.  
  
##  <a name="Connectingintegrated"></a> Connecting with Integrated Authentication On Windows  
 The JDBC driver supports the use of Type 2 integrated authentication on Windows operating systems through the integratedSecurity connection string property. To use integrated authentication, copy the sqljdbc_auth.dll file to a directory on the Windows system path on the computer where the JDBC driver is installed.  
  
 The sqljdbc_auth.dll files are installed in the following location:  
  
 \<*installation directory*>\sqljdbc_\<*version*>\\<*language*>\auth\  
  
 For any operating system supported by the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], see [Using Kerberos Integrated Authentication to Connect to SQL Server](../../connect/jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server.md) for a description of a feature added in [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)] that allows an application to connect to a database using integrated authentication with Type 4 Kerberos.  
  
> [!NOTE]  
>  If you are running a 32-bit Java Virtual Machine (JVM), use the sqljdbc_auth.dll file in the x86 folder, even if the operating system is the x64 version. If you are running a 64-bit JVM on a x64 processor, use the sqljdbc_auth.dll file in the x64 folder.  
  
 Alternatively you can set the java.library.path system property to specify the directory of the sqljdbc_auth.dll. For example, if the JDBC driver is installed in the default directory, you can specify the location of the DLL by using the following virtual machine (VM) argument when the Java application is started:  
  
 `-Djava.library.path=C:\Microsoft JDBC Driver 6.4 for SQL Server\sqljdbc_<version>\enu\auth\x86`  
  
## Connecting with IPv6 Addresses  
 The JDBC driver supports the use of IPv6 addresses with the connection properties collection, and with the serverName connection string property. The initial serverName value, such as jdbc:*sqlserver*://*serverName*, isn't supported for IPv6 addresses in connection strings. Using a name for *serverName* instead of a raw IPv6 address will work in every case in the connection. The following examples provide more information.  
  
 **To use the serverName property**  
  
 `jdbc:sqlserver://;serverName=3ffe:8311:eeee:f70f:0:5eae:10.203.31.9\\instance1;integratedSecurity=true;`  
  
 **To use the properties collection**  
  
 `Properties pro = new Properties();`  
  
 `pro.setProperty("serverName", "serverName=3ffe:8311:eeee:f70f:0:5eae:10.203.31.9\\instance1");`  
  
 `Connection con = DriverManager.getConnection("jdbc:sqlserver://;integratedSecurity=true;", pro);`  
  
## See Also  
 [Connecting to SQL Server with the JDBC Driver](../../connect/jdbc/connecting-to-sql-server-with-the-jdbc-driver.md)  
  
  
