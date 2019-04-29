---
title: "Perspectives | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "ready-only cube view"
  - "OLAP objects [Analysis Services], perspectives"
  - "storing data [Analysis Services], perspectives"
  - "perspectives [Analysis Services]"
  - "cubes [Analysis Services], perspectives"
  - "visibility [Analysis Services]"
  - "storage [Analysis Services], perspectives"
ms.assetid: b064171e-b1b4-4f32-95e5-59e1b831c4c9
author: minewiskan
ms.author: owend
manager: craigg
---
# Perspectives
  A perspective is a definition that allows users to see a cube in a simpler way. A perspective is a subset of the features of a cube. A perspective enables administrators to create views of a cube, helping users to focus on the most relevant data for them. A perspective contains subsets of all objects from a cube. A perspective cannot include elements that are not defined in the parent cube.  
  
 A simple <xref:Microsoft.AnalysisServices.Perspective> object is composed of: basic information, dimensions, measure groups, calculations, KPIs, and actions. Basic information includes the name and the default measure of the perspective. The dimensions are a subset of the cube dimensions. The measure groups are a subset of the cube measure groups. The calculations are a subset of the cube calculations. The KPIs are a subset of the cube KPIs. The actions are a subset of the cube actions.  
  
 A cube has to be updated and processed before the perspective can be used.  
  
 Cubes can be very complex objects for users to explore in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. A single cube can represent the contents of a complete data warehouse, with multiple measure groups in a cube representing multiple fact tables, and multiple dimensions based on multiple dimension tables. Such a cube can be very complex and powerful, but daunting to users who may only need to interact with a small part of the cube in order to satisfy their business intelligence and reporting requirements.  
  
 In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you can use a perspective to reduce the perceived complexity of a cube in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. A perspective defines a viewable subset of a cube that provides focused, business-specific or application-specific viewpoints on the cube. The perspective controls the visibility of objects that are contained by a cube. The following objects can be displayed or hidden in a perspective:  
  
-   Dimensions  
  
-   Attributes  
  
-   Hierarchies  
  
-   Measure groups  
  
-   Measures  
  
-   Key Performance Indicators (KPIs)  
  
-   Calculations (calculated members, named sets, and script commands)  
  
-   Actions  
  
 For example, the **Adventure Works** cube in the [!INCLUDE[ssAWDWsp](../../includes/ssawdwsp-md.md)] sample [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database contains eleven measure groups and twenty-one different cube dimensions, representing sales, sales forecasting, and financial data. A client application can directly reference the complete cube, but this viewpoint may be overwhelming to a user trying to extract basic sales forecasting information. Instead, the same user can use the **Sales Targets** perspective to limit the view of the **Adventure Works** cube to only those objects relevant to sales forecasting.  
  
 Objects in a cube that are not visible to the user through a perspective can still be directly referenced and retrieved using XML for Analysis (XMLA), Multidimensional Expressions (MDX), or Data Mining Extensions (DMX) statements. Perspectives do not restrict access to objects in a cube and should not be used as such; instead, perspectives are used to provide a better user experience while accessing a cube.  
  
 A perspective is a read-only view of the cube; objects in the cube cannot be renamed or changed by using a perspective. Similarly, the behavior or features of a cube, such as the use of visual totals, cannot be changed by using a perspective.  
  
## Security  
 Perspectives are not meant to be used as a security mechanism, but as a tool for providing a better user experience in business intelligence applications. All security for a particular perspective is inherited from the underlying cube. For example, perspectives cannot provide access to objects in a cube to which a user does not already have access. - Security for the cube must be resolved before access to objects in the cube can be provided through a perspective.  
  
  
