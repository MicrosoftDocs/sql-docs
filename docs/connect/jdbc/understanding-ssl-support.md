---
title: Understanding encryption support
description: Learn how to ensure the JDBC driver uses TLS encryption to secure connections to a SQL database.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: vanto
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Understanding encryption support

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if the application requests encryption and the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to support TLS encryption, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] initiates the TLS handshake. The handshake allows the server and client to negotiate the encryption and cryptographic algorithms to be used to protect data. After the TLS handshake is complete, the client and server can send the encrypted data securely. During the TLS handshake, the server sends its public key certificate to the client. The issuer of a public key certificate is known as a Certificate Authority (CA). The client is responsible for validating that the certificate authority is one the client trusts.

If the application doesn't request encryption, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] won't force [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to support TLS encryption. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance isn't configured to force the TLS encryption, a connection is established without encryption. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is configured to force the TLS encryption, the driver will automatically enable TLS encryption when running on properly configured Java Virtual Machine (JVM), or else the connection is terminated and the driver will raise an error.

> [!NOTE]
> Make sure the value passed to **serverName** exactly matches the Common Name (CN) or DNS name in the Subject Alternate Name (SAN) in the server certificate for a TLS connection to succeed.
>
> For more information about how to configure TLS for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Enable encrypted connections to the Database Engine](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).

## Remarks

To allow applications to use TLS encryption, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] has introduced the following connection properties starting with the version 1.2 release: **encrypt**, **trustServerCertificate**, **trustStore**, **trustStorePassword**, and **hostNameInCertificate**. To allow the driver to use TDS 8.0 with TLS encryption, the connection property **serverCertificate** has been introduced starting with the version 11.2 release. For more information, see [Setting the connection properties](setting-the-connection-properties.md).

The following table summarizes how the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] version behaves for possible TLS connection scenarios. Each scenario uses a different set of TLS connection properties. The table includes:

- **blank**: "The property doesn't exist in the connection string"
- **value**: "The property exists in the connection string and its value is valid"
- **any**: "It doesn't matter whether the property exists in the connection string or its value is valid"

> [!NOTE]
> The same behavior applies for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user authentication and Windows integrated authentication.

