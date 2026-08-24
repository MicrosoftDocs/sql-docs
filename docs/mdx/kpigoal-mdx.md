---
title: "KPIGoal (MDX)"
description: "KPIGoal (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# KPIGoal (MDX)


  Returns the member that calculates the value for the goal portion of the specified Key Performance Indicator (KPI).  
  
## Syntax  
  
```  
  
KPIGoal(KPI_Name)  
```  
  
## Arguments  
 *KPI_Name*  
 A valid string expression that specifies the name of a KPI.  
  
## Remarks  
  
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
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
