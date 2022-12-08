---
title: System requirements
description: Find the system requirements for the JDBC driver. Including what Java, operation system, and database versions are supported.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# System requirements for the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  To use the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] to access data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], you must have the following components installed on your computer:

- [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] ([download](download-microsoft-jdbc-driver-for-sql-server.md))
- Java Runtime Environment

## Java Runtime Environment requirements  

 As of Microsoft JDBC Driver 11.2 for SQL Server, Java Development Kit (JDK) 18.0 and Java Runtime Environment (JRE) 18.0 are supported.

 As of Microsoft JDBC Driver 10.2 for SQL Server, Java Development Kit (JDK) 17.0 and Java Runtime Environment (JRE) 17.0 are supported.

 As of Microsoft JDBC Driver 9.4 for SQL Server, Java Development Kit (JDK) 16.0 and Java Runtime Environment (JRE) 16.0 are supported.

 As of Microsoft JDBC Driver 9.2 for SQL Server, Java Development Kit (JDK) 15.0 and Java Runtime Environment (JRE) 15.0 are supported.

 As of Microsoft JDBC Driver 8.4 for SQL Server, Java Development Kit (JDK) 14.0 and Java Runtime Environment (JRE) 14.0 are supported.

 As of Microsoft JDBC Driver 8.2 for SQL Server, Java Development Kit (JDK) 13.0 and Java Runtime Environment (JRE) 13.0 are supported.

 As of Microsoft JDBC Driver 7.4 for SQL Server, Java Development Kit (JDK) 12.0 and Java Runtime Environment (JRE) 12.0 are supported.

 As of Microsoft JDBC Driver 7.2 for SQL Server, Java Development Kit (JDK) 11.0 and Java Runtime Environment (JRE) 11.0 are supported.

 As of Microsoft JDBC Driver 7.0 for SQL Server, Java Development Kit (JDK) 10.0 and Java Runtime Environment (JRE) 10.0 are supported.

 As of Microsoft JDBC Driver 6.4 for SQL Server, Java Development Kit (JDK) 9.0 and Java Runtime Environment (JRE) 9.0 are supported.

 As of Microsoft JDBC Driver 4.2 for SQL Server, Java Development Kit (JDK) 8.0 and Java Runtime Environment (JRE) 8.0 are supported. Support for JDBC Spec API has been extended to include the JDBC 4.1 and 4.2 API.
  
 As of Microsoft JDBC Driver 4.1 for SQL Server, Java Development Kit (JDK) 7.0 and Java Runtime Environment (JRE) 7.0 are supported.
  
 As of [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)], the JDBC driver support for JDBC Spec API has been extended to include the JDBC 4.0 API. The JDBC 4.0 API was introduced as part of the Java Development Kit (JDK) 6.0 and Java Runtime Environment (JRE) 6.0. JDBC 4.0 is a superset of the JDBC 3.0 API.
  
 When you deploy the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] on Windows and UNIX operating systems, you must use the installation packages, *sqljdbc_\<version>_enu.exe*, and *sqljdbc_\<version>_enu.tar.gz*, respectively. For more information about how to deploy the JDBC driver, see [Deploying the JDBC driver](deploying-the-jdbc-driver.md) article. 

 **Microsoft JDBC Driver 11.2 for SQL Server:**  

  The JDBC Driver 11.2 includes four JAR class libraries in each installation package: **mssql-jdbc-11.2.0.jre8.jar**, **mssql-jdbc-11.2.0.jre11.jar**, **mssql-jdbc-11.2.0.jre17.jar**, and **mssql-jdbc-11.2.0.jre18.jar**.

  The JDBC Driver 11.2 is designed to work with, and supports all major Java virtual machines, but is tested only on OpenJDK 1.8, OpenJDK 11.0, OpenJDK 17.0, OpenJDK 18.0, Azul Zulu JRE 1.8, Azul Zulu JRE 11.0, and Azul Zulu JRE 17.0.
  
  The following chart summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 11.2 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|mssql-jdbc-11.2.0.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 1.8. Using JRE 1.7 or lower throws an exception. |
