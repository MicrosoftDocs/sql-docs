---
title: Connecting with encryption
description: Find examples of how to connect using TLS encryption in your Java application by using the JDBC driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: vanto
ms.date: 03/31/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connecting with encryption

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The examples in this article describe how to use connection string properties that allow applications to use Transport Layer Security (TLS) encryption in a Java application. For more information about these new connection string properties such as **encrypt**, **trustServerCertificate**, **trustStore**, **trustStorePassword**, and **hostNameInCertificate**, see [Setting the Connection Properties](setting-the-connection-properties.md).

## Configuring the connection

When the **encrypt** property is set to **true** and the **trustServerCertificate** property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] won't validate the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] TLS certificate. This setting is common for allowing connections in test environments, such as where the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance has only a self-signed certificate.

The following code example demonstrates how to set the **trustServerCertificate** property in a connection string:

```java
String connectionUrl =
    "jdbc:sqlserver://localhost:1433;" +
     "databaseName=AdventureWorks;integratedSecurity=true;" +
     "encrypt=true;trustServerCertificate=true";
```

When the **encrypt** property is set to **true** and the **trustServerCertificate** property is set to **false**, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] will validate the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] TLS certificate. Validating the server certificate is a part of the TLS handshake and ensures that the server is the correct server to connect to. To validate the server certificate, the trust material must be supplied at connection time either by using **trustStore** and **trustStorePassword** connection properties explicitly, or by using the underlying Java Virtual Machine (JVM)'s default trust store implicitly.

The **trustStore** property specifies the path (including filename) to the certificate trustStore file, which contains the list of certificates that the client trusts. The **trustStorePassword** property specifies the password used to check the integrity of the trustStore data. For more information on using the JVM's default trust store, see the [Configuring the client for encryption](configuring-the-client-for-ssl-encryption.md).

The following code example demonstrates how to set the **trustStore** and **trustStorePassword** properties in a connection string:

```java
String connectionUrl =
    "jdbc:sqlserver://localhost:1433;" +
     "databaseName=AdventureWorks;integratedSecurity=true;" +
     "encrypt=true; trustServerCertificate=false;" +
     "trustStore=storeName;trustStorePassword=storePassword";
```

The JDBC Driver provides another property, **hostNameInCertificate**, which specifies the host name of the server. The value of this property must match the subject property of the certificate.

The following code example demonstrates how to use the **hostNameInCertificate** property in a connection string:

```java
String connectionUrl =
    "jdbc:sqlserver://localhost:1433;" +
     "databaseName=AdventureWorks;integratedSecurity=true;" +
     "encrypt=true; trustServerCertificate=false;" +
     "trustStore=storeName;trustStorePassword=storePassword;" +
     "hostNameInCertificate=hostName";
```

> [!NOTE]
> Alternatively, you can set the value of connection properties by using the appropriate **setter** methods provided by the [SQLServerDataSource](reference/sqlserverdatasource-class.md) class.

If the **encrypt** property is **true** and the **trustServerCertificate** property is **false** and if the server name in the connection string doesn't match the server name in the TLS certificate, the following error will be issued: `The driver couldn't establish a secure connection to SQL Server by using Secure Sockets Layer (SSL) encryption. Error: "java.security.cert.CertificateException: Failed to validate the server name in a certificate during Secure Sockets Layer (SSL) initialization."`. With version 7.2 and up, the driver supports wildcard pattern matching in the left-most label of the server name in the TLS certificate.

## See also

[Using encryption](using-ssl-encryption.md)  
[Securing JDBC driver applications](securing-jdbc-driver-applications.md)  
