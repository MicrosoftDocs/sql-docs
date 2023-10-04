---
title: Application security
description: Learn about application security and java policy permissions when developing an application using the JDBC driver.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Application security

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When you use the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], it's important to take precautions to ensure the security of your application. The following sections provide information about steps you can take to help secure your application.

## Using Java policy permissions

When you use the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], it's important to specify the required Java policy permissions that the JDBC driver requires. The Java Runtime Environment (JRE) provides an extensive security model. That model can be used at runtime to determine whether a thread has access to a resource. Security policy files can control this access. The policy files themselves are managed by the deployer and the sysadmin for the container. The permissions listed in this article are ones that affect the functioning of the JDBC driver.

A typical permission in the policy file looks like the following.

```config
// Example policy file entry.
grant [signedBy <signer>,] [codeBase <code source>] {
   permission  <class>  [<name> [, <action list>]];
};
```

 The following codebase should be restricted to the JDBC driver codebase to ensure that you grant the least number of privileges.

```config
grant codeBase "file:/install_dir/lib/-" {

// Grant access to data source.
permission java.util.PropertyPermission "java.naming.*", "read,write";

// Specify which hosts can be connected to.
permission java.net.socketPermission "host:port", "connect";

// Logger permission to take advantage of logging.
permission java.util.logging.LoggingPermission;

// Grant listen/connect/accept permissions to the driver if
// connecting to a named instance as the client driver.
// This connects to a udp service and listens for a response.
permission java.net.SocketPermission "*", "listen, connect, accept";
};
```

> [!NOTE]
> The code "file:/install_dir/lib/-" refers to the installation directory of the JDBC driver.

## Protecting server communication

When you use the JDBC driver to communicate with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, it's important to secure the communication channel. You can secure the channel by using either Internet Protocol Security (IPSEC) or Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), or you can use both.

TLS support can be used to provide an extra level of protection besides IPSEC. For more information about using TLS, see [Using encryption](using-ssl-encryption.md).

## See also

[Securing JDBC driver applications](securing-jdbc-driver-applications.md)
