---
title: "KPIValue (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "KPIValue function"
ms.assetid: c4f8532c-4c24-4ad5-8847-4522511e0039
caps.latest.revision: 19
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# KPIValue (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the member that calculates the value of the specified Key Performance Indicator (KPI).  
  
## Syntax  
  
```  
  
KPIValue(KPI_Name)  
```  
  
## Arguments  
 *KPI_Name*  
 A valid string expression that specifies the name of the KPI.  
  
## Remarks  
  
## Example  
 The following example returns the KPI value, KPI goal, KPI status, and KPI trend for the channel revenue measure for the descendants of three members of the Fiscal Year attribute hierarchy.  
  
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
  
  
