---
title: "Restricting the Query with Query and Slicer Axes (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Multidimensional Expressions [Analysis Services], axes"
  - "queries [MDX], axes"
  - "axes [MDX]"
  - "MDX [Analysis Services], axes"
ms.assetid: a64b8172-cd73-42f9-8847-52e967b9697a
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Restricting the Query with Query and Slicer Axes (MDX)
  When formulating a Multidimensional Expressions (MDX) SELECT statement, an application typically examines a cube and divides the set of hierarchies into two subsets:  
  
-   **Query axes**—the set of hierarchies from which data is retrieved for multiple members. For more information about query axes, see [Specifying the Contents of a Query Axis &#40;MDX&#41;](../../2014/analysis-services/specifying-the-contents-of-a-query-axis-mdx.md).  
  
-   **Slicer axis**—the set of hierarchies from which data is retrieved for a single member. For more information about the slicer axis, see [Specifying the Contents of a Slicer Axis &#40;MDX&#41;](../../2014/analysis-services/specifying-the-contents-of-a-slicer-axis-mdx.md).  
  
 Because query and slicer axes can be constructed from multiple hierarchies of the cube to be queried, these terms are used to differentiate the hierarchies used by the cube that is to be queried from the hierarchies created in the cube returned by an MDX query.  
  
 To see a simple example using query and slicer axes, see [Using Query and Slicer Axes in a Simple Example &#40;MDX&#41;](../../2014/analysis-services/using-query-and-slicer-axes-in-a-simple-example-mdx.md).  
  
## See Also  
 [Working with Members, Tuples, and Sets &#40;MDX&#41;](../../2014/analysis-services/working-with-members-tuples-and-sets-mdx.md)   
 [MDX Query Fundamentals &#40;Analysis Services&#41;](../../2014/analysis-services/mdx-query-fundamentals-analysis-services.md)  
  
  