---
title: "Create User-Defined Hierarchies | Microsoft Docs"
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
# User-Defined Hierarchies - Create
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] lets you create user-defined hierarchies. A hierarchy is a collection of levels based on attributes. For example, a time hierarchy might contain the Year, Quarter, Month, Week, and Day levels. In some hierarchies, each member attribute uniquely implies the member attribute above it. This is sometimes referred to as a natural hierarchy. A hierarchy can be used by end users to browse cube data. Define hierarchies by using the Hierarchies pane of Dimension Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 When you create a user-defined hierarchy, the hierarchy might become *ragged*. A ragged hierarchy is where the logical parent member of at least one member is not in the level immediately above the member. If you have a ragged hierarchy, there are settings that control whether the missing members are visible and how to display the missing members. For more information, see [Ragged Hierarchies](../../analysis-services/multidimensional-models/user-defined-hierarchies-ragged-hierarchies.md).  
  
  
## See Also  
 [User Hierarchy Properties](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/user-hierarchies-properties.md)   
 [Level Properties - user hierarchies](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/user-hierarchies-level-properties.md)   
 [Parent-Child Dimensions](../../analysis-services/multidimensional-models/parent-child-dimension.md)  
  
  
