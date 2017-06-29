---
title: "Frequently Asked Questions (FAQ) for JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: cbc0e397-ecf2-4494-87b2-a492609bceae
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Frequently Asked Questions (FAQ) for JDBC Driver
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  This article provides answers to frequently asked questions about the Microsoft JDBC Driver for SQL Server.  
  
## Frequently Asked Questions  
 Which versions of SQL Server and Java does the driver support?  
 See the [Microsoft JDBC Driver for SQL Server Support Matrix](../../connect/jdbc/microsoft-jdbc-driver-for-sql-server-support-matrix.md) page for details.  
  
 What should I know when upgrading my driver?  
 The Microsoft JDBC Driver 6.2 supports both JDBC 4.1 and JDBC 4.2 specifications and include two JAR class libraries in the installation package as follows:  
  
||||  
|-|-|-|  
|mssql-jdbc-6.2.0.jre8.jar|JDBC 4.2, 4.1, and 4.0|JDK 8.0 and 7.0|  
|mssql-jdbc-6.2.0.jre7.jar|JDBC 4.1 and 4.0|JDK 7.0 and 6.0|  
 
 The Microsoft JDBC Drivers 6.0 and 4.2 for SQL Server support both JDBC 4.1 and JDBC 4.2 specifications and include two JAR class libraries in the installation package as follows:  
  
||||  
|-|-|-|  
|sqljdbc42.jar|JDBC 4.2, 4.1, and 4.0|JDK 8.0 and 7.0|  
|sqljdbc41.jar|JDBC 4.1 and 4.0|JDK 7.0 and 6.0 |  
  
 The Microsoft JDBC Driver 4.1 for SQL Server supports the JDBC 4.0 specification and includes one JAR class library in the installation package as follows:  
  
||||  
|-|-|-|  
|sqljdbc41.jar|JDBC 4.0|JDK 7.0, 6.0 and 5.0|
  
 The Microsoft JDBC Driver 4.0 for SQL Server supports both JDBC 3.0 and JDBC 4.0 specifications and includes two JAR class libraries in the installation package for each specification: sqljdbc.jar and sqljdbc4.jar, respectively.  
  
||||  
|-|-|-|  
|sqljdbc4.jar|JDBC 4.0|JDK 6.0 and 5.0|  
|sqljdbc.jar|JDBC 3.0|JDK 6.0 and 5.0|  
  
 Do I need to make any code changes in my application to use the latest driver with my existing SQL Server version?  
 In general, we design the driver to be backward compatible so that you do not need to change your existing applications when upgrading the driver. In the event that a new driver version introduces a breaking change, the  [Release Notes for the JDBC Driver](../../connect/jdbc/release-notes-for-the-jdbc-driver.md) section will provide clear details on the change and the impact to existing applications. In addition, you can review the release notes included with the driver for a list of bugs fixed in that release and known issues.  
  
 How much does the driver cost?  
 The Microsoft JDBC Driver for SQL Server is available at no additional charge.  
  
 Can I redistribute the driver?  
 The driver is freely redistributable under a separate Redistribution License that requires registration. To register or for more information, see our  [Redistributing the Microsoft JDBC Driver](../../connect/jdbc/redistributing-the-microsoft-jdbc-driver.md).  
  
 Can I use the driver to access Microsoft SQL Server from a Linux computer?  
 Yes! You can use the driver to access SQL Server from Linux, Unix, and other non-Windows platforms. See our  [Microsoft JDBC Driver for SQL Server Support Matrix](../../connect/jdbc/microsoft-jdbc-driver-for-sql-server-support-matrix.md) for more details.  
  
 Does the driver support Secure Sockets Layer (SSL) encryption?  
 Starting with version 1.2, the driver supports Secure Sockets Layer (SSL) encryption. For more information, see  [Using SSL Encryption](../../connect/jdbc/using-ssl-encryption.md).  
  
 Which authentication types are supported by the Microsoft JDBC Driver for SQL Server?  
 The table below lists available authentication options. Note that pure Java Kerberos authentication is available starting with the 4.0 release of the driver.  
  
