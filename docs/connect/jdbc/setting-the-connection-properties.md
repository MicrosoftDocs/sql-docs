---
title: Set the Connection Properties
description: The connection string properties for the Microsoft JDBC Driver for SQL Server can be specified in various ways.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, machavan, sunilbs
ms.date: 08/27/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
---

# Set the connection properties

[!INCLUDE [Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

You can specify the connection string properties in various ways:

- As `<name>=<value>` properties in the connection URL when you connect by using the DriverManager class. For connection string syntax, see [Building the connection URL](building-the-connection-url.md).
- As `<name>=<value>` properties in the *Properties* parameter of the [Connect method in the DriverManager class](https://javadoc.io/doc/com.microsoft.sqlserver/mssql-jdbc/latest/com.microsoft.sqlserver.jdbc/com/microsoft/sqlserver/jdbc/SQLServerDriver.html#connect(java.lang.String,java.util.Properties)).
- As values in the appropriate setter method of the [data source of the driver](https://javadoc.io/doc/com.microsoft.sqlserver/mssql-jdbc/latest/com.microsoft.sqlserver.jdbc/com/microsoft/sqlserver/jdbc/ISQLServerDataSource.html). For example:

  ```java
  datasource.setServerName(value)
  datasource.setDatabaseName(value)
  ```

## Remarks

- Property names are case-insensitive. The driver resolves duplicate property names in the following order:

  1. API arguments, such as `user` and `password`
  1. Property collection
  1. Last instance in the connection string

- You can use unknown values for property names. The JDBC driver doesn't validate case sensitivity.

- You can use synonyms. The driver resolves them in order, just as it does with duplicate property names.

- The [!INCLUDE [jdbcNoVersion](../../includes/jdbcnoversion_md.md)] takes the server default values for connection properties except for `ANSI_DEFAULTS` and `IMPLICIT_TRANSACTIONS`. The [!INCLUDE [jdbcNoVersion](../../includes/jdbcnoversion_md.md)] automatically sets `ANSI_DEFAULTS` to `ON` and `IMPLICIT_TRANSACTIONS` to `OFF`.

- If you set authentication to `ActiveDirectoryPassword [DEPRECATED]`, include the following library in the classpath: [microsoft-authentication-library-for-java](https://github.com/AzureAD/microsoft-authentication-library-for-java). Find it on [Maven Repository](https://mvnrepository.com/artifact/com.microsoft.azure/msal4j). The simplest way to download the library and its dependencies is by using Maven:

  1. Install Maven on your system.
  1. Go to the [GitHub page](https://github.com/Microsoft/mssql-jdbc) of the driver.
  1. Download the `pom.xml` file.
  1. Run the following Maven command to download the library and its dependencies: `mvn dependency:copy-dependencies`.

## Properties

The following sections describe all the currently available connection string properties for the JDBC driver.

### `accessToken`

- **Type**: `String`
- **Default**: `null`

(Version 6.0+) Use this property to connect to a database using an access token. You can't set `accessToken` by using the connection URL.

### `accessTokenCallbackClass`

- **Type**: `String`
- **Default**: `null`

(Version 12.4+) The name of the callback-implementing class to use with the access token callback.

### `applicationIntent`

- **Type**: `String`
- **Default**: `ReadWrite`

(Version 6.0+) Declares the application workload type to connect to a server.

Possible values are `ReadOnly` and `ReadWrite`.

For more information about disaster recovery, see [JDBC driver support for High Availability, disaster recovery](jdbc-driver-support-for-high-availability-disaster-recovery.md).

### `applicationName`

- **Type**: `String` [`<=128 char`]
- **Default**: `null`

The application name, or "Microsoft JDBC Driver for SQL Server" if you don't provide a name.

Use this name to identify the specific application in various [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] profiling and logging tools.

### `authentication`

- **Type**: `String`
- **Default**: `NotSpecified`

(Version 6.0+) This optional property indicates which authentication method to use for connection.

Possible values are `ActiveDirectoryIntegrated`, `ActiveDirectoryManagedIdentity` (version 12.2+), `ActiveDirectoryMSI` (version 7.2+), `ActiveDirectoryInteractive` (version 9.2+), `ActiveDirectoryServicePrincipal` (version 9.2+), `ActiveDirectoryPassword [DEPRECATED]`, `SqlPassword`, and the default `NotSpecified`.

Use `ActiveDirectoryIntegrated` (version 6.0+) to connect to SQL using integrated Windows authentication.

Use `ActiveDirectoryManagedIdentity` (version 12.2+) or `ActiveDirectoryMSI` (version 7.2+) to connect to SQL from inside an Azure Resource. For example, an Azure Virtual Machine, App Service, or Function App using managed identity authentication.

The two types of managed identities supported by the driver when using `ActiveDirectoryManagedIdentity` or `ActiveDirectoryMSI` authentication mode are:

- *System-Assigned Managed Identity*: Used to acquire `accessToken` by default.

- *User-Assigned Managed Identity*: Used to acquire `accessToken` if the Client ID of a managed identity is specified with the `msiClientId` connection property.

Use `ActiveDirectoryInteractive` to connect to a database using an interactive authentication flow.

Use `ActiveDirectoryServicePrincipal` (version 9.2+) to connect to a database using the client ID and secret of a service principal identity. Specify client ID in the `userName` property and secret in the `password` property (10.2+).

Use `ActiveDirectoryServicePrincipalCertificate` (version 12.4+) to connect to a database using the client ID and certificate of a service principal identity. Specify client ID in the `userName` property and path to the certificate in the `clientCertificate` property.

For more options, see [Connect using ActiveDirectoryServicePrincipalCertificate authentication mode](connecting-using-azure-active-directory-authentication.md#connect-using-activedirectoryserviceprincipalcertificate-authentication-mode).

Use `ActiveDirectoryPassword [DEPRECATED]` to connect to SQL using a Microsoft Entra principal name and password.

ActiveDirectoryPassword is deprecated.

For more information, see [Connect using ActiveDirectoryPassword authentication mode](connecting-using-azure-active-directory-authentication.md#connect-using-activedirectorypassword-authentication-mode).

Use `SqlPassword` to connect to SQL using `userName`/`user` and `password` properties.

Use `NotSpecified` if none of these authentication methods are needed.

> [!IMPORTANT]  
> If authentication is set to ActiveDirectoryIntegrated, the following two libraries must be installed: `mssql-jdbc_auth-<version>-<arch>.dll` (available in the JDBC driver package) and Microsoft Authentication Library for SQL Server (`ADAL.DLL`). Microsoft Authentication Library can be installed from [Download ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md) or [Download Microsoft OLE DB Driver for SQL Server](../oledb/download-oledb-driver-for-sql-server.md). The JDBC driver only supports version **1.0.2028.318 and higher** for ADAL.DLL.

When you set the authentication property to any value other than `NotSpecified`, the driver uses Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), encryption by default.

For information on how to configure Microsoft Entra authentication, see [Microsoft Entra authentication for Azure SQL](/azure/azure-sql/database/authentication-aad-overview).

### `authenticationScheme`

- **Type**: `String`
- **Default**: `NativeAuthentication`

Indicates which kind of integrated security you want your application to use.

Possible values are `JavaKerberos`, `NTLM` (version 7.4+), and the default `NativeAuthentication`.

`NativeAuthentication` causes the driver to load `mssql-jdbc_auth-<version>-<arch>.dll` (for example, `mssql-jdbc_auth-8.2.2.x64.dll`) on Windows, which is used to obtain integrated authentication information.

(The native authentication library loaded is named `sqljdbc_auth.dll` when using driver versions 6.0 through 7.4.)

When using `authenticationScheme=JavaKerberos`, you must specify the fully qualified domain name (FQDN) in the `serverName` or `serverSpn` property. Otherwise, an error occurs (Server not found in Kerberos database).

For more information about using `authenticationScheme=JavaKerberos`, see [Using Kerberos integrated authentication to connect to SQL Server](using-kerberos-integrated-authentication-to-connect-to-sql-server.md).

When using `authenticationScheme=NTLM`, you must specify the Windows domain by using the `domain` or `domainName` property, the Windows credentials in the `user` or `userName` property, and the `password` property. Otherwise, an error occurs (connection properties must be specified).

### `bulkCopyForBatchInsertAllowEncryptedValueModifications`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 12.10+) When you set `useBulkCopyForBatchInsert` to `true`, set this option to `true` to enable bulk copying of encrypted data between tables or databases, without decrypting the data.

For more information and warnings about using this property, see the `allowEncryptedValueModifications` option in [SQLServerBulkCopyOptions](using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions).

### `bulkCopyForBatchInsertBatchSize`

- **Type**: `int`
- **Default**: `0`

(Version 12.10+) When you set `useBulkCopyForBatchInsert` to `true`, this property specifies the batch size for bulk copy operations that the driver creates from batch insert operations.

For more information about the effects of this setting, see the `BatchSize` option in [SQLServerBulkCopyOptions](using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions).

### `bulkCopyForBatchInsertCheckConstraints`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 12.10+) When you use `useBulkCopyForBatchInsert=true`, set this option to `true` to enable check constraints while inserting data. Set this option to `false` to disable check constraints.

For more information about the effects of this setting, see the `CheckConstraints` option in [SQLServerBulkCopyOptions](using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions).

### `bulkCopyForBatchInsertFireTriggers`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 12.10+) When you use `useBulkCopyForBatchInsert=true`, set this option to `true` to enable the firing of insert triggers while inserting rows into the database. Set this option to `false` to disable insert triggers.

For more information about the effects of this setting, see the `FireTriggers` option in [SQLServerBulkCopyOptions](using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions).

### `bulkCopyForBatchInsertKeepIdentity`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 12.10+) When you use `useBulkCopyForBatchInsert=true`, set this option to `true` to preserve source identity values while inserting data. Set the option to `false` to assign identity values by the destination.

For more information about the effects of this setting, see the `KeepIdentity` option in [SQLServerBulkCopyOptions](using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions).

### `bulkCopyForBatchInsertKeepNulls`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 12.10+) When you use `useBulkCopyForBatchInsert=true`, set this option to `true` to preserve null values in the destination table regardless of default value settings. Set this option to `false` to allow destination defaults to replace null values.

For more information about the effects of this setting, see the `KeepNulls` option in [SQLServerBulkCopyOptions](using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions).

### `bulkCopyForBatchInsertTableLock`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 12.10+) When you set `useBulkCopyForBatchInsert` to `true`, set this option to `true` to get a bulk update lock during the bulk copy operation. Set this option to `false` to use row locks.

For more information about the effects of this setting, see the `TableLock` option in [SQLServerBulkCopyOptions](using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions).

### `cacheBulkCopyMetadata`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 12.8+) When using `useBulkCopyForBatchInsert=true`, this property tells the driver whether it should cache destination column metadata at the connection level. If set to `true`, make sure the destination doesn't change between bulk inserts, as the driver doesn't have a way of handling this change.

### `calcBigDecimalPrecision`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 12.6+) Flag to indicate whether the driver should calculate precision for BigDecimal inputs, as opposed to using the maximum allowed value for precision (38).

### `cancelQueryTimeout`

- **Type**: `int`
- **Default**: `-1`

(Version 6.4+) Use this property to cancel a `queryTimeout` set on the connection. Query execution stops responding and doesn't throw an exception if the TCP connection to the server is silently dropped. This property is only applicable if `queryTimeout` is also set on the connection.

The driver waits the total amount of `cancelQueryTimeout` + `queryTimeout` seconds, to drop the connection and close the channel.

The default value for this property is -1 and behavior is to wait indefinitely.

### `clientCertificate`

- **Type**: `String`
- **Default**: `null`

(Version 8.4+) Specifies the location of the certificate to use for client certificate authentication. The JDBC driver supports PFX, PEM, DER, and CER file extensions.

For details, see [Client Certificate Authentication for Loopback Scenarios](client-certification-authentication-for-loopback-scenarios.md).

### `clientKey`

- **Type**: `String`
- **Default**: `null`

(Version 8.4+) Specifies the location of the private key for PEM, DER, and CER certificates specified by the `clientCertificate` attribute.

For details, see [Client Certificate Authentication for Loopback Scenarios](client-certification-authentication-for-loopback-scenarios.md).

### `clientKeyPassword`

- **Type**: `String`
- **Default**: `null`

(Version 8.4+) Specifies the optional password string for accessing the `clientKey` file's private key.

For details, see [Client Certificate Authentication for Loopback Scenarios](client-certification-authentication-for-loopback-scenarios.md).

### `columnEncryptionSetting`

- **Type**: `String` [`Enabled` | `Disabled`]
- **Default**: `Disabled`

(Version 6.0+) Set to `Enabled` to use the Always Encrypted (AE) feature. When AE is enabled, the JDBC driver transparently encrypts and decrypts sensitive data stored in encrypted database columns on the server.

For more information about Always Encrypted, see [Use Always Encrypted with the JDBC driver](using-always-encrypted-with-the-jdbc-driver.md).

> [!NOTE]  
> Always Encrypted is available with SQL Server 2016 and later versions, and Azure SQL Database.

### `concatNullYieldsNull`

- **Type**: `String` [`ON` | `OFF`]
- **Default**: `ON`

(Version 13.2+) When you set this option to `OFF`, the driver sets the database session variable `CONCAT_NULL_YIELDS_NULL` to `OFF` when it establishes the connection. The result is that concatenating a null value with a string yields the string itself (the null value is treated as an empty string).

For more information, see [SET CONCAT_NULL_YIELDS_NULL](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md).

### `connectRetryCount`

- **Type**: `int` [`0..255`]
- **Default**: `1`

(Version 9.4+) The number of reconnection attempts if there's a connection failure.

### `connectRetryInterval`

- **Type**: `int` [`1..60`]
- **Default**: `10`

(Version 9.4+) The number of seconds between each connection retry attempt.

### `databaseName, database`

- **Type**: `String` [`<=128 char`]
- **Default**: `null`

The name of the database to connect to.

If you don't specify a database name, the connection uses the default database.

### `datetimeParameterType`

- **Type**: `String` [`datetime` | `datetime2` | `datetimeoffset`]
- **Default**: `datetime2`

(Version 12.2+) The SQL data type to use for Java date and timestamp parameters.

When you connect to SQL Server 2016 and later versions and interact with legacy `datetime` values, set this property to `datetime`. This setting mitigates server-side conversion problems between `datetime` and `datetime2` values.

For more information, see [Addressing datetime to datetime2 conversion behavior change starting from SQL Server 2016](https://github.com/microsoft/mssql-jdbc/wiki/Addressing-datetime-to-datetime2-conversion-behavior-change-starting-from-SQL-Server-2016).

### `delayLoadingLobs`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `true`

Flag to indicate whether to stream all the LOB objects retrieved from the ResultSet. Setting this property to `false` loads the entire LOB object into memory without streaming.

### `disableStatementPooling`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `true`

Flag that indicates if statement pooling should be used.

### `domainName, domain`

- **Type**: `String`
- **Default**: `null`

(Version 7.4+) The Windows domain to authenticate to when using `NTLM` authentication.

### `enablePrepareOnFirstPreparedStatementCall`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

Set to `true` to enable prepared statement handle creation by calling `sp_prepexec` with the first execution of a prepared statement.

Set to `false` to change the first execution of a prepared statement to call `sp_executesql` and not prepare a statement. If a second execution happens, it calls `sp_prepexec` to set up a prepared statement handle.

### `enclaveAttestationProtocol`

- **Type**: `String`
- **Default**: `null`

(Version 8.2+) This optional property indicates the attestation protocol to use for Always Encrypted with secure enclaves. Currently, the only supported values for this field are `HGS`, `AAS`, and `NONE` (`NONE` is only supported in version 11.2+).

For more information about Always Encrypted with secure enclaves, see [Using Always Encrypted with secure enclaves with the JDBC driver](using-always-encrypted-with-secure-enclaves-with-the-jdbc-driver.md).

### `enclaveAttestationUrl`

- **Type**: `String`
- **Default**: `null`

(Version 8.2+) This optional property indicates the attestation service endpoint URL to use for Always Encrypted with secure enclaves.

For more information about Always Encrypted with secure enclaves, see [Using Always Encrypted with secure enclaves with the JDBC driver](using-always-encrypted-with-secure-enclaves-with-the-jdbc-driver.md).

### `encrypt`

- **Type**: `String`
- **Default**: `null`

Set to `true` to specify that the SQL Database Engine uses TLS encryption for all the data sent between the client and the server if the server has a certificate installed. The default value is `true` in version 10.2 and later and `false` in 9.4 and earlier.

In version 6.0 and up, there's a new connection setting `authentication` that uses TLS encryption by default.

For more information about this property, see the `authentication` property.

In version 11.2.0 and up, `encrypt` was changed from `Boolean` to `string`, allowing for TDS 8.0 support when the property is set to `strict`.

The default change in version 10.2 is a breaking change. If you're upgrading from 9.4 or earlier and your server doesn't have a valid TLS certificate, set `trustServerCertificate` to `true` or provide a valid certificate.

### `failoverPartner`

- **Type**: `String`
- **Default**: `null`

The name of the failover server used in a database mirroring configuration. This property is used for an initial connection failure to the principal server. After you make the initial connection, this property is ignored. Must be used with the `databaseName` property.

> [!NOTE]  
> The driver doesn't support the server instance port number for the failover partner instance as part of the `failoverPartner` property in the connection string. However, the driver does support specifying the `serverName`, `instanceName`, and `portNumber` properties of the principal server instance, and `failoverPartner` property of the failover partner instance, in the same connection string.

If you specify a Virtual Network Name in the `Server` connection property, you can't use database mirroring.

For more information about disaster recovery, see [JDBC driver support for High Availability, disaster recovery](jdbc-driver-support-for-high-availability-disaster-recovery.md).

### `fips`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

Set this property to `true` for FIPS-enabled Java Virtual Machine (JVM).

### `fipsProvider`

- **Type**: `String`
- **Default**: `null`

FIPS provider configured in JVM, such as BCFIPS or SunPKCS11-NSS. Removed in version 6.4.0.

For more information, see [GitHub issue 460](https://github.com/Microsoft/mssql-jdbc/pull/460).

### `gsscredential`

- **Type**: `org.ietf.jgss.GSSCredential`
- **Default**: `null`

(Version 6.2+) Pass user credentials for Kerberos Constrained Delegation in this property.

Use this setting with `integratedSecurity` as `true` and `JavaKerberos` as `authenticationScheme`.

### `hostNameInCertificate`

- **Type**: `String`
- **Default**: `null`

The host name to use to validate the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] TLS/SSL certificate.

The `hostNameInCertificate` option can be used to specify the host name in situations where the name, or names, used in the certificate doesn't match the name passed in to the `serverName` property. If there's a match however, the `hostNameInCertificate` option shouldn't be used.

In situations where the `hostNameInCertificate` property is unspecified or set to null, the [!INCLUDE [jdbcNoVersion](../../includes/jdbcnoversion_md.md)] uses the `serverName` property value on the connection URL as the host name to validate the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] TLS/SSL certificate.

> [!NOTE]  
> As the prior paragraph describes, don't set the `hostNameInCertificate` option unless you confirm the name or names in the certificate don't match the names you pass in the `serverName` option.

Use this property in combination with the `encrypt` and `authentication` properties, and the `trustServerCertificate` property. This property affects the certificate validation if the connection uses TLS encryption and the `trustServerCertificate` is set to `false`. Make sure the value you pass to `hostNameInCertificate` matches the Common Name (CN) or DNS name in the Subject Alternate Name (SAN) in the server certificate for a TLS connection to succeed.

For more information about encryption support, see [Understanding encryption support](understanding-ssl-support.md).

### `instanceName`

- **Type**: `String` [`<=128 char`]
- **Default**: `null`

The database instance name to connect to. When you don't specify this property, you connect to the default instance. For the case where you specify both the `instanceName` and port, see the notes for port.

If you specify a Virtual Network Name in the `Server` connection property, you can't use the `instanceName` connection property.

For more information about disaster recovery, see [JDBC driver support for High Availability, disaster recovery](jdbc-driver-support-for-high-availability-disaster-recovery.md).

### `integratedSecurity`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

Set to `true` to indicate that Windows credentials are used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Windows operating systems. If `true`, the JDBC driver searches the local computer credential cache for credentials that were provided when a user signed in to the computer or network.

Set to `true` (with `authenticationscheme=JavaKerberos`), to indicate that Kerberos credentials are used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

For more information about Kerberos authentication, see [Using Kerberos integrated authentication to connect to SQL Server](using-kerberos-integrated-authentication-to-connect-to-sql-server.md).

Set to `true` (with `authenticationscheme=NTLM`), to indicate that NTLM credentials are used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

If `false`, the username and password must be supplied.

### `ipaddresspreference`

- **Type**: `String` [`<=128 char`]
- **Default**: `IPv4First`

The IP preference used by the client application.

With `IPV4First`, the driver traverses IPv4 addresses first. If no IPv4 addresses can be connected to successfully, the driver continues and tries IPv6 addresses, if there are any.

With `IPV6First`, the driver traverses IPv6 addresses first. If no IPv6 addresses can be connected to successfully, the driver continues and tries IPv4 addresses, if there are any.

With `UsePlatformDefault`, the driver traverses all IP addresses in their initial orders from DNS resolution.

### `jaasConfigurationName`

- **Type**: `String`
- **Default**: `SQLJDBCDriver`

(Version 6.2+) Each connection to SQL Server can use its own JAAS Login Configuration name to establish a Kerberos connection. You can pass the name of the configuration entry through this property. Use this property when [creating a Kerberos configuration file](using-kerberos-integrated-authentication-to-connect-to-sql-server.md#creating-a-kerberos-configuration-file). By default, the driver looks for the name `SQLJDBCDriver`.

If the driver doesn't find an external configuration, it sets `useDefaultCcache=true` for IBM JVMs, and `useTicketCache=true` for other JVMs.

### `keyStoreAuthentication`

- **Type**: `String`
- **Default**: `null`

(Version 6.0+) This property identifies which key store to use with Always Encrypted and determines an authentication mechanism used to authenticate to the key store. The driver supports setting up the Java Key Store seamlessly when you set `keyStoreAuthentication=JavaKeyStorePassword`. To use this property, you also must set the `keyStoreLocation` and `keyStoreSecret` properties for the Java Key Store.

Beginning with Microsoft JDBC Driver 8.4, you can set `keyStoreAuthentication=KeyVaultManagedIdentity` or `keyStoreAuthentication=KeyVaultClientSecret` to authenticate to Azure Key Vault using Managed Identities.

For more information about Always Encrypted, see [Use Always Encrypted with the JDBC driver](using-always-encrypted-with-the-jdbc-driver.md).

### `keyStoreLocation`

- **Type**: `String`
- **Default**: `null`

(Version 6.0+) When `keyStoreAuthentication=JavaKeyStorePassword`, the `keyStoreLocation` property identifies the path to the Java keystore file that stores the Column Master Key to use with Always Encrypted data. The path must include the keystore filename.

For more information about Always Encrypted, see [Use Always Encrypted with the JDBC driver](using-always-encrypted-with-the-jdbc-driver.md).

### `keyStorePrincipalId`

- **Type**: `String`
- **Default**: `null`

(Version 8.4+) When `keyStoreAuthentication=KeyVaultManagedIdentity`, the `keyStorePrincipalId` property specifies a valid Microsoft Entra application client ID.

For more information about Always Encrypted, see [Use Always Encrypted with the JDBC driver](using-always-encrypted-with-the-jdbc-driver.md).

### `keyStoreSecret`

- **Type**: `String`
- **Default**: `null`

(Version 6.0+) When `keyStoreAuthentication=JavaKeyStorePassword`, the `keyStoreSecret` property identifies the password to use for the keystore and the key. When you use the Java Key Store, the keystore and the key password must be the same.

For more information about Always Encrypted, see [Use Always Encrypted with the JDBC driver](using-always-encrypted-with-the-jdbc-driver.md).

### `lastUpdateCount`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `true`

A `true` value returns only the last update count from a SQL statement that you pass to the server. Use this value with only single `SELECT`, `INSERT`, or `DELETE` statements to ignore other update counts that server triggers can cause. Set this property to `false` to return all update counts, including those that server triggers return.

> [!NOTE]  
> This property only applies when you use it with the [executeUpdate](reference/executeupdate-method-sqlserverstatement.md) methods. All other execute methods return all results and update counts. This property only affects update counts that server triggers return. It doesn't affect result sets or errors that result as part of trigger execution.

### `lockTimeout`

- **Type**: `int`
- **Default**: `-1`

The number of milliseconds to wait before the database reports a lock timeout. The default behavior is to wait indefinitely. If you don't specify a value for this property, this value is the default for all statements on the connection.

Alternatively, use `Statement.setQueryTimeout()` to set the query timeout for specific statements. The value can be 0, which specifies no wait.

### `loginTimeout`

- **Type**: `int` [`0..65535`]
- **Default**: `30` (version 11.2 and later), or `15` (version 10.2 and earlier)

The number of seconds the driver should wait before timing out a failed connection. A zero value indicates that the timeout is the default system timeout. This value is either 30 seconds (the default in version 11.2 and later) or 15 seconds (the default in version 10.2 and earlier). A nonzero value is the number of seconds the driver should wait before timing out a failed connection.

If you specify a Virtual Network Name in the `Server` connection property, specify a timeout value of three minutes or more to allow sufficient time for a failover connection to succeed.

For [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, `loginTimeout` bounds the driver's connection retries, not just a single attempt. The driver abandons the retry when the elapsed time plus `connectRetryInterval` reaches `loginTimeout`, so size it to hold the retries you want. For more information, see [Connect to an auto-paused serverless database](connection-resiliency.md#connect-to-an-auto-paused-serverless-database).

For more information about disaster recovery, see [JDBC driver support for High Availability, disaster recovery](jdbc-driver-support-for-high-availability-disaster-recovery.md).

### `maxResultBuffer`

- **Type**: `String`
- **Default**: `null`

(Version 9.2+) Use `maxResultBuffer` to set the maximum number of bytes to read when reading a result set. If you don't specify this value, the driver reads the entire result set. You can specify the size in two styles:

- As a size in bytes (for example, `100`, `150M`, `300K`, `400G`).
- As a percentage of maximum heap memory (for example, `10p`, `15pct`, `20percent`).

### `msiClientId`

- **Type**: `String`
- **Default**: `null`

(Deprecated) (Version 7.2+) The Client ID of the Managed Identity (MSI) used to acquire an `accessToken` to establish a connection using the `ActiveDirectoryManagedIdentity` or `ActiveDirectoryMSI` authentication mode.

### `multiSubnetFailover`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

Always specify `multiSubnetFailover=true` when the target is Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, an availability group listener of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] availability group, or a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster Instance. The driver attempts TCP connections to all resolved IP addresses in parallel and uses the first connection that succeeds. When DNS resolves to one address, the driver does a single connection attempt and doesn't start any parallel connection threads.

Possible values are `true` and `false`.

For more information about disaster recovery, see [JDBC driver support for High Availability, disaster recovery](jdbc-driver-support-for-high-availability-disaster-recovery.md).

You can programmatically access the `multiSubnetFailover` connection property using [getPropertyInfo](reference/getpropertyinfo-method-sqlserverdriver.md), [getMultiSubnetFailover](reference/getmultisubnetfailover-method-sqlserverdatasource.md), and [setMultiSubnetFailover](reference/setmultisubnetfailover-method-sqlserverdatasource.md).

> [!NOTE]  
> Beginning with Microsoft JDBC Driver 6.0 for SQL Server, you no longer need to set `multiSubnetFailover` to `true` to connect to an availability group listener. A new property, `transparentNetworkIPResolution`, which is enabled by default, provides the detection of and connection to the active server.

### `packetSize`

- **Type**: `int` [`-1` | `0` | `512..32767`]
- **Default**: `8000`

The network packet size used to communicate with the server, specified in bytes. A value of -1 indicates to use the server's default packet size. A value of 0 indicates to use the maximum value of 32767. If you set this property to a value outside the acceptable range, an exception occurs.

> [!IMPORTANT]  
> Don't use the `packetSize` property when encryption is enabled (`encrypt=true`). Otherwise, the driver might raise a connection error.

For more information about this property, see the [setPacketSize](reference/setpacketsize-method-sqlserverdatasource.md) method of the [SQLServerDataSource](reference/sqlserverdatasource-class.md) class.

### `password`

- **Type**: `String` [`<=128 char`]
- **Default**: `null`

The database password, if you connect with a SQL user and password.

For Kerberos connection with principal name and password, set this property to the Kerberos Principal password.

(Version 10.2+) When `authentication=ActiveDirectoryServicePrincipal`, the `password` property identifies the password to use for the Active Directory principal.

### `portNumber, port`

- **Type**: `int` [`0..65535`]
- **Default**: `1433`

The port where the server is listening. If you specify the port number in the connection string, no request to SQLbrowser is made. When you specify both the port and `instanceName`, the connection is made to the specified port. However, the `instanceName` is validated and an error is thrown if it doesn't match the port.

> [!IMPORTANT]  
> Always specify the port number, as it's more secure than using SQLbrowser.

### `prepareMethod`

- **Type**: `String` [`prepexec` | `prepare` | `scopeTempTablesToConnection` | `none`]
- **Default**: `prepexec`

(Version 11.2.0+) Specifies the underlying prepare method that the driver uses with prepared statements.

Set to `prepare` to use `sp_prepare` as the prepare method. Setting `prepareMethod` to this value results in a separate, initial trip to the database to prepare the statement without any initial values for the database to consider in the execution plan. Set to `prepexec` to use `sp_prepexec` as the prepare method. This method combines the prepare action with the first execute, reducing round trips. It also provides the database with initial parameter values that the database can consider in the execution plan.

(Version 13.4.0+) Set to `scopeTempTablesToConnection` to scope temporary tables created in prepared statements to the connection by using literal parameter substitution instead of server-side prepared handles. Set to `none` to enforce literal parameter substitution with SQL batch execution, bypassing server-side prepared statement handles (`sp_prepexec` / `sp_prepare`).

**Limitations and disclaimers for `scopeTempTablesToConnection` and `none`**:

These `prepareMethod` options are intended for compatibility and migration scenarios, not for general performance use.

- No server-side prepared statements; SQL is always executed as a batch.
- Requires `FORCED_PARAMETERIZATION` for effective plan reuse.
- Parameters are inlined as literals rather than bound types.
- Numeric precision and scale might differ from server-side parameter binding.
- Date and time values are formatted as strings by the driver.
- Large string parameters increase SQL text size and memory usage.
- BLOB and CLOB parameters can cause high memory usage or out-of-memory conditions.
- SQL Server infers parameter data types at SQL parse time.
- Query plans might vary due to differences in literal values.
- Errors are detected at execution time rather than at bind time.
- Executed SQL includes literal values and is visible in server traces and logs.

### `queryTimeout`

- **Type**: `int`
- **Default**: `-1`

The number of seconds to wait before a timeout happens on a query. The default value is -1, which means infinite timeout. Set this value to 0 also implies to wait indefinitely.

### `quotedIdentifier`

- **Type**: `String` [`ON` | `OFF`]
- **Default**: `ON`

(Version 13.2+) When you set this option to `OFF`, the driver sets the database session variable `QUOTED_IDENTIFIER` to `OFF` when it establishes the connection. The database treats double quotation marks as string delimiters for character literals, and you can't enclose identifiers in double quotes.

For more information, see [SET QUOTED_IDENTIFIER](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

### `realm`

- **Type**: `String`
- **Default**: `null`

(Version 9.4+) The realm for Kerberos authentication. Set this value to override the Kerberos authentication realm the driver autodetects from the server's realm.

### `replication`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 9.4+) This setting tells the server if the connection is used for replication. When enabled, triggers with the `NOT FOR REPLICATION` option don't fire on the connection.

### `responseBuffering`

- **Type**: `String` [`full` | `adaptive`]
- **Default**: `adaptive`

If you set this property to `adaptive`, the driver buffers the minimum amount of data when necessary. The default mode is `adaptive`.

If you set this property to `full`, the driver reads the entire result set from the server when it executes a statement.

> [!NOTE]  
> Starting with JDBC driver version 1.2, the default buffering behavior is `adaptive`. To use the version 1.2 default behavior in your application, set the `responseBuffering` property to `full` either in the connection properties or with the [setResponseBuffering](reference/setresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement Class](reference/sqlserverstatement-class.md) object.

### `selectMethod`

- **Type**: `String` [`direct` | `cursor`]
- **Default**: `direct`

If you set this property to `cursor`, the driver creates a database cursor for each query it creates on the connection for `TYPE_FORWARD_ONLY` and `CONCUR_READ_ONLY` cursors. Typically, you only need this property if your application generates large result sets that can't fit entirely in client memory. If you set this property to `cursor`, the driver keeps only a limited number of result set rows in client memory.

By default, the driver keeps all result set rows in client memory. This default behavior provides the fastest performance when the application processes all rows.

### `sendStringParametersAsUnicode`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `true`

If you set the `sendStringParametersAsUnicode` property to `true`, the driver sends string parameters to the server in Unicode format.

If you set the `sendStringParametersAsUnicode` property to `false`, the driver sends string parameters to the server in non-Unicode formats such as ASCII or MBCS instead of Unicode.

The default value for the `sendStringParametersAsUnicode` property is `true`.

> [!NOTE]  
> The driver checks the `sendStringParametersAsUnicode` property only when sending a parameter value with `CHAR`, `VARCHAR`, or `LONGVARCHAR` JDBC types. The new JDBC 4.0 national character methods include methods such as `setNString`, `setNCharacterStream`, and `setNClob` of the [SQLServerPreparedStatement Class](reference/sqlserverpreparedstatement-class.md) and [SQLServerCallableStatement Class](reference/sqlservercallablestatement-class.md) classes. These methods always send their parameter values to the server in Unicode regardless of the setting of this property.

For optimal performance with the `CHAR`, `VARCHAR`, and `LONGVARCHAR` JDBC data types, an application should set the `sendStringParametersAsUnicode` property to `false` and use the `setString`, `setCharacterStream`, and `setClob` non-national character methods of the [SQLServerPreparedStatement Class](reference/sqlserverpreparedstatement-class.md) and [SQLServerCallableStatement Class](reference/sqlservercallablestatement-class.md) classes.

When the application sets the `sendStringParametersAsUnicode` property to `false` and uses a non-national character method to access Unicode data types on the server side (such as `nchar`, `nvarchar`, and `ntext`), some data might be lost if the database collation doesn't support the characters in the `String` parameters passed by the non-national character method.

An application should use the `setNString`, `setNCharacterStream`, and `setNClob` national character methods of the [SQLServerPreparedStatement Class](reference/sqlserverpreparedstatement-class.md) and [SQLServerCallableStatement Class](reference/sqlservercallablestatement-class.md) classes for the `NCHAR`, `NVARCHAR`, and `LONGNVARCHAR` JDBC data types.

Changing this value can affect sorting of results from the database. The sorting differences are due to different sorting rules for Unicode versus non-Unicode characters.

### `sendTemporalDataTypesAsStringForBulkCopy`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `true`

(Version 8.4+) When you set this connection property to `false`, the driver sends `DATE`, `DATETIME`, `DATETIME2`, `DATETIMEOFFSET`, `SMALLDATETIME`, and `TIME` datatypes as their respective types instead of sending them as `String`.

When you set this connection property to `false`, the driver accepts the default string literal format of each temporal data type, for example:

- `DATE`: `YYYY-MM-DD`
- `DATETIME`: `YYYY-MM-DD hh:mm:ss[.nnn]`
- `DATETIME2`: `YYYY-MM-DD hh:mm:ss[.nnnnnnn]`
- `DATETIMEOFFSET`: `YYYY-MM-DD hh:mm:ss[.nnnnnnn] [{+/-}hh:mm]`
- `SMALLDATETIME`: `YYYY-MM-DD hh:mm:ss`
- `TIME`: `hh:mm:ss[.nnnnnnn]`

### `sendTimeAsDatetime`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `true`

This property was added in SQL Server JDBC Driver 3.0.

- Set to `true` to send java.sql.Time values to the server as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] `datetime` values.

- Set to `false` to send java.sql.Time values to the server as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] `time` values.

The default value for this property is currently `true` and might change in a future release.

For more information about how the [!INCLUDE [jdbcNoVersion](../../includes/jdbcnoversion_md.md)] configures java.sql.Time values before it sends them to the server, see [Configuring how java.sql.Time values are sent](configuring-how-java-sql-time-values-are-sent-to-the-server.md).

### `serverCertificate, server`

- **Type**: `String`
- **Default**: `null`

(Version 11.2.0+) The path to the server certificate file. The driver uses this certificate for validation when you set `encrypt` to `strict`. The driver supports certificate files that use the PEM file format.

### `serverName, server`

- **Type**: `String`
- **Default**: `null`

The computer running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or an Azure SQL database.

You can also specify the Virtual Network Name of an availability group.

For more information about disaster recovery, see [JDBC driver support for High Availability, disaster recovery](jdbc-driver-support-for-high-availability-disaster-recovery.md).

### `serverNameAsACE`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 6.0+) Set to `true` to indicate that the driver should translate the Unicode server name to ASCII compatible encoding (Punycode) for the connection. If this setting is `false`, the driver uses the server name as provided to connect.

For more information about international features, see [International features of the JDBC driver](international-features-of-the-jdbc-driver.md).

### `serverPreparedStatementDiscardThreshold`

- **Type**: `int`
- **Default**: `10`

(Version 6.2+) Use this property to control how many outstanding prepared statement discard actions (`sp_unprepare`) can be outstanding per connection before the driver cleans up the outstanding handles on the server.

If you set this property to `<= 1`, the driver immediately executes unprepare actions on prepared statement close. If you set the property to `> 1`, the driver batches these calls together to avoid the overhead of calling `sp_unprepare` too often.

### `serverSpn`

- **Type**: `String`
- **Default**: `null`

(Version 4.2+) Use this optional property to specify the Service Principal Name (SPN) for a Java Kerberos connection. Use it with `authenticationScheme`.

To specify the SPN, use the form: `MSSQLSvc/fqdn:port@REALM` where fqdn is the fully qualified domain name, port is the port number, and REALM is the Kerberos realm of the SQL Server in uppercase letters.

> [!NOTE]  
> The `@REALM` is optional if the default realm of the client (as specified in the Kerberos configuration) is the same as the Kerberos realm for the SQL Server.

For more information about using `serverSpn` with Java Kerberos, see [Using Kerberos integrated authentication to connect to SQL Server](using-kerberos-integrated-authentication-to-connect-to-sql-server.md).

### `socketFactoryClass`

- **Type**: `String`
- **Default**: `null`

(Version 8.4+) Specifies the class name for a custom socket factory to use instead of the default socket factory.

### `socketTimeout`

- **Type**: `int`
- **Default**: `0`

The number of milliseconds to wait before a timeout occurs on a socket read or accept. The default value is 0, which means infinite timeout.

### `statementPoolingCacheSize`

- **Type**: `int`
- **Default**: `0`

(Version 6.4+) Use this property to enable prepared statement handle caching in the driver.

This property defines the size of the cache for statement pooling.

Use this property only with the `disableStatementPooling` connection property, which you should set to `false`. Setting `disableStatementPooling` to `true` or `statementPoolingCacheSize` to 0 disables prepared statement handle caching.

### `sslProtocol`

- **Type**: `String`
- **Default**: `TLS`

(Version 6.4+) Use this property to specify the TLS protocol to consider during secure connection.

Possible values are: `TLS`, `TLSv1`, `TLSv1.1`, and `TLSv1.2`.

For more information about the Secure Sockets Layer protocol, see [SSLProtocol](https://github.com/Microsoft/mssql-jdbc/wiki/SSLProtocol).

### `transparentNetworkIPResolution`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `true`

(Version 6.0+) This property provides faster detection of and connection to the active server. Set it to `true` or `false`. The default value is `true`.

Before Microsoft JDBC Driver 6.0 for SQL Server, an application had to set the connection string to include `multiSubnetFailover=true` to indicate that it was connecting to an Always On Availability Group. Without setting the `multiSubnetFailover` connection keyword to `true`, an application might experience a timeout while connecting to an Always On Availability Group. Starting with version 6.0, an application isn't required to set `multiSubnetFailover` to `true` anymore.

> [!NOTE]  
> When you use federated authentication or specify `multisubnetfailover`, the driver disables `transparentNetworkIPResolution` by default. To enable this feature, explicitly set `transparentNetworkIPResolution` to `true`.

When `transparentNetworkIPResolution=true`, the first connection attempt uses 500 milliseconds as the timeout. Any later attempts use the same timeout logic as used by the `multiSubnetFailover` property.

### `trustManagerClass`

- **Type**: `String`
- **Default**: `null`

(Version 6.4+) The fully qualified class name of a custom `javax.net.ssl.TrustManager` implementation.

### `trustManagerConstructorArg`

- **Type**: `String`
- **Default**: `null`

(Version 6.4+) An optional argument to pass to the constructor of the TrustManager. If you specify the `trustManagerClass` property and request an encrypted connection, the driver uses the custom TrustManager instead of the default system JVM keystore-based TrustManager.

### `trustServerCertificate`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

Set to `true` to specify that the driver doesn't validate the server TLS/SSL certificate.

- If `true`, the server TLS/SSL certificate is automatically trusted when the communication layer is encrypted using TLS.

- If `false`, the driver validates the server TLS/SSL certificate. If the server certificate validation fails, the driver raises an error and closes the connection. The default value is `false`. Make sure the value passed to `serverName` exactly matches the Common Name (CN) or DNS name in the Subject Alternate Name in the server certificate for a TLS/SSL connection to succeed.

For more information about encryption support, see [Understanding encryption support](understanding-ssl-support.md).

> [!NOTE]  
> Use this property with the `encrypt` and `authentication` properties. This property only affects server TLS/SSL certificate validation if the connection uses TLS encryption.

### `trustStore`

- **Type**: `String`
- **Default**: `null`

The path (including filename) to the certificate `trustStore` file. The `trustStore` file contains the list of certificates that the client trusts.

When you don't specify this property or set it to null, the driver uses the trust manager factory's lookup rules to decide which certificate store to use.

**The default SunX509 TrustManagerFactory tries to find the trusted material in the following search order**:

1. A file specified by the `javax.net.ssl.trustStore` JVM system property.
1. `<java-home>/lib/security/jssecacerts` file.
1. `<java-home>/lib/security/cacerts` file.

For more information about the SUNX509 TrustManager Interface, see the SUNX509 TrustManager Interface documentation on the Sun Microsystems Web site.

> [!NOTE]  
> This property only affects the certificate `trustStore` lookup if the connection uses TLS encryption and the `trustServerCertificate` property is set to `false`.

### `trustStorePassword`

- **Type**: `String`
- **Default**: `null`

The password used to check the integrity of the `trustStore` data.

If you set the `trustStore` property but don't set the `trustStorePassword` property, the driver doesn't check the integrity of the `trustStore`.

When you don't specify the `trustStore` and `trustStorePassword` properties, the driver uses the JVM system properties, `javax.net.ssl.trustStore` and `javax.net.ssl.trustStorePassword`. If you don't specify the `javax.net.ssl.trustStorePassword` system property, the driver doesn't check the integrity of the `trustStore`.

If you don't set the `trustStore` property but set the `trustStorePassword` property, the JDBC driver uses the file that the `javax.net.ssl.trustStore` specifies as a trust store. The driver checks the integrity of the trust store by using the specified `trustStorePassword`. This setting is needed when the client application doesn't want to store the password in the JVM system property.

> [!NOTE]  
> The `trustStorePassword` property only affects the certificate `trustStore` lookup, if the connection uses TLS connection and the `trustServerCertificate` property is set to `false`.

### `trustStoreType`

- **Type**: `String`
- **Default**: `JKS`

Set this property to specify trust store type to be used for FIPS mode.

Possible values are either `PKCS12` or type defined by FIPS provider.

### `useBulkCopyForBatchInsert`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 9.2+) When you enable this connection property, the driver transparently uses the Bulk Copy API for batch insert operations that use `java.sql.PreparedStatement`. This feature can provide better performance.

This feature is disabled by default. Set this property to `true` to enable it.

> [!IMPORTANT]  
> This feature only supports fully parameterized `INSERT` queries. If you combine the `INSERT` queries with other SQL queries, or if the queries contain data in values, execution falls back to the basic batch insert operation.

For more information about how to use this property, see [Using bulk copy API for batch insert operation](use-bulk-copy-api-batch-insert-operation.md).

### `useDefaultGSSCredential`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 12.6+) Flag to indicate whether the driver should create the GSSCredential on behalf of the user for using Native GSS-API for Kerberos authentication.

### `useDefaultJaasConfig`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 12.6+) When the application exists alongside libraries that configure JAAS at the system level, set this property to `true` to allow the driver to use that same configuration to perform Kerberos authentication.

### `useFmtOnly`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

(Version 7.4+) Provides an alternative way to query Parameter Metadata from the server. Set this property to `true` to specify that the driver should use `SET FMTONLY` logic when querying Parameter Metadata. This feature is off by default, and it isn't recommended to use this property as `SET FMTONLY` is marked for deprecation. `useFmtOnly` is made available to use only as a workaround for known issues and limitations in [sp_describe_undeclared_parameters](../../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md).

This feature currently only supports single `SELECT/INSERT/UPDATE/DELETE` queries. Attempting to use this feature with unsupported/multiple queries causes the driver to attempt to parse the query, but most likely results in an exception.

For more information about this property, see [Retrieving ParameterMetaData via `useFmtOnly`](using-usefmtonly.md).

### `userName, user`

- **Type**: `String` [`<=128 char`]
- **Default**: `null`

The database user, if you connect by using a SQL user and password.

For Kerberos connection by using principal name and password, set this property to the Kerberos Principal name.

(Version 10.2+) When `authentication=ActiveDirectoryServicePrincipal`, the `userName` property specifies a valid Microsoft Entra secure client ID.

### `vectorTypeSupport`

- **Type**: `String` [`v2` | `v1` | `off`]
- **Default**: `v1`

(Version 13.2+) Set to `off` to specify that server sends vector types as string data in JSON format and `v1` to specify that server sends vector types of `FLOAT32` as vector data. The default value is `v1`.

(Version 13.4+) Set to `v2` to enable native vector type support for both `FLOAT32` and `FLOAT16` vectors. `FLOAT16` vectors use IEEE-754 half-precision serialization on the wire and are exposed as `Float[]` arrays in Java.

For more information, see [Use vector data type with the JDBC driver](use-vector-data-type.md).

### `workstationID`

- **Type**: `String` [`<=128 char`]
- **Default**: `<empty string>`

The workstation ID. Use this ID to identify the specific workstation in various profiling and logging tools.

If you don't specify a value, the default is `<empty string>`.

### `xopenStates`

- **Type**: `Boolean` [`true` | `false`]
- **Default**: `false`

Set to `true` to specify that the driver returns XOPEN-compliant state codes in exceptions.

The default is to return SQL 99 state codes.

## Related content

- [Connecting to SQL Server with the JDBC driver](connecting-to-sql-server-with-the-jdbc-driver.md)
- [FIPS mode](fips-mode.md)
