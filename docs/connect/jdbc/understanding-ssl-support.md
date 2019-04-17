---
title: "Understanding SSL Support | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 073f3b9e-8edd-4815-88ea-de0655d0325e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Understanding SSL Support

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if the application requests encryption and the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to support SSL encryption, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] initiates the SSL handshake. The handshake allows the server and client to negotiate the encryption and cryptographic algorithms to be used to protect data. After the SSL handshake is complete, the client and server can send the encrypted data securely. During SSL handshake, the server sends its public key certificate to the client. The issuer of a public key certificate is known as a Certificate Authority (CA). The client is responsible for validating that the certificate authority is one that the client trusts.  
  
If the application does not request encryption, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] will not force [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to support SSL encryption. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is not configured to force the SSL encryption, a connection is established without encryption. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is configured to force the SSL encryption, the driver will automatically enable SSL encryption when running on properly configured Java Virtual Machine (JVM), or else the connection is terminated and the driver will raise an error.  
  
> [!NOTE]  
> Make sure the value passed to **serverName** exactly matches the Common Name (CN) or DNS name in the Subject Alternate Name (SAN) in the server certificate for an SSL connection to succeed.  
>
> For more information about how to configure SSL for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see the Encrypting Connections to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] topic in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Remarks

In order to allow applications to use SSL encryption, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] has introduced the following connection properties starting with the version 1.2 release: **encrypt**, **trustServerCertificate**, **trustStore**, **trustStorePassword**, and **hostNameInCertificate**. For more information, see [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md).  
  
 The following table summarizes how the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] version behaves for possible SSL connection scenarios. Each scenario uses a different set of SSL connection properties. The table includes:  
  
- **blank**: "The property does not exist in the connection string"  
  
- **value**: "The property exists in the connection string and its value is valid"  
  
- **any**: "It does not matter whether the property exists in the connection string or its value is valid"  
  
> [!NOTE]  
> The same behavior applies for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user authentication and Windows integrated authentication.  
  
