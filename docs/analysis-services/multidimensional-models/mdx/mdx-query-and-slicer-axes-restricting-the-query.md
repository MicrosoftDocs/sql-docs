---
title: "Restricting the Query with Query and Slicer Axis (MDX) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MDX Query and Slicer Axes - Restricting the Query
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  When formulating a Multidimensional Expressions (MDX) SELECT statement, an application typically examines a cube and divides the set of hierarchies into two subsets:  
  
-   **Query axes**-the set of hierarchies from which data is retrieved for multiple members. For more information about query axes, see [Specifying the Contents of a Query Axis &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-and-slicer-axes-specify-the-contents-of-a-query-axis.md).  
  
-   **Slicer axis**-the set of hierarchies from which data is retrieved for a single member. For more information about the slicer axis, see [Specifying the Contents of a Slicer Axis &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-and-slicer-axes-specify-the-contents-of-a-slicer-axis.md).  
  
 Because query and slicer axes can be constructed from multiple hierarchies of the cube to be queried, these terms are used to differentiate the hierarchies used by the cube that is to be queried from the hierarchies created in the cube returned by an MDX query.  
  
 To see a simple example using query and slicer axes, see [Using Query and Slicer Axes in a Simple Example &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-and-slicer-axes-using-axes-in-a-simple-example.md).  
  
## See Also  
 [Working with Members, Tuples, and Sets &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/working-with-members-tuples-and-sets-mdx.md)   
 [MDX Query Fundamentals &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-fundamentals-analysis-services.md)  
  
  
