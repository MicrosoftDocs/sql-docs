---
title: "Extending OLAP through personalizations | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "Analysis Services, extensibility"
ms.assetid: 348e49fc-4390-43c1-9b6c-61b386ff4373
author: minewiskan
ms.author: owend
manager: craigg
---
# Extending OLAP through personalizations
  Microsoft  [!INCLUDE[ssASCurrent](../../../includes/ssascurrent-md.md)] supplies many intrinsic functions for use with the Multidimensional Expressions (MDX) and Data Mining Extensions (DMX) languages. These functions are designed to accomplish everything from standard statistical calculations to traversing members in a hierarchy. However, as with any other complex and robust product, there is always the need to extend the functionality of such a product further.  
  
 Therefore, Analysis Services provides you with the ability to add assemblies and personalized extensions to an instance of the service, in order to complete your business needs whenever the standard functionality is not enough.  
  
## Assemblies  
 Assemblies enable you to extend the business functionality of MDX and DMX. You build the functionality that you want into a library, such as a dynamic link library (DLL), then add the library as an assembly to an instance of Analysis Services or to an Analysis Services database. The public methods in the library are then exposed as user-defined functions to MDX and DMX expressions, procedures, calculations, actions, and client applications.  
  
## Personalized Extensions  
 SQL Server Analysis Services personalization extensions are the foundation of the idea of implementing a plug-in architecture. Analysis Services personalization extensions are a simple and elegant modification to the existing managed assembly architecture and are exposed throughout the Analysis Services <xref:Microsoft.AnalysisServices.AdomdServer> object model, Multidimensional Expressions (MDX) syntax, and schema rowsets.  
  
## See Also  
 [Multidimensional Model Assemblies Management](../multidimensional-model-assemblies-management.md)   
 [Analysis Services Personalization Extensions](analysis-services-personalization-extensions.md)  
  
  
