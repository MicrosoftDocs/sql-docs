---
title: Configuring the client for encryption
description: Learn about client-side encryption and certificate trust to ensure the security of clients using the Microsoft JDBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: vanto
ms.date: 03/31/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Configuring the client for encryption

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] or client has to validate that the server is the correct server and its certificate is issued by a certificate authority the client trusts. To validate the server certificate, the trust material must be supplied at connection time. Also, the issuer of the server certificate must be a certificate authority that the client trusts.

This article first describes how to supply the trust material in the client computer. Then it describes how to import a server certificate to the client computer's trust store when the instance of SQL Server's Transport Layer Security (TLS) certificate is issued by a private certificate authority.

For more information about validating the server certificate, see the Validating Server TLS Certificate section in [Understanding encryption support](understanding-ssl-support.md).

## Configuring the client trust store

Validating the server certificate requires the trust material at connection time either by using **trustStore** and **trustStorePassword** connection properties explicitly, or by using the underlying Java Virtual Machine (JVM)'s default trust store implicitly. For more information about how to set the **trustStore** and **trustStorePassword** properties in a connection string, see [Connecting with encryption](connecting-with-ssl-encryption.md).

If the **trustStore** property is unspecified or set to null, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] will rely on the underlying JVM's security provider, the Java Secure Socket Extension (SunJSSE). The SunJSSE provider provides a default TrustManager, which is used to validate X.509 certificates returned by SQL Server against the trust material provided in a trust store.

The TrustManager tries to locate the default trustStore in the following search order:

- If the system property "javax.net.ssl.trustStore" is defined, the TrustManager tries to find the default trustStore file by using the filename specified by that system property.
- If the "javax.net.ssl.trustStore" system property wasn't specified, and the file "\<java-home>/lib/security/jssecacerts" exists, that file is used.
- If the file "\<java-home>/lib/security/cacerts" exists, that file is used.

For more information, see the SUNX509 TrustManager interface documentation on the Sun Microsystems Web site.

The Java Runtime Environment allows you to set the trustStore and trustStorePassword system properties as follows:

```java
java -Djavax.net.ssl.trustStore=C:\MyCertificates\storeName
java -Djavax.net.ssl.trustStorePassword=storePassword
```

In this case, any application running on this JVM will use these settings as default. To override the default settings in your application, you should set the **trustStore** and **trustStorePassword** connection properties either in the connection string or in the appropriate setter method of the [SQLServerDataSource](reference/sqlserverdatasource-class.md) class.

Also, you can configure and manage the default trust store files such as "\<java-home>/lib/security/jssecacerts" and "\<java-home>/lib/security/cacerts". To do that, use the JAVA "keytool" utility that is installed with the JRE (Java Runtime Environment). For more information about the "keytool" utility, see [keytool documentation on the Oracle Web site](https://docs.oracle.com/javase/8/docs/technotes/tools/unix/keytool.html).

### Importing the server certificate to trust store

During the TLS handshake, the server sends its public key certificate to the client. The issuer of a public key certificate is known as a Certificate Authority (CA). The client has to ensure the certificate authority is one the client trusts. This assurance is achieved by knowing the public key of trusted CAs in advance. Normally, the JVM ships with a predefined set of trusted certificate authorities.

If the instance of SQL Server's TLS certificate is issued by a private certificate authority, you must add the certificate authority's certificate to the list of trusted certificates in the client computer's trust store.

To do that, use the JAVA "keytool" utility that is installed with the JRE (Java Runtime Environment). The following command prompt demonstrates how to use the "keytool" utility to import a certificate from a file:

```java
keytool -import -v -trustcacerts -alias myServer -file caCert.cer -keystore truststore.ks
```

The example uses a file named "caCert.cer" as a certificate file. You must obtain this certificate file from the server. The following steps explain how to export the server certificate to a file:

1. Select Start and then Run, and type MMC. (MMC is an acronym for the Microsoft Management Console.)
2. In MMC, open the Certificates.
3. Expand Personal and then Certificates.
4. Right-click the server certificate, and then select All Tasks\Export.
5. Select Next to move past the welcome dialog box of the Certificate Export Wizard.
6. Confirm that `No, do not export the private key` is selected, and then select Next.
7. Make sure to select either DER encoded binary X.509 (.CER) or Base-64 encoded X.509 (.CER), and then select Next.
8. Enter an export file name.
9. Select Next, and then select Finish to export the certificate.

## See also

[Using encryption](using-ssl-encryption.md)  
[Securing JDBC driver applications](securing-jdbc-driver-applications.md)  
