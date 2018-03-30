---
title: "Logical Architecture (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "Analysis Services, architecture"
  - "logical architecture [Analysis Services Multidimensional Data]"
ms.assetid: 1b9cae0a-8990-4194-af5f-a1ea5f2aff06
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Logical Architecture (Analysis Services - Multidimensional Data)
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] uses both server and client components to supply online analytical processing (OLAP) and data mining functionality for business intelligence applications:  
  
-   The server component of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] is implemented as a Microsoft Windows service. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] supports multiple instances on the same computer, with each instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] implemented as a separate instance of the Windows service.  
  
-   Clients communicate with [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] using the public standard XML for Analysis (XMLA), a SOAP-based protocol for issuing commands and receiving responses, exposed as a Web service. Client object models are also provided over XMLA, and can be accessed either by using a managed provider, such as ADOMD.NET, or a native OLE DB provider.  
  
-   Query commands can be issued using the following languages: SQL; Multidimensional Expressions (MDX), an industry standard query language for analysis; or Data Mining Extensions (DMX), an industry standard query language oriented toward data mining. Analysis Services Scripting Language (ASSL) can also be used to manage [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database objects.  
  
 [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] also supports a local cube engine that enables applications on disconnected clients to browse locally stored multidimensional data. For more information, see [Client Architecture Requirements for Analysis Services Development](../../../2014/analysis-services/dev-guide/client-architecture-requirements-for-analysis-services-development.md)  
  
## In This Section  
 **Logical Architecture Overview**  
 [Logical Architecture Overview &#40;Analysis Services - Multidimensional Data&#41;](../../../2014/analysis-services/dev-guide/logical-architecture-overview-analysis-services-multidimensional-data.md)  
  
 **Server Objects**  
 [Server Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../2014/analysis-services/dev-guide/server-objects-analysis-services-multidimensional-data.md)  
  
 **Database Objects**  
 [Database Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../2014/analysis-services/dev-guide/database-objects-analysis-services-multidimensional-data.md)  
  
 **Dimension Objects**  
 [Dimension Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../2014/analysis-services/dev-guide/dimension-objects-analysis-services-multidimensional-data.md)  
  
 **Cube Objects**  
 [Cube Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../2014/analysis-services/dev-guide/cube-objects-analysis-services-multidimensional-data.md)  
  
 **User Access Security**  
 [User Access Security Architecture](../Topic/User%20Access%20Security%20Architecture.md)  
  
## See Also  
 [Understanding Microsoft OLAP Architecture](../../../2014/analysis-services/dev-guide/understanding-microsoft-olap-architecture.md)   
 [Physical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../../2014/analysis-services/dev-guide/physical-architecture-analysis-services-multidimensional-data.md)  
  
  