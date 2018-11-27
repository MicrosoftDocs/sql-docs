---
title: "Release Notes for the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 074f211e-984a-4b76-bb15-ee36f5946f12
author: MightyPen
ms.author: genemi
manager: craigg
---

# Release Notes for the JDBC Driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

## Updates in Microsoft JDBC Driver 7.0 for SQL Server

The Microsoft JDBC Driver 7.0 for SQL Server is fully compliant with JDBC API Specification 4.2. The jars in the 7.0 package are named according to Java version compatibility. For example, the mssql-jdbc-7.0.0.jre10.jar file from the 7.0 package should be used with Java 10.

### Support for JDK 10

The Microsoft JDBC Driver 7.0 for SQL Server is now compatible with Java Development Kit (JDK) version 10.0 in addition to JDK 1.8. This update also exposes the driver's 'Automatic-Module-Name' as `com.microsoft.sqlserver.jdbc` through its MANIFEST file.

### Support for Spatial Datatypes

The Microsoft JDBC Driver 7.0 for SQL Server now provides support for SQL Server Spatial Datatypes 'Geography' and 'Geometry'. For more information about Spatial datatypes APIs and how to use them, see [here](../../connect/jdbc/use-spatial-datatypes.md).

### Implementation for JDBC 4.3 introduced java.sql.Connection APIs beginRequest() and endRequest()

The Microsoft JDBC Driver 7.0 for SQL Server now implements `beginRequest()` and `endRequest()` APIs from `java.sql.Connection` class. These APIs were introduced with JDBC 4.3 Specifications and JDK 9. For more information about the driver's implementation of these APIs, see [here](../../connect/jdbc/jdbc-4-3-compliance-for-the-jdbc-driver.md).

### Support for 'SQL data discovery and classification'

The Microsoft JDBC Driver 7.0 for SQL Server provides support for 'SQL data discovery and classification' feature with any target database that supports this feature. The driver now exposes `SQLServerResultSet.getSensitivityClassification()` APIs to extract this information from the fetched ResultSet.

For more information about how to use this feature with JDBC Driver, refer sample [here](../../connect/jdbc/data-discovery-classification-sample.md).

### Added new connection property: useBulkCopyForBatchInsert

The Microsoft JDBC Driver 7.0 for SQL Server introduces a new connection property, 'useBulkCopyForBatchInsert', which is only supported for **Azure Data Warehouse**.

This property is **disabled** by default and can be enabled to increase performance of user applications when pushing large amounts data to Azure Data Warehouse. Enabling this property changes the behavior of Batch Insert operations to switch to Bulk Copy operations with user provided data. For more information about this property and its limitations, refer [here](../../connect/jdbc/use-bulk-copy-api-batch-insert-operation.md).

### Added new connection property: cancelQueryTimeout

The Microsoft JDBC Driver 7.0 for SQL Server introduces new connection property, `cancelQueryTimeout`, to cancel `queryTimeout` on `java.sql.Connection` and `java.sql.Statement` objects.

### Added Azure Key Vault Provider Constructors

The Microsoft JDBC Driver 7.0 for SQL Server reintroduces a previously removed constructor, for `SQLServerColumnEncryptionAzureKeyVaultProvider`, which allowed authentication using a custom method implemented over `SQLServerKeyVaultAuthenticationCallback` to fetch an access token.

The new constructors have the below definition:

```java
/* This constructor is added to provide backwards compatibility with 6.0
* version of the driver. It is marked deprecated for removal in next
* stable release.
*/
@Deprecated
public SQLServerColumnEncryptionAzureKeyVaultProvider(
        SQLServerKeyVaultAuthenticationCallback authenticationCallback,
        ExecutorService executorService) throws SQLServerException;

/*New Constructor to replace the above constructor*/
public SQLServerColumnEncryptionAzureKeyVaultProvider(
            SQLServerKeyVaultAuthenticationCallback authenticationCallback) throws SQLServerException;
```

### Updated ADAL4J version to 1.6.0

