---
title: "KPIStatus (MDX)"
description: "KPIStatus (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# KPIStatus (MDX)


  Returns a normalized value that represents the status portion of the specified Key Performance Indicator (KPI).  
  
## Syntax  
  
```  
  
KPIStatus(KPI_Name)  
```  
  
## Arguments  
 *KPI_Name*  
 A valid string expression that specifies the name of the KPI.  
  
## Remarks  
 The status value is generally a normalized value between -1 and 1.  
  
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
