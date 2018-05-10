---
title: "Cube Objects (Analysis Services - Multidimensional Data) | Microsoft Docs"
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
# Cube Objects (Analysis Services - Multidimensional Data)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
    
## Introducing Cube Objects  
 A simple <xref:Microsoft.AnalysisServices.Cube> object is composed of: basic information, dimensions, and measure groups. Basic information includes the name of the cube, the default measure of the cube, the data source, the storage mode, and others.  
  
 The Dimensions collection contains the actual set of dimensions used in the cube from the database dimensions colection. All dimensions have to be defined in the dimensions collection of the database before being referenced in the cube. Private dimensions are not available in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 Measure groups are sets of measures in the cube. A measure group is a collection of measures that have a common data source view and a common set of dimensions. A measure group is the unit of process for measures; measure groups can be processed individually and then browsed.  
  
## In this section  
  
|||  
|-|-|  
|Topic||  
|[Actions &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models/actions-analysis-services-multidimensional-data.md)||  
|[Aggregations and Aggregation Designs](../../analysis-services/multidimensional-models-olap-logical-cube-objects/aggregations-and-aggregation-designs.md)||  
|[Calculations](../../analysis-services/multidimensional-models-olap-logical-cube-objects/calculations.md)||  
|[Cube Cells &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models-olap-logical-cube-objects/cube-cells-analysis-services-multidimensional-data.md)||  
|[Cube Properties - Multidimensional Model Programming](../../analysis-services/multidimensional-models-olap-logical-cube-objects/cube-properties-multidimensional-model-programming.md)||  
|[Cube Storage &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models-olap-logical-cube-objects/cube-storage-analysis-services-multidimensional-data.md)||  
|[Cube Translations](../../analysis-services/multidimensional-models-olap-logical-cube-objects/cube-translations.md)||  
|[Dimension Relationships](../../analysis-services/multidimensional-models-olap-logical-cube-objects/dimension-relationships.md)||  
|[Key Performance Indicators &#40;KPIs&#41; in Multidimensional Models](../../analysis-services/multidimensional-models/key-performance-indicators-kpis-in-multidimensional-models.md)||  
|[Measures and Measure Groups](../../analysis-services/multidimensional-models/measures-and-measure-groups.md)||  
|[Partitions &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models-olap-logical-cube-objects/partitions-analysis-services-multidimensional-data.md)||  
|[Perspectives](../../analysis-services/multidimensional-models-olap-logical-cube-objects/perspectives.md)||  
  
  
