---
title: "MeasureGroupMeasures (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MeasureGroupMeasures (MDX)


  Returns a set of measures that belongs to the specified measure group.  
  
## Syntax  
  
```  
  
MEASUREGROUPMEASURES(MeasureGroupName)  
```  
  
## Arguments  
 *MeasureGroupName*  
 A valid string expression that contains the name of the measure group from which to retrieve the set of measures.  
  
## Remarks  
 The specified string must match the measure group name exactly. Square brackets for measure group names with spaces are not required.  
  
## Example  
 The following example returns all of the measures in the Internet Sales measure group in the Adventure Works cube.  
  
```  
SELECT MeasureGroupMeasures('Internet Sales') ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
