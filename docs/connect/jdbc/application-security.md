---
description: "Application security"
title: "Application security | Microsoft Docs"
ms.custom: ""
ms.date: "08/12/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 940879b4-aa0f-41ce-a369-6cfc0e78e01d
author: David-Engel
ms.author: v-daenge
---
# Application security
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  When you use the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], it is important to take precautions to ensure the security of your application. The following sections provide information regarding steps you can take to help secure your application.  
  
## Using Java policy permissions  
 When you use the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], it is important to specify the required Java policy permissions that the JDBC driver requires. The Java Runtime Environment (JRE) provides an extensive security model that you can use at runtime to determine whether a thread has access to a resource. Security policy files can control this access. The policy files themselves are managed by the deployer and the sysadmin for the container, but the permissions listed in this topic are those that affect the functioning of the JDBC driver.  
  
 A typical permission in the policy file looks like the following.  
  
```  
// Example policy file entry.  
grant [signedBy <signer>,] [codeBase <code source>] {  
   permission  <class>  [<name> [, <action list>]];  
};  
```  
  
 The following codebase should be restricted to the JDBC driver codebase to ensure that you grant the least amount of privileges.  
  
```  
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
>  The code "file:/install_dir/lib/-" refers to the installation directory of the JDBC driver.  
  
## Protecting server communication  
 When you use the JDBC driver to communicate with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, you can secure the communication channel by using either Internet Protocol Security (IPSEC) or Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL); or you can use both.  
  
 TLS support can be used to provide an additional level of protection besides IPSEC. For more information about using TLS, see [Using encryption](../../connect/jdbc/using-ssl-encryption.md).  
  
## See also  
 [Securing JDBC driver applications](../../connect/jdbc/securing-jdbc-driver-applications.md)  
  
  