|mssql-jdbc-11.2.0.jre11.jar|4.3|11|Requires a Java Runtime Environment (JRE) 11.0. Using JRE 10.0 or lower throws an exception. |
|mssql-jdbc-11.2.0.jre17.jar|4.3|17|Requires a Java Runtime Environment (JRE) 17.0. Using JRE 16.0 or lower throws an exception. |
|mssql-jdbc-11.2.0.jre18.jar|4.3|18|Requires a Java Runtime Environment (JRE) 18.0. Using JRE 17.0 or lower throws an exception. |

  The JDBC Driver 10.2 is available on the Maven Central Repository, and can be added to a Maven project with the following code in the POM.XML:  
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>11.2.0.jre11</version>
</dependency> 
```

**Microsoft JDBC Driver 10.2 for SQL Server:**  

  The JDBC Driver 10.2 includes three JAR class libraries in each installation package: **mssql-jdbc-10.2.0.jre8.jar**, **mssql-jdbc-10.2.0.jre11.jar**, and **mssql-jdbc-10.2.0.jre17.jar**.

  The JDBC Driver 10.2 is designed to work with, and supports all major Java virtual machines, but is tested only on OpenJDK 1.8, OpenJDK 11.0, OpenJDK 17.0, Azul Zulu JRE 1.8, Azul Zulu JRE 11.0, and Azul Zulu JRE 17.0.
  
  The following chart summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 10.2 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|mssql-jdbc-10.2.0.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 1.8. Using JRE 1.7 or lower throws an exception. |
|mssql-jdbc-10.2.0.jre11.jar|4.3|11|Requires a Java Runtime Environment (JRE) 11.0. Using JRE 10.0 or lower throws an exception. |
|mssql-jdbc-10.2.0.jre17.jar|4.3|17|Requires a Java Runtime Environment (JRE) 17.0. Using JRE 16.0 or lower throws an exception. |

  The JDBC Driver 11.2 is available on the Maven Central Repository, and can be added to a Maven project with the following code in the POM.XML:  
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>11.2.0.jre11</version>
</dependency>
```

**Microsoft JDBC Driver 9.4 for SQL Server:**  

  The JDBC Driver 9.4 includes three JAR class libraries in each installation package: **mssql-jdbc-9.4.1.jre8.jar**, **mssql-jdbc-9.4.1.jre11.jar**, and **mssql-jdbc-9.4.1.jre16.jar**.

  The JDBC Driver 9.4 is designed to work with, and supports all major Java virtual machines, but is tested only on OpenJDK 1.8, OpenJDK 11.0, OpenJDK 16.0, Azul Zulu JRE 1.8, Azul Zulu JRE 11.0, and Azul Zulu JRE 16.0.
  
  The following chart summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 9.4 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|mssql-jdbc-9.4.1.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 1.8. Using JRE 1.7 or lower throws an exception. |
|mssql-jdbc-9.4.1.jre11.jar|4.3|11|Requires a Java Runtime Environment (JRE) 11.0. Using JRE 10.0 or lower throws an exception. |
|mssql-jdbc-9.4.1.jre16.jar|4.3|16|Requires a Java Runtime Environment (JRE) 16.0. Using JRE 15.0 or lower throws an exception. |

  The JDBC Driver 9.4 is available on the Maven Central Repository and can be added to a Maven project with the following code in the POM.XML:  
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>9.4.1.jre11</version>
</dependency>
```

**Microsoft JDBC Driver 9.2 for SQL Server:**  

  The JDBC Driver 9.2 includes three JAR class libraries in each installation package: **mssql-jdbc-9.2.1.jre8.jar**, **mssql-jdbc-9.2.1.jre11.jar**, and **mssql-jdbc-9.2.1.jre15.jar**.

  The JDBC Driver 9.2 is designed to work with, and supports all major Java virtual machines, but is tested only on OpenJDK 1.8, OpenJDK 11.0, OpenJDK 15.0, Azul Zulu JRE 1.8, Azul Zulu JRE 11.0, and Azul Zulu JRE 15.0.
  
  The following chart summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 9.2 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|mssql-jdbc-9.2.1.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 1.8. Using JRE 1.7 or lower throws an exception.<br /><br /> New Features in 9.2 include: JDK 15 support, support for Azure Active Directory Interactive Authentication, support for Azure Active Directory Service Principal Authentication, and support for useBulkCopyForBatchInsert for non-Azure Synapse Analytics servers. |
|mssql-jdbc-9.2.1.jre11.jar|4.3|11|Requires a Java Runtime Environment (JRE) 11.0. Using JRE 10.0 or lower throws an exception.<br /><br /> New Features in 9.2 include: JDK 15 support, support for Azure Active Directory Interactive Authentication, support for Azure Active Directory Service Principal Authentication, and support for useBulkCopyForBatchInsert for non-Azure Synapse Analytics servers. |
|mssql-jdbc-9.2.1.jre15.jar|4.3|15|Requires a Java Runtime Environment (JRE) 15.0. Using JRE 14.0 or lower throws an exception.<br /><br /> New Features in 9.2 include: JDK 15 support, support for Azure Active Directory Interactive Authentication, support for Azure Active Directory Service Principal Authentication, and support for useBulkCopyForBatchInsert for non-Azure Synapse Analytics servers. |

  The JDBC Driver 9.2 is available on the Maven Central Repository and can be added to a Maven project with the following code in the POM.XML:  
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>9.2.1.jre11</version>
</dependency>
```

