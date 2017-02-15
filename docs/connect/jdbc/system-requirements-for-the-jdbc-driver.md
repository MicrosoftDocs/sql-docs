---
title: "System Requirements for the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 447792bb-f39b-49b4-9fd0-1ef4154c74ab
caps.latest.revision: 73
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# System Requirements for the JDBC Driver
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  To access data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] by using the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], you must have the following components installed on your computer:  
  
-   [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)]  
  
     You can download the Microsoft JDBC Drivers 6.0, 4.2, 4.1, and 4.0 for SQL Server from [Microsoft Download Center](http://www.microsoft.com/download/details.aspx?id=11774).  
  
-   Java Runtime Environment  
  
## Java Runtime Environment Requirements  
 Starting with the Microsoft JDBC Driver 4.2 for SQL Server, Sun Java SE Development Kit (JDK) 8.0 and Java Runtime Environment (JRE) 8.0 are supported. Support for Java Database Connectivity (JDBC) Spec API has been extended to include the JDBC 4.1 and 4.2 API.  
  
 Starting with the Microsoft JDBC Driver 4.1 for SQL Server, Sun Java SE Development Kit (JDK) 7.0 and Java Runtime Environment (JRE) 7.0 are supported.  
  
 Starting with the [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)], the JDBC driver support for Java Database Connectivity (JDBC) Spec API has been extended to include the JDBC 4.0 API. The JDBC 4.0 API was introduced as part of the Sun Java SE Development Kit (JDK) 6.0 and Java Runtime Environment (JRE) 6.0. JDBC 4.0 is a superset of the JDBC 3.0 API.  
  
 When you deploy the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] on Windows and UNIX operating systems, you must use the installation packages, *sqljdbc_\<version>_enu.exe* and *sqljdbc_\<version>_enu.tar.gz*, respectively. For more information about how to deploy the JDBC Driver, see [Deploying the JDBC Driver](../../connect/jdbc/deploying-the-jdbc-driver.md) topic.  
  
 **Microsoft JDBC Driver 6.0 and 4.2 for SQL Server:**  
  
 To support backward compatibility and possible upgrade scenarios, the JDBC Drivers 6.0 and 4.2 include four JAR class libraries in each installation package: **sqljdbc.jar**, **sqljdbc4.jar**, **sqljdbc41.jar**, and **sqljdbc42.jar**. Note: **sqljdbc.jar**, **sqljdbc4.jar** are provided only for backwards compatibility, and do not contain new features included with driver versions 6.0, 4.2,  and 4.1.  
  
 The JDBC driver is designed to work with and be supported by all major Sun equivalent Java virtual machines, but is tested only on Sun JRE 5.0, 6.0, 7.0, and 8.0.  
  
 The following summarizes support provided by the four JAR files included with Microsoft JDBC Drivers 6.0 and 4.2 for SQL Server:  
  
|JAR|JDBC Version Compliance|Recommended Java|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|sqljdbc.jar|3.0|5|Requires a Java Runtime Environment (JRE) of version 5.0.  Using JRE 6.0 or higher will throw an exception when connecting to a database.<br /><br /> Provided for backwards compatibility, and does not contain new features included with version 4.1 and 4.2 releases.|  
|sqljdbc4.jar|4.0|6|Requires a Java Runtime Environment (JRE) of version 6.0 or 7.0. Using JRE 5.0 will throw an exception.<br /><br /> Provided for backwards compatibility, and does not contain new features included with version 4.1 and 4.2 releases.|  
|sqljdbc41.jar|4.1|7|Requires a Java Runtime Environment (JRE) 7.0. Using JRE 6.0 or lower will throw an exception.<br /><br /> New Features in 6.0 & 4.2 packages include: JDBC 4.1 Compliance and Bulk Copy<br /><br /> In Addition, new Features in only the 6.0 package include: Always Encrypted, Table-Valued Parameters, Azure Active Directory Authentication, transparent connections to Always On Availability Groups, improvement in parameter metadata retrieval  for prepared queries and Internationalized Domain Name (IDN)|  
|sqljdbc42.jar|4.2|8|Requires a Java Runtime Environment (JRE) 8.0. Using JRE 7.0 or lower will throw an exception.<br /><br /> New Features in 6.0 & 4.2 packages include: JDBC 4.1 Compliance, JDBC 4.2 Compliance, and Bulk Copy<br /><br /> In Addition, new Features in only the 6.0 package include: Always Encrypted, Table-Valued Parameters, Azure Active Directory Authentication, transparent connections to Always On Availability Groups, improvement in parameter metadata retrieval  for prepared queries and Internationalized Domain Name (IDN)|  
  
 **Microsoft JDBC Driver 4.1 for SQL Server:**  
  
 To support backward compatibility and possible upgrade scenarios, the JDBC Driver 4.1 includes three JAR class libraries in each installation package: **sqljdbc.jar**, **sqljdbc4.jar**, and **sqljdbc41.jar**.  
  
> [!NOTE]  
>  **sqljdbc.jar, sqljdbc4.jar** are provided for backwards compatibility, and do not contain new features included with the version 4.1 release.  
  
