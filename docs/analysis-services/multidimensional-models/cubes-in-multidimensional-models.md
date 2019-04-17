---
title: "Cubes in Multidimensional Models | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Cubes in Multidimensional Models
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A cube is a multidimensional structure that contains information for analytical purposes; the main constituents of a cube are dimensions and measures. Dimensions define the structure of the cube that you use to slice and dice over, and measures provide aggregated numerical values of interest to the end user. As a logical structure, a cube allows a client application to retrieve values, of measures, as if they were contained in cells in the cube; cells are defined for every possible summarized value. A cell, in the cube, is defined by the intersection of dimension members and contains the aggregated values of the measures at that specific intersection.  
  
## Benefits of Using Cubes  
 A cube provides a single place where all related data, for analysis, is stored.  
  
## Components of Cubes  
 A cube is composed of:  
  
|Element|Description|  
|-------------|-----------------|  
|Dimensions|[Dimensions in Multidimensional Models](../../analysis-services/multidimensional-models/dimensions-in-multidimensional-models.md)|  
|Measures and Measure Groups|[Create Measures and Measure Groups in Multidimensional Models](../../analysis-services/multidimensional-models/create-measures-and-measure-groups-in-multidimensional-models.md)|  
|Partitions|[Partitions in Multidimensional Models](../../analysis-services/multidimensional-models/partitions-in-multidimensional-models.md)|  
|Perspectives|[Perspectives in Multidimensional Models](../../analysis-services/multidimensional-models/perspectives-in-multidimensional-models.md)|  
|Hierarchies|[Create User-Defined Hierarchies](../../analysis-services/multidimensional-models/user-defined-hierarchies-create.md)|  
|Actions|[Actions in Multidimensional Models](../../analysis-services/multidimensional-models/actions-in-multidimensional-models.md)|  
|Key Performance Indicators (KPI)|[Key Performance Indicators &#40;KPIs&#41; in Multidimensional Models](../../analysis-services/multidimensional-models/key-performance-indicators-kpis-in-multidimensional-models.md)|  
|Calculations|[Calculations in Multidimensional Models](../../analysis-services/multidimensional-models/calculations-in-multidimensional-models.md)|  
|Translations|[Translations in Multidimensional Models &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/translations-in-multidimensional-models-analysis-services.md)|  
  
## Related Tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Create a Cube Using the Cube Wizard](../../analysis-services/multidimensional-models/create-a-cube-using-the-cube-wizard.md)|Describes how to use the Cube Wizard to define a cube, dimensions, dimension attributes, and user-defined hierarchies.|  
|[Create Measures and Measure Groups in Multidimensional Models](../../analysis-services/multidimensional-models/create-measures-and-measure-groups-in-multidimensional-models.md)|Describes how to define measure groups.|  
|[Calculations in Multidimensional Models](../../analysis-services/multidimensional-models/calculations-in-multidimensional-models.md)|Describes how to define and configure a calculation in an MDX script.|  
|[Actions in Multidimensional Models](../../analysis-services/multidimensional-models/actions-in-multidimensional-models.md)|Describes how to define and configure an action.|  
|[Perspectives in Multidimensional Models](../../analysis-services/multidimensional-models/perspectives-in-multidimensional-models.md)|Describes how to define and configure a perspective.|  
|[Defining Stored Procedures](../../analysis-services/multidimensional-models-extending-olap-stored-procedures/defining-stored-procedures.md)|Describes how to work with stored procedures.|  
  
  
