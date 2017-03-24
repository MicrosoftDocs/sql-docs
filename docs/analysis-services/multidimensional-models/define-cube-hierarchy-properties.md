---
title: "Define Cube Hierarchy Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "hierarchies [Analysis Services], multilevel"
  - "hierarchies [Analysis Services], cubes"
ms.assetid: 819d0a4e-7815-4332-a605-b07ca2ade6ac
caps.latest.revision: 18
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Define Cube Hierarchy Properties
  Cube hierarchy properties enable you to specify unique settings for user-defined hierarchies in cube dimensions based on the same database dimension. The following table describes the properties of a cube hierarchy.  
  
|Property|Description|  
|--------------|-----------------|  
|**Enabled**|Determines whether the hierarchy is enabled for the cube dimension.|  
|**HierarchyID**|Contains the unique identifier (ID) of the hierarchy.|  
|**OptimizedState**|Determines the level of optimization that is applied to the hierarchy. This property can have the following values:<br /><br /> **FullyOptimized**:<br />                    The instance builds indexes for the hierarchy to improve query performance. This is the default value.<br /><br /> **NotOptimized**:<br />                    The instance does not build additional indexes.|  
|**Visible**|Determines the visibility of the cube hierarchy. The default value is **True**.|  
  
## See Also  
 [User Hierarchies](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/user-hierarchies.md)  
  
  