|JAR|Description|  
|---------|-----------------|  
|sqljdbc.jar|**sqljdbc.jar** class library provides support for JDBC 3.0.<br /><br /> **sqljdbc.jar** class library requires a Java Runtime Environment (JRE) of version 5.0. Using **sqljdbc.jar** on JRE 6.0 or JRE 7.0 will throw an exception when connecting to a database.<br /><br /> **Note:** The JDBC Driver does not support JRE 1.4. You must upgrade JRE 1.4 to JRE 5.0, JRE 6.0, or JRE 7.0 when using the JDBC Driver. In some cases, you might need to recompile your application because it might not be compatible with JDK 5.0 API or JDK 6.0 API. For more information, see the documentation on the Sun Microsystems website.|  
|sqljdbc4.jar|**sqljdbc4.jar** class library provides support for JDBC 4.0 API. It includes all of the features of the **sqljdbc.jar** as well as the new JDBC 4.0 API methods.<br /><br /> **sqljdbc4.jar** class library requires a Java Runtime Environment (JRE) of version 6.0 or 7.0. Using **sqljdbc4.jar** on JRE 5.0 will throw an exception.<br /><br /> **Note:**  Use **sqljdbc4.jar** when your application must run on JRE 6.0 or 7.0, even if your application does not use JDBC 4.0 API features.|  
|sqljdbc41.jar|**sqljdbc41.jar** class library provides support for JDBC 4.0 API. It includes all of the features of the **sqljdbc4.jar** as well as the JDBC 4.0 API methods. JDBC 4.1 is not supported (will throw an exception “SQLFeatureNotSupportedException”).<br /><br /> **sqljdbc41.jar** class library requires a Java Runtime Environment (JRE) 7.0. Using **sqljdbc41.jar** on JRE 6.0 and 5.0 will throw an exception.<br /><br /> **Note:**  Use **sqljdbc41.jar** when your application must compile with JDK 7.0.|  
  
 Note that the JDBC driver is designed to work with and be supported by all major Sun equivalent Java virtual machines, but is tested on Sun JRE 5.0, 6.0 and 7.0.  
  
 The following summarizes support provided by the three JAR files included with Microsoft JDBC Driver 4.1 for SQL Server.  
  
|JAR|JDBC Version|JRE (can run)|JDK (can compile)|  
|---------|------------------|---------------------|-------------------------|  
|sqljdbc.jar|3|5|5|  
|sqljdbc4.jar|4|7 6|6 5|  
|sqljdbc41.jar|4|7|7 6 5|  
  
 **Microsoft JDBC Driver 4.0 for SQL Server:**  
  
 To support backward compatibility and possible upgrade scenarios, the JDBC Driver includes two JAR class libraries in each installation package: **sqljdbc.jar** and **sqljdbc4.jar**.  
  
|JAR|Description|  
|---------|-----------------|  
|sqljdbc.jar|**sqljdbc.jar** class library provides support for JDBC 3.0.<br /><br /> **sqljdbc.jar** class library requires a Java Runtime Environment (JRE) of version 5.0. Using **sqljdbc.jar** on JRE 6.0 or JRE 7.0 will throw an exception when connecting to a database.<br /><br /> **Note:** The JDBC Driver does not support JRE 1.4. You must upgrade JRE 1.4 to JRE 5.0, JRE 6.0, or JRE 7.0 when using the JDBC Driver. In some cases, you might need to recompile your application because it might not be compatible with JDK 5.0 API or JDK 6.0 API. For more information, see the documentation on the Sun Microsystems website.<br /><br /> [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)] does not support JDK 7.|  
|sqljdbc4.jar|**sqljdbc4.jar** class library provides support for JDBC 4.0 API. It includes all of the features of the **sqljdbc.jar** as well as the new JDBC 4.0 API methods.<br /><br /> **sqljdbc4.jar** class library requires a Java Runtime Environment (JRE) of version 6.0 or 7.0. Using **sqljdbc4.jar** on JRE 5.0 will throw an exception.<br /><br /> **Note:**  Use **sqljdbc4.jar** when your application must run on JRE 6.0 or 7.0, even if your application does not use JDBC 4.0 API features.|  
  
 Note that the JDBC driver is designed to work with and be supported by all major Sun equivalent Java virtual machines, but is tested on Sun JRE 5.0, 6.0 and 7.0.  
  
## SQL Server Requirements  
 The JDBC driver supports connections to Azure SQL database and SQL Server. For Microsoft JDBC Driver 4.2 and 4.1 for SQL Server, support begins with SQL Server 2008. For Microsoft JDBC Driver 4.0 for SQL Server, support beings with SQL Server 2005.  
  
## Operating System Requirements  
 The JDBC driver is designed to work on any operating system that supports the use of a Java Virtual Machine (JVM). However, only Sun Solaris, SUSE Linux, and Windows operating systems have officially been tested.  
  
## Supported Languages  
 The JDBC driver supports all [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] column collations. For more information about the collations supported by the JDBC driver, see [International Features of the JDBC Driver](../../connect/jdbc/international-features-of-the-jdbc-driver.md).  
  
 For more information about collations, see "Working with Collations" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Books Online.  
  
## See Also  
 [Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
  
  