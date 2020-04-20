---
title: "Securing JDBC driver applications"
description: "The topics in this section describe some common security concerns including connection strings, validating user input, and general application security."
ms.custom: ""
ms.date: "08/12/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 90724ec6-a9cb-43ef-903e-793f89410bc0
author: David-Engel
ms.author: v-daenge
---
# Securing JDBC driver applications

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Enhancing the security of a [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] application involves more than avoiding common coding pitfalls. An application that accesses data has many potential points of failure that an attacker can exploit to retrieve, manipulate, or destroy sensitive data. It is important to understand all aspects of security, from the process of threat modeling during the design phase of your application to its eventual deployment, and continuing through its ongoing maintenance.  
  
The topics in this section describe some common security concerns including connection strings, validating user input, and general application security.  
  
## In this section  
  
| Topic                                                                            | Description                                                                                                                                                           |
| -------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Securing Connection Strings](../../connect/jdbc/securing-connection-strings.md) | Describes techniques to help protect information used to connect to a data source.                                                                                    |
| [Validating User Input](../../connect/jdbc/validating-user-input.md)             | Describes techniques to validate user input.                                                                                                                          |
| [Application Security](../../connect/jdbc/application-security.md)               | Describes how to use Java policy permissions to help secure a JDBC driver application.                                                                                |
| [Using encryption](../../connect/jdbc/using-ssl-encryption.md)               | Describes how to establish a secure communication channel with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database using Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL). |
| [FIPS Mode](../../connect/jdbc/fips-mode.md)                                     | Describes how to use JDBC driver in FIPS compliant mode.                                                                                                              |
  
## See also  

 [Overview of the JDBC driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
