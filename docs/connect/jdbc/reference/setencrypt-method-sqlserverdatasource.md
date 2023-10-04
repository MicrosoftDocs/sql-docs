---
title: "setEncrypt Method (SQLServerDataSource)"
description: "setEncrypt Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "setEncrypt Method (SQLServerDataSource)"
apiname: "setEncrypt Method (SQLServerDataSource)"
apitype: "Assembly"
---
# setEncrypt Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets a **Boolean** value that indicates if the encrypt property is enabled.  
  
## Syntax  
  
```  
  
public void setEncypt(boolean encrypt)  
```  
  
#### Parameters  
 *encrypt*  
  
 **true** if the Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), encryption is enabled between the client and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Otherwise, **false**.  
  
## Remarks  
 If the encrypt property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] ensures that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses TLS encryption for all data sent between the client and server if the server has a certificate installed. The default value is **false**.  
  
 The JDBC driver detects the Java Virtual Machine (JVM) it is running on when trying to establish a TLS handshake.  
  
 If the encrypt property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] uses the JVM's default JSSE security provider to negotiate TLS encryption with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The default security provider may not support all of the features required to negotiate TLS encryption successfully. For example, the default security provider may not support the size of the RSA public key used in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] TLS/SSL certificate. In this case, the default security provider might raise an error that will cause the JDBC driver to terminate the connection. In order to resolve this issue, do one of the following:  
  
-   Configure the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with a server certificate that has a smaller RSA public key  
  
-   Configure the JVM to use a different JSSE security provider in the "\<java-home>/lib/security/java.security" security properties file  
  
-   Use a different JVM  
  
 If the encrypt property is unspecified or set to **false**, the driver will not enforce the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to support TLS encryption. If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance is not configured to force the TLS encryption, a connection is established without any encryption. If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance is configured to force the TLS encryption, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will automatically enable TLS encryption when running on properly configured JVM, or else the connection is terminated and the driver will raise an error.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
