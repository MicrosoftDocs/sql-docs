---
description: "MDX Data Definition Statements (MDX)"
title: "MDX Data Definition Statements (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Data Definition Statements (MDX)


  In Multidimensional Expressions (MDX), data definition statements create, drop, and manipulate multidimensional objects. The following table lists the available data definition statements.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[ALTER CUBE Statement &#40;MDX&#41;](../mdx/mdx-data-definition-alter-cube.md)|Alters the structure of a specified cube.|  
|[CREATE ACTION Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-action.md)|Creates an action that can be associated with a cube, dimension, hierarchy, or subordinate object.|  
|[CREATE CELL CALCULATION Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-cell-calculation.md)|Creates a calculation that evaluates an MDX expression over a specified set of tuples within a cube.|  
|[CREATE GLOBAL CUBE Statement  &#40;MDX&#41;](../mdx/mdx-data-definition-create-global-cube.md)|Creates and populates a locally persisted cube, based on a subcube from a cube on the server. A connection to the server is not required to connect to the locally persisted cube.|  
|[CREATE MEMBER Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-member.md)|Creates a calculated member.|  
|[CREATE SESSION CUBE Statement  &#40;MDX&#41;](../mdx/mdx-data-definition-create-session-cube.md)|Creates and populates a cube available to all queries in the same the session, based on cubes on the server.|  
|[CREATE SET Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-set.md)|Creates a named set for a specified cube.|  
|[CREATE SUBCUBE Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-subcube.md)|Redefines the cube space of a specified cube or subcube to a specified subcube.|  
|[DROP ACTION Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-action.md)|Deletes a specified action from a specified cube.|  
|[DROP CELL CALCULATION Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-cell-calculation.md)|Removes the specified cell calculation.|  
|[DROP MEMBER Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-member.md)|Removes a calculated member.|  
|[DROP SET Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-set.md)|Removes a named set.|  
|[DROP SUBCUBE Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-subcube.md)|Drops a specified subcube, reverting to the previously defined cube or subcube definition with the specified name.|  
|[REFRESH CUBE Statement &#40;MDX&#41;](../mdx/mdx-data-definition-refresh-cube.md)|Refreshes the client cache for a cube.|  
  
## See Also  
 [MDX Statement Reference &#40;MDX&#41;](../mdx/mdx-statement-reference-mdx.md)   
 [MDX Data Manipulation Statements &#40;MDX&#41;](../mdx/mdx-data-manipulation-statements-mdx.md)   
 [MDX Scripting Statements &#40;MDX&#41;](../mdx/mdx-scripting-statements-mdx.md)  
  
  
