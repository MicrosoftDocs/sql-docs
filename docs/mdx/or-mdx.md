---
title: "OR (MDX) | Microsoft Docs"
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
  - "OR"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "OR operator"
ms.assetid: 7634c08a-5b70-44cd-9422-6555fed6ae05
caps.latest.revision: 27
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# OR (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a logical disjunction on two numeric expressions.  
  
## Syntax  
  
```  
  
Expression1 OR Expression2   
```  
  
#### Parameters  
 Expression1  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
 Expression2  
 A valid MDX expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns **true** if either or both arguments evaluate to **true**; otherwise, **false**.  
  
## Remarks  
 The **OR** operator treats both arguments as Boolean values (zero, 0, as **false**; otherwise, **true**) before the operator performs the logical disjunction. The following table illustrates how the **OR** operator performs the logical disjunction.  
  
|*Expression1*|*Expression2*|Return Value|  
|-------------------|-------------------|------------------|  
|**true**|**true**|**true**|  
|**true**|**false**|**true**|  
|**false**|**true**|**true**|  
|**false**|**false**|**false**|  
  
## Example  
 The following query contains a calculated measure that returns the string “MARRIED OR MALE” if the current member on the Gender hierarchy of the Customer dimension is Male or the current member on the Marital Status hierarchy of the Customer dimension is Married; otherwise it returns the string “UNMARRIED OR FEMALE”.  
  
```  
WITH  
MEMBER MEASURES.ORDEMO AS  
IIF(  
([Customer].[Gender].CURRENTMEMBER IS [Customer].[Gender].&[M])  
OR  
([Customer].[Marital Status].CURRENTMEMBER IS [Customer].[Marital Status].&[M]),  
"MARRIED OR MALE",  
"UNMARRIED OR FEMALE")  
SELECT [Customer].[Gender].[Gender].MEMBERS ON 0,  
[Customer].[Marital Status].[Marital Status].MEMBERS ON 1  
FROM [Adventure Works]  
WHERE(MEASURES.ORDEMO)  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
