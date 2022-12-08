---
description: "Files and Version Numbers"
title: "Files and Version Numbers | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 

ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Management Objects, versions"
  - "components [SMO]"
  - "files [SMO], components"
  - "SMO [SQL Server], versions"
  - "versions [SMO]"
ms.assetid: 510907b6-e7a9-41bd-b892-d6d99a5118e1
author: "markingmyname"
ms.author: "maghan"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Files and Version Numbers
[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  All required SQL Server Management Object (SMO) components are included in the Microsoft.SqlServer.SqlManagementObjects NuGet package. SMO is implemented in several managed assemblies. You can develop SMO applications on either a client or a server.  

> > [!Important]
> > The file version of the SMO assemblies is displayed as Major.**0**.Build.Revision. But the embedded assembly version is Major.**100**.Build.Revision. This is done to keep the version of SMO used in each application separate so updates to one doesn't affect any others.
> > 
> > Because of this you should **not** install these versions of the assemblies to the Global Assembly Cache (GAC). Doing so could cause other applications, such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Studio, to break. 
  
|File|Description|  
|-----------|-----------------|  
|Microsoft.SqlServer.ConnectionInfo.dll|Contains support for connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|Microsoft.SqlServer.ServiceBrokerEnum.dll|Contains support for programming the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Service Broker. This is required only in programs that access the Service Broker.|  
|Microsoft.SqlServer.Smo.dll|Contains the most of the SMO classes.|  
|Microsoft.SqlServer.SmoExtended.dll<br /><br /> Microsoft.SqlServer.Management.Sdk.Sfc.dll<br /><br /> Microsoft.SqlServer.SqlEnum.dll|Contains support for the SMO classes.|  
|Microsoft.SqlServer.WmiEnum.dll|Contains the Windows Management Instrumentation (WMI) Provider classes. This is required only for programs that use the WMI Provider classes.|  
|Microsoft.SqlServer.RegSvrEnum.dll|Contains the Registered Server classes. This is required only for programs that use the Registered Server classes.|  
  
  
