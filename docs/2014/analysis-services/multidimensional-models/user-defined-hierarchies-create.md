---
title: "Create User-Defined Hierarchies | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "user-defined hierarchies [Analysis Services]"
ms.assetid: 16715b85-0630-4a8e-99b0-c0d213cade26
author: minewiskan
ms.author: owend
manager: craigg
---
# Create User-Defined Hierarchies
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] lets you create user-defined hierarchies. A hierarchy is a collection of levels based on attributes. For example, a time hierarchy might contain the Year, Quarter, Month, Week, and Day levels. In some hierarchies, each member attribute uniquely implies the member attribute above it. This is sometimes referred to as a natural hierarchy. A hierarchy can be used by end users to browse cube data. Define hierarchies by using the Hierarchies pane of Dimension Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 When you create a user-defined hierarchy, the hierarchy might become *ragged*. A ragged hierarchy is where the logical parent member of at least one member is not in the level immediately above the member. If you have a ragged hierarchy, there are settings that control whether the missing members are visible and how to display the missing members. For more information, see [Ragged Hierarchies](user-defined-hierarchies-ragged-hierarchies.md).  
  
> [!NOTE]  
>  For more information about performance issues related to the design and configuration of user-defined hierarchies, see the [SQL Server 2005 Analysis Services Performance Guide](https://blogs.msdn.microsoft.com/sqlperf/2007/02/12/analysis-services-2005-performance-guide/).  
  
## See Also  
 [User Hierarchy Properties](../multidimensional-models-olap-logical-dimension-objects/user-hierarchies-properties.md)   
 [Level Properties &#91;Paved Over&#93;](../multidimensional-models-olap-logical-dimension-objects/user-hierarchies-level-properties.md)   
 [Parent-Child Hierarchy](parent-child-dimension.md)  
  
  
