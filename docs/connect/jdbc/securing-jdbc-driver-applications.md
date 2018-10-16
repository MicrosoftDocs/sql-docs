---
title: "Securing JDBC Driver Applications | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 90724ec6-a9cb-43ef-903e-793f89410bc0
author: MightyPen
ms.author: genemi
manager: craigg
---
# Securing JDBC Driver Applications

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Enhancing the security of a [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] application involves more than avoiding common coding pitfalls. An application that accesses data has many potential points of failure that an attacker can exploit to retrieve, manipulate, or destroy sensitive data. It is important to understand all aspects of security, from the process of threat modeling during the design phase of your application to its eventual deployment, and continuing through its ongoing maintenance.  
  
The topics in this section describe some common security concerns including connection strings, validating user input, and general application security.  
  
## In This Section  
  
| Topic                                                                            | Description                                                                                                                                                           |
| -------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Securing Connection Strings](../../connect/jdbc/securing-connection-strings.md) | Describes techniques to help protect information used to connect to a data source.                                                                                    |
| [Validating User Input](../../connect/jdbc/validating-user-input.md)             | Describes techniques to validate user input.                                                                                                                          |
| [Application Security](../../connect/jdbc/application-security.md)               | Describes how to use Java policy permissions to help secure a JDBC driver application.                                                                                |
| [Using SSL Encryption](../../connect/jdbc/using-ssl-encryption.md)               | Describes how to establish a secure communication channel with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database using Secure Sockets Layer (SSL). |
| [FIPS Mode](../../connect/jdbc/fips-mode.md)                                     | Describes how to use JDBC driver in FIPS compliant mode.                                                                                                              |
  
## See Also  

 [Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
