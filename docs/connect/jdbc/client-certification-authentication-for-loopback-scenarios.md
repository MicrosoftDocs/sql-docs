---
title: Client Certificate Authentication for loopback scenarios
description: Learn how to use client certificates for authentication in loopback scenarios on SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: 07/31/2020
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Client Certificate Authentication for Loopback Scenarios

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

A new stored procedure called sp_execute_external_script (SPEES) was added in SQL Server 2016. This stored procedure allows SQL Server to launch and execute an external script outside of the SQL Server, as part of an extensibility effort. With it came the support for R and Python scripts, both of which has libraries that can use a JDBC driver to connect to the SQL Server. While SQL Servers on Windows box can use Windows Integrated Authentication to authenticate these loopback connections with the same credentials as the user who started the query, Linux SQL Server cannot do the same. Therefore, client certificate authentication is being added to allow users to authenticate with a certificate and key.

## Connecting using Client Certificate Authentication

The JDBC driver adds three connection properties for this feature:

* clientCertificate – specifies the certificate to be used for authentication. The JDBC driver will support PFX, PEM, DER, and CER file extensions.

  Format

  ```java
  clientCertificate=<file_location>
  ```

The driver uses a certificate file. For certificates in PEM, DER, and CER formats clientKey attribute is required. File location can be either relative or absolute.

* clientKey – specifies a file location of the private key for PEM, DER, and CER certificates specified by the clientCertificate attribute.

  Format

  ```java
  clientKey=<file_location>
  ```

Specifies location of the private key file. In case if private key file is password protected then password keyword is required. File location can be either relative or absolute.

* clientKeyPassword – optional password string provided to access the clientKey file's private key.

  This feature is only officially supported for loopback authentication scenarios against Linux SQL Server 2019 and up.

## See also

[Connecting to SQL Server with the JDBC driver](../../connect/jdbc/connecting-to-sql-server-with-the-jdbc-driver.md)  
[sp_execute_external_script (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md)
