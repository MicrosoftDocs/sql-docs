---
title: "System Requirements for the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "02/06/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 447792bb-f39b-49b4-9fd0-1ef4154c74ab
author: MightyPen
ms.author: genemi
manager: craigg
---
# System Requirements for the JDBC Driver
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  To access data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] by using the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], you must have the following components installed on your computer:

- [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] ([download](download-microsoft-jdbc-driver-for-sql-server.md))
- Java Runtime Environment

## Java Runtime Environment Requirements  

 Starting with the Microsoft JDBC Driver 7.2 for SQL Server, Java Development Kit (JDK) 11.0 and Java Runtime Environment (JRE) 11.0 are supported.
 
 Starting with the Microsoft JDBC Driver 7.0 for SQL Server, Java Development Kit (JDK) 10.0 and Java Runtime Environment (JRE) 10.0 are supported.

 Starting with the Microsoft JDBC Driver 6.4 for SQL Server, Java Development Kit (JDK) 9.0 and Java Runtime Environment (JRE) 9.0 are supported.

 Starting with the Microsoft JDBC Driver 4.2 for SQL Server, Java Development Kit (JDK) 8.0 and Java Runtime Environment (JRE) 8.0 are supported. Support for Java Database Connectivity (JDBC) Spec API has been extended to include the JDBC 4.1 and 4.2 API.
  
 Starting with the Microsoft JDBC Driver 4.1 for SQL Server, Java Development Kit (JDK) 7.0 and Java Runtime Environment (JRE) 7.0 are supported.
  
 Starting with the [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)], the JDBC driver support for Java Database Connectivity (JDBC) Spec API has been extended to include the JDBC 4.0 API. The JDBC 4.0 API was introduced as part of the Java Development Kit (JDK) 6.0 and Java Runtime Environment (JRE) 6.0. JDBC 4.0 is a superset of the JDBC 3.0 API.
  
 When you deploy the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] on Windows and UNIX operating systems, you must use the installation packages, *sqljdbc_\<version>_enu.exe*, and *sqljdbc_\<version>_enu.tar.gz*, respectively. For more information about how to deploy the JDBC Driver, see [Deploying the JDBC Driver](../../connect/jdbc/deploying-the-jdbc-driver.md) topic.  

**Microsoft JDBC Driver 7.2 for SQL Server:**  

  The JDBC Driver 7.2 includes two JAR class libraries in each installation package: **mssql-jdbc-7.2.2.jre8.jar**, and **mssql-jdbc-7.2.2.jre11.jar**.

  The JDBC Driver 7.2 is designed to work with and be supported by all major Java virtual machines, but is tested only on OpenJDK 8.0, OpenJDK 11.0, Azul Zulu JRE 8.0 and Azul Zulu JRE 11.0.
  
  The following summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 7.2 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|   
|mssql-jdbc-7.2.2.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 8.0. Using JRE 7.0 or lower throws an exception.<br /><br /> New Features in 7.2 include: JDK 11 support, Active Directory Managed Service Identity (MSI) authentication, OSGi support, SQLServerError APIs. |    
|mssql-jdbc-7.2.2.jre11.jar|4.3|10|Requires a Java Runtime Environment (JRE) 11.0. Using JRE 10.0 or lower throws an exception.<br /><br /> New Features in 7.2 include: JDK 11 support, Active Directory Managed Service Identity (MSI) authentication, OSGi support, SQLServerError APIs. |    


  The JDBC Driver 7.2 is also available on the Maven Central Repository and can be added to a Maven project by adding the following code in the POM.XML:  
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>7.2.2.jre11</version>
</dependency>
```
 
**Microsoft JDBC Driver 7.0 for SQL Server:**  

  The JDBC Driver 7.0 includes two JAR class libraries in each installation package: **mssql-jdbc-7.0.0.jre8.jar**, and **mssql-jdbc-7.0.0.jre10.jar**.

  The JDBC Driver 7.0 is designed to work with and be supported by all major Java virtual machines, but is tested only on OpenJDK 8.0, and 10.0.
  
  The following summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 7.0 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|   
|mssql-jdbc-7.0.0.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 8.0. Using JRE 7.0 or lower throws an exception.<br /><br /> New Features in 7.0 include: JDK 10 support, updated default compliance level to JDBC 4.2 specifications, Spatial Datatypes support, cancelQueryTimeout connection property, Request Boundary methods, useBulkCopyForBatchInsert connection property, Data Discovery and Classification information, UTF-8 feature extension, and CityHash support. |    
|mssql-jdbc-7.0.0.jre10.jar|4.3|10|Requires a Java Runtime Environment (JRE) 10.0. Using JRE 9.0 or lower throws an exception.<br /><br /> New Features in 7.0 include: JDK 10 support, updated default compliance level to JDBC 4.2 specifications, Spatial Datatypes support, cancelQueryTimeout connection property, Request Boundary methods, useBulkCopyForBatchInsert connection property, Data Discovery and Classification information, UTF-8 feature extension, and CityHash support. |    


  The JDBC Driver 7.0 is also available on the Maven Central Repository and can be added to a Maven project by adding the following code in the POM.XML:  
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>7.0.0.jre10</version>
</dependency>
```
  
