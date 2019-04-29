---
title: "Microsoft OLE DB Driver for SQL Server | Microsoft Docs"
description: "Microsoft OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "02/12/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "MSOLEDBSQL, about OLE DB Driver for SQL Server"
  - "OLE DB Driver for SQL Server, about OLE DB Driver for SQL Server"
  - "data access [OLE DB Driver for SQL Server], about OLE DB Driver for SQL Server"
  - "data access [OLE DB Driver for SQL Server]"
  - "OLE DB Driver for SQL Server"
  - "MSOLEDBSQL"
  - "native data access [OLE DB Driver for SQL Server]"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Microsoft OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../includes/driver_oledb_download.md)]

The OLE DB Driver for SQL Server is a stand-alone data access application programming interface (API), used for OLE DB, that was introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. OLE DB Driver for SQL Server delivers the SQL OLE DB driver in one dynamic-link library (DLL). It also provides new functionality above and beyond that supplied by the Windows Data Access Components (Windows DAC, formerly Microsoft Data Access Components, or MDAC). The OLE DB Driver for SQL Server can be used to create new applications or enhance existing applications that need to take advantage of features introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], such as multiple active result sets (MARS), user-defined data types (UDT), query notifications, snapshot isolation, and XML data type support.  
  
> [!NOTE]  
> For a list of the differences between OLE DB Driver for SQL Server and Windows DAC, plus information about issues to consider before updating a Windows DAC application to OLE DB Driver for SQL Server, see [Updating an Application to OLE DB Driver for SQL Server from MDAC](../oledb/applications/updating-an-application-to-oledb-driver-for-sql-server-from-mdac.md).  

> [!IMPORTANT]
> The previous [Microsoft OLE DB Provider for SQL Server](../sql/ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server.md) (SQLOLEDB) remains deprecated and it is not recommended to use it for new development work.
  
 The OLE DB Driver for SQL Server can be used in conjunction with OLE DB Core Services supplied with Windows DAC, but this is not a requirement; the choice to use Core Services or not depends on the requirements of the individual application (for example, if connection pooling is required).  
  
 ActiveX Data Object (ADO) applications may use the OLE DB Driver for SQL Server, but it is recommended to use ADO in conjunction with the **DataTypeCompatibility** connection string keyword (or its corresponding **DataSource** property). When using the OLE DB Driver for SQL Server, ADO applications may exploit those new features introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] that are available via the OLE DB Driver for SQL Server via connection string keywords or OLE DB properties or [!INCLUDE[tsql](../../includes/tsql-md.md)]. For more information about the use of these features with ADO, see [Using ADO with OLE DB Driver for SQL Server](../oledb/applications/using-ado-with-oledb-driver-for-sql-server.md).  
  
 OLE DB Driver for SQL Server was designed to provide a simplified method of gaining native data access to SQL Server using OLE DB. It provides a way to innovate and evolve new data access features without changing the current Windows DAC components, which are now part of the Microsoft Windows platform.  
  
 While OLE DB Driver for SQL Server uses components in Windows DAC, it is not explicitly dependant on a particular version of Windows DAC. You can use OLE DB Driver for SQL Server with the version of Windows DAC that is installed with any operating system supported by OLE DB Driver for SQL Server.  

 ## Different generations of OLE DB Drivers

There are three distinct generations of Microsoft OLE DB providers for SQL Server.

### 1. Microsoft OLE DB Provider for SQL Server (SQLOLEDB)
The [Microsoft OLE DB Provider for SQL Server](../../ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server.md) (SQLOLEDB) still ships as part of [Windows Data Access Components](https://msdn.microsoft.com/library/ms692897.aspx). It is not maintained anymore and it is not recommended to use this driver for new development.

### 2. SQL Server Native Client (SNAC)
Starting in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the [SQL Server Native Client (SNAC)](../../relational-databases/native-client/sql-server-native-client.md) includes an OLE DB provider interface (SQLNCLI) and is the OLE DB provider that shipped with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] through [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].

It was [announced as deprecated in 2011](https://blogs.msdn.microsoft.com/sqlnativeclient/2011/08/29/microsoft-is-aligning-with-odbc-for-native-relational-data-access/) and it is not recommended to use this driver for new development. For more information about the SNAC lifecycle and available downloads, refer to [SNAC lifecycle explained](https://blogs.msdn.microsoft.com/sqlreleaseservices/snac-lifecycle-explained/).

### 3. Microsoft OLE DB Driver for SQL Server (MSOLEDBSQL)
OLE DB was [undeprecated](https://blogs.msdn.microsoft.com/sqlnativeclient/2017/10/06/announcing-the-new-release-of-ole-db-driver-for-sql-server/) and released in 2018.

The new OLE DB provider is called the Microsoft OLE DB Driver for SQL Server (MSOLEDBSQL). The new provider will be updated with the most recent server features going forward.

> [!NOTE]
> To use the new Microsoft OLE DB Driver for SQL Server in existing applications, you should plan to convert your connection strings from SQLOLEDB or SQLNCLI, to MSOLEDBSQL.
  
## In this section  
[When to use OLE DB Driver for SQL Server](../oledb/when-to-use-oledb-driver-for-sql-server.md)  
 Discusses how OLE DB Driver for SQL Server fits in with Microsoft data access technologies, how it compares to Windows DAC and ADO.NET, and provides pointers for deciding which data access technology to use.  
  
 [OLE DB Driver for SQL Server features](../oledb/features/oledb-driver-for-sql-server-features.md )  
 Describes the features supported by OLE DB Driver for SQL Server.  
  
 [Building applications with OLE DB Driver for SQL Server](../oledb/applications/building-applications-with-oledb-driver-for-sql-server.md)  
 Provides an overview of OLE DB Driver for SQL Server development, including how it differs from Windows DAC, the components that it uses, and how ADO can be used with it.  
  
 This section also discusses OLE DB Driver for SQL Server installation and deployment, including how to redistribute the OLE DB Driver for SQL Server library.  
  
 [System requirements for OLE DB Driver for SQL Server](../oledb/system-requirements-for-oledb-driver-for-sql-server.md)  
 Discusses the system resources needed to use OLE DB Driver for SQL Server.  
  
 [OLE DB Driver for SQL Server programming](../oledb/ole-db/oledb-driver-for-sql-server-programming.md)  
 Provides information about using the OLE DB Driver for SQL Server.  
  
 [Finding more OLE DB Driver for SQL Server information](../oledb/finding-more-oledb-driver-for-sql-server-information.md)  
 Provides additional resources about OLE DB Driver for SQL Server, including links to external resources and getting further assistance.  
  
  
## See also  
 [Updating an application from SQL Server 2005 Native Client](../oledb/applications/updating-an-application-from-sql-server-2005-native-client.md)    
 [OLE DB how-to topics](../oledb/ole-db-how-to/ole-db-how-to-topics.md)  
  
  
