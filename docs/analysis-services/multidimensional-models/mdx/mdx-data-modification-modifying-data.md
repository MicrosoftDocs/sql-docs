---
title: "Modifying Data (MDX) | Microsoft Docs"
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
# MDX Data Modification - Modifying Data
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Besides using Multidimensional Expressions (MDX) to retrieve and handle data from dimensions and cubes, you can use MDX to update or *writeback* dimension and cube data. These updates can be temporary, as with speculative, or "what if", analysis, or permanent, as when changes must occur based upon data analysis.  
  
 Updates to data can occur at the dimension or cube level:  
  
 **Dimension writebacks**  
 You use the [ALTER CUBE Statement (MDX)](../../../mdx/mdx-data-definition-alter-cube.md) statement to change a write-enabled dimension's data and the [REFRESH CUBE Statement (MDX)](../../../mdx/mdx-data-definition-refresh-cube.md) to reflect the deletion, creation, and updating of attribute values. You can also use the ALTER CUBE statement to perform complex operations, such as deleting a whole sub-tree in a hierarchy and promoting the children of a deleted member.  
  
 **Cube writebacks**  
 You use the [UPDATE CUBE](../../../mdx/mdx-data-manipulation-update-cube.md) statement to make updates to a write-enabled cube. The UPDATE CUBE statement lets you update a tuple with a specific value. You use the [REFRESH CUBE Statement (MDX)](../../../mdx/mdx-data-definition-refresh-cube.md) to refresh data in a client session with updated data from the server.  
  
 For more information, see [Using Cube Writebacks &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-data-modification-using-cube-writebacks.md).  
  
## See Also  
 [MDX Query Fundamentals &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-fundamentals-analysis-services.md)  
  
  