**Microsoft JDBC Driver 6.4 for SQL Server:**  

  The JDBC Driver 6.4 includes three JAR class libraries in each installation package: **mssql-jdbc-6.4.0.jre7.jar**, **mssql-jdbc-6.4.0.jre8.jar**, and **mssql-jdbc-6.4.0.jre9.jar**.

  The JDBC Driver 6.4 is designed to work with and be supported by all major Java virtual machines, but is tested only on OpenJDK 7.0, 8.0, and 9.0.
  
  The following summarizes support provided by the three JAR files included with Microsoft JDBC Drivers 6.4 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|   
|mssql-jdbc-6.4.0.jre7.jar|4.1|7|Requires a Java Runtime Environment (JRE) 7.0. Using JRE 6.0 or lower throws an exception.<br /><br /> New Features in 6.4 include: Azure AD authentication for Linux, Principal/Password method for Kerberos, automatic detection of REALM in SPN for Cross-Domain authentication, Kerberos Constrained Delegation, Query Timeout, Socket Timeout, and prepared statement handle re-use. |  
|mssql-jdbc-6.4.0.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 8.0. Using JRE 7.0 or lower throws an exception.<br /><br /> New Features in 6.4 include: Azure AD authentication for Linux, Principal/Password method for Kerberos, automatic detection of REALM in SPN for Cross-Domain authentication, Kerberos Constrained Delegation, Query Timeout, Socket Timeout, and prepared statement handle re-use. |    
|mssql-jdbc-6.4.0.jre9.jar|4.3|9|Requires a Java Runtime Environment (JRE) 9.0. Using JRE 8.0 or lower throws an exception.<br /><br /> New Features in 6.4 include: Azure AD authentication for Linux, Principal/Password method for Kerberos, automatic detection of REALM in SPN for Cross-Domain authentication, Kerberos Constrained Delegation, Query Timeout, Socket Timeout, and prepared statement handle re-use. |

The JDBC Driver 6.4 is also available on the Maven Central Repository and can be added to a Maven project by adding the following code in the POM.XML 

 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>6.4.0.jre9</version>