| encrypt        | trustServerCertificate | hostNameInCertificate | trustStore | trustStorePassword | Behavior                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |
| -------------- | ---------------------- | --------------------- | ---------- | ------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| false or blank | any                    | any                   | any        | any                | The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] will not force [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to support SSL encryption. If the server has a self-signed certificate, the driver initiates the SSL certificate exchange. The SSL certificate will not be validated and only the credentials (in the login packet) are encrypted.<br /><br /> If the server requires the client to support SSL encryption, the driver will initiate the SSL certificate exchange. The SSL certificate will not be validated, but the entire communication will be encrypted.                                                                                                                                                                                    |
| true           | true                   | any                   | any        | any                | The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] requests to use SSL encryption with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If the server requires the client to support SSL encryption or if the server supports encryption, the driver will initiate the SSL certificate exchange. Note that if the **trustServerCertificate** property is set to "true", the driver will not validate the SSL certificate.<br /><br /> If the server is not configured to support encryption, the driver will raise an error and terminate the connection.                                                                                                                                                                                          |
| true           | false or blank         | blank                 | blank      | blank              | The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] requests to use SSL encryption with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If the server requires the client to support SSL encryption or if the server supports encryption, the driver will initiate the SSL certificate exchange.<br /><br /> The driver will use the **serverName** property specified on the connection URL to validate the server SSL certificate and rely on the trust manager factory's look-up rules to determine which certificate store to use.<br /><br /> If the server is not configured to support encryption, the driver will raise an error and terminate the connection.                                                                             |
| true           | false or blank         | value                 | blank      | blank              | The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] requests to use SSL encryption with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If the server requires the client to support SSL encryption or if the server supports encryption, the driver will initiate the SSL certificate exchange.<br /><br /> The driver will validate the SSL certificate's subject value by using the value specified for the **hostNameInCertificate** property.<br /><br /> If the server is not configured to support encryption, the driver will raise an error and terminate the connection.                                                                                                                                                                 |
| true           | false or blank         | blank                 | value      | value              | The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] requests to use SSL encryption with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If the server requires the client to support SSL encryption or if the server supports encryption, the driver will initiate the SSL certificate exchange.<br /><br /> The driver will use the **trustStore** property value to find the certificate trustStore file and **trustStorePassword** property value to check the integrity of the trustStore file.<br /><br /> If the server is not configured to support encryption, the driver will raise an error and terminate the connection.                                                                                                                |
| true           | false or blank         | blank                 | blank      | value              | The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] requests to use SSL encryption with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If the server requires the client to support SSL encryption or if the server supports encryption, the driver will initiate the SSL certificate exchange.<br /><br /> The driver will use the **trustStorePassword** property value to check the integrity of the default trustStore file.<br /><br /> If the server is not configured to support encryption, the driver will raise an error and terminate the connection.                                                                                                                                                                                  |
| true           | false or blank         | blank                 | value      | blank              | The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] requests to use SSL encryption with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If the server requires the client to support SSL encryption or if the server supports encryption, the driver will initiate the SSL certificate exchange.<br /><br /> The driver will use the **trustStore** property value to look up the location of the trustStore file.<br /><br /> If the server is not configured to support encryption, the driver will raise an error and terminate the connection.                                                                                                                                                                                                 |
| true           | false or blank         | value                 | blank      | value              | The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] requests to use SSL encryption with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If the server requires the client to support SSL encryption or if the server supports encryption, the driver will initiate the SSL certificate exchange.<br /><br /> The driver will use the **trustStorePassword** property value to check the integrity of the default trustStore file. In addition, the driver will use the **hostNameInCertificate** property value to validate the SSL certificate.<br /><br /> If the server is not configured to support encryption, the driver will raise an error and terminate the connection.                                                                   |
| true           | false or blank         | value                 | value      | blank              | The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] requests to use SSL encryption with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If the server requires the client to support SSL encryption or if the server supports encryption, the driver will initiate the SSL certificate exchange.<br /><br /> The driver will use the **trustStore** property value to look up the location of the trustStore file. In addition, the driver will use the **hostNameInCertificate** property value to validate the SSL certificate.<br /><br /> If the server is not configured to support encryption, the driver will raise an error and terminate the connection.                                                                                  |
| true           | false or blank         | value                 | value      | value              | The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] requests to use SSL encryption with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> If the server requires the client to support SSL encryption or if the server supports encryption, the driver will initiate the SSL certificate exchange.<br /><br /> The driver will use the **trustStore** property value to find the certificate trustStore file and **trustStorePassword** property value to check the integrity of the trustStore file. In addition, the driver will use the **hostNameInCertificate** property value to validate the SSL certificate.<br /><br /> If the server is not configured to support encryption, the driver will raise an error and terminate the connection. |
  
If the encrypt property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] uses the JVM's default JSSE security provider to negotiate SSL encryption with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The default security provider may not support all of the features required to negotiate SSL encryption successfully. For example, the default security provider may not support the size of the RSA public key used in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SSL certificate. In this case, the default security provider might raise an error that will cause the JDBC driver to terminate the connection. In order to resolve this issue, do one of the following:  
  
- Configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with a server certificate that has a smaller RSA public key  
  
- Configure the JVM to use a different JSSE security provider in the "\<java-home>/lib/security/java.security" security properties file  
  
- Use a different JVM  
  
## Validating Server SSL Certificate  

During SSL handshake, the server sends its public key certificate to the client. The JDBC driver or client has to validate that the server certificate is issued by a certificate authority that the client trusts. The driver requires that the server certificate must meet the following conditions:  
  
- The certificate was issued by a trusted certificate authority.  
  
- The certificate must be issued for server authentication.  
  
- The certificate is not expired.  
  
- The Common Name (CN) in the Subject or a DNS name in the Subject Alternate Name (SAN) of the certificate exactly matches the **serverName** value specified in the connection string or, if specified, the **hostNameInCertificate** property value.  
  
- A DNS name can include wild card characters. But the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] does not support wild card matching. That is, abc.com will not match \*.com but \*.com will match \*.com.  
  
## See Also

[Using SSL Encryption](../../connect/jdbc/using-ssl-encryption.md)

[Securing JDBC Driver Applications](../../connect/jdbc/securing-jdbc-driver-applications.md)  
