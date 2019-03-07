---
title: "System requirements for OLE DB Driver for SQL Server | Microsoft Docs"
description: "Requirements for OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "02/12/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "system requirements [OLE DB Driver for SQL Server]"
  - "data access [OLE DB Driver for SQL Server], system requirements"
  - "OLE DB Driver for SQL Server, system requirements"
  - "MSOLEDBSQL, system requirements"
author: pmasl
ms.author: pelopes
manager: craigg
---
# System requirements for OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../includes/driver_oledb_download.md)]

  To use data access features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] such as MARS, you must have the following software installed:  

-   OLE DB Driver for SQL Server on your client.  

-   An instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on your server.   

> [!NOTE]  
>  Make sure you log on with administrator privileges before installing this software.  

## Operating system requirements  
 For a list of operating systems that support OLE DB Driver for SQL Server, see [Support policies for OLE DB Driver for SQL Server](../oledb/applications/support-policies-for-oledb-driver-for-sql-server.md).  

 ## Azure Active Directory authentication requirements  
 When using Azure Active Directory authentication methods with the OLE DB driver, ensure that the [Active Directory Authentication Library for SQL Server](https://go.microsoft.com/fwlink/?LinkID=513072) has been installed. ADAL isn't required for the other authentication methods or OLE DB operations.
 For more information, see: [Using Azure Active Directory](features/using-azure-active-directory.md).

## SQL Server requirements  
 To use OLE DB Driver for SQL Server to access data in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases, you must have an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed.  

 [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] supports connections from all versions of MDAC, Windows Data Access Components, and all versions of OLE DB Driver for SQL Server. When an older client version connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], server data types not known to the client are mapped to types that are compatible with the client version. For more information, see [Data type compatibility for client versions](#data-type-compatibility-for-client-versions).  

## Cross-language requirements  
 The English-language version of OLE DB Driver for SQL Server is supported on all localized versions of supported operating systems. Localized versions of OLE DB Driver for SQL Server are supported on localized operating systems that are the same language as the localized OLE DB Driver for SQL Server version. Localized versions of OLE DB Driver for SQL Server are also supported on English-language versions of supported operating systems as long as the matching language settings are installed.  

 For upgrades:  

-   English-language versions of OLE DB Driver for SQL Server can be upgraded to any localized version of OLE DB Driver for SQL Server.  

-   Localized versions of OLE DB Driver for SQL Server can be upgraded to localized versions of OLE DB Driver for SQL Server of the same language.  

-   Localized version of OLE DB Driver for SQL Server can be upgraded to the English-language version of OLE DB Driver for SQL Server.  

-   Localized versions of OLE DB Driver for SQL Server cannot be upgraded to localized OLE DB Driver for SQL Server versions of a different localized language.  

## Data type compatibility for client versions  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and OLE DB Driver for SQL Server map new data types to older datatypes that are compatible with down-level clients, as shown in the table below.  

 OLE DB and ADO applications can use the **DataTypeCompatibility** connection string keyword with OLE DB Driver for SQL Server to operate with older data types. When **DataTypeCompatibility=80**, OLE DB clients will connect using the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] tabular data stream (TDS) version, rather than the  TDS version. This means that for [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later data types, down-level conversion will be performed by the server, rather than by OLE DB Driver for SQL Server. It also means that the features available on the connection will be limited to the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] feature set. Attempts to use new datatypes or features are detected as early as possible on API calls and errors are returned to the calling application, rather than attempting to pass invalid requests to the server.   


 IDBInfo::GetKeywords will always return a keyword list that corresponds to the server version on the connection and is not affected by **DataTypeCompatibility**.  

|Data type|SQL Server Native Client<br /><br />SQL Server 2005|SQL Server Native Client 11.0<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|OLE DB Driver for SQL Server|Windows Data Access Components, MDAC, and<br /><br /> OLE DB Driver for SQL Server OLE DB applications with DataTypeCompatibility=80|  
|---------------|--------------------------------------------------|-------------------------------------------------------------|-------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------|  
|CLR UDT (\<= 8 Kb)|udt|udt|udt|Varbinary|  
|varbinary(max)|varbinary|varbinary|varbinary|Image|  
|varchar(max)|varchar|varchar|varchar|Text|  
|nvarchar(max)|nvarchar|nvarchar|nvarchar|Ntext|  
|xml|xml|xml|xml|Ntext|  
|CLR UDT (> 8 Kb)|varbinary|udt|udt|Image|  
|date|varchar|date|date|Varchar|  
|datetime2|varchar|datetime2|datetime2|Varchar|  
|datetimeoffset|varchar|datetimeoffset|datetimeoffset|Varchar|  
|time|varchar|time|time|Varchar|  

## See also  
 [OLE DB Driver for SQL Server](../oledb/oledb-driver-for-sql-server.md)   
 [Installing OLE DB Driver for SQL Server](../oledb/applications/installing-oledb-driver-for-sql-server.md)  
