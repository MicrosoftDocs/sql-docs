---
title: "OLAP Engine Server Components | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "Analysis Services, architecture"
  - "ports [Analysis Services]"
  - "XML/A listener"
  - "server architecture [Analysis Services]"
ms.assetid: 5193c976-9dcd-459c-abba-8c3c44e7a7f2
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# OLAP Engine Server Components
  The server component of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] is the **msmdsrv.exe** application, which runs as a Windows service. This application consists of security components, an XML for Analysis (XMLA) listener component, a query processor component and numerous other internal components that perform the following functions:  
  
-   Parsing statements received from clients  
  
-   Managing metadata  
  
-   Handling transactions  
  
-   Processing calculations  
  
-   Storing dimension and cell data  
  
-   Creating aggregations  
  
-   Scheduling queries  
  
-   Caching objects  
  
-   Managing server resources  
  
## Architectural Diagram  
 An [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance runs as a stand-alone service and communication with the service occurs through XML for Analysis (XMLA), by using either HTTP or TCP. AMO is a layer between the user application and the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. This layer provides access to [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] administrative objects. AMO is a class library that takes commands from a client application and converts those commands into XMLA messages for the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. AMO presents [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance objects as classes to the end user application, with method members that run commands and property members that hold the data for the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] objects.  
  
 The following illustration shows the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] components architecture, including all major elements running within the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance and all user components that interact with the instance. The illustration also shows that the only way to access the instance is by using the XML for Analysis (XMLA) Listener, either by using HTTP or TCP.  
  
 ![Analysis Services System Architecture Diagram](../../../analysis-services/data-mining/media/analysisservicessystemarchitecture.gif "Analysis Services System Architecture Diagram")  
  
## XMLA Listener  
 The XMLA listener component handles all XMLA communications between [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] and its clients. The [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] **Port** configuration setting in the msmdsrv.ini file can be used to specify a port on which an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance listens. A value of 0 in this file indicates that [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] listen on the default port. Unless otherwise specified, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] uses the following default TCP ports:  
  
|Port|Description|  
|----------|-----------------|  
|2383|Default instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|2382|Redirector for other instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|Dynamically assigned at server startup|Named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
  
 See [Configure the Windows Firewall to Allow Analysis Services Access](../../../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md) for more details.  
  
## See Also  
 [Object Naming Rules &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/olap-physical/object-naming-rules-analysis-services.md)   
 [Physical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-physical/understanding-microsoft-olap-physical-architecture.md)   
 [Logical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/understanding-microsoft-olap-logical-architecture.md)  
  
  