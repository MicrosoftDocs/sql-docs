---
title: "DROP CELL CALCULATION Statement (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "Calculation"
  - "DROP"
  - "DROP_CELL_CALCULATION"
  - "CELL CALCULATION"
  - "DROP CELL"
  - "cell"
  - "DROP CELL CALCULATION"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "deleting calculations"
  - "dropping calculations"
  - "removing calculations"
  - "DROP CELL CALCULATION statement"
  - "calculations [SQL Server]"
  - "cubes [Analysis Services], calculations"
ms.assetid: 77caedf4-dd96-4eac-a5e4-fd82148a44a7
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDX Data Definition - DROP CELL CALCULATION
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes the specified cell calculation.  
  
## Syntax  
  
```  
  
DROP [ SESSION ] CELL CALCULATION CURRENTCUBE | Cube_Name.CellCalc_Name  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that provides the name of a cube expression.  
  
 *CellCalc_Name*  
 A valid string expression that provides the name of the cell calculation to be dropped.  
  
## See Also  
 [CREATE CELL CALCULATION Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-cell-calculation.md)   
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
