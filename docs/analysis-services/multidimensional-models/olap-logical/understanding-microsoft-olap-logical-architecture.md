---
title: "Logical Architecture (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Understanding Microsoft OLAP Logical Architecture
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] uses both server and client components to supply online analytical processing (OLAP) and data mining functionality for business intelligence applications:  
  
-   The server component of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] is implemented as a Microsoft Windows service. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] supports multiple instances on the same computer, with each instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] implemented as a separate instance of the Windows service.  
  
-   Clients communicate with [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] using the public standard XML for Analysis (XMLA), a SOAP-based protocol for issuing commands and receiving responses, exposed as a Web service. Client object models are also provided over XMLA, and can be accessed either by using a managed provider, such as ADOMD.NET, or a native OLE DB provider.  
  
-   Query commands can be issued using the following languages: SQL; Multidimensional Expressions (MDX), an industry standard query language for analysis; or Data Mining Extensions (DMX), an industry standard query language oriented toward data mining. Analysis Services Scripting Language (ASSL) can also be used to manage [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database objects.  
  
 [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] also supports a local cube engine that enables applications on disconnected clients to browse locally stored multidimensional data. For more information, see [Client Architecture Requirements for Analysis Services Development](../../../analysis-services/multidimensional-models/olap-physical/client-architecture-requirements-for-analysis-services-development.md)  
  
## In This Section  
 **Logical Architecture Overview**  
 [Logical Architecture Overview &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/logical-architecture-overview-analysis-services-multidimensional-data.md)  
  
 **Server Objects**  
 [Server Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/server-objects-analysis-services-multidimensional-data.md)  
  
 **Database Objects**  
 [Database Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/database-objects-analysis-services-multidimensional-data.md)  
  
 **Dimension Objects**  
 [Dimension Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models-olap-logical-dimension-objects/dimension-objects-analysis-services-multidimensional-data.md)  
  
 **Cube Objects**  
 [Cube Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models-olap-logical-cube-objects/cube-objects-analysis-services-multidimensional-data.md)  
  
 **User Access Security**  
 [User Access Security Architecture](http://msdn.microsoft.com/library/71b44e10-2bd0-44f7-8de9-7c8f5b7ac082)  
  
## See Also  
 [Understanding Microsoft OLAP Architecture](../../../analysis-services/multidimensional-models/olap-physical/understanding-microsoft-olap-architecture.md)   
 [Physical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-physical/understanding-microsoft-olap-physical-architecture.md)  
  
  
