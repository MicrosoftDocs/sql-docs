---
title: "Extending OLAP through personalizations | Microsoft Docs"
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
# Extending OLAP through personalizations
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Analysis Services provides many intrinsic functions for use with the Multidimensional Expressions (MDX) and Data Mining Extensions (DMX) languages. These functions are designed to accomplish everything from standard statistical calculations to traversing members in a hierarchy. However, as with any other complex and robust product, there is always the need to extend the functionality of such a product further.  
  
 Therefore, Analysis Services provides you with the ability to add assemblies and personalized extensions to an instance of the service, in order to complete your business needs whenever the standard functionality is not enough.  
  
## Assemblies  
 Assemblies enable you to extend the business functionality of MDX and DMX. You build the functionality that you want into a library, such as a dynamic link library (DLL), then add the library as an assembly to an instance of Analysis Services or to an Analysis Services database. The public methods in the library are then exposed as user-defined functions to MDX and DMX expressions, procedures, calculations, actions, and client applications.  
  
## Personalized Extensions  
 SQL Server Analysis Services personalization extensions are the foundation of the idea of implementing a plug-in architecture. Analysis Services personalization extensions are a simple and elegant modification to the existing managed assembly architecture and are exposed throughout the Analysis Services <xref:Microsoft.AnalysisServices.AdomdServer> object model, Multidimensional Expressions (MDX) syntax, and schema rowsets.  
  
## See Also  
 [Multidimensional Model Assemblies Management](../../../analysis-services/multidimensional-models/multidimensional-model-assemblies-management.md)   
 [Analysis Services Personalization Extensions](../../../analysis-services/multidimensional-models/extending-olap/analysis-services-personalization-extensions.md)  
  
  
