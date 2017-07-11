---
title: "FORE_COLOR and BACK_COLOR Contents (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
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
  - "FORE_COLOR contents"
  - "backgrounds [MDX]"
  - "cells [MDX]"
  - "colors [MDX]"
  - "storing cell color information"
  - "cell backgrounds"
  - "BACK_COLOR contents"
ms.assetid: ff8f40cb-2ac4-4fc2-9761-7f1b14c17c8c
caps.latest.revision: 25
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDX Cell Properties - FORE_COLOR and BACK_COLOR Contents
  The **FORE_COLOR** and **BACK_COLOR** cell properties store color information for the text and the background of a cell, respectively, in the Microsoft Windows operating system red-green-blue (RGB) format.  
  
 The valid range for an ordinary RGB color is zero (0) to 16,777,215 (&H00FFFFFF). The high byte of a number in this range always equals 0; the lower 3 bytes, from least to most significant byte, determine the amount of red, green, and blue, respectively. The red, green, and blue components are each represented by a number between 0 and 255 (&HFF).  
  
## See Also  
 [Using Cell Properties &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-cell-properties-using-cell-properties.md)  
  
  