**Microsoft JDBC Driver 8.4 for SQL Server:**  

  The JDBC Driver 8.4 includes three JAR class libraries in each installation package: **mssql-jdbc-8.4.1.jre8.jar**, **mssql-jdbc-8.4.1.jre11.jar**, and **mssql-jdbc-8.4.1.jre14.jar**.

  The JDBC Driver 8.4 is designed to work with, and supports all major Java virtual machines, but is tested only on OpenJDK 1.8, OpenJDK 11.0, OpenJDK 14.0, Azul Zulu JRE 1.8, Azul Zulu JRE 11.0, and Azul Zulu JRE 14.0.
  
  The following chart summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 8.4 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|mssql-jdbc-8.4.1.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 1.8. Using JRE 1.7 or lower throws an exception.<br /><br /> New Features in 8.4 include: JDK 14 support, support for authentication to Azure Key Vault using Managed Identity, extended support for bulk copy for Azure Data Warehouse, Azure SQL DNS caching, support for backwards compatibility for streaming LOB objects, and client certificate authentication for loopback scenarios. |
|mssql-jdbc-8.4.1.jre11.jar|4.3|11|Requires a Java Runtime Environment (JRE) 11.0. Using JRE 10.0 or lower throws an exception.<br /><br /> New Features in 8.4 include: JDK 14 support, support for authentication to Azure Key Vault using Managed Identity, extended support for bulk copy for Azure Data Warehouse, Azure SQL DNS caching, support for backwards compatibility for streaming LOB objects, and client certificate authentication for loopback scenarios. |
|mssql-jdbc-8.4.1.jre13.jar|4.3|14|Requires a Java Runtime Environment (JRE) 14.0. Using JRE 13.0 or lower throws an exception.<br /><br /> New Features in 8.4 include: JDK 14 support, support for authentication to Azure Key Vault using Managed Identity, extended support for bulk copy for Azure Data Warehouse, Azure SQL DNS caching, support for backwards compatibility for streaming LOB objects, and client certificate authentication for loopback scenarios. |

  The JDBC Driver 8.4 is available on the Maven Central Repository and can be added to a Maven project with the following code in the POM.XML:  
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>8.4.1.jre11</version>
</dependency>
```

**Microsoft JDBC Driver 8.2 for SQL Server:**  

  The JDBC Driver 8.2 includes three JAR class libraries in each installation package: **mssql-jdbc-8.2.2.jre8.jar**, **mssql-jdbc-8.2.2.jre11.jar**, and **mssql-jdbc-8.2.2.jre13.jar**.

  The JDBC Driver 8.2 is designed to work with, and supports all major Java virtual machines, but is tested only on OpenJDK 1.8, OpenJDK 11.0, OpenJDK 13.0, Azul Zulu JRE 1.8, Azul Zulu JRE 11.0, and Azul Zulu JRE 13.0.
  
  The following chart summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 8.2 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|mssql-jdbc-8.2.2.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 1.8. Using JRE 1.7 or lower throws an exception.<br /><br /> New Features in 8.2 include: JDK 13 support, Always Encrypted with secure enclaves, and temporal datatype performance improvements. |
|mssql-jdbc-8.2.2.jre11.jar|4.3|11|Requires a Java Runtime Environment (JRE) 11.0. Using JRE 10.0 or lower throws an exception.<br /><br /> New Features in 8.2 include: JDK 13 support, Always Encrypted with secure enclaves, and temporal datatype performance improvements. |
|mssql-jdbc-8.2.2.jre13.jar|4.3|13|Requires a Java Runtime Environment (JRE) 13.0. Using JRE 11.0 or lower throws an exception.<br /><br /> New Features in 8.2 include: JDK 13 support, Always Encrypted with secure enclaves, and temporal datatype performance improvements. |

  The JDBC Driver 8.2 is available on the Maven Central Repository and can be added to a Maven project with the following code in the POM.XML:  
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>8.2.2.jre11</version>
</dependency>
```

