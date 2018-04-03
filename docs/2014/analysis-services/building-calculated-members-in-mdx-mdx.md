---
title: "Building Calculated Members in MDX (MDX) | Microsoft Docs"
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
  - "MDX [Analysis Services], calculated members"
  - "calculated members [MDX]"
  - "Multidimensional Expressions [Analysis Services], calculated members"
  - "queries [MDX], calculated members"
ms.assetid: 9322e8b8-43e1-4e02-a7d1-e41a586a5bb8
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Building Calculated Members in MDX (MDX)
  In Multidimensional Expressions (MDX), a calculated member is a member that is resolved by calculating an MDX expression to return a value. This innocuous definition covers an incredible amount of ground. The ability to construct and use calculated members in an MDX query provides a great deal of manipulation capability for multidimensional data.  
  
 You can create calculated members at any point within a hierarchy. You can also create calculated members that depend not only on existing members in a cube, but also on other calculated members defined in the same MDX expression.  
  
 You can define a calculated member to have one of the following contexts:  
  
-   **Query-scoped** To create a calculated member that is defined as part of an MDX query, and therefore whose scope is limited to the query, you use the WITH keyword. You can then use the calculated member within an MDX SELECT statement. Using this approach, the calculated member created by using the WITH keyword can be changed without disturbing the SELECT statement.  
  
     For more information about how to use the WITH keyword to create calculated members, see [Creating Query-Scoped Calculated Members &#40;MDX&#41;](../../2014/analysis-services/creating-query-scoped-calculated-members-mdx.md).  
  
-   **Session-scoped** To create a calculated member whose scope is wider than the context of the query, that is, whose scope is the lifetime of the MDX session, you use the CREATE MEMBER statement. A calculated member defined by using the CREATE MEMBER statement is available to all MDX queries in that session. The CREATE MEMBER statement makes sense, for example, in a client application that consistently reuses the same set in a variety of queries.  
  
     For more information about how to use the CREATE MEMBER statement to create calculated members in a session, see [Creating Session-Scoped Calculated Members &#40;MDX&#41;](../../2014/analysis-services/creating-session-scoped-calculated-members-mdx.md).  
  
## See Also  
 [CREATE MEMBER Statement &#40;MDX&#41;](~/mdx/mdx-data-definition-create-member.md)   
 [MDX Function Reference &#40;MDX&#41;](~/mdx/mdx-function-reference-mdx.md)   
 [SELECT Statement &#40;MDX&#41;](~/mdx/mdx-data-manipulation-select.md)  
  
  