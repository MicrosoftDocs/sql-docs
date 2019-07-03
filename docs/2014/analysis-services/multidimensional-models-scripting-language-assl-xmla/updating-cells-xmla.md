---
title: "Updating Cells (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "modifying cells"
  - "XMLA, cells"
  - "updating cells"
  - "cells [Analysis Services]"
  - "XML for Analysis, cells"
ms.assetid: a1c61496-36ee-4bce-98d9-d13440d349aa
author: minewiskan
ms.author: owend
manager: craigg
---
# Updating Cells (XMLA)
  You can use the [UpdateCells](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/updatecells-element-xmla) command to change the value of one or more cells in a cube enabled for cube writeback. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] stores the updated information in a separate writeback table for each partition that contains cells to be updated.  
  
> [!NOTE]  
>  The `UpdateCells` command does not support allocations during cube writeback. To use allocated writeback, you should use the [Statement](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/statement-element-xmla) command to send a Multidimensional Expressions (MDX) UPDATE statement. For more information, see [UPDATE CUBE Statement &#40;MDX&#41;](/sql/mdx/mdx-data-manipulation-update-cube).  
  
## Specifying Cells  
 The [Cell](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/cell-element-xmla) property of the `UpdateCells` command contains the cells to be updated. You identify each cell in the `Cell` property using that cell's ordinal number. Conceptually, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] numbers cells in a cube as if the cube were a *p*-dimensional array, where *p* is the number of axes. Cells are addressed in row-major order. The following illustration shows the formula for calculating the ordinal number of a cell.  
  
 ![Formula to calculate the cell ordinal position](../../../2014/analysis-services/dev-guide/media/cellordinalformula.gif "Formula to calculate the cell ordinal position")  
  
 Once you know a cell's ordinal number, you can indicate the intended value of the cell in the [Value](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/value-element-xmla) property of the [Cell](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/cell-element-xmla) property.  
  
## See Also  
 [Update Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/update-element-xmla)   
 [Developing with XMLA in Analysis Services](../multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md)  
  
  
