---
title: Microsoft OLE DB Driver for SQL Server
description: The Microsoft OLE DB Driver for SQL Server provides connectivity to SQL Server and Azure SQL Database via standard OLE DB APIs.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, sunilbs, mcimfl
ms.date: 09/10/2025
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
ms.custom:
  - ignite-2024
helpviewer_keywords:
  - "MSOLEDBSQL, about OLE DB Driver for SQL Server"
  - "OLE DB Driver for SQL Server, about OLE DB Driver for SQL Server"
  - "data access [OLE DB Driver for SQL Server], about OLE DB Driver for SQL Server"
  - "data access [OLE DB Driver for SQL Server]"
  - "OLE DB Driver for SQL Server"
  - "MSOLEDBSQL"
  - "native data access [OLE DB Driver for SQL Server]"
---

# Microsoft OLE DB Driver for SQL Server

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

[!INCLUDE [Driver_OLEDB_Download](../../includes/driver_oledb_download.md)]

The OLE DB Driver for SQL Server is a stand-alone data access application programming interface (API), that is part of [OLE DB](/cpp/data/oledb/ole-db-programming-overview). It was first released in 2018 as version 18 and included in [!INCLUDE [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. The OLE DB Driver for SQL Server is generally backward compatible with SQL Server Native Client (SNAC). (For differences between major versions of OLE DB Driver for SQL Server, including breaking changes, see [Major version differences](major-version-differences.md).) The driver provides new functionality beyond SNAC and the SQL Server OLE DB provider supplied by the Windows Data Access Components (Windows DAC, formerly Microsoft Data Access Components, or MDAC). The OLE DB Driver for SQL Server can be used to create new applications or enhance existing applications that need to take advantage of features such as multiple active result sets (MARS), user-defined data types (UDT), query notifications, snapshot isolation, XML data type support, Microsoft Entra ID, and strict encryption.

For a list of the differences between OLE DB Driver for SQL Server and Windows DAC, plus information about issues to consider before updating a Windows DAC application to OLE DB Driver for SQL Server, see [Updating an Application to OLE DB Driver for SQL Server from MDAC](applications/updating-an-application-to-oledb-driver-for-sql-server-from-mdac.md).

The OLE DB Driver for SQL Server can be used with OLE DB Core Services supplied with Windows DAC, but this use isn't a requirement. The choice to use Core Services depends on the requirements of the individual application (for example, if connection pooling is required).

ActiveX Data Object (ADO) applications can use the OLE DB Driver for SQL Server, but you should use ADO with the `DataTypeCompatibility` connection string keyword (or its corresponding `DataSource` property). The OLE DB Driver for SQL Server allows ADO applications to use features introduced in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)], which are available with OLE DB Driver for SQL Server via connection string keywords or OLE DB properties or [!INCLUDE [tsql](../../includes/tsql-md.md)]. For more information about the use of these features with ADO, see [Using ADO with OLE DB Driver for SQL Server](applications/using-ado-with-oledb-driver-for-sql-server.md).

OLE DB Driver for SQL Server was designed to provide a simplified method of gaining native data access to SQL Server using OLE DB. It provides a way to innovate and evolve new data access features without changing the current Windows DAC components, which are now part of the Microsoft Windows platform.

While OLE DB Driver for SQL Server uses components in Windows DAC, it isn't explicitly dependent on a particular version of Windows DAC. You can use OLE DB Driver for SQL Server with the version of Windows DAC that is installed with any operating system supported by OLE DB Driver for SQL Server.

## Different generations of OLE DB Drivers

There are three distinct generations of Microsoft OLE DB providers for SQL Server.

### 1. Microsoft OLE DB Driver for SQL Server (MSOLEDBSQL) (recommended)

The newest generation of the OLE DB driver (MSOLEDBSQL) offers the latest features, including:

- TLS 1.3 support (version 19+, MSOLEDBSQL19)
- multiple language support
- support for various SQL Server features for Availability Groups
- Microsoft Entra ID support

