---
title: "Define Cube Hierarchy Properties | Microsoft Docs"
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
# Define Cube Hierarchy Properties
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Cube hierarchy properties enable you to specify unique settings for user-defined hierarchies in cube dimensions based on the same database dimension. The following table describes the properties of a cube hierarchy.  
  
|Property|Description|  
|--------------|-----------------|  
|**Enabled**|Determines whether the hierarchy is enabled for the cube dimension.|  
|**HierarchyID**|Contains the unique identifier (ID) of the hierarchy.|  
|**OptimizedState**|Determines the level of optimization that is applied to the hierarchy. This property can have the following values:<br /><br /> **FullyOptimized**:<br />                    The instance builds indexes for the hierarchy to improve query performance. This is the default value.<br /><br /> **NotOptimized**:<br />                    The instance does not build additional indexes.|  
|**Visible**|Determines the visibility of the cube hierarchy. The default value is **True**.|  
  
## See Also  
 [User Hierarchies](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/user-hierarchies.md)  
  
  