|||  
|-|-|  
|Platform|Authentication|  
|Non-Windows|Pure Java Kerberos|  
|Non-Windows|SQL Server|  
|Windows|Pure Java Kerberos|  
|Windows|SQL Server|  
|Windows|Kerberos with NTLM backup|  
|Windows|NTLM|  
  
 Does the driver support Internet Protocol version 6 (IPv6) addresses?  
 Yes, the driver supports the use of IPv6 addresses with the connection properties collection and with the serverName connection string property. For more information, see [Building the Connection URL](../../connect/jdbc/building-the-connection-url.md).  
  
 What is adaptive buffering?  
 Adaptive buffering is introduced starting with Microsoft SQL Server 2005 JDBC Driver version 1.2, and is designed to retrieve any kind of large-value data without the overhead of server cursors. The adaptive buffering feature of the Microsoft SQL Server JDBC Driver provides a connection string property, responseBuffering, which can be set to "adaptive" or "full". Starting with the JDBC Driver version 2.0, the default behavior of the driver is "adaptive". In other words, in order to get the adaptive buffering behavior, your application does not have to request the adaptive behavior explicitly. In the version 1.2 release, however, the buffering mode was "full" by default and the application had to request the adaptive buffering mode explicitly. For more information, see [Using Adaptive Buffering](../../connect/jdbc/using-adaptive-buffering.md) topic and the blog [What is adaptiveresponse buffering and why should I use it?](http://go.microsoft.com/fwlink/?LinkId=111575).  
  
 Does the driver support connection pooling?  
 The driver provides support for Java Platform, Enterprise Edition 5 (Java EE 5) connection pooling. The driver implements the JDBC 3.0 required interfaces to enable the driver to participate in any connection-pooling implementation that is provided by middleware application server vendors. The driver participates in pooled connections in these environments. For more information, see [Using Connection Pooling](../../connect/jdbc/using-connection-pooling.md). The driver does not provide its own pooling implementation, but rather it relies on third-party Java application servers.  
  
 Is support available for the driver?  
 Several support options are available. You may post your question to our [forums](http://go.microsoft.com/fwlink/?LinkID=246673) which are monitored by Microsoft, MVP’s, and the community. You may also contact Microsoft Customer Support. We may ask you to reproduce the issue outside any third-party application servers. If the issue cannot be reproduced outside the hosting Java container environment, you will need to involve the related third-party so that we can continue to assist you. We may also ask you to reproduce your issue on an operating system such as Windows so we can best assist you.  
  
 Is the driver certified for use with any third-party application servers?  
 The driver has been tested against various application servers including IBM WebSphere and SAP Netweaver.  
  
 How do I enable tracing?  
 The driver supports the use of tracing (or logging) to help resolve issues and problems with the JDBC Driver when it is used in your application. To enable the use of client-side JAR tracing, the JDBC Driver uses the logging APIs in java.util.logging. For more information, see  [Tracing Driver Operation](../../connect/jdbc/tracing-driver-operation.md). For server-side XA tracing, see [Data Access Tracing in SQL Server](http://go.microsoft.com/fwlink/?LinkId=248705).  
  
 Where can I download older versions of the driver such as the SQL Server 2000 JDBC driver, 2005 driver, 1.0, 1.1, or 1.2 driver?  
 These driver versions are not available for download as they are no longer supported. We are continually improving our Java connectivity support. As such we highly recommend you work with the latest version of our JDBC driver.  
  
 I am using JRE 1.4. Which driver is compatible with JRE 1.4?  
 For customers who are using SAP products and require JRE 1.4 support, you may contact [SAPService Marketplace](http://service.sap.com/) to obtain the 1.2 Microsoft JDBC driver.  
  
 Can the driver communicate using FIPS validated algorithms?  
 The Microsoft JDBC Driver does not contain any cryptographic algorithms. If a customer leverages operating system, application, and JVM algorithms that are deemed acceptable by Federal Information Processing Standards (FIPS) and configures the driver to use those algorithms then the driver will use only the designated algorithms for communication.  
  
  