The Microsoft JDBC Driver 7.0 for SQL Server has updated its maven dependency upon azure-activedirectory-library-for-java (ADAL4J) to version 1.6.0. For more information about dependencies, see [here](../../connect/jdbc/feature-dependencies-of-microsoft-jdbc-driver-for-sql-server.md).

## Updates in Microsoft JDBC Driver 6.4 for SQL Server

The Microsoft JDBC Driver 6.4 for SQL Server is fully compliant with JDBC specifications 4.1 and 4.2. The jars in the 6.4 package are named according to Java version compatibility. For example, the mssql-jdbc-6.4.0.jre8.jar file from the 6.4 package must be used with Java 8.

### Support for JDK 9

Support for Java Development Kit (JDK) version 9.0 in addition to JDK 8.0 and 7.0.

### JDBC 4.3 compliance

Support for Java Database Connectivity API 4.3 specification, in addition to 4.1 and 4.2. The JDBC 4.3 API methods are added but not implemented yet. For details see [JDBC 4.3 Compliance for the JDBC Driver](../../connect/jdbc/jdbc-4-3-compliance-for-the-jdbc-driver.md).

### Added new connection property: sslProtocol

Added a new connection property that lets users specify the TLS protocol keyword. Possible values are: "TLS", "TLSv1", "TLSv1.1", "TLSv1.2". See [SSLProtocol](https://github.com/Microsoft/mssql-jdbc/wiki/SSLProtocol) for details.

### Deprecated connection property: fipsProvider

Connection property "fipsProvider" is removed from the list of accepted connection properties. See the details [Here](https://github.com/Microsoft/mssql-jdbc/pull/460).

### Added connection properties for specifying custom TrustManager

Driver now supports specifying custom TrustManager with added "trustManagerClass" and "trustManagerConstructorArg" connection properties. This allows for dynamic specification of a set of certificates that are trusted on a per connection basis without modifying the global settings for the JVM environment.

### Added support for datetime/smallDatetime in Table-Valued Parameters (TVP)

Driver now supports datatypes DATETIME and SMALLDATETIME when using Table-Valued Parameters (TVP).

### Added support for sql_variant datatype

The JDBC Driver now supports sql_variant datatypes to be used with SQL Server. Sql_variant is also supported with features such as Table-Valued Parameters (TVP) and BulkCopy with below limitations:

1. For Date values:
    When using TVP to populate a table that contains datetime/smalldatetime/date values stored in sql_variant column, calling getDateTime()/getSmallDateTime()/getDate() methods on resultset doesn't work and throws the following exception:
    `java java.lang.String cannot be cast to java.sql.Timestamp`
    Workaround: use "getString()" or "getObject()" methods instead.

2. Using TVP with SQL Variant for null values

If you're using TVP to populate a table and send NULL value to sql_variant column type, you'll encounter an exception as inserting NULL value with column type sql_variant in TVP is currently not supported.

### Implemented Prepared Statement Metadata Caching

The JDBC Driver has implemented Prepared Statement Metadata Caching for performance improvement. Driver now supports caching Prepared Statement metadata in the driver with "disableStatementPooling" and "statementPoolingCacheSize" connection properties. This feature is disabled by default. More information can be found [here](../../connect/jdbc/prepared-statement-metadata-caching-for-the-jdbc-driver.md)

### Added support for AAD Integrated Authentication on Linux/Mac

The JDBC Driver now also supports Azure Active Directory Integrated Authentication on all supported Operating Systems (Windows/Linux/Mac) with Kerberos. Alternatively, on Windows Operating Systems, users can authenticate with sqljdbc_auth.dll.

### Updated ADAL4J version to 1.4.0

The JDBC Driver has updated its maven dependency upon azure-activedirectory-library-for-java (ADAL4J) to version 1.4.0. For more information about dependencies, see [here](../../connect/jdbc/feature-dependencies-of-microsoft-jdbc-driver-for-sql-server.md)

## Updates in Microsoft JDBC Driver 6.2 for SQL Server

The Microsoft JDBC Driver 6.2 for SQL Server is fully compliant with JDBC specifications 4.1 and 4.2. The jars in the 6.2 package are named according to Java version compatibility. For example, the mssql-jdbc-6.2.2.jre8.jar file from the 6.2 package is recommended to be used with Java 8.

> [!NOTE]  
> An issue with the metadata caching improvement was found in the JDBC 6.2 RTW released on June 29, 2017. The improvement was rolled back and new jars (version 6.2.1) were released on July 17, 2017. 
>
> Another improvement to upgrade Azure Key Vault dependent library version to 1.0.0 was made, and new jars (version 6.2.2) were released on October 19, 2017.
>
> Download the latest updates in JDBC Driver 6.2 on [Microsoft Download Center](https://go.microsoft.com/fwlink/?linkid=852460), [GitHub](https://github.com/Microsoft/mssql-jdbc/releases/tag/v6.2.2), and [Maven Central](https://search.maven.org/#search%7Cgav%7C1%7Cg%3A%22com.microsoft.sqlserver%22%20AND%20a%3A%22mssql-jdbc%22). Please update your projects to use the 6.2.2 release jars. Please view release notes for [v6.2.1](https://github.com/Microsoft/mssql-jdbc/releases/tag/v6.2.1) and [v6.2.2](https://github.com/Microsoft/mssql-jdbc/releases/tag/v6.2.2) for more details.

### Azure Active Directory (AAD) support for Linux

Connect your Linux applications to Azure SQL Database using AAD authentication via username/password and access token methods.

### Federal Information Processing Standard (FIPS) enabled JVMs

The JDBC Driver can now be used on JVMs that run in FIPS 140 compliance mode to meet federal standards and compliance.

### Kerberos Authentication Improvements

The JDBC Driver now has support for:

- Principal/Password method for applications where the Kerberos configuration can't be modified or unable to retrieve a new token or keytab. This method can be used for authenticating to a SQL Server that only allows Kerberos authentication.
- Cross-realm authentication using Kerberos Integrated authentication without explicitly setting the server SPN. The driver now automatically computes the REALM even when it hasn't been provided.
- Kerberos Constrained Delegation by accepting impersonated user credentials as a GSS Credential object via data source. This impersonated credential is then used to establish a Kerberos connection.

### Added Timeouts

The JDBC Driver now supports the following configurable timeouts you can change based on your application's needs:

- Query Timeout to control the number of seconds to wait before a timeout occurs when running a query.
- Socket Timeout to specify the number of milliseconds to wait before a timeout occurs on a socket read or accept.

## Updates in Microsoft JDBC Driver 6.1 for SQL Server

The Microsoft JDBC Driver 6.1 for SQL Server is fully compliant with JDBC specifications 4.1 and 4.2. This is the initial open-source release of the JDBC Driver and contains the mssql-jdbc-6.1.0.jre8.jar mssql-jdbc-6.1.0.jre7.jar files, which correspond to the Java version compatibility.

## Updates in Microsoft JDBC Driver 6.0 for SQL Server

The Microsoft JDBC Driver 6.0 for SQL Server is fully compliant with JDBC specifications 4.1 and 4.2. The jars in the 6.0 package are named according to their compliance with the JDBC API version. For example, the sqljdbc42.jar file from the 6.0 package is JDBC API 4.2 compliant. Similarly, the sqljdbc41.jar file is compliant with JDBC API 4.1.

To ensure you have the right sqljdbc42.jar or sqljdbc41.jar, run the following lines of code. If the output is "Driver version: 6.0.7507.100", you have the JDBC Driver 6.0 package.

```java
Connection conn = DriverManager.getConnection("jdbc:sqlserver://<server>;user=<user>;password=<password>;");
System.out.println("Driver version: " + conn.getMetaData().getDriverVersion());
```

### Always Encrypted

Support for the recently released Always Encrypted feature in SQL Server 2016, a new security feature that ensures sensitive data is never seen in plaintext in a SQL Server instance. Always Encrypted works by transparently encrypting the data in the application, so that SQL Server will only handle the encrypted data and not plaintext values. Even if the SQL instance or the host machine is compromised, all an attacker can get is ciphertext of sensitive data. For details, see [Using Always Encrypted with the JDBC Driver](../../connect/jdbc/using-always-encrypted-with-the-jdbc-driver.md).

### Internationalized Domain Name (IDN)

Support for Internationalized Domain Names (IDNs) for server names. For details see Using International Domain Names on the [International Features of the JDBC Driver](../../connect/jdbc/international-features-of-the-jdbc-driver.md) page.

### Parameterized Query

Now supports retrieving parameter metadata with prepared statements for complex queries such as subqueries and/or joins. Note that this improvement is available only when using SQL Server 2012 and newer versions.

### Azure Active Directory (AAD)

AAD authentication is a mechanism of connecting to Azure SQL Database v12 using identities in AAD. Use AAD authentication to centrally manage identities of database users and as an alternative to SQL Server authentication. The JDBC Driver 6.0 allows you to specify your AAD credentials in the JDBC connection string to connect to Azure SQL DB. For details see the authentication property on the [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md) page.

### Table-Valued Parameters

Table-valued parameters provide an easy way to marshal multiple rows of data from a client application to SQL Server without requiring multiple round trips or special server-side logic for processing the data. You can use table-valued parameters to encapsulate rows of data in a client application and send the data to the server in a single parameterized command. The incoming data rows are stored in a table variable that can then be operated on by using Transact-SQL. For details, see [Using Table-Valued Parameters](../../connect/jdbc/using-table-valued-parameters.md).

### AlwaysOn Availability Groups (AG)

The driver now supports transparent connections to AlwaysOn Availability Groups. The driver quickly discovers the current AlwaysOn topology of your server infrastructure and connects to the current active server transparently.

## Updates in Microsoft JDBC Driver 4.2 for SQL Server and later

The Microsoft JDBC Driver 4.2 for SQL Server is fully compliant with JDBC specifications 4.1 and 4.2. The jars in the 4.2 package are named according to their compliance with the JDBC API version. For example, the sqljdbc42.jar file from the 4.2 package is JDBC API 4.2 compliant. Similarly, the sqljdbc41.jar file is compliant with JDBC API 4.1.

To ensure you have the right sqljdbc42.jar or sqljdbc41.jar, run the following lines of code. If the output is "Driver version: 4.2.6420.100", you have the JDBC Driver 4.2 package.

```java
Connection conn = DriverManager.getConnection("jdbc:sqlserver://<server>;user=<user>;password=<password>;");
System.out.println("Driver version: " + conn.getMetaData().getDriverVersion());
```

### Support for JDK 8

Support for Java Development Kit (JDK) version 8.0 in addition to JDK 7.0, 6.0, and 5.0.

### JDBC 4.1 and 4.2 compliance

Support for Java Database Connectivity API 4.1 and 4.2 specifications, in addition to 4.0. For details, see [JDBC 4.1 Compliance for the JDBC Driver](../../connect/jdbc/jdbc-4-1-compliance-for-the-jdbc-driver.md) and [JDBC 4.2 Compliance for the JDBC Driver](../../connect/jdbc/jdbc-4-2-compliance-for-the-jdbc-driver.md).

### Bulk copy

The bulk copy feature is used to quickly copy large amounts of data into tables or views in SQL Server databases. For details, see [Using Bulk Copy with the JDBC Driver](../../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md).

### XA transaction rollback option

Added new timeout options for existing automatic rollback of unprepared transactions. For details, see [Understanding XA Transactions](../../connect/jdbc/understanding-xa-transactions.md).

### New Kerberos Principal Connection Property

Added a new connection property to facilitate flexibility with Kerberos connections. For details, see [Using Kerberos Integrated Authentication to Connect to SQL Server](../../connect/jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server.md).

## Updates in Microsoft JDBC Driver 4.1 for SQL Server and later

### Support for JDK 7

Support for Java Development Kit (JDK) version 7.0 in addition to JDK 6.0 and 5.0.

## Itanium Not Supported for JDBC Driver 6.4, 6.0, 4.2, and 4.1 Applications

Microsoft JDBC Drivers 6.4, 6.0, 4.2, and 4.1 for SQL Server applications aren't supported to run on an Itanium computer.

## See Also

[Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)
