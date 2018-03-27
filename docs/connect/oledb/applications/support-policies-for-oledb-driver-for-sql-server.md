---
title: "Support Policies for OLE DB Driver for SQL Server | Microsoft Docs"
description: "Support policies for OLE DB Driver for SQL Server"
ms.date: "03/26/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "oledb|applications"
ms.reviewer: ""
ms.suite: "sql"
ms.custom: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: "jhubbard"
ms.workload: "On Demand"
---
# Support Policies for OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  This article discusses how various data-access components can be used with OLE DB Driver for SQL Server.  
  
## Server Support  
 OLE DB Driver for SQL Server supports connections to, [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)],  [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)],  [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)],[!INCLUDE[ssSQL15](../../../includes/sssql15-md.md)],  [!INCLUDE[ssSQL17](../../../includes/sssql17-md.md)], and [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)].  
  
## Supported Operating System Versions  
 The following table lists which operating systems support OLE DB Driver for SQL Server.  
  
|OLE DB Driver for SQL Server|Supported operating systems|  
|--------------------------------------|---------------------------------|   
|OLE DB Driver for SQL Server ([!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)],  [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)], [!INCLUDE[ssSQL15](../../../includes/sssql15-md.md)], and [!INCLUDE[ssSQL17](../../../includes/sssql17-md.md)] )|Microsoft Windows Vista<br /><br /> Microsoft Windows Server 2008 R2<br /><br /> Microsoft Windows 7<br /><br /> Microsoft Windows 8<br /><br /> Microsoft Windows Server 2012<br /><br />Microsoft Windows 10<br /><br />Microsoft Windows Server 2016|  
  
## ADO Support Policies  
 ADO applications can use the SQLOLEDB OLE DB provider that is included with Windows if they do not require any of the features of [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later.  
  
 ADO applications can use the OLE DB Driver for SQL Server, but if they do so they must specify `DataTypeCompatibility=80` in the connection strings. Only features from [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] are available when `DataTypeCompatibility=80` is present in the connection strings.  
  
## BCP Support Policies  
 Beginning in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], bcp.exe supports data files that are no more than three [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] versions older than the version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in which bcp.exe shipped.    
  
## OLE DB Support Policies  
 Applications should use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] OLE DB provider included with the Windows operating system. You can use the OLE DB Driver for SQL Server if the application is certified for use with a specific version of OLE DB Driver for SQL Server.  
  
 OLE DB applications that haven’t been certified for use with OLE DB Driver for SQL Server can use OLE DB Driver for SQL Server. These applications may need to specify `DataTypeCompatibility=80` in their connection strings.  
  
 OLE DB applications that use OLE DB Service Components can only use OLE DB Driver for SQL Server if they specify `DataTypeCompatibility=80` in their connection strings. However, no features added after [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] will be available in this case.  
  
## See Also  
 [Building Applications with OLE DB Driver for SQL Server](../../oledb/applications/building-applications-with-oledb-driver-for-sql-server.md)   
  
  
