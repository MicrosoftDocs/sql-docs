---
title: "Using SSL encryption | Microsoft Docs"
ms.custom: ""
ms.date: "08/12/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 8e566243-2f93-4b21-8065-3c8336649309
author: MightyPen
ms.author: genemi
---
# Using SSL encryption

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Secure Sockets Layer (SSL) encryption enables transmitting encrypted data across the network between an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a client application.  
  
Secure Sockets Layer (SSL) is a protocol for establishing a secure communication channel to prevent the interception of critical or sensitive information across the network and other Internet communications. SSL allows the client and the server to authenticate the identity of each other. After the participants are authenticated, SSL provides encrypted connections between them for secure message transmission.  
  
The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides an infrastructure to enable and disable the encryption on a particular connection based on the user specified connection properties and the server and client settings. The user can specify the certificate store location and password, a host name to be used to validate the certificate, and when to encrypt the communication channel.  
  
Enabling SSL encryption increases the security of data transmitted across networks between instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and applications. However, enabling encryption does slow performance.  
  
The topics in this section describe how the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] version supports SSL encryption, including new connection properties, and how you can configure the trust store at the client-side.  
  
> [!NOTE]  
> The **hostNameInCertificate** connection property is recommended to validate an SSL certificate.  

## In this section  

| Topic                                                                                                        | Description                                                                                                                                           |
| ------------------------------------------------------------------------------------------------------------ | ----------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Understanding SSL support](../../connect/jdbc/understanding-ssl-support.md)                                 | Describes how the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports SSL encryption.                                              |
| [Connecting with SSL encryption](../../connect/jdbc/connecting-with-ssl-encryption.md)                       | Describes how to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using the new SSL specific connection properties. |
| [Configuring the client for SSL encryption](../../connect/jdbc/configuring-the-client-for-ssl-encryption.md) | Describes how to configure the default trust store at the client-side and how to import a private certificate to the client computer's trust store.   |
  
## See also

[Securing JDBC driver applications](../../connect/jdbc/securing-jdbc-driver-applications.md)  
