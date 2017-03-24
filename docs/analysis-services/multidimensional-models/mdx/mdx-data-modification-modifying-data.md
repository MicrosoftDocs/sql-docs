---
title: "Modifying Data (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
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
  - "modifying data [MDX]"
  - "Multidimensional Expressions [Analysis Services], data modifications"
  - "MDX [Analysis Services], data modifications"
  - "data modifications [MDX]"
ms.assetid: 363b662c-b839-4971-bbd7-1842f73ce141
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDX Data Modification - Modifying Data
  Besides using Multidimensional Expressions (MDX) to retrieve and handle data from dimensions and cubes, you can use MDX to update or *writeback* dimension and cube data. These updates can be temporary, as with speculative, or "what if", analysis, or permanent, as when changes must occur based upon data analysis.  
  
 Updates to data can occur at the dimension or cube level:  
  
 **Dimension writebacks**  
 You use the [ALTER CUBE Statement (MDX)](../../../mdx/mdx-data-definition-alter-cube.md) statement to change a write-enabled dimension's data and the [REFRESH CUBE Statement (MDX)](../../../mdx/mdx-data-definition-refresh-cube.md) to reflect the deletion, creation, and updating of attribute values. You can also use the ALTER CUBE statement to perform complex operations, such as deleting a whole sub-tree in a hierarchy and promoting the children of a deleted member.  
  
 **Cube writebacks**  
 You use the [UPDATE CUBE](../../../mdx/mdx-data-manipulation-update-cube.md) statement to make updates to a write-enabled cube. The UPDATE CUBE statement lets you update a tuple with a specific value. You use the [REFRESH CUBE Statement (MDX)](../../../mdx/mdx-data-definition-refresh-cube.md) to refresh data in a client session with updated data from the server.  
  
 For more information, see [Using Cube Writebacks &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-data-modification-using-cube-writebacks.md).  
  
## See Also  
 [MDX Query Fundamentals &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-fundamentals-analysis-services.md)  
  
  