</dependency>
```

**Microsoft JDBC Driver 6.2 for SQL Server:**  
  
  The JDBC Driver 6.2 includes two JAR class libraries in each installation package: **mssql-jdbc-6.2.2.jre7.jar**, and **mssql-jdbc-6.2.2.jre8.jar**. 
  
 The JDBC Driver 6.2 is designed to work with and be supported by all major Java virtual machines, but is tested only on Sun JRE 5.0, 6.0, 7.0, and 8.0.
  
 The following summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 6.0 and 4.2 for SQL Server:  
  
|JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|
|mssql-jdbc-6.2.2.jre7.jar|4.1|7|Requires a Java Runtime Environment (JRE) 7.0. Using JRE 6.0 or lower throws an exception.<br /><br /> New Features in 6.2 include: Azure AD authentication for Linux, Principal/Password method for Kerberos, automatic detection of REALM in SPN for Cross-Domain authentication, Kerberos Constrained Delegation, Query Timeout, Socket Timeout, and prepared statement handle re-use. |  
|mssql-jdbc-6.2.3.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 8.0. Using JRE 7.0 or lower throws an exception.<br /><br /> New Features in 6.2 include: Azure AD authentication for Linux, Principal/Password method for Kerberos, automatic detection of REALM in SPN for Cross-Domain authentication, Kerberos Constrained Delegation, Query Timeout, Socket Timeout, and prepared statement handle re-use|    

  The JDBC Driver 6.2 is also available on the Maven Central Repository and can be added to a Maven project by adding the following code in the POM.XML 
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>6.2.2.jre8</version>
</dependency>
```    

 **Microsoft JDBC Driver 6.0 and 4.2 for SQL Server:**  
  
  The JDBC Drivers 6.0 and 4.2 include two JAR class libraries in each installation package: **sqljdbc41.jar**, and **sqljdbc42.jar**. 
  
 The JDBC Drivers 6.0 and 4.2 are designed to work with and be supported by all major Java virtual machines, but is tested only on Sun JRE 5.0, 6.0, 7.0, and 8.0.
  
 The following summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 6.0 and 4.2 for SQL Server:  
  
|JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|   
|sqljdbc41.jar|4.1|7|Requires a Java Runtime Environment (JRE) 7.0. Using JRE 6.0 or lower throws an exception.<br /><br /> New Features in 6.0 & 4.2 packages include: JDBC 4.1 Compliance and Bulk Copy<br /><br /> In Addition, new Features in only the 6.0 package include: Always Encrypted, Table-Valued Parameters, Azure Active Directory Authentication, transparent connections to Always On Availability Groups, improvement in parameter metadata retrieval  for prepared queries and Internationalized Domain Name (IDN)|  
|sqljdbc42.jar|4.2|8|Requires a Java Runtime Environment (JRE) 8.0. Using JRE 7.0 or lower throws an exception.<br /><br /> New Features in 6.0 & 4.2 packages include: JDBC 4.1 Compliance, JDBC 4.2 Compliance, and Bulk Copy<br /><br /> In Addition, new Features in only the 6.0 package include: Always Encrypted, Table-Valued Parameters, Azure Active Directory Authentication, transparent connections to Always On Availability Groups, improvement in parameter metadata retrieval  for prepared queries and Internationalized Domain Name (IDN)|  
  
 **Microsoft JDBC Driver 4.1 for SQL Server:**  
  
 The JDBC Driver 4.1 includes one JAR class library in each installation package: **sqljdbc41.jar**.  
    
|JAR|Description|  
|---------|-----------------|  
|sqljdbc41.jar|**sqljdbc41.jar** class library provides support for JDBC 4.0 API. It includes all of the features of the JDBC 4.0 driver as well as the JDBC 4.0 API methods. JDBC 4.1 is not supported (throws an exception "SQLFeatureNotSupportedException").<br /><br /> **sqljdbc41.jar** class library requires a Java Runtime Environment (JRE) 7.0. Using **sqljdbc41.jar** on JRE 6.0 and 5.0 throws an exception.<br /><br /> 
  
 The JDBC driver is designed to work with and be supported by all major Java virtual machines, but is tested on Sun JRE 5.0, 6.0 and 7.0.
  
 The following summarizes support provided by the JAR file included with Microsoft JDBC Driver 4.1 for SQL Server.  
  
|JAR|JDBC Version|JRE (can run)|JDK (can compile)|  
|---------|------------------|---------------------|-------------------------|   
|sqljdbc41.jar|4|7|7 6 5|  
  
## SQL Server Requirements  
 The JDBC driver supports connections to Azure SQL database and SQL Server. For Microsoft JDBC Driver 4.2 and 4.1 for SQL Server, support begins with SQL Server 2008.
  
## Operating System Requirements  
 The JDBC driver is designed to work on any operating system that supports the use of a Java Virtual Machine (JVM). However, only Sun Solaris, SUSE Linux, and Windows operating systems have officially been tested.  
  
## Supported Languages  
 The JDBC driver supports all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] column collations. For more information about the collations supported by the JDBC driver, see [International Features of the JDBC Driver](../../connect/jdbc/international-features-of-the-jdbc-driver.md).  
  
 For more information about collations, see "Working with Collations" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
  
  