**Microsoft JDBC Driver 7.4 for SQL Server:**  

  The JDBC Driver 7.4 includes three JAR class libraries in each installation package: **mssql-jdbc-7.4.1.jre8.jar**, **mssql-jdbc-7.4.1.jre11.jar**, and **mssql-jdbc-7.4.1.jre12.jar**.

  The JDBC Driver 7.4 is designed to work with, and supports all major Java virtual machines, but is tested only on OpenJDK 1.8, OpenJDK 11.0, OpenJDK 12.0, Azul Zulu JRE 1.8, Azul Zulu JRE 11.0, and Azul Zulu JRE 12.0.
  
  The following chart summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 7.4 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|mssql-jdbc-7.4.1.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 1.8. Using JRE 1.7 or lower throws an exception.<br /><br /> New Features in 7.4 include: JDK 12 support, NTLM authentication, and useFmtOnly. |  
|mssql-jdbc-7.4.1.jre11.jar|4.3|11|Requires a Java Runtime Environment (JRE) 11.0. Using JRE 10.0 or lower throws an exception.<br /><br /> New Features in 7.4 include: JDK 12 support, NTLM authentication, and useFmtOnly. |  
|mssql-jdbc-7.4.1.jre12.jar|4.3|12|Requires a Java Runtime Environment (JRE) 12.0. Using JRE 11.0 or lower throws an exception.<br /><br /> New Features in 7.4 include: JDK 12 support, NTLM authentication, and useFmtOnly. |  

  The JDBC Driver 7.4 is available on the Maven Central Repository and can be added to a Maven project with the following code in the POM.XML:  
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>7.4.1.jre11</version>
</dependency>
```

**Microsoft JDBC Driver 7.2 for SQL Server:**  

  The JDBC Driver 7.2 includes two JAR class libraries in each installation package: **mssql-jdbc-7.2.2.jre8.jar**, and **mssql-jdbc-7.2.2.jre11.jar**.

  The JDBC Driver 7.2 is designed to work with, and supports all major Java virtual machines, but is tested only on OpenJDK 8.0, OpenJDK 11.0, Azul Zulu JRE 8.0, and Azul Zulu JRE 11.0.
  
  The following chart summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 7.2 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|mssql-jdbc-7.2.2.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 8.0. Using JRE 7.0 or lower throws an exception.<br /><br /> New Features in 7.2 include: JDK 11 support, Active Directory Managed Identity (MSI) authentication, OSGi support, SQLServerError APIs. |  
|mssql-jdbc-7.2.2.jre11.jar|4.3|10|Requires a Java Runtime Environment (JRE) 11.0. Using JRE 10.0 or lower throws an exception.<br /><br /> New Features in 7.2 include: JDK 11 support, Active Directory Managed Identity (MSI) authentication, OSGi support, SQLServerError APIs. |  

  The JDBC Driver 7.2 is available on the Maven Central Repository and can be added to a Maven project with the following code in the POM.XML:  
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>7.2.2.jre11</version>
</dependency>
```

**Microsoft JDBC Driver 7.0 for SQL Server:**  

  The JDBC Driver 7.0 includes two JAR class libraries in each installation package: **mssql-jdbc-7.0.0.jre8.jar**, and **mssql-jdbc-7.0.0.jre10.jar**.

  The JDBC Driver 7.0 is designed to work with, and supports all major Java virtual machines, but is tested only on OpenJDK 8.0, and 10.0.
  
  The following chart summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 7.0 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|mssql-jdbc-7.0.0.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 8.0. Using JRE 7.0 or lower throws an exception.<br /><br /> New Features in 7.0 include: JDK 10 support, updated default compliance level to JDBC 4.2 specifications, Spatial Datatypes support, cancelQueryTimeout connection property, Request Boundary methods, useBulkCopyForBatchInsert connection property, Data Discovery and Classification information, UTF-8 feature extension, and CityHash support. |  