<!-- Do not remove the extra &nbsp;'s in the column1 heading. They keep column1 from wrapping and improve readability of the table. -->
| Property&nbsp;settings&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | Behavior |
| ----------------- | -------- |
| **encrypt** = false or blank<br/> **trustServerCertificate** = any<br/> **hostNameInCertificate** = any<br/> **trustStore** = any<br/> **trustStorePassword** = any<br/> | The driver won't force the server to support TLS encryption. If the server has a self-signed certificate, the driver initiates the TLS certificate exchange. The TLS certificate won't be validated and only the credentials (in the login packet) are encrypted.<br /><br /> If the server requires the client to support TLS encryption, the driver will initiate the TLS certificate exchange. The TLS certificate won't be validated, but the entire communication will be encrypted. |
| **encrypt** = true<br/> **trustServerCertificate** = true<br/> **hostNameInCertificate** = any<br/> **trustStore** = any<br/> **trustStorePassword** = any<br/> | The driver requests to use TLS encryption with the server.<br /><br /> If the server requires the client to support TLS encryption or if the server supports encryption, the driver will initiate the TLS certificate exchange. If the **trustServerCertificate** property is set to "true", the driver won't validate the TLS certificate.<br /><br /> If the server isn't configured to support encryption, the driver will raise an error and terminate the connection. |
| **encrypt** = true<br/> **trustServerCertificate** = false or blank<br/> **hostNameInCertificate** = blank<br/> **trustStore** = blank<br/> **trustStorePassword** = blank<br/> | The driver requests to use TLS encryption with the server.<br /><br /> If the server requires the client to support TLS encryption or if the server supports encryption, the driver will initiate the TLS certificate exchange.<br /><br /> The driver will use the **serverName** property specified on the connection URL to validate the server TLS certificate and rely on the trust manager factory's look-up rules to determine which certificate store to use.<br /><br /> If the server isn't configured to support encryption, the driver will raise an error and terminate the connection. |
| **encrypt** = true<br/> **trustServerCertificate** = false or blank<br/> **hostNameInCertificate** = value<br/> **trustStore** = blank<br/> **trustStorePassword** = blank<br/> | The driver requests to use TLS encryption with the server.<br /><br /> If the server requires the client to support TLS encryption or if the server supports encryption, the driver will initiate the TLS certificate exchange.<br /><br /> The driver will validate the TLS certificate's subject value by using the value specified for the **hostNameInCertificate** property.<br /><br /> If the server isn't configured to support encryption, the driver will raise an error and terminate the connection. |
| **encrypt** = true<br/> **trustServerCertificate** = false or blank<br/> **hostNameInCertificate** = blank<br/> **trustStore** = value<br/> **trustStorePassword** = value<br/> | The driver requests to use TLS encryption with the server.<br /><br /> If the server requires the client to support TLS encryption or if the server supports encryption, the driver will initiate the TLS certificate exchange.<br /><br /> The driver will use the **trustStore** property value to find the certificate trustStore file and **trustStorePassword** property value to check the integrity of the trustStore file.<br /><br /> If the server isn't configured to support encryption, the driver will raise an error and terminate the connection. |
| **encrypt** = true<br/> **trustServerCertificate** = false or blank<br/> **hostNameInCertificate** = blank<br/> **trustStore** = blank<br/> **trustStorePassword** = value<br/> | The driver requests to use TLS encryption with the server.<br /><br /> If the server requires the client to support TLS encryption or if the server supports encryption, the driver will initiate the TLS certificate exchange.<br /><br /> The driver will use the **trustStorePassword** property value to check the integrity of the default trustStore file.<br /><br /> If the server isn't configured to support encryption, the driver will raise an error and terminate the connection. |
| **encrypt** = true<br/> **trustServerCertificate** = false or blank<br/> **hostNameInCertificate** = blank<br/> **trustStore** = value<br/> **trustStorePassword** = blank<br/> | The driver requests to use TLS encryption with the server.<br /><br /> If the server requires the client to support TLS encryption or if the server supports encryption, the driver will initiate the TLS certificate exchange.<br /><br /> The driver will use the **trustStore** property value to look up the location of the trustStore file.<br /><br /> If the server isn't configured to support encryption, the driver will raise an error and terminate the connection. |
| **encrypt** = true<br/> **trustServerCertificate** = false or blank<br/> **hostNameInCertificate** = value<br/> **trustStore** = blank<br/> **trustStorePassword** = value<br/> | The driver requests to use TLS encryption with the server.<br /><br /> If the server requires the client to support TLS encryption or if the server supports encryption, the driver will initiate the TLS certificate exchange.<br /><br /> The driver will use the **trustStorePassword** property value to check the integrity of the default trustStore file. Also, the driver will use the **hostNameInCertificate** property value to validate the TLS certificate.<br /><br /> If the server isn't configured to support encryption, the driver will raise an error and terminate the connection. |
| **encrypt** = true<br/> **trustServerCertificate** = false or blank<br/> **hostNameInCertificate** = value<br/> **trustStore** = value<br/> **trustStorePassword** = blank<br/> | The driver requests to use TLS encryption with the server.<br /><br /> If the server requires the client to support TLS encryption or if the server supports encryption, the driver will initiate the TLS certificate exchange.<br /><br /> The driver will use the **trustStore** property value to look up the location of the trustStore file. Also, the driver will use the **hostNameInCertificate** property value to validate the TLS certificate.<br /><br /> If the server isn't configured to support encryption, the driver will raise an error and terminate the connection. |
| **encrypt** = true<br/> **trustServerCertificate** = false or blank<br/> **hostNameInCertificate** = value<br/> **trustStore** = value<br/> **trustStorePassword** = value<br/> | The driver requests to use TLS encryption with the server.<br /><br /> If the server requires the client to support TLS encryption or if the server supports encryption, the driver will initiate the TLS certificate exchange.<br /><br /> The driver will use the **trustStore** property value to find the certificate trustStore file and **trustStorePassword** property value to check the integrity of the trustStore file. Also, the driver will use the **hostNameInCertificate** property value to validate the TLS certificate.<br /><br /> If the server isn't configured to support encryption, the driver will raise an error and terminate the connection. |
| **encrypt** = strict<br/> **hostNameInCertificate** = value<br/> **trustStore** = blank<br/> **trustStorePassword** = blank<br/> **serverCertificate** = value<br/> | The driver requests to use TDS 8.0 `strict` TLS encryption with the server.<br /><br /> The driver will initiate the TLS handshake and certificate exchange with the server as the first action.<br /><br /> The **trustServerCertificate** setting is ignored and treated as false in `strict` mode.<br /><br /> The driver will use the optional **hostNameInCertificate** or **serverCertificate** properties to validate the server TLS certificate.<br /><br /> If the server isn't configured to support TDS 8 connections, the driver will raise an error and terminate the connection. |

If the encrypt property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] uses the JVM's default JSSE security provider to negotiate TLS encryption with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The default security provider may not support all of the features required to negotiate TLS encryption successfully. For example, the default security provider may not support the size of the RSA public key used in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] TLS certificate. In this case, the default security provider might raise an error that will cause the JDBC driver to terminate the connection. To resolve this issue, one of the following options can be used:

- Configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with a server certificate that has a smaller RSA public key
- Configure the JVM to use a different JSSE security provider in the "\<java-home>/lib/security/java.security" security properties file
- Use a different JVM

## Validating server TLS certificate

During the TLS handshake, the server sends its public key certificate to the client. The JDBC driver or client has to validate that the server certificate is issued by a certificate authority the client trusts. The driver requires the server certificate to meet the following conditions:

- The certificate was issued by a trusted certificate authority.
- The certificate must be issued for server authentication.
- The certificate isn't expired.
- The Common Name (CN) in the Subject or a DNS name in the Subject Alternate Name (SAN) of the certificate exactly matches the **serverName** value specified in the connection string or, if specified, the **hostNameInCertificate** property value.
- A DNS name can include wild-card characters. Prior version 7.2, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] doesn't support wild-card matching. That is, abc.com won't match \*.com but \*.com will match \*.com. With version 7.2 and up, standard certificate wild-card matching is supported.

For use of TDS 8.0 with `strict` encryption, the **serverCertifcate** property value provides the path to a server certificate to be used for server certificate validation. This file must use the PEM file format. The certificate received from the server must match this certificate exactly.

## See also

[Using encryption](using-ssl-encryption.md)  
[Securing JDBC driver applications](securing-jdbc-driver-applications.md)  
