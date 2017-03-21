---
title: "Install Analysis Services data providers (AMO, ADOMD.NET, MSOLAP) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: a7aedabc-6af9-4698-a7a4-98f894001476
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Install Analysis Services data providers (AMO, ADOMD.NET, MSOLAP)
  [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] is a version update of the Analysis Services data providers, consisting of ADOMD.Net, AMO, and MSOLAP.  
  
 For most query-based data access scenarios, you can use the existing older versions of the data providers already installed  on client systems to access tabular and multidimensional models on a SQL Server 2016 Analysis Services instance, including tabular models that use features exclusive to SQL Server 2016. As a general rule, client applications that generate queries, such as Excel, Reporting Services, or Tableau, shouldn't require the very latest data providers when accessing an Analysis Services model.  
  
 Modeling and administration tools are an exception to the rule, at least in terms of version requirements for data providers. These tools must have the data providers that were specifically built for the target server in order to manipulate objects on that server. Fortunately, the tools that ship with [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] automatically include data providers that conform to the current version of the server.  Setup for SQL Server Data Tools for Visual Studio 2015 installs data providers for SQL Server 2016 Analysis Services. Similarly, SQL Server 2016 Management Studio, which uses both AMO and ADOMD.Net, installs both data providers targeting [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
 One scenario that does call for a manual installation of a data provider  is custom applications or scripts that use Analysis Services Management Objects (AMO). This release introduces a re-factored namespace that moves major objects, such as Server and Database, to a new namespace (Microsoft.AnalysisServices.Core) that is part of the Microsoft.AnalysisServices.dll assembly. If you have custom code or scripts that use AMO, you'll need to recompile code and manually update AMO on every server and client workstation that makes a direct connection via AMO  to a SQL Server 2016 instance of Analysis Services.  
  
## Provider list  
 The following table describes each provider.  
  
||||  
|-|-|-|  
|Provider|Filename|Version|  
|Analysis Services Management Objects (AMO)|Microsoft.AnalysisServices.dll|13.0.0.0|  
|Analysis Services Core|Microsoft.AnalysisServices.Core.dll|13.0.0.0|  
|Analysis Services (ADOMD.NET)|Microsoft.AnalysisServices.AdomdClient.dll|13.0.0.0|  
|OLE DB provider for Analysis Services (MSOLAP)|MSOLAP130.dll|13.0.0.0|  
  
## Download and install data provider  
  
1.  Go to the [Feature Pack download page for SQL Server 2016](http://go.microsoft.com/fwlink/?LinkID=398150).  
  
2.  Click **Download** to enable installation of individual components.  
  
3.  For 64-bit computers, select all or some of the following (otherwise choose the x86 equivalent):  
  
    -   ENU\x64\SQL_AS_ADOMD.msi  
  
    -   ENU\x64\SQL_AS_AMO.msi  
  
    -   ENU\x64\SQL_AS_OLEDB.msi  
  
4.  Click **Next** to download the files.  
  
5.  Run each program to install the provider. ADO.MD and AMO assemblies are installed in C:\Windows\assembly\GAC_MSIL. The Analysis Services OLE DB provider is installed at C:\Program Files\Microsoft Analysis Services\AS OLEDB\130.  
  
## Verify installation  
  
1.  In File Explorer, go to C:\Windows\Assembly.  
  
2.  Right-click Microsoft.AnalysisServices and select **Properties**.  
  
3.  Click **Version** to confirm you have the most recent build.  
  
  