|mssql-jdbc-7.0.0.jre10.jar|4.3|10|Requires a Java Runtime Environment (JRE) 10.0. Using JRE 9.0 or lower throws an exception.<br /><br /> New Features in 7.0 include: JDK 10 support, updated default compliance level to JDBC 4.2 specifications, Spatial Datatypes support, cancelQueryTimeout connection property, Request Boundary methods, useBulkCopyForBatchInsert connection property, Data Discovery and Classification information, UTF-8 feature extension, and CityHash support. |  

  The JDBC Driver 7.0 is available on the Maven Central Repository and can be added to a Maven project with the following code in the POM.XML:  
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>7.0.0.jre10</version>
</dependency>
```
  
**Microsoft JDBC Driver 6.4 for SQL Server:**  

  The JDBC Driver 6.4 includes three JAR class libraries in each installation package: **mssql-jdbc-6.4.0.jre7.jar**, **mssql-jdbc-6.4.0.jre8.jar**, and **mssql-jdbc-6.4.0.jre9.jar**.

  The JDBC Driver 6.4 is designed to work with, and supports all major Java virtual machines, but is tested only on OpenJDK 7.0, 8.0, and 9.0.
  
  The following chart summarizes support provided by the three JAR files included with Microsoft JDBC Drivers 6.4 for SQL Server:  
  
  |JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|mssql-jdbc-6.4.0.jre7.jar|4.1|7|Requires a Java Runtime Environment (JRE) 7.0. Using JRE 6.0 or lower throws an exception.<br /><br /> New Features in 6.4 include: Azure AD authentication for Linux, Principal/Password method for Kerberos, automatic detection of REALM in SPN for Cross-Domain authentication, Kerberos Constrained Delegation, Query Timeout, Socket Timeout, and prepared statement handle reuse. |  
|mssql-jdbc-6.4.0.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 8.0. Using JRE 7.0 or lower throws an exception.<br /><br /> New Features in 6.4 include: Azure AD authentication for Linux, Principal/Password method for Kerberos, automatic detection of REALM in SPN for Cross-Domain authentication, Kerberos Constrained Delegation, Query Timeout, Socket Timeout, and prepared statement handle reuse. |  
|mssql-jdbc-6.4.0.jre9.jar|4.3|9|Requires a Java Runtime Environment (JRE) 9.0. Using JRE 8.0 or lower throws an exception.<br /><br /> New Features in 6.4 include: Azure AD authentication for Linux, Principal/Password method for Kerberos, automatic detection of REALM in SPN for Cross-Domain authentication, Kerberos Constrained Delegation, Query Timeout, Socket Timeout, and prepared statement handle reuse. |

The JDBC Driver 6.4 is available on the Maven Central Repository and can be added to a Maven project with the following code in the POM.XML

 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>6.4.0.jre9</version>
</dependency>
```

**Microsoft JDBC Driver 6.2 for SQL Server:**  
  
  The JDBC Driver 6.2 includes two JAR class libraries in each installation package: **mssql-jdbc-6.2.2.jre7.jar**, and **mssql-jdbc-6.2.2.jre8.jar**.
  
 The JDBC Driver 6.2 is designed to work with, and supports all major Java virtual machines, but is tested only on Sun JRE 5.0, 6.0, 7.0, and 8.0.
  
 The following chart summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 6.0 and 4.2 for SQL Server:  
  
|JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|
|mssql-jdbc-6.2.2.jre7.jar|4.1|7|Requires a Java Runtime Environment (JRE) 7.0. Using JRE 6.0 or lower throws an exception.<br /><br /> New Features in 6.2 include: Azure AD authentication for Linux, Principal/Password method for Kerberos, automatic detection of REALM in SPN for Cross-Domain authentication, Kerberos Constrained Delegation, Query Timeout, Socket Timeout, and prepared statement handle reuse. |  
|mssql-jdbc-6.2.3.jre8.jar|4.2|8|Requires a Java Runtime Environment (JRE) 8.0. Using JRE 7.0 or lower throws an exception.<br /><br /> New Features in 6.2 include: Azure AD authentication for Linux, Principal/Password method for Kerberos, automatic detection of REALM in SPN for Cross-Domain authentication, Kerberos Constrained Delegation, Query Timeout, Socket Timeout, and prepared statement handle reuse|  

  The JDBC Driver 6.2 is available on the Maven Central Repository and can be added to a Maven project with the following code in the POM.XML
  
 ```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>6.2.2.jre8</version>
</dependency>
```

 **Microsoft JDBC Driver 6.0 and 4.2 for SQL Server:**  
  
  The JDBC Drivers 6.0 and 4.2 include two JAR class libraries in each installation package: **sqljdbc41.jar**, and **sqljdbc42.jar**.
  
 The JDBC Drivers 6.0 and 4.2 are designed to work with, and supports all major Java virtual machines, but is tested only on Sun JRE 5.0, 6.0, 7.0, and 8.0.
  
 The following chart summarizes support provided by the two JAR files included with Microsoft JDBC Drivers 6.0 and 4.2 for SQL Server:  
  
|JAR|JDBC Version Compliance|Recommended Java Version|Description|  
|---------|-----------------------------|----------------------|-----------------|  
|sqljdbc41.jar|4.1|7|Requires a Java Runtime Environment (JRE) 7.0. Using JRE 6.0 or lower throws an exception.<br /><br /> New Features in 6.0 & 4.2 packages include: JDBC 4.1 Compliance and Bulk Copy<br /><br /> In Addition, new Features in only the 6.0 package include: Always Encrypted, Table-Valued Parameters, Azure Active Directory Authentication, transparent connections to Always On Availability Groups, improvement in parameter metadata retrieval  for prepared queries and Internationalized Domain Name (IDN)|  
|sqljdbc42.jar|4.2|8|Requires a Java Runtime Environment (JRE) 8.0. Using JRE 7.0 or lower throws an exception.<br /><br /> New Features in 6.0 & 4.2 packages include: JDBC 4.1 Compliance, JDBC 4.2 Compliance, and Bulk Copy<br /><br /> In Addition, new Features in only the 6.0 package include: Always Encrypted, Table-Valued Parameters, Azure Active Directory Authentication, transparent connections to Always On Availability Groups, improvement in parameter metadata retrieval  for prepared queries and Internationalized Domain Name (IDN)|  
  
 **Microsoft JDBC Driver 4.1 for SQL Server:**  
  
 The JDBC Driver 4.1 includes one JAR class library in each installation package: **sqljdbc41.jar**.  

|JAR|Description|  
|---------|-----------------|  
|sqljdbc41.jar|**sqljdbc41.jar** class library provides support for JDBC 4.0 API. It includes all of the features of the JDBC 4.0 driver and the JDBC 4.0 API methods. JDBC 4.1 isn't supported (throws an exception "SQLFeatureNotSupportedException").<br /><br /> **sqljdbc41.jar** class library requires a Java Runtime Environment (JRE) 7.0. Using **sqljdbc41.jar** on JRE 6.0 and 5.0 throws an exception.<br /><br />
  
 The JDBC driver is designed to work with, and supports all major Java virtual machines, but is tested on Sun JRE 5.0, 6.0 and 7.0.
  
 The following chart summarizes support provided by the JAR file included with Microsoft JDBC Driver 4.1 for SQL Server.  
  
|JAR|JDBC Version|JRE (can run)|JDK (can compile)|  
|---------|------------------|---------------------|-------------------------|  
|sqljdbc41.jar|4|7|7 6 5|  
  
## SQL Server requirements  

 The JDBC driver supports connections to Azure SQL database and SQL Server. For Microsoft JDBC Driver 4.2 and 4.1 for SQL Server, support begins with SQL Server 2008.
  
## Operating System requirements  

 The JDBC driver is designed to work on any operating system that supports the use of a Java Virtual Machine (JVM). However, only Sun Solaris, SUSE Linux, Ubuntu Linux, CentOS Linux, macOS, and Windows operating systems have officially been tested.  
  
## Supported languages  

 The JDBC driver supports all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] column collations. For more information about the collations supported by the JDBC driver, see [International features of the JDBC driver](international-features-of-the-jdbc-driver.md).  
  
 For more information about collations, see "Working with Collations" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See also  

 [Overview of the JDBC driver](overview-of-the-jdbc-driver.md)  
