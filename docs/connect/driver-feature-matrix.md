---
title: Driver Feature Support Matrix
description: Compare feature support in Microsoft SQL drivers for .NET, ODBC, OLE DB, Go, JDBC, Node.js, JavaScript, PHP, and Python.
author: David-Engel
ms.author: davidengel
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
ms.custom:
  - sfi-ropc-nochange
---
# Driver feature support matrix for Microsoft SQL

This matrix compares capabilities implemented by drivers that connect applications to SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, and SQL database in Microsoft Fabric. It doesn't indicate whether a feature is available in every database product or service. Check the linked feature documentation for product availability.

Start with the driver for your programming language and runtime, and then verify that it supports the capabilities your application requires. Driver support varies because a capability might not apply to the driver technology, might depend on another component, or might not be implemented.

## How to read the matrix

- **Yes** means the driver supports the capability. The link provides supporting documentation.
- **Partial** means the driver supports only the scenario or representation described in the cell.
- **No** means the driver doesn't provide driver-level support for the capability. Where applicable, applications might still access data through a generic representation, such as text or binary data.

Driver version qualifiers appear only when a capability isn't available in every currently supported release. Select a linked driver name or support value to open the corresponding documentation. If you need an unsupported capability, use the **Request a feature** section on the driver's landing page.

## .NET drivers

<a id="table1"></a>

