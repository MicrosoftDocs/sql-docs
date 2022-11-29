---
title: "Programming"
description: Use these resources to understand SQL Server Native Client, a stand-alone data access API, used for both OLE DB and ODBC. 
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLNCLI, about SQL Server Native Client"
  - "SQL Server Native Client, about SQL Server Native Client"
  - "data access [SQL Server Native Client], about SQL Server Native Client"
  - "data access [SQL Server Native Client]"
  - "SQL Server Native Client"
  - "SQLNCLI"
  - "native data access [SQL Server Native Client]"
ms.assetid: 14ba2cb1-a424-4e4d-b224-0bf1015ab801
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Native Client Programming
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../includes/snac-removed-oledb-and-odbc.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is a stand-alone data access application programming interface (API), used for both OLE DB and ODBC, that was introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client combines the SQL OLE DB provider and the SQL ODBC driver into one native dynamic-link library (DLL). It also provides new functionality above and beyond that supplied by the Windows Data Access Components (Windows DAC, formerly Microsoft Data Access Components, or MDAC). [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client can be used to create new applications or enhance existing applications that need to take advantage of features introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], such as multiple active result sets (MARS), user-defined data types (UDT), query notifications, snapshot isolation, and XML data type support.  
  
> [!NOTE]  
>  For a list of the differences between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client and Windows DAC, plus information about issues to consider before updating a Windows DAC application to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, see [Updating an Application to SQL Server Native Client from MDAC](../../relational-databases/native-client/applications/updating-an-application-to-sql-server-native-client-from-mdac.md).  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver is always used in conjunction with the ODBC Driver Manager supplied with Windows DAC. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider can be used in conjunction with OLE DB Core Services supplied with Windows DAC, but this is not a requirement; the choice to use Core Services or not depends on the requirements of the individual application (for example, if connection pooling is required).  
  
 ActiveX Data Object (ADO) applications may use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider, but it is recommended to use ADO in conjunction with the **DataTypeCompatibility** connection string keyword (or its corresponding **DataSource** property). When using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider, ADO applications may exploit those new features introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] that are available via the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client via connection string keywords or OLE DB properties or [!INCLUDE[tsql](../../includes/tsql-md.md)]. For more information about the use of these features with ADO, see [Using ADO with SQL Server Native Client](../../relational-databases/native-client/applications/using-ado-with-sql-server-native-client.md).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client was designed to provide a simplified method of gaining native data access to SQL Server using either OLE DB or ODBC. It is simplified in that it combines OLE DB and ODBC technologies into one library, and it provides a way to innovate and evolve new data access features without changing the current Windows DAC components, which are now part of the Microsoft Windows platform.  
  
 While [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client uses components in Windows DAC, it is not explicitly dependent on a particular version of Windows DAC. You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client with the version of Windows DAC that is installed with any operating system supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
## In This Section  
 [SQL Server Native Client](../../relational-databases/native-client/sql-server-native-client.md)  
 Lists the significant new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client features.  
  
 [When to Use SQL Server Native Client](../../relational-databases/native-client/when-to-use-sql-server-native-client.md)  
 Discusses how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client fits in with Microsoft data access technologies, how it compares to Windows DAC and ADO.NET, and provides pointers for deciding which data access technology to use.  
  
 [SQL Server Native Client Features](../../relational-databases/native-client/features/sql-server-native-client-features.md)  
 Describes the features supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
 [Building Applications with SQL Server Native Client](../../relational-databases/native-client/applications/building-applications-with-sql-server-native-client.md)  
 Provides an overview of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client development, including how it differs from Windows DAC, the components that it uses, and how ADO can be used with it.  
  
 This section also discusses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client installation and deployment, including how to redistribute the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client library.  
  
 [System Requirements for SQL Server Native Client](../../relational-databases/native-client/system-requirements-for-sql-server-native-client.md)  
 Discusses the system resources needed to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
 [SQL Server Native Client &#40;OLE DB&#41;](../../relational-databases/native-client/ole-db/sql-server-native-client-ole-db.md)  
 Provides information about using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider.  
  
 [SQL Server Native Client &#40;ODBC&#41;](../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)  
 Provides information about using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver.  
  
 [Finding More SQL Server Native Client Information](../../relational-databases/native-client/finding-more-sql-server-native-client-information.md)  
 Provides additional resources about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, including links to external resources and getting further assistance.  
  
 [SQL Server Native Client Errors](./sql-server-native-client-error-mssqlserver-50000.md)  
 Contains topics about runtime errors associated with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
## See Also  
 [Updating an Application from SQL Server 2005 Native Client](../../relational-databases/native-client/applications/updating-an-application-from-sql-server-2005-native-client.md)   
 [ODBC How-to Topics](../../relational-databases/native-client-odbc-how-to/odbc-how-to-topics.md)   
 [OLE DB How-to Topics](../../relational-databases/native-client-ole-db-how-to/ole-db-how-to-topics.md)  
  
