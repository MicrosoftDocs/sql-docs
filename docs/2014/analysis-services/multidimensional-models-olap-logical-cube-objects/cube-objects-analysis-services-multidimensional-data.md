---
title: "Cube Objects (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "cubes [Analysis Services], objects"
ms.assetid: 5cee362e-3f95-4467-bc6c-29b1518ecbf3
author: minewiskan
ms.author: owend
manager: craigg
---
# Cube Objects (Analysis Services - Multidimensional Data)
    
## Introducing Cube Objects  
 A simple <xref:Microsoft.AnalysisServices.Cube> object is composed of: basic information, dimensions, and measure groups. Basic information includes the name of the cube, the default measure of the cube, the data source, the storage mode, and others.  
  
 The Dimensions collection contains the actual set of dimensions used in the cube from the database dimensions colection. All dimensions have to be defined in the dimensions collection of the database before being referenced in the cube. Private dimensions are not available in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 Measure groups are sets of measures in the cube. A measure group is a collection of measures that have a common data source view and a common set of dimensions. A measure group is the unit of process for measures; measure groups can be processed individually and then browsed.  
  
## In this section  
  
|||  
|-|-|  
|Topic||  
|[Actions &#40;Analysis Services - Multidimensional Data&#41;](../multidimensional-models/actions-analysis-services-multidimensional-data.md)||  
|[Aggregations and Aggregation Designs](aggregations-and-aggregation-designs.md)||  
|[Calculations](calculations.md)||  
|[Cube Cells &#40;Analysis Services - Multidimensional Data&#41;](cube-cells-analysis-services-multidimensional-data.md)||  
|[Cube Properties](cube-properties-multidimensional-model-programming.md)||  
|[Cube Storage &#40;Analysis Services - Multidimensional Data&#41;](cube-storage-analysis-services-multidimensional-data.md)||  
|[Cube Translations](cube-translations.md)||  
|[Dimension Relationships](dimension-relationships.md)||  
|[Key Performance Indicators &#40;KPIs&#41; in Multidimensional Models](../multidimensional-models/key-performance-indicators-kpis-in-multidimensional-models.md)||  
|[Measures and Measure Groups](../multidimensional-models/measures-and-measure-groups.md)||  
|[Partitions &#40;Analysis Services - Multidimensional Data&#41;](partitions-analysis-services-multidimensional-data.md)||  
|[Perspectives](perspectives.md)||  
  
  
