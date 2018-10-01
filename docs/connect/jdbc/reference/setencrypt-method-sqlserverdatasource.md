---
title: "setEncrypt Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "setEncrypt Method (SQLServerDataSource)"
apilocation: 
  - "setEncrypt Method (SQLServerDataSource)"
apitype: "Assembly"
ms.assetid: 0c85a9c1-f27c-457e-8461-403cc03e2d17
author: MightyPen
ms.author: genemi
manager: craigg
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
  
 **true** if the Secure Sockets Layer (SSL) encryption is enabled between the client and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Otherwise, **false**.  
  
## Remarks  
 If the encrypt property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] ensures that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses SSL encryption for all data sent between the client and server if the server has a certificate installed. The default value is **false**.  
  
 The JDBC driver detects the Java Virtual Machine (JVM) it is running on when trying to establish an SSL handshake.  
  
 If the encrypt property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] uses the JVM's default JSSE security provider to negotiate SSL encryption with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The default security provider may not support all of the features required to negotiate SSL encryption successfully. For example, the default security provider may not support the size of the RSA public key used in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] SSL certificate. In this case, the default security provider might raise an error that will cause the JDBC driver to terminate the connection. In order to resolve this issue, do one of the following:  
  
-   Configure the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with a server certificate that has a smaller RSA public key  
  
-   Configure the JVM to use a different JSSE security provider in the "\<java-home>/lib/security/java.security" security properties file  
  
-   Use a different JVM  
  
 If the encrypt property is unspecified or set to **false**, the driver will not enforce the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to support SSL encryption. If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance is not configured to force the SSL encryption, a connection is established without any encryption. If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance is configured to force the SSL encryption, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will automatically enable SSL encryption when running on properly configured JVM, or else the connection is terminated and the driver will raise an error.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
