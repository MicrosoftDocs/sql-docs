---
title: "Configuring the Client for SSL Encryption | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: ae34cd1f-3569-4759-80c7-7c9b33b3e9eb
author: MightyPen
ms.author: genemi
manager: craigg
---
# Configuring the Client for SSL Encryption
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] or client has to validate that the server is the correct server and its certificate is issued by a certificate authority that the client trusts. In order to validate the server certificate, the trust material must be supplied at connection time. In addition, the issuer of the server certificate must be a certificate authority that the client trusts.  
  
 This topic first describes how to supply the trust material in the client computer. Then, the topic describes how to import a server certificate to the client computer's trust store when the instance of SQL Server's Secure Sockets Layer (SSL) certificate is issued by a private certificate authority.  
  
 For more information about validating the server certificate, see the Validating Server SSL Certificate section in [Understanding SSL Support](../../connect/jdbc/understanding-ssl-support.md).  
  
## Configuring the Client Trust Store  
 Validating the server certificate requires that the trust material must be supplied at connection time either by using **trustStore** and **trustStorePassword** connection properties explicitly, or by using the underlying Java Virtual Machine (JVM)'s default trust store implicitly. For more information about how to set the **trustStore** and **trustStorePassword** properties in a connection string, see [Connecting with SSL Encryption](../../connect/jdbc/connecting-with-ssl-encryption.md).  
  
 If the **trustStore** property is unspecified or set to null, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] will rely on the underlying JVM's security provider, the Java Secure Socket Extension (SunJSSE). The SunJSSE provider provides a default TrustManager, which is used to validate X.509 certificates returned by SQL Server against the trust material provided in a trust store.  
  
 The TrustManager tries to locate the default trustStore in the following search order:  
  
-   If the system property "javax.net.ssl.trustStore" is defined, the TrustManager tries to find the default trustStore file by using the filename specified by that system property.  
  
-   If the "javax.net.ssl.trustStore" system property was not specified, and if the file "\<java-home>/lib/security/jssecacerts" exists, that file is used.  
  
-   If the file "\<java-home>/lib/security/cacerts" exists, that file is used.  
  
 For more information, see the SUNX509 TrustManager interface documentation on the Sun Microsystems Web site.  
  
 The Java Runtime Environment allows you to set the trustStore and trustStorePassword system properties as follows:  
  
```  
java -Djavax.net.ssl.trustStore=C:\MyCertificates\storeName  
java -Djavax.net.ssl.trustStorePassword=storePassword  
```  
  
 In this case, any application running on this JVM will use these settings as default. In order to override the default settings in your application, you should set the **trustStore** and **trustStorePassword** connection properties either in the connection string or in the appropriate setter method of the [SQLServerDataSource](../../connect/jdbc/reference/sqlserverdatasource-class.md) class.  
  
 In addition, you can configure and manage the default trust store files such as "\<java-home>/lib/security/jssecacerts" and "\<java-home>/lib/security/cacerts". To do that, use the JAVA "keytool" utility that is installed with the JRE (Java Runtime Environment). For more information about the "keytool" utility, see keytool documentation on the Sun Microsystems Web site.  
  
### Importing the Server Certificate to Trust Store  
 During the SSL handshake, the server sends its public key certificate to the client. The issuer of a public key certificate is known as a Certificate Authority (CA). The client has to ensure that the certificate authority is one that the client trusts. This is achieved by knowing the public key of trusted CAs in advance. Normally, the JVM ships with a predefined set of trusted certificate authorities.  
  
 If the instance of SQL Server's SSL certificate is issued by a private certificate authority, you must add the certificate authority's certificate to the list of trusted certificates in the client computer's trust store.  
  
 In order to do that, use the JAVA "keytool" utility that is installed with the JRE (Java Runtime Environment). The following command prompt demonstrates how to use the "keytool" utility to import a certificate from a file:  
  
```  
keytool -import -v -trustcacerts -alias myServer -file caCert.cer -keystore truststore.ks  
```  
  
 The example uses a file named "caCert.cer" as a certificate file. You must obtain this certificate file from the server. The following steps explain how to export the server certificate to a file:  
  
1.  Click Start and then Run, and type MMC. (MMC is an acronym for the Microsoft Management Console.)  
  
2.  In MMC, open the Certificates.  
  
3.  Expand Personal and then Certificates.  
  
4.  Right-click the server certificate, and then select All Tasks\Export.  
  
5.  Click Next to move past the welcome dialog box of the Certificate Export Wizard.  
  
6.  Confirm that "No, do not export the private key" is selected, and then click Next.  
  
7.  Make sure that either DER encoded binary X.509 (.CER) or Base-64 encoded X.509 (.CER) is selected, and then click Next.  
  
8.  Enter an export file name.  
  
9. Click Next, and then click Finish to export the certificate.  
  
## See Also  
 [Using SSL Encryption](../../connect/jdbc/using-ssl-encryption.md)   
 [Securing JDBC Driver Applications](../../connect/jdbc/securing-jdbc-driver-applications.md)  
  
  