The OLE DB provider was [undeprecated](/archive/blogs/sqlnativeclient/announcing-the-new-release-of-ole-db-driver-for-sql-server) and released in 2018. For details on improvements and fixes, see [Release notes](release-notes-for-oledb-driver-for-sql-server.md). Previously called OLE DB provider, the new name is the Microsoft OLE DB Driver for SQL Server (MSOLEDBSQL). The new driver is updated with the most recent server features.

You should use the new Microsoft OLE DB Driver for SQL Server with new and existing applications. Convert your existing applications connection strings from SQLOLEDB or SQLNCLI, to MSOLEDBSQL19 or MSOLEDBSQL.

### 2. SQL Server Native Client (SNAC)

[SQL Server Native Client (SNAC)](../../relational-databases/native-client/sql-server-native-client.md) was available starting with [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. SNAC includes an OLE DB provider interface (SQLNCLI) and is the OLE DB provider that shipped with [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] through [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].

The [SQL Server Native Client](../../relational-databases/native-client/sql-server-native-client.md) (often abbreviated SNAC) was removed from [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] 19 (SSMS). The SQL Server Native Client OLE DB providers (SQLNCLI or SQLNCLI11) aren't recommended for new development. Switch to the new Microsoft OLE DB Driver (MSOLEDBSQL) for SQL Server going forward. For more information about the SNAC lifecycle and available downloads, see [SNAC lifecycle explained](/archive/blogs/sqlreleaseservices/snac-lifecycle-explained).

### 3. Microsoft OLE DB Provider for SQL Server (SQLOLEDB)

The [Microsoft OLE DB Provider for SQL Server](../../ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server.md) (SQLOLEDB) was the original OLE DB connectivity software for SQL Server applications. It still ships as part of [Windows Data Access Components](/previous-versions/windows/desktop/ms692897(v=vs.85)). It isn't maintained anymore and it isn't recommended to use this driver for new development. The legacy Microsoft OLE DB Provider for SQL Server (SQLOLEDB) isn't recommended for new development. Switch to the new Microsoft OLE DB Driver (MSOLEDBSQL / MSOLEDBSQL19) for SQL Server going forward.

## In this section

| Article | Description |
| --- | --- |
| [When to Use OLE DB Driver for SQL Server](when-to-use-oledb-driver-for-sql-server.md) | Discusses how OLE DB Driver for SQL Server fits in with Microsoft data access technologies, how it compares to Windows DAC and ADO.NET, and provides pointers for deciding which data access technology to use. |
| [OLE DB Driver for SQL Server features](features/oledb-driver-for-sql-server-features.md) | Describes the features supported by OLE DB Driver for SQL Server. |
| [Building applications with OLE DB Driver for SQL Server](applications/building-applications-with-oledb-driver-for-sql-server.md) | Provides an overview of OLE DB Driver for SQL Server development, including how it differs from Windows DAC, the components that it uses, and how ADO can be used with it. This section also discusses OLE DB Driver for SQL Server installation and deployment, including how to redistribute the OLE DB Driver for SQL Server library. |
| [System requirements for OLE DB Driver for SQL Server](system-requirements-for-oledb-driver-for-sql-server.md) | Discusses the system resources needed to use OLE DB Driver for SQL Server. |
| [OLE DB Driver for SQL Server Programming](ole-db/oledb-driver-for-sql-server-programming.md) | Provides information about using the OLE DB Driver for SQL Server. |
| [Finding More OLE DB Driver for SQL Server Information](finding-more-oledb-driver-for-sql-server-information.md) | Provides more resources about OLE DB Driver for SQL Server, including links to external resources and getting further assistance. |

## Related content

- [Updating Applications from SQL Server 2005 Native Client](applications/updating-an-application-from-sql-server-2005-native-client.md)
- [OLE DB How-to articles](ole-db-how-to/ole-db-how-to-topics.md)
