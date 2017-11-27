---
title: "CLEAR CALCULATIONS Statement (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CLEAR CALCULATIONS"
  - "clalculations"
  - "clear"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "clearing calculations"
  - "CLEAR CALCULATIONS statement"
  - "deleting calculations"
  - "removing calculations"
  - "calculations [Analysis Services], clearing"
  - "cubes [Analysis Services], calculations"
ms.assetid: aebec9a1-1d1d-4697-aa3f-cc2449625603
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# MDX Data Manipulation - CLEAR CALCULATIONS
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Removes all calculations from the cube and returns the cube to calculation pass 0.  
  
## Syntax  
  
```  
  
CLEAR CALCULATIONS [FROMCube_Expression]  
```  
  
## Arguments  
 *Cube_Expression*  
 A valid Multidimensional Expressions (MDX) cube expression.  
  
## Remarks  
 The **FROM** clause can be omitted when the context of the cube is known, such as in an MDX script.  
  
> [!NOTE]  
>  This statement can only be executed by a server or database administrator, or a member of a role that has access to the source data in the cube (that is, ReadSourceData=true)  
  
## See Also  
 [MDX Data Manipulation Statements &#40;MDX&#41;](../mdx/mdx-data-manipulation-statements-mdx.md)  
  
  
