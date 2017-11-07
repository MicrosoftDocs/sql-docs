---
title: "CustomData (MDX) | Microsoft Docs"
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
  - "EXISTS"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Exists function"
ms.assetid: 61d9f5a2-6f56-4179-a39b-317c8e0a2cdd
caps.latest.revision: 18
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# CustomData (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the value of the **CustomData** connection string property if defined; otherwise, **null**.  
  
## Syntax  
  
```  
  
CustomData()  
```  
  
## Return Value  
 The **CustomData** function can retrieve the **CustomData** connection string property and pass a configuration setting to be used by Multidimensional Expressions (MDX) functions and statements, such as [UserName (MDX)](../mdx/username-mdx.md) and [CALL Statement (MDX)](../mdx/mdx-data-manipulation-call.md). For example, this function can be used in a dynamic security expression to select the allowed/denied set members for the string value in the **CustomData** connection string property.  
  
## Example  
 The following query displays the value returned by the **CustomData** function in a calculated measure:  
  
```  
WITH MEMBER [Measures].CUSTOMDATADEMO AS CUSTOMDATA()  
SELECT [Measures].CUSTOMDATADEMO ON 0  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
