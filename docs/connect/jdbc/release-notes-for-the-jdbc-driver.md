---
title: "Release Notes for the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 074f211e-984a-4b76-bb15-ee36f5946f12
caps.latest.revision: 206
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Release Notes for the JDBC Driver
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

## Updates in Microsoft JDBC Driver 6.2 for SQL Server
The Microsoft JDBC Driver 6.2 for SQL Server is fully compliant with JDBC specifications 4.1 and 4.2. The jars contained in the 6.0 package are named according to Java version compatibility. For example, the mssql-jdbc-6.2.1.jre8.jar file from the 6.2 package is recommended to be used with Java 8. 

**Note:** An issue with the metadata caching improvement was found in the JDBC 6.2 RTW released on June 29, 2017. The improvement was rolled back and new jars (version 6.2.1) were released on July 17, 2017 on the [Microsoft Download Center](https://go.microsoft.com/fwlink/?linkid=852460), [GitHub](https://github.com/Microsoft/mssql-jdbc/releases/tag/v6.2.1), and [Maven Central](http://search.maven.org/#search%7Cgav%7C1%7Cg%3A%22com.microsoft.sqlserver%22%20AND%20a%3A%22mssql-jdbc%22). Please update your projects to use the 6.2.1 release jars. Please view [release notes](https://github.com/Microsoft/mssql-jdbc/releases/tag/v6.2.1) for more details.

**Azure Active Directory (AAD) support for Linux**

Connect your Linux applications to Azure SQL Database using AAD authentication via username/password and access token methods.

**Federal Information Processing Standard (FIPS) enabled JVMs**

The JDBC Driver can now be used on JVMs that run in FIPS 140 compliance mode to meet federal standards and compliance. 

**Kerberos Authentication Improvements** 

The JDBC Driver now has support for: 
* Principal/Password method for applications where the Kerberos configuration cannot be modified or unable to retrieve a new token or keytab. This method can be used for authenticating to a SQL Server that only allows Kerberos authentication. 
* Cross-realm authentication using Kerberos Integrated authentication without explicitly setting the server SPN. The driver now automatically computes the REALM even when it has not been provided.
* Kerberos Constrained Delegation by accepting impersonated user credentials as a GSS Credential object via data source. This impersonated credential is then used to establish a Kerberos connection. 

**Added Timeouts**

The JDBC Driver now supports the following configurable timeouts you can change based on your application’s needs: 
* Query Timeout to control the number of seconds to wait before a timeout occurs when running a query. 
* Socket Timeout to specify the number of milliseconds to wait before a timeout occurs on a socket read or accept. 

## Updates in Microsoft JDBC Driver 6.1 for SQL Server

The Microsoft JDBC Driver 6.1 for SQL Server is fully compliant with JDBC specifications 4.1 and 4.2. This release is the initial open source release of the JDBC Driver and contains the mssql-jdbc-6.1.0.jre8.jar mssql-jdbc-6.1.0.jre7.jar files, which correspond to the Java version compatibility. 

## Updates in Microsoft JDBC Driver 6.0 for SQL Server

The Microsoft JDBC Driver 6.0 for SQL Server is fully compliant with JDBC specifications 4.1 and 4.2. The jars contained in the 6.0 package are named according to their compliance with the JDBC API version. For example, the sqljdbc42.jar file from the 6.0 package is JDBC API 4.2 compliant. Similarly, the sqljdbc41.jar file is compliant with JDBC API 4.1.

To ensure you have the right sqljdbc42.jar or sqljdbc41.jar, run the following lines of code. If the output is “Driver version: 6.0.7507.100“, you have the JDBC Driver 6.0 package.
```
Connection conn = DriverManager.getConnection("jdbc:sqlserver://<server>;user=<user>;password=<password>;");
System.out.println("Driver version: " + conn.getMetaData().getDriverVersion());
```  
 **Always Encrypted**  
  
 Support for the recently released Always Encrypted feature in SQL Server 2016, a new security feature that ensures sensitive data is never seen in plaintext in a SQL Server instance. Always Encrypted works by transparently encrypting the data in the application, so that SQL Server will only handle the encrypted data and not plaintext values. Even if the SQL instance or the host machine is compromised, all an attacker can get is ciphertext of sensitive data. For details see [Using Always Encrypted with the JDBC Driver](../../connect/jdbc/using-always-encrypted-with-the-jdbc-driver.md).  
  
 **Internationalized Domain Name (IDN)**  
  
 Support for Internationalized Domain Names (IDNs) for server names. For details see Using International Domain Names on the [International Features of the JDBC Driver](../../connect/jdbc/international-features-of-the-jdbc-driver.md) page.  
  
 **Parameterized Query**  
  
 Now supports retrieving parameter metadata with prepared statements for complex queries such as sub-queries and/or joins. Note that this improvement is available only when using SQL Server 2012 and newer versions.  
  
 **Azure Active Directory (AAD)**  
  
 AAD authentication is a mechanism of connecting to Azure SQL Database v12 using identities in AAD. Use AAD authentication to centrally manage identities of database users and as an alternative to SQL Server authentication. The JDBC Driver 6.0 allows you to specify your AAD credentials in the JDBC connection string to connect to Azure SQL DB.  For details see the authentication property on the [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md) page.  
  
 **Table-Valued Parameters**  
  
 Table-valued parameters provide an easy way to marshal multiple rows of data from a client application to SQL Server without requiring multiple round trips or special server-side logic for processing the data. You can use table-valued parameters to encapsulate rows of data in a client application and send the data to the server in a single parameterized command. The incoming data rows are stored in a table variable that can then be operated on by using Transact-SQL. For details see [Using Table-Valued Parameters](../../connect/jdbc/using-table-valued-parameters.md).  
  
 **AlwaysOn Availability Groups (AG)**  
  
 The driver now supports transparent connections to AlwaysOn Availability Groups. The driver quickly discovers the current AlwaysOn topology of your server infrastructure and connects to the current active server transparently.  
  
## Updates in Microsoft JDBC Driver 4.2 for SQL Server and later  
The Microsoft JDBC Driver 4.2 for SQL Server is fully compliant with JDBC specifications 4.1 and 4.2. The jars contained in the 4.2 package are named according to their compliance with the JDBC API version. For example, the sqljdbc42.jar file from the 4.2 package is JDBC API 4.2 compliant. Similarly, the sqljdbc41.jar file is compliant with JDBC API 4.1.

To ensure you have the right sqljdbc42.jar or sqljdbc41.jar, run the following lines of code. If the output is "Driver version: 4.2.6420.100", you have the JDBC Driver 4.2 package.
```
Connection conn = DriverManager.getConnection("jdbc:sqlserver://<server>;user=<user>;password=<password>;");
System.out.println("Driver version: " + conn.getMetaData().getDriverVersion());
```
 **Support for JDK 8**  
  
 Support for Java Development Kit (JDK) version 8.0 in addition to JDK 7.0, 6.0, and 5.0.  
  
 **JDBC 4.1 and 4.2 compliance**  
  
 Support for Java Database Connectivity API 4.1 and 4.2 specifications, in addition to 4.0. For details see [JDBC 4.1 Compliance for the JDBC Driver](../../connect/jdbc/jdbc-4-1-compliance-for-the-jdbc-driver.md) and [JDBC 4.2 Compliance for the JDBC Driver](../../connect/jdbc/jdbc-4-2-compliance-for-the-jdbc-driver.md).  
  
 **Bulk copy**  
  
 The bulk copy feature is used to quickly copy large amounts of data into tables or views in SQL Server databases. For details see [Using Bulk Copy with the JDBC Driver](../../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md).  
  
 **XA transaction rollback option**  
  
 Added new timeout options for existing automatic rollback of unprepared transactions. For detail see [Understanding XA Transactions](../../connect/jdbc/understanding-xa-transactions.md).  
  
 **New Kerberos Principal Connection Property**  
  
 Added a new connection property to facilitate flexibility with Kerberos connections. For detail see [Using Kerberos Integrated Authentication to Connect to SQL Server](../../connect/jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server.md).  
  
## Updates in Microsoft JDBC Driver 4.1 for SQL Server and later  
 **Support for JDK 7**  
  
 Support for Java Development Kit (JDK) version 7.0 in addition to JDK 6.0 and 5.0.  
  
## Updates in Microsoft JDBC Driver 4.0 for SQL Server and later  
 **Information about Connecting to an Azure SQL Database**  
  
 There is now a topic with information about connecting to an Azure SQL database. See [Connecting to an Azure SQL database](../../connect/jdbc/connecting-to-an-azure-sql-database.md) for more information.  
  
 **Support for High Availability, Disaster Recovery**  
  
 Support for high-availability, disaster recovery connections to AlwaysOn Availability Groups in [!INCLUDE[ssSQL11](../../includes/sssql11_md.md)]. See [JDBC Driver Support for High Availability, Disaster Recovery](../../connect/jdbc/jdbc-driver-support-for-high-availability-disaster-recovery.md) for more information.  
  
 **Using Kerberos Integrated Authentication to Connect to SQL Server**  
  
 Support for type 4 Kerberos integrated authentication for applications to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] database. For more information, see [Using Kerberos Integrated Authentication to Connect to SQL Server](../../connect/jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server.md). (Type 2 Kerberos integrated authentication is available in [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] versions prior to 4.0.)  
  
 **Accessing Diagnostic Information in the Extended Events Log**  
  
 You can access information in the server's extended events log to understand connection failures. For more information, see [Accessing Diagnostic Information in the Extended Events Log](../../connect/jdbc/accessing-diagnostic-information-in-the-extended-events-log.md).  
  
 **Additional Support for Sparse Columns**  
  
 If your application already accesses data in a table that uses sparse columns, you should see an increase in performance. You can get information about columns (including sparse column information) with [getColumns Method &#40;SQLServerDatabaseMetaData&#41;](../../connect/jdbc/reference/getcolumns-method-sqlserverdatabasemetadata.md). For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] sparse columns, see [Using Sparse Columns](http://go.microsoft.com/fwlink/?LinkId=224244).  
  
 **Xid.getFormatId**  
  
 Beginning in [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)], the JDBC driver will pass the format identifier from the application to the database server. To get the updated behavior, make sure the sqljdbc_xa.dll on the server is updated. For more information on copying an updated version of sqljdbc_xa.dll to the server, see [Understanding XA Transactions](../../connect/jdbc/understanding-xa-transactions.md).  
  
## Itanium Not Supported for JDBC Driver 6.0, 4.2, 4.1, and 4.0 Applications  
  
 Microsoft JDBC Drivers 6.0, 4.2, 4.1, and 4.0 for SQL Server applications are not supported to run on an Itanium computer.  
  
## See Also  
 [Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
  
  
