---
title: "Files and Version Numbers | Microsoft Docs"
ms.custom: ""
ms.date: "08/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Management Objects, versions"
  - "components [SMO]"
  - "files [SMO], components"
  - "SMO [SQL Server], versions"
  - "versions [SMO]"
ms.assetid: 510907b6-e7a9-41bd-b892-d6d99a5118e1
caps.latest.revision: 34
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Files and Version Numbers
  All required [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO) components are installed as part of an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client or server. SMO is implemented in several managed assemblies. You can develop SMO applications on either a client or a server.  
  
|Directory|File|Description|  
|---------------|----------|-----------------|  
|[!INCLUDE[ssSampPathSDK](../../includes/sssamppathsdk-md.md)]|Microsoft.SqlServer.ConnectionInfo.dll|Contains support for connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|C:\Program Files\Microsoft SQL Server\130\SDK\Assemblies\|Microsoft.SqlServer.ServiceBrokerEnum.dll|Contains support for programming the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Service Broker. This is required only in programs that access the Service Broker.|  
|C:\Program Files\Microsoft SQL Server\130\SDK\Assemblies\|Microsoft.SqlServer.Smo.dll|Contains the most of the SMO classes.|  
|C:\Program Files\Microsoft SQL Server\130\SDK\Assemblies\|Microsoft.SqlServer.SmoExtended.dll<br /><br /> Microsoft.SqlServer.Management.Sdk.Sfc.dll<br /><br /> Microsoft.SqlServer.SqlEnum.dll|Contains support for the SMO classes.|  
|C:\Program Files\Microsoft SQL Server\130\SDK\Assemblies\|Microsoft.SqlServer.WmiEnum.dll|Contains the Windows Management Instrumentation (WMI) Provider classes. This is required only for programs that use the WMI Provider classes.|  
|C:\Program Files\Microsoft SQL Server\130\SDK\Assemblies\|Microsoft.SqlServer.RegSvrEnum.dll|Contains the Registered Server classes. This is required only for programs that use the Registered Server classes.|  
  
  
