---
title: "KPITrend (MDX) | Microsoft Docs"
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
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "KPITrend function"
ms.assetid: 0afd4513-af6e-4517-8e69-177bc7807d03
caps.latest.revision: 18
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# KPITrend (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns the normalized value that represents the trend portion of the specified Key Performance Indicator (KPI).  
  
## Syntax  
  
```  
  
KPITrend(KPI_Name)  
```  
  
## Arguments  
 *KPI_Name*  
 A valid string expression that specifies the name of the KPI.  
  
## Remarks  
 The trend value is generally a normalized value between -1 and 1.  
  
## Example  
 The following example returns the KPI value, KPI goal, KPI status, and KPI trend for the channel revenue measure for the descendants of three members of the Fiscal Year attribute hierarchy:  
  
```  
SELECT  
   { KPIValue("Channel Revenue"),   
     KPIGoal("Channel Revenue"),  
     KPIStatus("Channel Revenue"),   
     KPITrend("Channel Revenue")  
   } ON Columns,  
Descendants  
   ( { [Date].[Fiscal].[Fiscal Year].&[2002],  
       [Date].[Fiscal].[Fiscal Year].&[2003],  
       [Date].[Fiscal].[Fiscal Year].&[2004]   
     }, [Date].[Fiscal].[Fiscal Quarter]  
   ) ON Rows  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