> [!IMPORTANT]
> Use Microsoft.Data.SqlClient for new .NET development. The System.Data.SqlClient columns describe compatibility for existing applications only. System.Data.SqlClient isn't recommended for new development. Migrate existing applications by following [Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient](ado-net/migrate-system-data-sql-client-to-microsoft-data-sql-client.md).
>
> The [System.Data.SqlClient NuGet package](https://techcommunity.microsoft.com/blog/sqlserver/announcement-system-data-sqlclient-package-is-now-deprecated/4227205) supports .NET 8, but not .NET 9 or later versions. After [.NET 8 reaches end of support in November 2026](/dotnet/core/releases-and-support#currently-supported-versions), the package won't have a supported .NET runtime.

| Feature | [Microsoft.Data.SqlClient (.NET)](ado-net/microsoft-ado-net-sql-server.md) | [Microsoft.Data.SqlClient (.NET Framework)](ado-net/microsoft-ado-net-sql-server.md) | [System.Data.SqlClient (.NET 8 NuGet package, existing applications)](connect-history.md#systemdatasqlclient) | [System.Data.SqlClient (.NET Framework, existing applications)](connect-history.md#systemdatasqlclient) |
| --- | --- | --- | --- | --- |
| [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md) | No | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md) (.NET Framework 4.6 and later versions) |
| [Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md#enabling-always-encrypted-with-secure-enclaves) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md#enabling-always-encrypted-with-secure-enclaves) | No | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md#enabling-always-encrypted-with-secure-enclaves) (.NET Framework 4.7.2 and later versions) |
| [Microsoft Entra access token authentication](/entra/identity-platform/access-tokens) | [Yes](/dotnet/api/microsoft.data.sqlclient.sqlconnection.accesstoken) | [Yes](/dotnet/api/microsoft.data.sqlclient.sqlconnection.accesstoken) | [Yes](/dotnet/api/system.data.sqlclient.sqlconnection.accesstoken) | [Yes](/dotnet/api/system.data.sqlclient.sqlconnection.accesstoken) (.NET Framework 4.6 and later versions) |
| [Microsoft Entra password authentication (deprecated)](/azure/azure-sql/database/authentication-aad-overview) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | No | [Yes](/dotnet/api/system.data.sqlclient.sqlauthenticationmethod#fields) (.NET Framework 4.6 and later versions) |
| [Microsoft Entra integrated authentication](/azure/azure-sql/database/authentication-aad-overview) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | No | [Yes](/dotnet/api/system.data.sqlclient.sqlauthenticationmethod#fields) (.NET Framework 4.6 and later versions) |
| [Microsoft Entra Interactive (MFA) authentication](/azure/azure-sql/database/authentication-aad-overview) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | No | [Yes](/dotnet/api/system.data.sqlclient.sqlauthenticationmethod#fields) (.NET Framework 4.7.2 and later versions) |
| [Microsoft Entra managed identity authentication](/entra/identity/managed-identities-azure-resources/overview) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | No | No |
| [Microsoft Entra service principal authentication](/entra/identity-platform/app-objects-and-service-principals) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | No | No |
| [Microsoft Entra service principal certificate authentication](/entra/identity-platform/app-objects-and-service-principals) | No | No | No | No |
| [Microsoft Entra default Azure authentication](/azure/developer/intro/passwordless-overview#introducing-defaultazurecredential) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | No | No |
| [Integrated authentication](/windows-server/security/windows-authentication/windows-authentication-overview) | [Yes](ado-net/sql/authentication-sql-server.md) | [Yes](ado-net/sql/authentication-sql-server.md) | [Yes](/dotnet/framework/data/adonet/sql/authentication-in-sql-server) | [Yes](/dotnet/framework/data/adonet/sql/authentication-in-sql-server) |
| [Bulk Copy](../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md) | [Yes](ado-net/sql/bulk-copy-operations-sql-server.md) | [Yes](ado-net/sql/bulk-copy-operations-sql-server.md) | [Yes](/dotnet/framework/data/adonet/sql/bulk-copy-operations-in-sql-server) | [Yes](/dotnet/framework/data/adonet/sql/bulk-copy-operations-in-sql-server) |
| [Data Discovery and Classification metadata](../relational-databases/security/sql-data-discovery-and-classification.md) | [Yes](ado-net/sql/data-classification.md) | [Yes](ado-net/sql/data-classification.md) | No | No |
| Multiple Active Result Sets (MARS) | [Yes](ado-net/sql/multiple-active-result-sets-mars.md) | [Yes](ado-net/sql/multiple-active-result-sets-mars.md) | [Yes](/dotnet/framework/data/adonet/sql/multiple-active-result-sets-mars) | [Yes](/dotnet/framework/data/adonet/sql/multiple-active-result-sets-mars) |
| [Spatial Data Types](../relational-databases/spatial/spatial-data-sql-server.md) | [Partial](/ef/ef6/fundamentals/providers/spatial-support#prerequisites-for-spatial-types-with-microsoft-sql-server) (with `Microsoft.SqlServer.Types`) | [Partial](/ef/ef6/fundamentals/providers/spatial-support#prerequisites-for-spatial-types-with-microsoft-sql-server) (with `Microsoft.SqlServer.Types`) | [Partial](/ef/ef6/fundamentals/providers/spatial-support#prerequisites-for-spatial-types-with-microsoft-sql-server) (with `Microsoft.SqlServer.Types`) | [Partial](/ef/ef6/fundamentals/providers/spatial-support#prerequisites-for-spatial-types-with-microsoft-sql-server) (with `Microsoft.SqlServer.Types`) |
| [Table-Valued Parameters (TVP)](../relational-databases/tables/use-table-valued-parameters-database-engine.md) | [Yes](ado-net/sql/table-valued-parameters.md) | [Yes](ado-net/sql/table-valued-parameters.md) | [Yes](/dotnet/framework/data/adonet/sql/table-valued-parameters) | [Yes](/dotnet/framework/data/adonet/sql/table-valued-parameters) |
| MultiSubnetFailover | [Yes](ado-net/sql/sqlclient-support-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](ado-net/sql/sqlclient-support-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder.multisubnetfailover?view=netcore-1.0&preserve-view=true) | [Yes](/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder.multisubnetfailover?view=netframework-4.8&preserve-view=true) |
| [Transparent Network IP Resolution](odbc/using-transparent-network-ip-resolution.md) | No | [Yes](/dotnet/api/microsoft.data.sqlclient.sqlconnectionstringbuilder.transparentnetworkipresolution) | No | [Yes](/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder.transparentnetworkipresolution) |
| [TDS 8.0 strict encryption](../relational-databases/security/networking/tds-8.md) | [Yes](ado-net/encryption-and-certificate-validation.md#changes-in-encryption-and-certificate-validation-behavior) | [Yes](ado-net/encryption-and-certificate-validation.md#changes-in-encryption-and-certificate-validation-behavior) | No | No |
| [TLS 1.3](../relational-databases/security/networking/tds-8.md) | [Yes](ado-net/microsoft-data-sql-client-release-notes.md#51) | [Yes](ado-net/microsoft-data-sql-client-release-notes.md#51) | No | No |
| [JSON data type](../relational-databases/json/json-data-sql-server.md) | [Yes](ado-net/sql/json-data-sql-server.md) | [Yes](ado-net/sql/json-data-sql-server.md) | No | No |
| [Vector (float32) data type](../t-sql/data-types/vector-data-type.md) | [Yes](ado-net/sql/vector-data-sql-server.md) | [Yes](ado-net/sql/vector-data-sql-server.md) | No | No |

## ODBC, JDBC, and OLE DB drivers

<a id="table2"></a>

| Feature | [ODBC Driver for SQL Server on Windows](odbc/microsoft-odbc-driver-for-sql-server.md) | [ODBC Driver for SQL Server on Linux and macOS](odbc/microsoft-odbc-driver-for-sql-server.md) | [JDBC Driver for SQL Server](jdbc/microsoft-jdbc-driver-for-sql-server.md) | [OLE DB Driver for SQL Server](oledb/oledb-driver-for-sql-server.md) |
| :-- | :-- | :-- | :-- | :-- |
| [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md) | [Yes](jdbc/using-always-encrypted-with-the-jdbc-driver.md) | No |
| [Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md#enabling-always-encrypted-with-secure-enclaves) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md#enabling-always-encrypted-with-secure-enclaves) | [Yes](jdbc/using-always-encrypted-with-secure-enclaves-with-the-jdbc-driver.md) | No |
| [Microsoft Entra access token authentication](/entra/identity-platform/access-tokens) | [Yes](odbc/using-azure-active-directory.md#authenticating-with-an-access-token) | [Yes](odbc/using-azure-active-directory.md#authenticating-with-an-access-token) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md#connect-using-access-token) | [Yes](oledb/features/using-azure-active-directory.md) |
| [Microsoft Entra password authentication (deprecated)](/azure/azure-sql/database/authentication-aad-overview) | [Yes](odbc/using-azure-active-directory.md) | [Yes](odbc/using-azure-active-directory.md) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) | [Yes](oledb/features/using-azure-active-directory.md) |
| [Microsoft Entra integrated authentication](/azure/azure-sql/database/authentication-aad-overview) | [Yes](odbc/using-azure-active-directory.md) | [Yes](odbc/using-azure-active-directory.md) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) | [Yes](oledb/features/using-azure-active-directory.md) |
| [Microsoft Entra Interactive (MFA) authentication](/azure/azure-sql/database/authentication-aad-overview) | [Yes](odbc/using-azure-active-directory.md) | No | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) | [Yes](oledb/features/using-azure-active-directory.md) |
| [Microsoft Entra managed identity authentication](/entra/identity/managed-identities-azure-resources/overview) | [Yes](odbc/using-azure-active-directory.md) | [Yes](odbc/using-azure-active-directory.md) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) | [Yes](oledb/features/using-azure-active-directory.md) |
| [Microsoft Entra service principal authentication](/entra/identity-platform/app-objects-and-service-principals) | [Yes](odbc/using-azure-active-directory.md) | [Yes](odbc/using-azure-active-directory.md) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) | [Yes](oledb/features/using-azure-active-directory.md) |
| [Microsoft Entra service principal certificate authentication](/entra/identity-platform/app-objects-and-service-principals) | No | No | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) (12.4 and later versions) | No |
| [Microsoft Entra default Azure authentication](/azure/developer/intro/passwordless-overview#introducing-defaultazurecredential) | No | No | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) (12.2 and later versions) | No |
| Integrated authentication | [Yes](odbc/dsn-connection-string-attribute.md) | [Yes](odbc/linux-mac/using-integrated-authentication.md) | [Yes](jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server.md) | [Yes](oledb/applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md) |
| [Bulk Copy](../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md#bulk-copy-of-encrypted-columns) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md#bulk-copy-of-encrypted-columns) | [Yes](jdbc/using-bulk-copy-with-the-jdbc-driver.md) | [Yes](oledb/features/performing-bulk-copy-operations.md) |
| [Data Discovery and Classification metadata](../relational-databases/security/sql-data-discovery-and-classification.md) | [Yes](odbc/data-classification.md) | [Yes](odbc/data-classification.md) | [Yes](jdbc/data-discovery-classification-sample.md) | [Yes](oledb/features/using-data-classification.md) |
| Multiple Active Result Sets (MARS) | [Yes](odbc/dsn-connection-string-attribute.md) | [Yes](odbc/dsn-connection-string-attribute.md) | No | [Yes](oledb/features/using-multiple-active-result-sets-mars.md) |
| [Spatial Data Types](../relational-databases/spatial/spatial-data-sql-server.md) | [Partial](../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md) (via UDT) | [Partial](../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md) (via UDT) | [Yes](jdbc/use-spatial-datatypes.md) | [Partial](oledb/features/using-user-defined-types.md) (via UDT) |
| [Table-Valued Parameters (TVP)](../relational-databases/tables/use-table-valued-parameters-database-engine.md) | [Yes](../relational-databases/native-client-odbc-table-valued-parameters/uses-of-odbc-table-valued-parameters.md) | [Yes](../relational-databases/native-client-odbc-table-valued-parameters/uses-of-odbc-table-valued-parameters.md) | [Yes](jdbc/using-table-valued-parameters.md) | [Yes](oledb/ole-db-table-valued-parameters/table-valued-parameters-ole-db.md) |
| MultiSubnetFailover | [Yes](odbc/odbc-driver-support-for-high-availability-disaster-recovery.md#connect-with-multisubnetfailover) | [Yes](odbc/odbc-driver-support-for-high-availability-disaster-recovery.md#connect-with-multisubnetfailover) | [Yes](jdbc/jdbc-driver-support-for-high-availability-disaster-recovery.md) | [Yes](oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) |
| [Transparent Network IP Resolution](odbc/using-transparent-network-ip-resolution.md) | [Yes](odbc/using-transparent-network-ip-resolution.md) | [Yes](odbc/using-transparent-network-ip-resolution.md) | [Yes](jdbc/setting-the-connection-properties.md) | [Yes](oledb/features/using-transparent-network-ip-resolution.md) |
| [TDS 8.0 strict encryption](../relational-databases/security/networking/tds-8.md) | [Yes](odbc/dsn-connection-string-attribute.md#encrypt) (ODBC 18 only) | [Yes](odbc/dsn-connection-string-attribute.md#encrypt) (ODBC 18 only) | [Yes](jdbc/setting-the-connection-properties.md#encrypt) (11.2 and later versions) | [Yes](oledb/features/encryption-and-certificate-validation.md#major-version-19) (OLE DB 19 only) |
| [TLS 1.3](../relational-databases/security/networking/tds-8.md) | [Yes](../relational-databases/security/networking/tds-8.md) (ODBC 18 only) | [Yes](../relational-databases/security/networking/tds-8.md) (ODBC 18 only) | [Yes](jdbc/understanding-ssl-support.md) (11.2 and later versions) | [Yes](oledb/features/encryption-and-certificate-validation.md#major-version-19) (OLE DB 19 only) |
| [JSON data type](../relational-databases/json/json-data-sql-server.md) | No | No | [Yes](jdbc/use-json-data-type.md) (13.2 and later versions) | No |
| [Vector (float32) data type](../t-sql/data-types/vector-data-type.md) | [Yes](odbc/vector-data-type.md) (ODBC 18 only) | [Yes](odbc/vector-data-type.md) (ODBC 18 only) | [Yes](jdbc/use-vector-data-type.md) (13.2 and later versions) | No |
| [Vector (float16) data type](../t-sql/data-types/vector-data-type-half-precision-float.md) | [Yes](odbc/vector-data-type.md) (ODBC 18 only) | [Yes](odbc/vector-data-type.md) (ODBC 18 only) | [Yes](jdbc/use-vector-data-type.md#use-vector-float16-data-type) (13.4 and later versions) | No |

## PHP, Node.js, Python, and Go drivers

<a id="table3"></a>

| Feature | [Microsoft Drivers for PHP for SQL Server](php/microsoft-php-driver-for-sql-server.md)<sup>1</sup> | [Tedious (Node.js)](node-js/node-js-driver-for-sql-server.md) | [mssql-python (Python)](python/mssql-python/python-sql-driver-mssql-python.md) | [go-mssqldb (Go)](golang/microsoft-go-mssqldb-driver.md) |
| :-- | :-- | :-- | :-- | :-- |
| [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md) | [Yes](php/using-always-encrypted-php-drivers.md) | No | No | [Yes](golang/always-encrypted.md) |
| [Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md) | [Yes](php/always-encrypted-secure-enclaves.md) | No | No | No |
| [Microsoft Entra access token authentication](/entra/identity-platform/access-tokens) | [Yes](php/azure-active-directory.md) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | [Yes](python/mssql-python/entra-authentication.md#access-token-authentication) | [Yes](golang/entra-authentication.md) |
| [Microsoft Entra password authentication (deprecated)](/azure/azure-sql/database/authentication-aad-overview) | [Yes](php/azure-active-directory.md) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | [Yes](python/mssql-python/entra-authentication.md#password-authentication-deprecated) | [Yes](golang/entra-authentication.md) |
| [Microsoft Entra integrated authentication](/azure/azure-sql/database/authentication-aad-overview) | No | No | [Yes](python/mssql-python/entra-authentication.md#windows-integrated-authentication) | No |
| [Microsoft Entra Interactive (MFA) authentication](/azure/azure-sql/database/authentication-aad-overview) | No | No | [Yes](python/mssql-python/entra-authentication.md#interactive-authentication) | [Yes](golang/entra-authentication.md) |
| [Microsoft Entra managed identity authentication](/entra/identity/managed-identities-azure-resources/overview) | [Yes](php/azure-active-directory.md) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | [Yes](python/mssql-python/entra-authentication.md#managed-identity) | [Yes](golang/entra-authentication.md) |
| [Microsoft Entra service principal authentication](/entra/identity-platform/app-objects-and-service-principals) | [Yes](php/azure-active-directory.md) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | [Yes](python/mssql-python/entra-authentication.md#service-principal-authentication) | [Yes](golang/entra-authentication.md) |
| [Microsoft Entra service principal certificate authentication](/entra/identity-platform/app-objects-and-service-principals) | No | No | No | [Yes](golang/entra-authentication.md) |
| [Microsoft Entra default Azure authentication](/azure/developer/intro/passwordless-overview#introducing-defaultazurecredential) | No | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | [Yes](python/mssql-python/entra-authentication.md#defaultazurecredential) | [Yes](golang/entra-authentication.md) |
| Integrated authentication | [Yes](php/how-to-connect-using-windows-authentication.md); see [Linux and macOS](odbc/linux-mac/using-integrated-authentication.md) | [Partial](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | [Yes](python/mssql-python/connection-strings.md#authentication) | [Yes](golang/authentication.md) |
| [Bulk Copy](../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md) | No | [Yes](https://tediousjs.github.io/tedious/bulk-load.html) | [Yes](python/mssql-python/bulk-copy.md) | [Yes](golang/bulk-operations.md) |
| [Data Discovery and Classification metadata](../relational-databases/security/sql-data-discovery-and-classification.md) | [Yes](php/release-notes-php-sql-driver.md#whats-new-in-58) | No | No | No |
| Multiple Active Result Sets (MARS) | [Yes](php/how-to-disable-multiple-active-resultsets-mars.md) | No | No | No |
| [Spatial Data Types](../relational-databases/spatial/spatial-data-sql-server.md) | [Partial](php/default-php-data-types.md) (via UDT binary or stream) | [Partial](https://tediousjs.github.io/tedious/api-datatypes.html) (UDT buffer) | [Partial](python/mssql-python/spatial-data.md) (binary or WKT) | [Partial](golang/data-type-mappings.md) (via UDT) |
| [Table-Valued Parameters (TVP)](../relational-databases/tables/use-table-valued-parameters-database-engine.md) | [Yes](php/use-table-valued-parameters.md) | [Yes](https://tediousjs.github.io/tedious/parameters.html) | No | [Yes](golang/table-valued-parameters.md) |
| MultiSubnetFailover | [Yes](php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | [Yes](python/mssql-python/availability-groups.md) | [Yes](golang/connection-options.md) (enabled by default) |
| [Transparent Network IP Resolution](odbc/using-transparent-network-ip-resolution.md) | [Yes](php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | No | No | No |
| [TDS 8.0 strict encryption](../relational-databases/security/networking/tds-8.md) | [Partial](../relational-databases/security/networking/tds-8.md) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | [Yes](python/mssql-python/encryption-certificates.md#tds-80-strict-encryption) | [Yes](golang/encryption-certificates.md) |
| [TLS 1.3](../relational-databases/security/networking/tds-8.md) | [Yes](../relational-databases/security/networking/tds-8.md) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | [Yes](python/mssql-python/encryption-certificates.md) | [Yes](golang/encryption-certificates.md) |
| [JSON data type](../relational-databases/json/json-data-sql-server.md) | No | No | No | No |
| [Vector (float32) data type](../t-sql/data-types/vector-data-type.md) | No | No | No | No |
| [Vector (float16) data type](../t-sql/data-types/vector-data-type-half-precision-float.md) | No | No | No | No |

<sup>1</sup> The PHP drivers rely on the Microsoft ODBC Driver for SQL Server. Use an ODBC driver version that supports the feature.

## Related content

- [SQL drivers and frameworks](sql-connection-libraries.md)
- [Driver history for Microsoft SQL Server](connect-history.md)
