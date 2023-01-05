---
title: Driver feature support matrix
description: Learn which SQL Server features are supported in the drivers for .NET, ODBC, OLE DB, JDBC, Node.js, JavaScript, and Python.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: 02/01/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Driver feature support matrix for Microsoft SQL Server

If you're planning to use a feature in Microsoft SQL Server, it might not be available in all drivers. Some reasons a feature might not be in a particular driver include:

- The feature doesn't apply to the driver technology.
- The feature is new and hasn't been implemented across all drivers yet.
- The feature isn't in demand in a particular driver.
- Other features are being implemented first.

We wish all drivers supported every feature and spend effort to ensure feature parity across drivers. However that isn't always possible. To help you choose the appropriate driver for your needs, here's a list of popular features and the drivers that implement them.

- [.NET](#table1)
- [ODBC](#table2)
- [OLE DB](#table2)
- [JDBC](#table2)
- [PHP](#table3)
- [Node.js / JavaScript](#table3)
- [Python](#table3)

| <a id="table1"></a>Feature | [Microsoft.<wbr>Data.<wbr>SqlClient (.NET Core)](ado-net/microsoft-ado-net-sql-server.md) | [Microsoft.<wbr>Data.<wbr>SqlClient (.NET Framework)](ado-net/microsoft-ado-net-sql-server.md) | System.<wbr>Data.<wbr>SqlClient (.NET Core) | [System.<wbr>Data.<wbr>SqlClient (.NET Framework)](/dotnet/framework/data/adonet/sql/) |
| :-- | :-- | :-- | :-- | :-- |
| [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md) | | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md) (v4.6+) |
| [Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md#enabling-always-encrypted-with-secure-enclaves) (v1.1+) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md#enabling-always-encrypted-with-secure-enclaves) (v1.1+) | | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md#enabling-always-encrypted-with-secure-enclaves) (v4.7.2+) |
| [Azure Active Directory Access Token authentication](/azure/active-directory/develop/access-tokens) | [Yes](/dotnet/api/system.data.sqlclient.sqlconnection.accesstoken) | [Yes](/dotnet/api/microsoft.data.sqlclient.sqlconnection.accesstoken) | [Yes](/dotnet/api/microsoft.data.sqlclient.sqlconnection.accesstoken) (v4.6+) | [Yes](/dotnet/api/microsoft.data.sqlclient.sqlconnection.accesstoken) (v4.6+) |
| [Azure Active Directory Password authentication](/azure/sql-database/sql-database-aad-authentication) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | | Yes (v4.6+) |
| [Azure Active Directory Integrated authentication](/azure/sql-database/sql-database-aad-authentication) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | | Yes (v4.6+) |
| [Azure Active Directory Interactive (MFA) authentication](/azure/sql-database/sql-database-aad-authentication) | [Yes](ado-net/sql/azure-active-directory-authentication.md) | [Yes](ado-net/sql/azure-active-directory-authentication.md) (v2.0+) | | |
| [Azure Active Directory Managed Identity authentication](/azure/active-directory/managed-identities-azure-resources/overview) | [Yes](ado-net/sql/azure-active-directory-authentication.md) (v2.1+) | [Yes](ado-net/sql/azure-active-directory-authentication.md) (v2.1+) | | |
| [Azure Active Directory Service Principal authentication](/azure/active-directory/develop/app-objects-and-service-principals) | [Yes](ado-net/sql/azure-active-directory-authentication.md) (v2.0+) | [Yes](ado-net/sql/azure-active-directory-authentication.md) (v2.0+) | | |
| [Windows-Integrated authentication](/windows-server/security/windows-authentication/windows-authentication-overview) | [Yes](ado-net/sql/authentication-sql-server.md) | [Yes](ado-net/sql/authentication-sql-server.md) | [Yes](/dotnet/framework/data/adonet/sql/authentication-in-sql-server) | [Yes](/dotnet/framework/data/adonet/sql/authentication-in-sql-server) |
| [Bulk Copy](../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md) | [Yes](ado-net/sql/bulk-copy-operations-sql-server.md) | [Yes](ado-net/sql/bulk-copy-operations-sql-server.md) | [Yes](/dotnet/framework/data/adonet/sql/bulk-copy-operations-in-sql-server) | [Yes](/dotnet/framework/data/adonet/sql/bulk-copy-operations-in-sql-server) |
| [Data Sensitivity and Classification metadata](../relational-databases/security/sql-data-discovery-and-classification.md) | [Yes](ado-net/sql/data-classification.md) | [Yes](ado-net/sql/data-classification.md) | | |
| [Multiple Active Result Sets (MARS)](../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md) | [Yes](ado-net/sql/multiple-active-result-sets-mars.md) | [Yes](ado-net/sql/multiple-active-result-sets-mars.md) | [Yes](/dotnet/framework/data/adonet/sql/multiple-active-result-sets-mars) | [Yes](/dotnet/framework/data/adonet/sql/multiple-active-result-sets-mars) |
| [Spatial Data Types](../relational-databases/spatial/spatial-data-sql-server.md) | | Yes | | Yes |
| [Table-Valued Parameters (TVP)](../relational-databases/tables/use-table-valued-parameters-database-engine.md) | [Yes](ado-net/sql/table-valued-parameters.md) | [Yes](ado-net/sql/table-valued-parameters.md) | [Yes](/dotnet/framework/data/adonet/sql/table-valued-parameters) | [Yes](/dotnet/framework/data/adonet/sql/table-valued-parameters) |
| [MultiSubnetFailover](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](ado-net/sql/sqlclient-support-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](ado-net/sql/sqlclient-support-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder.multisubnetfailover?view=netcore-1.0&preserve-view=true) | [Yes](/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder.multisubnetfailover?view=netframework-4.8&preserve-view=true) |
| [Transparent Network IP Resolution](odbc/using-transparent-network-ip-resolution.md) | | [Yes](/dotnet/api/microsoft.data.sqlclient.sqlconnection.connectionstring?view=sqlclient-dotnet-1.1&preserve-view=true) | | [Yes](/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=netframework-4.8&preserve-view=true) |

| <a id="table2"></a>Feature | [ODBC Driver for SQL Server on Windows](odbc/microsoft-odbc-driver-for-sql-server.md) | [ODBC Driver for SQL Server on Linux and macOS](odbc/microsoft-odbc-driver-for-sql-server.md) | [JDBC Driver for SQL Server](jdbc/microsoft-jdbc-driver-for-sql-server.md) | [OLE DB Driver for SQL Server](oledb/oledb-driver-for-sql-server.md) |
| :-- | :-- | :-- | :-- | :-- |
| [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md) (v13.1+) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md) (v13.1+) | [Yes](jdbc/using-always-encrypted-with-the-jdbc-driver.md) (v6.0+) |
| [Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md#enabling-always-encrypted-with-secure-enclaves) (v17.4+) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md#enabling-always-encrypted-with-secure-enclaves) (v17.4+) | [Yes](jdbc/using-always-encrypted-with-the-jdbc-driver.md) (v8.2+) | |
| [Azure Active Directory Access Token authentication](/azure/active-directory/develop/access-tokens) | [Yes](odbc/using-azure-active-directory.md#authenticating-with-an-access-token) (v13.1+) | [Yes](odbc/using-azure-active-directory.md#authenticating-with-an-access-token) (v13.1+) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md#connect-using-access-token) (v6.0+) | [Yes](oledb/features/using-azure-active-directory.md) (v18.2+) |
| [Azure Active Directory Password authentication](/azure/sql-database/sql-database-aad-authentication) |  [Yes](odbc/using-azure-active-directory.md) (v13.1+) | [Yes](odbc/using-azure-active-directory.md) (v13.1+) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) (v6.0+) | [Yes](oledb/features/using-azure-active-directory.md) (v18.2+) |
| [Azure Active Directory Integrated authentication](/azure/sql-database/sql-database-aad-authentication) | [Yes](odbc/using-azure-active-directory.md) (v13.1+) | [Yes](odbc/using-azure-active-directory.md) (v17.6+) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) (v6.0+) | [Yes](oledb/features/using-azure-active-directory.md) (v18.2+) |
| [Azure Active Directory Interactive (MFA) authentication](/azure/sql-database/sql-database-aad-authentication) | [Yes](odbc/using-azure-active-directory.md) (v17.1+) | | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) (v9.2+) | [Yes](oledb/features/using-azure-active-directory.md) (v18.3+) |
| [Azure Active Directory Managed Identity authentication](/azure/active-directory/managed-identities-azure-resources/overview) | [Yes](odbc/using-azure-active-directory.md) (v17.3+) | [Yes](odbc/using-azure-active-directory.md) (v17.3+) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) (v7.2+) | [Yes](oledb/features/using-azure-active-directory.md) (v18.3+) |
| [Azure Active Directory Service Principal authentication](/azure/active-directory/develop/app-objects-and-service-principals) | [Yes](odbc/using-azure-active-directory.md) (v17.7+) | [Yes](odbc/using-azure-active-directory.md) (v17.7+) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) (v9.2+) | [Yes](oledb/features/using-azure-active-directory.md) (v18.5+) |
| [Windows-Integrated authentication](/windows-server/security/windows-authentication/windows-authentication-overview) | Yes | [Yes](odbc/linux-mac/using-integrated-authentication.md) | [Yes](jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server.md) | Yes |
| [Bulk Copy](../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md) | [Yes](../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md) | [Yes](../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md) | [Yes](jdbc/using-bulk-copy-with-the-jdbc-driver.md) | [Yes](oledb/features/performing-bulk-copy-operations.md) |
| [Data Discovery and Classification metadata](../relational-databases/security/sql-data-discovery-and-classification.md) | [Yes](odbc/data-classification.md) (v17.2+) | [Yes](odbc/data-classification.md) (v17.2+) | [Yes](jdbc/data-discovery-classification-sample.md) (v7.0+) | [Yes](oledb/features/using-data-classification.md) (v18.5+) |
| [Multiple Active Result Sets (MARS)](../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md) | [Yes](../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md) | [Yes](../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md) | | [Yes](oledb/features/using-multiple-active-result-sets-mars.md) |
| [Spatial Data Types](../relational-databases/spatial/spatial-data-sql-server.md) | | | [Yes](jdbc/use-spatial-datatypes.md) (v7.0+) | |
| [Table-Valued Parameters (TVP)](../relational-databases/tables/use-table-valued-parameters-database-engine.md) | [Yes](../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md) | [Yes](../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md) | [Yes](jdbc/using-table-valued-parameters.md) (v6.0+) | [Yes](oledb/ole-db-table-valued-parameters/table-valued-parameters-ole-db.md) |
| [MultiSubnetFailover](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](jdbc/jdbc-driver-support-for-high-availability-disaster-recovery.md) | [Yes](oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) |
| [Transparent Network IP Resolution](odbc/using-transparent-network-ip-resolution.md) | [Yes](odbc/using-transparent-network-ip-resolution.md) (v13.0+) | [Yes](odbc/using-transparent-network-ip-resolution.md) (v13.1+) | [Yes](jdbc/setting-the-connection-properties.md) (v6.0+) | [Yes](oledb/features/using-transparent-network-ip-resolution.md) (v18.4+) |

| <a id="table3"></a>Feature | [Drivers for PHP for SQL Server on Windows](php/microsoft-php-driver-for-sql-server.md)<sup>[1](#note1)</sup> | [Drivers for PHP for SQL Server on Linux and macOS](php/microsoft-php-driver-for-sql-server.md)<sup>[1](#note1)</sup> | [Tedious (Node.js)](node-js/node-js-driver-for-sql-server.md) | [pyODBC (Python)](python/pyodbc/python-sql-driver-pyodbc.md)<sup>[1](#note1)</sup> |
| :-- | :-- | :-- | :-- | :-- |
| [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md) | [Yes](php/using-always-encrypted-php-drivers.md) (v5.2+) | [Yes](php/using-always-encrypted-php-drivers.md) (v5.2+) | | Yes |
| [Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md) | [Yes](php/always-encrypted-secure-enclaves.md) (v5.8+) | [Yes](php/always-encrypted-secure-enclaves.md) (v5.8+) | | Yes |
| [Azure Active Directory Access Token authentication](/azure/active-directory/develop/access-tokens) | [Yes](php/azure-active-directory.md) (v4.3+) | [Yes](php/azure-active-directory.md) (v4.3+) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | Yes |
| [Azure Active Directory Password authentication](/azure/sql-database/sql-database-aad-authentication) | [Yes](php/azure-active-directory.md) (v4.3+) | [Yes](php/azure-active-directory.md) (v4.3+) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | Yes |
| [Azure Active Directory Integrated authentication](/azure/sql-database/sql-database-aad-authentication) | [Yes](php/azure-active-directory.md) (v4.3+) | [Yes](php/azure-active-directory.md) (v4.3+) | | Yes |
| [Azure Active Directory Interactive (MFA) authentication](/azure/sql-database/sql-database-aad-authentication) | | | | Yes<sup>[2](#note2)</sup> |
| [Azure Active Directory Managed Identity authentication](/azure/active-directory/managed-identities-azure-resources/overview) | [Yes](php/azure-active-directory.md) (v5.6+) | [Yes](php/azure-active-directory.md) (v5.6+) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | Yes |
| [Azure Active Directory Service Principal authentication](/azure/active-directory/develop/app-objects-and-service-principals) | [Yes](php/azure-active-directory.md) (v5.9+) | [Yes](php/azure-active-directory.md) (v5.9+) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | Yes |
| [Windows-Integrated authentication](/windows-server/security/windows-authentication/windows-authentication-overview) | [Yes](php/how-to-connect-using-windows-authentication.md) | [Yes](odbc/linux-mac/using-integrated-authentication.md) | | Yes |
| [Bulk Copy](../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md) | | | [Yes](https://tediousjs.github.io/tedious/bulk-load.html) | |
| [Data Discovery and Classification metadata](../relational-databases/security/sql-data-discovery-and-classification.md) | Yes (v5.8+) | Yes (v5.8+) | | |
| [Multiple Active Result Sets (MARS)](../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md) | [Yes](php/how-to-disable-multiple-active-resultsets-mars.md) | [Yes](php/how-to-disable-multiple-active-resultsets-mars.md) | | Yes |
| [Spatial Data Types](../relational-databases/spatial/spatial-data-sql-server.md) | | | | |
| [Table-Valued Parameters (TVP)](../relational-databases/tables/use-table-valued-parameters-database-engine.md) | [Yes](php/use-table-valued-parameters.md) (v5.10+) | [Yes](php/use-table-valued-parameters.md) (v5.10+) | [Yes](https://tediousjs.github.io/tedious/parameters.html) | Yes |
| [MultiSubnetFailover](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | [Yes](php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | | [Yes](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) |
| [Transparent Network IP Resolution](odbc/using-transparent-network-ip-resolution.md) | [Yes](php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | [Yes](php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | | [Yes](odbc/using-transparent-network-ip-resolution.md) |

<a id="note1"></a><sup>1</sup> Since these drivers rely on the Microsoft ODBC Driver for SQL Server, a version of that driver that supports the feature must also be used.

<a id="note2"></a><sup>2</sup> Only on Windows.

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
