---
title: "Frequently Asked Questions (FAQ) for JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "02/06/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: cbc0e397-ecf2-4494-87b2-a492609bceae
author: MightyPen
ms.author: genemi
manager: craigg
---

# Frequently Asked Questions (FAQ) for JDBC Driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This page provides answers to frequently asked questions about the Microsoft JDBC Driver for SQL Server.

## Frequently Asked Questions

**How can I help improve the JDBC Driver?**  
The JDBC Driver is open-source and the source code can be found on [GitHub](https://github.com/microsoft/mssql-jdbc). You can help improve the driver by filing issues and contributing to the code base.

**Which versions of SQL Server and Java do the driver support?**  
See the [Microsoft JDBC Driver for SQL Server Support Matrix](../../connect/jdbc/microsoft-jdbc-driver-for-sql-server-support-matrix.md) page for details.

**What is the difference between the JDBC driver packages available on the Microsoft Download Center and the JDBC driver available on GitHub?**  
The JDBC driver files available on the GitHub repository for the Microsoft JDBC driver are the core of the JDBC driver and are under the open-source license listed in the repository. The driver packages on the Microsoft Download Center include additional libraries for Windows-integrated authentication and enabling XA transactions with the JDBC driver. Those additional libraries are under the license included with the downloadable package.

**What should I know when upgrading my driver?**
 The Microsoft JDBC Driver 7.2 supports the JDBC 4.2, and 4.3 (partially) specifications and includes two JAR class libraries in the installation package as follows:

| JAR                        | JDBC Specification            | JDK Version |
| -------------------------- | ----------------------------- | ----------- |
| mssql-jdbc-7.2.1.jre11.jar | JDBC 4.3 (partially), and 4.2 | JDK 11.0    |
| mssql-jdbc-7.2.1.jre8.jar  | JDBC 4.2                      | JDK 8.0     |

 The Microsoft JDBC Driver 7.0 supports the JDBC 4.2, and 4.3 (partially) specifications and includes two JAR class libraries in the installation package as follows:

| JAR                        | JDBC Specification            | JDK Version |
| -------------------------- | ----------------------------- | ----------- |
| mssql-jdbc-7.0.0.jre10.jar | JDBC 4.3 (partially), and 4.2 | JDK 10.0    |
| mssql-jdbc-7.0.0.jre8.jar  | JDBC 4.2                      | JDK 8.0     |

The Microsoft JDBC Driver 6.4 supports the JDBC 4.1, 4.2, and 4.3 (partially) specifications and includes three JAR class libraries in the installation package as follows:

| JAR                       | JDBC Specification                 | JDK Version |
| ------------------------- | ---------------------------------- | ----------- |
| mssql-jdbc-6.4.0.jre9.jar | JDBC 4.3 (partially), 4.2, and 4.1 | JDK 9.0     |
| mssql-jdbc-6.4.0.jre8.jar | JDBC 4.2, and 4.1                  | JDK 8.0     |
| mssql-jdbc-6.4.0.jre7.jar | JDBC 4.1                           | JDK 7.0     |

The Microsoft JDBC Driver 6.2 supports the JDBC 4.0, 4.1, and 4.2 specifications and includes two JAR class libraries in the installation package as follows:

| JAR                       | JDBC Specification     | JDK Version |
| ------------------------- | ---------------------- | ----------- |
| mssql-jdbc-6.2.2.jre8.jar | JDBC 4.2, 4.1, and 4.0 | JDK 8.0     |
| mssql-jdbc-6.2.2.jre7.jar | JDBC 4.1 and 4.0       | JDK 7.0     |

The Microsoft JDBC Drivers 6.0 and 4.2 for SQL Server supports JDBC 4.0, 4.1, and 4.2 specifications and include two JAR class libraries in the installation package as follows:

| JAR           | JDBC Specification     | JDK Version |
| ------------- | ---------------------- | ----------- |
| sqljdbc42.jar | JDBC 4.2, 4.1, and 4.0 | JDK 8.0     |
| sqljdbc41.jar | JDBC 4.1 and 4.0       | JDK 7.0     |

The Microsoft JDBC Driver 4.1 for SQL Server supports the JDBC 4.0 specification and includes one JAR class library in the installation package as follows:

| JAR           | JDBC Specification | JDK Version     |
| ------------- | ------------------ | --------------- |
| sqljdbc41.jar | JDBC 4.0           | JDK 7.0 and 6.0 |

**Do I need to make any code changes in my application to use the latest driver with my existing SQL Server version?**  
In general, the driver is designed to be backward compatible so that you do not need to change your existing applications when upgrading the driver. In the event that a new driver version introduces a breaking change, the [Release Notes for the JDBC Driver](../../connect/jdbc/release-notes-for-the-jdbc-driver.md) section provides clear details on the change and the impact to existing applications. In addition, you can review the release notes included with the driver for a list of bugs fixed in that release and known issues.

**How much does the driver cost?**  
The Microsoft JDBC Driver for SQL Server is available at no additional charge.

**Can I redistribute the driver?**
The JDBC Drivers 4.1, 4.2, 6.0, 6.2, 6.4, and 7.0 are redistributable. Review the "Distributable Code" clause in the license agreements.

**Can I use the driver to access Microsoft SQL Server from a Linux computer?**
Yes! You can use the driver to access SQL Server from Linux, Unix, and other non-Windows platforms. For more information, see [Microsoft JDBC Driver for SQL Server Support Matrix](../../connect/jdbc/microsoft-jdbc-driver-for-sql-server-support-matrix.md).

**Does the driver support Secure Sockets Layer (SSL) encryption?**
Starting with version 1.2, the driver supports Secure Sockets Layer (SSL) encryption. For more information, see [Using SSL Encryption](../../connect/jdbc/using-ssl-encryption.md).

**Which authentication types are supported by the Microsoft JDBC Driver for SQL Server?**  
The table below lists available authentication options. A pure Java Kerberos authentication is available starting with the 4.0 release of the driver.

|             |                                       |
| ----------- | ------------------------------------- |
| Platform    | Authentication                        |
| Non-Windows | Pure Java Kerberos                    |
| Non-Windows | SQL Server                            |
| Non-Windows | Azure Active Directory Authentication |
| Windows     | Pure Java Kerberos                    |
| Windows     | SQL Server                            |
| Windows     | Kerberos with NTLM backup             |
| Windows     | NTLM                                  |
| Windows     | Azure Active Directory Authentication |

**Does the driver support Internet Protocol version 6 (IPv6) addresses?**  
Yes. The driver supports the use of IPv6 addresses. Use the connection properties collection and the serverName connection string property. For more information, see [Building the Connection URL](../../connect/jdbc/building-the-connection-url.md).

**What is adaptive buffering?**  
Adaptive buffering is introduced starting with Microsoft SQL Server 2005 JDBC Driver version 1.2. It is designed to retrieve any kind of large-value data without the overhead of server cursors. The adaptive buffering feature of the Microsoft SQL Server JDBC Driver provides a connection string property, responseBuffering, which can be set to "adaptive" or "full". In the version 1.2 release, the buffering mode is "full" by default and the application must set the adaptive buffering mode explicitly. Starting with the JDBC Driver version 2.0, the default behavior of the driver is "adaptive". Thus, your application does not have to request the adaptive behavior explicitly to get the adaptive buffering behavior. For more information, see [Using Adaptive Buffering](../../connect/jdbc/using-adaptive-buffering.md) and the blog [What is adaptive response buffering and why should I use it?](https://go.microsoft.com/fwlink/?LinkId=111575).

**Does the driver support connection pooling?**  
The driver provides support for Java Platform, Enterprise Edition 5 (Java EE 5) connection pooling. The driver implements the JDBC 3.0 required interfaces to enable the driver to participate in any connection-pooling implementation that is provided by middleware application server vendors. The driver participates in pooled connections in these environments. For more information, see [Using Connection Pooling](../../connect/jdbc/using-connection-pooling.md). The driver does not provide its own pooling implementation, but rather it relies on third-party Java application servers.

**Is support available for the driver?**  
Several support options are available. You may post your question or issue to our [GitHub repository](https://github.com/microsoft/mssql-jdbc) which is monitored by Microsoft. [Forums](https://go.microsoft.com/fwlink/?LinkID=246673) are monitored by Microsoft, MVPs, and the community. You may also contact Microsoft Customer Support. The development team may ask you to reproduce the issue outside any third-party application servers. If the issue cannot be reproduced outside the hosting Java container environment, you will need to involve the related third-party so that the team can continue to assist you. The team may also ask you to reproduce your issue on an operating system such as Windows so the problem can be best supported.

**Is the driver certified for use with any third-party application servers?**
The driver has been tested against various application servers including IBM WebSphere and SAP Netweaver.

**How do I enable tracing?**  
The driver supports the use of tracing (or logging) to help resolve issues and problems with the JDBC Driver when it is used in your application. To enable the use of client-side JAR tracing, the JDBC Driver uses the logging APIs in java.util.logging. For more information, see [Tracing Driver Operation](../../connect/jdbc/tracing-driver-operation.md). For server-side XA tracing, see [Data Access Tracing in SQL Server](https://go.microsoft.com/fwlink/?LinkId=248705).

**Where can I download older versions of the driver such as the SQL Server 2000 JDBC driver, 2005 driver, 1.0, 1.1, or 1.2 driver?**  
These driver versions are not available for download as they are no longer supported. We are continually improving the Java connectivity support. As such, we highly recommend you work with the latest version of Microsoft JDBC driver.

**I am using JRE 1.4. Which driver is compatible with JRE 1.4?**  
For customers who are using SAP products and require JRE 1.4 support, you may contact [SAPService Marketplace](https://service.sap.com/) to obtain the 1.2 Microsoft JDBC driver.

**Can the driver communicate using FIPS validated algorithms?**  
The Microsoft JDBC Driver does not contain any cryptographic algorithms. If a customer leverages operating system, application, and JVM algorithms that are deemed acceptable by Federal Information Processing Standards (FIPS) and configures the driver to use those algorithms then the driver uses only the designated algorithms for communication.

## See Also

[Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)
