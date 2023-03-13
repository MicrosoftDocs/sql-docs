---
title: Securing applications
description: These articles describe some common security concerns including connection strings, validating user input, and general application security.
author: David-Engel
ms.author: v-davidengel
ms.date: 03/31/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Securing JDBC driver applications

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Enhancing the security of a [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] application is crucial. Security involves more than avoiding common coding pitfalls. An application that accesses data has many potential failure points that an attacker can exploit. Security failures may allow attackers to retrieve, manipulate, or destroy sensitive data. It's important to understand all aspects of application security. From the process of threat modeling during the design phase to eventual deployment, and continuing through ongoing maintenance.

The articles in this section describe some common security concerns including connection strings, validating user input, and general application security.

## In this section

| Article | Description |
| ------- | ----------- |
| [Securing connection strings](securing-connection-strings.md) | Describes techniques to help protect information used to connect to a data source. |
| [Validating user input](validating-user-input.md) | Describes techniques to validate user input. |
| [Application security](application-security.md) | Describes how to use Java policy permissions to help secure a JDBC driver application. |
| [Using encryption](using-ssl-encryption.md) | Describes how to establish a secure communication channel with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database using Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL). |
| [FIPS mode](fips-mode.md) | Describes how to use JDBC driver in FIPS-compliant mode. |
  
## See also

[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)
