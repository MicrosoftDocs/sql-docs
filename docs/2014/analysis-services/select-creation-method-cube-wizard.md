---
title: "Select Creation Method (Cube Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubewizard.cubedefinition.f1"
ms.assetid: 985d3b5b-7891-465b-862d-f7e75431b342
author: minewiskan
ms.author: owend
manager: craigg
---
# Select Creation Method (Cube Wizard)
  Use the **Select Creation Method** page to specify how to create the cube.  
  
## Options  
 **Use existing tables**  
 Select to create a cube by using existing tables in a data source. The wizard will guide you through the process of selecting and defining measure groups and dimensions based on existing tables to build your new cube.  
  
 **Create an empty cube**  
 Select to create an empty cube. This option is useful when you want to create everything manually, or when all measure groups in the cube are linked measure groups.  
  
> [!NOTE]  
>  This option is only available when you are working with an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project and not when you are connected directly to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
 **Generate tables in the data source**  
 Select to create a cube first and then generate new tables in the data source based on the cube definition.  
  
> [!NOTE]  
>  To use this option, you must have permission to create objects in the underlying data source.  
  
 Selecting this option will make the **Template** option available.  
  
 **Template**  
 Select the template that you want to use to create the cube. Templates provide a set of definitions oriented towards a specific business purpose.  
  
> [!NOTE]  
>  This option is only available when the **Generate tables in the data source** option has been selected.  
  
## See Also  
 [Cube Objects &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models-olap-logical-cube-objects/cube-objects-analysis-services-multidimensional-data.md)   
 [Cubes in Multidimensional Models](multidimensional-models/cubes-in-multidimensional-models.md)   
 [Dimensions in Multidimensional Models](multidimensional-models/dimensions-in-multidimensional-models.md)  
  
  
