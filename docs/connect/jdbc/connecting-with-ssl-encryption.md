---
title: "Connecting with SSL Encryption | Microsoft Docs"
ms.custom: ""
ms.date: "01/21/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: ec91fa8a-ab7e-4c1e-a05a-d7951ddf33b1
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connecting with SSL Encryption
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  The examples in this article describe how to use connection string properties that allow applications to use Secure Sockets Layer (SSL) encryption in a Java application. For more information about these new connection string properties such as **encrypt**, **trustServerCertificate**, **trustStore**, **trustStorePassword**, and **hostNameInCertificate**, see [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md).  
  
 When the **encrypt** property is set to **true** and the **trustServerCertificate** property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] won't validate the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SSL certificate. This is usually required for allowing connections in test environments, such as where the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance has only a self signed certificate.  
  
 The following code example demonstrates how to set the **trustServerCertificate** property in a connection string:  
  
```java
String connectionUrl =   
    "jdbc:sqlserver://localhost:1433;" +  
     "databaseName=AdventureWorks;integratedSecurity=true;" +  
     "encrypt=true;trustServerCertificate=true";  
```  
  
 When the **encrypt** property is set to **true** and the **trustServerCertificate** property is set to **false**, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] will validate the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SSL certificate. Validating the server certificate is a part of the SSL handshake and ensures that the server is the correct server to connect to. To validate the server certificate, the trust material must be supplied at connection time either by using **trustStore** and **trustStorePassword** connection properties explicitly, or by using the underlying Java Virtual Machine (JVM)'s default trust store implicitly.  
  
 The **trustStore** property specifies the path (including filename) to the certificate trustStore file, which contains the list of certificates that the client trusts. The **trustStorePassword** property specifies the password used to check the integrity of the trustStore data. For more information on using the JVM's default trust store, see the [Configuring the Client for SSL Encryption](../../connect/jdbc/configuring-the-client-for-ssl-encryption.md).  
  
 The following code example demonstrates how to set the **trustStore** and **trustStorePassword** properties in a connection string:  
  
```java
String connectionUrl =   
    "jdbc:sqlserver://localhost:1433;" +  
     "databaseName=AdventureWorks;integratedSecurity=true;" +  
     "encrypt=true; trustServerCertificate=false;" +  
     "trustStore=storeName;trustStorePassword=storePassword";  
```  
  
 The JDBC Driver provides an additional property, **hostNameInCertificate**, which specifies the host name of the server. The value of this property must match the subject property of the certificate.  
  
 The following code example demonstrates how to use the **hostNameInCertificate** property in a connection string:  
  
```java
String connectionUrl =   
    "jdbc:sqlserver://localhost:1433;" +  
     "databaseName=AdventureWorks;integratedSecurity=true;" +  
     "encrypt=true; trustServerCertificate=false;" +  
     "trustStore=storeName;trustStorePassword=storePassword" +  
     "hostNameInCertificate=hostName";  
```  
  
> [!NOTE]  
>  Alternatively, you can set the value of connection properties by using the appropriate **setter** methods provided by the [SQLServerDataSource](../../connect/jdbc/reference/sqlserverdatasource-class.md) class.  
  
 If the **encrypt** property is set to **true** and the **trustServerCertificate** property is set to **false** and if the server name in the connection string doesn't match the server name in the SSL certificate, the following error will be issued: `The driver couldn't establish a secure connection to SQL Server by using Secure Sockets Layer (SSL) encryption. Error: "java.security.cert.CertificateException: Failed to validate the server name in a certificate during Secure Sockets Layer (SSL) initialization."`. As of version 7.2, the driver supports wildcard pattern matching in the left-most label of the server name in the SSL certificate.
## See Also  
 [Using SSL Encryption](../../connect/jdbc/using-ssl-encryption.md)   
 [Securing JDBC Driver Applications](../../connect/jdbc/securing-jdbc-driver-applications.md)  
  
  
