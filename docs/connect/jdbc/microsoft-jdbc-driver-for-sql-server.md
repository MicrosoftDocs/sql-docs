---
title: Microsoft JDBC Driver for SQL Server
description: Task hub for connecting Java applications to SQL Server, Azure SQL, and SQL database in Microsoft Fabric with the Microsoft JDBC Driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, machavan, sunilbs
ms.date: 11/06/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---

# Microsoft JDBC Driver for SQL Server

[!INCLUDE [Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The Microsoft Java Database Connectivity (JDBC) Driver for SQL Server is a Type 4 JDBC driver that uses the SQL Server Tabular Data Stream (TDS) protocol directly. Base SQL connections don't require native client libraries. Some authentication and security features use optional Java libraries or native components. The driver lets Java applications and application servers connect to the [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md) in Azure SQL Database, SQL database in Microsoft Fabric, Azure SQL Managed Instance, and in [all supported versions and editions](microsoft-jdbc-driver-for-sql-server-support-matrix.md#sql-version-compatibility) of SQL Server, including Express editions. It implements the standard JDBC APIs and works with major Java application servers, including IBM WebSphere and SAP NetWeaver.

## Choose your starting point

- To add the driver and run your first parameterized query, start with the [Java and Maven quickstart](getting-started-with-the-jdbc-driver.md).
- To choose a hosted database or SQL Server container, see [Choose your database](getting-started-with-the-jdbc-driver.md#choose-your-database).
- To connect to Azure SQL or SQL database in Microsoft Fabric without storing a password in your application, follow the quickstart's [Microsoft Entra configuration](getting-started-with-the-jdbc-driver.md#azure-sql-or-fabric-with-microsoft-entra-authentication). For other authentication modes, see [Connect using Microsoft Entra authentication](connecting-using-azure-active-directory-authentication.md).
- To add the driver to a Maven, Gradle, or other build, go to [Install the JDBC driver](using-the-jdbc-driver.md).
- To make an existing app resilient to transient failures, go to [Connection resiliency](connection-resiliency.md) and [Configurable retry logic](configurable-retry-logic.md).
- To diagnose a connection or query problem, go to [Diagnosing problems with the JDBC driver](diagnosing-problems-with-the-jdbc-driver.md) and [Troubleshooting connectivity](troubleshooting-connectivity.md).

## Production baseline for Azure SQL

Use this snippet as a starting point for a production-oriented Azure SQL connection. It loads the server name and database name from application configuration, such as Azure App Service app settings, environment variables, or a config file, and sets the rest of the connection properties programmatically. The configuration combines Transport Layer Security (TLS), managed identity, connection retries on transient failures, fast failover-group recovery, a longer login timeout to cover a cold-start failover, and configurable retry logic (CRL) for statements that hit Azure SQL throttling or mid-query failover.

For higher security and easier scale-out, keep connection information outside your code. In production, store connection information in your application's configuration system, and use Azure Key Vault for sensitive values and centrally managed connection settings. For more information, see [Securing connection strings](securing-connection-strings.md).

The Java snippets in this article omit imports and class wrappers for brevity.

```java
// Load endpoint details from application configuration. In Azure App Service,
// these can come from app settings or Key Vault-backed settings.
String serverName = System.getenv("SQL_SERVER_NAME");
String databaseName = System.getenv("SQL_DATABASE_NAME");
String port = System.getenv().getOrDefault("SQL_PORT", "1433");

if (serverName == null || databaseName == null) {
    throw new IllegalStateException(
            "Set SQL_SERVER_NAME and SQL_DATABASE_NAME in your application configuration.");
}

String url = "jdbc:sqlserver://" + serverName + ":" + port;

Properties props = new Properties();
props.setProperty("databaseName", databaseName);
props.setProperty("encrypt", "true");
props.setProperty("trustServerCertificate", "false");
props.setProperty("authentication", "ActiveDirectoryManagedIdentity");
props.setProperty("loginTimeout", "120");         // login timeout in seconds for each connection attempt
props.setProperty("connectRetryCount", "5");     // up to 5 outer connection retries; inner login retries are separate
props.setProperty("connectRetryInterval", "15"); // first outer retry is immediate; 15 seconds before later outer retries
props.setProperty("multiSubnetFailover", "true"); // parallel dials to all resolved IPs; safe on single-IP targets
// props.setProperty("applicationIntent", "ReadOnly"); // uncomment to route to a readable secondary
// Retry deadlocks and lock timeouts, plus Azure SQL throttling and mid-query failover.
props.setProperty("retryExec", "1205,1222:3,5+5;40501,40613,40197,10928,10929,49918:4,5*2");

try (Connection conn = DriverManager.getConnection(url, props);
     Statement stmt = conn.createStatement();
     ResultSet rs = stmt.executeQuery("SELECT 1")) {
    while (rs.next()) {
        System.out.println(rs.getInt(1));
    }
}
```

This snippet is tuned for Azure SQL Database failover groups and Azure SQL Managed Instance.

Set `multiSubnetFailover=true` when the target is Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, a failover-group listener, an availability group listener, or a failover cluster instance endpoint. The driver attempts TCP connections to all resolved IP addresses in parallel and uses the first connection that succeeds. When DNS resolves to one address, the driver does a single connection attempt and doesn't start any parallel connection threads. For more information, see [JDBC driver support for High Availability, disaster recovery](jdbc-driver-support-for-high-availability-disaster-recovery.md).

The initial connection process has an outer `connect()` retry loop and an inner `login()` retry loop. Each loop tracks its own retry count; `connectRetryCount` isn't a global cap on physical login attempts. The snippet doesn't set `retryConn` because the outer loop already retries the most common Azure SQL transient connection errors (including 4060, 40197, 40501, 40613, 49918, 49919, 49920, 10928, and 10929), gated by `connectRetryCount` and `connectRetryInterval`. For the full list, see [Built-in transient connection error list](configurable-retry-logic.md#built-in-transient-connection-error-list). Add `retryConn` with `+<errorNumber>` only when you need to extend the outer loop's error list, or set it to `<errorNumber>` (no leading `+`) to replace that list. Replacing the list drops the outer loop's built-in errors, so list any you still need. Inner login retries use separate error checks and waits that `retryConn` doesn't change. If you place the same value in a JDBC URL, wrap it as `retryConn={+<errorNumber>}` or `retryConn={<errorNumber>}`.

The `loginTimeout`, `connectRetryCount`, and `connectRetryInterval` values govern outer connection retries, but they don't impose a strict total deadline on `DriverManager.getConnection()`. Before starting another outer retry, the driver checks elapsed time plus `connectRetryInterval` against `loginTimeout`, but each call to the inner login loop receives a fresh login timeout. These retries can also help an application connect during a resume on [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview). The first connection attempt to an auto-paused database fails with error 40613 while the database resumes. For more information, see [Connect to an auto-paused serverless database](connection-resiliency.md#connect-to-an-auto-paused-serverless-database).

The `retryExec` property has two parts, written as `rule1;rule2` when you set it programmatically. If you place the same value in a JDBC URL, wrap each rule in braces as `{rule1};{rule2}`:

- `{1205,1222:3,5+5}` retries deadlock victims (1205) and lock-request timeouts (1222) three times with a linear backoff of 5, 10, and 15 seconds. Before enabling statement retries, verify that repeating the command is safe; batches and stored procedures aren't automatically safe. `retryExec` remains active inside transactions, so a matching rule can consume an error before your application's whole-transaction retry handler sees it. For 1205, SQL Server rolls back the transaction, and rerunning a later command doesn't replay earlier rolled-back work. Keep matching statement-retry rules from handling commands that require whole-transaction retry. Error 1222 leaves the transaction open; statement retries don't bound transaction duration. For more information, see [Configurable retry logic](configurable-retry-logic.md).

- `{40501,40613,40197,10928,10929,49918:4,5*2}` retries Azure SQL throttling, mid-query failover, and resource-limit errors four times with an exponential backoff of 5, 10, 20, and 40 seconds. These errors are in the built-in transient list for the *connect* loop, but `retryExec` is what catches them when they fire mid-query against an established connection. Before each retry wait, the driver compares that wait with the connection-level `queryTimeout` property, not the value set by `Statement.setQueryTimeout()`. If `queryTimeout` is nonnegative and shorter than the wait, the driver throws a new `SQLServerException` for an invalid retry interval instead of rethrowing the original error. The default value, `-1`, skips this check; `0` rejects positive waits. If you set a positive value, choose at least 40 seconds to allow every wait in this rule. This check doesn't limit the total duration of statement execution and retries.

For Azure SQL Database failover groups, Hyperscale named replicas and read scale-out, or Always On availability group listeners, set `applicationIntent=ReadOnly` when you want to land on a readable secondary. For sovereign clouds where the certificate Subject Alternative Name (SAN) doesn't include the host you're connecting to, also set `hostNameInCertificate` to match (for example, `*.database.usgovcloudapi.net` for Azure Government).

For more information about each part of this configuration, see:

- [Building the connection URL](building-the-connection-url.md)
- [Set the connection properties](setting-the-connection-properties.md)
- [Connect using Microsoft Entra authentication](connecting-using-azure-active-directory-authentication.md)
- [Connection resiliency](connection-resiliency.md)
- [Configurable retry logic](configurable-retry-logic.md)
- [JDBC driver support for High Availability, disaster recovery](jdbc-driver-support-for-high-availability-disaster-recovery.md)
- [Understanding timeout properties in the JDBC driver](understand-timeouts.md)

For the catalog of Azure SQL transient errors, see [Troubleshoot transient connection errors](/azure/azure-sql/database/troubleshoot-common-connectivity-issues).

## Key features

- **Standards-based JDBC**: Type 4 driver. The build for Java 11 and later versions implements JDBC 4.2 plus the JDBC 4.3 request-boundary methods (`beginRequest`, `endRequest`) and `Statement` quoting helpers. JDBC 4.3 sharding APIs (`setShardingKey`, the `createConnectionBuilder` family) throw `SQLFeatureNotSupportedException`. The Java 8 build implements JDBC 4.2. For the per-version breakdown and the full list of supported and unsupported 4.3 methods, see [Java and JDBC specification support](microsoft-jdbc-driver-for-sql-server-support-matrix.md#java-and-jdbc-specification-support).
- **Wide platform support**: Runs on any platform with a supported Java Virtual Machine (JVM), including Windows, Linux, and macOS.
- **Encrypted by default**: TLS-encrypted connections with `encrypt=true` as the default for current drivers.
- **Microsoft Entra ID authentication**: Passwordless connections with managed identity, service principal, interactive, integrated, default credential chain, and access-token flows.
- **Kerberos**: Integrated authentication for on-premises Active Directory.
- **NTLM**: Windows challenge/response authentication for nondomain or legacy scenarios.
- **Always Encrypted**: Client-side encryption for sensitive columns, with optional secure enclaves for in-place operations.
- **Bulk copy**: High-throughput inserts with the `SQLServerBulkCopy` API and batch-insert performance for `executeBatch`.
- **Connection resiliency**: Built-in connection retries for transient errors, plus opt-in configurable retry logic for statements (`retryExec`) and a customizable connection error list (`retryConn`).
- **Rich SQL Server data type support**: **datetimeoffset**, **sql_variant**, JSON, spatial, vector, table-valued parameters, and user-defined types.

## Get started

| Article | Description |
| --- | --- |
| [System requirements](system-requirements-for-the-jdbc-driver.md) | Supported Java, operating system, and SQL Server versions. |
| [Support matrix](microsoft-jdbc-driver-for-sql-server-support-matrix.md) | Detailed compatibility matrix for JDBC driver releases. |
| [Download Microsoft JDBC Driver for SQL Server](download-microsoft-jdbc-driver-for-sql-server.md) | Download links, Maven coordinates, and release artifacts. |
| [Java and Maven quickstart](getting-started-with-the-jdbc-driver.md) | Build a complete application, configure authentication, and verify a parameterized query result. |
| [Overview of the JDBC driver](overview-of-the-jdbc-driver.md) | Architecture, supported features, and JDBC specification compliance. |

## Configure and connect

| Article | Description |
| --- | --- |
| [Connecting to SQL Server with the JDBC driver](connecting-to-sql-server-with-the-jdbc-driver.md) | Open a connection to a SQL Server instance from Java. |
| [Connecting to an Azure SQL database](connecting-to-an-azure-sql-database.md) | Connect a Java application to Azure SQL Database. |
| [Building the connection URL](building-the-connection-url.md) | Full reference for `jdbc:sqlserver://` URL syntax and properties. |
| [Set the connection properties](setting-the-connection-properties.md) | All connection properties, defaults, and how to set them. |
| [Setting the data source properties](setting-the-data-source-properties.md) | Configure `SQLServerDataSource` for use with JNDI and app servers. |
| [Working with a connection](working-with-a-connection.md) | Open, reuse, and close connections correctly. |
| [Using connection pooling](using-connection-pooling.md) | JNDI data sources and integration with external pools. |
| [Connection resiliency](connection-resiliency.md) | Built-in connection retries and broken-connection detection. |
| [Configurable retry logic](configurable-retry-logic.md) | Retry failed statements with `retryExec` and customize the connection retry list with `retryConn`. |
| [Understanding timeout properties in the JDBC driver](understand-timeouts.md) | `loginTimeout`, `queryTimeout`, socket timeouts, and how they interact. |
| [Deploying the JDBC driver](deploying-the-jdbc-driver.md) | Package and deploy the driver with your application. |

## Authenticate

| Article | Description |
| --- | --- |
| [Microsoft Entra authentication](connecting-using-azure-active-directory-authentication.md) | Managed identity, service principal, interactive, integrated, and access-token authentication. |
| [Kerberos integrated authentication](using-kerberos-integrated-authentication-to-connect-to-sql-server.md) | Connect with Kerberos and Active Directory. |
| [NTLM authentication](using-ntlm-authentication-to-connect-to-sql-server.md) | Use NTLM credentials to authenticate. |
| [Client certificate authentication for loopback scenarios](client-certification-authentication-for-loopback-scenarios.md) | Authenticate clients with certificates on loopback connections. |

## Secure

| Article | Description |
| --- | --- |
| [Securing JDBC driver applications](securing-jdbc-driver-applications.md) | Security guidance for Java applications that use the driver. |
| [Application security](application-security.md) | Threat model and defense-in-depth recommendations. |
| [Securing connection strings](securing-connection-strings.md) | Keep credentials and connection strings out of source. |
| [Configuring the client for encryption](configuring-the-client-for-ssl-encryption.md) | Trust roots, certificate pinning, and TLS settings. |
| [Connecting with encryption](connecting-with-ssl-encryption.md) | Force `encrypt=true` and validate the server certificate. |
| [Understanding encryption support](understanding-ssl-support.md) | How the driver negotiates TLS with SQL Server. |
| [Validating user input](validating-user-input.md) | Parameterize SQL and avoid injection. |
| [FIPS mode](fips-mode.md) | Run the driver in FIPS-compliant environments. |
| [Always Encrypted](using-always-encrypted-with-the-jdbc-driver.md) | Configure client-side encryption for sensitive columns. |
| [Always Encrypted with secure enclaves](using-always-encrypted-with-secure-enclaves-with-the-jdbc-driver.md) | Enable rich operations on encrypted columns. |
| [Always Encrypted API reference](always-encrypted-api-reference-for-the-jdbc-driver.md) | API surface for column encryption providers and key stores. |

## Work with data

| Article | Description |
| --- | --- |
| [Working with statements and result sets](working-with-statements-and-result-sets.md) | Basics of `Statement`, `PreparedStatement`, and result sets. |
| [Using statements with the JDBC driver](using-statements-with-the-jdbc-driver.md) | Run parameterized and non-parameterized statements. |
| [Handling complex statements](handling-complex-statements.md) | Stored procedures, multiple results, and update counts. |
| [Working with result sets](working-with-result-sets.md) | Iterate, update, and scroll through query results. |
| [Using multiple result sets](using-multiple-result-sets.md) | Handle queries that return more than one result set. |
| [Understanding cursor types](understanding-cursor-types.md) | Forward-only, scrollable, and updatable cursors. |
| [Using table-valued parameters](using-table-valued-parameters.md) | Pass `TABLE` parameters to stored procedures. |
| [Using bulk copy with the JDBC driver](using-bulk-copy-with-the-jdbc-driver.md) | High-throughput inserts with `SQLServerBulkCopy`. |
| [Bulk copy API for batch insert](use-bulk-copy-api-batch-insert-operation.md) | Accelerate `executeBatch` for `INSERT` workloads. |
| [Performing batch operations](performing-batch-operations.md) | Batch inserts, updates, and deletes. |

## Data types

| Article | Description |
| --- | --- |
| [Working with data types](working-with-data-types-jdbc.md) | Map Java types to SQL Server types. |
| [Understanding the JDBC driver data types](understanding-the-jdbc-driver-data-types.md) | Driver type system and JDBC mappings. |
| [Data type conversions](understanding-data-type-conversions.md) | Implicit and explicit conversions between Java and SQL Server. |
| [Data type differences](understanding-data-type-differences.md) | Edge cases when mapping types across the boundary. |
| [JSON data type](use-json-data-type.md) | Store and query JSON columns. |
| [Spatial data types](use-spatial-datatypes.md) | Use **geometry** and **geography** from Java. |
| [Vector data type](use-vector-data-type.md) | Work with the SQL Server **vector** type. |
| [**sql_variant**](using-sql-variant-datatype.md) | Read and write **sql_variant** columns. |
| [User defined types](user-defined-types.md) | Use CLR user-defined types from Java. |
| [National character set support](national-character-set-support.md) | Unicode handling and **nvarchar** columns. |
| [International features](international-features-of-the-jdbc-driver.md) | Locale, collation, and globalization considerations. |

## Transactions and concurrency

| Article | Description |
| --- | --- |
| [Performing transactions](performing-transactions-with-the-jdbc-driver.md) | `commit`, `rollback`, and auto-commit semantics. |
| [Understanding transactions](understanding-transactions.md) | Transaction lifecycle and best practices. |
| [Isolation levels](understanding-isolation-levels.md) | Snapshot, read committed, serializable, and others. |
| [Concurrency control](understanding-concurrency-control.md) | Optimistic and pessimistic concurrency strategies. |
| [Row locking](understanding-row-locking.md) | How SQL Server takes and releases row locks. |
| [Using savepoints](using-savepoints.md) | Partial rollback within a transaction. |
| [Managing transaction size](managing-transaction-size.md) | Tune transaction scope to avoid long-running locks. |
| [XA transactions](understanding-xa-transactions.md) | Two-phase commit with `SQLServerXADataSource`. |

## Performance and reliability

| Article | Description |
| --- | --- |
| [Improving performance and reliability](improving-performance-and-reliability-with-the-jdbc-driver.md) | Index, query, and driver-level tuning. |
| [Prepared statement metadata caching](prepared-statement-metadata-caching-for-the-jdbc-driver.md) | Reuse prepared statement plans. |
| [Prepared statement parameter performance](prepared-statement-parameter-performance.md) | Parameter typing and execution plan reuse. |
| [Adaptive buffering](using-adaptive-buffering.md) | Stream large columns without loading them entirely into memory. |
| [Closing objects when not in use](closing-objects-when-not-in-use.md) | Free `Statement`, `ResultSet`, and `Connection` resources promptly. |
| [High availability and disaster recovery](jdbc-driver-support-for-high-availability-disaster-recovery.md) | Availability group listeners and multi-subnet failover. |
| [Database mirroring](using-database-mirroring-jdbc.md) | Use the driver with database mirroring partners. |

## Diagnose and troubleshoot

| Article | Description |
| --- | --- |
| [Diagnosing problems with the JDBC driver](diagnosing-problems-with-the-jdbc-driver.md) | Tracing, logging, and common failure modes. |
| [Troubleshooting connectivity](troubleshooting-connectivity.md) | Connection errors, TLS handshake failures, and named instances. |
| [Tracing driver operation](tracing-driver-operation.md) | Enable JDK logging for the driver. |
| [Performance logger and callback](performance-logger-callback.md) | Capture per-statement performance metrics. |
| [Extended events log](accessing-diagnostic-information-in-the-extended-events-log.md) | Correlate client errors with server-side extended events. |
| [Handling errors](handling-errors.md) | `SQLException`, error codes, and retry hints. |

## Related tasks

| Article | Description |
| --- | --- |
| [Release notes](release-notes-for-the-jdbc-driver.md) | Version history and what's new in each release. |
| [Feature dependencies](feature-dependencies-of-microsoft-jdbc-driver-for-sql-server.md) | Optional dependencies for Entra ID, Kerberos, Always Encrypted, and others. |
| [JDBC 4.3 compliance](jdbc-4-3-compliance-for-the-jdbc-driver.md) | JDBC 4.3 API conformance. |
| [JDBC 4.2 compliance](jdbc-4-2-compliance-for-the-jdbc-driver.md) | JDBC 4.2 API conformance. |
| [JDBC 4.1 compliance](jdbc-4-1-compliance-for-the-jdbc-driver.md) | JDBC 4.1 API conformance. |
| [Compliance and legal](compliance-and-legal-for-the-jdbc-sql-driver.md) | Specification compliance and licensing. |
| [JDBC driver API reference](reference/jdbc-driver-api-reference.md) | Classes, interfaces, methods, and fields exposed by the driver. |
| [Sample JDBC driver applications](sample-jdbc-driver-applications.md) | End-to-end code samples. |
| [FAQ](frequently-asked-questions-faq-for-jdbc-driver.yml) | Frequently asked questions. |

## Request a feature

To request a feature, open an issue in the [Microsoft JDBC Driver for SQL Server GitHub repository](https://github.com/microsoft/mssql-jdbc/issues/new/choose).

## Related content

- [Microsoft JDBC Driver for SQL Server on GitHub](https://github.com/microsoft/mssql-jdbc)
- [Microsoft JDBC Driver for SQL Server on Maven Central](https://search.maven.org/artifact/com.microsoft.sqlserver/mssql-jdbc)
- [Release notes for the Microsoft JDBC Driver for SQL Server](release-notes-for-the-jdbc-driver.md)
- [Finding additional JDBC driver information](finding-additional-jdbc-driver-information.md)
- [Microsoft SQL drivers and frameworks](../sql-connection-libraries.md)
