---
description: "System Requirements for SQL Server Native Client"
title: "System Requirements"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "system requirements [SQL Server Native Client]"
  - "data access [SQL Server Native Client], system requirements"
  - "SQL Server Native Client, system requirements"
  - "SQLNCLI, system requirements"
ms.assetid: 1c8e2f8a-a440-44da-8e3a-af632d34c52c
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# System Requirements for SQL Server Native Client
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../includes/snac-removed-oledb-and-odbc.md)]


  To use data access features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] such as MARS, you must have the following software installed:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client on your client.  
  
-   An instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on your server.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client requires Windows Installer 3.1. Windows Installer 3.1 is already installed on [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows operating systems. For all other platforms you need to explicitly install it. For more information, see [Windows Installer 3.1 Redistributable (v2)](https://support.microsoft.com/topic/windows-installer-3-1-v2-3-1-4000-2435-is-available-e3978d9b-5fbf-bfec-71b9-1a463290065a).  
  
> [!NOTE]  
>  Make sure you log on with administrator privileges before installing this software.  
  
## Operating System Requirements  
 For a list of operating systems that support [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, see [Support Policies for SQL Server Native Client](../../relational-databases/native-client/applications/support-policies-for-sql-server-native-client.md).  
  
## SQL Server Requirements  
 To use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client to access data in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases, you must have an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed.  
  
 [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] supports connections from all versions of MDAC, Windows Data Access Components, and all versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client. When an older client version connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], server data types not known to the client are mapped to types that are compatible with the client version. For more information, see Data Type Compatibility for Client Versions, later in this topic.  
  
## Cross-Language Requirements  
 The English-language version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is supported on all localized versions of supported operating systems. Localized versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client are supported on localized operating systems that are the same language as the localized [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client version. Localized versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client are also supported on English-language versions of supported operating systems as long as the matching language settings are installed.  
  
 For upgrades:  
  
-   English-language versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client can be upgraded to any localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
-   Localized versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client can be upgraded to localized versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client of the same language.  
  
-   Localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client can be upgraded to the English-language version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
-   Localized versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client cannot be upgraded to localized [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client versions of a different localized language.  
  
## Data Type Compatibility for Client Versions  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client map new data types to older datatypes that are compatible with down-level clients, as shown in the table below.  
  
 OLE DB and ADO applications can use the **DataTypeCompatibility** connection string keyword with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client to operate with older data types. When **DataTypeCompatibility=80**, OLE DB clients will connect using the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] tabular data stream (TDS) version, rather than the  TDS version. This means that for [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later data types, down-level conversion will be performed by the server, rather than by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client. It also means that the features available on the connection will be limited to the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] feature set. Attempts to use new datatypes or features are detected as early as possible on API calls and errors are returned to the calling application, rather than attempting to pass invalid requests to the server.  
  
 There is no **DataTypeCompatibility** control for ODBC.  
  
 IDBInfo::GetKeywords will always return a keyword list that corresponds to the server version on the connection and is not affected by **DataTypeCompatibility**.  
  
|Data type|SQL Server Native Client<br /><br /> SQL Server 2005|SQL Server Native Client 11.0<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|Windows Data Access Components, MDAC, and<br /><br /> SQL Server Native Client OLE DB applications with DataTypeCompatibility=80|  
|---------------|--------------------------------------------------|-------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------|  
|CLR UDT (\<= 8Kb)|udt|Udt|Varbinary|  
|varbinary(max)|varbinary|varbinary|Image|  
|varchar(max)|varchar|varchar|Text|  
|nvarchar(max)|nvarchar|nvarchar|Ntext|  
|xml|xml|xml|Ntext|  
|CLR UDT (> 8Kb)|udt|varbinary|Image|  
|date|date|varchar|Varchar|  
|datetime2|datetime2|varchar|Varchar|  
|datetimeoffset|datetimeoffset|varchar|Varchar|  
|time|time|varchar|Varchar|  
  
## See Also  
 [SQL Server Native Client Programming](../../relational-databases/native-client/sql-server-native-client-programming.md)   
 [Installing SQL Server Native Client](../../relational-databases/native-client/applications/installing-sql-server-native-client.md)  
  
  
