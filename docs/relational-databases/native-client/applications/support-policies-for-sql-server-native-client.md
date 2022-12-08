---
title: Support Policies
description: Learn about SQL Server Native Client supported SQL Server versions, operating systems, and support policies for ADO, BCP, ODBC, and OLE DB.
ms.date: 09/06/2022
ms.service: sql
ms.reviewer: ""
ms.custom: ""
ms.subservice: native-client
ms.topic: "reference"
ms.assetid: 09c80cf4-23e6-4027-a24f-cdb9c87af811
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Support Policies for SQL Server Native Client
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  This topic discusses how various data-access components can be used with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client (SNAC).  
  
## Server Support  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 11.0 supports connections to, [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)], and [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)].  
  
## Supported Operating System Versions  
 The following table lists which operating systems support [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client.  
  
|SQL Server Native Client version|Supported operating systems|  
|--------------------------------------|---------------------------------|  
|SQL Server Native Client (SQL Server 2005)|Microsoft Windows 2000 Service Pack 4 or later<br /><br /> Microsoft Windows Server 2003 or later<br /><br /> Microsoft Windows XP Service Pack 1 or later<br /><br /> Microsoft Windows Vista (requires [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Service Pack 2, or later)<br /><br /> Microsoft Windows Server 2008 R2 (requires [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Service Pack 2, or later)|  
|SQL Server Native Client 10.0 ([!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)])|Microsoft Windows Server 2003 Service Pack 2, or later<br /><br /> Microsoft Windows XP Service Pack 2, or later<br /><br /> Microsoft Windows Vista<br /><br /> Microsoft Windows Server 2008 R2|  
|SQL Server Native Client 10.5 ([!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)])|Microsoft Windows Server 2003 Service Pack 2, or later<br /><br /> Microsoft Windows XP Service Pack 2 or later<br /><br /> Microsoft Windows Vista<br /><br /> Microsoft Windows Server 2008 R2<br /><br /> Microsoft Windows 7|  
|SQL Server Native Client 11.0 ([!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] and [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)])|Microsoft Windows Vista<br /><br /> Microsoft Windows Server 2008 R2<br /><br /> Microsoft Windows 7<br /><br /> Microsoft Windows 8<br /><br /> Microsoft Windows Server 2012|  
  
## ADO Support Policies  
 ADO applications can use the SQLOLEDB OLE DB provider that is included with Windows if they do not require any of the features of [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later.  
  
 ADO applications can use the version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client included in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. ADO applications can also use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 11.0 (included in [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)]), but if they do so they must specify `DataTypeCompatibility=80` in the connection strings. Only features from [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] are available when `DataTypeCompatibility=80` is present in the connection strings.  
  
## BCP Support Policies  
 Beginning in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], bcp.exe supports data files that are no more than three [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] versions older than the version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in which bcp.exe shipped.  
  
## ODBC Support Policies  
 Applications should use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] ODBC driver included with the Windows operating system. You can use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver if the application is certified it for use with a specific version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client.  
  
## OLE DB Support Policies  
 Applications should use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] OLE DB provider included with the Windows operating system. You can use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider if the application is certified for use with a specific version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client.  
  
 OLE DB applications that have not been certified for use with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client can use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native client if they specify `DataTypeCompatibility=80` in their connection strings.  
  
 OLE DB applications that use OLE DB Service Components can only use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native client if they specify `DataTypeCompatibility=80` in their connection strings. However, no features added after [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] will be available in this case.  
 
 ## Support Lifecycle 
  
  SQL Server Native Client support lifecycle can be found on the [SNAC lifecycle explained blog](https://techcommunity.microsoft.com/t5/sql-server-blog/snac-lifecycle-explained/ba-p/385381). This lifecycle applies to building database applications using SQL Server Native Client. 
 
 ### Support Lifecycle exception
 
Limited support is provided for the SQL Native Client 11.0 that gets installed by SQL Server 2012 through 2019 until the SQL Server respective end-of-support lifecycles. This support exception only applies to the SNAC 11 driver that is installed and used by SQL Server components (e.g. SQL Server Engine Linked Servers). This means the driver is supported on Windows versions where SQL Server 2012 through SQL Server 2019 are supported. Do not remove the SQL Native Client provider that gets installed by SQL Server on the system. The support exception only covers critical security fixes and does not include enhancements or fixes to SQL Native Client 11.0.

This support exception does not cover using SQL Native Client 11.0 as a driver for database applications to connect to SQL Server. Any applications using SNAC 11.0 must be updated to use newer versions (see [Download ODBC Driver for SQL Server - ODBC Driver for SQL Server](../../../connect/odbc/download-odbc-driver-for-sql-server.md) and [Download Microsoft OLE DB Driver for SQL Server - OLE DB Driver for SQL Server](../../../connect/oledb/download-oledb-driver-for-sql-server.md)). 




## See Also  
 [Building Applications with SQL Server Native Client](../../../relational-databases/native-client/applications/building-applications-with-sql-server-native-client.md)  
  
