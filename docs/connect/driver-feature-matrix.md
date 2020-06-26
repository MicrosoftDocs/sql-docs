---
title: "Driver feature support matrix"
description: "Learn which popular features are supported in drivers for SQL Server and where to find information about them."
ms.custom: ""
ms.date: 06/17/2020
ms.prod: sql
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-daenge
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
| [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md) | | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md) |
| [Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md#enabling-always-encrypted-with-secure-enclaves) | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md#enabling-always-encrypted-with-secure-enclaves) | | [Yes](ado-net/sql/sqlclient-support-always-encrypted.md#enabling-always-encrypted-with-secure-enclaves) |
| [Azure Active Directory Access Token authentication](/azure/active-directory/develop/access-tokens) | [Yes](/dotnet/api/system.data.sqlclient.sqlconnection.accesstoken) | [Yes](/dotnet/api/microsoft.data.sqlclient.sqlconnection.accesstoken) | [Yes](/dotnet/api/microsoft.data.sqlclient.sqlconnection.accesstoken) | [Yes](/dotnet/api/microsoft.data.sqlclient.sqlconnection.accesstoken) |
| [Azure Active Directory Password authentication](/azure/sql-database/sql-database-aad-authentication) | Yes | Yes | | Yes |
| [Azure Active Directory Integrated authentication](/azure/sql-database/sql-database-aad-authentication) | Yes | Yes | | Yes |
| [Azure Active Directory Interactive (MFA) authentication](/azure/sql-database/sql-database-aad-authentication) | Yes | Yes | | Yes |
| [Azure Active Directory Managed Identity authentication](/azure/active-directory/managed-identities-azure-resources/overview) | | | | |
| [Azure Active Directory Service Principal authentication](/azure/active-directory/develop/app-objects-and-service-principals) | Yes | Yes | | |
| [Windows-Integrated authentication](/windows-server/security/windows-authentication/windows-authentication-overview) | [Yes](ado-net/sql/authentication-sql-server.md) | [Yes](ado-net/sql/authentication-sql-server.md) | [Yes](/dotnet/framework/data/adonet/sql/authentication-in-sql-server) | [Yes](/dotnet/framework/data/adonet/sql/authentication-in-sql-server) |
| [Bulk Copy](../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md) | [Yes](ado-net/sql/bulk-copy-operations-sql-server.md) | [Yes](ado-net/sql/bulk-copy-operations-sql-server.md) | [Yes](/dotnet/framework/data/adonet/sql/bulk-copy-operations-in-sql-server) | [Yes](/dotnet/framework/data/adonet/sql/bulk-copy-operations-in-sql-server) |
| [Data Sensitivity and Classification metadata](../relational-databases/security/sql-data-discovery-and-classification.md) | Yes | Yes | Yes | Yes |
| [Multiple Active Result Sets (MARS)](../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md) | [Yes](ado-net/sql/multiple-active-result-sets-mars.md) | [Yes](ado-net/sql/multiple-active-result-sets-mars.md) | [Yes](/dotnet/framework/data/adonet/sql/multiple-active-result-sets-mars) | [Yes](/dotnet/framework/data/adonet/sql/multiple-active-result-sets-mars) |
| [Spatial Data Types](../relational-databases/spatial/spatial-data-sql-server.md) | | Yes | | Yes |
| [Table-Valued Parameters (TVP)](../relational-databases/tables/use-table-valued-parameters-database-engine.md) | [Yes](ado-net/sql/table-valued-parameters.md) | [Yes](ado-net/sql/table-valued-parameters.md) | [Yes](/dotnet/framework/data/adonet/sql/table-valued-parameters) | [Yes](/dotnet/framework/data/adonet/sql/table-valued-parameters) |
| [MultiSubnetFailover](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](ado-net/sql/sqlclient-support-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](ado-net/sql/sqlclient-support-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder.multisubnetfailover?view=netcore-1.0) | [Yes](/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder.multisubnetfailover?view=netframework-4.8) |
| [Transparent Network IP Resolution](odbc/using-transparent-network-ip-resolution.md) | | [Yes](/dotnet/api/microsoft.data.sqlclient.sqlconnection.connectionstring?view=sqlclient-dotnet-1.1) | | [Yes](/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=netframework-4.8) |
| &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; |

| <a id="table2"></a>Feature | [ODBC Driver for SQL Server on Windows](odbc/microsoft-odbc-driver-for-sql-server.md) | [ODBC Driver for SQL Server on Linux and macOS](odbc/microsoft-odbc-driver-for-sql-server.md) | [JDBC Driver for SQL Server](jdbc/microsoft-jdbc-driver-for-sql-server.md) | [OLE DB Driver for SQL Server](oledb/oledb-driver-for-sql-server.md) |
| :-- | :-- | :-- | :-- | :-- |
| [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md) | [Yes](jdbc/using-always-encrypted-with-the-jdbc-driver.md) |
| [Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md#enabling-always-encrypted-with-secure-enclaves) | [Yes](odbc/using-always-encrypted-with-the-odbc-driver.md#enabling-always-encrypted-with-secure-enclaves) | [Yes](jdbc/using-always-encrypted-with-the-jdbc-driver.md) | |
| [Azure Active Directory Access Token authentication](/azure/active-directory/develop/access-tokens) | [Yes](odbc/using-azure-active-directory.md#authenticating-with-an-access-token) | [Yes](odbc/using-azure-active-directory.md#authenticating-with-an-access-token) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md#connecting-using-access-token) | [Yes](oledb/features/using-azure-active-directory.md) |
| [Azure Active Directory Password authentication](/azure/sql-database/sql-database-aad-authentication) |  [Yes](odbc/using-azure-active-directory.md) | [Yes](odbc/using-azure-active-directory.md)<sup>[1](#note1)</sup> | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) | [Yes](oledb/features/using-azure-active-directory.md) |
| [Azure Active Directory Integrated authentication](/azure/sql-database/sql-database-aad-authentication) | [Yes](odbc/using-azure-active-directory.md) | | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) | [Yes](oledb/features/using-azure-active-directory.md) |
| [Azure Active Directory Interactive (MFA) authentication](/azure/sql-database/sql-database-aad-authentication) | [Yes](odbc/using-azure-active-directory.md) | | | [Yes](oledb/features/using-azure-active-directory.md) |
| [Azure Active Directory Managed Identity authentication](/azure/active-directory/managed-identities-azure-resources/overview) | [Yes](odbc/using-azure-active-directory.md) | [Yes](odbc/using-azure-active-directory.md) | [Yes](jdbc/connecting-using-azure-active-directory-authentication.md) | [Yes](oledb/features/using-azure-active-directory.md) |
| [Azure Active Directory Service Principal authentication](/azure/active-directory/develop/app-objects-and-service-principals) | | | | |
| [Windows-Integrated authentication](/windows-server/security/windows-authentication/windows-authentication-overview) | Yes | [Yes](odbc/linux-mac/using-integrated-authentication.md) | [Yes](jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server.md) | Yes |
| [Bulk Copy](../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md) | [Yes](../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md) | [Yes](../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md) | [Yes](jdbc/using-bulk-copy-with-the-jdbc-driver.md) | [Yes](oledb/features/performing-bulk-copy-operations.md) |
| [Data Discovery and Classification metadata](../relational-databases/security/sql-data-discovery-and-classification.md) | [Yes](odbc/data-classification.md) | [Yes](odbc/data-classification.md) | [Yes](jdbc/data-discovery-classification-sample.md) | |
| [Multiple Active Result Sets (MARS)](../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md) | [Yes](../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md) | [Yes](../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md) | | [Yes](oledb/features/using-multiple-active-result-sets-mars.md) |
| [Spatial Data Types](../relational-databases/spatial/spatial-data-sql-server.md) | | | [Yes](jdbc/use-spatial-datatypes.md) | |
| [Table-Valued Parameters (TVP)](../relational-databases/tables/use-table-valued-parameters-database-engine.md) | [Yes](../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md) | [Yes](../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md) | [Yes](jdbc/using-table-valued-parameters.md) | [Yes](oledb/ole-db-table-valued-parameters/table-valued-parameters-ole-db.md) |
| [MultiSubnetFailover](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](jdbc/jdbc-driver-support-for-high-availability-disaster-recovery.md) | [Yes](oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) |
| [Transparent Network IP Resolution](odbc/using-transparent-network-ip-resolution.md) | [Yes](odbc/using-transparent-network-ip-resolution.md) | [Yes](odbc/using-transparent-network-ip-resolution.md) | [Yes](jdbc/setting-the-connection-properties.md) | [Yes](oledb/features/using-transparent-network-ip-resolution.md) |
| &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; |

| <a id="table3"></a>Feature | [Drivers for PHP for SQL Server on Windows](php/microsoft-php-driver-for-sql-server.md)<sup>[2](#note2)</sup> | [Drivers for PHP for SQL Server on Linux and macOS](php/microsoft-php-driver-for-sql-server.md)<sup>[2](#note2)</sup> | [Tedious (Node.js)](node-js/node-js-driver-for-sql-server.md) | [pyODBC (Python)](python/pyodbc/python-sql-driver-pyodbc.md)<sup>[2](#note2)</sup> |
| :-- | :-- | :-- | :-- | :-- |
| [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md) | [Yes](php/using-always-encrypted-php-drivers.md) | [Yes](php/using-always-encrypted-php-drivers.md) | | Yes |
| [Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md) | [Yes](php/always-encrypted-secure-enclaves.md) | [Yes](php/always-encrypted-secure-enclaves.md) | | Yes |
| [Azure Active Directory Access Token authentication](/azure/active-directory/develop/access-tokens) | [Yes](php/azure-active-directory.md) | [Yes](php/azure-active-directory.md) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | Yes |
| [Azure Active Directory Password authentication](/azure/sql-database/sql-database-aad-authentication) | [Yes](php/azure-active-directory.md) | [Yes](php/azure-active-directory.md)<sup>[1](#note1)</sup> | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | Yes |
| [Azure Active Directory Integrated authentication](/azure/sql-database/sql-database-aad-authentication) | | | | Yes<sup>[3](#note3)</sup> |
| [Azure Active Directory Interactive (MFA) authentication](/azure/sql-database/sql-database-aad-authentication) | | | | Yes<sup>[3](#note3)</sup> |
| [Azure Active Directory Managed Identity authentication](/azure/active-directory/managed-identities-azure-resources/overview) | [Yes](php/azure-active-directory.md) | [Yes](php/azure-active-directory.md) | [Yes](https://tediousjs.github.io/tedious/api-connection.html#function_newConnection) | Yes |
| [Azure Active Directory Service Principal authentication](/azure/active-directory/develop/app-objects-and-service-principals) | | | | |
| [Windows-Integrated authentication](/windows-server/security/windows-authentication/windows-authentication-overview) | [Yes](php/how-to-connect-using-windows-authentication.md) | [Yes](odbc/linux-mac/using-integrated-authentication.md) | | Yes |
| [Bulk Copy](../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md) | | | [Yes](https://tediousjs.github.io/tedious/bulk-load.html) | |
| [Data Discovery and Classification metadata](../relational-databases/security/sql-data-discovery-and-classification.md) | Yes | Yes | | |
| [Multiple Active Result Sets (MARS)](../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md) | [Yes](php/how-to-disable-multiple-active-resultsets-mars.md) | [Yes](php/how-to-disable-multiple-active-resultsets-mars.md) | | Yes |
| [Spatial Data Types](../relational-databases/spatial/spatial-data-sql-server.md) | | | | |
| [Table-Valued Parameters (TVP)](../relational-databases/tables/use-table-valued-parameters-database-engine.md) | | | [Yes](https://tediousjs.github.io/tedious/parameters.html) | Yes |
| [MultiSubnetFailover](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) | [Yes](php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | [Yes](php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | | [Yes](../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover) |
| [Transparent Network IP Resolution](odbc/using-transparent-network-ip-resolution.md) | [Yes](php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | [Yes](php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | | [Yes](odbc/using-transparent-network-ip-resolution.md) |
| &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; |

<a id="note1"></a><sup>1</sup> Active Directory federated authentication without password hash synchronization or pass-through authentication isn't supported on Linux and macOS.

<a id="note2"></a><sup>2</sup> Since these drivers rely on the Microsoft ODBC Driver for SQL Server, a version of that driver that supports the feature must also be used.

<a id="note3"></a><sup>3</sup> Only on Windows.

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
